using System;

namespace Zene.Structs
{
    /// <summary>
    /// Defines a line segment as two points.
    /// </summary>
    public struct Segment3I
    {
        public Segment3I(Vector3I a, Vector3I b)
        {
            A = a;
            B = b;
        }
        public Segment3I(int aX, int aY, int aZ, int bX, int bY, int bZ)
        {
            A = new Vector3I(aX, aY, aZ);
            B = new Vector3I(bX, bY, bZ);
        }
        public Segment3I(Line3I l, int distance)
        {
            A = l.Location;
            B = l.Location + ((Vector3I)l.Direction * distance);
        }

        /// <summary>
        /// The first point in space.
        /// </summary>
        public Vector3I A { get; set; }
        /// <summary>
        /// The second point in space.
        /// </summary>
        public Vector3I B { get; set; }

        /// <summary>
        /// The x distance and y distance between points <see cref="A"/> and <see cref="B"/>.
        /// </summary>
        public Vector3I Change => B - A;

        public CuboidI Bounds
        {
            get => new CuboidI(
                    A.X < B.X ? A.X : B.X,
                    A.Y > B.Y ? A.Y : B.Y,
                    A.Z < B.Z ? A.Z : B.Z,
                    Math.Abs(A.X - B.X),
                    Math.Abs(A.Y - B.Y),
                    Math.Abs(A.Z - B.Z));
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
            return obj is Segment3I seg &&
                A == seg.A &&
                B == seg.B;
        }
        public override int GetHashCode() => HashCode.Combine(A, B);

        public static bool operator ==(Segment3I l, Segment3I r) => l.Equals(r);
        public static bool operator !=(Segment3I l, Segment3I r) => l.Equals(r);

        public static implicit operator Segment3(Segment3I segment) => new Segment3(segment.A, segment.B);
    }
}
