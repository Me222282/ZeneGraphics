namespace Zene.Graphics
{
    /// <summary>
    /// Objects that encapsulate an OpenGL renderbuffer object.
    /// </summary>
    [OpenGLSupport(3.0)]
    public interface IRenderbuffer : IRenderTexture
    {
        /// <summary>
        /// The formating of data stored in this renderbuffer.
        /// </summary>
        public new TextureFormat InternalFormat { get; }
    }
}
