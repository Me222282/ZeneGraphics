using System;

namespace Zene.Windowing
{
    public class ScrollEventArgs : EventArgs
    {
        public ScrollEventArgs(double scrollX, double scrollY)
        {
            DeltaX = scrollX;
            DeltaY = scrollY;
        }

        public double DeltaX { get; }

        public double DeltaY { get; }
    }
}
