using System;
using System.Collections.Generic;
using System.Text;
using Zene.Graphics;
using Zene.Graphics.Shaders;
using Zene.Structs;
using Zene.Windowing;
using Zene.Windowing.Base;

namespace GUITest
{
    class Program : Window
    {
        static void Main()
        {
            // Start glfw
            Core.Init();

            Program window = new Program(800, 500, "Work");
            
            try
            {
                window.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }

            // End glfw
            Core.Terminate();
        }

        public void Run()
        {
            // Vsync
            GLFW.SwapInterval(1);

            State.DepthTesting = true;
            Zene.Graphics.Base.GL.DepthFunc(Zene.Graphics.Base.GLEnum.Less);

            while (GLFW.WindowShouldClose(Handle) == GLFW.False)
            {
                GLFW.PollEvents();

                Draw();

                GLFW.SwapBuffers(Handle);
            }

            Dispose();
        }

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            if (dispose)
            {
                _textRender.Dispose();
                _drawingBox.Dispose();
                _shader.Dispose();
                Framebuffer.Dispose();

                foreach (Panel panel in _panels)
                {
                    panel.Dispose();
                }
            }
        }

        public Program(int width, int height, string title)
            : base(width, height, title, 4.3)
        {
            _textRender = new TextRenderer(100)
            {
                AutoIncreaseCapacity = true,
                Projection = Matrix4.CreateOrthographic(10, 10, 0, -1)
            };

            _textRender2 = new TestTextRender(100)
            {
                AutoIncreaseCapacity = true,
                Projection = Matrix4.CreateOrthographic(10, 10, 0, -1)
            };

            //_font = new FontMeme("Resources/fontB.png");
            //_font = new FontA();
            _font = new IntelligentFont();

            _drawingBox = new DrawObject<double, byte>(new double[]
            {
                // Pos          Tex
                0.5, 0.5, 1,    1, 1,
                0.5, -0.5, 1,   1, 0,
                -0.5, -0.5, 1,  0, 0,
                -0.5, 0.5, 1,   0, 1
            }, new byte[] { 0, 1, 2, 2, 3, 0 }, 5, 0, AttributeSize.D3, BufferUsage.DrawFrequent);
            _drawingBox.AddAttribute((uint)BasicShader.Location.TextureCoords, 3, AttributeSize.D2);

            _shader = new BasicShader();

            Framebuffer = new TextureRenderer(width, height);
            Framebuffer.SetColourAttachment(0, TextureFormat.Rgba8);
            Framebuffer.SetDepthAttachment(TextureFormat.DepthComponent16, false);

            _panels = new List<Panel>()
            {
                new Panel(this, new Box(Vector2.Zero, new Vector2(75, 200)), _drawingBox, _shader),
                new Panel(this, new Box(Vector2.Zero, new Vector2(150, 225)), _drawingBox, _shader)
            };

            // Enabling transparency
            State.Blending = true;
            Zene.Graphics.Base.GL.BlendFunc(Zene.Graphics.Base.GLEnum.SrcAlpha, Zene.Graphics.Base.GLEnum.OneMinusSrcAlpha);

            OnSizePixelChange(new SizeChangeEventArgs(width, height));
        }

        private readonly TextRenderer _textRender;
        private readonly TestTextRender _textRender2;
        private readonly Font _font;
        private readonly StringBuilder _text = new StringBuilder();

        private readonly DrawObject<double, byte> _drawingBox;
        private readonly BasicShader _shader;

        public void BindFramebuffer() => Framebuffer.Bind();

        public override TextureRenderer Framebuffer { get; }
        private readonly List<Panel> _panels;

        private double _fontSize = 10d;
        private void Draw()
        {
            Framebuffer.Bind();

            Framebuffer.Clear(BufferBit.Colour | BufferBit.Depth);

            // Text
            _textRender2.Model = Matrix4.CreateScale(_fontSize, _fontSize, 0);
            _textRender2.DrawCentred(_text.ToString(), _font, -0.5, 0);

            double dp = 1 / _panels.Count;

            for (int i = 0; i < _panels.Count; i++)
            {
                _panels[i].Draw(dp * i);
            }

            Framebuffer.CopyFrameBuffer(Framebuffer.View, BufferBit.Colour, TextureSampling.Nearest);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e[Keys.F1])
            {
                _textRender2.Reload();
                return;
            }
            if (e.Key == Keys.Enter || e.Key == Keys.NumPadEnter)
            {
                _text.Append('\n');
                return;
            }
            if (e.Key == Keys.Tab)
            {
                _text.Append('\t');
                return;
            }
            if (e.Key == Keys.BackSpace)
            {
                // Nothing to remove
                if (_text.Length == 0) { return; }

                _text.Remove(_text.Length - 1, 1);
                return;
            }
        }
        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);

            // ' ' and '\t' arn't managed by fonts
            if ((e.Character != ' ') && (e.Character != '\t'))
            {
                // Character not supported
                if (!_font.GetCharacterData(e.Character).Supported)
                {
                    Console.WriteLine($"\'{e.Character}\' is not supported by font {_font.Name}.");
                    return;
                }
            }

            _text.Append(e.Character);
        }

        protected override void OnSizePixelChange(SizeChangeEventArgs e)
        {
            base.OnSizePixelChange(e);

            //IFrameBuffer.View(Width, Height);

            SetProjection();

            if (e.Width > 0 && e.Height > 0)
            {
                Framebuffer.Size = new Vector2I(Width, Height);
                Framebuffer.ViewSize = new Vector2I(Width, Height);
            }
        }

        public void SetProjection()
        {
            Matrix4 matrix = Matrix4.CreateOrthographic(Width, Height, 0, -2);

            _textRender.Projection = matrix;
            _textRender2.Projection = matrix;
            _shader.Matrix3 = matrix;
        }

        private Vector2 _mousePos = Vector2.One;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            _mousePos = new Vector2(e.Location.X - (Width * 0.5), (Height * 0.5) - e.Location.Y);

            MouseEventArgs panelEvent = new MouseEventArgs(_mousePos, e.Button, e.Modifier);

            // The cursor to set
            Cursor cursor = null;
            foreach (Panel panel in _panels)
            {
                Cursor c = panel.MouseMove(panelEvent, cursor == null);

                if ((c != null) && (cursor == null))
                {
                    cursor = c;
                }
            }

            CursorStyle = cursor;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MouseEventArgs panelEvent = new MouseEventArgs(_mousePos, e.Button, e.Modifier);

            foreach (Panel panel in _panels)
            {
                // Top panel
                if (panel.MouseDown(panelEvent))
                {
                    // Move panel to from of list
                    _panels.Remove(panel);
                    _panels.Insert(0, panel);
                    break;
                }
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            MouseEventArgs panelEvent = new MouseEventArgs(_mousePos, e.Button, e.Modifier);

            foreach (Panel panel in _panels)
            {
                panel.MouseUp(panelEvent);
            }
        }

        protected override void OnScroll(ScrollEventArgs e)
        {
            _fontSize += e.DeltaY;
        }
    }
}