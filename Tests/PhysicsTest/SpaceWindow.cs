using System;
using System.Diagnostics;
using Zene.Graphics;
using Zene.Graphics.Shaders;
using Zene.Windowing;
using Zene.Windowing.Base;
using Zene.Physics;
using Zene.Structs;

namespace PhysicsTest
{
    public class SpaceWindow : Window
    {
        private const int _nPlanets = 1_000;
        private const int _seed = 1;

        public SpaceWindow(int width, int height, string title)
            : base(width, height, title, 4.3)
        {
            _shader = new BasicShader();
            _planetShader = new TextureShader();
            Framebuffer = new PostProcessing(width, height);

            _rocket = new Rocket(0, 0, 15, 15, _shader);

            _viewSize = new Vector2(600, 600);
            _worldSize = new Vector2(6_000_000_000, 6_000_000_000);

            PlanetProperty[] planets = new PlanetProperty[_nPlanets];

            Random r = new Random(_seed);
            //planets[^1] = new PlanetProperty(Vector2.Zero, 1_390_000_000, 1400, Colour3.Zero);
            for (int i = 0; i < _nPlanets; i++)
            {
                double size = r.Next(12_000_000, 139_800_000);

                planets[i] = new PlanetProperty(
                    new Vector2(
                        r.NextDouble(0, _worldSize.X) - (_worldSize.X * 0.5),
                        r.NextDouble(0, _worldSize.Y) - (_worldSize.Y * 0.5)),
                    size,
                    r.NextDouble(PlanetGroup.MinDensity, PlanetGroup.MaxDensity),
                    r.NextColour3());
            }
            _planets = new PlanetGroup(planets, Box.Infinity, _planetShader);
            // Add planet gravity forces to _rocket
            _rocket.Forces.AddRange(_planets.Gravities);
            // Add planet collision forces to _rocket
            _rocket.Collisions.AddRange(_planets.Collisions);

            _background = new Background();

            _textDraw = new TextRenderer(30);
            _font = new FontA();

            // Enabling transparency
            Zene.Graphics.Base.GL.Enable(Zene.Graphics.Base.GLEnum.Blend);
            Zene.Graphics.Base.GL.BlendFunc(Zene.Graphics.Base.GLEnum.SrcAlpha, Zene.Graphics.Base.GLEnum.OneMinusSrcAlpha);

            OnSizeChange(new SizeChangeEventArgs(width, height));
        }

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            if (dispose)
            {
                _shader.Dispose();
                Framebuffer.Dispose();
                _rocket.Dispose();
                _background.Dispose();
                _planets.Dispose();

                _textDraw.Dispose();
            }
        }

        private readonly BasicShader _shader;
        public override PostProcessing Framebuffer { get; }
        private readonly Background _background;

        private readonly PlanetGroup _planets;
        private readonly TextureShader _planetShader;
        private readonly Rocket _rocket;

        private readonly TextRenderer _textDraw;
        private readonly Font _font;

        private Vector2 _viewSize;
        private Vector2 _worldSize;

        public void Run()
        {
            Stopwatch fpsTimer = new Stopwatch();
            fpsTimer.Start();

            // VSync
            GLFW.SwapInterval(1);

            long preFrame = 0;
            double frameTime = 1;

            while (GLFW.WindowShouldClose(Handle) == 0) // Window shouldn't close
            {
                Framebuffer.Bind();

                // Frametime is in seconds
                Draw(frameTime * 0.001);

                Framebuffer.Unbind();
                Framebuffer.Draw();

                GLFW.PollEvents();
                GLFW.SwapBuffers(Handle);
                //System.Threading.Thread.Sleep(16);

                frameTime = fpsTimer.ElapsedMilliseconds - preFrame;
                // Restart stopwatch to make sure it doesn't go over int.MaxValue
                if (fpsTimer.ElapsedMilliseconds > 214748547) // int.MaxValue - 100
                {
                    fpsTimer.Restart();
                }
                preFrame = fpsTimer.ElapsedMilliseconds;
            }
        }
        private void Draw(double frameTime)
        {
            //_planets.ApplyPhysics(frameTime);
            _rocket.ApplyPhysics(frameTime);
            //Console.WriteLine(_rocket.Velocity.Movement.ToString("N3"));
            //Console.WriteLine((1 / frameTime).ToString("N3"));
            //Console.WriteLine(_rocket.Velocity.Speed.ToString("N3"));

            Framebuffer.Clear(BufferBit.Colour | BufferBit.Depth);

            _background.SetTranslation(Matrix4.CreateTranslation((Vector3)(_rocket.Location / (_mSize * 0.1))));
            _background.Draw();

            _shader.Bind();
            // Create and set view matrix
            Matrix4 view = Matrix4.CreateTranslation(Vector3.Zero - _rocket.Location);
            //Console.WriteLine(Vector3.Zero - _rocket.Location);
            _shader.Matrix2 = view;
            _planetShader.View = view;

            _planets.Draw(Box.Infinity);
            _rocket.Display.Draw(_rocket);

            _textDraw.DrawLeftBound($"Speed:{_rocket.Velocity.Speed:N3}", _font, -0.15, 0);
            _textDraw.DrawLeftBound($"\nAngle:{GetAngle(_rocket.Velocity.Direction):N3}", _font, -0.15, 0);
            _textDraw.DrawLeftBound($"\n\nX:{_rocket.Location.X:N3}", _font, -0.15, 0);
            _textDraw.DrawLeftBound($"\n\n\nY:{_rocket.Location.Y:N3}", _font, -0.15, 0);
        }

        private static double GetAngle(Vector2 direction)
        {
            double d = Degrees.Radian(Math.Atan2(direction.X, direction.Y));
            // Make sure it is in the range 0 - 360 not (-180) - 180
            if (d < 0) { d += 360; }

            return d;
        }

        private double _mSize = 600;
        protected override void OnSizeChange(SizeChangeEventArgs e)
        {
            base.OnSizeChange(e);

            double mWidth;
            double mHeight;

            if (e.Width < e.Height)
            {
                double heightPercent = e.Height / e.Width;

                mWidth = _viewSize.X;

                mHeight = _viewSize.X * heightPercent;
            }
            else
            {
                double widthPercent = e.Width / e.Height;

                mHeight = _viewSize.Y;

                mWidth = _viewSize.Y * widthPercent;
            }

            _mSize = (mWidth + mHeight) * 0.5;

            Matrix4 orthMat = Matrix4.CreateOrthographic(mWidth, mHeight, 0, 10);
            _shader.Matrix3 = orthMat;
            _planetShader.Projection = orthMat;

            Framebuffer.Size = this.Size;

            _rocket.Arrow = mWidth > 6000;
            _rocket.Multiplier = (mWidth / _rocket.Box.Width) * 0.005;

            // Set text manipulation to be in the top-left corner
            // and a width of 0.013% of window width
            _textDraw.Projection = Matrix4.CreateOrthographic(e.Width, e.Height, 0, -1);
            double charSize = Width * 0.015;
            _textDraw.Model = Matrix4.CreateScale(charSize, charSize, 1) * 
                Matrix4.CreateTranslation(
                    (-e.Width * 0.5) + charSize * 0.15, 
                    (e.Height * 0.5) - charSize * 0.3, 0);
        }
        private Vector2 _viewSizeTemp;
        private bool _fullView = false;
        private bool _space = false;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Keys.Up)
            {
                _rocket.MoveForward = true;
                return;
            }
            if (e.Key == Keys.Down)
            {
                _rocket.MoveBackward = true;
                return;
            }
            if (e.Key == Keys.Left)
            {
                _rocket.TurnLeft = true;
                return;
            }
            if (e.Key == Keys.Right)
            {
                _rocket.TurnRight = true;
                return;
            }
            if (e.Key == Keys.Escape)
            {
                Close();
                return;
            }
            if (e.Key == Keys.Space)
            {
                _space = true;

                if (_rocket.ForwardStrength == 1312.5)
                {
                    _rocket.ForwardStrength *= 100;
                    _rocket.BackwardStrength *= 100;
                }
                return;
            }
            if (e.Modifier == Mods.Control)
            {
                return;
            }
            if (e.Modifier == Mods.Shift)
            {
                if (_rocket.TurnAngle == Radian.Degrees(30))
                {
                    _rocket.TurnAngle = Radian.Degrees(10);
                }
                return;
            }
            if (e.Key == Keys.Z)
            {
                if (_fullView)
                {
                    _viewSize = _viewSizeTemp;
                    _fullView = false;
                    // Update orthographic matrix
                    UpdateViewSize();
                    return;
                }

                _viewSizeTemp = _viewSize;
                _viewSize = _worldSize;
                _fullView = true;
                // Update orthographic matrix
                UpdateViewSize();
                return;
            }
            if (e.Key == Keys.Enter)
            {
                _rocket.Location = new Vector2(0, 0);
                _rocket.Velocity = Velocity.Zero;
                return;
            }
            if (e.Key == Keys.BackSpace)
            {
                _rocket.Velocity = Velocity.Zero;
                return;
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Keys.Up)
            {
                _rocket.MoveForward = false;
                return;
            }
            if (e.Key == Keys.Down)
            {
                _rocket.MoveBackward = false;
                return;
            }
            if (e.Key == Keys.Left)
            {
                _rocket.TurnLeft = false;
                return;
            }
            if (e.Key == Keys.Right)
            {
                _rocket.TurnRight = false;
                return;
            }
            if (e.Key == Keys.Space)
            {
                _space = false;

                if (_rocket.ForwardStrength != 1312.5)
                {
                    _rocket.ForwardStrength *= 0.01;
                    _rocket.BackwardStrength *= 0.01;
                }
                return;
            }
            if (e.Modifier == Mods.Shift)
            {
                if (_rocket.TurnAngle != Radian.Degrees(30))
                {
                    _rocket.TurnAngle = Radian.Degrees(30);
                }
                return;
            }
        }

        protected override void OnScroll(ScrollEventArgs e)
        {
            base.OnScroll(e);

            double scroll;

            if (_space)
            {
                scroll = -e.DeltaY * 3_000;
            }
            else
            {
                scroll = -e.DeltaY * 30;
            }

            if ((_viewSize.X + scroll) < 1 || (_viewSize.Y + scroll) < 1)
            {
                return;
            }
            _viewSize += scroll;
            UpdateViewSize();
        }

        private void UpdateViewSize()
        {
            // Update orthographic matrix
            GLFW.GetWindowSize(Handle, out int w, out int h);
            OnSizeChange(new SizeChangeEventArgs(w, h));
        }
    }
}
