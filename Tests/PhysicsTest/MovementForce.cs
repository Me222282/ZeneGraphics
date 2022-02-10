using Zene.Physics;
using Zene.Structs;

namespace PhysicsTest
{
    public class MovementForce : IForceController
    {
        public MovementForce(double forwardStrength, double turnStrength)
        {
            Strength = forwardStrength;
            TurnStrength = turnStrength;
        }

        public double Strength { get; set; }
        public bool Move { get; set; }
        public bool TurnLeft { get; set; }
        public bool TurnRight { get; set; }
        public double TurnStrength { get; set; }

        public Velocity GetForce(PhyisicsProperties properties, double frameTime)
        {
            Vector2 direction = properties.Velocity.Direction;

            if (properties.Velocity.Direction == Vector2.Zero)
            {
                direction = new Vector2(0, 1);
            }

            DirectionForce movement = new DirectionForce(Vector2.Zero);
            DirectionForce turning = new DirectionForce(Vector2.Zero);

            if (TurnLeft && !TurnRight)
            {
                turning.Direction = direction.Rotated90();
                turning.Strength = TurnStrength;
            }
            else if (TurnRight && !TurnLeft)
            {
                turning.Direction = direction.Rotated270();
                turning.Strength = TurnStrength;
            }

            if (Move)
            {
                movement.Direction = direction;
                movement.Strength = Strength;
                System.Console.WriteLine(movement.Direction);
            }

            return movement.GetForce(properties, frameTime) + turning.GetForce(properties, frameTime);
        }
    }
}
