using System;

namespace Zene.Structs
{
    public struct Matrix2x4<T> : IMatrix<T> where T : unmanaged
    {
        public Matrix2x4(Vector4<T> row0, Vector4<T> row1)
        {
            _matrix = new T[,]
            {
                { row0.X, row1.X },
                { row0.Y, row1.Y },
                { row0.Z, row1.Z },
                { row0.W, row1.W }
            };
        }
        public Matrix2x4(T[,] matrix)
        {
            _matrix = new T[4, 2];

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    try
                    {
                        _matrix[x, y] = matrix[x, y];
                    }
                    catch (Exception)
                    {
                        throw new Exception("Matrix needs to have at least 2 rows and 4 columns.");
                    }
                }
            }
        }
        public Matrix2x4(T[] matrix)
        {
            _matrix = new T[4, 2];

            if (matrix.Length < 8)
            {
                throw new Exception("Matrix needs to have at least 2 rows and 4 columns.");
            }

            _matrix[0, 0] = matrix[0];
            _matrix[1, 0] = matrix[1];
            _matrix[2, 0] = matrix[2];
            _matrix[3, 0] = matrix[3];
            _matrix[0, 1] = matrix[4];
            _matrix[1, 1] = matrix[5];
            _matrix[2, 1] = matrix[6];
            _matrix[3, 0] = matrix[7];
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
        int IMatrix<T>.ColumnSize => 4;

        public T this[int x, int y]
        {
            get
            {
                if (x >= 4 || y >= 2)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 4 x 2 range of matrix2x4.");
                }

                return _matrix[x, y];
            }
            set
            {
                if (x >= 4 || y >= 2)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 4 x 2 range of matrix2x4.");
                }

                _matrix[x, y] = value;
            }
        }

        public Vector4<T> Row0
        {
            get
            {
                return new Vector4<T>(_matrix[0, 0], _matrix[1, 0], _matrix[2, 0], _matrix[3, 0]);
            }
            set
            {
                _matrix[0, 0] = value.X;
                _matrix[1, 0] = value.Y;
                _matrix[2, 0] = value.Z;
                _matrix[3, 0] = value.W;
            }
        }
        public Vector4<T> Row1
        {
            get
            {
                return new Vector4<T>(_matrix[0, 1], _matrix[1, 1], _matrix[2, 1], _matrix[3, 1]);
            }
            set
            {
                _matrix[0, 1] = value.X;
                _matrix[1, 1] = value.Y;
                _matrix[2, 1] = value.Z;
                _matrix[3, 1] = value.W;
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
        public Vector2<T> Column3
        {
            get
            {
                return new Vector2<T>(_matrix[3, 0], _matrix[3, 1]);
            }
            set
            {
                _matrix[3, 0] = value.X;
                _matrix[3, 1] = value.Y;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix2x4<T> matrix &&
                _matrix == matrix._matrix;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public static bool operator ==(Matrix2x4<T> a, Matrix2x4<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Matrix2x4<T> a, Matrix2x4<T> b)
        {
            return !a.Equals(b);
        }
    }
}
