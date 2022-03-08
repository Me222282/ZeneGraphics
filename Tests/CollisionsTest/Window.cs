using System;
using Zene.Structs;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using Zene.Physics;

namespace CollisionsTest
{
    public class Window : Form
    {
        public Window()
        {
            _timer = new System.Timers.Timer(1000 / FPS);
            _timer.Elapsed += Frame;

            _collider = new Sprite(
                    new Box(
                        location: new Vector2(100, -100),
                        size: new Vector2(100, 100)));

            Vector2 size = new Vector2(150, 150);

            _boxes = new BoxSprite[]
            {
                new BoxSprite(new Vector2(100, -300), size),
                new BoxSprite(new Vector2(200, -150), size),
                new BoxSprite(new Vector2(300, -500), size),
                new BoxSprite(new Vector2(250, -600), size),
                new BoxSprite(new Vector2(700, -700), size),
                new BoxSprite(new Vector2(1000, -1000), size)
            };

            _timer.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            AllocConsole();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private readonly BoxSprite[] _boxes;

        private void Frame(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Update _collider movement to be distance from mouse position
            _collider.Direction = (_mousePos - _collider.Box.Location).Normalised();
            _collider.Velocity = _collider.Box.Location.Distance(_mousePos);

            if (Collisions.Grid(_collider.Box, _collider.Direction, _collider.Velocity, GetBoxes(_boxes)))
            {
                _collider.Colour = Color.Aqua;
            }
            else
            {
                _collider.Colour = Color.Red;
            }

            Invalidate(false);
        }

        private Sprite _collider;

        private readonly System.Timers.Timer _timer;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (BoxSprite s in _boxes)
            {
                s.Canvas = e.Graphics;
                s.Display.Draw(s);
            }

            _collider.Canvas = e.Graphics;
            _collider.Display.Draw(_collider);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            _collider.Box = new Box(_mousePos, _collider.Box.Size);
        }

        private Vector2 _mousePos;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            _mousePos = new Vector2(e.X, -e.Y);
        }

        private const double FPS = 30;

        private static IBox[] GetBoxes(BoxSprite[] sprites)
        {
            IBox[] boxes = new IBox[sprites.Length];

            for (int i = 0; i < sprites.Length; i++)
            {
                boxes[i] = sprites[i].Box;
            }

            return boxes;
        }
    }
}
