using System;

namespace Zene.Structs
{
    public struct Matrix2 : IMatrix<double>
    {
        public Matrix2(Vector2 row0, Vector2 row1)
        {
            _matrix = new double[,]
            {
                { row0.X, row1.X },
                { row0.Y, row1.Y }
            };
        }

        public Matrix2(double[,] matrix)
        {
            _matrix = new double[2, 2];

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
                        throw new Exception("Matrix needs to have at least 2 rows and 2 columns.");
                    }
                }
            }
        }
        public Matrix2(double[] matrix)
        {
            _matrix = new double[2, 2];

            if (matrix.Length < 4)
            {
                throw new Exception("Matrix needs to have at least 2 rows and 2 columns.");
            }

            _matrix[0, 0] = matrix[0];
            _matrix[1, 0] = matrix[1];
            _matrix[0, 1] = matrix[2];
            _matrix[1, 1] = matrix[3];
        }

        private readonly double[,] _matrix;

        public double[,] Data => _matrix;
        int IMatrix<double>.RowSize => 2;
        int IMatrix<double>.ColumnSize => 2;

        public double this[int x, int y]
        {
            get
            {
                if (x >= 2 || y >= 2)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 2 x 2 range of matrix2.");
                }

                return _matrix[x, y];
            }
            set
            {
                if (x >= 2 || y >= 2)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 2 x 2 range of matrix2.");
                }

                _matrix[x, y] = value;
            }
        }

        public Vector2 Row0
        {
            get
            {
                return new Vector2(_matrix[0, 0], _matrix[1, 0]);
            }
            set
            {
                _matrix[0, 0] = value.X;
                _matrix[1, 0] = value.Y;
            }
        }
        public Vector2 Row1
        {
            get
            {
                return new Vector2(_matrix[0, 1], _matrix[1, 1]);
            }
            set
            {
                _matrix[0, 1] = value.X;
                _matrix[1, 1] = value.Y;
            }
        }
        public Vector2 Column0
        {
            get
            {
                return new Vector2(_matrix[0, 0], _matrix[0, 1]);
            }
            set
            {
                _matrix[0, 0] = value.X;
                _matrix[0, 1] = value.Y;
            }
        }
        public Vector2 Column1
        {
            get
            {
                return new Vector2(_matrix[1, 0], _matrix[1, 1]);
            }
            set
            {
                _matrix[1, 0] = value.X;
                _matrix[1, 1] = value.Y;
            }
        }

        public Matrix2 Add(Matrix2 matrix)
        {
            return new Matrix2(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] + matrix[0, 0], /*x:0 y:1*/this[0, 1] + matrix[0, 1] },
                { /*x:1 y:0*/this[1, 0] + matrix[1, 0], /*x:1 y:1*/this[1, 1] + matrix[1, 1] }
            });
        }

        public Matrix2 Subtract(Matrix2 matrix)
        {
            return new Matrix2(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] - matrix[0, 0], /*x:0 y:1*/this[0, 1] - matrix[0, 1] },
                { /*x:1 y:0*/this[1, 0] - matrix[1, 0], /*x:1 y:1*/this[1, 1] - matrix[1, 1] }
            });
        }

        public Matrix2 Multiply(double value)
        {
            return new Matrix2(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] * value, /*x:0 y:1*/this[0, 1] * value },
                { /*x:1 y:0*/this[1, 0] * value, /*x:1 y:1*/this[1, 1] * value }
            });
        }

        public Matrix2 Multiply(Matrix2 matrix)
        {
            return new Matrix2(new double[,]
            {
                { /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]), /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) },
                { /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]), /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) }
            });
        }

        public Matrix2x3 Multiply(Matrix2x3 matrix)
        {
            return new Matrix2x3(new double[,]
            {
                { /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]), /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) },
                { /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]), /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) },
                { /*x:2 y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]), /*x:2 y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]) }
            });
        }

        public Matrix2x4 Multiply(Matrix2x4 matrix)
        {
            return new Matrix2x4(new double[,]
            {
                { /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]), /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) },
                { /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]), /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) },
                { /*x:2 y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]), /*x:2 y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]) },
                { /*x:3 y:0*/(this[0, 0] * matrix[3, 0]) + (this[1, 0] * matrix[3, 1]), /*x:3 y:1*/(this[0, 1] * matrix[3, 0]) + (this[1, 1] * matrix[3, 1]) }
            });
        }

        public Matrix Add(IMatrix<double> matrix)
        {
            if ((matrix.RowSize != 2) || (matrix.ColumnSize != 2))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 2 rows and 2 columns.");
            }

            Matrix output = new Matrix(new double[2, 2]);

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    output[x, y] = _matrix[x, y] + matrix[x, y];
                }
            }

            return output;
        }
        public Matrix Subtract(IMatrix<double> matrix)
        {
            if ((matrix.RowSize != 2) || (matrix.ColumnSize != 2))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 2 rows and 2 columns.");
            }

            Matrix output = new Matrix(new double[2, 2]);

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    output[x, y] = _matrix[x, y] - matrix[x, y];
                }
            }

            return output;
        }
        public Matrix Multiply(IMatrix<double> matrix)
        {
            if (matrix.RowSize != 2)
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have {2} rows.");
            }

            Matrix output = new Matrix(new double[2, matrix.ColumnSize]);

            for (int x = 0; x < matrix.ColumnSize; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    double value = 0;

                    for (int m = 0; m < 2; m++)
                    {
                        value += _matrix[m, y] * matrix[x, m];
                    }

                    output[x, y] = value;
                }
            }

            return output;
        }

        public double Determinant()
        {
            return (this[0, 0] * this[1, 1]) - (this[1, 0] * this[0, 1]);
        }

        public double Trace()
        {
            return this[0, 0] + this[1, 1];
        }

        public Matrix2 Normalize()
        {
            double det = Determinant();

            return new Matrix2(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] / det, /*x:0 y:1*/this[0, 1] / det },
                { /*x:1 y:0*/this[1, 0] / det, /*x:1 y:1*/this[1, 1] / det }
            });
        }

        public Matrix2 Invert()
        {
            double det = Determinant();

            if (det == 0)
            {
                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            }

            double invDet = 1.0 / det;

            return new Matrix2(new double[,]
            {
                { /*x:0 y:0*/this[1, 1] * invDet, /*x:0 y:1*/this[0, 1] * invDet },
                { /*x:1 y:0*/this[1, 0] * invDet, /*x:1 y:1*/this[0, 0] * invDet }
            });
        }

        public Matrix2 Transpose()
        {
            return new Matrix2(new double[,]
            {
                { /*x:0 y:0*/this[0, 0], /*x:0 y:1*/this[1, 0] },
                { /*x:1 y:0*/this[0, 1], /*x:1 y:1*/this[1, 1] }
            });
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix2 matrix &&
                _matrix == matrix._matrix;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public ReadOnlySpan<float> GetGLData()
        {
            int w = 2;
            int h = 2;

            float[] data = new float[w * h];

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    data[x + (y * w)] = (float)_matrix[x, y];
                }
            }

            return new ReadOnlySpan<float>(data);
        }

        public override string ToString()
        {
            return $@"[{_matrix[0, 0]}, {_matrix[1, 0]}]
[{_matrix[0, 1]}, {_matrix[1, 1]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0, 0].ToString(format)}, {_matrix[1, 0].ToString(format)}]
[{_matrix[0, 1].ToString(format)}, {_matrix[1, 1].ToString(format)}]";
        }

        public static bool operator ==(Matrix2 a, Matrix2 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Matrix2 a, Matrix2 b)
        {
            return !a.Equals(b);
        }

        public static Matrix2 operator +(Matrix2 a, Matrix2 b)
        {
            return a.Add(b);
        }

        public static Matrix2 operator -(Matrix2 a, Matrix2 b)
        {
            return a.Subtract(b);
        }

        public static Matrix2 operator *(Matrix2 a, Matrix2 b)
        {
            return a.Multiply(b);
        }

        public static Matrix2x3 operator *(Matrix2 a, Matrix2x3 b)
        {
            return a.Multiply(b);
        }

        public static Matrix2x4 operator *(Matrix2 a, Matrix2x4 b)
        {
            return a.Multiply(b);
        }

        public static Matrix2 operator *(Matrix2 a, double b)
        {
            return a.Multiply(b);
        }

        public static Matrix2 operator *(double a, Matrix2 b)
        {
            return b.Multiply(a);
        }

        public static Matrix operator +(Matrix2 a, IMatrix<double> b)
        {
            return a.Add(b);
        }
        public static Matrix operator -(Matrix2 a, IMatrix<double> b)
        {
            return a.Subtract(b);
        }
        public static Matrix operator *(Matrix2 a, IMatrix<double> b)
        {
            return a.Multiply(b);
        }

        public static Matrix2 CreateRotation(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix2(new Vector2(cos, sin), new Vector2(-sin, cos));
        }

        public static Matrix2 CreateScale(double scale)
        {
            return new Matrix2(new Vector2(scale, 0), new Vector2(0, scale));
        }

        public static Matrix2 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix2(new Vector2(scaleX, 0), new Vector2(0, scaleY));
        }

        public static Matrix2 CreateScale(Vector2 scale)
        {
            return CreateScale(scale.X, scale.Y);
        }

        public static implicit operator Matrix2(Matrix2<double> matrix)
        {
            return new Matrix2(matrix.Data);
        }
        public static explicit operator Matrix2(Matrix2<float> matrix)
        {
            double[,] data = new double[2, 2]
            {
                { matrix[0, 0], matrix[0, 1] },
                { matrix[1, 0], matrix[1, 1] }
            };

            return new Matrix2(data);
        }

        public static implicit operator Matrix2<double>(Matrix2 matrix)
        {
            return new Matrix2<double>(matrix._matrix);
        }
        public static explicit operator Matrix2<float>(Matrix2 matrix)
        {
            float[,] data = new float[2, 2]
            {
                { (float)matrix[0, 0], (float)matrix[0, 1] },
                { (float)matrix[1, 0], (float)matrix[1, 1] }
            };

            return new Matrix2<float>(data);
        }
    }
}
