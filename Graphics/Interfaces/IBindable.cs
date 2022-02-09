namespace Zene.Graphics
{
    /// <summary>
    /// Objects that can be bound to an OpenGL target.
    /// </summary>
    public interface IBindable
    {
        /// <summary>
        /// Binds this object to its relative OpenGL target.
        /// </summary>
        public void Bind();

        /// <summary>
        /// Sets this objects relative OpenGL target to 0.
        /// </summary>
        public void UnBind();
    }
}
