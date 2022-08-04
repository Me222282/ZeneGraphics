namespace Zene.Graphics
{
    public interface IRenderTexture : IGLObject
    {
        /// <summary>
        /// Determines wether this is a textrue or renderbuffer.
        /// </summary>
        public bool IsRenderbuffer { get; }

        /// <summary>
        /// The properties for this texture.
        /// </summary>
        public new TexRenProperties Properties { get; }
        IProperties IGLObject.Properties => Properties;

        /// <summary>
        /// The formating of data stored in this texture.
        /// </summary>
        public TextureFormat InternalFormat { get; }
    }
}
