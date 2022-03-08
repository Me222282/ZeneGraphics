using System;
using Zene.Structs;

namespace Zene.Physics
{
    public struct Velocity
    {
        public Velocity(Vector2 dir, double speed)
        {
            _direction = dir.Normalised();
            _speed = speed;
        }
        public Velocity(Vector2 velocity)
        {
            double length = velocity.Length;
            double scale = 1.0 / length;
            // Fix problems with double.Infinity
            if (length == 0) { scale = 0; }

            _direction = velocity * scale;
            _speed = length;
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
                _direction = value.Normalised();
            }
        }

        private double _speed;
        public double Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                if (value < 0)
                {
                    _speed = 0;
                    return;
                }

                _speed = value;
            }
        }

        public Vector2 Movement
        {
            get
            {
                return _direction * Speed;
            }
            set
            {
                double length = value.Length;
                double scale = 1.0 / length;
                // Fix problems with double.Infinity
                if (length == 0) { scale = 0; }

                _direction = value * scale;
                _speed = length;
            }
        }

        public void AddMovement(Vector2 value)
        {
            Movement += value;
        }

        public double VelocityX
        {
            get
            {
                return _direction.X * Speed;
            }
        }
        public double VelocityY
        {
            get
            {
                return _direction.Y * Speed;
            }
        }

        public static Velocity operator +(Velocity a, Velocity b)
        {
            return new Velocity(a.Movement + b.Movement);
        }

        public override bool Equals(object obj)
        {
            return obj is Velocity v &&
                   _direction == v._direction &&
                   _speed == v._speed;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_direction, _speed);
        }

        public static bool operator ==(Velocity a, Velocity b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Velocity a, Velocity b)
        {
            return !a.Equals(b);
        }

        public static Velocity FromPath(Vector2 start, Vector2 end)
        {
            return new Velocity(end - start);
        }

        public static Velocity Zero { get; } = new Velocity();
    }
}
