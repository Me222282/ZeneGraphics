using System;

namespace Zene.Structs
{
    public struct Radian
    {
        internal const double _over180 = 1.0 / 180;

        public Radian(double radians)
        {
            _radian = radians;
        }

        private readonly double _radian;

#nullable enable
        public override string ToString() => _radian.ToString();
        public string ToString(string? format) => _radian.ToString(format);
#nullable disable

        public static Radian Degrees(double degrees)
        {
            return new Radian(degrees * _over180 * Math.PI);
        }

        public static Radian Percent(double percent)
        {
            return new Radian(percent * 2 * Math.PI);
        }

        public override bool Equals(object obj)
        {
            return (obj is double d && _radian == d)
                ||
                (obj is float f && _radian == f)
                ||
                (obj is Radian r && _radian == r._radian)
                ||
                (obj is Degrees deg && _radian == (deg * _over180 * Math.PI));
        }
        public override int GetHashCode() => HashCode.Combine(_radian);

        public static bool operator ==(Radian l, Radian r) => l.Equals(r);
        public static bool operator !=(Radian l, Radian r) => !l.Equals(r);

        public static implicit operator double(Radian r) => r._radian;
        public static implicit operator Radian(double d) => new Radian(d);
        public static implicit operator Degrees(Radian r) => new Degrees(r._radian * 180 * Structs.Degrees._overPI);

        public static Radian operator -(Radian r) => new Radian(-r._radian);
    }
}
