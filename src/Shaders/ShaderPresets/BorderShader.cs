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

        private double _radiusPercent;
        public double Radius
        {
            get => _radiusPercent;
            set
            {
                _radiusPercent = value;

                SetRadius();
                SetIDMR();
            }
        }

        private Vector2 _size;
        private Vector2 _aspect;
        public Vector2 Size
        {
            get => _size;
            set
            {
                _size = value;
                _aspect = value / Math.Min(value.X, value.Y);

                SetUniformF(Uniforms[6], _aspect);

                BorderWidth = _bWidth;
            }
        }

        private double _bWidth;
        private double _widthPercent;
        private double _halfWidth;
        public double BorderWidth
        {
            get => _bWidth;
            set
            {
                _bWidth = value;

                if (_size.X <= 0 || _size.Y <= 0) { return; }

                _widthPercent = value / Math.Min(_size.X, _size.Y);
                _halfWidth = _widthPercent * 0.5;

                SetUniformF(Uniforms[11], _halfWidth);
                SetUniformF(Uniforms[12], _aspect - _halfWidth);

                SetScale();
                SetRadius();
                SetIDMR();
            }
        }

        private void SetScale()
        {
            Vector2 scale = (_size + (2 * _bWidth)) / _size;
            scale = 1d + ((scale - 1d) * 0.5);

            SetUniformF(Uniforms[4], scale);
            _borderScaleMatrix = Matrix4.CreateScale(scale);

            if (_m1 == Matrix4.Identity)
            {
                _m1 = _borderScaleMatrix;
                SetMatrices();
            }
        }
        private void SetRadius()
        {
            SetUniformF(Uniforms[5], (_radiusPercent - _halfWidth) * (_radiusPercent - _halfWidth));
            SetUniformF(Uniforms[7], (_radiusPercent + _halfWidth) * (_radiusPercent + _halfWidth));
        }
        private void SetIDMR()
        {
            SetUniformF(Uniforms[10], _radiusPercent);
            SetUniformF(Uniforms[9], _aspect - _radiusPercent);
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

        private ColourF _borderColour = ColourF.Zero;
        public ColourF BorderColour
        {
            get => _borderColour;
            set
            {
                _borderColour = value;

                SetUniformF(Uniforms[8], (Vector4)value);
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

        private Matrix4 _borderScaleMatrix = Matrix4.Identity;

        private Matrix4 _m1 = Matrix4.Identity;
        public Matrix4 Matrix1
        {
            get => _m1;
            set
            {
                if (value == null)
                {
                    value = Matrix4.Identity;
                }

                _m1 = _borderScaleMatrix * value;
                SetMatrices();
            }
        }
        private Matrix4 _m2 = Matrix4.Identity;
        public Matrix4 Matrix2
        {
            get => _m2;
            set
            {
                if (value == null)
                {
                    value = Matrix4.Identity;
                }

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
                if (value == null)
                {
                    value = Matrix4.Identity;
                }

                _m3 = value;
                SetMatrices();
            }
        }

        public void SetMatrices(Matrix4 a, Matrix4 b, Matrix4 c)
        {
            if (a == null)
            {
                a = Matrix4.Identity;
            }
            if (b == null)
            {
                b = Matrix4.Identity;
            }
            if (c == null)
            {
                c = Matrix4.Identity;
            }

            _m1 = _borderScaleMatrix * a;
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
