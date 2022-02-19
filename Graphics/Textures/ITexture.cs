using Zene.Graphics.Base;

namespace Zene.Graphics
{
    /// <summary>
    /// The formating of data for a texture.
    /// </summary>
    public enum TextureFormat : uint
    {
        Rgb = GLEnum.Rgb,
        Rgba = GLEnum.Rgba,
        R3G3B2 = GLEnum.R3G3B2,
        Rgb4 = GLEnum.Rgb4,
        Rgb5 = GLEnum.Rgb5,
        Rgb8 = GLEnum.Rgb8,
        Rgb10 = GLEnum.Rgb10,
        Rgb12 = GLEnum.Rgb12,
        Rgb16 = GLEnum.Rgb16,
        Rgba2 = GLEnum.Rgba2,
        Rgba4 = GLEnum.Rgba4,
        Rgb5A1 = GLEnum.Rgb5A1,
        Rgba8 = GLEnum.Rgba8,
        Rgb10A2 = GLEnum.Rgb10A2,
        Rgba12 = GLEnum.Rgba12,
        Rgba16 = GLEnum.Rgba16,
        R8 = GLEnum.R8,
        R16 = GLEnum.R16,
        Rg8 = GLEnum.Rg8,
        Rg16 = GLEnum.Rg16,
        R16f = GLEnum.R16f,
        R32f = GLEnum.R32f,
        Rg16f = GLEnum.Rg16f,
        Rg32f = GLEnum.Rg32f,
        R8i = GLEnum.R8i,
        R8ui = GLEnum.R8ui,
        R16i = GLEnum.R16i,
        R16ui = GLEnum.R16ui,
        R32i = GLEnum.R32i,
        R32ui = GLEnum.R32ui,
        Rg8i = GLEnum.Rg8i,
        Rg8ui = GLEnum.Rg8ui,
        Rg16i = GLEnum.Rg16i,
        Rg16ui = GLEnum.Rg16ui,
        Rg32i = GLEnum.Rg32i,
        Rg32ui = GLEnum.Rg32ui,
        Rgba32f = GLEnum.Rgba32f,
        Rgb32f = GLEnum.Rgb32f,
        Rgba16f = GLEnum.Rgba16f,
        Rgb16f = GLEnum.Rgb16f,
        R11fG11fB10f = GLEnum.R11fG11fB10f,
        Rgb9E5 = GLEnum.Rgb9E5,
        Srgb = GLEnum.Srgb,
        Srgb8 = GLEnum.Srgb8,
        SrgbAlpha = GLEnum.SrgbAlpha,
        Srgb8Alpha8 = GLEnum.Srgb8Alpha8,
        Rgba32ui = GLEnum.Rgba32ui,
        Rgb32ui = GLEnum.Rgb32ui,
        Rgba16ui = GLEnum.Rgba16ui,
        Rgb16ui = GLEnum.Rgb16ui,
        Rgba8ui = GLEnum.Rgba8ui,
        Rgb8ui = GLEnum.Rgb8ui,
        Rgba32i = GLEnum.Rgba32i,
        Rgb32i = GLEnum.Rgb32i,
        Rgba16i = GLEnum.Rgba16i,
        Rgb16i = GLEnum.Rgb16i,
        Rgba8i = GLEnum.Rgba8i,
        Rgb8i = GLEnum.Rgb8i,
        R8Snorm = GLEnum.R8Snorm,
        Rg8Snorm = GLEnum.Rg8Snorm,
        Rgb8Snorm = GLEnum.Rgb8Snorm,
        Rgba8Snorm = GLEnum.Rgba8Snorm,
        R16Snorm = GLEnum.R16Snorm,
        Rg16Snorm = GLEnum.Rg16Snorm,
        Rgb16Snorm = GLEnum.Rgb16Snorm,
        Rgba16Snorm = GLEnum.Rgba16Snorm,
        Rgb10A2ui = GLEnum.Rgb10A2ui,
        DepthComponent = GLEnum.DepthComponent,
        DepthComponent16 = GLEnum.DepthComponent16,
        DepthComponent24 = GLEnum.DepthComponent24,
        DepthComponent32 = GLEnum.DepthComponent32,
        DepthComponent32f = GLEnum.DepthComponent32f,
        DepthStencil = GLEnum.DepthStencil,
        Depth24Stencil8 = GLEnum.Depth24Stencil8,
        Depth32fStencil8 = GLEnum.Depth32fStencil8,
        CompressedRed = GLEnum.CompressedRed,
        CompressedRg = GLEnum.CompressedRg,
        CompressedRgb = GLEnum.CompressedRgb,
        CompressedRgba = GLEnum.CompressedRgba,
        CompressedSrgb = GLEnum.CompressedSrgb,
        CompressedSrgbAlpha = GLEnum.CompressedSrgbAlpha,
        CompressedRedRgtc1 = GLEnum.CompressedRedRgtc1,
        CompressedSignedRedRgtc1 = GLEnum.CompressedSignedRedRgtc1,
        CompressedRgRgtc2 = GLEnum.CompressedRgRgtc2,
        CompressedSignedRgRgtc2 = GLEnum.CompressedSignedRgRgtc2,
        CompressedRgbaBptcUnorm = GLEnum.CompressedRgbaBptcUnorm,
        CompressedSrgbAlphaBptcUnorm = GLEnum.CompressedSrgbAlphaBptcUnorm,
        CompressedRgbBptcSignedFloat = GLEnum.CompressedRgbBptcSignedFloat
    }
    /// <summary>
    /// Basic texture format types for data pased to OpenGL.
    /// </summary>
    public enum BaseFormat : uint
    {
        StencilIndex = GLEnum.StencilIndex,
        DepthComponent = GLEnum.DepthComponent,
        R = GLEnum.Red,
        G = GLEnum.Green,
        B = GLEnum.Blue,
        Rgb = GLEnum.Rgb,
        Rgba = GLEnum.Rgba,
        Bgr = GLEnum.Bgr,
        Bgra = GLEnum.Bgra,
        Rg = GLEnum.Rg,
        RgInteger = GLEnum.RgInteger,
        DepthStencil = GLEnum.DepthStencil,
        RedInteger = GLEnum.RedInteger,
        GreenInteger = GLEnum.GreenInteger,
        BlueInteger = GLEnum.BlueInteger,
        RgbInteger = GLEnum.RgbInteger,
        RgbaInteger = GLEnum.RgbaInteger,
        BgrInteger = GLEnum.BgrInteger,
        BgraInteger = GLEnum.BgraInteger
    }
    /// <summary>
    /// Data types that are accepted by textures.
    /// </summary>
    public enum TextureData : uint
    {
        SByte = GLEnum.Byte,
        Byte = GLEnum.UByte,
        Short = GLEnum.Short,
        UShort = GLEnum.UShort,
        Int = GLEnum.Int,
        UInt = GLEnum.UInt,
        Float = GLEnum.Float,
        HalfFloat = GLEnum.HalfFloat,
        UByte332 = GLEnum.UByte332,
        UShort4444 = GLEnum.UShort4444,
        UShort5551 = GLEnum.UShort5551,
        UInt8888 = GLEnum.UInt8888,
        UInt1010102 = GLEnum.UInt1010102,
        UByte233Reversed = GLEnum.UByte233Rev,
        UShort565 = GLEnum.UShort565,
        UShort565Reversed = GLEnum.UShort565Rev,
        UShort4444Reversed = GLEnum.UShort4444Rev,
        UShort1555Reversed = GLEnum.UShort1555Rev,
        UInt8888Reversed = GLEnum.UInt8888Rev,
        UInt2101010Reversed = GLEnum.UInt2101010Rev,
        UInt248 = GLEnum.UInt248,
        UInt5999Rev = GLEnum.UInt5999Rev
    }

    /// <summary>
    /// The style for extened texture coordinates.
    /// </summary>
    public enum WrapStyle : uint
    {
        /// <summary>
        /// Oversampled coordinates refernce the closest in-bound pixel.
        /// </summary>
        EdgeClamp = GLEnum.ClampToEdge,
        /// <summary>
        /// Oversampled coordinates refernce the set border colour.
        /// </summary>
        BorderClamp = GLEnum.ClampToBorder,
        /// <summary>
        /// Oversampled coordinates first refernce a mirrored copy before acting like <see cref="EdgeClamp"/>.
        /// </summary>
        MirrorClamp = GLEnum.MirrorClampToEdge,
        /// <summary>
        /// Oversampled coordinates repeat the texture mirrored from the last.
        /// </summary>
        RepeatedMirror = GLEnum.MirroredRepeat,
        /// <summary>
        /// Oversampled coordinates repeat the texture.
        /// </summary>
        Repeated = GLEnum.Repeat
    }

    /// <summary>
    /// The sampling quality for a texture.
    /// </summary>
    public enum TextureSampling : uint
    {
        /// <summary>
        /// Nearst neighbour up-scalling and down-scalling.
        /// </summary>
        Nearest = GLEnum.Nearest,
        /// <summary>
        /// Interpolated up-scalling and down-scalling.
        /// </summary>
        Blend = GLEnum.Linear,
        /// <summary>
        /// Chooses the mipmap that most closely matches the size of the pixel being textured and uses
        /// the <see cref="Nearest"/> criterion to produce a texture value.
        /// </summary>
        NearestMipMapNearest = GLEnum.NearestMipmapNearest,
        /// <summary>
        /// Chooses the two mipmaps that most closely match the size of the pixel being textured and uses
        /// the <see cref="Nearest"/> criterion to produce a texture value from each mipmap. The final texture value is a weighted average of those two values.
        /// </summary>
        NearestMipMapBlend = GLEnum.NearestMipmapLinear,
        /// <summary>
        /// Chooses the mipmap that most closely matches the size of the pixel being textured and uses
        /// the <see cref="Blend"/> criterion to produce a texture value.
        /// </summary>
        BlendMipMapNearest = GLEnum.LinearMipmapNearest,
        /// <summary>
        /// Chooses the two mipmaps that most closely match the size of the pixel being textured and uses
        /// the <see cref="Blend"/> criterion to produce a texture value from each mipmap. The final texture value is a weighted average of those two values.
        /// </summary>
        BlendMipMapBlend = GLEnum.LinearMipmapLinear
    }

    /// <summary>
    /// The type of texture being targeted.
    /// </summary>
    public enum TextureTarget : uint
    {
        /// <summary>
        /// A one-dimensional texture.
        /// </summary>
        Texture1D = GLEnum.Texture1d,
        /// <summary>
        /// An array of one-dimensional textures.
        /// </summary>
        Texture1DArray = GLEnum.Texture1dArray,
        /// <summary>
        /// A two-dimensional texture.
        /// </summary>
        Texture2D = GLEnum.Texture2d,
        /// <summary>
        /// An array of two-dimensional textures.
        /// </summary>
        Texture2DArray = GLEnum.Texture2dArray,
        /// <summary>
        /// A three-dimensional texture.
        /// </summary>
        Texture3D = GLEnum.Texture3d,
        /// <summary>
        /// An array of <see cref="CubeMap"/>.
        /// </summary>
        CubeMapArray = GLEnum.TextureCubeMapArray,
        /// <summary>
        /// A two-dimensional texture without mipmaping.
        /// </summary>
        Rectangle = GLEnum.TextureRectangle,
        /// <summary>
        /// A one-dimensional texture whos data comes from an <see cref="IBuffer"/> object.
        /// </summary>
        Buffer = GLEnum.TextureBuffer,
        /// <summary>
        /// Six two-dimensional textures that form a cube.
        /// </summary>
        CubeMap = GLEnum.TextureCubeMap,
        /// <summary>
        /// A two-dimensional texture with multiple samples.
        /// </summary>
        Multisample2D = GLEnum.Texture2dMultisample,
        /// <summary>
        /// An array of two-dimensional textures with multiple samples.
        /// </summary>
        MultisampleArray2D = GLEnum.Texture2dMultisampleArray
    }
    public enum CubeMapFace : uint
    {
        Right = GLEnum.TextureCubeMapPositiveX,
        Left = GLEnum.TextureCubeMapNegativeX,
        Top = GLEnum.TextureCubeMapPositiveY,
        Bottom = GLEnum.TextureCubeMapNegativeY,
        Front = GLEnum.TextureCubeMapPositiveZ,
        Back = GLEnum.TextureCubeMapNegativeZ
    }

    /// <summary>
    /// Object that encapsulate an OpenGL texture object.
    /// </summary>
    public interface ITexture : IRenderTexture
    {
        /// <summary>
        /// The properties of this Texture.
        /// </summary>
        public new TextureProperties Properties { get; }
        TexRenProperties IRenderTexture.Properties => Properties;

        /// <summary>
        /// The type of this texture.
        /// </summary>
        public TextureTarget Target { get; }

        /// <summary>
        /// The texture slot this texture is bound to.
        /// </summary>
        public uint ReferanceSlot { get; }

        /// <summary>
        /// Bind the texture to a texture slot.
        /// </summary>
        /// <param name="slot">The slot the bind to.</param>
        public void Bind(uint slot);
    }

    public enum ChannelType : uint
    {
        None = 0,
        Normalised = GLEnum.SignedNormalized,
        UNormalised = GLEnum.UnsignedNormalized,
        Float = GLEnum.Float,
        Int = GLEnum.Int,
        Uint = GLEnum.UInt
    }
    public enum FormatCompatibilityType : uint
    {
        None = 0,
        BySize = GLEnum.ImageFormatCompatibilityBySize,
        ByClass = GLEnum.ImageFormatCompatibilityByClass
    }
    public enum ComparisonFunction : uint
    {
        LessEqual = GLEnum.Lequal,
        GreaterEqual = GLEnum.Gequal,
        Less = GLEnum.Less,
        Greater = GLEnum.Greater,
        Equal = GLEnum.Equal,
        NotEqual = GLEnum.Notequal,
        Always = GLEnum.Always,
        Never = GLEnum.Never
    }
    public enum Swizzle : uint
    {
        Zero = GLEnum.Zero,
        One = GLEnum.One,
        Red = GLEnum.Red,
        Green = GLEnum.Green,
        Blue = GLEnum.Blue,
        Alpha = GLEnum.Alpha
    }
    public enum DepthStencilMode : uint
    {
        Depth = GLEnum.DepthComponent,
        Stencil = GLEnum.StencilIndex
    }
    public enum ComparisonMode : uint
    {
        None = 0,
        /// <summary>
        /// Specifies that the interpolated and clamped red texture coordinate should be compared to the value in the currently bound depth texture.
        /// </summary>
        CompareToDepth = GLEnum.CompareRefToTexture,
    }
}
