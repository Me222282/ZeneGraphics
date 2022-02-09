using System;

namespace Zene.Windowing
{
    public class KeyEventArgs : EventArgs
    {
        public KeyEventArgs(Keys key, Mods mod, KeyAction action)
        {
            Key = key;
            Modifier = mod;
            Action = action;
        }

        public Keys Key { get; }

        public Mods Modifier { get; }

        public KeyAction Action { get; }
    }
}
