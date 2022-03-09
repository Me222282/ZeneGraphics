using System;

namespace Zene.Structs
{
    /// <summary>
    /// Defines a line segment as two points in 2 dimensional space stored as integers.
    /// </summary>
    public struct Segment2I
    {
        /// <summary>
        /// Creates a segment from two points.
        /// </summary>
        /// <param name="a">The first point to reference.</param>
        /// <param name="b">The second point to reference.</param>
        public Segment2I(Vector2I a, Vector2I b)
        {
            A = a;
            B = b;
        }
        /// <summary>
        /// Creates a segment from two points.
        /// </summary>
        /// <param name="aX">The x value of the first point to reference.</param>
        /// <param name="aY">The y value of the first point to reference.</param>
        /// <param name="bX">The x value of the second point to reference.</param>
        /// <param name="bY">The y value of the second point to reference.</param>
        public Segment2I(int aX, int aY, int bX, int bY)
        {
            A = new Vector2I(aX, aY);
            B = new Vector2I(bX, bY);
        }
        /// <summary>
        /// Creates a segment from a line and distance.
        /// </summary>
        /// <remarks>
        /// Uses the reference point of the line as the value of <see cref="A"/>.
        /// </remarks>
        /// <param name="l">The line to use as a reference.</param>
        /// <param name="distance">THe distance along the line to bee used as the segment.</param>
        public Segment2I(Line2I l, int distance)
        {
            A = l.Location;
            B = l.Location + ((Vector2I)l.Direction * distance);
        }

        /// <summary>
        /// The first point in space.
        /// </summary>
        public Vector2I A { get; set; }
        /// <summary>
        /// The second point in space.
        /// </summary>
        public Vector2I B { get; set; }

        /// <summary>
        /// The x distance and y distance between points <see cref="A"/> and <see cref="B"/>.
        /// </summary>
        public Vector2I Change => B - A;

        /// <summary>
        /// Determines whether two line segments would intersect and outputs the intersection point.
        /// </summary>
        /// <param name="seg">The line segment to compare to.</param>
        /// <param name="intersection">The point at which the two lines will intersect. If false, this is <see cref="Vector2.Zero"/>.</param>
        public bool Intersects(Segment2I seg, out Vector2I intersection)
        {
            intersection = Vector2I.Zero;

            Vector2I b = Change;
            Vector2I d = seg.Change;

            int pDot = b.PerpDot(d);

            // If b dot d == 0, it means the lines are parallel
            if (pDot == 0) { return false; }

            Vector2I c = seg.A - A;
            double t = c.PerpDot(d) / (double)pDot;
            if (t < 0 || t > 1) { return false; }

            double u = c.PerpDot(b) / (double)pDot;
            if (u < 0 || u > 1) { return false; }

            intersection = A + ((int)t * b);

            return true;
        }
        
        /// <summary>
        /// The smallest box that can fit around this segment.
        /// </summary>
        public RectangleI Bounds
        {
            get => new RectangleI(
                    A.X < B.X ? A.X : B.X,
                    A.Y > B.Y ? A.Y : B.Y,
                    Math.Abs(A.X - B.X),
                    Math.Abs(A.Y - B.Y));
        }

#nullable enable
        public override string ToString()
        {
            return $"A:{A}, B:{B}";
        }
        public string ToString(string? format)
        {
            return $"A:{A.ToString(format)}, B:{B.ToString(format)}";
        }
#nullable disable

        public override bool Equals(object obj)
        {
            return obj is Segment2I seg &&
                A == seg.A &&
                B == seg.B;
        }
        public override int GetHashCode() => HashCode.Combine(A, B);

        public static bool operator ==(Segment2I l, Segment2I r) => l.Equals(r);
        public static bool operator !=(Segment2I l, Segment2I r) => l.Equals(r);

        public static implicit operator Segment2(Segment2I segment) => new Segment2I(segment.A, segment.B);
    }
}
