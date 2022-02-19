using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that manages an array of 1 dimensional textures.
    /// </summary>
    public class Texture2DArray : ITexture
    {
        /// <summary>
        /// Creates an array of 2 dimensional textures with a set internal format.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        /// <param name="dataType">The type of data that is going to be passed to OpenGL.</param>
        public Texture2DArray(TextureFormat format, TextureData dataType)
        {
            if (format.IsCompressed())
            {
                throw new Exception("Invalid format. Must not be a compressed type to be valid for Texture2DArray.");
            }

            _texture = new TextureGL(TextureTarget.Texture2DArray);
            InternalFormat = format;
            DataType = dataType;
        }
        internal Texture2DArray(uint id, TextureFormat format, TextureData dataType)
        {
            _texture = new TextureGL(id, TextureTarget.Texture2DArray, format);
            InternalFormat = format;
            DataType = dataType;
        }

        private readonly TextureGL _texture;

        public TextureTarget Target => TextureTarget.Texture2DArray;

        public TextureFormat InternalFormat { get; }

        public uint Id => _texture.Id;
        public uint ReferanceSlot => _texture.ReferanceSlot;

        /// <summary>
        /// The type of data being inputed into the texture.
        /// </summary>
        public TextureData DataType { get; set; }

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
        public int ArrayLength
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

        public void Bind(uint slot) => _texture.Bind(slot);
        public void Bind() => _texture.Bind();
        /// <summary>
        /// Binds a specified level of the texture to a texture slot.
        /// </summary>
        /// <param name="slot">The slot to bind to.</param>
        /// <param name="level">The level of the texture.</param>
        /// <param name="index">The index into the texture array.</param>
        /// <param name="access">The access type for the texture.</param>
        public void Bind(uint slot, int level, int index, AccessType access) => _texture.BindLevel(slot, level, false, index, access);
        public void Unbind() => _texture.Unbind();

        private bool _disposed = false;
        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);

                _disposed = true;

                GC.SuppressFinalize(this);
            }
        }
        protected virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                _texture.Dispose();
            }
        }

        /// <summary>
        /// Create a texture from <paramref name="data"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="arrayLength">The length of the texture array.</param>
        /// <param name="inputFormat">The format type of <paramref name="data"/>.</param>
        /// <param name="data">The data to set the texture to.</param>
        public void SetData<T>(int level, int width, int height, int arrayLength, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged
        {
            
            _texture.TexImage3D(level, InternalFormat, width, height, arrayLength, inputFormat, DataType, data);
        }
        /// <summary>
        /// Create a texture from <paramref name="data"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="arrayLength">The length of the texture array.</param>
        /// <param name="inputFormat">The format type of <paramref name="data"/>.</param>
        /// <param name="data">The data to set the texture to.</param>
        public void SetData<T>(int width, int height, int arrayLength, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged =>
            SetData(0, width, height, arrayLength, inputFormat, data);

        /// <summary>
        /// Change the data of a section of the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="x">The x pixel offset.</param>
        /// <param name="y">The y pixel offset.</param>
        /// <param name="index">The 0-based index into the array to set data.</param>
        /// <param name="width">The width of the texture to edit.</param>
        /// <param name="height">The height of the texture to edit..</param>
        /// <param name="inputFormat">The format type of <paramref name="data"/>.</param>
        /// <param name="data">The data to change the scetion to.</param>
        public void EditData<T>(int level, int x, int y, int index, int width, int height, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged
        {
            _texture.TexSubImage3D(level, x, y, index, width, height, 1, inputFormat, DataType, data);
        }
        /// <summary>
        /// Change the data of a section of the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">The x pixel offset.</param>
        /// <param name="y">The y pixel offset.</param>
        /// <param name="index">The 0-based index into the array to set data.</param>
        /// <param name="width">The width of the texture to edit.</param>
        /// <param name="height">The height of the texture to edit..</param>
        /// <param name="inputFormat">The format type of <paramref name="data"/>.</param>
        /// <param name="data">The data to change the scetion to.</param>
        public void EditData<T>(int x, int y, int index, int width, int height, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged =>
            EditData(0, x, y, index, width, height, inputFormat, data);

        /// <summary>
        /// Create an empy texture.
        /// </summary>
        /// <param name="levels">The number of mipmap levels.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="arrayLength">The size of the texture array.</param>
        public void CreateStorage(int levels, int width, int height, int arrayLength)
        {
            _texture.TexStorage3D(levels, InternalFormat, width, height, arrayLength);
        }
        /// <summary>
        /// Create an empy texture.
        /// </summary>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="arrayLength">The size of the texture array.</param>
        public void CreateStorage(int width, int height, int arrayLength) => CreateStorage(1, width, height, arrayLength);

        /// <summary>
        /// Returns the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="outputFormat">The format of the output data.</param>
        public GLArray<T> GetData<T>(int level, BaseFormat outputFormat) where T : unmanaged
        {
            return _texture.GetTexImage<T>(level, outputFormat, DataType);
        }
        /// <summary>
        /// Returns the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="outputFormat">The format of the output data.</param>
        public GLArray<T> GetData<T>(BaseFormat outputFormat) where T : unmanaged =>
            GetData<T>(0, outputFormat);

        /// <summary>
        /// Returns a section of the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="x">The x pixel offset.</param>
        /// <param name="y">The y pixel offset.</param>
        /// <param name="index">The 0-based index into the texture array.</param>
        /// <param name="width">The width of the section.</param>
        /// <param name="height">The height of the section.</param>
        /// <param name="outputFormat">The format of the output data. Can be ignored if getting compressed data.</param>
        public GLArray<T> GetDataSection<T>(int level, int x, int y, int index, int width, int height, BaseFormat outputFormat) where T : unmanaged
        {
            return _texture.GetTextureSubImage<T>(level, x, y, index, width, height, 1, outputFormat, DataType);
        }
        /// <summary>
        /// Returns a section of the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="x">The x pixel offset.</param>
        /// <param name="y">The y pixel offset.</param>
        /// <param name="index">The 0-based index into the texture array.</param>
        /// <param name="width">The width of the section.</param>
        /// <param name="height">The height of the section.</param>
        /// <param name="outputFormat">The format of the output data. Can be ignored if getting compressed data.</param>
        public GLArray<T> GetDataSection<T>(int x, int y, int index, int width, int height, BaseFormat outputFormat) where T : unmanaged =>
            GetDataSection<T>(0, x, y, index, width, height, outputFormat);

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
        /// <param name="level">The mipmap level.</param>
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
        /// Copy data from <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="srcLevel">The mipmap level from source.</param>
        /// <param name="srcX">The source x.</param>
        /// <param name="srcY">The source y.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        /// <param name="level">The mipmap level.</param>
        /// <param name="x">The x offset to write to.</param>
        /// <param name="y">The y offset to write to.</param>
        /// <param name="index">The 0-based index into the array to write to.</param>
        public void CopyTexture(Texture2D source, int srcLevel, int srcX, int srcY, int width, int height, int level, int x, int y, int index)
        {
            _texture.CopyImageSubData(source, srcLevel, srcX, srcY, 0, width, height, 1, level, x, y, index);
        }
        /// <summary>
        /// Copy data from <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="srcX">The source x.</param>
        /// <param name="srcY">The source y.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        /// <param name="x">The x offset to write to.</param>
        /// <param name="y">The y offset to write to.</param>
        /// <param name="index">The 0-based index into the array to write to.</param>
        public void CopyTexture(Texture2D source, int srcX, int srcY, int width, int height, int x, int y, int index) =>
            CopyTexture(source, 0, srcX, srcY, width, height, 0, x, y, index);

        /// <summary>
        /// Creates all levels for a mipmaped texture.
        /// </summary>
        public void CreateMipMap()
        {
            _texture.GenerateMipmap();
        }

        public static Texture2DArray Create(GLArray<Colour> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            Texture2DArray texture = new Texture2DArray(TextureFormat.Rgba8, TextureData.Byte);

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
        public static Texture2DArray Create(GLArray<ColourF> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            Texture2DArray texture = new Texture2DArray(TextureFormat.Rgba32f, TextureData.Float);

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
        public static Texture2DArray Create(GLArray<Colour3> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            Texture2DArray texture = new Texture2DArray(TextureFormat.Rgb8, TextureData.Byte);

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
        public static Texture2DArray Create(GLArray<ColourF3> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            Texture2DArray texture = new Texture2DArray(TextureFormat.Rgb32f, TextureData.Float);

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
        
        public static Texture2DArray Create(Bitmap3 image, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            Texture2DArray texture = new Texture2DArray(TextureFormat.Rgba8, TextureData.Byte);

            texture.SetData(image.Width, image.Height, image.Depth, BaseFormat.Rgba, (GLArray<Colour>)image);

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
        public static Texture2DArray Create(Bitmap[] images, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            Texture2DArray texture = new Texture2DArray(TextureFormat.Rgba8, TextureData.Byte);

            GLArray<Colour> data = new GLArray<Colour>(images[0].Width, images[0].Height, images.Length);

            try
            {
                for (int i = 0; i < images.Length; i++)
                {
                    for (int x = 0; x < images[i].Width; x++)
                    {
                        for (int y = 0; y < images[i].Height; y++)
                        {
                            data[x, y, i] = images[i][x, y];
                        }
                    }
                }
            }
            catch
            {
                throw new Exception($"All images in {nameof(images)} must be the same size.");
            }

            texture.SetData(images[0].Width, images[0].Height, images.Length, BaseFormat.Rgba, data);

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
