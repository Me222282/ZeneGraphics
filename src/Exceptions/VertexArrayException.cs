using System;

namespace Zene.Graphics
{
    public class VertexArrayException : Exception
    {
        public VertexArrayException(IVertexArray vertexArray, string message)
            : base($"Vertex array object named {vertexArray.Id} threw exception: {message}")
        {

        }
    }
}
