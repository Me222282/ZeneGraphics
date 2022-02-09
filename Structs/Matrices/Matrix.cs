using System;

namespace Zene.Structs
{
    public struct Matrix : IMatrix<double>
    {
        public Matrix(int row, int column, double[] matrix)
        {
            RowSize = row;
            ColumnSize = column;

            if (matrix.Length < row * column)
            {
                throw new Exception($"Matrix needs to have at least {row} rows and {column} columns.");
            }

            _matrix = new double[column, row];

            for (int x = 0; x < column; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    _matrix[x, y] = matrix[x + (y * row)];
                }
            }
        }
        public Matrix(double[,] matrix)
        {
            RowSize = matrix.GetLength(1);
            ColumnSize = matrix.GetLength(0);

            _matrix = matrix;
        }
        public Matrix(IMatrix<double> matrix)
        {
            RowSize = matrix.RowSize;
            ColumnSize = matrix.ColumnSize;
            _matrix = matrix.Data;
        }

        private readonly double[,] _matrix;
        public double[,] Data
        {
            get => _matrix;
        }

        public int RowSize { get; }
        public int ColumnSize { get; }

        public double this[int x, int y]
        {
            get => _matrix[x, y];
            set
            {
                _matrix[x, y] = value;
            }
        }

        public Matrix Add(IMatrix<double> matrix)
        {
            if ((matrix.RowSize != RowSize) || (matrix.ColumnSize != ColumnSize))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have {RowSize} rows and {ColumnSize} columns.");
            }

            Matrix output = new Matrix(new double[RowSize, ColumnSize]);

            for (int x = 0; x < ColumnSize; x++)
            {
                for (int y = 0; y < RowSize; y++)
                {
                    output[x, y] = _matrix[x, y] + matrix[x, y];
                }
            }

            return output;
        }
        public Matrix Subtract(IMatrix<double> matrix)
        {
            if ((matrix.RowSize != RowSize) || (matrix.ColumnSize != ColumnSize))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have {RowSize} rows and {ColumnSize} columns.");
            }

            Matrix output = new Matrix(new double[RowSize, ColumnSize]);

            for (int x = 0; x < ColumnSize; x++)
            {
                for (int y = 0; y < RowSize; y++)
                {
                    output[x, y] = _matrix[x, y] - matrix[x, y];
                }
            }

            return output;
        }
        public Matrix Multiply(double value)
        {
            Matrix output = new Matrix(new double[RowSize, ColumnSize]);

            for (int x = 0; x < ColumnSize; x++)
            {
                for (int y = 0; y < RowSize; y++)
                {
                    output[x, y] = _matrix[x, y] * value;
                }
            }

            return output;
        }

        public Matrix Multiply(IMatrix<double> matrix)
        {
            if (matrix.RowSize != ColumnSize)
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have {ColumnSize} rows.");
            }

            Matrix output = new Matrix(new double[RowSize, matrix.ColumnSize]);

            for (int x = 0; x < matrix.ColumnSize; x++)
            {
                for (int y = 0; y < RowSize; y++)
                {
                    double value = 0;

                    for (int m = 0; m < ColumnSize; m++)
                    {
                        value += _matrix[m, y] * matrix[x, m];
                    }

                    output[x, y] = value;
                }
            }

            return output;
        }

        public override bool Equals(object obj)
        {
            return obj is IMatrix<double> matrix &&
                _matrix == matrix.Data;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public static bool operator ==(Matrix a, IMatrix<double> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Matrix a, IMatrix<double> b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(IMatrix<double> a, Matrix b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(IMatrix<double> a, Matrix b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(Matrix a, Matrix b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Matrix a, Matrix b)
        {
            return !a.Equals(b);
        }

        public static Matrix operator +(Matrix a, IMatrix<double> b)
        {
            return a.Add(b);
        }
        public static Matrix operator -(Matrix a, IMatrix<double> b)
        {
            return a.Subtract(b);
        }
        public static Matrix operator *(Matrix a, IMatrix<double> b)
        {
            return a.Multiply(b);
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            return a.Add(b);
        }
        public static Matrix operator -(Matrix a, Matrix b)
        {
            return a.Subtract(b);
        }
        public static Matrix operator *(Matrix a, Matrix b)
        {
            return a.Multiply(b);
        }

        public static Matrix operator *(Matrix a, double b)
        {
            return a.Multiply(b);
        }
        public static Matrix operator *(double a, Matrix b)
        {
            return b.Multiply(a);
        }
    }
}
