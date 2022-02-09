using System;

namespace Zene.Windowing
{
    public class SizeChangeEventArgs : EventArgs
    {
        public SizeChangeEventArgs(double w, double h)
        {
            Width = w;
            Height = h;
        }

        public double Width { get; }
        public double Height { get; }
    }
}
