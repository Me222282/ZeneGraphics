using Zene.Windowing;
using Zene.Graphics;
using Zene.Structs;
using System;
using System.Diagnostics;
using Zene.Windowing.Base;

namespace ImplicitFunctions
{
    public class Game : Window
    {
        private const int BallCount = 512;

        private class Ball
        {
            public Ball(Vector2 location, double radius, Vector2 velocity, Player player, MetaBallShader shader, int index)
            {
                _shader = shader;
                _index = index;
                _velocity = velocity;

                _player = player;

                _shader[_index] = new Vector3(location.X, location.Y, radius);
            }

            private readonly MetaBallShader _shader;
            private readonly Player _player;
            private readonly int _index;

            public double Radius
            {
                get
                {
                    return _shader[_index].Z;
                }
                set
                {
                    Vector3 v = _shader[_index];

                    _shader[_index] = new Vector3(v.X, v.Y, value);
                }
            }
            public Vector2 Location
            {
                get
                {
                    return (Vector2)_shader[_index];
                }
                set
                {
                    _shader[_index] = new Vector3(value.X, value.Y, _shader[_index].Z);
                }
            }

            private Vector2 _velocity;
            public Vector2 Velocity
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
            public void Update(IBox bounds, double frameTime)
            {
                if (_removing)
                {
                    Radius *= 1 - (0.5 * frameTime);
                }

                // Radius too small to be significant
                if (Radius < 0.000001)
                {
                    _velocity = Vector2.Zero;
                    Radius = 0;
                    return;
                }

                _velocity *= 1 - (0.01 * frameTime);

                if (_player.Collecting && !_removing)
                {
                    // Use newtons equation g = G*M / r^2 to find the force of the gravity
                    double distance = Location.Distance(_player.Position);
                    _velocity += (_player.Position - Location).Normalised() * frameTime * ((Player.PullStrength) / (distance * distance));
                }

                Vector2 location = Location + (_velocity * frameTime);

                Box b = new Box(location, new Vector2(Radius * 2));

                // Outside of bounds on left
                if (bounds.Left >= b.Left)
                {
                    // Create bounce
                    if (_velocity.X < 0)
                    {
                        _velocity.X = -_velocity.X;
                    }
                    // Snap to bounds
                    location.X -= b.Left - bounds.Left;
                }
                // Outside of bounds on right
                if (bounds.Right <= b.Right)
                {
                    // Create bounce
                    if (_velocity.X > 0)
                    {
                        _velocity.X = -_velocity.X;
                    }
                    // Snap to bounds
                    location.X -= b.Right - bounds.Right;
                }
                // Outside of bounds on top
                if (bounds.Top <= b.Top)
                {
                    // Create bounce
                    if (_velocity.Y > 0)
                    {
                        _velocity.Y = -_velocity.Y;
                    }
                    // Snap to bounds
                    location.Y -= b.Top - bounds.Top;
                }
                // Outside of bounds on bottom
                if (bounds.Bottom >= b.Bottom)
                {
                    // Create bounce
                    if (_velocity.Y < 0)
                    {
                        _velocity.Y = -_velocity.Y;
                    }
                    // Snap to bounds
                    location.Y -= b.Bottom - bounds.Bottom;
                }

                if (_player.Collecting)
                {
                    if (_player.Position.Distance(location) <= (_player.Radius + Radius))
                    {
                        _removing = true;
                        return;
                    }
                }

                Location = location;
            }

            private bool _removing = false;
        }

        public Game(int width, int height, string title)
            : base(width, height, title)
        {
            _oilObj = new DrawObject<double, byte>(new double[]
            {
                // Pos:     Colour:
                -1, 1,      0, 0.7, 0.2,
                -1, -1,     0, 0.7, 0.2,
                1, -1,      0, 0.3, 0.6,
                1, 1,       0, 0.3, 0.6
            }, new byte[] { 0, 1, 2, 2, 3, 0 }, 5, 0, AttributeSize.D2, BufferUsage.DrawFrequent);
            _oilObj.AddAttribute(1, 0, AttributeSize.D2);
            _oilObj.AddAttribute(2, 2, AttributeSize.D3);

            _shader = new MetaBallShader(BallCount)
            {
                Scale = new Vector2(width, height),
                Offset = Vector2.Zero
            };
            _bounds = new Box(-width, width, height, -height);

            _player = new Player();

            _balls = new Ball[BallCount];
            Random r = new Random();
            for (int i = 0; i < BallCount; i++)
            {
                _balls[i] = new Ball(Vector2.Zero, (r.NextDouble() * 3) + 2, Vector2.Random(r, -200, 200), _player, _shader, i);
            }

            // Enabling transparency
            Zene.Graphics.Base.GL.Enable(Zene.Graphics.Base.GLEnum.Blend);
            Zene.Graphics.Base.GL.BlendFunc(Zene.Graphics.Base.GLEnum.SrcAlpha, Zene.Graphics.Base.GLEnum.OneMinusSrcAlpha);
        }

        private readonly DrawObject<double, byte> _oilObj;
        private readonly MetaBallShader _shader;
        private readonly Player _player;

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            if (dispose)
            {
                _oilObj.Dispose();
                _shader.Dispose();
                _player.Dispose();
            }
        }

        private readonly Ball[] _balls;
        private Box _bounds;

        public void Run()
        {
            // Vsync
            GLFW.SwapInterval(GLFW.True);

            Stopwatch s = new Stopwatch();
            long time = 0;

            s.Start();

            while (GLFW.WindowShouldClose(Handle) == GLFW.False)
            {
                long elap = s.ElapsedMilliseconds;
                double frameTime = (elap - time) * 0.001;
                time = elap;

                GLFW.PollEvents();
                // Clear screen black
                BaseFramebuffer.Clear(BufferBit.Colour);

                foreach (Ball b in _balls)
                {
                    b.Update(_bounds, frameTime);
                }

                _shader.Bind();
                _shader.ParseBalls();
                _oilObj.Draw();

                _player.Draw(frameTime);

                GLFW.SwapBuffers(Handle);
            }

            Dispose();
        }

        protected override void OnSizeChange(SizeChangeEventArgs e)
        {
            base.OnSizeChange(e);

            BaseFramebuffer.ViewSize = new Vector2I((int)e.Width, (int)e.Height);

            _shader.Scale = new Vector2(e.Width * 2, e.Height * 2);
            _bounds = new Box(-e.Width * 2, e.Width * 2, e.Height * 2, -e.Height * 2);

            _player.SizeChange(e.Width * 4, e.Height * 4);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Keys.Escape)
            {
                Close();
            }

            _player.KeyDown(e.Key);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            _player.KeyUp(e.Key);
        }
    }
}
