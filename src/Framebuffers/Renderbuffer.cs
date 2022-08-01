using Zene.Graphics.Base;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that manages a renderbuffer.
    /// </summary>
    public class Renderbuffer : RenderbufferGL
    {
        /// <summary>
        /// Creates a renderbuffer from an internal format.
        /// </summary>
        /// <param name="internalFormat">The internal format of the renderbuffer.</param>
        public Renderbuffer(TextureFormat internalFormat)
        {
            _targetFormat = internalFormat;
        }

        private readonly TextureFormat _targetFormat;

        /// <summary>
        /// Creates storage for the renderbuffer.
        /// </summary>
        /// <param name="width">The width of the renderbuffer.</param>
        /// <param name="height">The height of the renderbuffer.</param>
        public void CreateStorage(int width, int height) => RenderbufferStorage(_targetFormat, width, height);

        /// <summary>
        /// Creates storage for a multisample renderbuffer.
        /// </summary>
        /// <param name="samples">The number of samples in the renderbuffer.</param>
        /// <param name="width">The width of the renderbuffer.</param>
        /// <param name="height">The height of the renderbuffer.</param>
        public void CreateStorage(int samples, int width, int height) => RenderbufferStorageMultisample(_targetFormat, samples, width, height);

        /// <summary>
        /// The width of the renderbuffer.
        /// </summary>
        public int Width => Properties.Width;
        /// <summary>
        /// The height of the renderbuffer.
        /// </summary>
        public int Height => Properties.Height;
        /// <summary>
        /// The number of samples in the multisample renderbuffer.
        /// </summary>
        public int Samples => Properties.Samples;
        /// <summary>
        /// The internal storage resolution of the red component.
        /// </summary>
        public int RedSize => Properties.RedSize;
        /// <summary>
        /// The internal storage resolution of the green component.
        /// </summary>
        public int GreenSize => Properties.GreenSize;
        /// <summary>
        /// The internal storage resolution of the blue component.
        /// </summary>
        public int BlueSize => Properties.BlueSize;
        /// <summary>
        /// The internal storage resolution of the alpha component.
        /// </summary>
        public int AlphaSize => Properties.AlphaSize;
        /// <summary>
        /// The internal storage resolution of the depth component.
        /// </summary>
        public int DepthSize => Properties.DepthSize;
        /// <summary>
        /// The internal storage resolution of the stencil component.
        /// </summary>
        public int StencilSize => Properties.StencilSize;
    }
}
