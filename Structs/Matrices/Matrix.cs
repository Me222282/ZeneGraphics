using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that manages a mathmatical matrix stored as doubles.
    /// </summary>
    /// <typeparam name="T">The type of value in each slot of the grid.</typeparam>
    public struct Matrix : IMatrix<double>
    {
        /// <summary>
        /// Creates a matrix from a size and array.
        /// </summary>
        /// <param name="row">The number of rows in the matrix.</param>
        /// <param name="column">The number of columns in the matrix.</param>
        /// <param name="matrix">The valeus to be stored in the matrix.</param>
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
        /// <summary>
        /// Creates a matrix from a 2d array.
        /// </summary>
        /// <param name="matrix">The array to crewate the matrix from.</param>
        public Matrix(double[,] matrix)
        {
            RowSize = matrix.GetLength(1);
            ColumnSize = matrix.GetLength(0);

            _matrix = matrix;
        }
        /// <summary>
        /// Creates a matrix from another matrix.
        /// </summary>
        /// <param name="matrix">The matrix to reference from.</param>
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

        /// <summary>
        /// Adds this matrix to another matrix.
        /// </summary>
        /// <param name="matrix">The matrix to be added.</param>
        /// <returns>The result of the addition.</returns>
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
        /// <summary>
        /// Subtracts another matrix from this matrix.
        /// </summary>
        /// <param name="matrix">The matrix to be added.</param>
        /// <returns>The result of the subtraction.</returns>
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
        /// <summary>
        /// Multiplies this matrix by a value.
        /// </summary>
        /// <param name="value">The value to multiple by.</param>
        /// <returns>The result of the addition.</returns>
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
        /// <summary>
        /// Multiplies this matrix by another matrix.
        /// </summary>
        /// <param name="matrix">The matrix to multiple to this matrix.</param>
        /// <returns>The result of the multiplication.</returns>
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
