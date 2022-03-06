using System;

namespace Zene.Structs
{
    public struct Vector4I
    {
        public Vector4I(int value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }
        public Vector4I(double value)
        {
            X = (int)value;
            Y = X;
            Z = X;
            W = X;
        }
        public Vector4I(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Vector4I(double x, double y, double z, double w)
        {
            X = (int)x;
            Y = (int)y;
            Z = (int)z;
            W = (int)w;
        }
        public Vector4I(Vector2 xy, Vector2 zw)
        {
            X = (int)xy.X;
            Y = (int)xy.Y;
            Z = (int)zw.X;
            W = (int)zw.Y;
        }
        public Vector4I(Vector2I xy, Vector2I zw)
        {
            X = xy.X;
            Y = xy.Y;
            Z = zw.X;
            W = zw.Y;
        }
        public Vector4I(Vector2 xy, int z, int w)
        {
            X = (int)xy.X;
            Y = (int)xy.Y;
            Z = z;
            W = w;
        }
        public Vector4I(Vector2 xy, double z, double w)
        {
            X = (int)xy.X;
            Y = (int)xy.Y;
            Z = (int)z;
            W = (int)w;
        }
        public Vector4I(Vector2I xy, int z, int w)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
            W = w;
        }
        public Vector4I(Vector2I xy, double z, double w)
        {
            X = xy.X;
            Y = xy.Y;
            Z = (int)z;
            W = (int)w;
        }
        public Vector4I(Vector3 xyz, int w)
        {
            X = (int)xyz.X;
            Y = (int)xyz.Y;
            Z = (int)xyz.Z;
            W = w;
        }
        public Vector4I(Vector3 xyz, double w)
        {
            X = (int)xyz.X;
            Y = (int)xyz.Y;
            Z = (int)xyz.Z;
            W = (int)w;
        }
        public Vector4I(Vector3I xyz, int w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }
        public Vector4I(Vector3I xyz, double w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = (int)w;
        }
        public Vector4I(Vector4I xyzw)
        {
            X = xyzw.X;
            Y = xyzw.Y;
            Z = xyzw.Z;
            W = xyzw.W;
        }
        public Vector4I(Vector4 xyzw)
        {
            X = (int)xyzw.X;
            Y = (int)xyzw.Y;
            Z = (int)xyzw.Z;
            W = (int)xyzw.W;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int W { get; set; }

        public double Length
        {
            get
            {
                return Math.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
            }
        }
        public int SquaredLength
        {
            get
            {
                return (X * X) + (Y * Y) + (Z * Z) + (W * W);
            }
        }

        public double Distance(Vector4I b)
        {
            return Math.Sqrt(SquaredDistance(b));
        }
        public int SquaredDistance(Vector4I b)
        {
            return ((b.X - X) * (b.X - X)) + ((b.Y - Y) * (b.Y - Y)) + ((b.Z - Z) * (b.Z - Z)) + ((b.W - W) * (b.W - W));
        }

        public int Dot(Vector4I b)
        {
            return (X * b.X) + (Y * b.Y) + (Z * b.Z) + (W * b.W);
        }

        public Vector4I Lerp(Vector4I b, double blend)
        {
            return new Vector4I(
                (blend * (b.X - X)) + X,
                (blend * (b.Y - Y)) + Y,
                (blend * (b.Z - Z)) + Z,
                (blend * (b.W - W)) + W);
        }

        public Vector4I BaryCentric(Vector4I b, Vector4I c, int u, int v)
        {
            return (this + ((b - this) * u)) + ((c - this) * v);
        }

        public Vector4I Normalised()
        {
            if (Length == 0) { return Zero; }

            double scale = 1.0 / Length;
            return new Vector4I(X * scale, Y * scale, Z * scale, W * scale);
        }
        public void Normalise()
        {
            double scale = 1.0 / Length;

            X = (int)(X * scale);
            Y = (int)(Y * scale);
            Z = (int)(Z * scale);
            W = (int)(W * scale);
        }

        public Vector2I MultiplyMatrix(Matrix2x4 matrix)
        {
            return new Vector2I(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y) + (matrix[0, 2] * Z) + (matrix[0, 3] * W),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y) + (matrix[1, 2] * Z) + (matrix[1, 3] * W));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z) + (matrix[3, 0] * W),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z) + (matrix[3, 1] * W));
        }
        public Vector3I MultiplyMatrix(Matrix3x4 matrix)
        {
            return new Vector3I(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y) + (matrix[0, 2] * Z) + (matrix[0, 3] * W),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y) + (matrix[1, 2] * Z) + (matrix[1, 3] * W),
                //(matrix[2, 0] * X) + (matrix[2, 1] * Y) + (matrix[2, 2] * Z) + (matrix[2, 3] * W));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z) + (matrix[3, 0] * W),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z) + (matrix[3, 1] * W),
                (matrix[0, 2] * X) + (matrix[1, 2] * Y) + (matrix[2, 2] * Z) + (matrix[3, 2] * W));
        }
        public Vector4I MultiplyMatrix(Matrix4 matrix)
        {
            return new Vector4I(
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
                    obj is Vector4I p &&
                    X == p.X && Y == p.Y && Z == p.Z && W == p.W
                ) ||
                (
                    obj is Vector4 pd &&
                    X == pd.X && Y == pd.Y && Z == pd.Z && W == pd.W
                );
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }

        public static bool operator ==(Vector4I a, Vector4I b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector4I a, Vector4I b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(Vector4I a, Vector4 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector4I a, Vector4 b)
        {
            return !a.Equals(b);
        }

        public static Vector4I operator +(Vector4I a, int b)
        {
            return new Vector4I(a.X + b, a.Y + b, a.Z + b, a.W + b);
        }
        public static Vector4I operator +(int a, Vector4I b)
        {
            return b + a;
        }
        public static Vector4I operator +(Vector4I a, Vector4I b)
        {
            return new Vector4I(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        public static Vector4I operator -(Vector4I a, int b)
        {
            return new Vector4I(a.X - b, a.Y - b, a.Z - b, a.W - b);
        }
        public static Vector4I operator -(Vector4I a, Vector4I b)
        {
            return new Vector4I(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }
        public static Vector4I operator -(Vector4I v)
        {
            return new Vector4I(-v.X, -v.Y, -v.Z, -v.W);
        }

        public static Vector4I operator *(Vector4I a, int b)
        {
            return new Vector4I(a.X * b, a.Y * b, a.Z * b, a.W * b);
        }
        public static Vector4I operator *(int a, Vector4I b)
        {
            return b * a;
        }
        public static Vector4I operator *(Vector4I a, Vector4I b)
        {
            return new Vector4I(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }

        public static Vector2I operator *(Vector4I a, Matrix2x4 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector2I operator *(Matrix2x4 a, Vector4I b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector3I operator *(Vector4I a, Matrix3x4 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector3I operator *(Matrix3x4 a, Vector4I b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector4I operator *(Vector4I a, Matrix4 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector4I operator *(Matrix4 a, Vector4I b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector4I operator /(Vector4I a, int b)
        {
            return new Vector4I(a.X / b, a.Y / b, a.Z / b, a.W / b);
        }
        public static Vector4I operator /(Vector4I a, Vector4I b)
        {
            return new Vector4I(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }

        public static implicit operator Vector4I(Vector2 p)
        {
            return new Vector4I(p, 1, 1);
        }
        public static implicit operator Vector4I(Vector3 p)
        {
            return new Vector4I(p, 1);
        }
        public static implicit operator Vector4I(Vector2I p)
        {
            return new Vector4I(p, 1, 1);
        }
        public static implicit operator Vector4I(Vector3I p)
        {
            return new Vector4I(p, 1);
        }
        public static implicit operator Vector4I(Vector4 p)
        {
            return new Vector4I(p);
        }

        public static Vector4I Zero { get; } = new Vector4I(0, 0, 0, 0);
        public static Vector4I One { get; } = new Vector4I(1, 1, 1, 1);

        public static Vector4I operator +(Vector4I a, Vector2 b)
        {
            return new Vector4I(a.X + b.X, a.Y + b.Y, a.Z, a.W);
        }
        public static Vector4I operator +(Vector4I a, Vector3 b)
        {
            return new Vector4I(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W);
        }
        public static Vector4I operator -(Vector4I a, Vector2 b)
        {
            return new Vector4I(a.X - b.X, a.Y - b.Y, a.Z, a.W);
        }
        public static Vector4I operator -(Vector4I a, Vector3 b)
        {
            return new Vector4I(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W);
        }
        public static Vector4I operator +(Vector4I a, Vector2I b)
        {
            return new Vector4I(a.X + b.X, a.Y + b.Y, a.Z, a.W);
        }
        public static Vector4I operator +(Vector4I a, Vector3I b)
        {
            return new Vector4I(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W);
        }
        public static Vector4I operator -(Vector4I a, Vector2I b)
        {
            return new Vector4I(a.X - b.X, a.Y - b.Y, a.Z, a.W);
        }
        public static Vector4I operator -(Vector4I a, Vector3I b)
        {
            return new Vector4I(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W);
        }

        public static Vector4I operator +(Vector4I a, Vector4 b)
        {
            return new Vector4I(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }
        public static Vector4I operator +(Vector4 a, Vector4I b)
        {
            return new Vector4I(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }
        public static Vector4I operator -(Vector4 a, Vector4I b)
        {
            return new Vector4I(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }
        public static Vector4I operator -(Vector4I a, Vector4 b)
        {
            return new Vector4I(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }
        public static Vector4I operator *(Vector4 a, Vector4I b)
        {
            return new Vector4I(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }
        public static Vector4I operator *(Vector4I a, Vector4 b)
        {
            return new Vector4I(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }
        public static Vector4I operator /(Vector4 a, Vector4I b)
        {
            return new Vector4I(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }
        public static Vector4I operator /(Vector4I a, Vector4 b)
        {
            return new Vector4I(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }

        public static Vector4I Random(Random r)
        {
            return new Vector4I(
                r.Next(),
                r.Next(),
                r.Next(),
                r.Next());
        }
        public static Vector4I Random(Random r, int min, int max)
        {
            return new Vector4I(
                r.Next(min, max),
                r.Next(min, max),
                r.Next(min, max),
                r.Next(min, max));
        }
        public static Vector4I Random(Random r, int minX, int maxX, int minY, int maxY, int minZ, int maxZ, int minW, int maxW)
        {
            return new Vector4I(
                r.Next(minX, maxX),
                r.Next(minY, maxY),
                r.Next(minZ, maxZ),
                r.Next(minW, maxW));
        }
    }
}
