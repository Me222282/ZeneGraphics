using StbImageSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that manages a cube map texture object.
    /// </summary>
    public class CubeMap : TextureGL
    {
        /// <summary>
        /// Creates the cube map texture with a set internal format.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        /// <param name="dataType">The type of data that is going to be passed to OpenGL.</param>
        public CubeMap(TextureFormat format, TextureData dataType)
            : base(TextureTarget.CubeMap)
        {
            InternalFormat = format;
            DataType = dataType;
        }
        internal CubeMap(uint id, TextureFormat format, TextureData dataType)
            : base(id, TextureTarget.CubeMap, format)
        {
            InternalFormat = format;
            DataType = dataType;
        }

        /// <summary>
        /// The type of data being inputed into the texture.
        /// </summary>
        public TextureData DataType { get; set; }

        /// <summary>
        /// The formating of data stored in this texture.
        /// </summary>
        public new TextureFormat InternalFormat { get; }

        /// <summary>
        /// Creats and filles the data of <paramref name="face"/> inside the texture with <paramref name="data"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="face">The face to fill with <paramref name="data"/>.</param>
        /// <param name="level">The mipmap level.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="inputFormat">The format of <paramref name="data"/>.</param>
        /// <param name="data">The data to set the texture with.</param>
        public void SetData<T>(CubeMapFace face, int level, int width, int height, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged
        {
            TexImage2D(face, level, InternalFormat, width, height, inputFormat, DataType, data);
        }
        /// <summary>
        /// Creats and filles the data of <paramref name="face"/> inside the texture with <paramref name="data"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="face">The face to fill with <paramref name="data"/>.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="inputFormat">The format of <paramref name="data"/>.</param>
        /// <param name="data">The data to set the texture with.</param>
        public void SetData<T>(CubeMapFace face, int width, int height, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged =>
            SetData(face, 0, width, height, inputFormat, data);

        /// <summary>
        /// Changes the data in a section of the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="face">The face to edit.</param>
        /// <param name="level">The mipmap level.</param>
        /// <param name="x">The x location of the region.</param>
        /// <param name="y">The y location of the region.</param>
        /// <param name="width">The width of the region.</param>
        /// <param name="height">The height of the region.</param>
        /// <param name="inputFormat">The format of <paramref name="data"/>.</param>
        /// <param name="data">The data to set the region with.</param>
        public void EditData<T>(CubeMapFace face, int level, int x, int y, int width, int height, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged
        {
            TexSubImage2D(face, level, x, y, width, height, inputFormat, DataType, data);
        }
        /// <summary>
        /// Changes the data in a section of the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="face">The face to edit.</param>
        /// <param name="x">The x location of the region.</param>
        /// <param name="y">The y location of the region.</param>
        /// <param name="width">The width of the region.</param>
        /// <param name="height">The height of the region.</param>
        /// <param name="inputFormat">The format of <paramref name="data"/>.</param>
        /// <param name="data">The data to set the region with.</param>
        public void EditData<T>(CubeMapFace face, int x, int y, int width, int height, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged =>
            EditData(face, 0, x, y, width, height, inputFormat, data);

        /// <summary>
        /// Creates the storage space for the texture.
        /// </summary>
        /// <param name="face">The face to alocate.</param>
        /// <param name="level">The number of mipmap levels.</param>
        /// <param name="width">The width of the space.</param>
        /// <param name="height">The height of the space.</param>
        public void CreateStorage(CubeMapFace face, int levels, int width, int height)
        {
            TexStorage2D(face, levels, InternalFormat, width, height);
        }
        /// <summary>
        /// Creates the storage space for the texture.
        /// </summary>
        /// <param name="face">The face to alocate.</param>
        /// <param name="width">The width of the space.</param>
        /// <param name="height">The height of the space.</param>
        public void CreateStorage(CubeMapFace face, int width, int height) => CreateStorage(face, 1, width, height);

        /// <summary>
        /// Gets the data stored in the texture for a single face.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="face">The face to referance.</param>
        /// <param name="level">The mipmap level.</param>
        /// <param name="outputFormat">The format of the returned data.</param>
        /// <returns></returns>
        public GLArray<T> GetData<T>(CubeMapFace face, int level, BaseFormat outputFormat) where T : unmanaged
        {
            return GetTexImage<T>(face, level, outputFormat, DataType);
        }
        /// <summary>
        /// Gets the data stored in the texture for a single face.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="face">The face to referance.</param>
        /// <param name="outputFormat">The format of the returned data.</param>
        /// <returns></returns>
        public GLArray<T> GetData<T>(CubeMapFace face, BaseFormat outputFormat) where T : unmanaged =>
            GetData<T>(face, 0, outputFormat);

        /// <summary>
        /// Gets the data stored in all faces of the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="outputFormat">The format of the returned data.</param>
        /// <returns></returns>
        public GLArray<T> GetData<T>(int level, BaseFormat outputFormat) where T : unmanaged
        {
            return GetTextureSubImage<T>(level, 0, 0, 0, Width, Height, 6, outputFormat, DataType);
        }
        /// <summary>
        /// Gets the data stored in all faces of the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="outputFormat">The format of the returned data.</param>
        /// <returns></returns>
        public GLArray<T> GetData<T>(BaseFormat outputFormat) where T : unmanaged =>
            GetData<T>(0, outputFormat);
        /// <summary>
        /// Get a section of the data of <paramref name="face"/> stored in the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="face">The face to referance.</param>
        /// <param name="level">The mipmap level.</param>
        /// <param name="x">The x location of the region.</param>
        /// <param name="y">The y location of the region.</param>
        /// <param name="width">The width of the region.</param>
        /// <param name="height">The height of the region.</param>
        /// <param name="outputFormat">The format of the returned data.</param>
        /// <returns></returns>
        public GLArray<T> GetDataSection<T>(CubeMapFace face, int level, int x, int y, int width, int height, BaseFormat outputFormat) where T : unmanaged
        {
            int zFace = face switch
            {
                CubeMapFace.Right => 0,
                CubeMapFace.Left => 1,
                CubeMapFace.Top => 2,
                CubeMapFace.Bottom => 3,
                CubeMapFace.Front => 4,
                CubeMapFace.Back => 5,
                _ => 0
            };

            return GetTextureSubImage<T>(level, x, y, zFace, width, height, 1, outputFormat, DataType);
        }
        /// <summary>
        /// Get a section of the data of <paramref name="face"/> stored in the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="face">The face to referance.</param>
        /// <param name="x">The x location of the region.</param>
        /// <param name="y">The y location of the region.</param>
        /// <param name="width">The width of the region.</param>
        /// <param name="height">The height of the region.</param>
        /// <param name="outputFormat">The format of the returned data.</param>
        /// <returns></returns>
        public GLArray<T> GetDataSection<T>(CubeMapFace face, int x, int y, int width, int height, BaseFormat outputFormat) where T : unmanaged =>
            GetDataSection<T>(face, 0, x, y, width, height, outputFormat);

        /// <summary>
        /// Copy data from <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="srcLevel">The mipmap level from source.</param>
        /// <param name="srcX">The source x.</param>
        /// <param name="srcY">The source y.</param>
        /// <param name="srcZ">The source z.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        /// <param name="numFaces">The number of faces to copy to starting from <paramref name="startFace"/>.</param>
        /// <param name="level">The mipmap level.</param>
        /// <param name="x">The x offset to write to.</param>
        /// <param name="y">The y offset to write to.</param>
        /// <param name="startFace">The starting face to copy data to.</param>
        public void CopyTexture(ITexture source, int srcLevel, int srcX, int srcY, int srcZ, int width, int height, int numFaces, int level, int x, int y, CubeMapFace startFace)
        {
            int zFace = startFace switch
            {
                CubeMapFace.Right => 0,
                CubeMapFace.Left => 1,
                CubeMapFace.Top => 2,
                CubeMapFace.Bottom => 3,
                CubeMapFace.Front => 4,
                CubeMapFace.Back => 5,
                _ => 0
            };

            CopyImageSubData(source, srcLevel, srcX, srcY, srcZ, width, height, numFaces, level, x, y, zFace);
        }
        /// <summary>
        /// Copy data from <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="srcX">The source x.</param>
        /// <param name="srcY">The source y.</param>
        /// <param name="srcZ">The source z.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        /// <param name="numFaces">The number of faces to copy to starting from <paramref name="startFace"/>.</param>
        /// <param name="x">The x offset to write to.</param>
        /// <param name="y">The y offset to write to.</param>
        /// <param name="startFace">The starting face to copy data to.</param>
        public void CopyTexture(ITexture source, int srcX, int srcY, int srcZ, int width, int height, int numFaces, int x, int y, CubeMapFace startFace) =>
            CopyTexture(source, 0, srcX, srcY, srcZ, width, height, numFaces, 0, x, y, startFace);
        /// <summary>
        /// Copy data from <paramref name="source"/> to a single face.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="srcLevel">The mipmap level from source.</param>
        /// <param name="srcX">The source x.</param>
        /// <param name="srcY">The source y.</param>
        /// <param name="srcZ">The source z.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        /// <param name="level">The mipmap level.</param>
        /// <param name="x">The x offset to write to.</param>
        /// <param name="y">The y offset to write to.</param>
        /// <param name="face">The face to copy data to.</param>
        public void CopyTexture(ITexture source, int srcLevel, int srcX, int srcY, int srcZ, int width, int height, int level, int x, int y, CubeMapFace face) =>
            CopyTexture(source, srcLevel, srcX, srcY, srcZ, width, height, 1, level, x, y, face);
        /// <summary>
        /// Copy data from <paramref name="source"/> to a single face.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="srcX">The source x.</param>
        /// <param name="srcY">The source y.</param>
        /// <param name="srcZ">The source z.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        /// <param name="x">The x offset to write to.</param>
        /// <param name="y">The y offset to write to.</param>
        /// <param name="face">The face to copy data to.</param>
        public void CopyTexture(ITexture source, int srcX, int srcY, int srcZ, int width, int height, int x, int y, CubeMapFace face) =>
            CopyTexture(source, 0, srcX, srcY, srcZ, width, height, 1, 0, x, y, face);
        /// <summary>
        /// Copy all faces from <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="srcLevel">The mipmap level from source.</param>
        /// <param name="srcX">The source x.</param>
        /// <param name="srcY">The source y.</param>
        /// <param name="srcZ">The source z.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        /// <param name="level">The mipmap level.</param>
        /// <param name="x">The x offset to write to.</param>
        /// <param name="y">The y offset to write to.</param>
        public void CopyTexture(CubeMap source, int srcLevel, int srcX, int srcY, int width, int height, int level, int x, int y)
        {
            CopyImageSubData(source, srcLevel, srcX, srcY, 0, width, height, 6, level, x, y, 0);
        }
        /// <summary>
        /// Copy all faces from <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="srcX">The source x.</param>
        /// <param name="srcY">The source y.</param>
        /// <param name="srcZ">The source z.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        /// <param name="x">The x offset to write to.</param>
        /// <param name="y">The y offset to write to.</param>
        public void CopyTexture(CubeMap source, int srcX, int srcY, int width, int height, int x, int y) =>
            CopyTexture(source, 0, srcX, srcY, width, height, 0, x, y);

        /// <summary>
        /// Creates all levels for a mipmaped texture.
        /// </summary>
        public void CreateMipMap()
        {
            GenerateMipmap();
        }

        /// <summary>
        /// The internal storage resolution of the alpha component at base level.
        /// </summary>
        public int AlphaSize => Properties.AlphaSize;
        /// <summary>
        /// The data type used to store the alpha component at base level.
        /// </summary>
        public ChannelType AlphaChannel => Properties.AlphaChannel;
        /// <summary>
        /// The base texture mipmap level.
        /// </summary>
        public int BaseLevel
        {
            get => Properties._baseLevel;
            set => Properties.BaseLevel = value;
        }
        /// <summary>
        /// The internal storage resolution of the blue component at base level.
        /// </summary>
        public int BlueSize => Properties.BlueSize;
        /// <summary>
        /// The data type used to store the blue component at base level.
        /// </summary>
        public ChannelType BlueChannel => Properties.BlueChannel;
        /// <summary>
        /// The border colour of the texture.
        /// </summary>
        public ColourF BorderColour
        {
            get => Properties.BorderColour;
            set => Properties.BorderColour = value;
        }
        /// <summary>
        /// The comparison operator used when <see cref="ComparisonMode"/> is set to <see cref="ComparisonMode.CompareToDepth"/>.
        /// </summary>
        public ComparisonFunction ComparisonFunction
        {
            get => Properties.ComparisonFunction;
            set => Properties.ComparisonFunction = value;
        }
        /// <summary>
        /// The texture comparison mode for depth textures.
        /// </summary>
        public ComparisonMode ComparisonMode
        {
            get => Properties.ComparisonMode;
            set => Properties.ComparisonMode = value;
        }
        /// <summary>
        /// The internal storage resolution of the depth component at base level.
        /// </summary>
        public int DepthSize => Properties.DepthSize;
        /// <summary>
        /// The mode used to read from depth-stencil format textures.
        /// </summary>
        public DepthStencilMode DepthStencilMode
        {
            get => Properties.DepthStencilMode;
            set => Properties.DepthStencilMode = value;
        }
        /// <summary>
        /// The data type used to store the depth component at base level.
        /// </summary>
        public ChannelType DepthChannel => Properties.DepthChannel;
        /// <summary>
        /// The matching criteria use for the texture when used as an image texture.
        /// </summary>
        public FormatCompatibilityType FormatCompatibilityType => Properties.FormatCompatibilityType;
        /// <summary>
        /// The internal storage resolution of the green component at base level.
        /// </summary>
        public int GreenSize => Properties.GreenSize;
        /// <summary>
        /// The data type used to store the green component at base level.
        /// </summary>
        public ChannelType GreenChannel => Properties.GreenChannel;
        /// <summary>
        /// The height of the texture at base level.
        /// </summary>
        public int Height => Properties._height;
        /// <summary>
        /// A fixed bias that is to be added to the level-of-detail parameter before texture sampling.
        /// </summary>
        public double LodBias
        {
            get => Properties.LodBias;
            set => Properties.LodBias = value;
        }
        /// <summary>
        /// The texture magnification function used when the level-of-detail function determines that the texture should be magified.
        /// </summary>
        public TextureSampling MagFilter
        {
            get => Properties.MagFilter;
            set => Properties.MagFilter = value;
        }
        /// <summary>
        /// The maximum texture mipmap array level.
        /// </summary>
        public int MaxLevel
        {
            get => Properties.MaxLevel;
            set => Properties.MaxLevel = value;
        }
        /// <summary>
        /// The maximum value for the level-of-detail parameter.
        /// </summary>
        public double MaxLod
        {
            get => Properties.MaxLod;
            set => Properties.MaxLod = value;
        }
        /// <summary>
        /// The texture minification function used when the level-of-detail function determines that the texture should be minified.
        /// </summary>
        public TextureSampling MinFilter
        {
            get => Properties.MinFilter;
            set => Properties.MinFilter = value;
        }
        /// <summary>
        /// The minimum value for the level-of-detail parameter.
        /// </summary>
        public double MinLod
        {
            get => Properties.MinLod;
            set => Properties.MinLod = value;
        }
        /// <summary>
        /// The internal storage resolution of the red component at base level.
        /// </summary>
        public int RedSize => Properties.RedSize;
        /// <summary>
        /// The data type used to store the red component at base level.
        /// </summary>
        public ChannelType RedChannel => Properties.RedChannel;
        /// <summary>
        /// The swizzle that will be applied to the red component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle RedSwizzle
        {
            get => Properties.RedSwizzle;
            set => Properties.RedSwizzle = value;
        }
        /// <summary>
        /// The swizzle that will be applied to the green component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle GreenSwizzle
        {
            get => Properties.GreenSwizzle;
            set => Properties.GreenSwizzle = value;
        }
        /// <summary>
        /// The swizzle that will be applied to the blue component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle BlueSwizzle
        {
            get => Properties.BlueSwizzle;
            set => Properties.BlueSwizzle = value;
        }
        /// <summary>
        /// The swizzle that will be applied to the alpha component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle AlphaSwizzle
        {
            get => Properties.AlphaSwizzle;
            set => Properties.AlphaSwizzle = value;
        }
        /// <summary>
        /// The width of the texture at base level.
        /// </summary>
        public int Width => Properties._width;
        /// <summary>
        /// The wrapping function used on the x coordinate.
        /// </summary>
        public WrapStyle WrapX
        {
            get => Properties.WrapX;
            set => Properties.WrapX = value;
        }
        /// <summary>
        /// The wrapping function used on the y coordinate.
        /// </summary>
        public WrapStyle WrapY
        {
            get => Properties.WrapY;
            set => Properties.WrapY = value;
        }
        /// <summary>
        /// The wrapping function used on the z coordinate.
        /// </summary>
        public WrapStyle WrapZ
        {
            get => Properties.WrapZ;
            set => Properties.WrapZ = value;
        }
        /// <summary>
        /// The wrapping function used on all axes.
        /// </summary>
        public WrapStyle WrapStyle
        {
            set
            {
                Properties.WrapX = value;
                Properties.WrapY = value;
                Properties.WrapZ = value;
            }
        }

        public static CubeMap Create(GLArray<Colour> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            CubeMap texture = new CubeMap(TextureFormat.Rgba8, TextureData.Byte);

            if (data.Depth < 6)
            {
                throw new Exception($"{nameof(data)} must have a length of at least 6.");
            }

            int width = data.Width;
            int height = data.Height;

            texture.SetData(CubeMapFace.Right,  width, height, BaseFormat.Rgba, data.SubSection(0, 0, 0, width, height, 1));
            texture.SetData(CubeMapFace.Left,   width, height, BaseFormat.Rgba, data.SubSection(0, 0, 1, width, height, 1));
            texture.SetData(CubeMapFace.Top,    width, height, BaseFormat.Rgba, data.SubSection(0, 0, 2, width, height, 1));
            texture.SetData(CubeMapFace.Bottom, width, height, BaseFormat.Rgba, data.SubSection(0, 0, 3, width, height, 1));
            texture.SetData(CubeMapFace.Front,  width, height, BaseFormat.Rgba, data.SubSection(0, 0, 4, width, height, 1));
            texture.SetData(CubeMapFace.Back,   width, height, BaseFormat.Rgba, data.SubSection(0, 0, 5, width, height, 1));

            texture.WrapStyle = wrapStyle;
            texture.MinFilter = textureQuality;
            texture.MagFilter = textureQuality switch
            {
                TextureSampling.BlendMipMapBlend => TextureSampling.Blend,
                TextureSampling.BlendMipMapNearest => TextureSampling.Blend,
                TextureSampling.NearestMipMapBlend => TextureSampling.Nearest,
                TextureSampling.NearestMipMapNearest => TextureSampling.Nearest,
                _ => textureQuality
            };
            if (mipmap) { texture.CreateMipMap(); }

            return texture;
        }
        public static CubeMap Create(GLArray<ColourF> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            CubeMap texture = new CubeMap(TextureFormat.Rgba32f, TextureData.Float);

            if (data.Depth < 6)
            {
                throw new Exception($"{nameof(data)} must have a length of at least 6.");
            }

            int width = data.Width;
            int height = data.Height;

            texture.SetData(CubeMapFace.Right,  width, height, BaseFormat.Rgba, data.SubSection(0, 0, 0, width, height, 1));
            texture.SetData(CubeMapFace.Left,   width, height, BaseFormat.Rgba, data.SubSection(0, 0, 1, width, height, 1));
            texture.SetData(CubeMapFace.Top,    width, height, BaseFormat.Rgba, data.SubSection(0, 0, 2, width, height, 1));
            texture.SetData(CubeMapFace.Bottom, width, height, BaseFormat.Rgba, data.SubSection(0, 0, 3, width, height, 1));
            texture.SetData(CubeMapFace.Front,  width, height, BaseFormat.Rgba, data.SubSection(0, 0, 4, width, height, 1));
            texture.SetData(CubeMapFace.Back,   width, height, BaseFormat.Rgba, data.SubSection(0, 0, 5, width, height, 1));

            texture.WrapStyle = wrapStyle;
            texture.MinFilter = textureQuality;
            texture.MagFilter = textureQuality switch
            {
                TextureSampling.BlendMipMapBlend => TextureSampling.Blend,
                TextureSampling.BlendMipMapNearest => TextureSampling.Blend,
                TextureSampling.NearestMipMapBlend => TextureSampling.Nearest,
                TextureSampling.NearestMipMapNearest => TextureSampling.Nearest,
                _ => textureQuality
            };
            if (mipmap) { texture.CreateMipMap(); }

            return texture;
        }
        public static CubeMap Create(GLArray<Colour3> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            CubeMap texture = new CubeMap(TextureFormat.Rgb8, TextureData.Byte);

            if (data.Depth < 6)
            {
                throw new Exception($"{nameof(data)} must have a length of at least 6.");
            }

            int width = data.Width;
            int height = data.Height;

            texture.SetData(CubeMapFace.Right,  width, height, BaseFormat.Rgba, data.SubSection(0, 0, 0, width, height, 1));
            texture.SetData(CubeMapFace.Left,   width, height, BaseFormat.Rgba, data.SubSection(0, 0, 1, width, height, 1));
            texture.SetData(CubeMapFace.Top,    width, height, BaseFormat.Rgba, data.SubSection(0, 0, 2, width, height, 1));
            texture.SetData(CubeMapFace.Bottom, width, height, BaseFormat.Rgba, data.SubSection(0, 0, 3, width, height, 1));
            texture.SetData(CubeMapFace.Front,  width, height, BaseFormat.Rgba, data.SubSection(0, 0, 4, width, height, 1));
            texture.SetData(CubeMapFace.Back,   width, height, BaseFormat.Rgba, data.SubSection(0, 0, 5, width, height, 1));

            texture.WrapStyle = wrapStyle;
            texture.MinFilter = textureQuality;
            texture.MagFilter = textureQuality switch
            {
                TextureSampling.BlendMipMapBlend => TextureSampling.Blend,
                TextureSampling.BlendMipMapNearest => TextureSampling.Blend,
                TextureSampling.NearestMipMapBlend => TextureSampling.Nearest,
                TextureSampling.NearestMipMapNearest => TextureSampling.Nearest,
                _ => textureQuality
            };
            if (mipmap) { texture.CreateMipMap(); }

            return texture;
        }
        public static CubeMap Create(GLArray<ColourF3> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            CubeMap texture = new CubeMap(TextureFormat.Rgb32f, TextureData.Float);

            if (data.Depth < 6)
            {
                throw new Exception($"{nameof(data)} must have a length of at least 6.");
            }

            int width = data.Width;
            int height = data.Height;

            texture.SetData(CubeMapFace.Right,  width, height, BaseFormat.Rgba, data.SubSection(0, 0, 0, width, height, 1));
            texture.SetData(CubeMapFace.Left,   width, height, BaseFormat.Rgba, data.SubSection(0, 0, 1, width, height, 1));
            texture.SetData(CubeMapFace.Top,    width, height, BaseFormat.Rgba, data.SubSection(0, 0, 2, width, height, 1));
            texture.SetData(CubeMapFace.Bottom, width, height, BaseFormat.Rgba, data.SubSection(0, 0, 3, width, height, 1));
            texture.SetData(CubeMapFace.Front,  width, height, BaseFormat.Rgba, data.SubSection(0, 0, 4, width, height, 1));
            texture.SetData(CubeMapFace.Back,   width, height, BaseFormat.Rgba, data.SubSection(0, 0, 5, width, height, 1));

            texture.WrapStyle = wrapStyle;
            texture.MinFilter = textureQuality;
            texture.MagFilter = textureQuality switch
            {
                TextureSampling.BlendMipMapBlend => TextureSampling.Blend,
                TextureSampling.BlendMipMapNearest => TextureSampling.Blend,
                TextureSampling.NearestMipMapBlend => TextureSampling.Nearest,
                TextureSampling.NearestMipMapNearest => TextureSampling.Nearest,
                _ => textureQuality
            };
            if (mipmap) { texture.CreateMipMap(); }

            return texture;
        }

        public static CubeMap Create(Bitmap[] images, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            CubeMap texture = new CubeMap(TextureFormat.Rgba8, TextureData.Byte);

            if (images.Length < 6)
            {
                throw new Exception($"{nameof(images)} must have a length of at least 6.");
            }

            int width = images[0].Width;
            int height = images[0].Height;

            texture.SetData(CubeMapFace.Right,  width, height, BaseFormat.Rgba, images[0]);
            texture.SetData(CubeMapFace.Left,   width, height, BaseFormat.Rgba, images[1]);
            texture.SetData(CubeMapFace.Top,    width, height, BaseFormat.Rgba, images[2]);
            texture.SetData(CubeMapFace.Bottom, width, height, BaseFormat.Rgba, images[3]);
            texture.SetData(CubeMapFace.Front,  width, height, BaseFormat.Rgba, images[4]);
            texture.SetData(CubeMapFace.Back,   width, height, BaseFormat.Rgba, images[5]);

            texture.WrapStyle = wrapStyle;
            texture.MinFilter = textureQuality;
            texture.MagFilter = textureQuality switch
            {
                TextureSampling.BlendMipMapBlend => TextureSampling.Blend,
                TextureSampling.BlendMipMapNearest => TextureSampling.Blend,
                TextureSampling.NearestMipMapBlend => TextureSampling.Nearest,
                TextureSampling.NearestMipMapNearest => TextureSampling.Nearest,
                _ => textureQuality
            };
            if (mipmap) { texture.CreateMipMap(); }

            return texture;
        }

        private static readonly ConcurrentDictionary<KeyValuePair<CubeMap, CubeMapFace>, KeyValuePair<GLArray<Colour>, bool>> _loadingTextures =
            new ConcurrentDictionary<KeyValuePair<CubeMap, CubeMapFace>, KeyValuePair<GLArray<Colour>, bool>>();
        private static readonly List<KeyValuePair<CubeMap, CubeMapFace>> _textures = new List<KeyValuePair<CubeMap, CubeMapFace>>();
        public static CubeMap LoadSync(string path, RectangleI[] sections, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            CubeMap texture = new CubeMap(TextureFormat.Rgba8, TextureData.Byte);


            KeyValuePair<GLArray<Colour>, bool> defaultPair = new KeyValuePair<GLArray<Colour>, bool>(null, mipmap);
            bool added = false;

            // Right face
            KeyValuePair<CubeMap, CubeMapFace> keyR = new KeyValuePair<CubeMap, CubeMapFace>(texture, CubeMapFace.Right);
            added = _loadingTextures.TryAdd(keyR, defaultPair);
            _textures.Add(keyR);
            if (!added)
            {
                throw new Exception("Couldn't add texture temperarerly to collection.");
            }

            // Left face
            KeyValuePair<CubeMap, CubeMapFace> keyL = new KeyValuePair<CubeMap, CubeMapFace>(texture, CubeMapFace.Left);
            added = _loadingTextures.TryAdd(keyL, defaultPair);
            _textures.Add(keyL);
            if (!added)
            {
                throw new Exception("Couldn't add texture temperarerly to collection.");
            }

            // Top face
            KeyValuePair<CubeMap, CubeMapFace> keyT = new KeyValuePair<CubeMap, CubeMapFace>(texture, CubeMapFace.Top);
            added = _loadingTextures.TryAdd(keyT, defaultPair);
            _textures.Add(keyT);
            if (!added)
            {
                throw new Exception("Couldn't add texture temperarerly to collection.");
            }

            // Bottom face
            KeyValuePair<CubeMap, CubeMapFace> keyBo = new KeyValuePair<CubeMap, CubeMapFace>(texture, CubeMapFace.Bottom);
            added = _loadingTextures.TryAdd(keyBo, defaultPair);
            _textures.Add(keyBo);
            if (!added)
            {
                throw new Exception("Couldn't add texture temperarerly to collection.");
            }

            // Front face
            KeyValuePair<CubeMap, CubeMapFace> keyF = new KeyValuePair<CubeMap, CubeMapFace>(texture, CubeMapFace.Front);
            added = _loadingTextures.TryAdd(keyF, defaultPair);
            _textures.Add(keyF);
            if (!added)
            {
                throw new Exception("Couldn't add texture temperarerly to collection.");
            }

            // Back face
            KeyValuePair<CubeMap, CubeMapFace> keyBa = new KeyValuePair<CubeMap, CubeMapFace>(texture, CubeMapFace.Back);
            added = _loadingTextures.TryAdd(keyBa, defaultPair);
            _textures.Add(keyBa);
            if (!added)
            {
                throw new Exception("Couldn't add texture temperarerly to collection.");
            }

            byte[] data = File.ReadAllBytes(path);
            
            Task.Run(() =>
            {
                // Load image
                ImageResult imageData = ImageResult.FromMemory(data, ColorComponents.RedGreenBlueAlpha);

                Bitmap bitmap = Bitmap.FromArray(imageData.Width, imageData.Height, imageData.Data);

                KeyValuePair<GLArray<Colour>, bool> value;

                // Right face
                value = new KeyValuePair<GLArray<Colour>, bool>(
                    bitmap.SubSection(sections[0]), mipmap);

                while (!_loadingTextures.TryUpdate(keyR, value, defaultPair))
                {
                    // Content in condition statment
                }
                
                // Left face
                value = new KeyValuePair<GLArray<Colour>, bool>(
                    bitmap.SubSection(sections[1]), mipmap);

                while (!_loadingTextures.TryUpdate(keyL, value, defaultPair))
                {
                    // Content in condition statment
                }
                
                // Top face
                value = new KeyValuePair<GLArray<Colour>, bool>(
                    bitmap.SubSection(sections[2]), mipmap);

                while (!_loadingTextures.TryUpdate(keyT, value, defaultPair))
                {
                    // Content in condition statment
                }

                // Bottom face
                value = new KeyValuePair<GLArray<Colour>, bool>(
                    bitmap.SubSection(sections[3]), mipmap);

                while (!_loadingTextures.TryUpdate(keyBo, value, defaultPair))
                {
                    // Content in condition statment
                }

                // Font face
                value = new KeyValuePair<GLArray<Colour>, bool>(
                    bitmap.SubSection(sections[4]), mipmap);

                while (!_loadingTextures.TryUpdate(keyF, value, defaultPair))
                {
                    // Content in condition statment
                }

                // Back face
                value = new KeyValuePair<GLArray<Colour>, bool>(
                    bitmap.SubSection(sections[5]), mipmap);

                while (!_loadingTextures.TryUpdate(keyBa, value, defaultPair))
                {
                    // Content in condition statment
                }
            });

            // Temperarerly set data to null
            texture.SetData(CubeMapFace.Right,  sections[0].Width, sections[0].Width, BaseFormat.Rgba, GLArray<byte>.Empty);
            texture.SetData(CubeMapFace.Left,   sections[0].Width, sections[0].Width, BaseFormat.Rgba, GLArray<byte>.Empty);
            texture.SetData(CubeMapFace.Top,    sections[0].Width, sections[0].Width, BaseFormat.Rgba, GLArray<byte>.Empty);
            texture.SetData(CubeMapFace.Bottom, sections[0].Width, sections[0].Width, BaseFormat.Rgba, GLArray<byte>.Empty);
            texture.SetData(CubeMapFace.Front,  sections[0].Width, sections[0].Width, BaseFormat.Rgba, GLArray<byte>.Empty);
            texture.SetData(CubeMapFace.Back,   sections[0].Width, sections[0].Width, BaseFormat.Rgba, GLArray<byte>.Empty);

            texture.WrapStyle = wrapStyle;
            texture.MinFilter = textureQuality;
            texture.MagFilter = textureQuality switch
            {
                TextureSampling.BlendMipMapBlend => TextureSampling.Blend,
                TextureSampling.BlendMipMapNearest => TextureSampling.Blend,
                TextureSampling.NearestMipMapBlend => TextureSampling.Nearest,
                TextureSampling.NearestMipMapNearest => TextureSampling.Nearest,
                _ => textureQuality
            };

            return texture;
        }

        public static CubeMap LoadSync(string[] paths, int size, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            CubeMap texture = new CubeMap(TextureFormat.Rgba8, TextureData.Byte);

            if (paths.Length < 6)
            {
                throw new Exception($"{nameof(paths)} must have a length of at least 6.");
            }

            KeyValuePair<GLArray<Colour>, bool> defaultPair = new KeyValuePair<GLArray<Colour>, bool>(null, mipmap);
            bool added = false;

            // Right face
            KeyValuePair<CubeMap, CubeMapFace> keyR = new KeyValuePair<CubeMap, CubeMapFace>(texture, CubeMapFace.Right);
            added = _loadingTextures.TryAdd(keyR, defaultPair);
            _textures.Add(keyR);
            if (!added)
            {
                throw new Exception("Couldn't add texture temperarerly to collection.");
            }

            // Left face
            KeyValuePair<CubeMap, CubeMapFace> keyL = new KeyValuePair<CubeMap, CubeMapFace>(texture, CubeMapFace.Left);
            added = _loadingTextures.TryAdd(keyL, defaultPair);
            _textures.Add(keyL);
            if (!added)
            {
                throw new Exception("Couldn't add texture temperarerly to collection.");
            }

            // Top face
            KeyValuePair<CubeMap, CubeMapFace> keyT = new KeyValuePair<CubeMap, CubeMapFace>(texture, CubeMapFace.Top);
            added = _loadingTextures.TryAdd(keyT, defaultPair);
            _textures.Add(keyT);
            if (!added)
            {
                throw new Exception("Couldn't add texture temperarerly to collection.");
            }

            // Bottom face
            KeyValuePair<CubeMap, CubeMapFace> keyBo = new KeyValuePair<CubeMap, CubeMapFace>(texture, CubeMapFace.Bottom);
            added = _loadingTextures.TryAdd(keyBo, defaultPair);
            _textures.Add(keyBo);
            if (!added)
            {
                throw new Exception("Couldn't add texture temperarerly to collection.");
            }

            // Front face
            KeyValuePair<CubeMap, CubeMapFace> keyF = new KeyValuePair<CubeMap, CubeMapFace>(texture, CubeMapFace.Front);
            added = _loadingTextures.TryAdd(keyF, defaultPair);
            _textures.Add(keyF);
            if (!added)
            {
                throw new Exception("Couldn't add texture temperarerly to collection.");
            }

            // Back face
            KeyValuePair<CubeMap, CubeMapFace> keyBa = new KeyValuePair<CubeMap, CubeMapFace>(texture, CubeMapFace.Back);
            added = _loadingTextures.TryAdd(keyBa, defaultPair);
            _textures.Add(keyBa);
            if (!added)
            {
                throw new Exception("Couldn't add texture temperarerly to collection.");
            }

            byte[] dataR = File.ReadAllBytes(paths[0]);
            byte[] dataL = File.ReadAllBytes(paths[1]);
            byte[] dataT = File.ReadAllBytes(paths[2]);
            byte[] dataBo = File.ReadAllBytes(paths[3]);
            byte[] dataF = File.ReadAllBytes(paths[4]);
            byte[] dataBa = File.ReadAllBytes(paths[5]);

            Task.Run(() =>
            {
                KeyValuePair<GLArray<Colour>, bool> value;

                // Right face
                value = new KeyValuePair<GLArray<Colour>, bool>(
                    new Bitmap(new MemoryStream(dataR)), mipmap);

                while (!_loadingTextures.TryUpdate(keyR, value, defaultPair))
                {
                    // Content in condition statment
                }

                // Left face
                value = new KeyValuePair<GLArray<Colour>, bool>(
                    new Bitmap(new MemoryStream(dataL)), mipmap);

                while (!_loadingTextures.TryUpdate(keyL, value, defaultPair))
                {
                    // Content in condition statment
                }

                // Top face
                value = new KeyValuePair<GLArray<Colour>, bool>(
                    new Bitmap(new MemoryStream(dataT)), mipmap);

                while (!_loadingTextures.TryUpdate(keyT, value, defaultPair))
                {
                    // Content in condition statment
                }

                // Bottom face
                value = new KeyValuePair<GLArray<Colour>, bool>(
                    new Bitmap(new MemoryStream(dataBo)), mipmap);

                while (!_loadingTextures.TryUpdate(keyBo, value, defaultPair))
                {
                    // Content in condition statment
                }

                // Font face
                value = new KeyValuePair<GLArray<Colour>, bool>(
                    new Bitmap(new MemoryStream(dataF)), mipmap);

                while (!_loadingTextures.TryUpdate(keyF, value, defaultPair))
                {
                    // Content in condition statment
                }

                // Back face
                value = new KeyValuePair<GLArray<Colour>, bool>(
                    new Bitmap(new MemoryStream(dataBa)), mipmap);

                while (!_loadingTextures.TryUpdate(keyBa, value, defaultPair))
                {
                    // Content in condition statment
                }
            });

            // Temperarerly set data to null
            texture.SetData(CubeMapFace.Right,  size, size, BaseFormat.Rgba, GLArray<byte>.Empty);
            texture.SetData(CubeMapFace.Left,   size, size, BaseFormat.Rgba, GLArray<byte>.Empty);
            texture.SetData(CubeMapFace.Top,    size, size, BaseFormat.Rgba, GLArray<byte>.Empty);
            texture.SetData(CubeMapFace.Bottom, size, size, BaseFormat.Rgba, GLArray<byte>.Empty);
            texture.SetData(CubeMapFace.Front,  size, size, BaseFormat.Rgba, GLArray<byte>.Empty);
            texture.SetData(CubeMapFace.Back,   size, size, BaseFormat.Rgba, GLArray<byte>.Empty);

            texture.WrapStyle = wrapStyle;
            texture.MinFilter = textureQuality;
            texture.MagFilter = textureQuality switch
            {
                TextureSampling.BlendMipMapBlend => TextureSampling.Blend,
                TextureSampling.BlendMipMapNearest => TextureSampling.Blend,
                TextureSampling.NearestMipMapBlend => TextureSampling.Nearest,
                TextureSampling.NearestMipMapNearest => TextureSampling.Nearest,
                _ => textureQuality
            };

            return texture;
        }

        public static bool CheckSyncLoading()
        {
            foreach (KeyValuePair<KeyValuePair<CubeMap, CubeMapFace>, KeyValuePair<GLArray<Colour>, bool>> face in _loadingTextures)
            {
                if (face.Value.Key == null) { continue; }

                face.Key.Key.SetData(face.Key.Value, face.Value.Key.Width, face.Value.Key.Height, BaseFormat.Rgba, face.Value.Key);

                if (face.Value.Value) { face.Key.Key.CreateMipMap(); }
            }

            foreach (KeyValuePair<CubeMap, CubeMapFace> key in _textures)
            {
                if (_loadingTextures.TryGetValue(key, out KeyValuePair<GLArray<Colour>, bool> value))
                {
                    if (value.Key == null) { continue; }

                    while (!_loadingTextures.TryRemove(key, out _))
                    {
                        // Content in condition statment
                    }
                }
            }

            return _loadingTextures.IsEmpty;
        }
    }
}
