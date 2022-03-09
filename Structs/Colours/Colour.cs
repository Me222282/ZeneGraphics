using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that holds a RGBA colour value as bytes.
    /// </summary>
    public struct Colour
    {
        /// <summary>
        /// Creates a colour from RGB values.
        /// </summary>
        /// <remarks>
        /// Alpha has a value of 255.
        /// </remarks>
        /// <param name="r">The red component of the colour.</param>
        /// <param name="g">The green component of the colour.</param>
        /// <param name="b">The blue component of the colour.</param>
        public Colour(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = 255;
        }
        /// <summary>
        /// Creates a colour from RGBA values.
        /// </summary>
        /// <param name="r">The red component of the colour.</param>
        /// <param name="g">The green component of the colour.</param>
        /// <param name="b">The blue component of the colour.</param>
        /// <param name="a">The alpha component of the colour.</param>
        public Colour(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Create a colour from an already defined colour with an opacity.
        /// </summary>
        /// <param name="colour">The colour containing the RGB values.</param>
        /// <param name="alpha">The alpha component of the colour.</param>
        public Colour(Colour3 colour, byte alpha)
        {
            R = colour.R;
            G = colour.G;
            B = colour.B;
            A = alpha;
        }
        /// <summary>
        /// Create a colour from an already defined colour stored as floats with an opacity.
        /// </summary>
        /// <param name="colour">The colour containing the RGB values as floats.</param>
        /// <param name="alpha">The alpha component of the colour.</param>
        public Colour(ColourF3 colour, byte alpha)
        {
            R = (byte)(colour.R * 255);
            G = (byte)(colour.G * 255);
            B = (byte)(colour.B * 255);
            A = alpha;
        }
        /// <summary>
        /// Create a colour from an already defined colour stored as integers with an opacity.
        /// </summary>
        /// <param name="colour">The colour containing the RGB values as integers.</param>
        /// <param name="alpha">The alpha component of the colour.</param>
        public Colour(ColourI3 colour, byte alpha)
        {
            R = (byte)colour.R;
            G = (byte)colour.G;
            B = (byte)colour.B;
            A = alpha;
        }

        /// <summary>
        /// The red component of the colour.
        /// </summary>
        public byte R { get; set; }
        /// <summary>
        /// The green component of the colour.
        /// </summary>
        public byte G { get; set; }
        /// <summary>
        /// The blue component of the colour.
        /// </summary>
        public byte B { get; set; }
        /// <summary>
        /// The alpha component of the colour.
        /// </summary>
        public byte A { get; set; }

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
        /// <remarks>
        /// Alpha has a value of 255.
        /// </remarks>
        /// <param name="h">The hue of the colour.</param>
        /// <param name="s">The saturation of the colour.</param>
        /// <param name="l">The luminosity of the colour.</param>
        public static Colour FromHsl(double h, double s, double l)
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
                r = QqhToRgb(p1, p2, h + 120);
                g = QqhToRgb(p1, p2, h);
                b = QqhToRgb(p1, p2, h - 120);
            }

            return new Colour(
                (byte)(r * 255.0),
                (byte)(g * 255.0),
                (byte)(b * 255.0));
        }
        /// <summary>
        /// Creates a colour from HLS values and opacity.
        /// </summary>
        /// <param name="h">The hue of the colour.</param>
        /// <param name="s">The saturation of the colour.</param>
        /// <param name="l">The luminosity of the colour.</param>
        /// <param name="a">THe alpha component of the colour.</param>
        public static Colour FromHsl(double h, double s, double l, byte a)
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
                r = QqhToRgb(p1, p2, h + 120);
                g = QqhToRgb(p1, p2, h);
                b = QqhToRgb(p1, p2, h - 120);
            }

            return new Colour(
                (byte)(r * 255.0),
                (byte)(g * 255.0),
                (byte)(b * 255.0),
                a);
        }
        internal static double QqhToRgb(double q1, double q2, double hue)
        {
            if (hue > 360) hue -= 360;
            else if (hue < 0) hue += 360;

            if (hue < 60) return q1 + (q2 - q1) * hue / 60;
            if (hue < 180) return q2;
            if (hue < 240) return q1 + (q2 - q1) * (240 - hue) / 60;
            return q1;
        }

#nullable enable
        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}, A:{A}";
        }
        public string ToString(string? format)
        {
            return $"R:{R.ToString(format)}, G:{G.ToString(format)}, B:{B.ToString(format)}, A:{A.ToString(format)}";
        }
#nullable disable

        public override bool Equals(object obj)
        {
            return (obj is Colour c &&
                R == c.R &&
                G == c.G &&
                B == c.B &&
                A == c.A) ||
                (obj is ColourF f &&
                (R * 255f) == f.R &&
                (G * 255f) == f.G &&
                (B * 255f) == f.B &&
                (A * 255f) == f.A) ||
                (obj is ColourI i &&
                R == i.R &&
                G == i.G &&
                B == i.B &&
                A == i.A);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B, A);
        }

        public static bool operator ==(Colour l, Colour r) => l.Equals(r);
        public static bool operator !=(Colour l, Colour r) => !l.Equals(r);

        public static bool operator ==(Colour l, ColourF r) => l.Equals(r);
        public static bool operator !=(Colour l, ColourF r) => !l.Equals(r);
        public static bool operator ==(Colour l, ColourI r) => l.Equals(r);
        public static bool operator !=(Colour l, ColourI r) => !l.Equals(r);

        public static implicit operator ColourF(Colour c)
        {
            return new ColourF(
                c.R * ColourF.ByteToFloat,
                c.G * ColourF.ByteToFloat,
                c.B * ColourF.ByteToFloat,
                c.A * ColourF.ByteToFloat);
        }
        public static implicit operator ColourI(Colour c)
        {
            return new ColourI(c.R, c.G, c.B, c.A);
        }

        public static explicit operator ColourF3(Colour c)
        {
            return new ColourF3(
                c.R * ColourF.ByteToFloat,
                c.G * ColourF.ByteToFloat,
                c.B * ColourF.ByteToFloat);
        }
        public static explicit operator Colour3(Colour c)
        {
            return new Colour3(c.R, c.G, c.B);
        }
        public static explicit operator ColourI3(Colour c)
        {
            return new ColourI3(c.R, c.G, c.B);
        }

        public static explicit operator Vector4<byte>(Colour c)
        {
            return new Vector4<byte>(c.R, c.G, c.B, c.A);
        }
        public static explicit operator Colour(Vector4<byte> v)
        {
            return new Colour(v.X, v.Y, v.Z, v.W);
        }

        public static explicit operator Vector4<int>(Colour c)
        {
            return new Vector4<int>(c.R, c.G, c.B, c.A);
        }
        public static explicit operator Colour(Vector4<int> v)
        {
            return new Colour((byte)v.X, (byte)v.Y, (byte)v.Z, (byte)v.W);
        }

        public static explicit operator Vector4<float>(Colour c)
        {
            ColourF cf = c;

            return new Vector4<float>(cf.R, cf.G, cf.B, cf.A);
        }
        public static explicit operator Colour(Vector4<float> v)
        {
            return new Colour((byte)(v.X * 255), (byte)(v.Y * 255), (byte)(v.Z * 255), (byte)(v.W * 255));
        }

        public static explicit operator Vector4I(Colour c)
        {
            return new Vector4I(c.R, c.G, c.B, c.A);
        }
        public static explicit operator Colour(Vector4I v)
        {
            return new Colour((byte)v.X, (byte)v.Y, (byte)v.Z, (byte)v.W);
        }

        public static explicit operator Vector4(Colour c)
        {
            ColourF cf = c;

            return new Vector4(cf.R, cf.G, cf.B, cf.A);
        }
        public static explicit operator Colour(Vector4 v)
        {
            return new Colour((byte)(v.X * 255), (byte)(v.Y * 255), (byte)(v.Z * 255), (byte)(v.W * 255));
        }

        /// <summary>
        /// A colour that has all components set to 0.
        /// </summary>
        public static Colour Zero { get; } = new Colour(0, 0, 0, 0);
    }
}
