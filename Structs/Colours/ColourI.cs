using System;

namespace Zene.Structs
{
    public struct ColourI
    {
        public ColourI(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
            A = 255;
        }
        public ColourI(int r, int g, int b, int a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public ColourI(Colour3 colour, int alpha)
        {
            R = colour.R;
            G = colour.G;
            B = colour.B;
            A = alpha;
        }
        public ColourI(ColourF3 colour, int alpha)
        {
            R = (int)(colour.R * 255);
            G = (int)(colour.G * 255);
            B = (int)(colour.B * 255);
            A = alpha;
        }

        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public int A { get; set; }

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

        public override string ToString()
        {
            return $"R:{R}, G:{G}, B:{B}, A:{A}";
        }

        public static ColourI Zero { get; } = new ColourI(0, 0, 0, 0);

        public static ColourI Random(Random r)
        {
            return new ColourI(
                r.Next(0, 256),
                r.Next(0, 256),
                r.Next(0, 256));
        }
        public static ColourI RandomA(Random r)
        {
            return new ColourI(
                r.Next(0, 256),
                r.Next(0, 256),
                r.Next(0, 256),
                r.Next(0, 256));
        }
    }
}
