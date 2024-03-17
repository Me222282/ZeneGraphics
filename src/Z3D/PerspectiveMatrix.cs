using System;
using Zene.Structs;

namespace Zene.Graphics
{
    public class PerspectiveMatrix : IMatrix
    {
        public PerspectiveMatrix() { }
        public PerspectiveMatrix(Radian fovy, double aspect, double depthNear, double depthFar)
        {
            _aspect = aspect;
            Fovy = fovy;
            _depthN = depthNear;
            DepthFar = depthFar;
        }

        public int Rows => 4;
        public int Columns => 4;

        private Radian _fov;
        public Radian Fovy
        {
            get => _fov;
            set
            {
                if (value <= 0 || value > Math.PI)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _fov = value;
                double d = 1d / Math.Tan(value * 0.5);
                _v1 = d / _aspect;
                _v2 = d;
            }
        }
        private double _aspect;
        public double Aspect
        {
            get => _aspect;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _aspect = value;
                _v1 = _v2 / value;
            }
        }
        private double _depthN;
        public double DepthNear
        {
            get => _depthN;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _depthN = value;
                double dm = 1d / (_depthF - value);
                _v3 = _depthF * dm;
                _v4 = -value * _v3;
            }
        }
        private double _depthF;
        public double DepthFar
        {
            get => _depthF;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _depthF = value;
                double dm = 1d / (value - _depthN);
                _v3 = value * dm;
                _v4 = -_depthN * _v3;
            }
        }

        private double _v1;
        private double _v2;
        private double _v3;
        private double _v4;

        public MatrixSpan MatrixData()
        {
            return new MatrixSpan(4, 4, new double[]
            {
                _v1, 0d, 0d, 0d,
                0d, _v2, 0d, 0d,
                0d, 0d, _v3, 1d,
                0d, 0d, _v4, 0d
            });
        }
    }
}
