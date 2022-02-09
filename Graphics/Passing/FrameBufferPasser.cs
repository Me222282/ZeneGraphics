using System;
using Zene.Graphics.OpenGL;
using Zene.Graphics.OpenGL.Abstract3;

namespace Zene.Graphics.Passing
{
    /// <summary>
    /// An object for coping and externally managing framebuffer objects.
    /// </summary>
    public class FrameBufferPasser : IFrameBuffer
    {
        public FrameBufferPasser(IFrameBuffer frameBuffer)
        {
            Id = frameBuffer.Id;
        }
        public FrameBufferPasser(uint id)
        {
            Id = id;
        }

        public uint Id { get; }
        public FrameTarget Binding { get; private set; } = FrameTarget.FrameBuffer;

        public void Bind(FrameTarget target)
        {
            Binding = target;
            GL.BindFramebuffer((uint)target, Id);
        }
        public void Bind()
        {
            Binding = FrameTarget.FrameBuffer;
            GL.BindFramebuffer(GLEnum.Framebuffer, Id);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public void UnBind()
        {
            GL.BindFramebuffer((uint)Binding, 0);
        }
        public bool Validate()
        {
            return this.CheckStatus() == FrameBufferStatus.Complete;
        }

        /// <summary>
        /// Returns a new <see cref="FrameBufferGL"/> equivalent for the data this framebuffer contains.
        /// </summary>
        /// <returns></returns>
        public FrameBufferGL Pass()
        {
            return new FrameBufferGL(Id);
        }

        /// <summary>
        /// Returns a new <see cref="FrameBufferGL"/> equivalent for the data provided.
        /// </summary>
        /// <returns></returns>
        public static FrameBufferGL Pass(uint id)
        {
            return new FrameBufferGL(id);
        }
    }
}
