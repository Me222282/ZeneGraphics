using System;

namespace Zene.Windowing
{
    public class ScrollEventArgs : EventArgs
    {
        public ScrollEventArgs(double scrollX, double scrollY)
        {
            XDelta = scrollX;
            YDelta = scrollY;
        }

        public double XDelta { get; }

        public double YDelta { get; }
    }
}
