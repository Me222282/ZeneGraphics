using System;
using Zene.Graphics.Base;
using Zene.Graphics.Base.Extensions;
using Zene.Structs;

namespace Zene.Graphics.Passing
{
    /// <summary>
    /// An object for coping and externally managing framebuffer objects.
    /// </summary>
    public class FrameBufferPasser : IFramebuffer
    {
        public FrameBufferPasser(IFramebuffer frameBuffer)
        {
            Id = frameBuffer.Id;
        }
        public FrameBufferPasser(uint id)
        {
            Id = id;
        }

        public uint Id { get; }
        public FrameTarget Binding { get; private set; } = FrameTarget.FrameBuffer;

        public RectangleI View { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
        public Vector2I ViewSize { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
        public FrameDrawTarget ReadBuffer { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
        public FrameDrawTarget[] DrawBuffers { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        public void Bind(FrameTarget target)
        {
            if (this.Bound()) { return; }

            Binding = target;
            GL.BindFramebuffer((uint)target, Id);
        }
        public void Bind()
        {
            if (this.Bound()) { return; }

            Binding = FrameTarget.FrameBuffer;
            GL.BindFramebuffer(GLEnum.Framebuffer, Id);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public void Unbind()
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

        public void Clear(BufferBit buffer)
        {
            Bind();

            GL.Clear((uint)buffer);
        }
    }
}
