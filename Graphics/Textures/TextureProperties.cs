using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public unsafe sealed class TextureProperties : TexRenProperties
    {
        public TextureProperties(ITexture source)
            : base(source)
        {

        }
        public override ITexture Handle { get; }

        /// <summary>
        /// The data type used to store the alpha component at base level.
        /// </summary>
        public ChannelType AlphaChannel
        {
            get
            {
                Handle.Bind();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureAlphaType, &output);

                return (ChannelType)output;
            }
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
        /// <summary>
        /// The data type used to store the blue component at base level.
        /// </summary>
        public ChannelType BlueChannel
        {
            get
            {
                Handle.Bind();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureBlueType, &output);

                return (ChannelType)output;
            }
        }
        internal ColourF _border = ColourF.Zero;
        /// <summary>
        /// The border colour of the texture.
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
                float[] colour = new float[] { value.R, value.G, value.B, value.A };

                fixed (float* parameter = &colour[0])
                {
                    GL.TexParameterfv((uint)Handle.Target, GLEnum.TextureBorderColour, parameter);
                }
            }
        }
        /// <summary>
        /// The border colour of the texture.
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
                int[] colour = new int[] { value.R, value.G, value.B, value.A };

                fixed (int* parameter = &colour[0])
                {
                    GL.TexParameteriv((uint)Handle.Target, GLEnum.TextureBorderColour, parameter);
                }
            }
        }
        internal ComparisonFunction _compareFunc = ComparisonFunction.LessEqual;
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
        internal ComparisonMode _compareMode = ComparisonMode.None;
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
        /// The depth of the texture at base level.
        /// </summary>
        public int Depth
        {
            get => _depth;
        }
        internal DepthStencilMode _dsMode = DepthStencilMode.Depth;
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
        /// The data type used to store the depth component at base level.
        /// </summary>
        public ChannelType DepthChannel
        {
            get
            {
                Handle.Bind();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureDepthType, &output);

                return (ChannelType)output;
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
        /// <summary>
        /// The data type used to store the green component at base level.
        /// </summary>
        public ChannelType GreenChannel
        {
            get
            {
                Handle.Bind();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureGreenType, &output);

                return (ChannelType)output;
            }
        }
        internal double _lodBias = 0;
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
        internal TextureSampling _magFilter = TextureSampling.Blend;
        /// <summary>
        /// The texture magnification function used when the level-of-detail function determines that the texture should be magified.
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
        internal double _maxLod = 1000;
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
        internal TextureSampling _minFilter = TextureSampling.Blend;
        /// <summary>
        /// The texture minification function used when the level-of-detail function determines that the texture should be minified.
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
        internal double _minLod = -1000;
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
        /// <summary>
        /// The data type used to store the red component at base level.
        /// </summary>
        public ChannelType RedChannel
        {
            get
            {
                Handle.Bind();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureRedType, &output);

                return (ChannelType)output;
            }
        }
        internal Swizzle _redSwiz = Swizzle.Red;
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
        internal Swizzle _greenSwiz = Swizzle.Green;
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
        internal Swizzle _blueSwiz = Swizzle.Blue;
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
        internal Swizzle _alphaSwiz = Swizzle.Alpha;
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
        internal WrapStyle _wrapX = WrapStyle.Repeated;
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
        internal WrapStyle _wrapY = WrapStyle.Repeated;
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
        internal WrapStyle _wrapZ = WrapStyle.Repeated;
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
        /// The number of bytes that make up the texture's data.
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
        /// <summary>
        /// Returns <see cref="true"/> if the texture has an immutable format, otherwise <see cref="false"/>.
        /// </summary>
        public bool ImmutableFormat
        {
            get
            {
                Handle.Bind();
                int output;

                GL.GetTexParameteriv((uint)Handle.Target, GLEnum.TextureImmutableFormat, &output);

                return output > 0;
            }
        }
    }
}
