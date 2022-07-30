using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that manages a 2 dimensional multisample texture.
    /// </summary>
    public class Texture2DMultisample : TextureGL
    {
        /// <summary>
        /// Creates a 2 dimensional multisample texture with a set internal format.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        public Texture2DMultisample(TextureFormat format)
            : base(TextureTarget.Multisample2D)
        {
            InternalFormat = format;
        }
        internal Texture2DMultisample(uint id, TextureFormat format)
            : base(id, TextureTarget.Multisample2D, format)
        {
            InternalFormat = format;
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
        /// <summary>
        /// The number of samples in this multisample texture.
        /// </summary>
        public int Samples => Properties._samples;

        /// <summary>
        /// Binds a specified level of the texture to a texture slot.
        /// </summary>
        /// <param name="slot">The slot to bind to.</param>
        /// <param name="level">The level of the texture.</param>
        /// <param name="access">The access type for the texture.</param>
        public void Bind(uint slot, int level, AccessType access) => BindLevel(slot, level, false, 0, access);

        /// <summary>
        /// Creates the space for the texture data.
        /// </summary>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="samples">The number of smaples in the texture.</param>
        /// <param name="fixedSampleLocation"></param>
        public void CreateData(int width, int height, int samples, bool fixedSampleLocation)
        {
            
            TexImage2DMultisample(samples, InternalFormat, width, height, fixedSampleLocation);
        }
        /// <summary>
        /// Creates the storage for the texture data.
        /// </summary>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="samples">The number of smaples in the texture.</param>
        /// <param name="fixedSampleLocation"></param>
        public void CreateStorage(int width, int height, int samples, bool fixedSampleLocation)
        {
            
            TexStorage2DMultisample(samples, InternalFormat, width, height, fixedSampleLocation);
        }

        /// <summary>
        /// Returns the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level">The mipmap level.</param>
        /// <param name="outputFormat">The format of the output data.</param>
        public GLArray<T> GetData<T>(BaseFormat outputFormat, TextureData dataType) where T : unmanaged
        {
            
            return GetTexImage<T>(0, outputFormat, dataType);
        }

        public static Texture2DMultisample Create(TextureFormat format, int samples, int width, int height, WrapStyle wrapStyle)
        {
            Texture2DMultisample texture = new Texture2DMultisample(format);
            texture.CreateData(width, height, samples, true);
            texture.WrapStyle = wrapStyle;

            return texture;
        }
    }
}
