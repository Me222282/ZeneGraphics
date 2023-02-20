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

            _m2m3 = Matrix.Identity * Matrix.Identity;
            _m1Mm2m3 = Matrix.Identity * _m2m3;

            SetUniform(Uniforms[3], Matrix.Identity);
        }

        private ColourSource _source = 0;
        public ColourSource ColourSource
        {
            get => _source;
            set
            {
                _source = value;

                SetUniform(Uniforms[0], (int)value);
            }
        }

        private ColourF _colour = ColourF.Zero;
        public ColourF Colour
        {
            get => _colour;
            set
            {
                _colour = value;

                SetUniform(Uniforms[1], (Vector4)value);
            }
        }

        private int _texSlot = 0;
        public int TextureSlot
        {
            get => _texSlot;
            set
            {
                _texSlot = value;

                SetUniform(Uniforms[2], value);
            }
        }

        public IMatrix Matrix1
        {
            get => _m1Mm2m3.Left;
            set => _m1Mm2m3.Left = value;
        }
        public IMatrix Matrix2
        {
            get => _m2m3.Left;
            set => _m2m3.Left = value;
        }
        public IMatrix Matrix3
        {
            get => _m2m3.Right;
            set => _m2m3.Right = value;
        }

        private readonly MultiplyMatrix _m1Mm2m3;
        private readonly MultiplyMatrix _m2m3;
        public override void PrepareDraw() => SetUniform(Uniforms[3], _m1Mm2m3);

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            State.CurrentContext.RemoveTrack(this);
        }
        /// <summary>
        /// Gets the instance of the <see cref="BasicShader"/> for this <see cref="GraphicsContext"/>.
        /// </summary>
        /// <returns></returns>
        public static BasicShader GetInstance() => GetInstance<BasicShader>();
    }
}
