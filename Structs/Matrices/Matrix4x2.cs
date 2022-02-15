using System;

namespace Zene.Structs
{
    public struct Matrix4x2 : IMatrix<double>
    {
        public Matrix4x2(Vector2 row0, Vector2 row1, Vector2 row2, Vector2 row3)
        {
            _matrix = new double[,]
            {
                { row0.X, row1.X, row2.X, row3.X },
                { row0.Y, row1.Y, row2.Y, row3.Y }
            };
        }

        public Matrix4x2(double[,] matrix)
        {
            _matrix = new double[2, 4];

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
        public Matrix4x2(double[] matrix)
        {
            _matrix = new double[2, 4];

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

        private readonly double[,] _matrix;

        public double[,] Data => _matrix;
        int IMatrix<double>.RowSize => 4;
        int IMatrix<double>.ColumnSize => 2;

        public double this[int x, int y]
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

        public Vector2 Row3
        {
            get
            {
                return new Vector2(_matrix[0, 3], _matrix[1, 3]);
            }
            set
            {
                _matrix[0, 3] = value.X;
                _matrix[1, 3] = value.Y;
            }
        }

        public Vector4 Column0
        {
            get
            {
                return new Vector4(_matrix[0, 0], _matrix[0, 1], _matrix[0, 2], _matrix[0, 3]);
            }
            set
            {
                _matrix[0, 0] = value.X;
                _matrix[0, 1] = value.Y;
                _matrix[0, 2] = value.Z;
                _matrix[0, 3] = value.W;
            }
        }

        public Vector4 Column1
        {
            get
            {
                return new Vector4(_matrix[1, 0], _matrix[1, 1], _matrix[1, 2], _matrix[1, 3]);
            }
            set
            {
                _matrix[1, 0] = value.X;
                _matrix[1, 1] = value.Y;
                _matrix[1, 2] = value.Z;
                _matrix[1, 3] = value.W;
            }
        }

        public Matrix4x2 Add(Matrix4x2 matrix)
        {
            return new Matrix4x2(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] + matrix[0, 0], /*x:0 y:1*/this[0, 1] + matrix[0, 1], /*x:0 y:2*/this[0, 2] + matrix[0, 2], /*x:0 y:3*/this[0, 3] + matrix[0, 3] },
                { /*x:1 y:0*/this[1, 0] + matrix[1, 0], /*x:1 y:1*/this[1, 1] + matrix[1, 1], /*x:1 y:2*/this[1, 2] + matrix[1, 2], /*x:1 y:3*/this[1, 3] + matrix[1, 3] }
            });
        }

        public Matrix4x2 Subtract(Matrix4x2 matrix)
        {
            return new Matrix4x2(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] - matrix[0, 0], /*x:0 y:1*/this[0, 1] - matrix[0, 1], /*x:0 y:2*/this[0, 2] - matrix[0, 2], /*x:0 y:3*/this[0, 3] - matrix[0, 3] },
                { /*x:1 y:0*/this[1, 0] - matrix[1, 0], /*x:1 y:1*/this[1, 1] - matrix[1, 1], /*x:1 y:2*/this[1, 2] - matrix[1, 2], /*x:1 y:3*/this[1, 3] - matrix[1, 3] }
            });
        }

        public Matrix4x2 Multiply(double value)
        {
            return new Matrix4x2(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] * value, /*x:0 y:1*/this[0, 1] * value, /*x:0 y:2*/this[0, 2] * value, /*x:0 y:3*/this[0, 3] * value },
                { /*x:1 y:0*/this[1, 0] * value, /*x:1 y:1*/this[1, 1] * value, /*x:1 y:2*/this[1, 2] * value, /*x:1 y:3*/this[1, 3] * value }
            });
        }

        public Matrix4x2 Multiply(Matrix2 matrix)
        {
            return new Matrix4x2(new double[,]
            {
                {
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]),
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]),
                    /*x:0 y:2*/(this[0, 2] * matrix[0, 0]) + (this[1, 2] * matrix[0, 1]),
                    /*x:0 y:2*/(this[0, 3] * matrix[0, 0]) + (this[1, 3] * matrix[0, 1])
                },
                {
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]),
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]),
                    /*x:1 y:2*/(this[0, 2] * matrix[1, 0]) + (this[1, 2] * matrix[1, 1]),
                    /*x:0 y:2*/(this[0, 3] * matrix[1, 0]) + (this[1, 3] * matrix[1, 1])
                }
            });
        }

        public Matrix4x3 Multiply(Matrix2x3 matrix)
        {
            return new Matrix4x3(new double[,]
            {
                {
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]),
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]),
                    /*x:0 y:2*/(this[0, 2] * matrix[0, 0]) + (this[1, 2] * matrix[0, 1]),
                    /*x:0 y:2*/(this[0, 3] * matrix[0, 0]) + (this[1, 3] * matrix[0, 1])
                },
                {
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]),
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]),
                    /*x:1 y:2*/(this[0, 2] * matrix[1, 0]) + (this[1, 2] * matrix[1, 1]),
                    /*x:0 y:2*/(this[0, 3] * matrix[1, 0]) + (this[1, 3] * matrix[1, 1])
                },
                {
                    /*x:2 y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]),
                    /*x:2 y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]),
                    /*x:2 y:2*/(this[0, 2] * matrix[2, 0]) + (this[1, 2] * matrix[2, 1]),
                    /*x:0 y:2*/(this[0, 3] * matrix[2, 0]) + (this[1, 3] * matrix[2, 1])
                }
            });
        }

        public Matrix4 Multiply(Matrix2x4 matrix)
        {
            return new Matrix4(new double[,]
            {
                {
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]),
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]),
                    /*x:0 y:2*/(this[0, 2] * matrix[0, 0]) + (this[1, 2] * matrix[0, 1]),
                    /*x:0 y:2*/(this[0, 2] * matrix[0, 0]) + (this[1, 3] * matrix[0, 1])
                },
                {
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]),
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]),
                    /*x:1 y:2*/(this[0, 2] * matrix[1, 0]) + (this[1, 2] * matrix[1, 1]),
                    /*x:0 y:2*/(this[0, 3] * matrix[1, 0]) + (this[1, 3] * matrix[1, 1])
                },
                {
                    /*x:2 y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]),
                    /*x:2 y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]),
                    /*x:2 y:2*/(this[0, 2] * matrix[2, 0]) + (this[1, 2] * matrix[2, 1]),
                    /*x:0 y:2*/(this[0, 3] * matrix[2, 0]) + (this[1, 3] * matrix[2, 1])
                },
                {
                    /*x:2 y:0*/(this[0, 0] * matrix[3, 0]) + (this[1, 0] * matrix[3, 1]),
                    /*x:2 y:1*/(this[0, 1] * matrix[3, 0]) + (this[1, 1] * matrix[3, 1]),
                    /*x:2 y:2*/(this[0, 2] * matrix[3, 0]) + (this[1, 2] * matrix[3, 1]),
                    /*x:0 y:2*/(this[0, 3] * matrix[3, 0]) + (this[1, 3] * matrix[3, 1])
                }
            });
        }

        public Matrix Add(IMatrix<double> matrix)
        {
            if ((matrix.RowSize != 4) || (matrix.ColumnSize != 2))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 4 rows and 2 columns.");
            }

            Matrix output = new Matrix(new double[2, 4]);

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    output[x, y] = _matrix[x, y] + matrix[x, y];
                }
            }

            return output;
        }
        public Matrix Subtract(IMatrix<double> matrix)
        {
            if ((matrix.RowSize != 4) || (matrix.ColumnSize != 2))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 4 rows and 2 columns.");
            }

            Matrix output = new Matrix(new double[2, 4]);

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 4; y++)
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
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 2 rows.");
            }

            Matrix output = new Matrix(new double[4, matrix.ColumnSize]);

            for (int x = 0; x < matrix.ColumnSize; x++)
            {
                for (int y = 0; y < 4; y++)
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

        public Matrix2x4 Transpose()
        {
            return new Matrix2x4(Column0, Column1);
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix4x2 matrix &&
                _matrix == matrix._matrix;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public ReadOnlySpan<float> GetGLData()
        {
            int w = 2;
            int h = 4;

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
[{_matrix[0, 2]}, {_matrix[1, 2]}]
[{_matrix[0, 3]}, {_matrix[1, 3]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0, 0].ToString(format)}, {_matrix[1, 0].ToString(format)}]
[{_matrix[0, 1].ToString(format)}, {_matrix[1, 1].ToString(format)}]
[{_matrix[0, 2].ToString(format)}, {_matrix[1, 2].ToString(format)}]
[{_matrix[0, 3].ToString(format)}, {_matrix[1, 3].ToString(format)}]";
        }

        public static bool operator ==(Matrix4x2 a, Matrix4x2 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Matrix4x2 a, Matrix4x2 b)
        {
            return !a.Equals(b);
        }

        public static Matrix4x2 operator +(Matrix4x2 a, Matrix4x2 b)
        {
            return a.Add(b);
        }

        public static Matrix4x2 operator -(Matrix4x2 a, Matrix4x2 b)
        {
            return a.Subtract(b);
        }

        public static Matrix4x2 operator *(Matrix4x2 a, Matrix2 b)
        {
            return a.Multiply(b);
        }

        public static Matrix4x3 operator *(Matrix4x2 a, Matrix2x3 b)
        {
            return a.Multiply(b);
        }

        public static Matrix4 operator *(Matrix4x2 a, Matrix2x4 b)
        {
            return a.Multiply(b);
        }

        public static Matrix4x2 operator *(Matrix4x2 a, double b)
        {
            return a.Multiply(b);
        }

        public static Matrix4x2 operator *(double a, Matrix4x2 b)
        {
            return b.Multiply(a);
        }

        public static Matrix operator +(Matrix4x2 a, IMatrix<double> b)
        {
            return a.Add(b);
        }
        public static Matrix operator -(Matrix4x2 a, IMatrix<double> b)
        {
            return a.Subtract(b);
        }
        public static Matrix operator *(Matrix4x2 a, IMatrix<double> b)
        {
            return a.Multiply(b);
        }

        public static Matrix4x2 CreateRotation(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4x2(new Vector2(cos, sin), new Vector2(-sin, cos), Vector2.Zero, Vector2.Zero);
        }

        public static Matrix4x2 CreateScale(double scale)
        {
            return new Matrix4x2(new Vector2(scale, 0), new Vector2(0, scale), Vector2.Zero, Vector2.Zero);
        }
        public static Matrix4x2 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix4x2(new Vector2(scaleX, 0), new Vector2(0, scaleY), Vector2.Zero, Vector2.Zero);
        }
        public static Matrix4x2 CreateScale(Vector2 scale)
        {
            return CreateScale(scale.X, scale.Y);
        }

        public static Matrix4x2 CreateTranslation(double xy)
        {
            return new Matrix4x2(
                new Vector2(1, 0),
                new Vector2(0, 1),
                Vector2.Zero,
                new Vector2(xy, xy));
        }
        public static Matrix4x2 CreateTranslation(double x, double y)
        {
            return new Matrix4x2(
                new Vector2(1, 0),
                new Vector2(0, 1),
                Vector2.Zero,
                new Vector2(x, y));
        }
        public static Matrix4x2 CreateTranslation(Vector2 xy)
        {
            return CreateTranslation(xy.X, xy.Y);
        }

        public static implicit operator Matrix4x2(Matrix4x2<double> matrix)
        {
            return new Matrix4x2(matrix.Data);
        }
        public static explicit operator Matrix4x2(Matrix4x2<float> matrix)
        {
            double[,] data = new double[2, 4]
            {
                { matrix[0, 0], matrix[0, 1], matrix[0, 2], matrix[0, 3] },
                { matrix[1, 0], matrix[1, 1], matrix[1, 2], matrix[1, 3] }
            };

            return new Matrix4x2(data);
        }

        public static implicit operator Matrix4x2<double>(Matrix4x2 matrix)
        {
            return new Matrix4x2<double>(matrix._matrix);
        }
        public static explicit operator Matrix4x2<float>(Matrix4x2 matrix)
        {
            float[,] data = new float[2, 4]
            {
                { (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2], (float)matrix[0, 3] },
                { (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2], (float)matrix[1, 3] }
            };

            return new Matrix4x2<float>(data);
        }
    }
}
