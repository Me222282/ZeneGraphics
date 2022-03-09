using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that holds a RGBA colour value as integers.
    /// </summary>
    public struct ColourI
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
        public ColourI(int r, int g, int b)
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
        public ColourI(int r, int g, int b, int a)
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
        public ColourI(ColourI3 colour, int alpha)
        {
            R = colour.R;
            G = colour.G;
            B = colour.B;
            A = alpha;
        }
        /// <summary>
        /// Create a colour from an already defined colour stored as bytes with an opacity.
        /// </summary>
        /// <param name="colour">The colour containing the RGB values as bytes.</param>
        /// <param name="alpha">The alpha component of the colour.</param>
        public ColourI(Colour3 colour, int alpha)
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
        public ColourI(ColourF3 colour, int alpha)
        {
            R = (int)(colour.R * 255);
            G = (int)(colour.G * 255);
            B = (int)(colour.B * 255);
            A = alpha;
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
        /// The alpha component of the colour.
        /// </summary>
        public int A { get; set; }

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
        public static ColourI FromHsl(double h, double s, double l)
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

            return new ColourI(
                (int)(r * 255.0),
                (int)(g * 255.0),
                (int)(b * 255.0));
        }
        /// <summary>
        /// Creates a colour from HLS values.
        /// </summary>
        /// <param name="h">The hue of the colour.</param>
        /// <param name="s">The saturation of the colour.</param>
        /// <param name="l">The luminosity of the colour.</param>
        /// <param name="a">THe alpha component of the colour.</param>
        public static ColourI FromHsl(double h, double s, double l, int a)
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

            return new ColourI(
                (int)(r * 255.0),
                (int)(g * 255.0),
                (int)(b * 255.0),
                a);
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
            return (obj is ColourI i &&
                R == i.R &&
                G == i.G &&
                B == i.B &&
                A == i.A) ||
                (obj is Colour c &&
                R == c.R &&
                G == c.G &&
                B == c.B &&
                A == c.A) ||
                (obj is ColourF f &&
                (R * 255f) == f.R &&
                (G * 255f) == f.G &&
                (B * 255f) == f.B &&
                (A * 255f) == f.A);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B, A);
        }

        public static bool operator ==(ColourI l, ColourI r) => l.Equals(r);
        public static bool operator !=(ColourI l, ColourI r) => !l.Equals(r);

        public static bool operator ==(ColourI l, Colour r) => l.Equals(r);
        public static bool operator !=(ColourI l, Colour r) => !l.Equals(r);
        public static bool operator ==(ColourI l, ColourF r) => l.Equals(r);
        public static bool operator !=(ColourI l, ColourF r) => !l.Equals(r);

        public static implicit operator ColourF(ColourI c)
        {
            return new ColourF(
                c.R * ColourF.ByteToFloat,
                c.G * ColourF.ByteToFloat,
                c.B * ColourF.ByteToFloat,
                c.A * ColourF.ByteToFloat);
        }
        public static explicit operator Colour(ColourI c)
        {
            return new Colour((byte)c.R, (byte)c.G, (byte)c.B, (byte)c.A);
        }

        public static explicit operator ColourF3(ColourI c)
        {
            return new ColourF3(
                c.R * ColourF.ByteToFloat,
                c.G * ColourF.ByteToFloat,
                c.B * ColourF.ByteToFloat);
        }
        public static explicit operator Colour3(ColourI c)
        {
            return new Colour3((byte)c.R, (byte)c.G, (byte)c.B);
        }
        public static explicit operator ColourI3(ColourI c)
        {
            return new ColourI3(c.R, c.G, c.B);
        }

        public static explicit operator Vector4<byte>(ColourI c)
        {
            return new Vector4<byte>((byte)c.R, (byte)c.G, (byte)c.B, (byte)c.A);
        }
        public static explicit operator ColourI(Vector4<byte> v)
        {
            return new ColourI(v.X, v.Y, v.Z, v.W);
        }

        public static explicit operator Vector4<int>(ColourI c)
        {
            return new Vector4<int>(c.R, c.G, c.B, c.A);
        }
        public static explicit operator ColourI(Vector4<int> v)
        {
            return new ColourI(v.X, v.Y, v.Z, v.W);
        }

        public static explicit operator Vector4<float>(ColourI c)
        {
            ColourF cf = c;

            return new Vector4<float>(cf.R, cf.G, cf.B, cf.A);
        }
        public static explicit operator ColourI(Vector4<float> v)
        {
            return new ColourI((int)(v.X * 255), (int)(v.Y * 255), (int)(v.Z * 255), (int)(v.W * 255));
        }

        public static explicit operator Vector4I(ColourI c)
        {
            return new Vector4I(c.R, c.G, c.B, c.A);
        }
        public static explicit operator ColourI(Vector4I v)
        {
            return new ColourI(v.X, v.Y, v.Z, v.W);
        }

        public static explicit operator Vector4(ColourI c)
        {
            ColourF cf = c;

            return new Vector4(cf.R, cf.G, cf.B, cf.A);
        }
        public static explicit operator ColourI(Vector4 v)
        {
            return new ColourI((int)(v.X * 255), (int)(v.Y * 255), (int)(v.Z * 255), (int)(v.W * 255));
        }

        /// <summary>
        /// A colour that has all components set to 0.
        /// </summary>
        public static ColourI Zero { get; } = new ColourI(0, 0, 0, 0);
    }
}
