using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public unsafe sealed class TextureProperties : TexRenProperties
    {
        public TextureProperties(ITexture source)
            : base(source)
        {
            Handle = source;
        }
        public override ITexture Handle { get; }

        /// <summary>
        /// Returns the size of <see cref="Handle"/> at a given mipmap based on the baselevel mipmap.
        /// </summary>
        /// <param name="level">The mipmap level used to determin the size reduction from baselevel.</param>
        public Vector3I GetMipMapSize(int level)
        {
            int i = level - _baseLevel;

            Vector3I size;

            switch (Handle.Target)
            {
                case TextureTarget.Texture1D:
                case TextureTarget.Buffer:
                    size = new Vector3I(
                        (int)Math.Floor(_width / Math.Pow(2, i)),
                        1,
                        1);
                    break;

                case TextureTarget.Texture1DArray:
                    size = new Vector3I(
                        (int)Math.Floor(_width / Math.Pow(2, i)),
                        _height,
                        1);
                    break;

                case TextureTarget.Texture2D:
                case TextureTarget.Multisample2D:
                case TextureTarget.Rectangle:
                    size = new Vector3I(
                        (int)Math.Floor(_width / Math.Pow(2, i)),
                        (int)Math.Floor(_height / Math.Pow(2, i)),
                        1);
                    break;

                case TextureTarget.CubeMap:
                case TextureTarget.CubeMapArray:
                case TextureTarget.Multisample2DArray:
                case TextureTarget.Texture2DArray:
                    size = new Vector3I(
                        (int)Math.Floor(_width / Math.Pow(2, i)),
                        (int)Math.Floor(_height / Math.Pow(2, i)),
                        _depth);
                    break;

                case TextureTarget.Texture3D:
                    size = new Vector3I(
                        (int)Math.Floor(_width / Math.Pow(2, i)),
                        (int)Math.Floor(_height / Math.Pow(2, i)),
                        (int)Math.Floor(_depth / Math.Pow(2, i)));
                    break;

                default:
                    return Vector3I.Zero;
            }

            if (size.X < 1 || size.Y < 1 || size.Z < 1)
            {
                return Vector3I.Zero;
            }

            return size;
        }

        internal int _baseLevel = 0;
        /// <summary>
        /// The base texture mipmap level.
        /// </summary>
        public int BaseLevel
        {
            get => _baseLevel;
            set
            {
                _baseLevel = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureBaseLevel, value);
            }
        }
        private ColourF _border = ColourF.Zero;
        /// <summary>
        /// The border colour of <see cref="Handle"/>.
        /// </summary>
        /// <remarks>
        /// This stores the value as a float.
        /// </remarks>
        public ColourF BorderColour
        {
            get => _border;
            set
            {
                _border = value;

                Handle.Bind();
                float* colour = stackalloc float[] { value.R, value.G, value.B, value.A };

                GL.TexParameterfv((uint)Handle.Target, GLEnum.TextureBorderColour, colour);
            }
        }
        /// <summary>
        /// The border colour of <see cref="Handle"/>.
        /// </summary>
        /// <remarks>
        /// This stores the value as a integer.
        /// </remarks>
        public Colour BorderColourI
        {
            get => (Colour)_border;
            set
            {
                _border = value;

                Handle.Bind();
                int* colour = stackalloc int[] { value.R, value.G, value.B, value.A };

                GL.TexParameteriv((uint)Handle.Target, GLEnum.TextureBorderColour, colour);
            }
        }
        private ComparisonFunction _compareFunc = ComparisonFunction.LessEqual;
        /// <summary>
        /// The comparison operator used when <see cref="ComparisonMode"/> is set to <see cref="ComparisonMode.CompareToDepth"/>.
        /// </summary>
        public ComparisonFunction ComparisonFunction
        {
            get => _compareFunc;
            set
            {
                _compareFunc = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureCompareFunc, (int)value);
            }
        }
        private ComparisonMode _compareMode = ComparisonMode.None;
        /// <summary>
        /// The texture comparison mode for depth textures.
        /// </summary>
        public ComparisonMode ComparisonMode
        {
            get => _compareMode;
            set
            {
                _compareMode = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureCompareMode, (int)value);
            }
        }
        internal int _depth = 0;
        /// <summary>
        /// The depth of <see cref="Handle"/> at base level.
        /// </summary>
        public int Depth => _depth;
        private DepthStencilMode _dsMode = DepthStencilMode.Depth;
        /// <summary>
        /// The mode used to read from depth-stencil format textures.
        /// </summary>
        public DepthStencilMode DepthStencilMode
        {
            get => _dsMode;
            set
            {
                _dsMode = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.DepthStencilTextureMode, (int)value);
            }
        }
        /// <summary>
        /// The matching criteria use for the texture when used as an image texture.
        /// </summary>
        public FormatCompatibilityType FormatCompatibilityType
        {
            get
            {
                Handle.Bind();
                int output;

                GL.GetTexParameteriv((uint)Handle.Target, GLEnum.ImageFormatCompatibilityType, &output);

                return (FormatCompatibilityType)output;
            }
        }
        private double _lodBias = 0;
        /// <summary>
        /// A fixed bias that is to be added to the level-of-detail parameter before texture sampling.
        /// </summary>
        public double LodBias
        {
            get => _lodBias;
            set
            {
                _lodBias = value;

                Handle.Bind();
                GL.TexParameterf((uint)Handle.Target, GLEnum.TextureLodBias, (float)value);
            }
        }
        private TextureSampling _magFilter = TextureSampling.Blend;
        /// <summary>
        /// The texture magnification function used when the level-of-detail function determines that <see cref="Handle"/> should be magified.
        /// </summary>
        public TextureSampling MagFilter
        {
            get => _magFilter;
            set
            {
                _magFilter = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureMagFilter, (int)value);
            }
        }
        internal int _maxLevel = 0;
        /// <summary>
        /// The maximum texture mipmap array level.
        /// </summary>
        public int MaxLevel
        {
            get => _maxLevel;
            set
            {
                _maxLevel = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureMaxLevel, value);
            }
        }
        private double _maxLod = 1000;
        /// <summary>
        /// The maximum value for the level-of-detail parameter.
        /// </summary>
        public double MaxLod
        {
            get => _maxLod;
            set
            {
                _maxLod = value;

                Handle.Bind();
                GL.TexParameterf((uint)Handle.Target, GLEnum.TextureMaxLod, (float)value);
            }
        }
        private TextureSampling _minFilter = TextureSampling.Blend;
        /// <summary>
        /// The texture minification function used when the level-of-detail function determines that <see cref="Handle"/> should be minified.
        /// </summary>
        public TextureSampling MinFilter
        {
            get => _minFilter;
            set
            {
                _minFilter = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureMinFilter, (int)value);
            }
        }
        private double _minLod = -1000;
        /// <summary>
        /// The minimum value for the level-of-detail parameter.
        /// </summary>
        public double MinLod
        {
            get => _minLod;
            set
            {
                _minLod = value;

                Handle.Bind();
                GL.TexParameterf((uint)Handle.Target, GLEnum.TextureMinLod, (float)value);
            }
        }
        private Swizzle _redSwiz = Swizzle.Red;
        /// <summary>
        /// The swizzle that will be applied to the red component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle RedSwizzle
        {
            get => _redSwiz;
            set
            {
                _redSwiz = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureSwizzleR, (int)value);
            }
        }
        private Swizzle _greenSwiz = Swizzle.Green;
        /// <summary>
        /// The swizzle that will be applied to the green component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle GreenSwizzle
        {
            get => _greenSwiz;
            set
            {
                _greenSwiz = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureSwizzleG, (int)value);
            }
        }
        private Swizzle _blueSwiz = Swizzle.Blue;
        /// <summary>
        /// The swizzle that will be applied to the blue component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle BlueSwizzle
        {
            get => _blueSwiz;
            set
            {
                _blueSwiz = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureSwizzleB, (int)value);
            }
        }
        private Swizzle _alphaSwiz = Swizzle.Alpha;
        /// <summary>
        /// The swizzle that will be applied to the alpha component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle AlphaSwizzle
        {
            get => _alphaSwiz;
            set
            {
                _alphaSwiz = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureSwizzleA, (int)value);
            }
        }
        private WrapStyle _wrapX = WrapStyle.Repeated;
        /// <summary>
        /// The wrapping function used on the x coordinate.
        /// </summary>
        public WrapStyle WrapX
        {
            get => _wrapX;
            set
            {
                _wrapX = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureWrapS, (int)value);
            }
        }
        private WrapStyle _wrapY = WrapStyle.Repeated;
        /// <summary>
        /// The wrapping function used on the y coordinate.
        /// </summary>
        public WrapStyle WrapY
        {
            get => _wrapY;
            set
            {
                _wrapY = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureWrapT, (int)value);
            }
        }
        private WrapStyle _wrapZ = WrapStyle.Repeated;
        /// <summary>
        /// The wrapping function used on the z coordinate.
        /// </summary>
        public WrapStyle WrapZ
        {
            get => _wrapZ;
            set
            {
                _wrapZ = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureWrapR, (int)value);
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

        internal override void InternalFormatChanged()
        {
            // Texture format hasn't changed
            if (Handle.InternalFormat == _oldFormat) { return; }

            base.InternalFormatChanged();

            switch (Handle.InternalFormat)
            {
                case TextureFormat.CompressedRed:
                case TextureFormat.CompressedRedRgtc1:
                case TextureFormat.R16:
                case TextureFormat.R8:
                    _redChannel = ChannelType.UNormalised;
                    _greenChannel = ChannelType.None;
                    _blueChannel = ChannelType.None;
                    _alphaChannel = ChannelType.None;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.CompressedRg:
                case TextureFormat.CompressedRgRgtc2:
                case TextureFormat.Rg16:
                case TextureFormat.Rg8:
                    _redChannel = ChannelType.UNormalised;
                    _greenChannel = ChannelType.UNormalised;
                    _blueChannel = ChannelType.None;
                    _alphaChannel = ChannelType.None;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.CompressedRgb:
                case TextureFormat.CompressedSrgb:
                case TextureFormat.R3G3B2:
                case TextureFormat.Rgb:
                case TextureFormat.Rgb10:
                case TextureFormat.Rgb12:
                case TextureFormat.Rgb16:
                case TextureFormat.Rgb4:
                case TextureFormat.Rgb5:
                case TextureFormat.Rgb8:
                case TextureFormat.Srgb:
                case TextureFormat.Srgb8:
                    _redChannel = ChannelType.UNormalised;
                    _greenChannel = ChannelType.UNormalised;
                    _blueChannel = ChannelType.UNormalised;
                    _alphaChannel = ChannelType.None;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.CompressedRgba:
                case TextureFormat.CompressedRgbaBptcUnorm:
                case TextureFormat.CompressedSrgba:
                case TextureFormat.CompressedSrgbaBptcUnorm:
                case TextureFormat.Rgb10A2:
                case TextureFormat.Rgb5A1:
                case TextureFormat.Rgba:
                case TextureFormat.Rgba12:
                case TextureFormat.Rgba16:
                case TextureFormat.Rgba2:
                case TextureFormat.Rgba4:
                case TextureFormat.Rgba8:
                case TextureFormat.Srgba:
                case TextureFormat.Srgba8:
                    _redChannel = ChannelType.UNormalised;
                    _greenChannel = ChannelType.UNormalised;
                    _blueChannel = ChannelType.UNormalised;
                    _alphaChannel = ChannelType.UNormalised;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.R16f:
                case TextureFormat.R32f:
                    _redChannel = ChannelType.Float;
                    _greenChannel = ChannelType.None;
                    _blueChannel = ChannelType.None;
                    _alphaChannel = ChannelType.None;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.Rg16f:
                case TextureFormat.Rg32f:
                    _redChannel = ChannelType.Float;
                    _greenChannel = ChannelType.Float;
                    _blueChannel = ChannelType.None;
                    _alphaChannel = ChannelType.None;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.CompressedRgbBptcSignedFloat:
                case TextureFormat.R11fG11fB10f:
                case TextureFormat.Rgb16f:
                case TextureFormat.Rgb32f:
                case TextureFormat.Rgb9E5:
                    _redChannel = ChannelType.Float;
                    _greenChannel = ChannelType.Float;
                    _blueChannel = ChannelType.Float;
                    _alphaChannel = ChannelType.None;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.Rgba16f:
                case TextureFormat.Rgba32f:
                    _redChannel = ChannelType.Float;
                    _greenChannel = ChannelType.Float;
                    _blueChannel = ChannelType.Float;
                    _alphaChannel = ChannelType.Float;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.R16i:
                case TextureFormat.R32i:
                case TextureFormat.R8i:
                    _redChannel = ChannelType.Int;
                    _greenChannel = ChannelType.None;
                    _blueChannel = ChannelType.None;
                    _alphaChannel = ChannelType.None;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.Rg16i:
                case TextureFormat.Rg32i:
                case TextureFormat.Rg8i:
                    _redChannel = ChannelType.Int;
                    _greenChannel = ChannelType.Int;
                    _blueChannel = ChannelType.None;
                    _alphaChannel = ChannelType.None;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.Rgb16i:
                case TextureFormat.Rgb32i:
                case TextureFormat.Rgb8i:
                    _redChannel = ChannelType.Int;
                    _greenChannel = ChannelType.Int;
                    _blueChannel = ChannelType.Int;
                    _alphaChannel = ChannelType.None;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.Rgba16i:
                case TextureFormat.Rgba32i:
                case TextureFormat.Rgba8i:
                    _redChannel = ChannelType.Int;
                    _greenChannel = ChannelType.Int;
                    _blueChannel = ChannelType.Int;
                    _alphaChannel = ChannelType.Int;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.R16ui:
                case TextureFormat.R32ui:
                case TextureFormat.R8ui:
                    _redChannel = ChannelType.Uint;
                    _greenChannel = ChannelType.None;
                    _blueChannel = ChannelType.None;
                    _alphaChannel = ChannelType.None;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.Rg16ui:
                case TextureFormat.Rg32ui:
                case TextureFormat.Rg8ui:
                    _redChannel = ChannelType.Uint;
                    _greenChannel = ChannelType.Uint;
                    _blueChannel = ChannelType.None;
                    _alphaChannel = ChannelType.None;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.Rgb16ui:
                case TextureFormat.Rgb32ui:
                case TextureFormat.Rgb8ui:
                    _redChannel = ChannelType.Uint;
                    _greenChannel = ChannelType.Uint;
                    _blueChannel = ChannelType.Uint;
                    _alphaChannel = ChannelType.None;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.Rgba16ui:
                case TextureFormat.Rgba32ui:
                case TextureFormat.Rgba8ui:
                case TextureFormat.Rgb10A2ui:
                    _redChannel = ChannelType.Uint;
                    _greenChannel = ChannelType.Uint;
                    _blueChannel = ChannelType.Uint;
                    _alphaChannel = ChannelType.Uint;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.CompressedSignedRedRgtc1:
                case TextureFormat.R16Snorm:
                case TextureFormat.R8Snorm:
                    _redChannel = ChannelType.Normalised;
                    _greenChannel = ChannelType.None;
                    _blueChannel = ChannelType.None;
                    _alphaChannel = ChannelType.None;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.CompressedSignedRgRgtc2:
                case TextureFormat.Rg16Snorm:
                case TextureFormat.Rg8Snorm:
                    _redChannel = ChannelType.Normalised;
                    _greenChannel = ChannelType.Normalised;
                    _blueChannel = ChannelType.None;
                    _alphaChannel = ChannelType.None;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.Rgb16Snorm:
                case TextureFormat.Rgb8Snorm:
                    _redChannel = ChannelType.Normalised;
                    _greenChannel = ChannelType.Normalised;
                    _blueChannel = ChannelType.Normalised;
                    _alphaChannel = ChannelType.None;
                    _depthChannel = ChannelType.None;
                    break;

                case TextureFormat.Rgba16Snorm:
                case TextureFormat.Rgba8Snorm:
                    _redChannel = ChannelType.Normalised;
                    _greenChannel = ChannelType.Normalised;
                    _blueChannel = ChannelType.Normalised;
                    _alphaChannel = ChannelType.Normalised;
                    _depthChannel = ChannelType.None;
                    break;
            }
        }

        private ChannelType _redChannel = ChannelType.None;
        /// <summary>
        /// The data type used to store the red component at base level.
        /// </summary>
        public ChannelType RedChannel => _redChannel;
        private ChannelType _greenChannel = ChannelType.None;
        /// <summary>
        /// The data type used to store the green component at base level.
        /// </summary>
        public ChannelType GreenChannel => _greenChannel;
        private ChannelType _blueChannel = ChannelType.None;
        /// <summary>
        /// The data type used to store the blue component at base level.
        /// </summary>
        public ChannelType BlueChannel => _blueChannel;
        private ChannelType _alphaChannel = ChannelType.None;
        /// <summary>
        /// The data type used to store the alpha component at base level.
        /// </summary>
        public ChannelType AlphaChannel => _alphaChannel;
        private ChannelType _depthChannel = ChannelType.None;
        /// <summary>
        /// The data type used to store the depth component at base level.
        /// </summary>
        public ChannelType DepthChannel => _depthChannel;

        /// <summary>
        /// The offset into the data store of the buffer bound to a buffer texture
        /// </summary>
        public int BufferOffset
        {
            get
            {
                Handle.Bind();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureBufferOffset, &output);

                return output;
            }
        }
        /// <summary>
        /// The size of the range of a data store of the buffer bound to a buffer texture.
        /// </summary>
        public int BufferSize
        {
            get
            {
                Handle.Bind();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureBufferSize, &output);

                return output;
            }
        }

        internal int _immutableLevels = 0;
        /// <summary>
        /// The number of immutable texture levels in a texture view.
        /// </summary>
        public int ImmutableLevels
        {
            get => _immutableLevels;
            set
            {
                _immutableLevels = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureImmutableLevels, value);
            }
        }
        internal int _minLayer = 0;
        /// <summary>
        /// The first level of a texture array view relative to its parent.
        /// </summary>
        public int MinLayer
        {
            get => _minLayer;
            set
            {
                _minLayer = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureViewMinLayer, value);
            }
        }
        internal int _minLevel = 0;
        /// <summary>
        /// The base level of a texture view relative to its parent.
        /// </summary>
        public int MinLevel
        {
            get => _minLevel;
            set
            {
                _minLevel = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureViewMinLevel, value);
            }
        }
        internal int _numLayers = 0;
        /// <summary>
        /// The number of layers in a texture array view.
        /// </summary>
        public int NumLayers
        {
            get => _numLayers;
            set
            {
                _numLayers = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureViewNumLayers, value);
            }
        }
        internal int _numLevels = 0;
        /// <summary>
        /// The number of levels of detail of a texture view.
        /// </summary>
        public int NumLevels
        {
            get => _numLevels;
            set
            {
                _numLevels = value;

                Handle.Bind();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureViewNumLevels, value);
            }
        }

        /// <summary>
        /// The number of bytes that make up <see cref="Handle"/>'s data.
        /// </summary>
        public int CompressedImageSize
        {
            get
            {
                Handle.Bind();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureCompressedImageSize, &output);

                return output;
            }
        }
        internal bool _immutableFormat = false;
        /// <summary>
        /// Returns <see cref="true"/> if <see cref="Handle"/> has an immutable format, otherwise <see cref="false"/>.
        /// </summary>
        public bool Immutable => _immutableFormat;

        /// <summary>
        /// Determines whether <see cref="Handle"/> is a layered texture.
        /// </summary>
        public bool Layered
        {
            get
            {
                return Handle.Target switch
                {
                    TextureTarget.CubeMap or TextureTarget.CubeMapArray or
                        TextureTarget.Multisample2DArray or TextureTarget.Texture1DArray or
                        TextureTarget.Texture2DArray => true,
                    _ => false,
                };
            }
        }
    }
}
