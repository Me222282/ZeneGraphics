using Zene.Structs;

namespace Zene.Graphics
{
    public class MultiplyMatrix4 : IMatrix
    {
        public MultiplyMatrix4(IMatrix left, IMatrix right)
        {
            _left = left;
            _right = right;
        }

        public int Rows => 4;
        public int Columns => 4;
        
        public bool Constant => (_left is null || _left.Constant) && (_right is null || _right.Constant);
        
        private IMatrix _left;
        public IMatrix Left
        {
            get => _left;
            set
            {
                if (_left != value)
                {
                    _dataCache = null;
                }
                _left = value;
            }
        }
        private IMatrix _right;
        public IMatrix Right
        {
            get => _right;
            set
            {
                if (_right != value)
                {
                    _dataCache = null;
                }
                _right = value;
            }
        }
        
        private double[] _dataCache = null;
        
        public MatrixSpan MatrixData()
        {
            if (!Constant) { _dataCache = null; }
            else if (_dataCache != null)
            {
                return new MatrixSpan(4, 4, _dataCache);
            }
            
            if (_left == null)
            {
                if (_right == null)
                {
                    return MatrixSpan.Identity;
                }

                return _right.MatrixData();
            }
            if (_right == null)
            {
                return _left.MatrixData();
            }
            
            MatrixSpan a = _left.MatrixData();
            MatrixSpan b = _right.MatrixData();
            
            double[] data = new double[]
            {
                (a[0, 0] * b[0, 0]) + (a[1, 0] * b[0, 1]) + (a[2, 0] * b[0, 2]) + (a[3, 0] * b[0, 3]),
                (a[0, 0] * b[1, 0]) + (a[1, 0] * b[1, 1]) + (a[2, 0] * b[1, 2]) + (a[3, 0] * b[1, 3]),
                (a[0, 0] * b[2, 0]) + (a[1, 0] * b[2, 1]) + (a[2, 0] * b[2, 2]) + (a[3, 0] * b[2, 3]),
                (a[0, 0] * b[3, 0]) + (a[1, 0] * b[3, 1]) + (a[2, 0] * b[3, 2]) + (a[3, 0] * b[3, 3]),
                (a[0, 1] * b[0, 0]) + (a[1, 1] * b[0, 1]) + (a[2, 1] * b[0, 2]) + (a[3, 1] * b[0, 3]),
                (a[0, 1] * b[1, 0]) + (a[1, 1] * b[1, 1]) + (a[2, 1] * b[1, 2]) + (a[3, 1] * b[1, 3]),
                (a[0, 1] * b[2, 0]) + (a[1, 1] * b[2, 1]) + (a[2, 1] * b[2, 2]) + (a[3, 1] * b[2, 3]),
                (a[0, 1] * b[3, 0]) + (a[1, 1] * b[3, 1]) + (a[2, 1] * b[3, 2]) + (a[3, 1] * b[3, 3]),
                (a[0, 2] * b[0, 0]) + (a[1, 2] * b[0, 1]) + (a[2, 2] * b[0, 2]) + (a[3, 2] * b[0, 3]),
                (a[0, 2] * b[1, 0]) + (a[1, 2] * b[1, 1]) + (a[2, 2] * b[1, 2]) + (a[3, 2] * b[1, 3]),
                (a[0, 2] * b[2, 0]) + (a[1, 2] * b[2, 1]) + (a[2, 2] * b[2, 2]) + (a[3, 2] * b[2, 3]),
                (a[0, 2] * b[3, 0]) + (a[1, 2] * b[3, 1]) + (a[2, 2] * b[3, 2]) + (a[3, 2] * b[3, 3]),
                (a[0, 3] * b[0, 0]) + (a[1, 3] * b[0, 1]) + (a[2, 3] * b[0, 2]) + (a[3, 3] * b[0, 3]),
                (a[0, 3] * b[1, 0]) + (a[1, 3] * b[1, 1]) + (a[2, 3] * b[1, 2]) + (a[3, 3] * b[1, 3]),
                (a[0, 3] * b[2, 0]) + (a[1, 3] * b[2, 1]) + (a[2, 3] * b[2, 2]) + (a[3, 3] * b[2, 3]),
                (a[0, 3] * b[3, 0]) + (a[1, 3] * b[3, 1]) + (a[2, 3] * b[3, 2]) + (a[3, 3] * b[3, 3])
            };
            
            if (Constant) { _dataCache = data; }
            
            return new MatrixSpan(4, 4, data);
        }
    }
}
