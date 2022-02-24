using Zene.Structs;

namespace Zene.Graphics.Base.Extensions
{
    [OpenGLSupport(3.0)]
    public static unsafe class FrameBufferOpenGL
    {
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
        public static void BlitBuffer(this IFrameBuffer framebuffer, int x, int y, int width, int height, int dstX, int dstY, int dstWidth, int dstHeight, BufferBit mask, TextureSampling filter)
        {
            GL.BlitFramebuffer(x, y, x + width, y + height, dstX, dstY, dstX + dstWidth, dstY + dstHeight, (uint)mask, (uint)filter);
        }*/
        /// <summary>
        /// Copy all of pixels from this framebuffer object to <paramref name="destination"/>.
        /// </summary>
        /// <param name="destination">The framebuffer to copy to.</param>
        /// <param name="mask">The bitwise or of the flags indicating which buffers are to be copied.</param>
        /// <param name="filter">Specifies the interpolation to be applied if the image is stretched.</param>
        [OpenGLSupport(3.0)]
        public static void BlitBuffer(this IFramebuffer framebuffer, IFramebuffer destination, IBox srcBox, IBox dstBox, BufferBit mask, TextureSampling filter)
        {
            if (destination == null)
            {
                State.NullBind(Target.DrawFramebuffer);
            }
            else
            {
                destination.Bind(FrameTarget.Draw);
            }
            framebuffer.Bind(FrameTarget.Read);

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
        public static void BlitBuffer(this IFramebuffer framebuffer, IFramebuffer destination, int x, int y, int width, int height, int dstX, int dstY, int dstWidth, int dstHeight, BufferBit mask, TextureSampling filter)
        {
            if (destination == null)
            {
                State.NullBind(Target.DrawFramebuffer);
            }
            else
            {
                destination.Bind(FrameTarget.Draw);
            }
            framebuffer.Bind(FrameTarget.Read);

            GL.BlitFramebuffer(x, y, x + width, y + height, dstX, dstY, dstX + dstWidth, dstY + dstHeight, (uint)mask, (uint)filter);
        }

        /// <summary>
        /// Check the completeness status of a framebuffer.
        /// </summary>
        [OpenGLSupport(3.0)]
        public static FrameBufferStatus CheckStatus(this IFramebuffer framebuffer)
        {
            framebuffer.Bind();
            return (FrameBufferStatus)GL.CheckFramebufferStatus((uint)framebuffer.Binding);
        }

        /// <summary>
        /// Specifies the colour buffer to be drawn into.
        /// </summary>
        /// <param name="buffer">The buffer to draw to.</param>
        [OpenGLSupport(3.0)]
        public static void DrawBuffers(this IFramebuffer framebuffer, FrameDrawTarget buffer)
        {
            framebuffer.Bind();
            uint buf = (uint)buffer;
            GL.DrawBuffers(1, &buf);
        }
        /// <summary>
        /// Specifies the list of colour buffers to be drawn into.
        /// </summary>
        /// <param name="buffers">The buffers to draw to.</param>
        [OpenGLSupport(3.0)]
        public static void DrawBuffers(this IFramebuffer framebuffer, FrameDrawTarget[] buffers)
        {
            framebuffer.Bind();
            fixed (void* pointer = &buffers[0])
            {
                GL.DrawBuffers(buffers.Length, (uint*)pointer);
            }
        }

        /// <summary>
        /// Attach a renderbuffer as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="renderbuffer">Specifies the renderbuffer object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public static void FramebufferRenderbuffer(this IFramebuffer framebuffer, IRenderbuffer renderbuffer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            GL.FramebufferRenderbuffer(framebuffer, (uint)attachment, GLEnum.Renderbuffer, renderbuffer);
        }

        /// <summary>
        /// Attach a level of a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="level">Specifies the mipmap level of the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.2)]
        public static void FramebufferTexture(this IFramebuffer framebuffer, ITexture texture, int level, FrameAttachment attachment)
        {
            framebuffer.Bind();
            GL.FramebufferTexture(framebuffer, (uint)attachment, texture, level);
        }
        /// <summary>
        /// Attach a level of a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="level">Specifies the mipmap level of the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public static void FramebufferTexture1D(this IFramebuffer framebuffer, ITexture texture, int level, FrameAttachment attachment)
        {
            framebuffer.Bind();
            GL.FramebufferTexture1D(framebuffer, (uint)attachment, GLEnum.Texture1d, texture, level);
        }
        /// <summary>
        /// Attach a level of a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="level">Specifies the mipmap level of the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public static void FramebufferTexture2D(this IFramebuffer framebuffer, CubeMapFace textureTarget, ITexture texture, int level, FrameAttachment attachment)
        {
            framebuffer.Bind();
            GL.FramebufferTexture2D(framebuffer, (uint)attachment, (uint)textureTarget, texture, level);
        }
        /// <summary>
        /// Attach a level of a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="level">Specifies the mipmap level of the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public static void FramebufferTexture2D(this IFramebuffer framebuffer, ITexture texture, int level, FrameAttachment attachment)
        {
            framebuffer.Bind();
            GL.FramebufferTexture2D(framebuffer, (uint)attachment, (uint)texture.Target, texture, level);
        }
        /// <summary>
        /// Attach a level of a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="level">Specifies the mipmap level of the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        /// <param name="offset">The offset into the 3d texture to be the 2d section to be attached.</param>
        [OpenGLSupport(3.0)]
        public static void FramebufferTexture3D(this IFramebuffer framebuffer, ITexture texture, int level, FrameAttachment attachment, int offset)
        {
            framebuffer.Bind();
            GL.FramebufferTexture3D(framebuffer, (uint)attachment, (uint)texture.Target, texture, level, offset);
        }

        /// <summary>
        /// Attach a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.2)]
        public static void FramebufferTexture(this IFramebuffer framebuffer, ITexture texture, FrameAttachment attachment)
        {
            framebuffer.Bind();
            GL.FramebufferTexture(framebuffer, (uint)attachment, texture, 0);
        }
        /// <summary>
        /// Attach a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public static void FramebufferTexture1D(this IFramebuffer framebuffer, ITexture texture, FrameAttachment attachment)
        {
            framebuffer.Bind();
            GL.FramebufferTexture1D(framebuffer, (uint)attachment, GLEnum.Texture1d, texture, 0);
        }
        /// <summary>
        /// Attach a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public static void FramebufferTexture2D(this IFramebuffer framebuffer, CubeMapFace textureTarget, ITexture texture, FrameAttachment attachment)
        {
            framebuffer.Bind();
            GL.FramebufferTexture2D(framebuffer, (uint)attachment, (uint)textureTarget, texture, 0);
        }
        /// <summary>
        /// Attach a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public static void FramebufferTexture2D(this IFramebuffer framebuffer, ITexture texture, FrameAttachment attachment)
        {
            framebuffer.Bind();
            GL.FramebufferTexture2D(framebuffer, (uint)attachment, (uint)texture.Target, texture, 0);
        }
        /// <summary>
        /// Attach a texture object as a logical buffer of this framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        /// <param name="offset">The offset into the 3d texture to be the 2d section to be attached.</param>
        [OpenGLSupport(3.0)]
        public static void FramebufferTexture3D(this IFramebuffer framebuffer, ITexture texture, FrameAttachment attachment, int offset)
        {
            framebuffer.Bind();
            GL.FramebufferTexture3D(framebuffer, (uint)attachment, (uint)texture.Target, texture, 0, offset);
        }

        /// <summary>
        /// Attach a single layer of a texture object as a logical buffer of a framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="level">Specifies the mipmap level of the texture object to attach.</param>
        /// <param name="layer">Specifies the layer of the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public static void FramebufferTextureLayer(this IFramebuffer framebuffer, ITexture texture, int level, int layer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            GL.FramebufferTextureLayer(framebuffer, (uint)attachment, texture, level, layer);
        }
        /// <summary>
        /// Attach a single layer of a texture object as a logical buffer of a framebuffer object.
        /// </summary>
        /// <param name="texture">Specifies the texture object to attach.</param>
        /// <param name="layer">Specifies the layer of the texture object to attach.</param>
        /// <param name="attachment">Specifies the attachment point of the framebuffer.</param>
        [OpenGLSupport(3.0)]
        public static void FramebufferTextureLayer(this IFramebuffer framebuffer, ITexture texture, int layer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            GL.FramebufferTextureLayer(framebuffer, (uint)attachment, texture, 0, layer);
        }

        /// <summary>
        /// Invalidate the content of some or all of this framebuffer's attachments.
        /// </summary>
        /// <param name="attachments">Specifies the attachments to be invalidated.</param>
        [OpenGLSupport(4.3)]
        public static void Invalidate(this IFramebuffer framebuffer, FrameAttachment[] attachments)
        {
            framebuffer.Bind();
            fixed (void* pointer = &attachments[0])
            {
                GL.InvalidateFramebuffer((uint)framebuffer.Binding, attachments.Length, (uint*)pointer);
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
        public static void InvalidateSub(this IFramebuffer framebuffer, FrameAttachment[] attachments, int x, int y, int width, int height)
        {
            framebuffer.Bind();
            fixed (void* pointer = &attachments[0])
            {
                GL.InvalidateSubFramebuffer((uint)framebuffer.Binding, attachments.Length, (uint*)pointer, x, y, width, height);
            }
        }

        /// <summary>
        /// Returns the number of bits in the red component of <paramref name="attachment"/>.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public static int GetRedSize(this IFramebuffer framebuffer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)framebuffer.Binding, (uint)attachment, GLEnum.FramebufferAttachmentRedSize, &value);

            return value;
        }

        /// <summary>
        /// Returns the number of bits in the green component of <paramref name="attachment"/>.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public static int GetGreenSize(this IFramebuffer framebuffer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)framebuffer.Binding, (uint)attachment, GLEnum.FramebufferAttachmentGreenSize, &value);

            return value;
        }

        /// <summary>
        /// Returns the number of bits in the blue component of <paramref name="attachment"/>.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public static int GetBlueSize(this IFramebuffer framebuffer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)framebuffer.Binding, (uint)attachment, GLEnum.FramebufferAttachmentBlueSize, &value);

            return value;
        }

        /// <summary>
        /// Returns the number of bits in the alpha component of <paramref name="attachment"/>.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public static int GetAlphaSize(this IFramebuffer framebuffer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)framebuffer.Binding, (uint)attachment, GLEnum.FramebufferAttachmentAlphaSize, &value);

            return value;
        }

        /// <summary>
        /// Returns the number of bits in the depth component of <paramref name="attachment"/>.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public static int GetDepthSize(this IFramebuffer framebuffer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)framebuffer.Binding, (uint)attachment, GLEnum.FramebufferAttachmentDepthSize, &value);

            return value;
        }

        /// <summary>
        /// Returns the number of bits in the stencil component of <paramref name="attachment"/>.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public static int GetStencilSize(this IFramebuffer framebuffer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)framebuffer.Binding, (uint)attachment, GLEnum.FramebufferAttachmentStencilSize, &value);

            return value;
        }

        /// <summary>
        /// Returns the format of components of <paramref name="attachment"/>.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public static ChannelType GetComponentType(this IFramebuffer framebuffer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)framebuffer.Binding, (uint)attachment, GLEnum.FramebufferAttachmentComponentType, &value);

            return (ChannelType)value;
        }

        /// <summary>
        /// Returns the encoding of components of <paramref name="attachment"/>.
        /// </summary>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public static ColourEncode GetColourEncoding(this IFramebuffer framebuffer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)framebuffer.Binding, (uint)attachment, GLEnum.FramebufferAttachmentColourEncoding, &value);

            return (ColourEncode)value;
        }

        /// <summary>
        /// Returns the storage type of <paramref name="attachment"/>.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public static AttachType GetObjectType(this IFramebuffer framebuffer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)framebuffer.Binding, (uint)attachment, GLEnum.FramebufferAttachmentObjectType, &value);

            return (AttachType)value;
        }

        /// <summary>
        /// Returns the name of the object which contains the attached image.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public static uint GetObjectId(this IFramebuffer framebuffer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)framebuffer.Binding, (uint)attachment, GLEnum.FramebufferAttachmentObjectName, &value);

            return (uint)value;
        }

        /// <summary>
        /// Returns the mipmap level of the texture object which contains the attached image.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public static int GetTextureLevel(this IFramebuffer framebuffer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)framebuffer.Binding, (uint)attachment, GLEnum.FramebufferAttachmentTextureLevel, &value);

            return value;
        }

        /// <summary>
        /// Returns the cube map face of the cubemap texture object which contains the attached image.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public static CubeMapFace GetTextureCubeMapFace(this IFramebuffer framebuffer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)framebuffer.Binding, (uint)attachment, GLEnum.FramebufferAttachmentTextureCubeMapFace, &value);

            return (CubeMapFace)value;
        }

        /// <summary>
        /// Returns a value indicating whether the <paramref name="attachment"/> is attached as a layered object.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.2)]
        public static bool GetLayered(this IFramebuffer framebuffer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)framebuffer.Binding, (uint)attachment, GLEnum.FramebufferAttachmentLayered, &value);

            return value == GLEnum.True;
        }

        /// <summary>
        /// Returns the texture layer which contains the attached image.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <param name="attachment">Specifies the attachment of the framebuffer object to query.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public static int GetTextureLayer(this IFramebuffer framebuffer, FrameAttachment attachment)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferAttachmentParameteriv((uint)framebuffer.Binding, (uint)attachment, GLEnum.FramebufferAttachmentTextureLayer, &value);

            return value;
        }

        //
        // OpenGL 4.3 parameters
        //

        /// <summary>
        /// Returns the defualt width of this framebuffer.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public static int GetDefaultWidth(this IFramebuffer framebuffer)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)framebuffer.Binding, GLEnum.FramebufferDefaultWidth, &value);

            return value;
        }

        /// <summary>
        /// Returns the defualt height of this framebuffer.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public static int GetDefaultHeight(this IFramebuffer framebuffer)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)framebuffer.Binding, GLEnum.FramebufferDefaultHeight, &value);

            return value;
        }

        /// <summary>
        /// Returns the defualt number of layers of this framebuffer.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public static int GetDefaultLayers(this IFramebuffer framebuffer)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)framebuffer.Binding, GLEnum.FramebufferDefaultLayers, &value);

            return value;
        }

        /// <summary>
        /// Returns the defualt number of samples of this framebuffer.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public static int GetDefaultSamples(this IFramebuffer framebuffer)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)framebuffer.Binding, GLEnum.FramebufferDefaultSamples, &value);

            return value;
        }

        /// <summary>
        /// Returns the defualt fixed sample location value of this framebuffer.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public static bool GetDefaultFixedSampleLocations(this IFramebuffer framebuffer)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)framebuffer.Binding, GLEnum.FramebufferDefaultFixedSampleLocations, &value);

            return value == GLEnum.True;
        }

        /// <summary>
        /// Returns a value indicating whether double buffering is supported for this framebuffer object.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <returns></returns>
        [OpenGLSupport(4.5)]
        public static bool GetDoubleBuffer(this IFramebuffer framebuffer)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)framebuffer.Binding, GLEnum.Doublebuffer, &value);

            return value == GLEnum.True;
        }

        /// <summary>
        /// Returns a value indicating the coverage mask size for this framebuffer object.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <returns></returns>
        [OpenGLSupport(4.5)]
        public static int GetSamples(this IFramebuffer framebuffer)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)framebuffer.Binding, GLEnum.Samples, &value);

            return value;
        }

        /// <summary>
        /// Returns a value indicating the number of sample buffers associated with this framebuffer object.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <returns></returns>
        [OpenGLSupport(4.5)]
        public static int GetSampleBuffers(this IFramebuffer framebuffer)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)framebuffer.Binding, GLEnum.SampleBuffers, &value);

            return value;
        }

        /// <summary>
        /// Returns a value indicating whether stereo buffers (left and right) are supported for this framebuffer object.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <returns></returns>
        [OpenGLSupport(4.5)]
        public static bool GetStereo(this IFramebuffer framebuffer)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)framebuffer.Binding, GLEnum.Stereo, &value);

            return value == GLEnum.True;
        }

        /// <summary>
        /// Returns a value indicating the preferred pixel data format for this framebuffer object.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <returns></returns>
        [OpenGLSupport(4.5)]
        public static BaseFormat GetColourReadFormat(this IFramebuffer framebuffer)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)framebuffer.Binding, GLEnum.Doublebuffer, &value);

            return (BaseFormat)value;
        }

        /// <summary>
        /// Returns a value indicating the implementation's preferred pixel data type for this framebuffer object.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="FramebufferProperties"/> for framebuffer related parameters.
        /// </remarks>
        /// <returns></returns>
        [OpenGLSupport(4.5)]
        public static TextureData GetColourReadType(this IFramebuffer framebuffer)
        {
            framebuffer.Bind();
            int value = 0;

            GL.GetFramebufferParameteriv((uint)framebuffer.Binding, GLEnum.Doublebuffer, &value);

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
        public static void SetDefaultWidth(this IFramebuffer framebuffer, int value)
        {
            framebuffer.Bind();
            GL.FramebufferParameteri((uint)framebuffer.Binding, GLEnum.FramebufferDefaultWidth, value);
        }

        /// <summary>
        /// Specifies the assumed height for this framebuffer object if with no attachments.
        /// </summary>
        /// <param name="value">The value to set to the parameter.</param>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public static void SetDefaultHeight(this IFramebuffer framebuffer, int value)
        {
            framebuffer.Bind();
            GL.FramebufferParameteri((uint)framebuffer.Binding, GLEnum.FramebufferDefaultHeight, value);
        }

        /// <summary>
        /// Specifies the assumed number of layers for this framebuffer object if with no attachments.
        /// </summary>
        /// <param name="value">The value to set to the parameter.</param>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public static void SetDefaultLayers(this IFramebuffer framebuffer, int value)
        {
            framebuffer.Bind();
            GL.FramebufferParameteri((uint)framebuffer.Binding, GLEnum.FramebufferDefaultLayers, value);
        }

        /// <summary>
        /// Specifies the assumed number of samples for this framebuffer object if with no attachments.
        /// </summary>
        /// <param name="value">The value to set to the parameter.</param>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public static void SetDefaultSamples(this IFramebuffer framebuffer, int value)
        {
            framebuffer.Bind();
            GL.FramebufferParameteri((uint)framebuffer.Binding, GLEnum.FramebufferDefaultSamples, value);
        }

        /// <summary>
        /// Specifies whether this framebuffer should assume identical sample locations and the same number of samples for all texels in the virtual image.
        /// </summary>
        /// <param name="value">The value to set to the parameter.</param>
        /// <returns></returns>
        [OpenGLSupport(4.3)]
        public static void SetDefaultFixedSampleLocations(this IFramebuffer framebuffer, bool value)
        {
            framebuffer.Bind();
            GL.FramebufferParameteri((uint)framebuffer.Binding, GLEnum.FramebufferDefaultFixedSampleLocations, value ? 1 : 0);
        }
    }
}
