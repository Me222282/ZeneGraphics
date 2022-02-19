using Zene.Graphics;
using Zene.Windowing;
using Zene.Windowing.Base;
using Zene.Structs;

namespace PhysicsTest
{
    public class Program : Window
    {
        public static bool MultiThread { get; } = true;

        public static void Main()
        {
            Core.Init();

            //Program window = new Program(800, 500, "Work");
            SpaceWindow window = new SpaceWindow(800, 500, "Work");

            window.Run();

            Core.Terminate();
        }

        public Program(int width, int height, string title)
            : base(width, height, title)
        {
            _textDraw = new TextRenderer(13);
            _font = new FontA();

            // Enabling transparency
            Zene.Graphics.Base.GL.Enable(Zene.Graphics.Base.GLEnum.Blend);
            Zene.Graphics.Base.GL.BlendFunc(Zene.Graphics.Base.GLEnum.SrcAlpha, Zene.Graphics.Base.GLEnum.OneMinusSrcAlpha);

            OnSizeChange(new SizeChangeEventArgs(width, height));
        }

        private readonly TextRenderer _textDraw;
        private readonly Font _font;

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            if (dispose)
            {
                _textDraw.Dispose();
            }
        }

        public void Run()
        {
            // VSync
            GLFW.SwapInterval(1);

            double count = 0;

            while (GLFW.WindowShouldClose(Handle) == 0) // While window won't close
            {
                GLFW.PollEvents();

                Framebuffer.Clear(BufferBit.Colour);

                _textDraw.DrawLeftBound($"Count:{count:N3}", _font, -0.15, 0);
                count += 0.001;

                GLFW.SwapBuffers(Handle);
            }
        }

        protected override void OnSizeChange(SizeChangeEventArgs e)
        {
            base.OnSizeChange(e);

            _textDraw.Projection = Matrix4.CreateOrthographic(e.Width, e.Height, 0, -1);
            _textDraw.Model = Matrix4.CreateScale(30, 30, 1) * Matrix4.CreateTranslation(-e.Width * 0.5, e.Height * 0.5, 0);

            Framebuffer.ViewSize = new Vector2I((int)e.Width, (int)e.Height);
        }
    }
}
