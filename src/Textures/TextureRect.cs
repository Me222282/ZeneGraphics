using StbImageSharp;
using System;
using System.IO;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that manages a 2 dimensional unmipmaped texture.
    /// </summary>
    public class TextureRect : TextureGL
    {
        /// <summary>
        /// Creates a 2 dimensional texture with a set internal format.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        /// <param name="dataType">The type of data that is going to be passed to OpenGL.</param>
        public TextureRect(TextureFormat format, TextureData dataType)
            : base(TextureTarget.Rectangle)
        {
            InternalFormat = format;
            DataType = dataType;
        }
        internal TextureRect(uint id, TextureFormat format, TextureData dataType)
            : base(id, TextureTarget.Rectangle, format)
        {
            InternalFormat = format;
            DataType = dataType;
        }

        /// <summary>
        /// The type of data being inputed into the texture.
        /// </summary>
        public TextureData DataType { get; set; }

        /// <summary>
        /// Creats and filles the data inside the texture with <paramref name="data"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="inputFormat">The format of <paramref name="data"/>.</param>
        /// <param name="data">The data to set the texture with.</param>
        public void SetData<T>(int width, int height, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged
        {
            
            TexImage2D(0, InternalFormat, width, height, inputFormat, DataType, data);
        }

        /// <summary>
        /// Changes the data in a section of the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">The x location of the region.</param>
        /// <param name="y">The y location of the region.</param>
        /// <param name="width">The width of the region.</param>
        /// <param name="height">The height of the region.</param>
        /// <param name="inputFormat">The format of <paramref name="data"/>.</param>
        /// <param name="data">The data to set the region with.</param>
        public void EditData<T>(int x, int y, int width, int height, BaseFormat inputFormat, GLArray<T> data) where T : unmanaged
        {
            
            TexSubImage2D(0, x, y, width, height, inputFormat, DataType, data);
        }

        /// <summary>
        /// Creates the storage space for a texture.
        /// </summary>
        /// <param name="width">The width of the space.</param>
        /// <param name="height">The height of the space.</param>
        public void CreateStorage(int width, int height)
        {
            
            TexStorage2D(1, InternalFormat, width, height);
        }

        /// <summary>
        /// Gets the data stored in the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="outputFormat">The format of the returned data.</param>
        /// <returns></returns>
        public GLArray<T> GetData<T>(BaseFormat outputFormat) where T : unmanaged
        {
            return GetTexImage<T>(0, outputFormat, DataType);
        }
        /// <summary>
        /// Get a section of the data stored in the texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">The x location of the region.</param>
        /// <param name="y">The y location of the region.</param>
        /// <param name="width">The width of the region.</param>
        /// <param name="height">The height of the region.</param>
        /// <param name="outputFormat">The format of the returned data.</param>
        /// <returns></returns>
        public GLArray<T> GetDataSection<T>(int x, int y, int width, int height, BaseFormat outputFormat) where T : unmanaged
        {
            return GetTextureSubImage<T>(0, x, y, 0, width, height, 1, outputFormat, DataType);
        }

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
        /// <param name="x">The x offset to write to.</param>
        /// <param name="y">The y offset to write to.</param>
        public void CopyTexture(ITexture source, int srcLevel, int srcX, int srcY, int srcZ, int width, int height, int x, int y)
        {
            
            CopyImageSubData(source, srcLevel, srcX, srcY, srcZ, width, height, 1, 0, x, y, 0);
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
        public void CopyTexture(TextureRect source, int srcX, int srcY, int width, int height, int x, int y)
        {
            
            CopyImageSubData(source, 0, srcX, srcY, 0, width, height, 1, 0, x, y, 0);
        }

        /// <summary>
        /// The internal storage resolution of the alpha component at base level.
        /// </summary>
        public int AlphaSize => Properties.AlphaSize;
        /// <summary>
        /// The data type used to store the alpha component at base level.
        /// </summary>
        public ChannelType AlphaChannel => Properties.AlphaChannel;
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
        /// The texture magnification function used when the level-of-detail function determines that the texture should be magified.
        /// </summary>
        public TextureSampling MagFilter
        {
            get => Properties.MagFilter;
            set => Properties.MagFilter = value;
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
        public int Width => Properties._width;
        /// <summary>
        /// The wrapping function used on the x coordinate.
        /// </summary>
        public WrapStyle WrapX
        {
            get => Properties.WrapX;
            set => Properties.WrapX = value;
        }
        /// <summary>
        /// The wrapping function used on the y coordinate.
        /// </summary>
        public WrapStyle WrapY
        {
            get => Properties.WrapY;
            set => Properties.WrapY = value;
        }
        /// <summary>
        /// The wrapping function used on all axes.
        /// </summary>
        public WrapStyle WrapStyle
        {
            set
            {
                Properties.WrapX = value;
                Properties.WrapY = value;
            }
        }

        public static TextureRect Create(GLArray<Colour> data, WrapStyle wrapStyle, TextureSampling textureQuality)
        {
            TextureRect texture = new TextureRect(TextureFormat.Rgba8, TextureData.Byte);

            texture.SetData(data.Width, data.Height, BaseFormat.Rgba, data);

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

            return texture;
        }
        public static TextureRect Create(GLArray<ColourF> data, WrapStyle wrapStyle, TextureSampling textureQuality)
        {
            TextureRect texture = new TextureRect(TextureFormat.Rgba32f, TextureData.Float);

            texture.SetData(data.Width, data.Height, BaseFormat.Rgba, data);

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

            return texture;
        }
        public static TextureRect Create(GLArray<Colour3> data, WrapStyle wrapStyle, TextureSampling textureQuality)
        {
            TextureRect texture = new TextureRect(TextureFormat.Rgb8, TextureData.Byte);

            texture.SetData(data.Width, data.Height, BaseFormat.Rgb, data);

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

            return texture;
        }
        public static TextureRect Create(GLArray<ColourF3> data, WrapStyle wrapStyle, TextureSampling textureQuality)
        {
            TextureRect texture = new TextureRect(TextureFormat.Rgb32f, TextureData.Float);

            texture.SetData(data.Width, data.Height, BaseFormat.Rgb, data);

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

            return texture;
        }

        public static TextureRect Create(Bitmap image, WrapStyle wrapStyle, TextureSampling textureQuality)
        {
            TextureRect texture = new TextureRect(TextureFormat.Rgba8, TextureData.Byte);

            texture.SetData(image.Width, image.Height, BaseFormat.Rgba, (GLArray<Colour>)image);

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

            return texture;
        }
        public static TextureRect Create(Stream stream, WrapStyle wrapStyle, TextureSampling textureQuality, bool close = false)
        {
            TextureRect texture = new TextureRect(TextureFormat.Rgba8, TextureData.Byte);

            // Load image
            ImageResult imageData = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

            if (close) { stream.Close(); }

            texture.SetData(imageData.Width, imageData.Height, BaseFormat.Rgba, new GLArray<byte>(imageData.Width * 4, imageData.Height, 1, imageData.Data));
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

            return texture;
        }
        public static TextureRect Create(string path, WrapStyle wrapStyle, TextureSampling textureQuality) =>
            Create(new FileStream(path, FileMode.Open), wrapStyle, textureQuality, true);
    }
}
