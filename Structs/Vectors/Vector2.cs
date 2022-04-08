using System;

namespace Zene.Structs
{
    public struct Vector2
    {
        public Vector2(int value)
        {
            X = value;
            Y = X;
        }
        public Vector2(double value)
        {
            X = value;
            Y = value;
        }
        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Vector2(Vector2I xy)
        {
            X = xy.X;
            Y = xy.Y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public double Length
        {
            get
            {
                return Math.Sqrt((X * X) + (Y * Y));
            }
        }
        public double SquaredLength
        {
            get
            {
                return (X * X) + (Y * Y);
            }
        }

        public Vector2 PerpendiclarRight
        {
            get
            {
                return new Vector2(Y, -X);
            }
        }
        public Vector2 PerpendiclarLeft
        {
            get
            {
                return new Vector2(-Y, X);
            }
        }

        public double Distance(Vector2 b)
        {
            return Math.Sqrt(SquaredDistance(b));
        }
        public double SquaredDistance(Vector2 b)
        {
            return ((b.X - X) * (b.X - X)) + ((b.Y - Y) * (b.Y - Y));
        }

        public double Dot(Vector2 b)
        {
            return (X * b.X) + (Y * b.Y);
        }
        public double PerpDot(Vector2 b)
        {
            return (X * b.Y) - (Y * b.X);
        }

        public Vector2 Lerp(Vector2 b, double blend)
        {
            return new Vector2(
                (blend * (b.X - X)) + X,
                (blend * (b.Y - Y)) + Y);
        }

        public Vector2 BaryCentric(Vector2 b, Vector2 c, double u, double v)
        {
            return (this + ((b - this) * u)) + ((c - this) * v);
        }

        public Vector2 Normalised()
        {
            if (Length == 0) { return Zero; }

            double scale = 1.0 / Length;
            return new Vector2(X * scale, Y * scale);
        }
        public void Normalise()
        {
            double scale = 1.0 / Length;
            X *= scale;
            Y *= scale;
        }

        public Vector2 MultiplyMatrix(Matrix2 matrix)
        {
            return new Vector2(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y));
        }
        public Vector3 MultiplyMatrix(Matrix3x2 matrix)
        {
            return new Vector3(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y),
                //(matrix[2, 0] * X) + (matrix[2, 1] * Y));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y),
                (matrix[0, 2] * X) + (matrix[1, 2] * Y));
        }
        public Vector4 MultiplyMatrix(Matrix4x2 matrix)
        {
            return new Vector4(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y),
                //(matrix[2, 0] * X) + (matrix[2, 1] * Y),
                //(matrix[3, 0] * X) + (matrix[3, 1] * Y));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y),
                (matrix[0, 2] * X) + (matrix[1, 2] * Y),
                (matrix[0, 3] * X) + (matrix[1, 3] * Y));
        }

        public Vector2 Rotated(Vector2 point, Radian angle)
        {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);

            // Translate to origin
            Vector2 newP = this - point;

            newP.X = (newP.X * cos) - (newP.Y * sin);
            newP.Y = (newP.X * sin) + (newP.Y * cos);

            return newP + point;
        }
        public Vector2 Rotated(Radian angle)
        {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);

            return new Vector2((X * cos) - (Y * sin), (X * sin) + (Y * cos));
        }
        public void Rotate(Vector2 point, Radian angle)
        {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);

            // Translate to origin
            X -= point.X;
            Y -= point.Y;

            X = ((X * cos) - (Y * sin)) + point.X;
            Y = (X * sin) + (Y * cos) + point.Y;
        }
        public void Rotate(Radian angle)
        {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);

            X = (X * cos) - (Y * sin);
            Y = (X * sin) + (Y * cos);
        }
        public Vector2 Rotated90()
        {
            return new Vector2(-Y, X);
        }
        public Vector2 Rotated270()
        {
            return new Vector2(Y, -X);
        }
        public void Rotate90()
        {
            X = -Y;
            Y = X;
        }
        public void Rotate270()
        {
            X = Y;
            Y = -X;
        }

        /// <summary>
        /// Determines the smallest distance a point is from a line.
        /// </summary>
        /// <param name="l">The line to compare to.</param>
        public Vector2 Relative(Line2 l)
        {
            return new Vector2((X - l.GetX(Y)) / 2, (Y - l.GetY(X)) / 2);
        }

        public override string ToString()
        {
            return $"X:{X}, Y:{Y}";
        }
        public string ToString(string format)
        {
            return $"X:{X.ToString(format)}, Y:{Y.ToString(format)}";
        }

        public override bool Equals(object obj)
        {
            return
                (
                    obj is Vector2 p &&
                    X == p.X && Y == p.Y
                ) ||
                (
                    obj is Vector2I pi &&
                    X == pi.X && Y == pi.Y
                );
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(Vector2 a, Vector2I b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector2 a, Vector2I b)
        {
            return !a.Equals(b);
        }

        public static Vector2 operator +(Vector2 a, double b)
        {
            return new Vector2(a.X + b, a.Y + b);
        }
        public static Vector2 operator +(double a, Vector2 b)
        {
            return b + a;
        }
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, double b)
        {
            return new Vector2(a.X - b, a.Y - b);
        }
        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.X, -v.Y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator *(Vector2 a, double b)
        {
            return new Vector2(a.X * b, a.Y * b);
        }
        public static Vector2 operator *(double a, Vector2 b)
        {
            return b * a;
        }
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }

        public static Vector2 operator *(Vector2 a, Matrix2 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector2 operator *(Matrix2 a, Vector2 b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector3 operator *(Vector2 a, Matrix3x2 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector3 operator *(Matrix3x2 a, Vector2 b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector4 operator *(Vector2 a, Matrix4x2 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector4 operator *(Matrix4x2 a, Vector2 b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector2 operator /(Vector2 a, double b)
        {
            return new Vector2(a.X / b, a.Y / b);
        }
        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X / b.X, a.Y / b.Y);
        }

        public static explicit operator Vector2(Vector3 p)
        {
            return new Vector2(p.X, p.Y);
        }
        public static explicit operator Vector2(Vector4 p)
        {
            return new Vector2(p.X, p.Y);
        }
        public static explicit operator Vector2(Vector3I p)
        {
            return new Vector2(p.X, p.Y);
        }
        public static explicit operator Vector2(Vector4I p)
        {
            return new Vector2(p.X, p.Y);
        }
        public static implicit operator Vector2(Vector2I p)
        {
            return new Vector2(p);
        }

        public static Vector2 Zero { get; } = new Vector2(0, 0);
        public static Vector2 One { get; } = new Vector2(1, 1);
        public static Vector2 PositiveInfinity { get; } = new Vector2(double.PositiveInfinity, double.PositiveInfinity);
        public static Vector2 NegativeInfinity { get; } = new Vector2(double.NegativeInfinity, double.NegativeInfinity);

        public static Vector2 operator +(Vector2 a, Vector2I b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2 operator -(Vector2 a, Vector2I b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2 operator /(Vector2 a, Vector2I b)
        {
            return new Vector2(a.X / b.X, a.Y / b.Y);
        }
        public static Vector2 operator *(Vector2 a, Vector2I b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }

        public static implicit operator Vector2((double, double) v)
        {
            return new Vector2(v.Item1, v.Item2);
        }

        public static Vector2 Random(Random r)
        {
            return new Vector2(
                r.NextDouble(),
                r.NextDouble());
        }
        public static Vector2 Random(Random r, double scale)
        {
            return new Vector2(
                r.NextDouble() * scale,
                r.NextDouble() * scale);
        }
        public static Vector2 Random(Random r, double min, double max)
        {
            return new Vector2(
                (r.NextDouble() * (max - min)) + min,
                (r.NextDouble() * (max - min)) + min);
        }
        public static Vector2 Random(Random r, double minX, double maxX, double minY, double maxY)
        {
            return new Vector2(
                (r.NextDouble() * (maxX - minX)) + minX,
                (r.NextDouble() * (maxY - minY)) + minY);
        }
    }
}
