using Zene.Structs;

namespace Zene.Graphics
{
    public class OrthographicMatrix : IMatrix, ISizeable
    {
        public OrthographicMatrix() { }
        public OrthographicMatrix(double width, double height, double depthNear, double depthFar)
        {
            Width = width;
            Height = height;
            _depthN = depthNear;
            DepthFar = depthFar;
            _data[15] = 1d;
        }

        public int Rows => 4;
        public int Columns => 4;
        
        public bool Constant => false;
        
        private double _width;
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                _v1 = 1d / value;
            }
        }
        private double _height;
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                _v2 = 1d / value;
            }
        }
        public Vector2 Size
        {
            get => new Vector2(_width, _height);
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }
        private double _depthN;
        public double DepthNear
        {
            get => _depthN;
            set
            {
                _depthN = value;
                double invFN = 1d / (value - _depthF);
                _v3 = 2d * invFN;
                _v4 = (value + _depthF) * invFN;
            }
        }
        private double _depthF;
        public double DepthFar
        {
            get => _depthF;
            set
            {
                _depthF = value;
                double invFN = 1d / (_depthN - value);
                _v3 = 2d * invFN;
                _v4 = (value + _depthN) * invFN;
            }
        }
        
        private double[] _data = new double[16];
        
        private double _v1
        {
            set => _data[0] = value;
        }
        private double _v2
        {
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

        Vector2I ISizeable.Size { set { Width = value.X; Height = value.Y; }  }

        public MatrixSpan MatrixData() => new MatrixSpan(4, 4, _data);
    }
}
