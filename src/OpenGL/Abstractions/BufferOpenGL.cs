using System;

namespace Zene.Graphics.Base.Extensions
{
    public static unsafe class BufferOpenGL
    {
        /// <summary>
        /// Creates and initializes a buffer object's data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Specifies the array of data that will be copied into the data store for initialization.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        public static void BufferData<T>(this IBuffer buffer, T[] data) where T : unmanaged
        {
            buffer.Bind();

            fixed (T* ptr = &data[0])
            {
                GL.BufferData(buffer, data.Length * sizeof(T), ptr, (uint)buffer.UsageType);
            }
        }
        /// <summary>
        /// Creates and initializes a buffer object's data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Specifies the array of data that will be copied into the data store for initialization.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        public static void BufferData<T>(this IBuffer buffer, GLArray<T> data) where T : unmanaged
        {
            buffer.Bind();

            GL.BufferData(buffer, data.Length * sizeof(T), data ?? (void*)0, (uint)buffer.UsageType);
        }
        /// <summary>
        /// Creates and initializes a buffer object's data store.
        /// </summary>
        /// <param name="size">Specifies the size in bytes of the buffer object's new data store.</param>
        /// <param name="data">Specifies a pointer to data that will be copied into the data store for initialization.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        public static void BufferData(this IBuffer buffer, int size, IntPtr data)
        {
            buffer.Bind();

            GL.BufferData(buffer, size, data.ToPointer(), (uint)buffer.UsageType);
        }
        /// <summary>
        /// Creates and initializes a buffer object's data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="length">Specifies the size of the array to be the buffer object's new data store.</param>
        /// <param name="data">Specifies a pointer to data that will be copied into the data store for initialization.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        public static void BufferData<T>(this IBuffer buffer, int length, T* data) where T : unmanaged
        {
            buffer.Bind();

            GL.BufferData(buffer, length * sizeof(T), data, (uint)buffer.UsageType);
        }

        /// <summary>
        /// Creates and initializes a buffer object's immutable data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Specifies the array of data that will be copied into the data store for initialization.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        [OpenGLSupport(4.4)]
        public static void BufferStorage<T>(this IBuffer buffer, T[] data) where T : unmanaged
        {
            buffer.Bind();

            fixed (T* ptr = &data[0])
            {
                GL.BufferStorage(buffer, data.Length * sizeof(T), ptr, (uint)buffer.UsageType);
            }
        }
        /// <summary>
        /// Creates and initializes a buffer object's immutable data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">Specifies the array of data that will be copied into the data store for initialization.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        [OpenGLSupport(4.4)]
        public static void BufferStorage<T>(this IBuffer buffer, GLArray<T> data) where T : unmanaged
        {
            buffer.Bind();

            GL.BufferStorage(buffer, data.Length * sizeof(T), data ?? (void*)0, (uint)buffer.UsageType);
        }
        /// <summary>
        /// Creates and initializes a buffer object's immutable data store.
        /// </summary>
        /// <param name="size">Specifies the size in bytes of the buffer object's new data store.</param>
        /// <param name="data">Specifies a pointer to data that will be copied into the data store for initialization.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        [OpenGLSupport(4.4)]
        public static void BufferStorage(this IBuffer buffer, int size, IntPtr data)
        {
            buffer.Bind();

            GL.BufferStorage(buffer, size, data.ToPointer(), (uint)buffer.UsageType);
        }
        /// <summary>
        /// Creates and initializes a buffer object's immutable data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="length">Specifies the size of the array to be the buffer object's new data store.</param>
        /// <param name="data">Specifies a pointer to data that will be copied into the data store for initialization.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        [OpenGLSupport(4.4)]
        public static void BufferStorage<T>(this IBuffer buffer, int length, T* data) where T : unmanaged
        {
            buffer.Bind();

            GL.BufferStorage(buffer, length * sizeof(T), data, (uint)buffer.UsageType);
        }

        /// <summary>
        /// Updates a subset of a buffer object's data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">Specifies the offset into the buffer object's data store where data replacement will begin, measured in <typeparamref name="T"/>.</param>
        /// <param name="data">Specifies the array of data that will be copied into the data store.</param>
        public static void BufferSubData<T>(this IBuffer buffer, int offset, T[] data) where T : unmanaged
        {
            buffer.Bind();

            fixed (T* ptr = &data[0])
            {
                GL.BufferSubData((uint)buffer.Target, offset * sizeof(T), data.Length * sizeof(T), ptr);
            }
        }
        /// <summary>
        /// Updates a subset of a buffer object's data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">Specifies the offset into the buffer object's data store where data replacement will begin, measured in <typeparamref name="T"/>.</param>
        /// <param name="data">Specifies the array of data that will be copied into the data store.</param>
        public static void BufferSubDataF<T>(this IBuffer buffer, int offset, T[] data) where T : unmanaged
        {
            buffer.Bind();

            fixed (T* ptr = &data[0])
            {
                GL.BufferSubData((uint)buffer.Target, offset, data.Length * sizeof(T), ptr);
            }
        }
        /// <summary>
        /// Updates a subset of a buffer object's data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">Specifies the offset into the buffer object's data store where data replacement will begin, measured in <typeparamref name="T"/>.</param>
        /// <param name="data">Specifies the array of data that will be copied into the data store.</param>
        public static void BufferSubData<T>(this IBuffer buffer, int offset, GLArray<T> data) where T : unmanaged
        {
            buffer.Bind();

            GL.BufferSubData((uint)buffer.Target, offset * sizeof(T), data.Length * sizeof(T), data ?? (void*)0);
        }
        /// <summary>
        /// Updates a subset of a buffer object's data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">Specifies the offset into the buffer object's data store where data replacement will begin, measured in <typeparamref name="T"/>.</param>
        /// <param name="data">Specifies the array of data that will be copied into the data store.</param>
        public static void BufferSubDataF<T>(this IBuffer buffer, int offset, GLArray<T> data) where T : unmanaged
        {
            buffer.Bind();

            GL.BufferSubData((uint)buffer.Target, offset, data.Length * sizeof(T), data ?? (void*)0);
        }
        /// <summary>
        /// Updates a subset of a buffer object's data store.
        /// </summary>
        /// <param name="offset">Specifies the offset into the buffer object's data store where data replacement will begin, measured in bytes.</param>
        /// <param name="size">Specifies the size in bytes of the data store region being replaced.</param>
        /// <param name="data">Specifies a pointer to the new data that will be copied into the data store.</param>
        public static void BufferSubData(this IBuffer buffer, int offset, int size, IntPtr data)
        {
            buffer.Bind();

            GL.BufferSubData((uint)buffer.Target, offset, size, data.ToPointer());
        }
        /// <summary>
        /// Updates a subset of a buffer object's data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">Specifies the offset into the buffer object's data store where data replacement will begin, measured in <typeparamref name="T"/>.</param>
        /// <param name="length">Specifies the size of the array the data store region being replaced.</param>
        /// <param name="data">Specifies a pointer to the new data that will be copied into the data store.</param>
        public static void BufferSubData<T>(this IBuffer buffer, int offset, int length, T* data) where T : unmanaged
        {
            buffer.Bind();

            GL.BufferSubData((uint)buffer.Target, offset * sizeof(T), length * sizeof(T), data);
        }
        /// <summary>
        /// Updates a subset of a buffer object's data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">Specifies the offset into the buffer object's data store where data replacement will begin, measured in <typeparamref name="T"/>.</param>
        /// <param name="length">Specifies the size of the array the data store region being replaced.</param>
        /// <param name="data">Specifies a pointer to the new data that will be copied into the data store.</param>
        public static void BufferSubDataF<T>(this IBuffer buffer, int offset, int length, T* data) where T : unmanaged
        {
            buffer.Bind();

            GL.BufferSubData((uint)buffer.Target, offset, length * sizeof(T), data);
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
        public static void ClearBufferData<T>(this IBuffer buffer, TextureFormat internalFormat, BaseFormat format, TextureData type, T value) where T : unmanaged 
        {
            buffer.Bind();

            GL.ClearBufferData((uint)buffer.Target, (uint)internalFormat, (uint)format, (uint)type, &value);
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
        public static void ClearBufferSubData<T>(this IBuffer buffer, TextureFormat internalFormat, int offset, int size, BaseFormat format, TextureData type, T value) where T : unmanaged
        {
            buffer.Bind();

            GL.ClearBufferSubData((uint)buffer.Target, (uint)internalFormat, offset, size, (uint)format, (uint)type, &value);
        }

        /// <summary>
        /// Copy all or part of the data store of a buffer object to the data store of another buffer object
        /// </summary>
        /// <param name="dest">Specifies the <see cref="IBuffer"/> to copy to.</param>
        /// <param name="srcOffset">Specifies the offset, in basic machine units, within the data store of the source buffer object at which data will be read.</param>
        /// <param name="destOffset">Specifies the offset, in basic machine units, within the data store of the destination buffer object at which data will be written.</param>
        /// <param name="size">Specifies the size, in basic machine units, of the data to be copied from the source buffer object to the destination buffer object.</param>
        public static void CopyBufferSubData(this IBuffer buffer, IBuffer dest, int srcOffset, int destOffset, int size)
        {
            BufferTarget srcTarget = buffer.BindRead();
            BufferTarget destTarget = dest.BindWrite();

            GL.CopyBufferSubData((uint)srcTarget, (uint)destTarget, srcOffset, destOffset, size);
        }

        /// <summary>
        /// Indicate modifications to a range of a mapped buffer.
        /// </summary>
        /// <param name="offset">Specifies the start of the buffer subrange, in basic machine units.</param>
        /// <param name="length">Specifies the length of the buffer subrange, in basic machine units.</param>
        [OpenGLSupport(3.0)]
        public static void FlushMappedBufferRange(this IBuffer buffer, int offset, int length)
        {
            if (!buffer.Properties.Mapped)
            {
                throw new BufferException(buffer, $"Cannot use {nameof(FlushMappedBufferRange)} when the buffer is not mapped.");
            }

            if (!buffer.Properties.MappedAccessFlags.HasFlag(MappedAccessFlags.FlushExplicit))
            {
                throw new BufferException(buffer, $"{nameof(buffer.Properties.MappedAccessFlags)} must contain {MappedAccessFlags.FlushExplicit} to use {nameof(FlushMappedBufferRange)}.");
            }

            buffer.Bind();

            GL.FlushMappedBufferRange((uint)buffer.Target, offset, length);
        }

        /// <summary>
        /// Returns a subset of a buffer object's data store.
        /// </summary>
        /// <remarks>
        /// Reference is aligned to sizeof(<typeparamref name="T"/>).
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">Specifies the offset into the buffer object's data store from which data will be returned, measured in sizeof(<typeparamref name="T"/>).</param>
        /// <param name="size">Specifies the size in sizeof(<typeparamref name="T"/>) of the data store region being returned.</param>
        /// <returns></returns>
        [OpenGLSupport(1.5)]
        public static T[] GetBufferSubData<T>(this IBuffer buffer, int offset, int size) where T : unmanaged
        {
            buffer.Bind();

            T[] data = new T[size];

            fixed (T* ptr = &data[0])
            {
                GL.GetBufferSubData((uint)buffer.Target, offset * sizeof(T), size * sizeof(T), ptr);
            }

            return data;
        }
        /// <summary>
        /// Returns a subset of a buffer object's data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">Specifies the offset into the buffer object's data store from which data will be returned, measured in bytes.</param>
        /// <param name="size">Specifies the size in sizeof(<typeparamref name="T"/>) of the data store region being returned.</param>
        /// <returns></returns>
        public static T[] GetBufferSubDataF<T>(this IBuffer buffer, int offset, int size) where T : unmanaged
        {
            buffer.Bind();

            T[] data = new T[size];

            fixed (T* ptr = &data[0])
            {
                GL.GetBufferSubData((uint)buffer.Target, offset, size * sizeof(T), ptr);
            }

            return data;
        }
        /// <summary>
        /// Returns a subset of a buffer object's data store.
        /// </summary>
        /// <remarks>
        /// Reference is aligned to sizeof(<typeparamref name="T"/>).
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">Specifies the offset into the buffer object's data store from which data will be returned, measured in sizeof(<typeparamref name="T"/>).</param>
        /// <param name="size">Specifies the size in sizeof(<typeparamref name="T"/>) of the data store region being returned.</param>
        /// <returns></returns>
        public static GLArray<T> GetBufferSubDataA<T>(this IBuffer buffer, int offset, int size) where T : unmanaged
        {
            buffer.Bind();

            GLArray<T> data = new GLArray<T>(size);

            GL.GetBufferSubData((uint)buffer.Target, offset * sizeof(T), size * sizeof(T), data);

            return data;
        }
        /// <summary>
        /// Returns a subset of a buffer object's data store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">Specifies the offset into the buffer object's data store from which data will be returned, measured in bytes.</param>
        /// <param name="size">Specifies the size in sizeof(<typeparamref name="T"/>) of the data store region being returned.</param>
        /// <returns></returns>
        public static GLArray<T> GetBufferSubDataAF<T>(this IBuffer buffer, int offset, int size) where T : unmanaged
        {
            buffer.Bind();

            GLArray<T> data = new GLArray<T>(size);

            GL.GetBufferSubData((uint)buffer.Target, offset, size * sizeof(T), data);

            return data;
        }

        /// <summary>
        /// Invalidate the content of a buffer object's data store.
        /// </summary>
        [OpenGLSupport(4.3)]
        public static void InvalidateBufferData(this IBuffer buffer) => GL.InvalidateBufferData(buffer.Id);

        /// <summary>
        /// Invalidate a region of a buffer object's data store.
        /// </summary>
        /// <param name="offset">The offset, in bytes, within the buffer's data store of the start of the range to be invalidated.</param>
        /// <param name="length">The length, in bytes, of the range within the buffer's data store to be invalidated.</param>
        [OpenGLSupport(4.3)]
        public static void InvalidateBufferSubData(this IBuffer buffer, int offset, int length)
        {
            GL.InvalidateBufferSubData(buffer.Id, offset, length);
        }

        /// <summary>
        /// Map all of a buffer object's data store into the client's address space.
        /// </summary>
        /// <param name="access">Specifies the access policy for the mapped data.</param>
        /// <returns></returns>
        public static MappedBuffer MapBuffer(this IBuffer buffer, AccessType access)
        {
            buffer.Bind();

            IntPtr ptr = GL.MapBuffer(buffer, (uint)access);

            return new MappedBuffer(buffer, ptr);
        }
        /// <summary>
        /// Map all or part of a buffer object's data store into the client's address space.
        /// </summary>
        /// <param name="offset">Specifies the starting offset within the buffer of the range to be mapped.</param>
        /// <param name="length">Specifies the length of the range to be mapped.</param>
        /// <param name="access">Specifies a combination of access flags indicating the desired access to the mapped range.</param>
        /// <returns></returns>
        [OpenGLSupport(3.0)]
        public static MappedBuffer MapBufferRange(this IBuffer buffer, int offset, int length, MappedAccessFlags access)
        {
            if (!access.HasFlag(MappedAccessFlags.Read) &&
                !access.HasFlag(MappedAccessFlags.Write))
            {
                throw new ArgumentException($"{nameof(access)} must contain {MappedAccessFlags.Read} or {MappedAccessFlags.Write}.", $"{nameof(access)}");
            }

            if (access.HasFlag(MappedAccessFlags.Read) &&
                (access.HasFlag(MappedAccessFlags.InvalidateRange) ||
                access.HasFlag(MappedAccessFlags.InvalidateBuffer) ||
                access.HasFlag(MappedAccessFlags.Unsynchronized)))
            {
                throw new ArgumentException($@"{nameof(access)} cannot contain {MappedAccessFlags.Read} and {MappedAccessFlags.InvalidateRange},
{MappedAccessFlags.InvalidateBuffer} or {MappedAccessFlags.Unsynchronized}.", $"{nameof(access)}");
            }

            if (access.HasFlag(MappedAccessFlags.FlushExplicit) &&
                !access.HasFlag(MappedAccessFlags.Write))
            {
                throw new ArgumentException($"{nameof(access)} must contain {MappedAccessFlags.Write} when including {MappedAccessFlags.FlushExplicit} flag.", $"{nameof(access)}");
            }

            buffer.Bind();

            IntPtr ptr = GL.MapBufferRange(buffer, offset, length, (uint)access);

            return new MappedBuffer(buffer, ptr);
        }

        /// <summary>
        /// Release the mapping of a buffer object's data store into the client's address space.
        /// </summary>
        [OpenGLSupport(1.5)]
        public static void UnmapBuffer(this IBuffer buffer)
        {
            if (!buffer.Properties.Mapped)
            {
                throw new BufferException(buffer, "The buffer is already unmapped.");
            }

            buffer.Bind();

            GL.UnmapBuffer(buffer);
        }
    }
}
