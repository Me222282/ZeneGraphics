namespace Zene.Structs
{
    /// <summary>
    /// Defines a line segment as two points.
    /// </summary>
    public struct Segment3
    {
        public Segment3(Vector3 a, Vector3 b)
        {
            A = a;
            B = b;
        }
        public Segment3(Line3 l, double distance)
        {
            A = l.Location;
            B = l.Location + (l.Direction * distance);
        }

        /// <summary>
        /// The first point in space.
        /// </summary>
        public Vector3 A { get; set; }
        /// <summary>
        /// The second point in space.
        /// </summary>
        public Vector3 B { get; set; }

        /// <summary>
        /// The x distance and y distance between points <see cref="A"/> and <see cref="B"/>.
        /// </summary>
        public Vector3 Change
        {
            get
            {
                return B - A;
            }
        }

        public Box3 Bounds
        {
            get => new Box3(
                    A.X < B.X ? A.X : B.X,
                    A.X > B.X ? A.X : B.X,
                    A.Y > B.Y ? A.Y : B.Y,
                    A.Y < B.Y ? A.Y : B.Y,
                    A.Z < B.Z ? A.Z : B.Z,
                    A.Z > B.Z ? A.Z : B.Z);
        }
    }
}
