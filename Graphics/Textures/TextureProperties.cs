using Zene.Graphics.Base;
using Zene.Graphics.Base.Extensions;
using Zene.Structs;

namespace Zene.Graphics.GLObjects.Textures
{
    public unsafe struct TextureProperties
    {
        public TextureProperties(ITexture source)
        {
            Handle = source;

            _dsMode = DepthStencilMode.Depth;
            _magFilter = TextureSampling.Blend;
            _minFilter = TextureSampling.Blend;
            _minLod = -1000;
            _lodBias = 0;
            _maxLod = 1000;
            _baseLevel = 0;
            _maxLevel = 0;
            _redSwiz = Swizzle.Red;
            _greenSwiz = Swizzle.Green;
            _blueSwiz = Swizzle.Blue;
            _alphaSwiz = Swizzle.Alpha;
            _wrapX = WrapStyle.Repeated;
            _wrapY = WrapStyle.Repeated;
            _wrapZ = WrapStyle.Repeated;
            _border = ColourF.Zero;
            _compareMode = ComparisonMode.None;
            _compareFunc = ComparisonFunction.LessEqual;
            _minLevel = 0;
            _minLayer = 0;
            _numLevels = 0;
            _numLayers = 0;
            _immutableLevels = 0;
        }
        public ITexture Handle;

        /// <summary>
        /// The internal storage resolution of the alpha component at base level.
        /// </summary>
        public int AlphaSize
        {
            get
            {
                Handle.SetGLContext();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureAlphaSize, &output);

                return output;
            }
        }
        /// <summary>
        /// The data type used to store the alpha component at base level.
        /// </summary>
        public ChannelType AlphaChannel
        {
            get
            {
                Handle.SetGLContext();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureAlphaType, &output);

                return (ChannelType)output;
            }
        }
        private int _baseLevel;
        /// <summary>
        /// The base texture mipmap level.
        /// </summary>
        public int BaseLevel
        {
            get => _baseLevel;
            set
            {
                _baseLevel = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureBaseLevel, value);
            }
        }
        /// <summary>
        /// The internal storage resolution of the blue component at base level.
        /// </summary>
        public int BlueSize
        {
            get
            {
                Handle.SetGLContext();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureBlueSize, &output);

                return output;
            }
        }
        /// <summary>
        /// The data type used to store the blue component at base level.
        /// </summary>
        public ChannelType BlueChannel
        {
            get
            {
                Handle.SetGLContext();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureBlueType, &output);

                return (ChannelType)output;
            }
        }
        private ColourF _border;
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

                Handle.SetGLContext();
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

                Handle.SetGLContext();
                int[] colour = new int[] { value.R, value.G, value.B, value.A };

                fixed (int* parameter = &colour[0])
                {
                    GL.TexParameteriv((uint)Handle.Target, GLEnum.TextureBorderColour, parameter);
                }
            }
        }
        private ComparisonFunction _compareFunc;
        /// <summary>
        /// The comparison operator used when <see cref="ComparisonMode"/> is set to <see cref="ComparisonMode.CompareToDepth"/>.
        /// </summary>
        public ComparisonFunction ComparisonFunction
        {
            get => _compareFunc;
            set
            {
                _compareFunc = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureCompareFunc, (int)value);
            }
        }
        private ComparisonMode _compareMode;
        /// <summary>
        /// The texture comparison mode for depth textures.
        /// </summary>
        public ComparisonMode ComparisonMode
        {
            get => _compareMode;
            set
            {
                _compareMode = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureCompareMode, (int)value);
            }
        }
        /// <summary>
        /// The depth of the texture at base level.
        /// </summary>
        public int Depth
        {
            get
            {
                Handle.SetGLContext();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureDepth, &output);

                return output;
            }
        }
        /// <summary>
        /// The internal storage resolution of the depth component at base level.
        /// </summary>
        public int DepthSize
        {
            get
            {
                Handle.SetGLContext();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureDepthSize, &output);

                return output;
            }
        }
        private DepthStencilMode _dsMode;
        /// <summary>
        /// The mode used to read from depth-stencil format textures.
        /// </summary>
        public DepthStencilMode DepthStencilMode
        {
            get => _dsMode;
            set
            {
                _dsMode = value;

                Handle.SetGLContext();
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
                Handle.SetGLContext();
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
                Handle.SetGLContext();
                int output;

                GL.GetTexParameteriv((uint)Handle.Target, GLEnum.ImageFormatCompatibilityType, &output);

                return (FormatCompatibilityType)output;
            }
        }
        /// <summary>
        /// The internal storage resolution of the green component at base level.
        /// </summary>
        public int GreenSize
        {
            get
            {
                Handle.SetGLContext();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureGreenSize, &output);

                return output;
            }
        }
        /// <summary>
        /// The data type used to store the green component at base level.
        /// </summary>
        public ChannelType GreenChannel
        {
            get
            {
                Handle.SetGLContext();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureGreenType, &output);

                return (ChannelType)output;
            }
        }
        /// <summary>
        /// The height of the texture at base level.
        /// </summary>
        public int Height
        {
            get
            {
                Handle.SetGLContext();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureHeight, &output);

                return output;
            }
        }
        private double _lodBias;
        /// <summary>
        /// A fixed bias that is to be added to the level-of-detail parameter before texture sampling.
        /// </summary>
        public double LodBias
        {
            get => _lodBias;
            set
            {
                _lodBias = value;

                Handle.SetGLContext();
                GL.TexParameterf((uint)Handle.Target, GLEnum.TextureLodBias, (float)value);
            }
        }
        private TextureSampling _magFilter;
        /// <summary>
        /// The texture magnification function used when the level-of-detail function determines that the texture should be magified.
        /// </summary>
        public TextureSampling MagFilter
        {
            get => _magFilter;
            set
            {
                _magFilter = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureMagFilter, (int)value);
            }
        }
        private int _maxLevel;
        /// <summary>
        /// The maximum texture mipmap array level.
        /// </summary>
        public int MaxLevel
        {
            get => _maxLevel;
            set
            {
                _maxLevel = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureMaxLevel, value);
            }
        }
        private double _maxLod;
        /// <summary>
        /// The maximum value for the level-of-detail parameter.
        /// </summary>
        public double MaxLod
        {
            get => _maxLod;
            set
            {
                _maxLod = value;

                Handle.SetGLContext();
                GL.TexParameterf((uint)Handle.Target, GLEnum.TextureMaxLod, (float)value);
            }
        }
        private TextureSampling _minFilter;
        /// <summary>
        /// The texture minification function used when the level-of-detail function determines that the texture should be minified.
        /// </summary>
        public TextureSampling MinFilter
        {
            get => _minFilter;
            set
            {
                _minFilter = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureMinFilter, (int)value);
            }
        }
        private double _minLod;
        /// <summary>
        /// The minimum value for the level-of-detail parameter.
        /// </summary>
        public double MinLod
        {
            get => _minLod;
            set
            {
                _minLod = value;

                Handle.SetGLContext();
                GL.TexParameterf((uint)Handle.Target, GLEnum.TextureMinLod, (float)value);
            }
        }
        /// <summary>
        /// The internal storage resolution of the red component at base level.
        /// </summary>
        public int RedSize
        {
            get
            {
                Handle.SetGLContext();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureRedSize, &output);

                return output;
            }
        }
        /// <summary>
        /// The data type used to store the red component at base level.
        /// </summary>
        public ChannelType RedChannel
        {
            get
            {
                Handle.SetGLContext();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureRedType, &output);

                return (ChannelType)output;
            }
        }
        private Swizzle _redSwiz;
        /// <summary>
        /// The swizzle that will be applied to the red component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle RedSwizzle
        {
            get => _redSwiz;
            set
            {
                _redSwiz = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureSwizzleR, (int)value);
            }
        }
        private Swizzle _greenSwiz;
        /// <summary>
        /// The swizzle that will be applied to the green component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle GreenSwizzle
        {
            get => _greenSwiz;
            set
            {
                _greenSwiz = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureSwizzleG, (int)value);
            }
        }
        private Swizzle _blueSwiz;
        /// <summary>
        /// The swizzle that will be applied to the blue component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle BlueSwizzle
        {
            get => _blueSwiz;
            set
            {
                _blueSwiz = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureSwizzleB, (int)value);
            }
        }
        private Swizzle _alphaSwiz;
        /// <summary>
        /// The swizzle that will be applied to the alpha component of a texel before it is returned to the shader.
        /// </summary>
        public Swizzle AlphaSwizzle
        {
            get => _alphaSwiz;
            set
            {
                _alphaSwiz = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureSwizzleA, (int)value);
            }
        }
        /// <summary>
        /// The width of the texture at base level.
        /// </summary>
        public int Width
        {
            get
            {
                Handle.SetGLContext();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureWidth, &output);

                return output;
            }
        }
        private WrapStyle _wrapX;
        /// <summary>
        /// The wrapping function used on the x coordinate.
        /// </summary>
        public WrapStyle WrapX
        {
            get => _wrapX;
            set
            {
                _wrapX = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureWrapS, (int)value);
            }
        }
        private WrapStyle _wrapY;
        /// <summary>
        /// The wrapping function used on the y coordinate.
        /// </summary>
        public WrapStyle WrapY
        {
            get => _wrapY;
            set
            {
                _wrapY = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureWrapT, (int)value);
            }
        }
        private WrapStyle _wrapZ;
        /// <summary>
        /// The wrapping function used on the z coordinate.
        /// </summary>
        public WrapStyle WrapZ
        {
            get => _wrapZ;
            set
            {
                _wrapZ = value;

                Handle.SetGLContext();
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
                Handle.SetGLContext();
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
                Handle.SetGLContext();
                int output;

                GL.GetTexLevelParameteriv((uint)Handle.Target, _baseLevel, GLEnum.TextureBufferSize, &output);

                return output;
            }
        }

        private int _immutableLevels;
        /// <summary>
        /// The number of immutable texture levels in a texture view.
        /// </summary>
        public int ImmutableLevels
        {
            get => _immutableLevels;
            set
            {
                _immutableLevels = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureImmutableLevels, value);
            }
        }
        private int _minLayer;
        /// <summary>
        /// The first level of a texture array view relative to its parent.
        /// </summary>
        public int MinLayer
        {
            get => _minLayer;
            set
            {
                _minLayer = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureViewMinLayer, value);
            }
        }
        private int _minLevel;
        /// <summary>
        /// The base level of a texture view relative to its parent.
        /// </summary>
        public int MinLevel
        {
            get => _minLevel;
            set
            {
                _minLevel = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureViewMinLevel, value);
            }
        }
        private int _numLayers;
        /// <summary>
        /// The number of layers in a texture array view.
        /// </summary>
        public int NumLayers
        {
            get => _numLayers;
            set
            {
                _numLayers = value;

                Handle.SetGLContext();
                GL.TexParameteri((uint)Handle.Target, GLEnum.TextureViewNumLayers, value);
            }
        }
        private int _numLevels;
        /// <summary>
        /// The number of levels of detail of a texture view.
        /// </summary>
        public int NumLevels
        {
            get => _numLevels;
            set
            {
                _numLevels = value;

                Handle.SetGLContext();
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
                Handle.SetGLContext();
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
                Handle.SetGLContext();
                int output;

                GL.GetTexParameteriv((uint)Handle.Target, GLEnum.TextureImmutableFormat, &output);

                return output > 0;
            }
        }
    }
}
