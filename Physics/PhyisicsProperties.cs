using Zene.Structs;

namespace Zene.Physics
{
    public class PhyisicsProperties
    {
        public IBox Box { get; init; }
        public Vector2 NextPosition { get; init; }
        public Velocity Velocity { get; init; }
        public double Mass { get; init; }

        public Box NextBox
        {
            get
            {
                return new Box(NextPosition, new Vector2(Box.Width, Box.Height));
            }
        }
    }
}
