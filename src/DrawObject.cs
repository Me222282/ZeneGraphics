using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public class DrawObject<T, IndexT> : IBindable, IDisposable, IDrawable where T : unmanaged where IndexT : unmanaged
    {
        public DrawObject(ReadOnlySpan<T> vertices, ReadOnlySpan<IndexT> indices, uint dataSplit, int vertexIndex, AttributeSize vertexSize, BufferUsage usage)
        {
            if ((int)vertexSize == 1) { throw new Exception("Cannot have a 1 dimensional vertex position."); }

            Vao = new VertexArray();

            Buffer = new ArrayBuffer<T>(dataSplit, usage);
            Buffer.SetData(vertices);

            _vertexNumber = (uint)vertices.Length / dataSplit;

            Ibo = new IndexBuffer<IndexT>(usage);
            Ibo.SetData(indices);

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

            Vao.AddBuffer(Buffer, 0, vertexIndex, _dataType, vertexSize);

            IndexT indexType = default;
            _drawType = indexType switch
            {
                byte => GLEnum.UByte,
                ushort => GLEnum.UShort,
                _ => GLEnum.UInt,
            };
        }

        public VertexArray Vao { get; private set; }

        public ArrayBuffer<T> Buffer { get; private set; }

        public IndexBuffer<IndexT> Ibo { get; private set; }

        public BufferUsage Usage { get; }

        private uint _vertexNumber;

        private readonly uint _drawType;

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
            Vao.AddBuffer(Buffer, index, dataStart, _dataType, attributeSize);
        }

        public void Bind()
        {
            Vao.Bind();
            Ibo.Bind();
        }
        public void Unbind()
        {
            Vao.Unbind();
            Buffer.Unbind();
            Ibo.Unbind();
        }

        public virtual void Draw()
        {
            Bind();

            GL.DrawElements(GLEnum.Triangles, Ibo.Size, _drawType, IntPtr.Zero);
        }

        /// <summary>
        /// Draw <paramref name="n"/> number of instances of this object.
        /// </summary>
        /// <param name="n">The amount of copies to draw.</param>
        public unsafe void DrawMultiple(int n)
        {
            Bind();

            GL.DrawElementsInstanced(GLEnum.Triangles, Ibo.Size, _drawType, IntPtr.Zero.ToPointer(), n);
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            Buffer.Dispose();
            Ibo.Dispose();
            Vao.Dispose();

            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
