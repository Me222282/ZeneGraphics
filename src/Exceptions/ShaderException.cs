using System;

namespace Zene.Graphics
{
    public class ShaderException : Exception
    {
        public ShaderException(IShader shader, string message)
            : base($"Shader object named {shader.Id} threw exception: {message}")
        {

        }
    }
}
