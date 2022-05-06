using System;

namespace Zene.Graphics
{
    public class GLObjectException : Exception
    {
        public GLObjectException(IIdentifiable obj, string message)
            : base($"OpenGL object named {obj.Id} threw exception: {message}")
        {
            
        }
    }
}
