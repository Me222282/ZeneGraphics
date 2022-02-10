using System;
using Zene.Graphics;
using Zene.Structs;
using Zene.Windowing;

namespace ImplicitFunctions
{
    public class Player : IDisposable
    {
        public const double PullStrength = 5_000_000;

        public Player()
        {
            _shader = new CircleShader();
            _drawable = new DrawObject<double, byte>(new double[]
            {
                // Pos:        Coords:
                -0.5, 0.5,      0, 1,
                -0.5, -0.5,     0, 0,
                0.5, -0.5,      1, 0,
                0.5, 0.5,       1, 1
            }, new byte[] { 0, 1, 2, 2, 3, 0 }, 4, 0, AttributeSize.D2, BufferUsage.DrawFrequent);
            _drawable.AddAttribute(1, 2, AttributeSize.D2);
        }

        private readonly DrawObject<double, byte> _drawable;
        private readonly CircleShader _shader;

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            _shader.Dispose();
            _drawable.Dispose();

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private Vector2 _position;
        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        public Vector2 Size { get; set; } = new Vector2(45, 45);
        public double Radius
        {
            get
            {
                return Size.X * 0.5;
            }
        }

        public void Draw(double frameTime)
        {
            if (_up)
            {
                _position.Y += 300 * frameTime;
            }
            if (_down)
            {
                _position.Y -= 300 * frameTime;
            }
            if (_left)
            {
                _position.X -= 300 * frameTime;
            }
            if (_right)
            {
                _position.X += 300 * frameTime;
            }

            _shader.Bind();
            _shader.Model = Matrix4.CreateScale(Size.X, Size.Y, 1) *
                Matrix4.CreateTranslation((Vector3)Position);

            _drawable.Draw();
        }

        private bool _up = false;
        private bool _down = false;
        private bool _left = false;
        private bool _right = false;
        public bool Collecting { get; set; } = false;

        public void KeyDown(Keys key)
        {
            if (key == Keys.Left)
            {
                _left = true;
                return;
            }
            if (key == Keys.Right)
            {
                _right = true;
                return;
            }
            if (key == Keys.Up)
            {
                _up = true;
                return;
            }
            if (key == Keys.Down)
            {
                _down = true;
                return;
            }
            if (key == Keys.Space)
            {
                Collecting = true;
                return;
            }
        }
        public void KeyUp(Keys key)
        {
            if (key == Keys.Left)
            {
                _left = false;
                return;
            }
            if (key == Keys.Right)
            {
                _right = false;
                return;
            }
            if (key == Keys.Up)
            {
                _up = false;
                return;
            }
            if (key == Keys.Down)
            {
                _down = false;
                return;
            }
            if (key == Keys.Space)
            {
                Collecting = false;
                return;
            }
        }

        public void SizeChange(double width, double height)
        {
            _shader.Projection = Matrix4.CreateOrthographic(width, height, 0, -1);
        }
    }
}
