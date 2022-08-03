using Zene.Graphics.Base;
using System;

namespace Zene.Graphics
{
    public enum AttributeSize
    {
        D1 = 1,
        D2 = 2,
        D3 = 3,
        D4 = 4
    }

    public enum DataType : uint
    {
        Byte = 0x1400,
        UByte = 0x1401,
        Short = 0x1402,
        UShort = 0x1403,
        Int = 0x1404,
        UInt = 0x1405,
        HalfFloat = 0x140b,
        Float = 0x1406,
        Double = 0x140a,
        Fixed = 0x140c,
        IntRev = 0x8d9f,
        UIntRev = 0x8368,
        UIntFRev = 0x8c3b
    }

    public unsafe class VertexArray : GLObject
    {
        public VertexArray()
        {
            Id = GL.GenVertexArray();
        }

        public uint Id { get; }

        public void AddBuffer<T>(ArrayBuffer<T> buffer, uint index, int dataStart, DataType dataType, AttributeSize attributeSize) where T : unmanaged
        {
            int typeSize = sizeof(T);

            Bind();

            buffer.Bind();
            GL.EnableVertexAttribArray(index);
            GL.VertexAttribPointer(index, (int)attributeSize, (uint)dataType, false, typeSize * (int)buffer.DataSplit, new IntPtr(dataStart * typeSize));

            //buffer.Unbind();
            //Unbind();
        }

        public override void CreateData()
        {

        }

        public override void DeleteData()
        {
            GL.DeleteVertexArray(Id);
        }

        public override void Bind()
        {
            GL.BindVertexArray(Id);
        }

        public override void Unbind()
        {
            GL.BindVertexArray(0);
        }
    }
}
