using System;
using Zene.Structs;

namespace Zene.Windowing
{
    public class SizeChangeEventArgs : EventArgs
    {
        public SizeChangeEventArgs(int w, int h)
        {
            Size = new Vector2I(w, h);
        }

        public int Width => Size.X;
        public int Height => Size.Y;
        public Vector2I Size { get; }
    }
}
