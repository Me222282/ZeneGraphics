using System;

namespace Zene.Graphics
{
    public class VertexArrayProperties : IProperties
    {
        public VertexArrayProperties(IVertexArray source)
        {
            Source = source;
        }

        public IVertexArray Source { get; }
        IGLObject IProperties.Source => Source;

        public bool Sync()
        {
            throw new NotImplementedException();
        }
    }
}
