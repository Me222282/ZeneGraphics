using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that holds a RGB colour value as integers.
    /// </summary>
    public struct ColourI3
    {
        /// <summary>
        /// Creates a colour from RGB values.
        /// </summary>
        /// <param name="r">The red component of the colour.</param>
        /// <param name="g">The green component of the colour.</param>
        /// <param name="b">The blue component of the colour.</param>
        public ColourI3(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// The red component of the colour.
        /// </summary>
        public int R { get; set; }
        /// <summary>
        /// The green component of the colour.
        /// </summary>
        public int G { get; set; }
        /// <summary>
        /// The blue component of the colour.
        /// </summary>
        public int B { get; set; }

        /// <summary>
        /// Returns this colour stored as HSL values.
        /// </summary>
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
        /// <summary>
        /// Creates a colour from HLS values.
        /// </summary>
        /// <param name="h">The hue of the colour.</param>
        /// <param name="s">The saturation of the colour.</param>
        /// <param name="l">The luminosity of the colour.</param>
        public static ColourI3 FromHsl(double h, double s, double l)
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

            return new ColourI3(
                (int)(r * 255.0),
                (int)(g * 255.0),
                (int)(b * 255.0));
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
            return (obj is ColourI3 i &&
                R == i.R &&
                G == i.G &&
                B == i.B) ||
                (obj is Colour3 c &&
                R == c.R &&
                G == c.G &&
                B == c.B) ||
                (obj is ColourF3 f &&
                (R * 255f) == f.R &&
                (G * 255f) == f.G &&
                (B * 255f) == f.B);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B);
        }

        public static bool operator ==(ColourI3 l, ColourI3 r) => l.Equals(r);
        public static bool operator !=(ColourI3 l, ColourI3 r) => !l.Equals(r);

        public static bool operator ==(ColourI3 l, Colour3 r) => l.Equals(r);
        public static bool operator !=(ColourI3 l, Colour3 r) => !l.Equals(r);
        public static bool operator ==(ColourI3 l, ColourF3 r) => l.Equals(r);
        public static bool operator !=(ColourI3 l, ColourF3 r) => !l.Equals(r);

        public static implicit operator ColourF3(ColourI3 c)
        {
            return new ColourF3(
                c.R * ColourF.ByteToFloat,
                c.G * ColourF.ByteToFloat,
                c.B * ColourF.ByteToFloat);
        }
        public static explicit operator Colour3(ColourI3 c)
        {
            return new Colour3((byte)c.R, (byte)c.G, (byte)c.B);
        }

        public static explicit operator ColourI(ColourI3 c)
        {
            return new ColourI(c.R, c.G, c.B);
        }
        public static explicit operator Colour(ColourI3 c)
        {
            return new Colour((byte)c.R, (byte)c.G, (byte)c.B);
        }
        public static explicit operator ColourF(ColourI3 c)
        {
            return new ColourF(
                c.R * ColourF.ByteToFloat,
                c.G * ColourF.ByteToFloat,
                c.B * ColourF.ByteToFloat);
        }

        public static explicit operator Vector3<byte>(ColourI3 c)
        {
            return new Vector3<byte>((byte)c.R, (byte)c.G, (byte)c.B);
        }
        public static explicit operator ColourI3(Vector3<byte> v)
        {
            return new ColourI3(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector3<int>(ColourI3 c)
        {
            return new Vector3<int>(c.R, c.G, c.B);
        }
        public static explicit operator ColourI3(Vector3<int> v)
        {
            return new ColourI3(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector3<float>(ColourI3 c)
        {
            ColourF3 cf = c;

            return new Vector3<float>(cf.R, cf.G, cf.B);
        }
        public static explicit operator ColourI3(Vector3<float> v)
        {
            return new ColourI3((int)(v.X * 255), (int)(v.Y * 255), (int)(v.Z * 255));
        }

        public static explicit operator Vector3I(ColourI3 c)
        {
            return new Vector3I(c.R, c.G, c.B);
        }
        public static explicit operator ColourI3(Vector3I v)
        {
            return new ColourI3(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector3(ColourI3 c)
        {
            ColourF3 cf = c;

            return new Vector3(cf.R, cf.G, cf.B);
        }
        public static explicit operator ColourI3(Vector3 v)
        {
            return new ColourI3((int)(v.X * 255), (int)(v.Y * 255), (int)(v.Z * 255));
        }

        /// <summary>
        /// A colour that has all components set to 0.
        /// </summary>
        public static ColourI3 Zero { get; } = new ColourI3(0, 0, 0);
    }
}
