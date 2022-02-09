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
        public Vector2I Center
        {
            get
            {
                return new Vector2I(X + (Width * 0.5), Y + (Height * 0.5));
            }
        }
        Vector2 IBox.Center => Center;

        /// <summary>
        /// The top-left location of the box.
        /// </summary>
        public Vector2I Location
        {
            get
            {
                return new Vector2I(X, Y);
            }
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
            get
            {
                return new Vector2I(Width, Height);
            }
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
            get
            {
                return X;
            }
            set
            {
                Width += X - value;
                X = value;
            }
        }
        double IBox.Left
        {
            get
            {
                return Left;
            }
            set
            {
                Left = (int)value;
            }
        }
        /// <summary>
        /// The right side of the box.
        /// </summary>
        public int Right
        {
            get
            {
                return X + Width;
            }
            set
            {
                Width = value - X;
            }
        }
        double IBox.Right
        {
            get
            {
                return Right;
            }
            set
            {
                Right = (int)value;
            }
        }
        /// <summary>
        /// The bottom side of the box.
        /// </summary>
        public int Bottom
        {
            get
            {
                return Y - Height;
            }
            set
            {
                Height = Y - value;
                Y = value;
            }
        }
        double IBox.Bottom
        {
            get
            {
                return Bottom;
            }
            set
            {
                Bottom = (int)value;
            }
        }
        /// <summary>
        /// The top side of the box.
        /// </summary>
        public int Top
        {
            get
            {
                return Y;
            }
            set
            {
                Height += value - Y;
            }
        }
        double IBox.Top
        {
            get
            {
                return Top;
            }
            set
            {
                Top = (int)value;
            }
        }

        public static explicit operator RectangleI(Box box)
        {
            return new RectangleI(box);
        }
        public static explicit operator RectangleI(Rectangle rect)
        {
            return new RectangleI(rect);
        }
    }
}
