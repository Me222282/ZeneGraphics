using System;
using Zene.Graphics.OpenGL;
using Zene.Graphics.OpenGL.Abstract3;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that manages all components of a multisample framebuffer.
    /// </summary>
    [OpenGLSupport(3.2)]
    public class TextureRendererMS : IFrameBuffer
    {
        /// <summary>
        /// Creats a multismaple framebuffer from set parameters.
        /// </summary>
        /// <param name="width">The width of the framebuffer attachments.</param>
        /// <param name="height">The height of the framebuffer attachments.</param>
        /// <param name="samples">The number of samples each framebuffer attachment has.</param>
        public TextureRendererMS(int width, int height, int samples)
        {
            if (width <= 0 || height <= 0)
            {
                throw new FrameBufferException(this, "Framebuffers must have a width and heihgt greater that 0.");
            }

            _framebuffer = new FrameBufferGL();

            Width = width;
            Height = height;
            Samples = samples;

            _view = new RectangleI(0, 0, width, height);

            _colourAttachs = new Texture2DMultisample[IFrameBuffer.MaxColourAttach()];
        }

        public uint Id => _framebuffer.Id;
        public FrameTarget Binding => _framebuffer.Binding;
        private readonly FrameBufferGL _framebuffer;

        /// <summary>
        /// The width of the framebuffer attachments.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// The height of the framebuffer attachments.
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// The number of samples each framebuffer attachment has.
        /// </summary>
        public int Samples { get; }
        public bool FixedSampleLocations { get; set; } = true;

        private RectangleI _view;
        /// <summary>
        /// The viewport for this framebuffer.
        /// </summary>
        public RectangleI View
        {
            get
            {
                return _view;
            }
            set
            {
                _view = value;

                if (this.Bound())
                {
                    GL.Viewport(_view.X, _view.Y, _view.Width, _view.Height);
                }
            }
        }
        /// <summary>
        /// The location of the viewport for this framebuffer.
        /// </summary>
        public Vector2I ViewLocation
        {
            get
            {
                return _view.Location;
            }
            set
            {
                _view.Location = value;

                if (this.Bound())
                {
                    GL.Viewport(_view.X, _view.Y, _view.Width, _view.Height);
                }
            }
        }
        /// <summary>
        /// The size of the viewport for this framebuffer.
        /// </summary>
        public Vector2I ViewSize
        {
            get
            {
                return _view.Size;
            }
            set
            {
                _view.Size = value;

                if (this.Bound())
                {
                    GL.Viewport(_view.X, _view.Y, _view.Width, _view.Height);
                }
            }
        }

        [OpenGLSupport(3.0)]
        public void Bind(FrameTarget target)
        {
            GL.Viewport(_view.X, _view.Y, _view.Width, _view.Height);

            _framebuffer.Bind(target);
        }
        void IFrameBuffer.Bind(FrameTarget target) => _framebuffer.Bind(target);
        [OpenGLSupport(3.0)]
        public void Bind()
        {
            GL.Viewport(_view.X, _view.Y, _view.Width, _view.Height);

            _framebuffer.Bind();
        }
        void IBindable.Bind() => _framebuffer.Bind();
        [OpenGLSupport(3.0)]
        public void UnBind()
        {
            _framebuffer.UnBind();
        }

        private bool _disposed = false;
        [OpenGLSupport(3.0)]
        public void Dispose()
        {
            if (_disposed) { return; }

            _framebuffer.Dispose();
            // Dispose of colour attachments
            foreach (Texture2DMultisample texture in _colourAttachs)
            {
                if (texture != null)
                {
                    texture.Dispose();
                }
            }
            // Dispose of depth attachment
            if (_depthTex != null) { _depthTex.Dispose(); }
            if (_depthRen != null) { _depthRen.Dispose(); }

            _disposed = true;
            GC.SuppressFinalize(this);
        }
        [OpenGLSupport(3.0)]
        public bool Validate()
        {
            return _framebuffer.Validate();
        }
        /// <summary>
        /// Gets the completion status of this framebuffer.
        /// </summary>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public FrameBufferStatus Status => _framebuffer.CheckStatus();

        private readonly Texture2DMultisample[] _colourAttachs;
        private Texture2DMultisample _depthTex;
        private RenderbufferGL _depthRen;

        /// <summary>
        /// Sets the colour attachment at <paramref name="attachment"/>.
        /// </summary>
        /// <param name="attachment">The colour attachment to set.</param>
        /// <param name="intFormat">The internal format of the texture to create.</param>
        [OpenGLSupport(3.2)]
        public void SetColourAttachment(int attachment, TextureFormat intFormat)
        {
            if (intFormat.IsDepth())
            {
                throw new FrameBufferException(this, "Colour attachments shouldn't have depth type internal formats.");
            }

            // Dispose of old texture
            if (_colourAttachs[attachment] != null)
            {
                _colourAttachs[attachment].Dispose();
            }

            Texture2DMultisample texture = new Texture2DMultisample(intFormat);
            texture.CreateData(Width, Height, Samples, FixedSampleLocations);

            _framebuffer.FramebufferTexture2D(texture, (FrameAttachment)((int)FrameAttachment.Colour0 + attachment));
            _colourAttachs[attachment] = texture;

            texture.UnBind();
        }
        /// <summary>
        /// Sets the depth attachment of this framebuffer.
        /// </summary>
        /// <param name="intFormat">The internal format of the attachment.</param>
        /// <param name="isTexture">Determines whether is store the depth attachment as a texture or renderbuffer.</param>
        [OpenGLSupport(3.2)]
        public void SetDepthAttachment(TextureFormat intFormat, bool isTexture)
        {
            if (!intFormat.IsDepth())
            {
                throw new FrameBufferException(this, "Depth attachments must have depth type internal formats.");
            }

            // Dispose of old texture
            if (_depthTex != null) { _depthTex.Dispose(); }
            // Dispose of old renderbuffer
            if (_depthRen != null) { _depthRen.Dispose(); }

            if (isTexture)
            {
                Texture2DMultisample texture = new Texture2DMultisample(intFormat);
                texture.CreateData(Width, Height, Samples, FixedSampleLocations);

                _framebuffer.FramebufferTexture2D(texture,
                    // The internal format contains a stencil attachment
                    intFormat.HasStencil() ? FrameAttachment.DepthStencil : FrameAttachment.Depth);

                _depthTex = texture;
                texture.UnBind();
                return;
            }

            RenderbufferGL renderbuffer = new RenderbufferGL();
            renderbuffer.RenderbufferStorageMultisample(intFormat, Samples, Width, Height);

            _framebuffer.FramebufferRenderbuffer(renderbuffer,
                // The internal format contains a stencil attachment
                intFormat.HasStencil() ? FrameAttachment.DepthStencil : FrameAttachment.Depth);

            _depthRen = renderbuffer;
            renderbuffer.UnBind();
        }

        /// <summary>
        /// Gets the texture that corresponds to an attachment.
        /// </summary>
        /// <param name="attachment">The attachment to retrieve.</param>
        /// <returns></returns>
        [OpenGLSupport(3.2)]
        public Texture2DMultisample GetTexture(FrameAttachment attachment)
        {
            // Depth attachment
            if (attachment == FrameAttachment.DepthStencil || attachment == FrameAttachment.Depth)
            {
                return _depthTex;
            }
            // Colour attachment
            return _colourAttachs[(int)attachment - (int)FrameAttachment.Colour0];
        }
        /// <summary>
        /// Returns the renderbuffer for the depth component.
        /// </summary>
        /// <returns></returns>
        [OpenGLSupport(3.2)]
        public IRenderbuffer DepthRenderbuffer => _depthRen;

        /// <summary>
        /// Removes a specific attachment.
        /// </summary>
        /// <param name="attachment">The attachment to remove.</param>
        [OpenGLSupport(3.0)]
        public void RemoveAttachment(FrameAttachment attachment)
        {
            // Depth attachment
            if (attachment == FrameAttachment.DepthStencil || attachment == FrameAttachment.Depth)
            {
                // Texture attachment
                if (_depthTex != null)
                {
                    _depthTex.Dispose();
                    _depthTex = null;
                    _framebuffer.FramebufferTexture2D(default, attachment);
                    return;
                }
                // Renderbuffer attachment
                if (_depthRen != null)
                {
                    _depthRen.Dispose();
                    _depthRen = null;
                    _framebuffer.FramebufferRenderbuffer(default, attachment);
                    return;
                }

                // No depth attachment
                return;
            }
            // Colour attachment
            int colourIndex = (int)attachment - (int)FrameAttachment.Colour0;

            // Colour attachment exists
            if (_colourAttachs[colourIndex] != null)
            {
                _colourAttachs[colourIndex].Dispose();
                _colourAttachs[colourIndex] = null;
                _framebuffer.FramebufferTexture2D(default, attachment);
            }
        }

        /// <summary>
        /// Sets the size of all the attachments and thus this framebuffer.
        /// </summary>
        [OpenGLSupport(3.2)]
        public Vector2I Size
        {
            get => new Vector2I(Width, Height);
            set
            {
                if (value.X <= 0 || value.Y <= 0)
                {
                    throw new FrameBufferException(this, "Framebuffers must have a width and heihgt greater that 0.");
                }

                Width = value.X;
                Height = value.Y;

                // Colour attachments
                foreach (Texture2DMultisample texture in _colourAttachs)
                {
                    // Make sure texture exists
                    if (texture != null)
                    {
                        texture.CreateData(Width, Height, Samples, FixedSampleLocations);
                    }
                }
                // Unbind texture
                State.NullBind(Target.Texture2D);

                // Depth attachment
                if (_depthTex != null)
                {
                    _depthTex.CreateData(Width, Height, Samples, FixedSampleLocations);
                    _depthTex.UnBind();
                    return;
                }

                if (_depthRen != null)
                {
                    _depthRen.RenderbufferStorageMultisample(Samples, Width, Height);
                    _depthRen.UnBind();
                    return;
                }

                // depth isn't assigned
            }
        }

        /// <summary>
        /// Copies the data from this framebuffer to <paramref name="destination"/>.
        /// </summary>
        /// <param name="destination">The framebuffer to copy to.</param>
        /// <param name="dstBox">The area to write data to.</param>
        /// <param name="mask">The attachments to copy.</param>
        /// <param name="filter">The quality of the copy.</param>
        [OpenGLSupport(3.0)]
        public void CopyFrameBuffer(IFrameBuffer destination, IBox dstBox, BufferBit mask, TextureSampling filter)
        {
            _framebuffer.BlitBuffer(destination, new Rectangle(0, 0, Width, Height), dstBox, mask, filter);
        }
        /// <summary>
        /// Copies the data from this framebuffer to <paramref name="destination"/>.
        /// </summary>
        /// <param name="destination">The framebuffer to copy to.</param>
        /// <param name="dstBox">The area to write data to.</param>
        /// <param name="box">The area to referance data from.</param>
        /// <param name="mask">The attachments to copy.</param>
        /// <param name="filter">The quality of the copy.</param>
        [OpenGLSupport(3.0)]
        public void CopyFrameBuffer(IFrameBuffer destination, IBox dstBox, IBox box, BufferBit mask, TextureSampling filter)
        {
            _framebuffer.BlitBuffer(destination, box, dstBox, mask, filter);
        }
        /// <summary>
        /// Copies the data from this framebuffer to <paramref name="destination"/>.
        /// </summary>
        /// <param name="destination">The framebuffer to copy to.</param>
        /// <param name="mask">The attachments to copy.</param>
        /// <param name="filter">The quality of the copy.</param>
        [OpenGLSupport(3.0)]
        public void CopyFrameBuffer(FrameBuffer destination, BufferBit mask, TextureSampling filter)
        {
            _framebuffer.BlitBuffer(destination, new Rectangle(0, 0, Width, Height), new Rectangle(0, 0, destination.Width, destination.Height), mask, filter);
        }
        /// <summary>
        /// Copies the data from this framebuffer to <paramref name="destination"/>.
        /// </summary>
        /// <param name="destination">The framebuffer to copy to.</param>
        /// <param name="mask">The attachments to copy.</param>
        /// <param name="filter">The quality of the copy.</param>
        [OpenGLSupport(3.0)]
        public void CopyFrameBuffer(TextureRenderer destination, BufferBit mask, TextureSampling filter)
        {
            _framebuffer.BlitBuffer(destination, new Rectangle(0, 0, Width, Height), new Rectangle(0, 0, destination.Width, destination.Height), mask, filter);
        }
        /// <summary>
        /// Copies the data from this framebuffer to <paramref name="destination"/>.
        /// </summary>
        /// <param name="destination">The framebuffer to copy to.</param>
        /// <param name="mask">The attachments to copy.</param>
        /// <param name="filter">The quality of the copy.</param>
        [OpenGLSupport(3.0)]
        public void CopyFrameBuffer(TextureRendererMS destination, BufferBit mask, TextureSampling filter)
        {
            _framebuffer.BlitBuffer(destination, new Rectangle(0, 0, Width, Height), new Rectangle(0, 0, destination.Width, destination.Height), mask, filter);
        }

        /// <summary>
        /// Copies the data from this framebuffer to the OpenGL Context's framebuffer.
        /// </summary>
        /// <param name="dstBox">The area to write data to.</param>
        /// <param name="mask">The attachments to copy.</param>
        /// <param name="filter">The quality of the copy.</param>
        [OpenGLSupport(3.0)]
        public void CopyFrameBuffer(IBox dstBox, BufferBit mask, TextureSampling filter)
        {
            _framebuffer.BlitBuffer(null, new Rectangle(0, 0, Width, Height), dstBox, mask, filter);
        }
        /// <summary>
        /// Copies the data from this framebuffer to OpenGL Context's framebuffer.
        /// </summary>
        /// <param name="dstBox">The area to write data to.</param>
        /// <param name="box">The area to referance data from.</param>
        /// <param name="mask">The attachments to copy.</param>
        /// <param name="filter">The quality of the copy.</param>
        [OpenGLSupport(3.0)]
        public void CopyFrameBuffer(IBox dstBox, IBox box, BufferBit mask, TextureSampling filter)
        {
            _framebuffer.BlitBuffer(null, box, dstBox, mask, filter);
        }

        /// <summary>
        /// Sets the buffers to be drawn to.
        /// </summary>
        /// <param name="buffers"></param>
        [OpenGLSupport(3.0)]
        public void DrawBuffers(FrameDrawTarget[] buffers)
        {
            _framebuffer.DrawBuffers(buffers);
        }
        /// <summary>
        /// Sets teh buffer to be drawn to.
        /// </summary>
        /// <param name="buffers"></param>
        [OpenGLSupport(3.0)]
        public void DrawBuffer(FrameDrawTarget buffer)
        {
            _framebuffer.Bind();

            IFrameBuffer.DrawBuffer(buffer);
        }
        /// <summary>
        /// Sets the buffer to be read from.
        /// </summary>
        /// <param name="buffers"></param>
        [OpenGLSupport(3.0)]
        public void ReadBuffer(FrameDrawTarget buffer)
        {
            _framebuffer.Bind();

            IFrameBuffer.ReadBuffer(buffer);
        }
    }
}
