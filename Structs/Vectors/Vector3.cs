using System;

namespace Zene.Structs
{
    public struct Vector3
    {
        public Vector3(double value)
        {
            X = value;
            Y = value;
            Z = value;
        }
        public Vector3(int value)
        {
            X = value;
            Y = X;
            Z = X;
        }
        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector3(Vector3 xyz)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
        }
        public Vector3(Vector3I xyz)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
        }
        public Vector3(Vector2 xy, double z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }
        public Vector3(Vector2 xy, int z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }
        public Vector3(Vector2I xy, double z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }
        public Vector3(Vector2I xy, int z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double Length
        {
            get
            {
                return Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
            }
        }
        public double SquaredLength
        {
            get
            {
                return (X * X) + (Y * Y) + (Z * Z);
            }
        }

        public double Distance(Vector3 b)
        {
            return Math.Sqrt(SquaredDistance(b));
        }
        public double SquaredDistance(Vector3 b)
        {
            return ((b.X - X) * (b.X - X)) + ((b.Y - Y) * (b.Y - Y)) + ((b.Z - Z) * (b.Z - Z));
        }

        public double Dot(Vector3 b)
        {
            return (X * b.X) + (Y * b.Y) + (Z * b.Z);
        }

        public Vector3 Cross(Vector3 b)
        {
            return new Vector3(
                (Y * b.Z) - (Z * b.Y),
                (Z * b.X) - (X * b.Z),
                (X * b.Y) - (Y * b.X));
        }

        public Vector3 Lerp(Vector3 b, double blend)
        {
            return new Vector3(
                (blend * (b.X - X)) + X,
                (blend * (b.Y - Y)) + Y,
                (blend * (b.Z - Z)) + Z);
        }

        public Vector3 BaryCentric(Vector3 b, Vector3 c, double u, double v)
        {
            return (this + ((b - this) * u)) + ((c - this) * v);
        }

        public Vector3 Normalised()
        {
            if (Length == 0) { return Zero; }

            double scale = 1.0 / Length;
            return new Vector3(X * scale, Y * scale, Z * scale);
        }
        public void Normalise()
        {
            double scale = 1.0 / Length;

            X *= scale;
            Y *= scale;
            Z *= scale;
        }

        public Vector2 MultiplyMatrix(Matrix2x3 matrix)
        {
            return new Vector2(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y) + (matrix[0, 2] * Z),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y) + (matrix[1, 2] * Z));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z));
        }
        public Vector3 MultiplyMatrix(Matrix3 matrix)
        {
            return new Vector3(
                //(matrix[0, 0] * X) + (matrix[0, 1] * X) + (matrix[0, 2] * X),
                //(matrix[1, 0] * Y) + (matrix[1, 1] * Y) + (matrix[1, 2] * Y),
                //(matrix[2, 0] * Z) + (matrix[2, 1] * Z) + (matrix[2, 2] * Z));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z),
                (matrix[0, 2] * X) + (matrix[1, 2] * Y) + (matrix[2, 2] * Z));
        }
        public Vector4 MultiplyMatrix(Matrix4x3 matrix)
        {
            return new Vector4(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y) + (matrix[0, 2] * Z),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y) + (matrix[1, 2] * Z),
                //(matrix[2, 0] * X) + (matrix[2, 1] * Y) + (matrix[2, 2] * Z),
                //(matrix[3, 0] * X) + (matrix[3, 1] * Y) + (matrix[3, 2] * Z));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z),
                (matrix[0, 2] * X) + (matrix[1, 2] * Y) + (matrix[2, 2] * Z),
                (matrix[0, 3] * X) + (matrix[1, 3] * Y) + (matrix[2, 3] * Z));
        }

        public override string ToString()
        {
            return $"X:{X}, Y:{Y}, Z:{Z}";
        }
        public string ToString(string format)
        {
            return $"X:{X.ToString(format)}, Y:{Y.ToString(format)}, Z:{Z.ToString(format)}";
        }

        public override bool Equals(object obj)
        {
            return
                (
                    obj is Vector3 p &&
                    X == p.X && Y == p.Y && Z == p.Z
                ) ||
                (
                    obj is Vector3I pi &&
                    X == pi.X && Y == pi.Y && Z == pi.Z
                );
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(Vector3 a, Vector3I b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector3 a, Vector3I b)
        {
            return !a.Equals(b);
        }

        public static Vector3 operator +(Vector3 a, double b)
        {
            return new Vector3(a.X + b, a.Y + b, a.Z + b);
        }
        public static Vector3 operator +(double a, Vector3 b)
        {
            return b + a;
        }
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, double b)
        {
            return new Vector3(a.X - b, a.Y - b, a.Z - b);
        }
        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.X, -v.Y, -v.Z);
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator *(Vector3 a, double b)
        {
            return new Vector3(a.X * b, a.Y * b, a.Z * b);
        }
        public static Vector3 operator *(double a, Vector3 b)
        {
            return b * a;
        }
        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static Vector2 operator *(Vector3 a, Matrix2x3 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector2 operator *(Matrix2x3 a, Vector3 b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector3 operator *(Vector3 a, Matrix3 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector3 operator *(Matrix3 a, Vector3 b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector4 operator *(Vector3 a, Matrix4x3 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector4 operator *(Matrix4x3 a, Vector3 b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector3 operator /(Vector3 a, double b)
        {
            return new Vector3(a.X / b, a.Y / b, a.Z / b);
        }
        public static Vector3 operator /(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static explicit operator Vector3(Vector2 p)
        {
            return new Vector3(p, 0);
        }
        public static explicit operator Vector3(Vector4 p)
        {
            return new Vector3(p.X, p.Y, p.Z);
        }
        public static explicit operator Vector3(Vector2I p)
        {
            return new Vector3(p, 0);
        }
        public static explicit operator Vector3(Vector4I p)
        {
            return new Vector3(p.X, p.Y, p.Z);
        }
        public static implicit operator Vector3(Vector3I p)
        {
            return new Vector3(p);
        }

        public static Vector3 Zero { get; } = new Vector3(0, 0, 0);
        public static Vector3 One { get; } = new Vector3(1, 1, 1);
        public static Vector3 PositiveInfinity { get; } = new Vector3(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        public static Vector3 NegativeInfinity { get; } = new Vector3(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);

        public static Vector3 PlaneNormal(Vector3 a, Vector3 b, Vector3 c)
        {
            return (b - a).Cross(c - a).Normalised();
        }

        public static Vector3 operator +(Vector3 a, Vector2 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z);
        }
        public static Vector3 operator +(Vector3 a, Vector2I b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z);
        }
        public static Vector3 operator -(Vector3 a, Vector2 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z);
        }
        public static Vector3 operator -(Vector3 a, Vector2I b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z);
        }

        public static Vector3 operator +(Vector3I a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Vector3 operator -(Vector3I a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Vector3 operator *(Vector3I a, Vector3 b)
        {
            return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }
        public static Vector3 operator /(Vector3I a, Vector3 b)
        {
            return new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static implicit operator Vector3((double, double, double) v)
        {
            return new Vector3(v.Item1, v.Item2, v.Item3);
        }

        public static Vector3 Random(Random r)
        {
            return new Vector3(
                r.NextDouble(),
                r.NextDouble(),
                r.NextDouble());
        }
        public static Vector3 Random(Random r, double scale)
        {
            return new Vector3(
                r.NextDouble() * scale,
                r.NextDouble() * scale,
                r.NextDouble() * scale);
        }
        public static Vector3 Random(Random r, double min, double max)
        {
            return new Vector3(
                (r.NextDouble() * (max - min)) + min,
                (r.NextDouble() * (max - min)) + min,
                (r.NextDouble() * (max - min)) + min);
        }
        public static Vector3 Random(Random r, double minX, double maxX, double minY, double maxY, double minZ, double maxZ)
        {
            return new Vector3(
                (r.NextDouble() * (maxX - minX)) + minX,
                (r.NextDouble() * (maxY - minY)) + minY,
                (r.NextDouble() * (maxZ - minZ)) + minZ);
        }
    }
}
