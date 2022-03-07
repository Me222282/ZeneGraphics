using System;

namespace Zene.Structs
{
    public struct Matrix4 : IMatrix<double>
    {
        public Matrix4(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
        {
            _matrix = new double[,]
            {
                { row0.X, row1.X, row2.X, row3.X },
                { row0.Y, row1.Y, row2.Y, row3.Y },
                { row0.Z, row1.Z, row2.Z, row3.Z },
                { row0.W, row1.W, row2.W, row3.W }
            };
        }

        public Matrix4(double[,] matrix)
        {
            _matrix = new double[4, 4];

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    try
                    {
                        _matrix[x, y] = matrix[x, y];
                    }
                    catch (Exception)
                    {
                        throw new Exception("Matrix needs to have at least 4 rows and 4 columns.");
                    }
                }
            }
        }
        public Matrix4(double[] matrix)
        {
            _matrix = new double[4, 4];

            if (matrix.Length < 16)
            {
                throw new Exception("Matrix needs to have at least 4 rows and 4 columns.");
            }

            _matrix[0, 0] = matrix[0];
            _matrix[1, 0] = matrix[1];
            _matrix[2, 0] = matrix[2];
            _matrix[3, 0] = matrix[3];

            _matrix[0, 1] = matrix[4];
            _matrix[1, 1] = matrix[5];
            _matrix[2, 1] = matrix[6];
            _matrix[3, 1] = matrix[7];

            _matrix[0, 2] = matrix[8];
            _matrix[1, 2] = matrix[9];
            _matrix[2, 2] = matrix[10];
            _matrix[3, 2] = matrix[11];

            _matrix[0, 3] = matrix[12];
            _matrix[1, 3] = matrix[13];
            _matrix[2, 3] = matrix[14];
            _matrix[3, 3] = matrix[15];
        }

        internal readonly double[,] _matrix;

        public double[,] Data => _matrix;
        int IMatrix<double>.RowSize => 4;
        int IMatrix<double>.ColumnSize => 4;

        public double this[int x, int y]
        {
            get
            {
                if (x >= 4 || y >= 4)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 4 x 4 range of matrix4.");
                }

                return _matrix[x, y];
            }
            set
            {
                if (x >= 4 || y >= 4)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 4 x 4 range of matrix4.");
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

        public Vector4 Row2
        {
            get
            {
                return new Vector4(_matrix[0, 2], _matrix[1, 2], _matrix[2, 2], _matrix[3, 2]);
            }
            set
            {
                _matrix[0, 2] = value.X;
                _matrix[1, 2] = value.Y;
                _matrix[2, 2] = value.Z;
                _matrix[3, 2] = value.W;
            }
        }

        public Vector4 Row3
        {
            get
            {
                return new Vector4(_matrix[0, 3], _matrix[1, 3], _matrix[2, 3], _matrix[3, 3]);
            }
            set
            {
                _matrix[0, 3] = value.X;
                _matrix[1, 3] = value.Y;
                _matrix[2, 3] = value.Z;
                _matrix[3, 3] = value.W;
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

        public Vector4 Column2
        {
            get
            {
                return new Vector4(_matrix[2, 0], _matrix[2, 1], _matrix[2, 2], _matrix[2, 3]);
            }
            set
            {
                _matrix[2, 0] = value.X;
                _matrix[2, 1] = value.Y;
                _matrix[2, 2] = value.Z;
                _matrix[2, 3] = value.W;
            }
        }

        public Vector4 Column3
        {
            get
            {
                return new Vector4(_matrix[3, 0], _matrix[3, 1], _matrix[3, 2], _matrix[3, 3]);
            }
            set
            {
                _matrix[3, 0] = value.X;
                _matrix[3, 1] = value.Y;
                _matrix[3, 2] = value.Z;
                _matrix[3, 3] = value.W;
            }
        }

        public Matrix4 Add(Matrix4 matrix)
        {
            return new Matrix4(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] + matrix[0, 0], /*x:0 y:1*/this[0, 1] + matrix[0, 1], /*x:0 y:2*/this[0, 2] + matrix[0, 2], /*x:0 y:3*/this[0, 3] + matrix[0, 3] },
                { /*x:1 y:0*/this[1, 0] + matrix[1, 0], /*x:1 y:1*/this[1, 1] + matrix[1, 1], /*x:1 y:2*/this[1, 2] + matrix[1, 2], /*x:1 y:3*/this[1, 3] + matrix[1, 3] },
                { /*x:2 y:0*/this[2, 0] + matrix[2, 0], /*x:2 y:1*/this[2, 1] + matrix[2, 1], /*x:2 y:2*/this[2, 2] + matrix[2, 2], /*x:2 y:3*/this[2, 3] + matrix[2, 3] },
                { /*x:3 y:0*/this[3, 0] + matrix[3, 0], /*x:3 y:1*/this[3, 1] + matrix[3, 1], /*x:3 y:2*/this[3, 2] + matrix[3, 2], /*x:3 y:3*/this[3, 3] + matrix[3, 3] }
            });
        }

        public Matrix4 Subtract(Matrix4 matrix)
        {
            return new Matrix4(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] - matrix[0, 0], /*x:0 y:1*/this[0, 1] - matrix[0, 1], /*x:0 y:2*/this[0, 2] - matrix[0, 2], /*x:0 y:3*/this[0, 3] - matrix[0, 3] },
                { /*x:1 y:0*/this[1, 0] - matrix[1, 0], /*x:1 y:1*/this[1, 1] - matrix[1, 1], /*x:1 y:2*/this[1, 2] - matrix[1, 2], /*x:1 y:3*/this[1, 3] - matrix[1, 3] },
                { /*x:2 y:0*/this[2, 0] - matrix[2, 0], /*x:2 y:1*/this[2, 1] - matrix[2, 1], /*x:2 y:2*/this[2, 2] - matrix[2, 2], /*x:2 y:3*/this[2, 3] - matrix[2, 3] },
                { /*x:3 y:0*/this[3, 0] - matrix[3, 0], /*x:3 y:1*/this[3, 1] - matrix[3, 1], /*x:3 y:2*/this[3, 2] - matrix[3, 2], /*x:3 y:3*/this[3, 3] - matrix[3, 3] }
            });
        }

        public Matrix4 Multiply(double value)
        {
            return new Matrix4(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] * value, /*x:0 y:1*/this[0, 1] * value, /*x:0 y:2*/this[0, 2] * value, /*x:0 y:3*/this[0, 3] * value },
                { /*x:1 y:0*/this[1, 0] * value, /*x:1 y:1*/this[1, 1] * value, /*x:1 y:2*/this[1, 2] * value, /*x:1 y:3*/this[1, 3] * value },
                { /*x:2 y:0*/this[2, 0] * value, /*x:2 y:1*/this[2, 1] * value, /*x:2 y:2*/this[2, 2] * value, /*x:2 y:3*/this[2, 3] * value },
                { /*x:3 y:0*/this[3, 0] * value, /*x:3 y:1*/this[3, 1] * value, /*x:3 y:2*/this[3, 2] * value, /*x:3 y:3*/this[3, 3] * value }
            });
        }

        public Matrix4 Multiply(Matrix4 matrix)
        {
            return new Matrix4(new double[,]
            {
                {   
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]) + (this[2, 0] * matrix[0, 2]) + (this[3, 0] * matrix[0, 3]), 
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) + (this[2, 1] * matrix[0, 2]) + (this[3, 1] * matrix[0, 3]),
                    /*x:0 y:2*/(this[0, 2] * matrix[0, 0]) + (this[1, 2] * matrix[0, 1]) + (this[2, 2] * matrix[0, 2]) + (this[3, 2] * matrix[0, 3]),
                    /*x:0 y:3*/(this[0, 3] * matrix[0, 0]) + (this[1, 3] * matrix[0, 1]) + (this[2, 3] * matrix[0, 2]) + (this[3, 3] * matrix[0, 3])
                },
                {   
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]) + (this[2, 0] * matrix[1, 2]) + (this[3, 0] * matrix[1, 3]), 
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) + (this[2, 1] * matrix[1, 2]) + (this[3, 1] * matrix[1, 3]),
                    /*x:1 y:2*/(this[0, 2] * matrix[1, 0]) + (this[1, 2] * matrix[1, 1]) + (this[2, 2] * matrix[1, 2]) + (this[3, 2] * matrix[1, 3]),
                    /*x:1 y:3*/(this[0, 3] * matrix[1, 0]) + (this[1, 3] * matrix[1, 1]) + (this[2, 3] * matrix[1, 2]) + (this[3, 3] * matrix[1, 3])
                },
                {   
                    /*x:2 y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]) + (this[2, 0] * matrix[2, 2]) + (this[3, 0] * matrix[2, 3]), 
                    /*x:2 y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]) + (this[2, 1] * matrix[2, 2]) + (this[3, 1] * matrix[2, 3]),
                    /*x:2 y:2*/(this[0, 2] * matrix[2, 0]) + (this[1, 2] * matrix[2, 1]) + (this[2, 2] * matrix[2, 2]) + (this[3, 2] * matrix[2, 3]),
                    /*x:2 y:3*/(this[0, 3] * matrix[2, 0]) + (this[1, 3] * matrix[2, 1]) + (this[2, 3] * matrix[2, 2]) + (this[3, 3] * matrix[2, 3])
                },
                {   
                    /*x:3 y:0*/(this[0, 0] * matrix[3, 0]) + (this[1, 0] * matrix[3, 1]) + (this[2, 0] * matrix[3, 2]) + (this[3, 0] * matrix[3, 3]), 
                    /*x:3 y:1*/(this[0, 1] * matrix[3, 0]) + (this[1, 1] * matrix[3, 1]) + (this[2, 1] * matrix[3, 2]) + (this[3, 1] * matrix[3, 3]),
                    /*x:3 y:2*/(this[0, 2] * matrix[3, 0]) + (this[1, 2] * matrix[3, 1]) + (this[2, 2] * matrix[3, 2]) + (this[3, 2] * matrix[3, 3]),
                    /*x:3 y:3*/(this[0, 3] * matrix[3, 0]) + (this[1, 3] * matrix[3, 1]) + (this[2, 3] * matrix[3, 2]) + (this[3, 3] * matrix[3, 3])
                }
            });
        }

        public Matrix4x2 Multiply(Matrix4x2 matrix)
        {
            return new Matrix4x2(new double[,]
            {
                {
                    /*X:0 Y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]) + (this[2, 0] * matrix[0, 2]) + (this[3, 0] * matrix[0, 3]),
                    /*X:0 Y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) + (this[2, 1] * matrix[0, 2]) + (this[3, 1] * matrix[0, 3]),
                    /*X:0 Y:2*/(this[0, 2] * matrix[0, 0]) + (this[1, 2] * matrix[0, 1]) + (this[2, 2] * matrix[0, 2]) + (this[3, 2] * matrix[0, 3]),
                    /*X:0 Y:3*/(this[0, 3] * matrix[0, 0]) + (this[1, 3] * matrix[0, 1]) + (this[2, 3] * matrix[0, 2]) + (this[3, 3] * matrix[0, 3])
                },
                {
                    /*X:1 Y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]) + (this[2, 0] * matrix[1, 2]) + (this[3, 0] * matrix[1, 3]),
                    /*X:1 Y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) + (this[2, 1] * matrix[1, 2]) + (this[3, 1] * matrix[1, 3]),
                    /*X:1 Y:2*/(this[0, 2] * matrix[1, 0]) + (this[1, 2] * matrix[1, 1]) + (this[2, 2] * matrix[1, 2]) + (this[3, 2] * matrix[1, 3]),
                    /*X:1 Y:3*/(this[0, 3] * matrix[1, 0]) + (this[1, 3] * matrix[1, 1]) + (this[2, 3] * matrix[1, 2]) + (this[3, 3] * matrix[1, 3])
                }
            });
        }

        public Matrix4x3 Multiply(Matrix4x3 matrix)
        {
            return new Matrix4x3(new double[,]
            {
                {
                    /*X:0 Y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]) + (this[2, 0] * matrix[0, 2]) + (this[3, 0] * matrix[0, 3]),
                    /*X:0 Y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) + (this[2, 1] * matrix[0, 2]) + (this[3, 1] * matrix[0, 3]),
                    /*X:0 Y:2*/(this[0, 2] * matrix[0, 0]) + (this[1, 2] * matrix[0, 1]) + (this[2, 2] * matrix[0, 2]) + (this[3, 2] * matrix[0, 3]),
                    /*X:0 Y:2*/(this[0, 3] * matrix[0, 0]) + (this[1, 3] * matrix[0, 1]) + (this[2, 3] * matrix[0, 2]) + (this[3, 3] * matrix[0, 3])
                },
                {
                    /*X:1 Y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]) + (this[2, 0] * matrix[1, 2]) + (this[3, 0] * matrix[1, 3]),
                    /*X:1 Y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) + (this[2, 1] * matrix[1, 2]) + (this[3, 1] * matrix[1, 3]),
                    /*X:1 Y:2*/(this[0, 2] * matrix[1, 0]) + (this[1, 2] * matrix[1, 1]) + (this[2, 2] * matrix[1, 2]) + (this[3, 2] * matrix[1, 3]),
                    /*X:1 Y:3*/(this[0, 3] * matrix[1, 0]) + (this[1, 3] * matrix[1, 1]) + (this[2, 3] * matrix[1, 2]) + (this[3, 3] * matrix[1, 3])
                },
                {
                    /*X:2 Y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]) + (this[2, 0] * matrix[2, 2]) + (this[3, 0] * matrix[2, 3]),
                    /*X:2 Y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]) + (this[2, 1] * matrix[2, 2]) + (this[3, 1] * matrix[2, 3]),
                    /*X:2 Y:2*/(this[0, 2] * matrix[2, 0]) + (this[1, 2] * matrix[2, 1]) + (this[2, 2] * matrix[2, 2]) + (this[3, 2] * matrix[2, 3]),
                    /*X:2 Y:3*/(this[0, 3] * matrix[2, 0]) + (this[1, 3] * matrix[2, 1]) + (this[2, 3] * matrix[2, 2]) + (this[3, 3] * matrix[2, 3])
                }
            });
        }

        public Matrix Add(IMatrix<double> matrix)
        {
            if ((matrix.RowSize != 4) || (matrix.ColumnSize != 4))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 4 rows and 4 columns.");
            }

            Matrix output = new Matrix(new double[4, 4]);

            for (int x = 0; x < 4; x++)
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
            if ((matrix.RowSize != 4) || (matrix.ColumnSize != 4))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 4 rows and 4 columns.");
            }

            Matrix output = new Matrix(new double[4, 4]);

            for (int x = 0; x < 4; x++)
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
            if (matrix.RowSize != 4)
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 4 rows.");
            }

            Matrix output = new Matrix(new double[4, matrix.ColumnSize]);

            for (int x = 0; x < matrix.ColumnSize; x++)
            {
                for (int y = 0; y < 4; y++)
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

        public double Determinant()
        {
            return
                (this[0, 0] * this[1, 1] * this[2, 2] * this[3, 3]) - (this[0, 0] * this[1, 1] * this[3, 2] * this[2, 3]) + (this[0, 0] * this[2, 1] * this[3, 2] * this[1, 3])
                - (this[0, 0] * this[2, 1] * this[1, 2] * this[3, 3]) + (this[0, 0] * this[3, 1] * this[1, 2] * this[2, 3]) - (this[0, 0] * this[3, 1] * this[2, 2] * this[1, 3])
                - (this[1, 0] * this[2, 1] * this[3, 2] * this[0, 3]) + (this[1, 0] * this[2, 1] * this[0, 2] * this[3, 3]) - (this[1, 0] * this[3, 1] * this[0, 2] * this[2, 3])
                + (this[1, 0] * this[3, 1] * this[2, 2] * this[0, 3]) - (this[1, 0] * this[0, 1] * this[2, 2] * this[3, 3]) + (this[1, 0] * this[0, 1] * this[3, 2] * this[2, 3])

                + (this[2, 0] * this[3, 1] * this[0, 2] * this[1, 3]) - (this[2, 0] * this[3, 1] * this[1, 2] * this[0, 3]) + (this[2, 0] * this[0, 1] * this[1, 2] * this[3, 3])
                - (this[2, 0] * this[0, 1] * this[3, 2] * this[1, 3]) + (this[2, 0] * this[1, 1] * this[3, 2] * this[0, 3]) - (this[2, 0] * this[1, 1] * this[0, 2] * this[3, 3])
                - (this[3, 0] * this[0, 1] * this[1, 2] * this[2, 3]) + (this[3, 0] * this[0, 1] * this[2, 2] * this[1, 3]) - (this[3, 0] * this[1, 1] * this[2, 2] * this[0, 3])
                + (this[3, 0] * this[1, 1] * this[0, 2] * this[2, 3]) - (this[3, 0] * this[2, 1] * this[0, 2] * this[1, 3]) + (this[3, 0] * this[2, 1] * this[1, 2] * this[0, 3]);
        }

        public Matrix4 Invert()
        {
            double a = this[0, 0], b = this[0, 1], c = this[0, 2], d = this[0, 3];
            double e = this[1, 0], f = this[1, 1], g = this[1, 2], h = this[1, 3];
            double i = this[2, 0], j = this[2, 1], k = this[2, 2], l = this[2, 3];
            double m = this[3, 0], n = this[3, 1], o = this[3, 2], p = this[3, 3];

            double kp_lo = k * p - l * o;
            double jp_ln = j * p - l * n;
            double jo_kn = j * o - k * n;
            double ip_lm = i * p - l * m;
            double io_km = i * o - k * m;
            double in_jm = i * n - j * m;

            double a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
            double a12 = -(e * kp_lo - g * ip_lm + h * io_km);
            double a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
            double a14 = -(e * jo_kn - f * io_km + g * in_jm);

            double det = a * a11 + b * a12 + c * a13 + d * a14;

            if (Math.Abs(det) < double.Epsilon)
            {
                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            }

            double invDet = 1.0f / det;

            Vector4 row0 = new Vector4(a11, a12, a13, a14) * invDet;

            Vector4 row1 = new Vector4(
                -(b * kp_lo - c * jp_ln + d * jo_kn),
                +(a * kp_lo - c * ip_lm + d * io_km),
                -(a * jp_ln - b * ip_lm + d * in_jm),
                +(a * jo_kn - b * io_km + c * in_jm)) * invDet;

            double gp_ho = g * p - h * o;
            double fp_hn = f * p - h * n;
            double fo_gn = f * o - g * n;
            double ep_hm = e * p - h * m;
            double eo_gm = e * o - g * m;
            double en_fm = e * n - f * m;

            Vector4 row2 = new Vector4(
                +(b * gp_ho - c * fp_hn + d * fo_gn),
                -(a * gp_ho - c * ep_hm + d * eo_gm),
                +(a * fp_hn - b * ep_hm + d * en_fm),
                -(a * fo_gn - b * eo_gm + c * en_fm)) * invDet;

            double gl_hk = g * l - h * k;
            double fl_hj = f * l - h * j;
            double fk_gj = f * k - g * j;
            double el_hi = e * l - h * i;
            double ek_gi = e * k - g * i;
            double ej_fi = e * j - f * i;

            Vector4 row3 = new Vector4(
                -(b * gl_hk - c * fl_hj + d * fk_gj),
                +(a * gl_hk - c * el_hi + d * ek_gi),
                -(a * fl_hj - b * el_hi + d * ej_fi),
                +(a * fk_gj - b * ek_gi + c * ej_fi)) * invDet;

            return new Matrix4(row0, row1, row2, row3);
        }

        public double Trace()
        {
            return this[0, 0] + this[1, 1] + this[2, 2] + this[3, 3];
        }

        public Matrix3 Normalize()
        {
            double det = Determinant();

            return new Matrix3(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] / det, /*x:0 y:1*/this[0, 1] / det, /*x:0 y:2*/this[0, 2] / det, /*x:0 y:3*/this[0, 3] / det },
                { /*x:1 y:0*/this[1, 0] / det, /*x:1 y:1*/this[1, 1] / det, /*x:1 y:2*/this[1, 2] / det, /*x:1 y:3*/this[1, 3] / det },
                { /*x:2 y:0*/this[2, 0] / det, /*x:2 y:1*/this[2, 1] / det, /*x:2 y:2*/this[2, 2] / det, /*x:2 y:3*/this[2, 3] / det },
                { /*x:3 y:0*/this[3, 0] / det, /*x:3 y:1*/this[3, 1] / det, /*x:3 y:2*/this[3, 2] / det, /*x:3 y:3*/this[3, 3] / det }
            });
        }

        public Matrix4 Transpose()
        {
            return new Matrix4(Column0, Column1, Column2, Column3);
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix4 matrix &&
                _matrix == matrix._matrix;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public ReadOnlySpan<float> GetGLData()
        {
            int w = 4;
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
            return $@"[{_matrix[0, 0]}, {_matrix[1, 0]}, {_matrix[2, 0]}, {_matrix[3, 0]}]
[{_matrix[0, 1]}, {_matrix[1, 1]}, {_matrix[2, 1]}, {_matrix[3, 1]}]
[{_matrix[0, 2]}, {_matrix[1, 2]}, {_matrix[2, 2]}, {_matrix[3, 2]}]
[{_matrix[0, 3]}, {_matrix[1, 3]}, {_matrix[2, 3]}, {_matrix[3, 3]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0, 0].ToString(format)}, {_matrix[1, 0].ToString(format)}, {_matrix[2, 0].ToString(format)}, {_matrix[3, 0].ToString(format)}]
[{_matrix[0, 1].ToString(format)}, {_matrix[1, 1].ToString(format)}, {_matrix[2, 1].ToString(format)}, {_matrix[3, 1].ToString(format)}]
[{_matrix[0, 2].ToString(format)}, {_matrix[1, 2].ToString(format)}, {_matrix[2, 2].ToString(format)}, {_matrix[3, 2].ToString(format)}]
[{_matrix[0, 3].ToString(format)}, {_matrix[1, 3].ToString(format)}, {_matrix[2, 3].ToString(format)}, {_matrix[3, 3].ToString(format)}]";
        }

        public static bool operator ==(Matrix4 a, Matrix4 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Matrix4 a, Matrix4 b)
        {
            return !a.Equals(b);
        }

        public static Matrix4 operator +(Matrix4 a, Matrix4 b)
        {
            return a.Add(b);
        }

        public static Matrix4 operator -(Matrix4 a, Matrix4 b)
        {
            return a.Subtract(b);
        }

        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            return a.Multiply(b);
        }

        public static Matrix4x2 operator *(Matrix4 a, Matrix4x2 b)
        {
            return a.Multiply(b);
        }

        public static Matrix4x3 operator *(Matrix4 a, Matrix4x3 b)
        {
            return a.Multiply(b);
        }

        public static Matrix4 operator *(Matrix4 a, double b)
        {
            return a.Multiply(b);
        }

        public static Matrix4 operator *(double a, Matrix4 b)
        {
            return b.Multiply(a);
        }

        public static Matrix operator +(Matrix4 a, IMatrix<double> b)
        {
            return a.Add(b);
        }
        public static Matrix operator -(Matrix4 a, IMatrix<double> b)
        {
            return a.Subtract(b);
        }
        public static Matrix operator *(Matrix4 a, IMatrix<double> b)
        {
            return a.Multiply(b);
        }

        public static Matrix4 Zero { get; } = new Matrix4(Vector4.Zero, Vector4.Zero, Vector4.Zero, Vector4.Zero);

        public static Matrix4 Identity { get; } = new Matrix4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));

        public static Matrix4 CreateRotation(Vector3 axis, Radian angle)
        {
            // normalize and create a local copy of the vector.
            axis.Normalise();
            double axisX = axis.X;
            double axisY = axis.Y;
            double axisZ = axis.Z;

            // calculate angles
            double cos = Math.Cos(-angle);
            double sin = Math.Sin(-angle);
            double t = 1.0f - cos;

            // do the conversion math once
            double tXX = t * axisX * axisX;
            double tXY = t * axisX * axisY;
            double tXZ = t * axisX * axisZ;
            double tYY = t * axisY * axisY;
            double tYZ = t * axisY * axisZ;
            double tZZ = t * axisZ * axisZ;

            double sinX = sin * axisX;
            double sinY = sin * axisY;
            double sinZ = sin * axisZ;

            return new Matrix4(
                new Vector4(tXX + cos, tXY - sinZ, tXZ + sinY, 0),
                new Vector4(tXY + sinZ, tYY + cos, tYZ - sinX, 0),
                new Vector4(tXZ - sinY, tYZ + sinX, tZZ + cos, 0),
                new Vector4(0, 0, 0, 1));
        }

        public static Matrix4 CreateRotationX(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, cos, sin, 0),
                new Vector4(0, -sin, cos, 0),
                new Vector4(0, 0, 0, 1));
        }

        public static Matrix4 CreateRotationY(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4(
                new Vector4(cos, 0, -sin, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(sin, 0, cos, 0),
                new Vector4(0, 0, 0, 1));
        }

        public static Matrix4 CreateRotationZ(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4(
                new Vector4(cos, sin, 0, 0),
                new Vector4(-sin, cos, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1));
        }

        public static Matrix4 CreateScale(double scale)
        {
            return new Matrix4(
                new Vector4(scale, 0, 0, 0),
                new Vector4(0, scale, 0, 0),
                new Vector4(0, 0, scale, 0),
                new Vector4(0, 0, 0, 1));
        }

        public static Matrix4 CreateScale(double scaleX, double scaleY, double scaleZ)
        {
            return new Matrix4(
                new Vector4(scaleX, 0, 0, 0),
                new Vector4(0, scaleY, 0, 0),
                new Vector4(0, 0, scaleZ, 0),
                new Vector4(0, 0, 0, 1));
        }

        public static Matrix4 CreateScale(Vector3 scale)
        {
            return CreateScale(scale.X, scale.Y, scale.Z);
        }

        public static Matrix4 CreateTranslation(double xyz)
        {
            return new Matrix4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(xyz, xyz, xyz, 1));
        }

        public static Matrix4 CreateTranslation(double x, double y, double z)
        {
            return new Matrix4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(x, y, z, 1));
        }

        public static Matrix4 CreateTranslation(Vector3 xyz)
        {
            return CreateTranslation(xyz.X, xyz.Y, xyz.Z);
        }

        public static Matrix4 CreateOrthographicOffCenter(double left, double right, double top, double bottom, double depthNear, double depthFar)
        {
            double invRL = 1.0 / (right - left);
            double invTB = 1.0 / (top - bottom);
            double invFN = 1.0 / (depthFar - depthNear);

            return new Matrix4(
                new Vector4(2 * invRL, 0, 0, 0),
                new Vector4(0, 2 * invTB, 0, 0),
                new Vector4(0, 0, -2 * invFN, 0),
                new Vector4(-(right + left) * invRL, -(top + bottom) * invTB, -(depthFar + depthNear) * invFN, 1));
        }

        public static Matrix4 CreateOrthographic(double width, double height, double depthNear, double depthFar)
        {
            return CreateOrthographicOffCenter(-width / 2, width / 2, height / 2, -height / 2, depthNear, depthFar);
        }

        public static Matrix4 CreatePerspectiveOffCenter(double left, double right, double top, double bottom, double depthNear, double depthFar)
        {
            if (depthNear <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(depthNear));
            }

            if (depthFar <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(depthFar));
            }

            if (depthNear >= depthFar)
            {
                throw new ArgumentOutOfRangeException(nameof(depthNear));
            }
            /*
            double x = 2.0 * depthNear / (right - left);
            double y = 2.0 * depthNear / (top - bottom);
            double a = (right + left) / (right - left);
            double b = (top + bottom) / (top - bottom);
            double c = -(depthFar + depthNear) / (depthFar - depthNear);
            double d = -(2.0 * depthFar * depthNear) / (depthFar - depthNear);

            return new Matrix4(
                new Vector4(x, 0, 0, 0),
                new Vector4(0, y, 0, 0),
                new Vector4(a, b, c, -1),
                new Vector4(0, 0, d, 0));*/

            double widthMulti = 1 / (right - left);
            double heightMulti = 1 / (bottom - top);
            double depthMutli = 1 / (depthFar - depthNear);
            double near2 = depthNear * 2;

            return new Matrix4(
                new Vector4(near2 * widthMulti, 0, 0, 0),
                new Vector4(0, near2 * heightMulti, 0, 0),
                new Vector4(-(right + left) * widthMulti, -(bottom + top) * heightMulti, depthFar * depthMutli, 1),
                new Vector4(0, 0, -depthFar * depthNear * depthMutli, 0));
        }

        public static Matrix4 CreatePerspectiveFieldOfView(Radian fovy, double aspect, double depthNear, double depthFar)
        {
            if (fovy <= 0 || fovy > Math.PI)
            {
                throw new ArgumentOutOfRangeException(nameof(fovy));
            }

            if (aspect <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(aspect));
            }

            if (depthNear <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(depthNear));
            }

            if (depthFar <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(depthFar));
            }
            /*
            double maxY = depthNear * Math.Tan(0.5 * fovy);
            double minY = -maxY;
            double minX = minY * aspect;
            double maxX = maxY * aspect;

            return CreatePerspectiveOffCenter(minX, maxX, minY, maxY, depthNear, depthFar);*/

            double depthMutli = 1 / (depthFar - depthNear);
            double degree = Math.Tan(fovy * 0.5);

            return new Matrix4(
                new Vector4(1 / (aspect * degree), 0, 0, 0),
                new Vector4(0, 1 / degree, 0, 0),
                new Vector4(0, 0, depthFar * depthMutli, 1),
                new Vector4(0, 0, -depthFar * depthNear * depthMutli, 0));
        }

        public static Matrix4 LookAt(Vector3 eye, Vector3 target, Vector3 up)
        {
            Vector3 z = (eye - target).Normalised();
            Vector3 x = up.Cross(z).Normalised();
            Vector3 y = z.Cross(x).Normalised();

            return new Matrix4(
                new Vector4(x.X, y.X, z.X, 0),
                new Vector4(x.Y, y.Y, z.Y, 0),
                new Vector4(x.Z, y.Z, z.Z, 0),
                new Vector4(-((x.X * eye.X) + (x.Y * eye.Y) + (x.Z * eye.Z)), -((y.X * eye.X) + (y.Y * eye.Y) + (y.Z * eye.Z)), -((z.X * eye.X) + (z.Y * eye.Y) + (z.Z * eye.Z)), 1));
        }

        public static Matrix4 LookAt(double eyeX, double eyeY, double eyeZ, double targetX, double targetY, double targetZ, double upX, double upY, double upZ)
        {
            return LookAt(new Vector3(eyeX, eyeY, eyeZ), new Vector3(targetX, targetY, targetZ), new Vector3(upX, upY, upZ));
        }

        public static implicit operator Matrix4(Matrix4<double> matrix)
        {
            return new Matrix4(matrix.Data);
        }
        public static explicit operator Matrix4(Matrix4<float> matrix)
        {
            double[,] data = new double[4, 4]
            {
                { matrix[0, 0], matrix[0, 1], matrix[0, 2], matrix[0, 3] },
                { matrix[1, 0], matrix[1, 1], matrix[1, 2], matrix[1, 3] },
                { matrix[2, 0], matrix[2, 1], matrix[2, 2], matrix[2, 3] },
                { matrix[3, 0], matrix[3, 1], matrix[3, 2], matrix[3, 3] }
            };

            return new Matrix4(data);
        }

        public static implicit operator Matrix4<double>(Matrix4 matrix)
        {
            return new Matrix4<double>(matrix._matrix);
        }
        public static explicit operator Matrix4<float>(Matrix4 matrix)
        {
            float[,] data = new float[4, 4]
            {
                { (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2], (float)matrix[0, 3] },
                { (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2], (float)matrix[1, 3] },
                { (float)matrix[2, 0], (float)matrix[2, 1], (float)matrix[2, 2], (float)matrix[2, 3] },
                { (float)matrix[3, 0], (float)matrix[3, 1], (float)matrix[3, 2], (float)matrix[3, 3] }
            };

            return new Matrix4<float>(data);
        }
    }
}
