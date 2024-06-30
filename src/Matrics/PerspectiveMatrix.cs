using System;
using Zene.Structs;

namespace Zene.Graphics
{
    public class PerspectiveMatrix : IMatrix, ISizeable
    {
        public PerspectiveMatrix() { }
        public PerspectiveMatrix(Radian fovy, double aspect, double depthNear, double depthFar)
        {
            _aspect = aspect;
            Fovy = fovy;
            _depthN = depthNear;
            DepthFar = depthFar;
            _data[11] = 1d;
        }

        public int Rows => 4;
        public int Columns => 4;
        
        public bool Constant => false;
        
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
                double v3 = _depthF * dm;
                _v3 = v3;
                _v4 = -value * v3;
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
                double v3 = value * dm;
                _v3 = v3;
                _v4 = -_depthN * v3;
            }
        }
        
        private double[] _data = new double[16];
        
        private double _v1
        {
            set => _data[0] = value;
        }
        private double _v2
        {
            get => _data[5];
            set => _data[5] = value;
        }
        private double _v3
        {
            set => _data[10] = value;
        }
        private double _v4
        {
            set => _data[14] = value;
        }

        Vector2I ISizeable.Size { set => Aspect = (double)value.X / value.Y;  }

        public MatrixSpan MatrixData() => new MatrixSpan(4, 4, _data);
    }
}
