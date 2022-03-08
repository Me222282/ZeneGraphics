using System;
using Zene.Physics;
using Zene.Structs;

namespace PhysicsTest
{
    public interface IPlanet
    {
        public IBox Box { get; }
        public double Radius { get; }
    }

    public class PlanetCollision : CollisionForce
    {
        public PlanetCollision(IPlanet p)
        {
            Planet = p;
        }

        public IPlanet Planet { get; set; }

        public override Velocity GetCollision(PhyisicsProperties properties, double frameTime)
        {
            // Not inside planet bounds - no chance of collision
            if (!Planet.Box.Overlaps(properties.NextBox)) { return Velocity.Zero; }

            Vector2 closeCorner = GetMinDist(Planet.Box.Centre,
                new Vector2(properties.NextBox.Left, properties.NextBox.Top), // Top left
                new Vector2(properties.NextBox.Left, properties.NextBox.Bottom), // Bottom left
                new Vector2(properties.NextBox.Right, properties.NextBox.Top), // Top right
                new Vector2(properties.NextBox.Right, properties.NextBox.Bottom)); // Bottom right

            // Not overlapping planet - no collision
            if (Planet.Box.Centre.Distance(closeCorner) > Planet.Radius) { return Velocity.Zero; }

            // Calculate location offset
            Vector2 difference = Planet.Box.Centre - closeCorner;
            Vector2 offset = difference.Normalised() * Planet.Radius;
            return new Velocity(-(offset - difference) / frameTime);
        }
        public override bool IsCollision(IBox box)
        {
            // Not inside planet bounds - no chance of collision
            if (!Planet.Box.Overlaps(box)) { return false; }

            Vector2 closeCorner = GetMinDist(Planet.Box.Centre,
                new Vector2(box.Left, box.Top), // Top left
                new Vector2(box.Left, box.Bottom), // Bottom left
                new Vector2(box.Right, box.Top), // Top right
                new Vector2(box.Right, box.Bottom)); // Bottom right

            // Overlapping planet - collision
            return Planet.Box.Centre.Distance(closeCorner) !> Planet.Radius;
        }

        private static Vector2 GetMinDist(Vector2 source, Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        {
            double aDist = source.Distance(a);
            double bDist = source.Distance(b);
            double cDist = source.Distance(c);
            double dDist = source.Distance(d);

            double min = Math.Min(
                Math.Min(aDist, bDist),
                Math.Min(cDist, dDist));

            if (min == aDist)
            {
                return a;
            }
            if (min == bDist)
            {
                return b;
            }
            if (min == cDist)
            {
                return c;
            }

            return d;
        }
    }
}
