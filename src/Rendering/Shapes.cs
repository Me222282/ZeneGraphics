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
            Cube = new DrawObject<float, byte>(stackalloc float[]
            {
                -0.5f, 0.5f, 0.5f,
                0.5f, 0.5f, 0.5f,
                0.5f, -0.5f, 0.5f,
                -0.5f, -0.5f, 0.5f,

                -0.5f, 0.5f, -0.5f,
                0.5f, 0.5f, -0.5f,
                0.5f, -0.5f, -0.5f,
                -0.5f, -0.5f, -0.5f
            }, stackalloc byte[]
            {
                // Front
                0, 3, 2,
                2, 1, 0,

                // Back
                5, 6, 7,
                7, 4, 5,

                // Left
                4, 7, 3,
                3, 0, 4,

                // Right
                1, 2, 6,
                6, 5, 1,

                // Top
                4, 0, 1,
                1, 5, 4,

                // Bottom
                2, 3, 7,
                7, 6, 2
            }, 3, 0, AttributeSize.D3, BufferUsage.DrawFrequent);
        }

        public static DrawObject<float, byte> Square { get; }
        public static DrawObject<float, byte> Cube { get; }

        public static BasicShader BasicShader { get; } = BasicShader.GetInstance();
        public static BorderShader BorderShader { get; } = BorderShader.GetInstance();
        public static CircleShader CircleShader { get; } = CircleShader.GetInstance();

        public static SampleFont SampleFont { get; } = SampleFont.GetInstance();
    }
}
