using System;
using Zene.Graphics.Base;
using Zene.Graphics.Base.Extensions;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that manages all components of a normal framebuffer.
    /// </summary>
    [OpenGLSupport(3.0)]
    public class TextureRenderer : IFramebuffer
    {
        /// <summary>
        /// Creats a framebuffer from set parameters.
        /// </summary>
        /// <param name="width">The width of the framebuffer attachments.</param>
        /// <param name="height">The height of the framebuffer attachments.</param>
        public TextureRenderer(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new FrameBufferException(this, "Framebuffers must have a width and heihgt greater that 0.");
            }

            _framebuffer = new FrameBufferGL();

            _targetWidth = width;
            _targetHeight = height;

            _framebuffer.View = new RectangleI(0, 0, width, height);

            _colourAttachs = new Texture2D[State.MaxColourAttach];
        }

        public uint Id => _framebuffer.Id;
        public FrameTarget Binding => _framebuffer.Binding;
        private readonly FrameBufferGL _framebuffer;

        protected FramebufferProperties Properties => _framebuffer.Properties;
        FramebufferProperties IFramebuffer.Properties => _framebuffer.Properties;

        /// <summary>
        /// The width of the framebuffer attachments.
        /// </summary>
        public int Width => _targetWidth;
        /// <summary>
        /// The height of the framebuffer attachments.
        /// </summary>
        public int Height => _targetHeight;

        // The size of newly added attachments.
        private int _targetWidth;
        private int _targetHeight;

        public RectangleI View
        {
            get => _framebuffer.View;
            set => _framebuffer.View = value;
        }
        public Vector2I ViewSize
        {
            get => _framebuffer.ViewSize;
            set => _framebuffer.ViewSize = value;
        }
        /// <summary>
        /// Sets the render location for the framebuffer.
        /// </summary>
        public Vector2I ViewLocation
        {
            get => _framebuffer.ViewLocation;
            set => _framebuffer.ViewLocation = value;
        }

        public FrameDrawTarget ReadBuffer
        {
            get => _framebuffer.ReadBuffer;
            set => _framebuffer.ReadBuffer = value;
        }
        public FrameDrawTarget[] DrawBuffers
        {
            get => _framebuffer.DrawBuffers;
            set => _framebuffer.DrawBuffers = value;
        }

        /// <summary>
        /// Gets or sets a colour buffer as a destination for draw calls.
        /// </summary>
        [OpenGLSupport(2.0)]
        public FrameDrawTarget DrawBuffer
        {
            set
            {
                _framebuffer.DrawBuffers = new FrameDrawTarget[] { value };
            }
        }

        [OpenGLSupport(3.0)]
        public void Bind(FrameTarget target) => _framebuffer.Bind(target);
        [OpenGLSupport(3.0)]
        public void Bind() => _framebuffer.Bind();
        [OpenGLSupport(3.0)]
        public void Unbind()
        {
            _framebuffer.Unbind();
        }

        private bool _disposed = false;
        [OpenGLSupport(3.0)]
        public void Dispose()
        {
            if (_disposed) { return; }

            _framebuffer.Dispose();
            // Dispose of colour attachments
            foreach (Texture2D texture in _colourAttachs)
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

        private readonly Texture2D[] _colourAttachs;
        private Texture2D _depthTex;
        private RenderbufferGL _depthRen;

        /// <summary>
        /// The clear colour that is used when <see cref="Clear(BufferBit)"/> is called.
        /// </summary>
        public ColourF ClearColour { get; set; } = ColourF.Zero;
        /// <summary>
        /// The depth value that is used when <see cref="Clear(BufferBit)"/> is called.
        /// </summary>
        public double ClearDepth { get; set; } = 1.0;
        /// <summary>
        /// The stencil value that is used when <see cref="Clear(BufferBit)"/> is called.
        /// </summary>
        public int CLearStencil { get; set; } = 0;

        public void Clear(BufferBit buffer)
        {
            Bind();

            if ((buffer & BufferBit.Colour) == BufferBit.Colour)
            {
                GL.ClearColour(ClearColour.R, ClearColour.G, ClearColour.B, ClearColour.A);
            }
            if ((buffer & BufferBit.Depth) == BufferBit.Depth)
            {
                GL.ClearDepth(ClearDepth);
            }
            if ((buffer & BufferBit.Stencil) == BufferBit.Stencil)
            {
                GL.ClearStencil(CLearStencil);
            }

            GL.Clear((uint)buffer);
        }

        /// <summary>
        /// Sets the colour attachment at <paramref name="attachment"/>.
        /// </summary>
        /// <param name="attachment">The colour attachment to set.</param>
        /// <param name="intFormat">The internal format of the texture to create.</param>
        [OpenGLSupport(3.0)]
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

            Texture2D texture = new Texture2D(intFormat, TextureData.Byte);
            texture.TexImage2D(0, _targetWidth, _targetHeight, BaseFormat.Rgb, texture.DataType, IntPtr.Zero);
            texture.MinFilter = TextureSampling.Nearest;
            texture.MagFilter = TextureSampling.Nearest;

            _framebuffer.FramebufferTexture2D(texture, (FrameAttachment)((int)FrameAttachment.Colour0 + attachment));
            _colourAttachs[attachment] = texture;

            texture.Unbind();
        }
        /// <summary>
        /// Sets the depth attachment of this framebuffer.
        /// </summary>
        /// <param name="intFormat">The internal format of the attachment.</param>
        /// <param name="isTexture">Determines whether is store the depth attachment as a texture or renderbuffer.</param>
        [OpenGLSupport(3.0)]
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
                Texture2D texture = new Texture2D(intFormat, TextureData.Byte);
                texture.TexImage2D(0, _targetWidth, _targetHeight, BaseFormat.DepthComponent, texture.DataType, IntPtr.Zero);
                texture.MinFilter = TextureSampling.Nearest;
                texture.MagFilter = TextureSampling.Nearest;

                _framebuffer.FramebufferTexture2D(texture, 
                    // The internal format contains a stencil attachment
                    intFormat.HasStencil() ? FrameAttachment.DepthStencil : FrameAttachment.Depth);

                _depthTex = texture;
                texture.Unbind();
                return;
            }

            RenderbufferGL renderbuffer = new RenderbufferGL();
            renderbuffer.RenderbufferStorage(intFormat, _targetWidth, _targetHeight);

            _framebuffer.FramebufferRenderbuffer(renderbuffer,
                // The internal format contains a stencil attachment
                intFormat.HasStencil() ? FrameAttachment.DepthStencil : FrameAttachment.Depth);

            _depthRen = renderbuffer;
            renderbuffer.Unbind();
        }

        /// <summary>
        /// Gets the texture that corresponds to an attachment.
        /// </summary>
        /// <param name="attachment">The attachment to retrieve.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public Texture2D GetTexture(FrameAttachment attachment)
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
        [OpenGLSupport(3.0)]
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
        [OpenGLSupport(3.0)]
        public Vector2I Size
        {
            get => new Vector2I(_targetWidth, _targetHeight);
            set
            {
                if (value.X <= 0 || value.Y <= 0)
                {
                    throw new FrameBufferException(this, "Framebuffers must have a width and heihgt greater that 0.");
                }

                _targetWidth = value.X;
                _targetHeight = value.Y;

                // Colour attachments
                foreach (Texture2D texture in _colourAttachs)
                {
                    // Make sure texture exists
                    if (texture != null)
                    {
                        texture.TexImage2D(0, _targetWidth, _targetHeight, BaseFormat.Rgb, texture.DataType, IntPtr.Zero);
                    }
                }
                // Unbind texture
                State.NullBind(Target.Texture2D);

                // Depth attachment
                if (_depthTex != null)
                {
                    _depthTex.TexImage2D(0, _targetWidth, _targetHeight, BaseFormat.DepthComponent, _depthTex.DataType, IntPtr.Zero);
                    _depthTex.Unbind();
                    return;
                }

                if (_depthRen != null)
                {
                    _depthRen.RenderbufferStorage(_targetWidth, _targetHeight);
                    _depthRen.Unbind();
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
        public void CopyFrameBuffer(IFramebuffer destination, IBox dstBox, BufferBit mask, TextureSampling filter)
        {
            _framebuffer.BlitBuffer(destination, new Rectangle(0, 0, _targetWidth, _targetHeight), dstBox, mask, filter);
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
        public void CopyFrameBuffer(IFramebuffer destination, IBox box, IBox dstBox, BufferBit mask, TextureSampling filter)
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
        public void CopyFrameBuffer(IFramebuffer destination, BufferBit mask, TextureSampling filter)
        {
            _framebuffer.BlitBuffer(destination,
                new Rectangle(0, 0, _targetWidth, _targetHeight),
                new Rectangle(0, 0, destination.Properties.Width, destination.Properties.Height), mask, filter);
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
            _framebuffer.BlitBuffer(null, new Rectangle(0, 0, _targetWidth, _targetHeight), dstBox, mask, filter);
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
    }
}
