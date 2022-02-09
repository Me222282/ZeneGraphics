using System;

namespace Zene.Structs
{
    public struct Matrix2x4 : IMatrix<double>
    {
        public Matrix2x4(Vector4 row0, Vector4 row1)
        {
            _matrix = new double[,]
            {
                { row0.X, row1.X },
                { row0.Y, row1.Y },
                { row0.Z, row1.Z },
                { row0.W, row1.W }
            };
        }

        public Matrix2x4(double[,] matrix)
        {
            _matrix = new double[4, 2];

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    try
                    {
                        _matrix[x, y] = matrix[x, y];
                    }
                    catch (Exception)
                    {
                        throw new Exception("Matrix needs to have at least 2 rows and 4 columns.");
                    }
                }
            }
        }
        public Matrix2x4(double[] matrix)
        {
            _matrix = new double[4, 2];

            if (matrix.Length < 8)
            {
                throw new Exception("Matrix needs to have at least 2 rows and 4 columns.");
            }

            _matrix[0, 0] = matrix[0];
            _matrix[1, 0] = matrix[1];
            _matrix[2, 0] = matrix[2];
            _matrix[3, 0] = matrix[3];
            _matrix[0, 1] = matrix[4];
            _matrix[1, 1] = matrix[5];
            _matrix[2, 1] = matrix[6];
            _matrix[3, 0] = matrix[7];
        }

        private readonly double[,] _matrix;

        public double[,] Data => _matrix;
        int IMatrix<double>.RowSize => 2;
        int IMatrix<double>.ColumnSize => 4;

        public double this[int x, int y]
        {
            get
            {
                if (x >= 4 || y >= 2)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 4 x 2 range of matrix2x4.");
                }

                return _matrix[x, y];
            }
            set
            {
                if (x >= 4 || y >= 2)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 4 x 2 range of matrix2x4.");
                }

                _matrix[x, y] = value;
            }
        }

        public Vector4 Row0
        {
            get
            {
                return new Vector4(_matrix[0, 0], _matrix[1, 0], _matrix[2, 0], _matrix[3, 0]);
            }
            set
            {
                _matrix[0, 0] = value.X;
                _matrix[1, 0] = value.Y;
                _matrix[2, 0] = value.Z;
                _matrix[3, 0] = value.W;
            }
        }

        public Vector4 Row1
        {
            get
            {
                return new Vector4(_matrix[0, 1], _matrix[1, 1], _matrix[2, 1], _matrix[3, 1]);
            }
            set
            {
                _matrix[0, 1] = value.X;
                _matrix[1, 1] = value.Y;
                _matrix[2, 1] = value.Z;
                _matrix[3, 1] = value.W;
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

        public Vector2 Column2
        {
            get
            {
                return new Vector2(_matrix[2, 0], _matrix[2, 1]);
            }
            set
            {
                _matrix[2, 0] = value.X;
                _matrix[2, 1] = value.Y;
            }
        }

        public Vector2 Column3
        {
            get
            {
                return new Vector2(_matrix[3, 0], _matrix[3, 1]);
            }
            set
            {
                _matrix[3, 0] = value.X;
                _matrix[3, 1] = value.Y;
            }
        }

        public Matrix2x4 Add(Matrix2x4 matrix)
        {
            return new Matrix2x4(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] + matrix[0, 0], /*x:0 y:1*/this[0, 1] + matrix[0, 1] },
                { /*x:1 y:0*/this[1, 0] + matrix[1, 0], /*x:1 y:1*/this[1, 1] + matrix[1, 1] },
                { /*x:2 y:0*/this[2, 0] + matrix[2, 0], /*x:2 y:1*/this[2, 1] + matrix[2, 1] },
                { /*x:3 y:0*/this[3, 0] + matrix[3, 0], /*x:3 y:1*/this[3, 1] + matrix[3, 1] }
            });
        }

        public Matrix2x4 Subtract(Matrix2x4 matrix)
        {
            return new Matrix2x4(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] - matrix[0, 0], /*x:0 y:1*/this[0, 1] - matrix[0, 1] },
                { /*x:1 y:0*/this[1, 0] - matrix[1, 0], /*x:1 y:1*/this[1, 1] - matrix[1, 1] },
                { /*x:2 y:0*/this[2, 0] - matrix[2, 0], /*x:2 y:1*/this[2, 1] - matrix[2, 1] },
                { /*x:3 y:0*/this[3, 0] - matrix[3, 0], /*x:3 y:1*/this[3, 1] - matrix[3, 1] }
            });
        }

        public Matrix2x4 Multiply(double value)
        {
            return new Matrix2x4(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] * value, /*x:0 y:1*/this[0, 1] * value },
                { /*x:1 y:0*/this[1, 0] * value, /*x:1 y:1*/this[1, 1] * value },
                { /*x:2 y:0*/this[2, 0] * value, /*x:2 y:1*/this[2, 1] * value },
                { /*x:3 y:0*/this[3, 0] * value, /*x:3 y:1*/this[3, 1] * value }
            });
        }

        public Matrix2 Multiply(Matrix4x2 matrix)
        {
            return new Matrix2(new double[,]
            {
                {
                    /*X:0 Y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]) + (this[2, 0] * matrix[0, 2]) + (this[3, 0] * matrix[0, 3]),
                    /*X:0 Y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) + (this[2, 1] * matrix[0, 2]) + (this[3, 1] * matrix[0, 3])
                },
                {
                    /*X:1 Y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]) + (this[2, 0] * matrix[1, 2]) + (this[3, 0] * matrix[1, 3]),
                    /*X:1 Y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) + (this[2, 1] * matrix[1, 2]) + (this[3, 1] * matrix[1, 3])
                }
            });
        }

        public Matrix2x3 Multiply(Matrix4x3 matrix)
        {
            return new Matrix2x3(new double[,]
            {
                {
                    /*X:0 Y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]) + (this[2, 0] * matrix[0, 2]) + (this[3, 0] * matrix[0, 3]),
                    /*X:0 Y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) + (this[2, 1] * matrix[0, 2]) + (this[3, 1] * matrix[0, 3])
                },
                {
                    /*X:1 Y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]) + (this[2, 0] * matrix[1, 2]) + (this[3, 0] * matrix[1, 3]),
                    /*X:1 Y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) + (this[2, 1] * matrix[1, 2]) + (this[3, 1] * matrix[1, 3])
                },
                {
                    /*X:2 Y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]) + (this[2, 0] * matrix[2, 2]) + (this[3, 0] * matrix[2, 3]),
                    /*X:2 Y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]) + (this[2, 1] * matrix[2, 2]) + (this[3, 1] * matrix[2, 3])
                }
            });
        }

        public Matrix2x4 Multiply(Matrix4 matrix)
        {
            return new Matrix2x4(new double[,]
            {
                {
                    /*X:0 Y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]) + (this[2, 0] * matrix[0, 2]) + (this[3, 0] * matrix[0, 3]),
                    /*X:0 Y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) + (this[2, 1] * matrix[0, 2]) + (this[3, 1] * matrix[0, 3])
                },
                {
                    /*X:1 Y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]) + (this[2, 0] * matrix[1, 2]) + (this[3, 0] * matrix[1, 3]),
                    /*X:1 Y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) + (this[2, 1] * matrix[1, 2]) + (this[3, 1] * matrix[1, 3])
                },
                {
                    /*X:2 Y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]) + (this[2, 0] * matrix[2, 2]) + (this[3, 0] * matrix[2, 3]),
                    /*X:2 Y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]) + (this[2, 1] * matrix[2, 2]) + (this[3, 1] * matrix[2, 3])
                },
                {
                    /*X:3 Y:0*/(this[0, 0] * matrix[3, 0]) + (this[1, 0] * matrix[3, 1]) + (this[2, 0] * matrix[3, 2]) + (this[3, 0] * matrix[3, 3]),
                    /*X:3 Y:1*/(this[0, 1] * matrix[3, 0]) + (this[1, 1] * matrix[3, 1]) + (this[2, 1] * matrix[3, 2]) + (this[3, 1] * matrix[3, 3])
                }
            });
        }

        public Matrix Add(IMatrix<double> matrix)
        {
            if ((matrix.RowSize != 2) || (matrix.ColumnSize != 4))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 2 rows and 4 columns.");
            }

            Matrix output = new Matrix(new double[4, 2]);

            for (int x = 0; x < 4; x++)
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
            if ((matrix.RowSize != 2) || (matrix.ColumnSize != 4))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 2 rows and 4 columns.");
            }

            Matrix output = new Matrix(new double[4, 2]);

            for (int x = 0; x < 4; x++)
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
            if (matrix.RowSize != 4)
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 4 rows.");
            }

            Matrix output = new Matrix(new double[2, matrix.ColumnSize]);

            for (int x = 0; x < matrix.ColumnSize; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    double value = 0;

                    for (int m = 0; m < 4; m++)
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

        public Matrix4x2 Transpose()
        {
            return new Matrix4x2(Column0, Column1, Column2, Column3);
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix2x4 matrix &&
                _matrix == matrix._matrix;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public ReadOnlySpan<float> GetGLData()
        {
            int w = 4;
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
            return $@"[{_matrix[0, 0]}, {_matrix[1, 0]}, {_matrix[2, 0]}, {_matrix[3, 0]}]
[{_matrix[0, 1]}, {_matrix[1, 1]}, {_matrix[2, 1]}, {_matrix[3, 1]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0, 0].ToString(format)}, {_matrix[1, 0].ToString(format)}, {_matrix[2, 0].ToString(format)}, {_matrix[3, 0].ToString(format)}]
[{_matrix[0, 1].ToString(format)}, {_matrix[1, 1].ToString(format)}, {_matrix[2, 1].ToString(format)}, {_matrix[3, 1].ToString(format)}]";
        }

        public static bool operator ==(Matrix2x4 a, Matrix2x4 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Matrix2x4 a, Matrix2x4 b)
        {
            return !a.Equals(b);
        }

        public static Matrix2x4 operator +(Matrix2x4 a, Matrix2x4 b)
        {
            return a.Add(b);
        }

        public static Matrix2x4 operator -(Matrix2x4 a, Matrix2x4 b)
        {
            return a.Subtract(b);
        }

        public static Matrix2 operator *(Matrix2x4 a, Matrix4x2 b)
        {
            return a.Multiply(b);
        }

        public static Matrix2x3 operator *(Matrix2x4 a, Matrix4x3 b)
        {
            return a.Multiply(b);
        }

        public static Matrix2x4 operator *(Matrix2x4 a, Matrix4 b)
        {
            return a.Multiply(b);
        }

        public static Matrix2x4 operator *(Matrix2x4 a, double b)
        {
            return a.Multiply(b);
        }

        public static Matrix2x4 operator *(double a, Matrix2x4 b)
        {
            return b.Multiply(a);
        }

        public static Matrix operator +(Matrix2x4 a, IMatrix<double> b)
        {
            return a.Add(b);
        }
        public static Matrix operator -(Matrix2x4 a, IMatrix<double> b)
        {
            return a.Subtract(b);
        }
        public static Matrix operator *(Matrix2x4 a, IMatrix<double> b)
        {
            return a.Multiply(b);
        }

        public static Matrix2x4 CreateRotation(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix2x4(new Vector4(cos, sin, 0, 0), new Vector4(-sin, cos, 0, 0));
        }

        public static Matrix2x4 CreateScale(double scale)
        {
            return new Matrix2x4(new Vector4(scale, 0, 0, 0), new Vector4(0, scale, 0, 0));
        }
        public static Matrix2x4 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix2x4(new Vector4(scaleX, 0, 0, 0), new Vector4(0, scaleY, 0, 0));
        }
        public static Matrix2x4 CreateScale(Vector2 scale)
        {
            return CreateScale(scale.X, scale.Y);
        }

        public static implicit operator Matrix2x4(Matrix2x4<double> matrix)
        {
            return new Matrix2x4(matrix.Data);
        }
        public static explicit operator Matrix2x4(Matrix2x4<float> matrix)
        {
            double[,] data = new double[4, 2]
            {
                { matrix[0, 0], matrix[0, 1] },
                { matrix[1, 0], matrix[1, 1] },
                { matrix[2, 0], matrix[2, 1] },
                { matrix[3, 0], matrix[3, 1] }
            };

            return new Matrix2x4(data);
        }

        public static implicit operator Matrix2x4<double>(Matrix2x4 matrix)
        {
            return new Matrix2x4<double>(matrix._matrix);
        }
        public static explicit operator Matrix2x4<float>(Matrix2x4 matrix)
        {
            float[,] data = new float[4, 2]
            {
                { (float)matrix[0, 0], (float)matrix[0, 1] },
                { (float)matrix[1, 0], (float)matrix[1, 1] },
                { (float)matrix[2, 0], (float)matrix[2, 1] },
                { (float)matrix[3, 0], (float)matrix[3, 1] }
            };

            return new Matrix2x4<float>(data);
        }
    }
}
