using System;

namespace Zene.Graphics
{
    public class TextureException : Exception
    {
        public TextureException(ITexture texture, string message)
            : base($"Texture object named {texture.Id} threw exception: {message}")
        {
            
        }
    }
}
