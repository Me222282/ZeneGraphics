using System;

namespace Zene.Structs
{
    /// <summary>
    /// A box stored by the <see cref="X"/>, <see cref="Y"/>, <see cref="Width"/> and <see cref="Height"/> values as integers.
    /// </summary>
    public struct RectangleI : IBox
    {
        public RectangleI(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }
        public RectangleI(Vector2I location, Vector2I size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.X;
            Height = size.Y;
        }
        public RectangleI(IBox box)
        {
            X = (int)box.Left;
            Y = (int)box.Top;
            Width = (int)box.Width;
            Height = (int)box.Height;
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
        /// The width of the box.
        /// </summary>
        public int Width { get; set; }
        double IBox.Width => Width;
        /// <summary>
        /// The height of the box.
        /// </summary>
        public int Height { get; set; }
        double IBox.Height => Height;

        /// <summary>
        /// The center location of the box.
        /// </summary>
        public Vector2I Center => new Vector2I(X + (Width * 0.5), Y - (Height * 0.5));
        Vector2 IBox.Centre => Center;

        /// <summary>
        /// The top-left location of the box.
        /// </summary>
        public Vector2I Location
        {
            get => new Vector2I(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        /// <summary>
        /// The width and height of the box.
        /// </summary>
        public Vector2I Size
        {
            get => new Vector2I(Width, Height);
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        /// <summary>
        /// The left side of the box.
        /// </summary>
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
            set => Left = (int)value;
        }
        /// <summary>
        /// The right side of the box.
        /// </summary>
        public int Right
        {
            get => X + Width;
            set => Width = value - X;
        }
        double IBox.Right
        {
            get => X + Width;
            set => Right = (int)value;
        }
        /// <summary>
        /// The bottom side of the box.
        /// </summary>
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
            set => Bottom = (int)value;
        }
        /// <summary>
        /// The top side of the box.
        /// </summary>
        public int Top
        {
            get => Y;
            set => Height += value - Y;
        }
        double IBox.Top
        {
            get => Y;
            set => Top = (int)value;
        }

#nullable enable
        public override string ToString()
        {
            return $"X:{X}, Y:{X}, Width:{Width}, Height:{Height}";
        }
        public string ToString(string? format)
        {
            return $"X:{X.ToString(format)}, Y:{Y.ToString(format)}, Width:{Width.ToString(format)}, Height:{Height.ToString(format)}";
        }
#nullable disable

        public override bool Equals(object obj)
        {
            return obj is IBox b &&
                    X == b.Left && Width == b.Width &&
                    Y == b.Top && Height == b.Height;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Width, Height);
        }

        public static bool operator ==(RectangleI l, RectangleI r)
        {
            return l.Equals(r);
        }
        public static bool operator !=(RectangleI l, RectangleI r)
        {
            return !l.Equals(r);
        }

        public static explicit operator RectangleI(Box box)
        {
            return new RectangleI(box);
        }
        public static explicit operator RectangleI(Rectangle rect)
        {
            return new RectangleI(rect);
        }

        public static RectangleI Zero { get; } = new RectangleI(0, 0, 0, 0);
        public static RectangleI One { get; } = new RectangleI(-1, 1, 2, 2);
    }
}
