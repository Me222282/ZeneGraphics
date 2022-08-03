using System;

namespace Zene.Graphics
{
    public class ArrayBuffer<T> : Buffer<T> where T : unmanaged
    {
        public ArrayBuffer(uint dataSplit, BufferUsage usage)
            : base(BufferTarget.Array, usage)
        {
            DataSplit = dataSplit;
        }

        public uint DataSplit { get; private set; }

        public override int Size => base.Size / (int)DataSplit;

        public override void SetData(ReadOnlySpan<T> data)
        {
            if (data.Length % DataSplit != 0)
            {
                throw new Exception("Data doesn't fit the given split.");
            }

            base.SetData(data);
        }

        public void SetData(ReadOnlySpan<T> data, uint dataSplit)
        {
            DataSplit = dataSplit;

            base.SetData(data);
        }
    }
}
