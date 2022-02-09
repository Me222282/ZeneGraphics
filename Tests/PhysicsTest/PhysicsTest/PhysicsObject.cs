using System;
using System.Collections.Generic;
using Zene.Physics;
using Zene.Structs;

namespace PhysicsTest
{
    public class PhysicsObject
    {
        public PhysicsObject(Vector2 location, Vector2 size)
        {
            _box = new Box(location, size);
        }
        public PhysicsObject(double x, double y, double width, double height)
        {
            _box = new Box(new Vector2(x, y), new Vector2(width, height));
        }

        public double Mass { get; set; } = 1;

        private Box _box;
        public Box Box
        {
            get
            {
                return _box;
            }
            set
            {
                _box = value;
            }
        }

        public Vector2 Location
        {
            get
            {
                return _box.Location;
            }
            set
            {
                _box.Location = value;
            }
        }
        public Vector2 Size
        {
            get
            {
                return _box.Size;
            }
            set
            {
                _box.Size = value;
            }
        }

        private Velocity _velocity;
        public Velocity Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
            }
        }
        public List<IForceController> Forces { get; } = new List<IForceController>();
        public List<IForceController> OneTimeForces { get; } = new List<IForceController>();

        public AirResistance Resistance { get; set; }

        public void ApplyPhysics(double frameTime, double ariRes)
        {
            Velocity preVel = _velocity;
            Vector2 totalStrength = Vector2.Zero;

            foreach (IForceController f in Forces)
            {
                Velocity force = f.GetForce(new PhyisicsProperties()
                    {
                        Box = _box,
                        Mass = Mass,
                        Velocity = _velocity,
                        NextPosition = _box.Location + (_velocity.Movement * frameTime)
                    }, frameTime);

                totalStrength += force.Movement;
                
                _velocity += force;
            }
            foreach (IForceController f in OneTimeForces)
            {
                Velocity force = f.GetForce(new PhyisicsProperties()
                    {
                        Box = _box,
                        Mass = Mass,
                        Velocity = _velocity,
                        NextPosition = _box.Location + (_velocity.Movement * frameTime)
                    }, frameTime);

                totalStrength += force.Movement;

                _velocity += force;
            }

            // Add air resistance
            Vector2 movement = _velocity.Movement;
            _velocity.Movement = movement - CalculateResistance(totalStrength / 2, movement, ariRes);

            Location += _velocity.Movement * frameTime;

            OneTimeForces.Clear();
        }
        public void AdjustToBounds(IBox bounds)
        {
            // Bottom
            if (_box.Bottom < bounds.Bottom)
            {
                _box.Location += new Vector2(0, bounds.Bottom - _box.Bottom);
                _velocity = new Velocity(new Vector2(_velocity.Direction.X * _velocity.Speed, 0));
            }
            // Top
            if (_box.Top > bounds.Top)
            {
                _box.Location += new Vector2(0, bounds.Top - _box.Top);
                _velocity = new Velocity(new Vector2(_velocity.Direction.X * _velocity.Speed, 0));
            }
            // Left
            if (_box.Left < bounds.Left)
            {
                _box.Location += new Vector2(bounds.Left - _box.Left, 0);
                _velocity = new Velocity(new Vector2(0, _velocity.Direction.Y * _velocity.Speed));
            }
            // Right
            if (_box.Right > bounds.Right)
            {
                _box.Location += new Vector2(bounds.Right - _box.Right, 0);
                _velocity = new Velocity(new Vector2(0, _velocity.Direction.Y * _velocity.Speed));
            }
        }

        public static double CalculateResistance(double force, double speed, double res)
        {
            // Deal with negative force
            if (force < 0 && speed < 0)
            {
                force = -force;
            }

            // Make sure resistance doesn't go above res
            if (force < 1)
            {
                force = 1;
            }

            return (res / force) * speed;
        }
        public static Vector2 CalculateResistance(Vector2 force, Vector2 speed, double res)
        {
            return new Vector2(
                CalculateResistance(force.X, speed.X, res),
                CalculateResistance(force.Y, speed.Y, res));
        }
    }
}
