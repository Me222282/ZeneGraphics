using System;

namespace Zene.Graphics
{
    public class BufferException : Exception
    {
        public BufferException(IBuffer buffer, string message)
            : base($"Buffer object named {buffer.Id} threw exception: {message}")
        {

        }
    }
}
