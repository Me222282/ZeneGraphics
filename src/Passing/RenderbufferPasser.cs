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
            Properties = renderbuffer.Properties;
        }
        public RenderbufferPasser(uint id, TextureFormat format)
        {
            Id = id;
            InternalFormat = format;

            Properties = new TexRenProperties(this);
        }

        public uint Id { get; }
        public TextureFormat InternalFormat { get; }
        public TexRenProperties Properties { get; }

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
        public Renderbuffer Pass() => new Renderbuffer(Id, InternalFormat);

        /// <summary>
        /// Returns a new <see cref="RenderbufferGL"/> equivalent for the data provided.
        /// </summary>
        /// <returns></returns>
        public static Renderbuffer Pass(uint id, TextureFormat format) => new Renderbuffer(id, format);
    }
}
