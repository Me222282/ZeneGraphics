using System;

namespace Zene.Structs
{
    public struct Vector2<T> where T : unmanaged
    {
        public Vector2(T value)
        {
            X = value;
            Y = value;
        }
        public Vector2(T x, T y)
        {
            X = x;
            Y = y;
        }

        public Vector2(Vector2 xy)
        {
            X = (T)(object)xy.X;
            Y = (T)(object)xy.Y;
        }
        public Vector2(Vector2I xy)
        {
            X = (T)(object)xy.X;
            Y = (T)(object)xy.Y;
        }

        public T X { get; set; }
        public T Y { get; set; }

        public override string ToString()
        {
            return $"X:{X}, Y:{Y}";
        }

        public override bool Equals(object obj)
        {
            return
                obj is Vector2<T> p &&
                X.Equals(p.X) && Y.Equals(p.Y);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Vector2<T> a, Vector2<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector2<T> a, Vector2<T> b)
        {
            return !a.Equals(b);
        }

        public static explicit operator Vector2<T>(Vector2 obj)
        {
            return new Vector2<T>(obj);
        }
        public static explicit operator Vector2<T>(Vector2I obj)
        {
            return new Vector2<T>(obj);
        }
        public static explicit operator Vector2<T>(Vector3<T> obj)
        {
            return new Vector2<T>(obj.X, obj.Y);
        }
        public static explicit operator Vector2<T>(Vector4<T> obj)
        {
            return new Vector2<T>(obj.X, obj.Y);
        }

        public static explicit operator Vector2(Vector2<T> obj)
        {
            return new Vector2((double)(object)obj.X, (double)(object)obj.Y);
        }
        public static explicit operator Vector2I(Vector2<T> obj)
        {
            return new Vector2I((int)(object)obj.X, (int)(object)obj.Y);
        }

        public static implicit operator Vector2<T>((T, T) v)
        {
            return new Vector2<T>(v.Item1, v.Item2);
        }
    }
}
