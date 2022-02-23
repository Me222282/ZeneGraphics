using System.Drawing;
using Zene.Sprites;
using Zene.Structs;

namespace CollisionsTest
{
    public struct Sprite : ISprite
    {
        public Sprite(Box box)
        {
            Box = box;
            Display = new Display();
            Colour = Color.Red;
            Canvas = null;
            Direction = Vector2.Zero;
            Velocity = 0;
        }

        public Box Box { get; set; }
        IBox ISprite.Bounds => Box;

        public Display Display { get; set; }
        IDisplay ISprite.Display => Display;

        public Graphics Canvas { get; set; }

        public Color Colour { get; set; }

        public Vector2 Direction { get; set; }
        public double Velocity { get; set; }
    }

    public class BoxSprite : ISprite
    {
        public BoxSprite(Vector2 loc, Vector2 size)
        {
            Box = new Box(loc, size);
            Display = new BoxDisplay();
            Colour = Color.Beige;
            Canvas = null;
        }

        public Box Box { get; set; }
        IBox ISprite.Bounds => Box;

        public IDisplay Display { get; set; }

        public Graphics Canvas { get; set; }

        public Color Colour { get; set; }
    }
}
