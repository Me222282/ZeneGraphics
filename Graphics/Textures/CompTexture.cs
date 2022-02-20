using System;
using Zene.Graphics.Base.Extensions;
using Zene.Structs;

namespace Zene.Graphics.Textures
{
    public static unsafe class CompTexture
    {
        public static void SetCompressedData1D<T>(this ITexture texture, int level, int length, T[] data) where T : unmanaged
        {
            if (!texture.InternalFormat.IsCompressed())
            {
                throw new ArgumentException("Texture format wasn't a compressed format.", nameof(texture));
            }

            fixed (T* ptr = &data[0])
            {
                texture.CompressedTexImage1D(level, length, data.Length * sizeof(T), ptr);
            }
        }
        public static void SetCompressedData2D<T>(this ITexture texture, int level, int width, int height, T[] data) where T : unmanaged
        {
            if (!texture.InternalFormat.IsCompressed())
            {
                throw new ArgumentException("Texture format wasn't a compressed format.", nameof(texture));
            }

            fixed (T* ptr = &data[0])
            {
                texture.CompressedTexImage2D(level, width, height, data.Length * sizeof(T), ptr);
            }
        }
        public static void SetCompressedData3D<T>(this ITexture texture, int level, int width, int height, int depth, T[] data) where T : unmanaged
        {
            if (!texture.InternalFormat.IsCompressed())
            {
                throw new ArgumentException("Texture format wasn't a compressed format.", nameof(texture));
            }

            fixed (T* ptr = &data[0])
            {
                texture.CompressedTexImage3D(level, width, height, depth, data.Length * sizeof(T), ptr);
            }
        }

        public static void SetCompressedData1D<T>(this ITexture texture, int length, T[] data) where T : unmanaged
        {
            if (!texture.InternalFormat.IsCompressed())
            {
                throw new ArgumentException("Texture format wasn't a compressed format.", nameof(texture));
            }

            fixed (T* ptr = &data[0])
            {
                texture.CompressedTexImage1D(0, length, data.Length * sizeof(T), ptr);
            }
        }
        public static void SetCompressedData2D<T>(this ITexture texture, int width, int height, T[] data) where T : unmanaged
        {
            if (!texture.InternalFormat.IsCompressed())
            {
                throw new ArgumentException("Texture format wasn't a compressed format.", nameof(texture));
            }

            fixed (T* ptr = &data[0])
            {
                texture.CompressedTexImage2D(0, width, height, data.Length * sizeof(T), ptr);
            }
        }
        public static void SetCompressedData3D<T>(this ITexture texture, int width, int height, int depth, T[] data) where T : unmanaged
        {
            if (!texture.InternalFormat.IsCompressed())
            {
                throw new ArgumentException("Texture format wasn't a compressed format.", nameof(texture));
            }

            fixed (T* ptr = &data[0])
            {
                texture.CompressedTexImage3D(0, width, height, depth, data.Length * sizeof(T), ptr);
            }
        }

        public static void EditCompressedData1D<T>(this ITexture texture, int level, int offset, int length, T[] data) where T : unmanaged
        {
            if (!texture.InternalFormat.IsCompressed())
            {
                throw new ArgumentException("Texture format wasn't a compressed format.", nameof(texture));
            }

            fixed (T* ptr = &data[0])
            {
                texture.CompressedTexSubImage1D(level, offset, length, data.Length * sizeof(T), ptr);
            }
        }
        public static void EditCompressedData2D<T>(this ITexture texture, int level, Vector2I location, Vector2I size, T[] data) where T : unmanaged
        {
            if (!texture.InternalFormat.IsCompressed())
            {
                throw new ArgumentException("Texture format wasn't a compressed format.", nameof(texture));
            }

            fixed (T* ptr = &data[0])
            {
                texture.CompressedTexSubImage2D(level, location.X, location.Y, size.X, size.Y, data.Length * sizeof(T), ptr);
            }
        }
        public static void EditCompressedData3D<T>(this ITexture texture, int level, Vector3I location, Vector3I size, T[] data) where T : unmanaged
        {
            if (!texture.InternalFormat.IsCompressed())
            {
                throw new ArgumentException("Texture format wasn't a compressed format.", nameof(texture));
            }

            fixed (T* ptr = &data[0])
            {
                texture.CompressedTexSubImage3D(level, location.X, location.Y, location.Z, size.X, size.Y, size.Z, data.Length * sizeof(T), ptr);
            }
        }

        public static void EditCompressedData1D<T>(this ITexture texture, int offset, int length, T[] data) where T : unmanaged
        {
            if (!texture.InternalFormat.IsCompressed())
            {
                throw new ArgumentException("Texture format wasn't a compressed format.", nameof(texture));
            }

            fixed (T* ptr = &data[0])
            {
                texture.CompressedTexSubImage1D(0, offset, length, data.Length * sizeof(T), ptr);
            }
        }
        public static void EditCompressedData2D<T>(this ITexture texture, Vector2I location, Vector2I size, T[] data) where T : unmanaged
        {
            if (!texture.InternalFormat.IsCompressed())
            {
                throw new ArgumentException("Texture format wasn't a compressed format.", nameof(texture));
            }

            fixed (T* ptr = &data[0])
            {
                texture.CompressedTexSubImage2D(0, location.X, location.Y, size.X, size.Y, data.Length * sizeof(T), ptr);
            }
        }
        public static void EditCompressedData3D<T>(this ITexture texture, Vector3I location, Vector3I size, T[] data) where T : unmanaged
        {
            if (!texture.InternalFormat.IsCompressed())
            {
                throw new ArgumentException("Texture format wasn't a compressed format.", nameof(texture));
            }

            fixed (T* ptr = &data[0])
            {
                texture.CompressedTexSubImage3D(0, location.X, location.Y, location.Z, size.X, size.Y, size.Z, data.Length * sizeof(T), ptr);
            }
        }

        public static int GetCompressedSize(this ITexture texture)
        {
            if (!texture.InternalFormat.IsCompressed())
            {
                throw new ArgumentException("Texture format wasn't a compressed format.", nameof(texture));
            }

            return texture.GetCompressedImageSize(0);
        }
        public static int GetCompressedSize(this ITexture texture, int level)
        {
            if (!texture.InternalFormat.IsCompressed())
            {
                throw new ArgumentException("Texture format wasn't a compressed format.", nameof(texture));
            }

            return texture.GetCompressedImageSize(level);
        }

        public static T[] GetCompressedData<T>(this ITexture texture, int level) where T : unmanaged
        {
            return texture.GetCompressedTexImage<T>(level);
        }
        public static T[] GetCompressedData<T>(this ITexture texture) where T : unmanaged
        {
            return texture.GetCompressedTexImage<T>(0);
        }

        public static T[] GetCompressedDataSection<T>(this ITexture texture, int level, Vector3I location, Vector3I size, int bufferSize) where T : unmanaged
        {
            return texture.GetCompressedTextureSubImage<T>(level, location.X, location.Y, location.Z, size.X,size.Y, size.Z, bufferSize);
        }
        public static T[] GetCompressedDataSection<T>(this ITexture texture, Vector3I location, Vector3I size, int bufferSize) where T : unmanaged
        {
            return texture.GetCompressedTextureSubImage<T>(0, location.X, location.Y, location.Z, size.X, size.Y, size.Z, bufferSize);
        }
    }
}
