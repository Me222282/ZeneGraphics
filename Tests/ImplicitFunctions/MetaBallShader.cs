using System;
using System.IO;
using Zene.Graphics;
using Zene.Graphics.Base;
using Zene.Graphics.Shaders;
using Zene.Structs;

namespace ImplicitFunctions
{
    public class MetaBallShader : IShaderProgram
    {
        public MetaBallShader(int nBalls)
        {
            string fSource = File.ReadAllText("resources/MetaBallFrag.shader");

            Id = CustomShader.CreateShader(
                File.ReadAllText("resources/MetaBallVert.shader"),
                fSource.Replace("@SIZE", nBalls.ToString()));

            _uniformScale = GL.GetUniformLocation(Id, "scale");
            _uniformOffset = GL.GetUniformLocation(Id, "offset");

            _uniformBall = GL.GetUniformLocation(Id, "balls");
            _balls = new Vector3<float>[nBalls];
        }

        public uint Id { get; }

        private readonly int _uniformBall;
        private readonly Vector3<float>[] _balls;
        public Vector3 this[int index]
        {
            get
            {
                return new Vector3(_balls[index].X, _balls[index].Y, _balls[index].Z);
            }
            set
            {
                _balls[index] = new Vector3<float>((float)value.X, (float)value.Y, (float)value.Z);
            }
        }
        public unsafe void ParseBalls()
        {
            fixed (Vector3<float>* ptr = _balls)
            {
                GL.ProgramUniform3fv(Id, _uniformBall, _balls.Length, (float*)ptr);
            }
        }

        private readonly int _uniformScale;
        public Vector2 Scale
        {
            set
            {
                GL.ProgramUniform2f(Id, _uniformScale, (float)value.X, (float)value.Y);
            }
        }
        private readonly int _uniformOffset;
        public Vector2 Offset
        {
            set
            {
                GL.ProgramUniform2f(Id, _uniformOffset, (float)value.X, (float)value.Y);
            }
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
