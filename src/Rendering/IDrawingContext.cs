using System;
using Zene.Structs;

namespace Zene.Graphics
{
    public interface IDrawingContext : IBindable
    {
        public IFramebuffer Framebuffer { get; }
        public IDrawingShader Shader { get; set; }

        public IBox FrameBounds { get; }

        public IMatrix Projection { get; set; }
        public IMatrix View { get; set; }
        public IMatrix Model { get; set; }

        internal void SetMatrices()
        {
            Shader.Matrix1 = Model;
            Shader.Matrix2 = View;
            Shader.Matrix3 = Projection;
        }

        void IBindable.Bind()
        {
            if (Framebuffer == null)
            {
                throw new ArgumentNullException(nameof(Framebuffer));
            }

            Framebuffer.Bind(FrameTarget.Draw);
            Shader?.Bind();
        }
        void IBindable.Unbind()
        {
            if (Framebuffer == null)
            {
                throw new ArgumentNullException(nameof(Framebuffer));
            }

            Framebuffer.Unbind();
            Shader?.Unbind();
        }
    }
}
