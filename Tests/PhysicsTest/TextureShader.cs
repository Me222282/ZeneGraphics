using System;
using System.IO;
using Zene.Graphics;
using Zene.Graphics.Base;
using Zene.Graphics.Shaders;
using Zene.Structs;

namespace PhysicsTest
{
    public class TextureShader : IShaderProgram
    {
        public TextureShader()
        {
            Id = CustomShader.CreateShader(
                File.ReadAllText("Resources/Vertex.shader"),
                File.ReadAllText("Resources/Fragment.shader"));

            _uniformTexSlot = GL.GetUniformLocation(Id, "uTextureSlot");
            _uniformMatrix = GL.GetUniformLocation(Id, "uProj_View");

            // Set amtrix to identiy
            GL.ProgramUniformMatrix4fv(Id, _uniformMatrix, false, Matrix4.Zero.GetGLData());
        }

        public uint Id { get; }

        private readonly int _uniformTexSlot;
        public int TextureSlot
        {
            set
            {
                GL.ProgramUniform1i(Id, _uniformTexSlot, value);
            }
        }

        private readonly int _uniformMatrix;
        private Matrix4 _mView = Matrix4.Identity;
        public Matrix4 View
        {
            get
            {
                return _mView;
            }
            set
            {
                _mView = value;
                SetMatrices();
            }
        }
        private Matrix4 _mProj = Matrix4.Identity;
        public Matrix4 Projection
        {
            get
            {
                return _mProj;
            }
            set
            {
                _mProj = value;
                SetMatrices();
            }
        }
        private void SetMatrices()
        {
            Matrix4 matrix = _mView * _mProj;
            GL.ProgramUniformMatrix4fv(Id, _uniformMatrix, false, matrix.GetGLData());
        }

        public void Bind()
        {
            GL.UseProgram(Id);
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            GL.DeleteProgram(Id);

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        public void Unbind()
        {
            GL.UseProgram(0);
        }
    }
}
