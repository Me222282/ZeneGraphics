using System;

namespace Zene.Structs
{
    public struct Matrix4x3<T> : IMatrix<T> where T : unmanaged
    {
        public Matrix4x3(Vector3<T> row0, Vector3<T> row1, Vector3<T> row2, Vector3<T> row3)
        {
            _matrix = new T[,]
            {
                { row0.X, row1.X, row2.X, row3.X },
                { row0.Y, row1.Y, row2.Y, row3.Y },
                { row0.Z, row1.Z, row2.Z, row3.Z }
            };
        }
        public Matrix4x3(T[,] matrix)
        {
            _matrix = new T[3, 4];

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    try
                    {
                        _matrix[x, y] = matrix[x, y];
                    }
                    catch (Exception)
                    {
                        throw new Exception("Matrix needs to have at least 4 rows and 3 columns.");
                    }
                }
            }
        }
        public Matrix4x3(T[] matrix)
        {
            _matrix = new T[3, 4];

            if (matrix.Length < 12)
            {
                throw new Exception("Matrix needs to have at least 4 rows and 3 columns.");
            }

            _matrix[0, 0] = matrix[0];
            _matrix[1, 0] = matrix[1];
            _matrix[2, 0] = matrix[2];

            _matrix[0, 1] = matrix[3];
            _matrix[1, 1] = matrix[4];
            _matrix[2, 1] = matrix[5];

            _matrix[0, 2] = matrix[6];
            _matrix[1, 2] = matrix[7];
            _matrix[2, 2] = matrix[8];

            _matrix[0, 3] = matrix[9];
            _matrix[1, 3] = matrix[10];
            _matrix[2, 3] = matrix[11];
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
        int IMatrix<T>.ColumnSize => 3;

        public T this[int x, int y]
        {
            get
            {
                if (x >= 3 || y >= 4)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 3 x 4 range of matrix4x3.");
                }

                return _matrix[x, y];
            }
            set
            {
                if (x >= 3 || y >= 4)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 3 x 4 range of matrix4x3.");
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
        public Vector3<T> Row2
        {
            get
            {
                return new Vector3<T>(_matrix[0, 2], _matrix[1, 2], _matrix[2, 2]);
            }
            set
            {
                _matrix[0, 2] = value.X;
                _matrix[1, 2] = value.Y;
                _matrix[2, 2] = value.Z;
            }
        }
        public Vector3<T> Row3
        {
            get
            {
                return new Vector3<T>(_matrix[0, 3], _matrix[1, 3], _matrix[2, 3]);
            }
            set
            {
                _matrix[0, 3] = value.X;
                _matrix[1, 3] = value.Y;
                _matrix[2, 3] = value.Z;
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
        public Vector4<T> Column2
        {
            get
            {
                return new Vector4<T>(_matrix[2, 0], _matrix[2, 1], _matrix[2, 2], _matrix[2, 3]);
            }
            set
            {
                _matrix[2, 0] = value.X;
                _matrix[2, 1] = value.Y;
                _matrix[2, 2] = value.Z;
                _matrix[2, 3] = value.W;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix4x3<T> matrix &&
                _matrix == matrix._matrix;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public static bool operator ==(Matrix4x3<T> a, Matrix4x3<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Matrix4x3<T> a, Matrix4x3<T> b)
        {
            return !a.Equals(b);
        }
    }
}
