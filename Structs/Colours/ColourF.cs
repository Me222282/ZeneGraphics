using System;

namespace Zene.Structs
{
    public struct ColourF
    {
        public ColourF(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
            A = 1;
        }

        public ColourF(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public ColourF(Colour3 colour, float alpha)
        {
            R = colour.R * ByteToFloat;
            G = colour.G * ByteToFloat;
            B = colour.B * ByteToFloat;
            A = alpha;
        }

        public ColourF(ColourF3 colour, float alpha)
        {
            R = colour.R;
            G = colour.G;
            B = colour.B;
            A = alpha;
        }

        public ColourF(ColourI3 colour, float alpha)
        {
            R = colour.R * ByteToFloat;
            G = colour.G * ByteToFloat;
            B = colour.B * ByteToFloat;
            A = alpha;
        }

        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float A { get; set; }

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

        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}, A:{A}";
        }

        internal const float ByteToFloat = /*0.00392156862745098f*/ (float)1 / 255;

        public static ColourF Zero { get; } = new ColourF(0, 0, 0, 0);

        public static ColourF Random(Random r)
        {
            return new ColourF(
                (float)r.NextDouble(),
                (float)r.NextDouble(),
                (float)r.NextDouble());
        }
        public static ColourF RandomA(Random r)
        {
            return new ColourF(
                (float)r.NextDouble(),
                (float)r.NextDouble(),
                (float)r.NextDouble(),
                (float)r.NextDouble());
        }
    }
}
