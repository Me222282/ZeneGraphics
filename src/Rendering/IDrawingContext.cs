using System;

namespace Zene.Graphics
{
    public interface IDrawingContext : IBindable
    {
        public IFramebuffer Framebuffer { get; }
        public IShaderProgram Shader { get; set; }

        void IBindable.Bind()
        {
            if (Framebuffer == null)
            {
                throw new ArgumentNullException(nameof(Framebuffer));
            }

            Framebuffer.Bind();
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
