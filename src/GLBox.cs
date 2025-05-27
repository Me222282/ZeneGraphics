using System;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// A box stored by the bottom left point, <see cref="Width"/> and <see cref="Height"/> values as integers.
    /// </summary>
    public struct GLBox : IBox
    {
        /// <summary>
        /// Creates a box from a location and size.
        /// </summary>
        /// <param name="left">The x value of the location.</param>
        /// <param name="bottom">The y value of the location.</param>
        /// <param name="w">The width value of the size.</param>
        /// <param name="h">The height value of the size.</param>
        public GLBox(int left, int bottom, int w, int h)
        {
            X = left;
            Y = bottom;
            Width = w;
            Height = h;
        }
        /// <summary>
        /// Creates a box from a location and size.
        /// </summary>
        /// <param name="location">The location of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        public GLBox(Vector2I location, Vector2I size)
        {
            //X = location.X - (size.X / 2);
            //Y = location.Y - (size.Y / 2);
            X = location.X;
            Y = location.Y;
            Width = size.X;
            Height = size.Y;
        }
        /// <summary>
        /// Creates a box from a <see cref="floatv"/> based location and size.
        /// </summary>
        /// <param name="left">The x value of the location.</param>
        /// <param name="bottom">The y value of the location.</param>
        /// <param name="w">The width value of the size.</param>
        /// <param name="h">The height value of the size.</param>
        public GLBox(floatv left, floatv bottom, floatv w, floatv h)
        {
            X = (int)left;
            Y = (int)bottom;
            Width = (int)w;
            Height = (int)h;
        }
        /// <summary>
        /// Creates a box from a <see cref="floatv"/> based location and size.
        /// </summary>
        /// <param name="location">The location of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        public GLBox(Vector2 location, Vector2 size)
        {
            X = (int)location.X;
            Y = (int)location.Y;
            Width = (int)size.X;
            Height = (int)size.Y;
        }
        /// <summary>
        /// Creates a box from a <see cref="Rectangle"/> box.
        /// </summary>
        /// <param name="rect">The rectangle..</param>
        public GLBox(Rectangle rect)
        {
            X = (int)rect.X;
            Height = (int)rect.Height;
            Y = (int)rect.Y - Height;
            Width = (int)rect.Width;
        }
        /// <summary>
        /// Creates a box from a <see cref="RectangleI"/> box.
        /// </summary>
        /// <param name="rect">The rectangle..</param>
        public GLBox(RectangleI rect)
        {
            X = rect.X;
            Y = rect.Y - rect.Height;
            Width = rect.Width;
            Height = rect.Height;
        }
        /// <summary>
        /// Creates a box from a <see cref="Box"/>.
        /// </summary>
        /// <param name="box">The basic box.</param>
        public GLBox(Box box)
        {
            X = (int)box.Left;
            Y = (int)box.Bottom;
            Width = (int)box.Width;
            Height = (int)box.Height;
        }
        /// <summary>
        /// Creates a box from a <see cref="Bounds"/> box.
        /// </summary>
        /// <param name="box">The bounding box.</param>
        public GLBox(Bounds box)
        {
            X = (int)box.Left;
            Y = (int)box.Bottom;
            Width = (int)box.Width;
            Height = (int)box.Height;
        }

        /// <summary>
        /// The left x location of the box.
        /// </summary>
        public int X { readonly get; set; }
        floatv IBox.X { get => X; set => X = (int)value; }
        /// <summary>
        /// The bottom y location of the box.
        /// </summary>
        public int Y { readonly get; set; }
        floatv IBox.Y { get => Y; set => Y = (int)value; }
        /// <summary>
        /// The width of the box.
        /// </summary>
        public int Width { readonly get; set; }
        floatv IBox.Width { get => Width; set => Width = (int)value; }
        /// <summary>
        /// The height of the box.
        /// </summary>
        public int Height { readonly get; set; }
        floatv IBox.Height { get => Height; set => Height = (int)value; }

        /// <summary>
        /// The center location of the box.
        /// </summary>
        public Vector2I Centre
        {
            readonly get => new Vector2I(X + (Width * 0.5f), Y + (Height * 0.5f));
            set
            {
                X = value.X - (Width / 2);
                Y = value.Y - (Height / 2);
            }
        }
        Vector2 IBox.Centre { get => Centre; set => Centre = (Vector2I)value; }

        /// <summary>
        /// The bottom-left location of the box.
        /// </summary>
        public Vector2I Location
        {
            readonly get => new Vector2I(X, Y);
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
            readonly get => new Vector2I(Width, Height);
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }
        Vector2 IBox.Size { get => Size; set => Size = (Vector2I)value; }

        /// <summary>
        /// The left side of the box.
        /// </summary>
        public int Left
        {
            readonly get => X;
            set
            {
                Width += X - value;
                //if (Width < 0)
                //{
                //    Width = 0;
                //}
                X = value;
            }
        }
        floatv IBox.Left { get => Left; set => Left = (int)value; }
        /// <summary>
        /// The right side of the box.
        /// </summary>
        public int Right
        {
            readonly get => X + Width;
            set
            {
                Width = value - X;
                //if (Width < 0)
                //{
                //    Width = 0;
                //}
            }
        }
        floatv IBox.Right { get => Right; set => Right = (int)value; }
        /// <summary>
        /// The bottom side of the box.
        /// </summary>
        public int Bottom
        {
            readonly get => Y;
            set
            {
                Height += Y - value;
                //if (Height < 0)
                //{
                //    Height = 0;
                //}
                Y = value;
            }
        }
        floatv IBox.Bottom { get => Bottom; set => Bottom = (int)value; }
        /// <summary>
        /// The top side of the box.
        /// </summary>
        public int Top
        {
            readonly get => Y + Height;
            set
            {
                Height = value - Y;
                //if (Height < 0)
                //{
                //    Height = 0;
                //}
            }
        }
        floatv IBox.Top { get => Top; set => Top = (int)value; }

        /// <summary>
        /// The top-right point.
        /// </summary>
        public Vector2I TopRight
        {
            readonly get => new Vector2I(Right, Top);
            set => (Right, Y) = value;
        }
        /// <summary>
        /// The bottom-left point.
        /// </summary>
        public Vector2I TopLeft
        {
            readonly get => new Vector2I(X, Top);
            set => (X, Top) = value;
        }
        /// <summary>
        /// The bottom-right point.
        /// </summary>
        public Vector2I BottomRight
        {
            readonly get => new Vector2I(Right, Y);
            set => (Right, Y) = value;
        }

        ///// <summary>
        ///// Determines whether this box overlaps <paramref name="box"/>.
        ///// </summary>
        ///// <param name="box">The box to compare to.</param>
        //public readonly bool Overlaps(RectangleI box)
        //{
        //    return (Left < box.Right) &&
        //        (Right > box.Left) &&
        //        (Bottom < box.Top) &&
        //        (Top > box.Bottom);
        //}
        ///// <summary>
        ///// Determines whether <paramref name="box"/> is inside this.
        ///// </summary>
        ///// <param name="box">The box to compare to.</param>
        //public readonly bool Contains(RectangleI box)
        //{
        //    return (Left >= box.Right) &&
        //        (Right <= box.Left) &&
        //        (Bottom >= box.Top) &&
        //        (Top <= box.Bottom);
        //}
        ///// <summary>
        ///// Determines whether <paramref name="point"/> is inside this.
        ///// </summary>
        ///// <param name="point">The <see cref="Vector2I"/> to compare to.</param>
        //public readonly bool Contains(Vector2I point)
        //{
        //    return (point.X >= Left) &&
        //        (point.X <= Right) &&
        //        (point.Y >= Bottom) &&
        //        (point.Y <= Top);
        //}
        ///// <summary>
        ///// Determines whether <paramref name="point"/> is inside this.
        ///// </summary>
        ///// <param name="point">The <see cref="Vector2"/> to compare to.</param>
        //public readonly bool Contains(Vector2 point) => Contains(point);

        ///// <summary>
        ///// Returns the smallest box possable to contain <paramref name="b"/> and this.
        ///// </summary>
        ///// <param name="b">The second box.</param>
        ///// <returns></returns>
        //public readonly RectangleI Add(RectangleI b)
        //{
        //    Vector2I diff = Location - b.Location;
        //    Vector2I loc = 0;

        //    if (diff.X < 0)
        //    {
        //        diff.X = -diff.X;
        //        diff.X += b.Width;
        //        loc.X = X;
        //    }
        //    else
        //    {
        //        diff.X += Width;
        //        loc.X = b.X;
        //    }
        //    if (diff.Y < 0)
        //    {
        //        diff.Y = -diff.Y;
        //        diff.Y += Height;
        //        loc.Y = b.Y;
        //    }
        //    else
        //    {
        //        diff.Y += b.Height;
        //        loc.Y = Y;
        //    }

        //    return new RectangleI(loc, diff);
        //}

        ///// <summary>
        ///// Returns the combined volume of <paramref name="b"/> and this box.
        ///// </summary>
        ///// <param name="b">The second box.</param>
        ///// <returns></returns>
        //public readonly int CombinedVolume(RectangleI b)
        //{
        //    Vector2I diff = Location - b.Location;

        //    if (diff.X < 0)
        //    {
        //        diff.X = -diff.X;
        //        diff.X += Width;
        //    }
        //    else { diff.X += b.Width; }
        //    if (diff.Y < 0)
        //    {
        //        diff.Y = -diff.Y;
        //        diff.Y += Height;
        //    }
        //    else { diff.Y += b.Height; }

        //    return diff.X * diff.Y;
        //}

        ///// <summary>
        ///// Returns a box clamped to the bounds of <paramref name="bounds"/>.
        ///// </summary>
        ///// <param name="bounds">The constricting bounds.</param>
        ///// <returns></returns>
        //public readonly GLBox Clamped(GLBox bounds)
        //{
        //    GLBox r = this;
        //    int c = bounds.Right;
        //    if (r.Left < c) { r.Left = c; }
        //    else
        //    {
        //        c = bounds.Left;
        //        if (r.Right > c) { r.Right = c; }
        //    }
        //    c = bounds.Top;
        //    if (r.Bottom < c) { r.Bottom = c; }
        //    else
        //    {
        //        c = bounds.Bottom;
        //        if (r.Top > c) { r.Top = c; }
        //    }

        //    return r;
        //}

        ///// <summary>
        ///// Returns a rectangle with each side of the box extended by a value.
        ///// </summary>
        ///// <param name="value">The value to extend by</param>
        //public readonly RectangleI Expanded(Vector2I value)
        //{
        //    return new RectangleI(
        //        X - value.X,
        //        Y + value.Y,
        //        Width + value.X * 2,
        //        Height + value.Y * 2);
        //}
        ///// <summary>
        ///// Extend each side of the box by a value.
        ///// </summary>
        ///// <param name="value">The value to extend by</param>
        //public void Expand(Vector2I value)
        //{
        //    X -= value.X;
        //    Right += value.X;
        //    Bottom -= value.Y;
        //    Y += value.Y;
        //}

        ///// <summary>
        ///// Determines whether this box intersects the path of <see cref="Line2"/> <paramref name="line"/>.
        ///// </summary>
        ///// <param name="line">The line to compare to</param>
        ///// <param name="tolerance">Added tolerance to act as thickening the line.</param>
        ///// <returns></returns>
        //public readonly bool Intersects(Line2 line, Vector2 tolerance)
        //{
        //    Rectangle tolBox = ((Rectangle)this).Expanded(tolerance);

        //    Vector2 dist = tolBox.Centre.Relative(line);

        //    // Half of height
        //    floatv hh = tolBox.Height * 0.5f;
        //    // Half of width
        //    floatv hw = tolBox.Width * 0.5f;

        //    return ((dist.Y + hh >= 0) && (dist.Y - hh <= 0)) ||
        //        ((dist.X + hw >= 0) && (dist.X - hw <= 0));
        //}

        ///// <summary>
        ///// Determines whether <paramref name="b"/> shares a bound with this box.
        ///// </summary>
        ///// <param name="b">THe second box.</param>
        ///// <returns></returns>
        //public readonly bool ShareBound(RectangleI b)
        //{
        //    return X == b.X ||
        //        Right == b.Right ||
        //        Y == b.Y ||
        //        Bottom == b.Bottom;
        //}

#nullable enable
        public readonly override string ToString()
        {
            return $"X:{X}, Y:{Y}, Width:{Width}, Height:{Height}";
        }
        public readonly string ToString(string? format)
        {
            return $"X:{X.ToString(format)}, Y:{Y.ToString(format)}, Width:{Width.ToString(format)}, Height:{Height.ToString(format)}";
        }
#nullable disable

        public readonly override bool Equals(object obj)
        {
            return obj is GLBox b &&
                    X == b.Left && Width == b.Width &&
                    Y == b.Bottom && Height == b.Height;
        }
        public readonly override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Width, Height);
        }

        public static bool operator ==(GLBox l, GLBox r) => l.Equals(r);
        public static bool operator !=(GLBox l, GLBox r) => !l.Equals(r);

        public static GLBox operator *(GLBox box, int scale) => new GLBox(box.X * scale, box.Y * scale, box.Width * scale, box.Height * scale);
        public static GLBox operator *(GLBox box, Vector2I scale) => new GLBox(box.X * scale.X, box.Y * scale.Y, box.Width * scale.X, box.Height * scale.Y);
        public static GLBox operator /(GLBox box, int scale) => new GLBox(box.X / scale, box.Y / scale, box.Width / scale, box.Height / scale);
        public static GLBox operator /(GLBox box, Vector2I scale) => new GLBox(box.X / scale.X, box.Y / scale.Y, box.Width / scale.X, box.Height / scale.Y);

        public static GLBox operator +(GLBox box, Vector2I offset) => new GLBox(box.X + offset.X, box.Y + offset.Y, box.Width, box.Height);
        public static GLBox operator -(GLBox box, Vector2I offset) => new GLBox(box.X - offset.X, box.Y - offset.Y, box.Width, box.Height);
        public static GLBox operator +(GLBox a, GLBox b) => a.Add(b);
        public static GLBox operator -(GLBox a, GLBox b) => a.Clamped(b);

        public static explicit operator GLBox(Box box) => new GLBox(box);
        public static explicit operator GLBox(Bounds box) => new GLBox(box);
        public static explicit operator GLBox(Rectangle rect) => new GLBox(rect);
        public static implicit operator GLBox(RectangleI rect) => new GLBox(rect);
        public static implicit operator Box(GLBox box) => new Box(box.Centre, box.Size);
        public static implicit operator Bounds(GLBox box) => new Bounds(box.Left, box.Right, box.Top, box.Bottom);
        public static implicit operator Rectangle(GLBox box) => new Rectangle(box.X, box.Y + box.Height, box.Width, box.Height);
        public static implicit operator RectangleI(GLBox box) => new RectangleI(box.X, box.Y + box.Height, box.Width, box.Height);

        /// <summary>
        /// A <see cref="GLBox"/> with <see cref="X"/>, <see cref="Y"/>, <see cref="Width"/> and <see cref="Height"/> all set to 0.
        /// </summary>
        public static GLBox Zero { get; } = new GLBox(0, 0, 0, 0);
        /// <summary>
        /// A <see cref="GLBox"/> with a <see cref="Width"/> and <see cref="Height"/> of 1 with the top-left at origin.
        /// </summary>
        public static GLBox One { get; } = new GLBox(0, 0, 1, 1);
    }
}
