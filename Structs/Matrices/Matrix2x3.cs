using System;

namespace Zene.Structs
{
    public struct Matrix2x3 : IMatrix<double>
    {
        public Matrix2x3(Vector3 row0, Vector3 row1)
        {
            _matrix = new double[,]
            {
                { row0.X, row1.X },
                { row0.Y, row1.Y },
                { row0.Z, row1.Z }
            };
        }

        public Matrix2x3(double[,] matrix)
        {
            _matrix = new double[3, 2];

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    try
                    {
                        _matrix[x, y] = matrix[x, y];
                    }
                    catch (Exception)
                    {
                        throw new Exception("Matrix needs to have at least 2 rows and 3 columns.");
                    }
                }
            }
        }
        public Matrix2x3(double[] matrix)
        {
            _matrix = new double[3, 2];

            if (matrix.Length < 6)
            {
                throw new Exception("Matrix needs to have at least 2 rows and 3 columns.");
            }

            _matrix[0, 0] = matrix[0];
            _matrix[1, 0] = matrix[1];
            _matrix[2, 0] = matrix[2];
            _matrix[0, 1] = matrix[3];
            _matrix[1, 1] = matrix[4];
            _matrix[2, 1] = matrix[5];
        }

        private readonly double[,] _matrix;

        public double[,] Data => _matrix;
        int IMatrix<double>.RowSize => 2;
        int IMatrix<double>.ColumnSize => 3;

        public double this[int x, int y]
        {
            get
            {
                if (x >= 3 || y >= 2)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 3 x 2 range of matrix2x3.");
                }

                return _matrix[x, y];
            }
            set
            {
                if (x >= 3 || y >= 2)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 3 x 2 range of matrix2x3.");
                }

                _matrix[x, y] = value;
            }
        }

        public Vector3 Row0
        {
            get
            {
                return new Vector3(_matrix[0, 0], _matrix[1, 0], _matrix[2, 0]);
            }
            set
            {
                _matrix[0, 0] = value.X;
                _matrix[1, 0] = value.Y;
            }
        }

        public Vector3 Row1
        {
            get
            {
                return new Vector3(_matrix[0, 1], _matrix[1, 1], _matrix[2, 1]);
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

        public Matrix2x3 Add(Matrix2x3 matrix)
        {
            return new Matrix2x3(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] + matrix[0, 0], /*x:0 y:1*/this[0, 1] + matrix[0, 1] },
                { /*x:1 y:0*/this[1, 0] + matrix[1, 0], /*x:1 y:1*/this[1, 1] + matrix[1, 1] },
                { /*x:2 y:0*/this[2, 0] + matrix[2, 0], /*x:2 y:1*/this[2, 1] + matrix[2, 1] }
            });
        }

        public Matrix2x3 Subtract(Matrix2x3 matrix)
        {
            return new Matrix2x3(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] - matrix[0, 0], /*x:0 y:1*/this[0, 1] - matrix[0, 1] },
                { /*x:1 y:0*/this[1, 0] - matrix[1, 0], /*x:1 y:1*/this[1, 1] - matrix[1, 1] },
                { /*x:2 y:0*/this[2, 0] - matrix[2, 0], /*x:2 y:1*/this[2, 1] - matrix[2, 1] }
            });
        }

        public Matrix2x3 Multiply(double value)
        {
            return new Matrix2x3(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] * value, /*x:0 y:1*/this[0, 1] * value },
                { /*x:1 y:0*/this[1, 0] * value, /*x:1 y:1*/this[1, 1] * value },
                { /*x:2 y:0*/this[2, 0] * value, /*x:2 y:1*/this[2, 1] * value }
            });
        }

        public Matrix2x3 Multiply(Matrix3 matrix)
        {
            return new Matrix2x3(new double[,]
            {
                {
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]) + (this[2, 0] * matrix[0, 2]), 
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) + (this[2, 1] * matrix[0, 2])
                },
                {
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]) + (this[2, 0] * matrix[1, 2]), 
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) + (this[2, 1] * matrix[1, 2])
                },
                {
                    /*x:2 y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]) + (this[2, 0] * matrix[2, 2]), 
                    /*x:2 y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]) + (this[2, 1] * matrix[2, 2])
                }
            });
        }

        public Matrix2 Multiply(Matrix3x2 matrix)
        {
            return new Matrix2(new double[,]
            {
                {
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]) + (this[2, 0] * matrix[0, 2]), 
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) + (this[2, 1] * matrix[0, 2])
                },
                {
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]) + (this[2, 0] * matrix[1, 2]), 
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) + (this[2, 1] * matrix[1, 2])
                },
            });
        }

        public Matrix2x4 Multiply(Matrix3x4 matrix)
        {
            return new Matrix2x4(new double[,]
            {
                {
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]) + (this[2, 0] * matrix[0, 2]), 
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) + (this[2, 1] * matrix[0, 2])
                },
                {
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]) + (this[2, 0] * matrix[1, 2]), 
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) + (this[2, 1] * matrix[1, 2])
                },
                {
                    /*x:2 y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]) + (this[2, 0] * matrix[2, 2]), 
                    /*x:2 y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]) + (this[2, 1] * matrix[2, 2])
                },
                {
                    /*x:3 y:0*/(this[0, 0] * matrix[3, 0]) + (this[1, 0] * matrix[3, 1]) + (this[2, 0] * matrix[3, 2]), 
                    /*x:3 y:1*/(this[0, 1] * matrix[3, 0]) + (this[1, 1] * matrix[3, 1]) + (this[2, 1] * matrix[3, 2])
                }
            });
        }

        public Matrix Add(IMatrix<double> matrix)
        {
            if ((matrix.RowSize != 2) || (matrix.ColumnSize != 3))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 2 rows and 3 columns.");
            }

            Matrix output = new Matrix(new double[3, 2]);

            for (int x = 0; x < 3; x++)
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
            if ((matrix.RowSize != 2) || (matrix.ColumnSize != 3))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 2 rows and 3 columns.");
            }

            Matrix output = new Matrix(new double[3, 2]);

            for (int x = 0; x < 3; x++)
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
            if (matrix.RowSize != 3)
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have {3} rows.");
            }

            Matrix output = new Matrix(new double[2, matrix.ColumnSize]);

            for (int x = 0; x < matrix.ColumnSize; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    double value = 0;

                    for (int m = 0; m < 3; m++)
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

        public Matrix3x2 Transpose()
        {
            return new Matrix3x2(new double[,]
            {
                { /*x:0 y:0*/this[0, 0], /*x:0 y:1*/this[1, 0], /*x:0 y:2*/this[2, 0] },
                { /*x:1 y:0*/this[0, 1], /*x:1 y:1*/this[1, 1], /*x:1 y:2*/this[2, 1] }
            });
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix2x3 matrix &&
                _matrix == matrix._matrix;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public ReadOnlySpan<float> GetGLData()
        {
            int w = 3;
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
            return $@"[{_matrix[0, 0]}, {_matrix[1, 0]}, {_matrix[2, 0]}]
[{_matrix[0, 1]}, {_matrix[1, 1]}, {_matrix[2, 1]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0, 0].ToString(format)}, {_matrix[1, 0].ToString(format)}, {_matrix[2, 0].ToString(format)}]
[{_matrix[0, 1].ToString(format)}, {_matrix[1, 1].ToString(format)}, {_matrix[2, 1].ToString(format)}]";
        }

        public static bool operator ==(Matrix2x3 a, Matrix2x3 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Matrix2x3 a, Matrix2x3 b)
        {
            return !a.Equals(b);
        }

        public static Matrix2x3 operator +(Matrix2x3 a, Matrix2x3 b)
        {
            return a.Add(b);
        }

        public static Matrix2x3 operator -(Matrix2x3 a, Matrix2x3 b)
        {
            return a.Subtract(b);
        }

        public static Matrix2x3 operator *(Matrix2x3 a, Matrix3 b)
        {
            return a.Multiply(b);
        }

        public static Matrix2 operator *(Matrix2x3 a, Matrix3x2 b)
        {
            return a.Multiply(b);
        }

        public static Matrix2x4 operator *(Matrix2x3 a, Matrix3x4 b)
        {
            return a.Multiply(b);
        }

        public static Matrix2x3 operator *(Matrix2x3 a, double b)
        {
            return a.Multiply(b);
        }

        public static Matrix2x3 operator *(double a, Matrix2x3 b)
        {
            return b.Multiply(a);
        }

        public static Matrix operator +(Matrix2x3 a, IMatrix<double> b)
        {
            return a.Add(b);
        }
        public static Matrix operator -(Matrix2x3 a, IMatrix<double> b)
        {
            return a.Subtract(b);
        }
        public static Matrix operator *(Matrix2x3 a, IMatrix<double> b)
        {
            return a.Multiply(b);
        }

        public static Matrix2x3 CreateRotation(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix2x3(new Vector3(cos, sin, 0), new Vector3(-sin, cos, 0));
        }

        public static Matrix2x3 CreateScale(double scale)
        {
            return new Matrix2x3(new Vector3(scale, 0, 0), new Vector3(0, scale, 0));
        }
        public static Matrix2x3 CreateScale(double scaleX, double scaleY)
        {
            return new Matrix2x3(new Vector3(scaleX, 0, 0), new Vector3(0, scaleY, 0));
        }
        public static Matrix2x3 CreateScale(Vector2 scale)
        {
            return CreateScale(scale.X, scale.Y);
        }

        public static Matrix2x3 CreateTranslation(double xy)
        {
            return new Matrix2x3(
                new Vector3(1, 0, xy),
                new Vector3(0, 1, xy));
        }
        public static Matrix2x3 CreateTranslation(double x, double y)
        {
            return new Matrix2x3(
                new Vector3(1, 0, x),
                new Vector3(0, 1, y));
        }
        public static Matrix2x3 CreateTranslation(Vector3 xy)
        {
            return new Matrix2x3(
                new Vector3(1, 0, xy.X),
                new Vector3(0, 1, xy.Y));
        }

        public static implicit operator Matrix2x3(Matrix2x3<double> matrix)
        {
            return new Matrix2x3(matrix.Data);
        }
        public static explicit operator Matrix2x3(Matrix2x3<float> matrix)
        {
            double[,] data = new double[3, 2]
            {
                { matrix[0, 0], matrix[0, 1] },
                { matrix[1, 0], matrix[1, 1] },
                { matrix[2, 0], matrix[2, 1] }
            };

            return new Matrix2x3(data);
        }

        public static implicit operator Matrix2x3<double>(Matrix2x3 matrix)
        {
            return new Matrix2x3<double>(matrix._matrix);
        }
        public static explicit operator Matrix2x3<float>(Matrix2x3 matrix)
        {
            float[,] data = new float[3, 2]
            {
                { (float)matrix[0, 0], (float)matrix[0, 1] },
                { (float)matrix[1, 0], (float)matrix[1, 1] },
                { (float)matrix[2, 0], (float)matrix[2, 1] }
            };

            return new Matrix2x3<float>(data);
        }
    }
}
