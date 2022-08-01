using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    // An object that manages an array of 2 dimensional multismaple textures.
    public class Texture2DArrayMultisample : TextureGL
    {
        /// <summary>
        /// Creates an array of 2 dimensional multisample textures with a set internal format.
        /// </summary>
        /// <param name="format">The internal format of the texture.</param>
        /// <param name="dataType">The type of data that is going to be passed to OpenGL.</param>
        public Texture2DArrayMultisample(TextureFormat format)
            : base(TextureTarget.Multisample2DArray)
        {
            _targetFormat = format;
        }

        private readonly TextureFormat _targetFormat;

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
        /// The depth of the texture at base level.
        /// </summary>
        public int ArrayLength => Properties._depth;
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
        /// The wrapping function used on the z coordinate.
        /// </summary>
        public WrapStyle WrapZ
        {
            get => Properties.WrapZ;
            set => Properties.WrapZ = value;
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
                Properties.WrapZ = value;
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
        /// <param name="arrayLenght">The length of the texture array.</param>
        /// <param name="samples">The number of smaples in the texture.</param>
        /// <param name="fixedSampleLocation"></param>
        public void CreateData(int width, int height, int arrayLenght, int samples, bool fixedSampleLocation)
        {
            
            TexImage3DMultisample(samples, _targetFormat, width, height, arrayLenght, fixedSampleLocation);
        }
        /// <summary>
        /// Creates the storage for the texture data.
        /// </summary>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <param name="arrayLenght">The length of the texture array.</param>
        /// <param name="samples">The number of smaples in the texture.</param>
        /// <param name="fixedSampleLocation"></param>
        public void CreateStorage(int width, int height, int arrayLenght, int samples, bool fixedSampleLocation)
        {
            
            TexStorage3DMultisample(samples, _targetFormat, width, height, arrayLenght, fixedSampleLocation);
        }

        /// <summary>
        /// Returns the data stored in this texture.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="outputFormat">The format of the output data.</param>
        /// <param name="dataType">The type of data being returned.</param>
        public GLArray<T> GetData<T>(BaseFormat outputFormat, TextureData dataType) where T : unmanaged
        {
            return GetTexImage<T>(0, outputFormat, dataType);
        }

        public static Texture2DArrayMultisample Create(TextureFormat format, int samples, int width, int height, int arrayLength, WrapStyle wrapStyle)
        {
            Texture2DArrayMultisample texture = new Texture2DArrayMultisample(format);
            texture.CreateData(width, height, arrayLength, samples, true);
            texture.WrapStyle = wrapStyle;

            return texture;
        }
    }
}
