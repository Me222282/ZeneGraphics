using System;

namespace Zene.Graphics
{
    public interface IRenderTexture : IDisposable, IIdentifiable, IBindable
    {
        /// <summary>
        /// The formating of data stored in this texture.
        /// </summary>
        public TextureFormat InternalFormat { get; }
    }
}
