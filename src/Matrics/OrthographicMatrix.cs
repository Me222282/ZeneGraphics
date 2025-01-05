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
        
        private double _v1;
        private double _v2;
        private double _v3;
        private double _v4;

        Vector2I ISizeable.Size { set { Width = value.X; Height = value.Y; }  }

        public void MatrixData(MatrixSpan ms)
        {
            // 4x4 only
            if (ms.Rows != 4 || ms.Columns != 4)
            {
                ms.Padding(0, 0);
                return;
            }
            
            ms.Data[0] = _v1;
            ms.Data[5] = _v2;
            ms.Data[10] = _v3;
            ms.Data[14] = _v4;
            ms.Data[15] = 1d;
        }
    }
}
