namespace Zene.Graphics
{
    public class Drawable
    {
        public Drawable(IVertexArray va, RenderInfo info)
        {
            VertexArray = va;
            Info = info;
        }
        public Drawable(IVertexArray va, RenderInfo info, int instances)
        {
            VertexArray = va;
            Info = info;
            Instances = instances;
        }

        internal IVertexArray VertexArray { get; }

        public RenderInfo Info { get; }
        public int Instances { get; } = 1;
    }
}
