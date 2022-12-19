using Zene.Structs;

namespace Zene.Graphics
{
    public enum ColourSource
    {
        None = 0,
        UniformColour = 1,
        AttributeColour = 2,
        Texture = 3
    }

    public sealed class BasicShader : BaseShaderProgram, IBasicShader
    {
        public enum Location : uint
        {
            Positions = 0,
            ColourAttribute = 1,
            TextureCoords = 2
        }

        public BasicShader()
        {
            Create(ShaderPresets.BasicVertex, ShaderPresets.BasicFragment,
                  "colourType", "uColour", "uTextureSlot", "matrix");

            SetUniformF(Uniforms[3], Matrix4.Identity);
        }

        private ColourSource _source = 0;
        public ColourSource ColourSource
        {
            get => _source;
            set
            {
                _source = value;

                SetUniformI(Uniforms[0], (int)value);
            }
        }

        private ColourF _colour = ColourF.Zero;
        public ColourF Colour
        {
            get => _colour;
            set
            {
                _colour = value;

                SetUniformF(Uniforms[1], (Vector4)value);
            }
        }

        private int _texSlot = 0;
        public int TextureSlot
        {
            get => _texSlot;
            set
            {
                _texSlot = value;

                SetUniformI(Uniforms[2], value);
            }
        }

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

        public void SetMatrices(Matrix4 a, Matrix4 b, Matrix4 c)
        {
            _m1 = a;
            _m2 = b;
            _m3 = c;
            SetMatrices();
        }

        private void SetMatrices()
        {
            SetUniformF(Uniforms[3], _m1 * _m2 * _m3);
        }
    }
}
