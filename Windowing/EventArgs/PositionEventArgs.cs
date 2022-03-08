using System;
using Zene.Structs;

namespace Zene.Windowing
{
    public class PositionEventArgs : EventArgs
    {
        public PositionEventArgs(int x, int y)
        {
            Location = new Vector2I(x, y);
        }

        public int X => Location.X;
        public int Y => Location.Y;
        public Vector2I Location { get; }
    }
}
