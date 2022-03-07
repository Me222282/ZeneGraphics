using System;

namespace Zene.Structs
{
    /// <summary>
    /// A box stored by the <see cref="Left"/>, <see cref="Right"/>, <see cref="Top"/> and <see cref="Bottom"/> values.
    /// </summary>
    public struct Box : IBox
    {
        public Box(double l, double r, double t, double b)
        {
            Left = l;
            Right = r;
            Top = t;
            Bottom = b;
        }
        public Box(Vector2 location, Vector2 size)
        {
            Left = 0;
            Right = 0;
            Bottom = 0;
            Top = 0;

            Location = location;
            Size = size;
        }
        public Box(IBox box)
        {
            Left = box.Left;
            Right = box.Right;
            Bottom = box.Bottom;
            Top = box.Top;
        }

        public double Left { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }
        public double Top { get; set; }

        /// <summary>
        /// The center location of the box.
        /// </summary>
        public Vector2 Location
        {
            get => new Vector2(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        /// <summary>
        /// The width and height of the box.
        /// </summary>
        public Vector2 Size
        {
            get => new Vector2(Width, Height);
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        public Vector2 Centre
        {
            get => Location;
            set => Location = value;
        }

        public double X
        {
            get => Left + (Width * 0.5);
            set
            {
                double offset = value - X;

                Left += offset;
                Right += offset;
            }
        }
        public double Y
        {
            get => Bottom + (Height * 0.5);
            set
            {
                double offset = value - Y;

                Bottom += offset;
                Top += offset;
            }
        }
        public double Width
        {
            get => Right - Left;
            set
            {
                double offset = (value - Width) * 0.5;

                Left -= offset;
                Right += offset;
            }
        }
        public double Height
        {
            get => Top - Bottom;
            set
            {
                double offset = (value - Height) * 0.5;

                Bottom -= offset;
                Top += offset;
            }
        }

#nullable enable
        public override string ToString()
        {
            return $"Left:{Left}, Right:{Right}, Top:{Top}, Bottom:{Bottom}";
        }
        public string ToString(string? format)
        {
            return $"Left:{Left.ToString(format)}, Right:{Right.ToString(format)}, Top:{Top.ToString(format)}, Bottom:{Bottom.ToString(format)}";
        }
#nullable disable

        public override bool Equals(object obj)
        {
            return obj is IBox b &&
                    Left == b.Left && Right == b.Right &&
                    Top == b.Top && Bottom == b.Bottom;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Left, Right, Top, Bottom);
        }

        public static bool operator ==(Box l, Box r)
        {
            return l.Equals(r);
        }
        public static bool operator !=(Box l, Box r)
        {
            return !l.Equals(r);
        }

        public static explicit operator Box(Rectangle box)
        {
            return new Box(box);
        }

        public static Box Infinity { get; } = new Box(double.NegativeInfinity, double.PositiveInfinity, double.PositiveInfinity, double.NegativeInfinity);
        public static Box Zero { get; } = new Box(0, 0, 0, 0);
        public static Box One { get; } = new Box(-1, 1, 1, -1);
    }
}
