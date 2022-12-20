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

        public Matrix4 Matrix1 { get; set; } = Matrix4.Identity;
        public Matrix4 Matrix2 { get; set; } = Matrix4.Identity;
        public Matrix4 Matrix3 { get; set; } = Matrix4.Identity;

        public override void PrepareDraw()
        {
            SetUniformF(Uniforms[3], Matrix1 * Matrix2 * Matrix3);
        }
    }
}
