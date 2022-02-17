using System;
using Zene.Structs;

namespace Zene.Graphics.OpenGL
{
    /// <summary>
    /// The most basic implimentation of an OpenGL framebuffer.
    /// </summary>
    [OpenGLSupport(3.0)]
    public unsafe sealed class FrameBufferGL : IFramebuffer
    {
        /// <summary>
        /// Creats an OpenGL framebuffer object.
        /// </summary>
        public FrameBufferGL()
        {
            Id = GL.GenFramebuffer();
        }
        internal FrameBufferGL(uint id)
        {
            Id = id;
        }

        public uint Id { get; }
        public FrameTarget Binding { get; private set; } = FrameTarget.FrameBuffer;

        [OpenGLSupport(3.0)]
        public void Bind()
        {
            if (this.Bound()) { return; }

            // Set viewport
            GL.Viewport(_view.X, _view.Y, _view.Width, _view.Height);

            Binding = FrameTarget.FrameBuffer;
            GL.BindFramebuffer(GLEnum.Framebuffer, Id);
        }
        [OpenGLSupport(3.0)]
        public void Bind(FrameTarget target)
        {
            if (this.Bound(target)) { return; }

            // Set viewport
            GL.Viewport(_view.X, _view.Y, _view.Width, _view.Height);

            Binding = target;
            GL.BindFramebuffer((uint)target, Id);
        }

        private bool _disposed = false;
        [OpenGLSupport(3.0)]
        public void Dispose()
        {
            if (_disposed) { return; }

            GL.DeleteFramebuffer(Id);

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        [OpenGLSupport(3.0)]
        public void Unbind()
        {
            if (!this.Bound()) { return; }

            GL.BindFramebuffer((uint)Binding, 0);
        }

        public bool Validate()
        {
            return CheckStatus() == FrameBufferStatus.Complete;
        }

        private RectangleI _view = new RectangleI(0, 0, 1, 1);
        public RectangleI View
        {
            get => _view;
            set
            {
                _view = value;

                if (this.Bound())
                {
                    GL.Viewport(_view.X, _view.Y, _view.Width, _view.Height);
                }
            }
        }
        public Vector2I ViewSize
        {
            get => _view.Size;
            set
            {
                _view.Size = value;

                if (this.Bound())
                {
                    GL.Viewport(_view.X, _view.Y, _view.Width, _view.Height);
                }
            }
        }
        /// <summary>
        /// Sets the render location for the framebuffer.
        /// </summary>
        public Vector2I ViewLocation
        {
            get => _view.Location;
            set
            {
                _view.Location = value;

                if (this.Bound())
                {
                    GL.Viewport(_view.X, _view.Y, _view.Width, _view.Height);
                }
            }
        }

        private FrameDrawTarget _readBuffer = FrameDrawTarget.Colour0;
        public FrameDrawTarget ReadBuffer
        {
            get => _readBuffer;
            set
            {
                _readBuffer = value;

                Bind();

                GL.ReadBuffer((uint)value);
            }
        }

        private FrameDrawTarget[] _drawBuffers = new FrameDrawTarget[] { FrameDrawTarget.Colour0 };
        public FrameDrawTarget[] DrawBuffers
        {
            get => _drawBuffers;
            set
            {
                _drawBuffers = value;

                Bind();

                // Set draw buffers on gpu
                fixed (void* pointer = &value[0])
                {
                    GL.DrawBuffers(value.Length, (uint*)pointer);
                }
            }
        }

        public void Clear(BufferBit buffer)
        {
            Bind();

            GL.Clear((uint)buffer);
        }

        /*
        /// <summary>
        /// Copy a block of pixels from one framebuffer object to another.
        /// </summary>
        /// <param name="x">The x coordinate of the source framebuffer for data to be copied.</param>
        /// <param name="y">The y coordinate of the source framebuffer for data to be copied.</param>
        /// <param name="width">The width of the source framebuffer for data to be copied.</param>
        /// <param name="height">The height of the source framebuffer for data to be copied.</param>
        /// <param name="dstX">The y coordinate of the destination framebuffer for data to be copied.</param>
        /// <param name="dstY">The x coordinate of the destination framebuffer for data to be copied.</param>
        /// <param name="dstWidth">The width of the destination framebuffer for data to be copied.</param>
        /// <param name="dstHeight">The height of the destination framebuffer for data to be copied.</param>
        /// <param name="mask">The bitwise or of the flags indicating which buffers are to be copied.</param>
        /// <param name="filter">Specifies the interpolation to be applied if the image is stretched.</param>
        [OpenGLSupport(3.0)]
        public void BlitBuffer(int x, int y, int width, int height, int dstX, int dstY, int dstWidth, int dstHeight, BufferBit mask, TextureSampling filter)
        {
            Bind();
            GL.BlitFramebuffer(x, y, x + width, y + height, dstX, dstY, dstX + dstWidth, dstY + dstHeight, (uint)mask, (uint)filter);
        }*/
        /// <summary>
        /// Copy all of pixels from this framebuffer object to <paramref name="destination"/>.
        /// </summary>
        /// <param name="destination">The framebuffer to copy to.</param>
        /// <param name="mask">The bitwise or of the flags indicating which buffers are to be copied.</param>
        /// <param name="filter">Specifies the interpolation to be applied if the image is stretched.</param>
        [OpenGLSupport(3.0)]
        public void BlitBuffer(IFramebuffer destination, IBox srcBox, IBox dstBox, BufferBit mask, TextureSampling filter)
        {
            if (destination == null)
            {
                State.NullBind(Target.DrawFramebuffer);
            }
            else
            {
                destination.Bind(FrameTarget.Draw);
            }
            Bind(FrameTarget.Read);

            GL.BlitFramebuffer(
                (int)srcBox.Left, (int)srcBox.Top, (int)srcBox.Right, (int)(srcBox.Top + srcBox.Height),
                (int)dstBox.Left, (int)dstBox.Top, (int)dstBox.Right, (int)(dstBox.Top + dstBox.Height),
                (uint)mask, (uint)filter);
        }
        /// <summary>
        /// Copy a block of pixels from this framebuffer object to <paramref name="destination"/>.
        /// </summary>
        /// <param name="destination">The framebuffer to copy to.</param>
        /// <param name="x">The x coordinate of the source framebuffer for data to be copied.</param>
        /// <param name="y">The y coordinate of the source framebuffer for data to be copied.</param>
        /// <param name="width">The width of the source framebuffer for data to be copied.</param>
        /// <param name="height">The height of the source framebuffer for data to be copied.</param>
        /// <param name="dstX">The y coordinate of the destination framebuffer for data to be copied.</param>
        /// <param name="dstY">The x coordinate of the destination framebuffer for data to be copied.</param>
        /// <param name="dstWidth">The width of the destination framebuffer for data to be copied.</param>
        /// <param name="dstHeight">The height of the destination framebuffer for data to be copied.</param>
        /// <param name="mask">The bitwise or of the flags indicating which buffers are to be copied.</param>
        /// <param name="filter">Specifies the interpolation to be applied if the image is stretched.</param>
        [OpenGLSupport(3.0)]
        public void BlitBuffer(IFramebuffer destination, int x, int y, int width, int height, int dstX, int dstY, int dstWidth, int dstHeight, BufferBit mask, TextureSampling filter)
        {
            if (destination == null)
            {
                State.NullBind(Target.DrawFramebuffer);
            }
            else
            {
                destination.Bind(FrameTarget.Draw);
            }
            Bind(FrameTarget.Read);

            GL.BlitFramebuffer(x, y, x + width, y + height, dstX, dstY, dstX + dstWidth, dstY + dstHeight, (uint)mask, (uint)filter);
        }

        /// <summary>
        /// Check the completeness status of a framebuffer.
        /// </summary>
        [OpenGLSupport(3.0)]
        public FrameBufferStatus CheckStatus()
        {
            Bind();
            return (FrameBufferStatus)GL.CheckFramebufferStatus(GLEnum.Framebuffer);
        }

        /// <summary>
        /// Attach a renderbuffer as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="renderbuffer">Specifies the renderbuffer object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public void FramebufferRenderbuffer(IRenderbuffer renderbuffer, FrameAttachment attachment)
        {
            Bind();
            GL.FramebufferRenderbuffer((uint)Binding, (uint)attachment, GLEnum.Renderbuffer, renderbuffer.Id);
        }

        /// <summary>
        /// Attach a level of a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="level">Specifies the mipmap level of the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.2)]
        public void FramebufferTexture(ITexture texture, int level, FrameAttachment attachment)
        {
            Bind();
            GL.FramebufferTexture((uint)Binding, (uint)attachment, texture.Id, level);
        }
        /// <summary>
        /// Attach a level of a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="level">Specifies the mipmap level of the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public void FramebufferTexture1D(ITexture texture, int level, FrameAttachment attachment)
        {
            Bind();
            GL.FramebufferTexture1D((uint)Binding, (uint)attachment, GLEnum.Texture1d, texture.Id, level);
        }
        /// <summary>
        /// Attach a level of a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="level">Specifies the mipmap level of the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public void FramebufferTexture2D(CubeMapFace textureTarget, ITexture texture, int level, FrameAttachment attachment)
        {
            Bind();
            GL.FramebufferTexture2D((uint)Binding, (uint)attachment, (uint)textureTarget, texture.Id, level);
        }
        /// <summary>
        /// Attach a level of a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="level">Specifies the mipmap level of the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public void FramebufferTexture2D(ITexture texture, int level, FrameAttachment attachment)
        {
            Bind();
            GL.FramebufferTexture2D((uint)Binding, (uint)attachment, (uint)texture.Target, texture.Id, level);
        }
        /// <summary>
        /// Attach a level of a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="level">Specifies the mipmap level of the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        /// <param name="offset">The offset into the 3d texture to be the 2d section to be attached.</param>
        [OpenGLSupport(3.0)]
        public void FramebufferTexture3D(ITexture texture, int level, FrameAttachment attachment, int offset)
        {
            Bind();
            GL.FramebufferTexture3D((uint)Binding, (uint)attachment, (uint)texture.Target, texture.Id, level, offset);
        }

        /// <summary>
        /// Attach a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.2)]
        public void FramebufferTexture(ITexture texture, FrameAttachment attachment)
        {
            Bind();
            GL.FramebufferTexture((uint)Binding, (uint)attachment, texture.Id, 0);
        }
        /// <summary>
        /// Attach a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public void FramebufferTexture1D(ITexture texture, FrameAttachment attachment)
        {
            Bind();
            GL.FramebufferTexture1D((uint)Binding, (uint)attachment, GLEnum.Texture1d, texture.Id, 0);
        }
        /// <summary>
        /// Attach a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public void FramebufferTexture2D(CubeMapFace textureTarget, ITexture texture, FrameAttachment attachment)
        {
            Bind();
            GL.FramebufferTexture2D((uint)Binding, (uint)attachment, (uint)textureTarget, texture.Id, 0);
        }
        /// <summary>
        /// Attach a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public void FramebufferTexture2D(ITexture texture, FrameAttachment attachment)
        {
            Bind();
            GL.FramebufferTexture2D((uint)Binding, (uint)attachment, (uint)texture.Target, texture.Id, 0);
        }
        /// <summary>
        /// Attach a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        /// <param name="offset">The offset into the 3d texture to be the 2d section to be attached.</param>
        [OpenGLSupport(3.0)]
        public void FramebufferTexture3D(ITexture texture, FrameAttachment attachment, int offset)
        {
            Bind();
            GL.FramebufferTexture3D((uint)Binding, (uint)attachment, (uint)texture.Target, texture.Id, 0, offset);
        }

        /// <summary>
        /// Attach a single layer of a texture object as a logical buffer of a framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="level">Specifies the mipmap level of the texture object to attach.</param>
        /// <param name="layer">Specifies the layer of the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public void FramebufferTextureLayer(ITexture texture, int level, int layer, FrameAttachment attachment)
        {
            Bind();
            GL.FramebufferTextureLayer((uint)Binding, (uint)attachment, texture.Id, level, layer);
        }
        /// <summary>
        /// Attach a single layer of a texture object as a logical buffer of a framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="layer">Specifies the layer of the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public void FramebufferTextureLayer(ITexture texture, int layer, FrameAttachment attachment)
        {
            Bind();
            GL.FramebufferTextureLayer((uint)Binding, (uint)attachment, texture.Id, 0, layer);
        }

        /// <summary>
        /// Invalidate the content of some or all of this framebuffer's attachments.
        /// </summary>
        /// <param name="attachments">Specifies the attachments to be invalidated.</param>
        [OpenGLSupport(4.3)]
        public void Invalidate(FrameAttachment[] attachments)
        {
            Bind();
            fixed (void* pointer = &attachments[0])
            {
                GL.InvalidateFramebuffer((uint)Binding, attachments.Length, (uint*)pointer);
            }
        }
        /// <summary>
        /// Invalidate the content of a region of some or all of a framebuffer's attachments.
        /// </summary>
        /// <param name="attachments">Specifies the attachments to be invalidated.</param>
        /// <param name="x">Specifies the X offset of the region to be invalidated.</param>
        /// <param name="y">Specifies the Y offset of the region to be invalidated.</param>
        /// <param name="width">Specifies the width of the region to be invalidated.</param>
        /// <param name="height">Specifies the height of the region to be invalidated.</param>
        [OpenGLSupport(4.3)]
        public void InvalidateSub(FrameAttachment[] attachments, int x, int y, int width, int height)
        {
            Bind();
            fixed (void* pointer = &attachments[0])
            {
                GL.InvalidateSubFramebuffer((uint)Binding, attachments.Length, (uint*)pointer, x, y, width, height);
            }
        }

        //
        // Parameters
        //

        /// <summary>
        /// Returns the number of bits in the red component of <paramref name="attachment"/>.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public int GetRedSize(FrameAttachment attachment)
        {
            Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)Binding, (uint)attachment, GLEnum.FramebufferAttachmentRedSize, &value);

            return value;
        }

        /// <summary>
        /// Returns the number of bits in the green component of <paramref name="attachment"/>.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public int GetGreenSize(FrameAttachment attachment)
        {
            Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)Binding, (uint)attachment, GLEnum.FramebufferAttachmentGreenSize, &value);

            return value;
        }

        /// <summary>
        /// Returns the number of bits in the blue component of <paramref name="attachment"/>.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public int GetBlueSize(FrameAttachment attachment)
        {
            Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)Binding, (uint)attachment, GLEnum.FramebufferAttachmentBlueSize, &value);

            return value;
        }

        /// <summary>
        /// Returns the number of bits in the alpha component of <paramref name="attachment"/>.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public int GetAlphaSize(FrameAttachment attachment)
        {
            Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)Binding, (uint)attachment, GLEnum.FramebufferAttachmentAlphaSize, &value);

            return value;
        }

        /// <summary>
        /// Returns the number of bits in the depth component of <paramref name="attachment"/>.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public int GetDepthSize(FrameAttachment attachment)
        {
            Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)Binding, (uint)attachment, GLEnum.FramebufferAttachmentDepthSize, &value);

            return value;
        }

        /// <summary>
        /// Returns the number of bits in the stencil component of <paramref name="attachment"/>.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public int GetStencilSize(FrameAttachment attachment)
        {
            Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)Binding, (uint)attachment, GLEnum.FramebufferAttachmentStencilSize, &value);

            return value;
        }

        /// <summary>
        /// Returns the format of components of <paramref name="attachment"/>.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public ChannelType GetComponentType(FrameAttachment attachment)
        {
            Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)Binding, (uint)attachment, GLEnum.FramebufferAttachmentComponentType, &value);

            return (ChannelType)value;
        }

        /// <summary>
        /// Returns the encoding of components of <paramref name="attachment"/>.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public ColourEncode GetColourEncoding(FrameAttachment attachment)
        {
            Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)Binding, (uint)attachment, GLEnum.FramebufferAttachmentColourEncoding, &value);

            return (ColourEncode)value;
        }

        /// <summary>
        /// Returns the storage type of <paramref name="attachment"/>.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public AttachType GetObjectType(FrameAttachment attachment)
        {
            Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)Binding, (uint)attachment, GLEnum.FramebufferAttachmentObjectType, &value);

            return (AttachType)value;
        }

        /// <summary>
        /// Returns the name of the object which contains the attached image.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public uint GetObjectId(FrameAttachment attachment)
        {
            Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)Binding, (uint)attachment, GLEnum.FramebufferAttachmentObjectName, &value);

            return (uint)value;
        }

        /// <summary>
        /// Returns the mipmap level of the texture object which contains the attached image.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public int GetTextureLevel(FrameAttachment attachment)
        {
            Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)Binding, (uint)attachment, GLEnum.FramebufferAttachmentTextureLevel, &value);

            return value;
        }

        /// <summary>
        /// Returns the cube map face of the cubemap texture object which contains the attached image.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public CubeMapFace GetTextureCubeMapFace(FrameAttachment attachment)
        {
            Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)Binding, (uint)attachment, GLEnum.FramebufferAttachmentTextureCubeMapFace, &value);

            return (CubeMapFace)value;
        }

        /// <summary>
        /// Returns a value indicating whether the <paramref name="attachment"/> is attached as a layered object.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.2)]
        public bool GetLayered(FrameAttachment attachment)
        {
            Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)Binding, (uint)attachment, GLEnum.FramebufferAttachmentLayered, &value);

            return value == GLEnum.True;
        }

        /// <summary>
        /// Returns the texture layer which contains the attached image.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public int GetTextureLayer(FrameAttachment attachment)
        {
            Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)Binding, (uint)attachment, GLEnum.FramebufferAttachmentTextureLayer, &value);

            return value;
        }

        //
        // OpenGL 4.3 parameters
        //

        /// <summary>
        /// Returns the defualt width of this framebuffer.
        /// </summary>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public int GetDefaultWidth()
        {
            Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)Binding, GLEnum.FramebufferDefaultWidth, &value);

            return value;
        }

        /// <summary>
        /// Returns the defualt height of this framebuffer.
        /// </summary>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public int GetDefaultHeight()
        {
            Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)Binding, GLEnum.FramebufferDefaultHeight, &value);

            return value;
        }

        /// <summary>
        /// Returns the defualt number of layers of this framebuffer.
        /// </summary>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public int GetDefaultLayers()
        {
            Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)Binding, GLEnum.FramebufferDefaultLayers, &value);

            return value;
        }

        /// <summary>
        /// Returns the defualt number of samples of this framebuffer.
        /// </summary>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public int GetDefaultSamples()
        {
            Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)Binding, GLEnum.FramebufferDefaultSamples, &value);

            return value;
        }

        /// <summary>
        /// Returns the defualt fixed sample location value of this framebuffer.
        /// </summary>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public bool GetDefaultFixedSampleLocations()
        {
            Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)Binding, GLEnum.FramebufferDefaultFixedSampleLocations, &value);

            return value == GLEnum.True;
        }

        /// <summary>
        /// Returns a value indicating whether double buffering is supported for this framebuffer object.
        /// </summary>
        /// <returns></returns>
        [OpenGLSupport(4.5)]
        public bool GetDoubleBuffer()
        {
            Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)Binding, GLEnum.Doublebuffer, &value);

            return value == GLEnum.True;
        }

        /// <summary>
        /// Returns a value indicating the coverage mask size for this framebuffer object.
        /// </summary>
        /// <returns></returns>
        [OpenGLSupport(4.5)]
        public int GetSamples()
        {
            Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)Binding, GLEnum.Samples, &value);

            return value;
        }

        /// <summary>
        /// Returns a value indicating the number of sample buffers associated with this framebuffer object.
        /// </summary>
        /// <returns></returns>
        [OpenGLSupport(4.5)]
        public int GetSampleBuffers()
        {
            Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)Binding, GLEnum.SampleBuffers, &value);

            return value;
        }

        /// <summary>
        /// Returns a value indicating whether stereo buffers (left and right) are supported for this framebuffer object.
        /// </summary>
        /// <returns></returns>
        [OpenGLSupport(4.5)]
        public bool GetStereo()
        {
            Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)Binding, GLEnum.Stereo, &value);

            return value == GLEnum.True;
        }

        /// <summary>
        /// Returns a value indicating the preferred pixel data format for this framebuffer object.
        /// </summary>
        /// <returns></returns>
        [OpenGLSupport(4.5)]
        public BaseFormat GetColourReadFormat()
        {
            Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)Binding, GLEnum.Doublebuffer, &value);

            return (BaseFormat)value;
        }

        /// <summary>
        /// Returns a value indicating the implementation's preferred pixel data type for this framebuffer object.
        /// </summary>
        /// <returns></returns>
        [OpenGLSupport(4.5)]
        public TextureData GetColourReadType()
        {
            Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)Binding, GLEnum.Doublebuffer, &value);

            return (TextureData)value;
        }

        //
        // Set parameters
        //

        /// <summary>
        /// Specifies the assumed width for this framebuffer object if with no attachments.
        /// </summary>
        /// <param name="value">The value to set to the parameter.</param>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public void SetDefaultWidth(int value)
        {
            Bind();
            GL.FramebufferParameteri((uint)Binding, GLEnum.FramebufferDefaultWidth, value);
        }

        /// <summary>
        /// Specifies the assumed height for this framebuffer object if with no attachments.
        /// </summary>
        /// <param name="value">The value to set to the parameter.</param>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public void SetDefaultHeight(int value)
        {
            Bind();
            GL.FramebufferParameteri((uint)Binding, GLEnum.FramebufferDefaultHeight, value);
        }

        /// <summary>
        /// Specifies the assumed number of layers for this framebuffer object if with no attachments.
        /// </summary>
        /// <param name="value">The value to set to the parameter.</param>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public void SetDefaultLayers(int value)
        {
            Bind();
            GL.FramebufferParameteri((uint)Binding, GLEnum.FramebufferDefaultLayers, value);
        }

        /// <summary>
        /// Specifies the assumed number of samples for this framebuffer object if with no attachments.
        /// </summary>
        /// <param name="value">The value to set to the parameter.</param>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public void SetDefaultSamples(int value)
        {
            Bind();
            GL.FramebufferParameteri((uint)Binding, GLEnum.FramebufferDefaultSamples, value);
        }

        /// <summary>
        /// Specifies whether this framebuffer should assume identical sample locations and the same number of samples for all texels in the virtual image.
        /// </summary>
        /// <param name="value">The value to set to the parameter.</param>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public void SetDefaultFixedSampleLocations(bool value)
        {
            Bind();
            GL.FramebufferParameteri((uint)Binding, GLEnum.FramebufferDefaultFixedSampleLocations, value ? 1 : 0);
        }
    }
}
