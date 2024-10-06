using System;
using Zene.Graphics.Base;
using Zene.Graphics.Base.Extensions;

namespace Zene.Graphics
{
    public unsafe class BufferTexture : TextureGL
    {
        public BufferTexture(TextureFormat format)
            : base(TextureTarget.Buffer)
        {
            _targetFormat = format;
        }

        private readonly TextureFormat _targetFormat;
        private IBuffer _buffer;

        public virtual unsafe int Size => _buffer.Properties.Size;
        public IBuffer Buffer => _buffer;

        public void LinkBuffer(IBuffer buffer)
        {
            _buffer = buffer;
            TexBuffer(buffer, _targetFormat);
        }

        public T[] GetData<T>() where T : unmanaged
            => _buffer.GetBufferSubData<T>(0, _buffer.Properties.Size / sizeof(T));

        public void SetData<T>(ReadOnlySpan<T> data) where T : unmanaged
        {
            fixed (T* ptr = &data[0])
            {
                _buffer.BufferData(data.Length, ptr);
            }
        }

        public void InitData<T>(int size) where T : unmanaged
        {
            _buffer.BufferData(size * sizeof(T), IntPtr.Zero);
        }

        public void EditData<T>(int offset, ReadOnlySpan<T> data) where T : unmanaged
        {
            if ((offset + data.Length) >= _buffer.Properties.Size)
            {
                throw new BufferException(_buffer, "Tried to write outside buffer range.");
            }

            fixed (T* ptr = &data[0])
            {
                _buffer.BufferSubData(offset, data.Length, ptr);
            }
        }

        public MappedBuffer Map(AccessType access) => _buffer.MapBuffer(access);
        public void Unmap() => _buffer.UnmapBuffer();
    }
}
