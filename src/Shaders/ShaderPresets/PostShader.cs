using System;
using Zene.Structs;

namespace Zene.Graphics
{
    public class PostShader : BaseShaderProgram
    {
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
                SetUniform(Uniforms[0], value.X);
                SetUniform(Uniforms[1], value.X);
            }
        }

        private int _ts;
        public int TextureSlot
        {
            get => _ts;
            set
            {
                _ts = value;
                SetUniform(Uniforms[2], value);
            }
        }

        private bool _pixelate;
        public bool Pixelated
        {
            get => _pixelate;
            set
            {
                _pixelate = value;

                SetUniform(Uniforms[3], value);
            }
        }

        private Vector2 _sixelateSize;
        public Vector2 PixelateSize
        {
            get => _sixelateSize;
            set
            {
                _sixelateSize = value;
                SetUniform(Uniforms[4], value);
            }
        }

        private bool _greyScale;
        public bool GreyScale
        {
            get => _greyScale;
            set
            {
                _greyScale = value;

                SetUniform(Uniforms[5], value);
            }
        }

        private bool _invertedColour;
        public bool InvertedColour
        {
            get => _invertedColour;
            set
            {
                _invertedColour = value;

                SetUniform(Uniforms[6], value);
            }
        }

        private bool _useKernel;
        public bool UseKernel
        {
            get => _useKernel;
            set
            {
                _useKernel = value;

                SetUniform(Uniforms[7], value);
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

                SetUniform(Uniforms[8], value);
            }
        }

        private double _kernelOffset;
        public double KernelOffset
        {
            get => _kernelOffset;
            set
            {
                _kernelOffset = value;

                SetUniform(Uniforms[9], value);
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