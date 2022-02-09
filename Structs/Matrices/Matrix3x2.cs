using System;

namespace Zene.Structs
{
    public struct Matrix3x2 : IMatrix<double>
    {
        public Matrix3x2(Vector2 row0, Vector2 row1, Vector2 row2)
        {
            _matrix = new double[,]
            {
                { row0.X, row1.X, row2.X },
                { row0.Y, row1.Y, row2.Y }
            };
        }

        public Matrix3x2(double[,] matrix)
        {
            _matrix = new double[2, 3];

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 3; y++)
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
        public Matrix3x2(double[] matrix)
        {
            _matrix = new double[2, 3];

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

        private readonly double[,] _matrix;

        public double[,] Data => _matrix;
        int IMatrix<double>.RowSize => 3;
        int IMatrix<double>.ColumnSize => 2;

        public double this[int x, int y]
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

        public Vector2 Row2
        {
            get
            {
                return new Vector2(_matrix[0, 2], _matrix[1, 2]);
            }
            set
            {
                _matrix[0, 2] = value.X;
                _matrix[1, 2] = value.Y;
            }
        }

        public Vector3 Column0
        {
            get
            {
                return new Vector3(_matrix[0, 0], _matrix[0, 1], _matrix[0, 2]);
            }
            set
            {
                _matrix[0, 0] = value.X;
                _matrix[0, 1] = value.Y;
                _matrix[0, 2] = value.Z;
            }
        }

        public Vector3 Column1
        {
            get
            {
                return new Vector3(_matrix[1, 0], _matrix[1, 1], _matrix[1, 2]);
            }
            set
            {
                _matrix[1, 0] = value.X;
                _matrix[1, 1] = value.Y;
                _matrix[1, 2] = value.Z;
            }
        }

        public Matrix3x2 Add(Matrix3x2 matrix)
        {
            return new Matrix3x2(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] + matrix[0, 0], /*x:0 y:1*/this[0, 1] + matrix[0, 1], /*x:0 y:2*/this[0, 2] + matrix[0, 2] },
                { /*x:1 y:0*/this[1, 0] + matrix[1, 0], /*x:1 y:1*/this[1, 1] + matrix[1, 1], /*x:1 y:2*/this[1, 2] + matrix[1, 2] }
            });
        }

        public Matrix3x2 Subtract(Matrix3x2 matrix)
        {
            return new Matrix3x2(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] - matrix[0, 0], /*x:0 y:1*/this[0, 1] - matrix[0, 1], /*x:0 y:2*/this[0, 2] - matrix[0, 2] },
                { /*x:1 y:0*/this[1, 0] - matrix[1, 0], /*x:1 y:1*/this[1, 1] - matrix[1, 1], /*x:1 y:2*/this[1, 2] - matrix[1, 2] }
            });
        }

        public Matrix3x2 Multiply(double value)
        {
            return new Matrix3x2(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] * value, /*x:0 y:1*/this[0, 1] * value, /*x:0 y:2*/this[0, 2] * value },
                { /*x:1 y:0*/this[1, 0] * value, /*x:1 y:1*/this[1, 1] * value, /*x:1 y:2*/this[1, 2] * value }
            });
        }

        public Matrix3x2 Multiply(Matrix2 matrix)
        {
            return new Matrix3x2(new double[,]
            {
                {
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]),
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]),
                    /*x:0 y:2*/(this[0, 2] * matrix[0, 0]) + (this[1, 2] * matrix[0, 1])
                },
                {
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]),
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]),
                    /*x:1 y:2*/(this[0, 2] * matrix[1, 0]) + (this[1, 2] * matrix[1, 1])
                }
            });
        }

        public Matrix3 Multiply(Matrix2x3 matrix)
        {
            return new Matrix3(new double[,]
            {
                {
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]),
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]),
                    /*x:0 y:2*/(this[0, 2] * matrix[0, 0]) + (this[1, 2] * matrix[0, 1])
                },
                {
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]),
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]),
                    /*x:1 y:2*/(this[0, 2] * matrix[1, 0]) + (this[1, 2] * matrix[1, 1])
                },
                {
                    /*x:2 y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]),
                    /*x:2 y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]),
                    /*x:2 y:2*/(this[0, 2] * matrix[2, 0]) + (this[1, 2] * matrix[2, 1])
                }
            });
        }

        public Matrix3x4 Multiply(Matrix2x4 matrix)
        {
            return new Matrix3x4(new double[,]
            {
                {
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]),
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]),
                    /*x:0 y:2*/(this[0, 2] * matrix[0, 0]) + (this[1, 2] * matrix[0, 1])
                },
                {
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]),
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]),
                    /*x:1 y:2*/(this[0, 2] * matrix[1, 0]) + (this[1, 2] * matrix[1, 1])
                },
                {
                    /*x:2 y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]),
                    /*x:2 y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]),
                    /*x:2 y:2*/(this[0, 2] * matrix[2, 0]) + (this[1, 2] * matrix[2, 1])
                },
                {
                    /*x:2 y:0*/(this[0, 0] * matrix[3, 0]) + (this[1, 0] * matrix[3, 1]),
                    /*x:2 y:1*/(this[0, 1] * matrix[3, 0]) + (this[1, 1] * matrix[3, 1]),
                    /*x:2 y:2*/(this[0, 2] * matrix[3, 0]) + (this[1, 2] * matrix[3, 1])
                }
            });
        }

        public Matrix Add(IMatrix<double> matrix)
        {
            if ((matrix.RowSize != 3) || (matrix.ColumnSize != 2))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 3 rows and 2 columns.");
            }

            Matrix output = new Matrix(new double[2, 3]);

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    output[x, y] = _matrix[x, y] + matrix[x, y];
                }
            }

            return output;
        }
        public Matrix Subtract(IMatrix<double> matrix)
        {
            if ((matrix.RowSize != 3) || (matrix.ColumnSize != 2))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 3 rows and 2 columns.");
            }

            Matrix output = new Matrix(new double[2, 3]);

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    output[x, y] = _matrix[x, y] - matrix[x, y];
                }
            }

            return output;
        }
        public Matrix Multiply(IMatrix<double> matrix)
        {
            if (matrix.RowSize != 3)
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 2 rows.");
            }

            Matrix output = new Matrix(new double[3, matrix.ColumnSize]);

            for (int x = 0; x < matrix.ColumnSize; x++)
            {
                for (int y = 0; y < 3; y++)
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

        public double Trace()
        {
            return this[0, 0] + this[1, 1];
        }

        public Matrix2x3 Transpose()
        {
            return new Matrix2x3(new double[,]
            {
                { /*x:0 y:0*/this[0, 0], /*x:0 y:1*/this[1, 0] },
                { /*x:1 y:0*/this[0, 1], /*x:1 y:1*/this[1, 1] },
                { /*x:2 y:0*/this[0, 2], /*x:2 y:1*/this[1, 2] },
            });
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix3x2 matrix &&
                _matrix == matrix._matrix;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public ReadOnlySpan<float> GetGLData()
        {
            int w = 2;
            int h = 3;

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
[{_matrix[0, 1]}, {_matrix[1, 1]}]
[{_matrix[0, 2]}, {_matrix[1, 2]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0, 0].ToString(format)}, {_matrix[1, 0].ToString(format)}]
[{_matrix[0, 1].ToString(format)}, {_matrix[1, 1].ToString(format)}]
[{_matrix[0, 2].ToString(format)}, {_matrix[1, 2].ToString(format)}]";
        }

        public static bool operator ==(Matrix3x2 a, Matrix3x2 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Matrix3x2 a, Matrix3x2 b)
        {
            return !a.Equals(b);
        }

        public static Matrix3x2 operator +(Matrix3x2 a, Matrix3x2 b)
        {
            return a.Add(b);
        }

        public static Matrix3x2 operator -(Matrix3x2 a, Matrix3x2 b)
        {
            return a.Subtract(b);
        }

        public static Matrix3 operator *(Matrix3x2 a, Matrix2x3 b)
        {
            return a.Multiply(b);
        }

        public static Matrix3x2 operator *(Matrix3x2 a, Matrix2 b)
        {
            return a.Multiply(b);
        }

        public static Matrix3x2 operator *(Matrix3x2 a, double b)
        {
            return a.Multiply(b);
        }

        public static Matrix3x2 operator *(double a, Matrix3x2 b)
        {
            return b.Multiply(a);
        }

        public static Matrix operator +(Matrix3x2 a, IMatrix<double> b)
        {
            return a.Add(b);
        }
        public static Matrix operator -(Matrix3x2 a, IMatrix<double> b)
        {
            return a.Subtract(b);
        }
        public static Matrix operator *(Matrix3x2 a, IMatrix<double> b)
        {
            return a.Multiply(b);
        }

        public static Matrix3x2 CreateRotation(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix3x2(new Vector2(cos, sin), new Vector2(-sin, cos), Vector2.Zero);
        }

        public static Matrix3x2 CreateScale(double scale)
        {
            return new Matrix3x2(new Vector2(scale, 0), new Vector2(0, scale), Vector2.Zero);
        }
        public static Matrix3x2 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix3x2(new Vector2(scaleX, 0), new Vector2(0, scaleY), Vector2.Zero);
        }
        public static Matrix3x2 CreateScale(Vector2 scale)
        {
            return CreateScale(scale.X, scale.Y);
        }

        public static Matrix3x2 CreateTranslation(double xy)
        {
            return new Matrix3x2(
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(xy));
        }
        public static Matrix3x2 CreateTranslation(double x, double y)
        {
            return new Matrix3x2(
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(x, y));
        }
        public static Matrix3x2 CreateTranslation(Vector2 xy)
        {
            return new Matrix3x2(
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(xy.X, xy.Y));
        }

        public static implicit operator Matrix3x2(Matrix3x2<double> matrix)
        {
            return new Matrix3x2(matrix.Data);
        }
        public static explicit operator Matrix3x2(Matrix3x2<float> matrix)
        {
            double[,] data = new double[2, 3]
            {
                { matrix[0, 0], matrix[0, 1], matrix[0, 2] },
                { matrix[1, 0], matrix[1, 1], matrix[1, 2] }
            };

            return new Matrix3x2(data);
        }

        public static implicit operator Matrix3x2<double>(Matrix3x2 matrix)
        {
            return new Matrix3x2<double>(matrix._matrix);
        }
        public static explicit operator Matrix3x2<float>(Matrix3x2 matrix)
        {
            float[,] data = new float[2, 3]
            {
                { (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2] },
                { (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2] }
            };

            return new Matrix3x2<float>(data);
        }
    }
}
