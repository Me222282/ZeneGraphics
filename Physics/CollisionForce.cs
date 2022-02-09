using Zene.Structs;

namespace Zene.Physics
{
    public abstract class CollisionForce : IForceController
    {
        public double Intensity { get; set; }
        public double Strength { get; set; }

        Velocity IForceController.GetForce(PhyisicsProperties propities, double frameTime)
        {
            return GetCollision(propities, frameTime);
        }

        public abstract Velocity GetCollision(PhyisicsProperties propities, double frameTime);
        public abstract bool IsCollision(IBox box);
    }
}
