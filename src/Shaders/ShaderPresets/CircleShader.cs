using Zene.Structs;

namespace Zene.Graphics
{
    public sealed class CircleShader : BaseShaderProgram, IBasicShader
    {
        public CircleShader()
        {
            Create(ShaderPresets.CircleVert, ShaderPresets.CircleFrag,
                  "colourType", "uInnerColour", "uTextureSlot", "matrix", "size", "radius", "minRadius", "uColour");

            SetUniformF(Uniforms[3], Matrix4.Identity);
            Size = 1d;
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

        private double _size;
        public double Size
        {
            get => _size;
            set
            {
                _size = value;

                SetUniformF(Uniforms[4], value);
                SetUniformF(Uniforms[5], value * value * 0.25);
            }
        }

        private double _lWidth;
        public double LineWidth
        {
            get => _lWidth;
            set
            {
                _lWidth = value;

                double len = (_size * 0.5) - value;

                SetUniformF(Uniforms[6], len * len);
            }
        }

        private ColourF _innerColour = ColourF.Zero;
        public ColourF InnerColour
        {
            get => _innerColour;
            set
            {
                _innerColour = value;

                SetUniformF(Uniforms[1], (Vector4)value);
            }
        }

        private ColourF _colour = ColourF.Zero;
        public ColourF Colour
        {
            get => _colour;
            set
            {
                _colour = value;

                SetUniformF(Uniforms[7], (Vector4)value);
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

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            State.CurrentContext.RemoveTrack(this);
        }
        /// <summary>
        /// Gets the instance of the <see cref="CircleShader"/> for this <see cref="GraphicsContext"/>.
        /// </summary>
        /// <returns></returns>
        public static CircleShader GetInstance() => GetInstance<CircleShader>();
    }
}
