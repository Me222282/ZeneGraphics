using System;
using Zene.Graphics.Shaders;
using Zene.Graphics.Base;
using System.IO;
using Zene.Structs;
using Zene.Graphics;

namespace PhysicsTest
{
    public class BackgroundShader : IShaderProgram
    {
        public BackgroundShader()
        {
            Id = CustomShader.CreateShader(
                File.ReadAllText("Resources/Vertex2.shader"),
                File.ReadAllText("Resources/Fragment2.shader"));

            _uniformTexSlot = GL.GetUniformLocation(Id, "uTextureSlot");
            _uniformMatrix = GL.GetUniformLocation(Id, "matrix");

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
        private Matrix4 _m1 = Matrix4.Identity;
        public Matrix4 Matrix1
        {
            set
            {
                _m1 = value;
                SetMatrices();
            }
        }
        private Matrix4 _m2 = Matrix4.Identity;
        public Matrix4 Matrix2
        {
            set
            {
                _m2 = value;
                SetMatrices();
            }
        }
        private Matrix4 _m3 = Matrix4.Identity;
        public Matrix4 Matrix3
        {
            set
            {
                _m3 = value;
                SetMatrices();
            }
        }

        private void SetMatrices()
        {
            Matrix4 matrix = _m1 * _m2 * _m3;
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

    public class Background : IDisposable
    {
        public static double TexCoordSize { get; set; } = 2.5;

        public Background()
        {
            _shader = new BackgroundShader();

            _drawable = new DrawObject<Vector2, byte>(
                    new Vector2[]
                    {
                        new Vector2(1.0, 1.0), new Vector2(TexCoordSize, TexCoordSize),
                        new Vector2(1.0, -1.0), new Vector2(TexCoordSize, 0),
                        new Vector2(-1.0, -1.0), new Vector2(0, 0),
                        new Vector2(-1.0, 1.0), new Vector2(0, TexCoordSize)
                    },
                    new byte[]
                    {
                        0, 1, 2,
                        2, 3, 0
                    },
                    2, 0, AttributeSize.D2, BufferUsage.DrawFrequent);
            _drawable.AddAttribute(1, 1, AttributeSize.D2); // Texture Coordinates

            _texture = Texture2D.Create("Resources/stars.png", WrapStyle.Repeated, TextureSampling.Nearest, false);
        }

        private readonly BackgroundShader _shader;
        private readonly DrawObject<Vector2, byte> _drawable;
        private readonly Texture2D _texture;

        public void SetTranslation(Matrix4 viewMatrix)
        {
            _shader.Matrix2 = viewMatrix;
        }

        public void Draw()
        {
            _shader.Bind();

            _shader.TextureSlot = 0;
            _texture.Bind(0);
            _drawable.Draw();

            _shader.Unbind();
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            _shader.Dispose();
            _drawable.Dispose();
            _texture.Dispose();

            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
