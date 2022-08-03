using System;
using Zene.Graphics.Base;

namespace Zene.Graphics
{
    public unsafe class Buffer<T> : BufferGL where T : unmanaged
    {
        public Buffer(BufferTarget bufferType, BufferUsage usage)
            : base(bufferType)
        {
            _targetUsage = usage;
        }

        private readonly BufferUsage _targetUsage;

        public virtual unsafe int Size => Properties.Size / sizeof(T);

        public T[] GetData() => GetBufferSubData<T>(0, Properties.Size / sizeof(T));

        public virtual void SetData(ReadOnlySpan<T> data)
        {
            fixed (T* ptr = &data[0])
            {
                BufferData(data.Length, ptr, _targetUsage);
            }
        }
        public void InitData(int size) => BufferData(size * sizeof(T), IntPtr.Zero, _targetUsage);

        public void EditData(int offset, ReadOnlySpan<T> data)
        {
            if ((offset + data.Length) >= Properties.Size)
            {
                throw new BufferException(this, "Tried to write outside buffer range.");
            }

            fixed (T* ptr = &data[0])
            {
                BufferSubData(offset, data.Length, ptr);
            }
        }

        public MappedBuffer Map(AccessType access) => MapBuffer(access);
        public void Unmap() => UnmapBuffer();
    }
}
