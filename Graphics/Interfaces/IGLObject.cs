using System;

namespace Zene.Graphics
{
    public interface IGLObject : IDisposable, IBindable
    {
        public bool DataCreated { get; set; }

        public void CreateData();

        public void DeleteData();
    }
}