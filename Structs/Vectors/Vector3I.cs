using System;

namespace Zene.Structs
{
    public struct Vector3I
    {
        public Vector3I(int value)
        {
            X = value;
            Y = value;
            Z = value;
        }
        public Vector3I(double value)
        {
            X = (int)value;
            Y = X;
            Z = X;
        }
        public Vector3I(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector3I(double x, double y, double z)
        {
            X = (int)x;
            Y = (int)y;
            Z = (int)z;
        }
        public Vector3I(Vector3 xyz)
        {
            X = (int)xyz.X;
            Y = (int)xyz.Y;
            Z = (int)xyz.Z;
        }
        public Vector3I(Vector3I xyz)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
        }
        public Vector3I(Vector2 xy, double z)
        {
            X = (int)xy.X;
            Y = (int)xy.Y;
            Z = (int)z;
        }
        public Vector3I(Vector2 xy, int z)
        {
            X = (int)xy.X;
            Y = (int)xy.Y;
            Z = z;
        }
        public Vector3I(Vector2I xy, double z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = (int)z;
        }
        public Vector3I(Vector2I xy, int z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public double Length
        {
            get
            {
                return Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
            }
        }
        public int SquaredLength
        {
            get
            {
                return (X * X) + (Y * Y) + (Z * Z);
            }
        }

        public double Distance(Vector3I b)
        {
            return Math.Sqrt(SquaredDistance(b));
        }
        public int SquaredDistance(Vector3I b)
        {
            return ((b.X - X) * (b.X - X)) + ((b.Y - Y) * (b.Y - Y)) + ((b.Z - Z) * (b.Z - Z));
        }

        public int Dot(Vector3I b)
        {
            return (X * b.X) + (Y * b.Y) + (Z * b.Z);
        }

        public Vector3I Cross(Vector3I b)
        {
            return new Vector3I(
                (Y * b.Z) - (Z * b.Y),
                (Z * b.X) - (X * b.Z),
                (X * b.Y) - (Y * b.X));
        }

        public Vector3I Lerp(Vector3I b, double blend)
        {
            return new Vector3I(
                (blend * (b.X - X)) + X,
                (blend * (b.Y - Y)) + Y,
                (blend * (b.Z - Z)) + Z);
        }

        public Vector3I BaryCentric(Vector3I b, Vector3I c, int u, int v)
        {
            return (this + ((b - this) * u)) + ((c - this) * v);
        }

        public Vector3I Normalised()
        {
            if (Length == 0) { return Zero; }

            double scale = 1.0 / Length;
            return new Vector3I(X * scale, Y * scale, Z * scale);
        }
        public void Normalise()
        {
            double scale = 1.0 / Length;

            X = (int)(X * scale);
            Y = (int)(Y * scale);
            Z = (int)(Z * scale);
        }

        public Vector2I MultiplyMatrix(Matrix2x3 matrix)
        {
            return new Vector2I(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y) + (matrix[0, 2] * Z),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y) + (matrix[1, 2] * Z));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z));
        }
        public Vector3I MultiplyMatrix(Matrix3 matrix)
        {
            return new Vector3I(
                //(matrix[0, 0] * X) + (matrix[0, 1] * Y) + (matrix[0, 2] * Z),
                //(matrix[1, 0] * X) + (matrix[1, 1] * Y) + (matrix[1, 2] * Z),
                //(matrix[2, 0] * X) + (matrix[2, 1] * Y) + (matrix[2, 2] * Z));
                (matrix[0, 0] * X) + (matrix[1, 0] * Y) + (matrix[2, 0] * Z),
                (matrix[0, 1] * X) + (matrix[1, 1] * Y) + (matrix[2, 1] * Z),
                (matrix[0, 2] * X) + (matrix[1, 2] * Y) + (matrix[2, 2] * Z));
        }
        public Vector4I MultiplyMatrix(Matrix4x3 matrix)
        {
            return new Vector4I(
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
                    obj is Vector3I p &&
                    X == p.X && Y == p.Y && Z == p.Z
                ) ||
                (
                    obj is Vector3 pd &&
                    X == pd.X && Y == pd.Y && Z == pd.Z
                );
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static bool operator ==(Vector3I a, Vector3I b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector3I a, Vector3I b)
        {
            return !a.Equals(b);
        }
        public static bool operator ==(Vector3I a, Vector3 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector3I a, Vector3 b)
        {
            return !a.Equals(b);
        }

        public static Vector3I operator +(Vector3I a, int b)
        {
            return new Vector3I(a.X + b, a.Y + b, a.Z + b);
        }
        public static Vector3I operator +(int a, Vector3I b)
        {
            return b + a;
        }
        public static Vector3I operator +(Vector3I a, Vector3I b)
        {
            return new Vector3I(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3I operator -(Vector3I a, int b)
        {
            return new Vector3I(a.X - b, a.Y - b, a.Z - b);
        }
        public static Vector3I operator -(Vector3I a, Vector3I b)
        {
            return new Vector3I(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Vector3I operator -(Vector3I v)
        {
            return new Vector3I(-v.X, -v.Y, -v.Z);
        }

        public static Vector3I operator *(Vector3I a, int b)
        {
            return new Vector3I(a.X * b, a.Y * b, a.Z * b);
        }
        public static Vector3I operator *(int a, Vector3I b)
        {
            return b * a;
        }
        public static Vector3I operator *(Vector3I a, Vector3I b)
        {
            return new Vector3I(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static Vector2I operator *(Vector3I a, Matrix2x3 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector2I operator *(Matrix2x3 a, Vector3I b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector3I operator *(Vector3I a, Matrix3 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector3I operator *(Matrix3 a, Vector3I b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector4I operator *(Vector3I a, Matrix4x3 b)
        {
            return a.MultiplyMatrix(b);
        }
        public static Vector4I operator *(Matrix4x3 a, Vector3I b)
        {
            return b.MultiplyMatrix(a);
        }

        public static Vector3I operator /(Vector3I a, int b)
        {
            return new Vector3I(a.X / b, a.Y / b, a.Z / b);
        }
        public static Vector3I operator /(Vector3I a, Vector3I b)
        {
            return new Vector3I(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static implicit operator Vector3I(Vector2 p)
        {
            return new Vector3I(p, 0);
        }
        public static explicit operator Vector3I(Vector4 p)
        {
            return new Vector3I(p.X, p.Y, p.Z);
        }
        public static implicit operator Vector3I(Vector2I p)
        {
            return new Vector3I(p, 0);
        }
        public static explicit operator Vector3I(Vector4I p)
        {
            return new Vector3I(p.X, p.Y, p.Z);
        }
        public static explicit operator Vector3I(Vector3 p)
        {
            return new Vector3I(p);
        }

        public static Vector3I Zero { get; } = new Vector3I(0, 0, 0);
        public static Vector3I One { get; } = new Vector3I(1, 1, 1);

        public static Vector3I operator +(Vector3I a, Vector2 b)
        {
            return new Vector3I(a.X + b.X, a.Y + b.Y, a.Z);
        }
        public static Vector3I operator +(Vector3I a, Vector2I b)
        {
            return new Vector3I(a.X + b.X, a.Y + b.Y, a.Z);
        }
        public static Vector3I operator -(Vector3I a, Vector2 b)
        {
            return new Vector3I(a.X - b.X, a.Y - b.Y, a.Z);
        }
        public static Vector3I operator -(Vector3I a, Vector2I b)
        {
            return new Vector3I(a.X - b.X, a.Y - b.Y, a.Z);
        }

        public static Vector3I operator +(Vector3I a, Vector3 b)
        {
            return new Vector3I(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Vector3I operator +(Vector3 a, Vector3I b)
        {
            return new Vector3I(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Vector3I operator -(Vector3 a, Vector3I b)
        {
            return new Vector3I(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Vector3I operator -(Vector3I a, Vector3 b)
        {
            return new Vector3I(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Vector3I operator *(Vector3 a, Vector3I b)
        {
            return new Vector3I(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }
        public static Vector3I operator *(Vector3I a, Vector3 b)
        {
            return new Vector3I(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }
        public static Vector3I operator /(Vector3 a, Vector3I b)
        {
            return new Vector3I(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }
        public static Vector3I operator /(Vector3I a, Vector3 b)
        {
            return new Vector3I(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static Vector3I Random(Random r)
        {
            return new Vector3I(
                r.Next(),
                r.Next(),
                r.Next());
        }
        public static Vector3I Random(Random r, int min, int max)
        {
            return new Vector3I(
                r.Next(min, max),
                r.Next(min, max),
                r.Next(min, max));
        }
        public static Vector3I Random(Random r, int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
        {
            return new Vector3I(
                r.Next(minX, maxX),
                r.Next(minY, maxY),
                r.Next(minZ, maxZ));
        }
    }
}
