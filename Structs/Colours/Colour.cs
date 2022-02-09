using System;

namespace Zene.Structs
{
    public struct Colour
    {
        public Colour(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = 255;
        }
        public Colour(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Colour(Colour3 colour, byte alpha)
        {
            R = colour.R;
            G = colour.G;
            B = colour.B;
            A = alpha;
        }
        public Colour(ColourF3 colour, byte alpha)
        {
            R = (byte)(colour.R * 255);
            G = (byte)(colour.G * 255);
            B = (byte)(colour.B * 255);
            A = alpha;
        }
        public Colour(ColourI3 colour, byte alpha)
        {
            R = (byte)colour.R;
            G = (byte)colour.G;
            B = (byte)colour.B;
            A = alpha;
        }

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

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

        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}, A:{A}";
        }

        public static Colour Zero { get; } = new Colour(0, 0, 0, 0);

        public static Colour Random(Random r)
        {
            return new Colour(
                (byte)r.Next(0, 256),
                (byte)r.Next(0, 256),
                (byte)r.Next(0, 256));
        }
        public static Colour RandomA(Random r)
        {
            return new Colour(
                (byte)r.Next(0, 256),
                (byte)r.Next(0, 256),
                (byte)r.Next(0, 256),
                (byte)r.Next(0, 256));
        }
    }
}
