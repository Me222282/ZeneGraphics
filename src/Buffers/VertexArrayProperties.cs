using System;
using Zene.Graphics.Base;

namespace Zene.Graphics
{
    public class VertexArrayProperties : IProperties
    {
        public VertexArrayProperties(IVertexArray source)
        {
            Source = source;
        }

        internal readonly IBuffer[] _buffers = new IBuffer[State.MaxVertexAttributes];
        internal readonly bool[] _attributes = new bool[State.MaxVertexAttributes];

        public bool this[uint index]
        {
            get
            {
                if (index >= _buffers.Length)
                {
                    throw new IndexOutOfRangeException();
                }

                return _attributes[index];
            }
            set
            {
                if (index >= _buffers.Length)
                {
                    throw new IndexOutOfRangeException();
                }

                Source.Bind();

                if (value)
                {
                    GL.EnableVertexAttribArray(index);
                    return;
                }

                GL.DisableVertexAttribArray(index);
            }
        }

        public IVertexArray Source { get; }
        IGLObject IProperties.Source => Source;

        public IBuffer GetBuffer(int index)
        {
            if (index < 0 || index >= _buffers.Length)
            {
                throw new IndexOutOfRangeException();
            }

            return _buffers[index];
        }

        public bool Sync()
        {
            throw new NotImplementedException();
        }
    }
}
