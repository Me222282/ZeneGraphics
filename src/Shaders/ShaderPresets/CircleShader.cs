using Zene.Structs;

namespace Zene.Graphics
{
    public sealed class CircleShader : BaseShaderProgram, IBasicShader
    {
        public CircleShader()
        {
            Create(ShaderPresets.CircleVert, ShaderPresets.CircleFrag, 3,
                  "colourType", "uBorderColour", "uTextureSlot", "matrix",
                  "size", "radius", "minRadius", "uColour", "c_off");

            SetUniform(Uniforms[3], Matrix4.Identity);
            SetUniform(Uniforms[2], 0);
            Size = 1;
            Offset = 0.5f;
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

        private Vector2 _size;
        private floatv _radius;
        public floatv Size
        {
            get => _size.X;
            set
            {
                _size = value;
                floatv r = value * 0.5f;
                _radius = r;
                
                SetUniform(Uniforms[4], _size);
                SetUniform(Uniforms[5], r * r);
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
        
        private floatv _lWidth;
        public floatv LineWidth
        {
            get => _lWidth;
            set
            {
                _lWidth = value;

                floatv len = _radius - value;

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

        public void SetSR(Vector2 size, floatv radius)
        {
            _size = size;
            _radius = radius;
            SetUniform(Uniforms[4], size);
            SetUniform(Uniforms[5], radius * radius);
        }
        
        public override void PrepareDraw()
        {
            base.PrepareDraw();
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
