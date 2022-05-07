using System;
using Zene.Graphics.Base;

namespace Zene.Graphics
{
    public class OpenGLSupportException : Exception
    {
        public OpenGLSupportException(string message)
            : base($"{message} in OpenGL version {GL.Version}")
        {

        }
    }
}
