using System;

namespace Zene.Structs
{
    public struct ColourI3
    {
        public ColourI3(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

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

        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}";
        }

        public static ColourI3 Zero { get; } = new ColourI3(0, 0, 0);

        public static ColourI Random(Random r)
        {
            return new ColourI(
                r.Next(0, 256),
                r.Next(0, 256),
                r.Next(0, 256));
        }
    }
}
