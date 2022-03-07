using System;

namespace Zene.Structs
{
    public struct Matrix3 : IMatrix<double>
    {
        public Matrix3(Vector3 row0, Vector3 row1, Vector3 row2)
        {
            _matrix = new double[,]
            {
                { row0.X, row1.X, row2.X },
                { row0.Y, row1.Y, row2.Y },
                { row0.Z, row1.Z, row2.Z }
            };
        }

        public Matrix3(double[,] matrix)
        {
            _matrix = new double[3, 3];

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    try
                    {
                        _matrix[x, y] = matrix[x, y];
                    }
                    catch (Exception)
                    {
                        throw new Exception("Matrix needs to have at least 3 rows and 3 columns.");
                    }
                }
            }
        }
        public Matrix3(double[] matrix)
        {
            _matrix = new double[3, 3];

            if (matrix.Length < 9)
            {
                throw new Exception("Matrix needs to have at least 3 rows and 3 columns.");
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
        }

        private readonly double[,] _matrix;

        public double[,] Data => _matrix;
        int IMatrix<double>.RowSize => 3;
        int IMatrix<double>.ColumnSize => 3;

        public double this[int x, int y]
        {
            get
            {
                if (x >= 3 || y >= 3)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 3 x 3 range of matrix3.");
                }

                return _matrix[x, y];
            }
            set
            {
                if (x >= 3 || y >= 3)
                {
                    throw new IndexOutOfRangeException($"X: {x} and Y: {y} are outside the 3 x 3 range of matrix3.");
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
                _matrix[2, 0] = value.Z;
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
                _matrix[2, 1] = value.Z;
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
                _matrix[2, 2] = value.Z;
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

        public Vector3 Column2
        {
            get
            {
                return new Vector3(_matrix[2, 0], _matrix[2, 1], _matrix[2, 2]);
            }
            set
            {
                _matrix[2, 0] = value.X;
                _matrix[2, 1] = value.Y;
                _matrix[2, 2] = value.Z;
            }
        }

        public Matrix3 Add(Matrix3 matrix)
        {
            return new Matrix3(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] + matrix[0, 0], /*x:0 y:1*/this[0, 1] + matrix[0, 1], /*x:0 y:2*/this[0, 2] + matrix[0, 2] },
                { /*x:1 y:0*/this[1, 0] + matrix[1, 0], /*x:1 y:1*/this[1, 1] + matrix[1, 1], /*x:1 y:2*/this[1, 2] + matrix[1, 2] },
                { /*x:2 y:0*/this[2, 0] + matrix[2, 0], /*x:2 y:1*/this[2, 1] + matrix[2, 1], /*x:2 y:2*/this[2, 2] + matrix[2, 2] }
            });
        }

        public Matrix3 Subtract(Matrix3 matrix)
        {
            return new Matrix3(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] - matrix[0, 0], /*x:0 y:1*/this[0, 1] - matrix[0, 1], /*x:0 y:2*/this[0, 2] - matrix[0, 2] },
                { /*x:1 y:0*/this[1, 0] - matrix[1, 0], /*x:1 y:1*/this[1, 1] - matrix[1, 1], /*x:1 y:2*/this[1, 2] - matrix[1, 2] },
                { /*x:2 y:0*/this[2, 0] - matrix[2, 0], /*x:2 y:1*/this[2, 1] - matrix[2, 1], /*x:2 y:2*/this[2, 2] - matrix[2, 2] }
            });
        }

        public Matrix3 Multiply(double value)
        {
            return new Matrix3(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] * value, /*x:0 y:1*/this[0, 1] * value, /*x:0 y:2*/this[0, 2] * value },
                { /*x:1 y:0*/this[1, 0] * value, /*x:1 y:1*/this[1, 1] * value, /*x:1 y:2*/this[1, 2] * value },
                { /*x:2 y:0*/this[2, 0] * value, /*x:2 y:1*/this[2, 1] * value, /*x:2 y:2*/this[2, 2] * value }
            });
        }

        public Matrix3 Multiply(Matrix3 matrix)
        {
            return new Matrix3(new double[,]
            {
                {   
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]) + (this[2, 0] * matrix[0, 2]), 
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) + (this[2, 1] * matrix[0, 2]),
                    /*x:0 y:2*/(this[0, 2] * matrix[0, 0]) + (this[1, 2] * matrix[0, 1]) + (this[2, 2] * matrix[0, 2]) 
                },
                {   
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]) + (this[2, 0] * matrix[1, 2]), 
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) + (this[2, 1] * matrix[1, 2]),
                    /*x:1 y:2*/(this[0, 2] * matrix[1, 0]) + (this[1, 2] * matrix[1, 1]) + (this[2, 2] * matrix[1, 2]) 
                },
                {   
                    /*x:2 y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]) + (this[2, 0] * matrix[2, 2]), 
                    /*x:2 y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]) + (this[2, 1] * matrix[2, 2]),
                    /*x:2 y:2*/(this[0, 2] * matrix[2, 0]) + (this[1, 2] * matrix[2, 1]) + (this[2, 2] * matrix[2, 2]) 
                }
            });
        }

        public Matrix3x2 Multiply(Matrix3x2 matrix)
        {
            return new Matrix3x2(new double[,]
            {
                {   
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]) + (this[2, 0] * matrix[0, 2]), 
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) + (this[2, 1] * matrix[0, 2]),
                    /*x:0 y:2*/(this[0, 2] * matrix[0, 0]) + (this[1, 2] * matrix[0, 1]) + (this[2, 2] * matrix[0, 2])
                },
                {   
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]) + (this[2, 0] * matrix[1, 2]), 
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) + (this[2, 1] * matrix[1, 2]),
                    /*x:1 y:2*/(this[0, 2] * matrix[1, 0]) + (this[1, 2] * matrix[1, 1]) + (this[2, 2] * matrix[1, 2])
                }
            });
        }

        public Matrix3x4 Multiply(Matrix3x4 matrix)
        {
            return new Matrix3x4(new double[,]
            {
                {   
                    /*x:0 y:0*/(this[0, 0] * matrix[0, 0]) + (this[1, 0] * matrix[0, 1]) + (this[2, 0] * matrix[0, 2]), 
                    /*x:0 y:1*/(this[0, 1] * matrix[0, 0]) + (this[1, 1] * matrix[0, 1]) + (this[2, 1] * matrix[0, 2]),
                    /*x:0 y:2*/(this[0, 2] * matrix[0, 0]) + (this[1, 2] * matrix[0, 1]) + (this[2, 2] * matrix[0, 2])
                },
                {   
                    /*x:1 y:0*/(this[0, 0] * matrix[1, 0]) + (this[1, 0] * matrix[1, 1]) + (this[2, 0] * matrix[1, 2]), 
                    /*x:1 y:1*/(this[0, 1] * matrix[1, 0]) + (this[1, 1] * matrix[1, 1]) + (this[2, 1] * matrix[1, 2]),
                    /*x:1 y:2*/(this[0, 2] * matrix[1, 0]) + (this[1, 2] * matrix[1, 1]) + (this[2, 2] * matrix[1, 2])
                },
                {   
                    /*x:2 y:0*/(this[0, 0] * matrix[2, 0]) + (this[1, 0] * matrix[2, 1]) + (this[2, 0] * matrix[2, 2]), 
                    /*x:2 y:1*/(this[0, 1] * matrix[2, 0]) + (this[1, 1] * matrix[2, 1]) + (this[2, 1] * matrix[2, 2]),
                    /*x:2 y:2*/(this[0, 2] * matrix[2, 0]) + (this[1, 2] * matrix[2, 1]) + (this[2, 2] * matrix[2, 2])
                },
                {   
                    /*x:3 y:0*/(this[0, 0] * matrix[3, 0]) + (this[1, 0] * matrix[3, 1]) + (this[2, 0] * matrix[3, 2]), 
                    /*x:3 y:1*/(this[0, 1] * matrix[3, 0]) + (this[1, 1] * matrix[3, 1]) + (this[2, 1] * matrix[3, 2]),
                    /*x:3 y:2*/(this[0, 2] * matrix[3, 0]) + (this[1, 2] * matrix[3, 1]) + (this[2, 2] * matrix[3, 2])
                }
            });
        }

        public Matrix Add(IMatrix<double> matrix)
        {
            if ((matrix.RowSize != 3) || (matrix.ColumnSize != 3))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 3 rows and 3 columns.");
            }

            Matrix output = new Matrix(new double[3, 3]);

            for (int x = 0; x < 3; x++)
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
            if ((matrix.RowSize != 3) || (matrix.ColumnSize != 3))
            {
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 3 rows and 3 columns.");
            }

            Matrix output = new Matrix(new double[3, 3]);

            for (int x = 0; x < 3; x++)
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
                throw new Exception($"{nameof(matrix)} doesn't have a compatable size. Must have 3 rows.");
            }

            Matrix output = new Matrix(new double[3, matrix.ColumnSize]);

            for (int x = 0; x < matrix.ColumnSize; x++)
            {
                for (int y = 0; y < 3; y++)
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

        public double Determinant()
        {
            return (this[0, 0] * this[1, 1] * this[2, 2]) + (this[1, 0] * this[2, 1] * this[0, 2]) + (this[2, 0] * this[0, 1] * this[1, 2])
                - (this[2, 0] * this[1, 1] * this[0, 2]) - (this[0, 0] * this[2, 1] * this[1, 2]) - (this[1, 0] * this[0, 1] * this[2, 2]);
        }

        public double Trace()
        {
            return this[0, 0] + this[1, 1] + this[2, 2];
        }

        public Matrix3 Normalize()
        {
            double det = Determinant();

            return new Matrix3(new double[,]
            {
                { /*x:0 y:0*/this[0, 0] / det, /*x:0 y:1*/this[0, 1] / det, /*x:0 y:2*/this[0, 2] / det },
                { /*x:1 y:0*/this[1, 0] / det, /*x:1 y:1*/this[1, 1] / det, /*x:1 y:2*/this[1, 2] / det },
                { /*x:2 y:0*/this[2, 0] / det, /*x:2 y:1*/this[2, 1] / det, /*x:2 y:2*/this[2, 2] / det }
            });
        }

        public Matrix3 Invert()
        {
            double det = Determinant();

            if (det == 0)
            {
                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            }

            double invDet = 1.0 / det;

            return new Matrix3(new double[,]
            {
                {
                    /*x:0 y:0*/((+this[1, 1] * this[2, 2]) - (this[2, 1] * this[1, 2])) * invDet, //
                    /*x:0 y:1*/((-this[0, 1] * this[2, 2]) + (this[2, 1] * this[0, 2])) * invDet, //
                    /*x:0 y:2*/((+this[0, 1] * this[1, 2]) - (this[1, 1] * this[0, 2])) * invDet  //
                },
                {
                    /*x:1 y:0*/((-this[1, 0] * this[2, 2]) + (this[2, 0] * this[1, 2])) * invDet, //
                    /*x:1 y:1*/((+this[0, 0] * this[2, 2]) - (this[2, 0] * this[0, 2])) * invDet, 
                    /*x:1 y:2*/((-this[0, 0] * this[1, 2]) + (this[1, 0] * this[0, 2])) * invDet  
                },
                {
                    /*x:2 y:0*/((+this[1, 0] * this[2, 1]) - (this[2, 0] * this[1, 1])) * invDet, 
                    /*x:2 y:1*/((-this[0, 0] * this[2, 1]) + (this[2, 0] * this[0, 1])) * invDet, 
                    /*x:2 y:2*/((+this[0, 0] * this[1, 1]) - (this[1, 0] * this[0, 1])) * invDet  
                }
            });
        }

        public Matrix3 Transpose()
        {
            return new Matrix3(new double[,]
            {
                { /*x:0 y:0*/this[0, 0], /*x:0 y:1*/this[1, 0], /*x:0 y:2*/this[2, 0] },
                { /*x:1 y:0*/this[0, 1], /*x:1 y:1*/this[1, 0], /*x:1 y:2*/this[2, 1] },
                { /*x:2 y:0*/this[0, 2], /*x:2 y:1*/this[1, 2], /*x:2 y:2*/this[2, 2] }
            });
        }

        public override bool Equals(object obj)
        {
            return obj is Matrix3 matrix &&
                _matrix == matrix._matrix;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        public ReadOnlySpan<float> GetGLData()
        {
            int w = 3;
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
            return $@"[{_matrix[0, 0]}, {_matrix[1, 0]}, {_matrix[2, 0]}]
[{_matrix[0, 1]}, {_matrix[1, 1]}, {_matrix[2, 1]}]
[{_matrix[0, 2]}, {_matrix[1, 2]}, {_matrix[2, 2]}]";
        }
        public string ToString(string format)
        {
            return $@"[{_matrix[0, 0].ToString(format)}, {_matrix[1, 0].ToString(format)}, {_matrix[2, 0].ToString(format)}]
[{_matrix[0, 1].ToString(format)}, {_matrix[1, 1].ToString(format)}, {_matrix[2, 1].ToString(format)}]
[{_matrix[0, 2].ToString(format)}, {_matrix[1, 2].ToString(format)}, {_matrix[2, 2].ToString(format)}]";
        }

        public static bool operator ==(Matrix3 a, Matrix3 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Matrix3 a, Matrix3 b)
        {
            return !a.Equals(b);
        }

        public static Matrix3 operator +(Matrix3 a, Matrix3 b)
        {
            return a.Add(b);
        }

        public static Matrix3 operator -(Matrix3 a, Matrix3 b)
        {
            return a.Subtract(b);
        }

        public static Matrix3 operator *(Matrix3 a, Matrix3 b)
        {
            return a.Multiply(b);
        }

        public static Matrix3x2 operator *(Matrix3 a, Matrix3x2 b)
        {
            return a.Multiply(b);
        }

        public static Matrix3x4 operator *(Matrix3 a, Matrix3x4 b)
        {
            return a.Multiply(b);
        }

        public static Matrix3 operator *(Matrix3 a, double b)
        {
            return a.Multiply(b);
        }

        public static Matrix3 operator *(double a, Matrix3 b)
        {
            return b.Multiply(a);
        }

        public static Matrix operator +(Matrix3 a, IMatrix<double> b)
        {
            return a.Add(b);
        }
        public static Matrix operator -(Matrix3 a, IMatrix<double> b)
        {
            return a.Subtract(b);
        }
        public static Matrix operator *(Matrix3 a, IMatrix<double> b)
        {
            return a.Multiply(b);
        }

        public static Matrix3 CreateRotation(Vector3 axis, Radian angle)
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

            return new Matrix3(
                new Vector3(tXX + cos, tXY - sinZ, tXZ + sinY),
                new Vector3(tXY + sinZ, tYY + cos, tYZ - sinX),
                new Vector3(tXZ - sinY, tYZ + sinX, tZZ + cos));
        }

        public static Matrix3 CreateRotationX(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix3(
                new Vector3(1, 0, 0),
                new Vector3(0, cos, sin),
                new Vector3(0, -sin, cos));
        }

        public static Matrix3 CreateRotationY(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix3(
                new Vector3(cos, 0, -sin),
                new Vector3(0, 1, 0),
                new Vector3(sin, 0, cos));
        }

        public static Matrix3 CreateRotationZ(Radian angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            return new Matrix3(
                new Vector3(cos, sin, 0),
                new Vector3(-sin, cos, 0),
                new Vector3(0, 0, 1));
        }

        public static Matrix3 CreateScale(double scale)
        {
            return new Matrix3(
                new Vector3(scale, 0, 0),
                new Vector3(0, scale, 0),
                new Vector3(0, 0, scale));
        }

        public static Matrix3 CreateScale(double scaleX, double scaleY, double scaleZ)
        {
            return new Matrix3(
                new Vector3(scaleX, 0, 0),
                new Vector3(0, scaleY, 0),
                new Vector3(0, 0, scaleZ));
        }

        public static Matrix3 CreateScale(Vector3 scale)
        {
            return CreateScale(scale.X, scale.Y, scale.Z);
        }

        public static Matrix3 CreateTranslation(double xy)
        {
            return new Matrix3(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(xy, xy, 1));
        }
        public static Matrix3 CreateTranslation(double x, double y)
        {
            return new Matrix3(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(x, y, 1));
        }
        public static Matrix3 CreateTranslation(Vector2 xy)
        {
            return CreateTranslation(xy.X, xy.Y);
        }

        public static implicit operator Matrix3(Matrix3<double> matrix)
        {
            return new Matrix3(matrix.Data);
        }
        public static explicit operator Matrix3(Matrix3<float> matrix)
        {
            double[,] data = new double[3, 3]
            {
                { matrix[0, 0], matrix[0, 1], matrix[0, 2] },
                { matrix[1, 0], matrix[1, 1], matrix[1, 2] },
                { matrix[2, 0], matrix[2, 1], matrix[2, 2] }
            };

            return new Matrix3(data);
        }

        public static implicit operator Matrix3<double>(Matrix3 matrix)
        {
            return new Matrix3<double>(matrix._matrix);
        }
        public static explicit operator Matrix3<float>(Matrix3 matrix)
        {
            float[,] data = new float[3, 3]
            {
                { (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2] },
                { (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2] },
                { (float)matrix[2, 0], (float)matrix[2, 1], (float)matrix[2, 2] }
            };

            return new Matrix3<float>(data);
        }
    }
}
