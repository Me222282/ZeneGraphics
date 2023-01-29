namespace Zene.Graphics
{
    public interface IRenderable<T>
    {
        public void OnRender(IDrawingContext context, T param);
    }
}
