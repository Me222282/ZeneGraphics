using System;

namespace Zene.Structs
{
    /// <summary>
    /// A box stored by the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Width"/>, <see cref="Height"/> and <see cref="Depth"/> values as integers.
    /// </summary>
    public struct CuboidI : IBox3
    {
        public CuboidI(int x, int y, int z, int w, int h, int d)
        {
            X = x;
            Y = y;
            Z = z;
            Width = w;
            Height = h;
            Depth = d;
        }
        public CuboidI(Vector3I location, Vector3I size)
        {
            X = location.X;
            Y = location.Y;
            Z = location.Z;
            Width = size.X;
            Height = size.Y;
            Depth = size.Z;
        }
        public CuboidI(IBox3 box)
        {
            X = (int)box.Left;
            Y = (int)box.Top;
            Z = (int)box.Front;
            Width = (int)box.Width;
            Height = (int)box.Height;
            Depth = (int)box.Depth;
        }

        /// <summary>
        /// The left x location of the box.
        /// </summary>
        public int X { get; set; }
        double IBox.X => X;
        /// <summary>
        /// The top y location of the box.
        /// </summary>
        public int Y { get; set; }
        double IBox.Y => Y;
        /// <summary>
        /// The front z location of the box.
        /// </summary>
        public int Z { get; set; }
        double IBox3.Z => Z;
        public int Width { get; set; }
        double IBox.Width => Width;
        public int Height { get; set; }
        double IBox.Height => Height;
        public int Depth { get; set; }
        double IBox3.Depth => Depth;

        public Vector3I Centre => new Vector3I(X + (Width * 0.5), Y - (Height * 0.5), Z + (Depth * 0.5));
        Vector3 IBox3.Centre => new Vector3(X + (Width * 0.5), Y - (Height * 0.5), Z + (Depth * 0.5));

        /// <summary>
        /// The top-left-front location of the box.
        /// </summary>
        public Vector3I Location
        {
            get => new Vector3I(X, Y, Z);
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
        public Vector3I Size
        {
            get => new Vector3I(Width, Height, Depth);
            set
            {
                Width = value.X;
                Height = value.Y;
                Depth = value.Z;
            }
        }

        public int Left
        {
            get => X;
            set
            {
                Width += X - value;
                X = value;
            }
        }
        double IBox.Left
        {
            get => X;
            set
            {
                Left = (int)value;
            }
        }
        public int Right
        {
            get => X + Width;
            set => Width = value - X;
        }
        double IBox.Right
        {
            get => X + Width;
            set
            {
                Right = (int)value;
            }
        }
        public int Bottom
        {
            get => Y - Height;
            set
            {
                Height = Y - value;
                Y = value;
            }
        }
        double IBox.Bottom
        {
            get => Y - Height;
            set
            {
                Bottom = (int)value;
            }
        }
        public int Top
        {
            get => Y;
            set => Height += value - Y;
        }
        double IBox.Top
        {
            get => Y;
            set
            {
                Top = (int)value;
            }
        }
        public int Front
        {
            get => Z;
            set
            {
                Depth += value - Y;
                Z = value;
            }
        }
        double IBox3.Front
        {
            get => Z;
            set
            {
                Front = (int)value;
            }
        }
        public int Back
        {
            get => Z + Depth;
            set => Depth = value - Z;
        }
        double IBox3.Back
        {
            get => Z + Depth;
            set
            {
                Back = (int)value;
            }
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

        public static bool operator ==(CuboidI l, CuboidI r)
        {
            return l.Equals(r);
        }
        public static bool operator !=(CuboidI l, CuboidI r)
        {
            return !l.Equals(r);
        }

        public static explicit operator CuboidI(Box3 box)
        {
            return new CuboidI(box);
        }
        public static explicit operator CuboidI(Cuboid rect)
        {
            return new CuboidI(rect);
        }

        public static CuboidI Zero { get; } = new CuboidI(0, 0, 0, 0, 0, 0);
        public static CuboidI One { get; } = new CuboidI(-1, 1, -1, 2, 2, 2);
    }
}
