using System;
using Zene.Graphics.Base;

namespace Zene.Graphics.Passing
{
    /// <summary>
    /// An object for coping and externally managing renderbuffer objects.
    /// </summary>
    public class RenderbufferPasser : IRenderbuffer
    {
        public RenderbufferPasser(IRenderbuffer renderbuffer)
        {
            Id = renderbuffer.Id;
            InternalFormat = renderbuffer.InternalFormat;
        }
        public RenderbufferPasser(uint id, TextureFormat format)
        {
            Id = id;
            InternalFormat = format;
        }

        public uint Id { get; }
        public TextureFormat InternalFormat { get; }

        public void Bind()
        {
            if (this.Bound()) { return; }

            GL.BindRenderbuffer(GLEnum.Renderbuffer, Id);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public void Unbind()
        {
            GL.BindRenderbuffer(GLEnum.Renderbuffer, 0);
        }

        /// <summary>
        /// Returns a new <see cref="RenderbufferGL"/> equivalent for the data this renderbuffer contains.
        /// </summary>
        /// <returns></returns>
        public RenderbufferGL Pass()
        {
            return new RenderbufferGL(Id, InternalFormat);
        }

        /// <summary>
        /// Returns a new <see cref="RenderbufferGL"/> equivalent for the data provided.
        /// </summary>
        /// <returns></returns>
        public static RenderbufferGL Pass(uint id, TextureFormat format)
        {
            return new RenderbufferGL(id, format);
        }
    }
}
