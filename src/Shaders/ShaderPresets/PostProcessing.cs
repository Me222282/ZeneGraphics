using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public class PostProcessing : PostShader, IFramebuffer
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

        FramebufferProperties IFramebuffer.Properties => _multiSFramebuffer.Properties;

        public FrameTarget Binding => _multiSFramebuffer.Binding;

        public new Vector2I Size
        {
            get => base.Size;
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

        bool IFramebuffer.LockedState => _multiSFramebuffer.LockedState;
        Viewport IFramebuffer.Viewport => _multiSFramebuffer.Viewport;
        DepthState IFramebuffer.DepthState => _multiSFramebuffer.DepthState;
        Scissor IFramebuffer.Scissor => _multiSFramebuffer.Scissor;


        public FrameDrawTarget ReadBuffer
        {
            get => _multiSFramebuffer.ReadBuffer;
            set => _multiSFramebuffer.ReadBuffer = value;
        }
        public FrameDrawTarget[] DrawBuffers
        {
            get => _multiSFramebuffer.DrawBuffers;
            set => _multiSFramebuffer.DrawBuffers = value;
        }

        /// <summary>
        /// Gets or sets a colour buffer as a destination for draw calls.
        /// </summary>
        [OpenGLSupport(2.0)]
        public FrameDrawTarget DrawBuffer
        {
            get => _multiSFramebuffer.DrawBuffers[0];
            set
            {
                _multiSFramebuffer.DrawBuffers = new FrameDrawTarget[] { value };
            }
        }

        /// <summary>
        /// The clear colour that is used when <see cref="Clear(BufferBit)"/> is called.
        /// </summary>
        public ColourF ClearColour
        {
            get => _multiSFramebuffer.ClearColour;
            set => _multiSFramebuffer.ClearColour = value;
        }
        /// <summary>
        /// The depth value that is used when <see cref="Clear(BufferBit)"/> is called.
        /// </summary>
        public double ClearDepth
        {
            get => _multiSFramebuffer.ClearDepth;
            set => _multiSFramebuffer.ClearDepth = value;
        }
        /// <summary>
        /// The stencil value that is used when <see cref="Clear(BufferBit)"/> is called.
        /// </summary>
        public int CLearStencil
        {
            get => _multiSFramebuffer.ClearStencil;
            set => _multiSFramebuffer.ClearStencil = value;
        }

        public void Clear(BufferBit buffer) => _multiSFramebuffer.Clear(buffer);

        bool IFramebuffer.Validate() => throw new NotSupportedException();

        public new void Bind() => _multiSFramebuffer.Bind();
        public void Bind(FrameTarget target) => _multiSFramebuffer.Bind(target);
        public new void Unbind() => _multiSFramebuffer.Unbind();

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            if (dispose)
            {
                _multiSFramebuffer.Dispose();
                _framebuffer.Dispose();
            }
        }

        public void Draw(IFramebuffer destination = null)
        {
            _multiSFramebuffer.CopyFrameBuffer(_framebuffer, BufferBit.Colour, TextureSampling.Nearest);

            if (destination == null)
            {
                BaseFramebuffer.Bind(FrameTarget.FrameBuffer);
            }
            else
            {
                destination.Bind(FrameTarget.FrameBuffer);
            }

            GL.Disable(GLEnum.DepthTest);

            base.Bind();
            _framebuffer.GetTexture(FrameAttachment.Colour0).Bind(0);
            TextureSlot = 0;

            Shapes.Square.Draw();

            GL.BindTexture(GLEnum.Texture2d, null);
            GL.UseProgram(null);
        }

        IProperties IGLObject.Properties => Properties;
    }
}
