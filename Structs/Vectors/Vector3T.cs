using System;

namespace Zene.Structs
{
    public struct Vector3<T> where T : unmanaged
    {
        public Vector3(T value)
        {
            X = value;
            Y = value;
            Z = value;
        }
        public Vector3(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector3(Vector2<T> xy, T z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }

        public Vector3(Vector3 xyz)
        {
            X = (T)(object)xyz.X;
            Y = (T)(object)xyz.Y;
            Z = (T)(object)xyz.Z;
        }
        public Vector3(Vector3I xyz)
        {
            X = (T)(object)xyz.X;
            Y = (T)(object)xyz.Y;
            Z = (T)(object)xyz.Z;
        }

        public T X { get; set; }
        public T Y { get; set; }
        public T Z { get; set; }

        public override string ToString()
        {
            return $"X:{X}, Y:{Y}, Z:{Z}";
        }

        public override bool Equals(object obj)
        {
            return
                obj is Vector3<T> p &&
                X.Equals(p.X) && Y.Equals(p.Y) && Z.Equals(p.Z);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Vector3<T> a, Vector3<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector3<T> a, Vector3<T> b)
        {
            return !a.Equals(b);
        }

        public static explicit operator Vector3<T>(Vector3 obj)
        {
            return new Vector3<T>(obj);
        }
        public static explicit operator Vector3<T>(Vector3I obj)
        {
            return new Vector3<T>(obj);
        }
        public static explicit operator Vector3<T>(Vector2<T> obj)
        {
            return new Vector3<T>(obj, default);
        }
        public static explicit operator Vector3<T>(Vector4<T> obj)
        {
            return new Vector3<T>(obj.X, obj.Y, obj.Z);
        }

        public static explicit operator Vector3(Vector3<T> obj)
        {
            return new Vector3((double)(object)obj.X, (double)(object)obj.Y, (double)(object)obj.Z);
        }
        public static explicit operator Vector3I(Vector3<T> obj)
        {
            return new Vector3I((int)(object)obj.X, (int)(object)obj.Y, (int)(object)obj.Z);
        }

        public static implicit operator Vector3<T>((T, T, T) v)
        {
            return new Vector3<T>(v.Item1, v.Item2, v.Item3);
        }
    }
}
