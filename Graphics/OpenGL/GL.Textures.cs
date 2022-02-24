// Copyright (c) 2017-2019 Zachary Snow
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Runtime.InteropServices;

namespace Zene.Graphics.Base
{
    public unsafe partial class GL
    {
		public struct TextureBinding
		{
			public uint Texture1D;
			public uint Texture1DArray;
			public uint Texture2D;
			public uint Texture2DArray;
			public uint Texture2DArrayMS;
			public uint Texture2DMS;
			public uint Texture3D;
			public uint CubeMap;
			public uint CubeMapArray;
			public uint Buffer;
			public uint Rectangle;
		}

		public static uint ActiveTextureUnit { get; private set; } = 0;

		public static TextureBinding[] BoundTextures { get; private set; }

		[OpenGLSupport(1.3)]
		public static void ActiveTexture(uint texture)
		{
			ActiveTextureUnit = texture - GLEnum.Texture0;

			Functions.ActiveTexture(texture);
		}

		[OpenGLSupport(4.5)]
		public static void CreateTextures(uint target, int n, uint* textures)
		{
			Functions.CreateTextures(target, n, textures);
		}
		[OpenGLSupport(1.1)]
		public static void GenTextures(int n, uint* textures)
		{
			Functions.GenTextures(n, textures);
		}
		[OpenGLSupport(1.1)]
		public static uint GenTexture()
		{
			uint texture;
			Functions.GenTextures(1, &texture);
			return texture;
		}

		[OpenGLSupport(1.1)]
		public static void DeleteTextures(int n, uint* textures)
		{
			Functions.DeleteTextures(n, textures);
		}
		[OpenGLSupport(3.0)]
		public static void DeleteTexture(uint texture)
		{
			Functions.DeleteTextures(1, &texture);
		}

		[OpenGLSupport(1.1)]
		public static void BindTexture(uint target, uint texture)
		{
			Functions.BindTexture(target, texture);

			switch (target)
			{
				case GLEnum.Texture1d:
					BoundTextures[ActiveTextureUnit].Texture1D = texture;
					return;
				case GLEnum.Texture1dArray:
					BoundTextures[ActiveTextureUnit].Texture1DArray = texture;
					return;
				case GLEnum.Texture2d:
					BoundTextures[ActiveTextureUnit].Texture2D = texture;
					return;
				case GLEnum.Texture2dArray:
					BoundTextures[ActiveTextureUnit].Texture2DArray = texture;
					return;
				case GLEnum.Texture2dMultisample:
					BoundTextures[ActiveTextureUnit].Texture2DMS = texture;
					return;
				case GLEnum.Texture2dMultisampleArray:
					BoundTextures[ActiveTextureUnit].Texture2DArrayMS = texture;
					return;
				case GLEnum.Texture3d:
					BoundTextures[ActiveTextureUnit].Texture3D = texture;
					return;
				case GLEnum.TextureCubeMap:
					BoundTextures[ActiveTextureUnit].CubeMap = texture;
					return;
				case GLEnum.TextureCubeMapArray:
					BoundTextures[ActiveTextureUnit].CubeMapArray = texture;
					return;
				case GLEnum.TextureBuffer:
					BoundTextures[ActiveTextureUnit].Buffer = texture;
					return;
				case GLEnum.TextureRectangle:
					BoundTextures[ActiveTextureUnit].Rectangle = texture;
					return;
			}
		}
		/// <summary>
		/// Note: <paramref name="textures"/> cannot be null nor can any element of <paramref name="textures"/> be null.
		/// </summary>
		/// <param name="first"></param>
		/// <param name="count"></param>
		/// <param name="textures"></param>
		[OpenGLSupport(1.3)]
		public static void BindTextures(uint first, int count, ITexture[] textures)
		{
			if (textures == null) { return; }

			for (uint i = 0; i < count; i++)
			{
				if (textures[i] == null) { continue; }

				ActiveTexture(GLEnum.Texture0 + first + i);
				BindTexture((uint)textures[i].Target, textures[i].Id);
			}
		}
		[OpenGLSupport(4.5)]
		public static void BindTextureUnit(uint unit, ITexture texture)
		{
			if (texture != null)
			{
				Functions.BindTextureUnit(unit, texture.Id);

				switch (texture.Target)
				{
					case TextureTarget.Texture1D:
						BoundTextures[unit].Texture1D = texture.Id;
						return;
					case TextureTarget.Texture1DArray:
						BoundTextures[unit].Texture1DArray = texture.Id;
						return;
					case TextureTarget.Texture2D:
						BoundTextures[unit].Texture2D = texture.Id;
						return;
					case TextureTarget.Texture2DArray:
						BoundTextures[unit].Texture2DArray = texture.Id;
						return;
					case TextureTarget.Multisample2D:
						BoundTextures[unit].Texture2DMS = texture.Id;
						return;
					case TextureTarget.Multisample2DArray:
						BoundTextures[unit].Texture2DArrayMS = texture.Id;
						return;
					case TextureTarget.Texture3D:
						BoundTextures[unit].Texture3D = texture.Id;
						return;
					case TextureTarget.CubeMap:
						BoundTextures[unit].CubeMap = texture.Id;
						return;
					case TextureTarget.CubeMapArray:
						BoundTextures[unit].CubeMapArray = texture.Id;
						return;
					case TextureTarget.Buffer:
						BoundTextures[unit].Buffer = texture.Id;
						return;
					case TextureTarget.Rectangle:
						BoundTextures[unit].Rectangle = texture.Id;
						return;
				}
				return;
			}
			// Reset all texture binding referances in unit
			BoundTextures[unit] = new TextureBinding();

			Functions.BindTextureUnit(unit, 0);
		}
		[OpenGLSupport(4.2)]
		public static void BindImageTexture(uint unit, ITexture texture, int level, bool layered, int layer, uint access, uint format)
		{
			if (texture != null)
			{
				Functions.BindImageTexture(unit, texture.Id, level, layered, layer, access, format);

				switch (texture.Target)
				{
					case TextureTarget.Texture1D:
						BoundTextures[unit].Texture1D = texture.Id;
						return;
					case TextureTarget.Texture1DArray:
						BoundTextures[unit].Texture1DArray = texture.Id;
						return;
					case TextureTarget.Texture2D:
						BoundTextures[unit].Texture2D = texture.Id;
						return;
					case TextureTarget.Texture2DArray:
						BoundTextures[unit].Texture2DArray = texture.Id;
						return;
					case TextureTarget.Multisample2D:
						BoundTextures[unit].Texture2DMS = texture.Id;
						return;
					case TextureTarget.Multisample2DArray:
						BoundTextures[unit].Texture2DArrayMS = texture.Id;
						return;
					case TextureTarget.Texture3D:
						BoundTextures[unit].Texture3D = texture.Id;
						return;
					case TextureTarget.CubeMap:
						BoundTextures[unit].CubeMap = texture.Id;
						return;
					case TextureTarget.CubeMapArray:
						BoundTextures[unit].CubeMapArray = texture.Id;
						return;
					case TextureTarget.Buffer:
						BoundTextures[unit].Buffer = texture.Id;
						return;
					case TextureTarget.Rectangle:
						BoundTextures[unit].Rectangle = texture.Id;
						return;
				}
				return;
			}

			// Reset all texture binding referances in unit
			BoundTextures[unit] = new TextureBinding();

			Functions.BindImageTexture(unit, 0, level, layered, layer, access, format);
		}
		[OpenGLSupport(4.4)]
		public static void BindImageTextures(uint first, int count, ITexture[] textures)
		{
			for (uint i = 0; i < count; i++)
			{
				if (textures == null || textures[i] == null)
				{
					BindImageTexture(first + i, null, 0, false, 0, GLEnum.ReadOnly, GLEnum.R8);
				}
				BindImageTexture(first + i, textures[i], 0, true, 0, GLEnum.ReadWrite, (uint)textures[i].InternalFormat);
			}
		}

		[OpenGLSupport(4.4)]
		public static void ClearTexImage(uint texture, int level, uint format, uint type, void* data)
		{
			Functions.ClearTexImage(texture, level, format, type, data);
		}
		[OpenGLSupport(4.4)]
		public static void ClearTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, void* data)
		{
			Functions.ClearTexSubImage(texture, level, xoffset, yoffset, zoffset, width, height, depth, format, type, data);
		}

		[OpenGLSupport(1.3)]
		public static void CompressedTexImage1D(ITexture target, int level, uint internalformat, int width, int border, int imageSize, void* data)
		{
			if (target == null)
            {
				throw new ArgumentNullException(nameof(target));
            }

			Functions.CompressedTexImage1D((uint)target.Target, level, internalformat, width, border, imageSize, data);

			if (level == target.Properties._baseLevel)
            {
				target.Properties._width = width;
				target.Properties._height = 1;
				target.Properties._depth = 1;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(1.3)]
		public static void CompressedTexImage2D(ITexture target, int level, uint internalformat, int width, int height, int border, int imageSize, void* data)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.CompressedTexImage2D((uint)target.Target, level, internalformat, width, height, border, imageSize, data);

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = height;
				target.Properties._depth = 1;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(1.3)]
		public static void CompressedTexImage2D(ITexture target, CubeMapFace face, int level, uint internalformat, int width, int height, int border, int imageSize, void* data)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.CompressedTexImage2D((uint)face, level, internalformat, width, height, border, imageSize, data);

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = height;
				target.Properties._depth = 1;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(1.3)]
		public static void CompressedTexImage3D(ITexture target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, void* data)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.CompressedTexImage3D((uint)target.Target, level, internalformat, width, height, depth, border, imageSize, data);

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = height;
				target.Properties._depth = depth;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(1.3)]
		public static void CompressedTexSubImage1D(uint target, int level, int xoffset, int width, uint format, int imageSize, void* data)
		{
			Functions.CompressedTexSubImage1D(target, level, xoffset, width, format, imageSize, data);
		}
		[OpenGLSupport(1.3)]
		public static void CompressedTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, void* data)
		{
			Functions.CompressedTexSubImage2D(target, level, xoffset, yoffset, width, height, format, imageSize, data);
		}
		[OpenGLSupport(1.3)]
		public static void CompressedTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, void* data)
		{
			Functions.CompressedTexSubImage3D(target, level, xoffset, yoffset, zoffset, width, height, depth, format, imageSize, data);
		}
		[OpenGLSupport(4.5)]
		public static void CompressedTextureSubImage1D(uint texture, int level, int xoffset, int width, uint format, int imageSize, void* data)
		{
			Functions.CompressedTextureSubImage1D(texture, level, xoffset, width, format, imageSize, data);
		}
		[OpenGLSupport(4.5)]
		public static void CompressedTextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, void* data)
		{
			Functions.CompressedTextureSubImage2D(texture, level, xoffset, yoffset, width, height, format, imageSize, data);
		}
		[OpenGLSupport(4.5)]
		public static void CompressedTextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, void* data)
		{
			Functions.CompressedTextureSubImage3D(texture, level, xoffset, yoffset, zoffset, width, height, depth, format, imageSize, data);
		}
		
		[OpenGLSupport(4.3)]
		public static void CopyImageSubData(uint srcName, uint srcTarget, int srcLevel, int srcX, int srcY, int srcZ, uint dstName, uint dstTarget, int dstLevel, int dstX, int dstY, int dstZ, int srcWidth, int srcHeight, int srcDepth)
		{
			Functions.CopyImageSubData(srcName, srcTarget, srcLevel, srcX, srcY, srcZ, dstName, dstTarget, dstLevel, dstX, dstY, dstZ, srcWidth, srcHeight, srcDepth);
		}

		[OpenGLSupport(1.1)]
		public static void CopyTexImage1D(ITexture target, int level, uint internalformat, int x, int y, int width, int border)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.CopyTexImage1D((uint)target.Target, level, internalformat, x, y, width, border);

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = 1;
				target.Properties._depth = 1;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(1.1)]
		public static void CopyTexImage2D(ITexture target, int level, uint internalformat, int x, int y, int width, int height, int border)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.CopyTexImage2D((uint)target.Target, level, internalformat, x, y, width, height, border);

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = height;
				target.Properties._depth = 1;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(1.1)]
		public static void CopyTexImage2D(ITexture target, CubeMapFace face, int level, uint internalformat, int x, int y, int width, int height, int border)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.CopyTexImage2D((uint)face, level, internalformat, x, y, width, height, border);

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = height;
				target.Properties._depth = 1;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(1.1)]
		public static void CopyTexSubImage1D(uint target, int level, int xoffset, int x, int y, int width)
		{
			Functions.CopyTexSubImage1D(target, level, xoffset, x, y, width);
		}
		[OpenGLSupport(1.1)]
		public static void CopyTexSubImage2D(uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height)
		{
			Functions.CopyTexSubImage2D(target, level, xoffset, yoffset, x, y, width, height);
		}
		[OpenGLSupport(1.2)]
		public static void CopyTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height)
		{
			Functions.CopyTexSubImage3D(target, level, xoffset, yoffset, zoffset, x, y, width, height);
		}
		[OpenGLSupport(4.5)]
		public static void CopyTextureSubImage1D(uint texture, int level, int xoffset, int x, int y, int width)
		{
			Functions.CopyTextureSubImage1D(texture, level, xoffset, x, y, width);
		}
		[OpenGLSupport(4.5)]
		public static void CopyTextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int x, int y, int width, int height)
		{
			Functions.CopyTextureSubImage2D(texture, level, xoffset, yoffset, x, y, width, height);
		}
		[OpenGLSupport(4.5)]
		public static void CopyTextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height)
		{
			Functions.CopyTextureSubImage3D(texture, level, xoffset, yoffset, zoffset, x, y, width, height);
		}

		[OpenGLSupport(1.3)]
		public static void GetCompressedTexImage(uint target, int level, void* img)
		{
			Functions.GetCompressedTexImage(target, level, img);
		}
		[OpenGLSupport(4.5)]
		public static void GetCompressedTextureImage(uint texture, int level, int bufSize, void* pixels)
		{
			Functions.GetCompressedTextureImage(texture, level, bufSize, pixels);
		}
		[OpenGLSupport(4.5)]
		public static void GetCompressedTextureSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int bufSize, void* pixels)
		{
			Functions.GetCompressedTextureSubImage(texture, level, xoffset, yoffset, zoffset, width, height, depth, bufSize, pixels);
		}

		[OpenGLSupport(1.0)]
		public static void GetTexImage(uint target, int level, uint format, uint type, void* pixels)
		{
			Functions.GetTexImage(target, level, format, type, pixels);
		}
		[OpenGLSupport(4.5)]
		public static void GetTextureImage(uint texture, int level, uint format, uint type, int bufSize, void* pixels)
		{
			Functions.GetTextureImage(texture, level, format, type, bufSize, pixels);
		}
		[OpenGLSupport(4.5)]
		public static void GetTextureSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, int bufSize, void* pixels)
		{
			Functions.GetTextureSubImage(texture, level, xoffset, yoffset, zoffset, width, height, depth, format, type, bufSize, pixels);
		}

		[OpenGLSupport(4.5)]
		public static void GetTextureLevelParameterfv(uint texture, int level, uint pname, float* @params)
		{
			Functions.GetTextureLevelParameterfv(texture, level, pname, @params);
		}
		[OpenGLSupport(4.5)]
		public static void GetTextureLevelParameteriv(uint texture, int level, uint pname, int* @params)
		{
			Functions.GetTextureLevelParameteriv(texture, level, pname, @params);
		}
		
		[OpenGLSupport(4.5)]
		public static void GetTextureParameterfv(uint texture, uint pname, float* @params)
		{
			Functions.GetTextureParameterfv(texture, pname, @params);
		}
		[OpenGLSupport(4.5)]
		public static void GetTextureParameterIiv(uint texture, uint pname, int* @params)
		{
			Functions.GetTextureParameterIiv(texture, pname, @params);
		}
		[OpenGLSupport(4.5)]
		public static void GetTextureParameterIuiv(uint texture, uint pname, uint* @params)
		{
			Functions.GetTextureParameterIuiv(texture, pname, @params);
		}
		[OpenGLSupport(4.5)]
		public static void GetTextureParameteriv(uint texture, uint pname, int* @params)
		{
			Functions.GetTextureParameteriv(texture, pname, @params);
		}

		[OpenGLSupport(3.0)]
		public static void GenerateMipmap(uint target)
		{
			Functions.GenerateMipmap(target);
		}
		[OpenGLSupport(4.5)]
		public static void GenerateTextureMipmap(uint texture)
		{
			Functions.GenerateTextureMipmap(texture);
		}

		[OpenGLSupport(4.3)]
		public static void InvalidateTexImage(uint texture, int level)
		{
			Functions.InvalidateTexImage(texture, level);
		}
		[OpenGLSupport(4.3)]
		public static void InvalidateTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth)
		{
			Functions.InvalidateTexSubImage(texture, level, xoffset, yoffset, zoffset, width, height, depth);
		}

		[OpenGLSupport(3.1)]
		public static void TexBuffer(uint target, uint internalformat, uint buffer)
		{
			Functions.TexBuffer(target, internalformat, buffer);
		}
		[OpenGLSupport(4.3)]
		public static void TexBufferRange(uint target, uint internalformat, uint buffer, int offset, int size)
		{
			Functions.TexBufferRange(target, internalformat, buffer, offset, size);
		}
		[OpenGLSupport(4.5)]
		public static void TextureBuffer(uint texture, uint internalformat, uint buffer)
		{
			Functions.TextureBuffer(texture, internalformat, buffer);
		}
		[OpenGLSupport(4.5)]
		public static void TextureBufferRange(uint texture, uint internalformat, uint buffer, int offset, int size)
		{
			Functions.TextureBufferRange(texture, internalformat, buffer, offset, size);
		}

		[OpenGLSupport(1.0)]
		public static void TexImage1D(ITexture target, int level, int internalformat, int width, int border, uint format, uint type, void* pixels)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.TexImage1D((uint)target.Target, level, internalformat, width, border, format, type, pixels);

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = 1;
				target.Properties._depth = 1;
				target.Properties._samples = 0;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(1.0)]
		public static void TexImage2D(ITexture target, int level, int internalformat, int width, int height, int border, uint format, uint type, void* pixels)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.TexImage2D((uint)target.Target, level, internalformat, width, height, border, format, type, pixels);

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = height;
				target.Properties._depth = 1;
				target.Properties._samples = 0;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(1.0)]
		public static void TexImage2D(ITexture target, int level, int internalformat, int width, int height, int border, uint format, uint type, IntPtr pixels)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.TexImage2D((uint)target.Target, level, internalformat, width, height, border, format, type, pixels.ToPointer());

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = height;
				target.Properties._depth = 1;
				target.Properties._samples = 0;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(1.0)]
		public static void TexImage2D(ITexture target, CubeMapFace face, int level, int internalformat, int width, int height, int border, uint format, uint type, void* pixels)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.TexImage2D((uint)face, level, internalformat, width, height, border, format, type, pixels);

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = height;
				target.Properties._depth = 1;
				target.Properties._samples = 0;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(1.0)]
		public static void TexImage2D(ITexture target, CubeMapFace face, int level, int internalformat, int width, int height, int border, uint format, uint type, IntPtr pixels)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.TexImage2D((uint)face, level, internalformat, width, height, border, format, type, pixels.ToPointer());

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = height;
				target.Properties._depth = 1;
				target.Properties._samples = 0;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(3.2)]
		public static void TexImage2DMultisample(ITexture target, int samples, uint internalformat, int width, int height, bool fixedsamplelocations)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.TexImage2DMultisample((uint)target.Target, samples, internalformat, width, height, fixedsamplelocations);

			target.Properties._width = width;
			target.Properties._height = height;
			target.Properties._depth = 1;
			target.Properties._samples = samples;
			target.Properties.InternalFormatChanged();
		}
		[OpenGLSupport(1.2)]
		public static void TexImage3D(ITexture target, int level, int internalformat, int width, int height, int depth, int border, uint format, uint type, void* pixels)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.TexImage3D((uint)target.Target, level, internalformat, width, height, depth, border, format, type, pixels);

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = height;
				target.Properties._depth = depth;
				target.Properties._samples = 0;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(3.2)]
		public static void TexImage3DMultisample(ITexture target, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.TexImage3DMultisample((uint)target.Target, samples, internalformat, width, height, depth, fixedsamplelocations);

			target.Properties._width = width;
			target.Properties._height = height;
			target.Properties._depth = depth;
			target.Properties._samples = samples;
			target.Properties.InternalFormatChanged();
		}

		[OpenGLSupport(1.0)]
		public static unsafe void TexImage1D<T>(ITexture target, int level, int internalformat, int width, int border, uint format, uint type, ReadOnlySpan<T> pixels)
			where T : unmanaged
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			fixed (void* dataPtr = &MemoryMarshal.GetReference(pixels))
			{
				Functions.TexImage1D((uint)target.Target, level, internalformat, width, border, format, type, dataPtr);
			}

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = 1;
				target.Properties._depth = 1;
				target.Properties._samples = 0;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(1.0)]
		public static unsafe void TexImage2D<T>(ITexture target, int level, int internalformat, int width, int height, int border, uint format, uint type, ReadOnlySpan<T> pixels)
			where T : unmanaged
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			fixed (void* dataPtr = &MemoryMarshal.GetReference(pixels))
			{
				Functions.TexImage2D((uint)target.Target, level, internalformat, width, height, border, format, type, dataPtr);
			}

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = height;
				target.Properties._depth = 1;
				target.Properties._samples = 0;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(1.0)]
		public static unsafe void TexImage2D<T>(ITexture target, CubeMapFace face, int level, int internalformat, int width, int height, int border, uint format, uint type, ReadOnlySpan<T> pixels)
			where T : unmanaged
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			fixed (void* dataPtr = &MemoryMarshal.GetReference(pixels))
			{
				Functions.TexImage2D((uint)face, level, internalformat, width, height, border, format, type, dataPtr);
			}

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = height;
				target.Properties._depth = 1;
				target.Properties._samples = 0;
				target.Properties.InternalFormatChanged();
			}
		}
		[OpenGLSupport(1.2)]
		public static unsafe void TexImage3D<T>(ITexture target, int level, int internalformat, int width, int height, int depth, int border, uint format, uint type, ReadOnlySpan<T> pixels)
			where T : unmanaged
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			fixed (void* dataPtr = &MemoryMarshal.GetReference(pixels))
			{
				Functions.TexImage3D((uint)target.Target, level, internalformat, width, height, depth, border, format, type, dataPtr);
			}

			if (level == target.Properties._baseLevel)
			{
				target.Properties._width = width;
				target.Properties._height = height;
				target.Properties._depth = depth;
				target.Properties._samples = 0;
				target.Properties.InternalFormatChanged();
			}
		}

		[OpenGLSupport(4.2)]
		public static void TexStorage1D(ITexture target, int levels, uint internalformat, int width)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.TexStorage1D((uint)target.Target, levels, internalformat, width);

			target.Properties._width = width;
			target.Properties._height = 1;
			target.Properties._depth = 1;
			target.Properties._samples = 0;
			target.Properties._immutableFormat = true;
			target.Properties.InternalFormatChanged();
		}
		[OpenGLSupport(4.2)]
		public static void TexStorage2D(ITexture target, int levels, uint internalformat, int width, int height)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.TexStorage2D((uint)target.Target, levels, internalformat, width, height);

			target.Properties._width = width;
			target.Properties._height = height;
			target.Properties._depth = 1;
			target.Properties._samples = 0;
			target.Properties._immutableFormat = true;
			target.Properties.InternalFormatChanged();
		}
		[OpenGLSupport(4.2)]
		public static void TexStorage2D(ITexture target, CubeMapFace face, int levels, uint internalformat, int width, int height)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.TexStorage2D((uint)face, levels, internalformat, width, height);

			target.Properties._width = width;
			target.Properties._height = height;
			target.Properties._depth = 1;
			target.Properties._samples = 0;
			target.Properties._immutableFormat = true;
			target.Properties.InternalFormatChanged();
		}
		[OpenGLSupport(4.3)]
		public static void TexStorage2DMultisample(ITexture target, int samples, uint internalformat, int width, int height, bool fixedsamplelocations)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.TexStorage2DMultisample((uint)target.Target, samples, internalformat, width, height, fixedsamplelocations);

			target.Properties._width = width;
			target.Properties._height = height;
			target.Properties._depth = 1;
			target.Properties._samples = samples;
			target.Properties._immutableFormat = true;
			target.Properties.InternalFormatChanged();
		}
		[OpenGLSupport(4.2)]
		public static void TexStorage3D(ITexture target, int levels, uint internalformat, int width, int height, int depth)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.TexStorage3D((uint)target.Target, levels, internalformat, width, height, depth);

			target.Properties._width = width;
			target.Properties._height = height;
			target.Properties._depth = depth;
			target.Properties._samples = 0;
			target.Properties._immutableFormat = true;
			target.Properties.InternalFormatChanged();
		}
		[OpenGLSupport(4.3)]
		public static void TexStorage3DMultisample(ITexture target, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations)
		{
			if (target == null)
			{
				throw new ArgumentNullException(nameof(target));
			}

			Functions.TexStorage3DMultisample((uint)target.Target, samples, internalformat, width, height, depth, fixedsamplelocations);

			target.Properties._width = width;
			target.Properties._height = height;
			target.Properties._depth = depth;
			target.Properties._samples = samples;
			target.Properties._immutableFormat = true;
			target.Properties.InternalFormatChanged();
		}

		[OpenGLSupport(4.5)]
		public static void TextureStorage1D(ITexture texture, int levels, uint internalformat, int width)
		{
			if (texture == null)
			{
				throw new ArgumentNullException(nameof(texture));
			}

			Functions.TextureStorage1D(texture.Id, levels, internalformat, width);

			texture.Properties._width = width;
			texture.Properties._height = 1;
			texture.Properties._depth = 1;
			texture.Properties._samples = 0;
			texture.Properties._immutableFormat = true;
			texture.Properties.InternalFormatChanged();
		}
		[OpenGLSupport(4.5)]
		public static void TextureStorage2D(ITexture texture, int levels, uint internalformat, int width, int height)
		{
			if (texture == null)
			{
				throw new ArgumentNullException(nameof(texture));
			}

			Functions.TextureStorage2D(texture.Id, levels, internalformat, width, height);

			texture.Properties._width = width;
			texture.Properties._height = height;
			texture.Properties._depth = 1;
			texture.Properties._samples = 0;
			texture.Properties._immutableFormat = true;
			texture.Properties.InternalFormatChanged();
		}
		[OpenGLSupport(4.5)]
		public static void TextureStorage2DMultisample(ITexture texture, int samples, uint internalformat, int width, int height, bool fixedsamplelocations)
		{
			if (texture == null)
			{
				throw new ArgumentNullException(nameof(texture));
			}

			Functions.TextureStorage2DMultisample(texture.Id, samples, internalformat, width, height, fixedsamplelocations);

			texture.Properties._width = width;
			texture.Properties._height = height;
			texture.Properties._depth = 1;
			texture.Properties._samples = samples;
			texture.Properties._immutableFormat = true;
			texture.Properties.InternalFormatChanged();
		}
		[OpenGLSupport(4.5)]
		public static void TextureStorage3D(ITexture texture, int levels, uint internalformat, int width, int height, int depth)
		{
			if (texture == null)
			{
				throw new ArgumentNullException(nameof(texture));
			}

			Functions.TextureStorage3D(texture.Id, levels, internalformat, width, height, depth);

			texture.Properties._width = width;
			texture.Properties._height = height;
			texture.Properties._depth = depth;
			texture.Properties._samples = 0;
			texture.Properties._immutableFormat = true;
			texture.Properties.InternalFormatChanged();
		}
		[OpenGLSupport(4.5)]
		public static void TextureStorage3DMultisample(ITexture texture, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations)
		{
			if (texture == null)
			{
				throw new ArgumentNullException(nameof(texture));
			}

			Functions.TextureStorage3DMultisample(texture.Id, samples, internalformat, width, height, depth, fixedsamplelocations);

			texture.Properties._width = width;
			texture.Properties._height = height;
			texture.Properties._depth = depth;
			texture.Properties._samples = samples;
			texture.Properties._immutableFormat = true;
			texture.Properties.InternalFormatChanged();
		}

		[OpenGLSupport(1.1)]
		public static void TexSubImage1D(uint target, int level, int xoffset, int width, uint format, uint type, void* pixels)
		{
			Functions.TexSubImage1D(target, level, xoffset, width, format, type, pixels);
		}
		[OpenGLSupport(1.1)]
		public static void TexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, void* pixels)
		{
			Functions.TexSubImage2D(target, level, xoffset, yoffset, width, height, format, type, pixels);
		}
		[OpenGLSupport(1.2)]
		public static void TexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, void* pixels)
		{
			Functions.TexSubImage3D(target, level, xoffset, yoffset, zoffset, width, height, depth, format, type, pixels);
		}

		[OpenGLSupport(4.5)]
		public static void TextureSubImage1D(uint texture, int level, int xoffset, int width, uint format, uint type, void* pixels)
		{
			Functions.TextureSubImage1D(texture, level, xoffset, width, format, type, pixels);
		}
		[OpenGLSupport(4.5)]
		public static void TextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, void* pixels)
		{
			Functions.TextureSubImage2D(texture, level, xoffset, yoffset, width, height, format, type, pixels);
		}
		[OpenGLSupport(4.5)]
		public static void TextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, void* pixels)
		{
			Functions.TextureSubImage3D(texture, level, xoffset, yoffset, zoffset, width, height, depth, format, type, pixels);
		}

		[OpenGLSupport(4.3)]
		public static void TextureView(ITexture texture, uint target, ITexture origtexture, uint internalformat, uint minlevel, uint numlevels, uint minlayer, uint numlayers)
		{
			if (texture == null)
			{
				throw new ArgumentNullException(nameof(texture));
			}
			if (origtexture == null)
			{
				throw new ArgumentNullException(nameof(origtexture));
			}

			Functions.TextureView(texture.Id, target, origtexture.Id, internalformat, minlevel, numlevels, minlayer, numlayers);

			texture.Properties._width = origtexture.Properties._width;
			texture.Properties._height = origtexture.Properties._height;
			texture.Properties._depth = origtexture.Properties._depth;
			texture.Properties._samples = origtexture.Properties._samples;

			texture.Properties._immutableFormat = true;

			texture.Properties.InternalFormatChanged();
		}

		[OpenGLSupport(4.5)]
		public static void TextureParameterf(uint texture, uint pname, float param)
		{
			Functions.TextureParameterf(texture, pname, param);
		}
		[OpenGLSupport(4.5)]
		public static void TextureParameterfv(uint texture, uint pname, float* param)
		{
			Functions.TextureParameterfv(texture, pname, param);
		}
		[OpenGLSupport(4.5)]
		public static void TextureParameteri(uint texture, uint pname, int param)
		{
			Functions.TextureParameteri(texture, pname, param);
		}
		[OpenGLSupport(4.5)]
		public static void TextureParameterIiv(uint texture, uint pname, int* @params)
		{
			Functions.TextureParameterIiv(texture, pname, @params);
		}
		[OpenGLSupport(4.5)]
		public static void TextureParameterIuiv(uint texture, uint pname, uint* @params)
		{
			Functions.TextureParameterIuiv(texture, pname, @params);
		}
		[OpenGLSupport(4.5)]
		public static void TextureParameteriv(uint texture, uint pname, int* param)
		{
			Functions.TextureParameteriv(texture, pname, param);
		}

		[OpenGLSupport(1.0)]
		internal static void TexParameterf(uint target, uint pname, float param)
		{
			Functions.TexParameterf(target, pname, param);
		}
		[OpenGLSupport(1.0)]
		internal static void TexParameterfv(uint target, uint pname, float* @params)
		{
			Functions.TexParameterfv(target, pname, @params);
		}
		[OpenGLSupport(1.0)]
		internal static void TexParameteri(uint target, uint pname, int param)
		{
			Functions.TexParameteri(target, pname, param);
		}
		[OpenGLSupport(3.0)]
		internal static void TexParameterIiv(uint target, uint pname, int* @params)
		{
			Functions.TexParameterIiv(target, pname, @params);
		}
		[OpenGLSupport(3.0)]
		internal static void TexParameterIuiv(uint target, uint pname, uint* @params)
		{
			Functions.TexParameterIuiv(target, pname, @params);
		}
		[OpenGLSupport(1.0)]
		internal static void TexParameteriv(uint target, uint pname, int* @params)
		{
			Functions.TexParameteriv(target, pname, @params);
		}

		[OpenGLSupport(1.0)]
		public static void GetTexLevelParameterfv(uint target, int level, uint pname, float* @params)
		{
			Functions.GetTexLevelParameterfv(target, level, pname, @params);
		}
		[OpenGLSupport(1.0)]
		public static void GetTexLevelParameteriv(uint target, int level, uint pname, int* @params)
		{
			Functions.GetTexLevelParameteriv(target, level, pname, @params);
		}
		[OpenGLSupport(1.0)]
		public static void GetTexParameterfv(uint target, uint pname, float* @params)
		{
			Functions.GetTexParameterfv(target, pname, @params);
		}
		[OpenGLSupport(3.0)]
		public static void GetTexParameterIiv(uint target, uint pname, int* @params)
		{
			Functions.GetTexParameterIiv(target, pname, @params);
		}
		[OpenGLSupport(3.0)]
		public static void GetTexParameterIuiv(uint target, uint pname, uint* @params)
		{
			Functions.GetTexParameterIuiv(target, pname, @params);
		}
		[OpenGLSupport(1.0)]
		public static void GetTexParameteriv(uint target, uint pname, int* @params)
		{
			Functions.GetTexParameteriv(target, pname, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetnCompressedTexImage(uint target, int lod, int bufSize, void* pixels)
		{
			Functions.GetnCompressedTexImage(target, lod, bufSize, pixels);
		}
		[OpenGLSupport(4.5)]
		public static void GetnTexImage(uint target, int level, uint format, uint type, int bufSize, void* pixels)
		{
			Functions.GetnTexImage(target, level, format, type, bufSize, pixels);
		}

		[OpenGLSupport(4.3)]
		public static void GetInternalformati64v(uint target, uint internalformat, uint pname, int bufSize, long* @params)
		{
			Functions.GetInternalformati64v(target, internalformat, pname, bufSize, @params);
		}
		[OpenGLSupport(4.2)]
		public static void GetInternalformativ(uint target, uint internalformat, uint pname, int bufSize, int* @params)
		{
			Functions.GetInternalformativ(target, internalformat, pname, bufSize, @params);
		}
	}
}