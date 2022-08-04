using Zene.Graphics.Base;

namespace Zene.Graphics
{
    public unsafe class VertexArray : VertexArrayGL
    {
        public VertexArray()
        {
            
        }

        public void AddBuffer<T>(ArrayBuffer<T> buffer, uint index, int dataStart, DataType dataType, AttributeSize attributeSize) where T : unmanaged
        {
            int typeSize = sizeof(T);

            buffer.Bind();
            EnableVertexAttribArray(index);
            VertexAttribPointer(index, attributeSize, dataType, false, typeSize * (int)buffer.DataSplit, dataStart * typeSize);
        }
    }
}
