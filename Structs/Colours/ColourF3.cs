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
