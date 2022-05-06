namespace Zene.Graphics
{
    /// <summary>
    /// Object that encapsulates an OpenGL object.
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// The name of this object.
        /// </summary>
        public uint Id { get; }
    }
}
