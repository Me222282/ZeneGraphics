using System;

namespace Zene.Structs
{
    public struct ColourF3
    {
        public ColourF3(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }

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
        public static ColourF3 FromHsl(double h, double s, double l)
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

            return new ColourF3((float)r, (float)g, (float)b);
        }

        public static explicit operator Colour3(ColourF3 c)
        {
            return new Colour3(
                (byte)(c.R * 255),
                (byte)(c.G * 255),
                (byte)(c.B * 255));
        }

        public static explicit operator ColourI3(ColourF3 c)
        {
            return new ColourI3(
                (byte)(c.R * 255),
                (byte)(c.G * 255),
                (byte)(c.B * 255));
        }

        public static explicit operator ColourF(ColourF3 c)
        {
            return new ColourF(c.R, c.G, c.B);
        }
        public static explicit operator Colour(ColourF3 c)
        {
            return new Colour(
                (byte)(c.R * 255),
                (byte)(c.G * 255),
                (byte)(c.B * 255));
        }
        public static explicit operator ColourI(ColourF3 c)
        {
            return new ColourI(
                (int)(c.R * 255),
                (int)(c.G * 255),
                (int)(c.B * 255));
        }

        public static explicit operator Vector3<byte>(ColourF3 c)
        {
            return new Vector3<byte>((byte)(c.R * 255), (byte)(c.G * 255), (byte)(c.B * 255));
        }
        public static explicit operator ColourF3(Vector3<byte> v)
        {
            return new ColourF3(v.X * ColourF.ByteToFloat, v.Y * ColourF.ByteToFloat, v.Z * ColourF.ByteToFloat);
        }

        public static explicit operator Vector3<int>(ColourF3 c)
        {
            return new Vector3<int>((int)(c.R * 255), (int)(c.G * 255), (int)(c.B * 255));
        }
        public static explicit operator ColourF3(Vector3<int> v)
        {
            return new ColourF3(v.X * ColourF.ByteToFloat, v.Y * ColourF.ByteToFloat, v.Z * ColourF.ByteToFloat);
        }

        public static explicit operator Vector3<float>(ColourF3 c)
        {
            return new Vector3<float>(c.R, c.G, c.B);
        }
        public static explicit operator ColourF3(Vector3<float> v)
        {
            return new ColourF3(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector3I(ColourF3 c)
        {
            return new Vector3I((int)(c.R * 255), (int)(c.G * 255), (int)(c.B * 255));
        }
        public static explicit operator ColourF3(Vector3I v)
        {
            return new ColourF3(v.X * ColourF.ByteToFloat, v.Y * ColourF.ByteToFloat, v.Z * ColourF.ByteToFloat);
        }

        public static explicit operator Vector3(ColourF3 c)
        {
            return new Vector3(c.R, c.G, c.B);
        }
        public static explicit operator ColourF3(Vector3 v)
        {
            return new ColourF3((float)v.X, (float)v.Y, (float)v.Z);
        }

        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}";
        }

        public static ColourF3 Zero { get; } = new ColourF3(0, 0, 0);

        public static ColourF3 Random(Random r)
        {
            return new ColourF3(
                (float)r.NextDouble(),
                (float)r.NextDouble(),
                (float)r.NextDouble());
        }
    }
}
