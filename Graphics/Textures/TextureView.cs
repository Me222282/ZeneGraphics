using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that manages a pointer to another texture.
    /// </summary>
    public class TextureView : ITexture
    {
        /// <summary>
        /// Creates a pointer to the data in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The texture containing the real data.</param>
        /// <param name="target">The type of texture that this object is managing.</param>
        /// <param name="format">The internal format of this texture.</param>
        /// <param name="level">The level to source data from.</param>
        /// <param name="levelCount">The number of levels to point to.</param>
        /// <param name="layer">The layer to source data from.</param>
        /// <param name="layerCount">The number of layers to point to.</param>
        public TextureView(ITexture source, TextureTarget target, TextureFormat format, int level, int levelCount, int layer, int layerCount)
        {
            Target = target;
            _texture = new TextureGL(target);
            _texture.TextureView(source, format, (uint)level, (uint)levelCount, (uint)layer, (uint)layerCount);
        }
        /// <summary>
        /// Creates a pointer with the same internal format to the data in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The texture containing the real data.</param>
        /// <param name="target">The type of texture that this object is manageing.</param>
        /// <param name="level">The level to source data from.</param>
        /// <param name="levelCount">The number of levels to point to.</param>
        /// <param name="layer">The layer to source data from.</param>
        /// <param name="layerCount">The number of layers to point to.</param>
        public TextureView(ITexture source, TextureTarget target, int level, int levelCount, int layer, int layerCount)
        {
            Target = target;
            _texture = new TextureGL(target);
            _texture.TextureView(source, source.InternalFormat, (uint)level, (uint)levelCount, (uint)layer, (uint)layerCount);
        }

        private readonly TextureGL _texture;

        public TextureTarget Target { get; }
        public TextureFormat InternalFormat => _texture.InternalFormat;
        protected TextureProperties Properties => _texture.Properties;
        TextureProperties ITexture.Properties => _texture.Properties;

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
        /// The offset into the data store of the buffer bound to a buffer texture
        /// </summary>
        public int BufferOffset
        {
            get
            {
                

                return _texture.GetBufferOffset(_baseLevel);
            }
        }
        /// <summary>
        /// The size of the range of a data store of the buffer bound to a buffer texture.
        /// </summary>
        public int BufferSize
        {
            get
            {
                

                return _texture.GetBufferSize(_baseLevel);
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

        /// <summary>
        /// The number of immutable texture levels in a texture view.
        /// </summary>
        public int ImmutableLevels
        {
            get
            {
                

                return _texture.GetViewImmutableLevels();
            }
            set
            {
                

                _texture.SetViewImmutableLevels(value);
            }
        }
        /// <summary>
        /// The first level of a texture array view relative to its parent.
        /// </summary>
        public int MinLayer
        {
            get
            {
                

                return _texture.GetViewMinLayer();
            }
            set
            {
                

                _texture.SetViewMinLayer(value);
            }
        }
        /// <summary>
        /// The base level of a texture view relative to its parent.
        /// </summary>
        public int MinLevel
        {
            get
            {
                

                return _texture.GetViewMinLevel();
            }
            set
            {
                

                _texture.SetViewMinLevel(value);
            }
        }
        /// <summary>
        /// The number of layers in a texture array view.
        /// </summary>
        public int NumLayers
        {
            get
            {
                

                return _texture.GetViewNumLayers();
            }
            set
            {
                

                _texture.SetViewNumLayers(value);
            }
        }
        /// <summary>
        /// The number of levels of detail of a texture view.
        /// </summary>
        public int NumLevels
        {
            get
            {
                

                return _texture.GetViewNumLevels();
            }
            set
            {
                

                _texture.SetViewNumLevels(value);
            }
        }

        public void Bind(uint slot) => _texture.Bind(slot);
        public void Bind() => _texture.Bind();
        /// <summary>
        /// Binds a specified level and layer of the texture to a texture slot.
        /// </summary>
        /// <param name="slot">The slot to bind to.</param>
        /// <param name="level">The level of the texture.</param>
        /// <param name="layer">The index of a texture array.</param>
        /// <param name="access">The access type for the texture.</param>
        public void Bind(uint slot, int level, int layer, AccessType access) => _texture.BindLevel(slot, level, false, layer, access);
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
        /// Reasgines the pointer to <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The texture containing the real data.</param>
        /// <param name="level">The level to source data from.</param>
        /// <param name="levelCount">The number of levels to point to.</param>
        /// <param name="layer">The layer to source data from.</param>
        /// <param name="layerCount">The number of layers to point to.</param>
        public void View(ITexture source, int level, int levelCount, int layer, int layerCount)
        {
            _texture.TextureView(source, InternalFormat, (uint)level, (uint)levelCount, (uint)layer, (uint)layerCount);
        }

        /// <summary>
        /// Returns a section of the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="outputFormat">The format of the output data.</param>
        /// <param name="outputType">The type fo data to output.</param>
        public GLArray<T> GetData<T>(int level, BaseFormat outputFormat, TextureData outputType) where T : unmanaged
        {
            return _texture.GetTexImage<T>(level, outputFormat, outputType);
        }
        /// <summary>
        /// Returns a section of the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="outputFormat">The format of the output data.</param>
        /// <param name="outputType">The type fo data to output.</param>
        public GLArray<T> GetData<T>(BaseFormat outputFormat, TextureData outputType) where T : unmanaged
            => GetData<T>(0, outputFormat, outputType);

        /// <summary>
        /// Returns a section of the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="x">The x pixel offset.</param>
        /// <param name="width">The width of the section.</param>
        /// <param name="outputFormat">The format of the output data.</param>
        /// <param name="outputType">The type fo data to output.</param>
        public GLArray<T> GetDataSection<T>(int level, int x, int width, BaseFormat outputFormat, TextureData outputType) where T : unmanaged
        {
            return _texture.GetTextureSubImage<T>(level, x, 0, 0, width, 1, 1, outputFormat, outputType);
        }
        /// <summary>
        /// Returns a section of the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">The x pixel offset.</param>
        /// <param name="width">The width of the section.</param>
        /// <param name="outputFormat">The format of the output data.</param>
        /// <param name="outputType">The type fo data to output.</param>
        public GLArray<T> GetDataSection<T>(int x, int width, BaseFormat outputFormat, TextureData outputType) where T : unmanaged
            => GetDataSection<T>(0, x, width, outputFormat, outputType);
    }
}
