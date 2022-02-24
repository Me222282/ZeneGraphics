using System;
using Zene.Structs;

namespace Zene.Graphics.Base
{
    /// <summary>
    /// The most basic implimentation of an OpenGL texture.
    /// </summary>
    [OpenGLSupport(1.3)]
    public unsafe sealed class TextureGL : ITexture
    {
        /// <summary>
        /// Creates an OpenGL texture object based on a given <see cref="TextureTarget"/>.
        /// </summary>
        /// <param name="target">The type of texture to create.</param>
        public TextureGL(TextureTarget target)
        {
            Id = GL.GenTexture();
            Target = target;
            InternalFormat = 0;

            Properties = new TextureProperties(this);
        }
        internal TextureGL(uint id, TextureTarget target, TextureFormat format = 0)
        {
            Id = id;
            Target = target;
            InternalFormat = format;

            Properties = new TextureProperties(this);
        }

        public uint Id { get; }

        public TextureTarget Target { get; }
        public TextureFormat InternalFormat { get; private set; }

        public TextureProperties Properties { get; }

        public uint ReferanceSlot { get; private set; } = 0;

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            if (GL.Version >= 3.0)
            {
                GL.DeleteTexture(Id);
            }

            _disposed = true;
            GC.SuppressFinalize(this);
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
        public void BindUnit(uint unit)
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
        public void BindLevel(uint unit, int level, bool layered, int layer, AccessType access)
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
        public void ClearTextureImage<T>(int level, BaseFormat format, TextureData type, T data) where T : unmanaged
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
        public void ClearTextureSubImage<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, BaseFormat format, TextureData type, T data) where T : unmanaged
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
        public void ClearTextureImage<T>(int level, BaseFormat format, TextureData type, T[] data) where T : unmanaged
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
        public void ClearTextureSubImage<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, BaseFormat format, TextureData type, T[] data) where T : unmanaged
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
        public void CompressedTexImage1D<T>(int level, TextureFormat intFormat, int size, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage1D(this, level, (uint)intFormat, size, 0, data.Size * sizeof(T), data);

            InternalFormat = intFormat;
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
        public void CompressedTexImage1D<T>(int level, TextureFormat intFormat, int size, int imageSize, T* data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage1D(this, level, (uint)intFormat, size, 0, imageSize, data);

            InternalFormat = intFormat;
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
        public void CompressedTexImage1D(int level, TextureFormat intFormat, int size, int imageSize, IntPtr data)
        {
            Bind();
            GL.CompressedTexImage1D(this, level, (uint)intFormat, size, 0, imageSize, data.ToPointer());

            InternalFormat = intFormat;
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
        public void CompressedTexImage2D<T>(int level, TextureFormat intFormat, int width, int height, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage2D(this, level, (uint)intFormat, width, height, 0, data.Size * sizeof(T), data);

            InternalFormat = intFormat;
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
        public void CompressedTexImage2D<T>(CubeMapFace target, int level, TextureFormat intFormat, int width, int height, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage2D(this, level, (uint)intFormat, width, height, 0, data.Size * sizeof(T), data);

            InternalFormat = intFormat;
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
        public void CompressedTexImage2D<T>(int level, TextureFormat intFormat, int width, int height, int imageSize, T* data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage2D(this, level, (uint)intFormat, width, height, 0, imageSize, data);

            InternalFormat = intFormat;
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
        public void CompressedTexImage2D<T>(CubeMapFace target, int level, TextureFormat intFormat, int width, int height, int imageSize, T* data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage2D(this, target, level, (uint)intFormat, width, height, 0, imageSize, data);

            InternalFormat = intFormat;
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
        public void CompressedTexImage2D(int level, TextureFormat intFormat, int width, int height, int imageSize, IntPtr data)
        {
            Bind();
            GL.CompressedTexImage2D(this, level, (uint)intFormat, width, height, 0, imageSize, data.ToPointer());

            InternalFormat = intFormat;
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
        public void CompressedTexImage2D(CubeMapFace target, int level, TextureFormat intFormat, int width, int height, int imageSize, IntPtr data)
        {
            Bind();
            GL.CompressedTexImage2D(this, target, level, (uint)intFormat, width, height, 0, imageSize, data.ToPointer());

            InternalFormat = intFormat;
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
        public void CompressedTexImage3D<T>(int level, TextureFormat intFormat, int width, int height, int depth, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage3D(this, level, (uint)intFormat, width, height, depth, 0, data.Size * sizeof(T), data);

            InternalFormat = intFormat;
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
        public void CompressedTexImage3D<T>(int level, TextureFormat intFormat, int width, int height, int depth, int imageSize, T* data) where T : unmanaged
        {
            Bind();
            GL.CompressedTexImage3D(this, level, (uint)intFormat, width, height, depth, 0, imageSize, data);

            InternalFormat = intFormat;
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
        public void CompressedTexImage3D(int level, TextureFormat intFormat, int width, int height, int depth, int imageSize, IntPtr data)
        {
            Bind();
            GL.CompressedTexImage3D(this, level, (uint)intFormat, width, height, depth, 0, imageSize, data.ToPointer());

            InternalFormat = intFormat;
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
        public void CompressedTexSubImage1D<T>(int level, int offset, int size, GLArray<T> data) where T : unmanaged
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
        public void CompressedTexSubImage1D<T>(int level, int offset, int size, int imageSize, T* data) where T : unmanaged
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
        public void CompressedTexSubImage1D(int level, int offset, int size, int imageSize, IntPtr data)
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
        public void CompressedTexSubImage2D<T>(int level, int xOffset, int yOffset, int width, int height, GLArray<T> data) where T : unmanaged
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
        public void CompressedTexSubImage2D<T>(int level, int xOffset, int yOffset, int width, int height, int imageSize, T* data) where T : unmanaged
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
        public void CompressedTexSubImage2D(int level, int xOffset, int yOffset, int width, int height, int imageSize, IntPtr data)
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
        public void CompressedTexSubImage2D<T>(CubeMapFace target, int level, int xOffset, int yOffset, int width, int height, GLArray<T> data) where T : unmanaged
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
        public void CompressedTexSubImage2D<T>(CubeMapFace target, int level, int xOffset, int yOffset, int width, int height, int imageSize, T* data) where T : unmanaged
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
        public void CompressedTexSubImage2D(CubeMapFace target, int level, int xOffset, int yOffset, int width, int height, int imageSize, IntPtr data)
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
        public void CompressedTexSubImage3D<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, GLArray<T> data) where T : unmanaged
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
        public void CompressedTexSubImage3D<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, int imageSize, T* data) where T : unmanaged
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
        public void CompressedTexSubImage3D(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, int imageSize, IntPtr data)
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
        public void CopyImageSubData(ITexture source, int srcLevel, int srcX, int srcY, int srcZ, int width, int height, int depth, int level, int x, int y, int z)
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
        public void CopyImageSubData(IRenderbuffer source, int srcLevel, int srcX, int srcY, int srcZ, int width, int height, int depth, int level, int x, int y, int z)
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
        public void CopyImageSubData(ITexture source, CubeMapFace srcTarget, int srcLevel, int srcX, int srcY, int srcZ, int width, int height, int depth, CubeMapFace target, int level, int x, int y, int z)
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
        public void CopyTexImage1D(int level, TextureFormat intFormat, int x, int y, int size)
        {
            Bind();
            GL.CopyTexImage1D(this, level, (uint)intFormat, x, y, size, 0);

            InternalFormat = intFormat;
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
        public void CopyTexImage2D(int level, TextureFormat intFormat, int x, int y, int width, int height)
        {
            Bind();
            GL.CopyTexImage2D(this, level, (uint)intFormat, x, y, width, height, 0);

            InternalFormat = intFormat;
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
        public void CopyTexImage2D(CubeMapFace target, int level, TextureFormat intFormat, int x, int y, int width, int height)
        {
            Bind();
            GL.CopyTexImage2D(this, level, (uint)intFormat, x, y, width, height, 0);

            InternalFormat = intFormat;
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
        public void CopyTexSubImage1D(int level, int offset, int x, int y, int width)
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
        public void CopyTexSubImage2D(int level, int xOffset, int yOffset, int x, int y, int width, int height)
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
        public void CopyTexSubImage2D(CubeMapFace target, int level, int xOffset, int yOffset, int x, int y, int width, int height)
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
        public void CopyTexSubImage3D(int level, int xOffset, int yOffset, int zOffset, int x, int y, int width, int height)
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
        public T[] GetCompressedTexImage<T>(int level) where T : unmanaged
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
        public T[] GetCompressedTextureSubImage<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, int bufferSize) where T : unmanaged
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
        public T[] GetCompressedTexImage<T>(CubeMapFace target, int level) where T : unmanaged
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
        public T[] GetCompressedTextureSubImage<T>(CubeMapFace target, int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, int bufferSize) where T : unmanaged
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
        public GLArray<T> GetTexImage<T>(int level, BaseFormat format, TextureData type) where T : unmanaged
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
        public GLArray<T> GetTexImage<T>(CubeMapFace face, int level, BaseFormat format, TextureData type) where T : unmanaged
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
        public GLArray<T> GetTextureSubImage<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, BaseFormat format, TextureData type) where T : unmanaged
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
        public void GenerateMipmap()
        {
            Bind();
            GL.GenerateMipmap((uint)Target);
        }

        /// <summary>
        /// Invalidate the entirety a texture image.
        /// </summary>
        /// <param name="level">The level of detail of the texture object to invalidate.</param>
        [OpenGLSupport(4.3)]
        public void InvalidateTexImage(int level)
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
        public void InvalidateTexSubImage(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth)
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
        public void TexBuffer(IBuffer buffer, TextureFormat intFormat)
        {
            Bind();
            GL.TexBuffer((uint)Target, (uint)intFormat, buffer.Id);

            InternalFormat = intFormat;
        }
        /// <summary>
        /// Attach a range of a buffer object's data store to a buffer texture object.
        /// </summary>
        /// <param name="buffer">Specifies the name of the buffer object whose storage to attach to the active buffer texture.</param>
        /// <param name="intFormat">Specifies the internal format of the data in the store belonging to <paramref name="buffer"/>.</param>
        /// <param name="offset">Specifies the offset of the start of the range of the buffer's data store to attach.</param>
        /// <param name="size">Specifies the size of the range of the buffer's data store to attach.</param>
        [OpenGLSupport(4.3)]
        public void TexBufferRange(IBuffer buffer, TextureFormat intFormat, int offset, int size)
        {
            Bind();
            GL.TexBufferRange((uint)Target, (uint)intFormat, buffer.Id, offset, size);

            InternalFormat = intFormat;
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
        public void TexImage1D<T>(int level, TextureFormat intFormat, int size, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
        {
            Bind();
            GL.TexImage1D(this, level, (int)intFormat, size, 0, (uint)format, (uint)type, dataPtr);

            InternalFormat = intFormat;
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
        public void TexImage1D(int level, TextureFormat intFormat, int size, BaseFormat format, TextureData type, IntPtr dataPtr)
        {
            Bind();
            GL.TexImage1D(this, level, (int)intFormat, size, 0, (uint)format, (uint)type, dataPtr.ToPointer());

            InternalFormat = intFormat;
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
        public void TexImage1D<T>(int level, TextureFormat intFormat, int size, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.TexImage1D(this, level, (int)intFormat, size, 0, (uint)format, (uint)type, data);

            InternalFormat = intFormat;
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
        public void TexImage2D<T>(int level, TextureFormat intFormat, int width, int height, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
        {
            Bind();
            GL.TexImage2D(this, level, (int)intFormat, width, height, 0, (uint)format, (uint)type, dataPtr);

            InternalFormat = intFormat;
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
        public void TexImage2D<T>(CubeMapFace target, int level, TextureFormat intFormat, int width, int height, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
        {
            Bind();
            GL.TexImage2D(this, target, level, (int)intFormat, width, height, 0, (uint)format, (uint)type, dataPtr);

            InternalFormat = intFormat;
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
        public void TexImage2D(int level, TextureFormat intFormat, int width, int height, BaseFormat format, TextureData type, IntPtr dataPtr)
        {
            Bind();
            GL.TexImage2D(this, level, (int)intFormat, width, height, 0, (uint)format, (uint)type, dataPtr.ToPointer());

            InternalFormat = intFormat;
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
        public void TexImage2D(CubeMapFace target, int level, TextureFormat intFormat, int width, int height, BaseFormat format, TextureData type, IntPtr dataPtr)
        {
            Bind();
            GL.TexImage2D(this, target, level, (int)intFormat, width, height, 0, (uint)format, (uint)type, dataPtr.ToPointer());

            InternalFormat = intFormat;
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
        public void TexImage2D<T>(int level, TextureFormat intFormat, int width, int height, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.TexImage2D(this, level, (int)intFormat, width, height, 0, (uint)format, (uint)type, (T*)data);

            InternalFormat = intFormat;
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
        public void TexImage2D<T>(CubeMapFace target, int level, TextureFormat intFormat, int width, int height, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.TexImage2D(this, target, level, (int)intFormat, width, height, 0, (uint)format, (uint)type, (T*)data);

            InternalFormat = intFormat;
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
        public void TexImage2DMultisample(int samples, TextureFormat intFormat, int width, int height, bool fixedsampleLocations)
        {
            Bind();
            GL.TexImage2DMultisample(this, samples, (uint)intFormat, width, height, fixedsampleLocations);

            InternalFormat = intFormat;
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
        public void TexImage3D<T>(int level, TextureFormat intFormat, int width, int height, int depth, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
        {
            Bind();
            GL.TexImage3D(this, level, (int)intFormat, width, height, depth, 0, (uint)format, (uint)type, dataPtr);

            InternalFormat = intFormat;
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
        public void TexImage3D(int level, TextureFormat intFormat, int width, int height, int depth, BaseFormat format, TextureData type, IntPtr dataPtr)
        {
            Bind();
            GL.TexImage3D(this, level, (int)intFormat, width, height, depth, 0, (uint)format, (uint)type, dataPtr.ToPointer());

            InternalFormat = intFormat;
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
        public void TexImage3D<T>(int level, TextureFormat intFormat, int width, int height, int depth, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
        {
            Bind();
            GL.TexImage3D(this, level, (int)intFormat, width, height, depth, 0, (uint)format, (uint)type, data);

            InternalFormat = intFormat;
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
        public void TexImage3DMultisample(int samples, TextureFormat intFormat, int width, int height, int depth, bool fixedsampleLocations)
        {
            Bind();
            GL.TexImage3DMultisample(this, samples, (uint)intFormat, width, height, depth, fixedsampleLocations);

            InternalFormat = intFormat;
        }

        /// <summary>
        /// Simultaneously specify storage for all levels of a one-dimensional texture.
        /// </summary>
        /// <param name="levels">Specify the number of texture levels.</param>
        /// <param name="intFormat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="size">Specifies the width of the texture, in texels.</param>
        [OpenGLSupport(4.2)]
        public void TexStorage1D(int levels, TextureFormat intFormat, int size)
        {
            Bind();
            GL.TexStorage1D(this, levels, (uint)intFormat, size);

            InternalFormat = intFormat;
        }
        /// <summary>
        /// Simultaneously specify storage for all levels of a two-dimensional or one-dimensional array texture.
        /// </summary>
        /// <param name="levels">Specify the number of texture levels.</param>
        /// <param name="intFormat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        [OpenGLSupport(4.2)]
        public void TexStorage2D(int levels, TextureFormat intFormat, int width, int height)
        {
            Bind();
            GL.TexStorage2D(this, levels, (uint)intFormat, width, height);

            InternalFormat = intFormat;
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
        public void TexStorage2D(CubeMapFace target, int levels, TextureFormat intFormat, int width, int height)
        {
            Bind();
            GL.TexStorage2D(this, target, levels, (uint)intFormat, width, height);

            InternalFormat = intFormat;
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
        public void TexStorage2DMultisample(int samples, TextureFormat intFormat, int width, int height, bool fixedsampleLocations)
        {
            Bind();
            GL.TexStorage2DMultisample(this, samples, (uint)intFormat, width, height, fixedsampleLocations);

            InternalFormat = intFormat;
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
        public void TexStorage3D(int levels, TextureFormat intFormat, int width, int height, int depth)
        {
            Bind();
            GL.TexStorage3D(this, levels, (uint)intFormat, width, height, depth);

            InternalFormat = intFormat;
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
        public void TexStorage3DMultisample(int samples, TextureFormat intFormat, int width, int height, int depth, bool fixedsampleLocations)
        {
            Bind();
            GL.TexStorage3DMultisample(this, samples, (uint)intFormat, width, height, depth, fixedsampleLocations);

            InternalFormat = intFormat;
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
        public void TexSubImage1D<T>(int level, int offset, int size, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
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
        public void TexSubImage1D(int level, int offset, int size, BaseFormat format, TextureData type, IntPtr dataPtr)
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
        public void TexSubImage1D<T>(int level, int offset, int size, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
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
        public void TexSubImage2D<T>(int level, int xOffset, int yOffset, int width, int height, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
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
        public void TexSubImage2D(int level, int xOffset, int yOffset, int width, int height, BaseFormat format, TextureData type, IntPtr dataPtr)
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
        public void TexSubImage2D<T>(int level, int xOffset, int yOffset, int width, int height, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
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
        public void TexSubImage2D<T>(CubeMapFace target, int level, int xOffset, int yOffset, int width, int height, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
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
        public void TexSubImage2D(CubeMapFace target, int level, int xOffset, int yOffset, int width, int height, BaseFormat format, TextureData type, IntPtr dataPtr)
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
        public void TexSubImage2D<T>(CubeMapFace target, int level, int xOffset, int yOffset, int width, int height, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
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
        public void TexSubImage3D<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, BaseFormat format, TextureData type, T* dataPtr) where T : unmanaged
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
        public void TexSubImage3D(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, BaseFormat format, TextureData type, IntPtr dataPtr)
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
        public void TexSubImage3D<T>(int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, BaseFormat format, TextureData type, GLArray<T> data) where T : unmanaged
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
        public void TextureView(ITexture original, TextureFormat intFormat, uint minLevel, uint numLevels, uint minLayer, uint numbLayers)
        {
            Bind();
            GL.TextureView(this, (uint)Target, original, (uint)intFormat, minLevel, numLevels, minLayer, numbLayers);

            InternalFormat = intFormat;
        }

        //
        // Parameter Sets
        //

        /// <summary>
        /// Specifies the mode used to read from depth-stencil format textures.
        /// </summary>
        public void SetDepthStencilMode(DepthStencilMode value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.DepthStencilTextureMode, (int)value);
        }

        /// <summary>
        /// Specifies the index of the lowest defined mipmap level.
        /// </summary>
        /// <param name="value"></param>
        public void SetBaseLevel(int value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureBaseLevel, value);
        }

        /// <summary>
        /// If a texel is sampled from the border of the texture, the values of 
        /// <paramref name="r"/>, <paramref name="g"/>, <paramref name="b"/> and <paramref name="a"/> are used.
        /// </summary>
        /// <remarks>
        /// This stores the value as an integer.
        /// </remarks>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        public void SetBorderColourI(int r, int g, int b, int a)
        {
            Bind();
            int[] colour = new int[] { r, g, b, a };

            fixed (int* parameter = &colour[0])
            {
                GL.TexParameterIiv((uint)Target, GLEnum.TextureBorderColour, parameter);
            }
        }

        /// <summary>
        /// If a texel is sampled from the border of the texture, the values of 
        /// <paramref name="r"/>, <paramref name="g"/>, <paramref name="b"/> and <paramref name="a"/> are used.
        /// </summary>
        /// <remarks>
        /// This stores the value as an integer.
        /// </remarks>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        public void SetBorderColourI(byte r, byte g, byte b, byte a)
        {
            Bind();
            int[] colour = new int[] { r, g, b, a };

            fixed (int* parameter = &colour[0])
            {
                GL.TexParameterIiv((uint)Target, GLEnum.TextureBorderColour, parameter);
            }
        }

        /// <summary>
        /// If a texel is sampled from the border of the texture, the value of <paramref name="colour"/> is used.
        /// </summary>
        /// <remarks>
        /// This stores the value as an integer.
        /// </remarks>
        public void SetBorderColourI(Colour colour)
        {
            Bind();
            int[] iColour = new int[] { colour.R, colour.G, colour.B, colour.A };

            fixed (int* parameter = &iColour[0])
            {
                GL.TexParameterIiv((uint)Target, GLEnum.TextureBorderColour, parameter);
            }
        }

        /// <summary>
        /// If a texel is sampled from the border of the texture, the value of <paramref name="colour"/> is used.
        /// </summary>
        /// <remarks>
        /// This stores the value as an integer.
        /// </remarks>
        public void SetBorderColourI(ColourF colour)
        {
            Bind();
            // Convert to integer equivelents
            Colour c = (Colour)colour;

            int[] iColour = new int[] { c.R, c.G, c.B, c.A };

            fixed (int* parameter = &iColour[0])
            {
                GL.TexParameterIiv((uint)Target, GLEnum.TextureBorderColour, parameter);
            }
        }

        /// <summary>
        /// If a texel is sampled from the border of the texture, the values of 
        /// <paramref name="r"/>, <paramref name="g"/>, <paramref name="b"/> and <paramref name="a"/> are used.
        /// </summary>
        /// <remarks>
        /// This stores the value as a float.
        /// </remarks>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        public void SetBorderColour(int r, int g, int b, int a)
        {
            Bind();
            int[] colour = new int[] { r, g, b, a };

            fixed (int* parameter = &colour[0])
            {
                GL.TexParameteriv((uint)Target, GLEnum.TextureBorderColour, parameter);
            }
        }

        /// <summary>
        /// If a texel is sampled from the border of the texture, the values of 
        /// <paramref name="r"/>, <paramref name="g"/>, <paramref name="b"/> and <paramref name="a"/> are used.
        /// </summary>
        /// <remarks>
        /// This stores the value as a float.
        /// </remarks>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        public void SetBorderColour(byte r, byte g, byte b, byte a)
        {
            Bind();
            int[] colour = new int[] { r, g, b, a };

            fixed (int* parameter = &colour[0])
            {
                GL.TexParameteriv((uint)Target, GLEnum.TextureBorderColour, parameter);
            }
        }

        /// <summary>
        /// If a texel is sampled from the border of the texture, the values of 
        /// <paramref name="r"/>, <paramref name="g"/>, <paramref name="b"/> and <paramref name="a"/> are used.
        /// </summary>
        /// <remarks>
        /// This stores the value as a float.
        /// </remarks>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        public void SetBorderColour(float r, float g, float b, float a)
        {
            Bind();
            float[] colour = new float[] { r, g, b, a };

            fixed (float* parameter = &colour[0])
            {
                GL.TexParameterfv((uint)Target, GLEnum.TextureBorderColour, parameter);
            }
        }

        /// <summary>
        /// If a texel is sampled from the border of the texture, the value of <paramref name="colour"/> is used.
        /// </summary>
        /// <remarks>
        /// This stores the value as a float.
        /// </remarks>
        public void SetBorderColour(Colour colour)
        {
            Bind();
            int[] iColour = new int[] { colour.R, colour.G, colour.B, colour.A };

            fixed (int* parameter = &iColour[0])
            {
                GL.TexParameteriv((uint)Target, GLEnum.TextureBorderColour, parameter);
            }
        }

        /// <summary>
        /// If a texel is sampled from the border of the texture, the value of <paramref name="colour"/> is used.
        /// </summary>
        /// <remarks>
        /// This stores the value as a float.
        /// </remarks>
        public void SetBorderColour(ColourF colour)
        {
            Bind();
            float[] iColour = new float[] { colour.R, colour.G, colour.B, colour.A };

            fixed (float* parameter = &iColour[0])
            {
                GL.TexParameterfv((uint)Target, GLEnum.TextureBorderColour, parameter);
            }
        }

        /// <summary>
        /// Specifies a fixed bias value that is to be added to the level-of-detail parameter for the texture before texture sampling.
        /// </summary>
        public void SetLodBias(float value)
        {
            Bind();
            GL.TexParameterf((uint)Target, GLEnum.TextureLodBias, value);
        }

        /// <summary>
        /// The texture minifying function is used whenever the level-of-detail function used when sampling from the texture determines that the texture should be minified.
        /// </summary>
        public void SetMinFilter(TextureSampling value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureMinFilter, (int)value);
        }

        /// <summary>
        /// The texture magnification function is used whenever the level-of-detail function used when sampling from the texture determines that the texture should be magified.
        /// </summary>
        public void SetMagFilter(TextureSampling value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureMagFilter, (int)value);
        }

        /// <summary>
        /// Sets the minimum level-of-detail parameter. This floating-point value limits the selection of highest resolution mipmap (lowest mipmap level).
        /// </summary>
        public void SetMinLod(float value)
        {
            Bind();
            GL.TexParameterf((uint)Target, GLEnum.TextureMinLod, value);
        }

        /// <summary>
        /// Sets the maximum level-of-detail parameter. This floating-point value limits the selection of the lowest resolution mipmap (highest mipmap level).
        /// </summary>
        public void SetMaxLod(float value)
        {
            Bind();
            GL.TexParameterf((uint)Target, GLEnum.TextureMaxLod, value);
        }

        /// <summary>
        /// Sets the index of the highest defined mipmap level.
        /// </summary>
        public void SetMaxLevel(int value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureMaxLevel, value);
        }

        /// <summary>
        /// Sets the swizzle that will be applied to the red component of a texel before it is returned to the shader.
        /// </summary>
        public void SetSwizzleRed(Swizzle value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureSwizzleR, (int)value);
        }

        /// <summary>
        /// Sets the swizzle that will be applied to the green component of a texel before it is returned to the shader.
        /// </summary>
        public void SetSwizzleGreen(Swizzle value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureSwizzleG, (int)value);
        }

        /// <summary>
        /// Sets the swizzle that will be applied to the blue component of a texel before it is returned to the shader.
        /// </summary>
        public void SetSwizzleBlue(Swizzle value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureSwizzleB, (int)value);
        }

        /// <summary>
        /// Sets the swizzle that will be applied to the alpha component of a texel before it is returned to the shader.
        /// </summary>
        public void SetSwizzleAlpha(Swizzle value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureSwizzleA, (int)value);
        }

        /// <summary>
        /// Sets the swizzle that will be applied to the red, green,e and alpha component of a texel before it is returned to the shader.
        /// </summary>
        public void SetSwizzle(Swizzle r, Swizzle g, Swizzle b, Swizzle a)
        {
            Bind();
            int[] values = new int[] { (int)r, (int)g, (int)b, (int)a };

            fixed (int* parameters = &values[0])
            {
                GL.TexParameteriv((uint)Target, GLEnum.TextureSwizzleA, parameters);
            }
        }

        /// <summary>
        /// Sets the wrap parameter for texture coordinate s.
        /// </summary>
        /// <param name="value"></param>
        public void SetWrapS(WrapStyle value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureWrapS, (int)value);
        }

        /// <summary>
        /// Sets the wrap parameter for texture coordinate t.
        /// </summary>
        /// <param name="value"></param>
        public void SetWrapT(WrapStyle value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureWrapT, (int)value);
        }

        /// <summary>
        /// Sets the wrap parameter for texture coordinate r.
        /// </summary>
        /// <param name="value"></param>
        public void SetWrapR(WrapStyle value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureWrapR, (int)value);
        }

        /// <summary>
        /// Sets the base level of a texture view relative to its parent.
        /// </summary>
        public void SetViewMinLevel(int value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureViewMinLevel, value);
        }

        /// <summary>
        /// Sets the number of levels of detail of a texture view.
        /// </summary>
        public void SetViewNumLevels(int value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureViewNumLevels, value);
        }

        /// <summary>
        /// Sets the first level of a texture array view relative to its parent.
        /// </summary>
        public void SetViewMinLayer(int value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureViewMinLayer, value);
        }

        /// <summary>
        /// Sets the number of layers in a texture array view.
        /// </summary>
        public void SetViewNumLayers(int value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureViewNumLayers, value);
        }

        /// <summary>
        /// Sets the number of immutable texture levels in a texture view.
        /// </summary>
        public void SetViewImmutableLevels(int value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureImmutableLevels, value);
        }

        /// <summary>
        /// Specifies the comparison operator used when <see cref="SetComparisonMode(ComparisonMode)"/> is set to <see cref="ComparisonMode.CompareToDepth"/>.
        /// </summary>
        /// <param name="value"></param>
        public void SetComparisonFunction(ComparisonFunction value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureCompareFunc, (int)value);
        }

        /// <summary>
        /// Specifies the texture comparison mode for currently bound depth textures.
        /// </summary>
        public void SetComparisonMode(ComparisonMode value)
        {
            Bind();
            GL.TexParameteri((uint)Target, GLEnum.TextureCompareMode, (int)value);
        }

        //
        // Parameter Gets
        //


        /// <summary>
        /// Returns the single-value depth stencil texture mode.
        /// </summary>
        public DepthStencilMode GetDepthStencilMode()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.DepthStencilTextureMode, &output);

            return (DepthStencilMode)output;
        }

        /// <summary>
        /// Returns the single-valued base texture mipmap level.
        /// </summary>
        public int GetBaseLevel()
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
        public Colour GetBorderColourI()
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
        public ColourF GetBorderColour()
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
        public float GetLodBias()
        {
            Bind();
            float output;

            GL.GetTexParameterfv((uint)Target, GLEnum.TextureLodBias, &output);

            return output;
        }

        /// <summary>
        /// Returns the single-valued texture minification filter.
        /// </summary>
        public TextureSampling GetMinFilter()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureMinFilter, &output);

            return (TextureSampling)output;
        }

        /// <summary>
        /// Returns the single-valued texture magnification filter.
        /// </summary>
        public TextureSampling GetMagFilter()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureMagFilter, &output);

            return (TextureSampling)output;
        }

        /// <summary>
        /// Returns the single-valued texture minimum level-of-detail value.
        /// </summary>
        public float GetMinLod()
        {
            Bind();
            float output;

            GL.GetTexParameterfv((uint)Target, GLEnum.TextureMinLod, &output);

            return output;
        }

        /// <summary>
        /// Returns the single-valued texture maximum level-of-detail value.
        /// </summary>
        public float GetMaxLod()
        {
            Bind();
            float output;

            GL.GetTexParameterfv((uint)Target, GLEnum.TextureMaxLod, &output);

            return output;
        }

        /// <summary>
        /// Returns the single-valued maximum texture mipmap array level.
        /// </summary>
        public int GetMaxLevel()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureMaxLevel, &output);

            return output;
        }

        /// <summary>
        /// Returns the red component swizzle.
        /// </summary>
        public Swizzle GetSwizzleRed()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureSwizzleR, &output);

            return (Swizzle)output;
        }

        /// <summary>
        /// Returns the green component swizzle.
        /// </summary>
        public Swizzle GetSwizzleGreen()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureSwizzleG, &output);

            return (Swizzle)output;
        }

        /// <summary>
        /// Returns the blue component swizzle.
        /// </summary>
        public Swizzle GetSwizzleBlue()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureSwizzleB, &output);

            return (Swizzle)output;
        }

        /// <summary>
        /// Returns the alpha component swizzle.
        /// </summary>
        public Swizzle GetSwizzleAlpha()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureSwizzleA, &output);

            return (Swizzle)output;
        }

        /// <summary>
        /// Returns the component swizzle for all channels in a single query.
        /// </summary>
        public void GetSwizzle(out Swizzle r, out Swizzle g, out Swizzle b, out Swizzle a)
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
        public WrapStyle GetWrapS()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureWrapS, &output);

            return (WrapStyle)output;
        }

        /// <summary>
        /// Returns the single-valued wrapping function for texture coordinate t.
        /// </summary>
        public WrapStyle GetWrapT()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureWrapT, &output);

            return (WrapStyle)output;
        }

        /// <summary>
        /// Returns the single-valued wrapping function for texture coordinate r.
        /// </summary>
        public WrapStyle GetWrapR()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureWrapR, &output);

            return (WrapStyle)output;
        }

        /// <summary>
        /// Returns a single-valued base level of a texture view relative to its parent.
        /// </summary>
        public int GetViewMinLevel()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureViewMinLevel, &output);

            return output;
        }

        /// <summary>
        /// Returns a single-valued number of levels of detail of a texture view.
        /// </summary>
        public int GetViewNumLevels()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureViewNumLevels, &output);

            return output;
        }

        /// <summary>
        /// Returns a single-valued first level of a texture array view relative to its parent.
        /// </summary>
        public int GetViewMinLayer()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureViewMinLayer, &output);

            return output;
        }

        /// <summary>
        /// Returns a single-valued number of layers in a texture array view.
        /// </summary>
        public int GetViewNumLayers()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureViewNumLayers, &output);

            return output;
        }

        /// <summary>
        /// Returns a single-valued number of immutable texture levels in a texture view.
        /// </summary>
        public int GetViewImmutableLevels()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureImmutableLevels, &output);

            return output;
        }

        /// <summary>
        /// Returns a single-valued texture comparison function.
        /// </summary>
        public ComparisonFunction GetComparisonFunction()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureCompareFunc, &output);

            return (ComparisonFunction)output;
        }

        /// <summary>
        /// Returns a single-valued texture comparison mode.
        /// </summary>
        public ComparisonMode GetComparisonMode()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureCompareMode, &output);

            return (ComparisonMode)output;
        }

        /// <summary>
        /// Returns the matching criteria use for the texture when used as an image texture.
        /// </summary>
        public FormatCompatibilityType GetFormatCompatibilityType()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.ImageFormatCompatibilityType, &output);

            return (FormatCompatibilityType)output;
        }

        /// <summary>
        /// Returns <see cref="true"/> if the texture has an immutable format, otherwise <see cref="false"/>.
        /// </summary>
        public bool IsImmutableFormat()
        {
            Bind();
            int output;

            GL.GetTexParameteriv((uint)Target, GLEnum.TextureImmutableFormat, &output);

            return output > 0;
        }

        /// <summary>
        /// Returns a single value, the width of the texture image.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public int GetWidth(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureWidth, &output);

            return output;
        }

        /// <summary>
        /// Returns a single value, the height of the texture image.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public int GetHeight(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureHeight, &output);

            return output;
        }

        /// <summary>
        /// Returns a single value, the depth of the texture image.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public int GetDepth(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureDepth, &output);

            return output;
        }

        /// <summary>
        /// The data type used to store the red component.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public ChannelType GetRedType(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureRedType, &output);

            return (ChannelType)output;
        }

        /// <summary>
        /// The data type used to store the green component.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public ChannelType GetGreenType(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureGreenType, &output);

            return (ChannelType)output;
        }

        /// <summary>
        /// The data type used to store the blue component.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public ChannelType GetBlueType(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureBlueType, &output);

            return (ChannelType)output;
        }

        /// <summary>
        /// The data type used to store the alpha component.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public ChannelType GetAlphaType(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureAlphaType, &output);

            return (ChannelType)output;
        }

        /// <summary>
        /// The data type used to store the depth component.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public ChannelType GetDepthType(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureDepthType, &output);

            return (ChannelType)output;
        }

        /// <summary>
        /// The internal storage resolution of the red component.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public int GetRedSize(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureRedSize, &output);

            return output;
        }

        /// <summary>
        /// The internal storage resolution of the green component.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public int GetGreenSize(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureGreenSize, &output);

            return output;
        }

        /// <summary>
        /// The internal storage resolution of the blue component.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public int GetBlueSize(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureBlueSize, &output);

            return output;
        }

        /// <summary>
        /// The internal storage resolution of the alpha component.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public int GetAlphaSize(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureAlphaSize, &output);

            return output;
        }

        /// <summary>
        /// The internal storage resolution of the depth component.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public int GetDepthSize(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureDepthSize, &output);

            return output;
        }

        /// <summary>
        /// Returns a single boolean value indicating if the texture image is stored in a compressed internal format.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public bool GetIsCompressed(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureCompressed, &output);

            return output > 0;
        }

        /// <summary>
        /// Returns a single integer value, the number of unsigned bytes of the compressed texture image that would be returned from <see cref="GetCompressedTexImage{T}(int)"/>.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public int GetCompressedImageSize(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureCompressedImageSize, &output);

            return output;
        }

        /// <summary>
        /// Returns a single integer value, the offset into the data store of the buffer bound to a buffer texture.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public int GetBufferOffset(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureBufferOffset, &output);

            return output;
        }

        /// <summary>
        /// Returns a single integer value, the size of the range of a data store of the buffer bound to a buffer texture.
        /// </summary>
        /// <param name="level">Specifies the level-of-detail number of the desired image. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        public int GetBufferSize(int level)
        {
            Bind();
            int output;

            GL.GetTexLevelParameteriv((uint)Target, level, GLEnum.TextureBufferSize, &output);

            return output;
        }
    }
}