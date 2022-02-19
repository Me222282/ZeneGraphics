using System;
using System.Collections.Generic;
using Zene.Graphics.Base;

namespace Zene.Graphics
{
    public unsafe class ShaderStorageBuffer<T> : Buffer<T> where T : unmanaged
    {
        public ShaderStorageBuffer(IEnumerable<T> data, BufferUsage usage)
            : base(data, BufferTarget.ShaderStorage, usage, false)
        {

        }

        public void AddData(T item)
        {
            T[] data = new T[] { item };

            Array.Resize(ref _data, _data.Length + 1);
            Array.Copy(data, 0, _data, _data.Length - 1, 1);

            AsignData(false);
        }

        public void AddRange(T[] data)
        {
            Array.Resize(ref _data, _data.Length + data.Length);
            Array.Copy(data, 0, _data, _data.Length - data.Length, data.Length);

            AsignData(false);
        }

        public void SetData(int index, T item)
        {
            T[] data = new T[] { item };

            _data[index] = item;

            bool reAsign = true;

            if (!DataCreated)
            {
                Id = GL.GenBuffer();
                reAsign = false;
            }

            bool bound = Bound;

            if (!bound) { Bind(); }

            if (reAsign)
            {
                GL.BufferSubData((uint)Target, index * sizeof(T), sizeof(T), data);
            }
            else
            {
                GL.BufferData((uint)Target, _data.Length * sizeof(T), _data, (uint)UsageType);
            }

            _dataCreated = true;

            if (!bound) { Unbind(); }
        }

        public void SetDataRange(int index, T[] data)
        {
            Array.Copy(data, 0, _data, index, data.Length);

            bool reAsign = true;

            if (!DataCreated)
            {
                Id = GL.GenBuffer();
                reAsign = false;
            }

            bool bound = Bound;

            if (!bound) { Bind(); }

            if (reAsign)
            {
                GL.BufferSubData((uint)Target, index * sizeof(T), data.Length * sizeof(T), data);
            }
            else
            {
                GL.BufferData((uint)Target, _data.Length * sizeof(T), _data, (uint)UsageType);
            }

            _dataCreated = true;

            if (!bound) { Unbind(); }
        }

        protected override void AsignData(bool reAsign)
        {
            if (!DataCreated)
            {
                Id = GL.GenBuffer();
            }

            bool bound = Bound;

            if (!bound) { Bind(); }

            if (reAsign)
            {
                GL.BufferSubData((uint)Target, 0, _data.Length * sizeof(T), _data);
            }
            else
            {
                GL.BufferData((uint)Target, _data.Length * sizeof(T), _data, (uint)UsageType);
            }

            _dataCreated = true;

            if (!bound) { Unbind(); }
        }

        public void Bind(uint index)
        {
            GL.BindBufferBase((uint)Target, index, Id);
        }
    }
}
