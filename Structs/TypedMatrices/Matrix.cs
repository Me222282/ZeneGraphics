using System;

namespace Zene.Structs
{
    public struct Matrix<T> : IMatrix<T> where T : unmanaged
    {
        public Matrix(int row, int column, T[] matrix)
        {
            RowSize = row;
            ColumnSize = column;

            if (matrix.Length < row * column)
            {
                throw new Exception($"Matrix needs to have at least {row} rows and {column} columns.");
            }

            _matrix = new T[column, row];

            for (int x = 0; x < column; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    _matrix[x, y] = matrix[x + (y * row)];
                }
            }
        }
        public Matrix(T[,] matrix)
        {
            RowSize = matrix.GetLength(1);
            ColumnSize = matrix.GetLength(0);

            _matrix = matrix;
        }
        public Matrix(IMatrix<T> matrix)
        {
            RowSize = matrix.RowSize;
            ColumnSize = matrix.ColumnSize;
            _matrix = matrix.Data;
        }

        private readonly T[,] _matrix;
        public T[,] Data => _matrix;

        public int RowSize { get; }
        public int ColumnSize { get; }

        public T this[int x, int y]
        {
            get => _matrix[x, y];
            set => _matrix[x, y] = value;
        }

        public override bool Equals(object obj)
        {
            return obj is IMatrix<T> matrix &&
                _matrix == matrix.Data;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public static bool operator ==(Matrix<T> a, IMatrix<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Matrix<T> a, IMatrix<T> b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(IMatrix<T> a, Matrix<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(IMatrix<T> a, Matrix<T> b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(Matrix<T> a, Matrix<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Matrix<T> a, Matrix<T> b)
        {
            return !a.Equals(b);
        }
    }
}
