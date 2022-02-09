using System;

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

namespace Zene.Structs
{
    public struct Degrees
    {
        public Degrees(double degrees)
        {
            _degrees = degrees;
        }

        private readonly double _degrees;

        public override string ToString()
        {
            return _degrees.ToString();
        }
        public string ToString(string? format)
        {
            return _degrees.ToString(format);
        }
        public string ToString(IFormatProvider? provider)
        {
            return _degrees.ToString(provider);
        }
        public string ToString(string? format, IFormatProvider? provider)
        {
            return _degrees.ToString(format, provider);
        }

        public static Degrees Radian(double radian)
        {
            return new Degrees(radian * 180 / Math.PI);
        }

        public static Degrees Percent(double percent)
        {
            return new Degrees(percent * 360);
        }

        public static implicit operator double(Degrees d)
        {
            return d._degrees;
        }
        public static implicit operator Degrees(double d)
        {
            return new Degrees(d);
        }

        public static Degrees operator -(Degrees d)
        {
            return new Degrees(-d._degrees);
        }
    }
}
