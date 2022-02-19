using System;
using System.IO;
using Zene.Graphics;
using Zene.Graphics.Base;
using Zene.Graphics.Shaders;
using Zene.Structs;

namespace ImplicitFunctions
{
    public class CircleShader : IShaderProgram
    {
        public CircleShader()
        {
            Id = CustomShader.CreateShader(
                File.ReadAllText("resources/CircleVert.shader"),
                File.ReadAllText("resources/CircleFrag.shader"));

            _uniformMatrix = GL.GetUniformLocation(Id, "matrix");
        }

        public uint Id { get; }

        private readonly int _uniformMatrix;
        private Matrix4 _m1 = Matrix4.Identity;
        public Matrix4 Model
        {
            get
            {
                return _m1;
            }
            set
            {
                _m1 = value;
                SetMatrix();
            }
        }
        private Matrix4 _m2 = Matrix4.Identity;
        public Matrix4 View
        {
            get
            {
                return _m2;
            }
            set
            {
                _m2 = value;
                SetMatrix();
            }
        }
        private Matrix4 _m3 = Matrix4.Identity;
        public Matrix4 Projection
        {
            get
            {
                return _m3;
            }
            set
            {
                _m3 = value;
                SetMatrix();
            }
        }
        private void SetMatrix()
        {
            GL.ProgramUniformMatrix4fv(Id, _uniformMatrix, false, (_m1 * _m2 * _m3).GetGLData());
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            GL.DeleteProgram(Id);

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        public void Bind()
        {
            GL.UseProgram(Id);
        }
        public void Unbind()
        {
            GL.UseProgram(0);
        }
    }
}
