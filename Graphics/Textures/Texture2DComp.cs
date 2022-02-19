using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that manages compressed 2 dimensional textures.
    /// </summary>
    public class Texture2DComp : ITexture
    {
        /// <summary>
        /// Creates a 2 dimensional texture with a compressed internal format.
        /// </summary>
        /// <param name="target">The type of texture that this object is managing.</param>
        /// <param name="format">The internal format of the texture.</param>
        public Texture2DComp(TextureTarget target, TextureFormat format)
        {
            if (!target.Is2D())
            {
                throw new Exception("Invalid target. Must be of 2d origin to be valid for Texture2DComp.");
            }
            if (!format.IsCompressed())
            {
                throw new Exception("Invalid format. Must be a compressed type to be valid for Texture2DComp.");
            }

            _texture = new TextureGL(target);
            InternalFormat = format;
        }
        internal Texture2DComp(TextureTarget target, uint id, TextureFormat format)
        {
            _texture = new TextureGL(id, target, format);
            InternalFormat = format;
        }

        private readonly TextureGL _texture;

        public TextureTarget Target => _texture.Target;

        public TextureFormat InternalFormat { get; }

        public uint Id => _texture.Id;
        public uint ReferanceSlot => _texture.ReferanceSlot;

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
        /// The number of bytes that make up the texture's data.
        /// </summary>
        public int CompressedImageSize
        {
            get
            {
                return _texture.GetCompressedImageSize(_baseLevel);
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
        /// The wrapping function used on all axes.
        /// </summary>
        public WrapStyle WrapStyle
        {
            set
            {
                WrapX = value;
                WrapY = value;
            }
        }

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
        /// <param name="width">The width of the texture</param>
        /// <param name="height">The height of the texture</param>
        /// <param name="data">The data to set the texture to.</param>
        public void SetData<T>(int level, int width, int height, GLArray<T> data) where T : unmanaged
        {
            
            _texture.CompressedTexImage2D(level, InternalFormat, width, height, data);
        }
        /// <summary>
        /// Create a texture from <paramref name="data"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="width">The width of the texture</param>
        /// <param name="height">The height of the texture</param>
        /// <param name="data">The data to set the texture to.</param>
        public void SetData<T>(int width, int height, GLArray<T> data) where T : unmanaged => SetData(0, width, height, data);

        /// <summary>
        /// Change the data of a section of the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="x">The x pixel offset.</param>
        /// <param name="y">The y pixel offset.</param>
        /// <param name="width">The width of the section.</param>
        /// <param name="height">The height of the section.</param>
        /// <param name="data">The data to change the scetion to.</param>
        public void EditData<T>(int level, int x, int y, int width, int height, GLArray<T> data) where T : unmanaged
        {
            
            _texture.CompressedTexSubImage2D(level, x, y, width, height, data);
        }
        /// <summary>
        /// Change the data of a section of the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">The x pixel offset.</param>
        /// <param name="y">The y pixel offset.</param>
        /// <param name="width">The width of the section.</param>
        /// <param name="height">The height of the section.</param>
        /// <param name="data">The data to change the scetion to.</param>
        public void EditData<T>(int x, int y, int width, int height, GLArray<T> data) where T : unmanaged => EditData(0, x,  y, width, height, data);

        /// <summary>
        /// Create an empy texture.
        /// </summary>
        /// <param name="levels">The number of mipmap levels.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        public void CreateStorage(int levels, int width, int height)
        {
            
            _texture.TexStorage2D(levels, InternalFormat, width, height);
        }
        /// <summary>
        /// Create an empy texture.
        /// </summary>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        public void CreateStorage(int width, int height) => CreateStorage(1, width, height);

        /// <summary>
        /// Returns the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="bufferSize">The size, int bytes, of the returned array.</param>
        public T[] GetData<T>(int level, int bufferSize) where T : unmanaged
        {
            
            return _texture.GetCompressedTexImage<T>(level, bufferSize);
        }
        /// <summary>
        /// Returns the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bufferSize">The size, int bytes, of the returned array.</param>
        public T[] GetData<T>(int bufferSize) where T : unmanaged => GetData<T>(0, bufferSize);

        /// <summary>
        /// Returns a section of the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="x">The x pixel offset.</param>
        /// <param name="y">The y pixel offset.</param>
        /// <param name="width">The width of the section.</param>
        /// <param name="height">The height of the section.</param>
        /// <param name="bufferSize">The size of the returned array. Only used for compressed formats.</param>
        public unsafe T[] GetDataSection<T>(int level, int x, int y, int width, int height, int bufferSize) where T : unmanaged
        {
            
            return _texture.GetCompressedTextureSubImage<T>(level, x, y, 0, width, height, 1, bufferSize);
        }
        /// <summary>
        /// Returns a section of the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">The x pixel offset.</param>
        /// <param name="y">The y pixel offset.</param>
        /// <param name="width">The width of the section.</param>
        /// <param name="height">The height of the section.</param>
        /// <param name="bufferSize">The size in bytes of the returned array. Only used for compressed formats.</param>
        public T[] GetDataSection<T>(int x, int y, int width, int height, int bufferSize) where T : unmanaged
            => GetDataSection<T>(0, x, y, width, height, bufferSize);

        /// <summary>
        /// Creates all levels for a mipmaped texture.
        /// </summary>
        public void CreateMipMap()
        {
            

            _texture.GenerateMipmap();
        }

        public static Texture2DComp Create<T>(TextureTarget target, TextureFormat format, GLArray<T> data, int width, int height) where T : unmanaged
        {
            Texture2DComp texture = new Texture2DComp(target, format);

            texture.SetData(width, height, data);

            return texture;
        }
    }
}
