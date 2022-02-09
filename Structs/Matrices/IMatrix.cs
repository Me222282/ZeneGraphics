using System;

namespace Zene.Structs
{
    public interface IMatrix<T> where T : unmanaged
    {
        public T[,] Data { get; }

        public int RowSize { get; }
        public int ColumnSize { get; }

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
