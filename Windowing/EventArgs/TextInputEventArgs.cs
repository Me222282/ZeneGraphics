using System;

namespace Zene.Windowing
{
    public class TextInputEventArgs : EventArgs
    {
        public TextInputEventArgs(char character)
        {
            Character = character;
        }

        public char Character { get; }
    }
}
