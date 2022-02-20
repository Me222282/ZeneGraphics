namespace Zene.Graphics
{
    public class TexRenProperties
    {
        public TexRenProperties(IRenderTexture source)
        {
            Handle = source;
            _oldFormat = source.InternalFormat;
        }
        public virtual IRenderTexture Handle { get; }

        internal int _width = 0;
        /// <summary>
        /// The width of the texture at base level.
        /// </summary>
        public int Width => _width;
        internal int _height = 0;
        /// <summary>
        /// The height of the texture at base level.
        /// </summary>
        public int Height => _height;
        internal int _samples = 0;
        /// <summary>
        /// The number of samples in a multisample texture.
        /// </summary>
        public int Samples => _samples;

        protected TextureFormat _oldFormat;
        internal virtual void InternalFormatChanged()
        {
            // Texture format hasn't changed
            if (Handle.InternalFormat == _oldFormat) { return; }

            switch (Handle.InternalFormat)
            {
                case TextureFormat.CompressedRed:
                case TextureFormat.CompressedRedRgtc1:
                case TextureFormat.CompressedSignedRedRgtc1:
                case TextureFormat.R:
                case TextureFormat.R8:
                case TextureFormat.R8i:
                case TextureFormat.R8Snorm:
                case TextureFormat.R8ui:
                    _redSize = 8;
                    _greenSize = 0;
                    _blueSize = 0;
                    _alphaSize = 0;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.CompressedRg:
                case TextureFormat.CompressedRgRgtc2:
                case TextureFormat.CompressedSignedRgRgtc2:
                case TextureFormat.Rg:
                case TextureFormat.Rg8:
                case TextureFormat.Rg8i:
                case TextureFormat.Rg8Snorm:
                case TextureFormat.Rg8ui:
                    _redSize = 8;
                    _greenSize = 8;
                    _blueSize = 0;
                    _alphaSize = 0;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.CompressedRgb:
                case TextureFormat.CompressedSrgb:
                case TextureFormat.Rgb:
                case TextureFormat.Rgb8:
                case TextureFormat.Rgb8i:
                case TextureFormat.Rgb8Snorm:
                case TextureFormat.Rgb8ui:
                case TextureFormat.Srgb:
                case TextureFormat.Srgb8:
                    _redSize = 8;
                    _greenSize = 8;
                    _blueSize = 8;
                    _alphaSize = 0;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.CompressedRgba:
                case TextureFormat.CompressedRgbaBptcUnorm:
                case TextureFormat.CompressedSrgbAlpha:
                case TextureFormat.CompressedSrgbAlphaBptcUnorm:
                case TextureFormat.Rgba:
                case TextureFormat.Rgba2:
                case TextureFormat.Rgba4:
                case TextureFormat.Rgba8:
                case TextureFormat.Rgba8i:
                case TextureFormat.Rgba8Snorm:
                case TextureFormat.Rgba8ui:
                case TextureFormat.Srgba:
                case TextureFormat.Srgba8:
                    _redSize = 8;
                    _greenSize = 8;
                    _blueSize = 8;
                    _alphaSize = 8;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.R16:
                case TextureFormat.R16f:
                case TextureFormat.R16i:
                case TextureFormat.R16Snorm:
                case TextureFormat.R16ui:
                    _redSize = 16;
                    _greenSize = 0;
                    _blueSize = 0;
                    _alphaSize = 0;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.Rg16:
                case TextureFormat.Rg16f:
                case TextureFormat.Rg16i:
                case TextureFormat.Rg16Snorm:
                case TextureFormat.Rg16ui:
                    _redSize = 16;
                    _greenSize = 16;
                    _blueSize = 0;
                    _alphaSize = 0;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.Rgb12:
                case TextureFormat.Rgb16:
                case TextureFormat.Rgb16f:
                case TextureFormat.Rgb16i:
                case TextureFormat.Rgb16Snorm:
                case TextureFormat.Rgb16ui:
                    _redSize = 16;
                    _greenSize = 16;
                    _blueSize = 16;
                    _alphaSize = 0;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.Rgba12:
                case TextureFormat.Rgba16:
                case TextureFormat.Rgba16f:
                case TextureFormat.Rgba16i:
                case TextureFormat.Rgba16Snorm:
                case TextureFormat.Rgba16ui:
                    _redSize = 16;
                    _greenSize = 16;
                    _blueSize = 16;
                    _alphaSize = 16;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.R32f:
                case TextureFormat.R32i:
                case TextureFormat.R32ui:
                    _redSize = 32;
                    _greenSize = 0;
                    _blueSize = 0;
                    _alphaSize = 0;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.Rg32f:
                case TextureFormat.Rg32i:
                case TextureFormat.Rg32ui:
                    _redSize = 32;
                    _greenSize = 32;
                    _blueSize = 0;
                    _alphaSize = 0;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.CompressedRgbBptcSignedFloat:
                case TextureFormat.Rgb32f:
                case TextureFormat.Rgb32i:
                case TextureFormat.Rgb32ui:
                    _redSize = 32;
                    _greenSize = 32;
                    _blueSize = 32;
                    _alphaSize = 0;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.Rgba32f:
                case TextureFormat.Rgba32i:
                case TextureFormat.Rgba32ui:
                    _redSize = 32;
                    _greenSize = 32;
                    _blueSize = 32;
                    _alphaSize = 32;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.DepthStencil:
                case TextureFormat.Depth24Stencil8:
                    _redSize = 0;
                    _greenSize = 0;
                    _blueSize = 0;
                    _alphaSize = 0;
                    _depthSize = 24;
                    _stencilSize = 8;
                    break;

                case TextureFormat.Depth32fStencil8:
                    _redSize = 0;
                    _greenSize = 0;
                    _blueSize = 0;
                    _alphaSize = 0;
                    _depthSize = 32;
                    _stencilSize = 8;
                    break;

                case TextureFormat.DepthComponent:
                case TextureFormat.DepthComponent24:
                case TextureFormat.DepthComponent32:
                    _redSize = 0;
                    _greenSize = 0;
                    _blueSize = 0;
                    _alphaSize = 0;
                    _depthSize = 24;
                    _stencilSize = 0;
                    break;

                case TextureFormat.DepthComponent16:
                    _redSize = 0;
                    _greenSize = 0;
                    _blueSize = 0;
                    _alphaSize = 0;
                    _depthSize = 16;
                    _stencilSize = 0;
                    break;

                case TextureFormat.DepthComponent32f:
                    _redSize = 0;
                    _greenSize = 0;
                    _blueSize = 0;
                    _alphaSize = 0;
                    _depthSize = 32;
                    _stencilSize = 0;
                    break;

                case TextureFormat.R11fG11fB10f:
                    _redSize = 11;
                    _greenSize = 11;
                    _blueSize = 10;
                    _alphaSize = 0;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.Rgb4:
                case TextureFormat.Rgb5:
                case TextureFormat.R3G3B2:
                    _redSize = 5;
                    _greenSize = 6;
                    _blueSize = 5;
                    _alphaSize = 0;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.Rgb10:
                    _redSize = 10;
                    _greenSize = 10;
                    _blueSize = 10;
                    _alphaSize = 0;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.Rgb10A2:
                case TextureFormat.Rgb10A2ui:
                    _redSize = 10;
                    _greenSize = 10;
                    _blueSize = 10;
                    _alphaSize = 2;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.Rgb5A1:
                    _redSize = 5;
                    _greenSize = 6;
                    _blueSize = 5;
                    _alphaSize = 1;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;

                case TextureFormat.Rgb9E5:
                    _redSize = 9;
                    _greenSize = 9;
                    _blueSize = 9;
                    _alphaSize = 0;
                    _depthSize = 0;
                    _stencilSize = 0;
                    break;
            }
        }

        private int _redSize = 0;
        /// <summary>
        /// The internal storage resolution of the red component at base level.
        /// </summary>
        public int RedSize => _redSize;
        private int _greenSize = 0;
        /// <summary>
        /// The internal storage resolution of the green component at base level.
        /// </summary>
        public int GreenSize => _greenSize;
        private int _blueSize = 0;
        /// <summary>
        /// The internal storage resolution of the blue component at base level.
        /// </summary>
        public int BlueSize => _blueSize;
        private int _alphaSize = 0;
        /// <summary>
        /// The internal storage resolution of the alpha component at base level.
        /// </summary>
        public int AlphaSize => _alphaSize;

        private int _depthSize = 0;
        /// <summary>
        /// The internal storage resolution of the depth component at base level.
        /// </summary>
        public int DepthSize => _depthSize;
        private int _stencilSize = 0;
        /// <summary>
        /// The internal storage resolution of the stencil component at base level.
        /// </summary>
        public int StencilSize => _stencilSize;
    }
}
