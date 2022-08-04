using System;

namespace Zene.Graphics.Base
{
    public unsafe class VertexArrayGL : IVertexArray
    {
        public VertexArrayGL()
        {
            Id = GL.GenVertexArray();

            Properties = new VertexArrayProperties(this);
        }

        public uint Id { get; }

        public VertexArrayProperties Properties { get; }

        public void Bind()
        {
            if (this.Bound()) { return; }

            GL.BindVertexArray(Id);
        }
        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            Dispose(true);

            _disposed = true;
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                GL.DeleteVertexArray(Id);
            }
        }
        public void Unbind()
        {
            if (!this.Bound()) { return; }

            GL.BindVertexArray(0);
        }

        /// <summary>
        /// Enable a generic vertex attribute array.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be enabled.</param>
        public void EnableVertexAttribArray(uint index)
        {
            Bind();

            GL.EnableVertexAttribArray(index);
        }
        /// <summary>
        /// Disable a generic vertex attribute array.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be disabled.</param>
        public void DisableVertexAttribArray(uint index)
        {
            Bind();

            GL.DisableVertexAttribArray(index);
        }

        /// <summary>
        /// Define an array of generic vertex attribute data.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
        /// <param name="size">Specifies the number of components per generic vertex attribute.</param>
        /// <param name="type">Specifies the data type of each component in the array.</param>
        /// <param name="normalised">Specifies whether fixed-point data values should be normalised.</param>
        /// <param name="stride">Specifies the byte offset between consecutive generic vertex attributes.</param>
        /// <param name="pointer">Specifies a offset of the first component of the first generic vertex attribute in the array.</param>
        public void VertexAttribPointer(uint index, AttributeSize size, DataType type, bool normalised, int stride, int pointer)
        {
            Bind();

            GL.VertexAttribPointer(index, (int)size, (uint)type, normalised, stride, new IntPtr(pointer));
        }
    }
}
