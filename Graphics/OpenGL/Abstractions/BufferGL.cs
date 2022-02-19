using System;

namespace Zene.Graphics.Base
{
    public unsafe sealed class BufferGL : IBuffer
    {
        public uint Id { get; }

        public BufferUsage UsageType { get; private set; }
        public BufferTarget Target { get; }

        public void Bind()
        {
            GL.BindBuffer((uint)Target, Id);
        }
        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            GL.DeleteBuffer(Id);

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        public void Unbind()
        {
            if (!this.Bound()) { return; }

            GL.BindBuffer((uint)Target, 0);
        }
    }
}
