using System;

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

namespace Zene.Structs
{
    public struct Radian
    {
        public Radian(double radians)
        {
            _radian = radians;
        }

        private readonly double _radian;

        public override string ToString()
        {
            return _radian.ToString();
        }
        public string ToString(string? format)
        {
            return _radian.ToString(format);
        }
        public string ToString(IFormatProvider? provider)
        {
            return _radian.ToString(provider);
        }
        public string ToString(string? format, IFormatProvider? provider)
        {
            return _radian.ToString(format, provider);
        }

        public static Radian Degrees(double degrees)
        {
            return new Radian(degrees / 180 * Math.PI);
        }

        public static Radian Percent(double percent)
        {
            return new Radian(percent * 2 * Math.PI);
        }

        public static implicit operator double(Radian r)
        {
            return r._radian;
        }
        public static implicit operator Radian(double d)
        {
            return new Radian(d);
        }

        public static Radian operator -(Radian r)
        {
            return new Radian(-r._radian);
        }
    }
}
