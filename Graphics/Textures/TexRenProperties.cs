namespace Zene.Graphics
{
    public class TexRenProperties
    {
        public TexRenProperties(IRenderTexture source)
        {
            Handle = source;
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

        internal int _redSize = 0;
        /// <summary>
        /// The internal storage resolution of the red component at base level.
        /// </summary>
        public int RedSize => _redSize;
        internal int _greenSize = 0;
        /// <summary>
        /// The internal storage resolution of the green component at base level.
        /// </summary>
        public int GreenSize => _greenSize;
        internal int _blueSize = 0;
        /// <summary>
        /// The internal storage resolution of the blue component at base level.
        /// </summary>
        public int BlueSize => _blueSize;
        internal int _alphaSize = 0;
        /// <summary>
        /// The internal storage resolution of the alpha component at base level.
        /// </summary>
        public int AlphaSize => _alphaSize;

        internal int _depthSize = 0;
        /// <summary>
        /// The internal storage resolution of the depth component at base level.
        /// </summary>
        public int DepthSize => _depthSize;
        internal int _stencilSize = 0;
        /// <summary>
        /// The internal storage resolution of the stencil component at base level.
        /// </summary>
        public int StencilSize => _stencilSize;
    }
}
