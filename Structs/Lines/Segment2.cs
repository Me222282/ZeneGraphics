namespace Zene.Structs
{
    /// <summary>
    /// Defines a line segment as two points.
    /// </summary>
    public struct Segment2
    {
        public Segment2(Vector2 a, Vector2 b)
        {
            A = a;
            B = b;
        }
        public Segment2(Line2 l, double distance)
        {
            A = l.Location;
            B = l.Location + (l.Direction * distance);
        }

        /// <summary>
        /// The first point in space.
        /// </summary>
        public Vector2 A { get; set; }
        /// <summary>
        /// The second point in space.
        /// </summary>
        public Vector2 B { get; set; }

        /// <summary>
        /// The x distance and y distance between points <see cref="A"/> and <see cref="B"/>.
        /// </summary>
        public Vector2 Change
        {
            get
            {
                return B - A;
            }
        }

        /// <summary>
        /// Checks whether two line segments would intersect and outputs the intersection point.
        /// </summary>
        /// <param name="seg">The line segment to compare to.</param>
        /// <param name="intersection">The point at which the two lines will intersect. If false, this is <see cref="Vector2.Zero"/>.</param>
        public bool Intersects(Segment2 seg, out Vector2 intersection)
        {
            intersection = Vector2.Zero;

            Vector2 b = Change;
            Vector2 d = seg.Change;

            double pDot = b.PerpDot(d);

            // If b dot d == 0, it means the lines are parallel
            if (pDot == 0) { return false; }

            Vector2 c = seg.A - A;
            double t = c.PerpDot(d) / pDot;
            if (t < 0 || t > 1) { return false; }

            double u = c.PerpDot(b) / pDot;
            if (u < 0 || u > 1) { return false; }

            intersection = A + (t * b);

            return true;
        }
    }
}
