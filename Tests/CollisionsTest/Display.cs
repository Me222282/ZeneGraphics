using System;
using System.Drawing;
using Zene.Sprites;
using Zene.Structs;

namespace CollisionsTest
{
    public struct Display : IDisplay
    {
        public void Draw(Sprite sprite)
        {
            sprite.Canvas.FillRectangle(new SolidBrush(sprite.Colour), 
                new System.Drawing.Rectangle(
                    (int)sprite.Box.Left,
                    (int)-sprite.Box.Top,
                    (int)sprite.Box.Width,
                    (int)sprite.Box.Height));

            Box newPos = sprite.Box.Shifted(sprite.Direction * sprite.Velocity);

            sprite.Canvas.FillRectangle(new SolidBrush(Color.FromArgb(128, sprite.Colour)),
                new System.Drawing.Rectangle(
                    (int)newPos.Left,
                    (int)-newPos.Top,
                    (int)newPos.Width,
                    (int)newPos.Height));

            sprite.Canvas.DrawPolygon(new Pen(Color.Orange, 2), new Point[]
            {
                new Point((int)sprite.Box.Left, (int)-sprite.Box.Top),
                new Point((int)sprite.Box.Right, (int)-sprite.Box.Top),
                new Point((int)newPos.Right, (int)-newPos.Top),
                new Point((int)newPos.Right, (int)-newPos.Bottom),
                new Point((int)newPos.Left, (int)-newPos.Bottom),
                new Point((int)sprite.Box.Left, (int)-sprite.Box.Bottom),
            });
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        void IDisplay.Draw(ISprite source)
        {
            Draw((Sprite)source);
        }
    }

    public struct BoxDisplay : IDisplay
    {
        public void Draw(BoxSprite sprite)
        {
            sprite.Canvas.FillRectangle(new SolidBrush(sprite.Colour),
                new System.Drawing.Rectangle(
                    (int)sprite.Box.Left,
                    (int)-sprite.Box.Top,
                    (int)sprite.Box.Width,
                    (int)sprite.Box.Height));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        void IDisplay.Draw(ISprite source)
        {
            Draw((BoxSprite)source);
        }
    }
}
