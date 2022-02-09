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

        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}";
        }

        public static Colour3 Zero { get; } = new Colour3(0, 0, 0);

        public static Colour3 Random(Random r)
        {
            return new Colour3(
                (byte)r.Next(0, 256),
                (byte)r.Next(0, 256),
                (byte)r.Next(0, 256));
        }
    }
}
