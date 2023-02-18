namespace Zene.Graphics
{
    public interface IRenderable
    {
        public void OnRender(IDrawingContext context);
    }

    public interface IRenderable<T>
    {
        public void OnRender(IDrawingContext context, T param);
    }

    public interface IBasicRenderer
    {
        public void OnRender(DrawManager context);
    }

    public interface IBasicRenderer<T>
    {
        public void OnRender(DrawManager context, T param);
    }
}
