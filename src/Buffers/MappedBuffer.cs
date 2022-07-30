using System;
using Zene.Graphics.Base;

namespace Zene.Graphics
{
    /// <summary>
    /// An object the encapsulates a buffer's mapped region.
    /// </summary>
    public unsafe class MappedBuffer : IDisposable
    {
        public MappedBuffer(IBuffer buffer, IntPtr pointer)
        {
            Buffer = buffer;
            _pointer = (byte*)pointer.ToPointer();
        }

        /// <summary>
        /// The buffer who's region is mapped.
        /// </summary>
        public IBuffer Buffer { get; }
        /// <summary>
        /// The access type the map has.
        /// </summary>
        public AccessType Access => Buffer.Properties.MappedAccess;
        /// <summary>
        /// The access flags the map has.
        /// </summary>
        public MappedAccessFlags Flags => Buffer.Properties.MappedAccessFlags;

        private readonly byte* _pointer;
        /// <summary>
        /// The pointer to the buffers mapped data.
        /// </summary>
        public IntPtr Pointer => (IntPtr)_pointer;
        /// <summary>
        /// The size of the buffer's mapped region.
        /// </summary>
        public int Length => Buffer.Properties.MapLength;
        /// <summary>
        /// The offset into the buffer where the mapped region starts.
        /// </summary>
        public int Offset => Buffer.Properties.MapOffset;

        /// <summary>
        /// Retrieves a section of the mapped buffer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">The offset into the data in terms of bytes.</param>
        /// <param name="size">The size of the retrieved data in terms of <typeparamref name="T"/>.</param>
        /// <returns></returns>
        public ReadOnlySpan<T> Read<T>(int offset, int size) where T : unmanaged
        {
            if (!Access.CanRead())
            {
                throw new BufferException(Buffer, "The mapped buffer does not have read access.");
            }

            int byteSize = size * sizeof(T);

            if ((offset + byteSize) > Length)
            {
                throw new ArgumentOutOfRangeException($"{nameof(offset)}");
            }

            return new ReadOnlySpan<T>(_pointer + offset, byteSize);
        }
        /// <summary>
        /// Read a single value from the mapped buffer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">The offset into the data in terms of bytes.</param>
        /// <returns></returns>
        public T Read<T>(int offset) where T : unmanaged
        {
            if (!Access.CanRead())
            {
                throw new BufferException(Buffer, "The mapped buffer does not have read access.");
            }

            if ((offset + sizeof(T)) > Length)
            {
                throw new ArgumentOutOfRangeException($"{nameof(offset)}");
            }

            return *(T*)(_pointer + offset);
        }

        /// <summary>
        /// Write a block of data to a section of the mapped buffer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">The offset into the data in terms of bytes.</param>
        /// <param name="data">The data to copy to the buffer.</param>
        public void Write<T>(int offset, Span<T> data) where T : unmanaged
        {
            if (!Access.CanWrite())
            {
                throw new BufferException(Buffer, "The mapped buffer does not have write access.");
            }

            if (offset > Length)
            {
                throw new ArgumentOutOfRangeException($"{nameof(offset)}");
            }

            int sizeNeeded = offset + (data.Length * sizeof(T));

            // Make sure data doesn't overflow the allocated space
            int length = data.Length;
            if (sizeNeeded > Length)
            {
                length -= sizeNeeded - Length;
                fixed (T* ptr = &data[0])
                {
                    data = new Span<T>(ptr, length);
                }
            }

            Span<T> space = new Span<T>(_pointer + offset, length);

            data.CopyTo(space);

            if (sizeNeeded > Length)
            {
                throw new BufferException(Buffer, "The data parsed overflowed the size of the allocated buffer map.");
            }
        }
        /// <summary>
        /// Write a single value to the mapped buffer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="offset">The offset into the data in terms of bytes.</param>
        /// <param name="value">The value to write to the buffer.</param>
        public void Write<T>(int offset, T value) where T : unmanaged
        {
            if (!Access.CanWrite())
            {
                throw new BufferException(Buffer, "The mapped buffer does not have write access.");
            }

            if ((offset + sizeof(T)) > Length)
            {
                throw new ArgumentOutOfRangeException($"{nameof(offset)}");
            }

            T* ptr = (T*)(_pointer + offset);
            *ptr = value;
        }

        private bool _dispose = false;
        public void Dispose()
        {
            if (_dispose) { return; }

            //Buffer.UnmapBuffer();

            _dispose = true;
            GC.SuppressFinalize(this);
        }
        /*
/// <summary>
/// Updates the buffer with the new modifications.
/// </summary>
/// <param name="offset">The offset into the data in terms of bytes.</param>
/// <param name="length">The length of the updated region in terms of bytes.</param>
public void Flush(int offset, int length) => Buffer.FlushMappedBufferRange(offset, length);
*/
    }
}
