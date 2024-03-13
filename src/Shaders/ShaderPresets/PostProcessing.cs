using System;
using Zene.Structs;

namespace Zene.Graphics
{
    public class PostProcessing : PostShader, IDrawingContext, IRenderable
    {
        public PostProcessing(int width, int height)
        {
            _multiSFramebuffer = new TextureRendererMS(width, height, 4);
            _multiSFramebuffer.SetColourAttachment(0, TextureFormat.Rgb8);
            _multiSFramebuffer.SetDepthAttachment(TextureFormat.DepthComponent24, false);
            _framebuffer = new TextureRenderer(width, height);
            _framebuffer.SetColourAttachment(0, TextureFormat.Rgb8);

            base.Size = new Vector2I(width, height);
        }

        private readonly TextureRendererMS _multiSFramebuffer;
        private readonly TextureRenderer _framebuffer;

        public new Vector2I Size
        {
            get => _multiSFramebuffer.Size;
            set
            {
                if (value.X <= 0 || value.Y <= 0)
                {
                    throw new FrameBufferException(_framebuffer, "Framebuffers must have a width and heihgt greater that 0.");
                }

                base.Size = value;

                _multiSFramebuffer.Size = value;
                _framebuffer.Size = value;

                _multiSFramebuffer.ViewSize = value;
            }
        }
        public RectangleI View
        {
            get => _multiSFramebuffer.View;
            set
            {
                _multiSFramebuffer.View = value;
                Size = value.Size;
            }
        }

        public IFramebuffer Framebuffer => _multiSFramebuffer;
        public IShaderProgram Shader { get; set; }

        public IBox FrameBounds => new GLBox(Vector2I.Zero, Framebuffer.Properties.Size);

        public void Clear(BufferBit buffer) => _multiSFramebuffer.Clear(buffer);

        protected override void Dispose(bool dispose)
        {
            if (dispose)
            {
                _multiSFramebuffer.Dispose();
                _framebuffer.Dispose();
            }
        }

        public void OnRender(IDrawingContext context)
        {
            _multiSFramebuffer.CopyFrameBuffer(_framebuffer, BufferBit.Colour, TextureSampling.Nearest);

            context.Shader = this;

            _framebuffer.GetTexture(FrameAttachment.Colour0).Bind(0);
            TextureSlot = 0;

            context.Draw(Shapes.Square);
        }

        void IDrawingContext.PrepareDraw() { }
    }
}
