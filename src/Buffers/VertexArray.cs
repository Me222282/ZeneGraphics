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

        public void Draw(DrawMode mode, int first, int size) => DrawArrays(mode, first, size);
        public void DrawElements(IBuffer elements, DrawMode mode, IndexType type, int offset)
        {
            int tSize = type switch
            {
                IndexType.Byte => 1,
                IndexType.Uint => 4,
                IndexType.Ushort => 2,
                _ => 1
            };

            DrawElements(elements, mode, elements.Properties.Size / tSize, type, offset);
        }
    }
}
