using Zene.Graphics;
using Zene.Windowing;
using Zene.Windowing.Base;
using Zene.Graphics.Shaders;
using Zene.Structs;
using Zene.Evolution;

namespace EvolutionTest
{
    public class WindowL : Window
    {
        private readonly int _worldSize;
        private readonly int _lifeforms;
        private readonly int _genLength;

        public WindowL(int width, int height, string title, int lifeforms, int brainSize, int worldSize, int genLength)
            : base(width, height, title, 4.3, new WindowInitProperties()
            {
                // Anti aliasing
                Samples = 4
            })
        {
            _lifeforms = lifeforms;
            _worldSize = worldSize;
            _genLength = genLength;

            _shader = new BasicShader();

            _lifeGraphics = new DrawObject<Vector2, byte>(new Vector2[]
                {
                    new Vector2(-0.5, 0.25),
                    new Vector2(-0.25, 0.5),
                    new Vector2(0.25, 0.5),
                    new Vector2(0.5, 0.25),
                    new Vector2(0.5, -0.25),
                    new Vector2(0.25, -0.5),
                    new Vector2(-0.25, -0.5),
                    new Vector2(-0.5, -0.25)
                }, new byte[]
                {
                    0, 1, 2,
                    0, 2, 3,
                    0, 3, 4,
                    0, 4, 7,
                    4, 5, 6,
                    4, 6, 7
                }, 1, 0, AttributeSize.D2, BufferUsage.DrawFrequent);

            _world = new World(_lifeforms, brainSize, _worldSize, _worldSize);

            // Set Framebuffer's clear colour to light-grey
            BaseFramebuffer.ClearColour = new Colour(225, 225, 225);

            OnSizePixelChange(new SizeChangeEventArgs(width, height));

            // Setup propper alpha channel support
            //Zene.Graphics.GL4.GL.Enable(Zene.Graphics.GL4.GLEnum.Blend);
            //Zene.Graphics.GL4.GL.BlendFunc(Zene.Graphics.GL4.GLEnum.SrcAlpha, Zene.Graphics.GL4.GLEnum.OneMinusSrcAlpha);
        }

        private readonly BasicShader _shader;
        private readonly DrawObject<Vector2, byte> _lifeGraphics;

        public void Run(bool vsync, int delay)
        {
            if (vsync)
            {
                GLFW.SwapInterval(1);
            }
            else
            {
                GLFW.SwapInterval(0);
            }

            int counter = 0;

            while (GLFW.WindowShouldClose(Handle) == 0) // While window shouldn't close
            {
                if (counter >= _genLength)
                {
                    counter = 0;
                    _world = _world.NextGeneration(_lifeforms, Program.CheckLifeform);
                }

                Update();

                // Manage window input and output
                GLFW.SwapBuffers(Handle);
                GLFW.PollEvents();

                if (delay != 0)
                {
                    System.Threading.Thread.Sleep(delay);
                }

                counter++;
            }

            Dispose();
        }

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            if (dispose)
            {
                _shader.Dispose();
                _lifeGraphics.Dispose();
            }
        }

        private World _world;

        private void Update()
        {
            _shader.Bind();
            _shader.SetColourSource(ColourSource.UniformColour);

            // Shift so (0, 0) is in the bottom-left corner and increase the size of the drawn objects
            _shader.Matrix2 = Matrix4.CreateTranslation((_worldSize / -2) + 0.5, (_worldSize / -2) + 0.5, 0);

            // Clear screen light-grey
            Framebuffer.Clear(BufferBit.Colour);

            // Update and draw all lifeforms
            _world.UpdateDraw(DrawLifeform);
        }

        private void DrawLifeform(Lifeform lifeform)
        {
            _shader.SetDrawColour(lifeform.Colour);
            _shader.Matrix1 = Matrix4.CreateTranslation(lifeform.Location.X, lifeform.Location.Y, 0);
            _lifeGraphics.Draw();
        }

        protected override void OnSizePixelChange(SizeChangeEventArgs e)
        {
            base.OnSizePixelChange(e);

            // Set drawing view
            Framebuffer.ViewSize = new Vector2I((int)e.Width, (int)e.Height);

            int w;
            int h;

            if (e.Height > e.Width)
            {
                w = _worldSize;
                h = (int)((e.Height / e.Width) * _worldSize);
            }
            else // Width is bigger
            {
                h = _worldSize;
                w = (int)((e.Width / e.Height) * _worldSize);
            }

            _shader.Matrix3 = Matrix4.CreateOrthographic(w, h, -10, 10);
        }
    }
}
