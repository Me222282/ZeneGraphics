using System;

namespace Zene.Graphics
{
    public class FrameBufferException : Exception
    {
        public FrameBufferException(IFramebuffer framebuffer, string message)
            : base($"FrameBuffer object named {framebuffer.Id} threw exception: {message}")
        {
            
        }
    }
}
