using System;

namespace Zene.Structs
{
    public struct Vector4<T> where T : unmanaged
    {
        public Vector4(T value)
        {
            X = value;
            Y = value;
            Z = value;
            W = value;
        }
        public Vector4(T x, T y, T z, T w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Vector4(Vector3<T> xyz, T w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }
        public Vector4(Vector2<T> xy, T z, T w)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
            W = w;
        }
        public Vector4(Vector2<T> xy, Vector2<T> zw)
        {
            X = xy.X;
            Y = xy.Y;
            Z = zw.X;
            W = zw.Y;
        }

        public Vector4(Vector4 xyzw)
        {
            X = (T)(object)xyzw.X;
            Y = (T)(object)xyzw.Y;
            Z = (T)(object)xyzw.Z;
            W = (T)(object)xyzw.W;
        }
        public Vector4(Vector4I xyzw)
        {
            X = (T)(object)xyzw.X;
            Y = (T)(object)xyzw.Y;
            Z = (T)(object)xyzw.Z;
            W = (T)(object)xyzw.W;
        }

        public T X { get; set; }
        public T Y { get; set; }
        public T Z { get; set; }
        public T W { get; set; }

        public override string ToString()
        {
            return $"X:{X}, Y:{Y}, Z:{Z}, W:{W}";
        }

        public override bool Equals(object obj)
        {
            return obj is Vector4<T> p &&
                X.Equals(p.X) && Y.Equals(p.Y) && Z.Equals(p.Z) && W.Equals(p.W);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Vector4<T> a, Vector4<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector4<T> a, Vector4<T> b)
        {
            return !a.Equals(b);
        }

        public static explicit operator Vector4<T>(Vector4 obj)
        {
            return new Vector4<T>(obj);
        }
        public static explicit operator Vector4<T>(Vector4I obj)
        {
            return new Vector4<T>(obj);
        }
        public static explicit operator Vector4<T>(Vector2<T> obj)
        {
            return new Vector4<T>(obj, default, default);
        }
        public static explicit operator Vector4<T>(Vector3<T> obj)
        {
            return new Vector4<T>(obj, default);
        }

        public static explicit operator Vector4(Vector4<T> obj)
        {
            return new Vector4((double)(object)obj.X, (double)(object)obj.Y, (double)(object)obj.Z, (double)(object)obj.W);
        }
        public static explicit operator Vector4I(Vector4<T> obj)
        {
            return new Vector4I((int)(object)obj.X, (int)(object)obj.Y, (int)(object)obj.Z, (int)(object)obj.W);
        }

        public static implicit operator Vector4<T>((T, T, T, T) v)
        {
            return new Vector4<T>(v.Item1, v.Item2, v.Item3, v.Item4);
        }
    }
}
