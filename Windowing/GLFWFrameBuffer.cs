using System;
using Zene.Graphics;
using Zene.Graphics.OpenGL;
using Zene.Structs;

namespace Zene.Windowing
{
    internal class GLFWFrameBuffer : IFramebuffer
    {
        private static readonly FrameDrawTarget[] _drawBuffers = new FrameDrawTarget[] { FrameDrawTarget.Colour0 };

        public FrameTarget Binding => BaseFramebuffer.Binding;

        public RectangleI View
        {
            get => BaseFramebuffer.View;
            set => BaseFramebuffer.View = value;
        }
        public Vector2I ViewSize
        {
            get => BaseFramebuffer.ViewSize;
            set => BaseFramebuffer.ViewSize = value;
        }

        public FrameDrawTarget ReadBuffer
        {
            get => FrameDrawTarget.Colour0;
            set => throw new NotSupportedException();
        }
        public FrameDrawTarget[] DrawBuffers
        {
            get => _drawBuffers;
            set => throw new NotSupportedException();
        }

        public uint Id => 0;

        public void Bind(FrameTarget target) => BaseFramebuffer.Bind(target);
        public void Bind() => BaseFramebuffer.Bind();
        public void Unbind()
        {
            if (!this.Bound()) { return; }

            GL.BindFramebuffer((uint)Binding, 0);
        }

        public void Clear(BufferBit buffer) => BaseFramebuffer.Clear(buffer);

        public void Dispose()
        {
            // Cannot dispose nothing
        }
        public bool Validate() => throw new NotSupportedException();
    }
}
