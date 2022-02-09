using System;

namespace Zene.Structs
{
    public struct Matrix4x2<T> : IMatrix<T> where T : unmanaged
    {
        public Matrix4x2(Vector2<T> row0, Vector2<T> row1, Vector2<T> row2, Vector2<T> row3)
        {
            _matrix = new T[,]
            {
                { row0.X, row1.X, row2.X, row3.X },
                { row0.Y, row1.Y, row2.Y, row3.Y }
            };
        }
        public Matrix4x2(T[,] matrix)
        {
            _matrix = new T[2, 4];

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    try
                    {
                        _matrix[x, y] = matrix[x, y];
                    }
                    catch (Exception)
                    {
                        throw new Exception("Matrix needs to have at least 4 rows and 2 columns.");
                    }
                }
            }
        }
        public Matrix4x2(T[] matrix)
        {
            _matrix = new T[2, 4];

            if (matrix.Length < 8)
            {
                throw new Exception("Matrix needs to have at least 4 rows and 2 columns.");
            }

            _matrix[0, 0] = matrix[0];
            _matrix[1, 0] = matrix[1];

            _matrix[0, 1] = matrix[2];
            _matrix[1, 1] = matrix[3];

            _matrix[0, 2] = matrix[4];
            _matrix[1, 2] = matrix[5];

            _matrix[0, 3] = matrix[6];
            _matrix[1, 3] = matrix[7];
        }

        private readonly T[,] _matrix;
        public T[,] Data
        {
            get
            {
                return _matrix;
            }
        }

        int IMatrix<T>.RowSize => 4;
        int IMatrix<T>.ColumnSize => 2;

        public T this[int x, int y]
        {
            get
            {
                if (x >= 2 || y >= 4)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 2 x 4 range of matrix4x2.");
                }

                return _matrix[x, y];
            }
            set
            {
                if (x >= 2 || y >= 4)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 2 x 4 range of matrix4x2.");
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
        public Vector2<T> Row3
        {
            get
            {
                return new Vector2<T>(_matrix[0, 3], _matrix[1, 3]);
            }
            set
            {
                _matrix[0, 3] = value.X;
                _matrix[1, 3] = value.Y;
            }
        }
        public Vector4<T> Column0
        {
            get
            {
                return new Vector4<T>(_matrix[0, 0], _matrix[0, 1], _matrix[0, 2], _matrix[0, 3]);
            }
            set
            {
                _matrix[0, 0] = value.X;
                _matrix[0, 1] = value.Y;
                _matrix[0, 2] = value.Z;
                _matrix[0, 3] = value.W;
            }
        }
        public Vector4<T> Column1
        {
            get
            {
                return new Vector4<T>(_matrix[1, 0], _matrix[1, 1], _matrix[1, 2], _matrix[1, 3]);
            }
            set
            {
                _matrix[1, 0] = value.X;
                _matrix[1, 1] = value.Y;
                _matrix[1, 2] = value.Z;
                _matrix[1, 3] = value.W;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix4x2<T> matrix &&
                _matrix == matrix._matrix;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public static bool operator ==(Matrix4x2<T> a, Matrix4x2<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Matrix4x2<T> a, Matrix4x2<T> b)
        {
            return !a.Equals(b);
        }
    }
}
