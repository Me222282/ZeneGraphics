using System;

namespace Zene.Structs
{
    public struct Degrees
    {
        internal const double _overPI = 1 / Math.PI;

        public Degrees(double degrees)
        {
            _degrees = degrees;
        }

        private readonly double _degrees;

#nullable enable
        public override string ToString()
        {
            return _degrees.ToString();
        }
        public string ToString(string? format)
        {
            return _degrees.ToString(format);
        }
#nullable disable

        public static Degrees Radian(double radian)
        {
            return new Degrees(radian * 180 * _overPI);
        }

        public static Degrees Percent(double percent)
        {
            return new Degrees(percent * 360);
        }

        public override bool Equals(object obj)
        {
            return (obj is double d && _degrees == d)
                ||
                (obj is float f && _degrees == f)
                ||
                (obj is Degrees deg && _degrees == deg._degrees)
                ||
                (obj is Radian r && _degrees == (r * 180 * _overPI));
        }
        public override int GetHashCode() => HashCode.Combine(_degrees);

        public static bool operator ==(Degrees l, Degrees r) => l.Equals(r);
        public static bool operator !=(Degrees l, Degrees r) => !l.Equals(r);

        public static implicit operator double(Degrees d) => d._degrees;
        public static implicit operator Degrees(double d) => new Degrees(d);
        public static implicit operator Radian(Degrees deg) => new Radian(deg._degrees * Structs.Radian._over180 * Math.PI);

        public static Degrees operator -(Degrees d) => new Degrees(-d._degrees);
    }
}
