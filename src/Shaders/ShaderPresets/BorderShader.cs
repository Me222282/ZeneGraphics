using System;
using Zene.Structs;

namespace Zene.Graphics
{
    public sealed class BorderShader : BaseShaderProgram, IBasicShader
    {
        public BorderShader()
        {
            Create(ShaderPresets.BorderVert, ShaderPresets.BorderFrag,
                  "colourType", "uColour", "uTextureSlot", "matrix", "size", "radius",
                  "aspect", "outerRadius", "uBorderColour", "innerDMinusR", "rValue",
                  "halfBW", "borderCrossOver");

            _m2m3 = new MultiplyMatrix4(null, null);
            _m1Mm2m3 = new MultiplyMatrix4(null, _m2m3);
            _bsmMm1Mm2m3 = new MultiplyMatrix4(null, _m1Mm2m3);

            SetUniform(Uniforms[3], Matrix.Identity);
            SetUniform(Uniforms[2], 0);
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

        private double _radiusPercent;
        /// <summary>
        /// The percentage of the border that is curved; values between 0.0 - 0.5.
        /// </summary>
        public double Radius
        {
            get => _radiusPercent;
            set
            {
                _radiusPercent = Math.Clamp(value, 0d, 0.5);

                SetRadius();
                SetIDMR();
            }
        }

        private Vector2 _size;
        private Vector2 _aspect;
        /// <summary>
        /// The target size of the box being drawn.
        /// </summary>
        public Vector2 Size
        {
            get => _size;
            set
            {
                _size = value;
                _aspect = value / Math.Min(value.X, value.Y);

                SetUniform(Uniforms[6], _aspect);

                BorderWidth = _bWidth;
            }
        }

        private double _bWidth;
        private double _widthPercent;
        private double _halfWidth;
        /// <summary>
        /// The width, in pixels, of the border.
        /// </summary>
        public double BorderWidth
        {
            get => _bWidth;
            set
            {
                _bWidth = value;

                if (_size.X <= 0 || _size.Y <= 0) { return; }

                _widthPercent = value / Math.Min(_size.X, _size.Y);
                _halfWidth = _widthPercent * 0.5;

                SetUniform(Uniforms[11], _halfWidth);
                SetUniform(Uniforms[12], _aspect - _halfWidth);

                SetScale();
                SetRadius();
                SetIDMR();
            }
        }

        private void SetScale()
        {
            Vector2 scale = (_size + (2 * _bWidth)) / _size;
            scale = 1d + ((scale - 1d) * 0.5);

            SetUniform(Uniforms[4], scale);
            _bsmMm1Mm2m3.Left = Matrix4.CreateScale(scale);
        }
        private void SetRadius()
        {
            double radius = _radiusPercent - _halfWidth;
            double outerRadius = Math.Max(_radiusPercent + _halfWidth, _widthPercent);
            SetUniform(Uniforms[5], radius * radius);
            SetUniform(Uniforms[7], outerRadius * outerRadius);
        }
        private void SetIDMR()
        {
            double innerOffset = Math.Max(_radiusPercent, _halfWidth);
            SetUniform(Uniforms[10], innerOffset);
            SetUniform(Uniforms[9], _aspect - innerOffset);
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

        private ColourF _borderColour = ColourF.Zero;
        /// <summary>
        /// THe colour of the border of the box.
        /// </summary>
        public ColourF BorderColour
        {
            get => _borderColour;
            set
            {
                _borderColour = value;

                SetUniform(Uniforms[8], (Vector4)value);
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

        private readonly MultiplyMatrix4 _bsmMm1Mm2m3;
        private readonly MultiplyMatrix4 _m1Mm2m3;
        private readonly MultiplyMatrix4 _m2m3;
        public override void PrepareDraw()
        {
            SetUniform(Uniforms[3], _bsmMm1Mm2m3);
            Texture?.Bind(0);
        }

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            State.CurrentContext.RemoveTrack(this);
        }
        /// <summary>
        /// Gets the instance of the <see cref="BorderShader"/> for this <see cref="GraphicsContext"/>.
        /// </summary>
        /// <returns></returns>
        public static BorderShader GetInstance() => GetInstance<BorderShader>();
    }
}
