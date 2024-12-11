using Zene.Structs;

namespace Zene.Graphics
{
    public sealed class CircleShader : BaseShaderProgram, IBasicShader
    {
        public CircleShader()
        {
            Create(ShaderPresets.CircleVert, ShaderPresets.CircleFrag,
                  "colourType", "uBorderColour", "uTextureSlot", "matrix",
                  "size", "radius", "minRadius", "uColour", "c_off");

            _m2m3 = new MultiplyMatrix4(null, null);
            _m1Mm2m3 = new MultiplyMatrix4(null, _m2m3);

            SetUniform(Uniforms[3], Matrix.Identity);
            SetUniform(Uniforms[2], 0);
            Size = 1d;
            Offset = 0.5;
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

        private double _size;
        public double Size
        {
            get => _size;
            set
            {
                _size = value;

                SetUniform(Uniforms[4], value);
                SetUniform(Uniforms[5], value * value * 0.25);
            }
        }
        
        private Vector2 _offset;
        public Vector2 Offset
        {
            get => _offset;
            set
            {
                _offset = value;
                SetUniform(Uniforms[8], value);
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

                SetUniform(Uniforms[6], len * len);
            }
        }

        private ColourF _borderColour = ColourF.Zero;
        public ColourF BorderColour
        {
            get => _borderColour;
            set
            {
                _borderColour = value;

                SetUniform(Uniforms[1], (Vector4)value);
            }
        }

        private ColourF _colour = ColourF.Zero;
        public ColourF Colour
        {
            get => _colour;
            set
            {
                _colour = value;

                SetUniform(Uniforms[7], (Vector4)value);
            }
        }

        public ITexture Texture { get; set; }

        public override IMatrix Matrix1
        {
            get => _m1Mm2m3.Left;
            set => _m1Mm2m3.Left = value;
        }
        public override IMatrix Matrix2
        {
            get => _m2m3.Left;
            set => _m2m3.Left = value;
        }
        public override IMatrix Matrix3
        {
            get => _m2m3.Right;
            set => _m2m3.Right = value;
        }
        
        public void SetSR(double size, double radius)
        {
            SetUniform(Uniforms[4], size);
            SetUniform(Uniforms[5], radius * radius);
        }
        
        private readonly MultiplyMatrix4 _m1Mm2m3;
        private readonly MultiplyMatrix4 _m2m3;
        public override void PrepareDraw()
        {
            SetUniform(Uniforms[3], _m1Mm2m3);
            Texture?.Bind(0);
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
