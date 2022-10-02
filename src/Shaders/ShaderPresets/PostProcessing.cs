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

            _drawingObject = new DrawObject<Vector2I, byte>(new Vector2I[]
            {
                new Vector2I(-1, -1), new Vector2I(0, 0),
                new Vector2I(1, -1), new Vector2I(1, 0),
                new Vector2I(1, 1), new Vector2I(1, 1),
                new Vector2I(-1, 1), new Vector2I(0, 1)
            }, new byte[] { 0, 1, 2, 2, 3, 0 }, 2, 0, AttributeSize.D2, BufferUsage.DrawFrequent);
            // Texture coordinate attribute
            _drawingObject.AddAttribute((uint)Location.TextureCoords, 1, AttributeSize.D2);
        }

        private readonly TextureRendererMS _multiSFramebuffer;
        private readonly TextureRenderer _framebuffer;

        private readonly DrawObject<Vector2I, byte> _drawingObject;

        FramebufferProperties IFramebuffer.Properties => ((IFramebuffer)_multiSFramebuffer).Properties;

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
        Vector2I IFramebuffer.ViewSize
        {
            get => Size;
            set => Size = value;
        }

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

        public new void Dispose()
        {
            base.Dispose();

            _multiSFramebuffer.Dispose();
            _framebuffer.Dispose();

            _drawingObject.Dispose();
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

            GL.UseProgram(this);
            _framebuffer.GetTexture(FrameAttachment.Colour0).Bind(0);
            SetTextureSlot(0);

            _drawingObject.Draw();

            GL.BindTexture(GLEnum.Texture2d, null);
            GL.UseProgram(null);
        }
    }
}
