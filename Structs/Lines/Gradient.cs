namespace Zene.Structs
{
    internal struct Gradient3
    {
        public Gradient3(Vector3 direction)
        {
            XOverY = direction.X / direction.Y;
            XOverZ = direction.X / direction.Z;
            
            YOverX = direction.Y / direction.X;
            YOverZ = direction.Y / direction.Z;

            ZOverX = direction.Z / direction.X;
            ZOverY = direction.Z / direction.Y;
        }

        public double XOverY { get; }
        public double XOverZ { get; }

        public double YOverX { get; }
        public double YOverZ { get; }

        public double ZOverX { get; }
        public double ZOverY { get; }
    }

    internal struct Gradient2
    {
        public Gradient2(Vector2 direction)
        {
            XOverY = direction.X / direction.Y;
            YOverX = direction.Y / direction.X;
        }

        public double XOverY { get; }
        public double YOverX { get; }
    }
}
