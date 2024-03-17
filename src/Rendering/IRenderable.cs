using System;

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
    /*
    public interface IBasicRenderer : IRenderable
    {
        void IRenderable.OnRender(IDrawingContext context)
        {
            if (context is not DrawManager dm)
            {
                throw new Exception("Invalid Drawing context.");
            }

            OnRender(dm);
        }

        public void OnRender(DrawManager context);
    }

    public interface IBasicRenderer<T> : IRenderable<T>
    {
        void IRenderable<T>.OnRender(IDrawingContext context, T param)
        {
            if (context is not DrawManager dm)
            {
                throw new Exception("Invalid Drawing context.");
            }

            OnRender(dm, param);
        }

        public void OnRender(DrawManager context, T param);
    }*/
}
