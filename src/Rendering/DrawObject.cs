using System;
using Zene.Structs;

namespace Zene.Graphics
{
    public class DrawObject<T, IndexT> : VertexArray, IBindable, IDisposable
        where T : unmanaged where IndexT : unmanaged
    {
        public DrawObject(ReadOnlySpan<T> vertices, ReadOnlySpan<IndexT> indices, uint dataSplit, int vertexIndex, AttributeSize vertexSize, BufferUsage usage)
        {
            if ((int)vertexSize == 1) { throw new Exception("Cannot have a 1 dimensional vertex position."); }

            Buffer = new ArrayBuffer<T>(dataSplit, usage);
            Buffer.SetData(vertices);

            _vertexNumber = (uint)vertices.Length / dataSplit;

            Ibo = new IndexBuffer<IndexT>(usage);
            Ibo.SetData(indices);

            SetElementBuffer(Ibo);

            T t = default;
            _dataType = t switch
            {
                Vector2I or Vector3I or Vector4I or int => DataType.Int,
                uint => DataType.UInt,
                float => DataType.Float,
                Vector2 or Vector3 or Vector4 or double => DataType.Double,
                short => DataType.Short,
                ushort => DataType.UShort,
                byte => DataType.UByte,
                sbyte => DataType.Byte,
                _ => 0,
            };

            AddBuffer(Buffer, ShaderLocation.Vertex, vertexIndex, _dataType, vertexSize);

            IndexT indexType = default;
            IndexType = indexType switch
            {
                byte => IndexType.Byte,
                ushort => IndexType.Ushort,
                _ => IndexType.Uint,
            };
        }

        public ArrayBuffer<T> Buffer { get; private set; }
        public IndexBuffer<IndexT> Ibo { get; private set; }

        public BufferUsage Usage => Buffer.UsageType;

        private uint _vertexNumber;
        public IndexType IndexType { get; }
        private readonly DataType _dataType;

        public virtual void SetData(ReadOnlySpan<T> vertices)
        {
            Buffer.SetData(vertices.ToArray());

            if (vertices.Length / Buffer.DataSplit != _vertexNumber)
            {
                throw new Exception("Cannot change number of vertices without specifying an index array.");
            }
        }
        public virtual void SetData(ReadOnlySpan<T> vertices, ReadOnlySpan<IndexT> indices)
        {
            _vertexNumber = (uint)vertices.Length / Buffer.DataSplit;

            Buffer.SetData(vertices.ToArray());

            Ibo.SetData(indices.ToArray());
        }

        public void AddAttribute(uint index, int dataStart, AttributeSize attributeSize)
        {
            AddBuffer(Buffer, index, dataStart, _dataType, attributeSize);
        }
        public void AddAttribute(ShaderLocation index, int dataStart, AttributeSize attributeSize)
        {
            AddBuffer(Buffer, index, dataStart, _dataType, attributeSize);
        }

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            Buffer.Dispose();
            Ibo.Dispose();
        }
    }
}
