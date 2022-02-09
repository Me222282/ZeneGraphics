using System;
using Zene.Graphics;
using Zene.Structs;

namespace CSGL
{
    public class CObject
    {
        public CObject(double l, double r, double t, double bo, double f, double ba)
        {
            Left = l;
            Right = r;
            Top = t;
            Bottom = bo;
            Front = f;
            Back = ba;
        }

        public double Left { get; set; }

        public double Right { get; set; }

        public double Top { get; set; }

        public double Bottom { get; set; }

        public double Front { get; set; }

        public double Back { get; set; }

        public Vector3 Collision(CObject pre, CObject post)
        {
            if (post.Top < Bottom || post.Bottom > Top)
            {
                return Vector3.Zero;
            }

            Rectangle a = new Rectangle(Left, Right, Back, Front);

            Rectangle b = new Rectangle(post.Left, post.Right, post.Back, post.Front);

            Rectangle c = new Rectangle(pre.Left, pre.Right, pre.Back, pre.Front);

            Rectangle d = new Rectangle(
                Math.Min(b.Left, c.Left),
                Math.Max(b.Right, c.Right),
                Math.Min(b.Top, c.Top),
                Math.Max(b.Bottom, c.Bottom));

            if (a.Collision(d))
            {
                double dx1 = Math.Min(c.Left - a.Left, c.Left - a.Right);
                double dy1 = Math.Min(c.Top - a.Top, c.Top - a.Bottom);

                double dx2 = Math.Min(c.Right - a.Left, c.Left - a.Right);
                double dy2 = Math.Min(c.Bottom - a.Top, c.Top - a.Bottom);

                return new Vector3(Math.Min(dx1, dx2), 0, Math.Min(dy1, dy2));
            }

            return Vector3.Zero;
        }

        public void Shift(Vector3 value)
        {
            Left += value.X;
            Right += value.X;
            Top += value.Y;
            Bottom += value.Y;
            Front += value.Z;
            Back += value.Z;
        }

        public CObject Shifted(Vector3 value)
        {
            return new CObject(
                Left + value.X,
                Right + value.X,
                Top + value.Y,
                Bottom + value.Y,
                Front + value.Z,
                Back + value.Z);
        }

        private struct Rectangle
        {
            public Rectangle(double l, double r, double t, double b)
            {
                Left = l;
                Right = r;
                Top = t;
                Bottom = b;
            }

            public double Left { get; set; }

            public double Right { get; set; }

            public double Top { get; set; }

            public double Bottom { get; set; }

            public bool Collision(Rectangle r)
            {
                return Left > r.Right && Right < r.Left && Top < r.Bottom && Bottom > r.Top;
            }
        }
    }
}
