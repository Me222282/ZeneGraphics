namespace Zene.Graphics
{
    public interface IDrawObject
    {
        public Drawable GetRenderable(IDrawingContext context);
    }
}
