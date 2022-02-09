using Zene.Structs;

namespace Zene.Physics
{
    public class DirectionForce : IForceController
    {
        public DirectionForce(Vector2 dir, double strength)
        {
            _direction = dir.Normalized();
            Strength = strength;
        }
        public DirectionForce(Vector2 velocity)
        {
            double length = velocity.Length;
            double scale = 1.0 / length;
            // Fix problems with double.Infinity
            if (length == 0) { scale = 0; }

            _direction = velocity * scale;
            Strength = length;
        }

        private Vector2 _direction;
        public Vector2 Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value.Normalized();
            }
        }
        public double Strength { get; set; }

        public Velocity GetForce(PhyisicsProperties properties, double frameTime)
        {
            return new Velocity(_direction, frameTime * (Strength / properties.Mass));
        }
    }
}
