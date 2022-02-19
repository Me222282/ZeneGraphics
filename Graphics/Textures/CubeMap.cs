using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that manages a cube map texture object.
    /// </summary>
    public class CubeMap : ITexture
    {
        /// <summary>
        /// Creates the cube map texture with a set internal format.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        /// <param name="dataType">The type of data that is going to be passed to OpenGL.</param>
        public CubeMap(TextureFormat format, TextureData dataType)
        {
            _texture = new TextureGL(TextureTarget.CubeMap);

            InternalFormat = format;
            DataType = dataType;
        }
        internal CubeMap(uint id, TextureFormat format, TextureData dataType)
        {
            _texture = new TextureGL(id, TextureTarget.CubeMap, format);
            InternalFormat = format;
            DataType = dataType;
        }

        public uint Id => _texture.Id;
        public uint ReferanceSlot => _texture.ReferanceSlot;

        private readonly TextureGL _texture;

        public TextureTarget Target => TextureTarget.CubeMap;
        public TextureFormat InternalFormat { get; }
        protected TextureProperties Properties => _texture.Properties;
        TextureProperties ITexture.Properties => _texture.Properties;

        /// <summary>
        /// The type of data being inputed into the texture.
        /// </summary>
        public TextureData DataType { get; set; }
        /// <summary>
        /// The cube map face that is targeted for some texture parameters.
        /// </summary>
        public CubeMapFace ReferanceFace { get; set; }

        public void Bind(uint slot) => _texture.Bind(slot);
        public void Bind() => _texture.Bind();
        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            _texture.Dispose();

            _disposed = true;
            GC.SuppressFinalize(this);
        }
        public void Unbind() => _texture.Unbind();

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
            _texture.TexImage2D(face, level, InternalFormat, width, height, inputFormat, DataType, data);
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
            _texture.TexSubImage2D(face, level, x, y, width, height, inputFormat, DataType, data);
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
            _texture.TexStorage2D(face, levels, InternalFormat, width, height);
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
            return _texture.GetTexImage<T>(face, level, outputFormat, DataType);
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
            return _texture.GetTextureSubImage<T>(level, 0, 0, 0, Width, Height, 6, outputFormat, DataType);
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

            return _texture.GetTextureSubImage<T>(level, x, y, zFace, width, height, 1, outputFormat, DataType);
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

            _texture.CopyImageSubData(source, srcLevel, srcX, srcY, srcZ, width, height, numFaces, level, x, y, zFace);
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
            _texture.CopyImageSubData(source, srcLevel, srcX, srcY, 0, width, height, 6, level, x, y, 0);
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
            _texture.GenerateMipmap();
        }

        private int _baseLevel = 0;
        /// <summary>
        /// The base texture mipmap level.
        /// </summary>
        public int BaseLevel
        {
            get
            {
                return _texture.GetBaseLevel();
            }
            set
            {
                _baseLevel = value;
                _texture.SetBaseLevel(_baseLevel);
            }
        }
        /// <summary>
        /// The border colour of the texture.
        /// </summary>
        public ColourF BorderColour
        {
            get
            {
                return _texture.GetBorderColour();
            }
            set
            {
                _texture.SetBorderColour(value);
            }
        }
        /// <summary>
        /// The comparison operator used when <see cref="ComparisonMode"/> is set to <see cref="ComparisonMode.CompareToDepth"/>.
        /// </summary>
        public ComparisonFunction ComparisonFunction
        {
            get
            {
                return _texture.GetComparisonFunction();
            }
            set
            {
                _texture.SetComparisonFunction(value);
            }
        }
        /// <summary>
        /// The texture comparison mode for depth textures.
        /// </summary>
        public ComparisonMode ComparisonMode
        {
            get
            {
                return _texture.GetComparisonMode();
            }
            set
            {
                _texture.SetComparisonMode(value);
            }
        }
        /// <summary>
        /// The mode used to read from depth-stencil format textures.
        /// </summary>
        public DepthStencilMode DepthStencilMode
        {
            get
            {
                return _texture.GetDepthStencilMode();
            }
            set
            {
                _texture.SetDepthStencilMode(value);
            }
        }
        /// <summary>
        /// The matching criteria use for the texture when used as an image texture.
        /// </summary>
        public FormatCompatibilityType FormatCompatibilityType
        {
            get
            {
                return _texture.GetFormatCompatibilityType();
            }
        }
        /// <summary>
        /// A fixed bias that is to be added to the level-of-detail parameter before texture sampling.
        /// </summary>
        public double LodBias
        {
            get
            {
                return _texture.GetLodBias();
            }
            set
            {
                _texture.SetLodBias((float)value);
            }
        }
        /// <summary>
        /// The texture magnification function used when the level-of-detail function determines that the texture should be magified.
        /// </summary>
        public TextureSampling MagFilter
        {
            get
            {
                return _texture.GetMagFilter();
            }
            set
            {
                _texture.SetMagFilter(value);
            }
        }
        /// <summary>
        /// The maximum texture mipmap array level.
        /// </summary>
        public int MaxLevel
        {
            get
            {
                return _texture.GetMaxLevel();
            }
            set
            {
                _texture.SetMaxLevel(value);
            }
        }
        /// <summary>
        /// The maximum value for the level-of-detail parameter.
        /// </summary>
        public double MaxLod
        {
            get
            {
                return _texture.GetMaxLod();
            }
            set
            {
                _texture.SetMaxLod((float)value);
            }
        }
        /// <summary>
        /// The texture minification function used when the level-of-detail function determines that the texture should be minified.
        /// </summary>
        public TextureSampling MinFilter
        {
            get
            {
                return _texture.GetMinFilter();
            }
            set
            {
                _texture.SetMinFilter(value);
            }
        }
        /// <summary>
        /// The minimum value for the level-of-detail parameter.
        /// </summary>
        public double MinLod
        {
            get
            {
                return _texture.GetMinLod();
            }
            set
            {
                _texture.SetMinLod((float)value);
            }
        }
        /// <summary>
        /// The swizzle that will be applied to the red component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle RedSwizzle
        {
            get
            {
                return _texture.GetSwizzleRed();
            }
            set
            {
                _texture.SetSwizzleRed(value);
            }
        }
        /// <summary>
        /// The swizzle that will be applied to the green component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle GreenSwizzle
        {
            get
            {
                return _texture.GetSwizzleGreen();
            }
            set
            {
                _texture.SetSwizzleGreen(value);
            }
        }
        /// <summary>
        /// The swizzle that will be applied to the blue component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle BlueSwizzle
        {
            get
            {
                return _texture.GetSwizzleBlue();
            }
            set
            {
                _texture.SetSwizzleBlue(value);
            }
        }
        /// <summary>
        /// The swizzle that will be applied to the alpha component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle AlphaSwizzle
        {
            get
            {
                return _texture.GetSwizzleAlpha();
            }
            set
            {
                _texture.SetSwizzleAlpha(value);
            }
        }
        /// <summary>
        /// The wrapping function used on the x coordinate.
        /// </summary>
        public WrapStyle WrapX
        {
            get
            {
                return _texture.GetWrapS();
            }
            set
            {
                _texture.SetWrapS(value);
            }
        }
        /// <summary>
        /// The wrapping function used on the y coordinate.
        /// </summary>
        public WrapStyle WrapY
        {
            get
            {
                return _texture.GetWrapT();
            }
            set
            {
                _texture.SetWrapT(value);
            }
        }
        /// <summary>
        /// The wrapping function used on the z coordinate.
        /// </summary>
        public WrapStyle WrapZ
        {
            get
            {
                return _texture.GetWrapR();
            }
            set
            {
                _texture.SetWrapR(value);
            }
        }
        /// <summary>
        /// The wrapping function used on all axes.
        /// </summary>
        public WrapStyle WrapStyle
        {
            set
            {
                WrapX = value;
                WrapY = value;
                WrapZ = value;
            }
        }

        /// <summary>
        /// The internal storage resolution of the alpha component of <see cref="ReferanceFace"/> at base level.
        /// </summary>
        public int AlphaSize
        {
            get
            {
                return GL.GetTexLevelParameteriv((uint)ReferanceFace, _baseLevel, GLEnum.TextureAlphaSize);
            }
        }
        /// <summary>
        /// The data type used to store the alpha component of <see cref="ReferanceFace"/> at base level.
        /// </summary>
        public ChannelType AlphaChannel
        {
            get
            {
                return (ChannelType)GL.GetTexLevelParameteriv((uint)ReferanceFace, _baseLevel, GLEnum.TextureAlphaType);
            }
        }
        /// <summary>
        /// The internal storage resolution of the blue component of <see cref="ReferanceFace"/> at base level.
        /// </summary>
        public int BlueSize
        {
            get
            {
                return GL.GetTexLevelParameteriv((uint)ReferanceFace, _baseLevel, GLEnum.TextureBlueSize);
            }
        }
        /// <summary>
        /// The data type used to store the blue component of <see cref="ReferanceFace"/> at base level.
        /// </summary>
        public ChannelType BlueChannel
        {
            get
            {
                return (ChannelType)GL.GetTexLevelParameteriv((uint)ReferanceFace, _baseLevel, GLEnum.TextureBlueType);
            }
        }
        /// <summary>
        /// The internal storage resolution of the depth component of <see cref="ReferanceFace"/> at base level.
        /// </summary>
        public int DepthSize
        {
            get
            {
                return GL.GetTexLevelParameteriv((uint)ReferanceFace, _baseLevel, GLEnum.TextureDepthSize);
            }
        }
        /// <summary>
        /// The data type used to store the depth component of <see cref="ReferanceFace"/> at base level.
        /// </summary>
        public ChannelType DepthChannel
        {
            get
            {
                return (ChannelType)GL.GetTexLevelParameteriv((uint)ReferanceFace, _baseLevel, GLEnum.TextureDepthType);
            }
        }
        /// <summary>
        /// The internal storage resolution of the green component of <see cref="ReferanceFace"/> at base level.
        /// </summary>
        public int GreenSize
        {
            get
            {
                return GL.GetTexLevelParameteriv((uint)ReferanceFace, _baseLevel, GLEnum.TextureGreenSize);
            }
        }
        /// <summary>
        /// The data type used to store the green component of <see cref="ReferanceFace"/> at base level.
        /// </summary>
        public ChannelType GreenChannel
        {
            get
            {
                return (ChannelType)GL.GetTexLevelParameteriv((uint)ReferanceFace, _baseLevel, GLEnum.TextureGreenType);
            }
        }
        /// <summary>
        /// The height of <see cref="ReferanceFace"/> at base level.
        /// </summary>
        public int Height
        {
            get
            {
                return GL.GetTexLevelParameteriv((uint)ReferanceFace, _baseLevel, GLEnum.TextureHeight);
            }
        }
        /// <summary>
        /// The internal storage resolution of the red component of <see cref="ReferanceFace"/> at base level.
        /// </summary>
        public int RedSize
        {
            get
            {
                return GL.GetTexLevelParameteriv((uint)ReferanceFace, _baseLevel, GLEnum.TextureRedSize);
            }
        }
        /// <summary>
        /// The data type used to store the red component of <see cref="ReferanceFace"/> at base level.
        /// </summary>
        public ChannelType RedChannel
        {
            get
            {
                return (ChannelType)GL.GetTexLevelParameteriv((uint)ReferanceFace, _baseLevel, GLEnum.TextureRedType);
            }
        }
        /// <summary>
        /// The width of <see cref="ReferanceFace"/> at base level.
        /// </summary>
        public int Width
        {
            get
            {
                return GL.GetTexLevelParameteriv((uint)ReferanceFace, _baseLevel, GLEnum.TextureWidth);
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

            texture.SetData(CubeMapFace.Right,  width, height, BaseFormat.Rgba, (GLArray<Colour>)images[0]);
            texture.SetData(CubeMapFace.Left,   width, height, BaseFormat.Rgba, (GLArray<Colour>)images[1]);
            texture.SetData(CubeMapFace.Top,    width, height, BaseFormat.Rgba, (GLArray<Colour>)images[2]);
            texture.SetData(CubeMapFace.Bottom, width, height, BaseFormat.Rgba, (GLArray<Colour>)images[3]);
            texture.SetData(CubeMapFace.Front,  width, height, BaseFormat.Rgba, (GLArray<Colour>)images[4]);
            texture.SetData(CubeMapFace.Back,   width, height, BaseFormat.Rgba, (GLArray<Colour>)images[5]);

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
    }
}
