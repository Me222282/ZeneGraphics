using System;
using Zene.Structs;

namespace Zene.Graphics
{
    public class PostShader : BaseShaderProgram
    {
        public enum Location : uint
        {
            Positions = 0,
            TextureCoords = 1
        }

        public PostShader()
        {
            Create(ShaderPresets.PostVertex, ShaderPresets.PostFragment,
                "screenWidth", "screenHeight", "uTextureSlot",
                "crushBit", "bitCrush", "greyScale", "invertedColour",
                "useKernel", "kernel", "kernelOffset");
        }

        private Vector2I _size;
        public Vector2I Size
        {
            get => _size;
            set
            {
                _size = value;
                SetUniformI(Uniforms[0], value.X);
                SetUniformI(Uniforms[1], value.X);
            }
        }

        private int _ts;
        public int TextureSlot
        {
            get => _ts;
            set
            {
                _ts = value;
                SetUniformI(Uniforms[2], value);
            }
        }

        private bool _pixelate;
        public bool Pixelated
        {
            get => _pixelate;
            set
            {
                _pixelate = value;

                SetUniformI(Uniforms[3], value ? 1 : 0);
            }
        }

        private Vector2 _sixelateSize;
        public Vector2 PixelateSize
        {
            get => _sixelateSize;
            set
            {
                _sixelateSize = value;
                SetUniformF(Uniforms[4], value);
            }
        }

        private bool _greyScale;
        public bool GreyScale
        {
            get => _greyScale;
            set
            {
                _greyScale = value;

                SetUniformI(Uniforms[5], value ? 1 : 0);
            }
        }

        private bool _invertedColour;
        public bool InvertedColour
        {
            get => _invertedColour;
            set
            {
                _invertedColour = value;

                SetUniformI(Uniforms[6], value ? 1 : 0);
            }
        }

        private bool _useKernel;
        public bool UseKernel
        {
            get => _useKernel;
            set
            {
                _useKernel = value;

                SetUniformI(Uniforms[7], value ? 1 : 0);
            }
        }

        private double[] _kernel;
        public double[] Kernel
        {
            get => _kernel;
            set
            {
                if (value.Length != 9)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _kernel = value;

                SetUniformF(Uniforms[8], value);
            }
        }

        private double _kernelOffset;
        public double KernelOffset
        {
            get => _kernelOffset;
            set
            {
                _kernelOffset = value;

                SetUniformF(Uniforms[9], value);
            }
        }

        public static double[] BlurKernel { get; } = new double[]
        {
            1.0 / 16, 2.0 / 16, 1.0 / 16,
            2.0 / 16, 4.0 / 16, 2.0 / 16,
            1.0 / 16, 2.0 / 16, 1.0 / 16
        };

        public static double[] SharpenKernel { get; } = new double[]
        {
            -1, -1, -1,
            -1, 9, -1,
            -1, -1, -1
        };
    }
}