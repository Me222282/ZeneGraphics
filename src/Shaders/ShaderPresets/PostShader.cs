using System;
using Zene.Structs;

namespace Zene.Graphics
{
    public class PostShader : BaseShaderProgram
    {
        public PostShader()
        {
            Create(ShaderPresets.PostVertex, ShaderPresets.PostFragment, -1,
                "screenWidth", "screenHeight", "uTextureSlot",
                "crushBit", "bitCrush", "greyScale", "invertedColour",
                "useKernel", "kernel", "kernelOffset");

            SetUniform(Uniforms[2], 0);
        }

        private Vector2I _size;
        public Vector2I Size
        {
            get => _size;
            set
            {
                _size = value;
                // Currently unused
                //SetUniform(Uniforms[0], value.X);
                //SetUniform(Uniforms[1], value.X);
            }
        }

        public ITexture Texture { get; set; }

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

        private floatv[] _kernel;
        public floatv[] Kernel
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

        private floatv _kernelOffset;
        public floatv KernelOffset
        {
            get => _kernelOffset;
            set
            {
                _kernelOffset = value;

                SetUniform(Uniforms[9], value);
            }
        }

        public static floatv[] BlurKernel { get; } = new floatv[]
        {
            (floatv)1 / 16, (floatv)2 / 16, (floatv)1 / 16,
            (floatv)2 / 16, (floatv)4 / 16, (floatv)2 / 16,
            (floatv)1 / 16, (floatv)2 / 16, (floatv)1 / 16
        };
        public static floatv[] SharpenKernel { get; } = new floatv[]
        {
            -1, -1, -1,
            -1, 9, -1,
            -1, -1, -1
        };

        public override void PrepareDraw() => Texture?.Bind();
    }
}