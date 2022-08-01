using System;
using Zene.Structs;

namespace Zene.Graphics.Base
{
    /// <summary>
    /// The most basic implimentation of an OpenGL texture.
    /// </summary>
    [OpenGLSupport(1.3)]
    public unsafe class TextureGL : ITexture
    {
        /// <summary>
        /// Creates an OpenGL texture object based on a given <see cref="TextureTarget"/>.
        /// </summary>
        /// <param name="target">The type of texture to create.</param>
        public TextureGL(TextureTarget target)
        {
            Id = GL.GenTexture();
            Target = target;

            Properties = new TextureProperties(this);
        }

        public uint Id { get; }

        public TextureTarget Target { get; }
        public TextureFormat InternalFormat => Properties.InternalFormat;

        public TextureProperties Properties { get; }

        public uint ReferanceSlot { get; private set; } = 0;

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            Dispose(true);

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                if (GL.Version >= 3.0)
                {
                    GL.DeleteTexture(Id);
                }
            }
        }

        [OpenGLSupport(1.1)]
        public void Bind()
        {
            if (this.Bound(ReferanceSlot)) { return; }

            GL.ActiveTexture(GLEnum.Texture0 + ReferanceSlot);
            GL.BindTexture((uint)Target, Id);
        }
        [OpenGLSupport(1.3)]
        public void Bind(uint slot)
        {
            if (!this.Bound(slot))
            {
                GL.ActiveTexture(GLEnum.Texture0 + slot);
                ReferanceSlot = slot;
                GL.BindTexture((uint)Target, Id);
                return;
            }

            if (State.ActiveTexture != slot)
            {
                GL.ActiveTexture(GLEnum.Texture0 + slot);
            }
        }
        /// <summary>
        /// Bind the texture to a image unit.
        /// </summary>
        /// <param name="unit">Specifies the index of the image unit to which to bind the texture.</param>
        [OpenGLSupport(4.5)]
        protected void BindUnit(uint unit)
        {
            if (!this.Bound(unit))
            {
                ReferanceSlot = unit;
                GL.BindTextureUnit((uint)Target, this);
                return;
            }

            if (State.ActiveTexture != unit)
            {
                GL.ActiveTexture(GLEnum.Texture0 + unit);
            }
        }
        /// <summary>
        /// Bind a level of the texture to an image unit.
        /// </summary>
        /// <param name="unit">Specifies the index of the image unit to which to bind the texture.</param>
        /// <param name="level">Specifies the level of the texture that is to be bound.</param>
        /// <param name="layered">Specifies whether a layered texture binding is to be established.</param>
        /// <param name="layer">If <paramref name="layered"/>> is <see cref="false"/>, specifies the layer of texture to be bound to the image unit. Ignored otherwise.</param>
        /// <param name="access">Specifies a token indicating the type of access that will be performed on the image.</param>
        [OpenGLSupport(4.2)]
        protected void BindLevel(uint unit, int level, bool layered, int layer, AccessType access)
        {
            ReferanceSlot = unit;
            GL.BindImageTexture(unit, this, level, layered, layer, (uint)access, (uint)InternalFormat);
        }
        [OpenGLSupport(1.1)]
        public void Unbind()
        {
            if (!this.Bound()) { return; }

            State.ActiveTexture = ReferanceSlot;
            GL.BindTexture((uint)Target, 0);
        }

        /// <summary>
        /// Fills all a texture image with a constant value.
        /// </summary>
        /// <typeparam name="T"><paramref name="type"/></typeparam>
        /// <param name="level">The level containing the region to be cleared.</param>
        /// <param name="format">The format of the data whose address in memory is given by <paramref name="data"/>.</param>
        /// <param name="type">The type of the data whose address in memory is given by <paramref name="data"/>.</param>
        /// <param name="data">The data to be used to clear the specified region.</param>
        [OpenGLSupport(4.4)]
        protected void ClearTextureImage<T>(int level, BaseFormat format, TextureData type, T data) where T : unmanaged
        {
            GL.ClearTexImage(Id, level, (uint)format, (uint)type, &data);
        }
        /// <summary>
        /// Fills all a texture image with a constant value.
        /// </summary>
        /// <typeparam name="T"><paramref name="type"/></typeparam>
        /// <param name="level">The level containing the region to be cleared.</param>
        /// <param name="xOffset">The coordinate of the left edge of the region to be cleared.</param>
        /// <param name="yOffset">The coordinate of the lower edge of the region to be cleared.</param>
        /// <param name="zOffset">The coordinate of the front of the region to be cleared.</param>
        /// <param name="width">The width of the region to be cleared.</param>
        /// <param name="height">The height of the region to be cleared.</param>
        /// <param name="depth">The depth of the region to be cleared.</param>
        /// <param name="format">The format of the data whose address in memory is given by <paramref name="data"/>.</param>
        /// <param name="type">The type of the data whose address in memory is given by <paramref name="data"/>.</param>
        /// <param name="data">The data to be used to clear the specified region.</param>
        [OpenGLSupport(4.4)]
        protected void ClearTextureSubImage<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, BaseFormat format, TextureData type, T data) where T : unmanaged
        {
            GL.ClearTexSubImage(Id, level, xOffset, yOffset, zOffset, width, height, depth, (uint)format, (uint)type, &data);
        }
        /// <summary>
        /// Fills all a texture image with a constant value.
        /// </summary>
        /// <typeparam name="T"><paramref name="type"/></typeparam>
        /// <param name="level">The level containing the region to be cleared.</param>
        /// <param name="format">The format of the data whose address in memory is given by <paramref name="data"/>.</param>
        /// <param name="type">The type of the data whose address in memory is given by <paramref name="data"/>.</param>
        /// <param name="data">The data to be used to clear the specified region.</param>
        [OpenGLSupport(4.4)]
        protected void ClearTextureImage<T>(int level, BaseFormat format, TextureData type, T[] data) where T : unmanaged
        {
            fixed (T* ptr = &data[0])
            {
                GL.ClearTexImage(Id, level, (uint)format, (uint)type, ptr);
            }
        }
        /// <summary>
        /// Fills all a texture image with a constant value.
        /// </summary>
        /// <typeparam name="T"><paramref name="type"/></typeparam>
        /// <param name="level">The level containing the region to be cleared.</param>
        /// <param name="xOffset">The coordinate of the left edge of the region to be cleared.</param>
        /// <param name="yOffset">The coordinate of the lower edge of the region to be cleared.</param>
        /// <param name="zOffset">The coordinate of the front of the region to be cleared.</param>
        /// <param name="width">The width of the region to be cleared.</param>
        /// <param name="height">The height of the region to be cleared.</param>
        /// <param name="depth">The depth of the region to be cleared.</param>
        /// <param name="format">The format of the data whose address in memory is given by <paramref name="data"/>.</param>
        /// <param name="type">The type of the data whose address in memory is given by <paramref name="data"/>.</param>
        /// <param name="data">The data to be used to clear the specified region.</param>
        [OpenGLSupport(4.4)]
        protected void ClearTextureSubImage<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, BaseFormat format, TextureData type, T[] data) where T : unmanaged
        {
            fixed (T* ptr = &data[0])
            {
                GL.ClearTexSubImage(Id, level, xOffset, yOffset, zOffset, width, height, depth, (uint)format, (uint)type, ptr);
            }
        }

        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the format of the compressed image data stored at address <paramref name="data"/>.</param>
        /// <param name="size">Specifies the width of the texture image. All implementations support texture images that are at least 64 texels wide. The height of the 1D texture image is 1.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexImage1D<T>(int level, TextureFormat intFormat, int size, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage1D(this, level, (uint)intFormat, size, 0, data.Size * sizeof(T), data);
        }
        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the format of the compressed image data stored at address <paramref name="data"/>.</param>
        /// <param name="size">Specifies the width of the texture image. All implementations support texture images that are at least 64 texels wide. The height of the 1D texture image is 1.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the format of the compressed image data stored at address <paramref name="data"/>.</param>
        /// <param name="size">Specifies the width of the texture image. All implementations support texture images that are at least 64 texels wide. The height of the 1D texture image is 1.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexImage1D<T>(int level, TextureFormat intFormat, int size, int imageSize, T* data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage1D(this, level, (uint)intFormat, size, 0, imageSize, data);
        }
        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the format of the compressed image data stored at address <paramref name="data"/>.</param>
        /// <param name="size">Specifies the width of the texture image. All implementations support texture images that are at least 64 texels wide. The height of the 1D texture image is 1.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexImage1D(int level, TextureFormat intFormat, int size, int imageSize, IntPtr data)
        {
            Bind();
            GL.CompressedTexImage1D(this, level, (uint)intFormat, size, 0, imageSize, data.ToPointer());
        }

        /// <summary>
        /// Specify a two-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the format of the compressed image data stored at address <paramref name="data"/>.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support 2D texture and cube map texture images that are at least 16384 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image. All implementations support 2D texture and cube map texture images that are at least 16384 texels high.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexImage2D<T>(int level, TextureFormat intFormat, int width, int height, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage2D(this, level, (uint)intFormat, width, height, 0, data.Size * sizeof(T), data);
        }
        /// <summary>
        /// Specify a two-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the format of the compressed image data stored at address <paramref name="data"/>.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support 2D texture and cube map texture images that are at least 16384 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image. All implementations support 2D texture and cube map texture images that are at least 16384 texels high.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexImage2D<T>(CubeMapFace target, int level, TextureFormat intFormat, int width, int height, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage2D(this, level, (uint)intFormat, width, height, 0, data.Size * sizeof(T), data);
        }
        /// <summary>
        /// Specify a two-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the format of the compressed image data stored at address <paramref name="data"/>.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support 2D texture and cube map texture images that are at least 16384 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image. All implementations support 2D texture and cube map texture images that are at least 16384 texels high.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexImage2D<T>(int level, TextureFormat intFormat, int width, int height, int imageSize, T* data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage2D(this, level, (uint)intFormat, width, height, 0, imageSize, data);
        }
        /// <summary>
        /// Specify a two-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the format of the compressed image data stored at address <paramref name="data"/>.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support 2D texture and cube map texture images that are at least 16384 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image. All implementations support 2D texture and cube map texture images that are at least 16384 texels high.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexImage2D<T>(CubeMapFace target, int level, TextureFormat intFormat, int width, int height, int imageSize, T* data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage2D(this, target, level, (uint)intFormat, width, height, 0, imageSize, data);
        }
        /// <summary>
        /// Specify a two-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the format of the compressed image data stored at address <paramref name="data"/>.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support 2D texture and cube map texture images that are at least 16384 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image. All implementations support 2D texture and cube map texture images that are at least 16384 texels high.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexImage2D(int level, TextureFormat intFormat, int width, int height, int imageSize, IntPtr data)
        {
            Bind();
            GL.CompressedTexImage2D(this, level, (uint)intFormat, width, height, 0, imageSize, data.ToPointer());
        }
        /// <summary>
        /// Specify a two-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the format of the compressed image data stored at address <paramref name="data"/>.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support 2D texture and cube map texture images that are at least 16384 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image. All implementations support 2D texture and cube map texture images that are at least 16384 texels high.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexImage2D(CubeMapFace target, int level, TextureFormat intFormat, int width, int height, int imageSize, IntPtr data)
        {
            Bind();
            GL.CompressedTexImage2D(this, target, level, (uint)intFormat, width, height, 0, imageSize, data.ToPointer());
        }

        /// <summary>
        /// Specify a three-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the format of the compressed image data stored at address <paramref name="data"/>.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support 3D texture images that are at least 16 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image. All implementations support 3D texture images that are at least 16 texels high.</param>
        /// <param name="depth">Specifies the depth of the texture image. All implementations support 3D texture images that are at least 16 texels deep.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexImage3D<T>(int level, TextureFormat intFormat, int width, int height, int depth, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage3D(this, level, (uint)intFormat, width, height, depth, 0, data.Size * sizeof(T), data);
        }
        /// <summary>
        /// Specify a three-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the format of the compressed image data stored at address <paramref name="data"/>.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support 3D texture images that are at least 16 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image. All implementations support 3D texture images that are at least 16 texels high.</param>
        /// <param name="depth">Specifies the depth of the texture image. All implementations support 3D texture images that are at least 16 texels deep.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        /// <summary>
        /// Specify a three-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the format of the compressed image data stored at address <paramref name="data"/>.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support 3D texture images that are at least 16 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image. All implementations support 3D texture images that are at least 16 texels high.</param>
        /// <param name="depth">Specifies the depth of the texture image. All implementations support 3D texture images that are at least 16 texels deep.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexImage3D<T>(int level, TextureFormat intFormat, int width, int height, int depth, int imageSize, T* data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage3D(this, level, (uint)intFormat, width, height, depth, 0, imageSize, data);
        }
        /// <summary>
        /// Specify a three-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the format of the compressed image data stored at address <paramref name="data"/>.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support 3D texture images that are at least 16 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image. All implementations support 3D texture images that are at least 16 texels high.</param>
        /// <param name="depth">Specifies the depth of the texture image. All implementations support 3D texture images that are at least 16 texels deep.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        /// <summary>
        /// Specify a three-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the format of the compressed image data stored at address <paramref name="data"/>.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support 3D texture images that are at least 16 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image. All implementations support 3D texture images that are at least 16 texels high.</param>
        /// <param name="depth">Specifies the depth of the texture image. All implementations support 3D texture images that are at least 16 texels deep.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexImage3D(int level, TextureFormat intFormat, int width, int height, int depth, int imageSize, IntPtr data)
        {
            Bind();
            GL.CompressedTexImage3D(this, level, (uint)intFormat, width, height, depth, 0, imageSize, data.ToPointer());
        }

        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="offset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="size">Specifies the width of the texture subimage.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexSubImage1D<T>(int level, int offset, int size, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexSubImage1D((uint)Target, level, offset, size, (uint)InternalFormat, data.Size * sizeof(T), data);
        }
        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="offset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="size">Specifies the width of the texture subimage.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexSubImage1D<T>(int level, int offset, int size, int imageSize, T* data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexSubImage1D((uint)Target, level, offset, size, (uint)InternalFormat, imageSize, data);
        }
        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="offset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="size">Specifies the width of the texture subimage.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexSubImage1D(int level, int offset, int size, int imageSize, IntPtr data)
        {
            Bind();
            GL.CompressedTexSubImage1D((uint)Target, level, offset, size, (uint)InternalFormat, imageSize, data.ToPointer());
        }

        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexSubImage2D<T>(int level, int xOffset, int yOffset, int width, int height, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexSubImage2D((uint)Target, level, xOffset, yOffset, width, height, (uint)InternalFormat, data.Size * sizeof(T), data);
        }
        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexSubImage2D<T>(int level, int xOffset, int yOffset, int width, int height, int imageSize, T* data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexSubImage2D((uint)Target, level, xOffset, yOffset, width, height, (uint)InternalFormat, imageSize, data);
        }
        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexSubImage2D(int level, int xOffset, int yOffset, int width, int height, int imageSize, IntPtr data)
        {
            Bind();
            GL.CompressedTexSubImage2D((uint)Target, level, xOffset, yOffset, width, height, (uint)InternalFormat, imageSize, data.ToPointer());
        }
        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexSubImage2D<T>(CubeMapFace target, int level, int xOffset, int yOffset, int width, int height, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexSubImage2D((uint)target, level, xOffset, yOffset, width, height, (uint)InternalFormat, data.Size * sizeof(T), data);
        }
        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexSubImage2D<T>(CubeMapFace target, int level, int xOffset, int yOffset, int width, int height, int imageSize, T* data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexSubImage2D((uint)target, level, xOffset, yOffset, width, height, (uint)InternalFormat, imageSize, data);
        }
        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexSubImage2D(CubeMapFace target, int level, int xOffset, int yOffset, int width, int height, int imageSize, IntPtr data)
        {
            Bind();
            GL.CompressedTexSubImage2D((uint)target, level, xOffset, yOffset, width, height, (uint)InternalFormat, imageSize, data.ToPointer());
        }

        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="zOffset">Specifies a texel offset in the z direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="depth">Specifies the depth of the texture subimage.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexSubImage3D<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexSubImage3D((uint)Target, level, xOffset, yOffset, zOffset, width, height, depth, (uint)InternalFormat, data.Size * sizeof(T), data);
        }
        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="zOffset">Specifies a texel offset in the z direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="depth">Specifies the depth of the texture subimage.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexSubImage3D<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, int imageSize, T* data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexSubImage3D((uint)Target, level, xOffset, yOffset, zOffset, width, height, depth, (uint)InternalFormat, imageSize, data);
        }
        /// <summary>
        /// Specify a one-dimensional texture image in a compressed format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="zOffset">Specifies a texel offset in the z direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="depth">Specifies the depth of the texture subimage.</param>
        /// <param name="imageSize">Specifies the number of unsigned bytes of image data starting at the address specified by <paramref name="data"/>.</param>
        /// <param name="data">Specifies the compressed image data.</param>
        [OpenGLSupport(1.3)]
        protected void CompressedTexSubImage3D(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, int imageSize, IntPtr data)
        {
            Bind();
            GL.CompressedTexSubImage3D((uint)Target, level, xOffset, yOffset, zOffset, width, height, depth, (uint)InternalFormat, imageSize, data.ToPointer());
        }

        /// <summary>
        /// Perform a raw data copy between two images.
        /// </summary>
        /// <param name="source">The texture object from which to copy.</param>
        /// <param name="srcLevel">The mipmap level to read from the source.</param>
        /// <param name="srcX">The X coordinate of the left edge of the souce region to copy.</param>
        /// <param name="srcY">The Y coordinate of the top edge of the souce region to copy.</param>
        /// <param name="srcZ">The Z coordinate of the near edge of the souce region to copy.</param>
        /// <param name="width">The width of the region to be copied.</param>
        /// <param name="height">The height of the region to be copied.</param>
        /// <param name="depth">The depth of the region to be copied.</param>
        /// <param name="level">The mipmap level to write to the destination.</param>
        /// <param name="x">The X coordinate of the left edge of the destination region.</param>
        /// <param name="y">The Y coordinate of the top edge of the destination region.</param>
        /// <param name="z">The Z coordinate of the near edge of the destination region.</param>
        [OpenGLSupport(4.3)]
        protected void CopyImageSubData(ITexture source, int srcLevel, int srcX, int srcY, int srcZ, int width, int height, int depth, int level, int x, int y, int z)
        {
            GL.CopyImageSubData(source.Id, (uint)source.Target, srcLevel, srcX, srcY, srcZ, Id, (uint)Target, level, x, y, z, width, height, depth);
        }
        /// <summary>
        /// Perform a raw data copy between two images.
        /// </summary>
        /// <param name="source">The renderbuffer object from which to copy.</param>
        /// <param name="srcLevel">The mipmap level to read from the source.</param>
        /// <param name="srcX">The X coordinate of the left edge of the souce region to copy.</param>
        /// <param name="srcY">The Y coordinate of the top edge of the souce region to copy.</param>
        /// <param name="srcZ">The Z coordinate of the near edge of the souce region to copy.</param>
        /// <param name="width">The width of the region to be copied.</param>
        /// <param name="height">The height of the region to be copied.</param>
        /// <param name="depth">The depth of the region to be copied.</param>
        /// <param name="level">The mipmap level to write to the destination.</param>
        /// <param name="x">The X coordinate of the left edge of the destination region.</param>
        /// <param name="y">The Y coordinate of the top edge of the destination region.</param>
        /// <param name="z">The Z coordinate of the near edge of the destination region.</param>
        [OpenGLSupport(4.3)]
        protected void CopyImageSubData(IRenderbuffer source, int srcLevel, int srcX, int srcY, int srcZ, int width, int height, int depth, int level, int x, int y, int z)
        {
            GL.CopyImageSubData(source.Id, GLEnum.Renderbuffer, srcLevel, srcX, srcY, srcZ, Id, (uint)Target, level, x, y, z, width, height, depth);
        }
        /// <summary>
        /// Perform a raw data copy between two images.
        /// </summary>
        /// <param name="source">The texture object from which to copy.</param>
        /// <param name="srcLevel">The mipmap level to read from the source.</param>
        /// <param name="srcX">The X coordinate of the left edge of the souce region to copy.</param>
        /// <param name="srcY">The Y coordinate of the top edge of the souce region to copy.</param>
        /// <param name="srcZ">The Z coordinate of the near edge of the souce region to copy.</param>
        /// <param name="width">The width of the region to be copied.</param>
        /// <param name="height">The height of the region to be copied.</param>
        /// <param name="depth">The depth of the region to be copied.</param>
        /// <param name="level">The mipmap level to write to the destination.</param>
        /// <param name="x">The X coordinate of the left edge of the destination region.</param>
        /// <param name="y">The Y coordinate of the top edge of the destination region.</param>
        /// <param name="z">The Z coordinate of the near edge of the destination region.</param>
        [OpenGLSupport(4.3)]
        protected void CopyImageSubData(ITexture source, CubeMapFace srcTarget, int srcLevel, int srcX, int srcY, int srcZ, int width, int height, int depth, CubeMapFace target, int level, int x, int y, int z)
        {
            GL.CopyImageSubData(source.Id, (uint)srcTarget, srcLevel, srcX, srcY, srcZ, Id, (uint)target, level, x, y, z, width, height, depth);
        }

        /// <summary>
        /// Copy pixels into a 1D texture image.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the internal format of the texture.</param>
        /// <param name="x">Specify the x-axis window coordinate of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="y">Specify the y-axis window coordinate of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="size">Specifies the width of the texture image. The height of the texture image is 1.</param>
        [OpenGLSupport(1.1)]
        protected void CopyTexImage1D(int level, TextureFormat intFormat, int x, int y, int size)
        {
            Bind();
            GL.CopyTexImage1D(this, level, (uint)intFormat, x, y, size, 0);
        }
        /// <summary>
        /// Copy pixels into a 2D texture image.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the internal format of the texture.</param>
        /// <param name="x">Specify the x-axis window coordinate of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="y">Specify the y-axis window coordinate of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture image.</param>
        /// <param name="height">Specifies the height of the texture image.</param>
        [OpenGLSupport(1.1)]
        protected void CopyTexImage2D(int level, TextureFormat intFormat, int x, int y, int width, int height)
        {
            Bind();
            GL.CopyTexImage2D(this, level, (uint)intFormat, x, y, width, height, 0);
        }
        /// <summary>
        /// Copy pixels into a 2D texture image.
        /// </summary>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the internal format of the texture.</param>
        /// <param name="x">Specify the x-axis window coordinate of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="y">Specify the y-axis window coordinate of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        [OpenGLSupport(1.1)]
        protected void CopyTexImage2D(CubeMapFace target, int level, TextureFormat intFormat, int x, int y, int width, int height)
        {
            Bind();
            GL.CopyTexImage2D(this, target, level, (uint)intFormat, x, y, width, height, 0);
        }

        /// <summary>
        /// Copy a one-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="offset">Specifies the texel offset within the texture array.</param>
        /// <param name="x">Specify the x-axis window coordinate of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="y">Specify the y-axis window coordinate of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        [OpenGLSupport(1.1)]
        protected void CopyTexSubImage1D(int level, int offset, int x, int y, int width)
        {
            Bind();
            GL.CopyTexSubImage1D((uint)Target, level, offset, x, y, width);
        }
        /// <summary>
        /// Copy a two-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param> 
        /// <param name="x">Specify the x-axis window coordinate of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="y">Specify the y-axis window coordinate of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        [OpenGLSupport(1.1)]
        protected void CopyTexSubImage2D(int level, int xOffset, int yOffset, int x, int y, int width, int height)
        {
            Bind();
            GL.CopyTexSubImage2D((uint)Target, level, xOffset, yOffset, x, y, width, height);
        }
        /// <summary>
        /// Copy a two-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param> 
        /// <param name="x">Specify the x-axis window coordinate of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="y">Specify the y-axis window coordinate of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        [OpenGLSupport(1.1)]
        protected void CopyTexSubImage2D(CubeMapFace target, int level, int xOffset, int yOffset, int x, int y, int width, int height)
        {
            Bind();
            GL.CopyTexSubImage2D((uint)target, level, xOffset, yOffset, x, y, width, height);
        }
        /// <summary>
        /// Copy a three-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="zOffset">Specifies a texel offset in the z direction within the texture array.</param>
        /// <param name="x">Specify the x-axis window coordinate of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="y">Specify the y-axis window coordinate of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        [OpenGLSupport(1.2)]
        protected void CopyTexSubImage3D(int level, int xOffset, int yOffset, int zOffset, int x, int y, int width, int height)
        {
            Bind();
            GL.CopyTexSubImage3D((uint)Target, level, xOffset, yOffset, zOffset, x, y, width, height);
        }

        /// <summary>
        /// Return a compressed texture image.
        /// </summary>
        /// <typeparam name="T">The type of the returned array.</typeparam>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the n-th mipmap reduction image.</param>
        /// <returns>The compressed texture image.</returns>
        [OpenGLSupport(1.3)]
        protected T[] GetCompressedTexImage<T>(int level) where T : unmanaged
        {
            Bind();
            T[] output = new T[Properties.CompressedImageSize / sizeof(T)];

            fixed (T* pixelData = &output[0])
            {
                GL.GetCompressedTexImage((uint)Target, level, pixelData);
            }

            return output;
        }
        /// <summary>
        /// Retrieve a sub-region of a compressed texture image from a compressed texture object.
        /// </summary>
        /// <typeparam name="T">The type of the returned array.</typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="zOffset">Specifies a texel offset in the z direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage. Must be a multiple of the compressed block's width,
        /// unless the offset is zero and the size equals the texture image size.</param>
        /// <param name="height">Specifies the height of the texture subimage. Must be a multiple of the compressed block's height,
        /// unless the offset is zero and the size equals the texture image size.</param>
        /// <param name="depth">Specifies the depth of the texture subimage. Must be a multiple of the compressed block's depth,
        /// unless the offset is zero and the size equals the texture image size.</param>
        /// <param name="bufferSize">Specifies the size of the buffer to receive the retrieved pixel data.</param>
        /// <returns>The texture subimage.</returns>
        [OpenGLSupport(4.5)]
        protected T[] GetCompressedTextureSubImage<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, int bufferSize) where T : unmanaged
        {
            Bind();
            T[] output = new T[bufferSize / sizeof(T)];

            fixed (T* pixelData = &output[0])
            {
                GL.GetCompressedTextureSubImage((uint)Target, level, xOffset, yOffset, zOffset, width, height, depth, bufferSize, pixelData);
            }

            return output;
        }
        /// <summary>
        /// Return a compressed texture image.
        /// </summary>
        /// <typeparam name="T">The type of the returned array.</typeparam>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the n-th mipmap reduction image.</param>
        /// <returns>The compressed texture image.</returns>
        [OpenGLSupport(1.3)]
        protected T[] GetCompressedTexImage<T>(CubeMapFace target, int level) where T : unmanaged
        {
            Bind();
            T[] output = new T[Properties.CompressedImageSize / sizeof(T)];

            fixed (T* pixelData = &output[0])
            {
                GL.GetCompressedTexImage((uint)target, level, pixelData);
            }

            return output;
        }
        /// <summary>
        /// Retrieve a sub-region of a compressed texture image from a compressed texture object.
        /// </summary>
        /// <typeparam name="T">The type of the returned array.</typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="zOffset">Specifies a texel offset in the z direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage. Must be a multiple of the compressed block's width,
        /// unless the offset is zero and the size equals the texture image size.</param>
        /// <param name="height">Specifies the height of the texture subimage. Must be a multiple of the compressed block's height,
        /// unless the offset is zero and the size equals the texture image size.</param>
        /// <param name="depth">Specifies the depth of the texture subimage. Must be a multiple of the compressed block's depth,
        /// unless the offset is zero and the size equals the texture image size.</param>
        /// <param name="bufferSize">Specifies the size of the buffer to receive the retrieved pixel data.</param>
        /// <returns>The texture subimage.</returns>
        [OpenGLSupport(4.5)]
        protected T[] GetCompressedTextureSubImage<T>(CubeMapFace target, int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, int bufferSize) where T : unmanaged
        {
            Bind();
            T[] output = new T[bufferSize / sizeof(T)];

            fixed (T* pixelData = &output[0])
            {
                GL.GetCompressedTextureSubImage((uint)target, level, xOffset, yOffset, zOffset, width, height, depth, bufferSize, pixelData);
            }

            return output;
        }

        /// <summary>
        /// Return a texture image.
        /// </summary>
        /// <typeparam name="T">The type of the returned array.</typeparam>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="format">Specifies a pixel format for the returned data.</param>
        /// <param name="type">Specifies a pixel type for the returned data.</param>
        /// <returns>The texture image. Should be of the type specified by <paramref name="type"/>.</returns>
        [OpenGLSupport(1.0)]
        protected GLArray<T> GetTexImage<T>(int level, BaseFormat format, TextureData type) where T : unmanaged
        {
            Bind();
            Vector3I size = Properties.GetMipMapSize(level);
            GLArray<T> output = new GLArray<T>(
                (size.X * format.GetSize() * type.GetSize()) / sizeof(T),
                size.Y, size.Z);

            GL.GetTexImage((uint)Target, level, (uint)format, (uint)type, output);

            return output;
        }
        /// <summary>
        /// Return a texture image.
        /// </summary>
        /// <typeparam name="T">The type of the returned array.</typeparam>
        /// <param name="face">The cubemap face to referance.</param>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="format">Specifies a pixel format for the returned data.</param>
        /// <param name="type">Specifies a pixel type for the returned data.</param>
        /// <returns>The texture image. Should be of the type specified by <paramref name="type"/>.</returns>
        [OpenGLSupport(1.0)]
        protected GLArray<T> GetTexImage<T>(CubeMapFace face, int level, BaseFormat format, TextureData type) where T : unmanaged
        {
            Bind();
            Vector3I size = Properties.GetMipMapSize(level);
            GLArray<T> output = new GLArray<T>(
                (size.X * format.GetSize() * type.GetSize()) / sizeof(T),
                size.Y, size.Z);

            GL.GetTexImage((uint)face, level, (uint)format, (uint)type, output);

            return output;
        }
        /// <summary>
        /// Retrieve a sub-region of a texture image from a texture object.
        /// </summary>
        /// <typeparam name="T">The type of the returned array.</typeparam>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="zOffset">Specifies a texel offset in the z direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="depth">Specifies the depth of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <returns>The texture subimage. Should be of the type specified by <paramref name="type"/>.</returns>
        [OpenGLSupport(4.5)]
        protected GLArray<T> GetTextureSubImage<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, BaseFormat format, TextureData type) where T : unmanaged
        {
            int channelByte = format.GetSize() * type.GetSize();

            GLArray<T> output = new GLArray<T>(
                (width * channelByte) / sizeof(T),
                height, depth);

            GL.GetTextureSubImage(Id, level, xOffset, yOffset, zOffset, width, height, depth, (uint)format, (uint)type, width * height * depth * channelByte, output);

            return output;
        }

        /// <summary>
        /// Generate mipmaps for a specified texture object.
        /// </summary>
        [OpenGLSupport(3.0)]
        protected void GenerateMipmap()
        {
            Bind();
            GL.GenerateMipmap((uint)Target);
        }

        /// <summary>
        /// Invalidate the entirety a texture image.
        /// </summary>
        /// <param name="level">The level of detail of the texture object to invalidate.</param>
        [OpenGLSupport(4.3)]
        protected void InvalidateTexImage(int level)
        {
            Bind();
            GL.InvalidateTexImage(Id, level);
        }
        /// <summary>
        /// Invalidate a region of a texture image.
        /// </summary>
        /// <param name="level">The level of detail of the texture object within which the region resides.</param>
        /// <param name="xOffset">The X offset of the region to be invalidated.</param>
        /// <param name="yOffset">The Y offset of the region to be invalidated.</param>
        /// <param name="zOffset">The Z offset of the region to be invalidated.</param>
        /// <param name="width">The width of the region to be invalidated.</param>
        /// <param name="height">The height of the region to be invalidated.</param>
        /// <param name="depth">The depth of the region to be invalidated.</param>
        [OpenGLSupport(4.3)]
        protected void InvalidateTexSubImage(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth)
        {
            Bind();
            GL.InvalidateTexSubImage(Id, level, xOffset, yOffset, zOffset, width, height, depth);
        }

        /// <summary>
        /// Attach a buffer object's data store to a buffer texture object.
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object whose storage to attach to the active buffer texture.</param>
        /// <param name="intFormat">Specifies the internal format of the data in the store belonging to <paramref name="buffer"/>.</param>
        [OpenGLSupport(3.1)]
        protected void TexBuffer(IBuffer buffer, TextureFormat intFormat)
        {
            Bind();
            GL.TexBuffer(this, (uint)intFormat, buffer);
        }
        /// <summary>
        /// Attach a range of a buffer object's data store to a buffer texture object.
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object whose storage to attach to the active buffer texture.</param>
        /// <param name="intFormat">Specifies the internal format of the data in the store belonging to <paramref name="buffer"/>.</param>
        /// <param name="offset">Specifies the offset of the start of the range of the buffer's data store to attach.</param>
        /// <param name="size">Specifies the size of the range of the buffer's data store to attach.</param>
        [OpenGLSupport(4.3)]
        protected void TexBufferRange(IBuffer buffer, TextureFormat intFormat, int offset, int size)
        {
            Bind();
            GL.TexBufferRange(this, (uint)intFormat, buffer.Id, offset, size);
        }

        /// <summary>
        /// Specify a one-dimensional texture image.
        /// </summary>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the number of colour components in the texture.</param>
        /// <param name="size">Specifies the width of the texture image. All implementations support texture images that are at least 1024 texels wide.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.0)]
        protected void TexImage1D<T>(int level, TextureFormat intFormat, int size, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
        {
            Bind();
            GL.TexImage1D(this, level, (int)intFormat, size, 0, (uint)format, (uint)type, dataPtr);
        }
        /// <summary>
        /// Specify a one-dimensional texture image.
        /// </summary>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the number of colour components in the texture.</param>
        /// <param name="size">Specifies the width of the texture image. All implementations support texture images that are at least 1024 texels wide.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.0)]
        protected void TexImage1D(int level, TextureFormat intFormat, int size, BaseFormat format, TextureData type, IntPtr dataPtr)
        {
            Bind();
            GL.TexImage1D(this, level, (int)intFormat, size, 0, (uint)format, (uint)type, dataPtr.ToPointer());
        }
        /// <summary>
        /// Specify a one-dimensional texture image.
        /// </summary>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the number of colour components in the texture.</param>
        /// <param name="size">Specifies the width of the texture image. All implementations support texture images that are at least 1024 texels wide.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="data">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.0)]
        protected void TexImage1D<T>(int level, TextureFormat intFormat, int size, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.TexImage1D(this, level, (int)intFormat, size, 0, (uint)format, (uint)type, data);
        }

        /// <summary>
        /// Specify a two-dimensional texture image.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image. 
        /// If target is <see cref="TextureTarget.Rectangle"/>, level must be 0.</param>
        /// <param name="intFormat">Specifies the number of colour components in the texture.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support texture images that are at least 1024 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image, or the number of layers in a texture array, 
        /// in the case of the <see cref="TextureTarget.Array1D"/> targets. 
        /// All implementations support 2D texture images that are at least 1024 texels high, and texture arrays that are at least 256 layers deep.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.0)]
        protected void TexImage2D<T>(int level, TextureFormat intFormat, int width, int height, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
        {
            Bind();
            GL.TexImage2D(this, level, (int)intFormat, width, height, 0, (uint)format, (uint)type, dataPtr);
        }
        /// <summary>
        /// Specify a two-dimensional texture image.
        /// </summary>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image. 
        /// If target is <see cref="TextureTarget.Rectangle"/>, level must be 0.</param>
        /// <param name="intFormat">Specifies the number of colour components in the texture.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support texture images that are at least 1024 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image, or the number of layers in a texture array, 
        /// in the case of the <see cref="TextureTarget.Array1D"/> target. 
        /// All implementations support 2D texture images that are at least 1024 texels high, and texture arrays that are at least 256 layers deep.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.0)]
        protected void TexImage2D<T>(CubeMapFace target, int level, TextureFormat intFormat, int width, int height, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
        {
            Bind();
            GL.TexImage2D(this, target, level, (int)intFormat, width, height, 0, (uint)format, (uint)type, dataPtr);
        }
        /// <summary>
        /// Specify a two-dimensional texture image.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image. 
        /// If target is <see cref="TextureTarget.Rectangle"/>, level must be 0.</param>
        /// <param name="intFormat">Specifies the number of colour components in the texture.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support texture images that are at least 1024 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image, or the number of layers in a texture array, 
        /// in the case of the <see cref="TextureTarget.Array1D"/> targets. 
        /// All implementations support 2D texture images that are at least 1024 texels high, and texture arrays that are at least 256 layers deep.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.0)]
        protected void TexImage2D(int level, TextureFormat intFormat, int width, int height, BaseFormat format, TextureData type, IntPtr dataPtr)
        {
            Bind();
            GL.TexImage2D(this, level, (int)intFormat, width, height, 0, (uint)format, (uint)type, dataPtr.ToPointer());
        }
        /// <summary>
        /// Specify a two-dimensional texture image.
        /// </summary>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image. 
        /// If target is <see cref="TextureTarget.Rectangle"/>, level must be 0.</param>
        /// <param name="intFormat">Specifies the number of colour components in the texture.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support texture images that are at least 1024 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image, or the number of layers in a texture array, 
        /// in the case of the <see cref="TextureTarget.Array1D"/> target. 
        /// All implementations support 2D texture images that are at least 1024 texels high, and texture arrays that are at least 256 layers deep.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.0)]
        protected void TexImage2D(CubeMapFace target, int level, TextureFormat intFormat, int width, int height, BaseFormat format, TextureData type, IntPtr dataPtr)
        {
            Bind();
            GL.TexImage2D(this, target, level, (int)intFormat, width, height, 0, (uint)format, (uint)type, dataPtr.ToPointer());
        }
        /// <summary>
        /// Specify a two-dimensional texture image.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image. 
        /// If target is <see cref="TextureTarget.Rectangle"/>, level must be 0.</param>
        /// <param name="intFormat">Specifies the number of colour components in the texture.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support texture images that are at least 1024 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image, or the number of layers in a texture array, 
        /// in the case of the <see cref="TextureTarget.Array1D"/> targets. 
        /// All implementations support 2D texture images that are at least 1024 texels high, and texture arrays that are at least 256 layers deep.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="data">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.0)]
        protected void TexImage2D<T>(int level, TextureFormat intFormat, int width, int height, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.TexImage2D(this, level, (int)intFormat, width, height, 0, (uint)format, (uint)type, (T*)data);
        }
        /// <summary>
        /// Specify a two-dimensional texture image.
        /// </summary>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image. 
        /// If target is <see cref="TextureTarget.Rectangle"/>, level must be 0.</param>
        /// <param name="intFormat">Specifies the number of colour components in the texture.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support texture images that are at least 1024 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image, or the number of layers in a texture array, 
        /// in the case of the <see cref="TextureTarget.Array1D"/> target. 
        /// All implementations support 2D texture images that are at least 1024 texels high, and texture arrays that are at least 256 layers deep.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="data">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.0)]
        protected void TexImage2D<T>(CubeMapFace target, int level, TextureFormat intFormat, int width, int height, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.TexImage2D(this, target, level, (int)intFormat, width, height, 0, (uint)format, (uint)type, (T*)data);
        }

        /// <summary>
        /// Establish the data storage, format, dimensions, and number of samples of a multisample texture's image.
        /// </summary>
        /// <param name="target">Specifies the target of the operation.</param>
        /// <param name="samples">The number of samples in the multisample texture's image.</param>
        /// <param name="intFormat">The internal format to be used to store the multisample texture's image. 
        /// It must specify a colour-renderable, depth-renderable, or stencil-renderable format.</param>
        /// <param name="width">The width of the multisample texture's image, in texels.</param>
        /// <param name="height">The height of the multisample texture's image, in texels.</param>
        /// <param name="fixedsampleLocations">Specifies whether the image will use identical sample locations and the same number of 
        /// samples for all texels in the image, and the sample locations will not depend on the internal format or size of the image.</param>
        [OpenGLSupport(3.2)]
        protected void TexImage2DMultisample(int samples, TextureFormat intFormat, int width, int height, bool fixedsampleLocations)
        {
            Bind();
            GL.TexImage2DMultisample(this, samples, (uint)intFormat, width, height, fixedsampleLocations);
        }

        /// <summary>
        /// Specify a three-dimensional texture image.
        /// </summary>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the number of colour components in the texture.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support 3D texture images that are at least 16 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image. All implementations support 3D texture images that are at least 256 texels high.</param>
        /// <param name="depth">Specifies the depth of the texture image, or the number of layers in a texture array. 
        /// All implementations support 3D texture images that are at least 256 texels deep, and texture arrays that are at least 256 layers deep.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.2)]
        protected void TexImage3D<T>(int level, TextureFormat intFormat, int width, int height, int depth, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
        {
            Bind();
            GL.TexImage3D(this, level, (int)intFormat, width, height, depth, 0, (uint)format, (uint)type, dataPtr);
        }
        /// <summary>
        /// Specify a three-dimensional texture image.
        /// </summary>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the number of colour components in the texture.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support 3D texture images that are at least 16 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image. All implementations support 3D texture images that are at least 256 texels high.</param>
        /// <param name="depth">Specifies the depth of the texture image, or the number of layers in a texture array. 
        /// All implementations support 3D texture images that are at least 256 texels deep, and texture arrays that are at least 256 layers deep.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.2)]
        protected void TexImage3D(int level, TextureFormat intFormat, int width, int height, int depth, BaseFormat format, TextureData type, IntPtr dataPtr)
        {
            Bind();
            GL.TexImage3D(this, level, (int)intFormat, width, height, depth, 0, (uint)format, (uint)type, dataPtr.ToPointer());
        }
        /// <summary>
        /// Specify a three-dimensional texture image.
        /// </summary>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="intFormat">Specifies the number of colour components in the texture.</param>
        /// <param name="width">Specifies the width of the texture image. All implementations support 3D texture images that are at least 16 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image. All implementations support 3D texture images that are at least 256 texels high.</param>
        /// <param name="depth">Specifies the depth of the texture image, or the number of layers in a texture array. 
        /// All implementations support 3D texture images that are at least 256 texels deep, and texture arrays that are at least 256 layers deep.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="data">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.2)]
        protected void TexImage3D<T>(int level, TextureFormat intFormat, int width, int height, int depth, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.TexImage3D(this, level, (int)intFormat, width, height, depth, 0, (uint)format, (uint)type, data);
        }

        /// <summary>
        /// Establish the data storage, format, dimensions, and number of samples of a multisample texture's image.
        /// </summary>
        /// <param name="target">Specifies the target of the operation.</param>
        /// <param name="samples">The number of samples in the multisample texture's image.</param>
        /// <param name="intFormat">The internal format to be used to store the multisample texture's image. 
        /// It must specify a colour-renderable, depth-renderable, or stencil-renderable format.</param>
        /// <param name="width">The width of the multisample texture's image, in texels.</param>
        /// <param name="height">The height of the multisample texture's image, in texels.</param>
        /// <param name="depth">The depth of the multisample texture's image, in layers.</param>
        /// <param name="fixedsampleLocations">Specifies whether the image will use identical sample locations and the same number of 
        /// samples for all texels in the image, and the sample locations will not depend on the internal format or size of the image.</param>
        [OpenGLSupport(3.2)]
        protected void TexImage3DMultisample(int samples, TextureFormat intFormat, int width, int height, int depth, bool fixedsampleLocations)
        {
            Bind();
            GL.TexImage3DMultisample(this, samples, (uint)intFormat, width, height, depth, fixedsampleLocations);
        }

        /// <summary>
        /// Simultaneously specify storage for all levels of a one-dimensional texture.
        /// </summary>
        /// <param name="levels">Specify the number of texture levels.</param>
        /// <param name="intFormat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="size">Specifies the width of the texture, in texels.</param>
        [OpenGLSupport(4.2)]
        protected void TexStorage1D(int levels, TextureFormat intFormat, int size)
        {
            Bind();
            GL.TexStorage1D(this, levels, (uint)intFormat, size);
        }
        /// <summary>
        /// Simultaneously specify storage for all levels of a two-dimensional or one-dimensional array texture.
        /// </summary>
        /// <param name="levels">Specify the number of texture levels.</param>
        /// <param name="intFormat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        [OpenGLSupport(4.2)]
        protected void TexStorage2D(int levels, TextureFormat intFormat, int width, int height)
        {
            Bind();
            GL.TexStorage2D(this, levels, (uint)intFormat, width, height);
        }
        /// <summary>
        /// Simultaneously specify storage for all levels of a two-dimensional or one-dimensional array texture.
        /// </summary>
        /// <param name="target">Specifies the target to which the texture object is bound for <see cref="TexStorage2D(CubeMapFace, int, TextureFormat, int, int)"/>.</param>
        /// <param name="levels">Specify the number of texture levels.</param>
        /// <param name="intFormat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        [OpenGLSupport(4.2)]
        protected void TexStorage2D(CubeMapFace target, int levels, TextureFormat intFormat, int width, int height)
        {
            Bind();
            GL.TexStorage2D(this, target, levels, (uint)intFormat, width, height);
        }
        /// <summary>
        /// Specify storage for a two-dimensional multisample texture.
        /// </summary>
        /// <param name="samples">Specify the number of samples in the texture.</param>
        /// <param name="intFormat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        /// <param name="fixedsampleLocations">Specifies whether the image will use identical sample locations and the same number of samples for all 
        /// texels in the image, and the sample locations will not depend on the internal format or size of the image.</param>
        [OpenGLSupport(4.3)]
        protected void TexStorage2DMultisample(int samples, TextureFormat intFormat, int width, int height, bool fixedsampleLocations)
        {
            Bind();
            GL.TexStorage2DMultisample(this, samples, (uint)intFormat, width, height, fixedsampleLocations);
        }
        /// <summary>
        /// Simultaneously specify storage for all levels of a three-dimensional, two-dimensional array or cube-map array texture.
        /// </summary>
        /// <param name="target">Specifies the target to which the texture object is bound for <see cref="TexStorage3D(Target3D, int, TextureFormat, int, int, int)"/>.</param>
        /// <param name="levels">Specify the number of texture levels.</param>
        /// <param name="intFormat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        /// <param name="depth">Specifies the depth of the texture, in texels.</param>
        [OpenGLSupport(4.2)]
        protected void TexStorage3D(int levels, TextureFormat intFormat, int width, int height, int depth)
        {
            Bind();
            GL.TexStorage3D(this, levels, (uint)intFormat, width, height, depth);
        }
        /// <summary>
        /// Specify storage for a two-dimensional multisample array texture.
        /// </summary>
        /// <param name="target">Specifies the target to which the texture object is bound for 
        /// <see cref="TexStorage3DMultisample(Target3DMultisample, int, TextureFormat, int, int, int, bool)"/>.</param>
        /// <param name="samples">Specify the number of samples in the texture.</param>
        /// <param name="intFormat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        /// <param name="depth">Specifies the depth of the texture, in layers.</param>
        /// <param name="fixedsampleLocations">Specifies whether the image will use identical sample locations and the same number of samples for all 
        /// texels in the image, and the sample locations will not depend on the internal format or size of the image.</param>
        [OpenGLSupport(4.3)]
        protected void TexStorage3DMultisample(int samples, TextureFormat intFormat, int width, int height, int depth, bool fixedsampleLocations)
        {
            Bind();
            GL.TexStorage3DMultisample(this, samples, (uint)intFormat, width, height, depth, fixedsampleLocations);
        }

        /// <summary>
        /// Specify a one-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="offset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="size">Specifies the width of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.1)]
        protected void TexSubImage1D<T>(int level, int offset, int size, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
        {
            Bind();
            GL.TexSubImage1D((uint)Target, level, offset, size, (uint)format, (uint)type, dataPtr);
        }
        /// <summary>
        /// Specify a one-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="offset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="size">Specifies the width of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.1)]
        protected void TexSubImage1D(int level, int offset, int size, BaseFormat format, TextureData type, IntPtr dataPtr)
        {
            Bind();
            GL.TexSubImage1D((uint)Target, level, offset, size, (uint)format, (uint)type, dataPtr.ToPointer());
        }
        /// <summary>
        /// Specify a one-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="offset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="size">Specifies the width of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="data">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.1)]
        protected void TexSubImage1D<T>(int level, int offset, int size, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.TexSubImage1D((uint)Target, level, offset, size, (uint)format, (uint)type, data);
        }

        /// <summary>
        /// Specify a two-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.1)]
        protected void TexSubImage2D<T>(int level, int xOffset, int yOffset, int width, int height, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
        {
            Bind();
            GL.TexSubImage2D((uint)Target, level, xOffset, yOffset, width, height, (uint)format, (uint)type, dataPtr);
        }
        /// <summary>
        /// Specify a two-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.1)]
        protected void TexSubImage2D(int level, int xOffset, int yOffset, int width, int height, BaseFormat format, TextureData type, IntPtr dataPtr)
        {
            Bind();
            GL.TexSubImage2D((uint)Target, level, xOffset, yOffset, width, height, (uint)format, (uint)type, dataPtr.ToPointer());
        }
        /// <summary>
        /// Specify a two-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="data">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.1)]
        protected void TexSubImage2D<T>(int level, int xOffset, int yOffset, int width, int height, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.TexSubImage2D((uint)Target, level, xOffset, yOffset, width, height, (uint)format, (uint)type, data);
        }
        /// <summary>
        /// Specify a two-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.1)]
        protected void TexSubImage2D<T>(CubeMapFace target, int level, int xOffset, int yOffset, int width, int height, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
        {
            Bind();
            GL.TexSubImage2D((uint)target, level, xOffset, yOffset, width, height, (uint)format, (uint)type, dataPtr);
        }
        /// <summary>
        /// Specify a two-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.1)]
        protected void TexSubImage2D(CubeMapFace target, int level, int xOffset, int yOffset, int width, int height, BaseFormat format, TextureData type, IntPtr dataPtr)
        {
            Bind();
            GL.TexSubImage2D((uint)target, level, xOffset, yOffset, width, height, (uint)format, (uint)type, dataPtr.ToPointer());
        }
        /// <summary>
        /// Specify a two-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="data">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.1)]
        protected void TexSubImage2D<T>(CubeMapFace target, int level, int xOffset, int yOffset, int width, int height, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.TexSubImage2D((uint)target, level, xOffset, yOffset, width, height, (uint)format, (uint)type, data);
        }

        /// <summary>
        /// Specify a three-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="zOffset">Specifies a texel offset in the z direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="depth">Specifies the depth of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.2)]
        protected void TexSubImage3D<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
        {
            Bind();
            GL.TexSubImage3D((uint)Target, level, xOffset, yOffset, zOffset, width, height, depth, (uint)format, (uint)type, dataPtr);
        }
        /// <summary>
        /// Specify a three-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="zOffset">Specifies a texel offset in the z direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="depth">Specifies the depth of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="dataPtr">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.2)]
        protected void TexSubImage3D(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, BaseFormat format, TextureData type, IntPtr dataPtr)
        {
            Bind();
            GL.TexSubImage3D((uint)Target, level, xOffset, yOffset, zOffset, width, height, depth, (uint)format, (uint)type, dataPtr.ToPointer());
        }
        /// <summary>
        /// Specify a three-dimensional texture subimage.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xOffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yOffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="zOffset">Specifies a texel offset in the z direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="depth">Specifies the depth of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="data">Specifies a pointer to the image data in memory.</param>
        [OpenGLSupport(1.2)]
        protected void TexSubImage3D<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.TexSubImage3D((uint)Target, level, xOffset, yOffset, zOffset, width, height, depth, (uint)format, (uint)type, data);
        }

        /// <summary>
        /// Initialize a texture as a data alias of another texture's data store.
        /// </summary>
        /// <param name="original">Specifies the texture object of which to make a view.</param>
        /// <param name="intFormat">Specifies the internal format for the newly created view.</param>
        /// <param name="minLevel">Specifies lowest level of detail of the view.</param>
        /// <param name="numLevels">Specifies the number of levels of detail to include in the view.</param>
        /// <param name="minLayer">Specifies the index of the first layer to include in the view.</param>
        /// <param name="numbLayers">Specifies the number of layers to include in the view.</param>
        [OpenGLSupport(4.3)]
        protected void TextureView(ITexture original, TextureFormat intFormat, uint minLevel, uint numLevels, uint minLayer, uint numbLayers)
        {
            Bind();
            GL.TextureView(this, (uint)Target, original, (uint)intFormat, minLevel, numLevels, minLayer, numbLayers);
        }

        //
        // Parameter Gets
        //

        /// <summary>
        /// Returns the single-value depth stencil texture mode.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected DepthStencilMode GetDepthStencilMode()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.DepthStencilTextureMode, &output);

            return (DepthStencilMode)output;
        }

        /// <summary>
        /// Returns the single-valued base texture mipmap level.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected int GetBaseLevel()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureBaseLevel, &output);

            return output;
        }

        private const double IntToByte = byte.MaxValue / int.MaxValue;

        /// <summary>
        /// Returns the RGBA colour of the texture border.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected Colour GetBorderColourI()
        {
            Bind();
            int[] colour = new int[4];

            fixed (int* get = &colour[0])
            {
                GL.GetTexParameterIiv((uint)Target, GLEnum.TextureBorderColour, get);
            }

            return new Colour(
                (byte)Math.Round(colour[0] * IntToByte),    // R
                (byte)Math.Round(colour[1] * IntToByte),    // G
                (byte)Math.Round(colour[2] * IntToByte),    // B
                (byte)Math.Round(colour[3] * IntToByte));   // A
        }

        /// <summary>
        /// Returns the RGBA colour of the texture border.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected ColourF GetBorderColour()
        {
            Bind();
            float[] colour = new float[4];

            fixed (float* get = &colour[0])
            {
                GL.GetTexParameterfv((uint)Target, GLEnum.TextureBorderColour, get);
            }

            return new ColourF(colour[0], colour[1], colour[2], colour[3]);
        }

        /// <summary>
        /// Returns the single-value fixed bias that is added to the level-of-detail parameter for the texture before texture sampling.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected float GetLodBias()
        {
            Bind();
            float output;

            GL.GetTexParameterfv((uint)Target, GLEnum.TextureLodBias, &output);

            return output;
        }

        /// <summary>
        /// Returns the single-valued texture minification filter.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected TextureSampling GetMinFilter()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureMinFilter, &output);

            return (TextureSampling)output;
        }

        /// <summary>
        /// Returns the single-valued texture magnification filter.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected TextureSampling GetMagFilter()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureMagFilter, &output);

            return (TextureSampling)output;
        }

        /// <summary>
        /// Returns the single-valued texture minimum level-of-detail value.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected float GetMinLod()
        {
            Bind();
            float output;

            GL.GetTexParameterfv((uint)Target, GLEnum.TextureMinLod, &output);

            return output;
        }

        /// <summary>
        /// Returns the single-valued texture maximum level-of-detail value.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected float GetMaxLod()
        {
            Bind();
            float output;

            GL.GetTexParameterfv((uint)Target, GLEnum.TextureMaxLod, &output);

            return output;
        }

        /// <summary>
        /// Returns the single-valued maximum texture mipmap array level.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected int GetMaxLevel()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureMaxLevel, &output);

            return output;
        }

        /// <summary>
        /// Returns the red component swizzle.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected Swizzle GetSwizzleRed()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureSwizzleR, &output);

            return (Swizzle)output;
        }

        /// <summary>
        /// Returns the green component swizzle.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected Swizzle GetSwizzleGreen()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureSwizzleG, &output);

            return (Swizzle)output;
        }

        /// <summary>
        /// Returns the blue component swizzle.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected Swizzle GetSwizzleBlue()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureSwizzleB, &output);

            return (Swizzle)output;
        }

        /// <summary>
        /// Returns the alpha component swizzle.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected Swizzle GetSwizzleAlpha()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureSwizzleA, &output);

            return (Swizzle)output;
        }

        /// <summary>
        /// Returns the component swizzle for all channels in a single query.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected void GetSwizzle(out Swizzle r, out Swizzle g, out Swizzle b, out Swizzle a)
        {
            Bind();
            int[] outputs = new int[4];

            fixed (int* gets = &outputs[0])
            {
                GL.TexParameteriv((uint)Target, GLEnum.TextureSwizzleA, gets);
            }

            r = (Swizzle)outputs[0];
            g = (Swizzle)outputs[1];
            b = (Swizzle)outputs[2];
            a = (Swizzle)outputs[3];
        }

        /// <summary>
        /// Returns the single-valued wrapping function for texture coordinate s.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected WrapStyle GetWrapS()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureWrapS, &output);

            return (WrapStyle)output;
        }

        /// <summary>
        /// Returns the single-valued wrapping function for texture coordinate t.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected WrapStyle GetWrapT()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureWrapT, &output);

            return (WrapStyle)output;
        }

        /// <summary>
        /// Returns the single-valued wrapping function for texture coordinate r.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected WrapStyle GetWrapR()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureWrapR, &output);

            return (WrapStyle)output;
        }

        /// <summary>
        /// Returns a single-valued base level of a texture view relative to its parent.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected int GetViewMinLevel()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureViewMinLevel, &output);

            return output;
        }

        /// <summary>
        /// Returns a single-valued number of levels of detail of a texture view.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected int GetViewNumLevels()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureViewNumLevels, &output);

            return output;
        }

        /// <summary>
        /// Returns a single-valued first level of a texture array view relative to its parent.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected int GetViewMinLayer()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureViewMinLayer, &output);

            return output;
        }

        /// <summary>
        /// Returns a single-valued number of layers in a texture array view.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected int GetViewNumLayers()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureViewNumLayers, &output);

            return output;
        }

        /// <summary>
        /// Returns a single-valued number of immutable texture levels in a texture view.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected int GetViewImmutableLevels()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureImmutableLevels, &output);

            return output;
        }

        /// <summary>
        /// Returns a single-valued texture comparison function.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected ComparisonFunction GetComparisonFunction()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureCompareFunc, &output);

            return (ComparisonFunction)output;
        }

        /// <summary>
        /// Returns a single-valued texture comparison mode.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected ComparisonMode GetComparisonMode()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureCompareMode, &output);

            return (ComparisonMode)output;
        }

        /// <summary>
        /// Returns the matching criteria use for the texture when used as an image texture.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected FormatCompatibilityType GetFormatCompatibilityType()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.ImageFormatCompatibilityType, &output);

            return (FormatCompatibilityType)output;
        }

        /// <summary>
        /// Returns <see cref="true"/> if the texture has an immutable format, otherwise <see cref="false"/>.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        protected bool IsImmutableFormat()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureImmutableFormat, &output);

            return output > 0;
        }

        /// <summary>
        /// Returns a single value, the width of the texture image.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected int GetWidth(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureWidth, &output);

            return output;
        }

        /// <summary>
        /// Returns a single value, the height of the texture image.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected int GetHeight(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureHeight, &output);

            return output;
        }

        /// <summary>
        /// Returns a single value, the depth of the texture image.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected int GetDepth(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureDepth, &output);

            return output;
        }

        /// <summary>
        /// The data type used to store the red component.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected ChannelType GetRedType(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureRedType, &output);

            return (ChannelType)output;
        }

        /// <summary>
        /// The data type used to store the green component.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected ChannelType GetGreenType(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureGreenType, &output);

            return (ChannelType)output;
        }

        /// <summary>
        /// The data type used to store the blue component.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected ChannelType GetBlueType(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureBlueType, &output);

            return (ChannelType)output;
        }

        /// <summary>
        /// The data type used to store the alpha component.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected ChannelType GetAlphaType(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureAlphaType, &output);

            return (ChannelType)output;
        }

        /// <summary>
        /// The data type used to store the depth component.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected ChannelType GetDepthType(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureDepthType, &output);

            return (ChannelType)output;
        }

        /// <summary>
        /// The internal storage resolution of the red component.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected int GetRedSize(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureRedSize, &output);

            return output;
        }

        /// <summary>
        /// The internal storage resolution of the green component.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected int GetGreenSize(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureGreenSize, &output);

            return output;
        }

        /// <summary>
        /// The internal storage resolution of the blue component.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected int GetBlueSize(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureBlueSize, &output);

            return output;
        }

        /// <summary>
        /// The internal storage resolution of the alpha component.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected int GetAlphaSize(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureAlphaSize, &output);

            return output;
        }

        /// <summary>
        /// The internal storage resolution of the depth component.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected int GetDepthSize(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureDepthSize, &output);

            return output;
        }

        /// <summary>
        /// Returns a single boolean value indicating if the texture image is stored in a compressed internal format.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected bool GetIsCompressed(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureCompressed, &output);

            return output > 0;
        }

        /// <summary>
        /// Returns a single integer value, the number of unsigned bytes of the compressed texture image that would be returned from <see cref="GetCompressedTexImage{T}(int)"/>.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected int GetCompressedImageSize(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureCompressedImageSize, &output);

            return output;
        }

        /// <summary>
        /// Returns a single integer value, the offset into the data store of the buffer bound to a buffer texture.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected int GetBufferOffset(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureBufferOffset, &output);

            return output;
        }

        /// <summary>
        /// Returns a single integer value, the size of the range of a data store of the buffer bound to a buffer texture.
        /// </summary>
        /// <remarks>
        /// It is more advisable to use <see cref="TextureProperties"/> for texture related parameters.
        /// </remarks>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        protected int GetBufferSize(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureBufferSize, &output);

            return output;
        }
    }
}