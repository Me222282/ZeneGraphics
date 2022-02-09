using System;

namespace Zene.Structs
{
    public struct Matrix3x2<T> : IMatrix<T> where T : unmanaged
    {
        public Matrix3x2(Vector2<T> row0, Vector2<T> row1, Vector2<T> row2)
        {
            _matrix = new T[,]
            {
                { row0.X, row1.X, row2.X },
                { row0.Y, row1.Y, row2.Y }
            };
        }
        public Matrix3x2(T[,] matrix)
        {
            _matrix = new T[2, 3];

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    try
                    {
                        _matrix[x, y] = matrix[x, y];
                    }
                    catch (Exception)
                    {
                        throw new Exception("Matrix needs to have at least 3 rows and 2 columns.");
                    }
                }
            }
        }
        public Matrix3x2(T[] matrix)
        {
            _matrix = new T[2, 3];

            if (matrix.Length < 6)
            {
                throw new Exception("Matrix needs to have at least 3 rows and 2 columns.");
            }

            _matrix[0, 0] = matrix[0];
            _matrix[1, 0] = matrix[1];

            _matrix[0, 1] = matrix[2];
            _matrix[1, 1] = matrix[3];

            _matrix[0, 2] = matrix[4];
            _matrix[1, 2] = matrix[5];
        }

        private readonly T[,] _matrix;
        public T[,] Data
        {
            get
            {
                return _matrix;
            }
        }

        int IMatrix<T>.RowSize => 3;
        int IMatrix<T>.ColumnSize => 2;

        public T this[int x, int y]
        {
            get
            {
                if (x >= 2 || y >= 3)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 2 x 3 range of matrix3x2.");
                }

                return _matrix[x, y];
            }
            set
            {
                if (x >= 2 || y >= 3)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 2 x 3 range of matrix3x2.");
                }

                _matrix[x, y] = value;
            }
        }

        public Vector2<T> Row0
        {
            get
            {
                return new Vector2<T>(_matrix[0, 0], _matrix[1, 0]);
            }
            set
            {
                _matrix[0, 0] = value.X;
                _matrix[1, 0] = value.Y;
            }
        }
        public Vector2<T> Row1
        {
            get
            {
                return new Vector2<T>(_matrix[0, 1], _matrix[1, 1]);
            }
            set
            {
                _matrix[0, 1] = value.X;
                _matrix[1, 1] = value.Y;
            }
        }
        public Vector2<T> Row2
        {
            get
            {
                return new Vector2<T>(_matrix[0, 2], _matrix[1, 2]);
            }
            set
            {
                _matrix[0, 2] = value.X;
                _matrix[1, 2] = value.Y;
            }
        }
        public Vector3<T> Column0
        {
            get
            {
                return new Vector3<T>(_matrix[0, 0], _matrix[0, 1], _matrix[0, 2]);
            }
            set
            {
                _matrix[0, 0] = value.X;
                _matrix[0, 1] = value.Y;
                _matrix[0, 2] = value.Z;
            }
        }
        public Vector3<T> Column1
        {
            get
            {
                return new Vector3<T>(_matrix[1, 0], _matrix[1, 1], _matrix[1, 2]);
            }
            set
            {
                _matrix[1, 0] = value.X;
                _matrix[1, 1] = value.Y;
                _matrix[1, 2] = value.Z;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix3x2<T> matrix &&
                _matrix == matrix._matrix;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public static bool operator ==(Matrix3x2<T> a, Matrix3x2<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Matrix3x2<T> a, Matrix3x2<T> b)
        {
            return !a.Equals(b);
        }
    }
}
