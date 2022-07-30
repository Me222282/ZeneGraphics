using System;
using System.Collections.Generic;
using System.Linq;
using Zene.Graphics.Base;

namespace Zene.Graphics
{
    public unsafe class Buffer<T> : IBuffer where T : unmanaged
    {
        public Buffer(IEnumerable<T> data, BufferTarget bufferType, BufferUsage usage)
            : this(data, bufferType, usage, true)
        {
            
        }

        protected Buffer(IEnumerable<T> data, BufferTarget bufferType, BufferUsage usage, bool checkType)
        {
            if (checkType)
            {
                Type typeOf = typeof(T);

                if (!ValideType(typeOf))
                {
                    throw new Exception($"{typeOf.FullName} is not a valide generic type T for Buffer<T>.");
                }
            }

            Id = 0;

            _data = data.ToArray();
            Target = bufferType;
            UsageType = usage;
            _dataCreated = false;
            _bound = false;

            CreateData();
        }

        public uint Id { get; protected set; }

        public BufferTarget BindRead()
        {
            throw new NotImplementedException();
        }
        public BufferTarget BindWrite()
        {
            throw new NotImplementedException();
        }

        protected bool _dataCreated;
        public bool DataCreated
        {
            get
            {
                return _dataCreated;
            }
            set
            {
                if (value && (!_dataCreated))
                {
                    CreateData();
                }
                else if ((!value) && _dataCreated)
                {
                    DeleteData();
                }
            }
        }

        protected bool _bound;
        public bool Bound
        {
            get
            {
                return _bound;
            }
            set
            {
                if (value && (!_bound))
                {
                    Bind();
                }
                else if ((!value) && _bound)
                {
                    Unbind();
                }
            }
        }

        public BufferUsage UsageType { get; }

        public BufferTarget Target { get; }

        public virtual int Size
        {
            get
            {
                return _data.Length;
            }
        }

        public BufferProperties Properties => throw new NotImplementedException();

        protected T[] _data;

        public Type GetTypeT()
        {
            return typeof(T);
        }

        public T[] GetData()
        {
            T[] get = Array.Empty<T>();

            _data.CopyTo(get, 0);

            return get;
        }

        public virtual void SetData(IEnumerable<T> data)
        {
            _data = data.ToArray();

            AsignData(true);
        }

        protected virtual void AsignData(bool reAsign)
        {
            if (!DataCreated)
            {
                Id = GL.GenBuffer();
                reAsign = false;
            }

            bool bound = _bound;

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

        public virtual void CreateData()
        {
            if (DataCreated) { return; }

            AsignData(false);
        }

        public virtual void DeleteData()
        {
            if (!DataCreated) { return; }

            Unbind();
            GL.DeleteBuffer(Id);

            Id = 0;

            _dataCreated = false;
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            DeleteData();

            _disposed = true;

            GC.SuppressFinalize(this);
        }

        public virtual void Bind()
        {
            GL.BindBuffer((uint)Target, Id);

            _bound = true;
        }

        public virtual void Unbind()
        {
            GL.BindBuffer((uint)Target, 0);

            _bound = false;
        }

        protected virtual bool ValideType(Type type)
        {
            return true;
        }
    }
}
