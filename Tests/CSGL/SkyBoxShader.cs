using System;
using System.IO;
using Zene.Graphics.OpenGL;
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
            _uniformProjMatrix = GL.GetUniformLocation(Program, "projection");
            _uniformViewMatrix = GL.GetUniformLocation(Program, "view");
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
        public void UnBind()
        {
            GL.UseProgram(0);
        }

        private readonly int _uniformSampler;
        private readonly int _uniformProjMatrix;
        private readonly int _uniformViewMatrix;

        public int TextureSlot
        {
            set
            {
                GL.ProgramUniform1i(Program, _uniformSampler, value);
            }
        }
        public Matrix4 ProjectionMat
        {
            set
            {
                GL.ProgramUniformMatrix4fv(Program, _uniformProjMatrix, false, value.GetGLData());
            }
        }
        public Matrix4 ViewMat
        {
            set
            {
                GL.ProgramUniformMatrix4fv(Program, _uniformViewMatrix, false, value.GetGLData());
            }
        }
    }
}
