using System;

namespace Zene.Structs
{
    public struct Matrix4x3 : IMatrix<double>
    {
        public Matrix4x3(Vector3 row0, Vector3 row1, Vector3 row2, Vector3 row3)
        {
            _matrix = new double[,]
            {
                { row0.X, row1.X, row2.X , row3.X  },
                { row0.Y, row1.Y, row2.Y , row3.Y  },
                { row0.Z, row1.Z, row2.Z , row3.Z  }
            };
        }

        public Matrix4x3(double[,] matrix)
        {
            _matrix = new double[3, 4];

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    try
                    {
                        _matrix[x, y] = matrix[x, y];
                    }
                    catch (Exception)
                    {
                        throw new Exception("Matrix needs to have at least 4 rows and 3 columns.");
                    }
                }
            }
        }
        public Matrix4x3(double[] matrix)
        {
            _matrix = new double[3, 4];

            if (matrix.Length < 12)
            {
                throw new Exception("Matrix needs to have at least 4 rows and 3 columns.");
            }

            _matrix[0, 0] = matrix[0];
            _matrix[1, 0] = matrix[1];
            _matrix[2, 0] = matrix[2];

            _matrix[0, 1] = matrix[3];
            _matrix[1, 1] = matrix[4];
            _matrix[2, 1] = matrix[5];

            _matrix[0, 2] = matrix[6];
            _matrix[1, 2] = matrix[7];
            _matrix[2, 2] = matrix[8];

            _matrix[0, 3] = matrix[9];
            _matrix[1, 3] = matrix[10];
            _matrix[2, 3] = matrix[11];
        }

        private readonly double[,] _matrix;

        public double[,] Data => _matrix;
        int IMatrix<double>.RowSize => 4;
        int IMatrix<double>.ColumnSize => 3;

        public double this[int x, int y]
        {
            get
            {
                if (x >= 3 || y >= 4)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 3 x 4 range of matrix4x3.");
                }

                return _matrix[x, y];
            }
            set
            {
                if (x >= 3 || y >= 4)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 3 x 4 range of matrix4x3.");
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

        public Vector3 Row2
        {
            get
            {
                return new Vector3(_matrix[0, 2], _matrix[1, 2], _matrix[2, 2]);
            }
            set
            {
                _matrix[0, 2] = value.X;
                _matrix[1, 2] = value.Y;
            }
        }

        public Vector3 Row3
        {
            get
            {
                return new Vector3(_matrix[0, 3], _matrix[1, 3], _matrix[2, 3]);
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

        public Matrix4x3 Add(Matrix4x3 matrix)
        {
            return new Matrix4x3(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] + matrix[0, 0], /*x:0 y:1*/this[0, 1] + matrix[0, 1], /*x:0 y:2*/this[0, 2] + matrix[0, 2], /*x:0 y:3*/this[0, 3] + matrix[0, 3] },
                { /*x:1 y:0*/this[1, 0] + matrix[1, 0], /*x:1 y:1*/this[1, 1] + matrix[1, 1], /*x:1 y:2*/this[1, 2] + matrix[1, 2], /*x:1 y:3*/this[1, 3] + matrix[1, 3] },
                { /*x:2 y:0*/this[2, 0] + matrix[2, 0], /*x:2 y:1*/this[2, 1] + matrix[2, 1], /*x:2 y:2*/this[2, 2] + matrix[2, 2], /*x:2 y:3*/this[2, 3] + matrix[2, 3] }
            });
        }

        public Matrix4x3 Subtract(Matrix4x3 matrix)
        {
            return new Matrix4x3(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] - matrix[0, 0], /*x:0 y:1*/this[0, 1] - matrix[0, 1], /*x:0 y:2*/this[0, 2] - matrix[0, 2], /*x:0 y:3*/this[0, 3] - matrix[0, 3] },
                { /*x:1 y:0*/this[1, 0] - matrix[1, 0], /*x:1 y:1*/this[1, 1] - matrix[1, 1], /*x:1 y:2*/this[1, 2] - matrix[1, 2], /*x:1 y:3*/this[1, 3] - matrix[1, 3] },
                { /*x:2 y:0*/this[2, 0] - matrix[2, 0], /*x:2 y:1*/this[2, 1] - matrix[2, 1], /*x:2 y:2*/this[2, 2] - matrix[2, 2], /*x:2 y:3*/this[2, 3] - matrix[2, 3] }
            });
        }

        public Matrix4x3 Multiply(double value)
        {
            return new Matrix4x3(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] * value, /*x:0 y:1*/this[0, 1] * value, /*x:0 y:2*/this[0, 2] * value, /*x:0 y:3*/this[0, 3] * value },
                { /*x:1 y:0*/this[1, 0] * value, /*x:1 y:1*/this[1, 1] * value, /*x:1 y:2*/this[1, 2] * value, /*x:1 y:3*/this[1, 3] * value },
                { /*x:2 y:0*/this[2, 0] * value, /*x:2 y:1*/this[2, 1] * value, /*x:2 y:2*/this[2, 2] * value, /*x:2 y:3*/this[2, 3] * value }
            });
        }

        public Matrix4x3 Multiply(Matrix3 matrix)
        {
            return new Matrix4x3(new double[,]
            {
                {
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]) + (this[2, 0] * matrix[0, 2]), 
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) + (this[2, 1] * matrix[0, 2]),
                    /*x:0 y:1*/(this[0, 2] * matrix[0, 0]) + (this[1, 2] * matrix[0, 1]) + (this[2, 2] * matrix[0, 2]),
                    /*x:0 y:1*/(this[0, 3] * matrix[0, 0]) + (this[1, 3] * matrix[0, 1]) + (this[2, 3] * matrix[0, 2])
                },
                {
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]) + (this[2, 0] * matrix[1, 2]), 
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) + (this[2, 1] * matrix[1, 2]),
                    /*x:0 y:1*/(this[0, 2] * matrix[1, 0]) + (this[1, 2] * matrix[1, 1]) + (this[2, 2] * matrix[1, 2]),
                    /*x:0 y:1*/(this[0, 3] * matrix[0, 0]) + (this[1, 3] * matrix[1, 1]) + (this[2, 3] * matrix[1, 2])
                },
                {
                    /*x:2 y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]) + (this[2, 0] * matrix[2, 2]), 
                    /*x:2 y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]) + (this[2, 1] * matrix[2, 2]),
                    /*x:0 y:1*/(this[0, 2] * matrix[2, 0]) + (this[1, 2] * matrix[2, 1]) + (this[2, 2] * matrix[2, 2]),
                    /*x:0 y:1*/(this[0, 3] * matrix[2, 0]) + (this[1, 3] * matrix[2, 1]) + (this[2, 3] * matrix[2, 2])
                }
            });
        }

        public Matrix4x2 Multiply(Matrix3x2 matrix)
        {
            return new Matrix4x2(new double[,]
            {
                {
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]) + (this[2, 0] * matrix[0, 2]), 
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) + (this[2, 1] * matrix[0, 2]),
                    /*x:0 y:1*/(this[0, 2] * matrix[0, 0]) + (this[1, 2] * matrix[0, 1]) + (this[2, 2] * matrix[0, 2]),
                    /*x:0 y:1*/(this[0, 3] * matrix[0, 0]) + (this[1, 3] * matrix[0, 1]) + (this[2, 3] * matrix[0, 2])
                },
                {
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]) + (this[2, 0] * matrix[1, 2]), 
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) + (this[2, 1] * matrix[1, 2]),
                    /*x:0 y:1*/(this[0, 2] * matrix[1, 0]) + (this[1, 2] * matrix[1, 1]) + (this[2, 2] * matrix[1, 2]),
                    /*x:0 y:1*/(this[0, 3] * matrix[1, 0]) + (this[1, 3] * matrix[1, 1]) + (this[2, 3] * matrix[1, 2])
                },
            });
        }

        public Matrix4 Multiply(Matrix3x4 matrix)
        {
            return new Matrix4(new double[,]
            {
                {
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]) + (this[2, 0] * matrix[0, 2]), 
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) + (this[2, 1] * matrix[0, 2]),
                    /*x:0 y:2*/(this[0, 2] * matrix[0, 0]) + (this[1, 2] * matrix[0, 1]) + (this[2, 2] * matrix[0, 2]),
                    /*x:0 y:3*/(this[0, 3] * matrix[0, 0]) + (this[1, 3] * matrix[0, 1]) + (this[2, 3] * matrix[0, 2])
                },
                {
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]) + (this[2, 0] * matrix[1, 2]), 
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) + (this[2, 1] * matrix[1, 2]),
                    /*x:1 y:2*/(this[0, 2] * matrix[1, 0]) + (this[1, 2] * matrix[1, 1]) + (this[2, 2] * matrix[1, 2]),
                    /*x:1 y:3*/(this[0, 3] * matrix[1, 0]) + (this[1, 3] * matrix[1, 1]) + (this[2, 3] * matrix[1, 2])
                },
                {
                    /*x:2 y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]) + (this[2, 0] * matrix[2, 2]), 
                    /*x:2 y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]) + (this[2, 1] * matrix[2, 2]),
                    /*x:2 y:2*/(this[0, 2] * matrix[2, 0]) + (this[1, 2] * matrix[2, 1]) + (this[2, 2] * matrix[2, 2]),
                    /*x:2 y:3*/(this[0, 3] * matrix[2, 0]) + (this[1, 3] * matrix[2, 1]) + (this[2, 3] * matrix[2, 2])
                },
                {
                    /*x:3 y:0*/(this[0, 0] * matrix[3, 0]) + (this[1, 0] * matrix[3, 1]) + (this[2, 0] * matrix[3, 2]), 
                    /*x:3 y:1*/(this[0, 1] * matrix[3, 0]) + (this[1, 1] * matrix[3, 1]) + (this[2, 1] * matrix[3, 2]),
                    /*x:3 y:2*/(this[0, 2] * matrix[3, 0]) + (this[1, 2] * matrix[3, 1]) + (this[2, 2] * matrix[3, 2]),
                    /*x:3 y:3*/(this[0, 3] * matrix[3, 0]) + (this[1, 3] * matrix[3, 1]) + (this[2, 3] * matrix[3, 2])
                }
            });
        }

        public Matrix Add(IMatrix<double> matrix)
        {
            if ((matrix.RowSize != 4) || (matrix.ColumnSize != 3))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 4 rows and 3 columns.");
            }

            Matrix output = new Matrix(new double[3, 4]);

            for (int x = 0; x < 3; x++)
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
            if ((matrix.RowSize != 4) || (matrix.ColumnSize != 3))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 4 rows and 3 columns.");
            }

            Matrix output = new Matrix(new double[3, 4]);

            for (int x = 0; x < 3; x++)
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
            if (matrix.RowSize != 3)
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 3 rows.");
            }

            Matrix output = new Matrix(new double[4, matrix.ColumnSize]);

            for (int x = 0; x < matrix.ColumnSize; x++)
            {
                for (int y = 0; y < 4; y++)
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
            return this[0, 0] + this[1, 1] + this[2, 2];
        }

        public Matrix4x3 Invert()
        {
            Matrix3 inverseRotation = new Matrix3(
                (Vector3)Column0,
                (Vector3)Column1,
                (Vector3)Column2);
            inverseRotation.Row0 /= inverseRotation.Row0.SquaredLength;
            inverseRotation.Row1 /= inverseRotation.Row1.SquaredLength;
            inverseRotation.Row2 /= inverseRotation.Row2.SquaredLength;

            Vector3 translation = Row3;

            return new Matrix4x3(
                inverseRotation.Row0,
                inverseRotation.Row1,
                inverseRotation.Row2,
                new Vector3(
                    -inverseRotation.Row0.Dot(translation),
                    -inverseRotation.Row1.Dot(translation),
                    -inverseRotation.Row2.Dot(translation)
                ));
        }

        public Matrix3x4 Transpose()
        {
            return new Matrix3x4(Column0, Column1, Column2);
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix4x3 matrix &&
                _matrix == matrix._matrix;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public ReadOnlySpan<float> GetGLData()
        {
            int w = 3;
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
            return $@"[{_matrix[0, 0]}, {_matrix[1, 0]}, {_matrix[2, 0]}]
[{_matrix[0, 1]}, {_matrix[1, 1]}, {_matrix[2, 1]}]
[{_matrix[0, 2]}, {_matrix[1, 2]}, {_matrix[2, 2]}]
[{_matrix[0, 3]}, {_matrix[1, 3]}, {_matrix[2, 3]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0, 0].ToString(format)}, {_matrix[1, 0].ToString(format)}, {_matrix[2, 0].ToString(format)}]
[{_matrix[0, 1].ToString(format)}, {_matrix[1, 1].ToString(format)}, {_matrix[2, 1].ToString(format)}]
[{_matrix[0, 2].ToString(format)}, {_matrix[1, 2].ToString(format)}, {_matrix[2, 2].ToString(format)}]
[{_matrix[0, 3].ToString(format)}, {_matrix[1, 3].ToString(format)}, {_matrix[2, 3].ToString(format)}]";
        }

        public static bool operator ==(Matrix4x3 a, Matrix4x3 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Matrix4x3 a, Matrix4x3 b)
        {
            return !a.Equals(b);
        }

        public static Matrix4x3 operator +(Matrix4x3 a, Matrix4x3 b)
        {
            return a.Add(b);
        }

        public static Matrix4x3 operator -(Matrix4x3 a, Matrix4x3 b)
        {
            return a.Subtract(b);
        }

        public static Matrix4x3 operator *(Matrix4x3 a, Matrix3 b)
        {
            return a.Multiply(b);
        }

        public static Matrix4x2 operator *(Matrix4x3 a, Matrix3x2 b)
        {
            return a.Multiply(b);
        }

        public static Matrix4 operator *(Matrix4x3 a, Matrix3x4 b)
        {
            return a.Multiply(b);
        }

        public static Matrix4x3 operator *(Matrix4x3 a, double b)
        {
            return a.Multiply(b);
        }

        public static Matrix4x3 operator *(double a, Matrix4x3 b)
        {
            return b.Multiply(a);
        }

        public static Matrix operator +(Matrix4x3 a, IMatrix<double> b)
        {
            return a.Add(b);
        }
        public static Matrix operator -(Matrix4x3 a, IMatrix<double> b)
        {
            return a.Subtract(b);
        }
        public static Matrix operator *(Matrix4x3 a, IMatrix<double> b)
        {
            return a.Multiply(b);
        }

        public static Matrix4x3 CreateScale(double scale)
        {
            return new Matrix4x3(
                new Vector3(scale, 0, 0),
                new Vector3(0, scale, 0),
                new Vector3(0, 0, scale),
                Vector3.Zero);
        }
        public static Matrix4x3 CreateScale(double scaleX, double scaleY, double scaleZ)
        {
            return new Matrix4x3(
                new Vector3(scaleX, 0, 0),
                new Vector3(0, scaleY, 0),
                new Vector3(0, 0, scaleZ),
                Vector3.Zero);
        }
        public static Matrix4x3 CreateScale(Vector3 scale)
        {
            return new Matrix4x3(
                new Vector3(scale.X, 0, 0),
                new Vector3(0, scale.Y, 0),
                new Vector3(0, 0, scale.Z),
                Vector3.Zero);
        }

        public static Matrix4x3 CreateTranslation(double xyz)
        {
            return new Matrix4x3(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 1),
                new Vector3(xyz));
        }
        public static Matrix4x3 CreateTranslation(double x, double y, double z)
        {
            return new Matrix4x3(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 1),
                new Vector3(x, y, z));
        }
        public static Matrix4x3 CreateTranslation(Vector3 xyz)
        {
            return new Matrix4x3(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 1),
                new Vector3(xyz.X, xyz.Y, xyz.Z));
        }

        public static Matrix4x3 CreateRotationX(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4x3(
                new Vector3(1, 0, 0),
                new Vector3(0, cos, sin),
                new Vector3(0, -sin, cos),
                Vector3.Zero);
        }
        public static Matrix4x3 CreateRotationY(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4x3(
                new Vector3(cos, 0, -sin),
                new Vector3(0, 1, 0),
                new Vector3(sin, 0, cos),
                Vector3.Zero);
        }
        public static Matrix4x3 CreateRotationZ(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix4x3(
                new Vector3(cos, sin, 0),
                new Vector3(-sin, cos, 0),
                new Vector3(0, 0, 1),
                Vector3.Zero);
        }

        public static Matrix4x3 CreateFromAxisAngle(Vector3 axis, Radian angle)
        {
            axis.Normalize();
            double axisX = axis.X, axisY = axis.Y, axisZ = axis.Z;

            double cos = Math.Cos(-angle);
            double sin = Math.Sin(-angle);
            double t = 1.0f - cos;

            double tXX = t * axisX * axisX;
            double tXY = t * axisX * axisY;
            double tXZ = t * axisX * axisZ;
            double tYY = t * axisY * axisY;
            double tYZ = t * axisY * axisZ;
            double tZZ = t * axisZ * axisZ;

            double sinX = sin * axisX;
            double sinY = sin * axisY;
            double sinZ = sin * axisZ;

            return new Matrix4x3(
                new Vector3(tXX + cos, tXY - sinZ, tXZ + sinY),
                new Vector3(tXY + sinZ, tYY + cos, tYZ - sinX),
                new Vector3(tXZ - sinY, tYZ + sinX, tZZ + cos),
                Vector3.Zero);
        }

        public static implicit operator Matrix4x3(Matrix4x3<double> matrix)
        {
            return new Matrix4x3(matrix.Data);
        }
        public static explicit operator Matrix4x3(Matrix4x3<float> matrix)
        {
            double[,] data = new double[3, 4]
            {
                { matrix[0, 0], matrix[0, 1], matrix[0, 2], matrix[0, 3] },
                { matrix[1, 0], matrix[1, 1], matrix[1, 2], matrix[1, 3] },
                { matrix[2, 0], matrix[2, 1], matrix[2, 2], matrix[2, 3] }
            };

            return new Matrix4x3(data);
        }

        public static implicit operator Matrix4x3<double>(Matrix4x3 matrix)
        {
            return new Matrix4x3<double>(matrix._matrix);
        }
        public static explicit operator Matrix4x3<float>(Matrix4x3 matrix)
        {
            float[,] data = new float[3, 4]
            {
                { (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2], (float)matrix[0, 3] },
                { (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2], (float)matrix[1, 3] },
                { (float)matrix[2, 0], (float)matrix[2, 1], (float)matrix[2, 2], (float)matrix[2, 3] }
            };

            return new Matrix4x3<float>(data);
        }
    }
}
