using System;
using System.IO;
using Zene.Graphics.Base;
using Zene.Graphics;
using Zene.Structs;
using Zene.Graphics.Shaders;

namespace CSGL
{
    public class SkyBoxShader : IShaderProgram
    {
        public SkyBoxShader()
        {
            Program = CustomShader.CreateShader(
                File.ReadAllText("Resources/skyBoxVert.shader"),
                File.ReadAllText("Resources/skyBoxFrag.shader"));

            _uniformSampler = GL.GetUniformLocation(Program, "skybox");
            _uniformMatrix = GL.GetUniformLocation(Program, "matrix");
        }

        public uint Program { get; }
        uint IIdentifiable.Id => Program;

        public void Bind()
        {
            GL.UseProgram(Program);
        }
        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            GL.DeleteProgram(Program);

            _disposed = true;
            GC.SuppressFinalize(this);
        }
        public void Unbind()
        {
            GL.UseProgram(0);
        }

        private readonly int _uniformSampler;
        private readonly int _uniformMatrix;

        public int TextureSlot
        {
            set
            {
                GL.ProgramUniform1i(Program, _uniformSampler, value);
            }
        }
        private Matrix4 _m3 = Matrix4.Identity;
        public Matrix4 Projection
        {
            get => _m3;
            set
            {
                _m3 = value;

                SetMatrices();
            }
        }
        private Matrix4 _m2 = Matrix4.Identity;
        public Matrix4 View
        {
            get => _m2;
            set
            {
                _m2 = value;

                SetMatrices();
            }
        }
        private Matrix4 _m1 = Matrix4.Identity;
        public Matrix4 Model
        {
            get => _m1;
            set
            {
                _m1 = value;

                SetMatrices();
            }
        }
        private void SetMatrices()
        {
            GL.ProgramUniformMatrix4fv(Program, _uniformMatrix, false, (_m1 * _m2 * _m3).GetGLData());
        }
    }
}
