using System;

namespace Zene.Graphics
{
    public interface IRenderTexture : IDisposable, IIdentifiable, IBindable
    {
        /// <summary>
        /// Determines wether this is a textrue or renderbuffer.
        /// </summary>
        public bool IsRenderbuffer { get; }

        /// <summary>
        /// The properties for this texture.
        /// </summary>
        public TexRenProperties Properties { get; }

        /// <summary>
        /// The formating of data stored in this texture.
        /// </summary>
        public TextureFormat InternalFormat { get; }
    }
}
