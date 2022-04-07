using System;
using Zene.Windowing;
using Zene.Windowing.Base;
using Zene.Graphics;
using Zene.Structs;

namespace CustomConsole
{
    class Program : Window
    {
        static void Main()
        {
            Core.Init();
            
            Program window = new Program(800, 500, "Work");

            window.Run();
            
            Core.Terminate();
        }
        
        public Program(int width, int height, string title)
         : base(width, height, title)
        {
            _textRender = new TextRenderer(40);
            _fontA = new FontA();

            // Opacity
            State.Blending = true;
            Zene.Graphics.Base.GL.BlendFunc(Zene.Graphics.Base.GLEnum.SrcAlpha, Zene.Graphics.Base.GLEnum.OneMinusSrcAlpha);

            OnSizePixelChange(new SizeChangeEventArgs(width, height));
        }

        private readonly TextRenderer _textRender;
        private readonly Font _fontA;

        private double _margin = 5;
        private double _charSize = 15;

        public void Run()
        {
            // Vsync
            GLFW.SwapInterval(1);
            
            while (GLFW.WindowShouldClose(Handle) == GLFW.False)
            {
                Framebuffer.Clear(BufferBit.Colour);

                _textRender.Model = Matrix4.CreateScale(_charSize) *
                    Matrix4.CreateTranslation((Width * -0.5) + _margin, (Height * 0.5) - _margin, 0d);

                _textRender.DrawLeftBound("test", _fontA, 0.15, 0d);

                GLFW.SwapBuffers(Handle);
                GLFW.PollEvents();
            }
        }
        
        protected override void OnSizePixelChange(SizeChangeEventArgs e)
        {
            base.OnSizePixelChange(e);
            
            Framebuffer.ViewSize = e.Size;

            _textRender.Projection = Matrix4.CreateOrthographic(e.Width, e.Height, 0d, 1d);
        }
    }
}