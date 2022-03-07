using System;

namespace Zene.Structs
{
    /// <summary>
    /// A box stored by the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Width"/>, <see cref="Height"/> and <see cref="Depth"/> values.
    /// </summary>
    public struct Cuboid : IBox3
    {
        public Cuboid(double x, double y, double z, double w, double h, double d)
        {
            X = x;
            Y = y;
            Z = z;
            Width = w;
            Height = h;
            Depth = d;
        }
        public Cuboid(Vector3 location, Vector3 size)
        {
            X = location.X;
            Y = location.Y;
            Z = location.Z;
            Width = size.X;
            Height = size.Y;
            Depth = size.Z;
        }
        public Cuboid(IBox3 box)
        {
            X = box.Left;
            Y = box.Top;
            Z = box.Front;
            Width = box.Width;
            Height = box.Height;
            Depth = box.Depth;
        }

        /// <summary>
        /// The left x location of the box.
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// The top y location of the box.
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// The front z location of the box.
        /// </summary>
        public double Z { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }

        public Vector3 Centre => new Vector3(X + (Width * 0.5), Y - (Height * 0.5), Z + (Depth * 0.5));

        /// <summary>
        /// The top-left-front location of the box.
        /// </summary>
        public Vector3 Location
        {
            get => new Vector3(X, Y, Z);
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }
        /// <summary>
        /// The width and height of the box.
        /// </summary>
        public Vector3 Size
        {
            get => new Vector3(Width, Height, Depth);
            set
            {
                Width = value.X;
                Height = value.Y;
                Depth = value.Z;
            }
        }

        public double Left
        {
            get => X;
            set
            {
                Width += X - value;
                X = value;
            }
        }
        public double Right
        {
            get => X + Width;
            set => Width = value - X;
        }
        public double Bottom
        {
            get => Y - Height;
            set
            {
                Height = Y - value;
                Y = value;
            }
        }
        public double Top
        {
            get => Y;
            set => Height += value - Y;
        }
        public double Front
        {
            get => Z;
            set
            {
                Depth += value - Y;
                Z = value;
            }
        }
        public double Back
        {
            get => Z + Depth;
            set => Depth = value - Z;
        }

#nullable enable
        public override string ToString()
        {
            return $"X:{X}, Y:{X}, Z:{Z}, Width:{Width}, Height:{Height}, Depth:{Depth}";
        }
        public string ToString(string? format)
        {
            return @$"X:{X.ToString(format)}, Y:{Y.ToString(format)}, Z:{Z.ToString(format)}, Width:{Width.ToString(format)}, Height:{
                Height.ToString(format)}, Depth:{Depth.ToString(format)}";
        }
#nullable disable

        public override bool Equals(object obj)
        {
            return obj is IBox3 b &&
                    X == b.Left && Width == b.Width &&
                    Y == b.Top && Height == b.Height &&
                    Z == b.Front && Depth == b.Depth;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, Width, Height, Depth);
        }

        public static bool operator ==(Cuboid l, Cuboid r)
        {
            return l.Equals(r);
        }
        public static bool operator !=(Cuboid l, Cuboid r)
        {
            return !l.Equals(r);
        }

        public static explicit operator Cuboid(Box3 box)
        {
            return new Cuboid(box);
        }

        public static Cuboid Zero { get; } = new Cuboid(0, 0, 0, 0, 0, 0);
        public static Cuboid One { get; } = new Cuboid(-1, 1, -1, 2, 2, 2);
    }
}
