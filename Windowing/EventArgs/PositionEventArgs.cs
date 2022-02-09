using System;
using Zene.Structs;

namespace Zene.Windowing
{
    public class PositionEventArgs : EventArgs
    {
        public PositionEventArgs(double x, double y)
        {
            Location = new Vector2(x, y);
        }

        public Vector2 Location { get; }
    }
}
