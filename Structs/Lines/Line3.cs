namespace Zene.Structs
{
    /// <summary>
    /// Defines an infinite line as a point and direction.
    /// </summary>
    public struct Line3
    {
        public Line3(Vector3 dir, Vector3 loc)
        {
            _direction = dir;
            Location = loc;

            _gradients = new Gradient3(_direction);
        }
        public Line3(Segment3 seg)
        {
            Location = seg.A;
            _direction = seg.Change.Normalised();

            _gradients = new Gradient3(_direction);
        }

        private Gradient3 _gradients;
        private Vector3 _direction;
        /// <summary>
        /// The direction of the line.
        /// </summary>
        public Vector3 Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                _gradients = new Gradient3(value);
            }
        }
        /// <summary>
        /// A point along the line to define is position in space.
        /// </summary>
        public Vector3 Location { get; set; }

        /// <summary>
        /// Gets the x component of the point along the line with the y component of <paramref name="y"/>.
        /// </summary>
        public double GetXFromY(double y)
        {
            // Line is straight
            if (_direction.X == 0)
            {
                return Location.X;
            }

            return Location.X + (_gradients.XOverY * (y - Location.Y));
        }
        /// <summary>
        /// Gets the x component of the point along the line with the z component of <paramref name="z"/>.
        /// </summary>
        public double GetXFromZ(double z)
        {
            // Line is straight
            if (_direction.X == 0)
            {
                return Location.X;
            }

            return Location.X + (_gradients.XOverZ * (z - Location.Z));
        }

        /// <summary>
        /// Gets the y component of the point along the line with the x component of <paramref name="x"/>.
        /// </summary>
        public double GetYFromX(double x)
        {
            // Line is straight
            if (_direction.Y == 0)
            {
                return Location.Y;
            }

            return Location.Y + (_gradients.YOverX * (x - Location.X));
        }
        /// <summary>
        /// Gets the y component of the point along the line with the z component of <paramref name="z"/>.
        /// </summary>
        public double GetYFromZ(double z)
        {
            // Line is straight
            if (_direction.Y == 0)
            {
                return Location.Y;
            }

            return Location.Y + (_gradients.YOverZ * (z - Location.Z));
        }

        /// <summary>
        /// Gets the z component of the point along the line with the x component of <paramref name="x"/>.
        /// </summary>
        public double GetZFromX(double x)
        {
            // Line is straight
            if (_direction.Z == 0)
            {
                return Location.Z;
            }

            return Location.Z + (_gradients.ZOverX * (x - Location.X));
        }
        /// <summary>
        /// Gets the z component of the point along the line with the y component of <paramref name="y"/>.
        /// </summary>
        public double GetZFromY(double y)
        {
            // Line is straight
            if (_direction.Z == 0)
            {
                return Location.Z;
            }

            return Location.Z + (_gradients.ZOverY * (y - Location.Y));
        }
    }
}
