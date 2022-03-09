using System;

namespace Zene.Structs
{
    /// <summary>
    /// Defines an infinite line as a point and direction in 3 dimensional space.
    /// </summary>
    public struct Line3I
    {
        /// <summary>
        /// Create a line from a position and direction.
        /// </summary>
        /// <param name="dir">The direction of the line.</param>
        /// <param name="loc">The reference location for the line.</param>
        public Line3I(Vector3 dir, Vector3I loc)
        {
            _direction = dir;
            Location = loc;

            _gradients = new Gradient3(_direction);
        }
        /// <summary>
        /// Create a line from a position and direction.
        /// </summary>
        /// <param name="dirX">The x value for the direction of the line.</param>
        /// <param name="dirY">The y value for the direction of the line.</param>
        /// <param name="dirZ">The z value for the direction of the line.</param>
        /// <param name="locX">The x value for the reference location for the line.</param>
        /// <param name="locY">The y value for the reference location for the line.</param>
        /// <param name="locZ">The z value for the reference location for the line.</param>
        public Line3I(double dirX, double dirY, double dirZ, int locX, int locY, int locZ)
        {
            _direction = new Vector3(dirX, dirY, dirZ);
            Location = new Vector3I(locX, locY, locZ);

            _gradients = new Gradient3(_direction);
        }
        /// <summary>
        /// Creates a line based off a segment.
        /// </summary>
        /// <param name="seg">The segment to reference from.</param>
        public Line3I(Segment3I seg)
        {
            Location = seg.A;
            _direction = ((Vector3)seg.Change).Normalised();

            _gradients = new Gradient3(_direction);
        }
        /// <summary>
        /// Creates a line based off a segment.
        /// </summary>
        /// <param name="seg">The segment to reference from.</param>
        public Line3I(Segment3 seg)
        {
            Location = (Vector3I)seg.A;
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
        public Vector3I Location { get; set; }

        /// <summary>
        /// Gets the x component of the point along the line with the y component of <paramref name="y"/>.
        /// </summary>
        public int GetXFromY(int y)
        {
            // Line is straight
            if (_direction.X == 0)
            {
                return Location.X;
            }

            return Location.X + (int)(_gradients.XOverY * (y - Location.Y));
        }
        /// <summary>
        /// Gets the x component of the point along the line with the z component of <paramref name="z"/>.
        /// </summary>
        public int GetXFromZ(int z)
        {
            // Line is straight
            if (_direction.X == 0)
            {
                return Location.X;
            }

            return Location.X + (int)(_gradients.XOverZ * (z - Location.Z));
        }

        /// <summary>
        /// Gets the y component of the point along the line with the x component of <paramref name="x"/>.
        /// </summary>
        public int GetYFromX(int x)
        {
            // Line is straight
            if (_direction.Y == 0)
            {
                return Location.Y;
            }

            return Location.Y + (int)(_gradients.YOverX * (x - Location.X));
        }
        /// <summary>
        /// Gets the y component of the point along the line with the z component of <paramref name="z"/>.
        /// </summary>
        public int GetYFromZ(int z)
        {
            // Line is straight
            if (_direction.Y == 0)
            {
                return Location.Y;
            }

            return Location.Y + (int)(_gradients.YOverZ * (z - Location.Z));
        }

        /// <summary>
        /// Gets the z component of the point along the line with the x component of <paramref name="x"/>.
        /// </summary>
        public int GetZFromX(int x)
        {
            // Line is straight
            if (_direction.Z == 0)
            {
                return Location.Z;
            }

            return Location.Z + (int)(_gradients.ZOverX * (x - Location.X));
        }
        /// <summary>
        /// Gets the z component of the point along the line with the y component of <paramref name="y"/>.
        /// </summary>
        public int GetZFromY(int y)
        {
            // Line is straight
            if (_direction.Z == 0)
            {
                return Location.Z;
            }

            return Location.Z + (int)(_gradients.ZOverY * (y - Location.Y));
        }

#nullable enable
        public override string ToString() => $"Location:{Location}, Direction:{_direction}";
        public string ToString(string? format) => $"Location:{Location.ToString(format)}, Direction:{_direction.ToString(format)}";
#nullable disable

        public override bool Equals(object obj)
        {
            return obj is Line3I line &&
                _direction == line.Direction &&
                Location == line.Location;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_direction, Location);
        }

        public static bool operator ==(Line3I l, Line3I r) => l.Equals(r);
        public static bool operator !=(Line3I l, Line3I r) => !l.Equals(r);

        public static implicit operator Line3(Line3I line)
        {
            return new Line3(line._direction, line.Location);
        }
    }
}
