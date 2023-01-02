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
        internal VertexArrayGL(uint id)
        {
            Id = id;
        }

        public uint Id { get; }

        public VertexArrayProperties Properties { get; }

        public void Bind()
        {
            if (this.Bound()) { return; }

            GL.BindVertexArray(this);
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

            GL.BindVertexArray(null);
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

        /// <summary>
        /// Modify the rate at which generic vertex attributes advance during instanced rendering.
        /// </summary>
        /// <param name="index">Specify the index of the generic vertex attribute.</param>
        /// <param name="divisor">Specify the number of instances that will pass between updates of the generic attribute at slot <paramref name="index"/>.</param>
        public void VertexAttribDivisor(uint index, uint divisor)
        {
            Bind();

            GL.VertexAttribDivisor(index, divisor);
        }

        /// <summary>
        /// Configures element array buffer binding of a vertex array object.
        /// </summary>
        /// <param name="buffer">Specifies he buffer object to use for the element array buffer binding.</param>
        public void ElementBuffer(IBuffer buffer) => GL.VertexArrayElementBuffer(this, buffer);
    }
}
