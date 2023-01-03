namespace Zene.Graphics
{
    public interface IDrawObject
    {
        public Renderable GetRenderable(IDrawingContext context);
    }
}
