using Zene.Structs;

namespace Zene.Physics
{
    public class GravityForce : IForceController
    {
        public GravityForce(Vector2 location, double mass)
        {
            CenterOfMass = location;
            Mass = mass;
        }

        public Vector2 CenterOfMass { get; set; }

        public double Mass { get; set; }
        double IForceController.Strength
        {
            get
            {
                return Mass;
            }
            set
            {
                Mass = value;
            }
        }

        public Velocity GetForce(PhyisicsProperties properties, double frameTime)
        {
            Velocity v = Velocity.FromPath(properties.Box.Center, CenterOfMass);

            // Use newtons equation g = G*M / r^2 to find the force of the gravity
            double distance = properties.Box.Center.Distance(CenterOfMass);
            v.Speed = frameTime * ((Gravitation * Mass) / (distance * distance));

            return v;
        }

        public const double Gravitation = 0.0000000000667408;
    }
}
