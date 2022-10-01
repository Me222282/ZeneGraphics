using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics.Shaders
{
    public enum ColourSource
    {
        None = 0,
        UniformColour = 1,
        AttributeColour = 2,
        Texture = 3
    }

    public class BasicShader : IShaderProgram
    {
        public enum Location : uint
        {
            Positions = 0,
            ColourAttribute = 1,
            TextureCoords = 2
        }

        public BasicShader()
        {
            Program = CustomShader.CreateShader(ShaderPresets.BasicVertex, ShaderPresets.BasicFragment);

            FindUniforms();

            // Set matrix to "0"
            GL.ProgramUniformMatrix4fv(Program, _uniformMatrix, false, Matrix4.Identity.GetGLData());
        }

        public uint Program { get; private set; }
        uint IIdentifiable.Id => Program;

        protected bool _Bound;
        public bool Bound
        {
            get
            {
                return _Bound;
            }
            set
            {
                if (value && (!_Bound))
                {
                    Bind();
                }
                else if ((!value) && _Bound)
                {
                    Unbind();
                }
            }
        }

        private int _uniformColourType;

        public void SetColourSource(ColourSource type)
        {
            GL.ProgramUniform1i(Program, _uniformColourType, (int)type);
        }

        private int _uniformColour;

        public void SetDrawColour(float r, float g, float b, float a)
        {
            GL.ProgramUniform4f(Program, _uniformColour,
                r * ColourF.ByteToFloat,
                g * ColourF.ByteToFloat,
                b * ColourF.ByteToFloat,
                a * ColourF.ByteToFloat);
        }

        public void SetDrawColour(float r, float g, float b)
        {
            GL.ProgramUniform4f(Program, _uniformColour,
                r * ColourF.ByteToFloat,
                g * ColourF.ByteToFloat,
                b * ColourF.ByteToFloat,
                1.0f);
        }

        public void SetDrawColour(Colour colour)
        {
            ColourF c = (ColourF)colour;

            SetDrawColour(c.R, c.G, c.B, c.A);
        }

        public void SetDrawColour(ColourF colour)
        {
            SetDrawColour(colour.R, colour.G, colour.B, colour.A);
        }

        private int _uniformTexture;

        public void SetTextureSlot(int slot)
        {
            GL.ProgramUniform1i(Program, _uniformTexture, slot);
        }

        private int _uniformMatrix;

        private Matrix4 _m1 = Matrix4.Identity;
        public Matrix4 Matrix1
        {
            get
            {
                return _m1;
            }
            set
            {
                _m1 = value;
                SetMatrices();
            }
        }
        private Matrix4 _m2 = Matrix4.Identity;
        public Matrix4 Matrix2
        {
            get
            {
                return _m2;
            }
            set
            {
                _m2 = value;
                SetMatrices();
            }
        }
        private Matrix4 _m3 = Matrix4.Identity;
        public Matrix4 Matrix3
        {
            get
            {
                return _m3;
            }
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

        private void FindUniforms()
        {
            _uniformColourType = GL.GetUniformLocation(Program, "colourType");

            _uniformColour = GL.GetUniformLocation(Program, "uColour");

            _uniformTexture = GL.GetUniformLocation(Program, "uTextureSlot");

            _uniformMatrix = GL.GetUniformLocation(Program, "matrix");
        }

        public void Dispose()
        {
            GL.DeleteProgram(Program);
        }

        public void Bind()
        {
            GL.UseProgram(this);

            _Bound = true;
        }

        public void Unbind()
        {
            GL.UseProgram(null);

            _Bound = false;
        }
    }
}
