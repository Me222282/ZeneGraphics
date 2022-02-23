using Zene.Windowing;
using Zene.Windowing.Base;
using Zene.Graphics;
using Zene.Structs;

namespace ImplicitFunctions
{
    public class Program : Window
    {
        static void Main()
        {
            Core.Init();

            //Game window = new Game(1000, 1000, "Work");
            Program window = new Program(800, 800, "Work");

            window.Run();

            Core.Terminate();
        }

        public Program(int width, int height, string title)
            : base(width, height, title)
        {
            _drawable = new DrawObject<double, byte>(new double[]
            {
                -1, 1,
                -1, -1,
                1, -1,
                1, 1
            }, new byte[] { 0, 1, 2, 2, 3, 0 }, 2, 0, AttributeSize.D2, BufferUsage.DrawFrequent);
            _drawable.AddAttribute(1, 0, AttributeSize.D2);

            _shader = new IFShader();
        }

        private readonly DrawObject<double, byte> _drawable;
        private readonly IFShader _shader;

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            if (dispose)
            {
                _drawable.Dispose();
                _shader.Dispose();
            }
        }

        public void Run()
        {
            // Vsync
            GLFW.SwapInterval(GLFW.True);

            while (GLFW.WindowShouldClose(Handle) == GLFW.False)
            {
                GLFW.PollEvents();
                // Clear screen black
                BaseFramebuffer.Clear(BufferBit.Colour);

                // Use shader and render object
                _shader.Bind();
                _drawable.Draw();

                GLFW.SwapBuffers(Handle);
            }

            Dispose();
        }

        protected override void OnSizeChange(SizeChangeEventArgs e)
        {
            base.OnSizeChange(e);

            BaseFramebuffer.ViewSize = new Vector2I((int)e.Width, (int)e.Height);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Keys.Escape)
            {
                Close();
                return;
            }
            if (e.Key == Keys.F5)
            {
                _shader.Recreate();
                return;
            }
        }
    }
}
