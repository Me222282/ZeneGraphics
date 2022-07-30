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
    public unsafe class Texture : TextureGL
    {
        /// <summary>
        /// Creates an empty texture.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        public Texture(TextureFormat format)
            : base(TextureTarget.Texture2D)
        {
            InternalFormat = format;
        }
        /// <summary>
        /// Create a texture from <paramref name="data"/>.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        /// <param name="data">The data to asign the texture.</param>
        public Texture(TextureFormat format, GLArray<Colour> data)
            : base(TextureTarget.Texture2D)
        {
            InternalFormat = format;
            Data = data;
        }
        /// <summary>
        /// Creates a texture from the data inside <paramref name="stream"/>.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        /// <param name="stream"></param>
        public Texture(TextureFormat format, Stream stream, bool close = false)
            : base(TextureTarget.Texture2D)
        {
            InternalFormat = format;

            byte[] data = Bitmap.ExtractData(stream, out int width, out int height, close);
            fixed (byte* ptr = &data[0])
            {
                TexImage2D(0, InternalFormat, width, height, BaseFormat.Rgba, TextureData.Byte, ptr);
            }
        }
        /// <summary>
        /// Create a texture from the data in the file located at <paramref name="path"/>.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        /// <param name="path">The path to a file containing the textures data.</param>
        public Texture(TextureFormat format, string path)
            : this(format, new FileStream(path, FileMode.Open), true)
        {
            
        }

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
                if (GL.Version >= 4.5)
                {
                    return GetTextureSubImage<Colour>(0, x, y, 0, 1, 1, 1, BaseFormat.Rgba, TextureData.Byte)[0];
                }

                return GetTexImage<Colour>(0, BaseFormat.Rgba, TextureData.Byte)[x, y];
            }
            set
            {
                
                TexSubImage2D(0, x, y, 1, 1, BaseFormat.Rgba, TextureData.Byte, &value);
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
                if (GL.Version >= 4.5)
                {
                    return GetTextureSubImage<Colour>(0, x, y, 0, width, height, 1, BaseFormat.Rgba, TextureData.Byte);
                }

                return GetTexImage<Colour>(0, BaseFormat.Rgba, TextureData.Byte).SubSection(x, y, width, height);
            }
            set
            {
                
                TexSubImage2D(0, x, y, width, height, BaseFormat.Rgba, TextureData.Byte, value);
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
                return GetTexImage<Colour>(0, BaseFormat.Rgba, TextureData.Byte);
            }
            set
            {
                TexImage2D(0, InternalFormat, value.Width, value.Height, BaseFormat.Rgba, TextureData.Byte, value);
                MipMaped = false;
            }
        }

        public bool MipMaped { get; private set; } = false;
        /// <summary>
        /// Create all the levels for a mipmap texture.
        /// </summary>
        [OpenGLSupport(3.0)]
        public void GenerateMipMap()
        {
            GenerateMipmap();
            MipMaped = true;
        }

        /// <summary>
        /// The base texture mipmap level.
        /// </summary>
        public int BaseLevel
        {
            get => Properties._baseLevel;
            set => Properties.BaseLevel = value;
        }
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
        /// The height of the texture at base level.
        /// </summary>
        public int Height => Properties._height;
        /// <summary>
        /// A fixed bias that is to be added to the level-of-detail parameter before texture sampling.
        /// </summary>
        public double LodBias
        {
            get => Properties.LodBias;
            set => Properties.LodBias = value;
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
        /// The minimum value for the level-of-detail parameter.
        /// </summary>
        public double MinLod
        {
            get => Properties.MinLod;
            set => Properties.MinLod = value;
        }
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
        public int Width => Properties._width;
        /// <summary>
        /// The wrapping function used on when over sampled.
        /// </summary>
        public WrapStyle WrapStyle
        {
            get => Properties.WrapX;
            set
            {
                Properties.WrapX = value;
                Properties.WrapY = value;
            }
        }
        /// <summary>
        /// The sample function used when the level-fo-detail dertermines that the texture should use minified or magnified sampling.
        /// </summary>
        public TextureSampling Filter
        {
            get
            {
                return Properties.MinFilter;
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
                Properties.MagFilter = filter;

                if (MipMaped)
                {
                    // Minification filter
                    TextureSampling min = value switch
                    {
                        TextureSampling.Blend => TextureSampling.BlendMipMapBlend,
                        TextureSampling.Nearest => TextureSampling.NearestMipMapNearest,
                        _ => value
                    };
                    Properties.MinFilter = min;
                    return;
                }

                Properties.MinFilter = filter;
            }
        }
    }
}
