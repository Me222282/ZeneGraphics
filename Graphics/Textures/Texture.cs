using System;
using System.IO;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// A basic 2d texture object that passes data to and from the GPU by the <see cref="Colour"/> struct.
    /// </summary>
    [OpenGLSupport(3.0)]
    public unsafe class Texture : ITexture
    {
        /// <summary>
        /// Creates an empty texture.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        public Texture(TextureFormat format)
        {
            _texture = new TextureGL(TextureTarget.Texture2D);
            InternalFormat = format;
        }
        /// <summary>
        /// Create a texture from <paramref name="data"/>.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        /// <param name="data">The data to asign the texture.</param>
        public Texture(TextureFormat format, GLArray<Colour> data)
        {
            _texture = new TextureGL(TextureTarget.Texture2D);
            InternalFormat = format;
            Data = data;
        }
        /// <summary>
        /// Create a texture from <paramref name="data"/>.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        /// <param name="data">The <see cref="Bitmap"/> containing the data for the texture.</param>
        public Texture(TextureFormat format, Bitmap data)
            : this(format, data.Data)
        {

        }
        /// <summary>
        /// Creates a texture from the data inside <paramref name="stream"/>.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        /// <param name="stream"></param>
        public Texture(TextureFormat format, Stream stream)
        {
            _texture = new TextureGL(TextureTarget.Texture2D);
            InternalFormat = format;

            byte[] data = Bitmap.ExtractData(stream, out int width, out int height);
            _texture.TexImage2D(0, InternalFormat, width, height, BaseFormat.Rgba, TextureData.Byte, new GLArray<byte>(width, height, 1, data));
        }
        /// <summary>
        /// Create a texture from the data in the file located at <paramref name="path"/>.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        /// <param name="path">The path to a file containing the textures data.</param>
        public Texture(TextureFormat format, string path)
        {
            _texture = new TextureGL(TextureTarget.Texture2D);
            InternalFormat = format;

            byte[] data = Bitmap.ExtractData(path, out int width, out int height);
            _texture.TexImage2D(0, InternalFormat, width, height, BaseFormat.Rgba, TextureData.Byte, new GLArray<byte>(width, height, 1, data));
        }

        public uint Id => _texture.Id;
        public uint ReferanceSlot => _texture.ReferanceSlot;

        private readonly TextureGL _texture;

        public TextureTarget Target => _texture.Target;
        public TextureFormat InternalFormat { get; }

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
        /// Gets or sets a signal pixel of the texture.
        /// </summary>
        /// <param name="x">The 0-based x location of the pixel.</param>
        /// <param name="y">The 0-based y location of the pixel.</param>
        /// <returns></returns>
        [OpenGLSupport(1.1)]
        public Colour this[int x, int y]
        {
            get
            {
                
                return _texture.GetTexImage<Colour>(0, BaseFormat.Rgba, TextureData.Byte)[x, y];
            }
            set
            {
                
                _texture.TexSubImage2D(0, x, y, 1, 1, BaseFormat.Rgba, TextureData.Byte, &value);
            }
        }
        /// <summary>
        /// Gets or sets a section of the texture;
        /// </summary>
        /// <param name="x">The 0-based x location of the pixel.</param>
        /// <param name="y">The 0-based y location of the pixel.</param>
        /// <param name="width">The width of the region.</param>
        /// <param name="height">The height of the region.</param>
        /// <returns></returns>
        [OpenGLSupport(1.1)]
        public GLArray<Colour> this[int x, int y, int width, int height]
        {
            get
            {
                
                return _texture.GetTexImage<Colour>(0, BaseFormat.Rgba, TextureData.Byte).SubSection(x, y, width, height);
            }
            set
            {
                
                _texture.TexSubImage2D(0, x, y, width, height, BaseFormat.Rgba, TextureData.Byte, value);
            }
        }
        /// <summary>
        /// Gets or sets the all of the texture's data.
        /// </summary>
        [OpenGLSupport(1.0)]
        public GLArray<Colour> Data
        {
            get
            {
                
                return _texture.GetTexImage<Colour>(0, BaseFormat.Rgba, TextureData.Byte);
            }
            set
            {
                
                _texture.TexImage2D(0, InternalFormat, value.Width, value.Height, BaseFormat.Rgba, TextureData.Byte, value);
            }
        }

        public bool MipMaped { get; private set; } = false;
        /// <summary>
        /// Create all the levels for a mipmap texture.
        /// </summary>
        [OpenGLSupport(3.0)]
        public void GenerateMipMap()
        {
            _texture.GenerateMipmap();
            MipMaped = true;
        }

        private const int _baseLevel = 0;
        /// <summary>
        /// The base texture mipmap level.
        /// </summary>
        public int BaseLevel
        {
            get
            {
                

                return _texture.GetBaseLevel();
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
        /// The wrapping function used on when over sampled.
        /// </summary>
        public WrapStyle WrapStyle
        {
            set
            {
                

                _texture.SetWrapS(value);
                _texture.SetWrapT(value);
            }
            get
            {
                return _texture.GetWrapS();
            }
        }
        /// <summary>
        /// The smaple function used when the level-fo-detail dertermines that the texture should use minified or magnified sampleing.
        /// </summary>
        public TextureSampling Filter
        {
            get
            {
                return _texture.GetMinFilter();
            }
            set
            {
                TextureSampling filter = value switch
                {
                    TextureSampling.BlendMipMapBlend => TextureSampling.Blend,
                    TextureSampling.NearestMipMapNearest => TextureSampling.Nearest,
                    TextureSampling.BlendMipMapNearest => TextureSampling.Blend,
                    TextureSampling.NearestMipMapBlend => TextureSampling.Nearest,
                    _ => value
                };

                // Magnification filter
                _texture.SetMagFilter(filter);

                if (MipMaped)
                {
                    // Minification filter
                    TextureSampling min = value switch
                    {
                        TextureSampling.Blend => TextureSampling.BlendMipMapBlend,
                        TextureSampling.Nearest => TextureSampling.NearestMipMapNearest,
                        _ => value
                    };
                    _texture.SetMinFilter(min);
                    return;
                }

                _texture.SetMinFilter(filter);
            }
        }
    }
}
