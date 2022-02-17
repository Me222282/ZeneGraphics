using System;

namespace Zene.Structs
{
    public struct Vector2I
    {
        public Vector2I(int value)
        {
            X = value;
            Y = value;
        }
        public Vector2I(double value)
        {
            X = (int)value;
            Y = X;
        }
        public Vector2I(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Vector2I(double x, double y)
        {
            X = (int)x;
            Y = (int)y;
        }
        public Vector2I(Vector2 xy)
        {
            X = (int)xy.X;
            Y = (int)xy.Y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public double Length
        {
            get
            {
                return Math.Sqrt((X * X) + (Y * Y));
            }
        }
        public int SquaredLength
        {
            get
            {
                return (X * X) + (Y * Y);
            }
        }

        public Vector2I PerpendiclarRight
        {
            get
            {
                return new Vector2I(Y, -X);
            }
        }
        public Vector2I PerpendiclarLeft
        {
            get
            {
                return new Vector2I(-Y, X);
            }
        }

        public double Distance(Vector2I b)
        {
            return Math.Sqrt(SquaredDistance(b));
        }
        public int SquaredDistance(Vector2I b)
        {
            return ((b.X - X) * (b.X - X)) + ((b.Y - Y) * (b.Y - Y));
        }

        public int Dot(Vector2I b)
        {
            return (X * b.X) + (Y * b.Y);
        }
        public int PerpDot(Vector2I b)
        {
            return (X * b.Y) - (Y * b.X);
        }

        public Vector2I Lerp(Vector2I b, double blend)
        {
            return new Vector2I(
                (blend * (b.X - X)) + X,
                (blend * (b.Y - Y)) + Y);
        }

        public Vector2I BaryCentric(Vector2I b, Vector2I c, int u, int v)
        {
            return (this + ((b - this) * u)) + ((c - this) * v);
        }

        public Vector2I Normalized()
        {
            if (Length == 0) { return Zero; }

            double scale = 1.0 / Length;
            return new Vector2I(X * scale, Y * scale);
        }
        public void Normalize()
        {
            double scale = 1.0 / Length;

            X = (int)(X * scale);
            Y = (int)(Y * scale);
        }

        public Vector2I MultiplyMatrix(Matrix2 matrix)
        {
            return new Vector2I(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y));
        }
        public Vector3I MultiplyMatrix(Matrix3x2 matrix)
        {
            return new Vector3I(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y),
                //(matrix[2, 0] * X) + (matrix[2, 1] * Y));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y),
                (matrix[0, 2] * X) + (matrix[1, 2] * Y));
        }
        public Vector4I MultiplyMatrix(Matrix4x2 matrix)
        {
            return new Vector4I(
                //(matrix[0, 0] * X) + (matrix[0, 0] * Y),
                //(matrix[1, 0] * X) + (matrix[1, 0] * Y),
                //(matrix[2, 0] * X) + (matrix[2, 0] * Y),
                //(matrix[3, 0] * X) + (matrix[3, 0] * Y));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y),
                (matrix[0, 2] * X) + (matrix[1, 2] * Y),
                (matrix[0, 3] * X) + (matrix[1, 3] * Y));
        }

        public Vector2I Rotated90()
        {
            return new Vector2I(-Y, X);
        }
        public Vector2I Rotated270()
        {
            return new Vector2I(Y, -X);
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
        public Vector2I Relative(Line2I l)
        {
            return new Vector2I((X - l.GetX(Y)) / 2, (Y - l.GetY(X)) / 2);
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
                    obj is Vector2I p &&
                    X == p.X && Y == p.Y
                ) ||
                (
                    obj is Vector2 pd &&
                    X == pd.X && Y == pd.Y
                );
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Vector2I a, Vector2I b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector2I a, Vector2I b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(Vector2I a, Vector2 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector2I a, Vector2 b)
        {
            return !a.Equals(b);
        }

        public static Vector2I operator +(Vector2I a, int b)
        {
            return new Vector2I(a.X + b, a.Y + b);
        }
        public static Vector2I operator +(int a, Vector2I b)
        {
            return b + a;
        }
        public static Vector2I operator +(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2I operator -(Vector2I a, int b)
        {
            return new Vector2I(a.X - b, a.Y - b);
        }
        public static Vector2I operator -(Vector2I v)
        {
            return new Vector2I(-v.X, -v.Y);
        }
        public static Vector2I operator -(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2I operator *(Vector2I a, int b)
        {
            return new Vector2I(a.X * b, a.Y * b);
        }
        public static Vector2I operator *(int a, Vector2I b)
        {
            return b * a;
        }
        public static Vector2I operator *(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.X * b.X, a.Y * b.Y);
        }

        public static Vector2I operator *(Vector2I a, Matrix2 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector2I operator *(Matrix2 a, Vector2I b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector3I operator *(Vector2I a, Matrix3x2 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector3I operator *(Matrix3x2 a, Vector2I b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector4I operator *(Vector2I a, Matrix4x2 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector4I operator *(Matrix4x2 a, Vector2I b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector2I operator /(Vector2I a, int b)
        {
            return new Vector2I(a.X / b, a.Y / b);
        }
        public static Vector2I operator /(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.X / b.X, a.Y / b.Y);
        }

        public static explicit operator Vector2I(Vector3 p)
        {
            return new Vector2I(p.X, p.Y);
        }
        public static explicit operator Vector2I(Vector4 p)
        {
            return new Vector2I(p.X, p.Y);
        }
        public static explicit operator Vector2I(Vector3I p)
        {
            return new Vector2I(p.X, p.Y);
        }
        public static explicit operator Vector2I(Vector4I p)
        {
            return new Vector2I(p.X, p.Y);
        }
        public static explicit operator Vector2I(Vector2 p)
        {
            return new Vector2I(p);
        }

        public static Vector2I Zero { get; } = new Vector2I(0, 0);
        public static Vector2I One { get; } = new Vector2I(1, 1);

        public static Vector2I operator +(Vector2 a, Vector2I b)
        {
            return new Vector2I(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2I operator +(Vector2I a, Vector2 b)
        {
            return new Vector2I(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2I operator -(Vector2 a, Vector2I b)
        {
            return new Vector2I(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2I operator -(Vector2I a, Vector2 b)
        {
            return new Vector2I(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2I operator *(Vector2 a, Vector2I b)
        {
            return new Vector2I(a.X * b.X, a.Y * b.Y);
        }
        public static Vector2I operator *(Vector2I a, Vector2 b)
        {
            return new Vector2I(a.X * b.X, a.Y * b.Y);
        }
        public static Vector2I operator /(Vector2 a, Vector2I b)
        {
            return new Vector2I(a.X / b.X, a.Y / b.Y);
        }
        public static Vector2I operator /(Vector2I a, Vector2 b)
        {
            return new Vector2I(a.X / b.X, a.Y / b.Y);
        }

        public static Vector2I Random(Random r)
        {
            return new Vector2I(
                r.Next(),
                r.Next());
        }
        public static Vector2I Random(Random r, int min, int max)
        {
            return new Vector2I(
                r.Next(min, max),
                r.Next(min, max));
        }
        public static Vector2I Random(Random r, int minX, int maxX, int minY, int maxY)
        {
            return new Vector2I(
                r.Next(minX, maxX),
                r.Next(minY, maxY));
        }
    }
}
