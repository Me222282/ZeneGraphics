using System;

namespace Zene.Structs
{
    public struct Matrix2x3<T> : IMatrix<T> where T : unmanaged
    {
        public Matrix2x3(Vector3<T> row0, Vector3<T> row1)
        {
            _matrix = new T[,]
            {
                { row0.X, row1.X },
                { row0.Y, row1.Y },
                { row0.Z, row1.Z }
            };
        }
        public Matrix2x3(T[,] matrix)
        {
            _matrix = new T[3, 2];

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    try
                    {
                        _matrix[x, y] = matrix[x, y];
                    }
                    catch (Exception)
                    {
                        throw new Exception("Matrix needs to have at least 2 rows and 3 columns.");
                    }
                }
            }
        }
        public Matrix2x3(T[] matrix)
        {
            _matrix = new T[3, 2];

            if (matrix.Length < 6)
            {
                throw new Exception("Matrix needs to have at least 2 rows and 3 columns.");
            }

            _matrix[0, 0] = matrix[0];
            _matrix[1, 0] = matrix[1];
            _matrix[2, 0] = matrix[2];
            _matrix[0, 1] = matrix[3];
            _matrix[1, 1] = matrix[4];
            _matrix[2, 1] = matrix[5];
        }

        private readonly T[,] _matrix;
        public T[,] Data
        {
            get
            {
                return _matrix;
            }
        }

        int IMatrix<T>.RowSize => 2;
        int IMatrix<T>.ColumnSize => 3;

        public T this[int x, int y]
        {
            get
            {
                if (x >= 3 || y >= 2)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 3 x 2 range of matrix2x3.");
                }

                return _matrix[x, y];
            }
            set
            {
                if (x >= 3 || y >= 2)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 3 x 2 range of matrix2x3.");
                }

                _matrix[x, y] = value;
            }
        }

        public Vector3<T> Row0
        {
            get
            {
                return new Vector3<T>(_matrix[0, 0], _matrix[1, 0], _matrix[2, 0]);
            }
            set
            {
                _matrix[0, 0] = value.X;
                _matrix[1, 0] = value.Y;
                _matrix[2, 0] = value.Z;
            }
        }
        public Vector3<T> Row1
        {
            get
            {
                return new Vector3<T>(_matrix[0, 1], _matrix[1, 1], _matrix[2, 1]);
            }
            set
            {
                _matrix[0, 1] = value.X;
                _matrix[1, 1] = value.Y;
                _matrix[2, 1] = value.Z;
            }
        }
        public Vector2<T> Column0
        {
            get
            {
                return new Vector2<T>(_matrix[0, 0], _matrix[0, 1]);
            }
            set
            {
                _matrix[0, 0] = value.X;
                _matrix[0, 1] = value.Y;
            }
        }
        public Vector2<T> Column1
        {
            get
            {
                return new Vector2<T>(_matrix[1, 0], _matrix[1, 1]);
            }
            set
            {
                _matrix[1, 0] = value.X;
                _matrix[1, 1] = value.Y;
            }
        }
        public Vector2<T> Column2
        {
            get
            {
                return new Vector2<T>(_matrix[2, 0], _matrix[2, 1]);
            }
            set
            {
                _matrix[2, 0] = value.X;
                _matrix[2, 1] = value.Y;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix2x3<T> matrix &&
                _matrix == matrix._matrix;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public static bool operator ==(Matrix2x3<T> a, Matrix2x3<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Matrix2x3<T> a, Matrix2x3<T> b)
        {
            return !a.Equals(b);
        }
    }
}
