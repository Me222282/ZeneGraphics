using System;

namespace Zene.Structs
{
    /// <summary>
    /// An object that holds a RGBA colour value as floats.
    /// </summary>
    public struct ColourF
    {
        /// <summary>
        /// Creates a colour from RGB values.
        /// </summary>
        /// <remarks>
        /// Alpha has a value of 1.0f.
        /// </remarks>
        /// <param name="r">The red component of the colour.</param>
        /// <param name="g">The green component of the colour.</param>
        /// <param name="b">The blue component of the colour.</param>
        public ColourF(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
            A = 1;
        }
        /// <summary>
        /// Creates a colour from RGBA values.
        /// </summary>
        /// <param name="r">The red component of the colour.</param>
        /// <param name="g">The green component of the colour.</param>
        /// <param name="b">The blue component of the colour.</param>
        /// <param name="a">The alpha component of the colour.</param>
        public ColourF(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Create a colour from an already defined colour stored as bytes with an opacity.
        /// </summary>
        /// <param name="colour">The colour containing the RGB values as bytes.</param>
        /// <param name="alpha">The alpha component of the colour.</param>
        public ColourF(Colour3 colour, float alpha)
        {
            R = colour.R * ByteToFloat;
            G = colour.G * ByteToFloat;
            B = colour.B * ByteToFloat;
            A = alpha;
        }
        /// <summary>
        /// Create a colour from an already defined colour with an opacity.
        /// </summary>
        /// <param name="colour">The colour containing the RGB values.</param>
        /// <param name="alpha">The alpha component of the colour.</param>
        public ColourF(ColourF3 colour, float alpha)
        {
            R = colour.R;
            G = colour.G;
            B = colour.B;
            A = alpha;
        }
        /// <summary>
        /// Create a colour from an already defined colour stored as integers with an opacity.
        /// </summary>
        /// <param name="colour">The colour containing the RGB values as integers.</param>
        /// <param name="alpha">The alpha component of the colour.</param>
        public ColourF(ColourI3 colour, float alpha)
        {
            R = colour.R * ByteToFloat;
            G = colour.G * ByteToFloat;
            B = colour.B * ByteToFloat;
            A = alpha;
        }

        /// <summary>
        /// The red component of the colour.
        /// </summary>
        public float R { get; set; }
        /// <summary>
        /// The green component of the colour.
        /// </summary>
        public float G { get; set; }
        /// <summary>
        /// The blue component of the colour.
        /// </summary>
        public float B { get; set; }
        /// <summary>
        /// The alpha component of the colour.
        /// </summary>
        public float A { get; set; }

        /// <summary>
        /// Returns this colour stored as HSL values.
        /// </summary>
        public Vector3 ToHsl()
        {
            double h;
            double s;
            double l;

            // Get the maximum and minimum RGB components.
            double max = R;
            if (max < G) max = G;
            if (max < B) max = B;

            double min = R;
            if (min > G) min = G;
            if (min > B) min = B;

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

                double r_dist = (max - R) / diff;
                double g_dist = (max - G) / diff;
                double b_dist = (max - B) / diff;

                if (R == max) h = b_dist - g_dist;
                else if (G == max) h = 2 + r_dist - b_dist;
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
        /// Alpha has a value of 1.0f.
        /// </remarks>
        /// <param name="h">The hue of the colour.</param>
        /// <param name="s">The saturation of the colour.</param>
        /// <param name="l">The luminosity of the colour.</param>
        public static ColourF FromHsl(double h, double s, double l)
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

            return new ColourF((float)r, (float)g, (float)b);
        }
        /// <summary>
        /// Creates a colour from HLS values.
        /// </summary>
        /// <param name="h">The hue of the colour.</param>
        /// <param name="s">The saturation of the colour.</param>
        /// <param name="l">The luminosity of the colour.</param>
        /// <param name="a">THe alpha component of the colour.</param>
        public static ColourF FromHsl(double h, double s, double l, float a)
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

            return new ColourF((float)r, (float)g, (float)b, a);
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
            return (obj is ColourF f &&
                R == f.R &&
                G == f.G &&
                B == f.B &&
                A == f.A) ||
                (obj is Colour c &&
                R == (c.R * 255f) &&
                G == (c.G * 255f) &&
                B == (c.B * 255f) &&
                A == (c.A * 255f)) ||
                (obj is ColourI i &&
                R == (i.R * 255f) &&
                G == (i.G * 255f) &&
                B == (i.B * 255f) &&
                A == (i.A * 255f));
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(R, G, B, A);
        }

        public static bool operator ==(ColourF l, ColourF r) => l.Equals(r);
        public static bool operator !=(ColourF l, ColourF r) => !l.Equals(r);

        public static bool operator ==(ColourF l, Colour r) => l.Equals(r);
        public static bool operator !=(ColourF l, Colour r) => !l.Equals(r);
        public static bool operator ==(ColourF l, ColourI r) => l.Equals(r);
        public static bool operator !=(ColourF l, ColourI r) => !l.Equals(r);

        public static explicit operator Colour(ColourF c)
        {
            return new Colour(
                (byte)(c.R * 255),
                (byte)(c.G * 255),
                (byte)(c.B * 255),
                (byte)(c.A * 255));
        }

        public static explicit operator ColourI(ColourF c)
        {
            return new ColourI(
                (int)(c.R * 255),
                (int)(c.G * 255),
                (int)(c.B * 255),
                (int)(c.A * 255));
        }

        public static explicit operator ColourF3(ColourF c)
        {
            return new ColourF3(c.R, c.G, c.B);
        }
        public static explicit operator Colour3(ColourF c)
        {
            return new Colour3(
                (byte)(c.R * 255),
                (byte)(c.G * 255),
                (byte)(c.B * 255));
        }
        public static explicit operator ColourI3(ColourF c)
        {
            return new ColourI3(
                (int)(c.R * 255),
                (int)(c.G * 255),
                (int)(c.B * 255));
        }

        public static explicit operator Vector4<byte>(ColourF c)
        {
            return new Vector4<byte>((byte)(c.R * 255), (byte)(c.G * 255), (byte)(c.B * 255), (byte)(c.A * 255));
        }
        public static explicit operator ColourF(Vector4<byte> v)
        {
            return new ColourF(v.X * ByteToFloat, v.Y * ByteToFloat, v.Z * ByteToFloat, v.W * ByteToFloat);
        }

        public static explicit operator Vector4<int>(ColourF c)
        {
            return new Vector4<int>((int)(c.R * 255), (int)(c.G * 255), (int)(c.B * 255), (int)(c.A * 255));
        }
        public static explicit operator ColourF(Vector4<int> v)
        {
            return new ColourF(v.X * ByteToFloat, v.Y * ByteToFloat, v.Z * ByteToFloat, v.W * ByteToFloat);
        }

        public static explicit operator Vector4<float>(ColourF c)
        {
            return new Vector4<float>(c.R, c.G, c.B, c.A);
        }
        public static explicit operator ColourF(Vector4<float> v)
        {
            return new ColourF(v.X, v.Y, v.Z, v.W);
        }

        public static explicit operator Vector4I(ColourF c)
        {
            return new Vector4I((int)(c.R * 255), (int)(c.G * 255), (int)(c.B * 255), (int)(c.A * 255));
        }
        public static explicit operator ColourF(Vector4I v)
        {
            return new ColourF(v.X * ByteToFloat, v.Y * ByteToFloat, v.Z * ByteToFloat, v.W * ByteToFloat);
        }

        public static explicit operator Vector4(ColourF c)
        {
            return new Vector4(c.R, c.G, c.B, c.A);
        }
        public static explicit operator ColourF(Vector4 v)
        {
            return new ColourF((float)v.X, (float)v.Y, (float)v.Z, (float)v.W);
        }

        internal const float ByteToFloat = /*0.00392156862745098f*/ (float)1 / 255;

        /// <summary>
        /// A colour that has all components set to 0.
        /// </summary>
        public static ColourF Zero { get; } = new ColourF(0, 0, 0, 0);
    }
}
