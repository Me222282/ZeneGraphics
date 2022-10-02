using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public class PostShader : IShaderProgram
    {
        public enum Location : uint
        {
            Positions = 0,
            TextureCoords = 1
        }

        public PostShader()
        {
            
            Program = CustomShader.CreateShader(ShaderPresets.PostVertex, ShaderPresets.PostFragment);

            FindUniforms();
        }

        public uint Program { get; private set; }
        uint IIdentifiable.Id => Program;

        private int _uniformWidth;
        private int _uniformHeight;

        private Vector2I _size;
        public Vector2I Size
        {
            get => _size;
            set
            {
                _size = value;
                GL.ProgramUniform1i(Program, _uniformWidth, value.X);
                GL.ProgramUniform1i(Program, _uniformHeight, value.Y);
            }
        }

        private int _uniformTexSlot;

        public void SetTextureSlot(int slot)
        {
            GL.ProgramUniform1i(Program, _uniformTexSlot, slot);
        }

        private int _uniformBitCrush;

        public void Pixelate(bool value)
        {
            int i = 1;
            if (!value) { i = 0; }

            GL.ProgramUniform1i(Program, _uniformBitCrush, i);
        }

        private int _uniformBitCrushValue;

        public void PixelSize(double width, double height)
        {
            GL.ProgramUniform2f(Program, _uniformBitCrushValue, (float)width, (float)height);
        }

        private int _uniformGreyScale;

        public void GreyScale(bool value)
        {
            int i = 1;
            if (!value) { i = 0; }

            GL.ProgramUniform1i(Program, _uniformGreyScale, i);
        }

        private int _uniformInvertedColour;

        public void InvertedColour(bool value)
        {
            int i = 1;
            if (!value) { i = 0; }

            GL.ProgramUniform1i(Program, _uniformInvertedColour, i);
        }

        private int _uniformUseKernel;

        public void UseKernel(bool value)
        {
            int i = 1;
            if (!value) { i = 0; }

            GL.ProgramUniform1i(Program, _uniformUseKernel, i);
        }

        private int _uniformKernel;

        public unsafe void SetKernel(double[] kernel)
        {
            float[] value = new float[9];

            for (int i = 0; i < 9; i++) { value[i] = (float)kernel[i]; }

            fixed (float* valuePtr = &value[0])
            {
                GL.ProgramUniform1fv(Program, _uniformKernel, 9, valuePtr);
            }
        }

        private int _uniformKernelOffset;

        public void SetKernelOffset(double offset)
        {
            GL.ProgramUniform1f(Program, _uniformKernelOffset, (float)offset);
        }

        private void FindUniforms()
        {
            _uniformWidth = GL.GetUniformLocation(Program, "screenWidth");
            _uniformHeight = GL.GetUniformLocation(Program, "screenHeight");

            _uniformTexSlot = GL.GetUniformLocation(Program, "uTextureSlot");

            _uniformBitCrush = GL.GetUniformLocation(Program, "crushBit");
            _uniformBitCrushValue = GL.GetUniformLocation(Program, "bitCrush");

            _uniformGreyScale = GL.GetUniformLocation(Program, "greyScale");
            _uniformInvertedColour = GL.GetUniformLocation(Program, "invertedColour");

            _uniformUseKernel = GL.GetUniformLocation(Program, "useKernel");
            _uniformKernel = GL.GetUniformLocation(Program, "kernel");
            _uniformKernelOffset = GL.GetUniformLocation(Program, "kernelOffset");
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            GL.DeleteProgram(Program);

            _disposed = true;

            GC.SuppressFinalize(this);
        }

        public void Bind()
        {
            GL.UseProgram(this);
        }

        public void Unbind()
        {
            GL.UseProgram(null);
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