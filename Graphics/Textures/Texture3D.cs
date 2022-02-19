using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that manages a 3 dimensional texture.
    /// </summary>
    public class Texture3D : ITexture
    {
        /// <summary>
        /// Creates a 3 dimensional texture with a set internal format.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        /// <param name="dataType">The type of data that is going to be passed to OpenGL.</param>
        public Texture3D(TextureFormat format, TextureData dataType)
        {
            _texture = new TextureGL(TextureTarget.Texture3D);

            InternalFormat = format;
            DataType = dataType;
        }
        internal Texture3D(uint id, TextureFormat format, TextureData dataType)
        {
            _texture = new TextureGL(id, TextureTarget.Texture3D, format);
            InternalFormat = format;
            DataType = dataType;
        }

        public uint Id => _texture.Id;
        public uint ReferanceSlot => _texture.ReferanceSlot;

        private readonly TextureGL _texture;

        public TextureTarget Target => TextureTarget.Texture3D;
        public TextureFormat InternalFormat { get; }
        protected TextureProperties Properties => _texture.Properties;
        TextureProperties ITexture.Properties => _texture.Properties;

        /// <summary>
        /// The type of data being inputed into the texture.
        /// </summary>
        public TextureData DataType { get; set; }

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
        /// Creats and filles the data inside the texture with <paramref name="data"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">THe mipmap level.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <param name="inputFormat">The format of <paramref name="data"/>.</param>
        /// <param name="data">The data to set the texture with.</param>
        public void SetData<T>(int level, int width, int height, int depth, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged
        {
            
            _texture.TexImage3D(level, InternalFormat, width, height, depth, inputFormat, DataType, data);
        }
        /// <summary>
        /// Creats and filles the data inside the texture with <paramref name="data"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="depth">The depth of the texture.</param>
        /// <param name="inputFormat">The format of <paramref name="data"/>.</param>
        /// <param name="data">The data to set the texture with.</param>
        public void SetData<T>(int width, int height, int depth, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged =>
            SetData(0, width, height, depth, inputFormat, data);

        /// <summary>
        /// Changes the data in a section of the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="x">The x location of the region.</param>
        /// <param name="y">The y location of the region.</param>
        /// <param name="z">The z location of the region.</param>
        /// <param name="width">The width of the region.</param>
        /// <param name="height">The height of the region.</param>
        /// <param name="depth">The depth of the region.</param>
        /// <param name="inputFormat">The format of <paramref name="data"/>.</param>
        /// <param name="data">The data to set the region with.</param>
        public void EditData<T>(int level, int x, int y, int z, int width, int height, int depth, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged
        {
            
            _texture.TexSubImage3D(level, x, y, z, width, height, depth, inputFormat, DataType, data);
        }
        /// <summary>
        /// Changes the data in a section of the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">The x location of the region.</param>
        /// <param name="y">The y location of the region.</param>
        /// <param name="z">The z location of the region.</param>
        /// <param name="width">The width of the region.</param>
        /// <param name="height">The height of the region.</param>
        /// <param name="depth">The depth of the region.</param>
        /// <param name="inputFormat">The format of <paramref name="data"/>.</param>
        /// <param name="data">The data to set the region with.</param>
        public void EditData<T>(int x, int y, int z, int width, int height, int depth, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged =>
            EditData(0, x, y, z, width, height, depth, inputFormat, data);

        /// <summary>
        /// Creates the storage space for a texture.
        /// </summary>
        /// <param name="levels">The number of mipmap levels.</param>
        /// <param name="width">The width of the space.</param>
        /// <param name="height">The height of the space.</param>
        /// <param name="depth">The depth of the space.</param>
        public void CreateStorage(int levels, int width, int height, int depth)
        {
            
            _texture.TexStorage3D(levels, InternalFormat, width, height, depth);
        }
        /// <summary>
        /// Creates the storage space for a texture.
        /// </summary>
        /// <param name="levels">The number of mipmap levels.</param>
        /// <param name="width">The width of the space.</param>
        /// <param name="height">The height of the space.</param>
        /// <param name="depth">The depth of the space.</param>
        public void CreateStorage(int width, int height, int depth) => CreateStorage(1, width, height, depth);

        /// <summary>
        /// Gets the data stored in the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="outputFormat">The format of the returned data.</param>
        /// <returns></returns>
        public GLArray<T> GetData<T>(int level, BaseFormat outputFormat) where T : unmanaged
        {
            
            return _texture.GetTexImage<T>(level, outputFormat, DataType);
        }
        /// <summary>
        /// Get a section of the data stored in the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="x">The x location of the region.</param>
        /// <param name="y">The y location of the region.</param>
        /// <param name="z">The z location of the region.</param>
        /// <param name="width">The width of the region.</param>
        /// <param name="height">The height of the region.</param>
        /// <param name="depth">The depth of the region.</param>
        /// <param name="outputFormat">The format of the returned data.</param>
        /// <returns></returns>
        public GLArray<T> GetDataSection<T>(int level, int x, int y, int z, int width, int height, int depth, BaseFormat outputFormat) where T : unmanaged
        {
            return _texture.GetTextureSubImage<T>(level, x, y, z, width, height, depth, outputFormat, DataType);
        }
        /// <summary>
        /// Gets the data stored in the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="outputFormat">The format of the returned data.</param>
        /// <returns></returns>
        public GLArray<T> GetData<T>(BaseFormat outputFormat) where T : unmanaged => GetData<T>(0, outputFormat);
        /// <summary>
        /// Get a section of the data stored in the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">The x location of the region.</param>
        /// <param name="y">The y location of the region.</param>
        /// <param name="z">The z location of the region.</param>
        /// <param name="width">The width of the region.</param>
        /// <param name="height">The height of the region.</param>
        /// <param name="depth">The depth of the region.</param>
        /// <param name="outputFormat">The format of the returned data.</param>
        /// <returns></returns>
        public GLArray<T> GetDataSection<T>(int x, int y, int z, int width, int height, int depth, BaseFormat outputFormat) where T : unmanaged =>
            GetDataSection<T>(0, x, y, z, width, height, depth, outputFormat);

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
        /// <param name="depth">The depth in pixels.</param>
        /// <param name="level">The mipmap level to write to.</param>
        /// <param name="x">The x offset to write to.</param>
        /// <param name="y">The y offset to write to.</param>
        /// <param name="z">The z offset to write to.</param>
        public void CopyTexture(ITexture source, int srcLevel, int srcX, int srcY, int srcZ, int width, int height, int depth, int level, int x, int y, int z)
        {
            
            _texture.CopyImageSubData(source, srcLevel, srcX, srcY, srcZ, width, height, depth, level, x, y, z);
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
        /// <param name="depth">The depth in pixels.</param>
        /// <param name="x">The x offset to write to.</param>
        /// <param name="y">The y offset to write to.</param>
        /// <param name="z">The z offset to write to.</param>
        public void CopyTexture(ITexture source, int srcX, int srcY, int srcZ, int width, int height, int depth, int x, int y, int z) =>
            CopyTexture(source, 0, srcX, srcY, srcZ, width, height, depth, 0, x, y, z);

        /// <summary>
        /// Creates all levels for a mipmaped texture.
        /// </summary>
        public void CreateMipMap()
        {
            

            _texture.GenerateMipmap();
        }

        /// <summary>
        /// The internal storage resolution of the alpha component at base level.
        /// </summary>
        public int AlphaSize
        {
            get
            {
                

                return _texture.GetAlphaSize(_baseLevel);
            }
        }
        /// <summary>
        /// The data type used to store the alpha component at base level.
        /// </summary>
        public ChannelType AlphaChannel
        {
            get
            {
                

                return _texture.GetAlphaType(_baseLevel);
            }
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
        /// The internal storage resolution of the blue component at base level.
        /// </summary>
        public int BlueSize
        {
            get
            {
                

                return _texture.GetBlueSize(_baseLevel);
            }
        }
        /// <summary>
        /// The data type used to store the blue component at base level.
        /// </summary>
        public ChannelType BlueChannel
        {
            get
            {
                

                return _texture.GetBlueType(_baseLevel);
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
        /// The depth of the texture at base level.
        /// </summary>
        public int Depth
        {
            get
            {
                

                return _texture.GetDepth(_baseLevel);
            }
        }
        /// <summary>
        /// The internal storage resolution of the depth component at base level.
        /// </summary>
        public int DepthSize
        {
            get
            {
                

                return _texture.GetDepthSize(_baseLevel);
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
        /// The data type used to store the depth component at base level.
        /// </summary>
        public ChannelType DepthChannel
        {
            get
            {
                

                return _texture.GetDepthType(_baseLevel);
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
        /// The internal storage resolution of the green component at base level.
        /// </summary>
        public int GreenSize
        {
            get
            {
                

                return _texture.GetGreenSize(_baseLevel);
            }
        }
        /// <summary>
        /// The data type used to store the green component at base level.
        /// </summary>
        public ChannelType GreenChannel
        {
            get
            {
                

                return _texture.GetGreenType(_baseLevel);
            }
        }
        /// <summary>
        /// The height of the texture at base level.
        /// </summary>
        public int Height
        {
            get
            {
                

                return _texture.GetHeight(_baseLevel);
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
        /// The internal storage resolution of the red component at base level.
        /// </summary>
        public int RedSize
        {
            get
            {
                

                return _texture.GetRedSize(_baseLevel);
            }
        }
        /// <summary>
        /// The data type used to store the red component at base level.
        /// </summary>
        public ChannelType RedChannel
        {
            get
            {
                

                return _texture.GetRedType(_baseLevel);
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
        /// The width of the texture at base level.
        /// </summary>
        public int Width
        {
            get
            {
                

                return _texture.GetWidth(_baseLevel);
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

        public static Texture3D Create(GLArray<Colour> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            Texture3D texture = new Texture3D(TextureFormat.Rgba8, TextureData.Byte);

            texture.SetData(data.Width, data.Height, data.Depth, BaseFormat.Rgba, data);
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
        public static Texture3D Create(GLArray<ColourF> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            Texture3D texture = new Texture3D(TextureFormat.Rgba32f, TextureData.Float);

            texture.SetData(data.Width, data.Height, data.Depth, BaseFormat.Rgba, data);
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
        public static Texture3D Create(GLArray<Colour3> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            Texture3D texture = new Texture3D(TextureFormat.Rgb8, TextureData.Byte);

            texture.SetData(data.Width, data.Height, data.Depth, BaseFormat.Rgb, data);
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
        public static Texture3D Create(GLArray<ColourF3> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            Texture3D texture = new Texture3D(TextureFormat.Rgb32f, TextureData.Float);

            texture.SetData(data.Width, data.Height, data.Depth, BaseFormat.Rgb, data);
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

        public static Texture3D Create(Bitmap3 data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            Texture3D texture = new Texture3D(TextureFormat.Rgba8, TextureData.Byte);

            texture.SetData(data.Width, data.Height, data.Depth, BaseFormat.Rgba, (GLArray<Colour>)data);
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
