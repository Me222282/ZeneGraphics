namespace Zene.Graphics
{
    public struct RenderInfo
    {
        public RenderInfo(int count)
            : this(DrawMode.Triangles, 0, count)
        {
            
        }
        public RenderInfo(int count, IndexType it)
            : this(DrawMode.Triangles, 0, it, count)
        {
            
        }

        public RenderInfo(DrawMode dm, int offset, int count)
        {
            DrawMode = dm;
            VertexOffset = offset;
            VertexCount = count;

            Indexed = false;
            IndexType = IndexType.None;
        }
        public RenderInfo(DrawMode dm, int offset, IndexType it, int count)
        {
            DrawMode = dm;
            VertexOffset = offset;
            VertexCount = count;

            Indexed = true;
            IndexType = it;
        }

        public DrawMode DrawMode { get; }
        public int VertexOffset { get; }
        public int VertexCount { get; }

        public bool Indexed { get; }
        public IndexType IndexType { get; }
    }
}
