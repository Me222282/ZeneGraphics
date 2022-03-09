using System;

namespace Zene.Structs
{
    public struct Colour3
    {
        public Colour3(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public Vector3 ToHsl()
        {
            double h;
            double s;
            double l;

            // Convert RGB to a 0.0 to 1.0 range.
            double r = R / 255.0;
            double g = G / 255.0;
            double b = B / 255.0;

            // Get the maximum and minimum RGB components.
            double max = r;
            if (max < g) max = g;
            if (max < b) max = b;

            double min = r;
            if (min > g) min = g;
            if (min > b) min = b;

            double diff = max - min;
            l = (max + min) / 2;
            if (Math.Abs(diff) < 0.00001)
            {
                s = 0;
                h = 0;  // H is really undefined.
            }
            else
            {
                if (l <= 0.5) s = diff / (max + min);
                else s = diff / (2 - max - min);

                double r_dist = (max - r) / diff;
                double g_dist = (max - g) / diff;
                double b_dist = (max - b) / diff;

                if (r == max) h = b_dist - g_dist;
                else if (g == max) h = 2 + r_dist - b_dist;
                else h = 4 + g_dist - r_dist;

                h *= 60;
                if (h < 0) h += 360;
            }

            return new Vector3(h, s, l);
        }
        public static Colour3 FromHsl(double h, double s, double l)
        {
            double p2;
            if (l <= 0.5) p2 = l * (1 + s);
            else p2 = l + s - l * s;

            double p1 = 2 * l - p2;
            double r, g, b;
            if (s == 0)
            {
                r = l;
                g = l;
                b = l;
            }
            else
            {
                r = Colour.QqhToRgb(p1, p2, h + 120);
                g = Colour.QqhToRgb(p1, p2, h);
                b = Colour.QqhToRgb(p1, p2, h - 120);
            }

            return new Colour3(
                (byte)(r * 255.0),
                (byte)(g * 255.0),
                (byte)(b * 255.0));
        }

#nullable enable
        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}";
        }
        public string ToString(string? format)
        {
            return $"R:{R.ToString(format)}, G:{G.ToString(format)}, B:{B.ToString(format)}";
        }
#nullable disable

        public override bool Equals(object obj)
        {
            return (obj is Colour3 c &&
                R == c.R &&
                G == c.G &&
                B == c.B) ||
                (obj is ColourF3 f &&
                (R * 255f) == f.R &&
                (G * 255f) == f.G &&
                (B * 255f) == f.B) ||
                (obj is ColourI3 i &&
                R == i.R &&
                G == i.G &&
                B == i.B);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B);
        }

        public static bool operator ==(Colour3 l, Colour3 r) => l.Equals(r);
        public static bool operator !=(Colour3 l, Colour3 r) => !l.Equals(r);

        public static bool operator ==(Colour3 l, ColourF3 r) => l.Equals(r);
        public static bool operator !=(Colour3 l, ColourF3 r) => !l.Equals(r);
        public static bool operator ==(Colour3 l, ColourI3 r) => l.Equals(r);
        public static bool operator !=(Colour3 l, ColourI3 r) => !l.Equals(r);

        public static implicit operator ColourF3(Colour3 c)
        {
            return new ColourF3(
                c.R * ColourF.ByteToFloat,
                c.G * ColourF.ByteToFloat,
                c.B * ColourF.ByteToFloat);
        }
        public static implicit operator ColourI3(Colour3 c)
        {
            return new ColourI3(c.R, c.G, c.B);
        }

        public static explicit operator Colour(Colour3 c)
        {
            return new Colour(c.R, c.G, c.B);
        }
        public static explicit operator ColourF(Colour3 c)
        {
            return new ColourF(
                c.R * ColourF.ByteToFloat,
                c.G * ColourF.ByteToFloat,
                c.B * ColourF.ByteToFloat);
        }
        public static explicit operator ColourI(Colour3 c)
        {
            return new ColourI(c.R, c.G, c.B);
        }

        public static explicit operator Vector3<byte>(Colour3 c)
        {
            return new Vector3<byte>(c.R, c.G, c.B);
        }
        public static explicit operator Colour3(Vector3<byte> v)
        {
            return new Colour3(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector3<int>(Colour3 c)
        {
            return new Vector3<int>(c.R, c.G, c.B);
        }
        public static explicit operator Colour3(Vector3<int> v)
        {
            return new Colour3((byte)v.X, (byte)v.Y, (byte)v.Z);
        }

        public static explicit operator Vector3<float>(Colour3 c)
        {
            ColourF3 cf = c;

            return new Vector3<float>(cf.R, cf.G, cf.B);
        }
        public static explicit operator Colour3(Vector3<float> v)
        {
            return new Colour3((byte)(v.X * 255), (byte)(v.Y * 255), (byte)(v.Z * 255));
        }

        public static explicit operator Vector3I(Colour3 c)
        {
            return new Vector3I(c.R, c.G, c.B);
        }
        public static explicit operator Colour3(Vector3I v)
        {
            return new Colour3((byte)v.X, (byte)v.Y, (byte)v.Z);
        }

        public static explicit operator Vector3(Colour3 c)
        {
            ColourF3 cf = c;

            return new Vector3(cf.R, cf.G, cf.B);
        }
        public static explicit operator Colour3(Vector3 v)
        {
            return new Colour3((byte)(v.X * 255), (byte)(v.Y * 255), (byte)(v.Z * 255));
        }

        public static Colour3 Zero { get; } = new Colour3(0, 0, 0);
    }
}
