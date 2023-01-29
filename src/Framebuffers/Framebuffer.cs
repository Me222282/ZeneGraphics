using System;
using Zene.Graphics.Base;
using Zene.Graphics.Base.Extensions;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// A basic framebuffer object that allows you to add textures.
    /// </summary>
    [OpenGLSupport(3.0)]
    public class Framebuffer : FramebufferGL
    {
        public Framebuffer()
        {
            _colourAttachs = new ITexture[State.MaxColourAttach];
            MainAttachment = FrameAttachment.Colour0;
        }
        /// <summary>
        /// Gets the completion status of this framebuffer.
        /// </summary>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public FrameBufferStatus Status => CheckStatus();

        /// <summary>
        /// Gets or sets a colour buffer as a destination for draw calls.
        /// </summary>
        [OpenGLSupport(2.0)]
        public FrameDrawTarget DrawBuffer
        {
            set
            {
                DrawBuffers = new FrameDrawTarget[] { value };
            }
        }

        private readonly ITexture[] _colourAttachs;
        private IRenderTexture _depth;
        private ITexture _stencil;

        public new bool LockedState
        {
            get => base.LockedState;
            set => base.LockedState = value;
        }
        public new Viewport Viewport
        {
            get => base.Viewport;
            set => base.Viewport = value;
        }
        public new DepthState DepthState
        {
            get => base.DepthState;
            set => base.DepthState = value;
        }
        public new Scissor Scissor
        {
            get => base.Scissor;
            set => base.Scissor = value;
        }

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

        public override void Clear(BufferBit buffer)
        {
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

            base.Clear(buffer);
        }

        private void SetTextureAttach(ITexture texture, FrameAttachment attachment)
        {
            if (texture.Target.Is1D())
            {
                FramebufferTexture1D(texture, attachment);
                return;
            }
            if (texture.Target.Is2D())
            {
                FramebufferTexture2D(texture, attachment);
                return;
            }

            throw new FrameBufferException(this, "Texture target is unsupported or 3d. 3d colour attachments are to be set with FrameBuffer.Set3DAttachment(ITexture, FrameAttachment, int).");
        }
        /// <summary>
        /// Sets a framebuffer attachment to a 3d texture.
        /// </summary>
        /// <param name="texture">The 3d texture to attach.</param>
        /// <param name="attachment">The attachment to set.</param>
        /// <param name="offset">The offset into the 3d texture to which to referance a 2d section.</param>
        [OpenGLSupport(3.0)]
        public void Set3DAttachment(ITexture texture, FrameAttachment attachment, int offset)
        {
            if (!texture.Target.Is3D())
            {
                throw new FrameBufferException(this, "Can only attach 3d textures in FrameBuffer.Set3DAttachment(ITexture, FrameAttachment, int)");
            }

            FramebufferTexture3D(texture, attachment, offset);

            // Is depth attachment
            if (attachment == FrameAttachment.DepthStencil || attachment == FrameAttachment.Depth)
            {
                _depth = texture;
                return;
            }
            // Is stencil attachment
            if (attachment == FrameAttachment.Stencil)
            {
                _stencil = texture;
                return;
            }
            // Is colour attachment

            _colourAttachs[(int)attachment - (int)FrameAttachment.Colour0] = texture;
        }

        /// <summary>
        /// Gets or sets a colour attachment at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The colour attachment. Must be greater than or equal to 0 and less than <see cref="IFramebuffer.MaxColourAttach()"/>.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public ITexture this[int index]
        {
            get
            {
                return _colourAttachs[index];
            }
            set
            {
                SetTextureAttach(value, (FrameAttachment)((int)FrameAttachment.Colour0 + index));

                _colourAttachs[index] = value;
            }
        }
        /// <summary>
        /// Gets or sets the depth or depthStencil attachment.
        /// </summary>
        [OpenGLSupport(3.0)]
        public IRenderTexture Depth
        {
            get
            {
                return _depth;
            }
            set
            {
                _depth = value;

                // If value is a texture
                if (!value.IsRenderbuffer)
                {
                    ITexture texture = (ITexture)value;

                    if (texture.InternalFormat.HasStencil())
                    {
                        SetTextureAttach(texture, FrameAttachment.DepthStencil);
                        return;
                    }
                    if (texture.InternalFormat.IsDepth())
                    {
                        SetTextureAttach(texture, FrameAttachment.Depth);
                        return;
                    }

                    throw new FrameBufferException(this, "Depth attachment value must be stored as a depth component type.");
                }

                // value is a renderbuffer
                IRenderbuffer renderbuffer = (IRenderbuffer)value;

                if (renderbuffer.InternalFormat.HasStencil())
                {
                    FramebufferRenderbuffer(renderbuffer, FrameAttachment.DepthStencil);
                    return;
                }
                if (renderbuffer.InternalFormat.IsDepth())
                {
                    FramebufferRenderbuffer(renderbuffer, FrameAttachment.Depth);
                    return;
                }

                throw new FrameBufferException(this, "Depth attachment value must be stored as a depth component type.");
            }
        }
        /// <summary>
        /// Gets or sets the stencil attachment.
        /// </summary>
        [OpenGLSupport(4.3)]
        public ITexture Stencil
        {
            get
            {
                return _stencil;
            }
            set
            {
                _stencil = value;

                if (_stencil.InternalFormat.HasStencil())
                {
                    SetTextureAttach(_stencil, FrameAttachment.Stencil);
                    return;
                }

                throw new FrameBufferException(this, "Stencil attachment must have a stencil component.");
            }
        }

        /// <summary>
        /// Removes the reference to an attachment.
        /// </summary>
        /// <param name="attachment">The attachment which is being removed</param>
        [OpenGLSupport(3.0)]
        public void RemoveAttachment(FrameAttachment attachment)
        {
            // Is depth attachment
            if (attachment == FrameAttachment.DepthStencil || attachment == FrameAttachment.Depth)
            {
                // Depth is a texture
                if (!_depth.IsRenderbuffer)
                {
                    ITexture textureD = (ITexture)_depth;
                    _depth = null;

                    // 1d texture
                    if (textureD.Target.Is1D())
                    {
                        FramebufferTexture1D(null, attachment);
                        return;
                    }
                    // 2d texture
                    if (textureD.Target.Is1D())
                    {
                        FramebufferTexture2D(null, attachment);
                        return;
                    }
                    // 3d texture
                    FramebufferTexture3D(null, attachment, 0);
                    return;
                }
                // Depth is a renderbuffer
                FramebufferRenderbuffer(null, attachment);

                _depth = null;
                return;
            }
            // Is stencil attachment
            if (attachment == FrameAttachment.Stencil)
            {
                _stencil = null;

                // 1d texture
                if (_stencil.Target.Is1D())
                {
                    FramebufferTexture1D(null, attachment);
                    return;
                }
                // 2d texture
                if (_stencil.Target.Is1D())
                {
                    FramebufferTexture2D(null, attachment);
                    return;
                }
                // 3d texture
                FramebufferTexture3D(null, attachment, 0);
                return;
            }

            // Is colour attachment
            ITexture texture = _colourAttachs[(int)attachment - (int)FrameAttachment.Colour0];

            _colourAttachs[(int)attachment - (int)FrameAttachment.Colour0] = null;
            // 1d texture
            if (texture.Target.Is1D())
            {
                FramebufferTexture1D(null, attachment);
                return;
            }
            // 2d texture
            if (texture.Target.Is1D())
            {
                FramebufferTexture2D(null, attachment);
                return;
            }
            // 3d texture
            FramebufferTexture3D(null, attachment, 0);
        }

        /// <summary>
        /// The attachment that is referanced for cetain properties.
        /// </summary>
        public FrameAttachment MainAttachment { get; set; }

        /// <summary>
        /// The width of the framebuffer attachments.
        /// </summary>
        public int Width => Properties.Width;
        /// <summary>
        /// The height of the framebuffer attachments.
        /// </summary>
        public int Height => Properties.Height;

        private static void SetEmptyTexture(ITexture texture, int width, int height, int level, BaseFormat form)
        {
            // TexImage function dones't work on texture
            if (texture.Properties.Immutable)
            {
                // 1d texture
                if (texture.Target.Is1D())
                {
                    texture.TexStorage1D(1, width);
                    return;
                }

                // 2d texture
                if (texture.Target.Is2D())
                {
                    texture.TexStorage2D(1, width, height);
                    return;
                }

                texture.TexStorage3D(1, width, height, texture.GetDepth(level));
                return;
            }
            // Normal texture

            // 1d texture
            if (texture.Target.Is1D())
            {
                texture.TexImage1D(level, width, form, TextureData.Byte, IntPtr.Zero);
                return;
            }

            // 2d texture
            if (texture.Target.Is2D())
            {
                texture.TexImage2D(level, width, height, form, TextureData.Byte, IntPtr.Zero);
                return;
            }

            texture.TexImage3D(level, width, height, texture.GetDepth(level), form, TextureData.Byte, IntPtr.Zero);
        }
        /// <summary>
        /// Reassigns all attachments at <paramref name="level"/> to the size <paramref name="width"/> and <paramref name="height"/>.
        /// </summary>
        /// <remarks>
        /// Note: This function will remove pixel data from the texture.
        /// </remarks>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="level"></param>
        [OpenGLSupport(3.0)]
        public void SetSize(int width, int height, int level)
        {
            if (width <= 0 || height <= 0)
            {
                throw new FrameBufferException(this, "Framebuffers must have a width and heihgt greater that 0.");
            }

            // Colour attachments
            foreach (ITexture texture in _colourAttachs)
            {
                // Make sure texture exists
                if (texture != null)
                {
                    SetEmptyTexture(texture, width, height, level, BaseFormat.Rgb);
                    texture.Unbind();
                }
            }
            // Depth attachment

            // Depth component doesn't exist
            if (_depth == null) { return; }

            // Texture
            if (!_depth.IsRenderbuffer)
            {
                ITexture texture = (ITexture)_depth;

                SetEmptyTexture(texture, width, height, level, BaseFormat.DepthComponent);
                texture.Unbind();
                return;
            }

            // Renderbuffer
            IRenderbuffer renderbuffer = (IRenderbuffer)_depth;

            renderbuffer.RenderbufferStorage(width, height);
            renderbuffer.Unbind();
            return;
        }
        /// <summary>
        /// Reassigns all attachments at level 0 to the size <paramref name="width"/> and <paramref name="height"/>.
        /// </summary>
        /// <remarks>
        /// Note: This function will remove pixel data from the texture.
        /// </remarks>
        /// <param name="width"></param>
        /// <param name="height"></param>
        [OpenGLSupport(3.0)]
        public void SetSize(int width, int height) => SetSize(width, height, 0);

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
            BlitBuffer(destination, new GLBox(0d, 0d, Width, Height), dstBox, mask, filter);
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
        public void CopyFrameBuffer(IFramebuffer destination, IBox dstBox, IBox box, BufferBit mask, TextureSampling filter)
        {
            BlitBuffer(destination, box, dstBox, mask, filter);
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
            BlitBuffer(destination,
                new GLBox(0, 0d, Width, Height),
                new GLBox(0, 0d, destination.Properties.Width, destination.Properties.Height), mask, filter);
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
            BlitBuffer(null, new GLBox(0, 0, Width, Height), dstBox, mask, filter);
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
            BlitBuffer(null, box, dstBox, mask, filter);
        }

        /// <summary>
        /// The number of bits in the alpha component channel of <see cref="MainAttachment"/>.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int AlphaSize => Properties[MainAttachment].Attachment.Properties.AlphaSize;
        /// <summary>
        /// The number of bits in the blue component channel of <see cref="MainAttachment"/>.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int BlueSize => Properties[MainAttachment].Attachment.Properties.BlueSize;
        /// <summary>
        /// The colour encoding components of <see cref="MainAttachment"/>.
        /// </summary>
        [OpenGLSupport(3.0)]
        public ColourEncode ColourEncoding => GetColourEncoding(MainAttachment);
        /// <summary>
        /// The format of the <see cref="MainAttachment"/> components.
        /// </summary>
        [OpenGLSupport(3.0)]
        public ChannelType ComponentReadType => GetComponentType(MainAttachment);
        /// <summary>
        /// The number of bits in the depth component channel of <see cref="MainAttachment"/>.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int DepthSize => Properties[MainAttachment].Attachment.Properties.DepthSize;
        /// <summary>
        /// The number of bits in the green component channel of <see cref="MainAttachment"/>.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int GreenSize => Properties[MainAttachment].Attachment.Properties.GreenSize;
        /// <summary>
        /// Determiens whether <see cref="MainAttachment"/> is a layered object.
        /// </summary>
        [OpenGLSupport(3.2)]
        public bool Layered => Properties[MainAttachment].Layered;
        /// <summary>
        /// The number of bits in the red component channel of <see cref="MainAttachment"/>.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int RedSize => Properties[MainAttachment].Attachment.Properties.RedSize;
        /// <summary>
        /// The number of bits in the stencil component channel of <see cref="MainAttachment"/>.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int StencilSize => Properties[MainAttachment].Attachment.Properties.StencilSize;
        /// <summary>
        /// The cube map face from <see cref="MainAttachment"/> that is attached to this framebuffer.
        /// </summary>
        [OpenGLSupport(3.0)]
        public CubeMapFace TextureCubeFace => Properties[MainAttachment].Face;
        /// <summary>
        /// The texture layer of <see cref="MainAttachment"/> that is attached to this framebuffer.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int TextureLayer => Properties[MainAttachment].Layer;
        /// <summary>
        /// The texture level of <see cref="MainAttachment"/> that is attached to this framebuffer.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int TextureLevel => Properties[MainAttachment].Level;

        /// <summary>
        /// The prefered pixel format for this framebuffer.
        /// </summary>
        [OpenGLSupport(4.5)]
        public BaseFormat ColourReadFormat => Properties.ColourReadFormat;
        /// <summary>
        /// The prefered pixel data type for this framebuffer.
        /// </summary>
        [OpenGLSupport(4.5)]
        public TextureData ColourReadType => Properties.ColourReadType;
        /// <summary>
        /// Determines whether double buffering is supported by this framebuffer.
        /// </summary>
        [OpenGLSupport(4.5)]
        public bool DoubleBuffered => Properties.DoubleBuffered;
        /// <summary>
        /// The coverage mask size for this framebuffer.
        /// </summary>
        [OpenGLSupport(4.5)]
        public int Samples => Properties.Samples;
        /// <summary>
        /// The number of sample buffers a part of this framebuffer.
        /// </summary>
        [OpenGLSupport(4.5)]
        public int SampleBuffers => Properties.SampleBuffers;
        /// <summary>
        /// Determines whether stereo buffers are support by this framebuffer.
        /// </summary>
        [OpenGLSupport(4.5)]
        public bool Stereo => Properties.Stereo;
    }
}
