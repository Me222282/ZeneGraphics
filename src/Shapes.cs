using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zene.Graphics
{
    public static class Shapes
    {
        static Shapes()
        {
            Square = new DrawObject<float, byte>(stackalloc float[]
            {
                0.5f, 0.5f, 1f, 1f,
                0.5f, -0.5f, 1f, 0f,
                -0.5f, -0.5f, 0f, 0f,
                -0.5f, 0.5f, 0f, 1f
            }, stackalloc byte[] { 0, 1, 2, 2, 3, 0 }, 4, 0, AttributeSize.D2, BufferUsage.DrawFrequent);
            Square.AddAttribute(ShaderLocation.TextureCoords, 2, AttributeSize.D2);
        }

        public static DrawObject<float, byte> Square { get; }
    }
}
