using System;

namespace Zene.Graphics
{
    public interface IGLObject : IIdentifiable, IDisposable, IBindable
    {
        public IProperties Properties { get; }
    }
}