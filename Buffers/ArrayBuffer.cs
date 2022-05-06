using System;
using System.Collections.Generic;
using System.Linq;
using Zene.Structs;

namespace Zene.Graphics
{
    public class ArrayBuffer<T> : Buffer<T> where T : unmanaged
    {
        public ArrayBuffer(IEnumerable<T> data, uint dataSplit, BufferUsage usage)
            : base(data, BufferTarget.Array, usage, true)
        {            
            if (_data.Length % dataSplit != 0) {
                throw new Exception("Data doesn't fit the given split."); }

            DataSplit = dataSplit;
        }

        public uint DataSplit { get; private set; }

        public override int Size
        {
            get
            {
                return _data.Length / (int)DataSplit;
            }
        }

        public override void SetData(IEnumerable<T> data)
        {
            _data = data.ToArray();

            if (_data.Length % DataSplit != 0)
            {
                throw new Exception("Data doesn't fit the given split.");
            }

            AsignData(true);
        }

        public void SetData(IEnumerable<T> data, uint dataSplit)
        {
            _data = data.ToArray();

            DataSplit = dataSplit;

            if (_data.Length % DataSplit != 0) {
                throw new Exception("Data doesn't fit the given split."); }

            AsignData(true);
        }

        protected override bool ValideType(Type type)
        {
            return type == typeof(sbyte)
                || type == typeof(byte)
                || type == typeof(short)
                || type == typeof(ushort)
                || type == typeof(int)
                || type == typeof(uint)
                || type == typeof(float)
                || type == typeof(double)
                || type == typeof(Vector2)
                || type == typeof(Vector3)
                || type == typeof(Vector4)
                || type == typeof(Vector2I)
                || type == typeof(Vector3I)
                || type == typeof(Vector4I);
        }
    }
}
