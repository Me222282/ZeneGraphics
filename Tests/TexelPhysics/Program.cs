using System;
using Zene.Graphics;
using Zene.Graphics.Base.Extensions;
using Zene.Structs;
using Zene.Windowing;
using Zene.Windowing.Base;

namespace TexelPhysics
{
    public class Program : Window
    {
        private static Vector2I _worldSize = new Vector2I(100);
        private static Vector2I _drawingSize = new Vector2I(5);

        static void Main()
        {
            Core.Init();

            Program window = new Program(500, 500, "Physics");

            window.Run();

            window.Dispose();

            Core.Terminate();
        }

        private static Vector2I[] CreateLine(Segment2 segment, char unit)
        {
            Vector2I[] output;

            Box b = segment.Bounds;

            Line2I l = new Line2I(segment);

            if (b.Height > b.Width)
            {
                // One value per row
                output = new Vector2I[(int)b.Height];

                int top = (int)b.Top;

                int i = 0;
                for (int y = (int)b.Bottom; y < top; y++)
                {
                    int x = l.GetX(y);
                    output[i] = new Vector2I(x, y);
                    i++;
                }
            }
            else
            {
                // One value per column
                output = new Vector2I[(int)b.Width];

                int right = (int)b.Right;

                int i = 0;
                for (int x = (int)b.Left; x < right; x++)
                {
                    int y = l.GetY(x);
                    output[i] = new Vector2I(x, y);
                    i++;
                }
            }

            return output;
        }
        private static void DrawLine(Segment2 segment, Colour colour, Bitmap canvas)
        {
            Box b = segment.Bounds;

            Line2I l = new Line2I(segment);

            if (b.Height > b.Width)
            {
                // One value per row
                int top = (int)b.Top;

                for (int y = (int)b.Bottom; y < top; y++)
                {
                    int x = l.GetX(y);
                    canvas.Data[x + (y * canvas.Width)] = colour;
                }
            }
            else
            {
                // One value per column
                int right = (int)b.Right;

                for (int x = (int)b.Left; x < right; x++)
                {
                    int y = l.GetY(x);
                    canvas.Data[x + (y * canvas.Width)] = colour;
                }
            }
        }
        private static void DrawLine(Segment2 segment, Colour colour, uint width, Bitmap canvas)
        {
            Box b = segment.Bounds;

            Line2I l = new Line2I(segment);

            //int offset = (int)Math.Ceiling(width * 0.5);
            int offset = (int)width / 2;

            if (b.Height > b.Width)
            {
                // One value per row
                int top = (int)b.Top;

                for (int y = (int)b.Bottom; y < top; y++)
                {
                    int x = l.GetX(y);
                    int end = (x - offset) + (int)width;
                    for (int x2 = x - offset; x2 < end; x2++)
                    {
                        canvas.Data[x2 + (y * canvas.Width)] = colour;
                    }
                }
            }
            else
            {
                // One value per column
                int right = (int)b.Right;

                for (int x = (int)b.Left; x < right; x++)
                {
                    int y = l.GetY(x);
                    int end = (y - offset) + (int)width;
                    for (int y2 = y - offset; y2 < end; y2++)
                    {
                        canvas.Data[x + (y2 * canvas.Width)] = colour;
                    }
                }
            }
        }

        public Program(int width, int height, string title)
            : base(width, height, title)
        {
            Framebuffer = new Framebuffer();

            _texture = new Texture(TextureFormat.Rgba8, new GLArray<Colour>(_worldSize.X, _worldSize.Y));
            Framebuffer[0] = _texture;

            _world = new World(_worldSize.X, _worldSize.Y);
        }

        public override Framebuffer Framebuffer { get; }
        private readonly Texture _texture;

        protected override void Dispose(bool dispose)
        {
            if (dispose)
            {
                Framebuffer.Dispose();
                _texture.Dispose();
            }
        }

        private readonly World _world;
        private Cell.Type _addingCell;
        public void Run()
        {
            // Vsync
            GLFW.SwapInterval(1);

            // While window isn't closing
            while (GLFW.WindowShouldClose(Handle) == GLFW.False)
            {
                // Set cells to mouse pos
                if (_mouseDown)
                {
                    for (int x = 0; x < _drawingSize.X; x++)
                    {
                        for (int y = 0; y < _drawingSize.Y; y++)
                        {
                            _world.OverrideCell(_worldMouse.X + x, _worldMouse.Y + y, Cell.CreateType(_addingCell));
                        }
                    }
                }

                _world.UpdateCells();

                base.Framebuffer.Clear(BufferBit.Colour);


                // Set texture data to values of cellmap
                _texture.TexSubImage2D(0, 0, 0, _texture.Width, _texture.Height, BaseFormat.Rgba, TextureData.Byte, _world.GetTextureData());
                // Copy framebuffer to the main framebuffer
                Framebuffer.CopyFrameBuffer(base.Framebuffer, BufferBit.Colour, TextureSampling.Nearest);

                // Window management
                GLFW.SwapBuffers(Handle);
                GLFW.PollEvents();
            }
        }

        private bool _mouseDown;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            _mouseDown = true;

            if (e.Button == MouseButton.Left)
            {
                if (_sKey)
                {
                    _addingCell = Cell.Type.Sand;
                    return;
                }
                if (_wKey)
                {
                    _addingCell = Cell.Type.Water;
                    return;
                }

                _addingCell = Cell.Type.Stone;
                return;
            }

            _addingCell = Cell.Type.Empty;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            _mouseDown = false;
        }
        private Vector2I _worldMouse;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            _worldMouse = new Vector2I(
                (e.Location.X / Width) * _world.Width,
                (e.Location.Y / Height) * _world.Height);
        }

        private bool _sKey = false;
        private bool _wKey = false;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Keys.W)
            {
                _wKey = true;
                return;
            }
            if (e.Key == Keys.S)
            {
                _sKey = true;
                return;
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Keys.W)
            {
                _wKey = false;
                return;
            }
            if (e.Key == Keys.S)
            {
                _sKey = false;
                return;
            }
        }
    }
}
