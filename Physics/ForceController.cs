namespace Zene.Physics
{
    public interface IForceController
    {
        public double Strength { get; set; }

        public Velocity GetForce(PhyisicsProperties propities, double frameTime);
    }
}
