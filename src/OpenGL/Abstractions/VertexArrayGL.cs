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
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="first">Specifies the starting index in the enabled arrays.</param>
        /// <param name="size">Specifies the number of vertices to be rendered.</param>
        protected void DrawArrays(DrawMode mode, int first, int size)
        {
            Bind();

            GL.DrawArrays((uint)mode, first, size);
        }
        /// <summary>
        /// Render primitives from array data, taking parameters from memory.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count"></param>
        /// <param name="primCount"></param>
        /// <param name="first"></param>
        /// <param name="baseInstance"></param>
        [OpenGLSupport(4.0)]
        protected void DrawArraysIndirect(DrawMode mode, uint count, uint primCount, uint first, uint baseInstance)
        {
            Bind();
            // Make sure there is no buffer bound for parameter reference
            State.NullBind(Target.BufferDrawIndirect);

            if (GL.Version < 4.2)
            {
                baseInstance = 0;
            }

            uint* parameters = stackalloc uint[] { count, primCount, first, baseInstance };

            GL.DrawArraysIndirect((uint)mode, &parameters[0]);
        }
        /// <summary>
        /// Render primitives from array data, taking parameters from memory.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="paramSource">The <see cref="IBuffer"/> that contains the parameter data.</param>
        /// <param name="offset">The offset into <paramref name="paramSource"/> where the parameters should be sourced from.</param>
        [OpenGLSupport(4.0)]
        protected void DrawArraysIndirect(DrawMode mode, IBuffer paramSource, int offset)
        {
            Bind();

            // Bind param buffer to param reference
            if (!paramSource.Bound(BufferTarget.DrawIndirect))
            {
                GL.BindBuffer(GLEnum.DrawIndirectBuffer, paramSource);
            }

            GL.DrawArraysIndirect((uint)mode, (void*)new IntPtr(offset));
        }
        /// <summary>
        /// Draw multiple instances of a range of elements.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="first">Specifies the starting index in the enabled arrays.</param>
        /// <param name="size">Specifies the number of vertices to be rendered.</param>
        /// <param name="instances">Specifies the number of instances of the specified range of vertices to be rendered.</param>
        [OpenGLSupport(3.1)]
        protected void DrawArraysInstanced(DrawMode mode, int first, int size, int instances)
        {
            Bind();

            GL.DrawArraysInstanced((uint)mode, first, size, instances);
        }
        /// <summary>
        /// Draw multiple instances of a range of elements with offset applied to instanced attributes.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="first">Specifies the starting index in the enabled arrays.</param>
        /// <param name="size">Specifies the number of vertices to be rendered.</param>
        /// <param name="instances">Specifies the number of instances of the specified range of vertices to be rendered.</param>
        /// <param name="baseInstance">Specifies the base instance for use in fetching instanced vertex attributes.</param>
        [OpenGLSupport(4.2)]
        protected void DrawArraysInstancedBaseInstance(DrawMode mode, int first, int size, int instances, uint baseInstance)
        {
            Bind();

            GL.DrawArraysInstancedBaseInstance((uint)mode, first, size, instances, baseInstance);
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        protected void DrawElements(IBuffer elements, DrawMode mode, int count, IndexType type, int offset)
        {
            if (elements.Target != BufferTarget.ElementArray)
            {
                throw new BufferException(elements, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawElements)}.");
            }

            Bind();
            elements.Bind();

            GL.DrawElements((uint)mode, count, (uint)type, new IntPtr(offset));
        }
        /// <summary>
        /// Render primitives from array data with a per-element offset.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        /// <param name="baseVertex">Specifies a constant that should be added to each element of the array when being referenced.</param>
        [OpenGLSupport(3.2)]
        protected void DrawElementsBaseVertex(IBuffer elements, DrawMode mode, int count, IndexType type, int offset, int baseVertex)
        {
            if (elements.Target != BufferTarget.ElementArray)
            {
                throw new BufferException(elements, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawElementsBaseVertex)}.");
            }

            Bind();
            elements.Bind();

            GL.DrawElementsBaseVertex((uint)mode, count, (uint)type, (void*)new IntPtr(offset), baseVertex);
        }
        /// <summary>
        /// Render indexed primitives from array data, taking parameters from memory.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="type">Specifies the type of data in this element buffer.</param>
        /// <param name="count"></param>
        /// <param name="primCount"></param>
        /// <param name="first"></param>
        /// <param name="baseInstance"></param>
        [OpenGLSupport(4.0)]
        protected void DrawElementsIndirect(IBuffer elements, DrawMode mode, IndexType type, uint count, uint primCount, uint first, uint baseInstance)
        {
            if (elements.Target != BufferTarget.ElementArray)
            {
                throw new BufferException(elements, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawElementsIndirect)}.");
            }

            Bind();
            elements.Bind();
            // Make sure there is no buffer bound for parameter reference
            State.NullBind(Target.BufferDrawIndirect);

            if (GL.Version < 4.2)
            {
                baseInstance = 0;
            }

            uint* parameters = stackalloc uint[] { count, primCount, first, baseInstance };

            GL.DrawElementsIndirect((uint)mode, (uint)type, &parameters[0]);
        }
        /// <summary>
        /// Render indexed primitives from array data, taking parameters from memory.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="type">Specifies the type of data in this element buffer.</param>
        /// <param name="paramSource">The <see cref="IBuffer"/> that contains the parameter data.</param>
        /// <param name="offset">The offset into <paramref name="paramSource"/> where the parameters should be sourced from.</param>
        [OpenGLSupport(4.0)]
        protected void DrawElementsIndirect(IBuffer elements, DrawMode mode, IndexType type, IBuffer paramSource, int offset)
        {
            if (elements.Target != BufferTarget.Array)
            {
                throw new BufferException(elements, $"Buffer has to have a Target of {BufferTarget.Array} to use DrawArraysIndirect(DrawMode, IBuffer, int).");
            }

            Bind();
            elements.Bind();

            // Bind param buffer to param reference
            if (!paramSource.Bound(BufferTarget.DrawIndirect))
            {
                GL.BindBuffer(GLEnum.DrawIndirectBuffer, paramSource);
            }

            GL.DrawElementsIndirect((uint)mode, (uint)type, &offset);
        }
        /// <summary>
        /// Draw multiple instances of a set of elements with offset applied to instanced attributes.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        /// <param name="instances">Specifies the number of instances to render.</param>
        [OpenGLSupport(3.1)]
        protected void DrawElementsInstanced(IBuffer elements, DrawMode mode, int count, IndexType type, int offset, int instances)
        {
            if (elements.Target != BufferTarget.ElementArray)
            {
                throw new BufferException(elements, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawElementsInstanced)}.");
            }

            Bind();
            elements.Bind();

            GL.DrawElementsInstanced((uint)mode, count, (uint)type, (void*)new IntPtr(offset), instances);
        }
        /// <summary>
        /// Draw multiple instances of a set of elements.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        /// <param name="instances">Specifies the number of instances to render.</param>
        /// <param name="baseInstance">Specifies the base instance for use in fetching instanced vertex attributes.</param>
        [OpenGLSupport(4.2)]
        protected void DrawElementsInstancedBaseInstance(IBuffer elements, DrawMode mode, int count, IndexType type, int offset, int instances, uint baseInstance)
        {
            if (elements.Target != BufferTarget.ElementArray)
            {
                throw new BufferException(elements, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawElementsInstancedBaseInstance)}.");
            }

            Bind();
            elements.Bind();

            GL.DrawElementsInstancedBaseInstance((uint)mode, count, (uint)type, (void*)new IntPtr(offset), instances, baseInstance);
        }
        /// <summary>
        /// Render multiple instances of a set of primitives from array data with a per-element offset.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        /// <param name="instances">Specifies the number of instances to render.</param>
        /// <param name="baseVertex">Specifies a constant that should be added to each element of the array when being referenced.</param>
        [OpenGLSupport(3.2)]
        protected void DrawElementsInstancedBaseVertex(IBuffer elements, DrawMode mode, int count, IndexType type, int offset, int instances, int baseVertex)
        {
            if (elements.Target != BufferTarget.ElementArray)
            {
                throw new BufferException(elements, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawElementsInstancedBaseVertex)}.");
            }

            Bind();
            elements.Bind();

            GL.DrawElementsInstancedBaseVertex((uint)mode, count, (uint)type, (void*)new IntPtr(offset), instances, baseVertex);
        }
        /// <summary>
        /// Render multiple instances of a set of primitives from array data with a per-element offset.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        /// <param name="instances">Specifies the number of instances to render.</param>
        /// <param name="baseVertex">Specifies a constant that should be added to each element of the array when being referenced.</param>
        /// <param name="baseInstance">Specifies the base instance for use in fetching instanced vertex attributes.</param>
        [OpenGLSupport(3.2)]
        protected void DrawElementsInstancedBaseVertexBaseInstance(IBuffer elements, DrawMode mode, int count, IndexType type, int offset, int instances, int baseVertex, uint baseInstance)
        {
            if (elements.Target != BufferTarget.ElementArray)
            {
                throw new BufferException(elements, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawElementsInstancedBaseVertexBaseInstance)}.");
            }

            Bind();
            elements.Bind();

            GL.DrawElementsInstancedBaseVertexBaseInstance((uint)mode, count, (uint)type, (void*)new IntPtr(offset), instances, baseVertex, baseInstance);
        }
    }
}
