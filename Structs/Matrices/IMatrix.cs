using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that manages a mathmatical matrix stored as a given type.
    /// </summary>
    /// <typeparam name="T">The type of value in each slot of the grid.</typeparam>
    public interface IMatrix<T> where T : unmanaged
    {
        /// <summary>
        /// An 2d array storing the values of the matrix.
        /// </summary>
        public T[,] Data { get; }

        /// <summary>
        /// The number of rows in the matrix.
        /// </summary>
        public int RowSize { get; }
        /// <summary>
        /// The number of columns in the matrix
        /// </summary>
        public int ColumnSize { get; }

        /// <summary>
        /// Gets or sets a value in the matrix.
        /// </summary>
        /// <param name="x">The column to reference.</param>
        /// <param name="y">The row to reference.</param>
        /// <returns></returns>
        public T this[int x, int y] { get; set; }

        public static Matrix<T> operator +(IMatrix<T> a, IMatrix<T> b)
        {
            if ((b.RowSize != a.RowSize) || (b.ColumnSize != a.ColumnSize))
            {
                throw new Exception($"{nameof(b)} doesn't have a compatable size with {nameof(a)}. Must have {a.RowSize} rows and {a.ColumnSize} columns.");
            }

            Matrix<T> output = new Matrix<T>(new T[a.RowSize, a.ColumnSize]);

            try
            {
                for (int x = 0; x < a.ColumnSize; x++)
                {
                    for (int y = 0; y < a.RowSize; y++)
                    {
                        output[x, y] = (T)(object)(((double)(object)a[x, y]) + ((double)(object)b[x, y]));
                    }
                }
            }
            catch { throw; }

            return output;
        }
        public static Matrix<T> operator -(IMatrix<T> a, IMatrix<T> b)
        {
            if ((b.RowSize != a.RowSize) || (b.ColumnSize != a.ColumnSize))
            {
                throw new Exception($"{nameof(b)} doesn't have a compatable size with {nameof(a)}. Must have {a.RowSize} rows and {a.ColumnSize} columns.");
            }

            Matrix<T> output = new Matrix<T>(new T[a.RowSize, a.ColumnSize]);

            try
            {
                for (int x = 0; x < a.ColumnSize; x++)
                {
                    for (int y = 0; y < a.RowSize; y++)
                    {
                        output[x, y] = (T)(object)(((double)(object)a[x, y]) - ((double)(object)b[x, y]));
                    }
                }
            }
            catch { throw; }

            return output;
        }

        public static Matrix<T> operator *(IMatrix<T> a, IMatrix<T> b)
        {
            if (b.RowSize != a.ColumnSize)
            {
                throw new Exception($"{nameof(b)} doesn't have a compatable size with {nameof(a)}. Must have {a.ColumnSize} rows.");
            }

            Matrix<T> output = new Matrix<T>(new T[a.RowSize, b.ColumnSize]);

            try
            {
                for (int x = 0; x < b.ColumnSize; x++)
                {
                    for (int y = 0; y < a.RowSize; y++)
                    {
                        double value = 0;

                        for (int m = 0; m < a.ColumnSize; m++)
                        {
                            value += ((double)(object)a[m, y]) * ((double)(object)b[x, m]);
                        }

                        output[x, y] = (T)(object)value;
                    }
                }
            }
            catch { throw; }

            return output;
        }
        public static Matrix<T> operator *(IMatrix<T> a, T value)
        {
            Matrix<T> output = new Matrix<T>(new T[a.RowSize, a.ColumnSize]);

            try
            {
                for (int x = 0; x < a.ColumnSize; x++)
                {
                    for (int y = 0; y < a.RowSize; y++)
                    {
                        output[x, y] = (T)(object)(((double)(object)a[x, y]) * ((double)(object)value));
                    }
                }
            }
            catch { throw; }

            return output;
        }
    }
}
