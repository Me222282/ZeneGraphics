using System;
using Zene.Structs;

namespace Zene.Graphics
{
    public class PostProcessing : IDrawingContext, IRenderable
    {
        public PostProcessing(int width, int height)
        {
            _multiSFramebuffer = new TextureRendererMS(width, height, 4);
            _multiSFramebuffer.SetColourAttachment(0, TextureFormat.Rgb8);
            _multiSFramebuffer.SetDepthAttachment(TextureFormat.DepthComponent24, false);
            _framebuffer = new TextureRenderer(width, height);
            _framebuffer.SetColourAttachment(0, TextureFormat.Rgb8);
            _multiSFramebuffer.DepthState = new DepthState() { Testing = true };

            _ps = new PostShader
            {
                Texture = _framebuffer.GetTexture(FrameAttachment.Colour0),
                Size = new Vector2I(width, height)
            };
        }

        private readonly TextureRendererMS _multiSFramebuffer;
        private readonly TextureRenderer _framebuffer;
        private readonly PostShader _ps;

        public Vector2I Size
        {
            get => _multiSFramebuffer.Size;
            set
            {
                if (value.X <= 0 || value.Y <= 0)
                {
                    throw new FrameBufferException(_framebuffer, "Framebuffers must have a width and height greater that 0.");
                }

                _ps.Size = value;

                _multiSFramebuffer.Size = value;
                _framebuffer.Size = value;

                _multiSFramebuffer.ViewSize = value;
            }
        }

        public IFramebuffer Framebuffer => _multiSFramebuffer;
        public IDrawingShader Shader { get; set; }

        public IBox FrameBounds => new GLBox(Vector2I.Zero, Framebuffer.Properties.Size);

        public IMatrix Projection { get; set; } = Matrix.Identity;
        public IMatrix View { get; set; } = Matrix.Identity;
        public IMatrix Model { get; set; } = Matrix.Identity;

        public bool Pixelated
        {
            get => _ps.Pixelated;
            set => _ps.Pixelated = value;
        }
        public Vector2 PixelateSize
        {
            get => _ps.PixelateSize;
            set => _ps.PixelateSize = value;
        }
        public bool GreyScale
        {
            get => _ps.GreyScale;
            set => _ps.GreyScale = value;
        }
        public bool InvertedColour
        {
            get => _ps.InvertedColour;
            set => _ps.InvertedColour = value;
        }
        public bool UseKernel
        {
            get => _ps.UseKernel;
            set => _ps.UseKernel = value;
        }
        public double[] Kernel
        {
            get => _ps.Kernel;
            set => _ps.Kernel = value;
        }
        public double KernelOffset
        {
            get => _ps.KernelOffset;
            set => _ps.KernelOffset = value;
        }

        public void OnRender(IDrawingContext context)
        {
            _multiSFramebuffer.CopyFrameBuffer(_framebuffer, BufferBit.Colour, TextureSampling.Nearest);

            context.Shader = _ps;
            context.Draw(Shapes.Square);
        }
    }
}
