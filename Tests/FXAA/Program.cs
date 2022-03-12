using System;
using Zene.Windowing;
using Zene.Windowing.Base;
using Zene.Graphics;
using Zene.Graphics.Shaders;
using Zene.Structs;

namespace FXAA
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
            _fxaa = new FXAAShader2();
            //_fxaa.TexelStep = new Vector2(1.0 / width, 1.0 / height);
            //_fxaa.FXAAOn = true;
            //_fxaa.LumaThreshold = 0.1;
            //_fxaa.MaxSpan = 16.0;
            //_fxaa.MinReduce = 128.0;
            //_fxaa.MulReduce = 8.0;
            _fxaa.Size = new Vector2(width, height);
            _shader = new BasicShader();
            
            _circle = new CircleShader();
            _circle.Size = 200;
            _circle.LineWidth = 25;
            _circle.Colour = new ColourF(1f, 1f, 1f);
            
            Framebuffer = new TextureRenderer(width, height);
            Framebuffer.SetColourAttachment(0, TextureFormat.Rgba8);
            
            _drawable = new DrawObject<double, byte>(new double[]
            {
                -1.0, -1.0, 0.0, 0.0,
                1.0, -1.0, 1.0, 0.0,
                1.0, 1.0, 1.0, 1.0,
                -1.0, 1.0, 0.0, 1.0
            }, new byte[] { 0, 1, 2, 2, 3, 0 }, 4, 0, AttributeSize.D2, BufferUsage.DrawFrequent);
            // Texture coordinates
            _drawable.AddAttribute(2, 2, AttributeSize.D2);
            
            _texture = new Texture(TextureFormat.Rgba8, "resources/Untitled.png");
            _texture.Filter = TextureSampling.Blend;
            _texture.WrapStyle = WrapStyle.EdgeClamp;
            //_fxaa.Size = new Vector2(_texture.Width, _texture.Height);
            
            base.Framebuffer.ViewSize = new Vector2I(width, height);
        }
        
        private FXAAShader2 _fxaa;
        private BasicShader _shader;
        private CircleShader _circle;
        public override TextureRenderer Framebuffer { get; }
        
        private readonly DrawObject<double, byte> _drawable;
        private Texture _texture;
        
        public void Run()
        {
            // Vsync
            GLFW.SwapInterval(1);
            
            while (GLFW.WindowShouldClose(Handle) == GLFW.False)
            {
                Framebuffer.Bind();
                // Draw box
                //_shader.Bind();
                //_shader.SetColourSource(ColourSource.None);
                //_shader.Matrix1 = Matrix4.CreateScale(0.5) * Matrix4.CreateRotationZ(Radian.Percent(0.125));
                _circle.Bind();
                _circle.Matrix1 = Matrix4.CreateScale(0.5);
                _drawable.Draw();
                
                base.Framebuffer.Bind();
                // Draw to screen
                _fxaa.Bind();
                _fxaa.TextureSlot = 0;
                //_shader.Bind();
                //_shader.SetColourSource(ColourSource.Texture);
                //_shader.SetTextureSlot(0);
                //_shader.Matrix1 = Matrix4.Identity;
                Framebuffer.GetTexture(FrameAttachment.Colour0).Bind(0);
                //_texture.Bind(0);
                _drawable.Draw();
                //Framebuffer.CopyFrameBuffer(base.Framebuffer, BufferBit.Colour, TextureSampling.Nearest);
                
                GLFW.SwapBuffers(Handle);
                GLFW.PollEvents();
            }
        }
        
        protected override void OnSizePixelChange(SizeChangeEventArgs e)
        {
            base.OnSizePixelChange(e);
            
            Framebuffer.ViewSize = e.Size;
            Framebuffer.Size = e.Size;
            
            base.Framebuffer.ViewSize = e.Size;
            
            //_fxaa.TexelStep = new Vector2(1.0 / e.Width, 1.0 / e.Height);
            _fxaa.Size = e.Size;
        }
    }
}