namespace Zene.Graphics
{
    /// <summary>
    /// Objects that can be drawn.
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Draws this object in the current context.
        /// </summary>
        public void Draw();
    }
}
