using System.Collections.Generic;
using System;

namespace Zene.Graphics
{
    public class IndexBuffer<T> : Buffer<T> where T : unmanaged
    {
        public IndexBuffer(BufferUsage usage)
            : base(BufferTarget.ElementArray, usage)
        {
            
        }
    }
}
