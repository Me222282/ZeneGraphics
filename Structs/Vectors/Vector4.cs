using System;

namespace Zene.Structs
{
    public struct Vector4
    {
        public Vector4(int value)
        {
            X = value;
            Y = X;
            Z = X;
            W = X;
        }
        public Vector4(double value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }
        public Vector4(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Vector4(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Vector4(Vector2 xy, Vector2 zw)
        {
            X = xy.X;
            Y = xy.Y;
            Z = zw.X;
            W = zw.Y;
        }
        public Vector4(Vector2I xy, Vector2I zw)
        {
            X = xy.X;
            Y = xy.Y;
            Z = zw.X;
            W = zw.Y;
        }
        public Vector4(Vector2 xy, int z, int w)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
            W = w;
        }
        public Vector4(Vector2 xy, double z, double w)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
            W = w;
        }
        public Vector4(Vector2I xy, int z, int w)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
            W = w;
        }
        public Vector4(Vector2I xy, double z, double w)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
            W = w;
        }
        public Vector4(Vector3 xyz, int w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }
        public Vector4(Vector3 xyz, double w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }
        public Vector4(Vector3I xyz, int w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }
        public Vector4(Vector3I xyz, double w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }
        public Vector4(Vector4I xyzw)
        {
            X = xyzw.X;
            Y = xyzw.Y;
            Z = xyzw.Z;
            W = xyzw.W;
        }
        public Vector4(Vector4 xyzw)
        {
            X = xyzw.X;
            Y = xyzw.Y;
            Z = xyzw.Z;
            W = xyzw.W;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }

        public double Length
        {
            get
            {
                return Math.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
            }
        }
        public double SquaredLength
        {
            get
            {
                return (X * X) + (Y * Y) + (Z * Z) + (W * W);
            }
        }

        public double Distance(Vector4 b)
        {
            return Math.Sqrt(SquaredDistance(b));
        }
        public double SquaredDistance(Vector4 b)
        {
            return ((b.X - X) * (b.X - X)) + ((b.Y - Y) * (b.Y - Y)) + ((b.Z - Z) * (b.Z - Z)) + ((b.W - W) * (b.W - W));
        }

        public double Dot(Vector4 b)
        {
            return (X * b.X) + (Y * b.Y) + (Z * b.Z) + (W * b.W);
        }

        public Vector4 Lerp(Vector4 b, double blend)
        {
            return new Vector4(
                (blend * (b.X - X)) + X,
                (blend * (b.Y - Y)) + Y,
                (blend * (b.Z - Z)) + Z,
                (blend * (b.W - W)) + W);
        }

        public Vector4 BaryCentric(Vector4 b, Vector4 c, double u, double v)
        {
            return (this + ((b - this) * u)) + ((c - this) * v);
        }

        public Vector4 Normalized()
        {
            if (Length == 0) { return Zero; }

            double scale = 1.0 / Length;
            return new Vector4(X * scale, Y * scale, Z * scale, W * scale);
        }
        public void Normalize()
        {
            double scale = 1.0 / Length;

            X *= scale;
            Y *= scale;
            Z *= scale;
            W *= scale;
        }

        public Vector2 MultiplyMatrix(Matrix2x4 matrix)
        {
            return new Vector2(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y) + (matrix[0, 2] * Z) + (matrix[0, 3] * W),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y) + (matrix[1, 2] * Z) + (matrix[1, 3] * W));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z) + (matrix[3, 0] * W),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z) + (matrix[3, 1] * W));
        }
        public Vector3 MultiplyMatrix(Matrix3x4 matrix)
        {
            return new Vector3(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y) + (matrix[0, 2] * Z) + (matrix[0, 3] * W),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y) + (matrix[1, 2] * Z) + (matrix[1, 3] * W),
                //(matrix[2, 0] * X) + (matrix[2, 1] * Y) + (matrix[2, 2] * Z) + (matrix[2, 3] * W));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z) + (matrix[3, 0] * W),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z) + (matrix[3, 1] * W),
                (matrix[0, 2] * X) + (matrix[1, 2] * Y) + (matrix[2, 2] * Z) + (matrix[3, 2] * W));
        }
        public Vector4 MultiplyMatrix(Matrix4 matrix)
        {
            return new Vector4(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y) + (matrix[0, 2] * Z) + (matrix[0, 3] * W),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y) + (matrix[1, 2] * Z) + (matrix[1, 3] * W),
                //(matrix[2, 0] * X) + (matrix[2, 1] * Y) + (matrix[2, 2] * Z) + (matrix[2, 3] * W),
                //(matrix[3, 0] * X) + (matrix[3, 1] * Y) + (matrix[3, 2] * Z) + (matrix[3, 3] * W));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z) + (matrix[3, 0] * W),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z) + (matrix[3, 1] * W),
                (matrix[0, 2] * X) + (matrix[1, 2] * Y) + (matrix[2, 2] * Z) + (matrix[3, 2] * W),
                (matrix[0, 3] * X) + (matrix[1, 3] * Y) + (matrix[2, 3] * Z) + (matrix[3, 3] * W));
        }

        public override string ToString()
        {
            return $"X:{X}, Y:{Y}, Z:{Z}, W:{W}";
        }
        public string ToString(string format)
        {
            return $"X:{X.ToString(format)}, Y:{Y.ToString(format)}, Z:{Z.ToString(format)}, W:{W.ToString(format)}";
        }

        public override bool Equals(object obj)
        {
            return
                (
                    obj is Vector4 p &&
                    X == p.X && Y == p.Y && Z == p.Z && W == p.W
                ) ||
                (
                    obj is Vector4I pi &&
                    X == pi.X && Y == pi.Y && Z == pi.Z && W == pi.W
                );
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }

        public static bool operator ==(Vector4 a, Vector4 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector4 a, Vector4 b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(Vector4 a, Vector4I b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector4 a, Vector4I b)
        {
            return !a.Equals(b);
        }

        public static Vector4 operator +(Vector4 a, double b)
        {
            return new Vector4(a.X + b, a.Y + b, a.Z + b, a.W + b);
        }
        public static Vector4 operator +(double a, Vector4 b)
        {
            return b + a;
        }
        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        public static Vector4 operator -(Vector4 a, double b)
        {
            return new Vector4(a.X - b, a.Y - b, a.Z - b, a.W - b);
        }
        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }
        public static Vector4 operator -(Vector4 v)
        {
            return new Vector4(-v.X, -v.Y, -v.Z, -v.W);
        }

        public static Vector4 operator *(Vector4 a, double b)
        {
            return new Vector4(a.X * b, a.Y * b, a.Z * b, a.W * b);
        }
        public static Vector4 operator *(double a, Vector4 b)
        {
            return b * a;
        }
        public static Vector4 operator *(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }

        public static Vector2 operator *(Vector4 a, Matrix2x4 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector2 operator *(Matrix2x4 a, Vector4 b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector3 operator *(Vector4 a, Matrix3x4 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector3 operator *(Matrix3x4 a, Vector4 b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector4 operator *(Vector4 a, Matrix4 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector4 operator *(Matrix4 a, Vector4 b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector4 operator /(Vector4 a, double b)
        {
            return new Vector4(a.X / b, a.Y / b, a.Z / b, a.W / b);
        }
        public static Vector4 operator /(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }

        public static explicit operator Vector4(Vector2 p)
        {
            return new Vector4(p, 0, 1);
        }
        public static explicit operator Vector4(Vector3 p)
        {
            return new Vector4(p, 1);
        }
        public static explicit operator Vector4(Vector2I p)
        {
            return new Vector4(p, 0, 1);
        }
        public static explicit operator Vector4(Vector3I p)
        {
            return new Vector4(p, 1);
        }
        public static implicit operator Vector4(Vector4I p)
        {
            return new Vector4(p);
        }

        public static Vector4 Zero { get; } = new Vector4(0, 0, 0, 0);
        public static Vector4 One { get; } = new Vector4(1, 1, 1, 1);
        public static Vector4 PositiveInfinity { get; } = new Vector4(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        public static Vector4 NegativeInfinity { get; } = new Vector4(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);

        public static Vector4 operator +(Vector4 a, Vector2 b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z, a.W);
        }
        public static Vector4 operator +(Vector4 a, Vector3 b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W);
        }
        public static Vector4 operator -(Vector4 a, Vector2 b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z, a.W);
        }
        public static Vector4 operator -(Vector4 a, Vector3 b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W);
        }
        public static Vector4 operator +(Vector4 a, Vector2I b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z, a.W);
        }
        public static Vector4 operator +(Vector4 a, Vector3I b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W);
        }
        public static Vector4 operator -(Vector4 a, Vector2I b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z, a.W);
        }
        public static Vector4 operator -(Vector4 a, Vector3I b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W);
        }

        public static Vector4 operator +(Vector4 a, Vector4I b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }
        public static Vector4 operator +(Vector4I a, Vector4 b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }
        public static Vector4 operator -(Vector4I a, Vector4 b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }
        public static Vector4 operator -(Vector4 a, Vector4I b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }
        public static Vector4 operator *(Vector4I a, Vector4 b)
        {
            return new Vector4(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }
        public static Vector4 operator *(Vector4 a, Vector4I b)
        {
            return new Vector4(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }
        public static Vector4 operator /(Vector4I a, Vector4 b)
        {
            return new Vector4(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }
        public static Vector4 operator /(Vector4 a, Vector4I b)
        {
            return new Vector4(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }

        public static Vector4 Random(Random r)
        {
            return new Vector4(
                r.NextDouble(),
                r.NextDouble(),
                r.NextDouble(),
                r.NextDouble());
        }
        public static Vector4 Random(Random r, double scale)
        {
            return new Vector4(
                r.NextDouble() * scale,
                r.NextDouble() * scale,
                r.NextDouble() * scale,
                r.NextDouble() * scale);
        }
        public static Vector4 Random(Random r, double min, double max)
        {
            return new Vector4(
                (r.NextDouble() * (max - min)) + min,
                (r.NextDouble() * (max - min)) + min,
                (r.NextDouble() * (max - min)) + min,
                (r.NextDouble() * (max - min)) + min);
        }
        public static Vector4 Random(Random r, double minX, double maxX, double minY, double maxY, double minZ, double maxZ, double minW, double maxW)
        {
            return new Vector4(
                (r.NextDouble() * (maxX - minX)) + minX,
                (r.NextDouble() * (maxY - minY)) + minY,
                (r.NextDouble() * (maxZ - minZ)) + minZ,
                (r.NextDouble() * (maxW - minW)) + minW);
        }
    }
}
