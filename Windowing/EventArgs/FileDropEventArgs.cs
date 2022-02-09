using System;

namespace Zene.Windowing
{
    public class FileDropEventArgs : EventArgs
    {
        public FileDropEventArgs(string[] paths)
        {
            Paths = paths;
        }

        public string[] Paths { get; }
    }
}
