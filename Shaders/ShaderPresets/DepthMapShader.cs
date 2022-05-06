using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics.Shaders
{
    public unsafe class DepthMapShader : IMvpShader
    {
        public DepthMapShader()
        {
            _disposed = false;
            _bound = false;

            Program = CustomShader.CreateShader(ShaderPresets.DepthMapVertex, ShaderPresets.DepthMapFragment);

            FindUniforms();

            SetModelMatrix(Matrix4.Identity);
            SetViewMatrix(Matrix4.Identity);
            SetProjectionMatrix(Matrix4.Identity);
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

        private int _uniformProjMatrix;
        private int _uniformViewMatrix;
        private int _uniformModelMatrix;

        public void SetProjectionMatrix(Matrix4 matrix)
        {
            GL.ProgramUniformMatrix4fv(Program, _uniformProjMatrix, false, matrix.GetGLData());
        }

        public void SetViewMatrix(Matrix4 matrix)
        {
            GL.ProgramUniformMatrix4fv(Program, _uniformViewMatrix, false, matrix.GetGLData());
        }

        public void SetModelMatrix(Matrix4 matrix)
        {
            GL.ProgramUniformMatrix4fv(Program, _uniformModelMatrix, false, matrix.GetGLData());
        }

        private int _uniformDepthOffset;

        public void SetDepthOffset(double value)
        {
            GL.ProgramUniform1f(Program, _uniformDepthOffset, (float)value);
        }

        private void FindUniforms()
        {
            _uniformProjMatrix = GL.GetUniformLocation(Program, "projection");
            _uniformViewMatrix = GL.GetUniformLocation(Program, "view");
            _uniformModelMatrix = GL.GetUniformLocation(Program, "model");

            _uniformDepthOffset = GL.GetUniformLocation(Program, "depthOffset");
        }

        public void Bind()
        {
            GL.UseProgram(Program);

            _bound = true;
        }

        public void Unbind()
        {
            GL.UseProgram(0);

            _bound = false;
        }
    }
}
