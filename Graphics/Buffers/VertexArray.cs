using Zene.Graphics.Base;
using System;
using System.Collections.Generic;

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
            Id = 0;

            data = new List<VertexAttrib>();

            _bound = false;

            _dataCreated = false;

            CreateData();
        }

        public uint Id { get; private set; }

        private readonly List<VertexAttrib> data;

        public void AddBuffer<T>(ArrayBuffer<T> buffer, uint index, int dataStart, DataType dataType, AttributeSize attributeSize) where T : unmanaged
        {
            if (!buffer.DataCreated) {
                throw new Exception("Cannot add uncreated buffer."); }

            int typeSize = sizeof(T);

            VertexAttrib ad = new VertexAttrib(buffer.Id, index, (int)attributeSize, dataType, typeSize * (int)buffer.DataSplit, dataStart * typeSize);

            if (!data.Exists(a => a.index == index))
            {
                data.Add(ad);
            }

            bool bound = _bound;
            bool bBound = buffer.Bound;

            if (!bound) { Bind(); }

            AddBuffer(ad);

            if (!bound) { Unbind(); }
            if (!bBound) { buffer.Unbind(); }
        }

        private static void AddBuffer(VertexAttrib data)
        {
            GL.BindBuffer(GLEnum.ArrayBuffer, data.id);

            GL.EnableVertexAttribArray(data.index);

            GL.VertexAttribPointer(data.index, data.size, (uint)data.type, false, data.stride, new IntPtr(data.pointer));
        }

        public override void CreateData()
        {
            if (DataCreated) { return; }

            Id = GL.GenVertexArray();

            Bind();
            foreach (VertexAttrib d in data) { AddBuffer(d); }
            Unbind();
            GL.BindBuffer(GLEnum.ArrayBuffer, 0);

            _dataCreated = true;
        }

        public override void DeleteData()
        {
            if (!DataCreated) { return; }

            Unbind();
            GL.DeleteVertexArray(Id);

            Id = 0;

            _dataCreated = false;
        }

        public override void Bind()
        {
            GL.BindVertexArray(Id);

            _bound = true;
        }

        public override void Unbind()
        {
            GL.BindVertexArray(0);

            _bound = false;
        }

        private struct VertexAttrib
        {
            public VertexAttrib(uint id, uint index, int size, DataType type, int stride, int pointer)
            {
                this.id = id;
                this.index = index;
                this.size = size;

                this.type = type;
                this.stride = stride;
                this.pointer = pointer;
            }

            public uint id;

            public uint index;

            public int size;

            public DataType type;

            public int stride;

            public int pointer;
        }
    }
}
