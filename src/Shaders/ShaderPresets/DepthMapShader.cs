using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public unsafe class DepthMapShader : IMatrixShader
    {
        public DepthMapShader()
        {
            _disposed = false;
            _bound = false;

            Program = CustomShader.CreateShader(ShaderPresets.DepthMapVertex, ShaderPresets.DepthMapFragment);

            FindUniforms();

            Matrix1 = Matrix4.Identity;
            Matrix2 = Matrix4.Identity;
            Matrix3 = Matrix4.Identity;
        }

        public uint Program { get; }
        uint IIdentifiable.Id => Program;

        private bool _bound;
        public bool Bound
        {
            get
            {
                return _bound;
            }
            set
            {
                if (value && (!_bound))
                {
                    Bind();
                }
                else if ((!value) && _bound)
                {
                    Unbind();
                }
            }
        }

        private bool _disposed;
        public void Dispose()
        {
            if (!_disposed)
            {
                GL.DeleteProgram(Program);
                _disposed = true;

                GC.SuppressFinalize(this);
            }
        }

        private int _uniformMatrix;

        private Matrix4 _m1 = Matrix4.Identity;
        public Matrix4 Matrix1
        {
            get => _m1;
            set
            {
                _m1 = value;
                SetMatrices();
            }
        }
        private Matrix4 _m2 = Matrix4.Identity;
        public Matrix4 Matrix2
        {
            get => _m2;
            set
            {
                _m2 = value;
                SetMatrices();
            }
        }
        private Matrix4 _m3 = Matrix4.Identity;
        public Matrix4 Matrix3
        {
            get => _m3;
            set
            {
                _m3 = value;
                SetMatrices();
            }
        }

        private void SetMatrices()
        {
            GL.ProgramUniformMatrix4fv(Program, _uniformMatrix, false, (_m1 * _m2 * _m3).GetGLData());
        }

        private int _uniformDepthOffset;

        public void SetDepthOffset(double value)
        {
            GL.ProgramUniform1f(Program, _uniformDepthOffset, (float)value);
        }

        private void FindUniforms()
        {
            _uniformMatrix = GL.GetUniformLocation(Program, "matrix");

            _uniformDepthOffset = GL.GetUniformLocation(Program, "depthOffset");
        }

        public void Bind()
        {
            GL.UseProgram(this);

            _bound = true;
        }

        public void Unbind()
        {
            GL.UseProgram(null);

            _bound = false;
        }
    }
}
