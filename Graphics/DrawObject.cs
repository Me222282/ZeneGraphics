using System;
using System.Collections.Generic;
using System.Linq;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public class DrawObject<T, IndexT> : GLObject, IDrawable where T : unmanaged where IndexT : unmanaged
    {
        public DrawObject(IEnumerable<T> vertices, IEnumerable<IndexT> indices, uint dataSplit, int vertexIndex, AttributeSize vertexSize, BufferUsage usage)
        {
            if ((int)vertexSize == 1) { throw new Exception("Cannot have a 1 dimensional vertex position."); }

            Vao = new VertexArray();

            Buffer = new ArrayBuffer<T>(vertices, dataSplit, usage);

            _vertexNumber = (uint)vertices.Count() / dataSplit;

            Ibo = new IndexBuffer<IndexT>(indices, usage);

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
                ushort => GLEnum.UShort,
                byte => GLEnum.UByte,
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

        public virtual void SetData(IEnumerable<T> vertices)
        {
            Buffer.SetData(vertices);

            if (vertices.Count() / Buffer.DataSplit != _vertexNumber)
            {
                throw new Exception("Cannot change number of vertices without specifying an index array.");
            }
        }

        public virtual void SetData(IEnumerable<T> vertices, IEnumerable<IndexT> indices)
        {
            _vertexNumber = (uint)vertices.Count() / Buffer.DataSplit;

            Buffer.SetData(vertices);

            Ibo.SetData(indices);
        }

        public void AddAttribute(uint index, int dataStart, AttributeSize attributeSize)
        {
            if (!_dataCreated)
            {
                CreateData();
            }

            Vao.AddBuffer(Buffer, index, dataStart, _dataType, attributeSize);
        }

        public override void CreateData()
        {
            Buffer.CreateData();
            Vao.CreateData();
            Ibo.CreateData();

            _dataCreated = true;
        }

        public override void DeleteData()
        {
            if (!DataCreated) { return; }

            Buffer.DeleteData();
            Vao.DeleteData();
            Ibo.DeleteData();

            _dataCreated = false;

            _bound = false;
        }

        public override void Bind()
        {
            Vao.Bind();
            Ibo.Bind();

            _bound = true;
        }

        public override void Unbind()
        {
            Vao.Unbind();
            Ibo.Unbind();

            _bound = false;
        }

        public virtual void Draw()
        {
            if (!_dataCreated)
            {
                CreateData();
            }

            bool bound = _bound;

            if (!bound) { Bind(); }

            GL.DrawElements(GLEnum.Triangles, Ibo.Size, _drawType, IntPtr.Zero);

            if (!bound) { Unbind(); }
        }

        /// <summary>
        /// Draw <paramref name="n"/> number of instances of this object.
        /// </summary>
        /// <param name="n">The amount of copies to draw.</param>
        public unsafe void DrawMultiple(int n)
        {
            if (!_dataCreated)
            {
                CreateData();
            }

            bool bound = _bound;

            if (!bound) { Bind(); }

            GL.DrawElementsInstanced(GLEnum.Triangles, Ibo.Size, _drawType, IntPtr.Zero.ToPointer(), n);

            if (!bound) { Unbind(); }
        }
    }
}
