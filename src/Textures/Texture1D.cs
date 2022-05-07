using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that manages a 1 dimensional texture.
    /// </summary>
    public class Texture1D : ITexture
    {
        /// <summary>
        /// Creates a 1 dimensional texture with a set internal format.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        /// <param name="dataType">The type of data that is going to be passed to OpenGL.</param>
        public Texture1D(TextureFormat format, TextureData dataType)
        {
            _texture = new TextureGL(TextureTarget.Texture1D);
            InternalFormat = format;
            DataType = dataType;
        }
        internal Texture1D(uint id, TextureFormat format, TextureData dataType)
        {
            _texture = new TextureGL(id, TextureTarget.Texture1D, format);
            InternalFormat = format;
            DataType = dataType;
        }

        private readonly TextureGL _texture;

        public TextureTarget Target => TextureTarget.Texture1D;

        public TextureFormat InternalFormat { get; }
        protected TextureProperties Properties => _texture.Properties;
        TextureProperties ITexture.Properties => _texture.Properties;

        public uint Id => _texture.Id;
        public uint ReferanceSlot => _texture.ReferanceSlot;

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
        public int Size => Properties._width;
        /// <summary>
        /// The wrapping function used on all axes.
        /// </summary>
        public WrapStyle WrapStyle
        {
            get => Properties.WrapX;
            set => Properties.WrapX = value;
        }

        /// <summary>
        /// The type of data being inputed into the texture.
        /// </summary>
        public TextureData DataType { get; set; }

        public void Bind(uint slot) => _texture.Bind(slot);
        public void Bind() => _texture.Bind();
        /// <summary>
        /// Binds a specified level of the texture to a texture slot.
        /// </summary>
        /// <param name="slot">The slot to bind to.</param>
        /// <param name="level">The level of the texture.</param>
        /// <param name="access">The access type for the texture.</param>
        public void Bind(uint slot, int level, AccessType access) => _texture.BindLevel(slot, level, false, 0, access);
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
        /// <param name="size">The width of the texture</param>
        /// <param name="inputFormat">The format type of <paramref name="data"/>.</param>
        /// <param name="data">The data to set the texture to.</param>
        public void SetData<T>(int level, int size, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged
        {
            
            _texture.TexImage1D(level, InternalFormat, size, inputFormat, DataType, data);
        }
        /// <summary>
        /// Create a texture from <paramref name="data"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="size">The width of the texture</param>
        /// <param name="inputFormat">The format type of <paramref name="data"/>.</param>
        /// <param name="data">The data to set the texture to.</param>
        public void SetData<T>(int size, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged => SetData(0, size, inputFormat, data);

        /// <summary>
        /// Change the data of a section of the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="offset">The pixel offset.</param>
        /// <param name="size">The width of the section.</param>
        /// <param name="inputFormat">The format type of <paramref name="data"/>.</param>
        /// <param name="data">The data to change the scetion to.</param>
        public void EditData<T>(int level, int offset, int size, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged
        {
            
            _texture.TexSubImage1D(level, offset, size, inputFormat, DataType, data);
        }
        /// <summary>
        /// Change the data of a section of the first level of the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">The pixel offset.</param>
        /// <param name="size">The width of the section.</param>
        /// <param name="inputFormat">The format type of <paramref name="data"/>.</param>
        /// <param name="data">The data to change the scetion to.</param>
        public void EditData<T>(int offset, int size, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged => EditData(0, offset, size, inputFormat, data);

        /// <summary>
        /// Create an empy texture.
        /// </summary>
        /// <param name="levels">The number of mipmap levels.</param>
        /// <param name="size">The width of the texture.</param>
        public void CreateStorage(int levels, int size)
        {
            
            _texture.TexStorage1D(levels, InternalFormat, size);
        }
        /// <summary>
        /// Create an empy texture.
        /// </summary>
        /// <param name="size">The width of the texture.</param>
        public void CreateStorage(int size) => CreateStorage(1, size);

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
        /// Returns the data stored in the first level of this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="outputFormat">The format of the output data.</param>
        public GLArray<T> GetData<T>(BaseFormat outputFormat) where T : unmanaged => GetData<T>(0, outputFormat);

        /// <summary>
        /// Returns a section of the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="offset">The pixel offset.</param>
        /// <param name="size">The width of the section.</param>
        /// <param name="outputFormat">The format of the output data. Can be ignored if getting compressed data.</param>
        public GLArray<T> GetDataSection<T>(int level, int offset, int size, BaseFormat outputFormat) where T : unmanaged
        {
            return _texture.GetTextureSubImage<T>(level, offset, 0, 0, size, 1, 1, outputFormat, DataType);
        }
        /// <summary>
        /// Returns a section of the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">The pixel offset.</param>
        /// <param name="size">The width of the section.</param>
        /// <param name="outputFormat">The format of the output data. Can be ignored if getting compressed data.</param>
        public GLArray<T> GetDataSection<T>(int offset, int size, BaseFormat outputFormat) where T : unmanaged
            => GetDataSection<T>(0, offset, size, outputFormat);

        /// <summary>
        /// Copy data from <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="srcLevel">The mipmap level from source.</param>
        /// <param name="srcX">The source x.</param>
        /// <param name="srcY">The source y.</param>
        /// <param name="srcZ">The source z.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="level">The mipmap level.</param>
        /// <param name="x">The offset to write to.</param>
        public void CopyTexture(ITexture source, int srcLevel, int srcX, int srcY, int srcZ, int width, int level, int x)
        {
            
            _texture.CopyImageSubData(source, srcLevel, srcX, srcY, srcZ, width, 1, 1, level, x, 0, 0);
        }
        /// <summary>
        /// Copy data from <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="srcX">The source x.</param>
        /// <param name="srcY">The source y.</param>
        /// <param name="srcZ">The source z.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="x">The offset to write to.</param>
        public void CopyTexture(ITexture source, int srcX, int srcY, int srcZ, int width, int x) =>
            CopyTexture(source, 0, srcX, srcY, srcZ, width, 0, x);
        /// <summary>
        /// Copy data from another <see cref="Texture1D"/>.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="srcLevel">The mipmap level from source.</param>
        /// <param name="srcOffset">The offset into the source to referance.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="level">The mipmap level.</param>
        /// <param name="offset">The offset to write to.</param>
        public void CopyTexture(Texture1D source, int srcLevel, int srcOffset, int width, int level, int offset)
        {
            
            _texture.CopyImageSubData(source, srcLevel, srcOffset, 0, 0, width, 1, 1, level, offset, 0, 0);
        }
        /// <summary>
        /// Copy data from another <see cref="Texture1D"/>.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="srcOffset">The offset into the source to referance.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="offset">The offset to write to.</param>
        public void CopyTexture(Texture1D source, int srcOffset, int width, int offset) =>
            CopyTexture(source, 0, srcOffset, width, 0, offset);
        /// <summary>
        /// Copy data from another <see cref="Texture1D"/>.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="srcLevel">The mipmap level from source.</param>
        /// <param name="srcOffset">The offset into the source to referance.</param>
        /// <param name="srcIndex">The 0-based index into the source array to referance.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="level">The mipmap level.</param>
        /// <param name="offset">The offset to write to.</param>
        public void CopyTexture(Texture1DArray source, int srcLevel, int srcOffset, int srcIndex, int width, int level, int offset)
        {
            
            _texture.CopyImageSubData(source, srcLevel, srcOffset, srcIndex, 0, width, 1, 1, level, offset, 0, 0);
        }
        /// <summary>
        /// Copy data from another <see cref="Texture1D"/>.
        /// </summary>
        /// <param name="source">The source texture.</param>
        /// <param name="srcOffset">The offset into the source to referance.</param>
        /// <param name="srcIndex">The 0-based index into the source array to referance.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="offset">The offset to write to.</param>
        public void CopyTexture(Texture1DArray source, int srcOffset, int srcIndex, int width, int offset) =>
            CopyTexture(source, 0, srcOffset, srcIndex, width, 0, offset);

        /// <summary>
        /// Creates all levels for a mipmaped texture.
        /// </summary>
        public void CreateMipMap()
        {
            

            _texture.GenerateMipmap();
        }

        public static Texture1D Create(GLArray<Colour> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            Texture1D texture = new Texture1D(TextureFormat.Rgba8, TextureData.Byte);

            texture.SetData(data.Width, BaseFormat.Rgba, data);

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
        public static Texture1D Create(GLArray<ColourF> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            Texture1D texture = new Texture1D(TextureFormat.Rgba32f, TextureData.Float);

            texture.SetData(data.Width, BaseFormat.Rgba, data);

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
        public static Texture1D Create(GLArray<Colour3> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            Texture1D texture = new Texture1D(TextureFormat.Rgb8, TextureData.Byte);

            texture.SetData(data.Width, BaseFormat.Rgb, data);

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
        public static Texture1D Create(GLArray<ColourF3> data, WrapStyle wrapStyle, TextureSampling textureQuality, bool mipmap)
        {
            Texture1D texture = new Texture1D(TextureFormat.Rgb32f, TextureData.Float);

            texture.SetData(data.Width, BaseFormat.Rgb, data);

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
