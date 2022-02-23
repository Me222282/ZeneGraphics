using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zene.Graphics;
using Zene.Graphics.Shaders;
using Zene.Physics;
using Zene.Sprites;
using Zene.Structs;

namespace PhysicsTest
{
    public class Rocket : ISprite, IDisposable
    {
        public Rocket(double x, double y, double w, double h, BasicShader shader)
        {
            Box = new Box(new Vector2(x, y), new Vector2(w, h));
            _display = new RocketDisplay(shader);
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

        public bool Arrow
        {
            get
            {
                return _display.Arrow;
            }
            set
            {
                _display.Arrow = value;
            }
        }
        public double Multiplier
        {
            get
            {
                return _display.Multiplier;
            }
            set
            {
                _display.Multiplier = value;
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
        public List<CollisionForce> Collisions { get; } = new List<CollisionForce>();

        IBox ISprite.Bounds => Box;
        public IDisplay Display => _display;
        private readonly RocketDisplay _display;
        private class RocketDisplay : IDisplay
        {
            public RocketDisplay(BasicShader shader)
            {
                _shader = shader;

                _drawable = new DrawObject<Vector2, byte>(
                    new Vector2[]
                    {
                        new Vector2(0.5, 0.5), new Vector2(1, 1),
                        new Vector2(0.5, -0.5), new Vector2(1, 0),
                        new Vector2(-0.5, -0.5), new Vector2(0, 0),
                        new Vector2(-0.5, 0.5), new Vector2(0, 1)
                    },
                    new byte[]
                    {
                        0, 1, 2,
                        2, 3, 0
                    },
                    2, 0, AttributeSize.D2, BufferUsage.DrawFrequent);
                _drawable.AddAttribute((uint)BasicShader.Location.TextureCoords, 1, AttributeSize.D2); // Texture Coordinates

                _texture = Texture2D.Create("Resources/rocket.png", WrapStyle.EdgeClamp, TextureSampling.Blend, false);
                _arrow = Texture2D.Create("Resources/arrow.png", WrapStyle.EdgeClamp, TextureSampling.Blend, false);
            }

            private readonly DrawObject<Vector2, byte> _drawable;
            private readonly Texture2D _texture;
            private readonly Texture2D _arrow;
            private readonly BasicShader _shader;

            public bool Arrow { get; set; } = false;
            public double Multiplier { get; set; } = 10;

            private bool _disposed = false;
            public void Dispose()
            {
                if (_disposed) { return; }

                _drawable.Dispose();
                _texture.Dispose();
                _arrow.Dispose();

                _disposed = true;
                GC.SuppressFinalize(this);
            }

            void IDisplay.Draw(ISprite sprite)
            {
                if (sprite as Rocket == null)
                {
                    throw new Exception("sprite must be of Rocket type.");
                }

                Draw((Rocket)sprite);
            }
            public void Draw(Rocket sprite)
            {
                double multiplier = 1;

                if (Arrow)
                {
                    multiplier = Multiplier;
                }

                _shader.Bind();
                _shader.SetColourSource(ColourSource.Texture);
                _shader.Matrix1 =
                    Matrix4.CreateRotationZ(-Math.Atan2(sprite.Velocity.Direction.X, sprite.Velocity.Direction.Y)) * 
                    Matrix4.CreateScale(sprite.Box.Width * multiplier, sprite.Box.Height * multiplier, 1) * 
                    Matrix4.CreateTranslation((Vector3)sprite.Box.Centre);

                _shader.SetTextureSlot(0);
                if (Arrow)
                {
                    _arrow.Bind(0);
                }
                else
                {
                    _texture.Bind(0);
                }

                _drawable.Draw();

                _shader.Unbind();
            }
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            _display.Dispose();

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        public bool MoveForward { get; set; }
        public bool MoveBackward { get; set; }
        public bool TurnLeft { get; set; }
        public bool TurnRight { get; set; }
        public Radian TurnAngle { get; set; } = Radian.Degrees(30);
        public double ForwardStrength { get; set; } = 1312.5;
        public double BackwardStrength { get; set; } = 468.75;

        public void ApplyPhysics(double frameTime)
        {
            if (Program.MultiThread)
            {
                Parallel.ForEach(Forces, f =>
                {
                    Velocity v = f.GetForce(new PhyisicsProperties()
                    {
                        Box = _box,
                        Mass = Mass,
                        Velocity = _velocity,
                        NextPosition = _box.Location + (_velocity.Movement * frameTime)
                    }, frameTime);

                    if (v.Speed != 0)
                    {
                        _velocity += v;
                    }
                });
            }
            else
            {
                foreach (IForceController f in Forces)
                {
                    Velocity v = f.GetForce(new PhyisicsProperties()
                    {
                        Box = _box,
                        Mass = Mass,
                        Velocity = _velocity,
                        NextPosition = _box.Location + (_velocity.Movement * frameTime)
                    }, frameTime);

                    if (v.Speed != 0)
                    {
                        _velocity += v;
                    }
                }
            }

            ApplyMovement(frameTime);
            CreatePlanetCollisions(frameTime);

            _box.Location += _velocity.Movement * frameTime;
        }
        private void ApplyMovement(double frameTime)
        {
            if (_velocity.Direction == Vector2.Zero)
            {
                _velocity.Direction = new Vector2(0, 1);
            }

            if (TurnLeft && !TurnRight)
            {
                _velocity.Direction = _velocity.Direction.Rotated(TurnAngle * frameTime);
            }
            else if (TurnRight && !TurnLeft)
            {
                _velocity.Direction = _velocity.Direction.Rotated(-TurnAngle * frameTime);
            }

            if (MoveForward)
            {
                _velocity.Speed += ForwardStrength * frameTime;
            }
            if (MoveBackward)
            {
                _velocity.Speed -= BackwardStrength * frameTime;
            }
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

        private void CreatePlanetCollisions(double frameTime)
        {
            foreach (CollisionForce f in Collisions)
            {
                Velocity v = f.GetCollision(new PhyisicsProperties()
                    {
                        Box = _box,
                        Mass = Mass,
                        Velocity = _velocity,
                        NextPosition = _box.Location + (_velocity.Movement * frameTime)
                    }, frameTime);

                if (v.Speed != 0)
                {
                    _velocity += v;
                }
            }
        }
    }
}
