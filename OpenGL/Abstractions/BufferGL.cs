using System;

namespace Zene.Graphics.Base
{
    public unsafe sealed class BufferGL : IBuffer
    {
        public BufferGL()
        {
            Id = GL.GenBuffer();
        }

        public uint Id { get; }

        public BufferUsage UsageType { get; private set; }
        public BufferTarget Target { get; }

        public void Bind()
        {
            if (this.Bound()) { return; }

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

        public BufferTarget BindRead()
        {
            if (this.Bound()) { return Target; }

            GL.BindBuffer(GLEnum.CopyReadBuffer, Id);

            return BufferTarget.CopyRead;
        }
        public BufferTarget BindWrite()
        {
            if (this.Bound()) { return Target; }

            GL.BindBuffer(GLEnum.CopyWriteBuffer, Id);

            return BufferTarget.CopyWrite;
        }
        /// <summary>
        /// Bind a buffer to be referenced as parameters for draw calls
        /// </summary>
        public void BindIndirectDraw()
        {
            if (this.Bound(BufferTarget.DrawIndirect)) { return; }

            GL.BindBuffer(GLEnum.DrawIndirectBuffer, Id);
        }

        /// <summary>
        /// Bind a buffer object to an indexed buffer target.
        /// </summary>
        /// <param name="index">Specify the index of the binding point within the array.</param>
        [OpenGLSupport(3.0)]
        public void BindBase(uint index)
        {
            if (Target != BufferTarget.AtomicCounter ||
                Target != BufferTarget.TransformFeedback ||
                Target != BufferTarget.Uniform ||
                Target != BufferTarget.ShaderStorage)
            {
                throw new BufferException(this, $"Target must be either {BufferTarget.AtomicCounter}, {BufferTarget.TransformFeedback}, {BufferTarget.Uniform} or {BufferTarget.ShaderStorage} to use BindBase(uint).");
            }

            GL.BindBufferBase((uint)Target, index, Id);
        }
        /// <summary>
        /// Bind a range within a buffer object to an indexed buffer target
        /// </summary>
        /// <param name="index">Specify the index of the binding point within the array.</param>
        /// <param name="offset">The starting offset in basic machine units into the buffer object.</param>
        /// <param name="size">The amount of data in machine units that can be read from the buffer object while used as an indexed target.</param>
        [OpenGLSupport(3.0)]
        public void BindBase(uint index, int offset, int size)
        {
            if (Target != BufferTarget.AtomicCounter ||
                Target != BufferTarget.TransformFeedback ||
                Target != BufferTarget.Uniform ||
                Target != BufferTarget.ShaderStorage)
            {
                throw new BufferException(this, $"Target must be either {BufferTarget.AtomicCounter}, {BufferTarget.TransformFeedback}, {BufferTarget.Uniform} or {BufferTarget.ShaderStorage} to use BindBase(uint, int, int).");
            }

            GL.BindBufferRange((uint)Target, index, Id, offset, size);
        }

        /// <summary>
        /// Creates and initializes a buffer object's data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Specifies the array of data that will be copied into the data store for initialization.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        public void BufferData<T>(T[] data, BufferUsage usage) where T : unmanaged
        {
            if (usage.IsOld())
            {
                throw new BufferException(this, $"BufferUsage {nameof(usage)} is not a valid usage for BufferData<T>(int, T*, BufferUsage).");
            }

            Bind();

            fixed (T* ptr = &data[0])
            {
                GL.BufferData((uint)Target, data.Length * sizeof(T), ptr, (uint)usage);
            }

            UsageType = usage;
        }
        /// <summary>
        /// Creates and initializes a buffer object's data store.
        /// </summary>
        /// <param name="size">Specifies the size in bytes of the buffer object's new data store.</param>
        /// <param name="data">Specifies a pointer to data that will be copied into the data store for initialization.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        public void BufferData(int size, IntPtr data, BufferUsage usage)
        {
            if (usage.IsOld())
            {
                throw new BufferException(this, $"BufferUsage {nameof(usage)} is not a valid usage for BufferData<T>(int, T*, BufferUsage).");
            }

            Bind();

            GL.BufferData((uint)Target, size, data.ToPointer(), (uint)usage);

            UsageType = usage;
        }
        /// <summary>
        /// Creates and initializes a buffer object's data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="length">Specifies the size of the array to be the buffer object's new data store.</param>
        /// <param name="data">Specifies a pointer to data that will be copied into the data store for initialization.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        public void BufferData<T>(int length, T* data, BufferUsage usage) where T : unmanaged
        {
            if (usage.IsOld())
            {
                throw new BufferException(this, $"BufferUsage {nameof(usage)} is not a valid usage for BufferData<T>(int, T*, BufferUsage).");
            }

            Bind();

            GL.BufferData((uint)Target, length * sizeof(T), data, (uint)usage);

            UsageType = usage;
        }

        /// <summary>
        /// Creates and initializes a buffer object's immutable data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Specifies the array of data that will be copied into the data store for initialization.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        [OpenGLSupport(4.4)]
        public void BufferStorage<T>(T[] data, BufferUsage usage) where T : unmanaged
        {
            if (!usage.IsOld())
            {
                throw new BufferException(this, $"BufferUsage {nameof(usage)} is not a valid usage for BufferStorage<T>(int, T*, BufferUsage).");
            }

            Bind();

            fixed (T* ptr = &data[0])
            {
                GL.BufferStorage((uint)Target, data.Length * sizeof(T), ptr, (uint)usage);
            }

            UsageType = usage;
        }
        /// <summary>
        /// Creates and initializes a buffer object's immutable data store.
        /// </summary>
        /// <param name="size">Specifies the size in bytes of the buffer object's new data store.</param>
        /// <param name="data">Specifies a pointer to data that will be copied into the data store for initialization.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        [OpenGLSupport(4.4)]
        public void BufferStorage(int size, IntPtr data, BufferUsage usage)
        {
            if (!usage.IsOld())
            {
                throw new BufferException(this, $"BufferUsage {nameof(usage)} is not a valid usage for BufferStorage<T>(int, T*, BufferUsage).");
            }

            Bind();

            GL.BufferStorage((uint)Target, size, data.ToPointer(), (uint)usage);

            UsageType = usage;
        }
        /// <summary>
        /// Creates and initializes a buffer object's immutable data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="length">Specifies the size of the array to be the buffer object's new data store.</param>
        /// <param name="data">Specifies a pointer to data that will be copied into the data store for initialization.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        [OpenGLSupport(4.4)]
        public void BufferStorage<T>(int length, T* data, BufferUsage usage) where T : unmanaged
        {
            if (!usage.IsOld())
            {
                throw new BufferException(this, $"BufferUsage {nameof(usage)} is not a valid usage for BufferStorage<T>(int, T*, BufferUsage).");
            }

            Bind();

            GL.BufferStorage((uint)Target, length * sizeof(T), data, (uint)usage);

            UsageType = usage;
        }

        /// <summary>
        /// Updates a subset of a buffer object's data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">Specifies the offset into the buffer object's data store where data replacement will begin, measured in <typeparamref name="T"/>.</param>
        /// <param name="data">Specifies the array of data that will be copied into the data store.</param>
        public void BufferSubData<T>(int offset, T[] data) where T : unmanaged
        {
            Bind();

            fixed (T* ptr = &data[0])
            {
                GL.BufferSubData((uint)Target, offset * sizeof(T), data.Length * sizeof(T), ptr);
            }
        }
        /// <summary>
        /// Updates a subset of a buffer object's data store.
        /// </summary>
        /// <param name="offset">Specifies the offset into the buffer object's data store where data replacement will begin, measured in bytes.</param>
        /// <param name="size">Specifies the size in bytes of the data store region being replaced.</param>
        /// <param name="data">Specifies a pointer to the new data that will be copied into the data store.</param>
        public void BufferSubData(int offset, int size, IntPtr data)
        {
            Bind();

            GL.BufferSubData((uint)Target, offset, size, data.ToPointer());
        }
        /// <summary>
        /// Updates a subset of a buffer object's data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">Specifies the offset into the buffer object's data store where data replacement will begin, measured in <typeparamref name="T"/>.</param>
        /// <param name="length">Specifies the size of the array the data store region being replaced.</param>
        /// <param name="data">Specifies a pointer to the new data that will be copied into the data store.</param>
        public void BufferSubData<T>(int offset, int length, T* data) where T : unmanaged
        {
            Bind();

            GL.BufferSubData((uint)Target, offset * sizeof(T), length * sizeof(T), data);
        }

        /// <summary>
        /// Fill a buffer object's data store with a fixed value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="internalFormat">The internal format with which the data will be stored in the buffer object.</param>
        /// <param name="format">The format of <paramref name="value"/>.</param>
        /// <param name="type">The type of data in <paramref name="value"/>.</param>
        /// <param name="value">The data to be replicated into the buffer's data store.</param>
        [OpenGLSupport(4.3)]
        public void ClearBufferData<T>(TextureFormat internalFormat, BaseFormat format, TextureData type, T value) where T : unmanaged 
        {
            Bind();

            GL.ClearBufferData((uint)Target, (uint)internalFormat, (uint)format, (uint)type, &value);
        }
        /// <summary>
        /// Fill all or part of buffer object's data store with a fixed value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="internalFormat">The internal format with which the data will be stored in the buffer object.</param>
        /// <param name="offset">The offset in basic machine units into the buffer object's data store at which to start filling.</param>
        /// <param name="size">The size in basic machine units of the range of the data store to fill.</param>
        /// <param name="format">The format of <paramref name="value"/>.</param>
        /// <param name="type">The type of data in <paramref name="value"/>.</param>
        /// <param name="value">The data to be replicated into the buffer's data store.</param>
        [OpenGLSupport(4.3)]
        public void ClearBufferSubData<T>(TextureFormat internalFormat, int offset, int size, BaseFormat format, TextureData type, T value) where T : unmanaged
        {
            Bind();

            GL.ClearBufferSubData((uint)Target, (uint)internalFormat, offset, size, (uint)format, (uint)type, &value);
        }

        /// <summary>
        /// Copy all or part of the data store of a buffer object to the data store of another buffer object
        /// </summary>
        /// <param name="dest">Specifies the <see cref="IBuffer"/> to copy to.</param>
        /// <param name="srcOffset">Specifies the offset, in basic machine units, within the data store of the source buffer object at which data will be read.</param>
        /// <param name="destOffset">Specifies the offset, in basic machine units, within the data store of the destination buffer object at which data will be written.</param>
        /// <param name="size">Specifies the size, in basic machine units, of the data to be copied from the source buffer object to the destination buffer object.</param>
        public void CopyBufferSubData(IBuffer dest, int srcOffset, int destOffset, int size)
        {
            BufferTarget srcTarget = BindRead();
            BufferTarget destTarget = dest.BindWrite();

            GL.CopyBufferSubData((uint)srcTarget, (uint)destTarget, srcOffset, destOffset, size);
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="first">Specifies the starting index in the enabled arrays.</param>
        /// <param name="size">Specifies the number of vertices to be rendered.</param>
        public void DrawArrays(DrawMode mode, int first, int size)
        {
            if (Target != BufferTarget.Array)
            {
                throw new BufferException(this, $"Buffer has to have a Target of {BufferTarget.Array} to use {nameof(DrawArrays)}.");
            }

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
        public void DrawArraysIndirect(DrawMode mode, uint count, uint primCount, uint first, uint baseInstance)
        {
            if (Target != BufferTarget.Array)
            {
                throw new BufferException(this, $"Buffer has to have a Target of {BufferTarget.Array} to use {nameof(DrawArraysIndirect)}.");
            }

            Bind();
            // Make sure there is no buffer bound for parameter reference
            State.NullBind(Graphics.Target.BufferDrawIndirect);

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
        public void DrawArraysIndirect(DrawMode mode, IBuffer paramSource, int offset)
        {
            if (Target != BufferTarget.Array)
            {
                throw new BufferException(this, $"Buffer has to have a Target of {BufferTarget.Array} to use {nameof(DrawArraysIndirect)}.");
            }

            Bind();
            
            // Bind param buffer to param reference
            if (!paramSource.Bound(BufferTarget.DrawIndirect))
            {
                GL.BindBuffer(GLEnum.DrawIndirectBuffer, paramSource.Id);
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
        public void DrawArraysInstanced(DrawMode mode, int first, int size, int instances)
        {
            if (Target != BufferTarget.Array)
            {
                throw new BufferException(this, $"Buffer has to have a Target of {BufferTarget.Array} to use {nameof(DrawArraysInstanced)}.");
            }

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
        public void DrawArraysInstancedBaseInstance(DrawMode mode, int first, int size, int instances, uint baseInstance)
        {
            if (Target != BufferTarget.Array)
            {
                throw new BufferException(this, $"Buffer has to have a Target of {BufferTarget.Array} to use {nameof(DrawArraysInstancedBaseInstance)}.");
            }

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
        public void DrawElements(DrawMode mode, int count, IndexType type, int offset)
        {
            if (Target != BufferTarget.ElementArray)
            {
                throw new BufferException(this, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawElements)}.");
            }

            Bind();

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
        public void DrawElementsBaseVertex(DrawMode mode, int count, IndexType type, int offset, int baseVertex)
        {
            if (Target != BufferTarget.ElementArray)
            {
                throw new BufferException(this, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawElementsBaseVertex)}.");
            }

            Bind();

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
        public void DrawElementsIndirect(DrawMode mode, IndexType type, uint count, uint primCount, uint first, uint baseInstance)
        {
            if (Target != BufferTarget.ElementArray)
            {
                throw new BufferException(this, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawElementsIndirect)}.");
            }

            Bind();
            // Make sure there is no buffer bound for parameter reference
            State.NullBind(Graphics.Target.BufferDrawIndirect);

            if (GL.Version < 4.2)
            {
                baseInstance = 0;
            }

            uint* parameters = stackalloc uint[] { count, primCount, first, baseInstance };

            GL.DrawElementsIndirect((uint)mode, (uint)type, & parameters[0]);
        }
        /// <summary>
        /// Render indexed primitives from array data, taking parameters from memory.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="type">Specifies the type of data in this element buffer.</param>
        /// <param name="paramSource">The <see cref="IBuffer"/> that contains the parameter data.</param>
        /// <param name="offset">The offset into <paramref name="paramSource"/> where the parameters should be sourced from.</param>
        [OpenGLSupport(4.0)]
        public void DrawElementsIndirect(DrawMode mode, IndexType type, IBuffer paramSource, int offset)
        {
            if (Target != BufferTarget.Array)
            {
                throw new BufferException(this, $"Buffer has to have a Target of {BufferTarget.Array} to use DrawArraysIndirect(DrawMode, IBuffer, int).");
            }

            Bind();

            // Bind param buffer to param reference
            if (!paramSource.Bound(BufferTarget.DrawIndirect))
            {
                GL.BindBuffer(GLEnum.DrawIndirectBuffer, paramSource.Id);
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
        public void DrawElementsInstanced(DrawMode mode, int count, IndexType type, int offset, int instances)
        {
            if (Target != BufferTarget.ElementArray)
            {
                throw new BufferException(this, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawElementsInstanced)}.");
            }

            Bind();

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
        public void DrawElementsInstancedBaseInstance(DrawMode mode, int count, IndexType type, int offset, int instances, uint baseInstance)
        {
            if (Target != BufferTarget.ElementArray)
            {
                throw new BufferException(this, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawElementsInstancedBaseInstance)}.");
            }

            Bind();

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
        public void DrawElementsInstancedBaseVertex(DrawMode mode, int count, IndexType type, int offset, int instances, int baseVertex)
        {
            if (Target != BufferTarget.ElementArray)
            {
                throw new BufferException(this, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawElementsInstancedBaseVertex)}.");
            }

            Bind();

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
        public void DrawElementsInstancedBaseVertexBaseInstance(DrawMode mode, int count, IndexType type, int offset, int instances, int baseVertex, uint baseInstance)
        {
            if (Target != BufferTarget.ElementArray)
            {
                throw new BufferException(this, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawElementsInstancedBaseVertexBaseInstance)}.");
            }

            Bind();

            GL.DrawElementsInstancedBaseVertexBaseInstance((uint)mode, count, (uint)type, (void*)new IntPtr(offset), instances, baseVertex, baseInstance);
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="start">Specifies the minimum array index contained in this element buffer.</param>
        /// <param name="end">Specifies the maximum array index contained in this element buffer.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        public void DrawRangeElements(DrawMode mode, uint start, uint end, int count, IndexType type, int offset)
        {
            if (Target != BufferTarget.ElementArray)
            {
                throw new BufferException(this, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawRangeElements)}.");
            }

            Bind();

            GL.DrawRangeElements((uint)mode, start, end, count, (uint)type, (void*)new IntPtr(offset));
        }
        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="start">Specifies the minimum array index contained in this element buffer.</param>
        /// <param name="end">Specifies the maximum array index contained in this element buffer.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        /// <param name="baseVertex">Specifies a constant that should be added to each element of the array when being referenced.</param>
        [OpenGLSupport(3.2)]
        public void DrawRangeElementsBaseVertex(DrawMode mode, uint start, uint end, int count, IndexType type, int offset, int baseVertex)
        {
            if (Target != BufferTarget.ElementArray)
            {
                throw new BufferException(this, $"Buffer has to have a Target of {BufferTarget.ElementArray} to use {nameof(DrawRangeElementsBaseVertex)}.");
            }

            Bind();

            GL.DrawRangeElementsBaseVertex((uint)mode, start, end, count, (uint)type, (void*)new IntPtr(offset), baseVertex);
        }

        /// <summary>
        /// Indicate modifications to a range of a mapped buffer.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        [OpenGLSupport(3.0)]
        public void FlushMappedBufferRange(int offset, int length)
        {
            Bind();

            GL.FlushMappedBufferRange((uint)Target, offset, length);
        }
    }
}
