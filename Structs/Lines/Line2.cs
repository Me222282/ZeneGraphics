namespace Zene.Structs
{
    /// <summary>
    /// Defines an infinite line as a point and direction.
    /// </summary>
    public struct Line2
    {
        public Line2(Vector2 dir, Vector2 loc)
        {
            _direction = dir;
            Location = loc;

            _gradients = new Gradient2(_direction);
        }
        public Line2(Segment2 seg)
        {
            Location = seg.A;
            _direction = seg.Change.Normalized();

            _gradients = new Gradient2(_direction);
        }

        private Gradient2 _gradients;
        private Vector2 _direction;
        /// <summary>
        /// The direction of the line.
        /// </summary>
        public Vector2 Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                _gradients = new Gradient2(value);
            }
        }
        /// <summary>
        /// A point along the line to define is position in space.
        /// </summary>
        public Vector2 Location { get; set; }

        /// <summary>
        /// Returns the point at which two lines would intersect. If they are parallel, returns <see cref="Vector2.PositiveInfinity"/>.
        /// </summary>
        /// <param name="line">The line to intersect.</param>
        /// <returns></returns>
        public Vector2 Intersects(Line2 line)
        {
            Vector2 b = Direction * 10;
            Vector2 d = line.Direction * 10;

            double pDot = b.PerpDot(d);

            // If b dot d == 0, it means the lines are parallel and have an intersection of infinity
            if (pDot == 0) { return Vector2.PositiveInfinity; }

            Vector2 c = line.Location - Location;
            double t = c.PerpDot(d) / pDot;

            return Location + (t * b);
        }

        /// <summary>
        /// Gets the x component of the point along the line with the y component of <paramref name="y"/>.
        /// </summary>
        public double GetX(double y)
        {
            // Line is straight
            if (_direction.X == 0)
            {
                return Location.X;
            }

            return Location.X + (_gradients.XOverY * (y - Location.Y));
        }
        /// <summary>
        /// Gets the y component of the point along the line with the x component of <paramref name="x"/>.
        /// </summary>
        public double GetY(double x)
        {
            // Line is straight
            if (_direction.Y == 0)
            {
                return Location.Y;
            }

            return Location.Y + (_gradients.YOverX * (x - Location.X));
        }
    }
}
