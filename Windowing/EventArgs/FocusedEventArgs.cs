using System;

namespace Zene.Windowing
{
    public class FocusedEventArgs : EventArgs
    {
        public FocusedEventArgs(bool focused)
        {
            Focus = focused;
        }

        public bool Focus { get; }
    }
}
