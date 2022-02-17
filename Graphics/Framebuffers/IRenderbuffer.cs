using System;

namespace Zene.Graphics
{
    /// <summary>
    /// Objects that encapsulate an OpenGL renderbuffer object.
    /// </summary>
    [OpenGLSupport(3.0)]
    public interface IRenderbuffer : IDisposable, IIdentifiable, IBindable
    {
        /// <summary>
        /// The formating of data stored in this renderbuffer.
        /// </summary>
        public TextureFormat InternalFormat { get; }
    }
}
