using System.Collections.Generic;
using System;

namespace Zene.Graphics
{
    public class IndexBuffer<T> : Buffer<T> where T : unmanaged
    {
        public IndexBuffer(IEnumerable<T> indices, BufferUsage usage)
            : base(indices, BufferTarget.ElementArray, usage, true)
        {
            
        }

        protected override bool ValideType(Type type)
        {
            return type == typeof(byte)
                || type == typeof(ushort)
                || type == typeof(uint);
        }
    }
}
