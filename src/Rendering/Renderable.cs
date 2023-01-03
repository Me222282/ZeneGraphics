using Zene.Structs;

namespace Zene.Graphics
{
    public class Renderable
    {
        public Renderable(IVertexArray va, RenderInfo info)
        {
            FromFramebuffer = false;

            vertexArray = va;
            Info = info;
        }
        public Renderable(IVertexArray va, RenderInfo info, int instances)
        {
            FromFramebuffer = false;

            vertexArray = va;
            Info = info;
            Instances = instances;
        }
        public Renderable(IFramebuffer framebuffer, IBox bounds, BufferBit bb, TextureSampling sampling)
        {
            FromFramebuffer = true;

            this.framebuffer = framebuffer;
            framebufferBounds = bounds;
            bufferBit = bb;
            this.sampling = sampling;
        }

        public bool FromFramebuffer { get; }
        internal IBox framebufferBounds;
        internal BufferBit bufferBit;
        internal TextureSampling sampling;

        public IGLObject DrawingObject => FromFramebuffer ? framebuffer : vertexArray;
        internal IVertexArray vertexArray;
        internal IFramebuffer framebuffer;

        public RenderInfo Info { get; }
        public int Instances { get; } = 1;
    }
}
