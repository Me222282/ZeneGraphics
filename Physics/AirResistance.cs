namespace Zene.Physics
{
    public class AirResistance : IForceController
    {
        public AirResistance(double strength)
        {
            Strength = strength;
        }

        public double Strength { get; set; }

        public Velocity GetForce(PhyisicsProperties properties, double frameTime)
        {
            double area = properties.Box.Width * properties.Box.Height * properties.Velocity.Speed;
            System.Console.WriteLine(area);

            System.Console.WriteLine(area);

            return new Velocity(-properties.Velocity.Direction, area * Strength);
        }
    }
}
