namespace Zene.Windowing
{
    /// <summary>
    /// An object that manages all the per window initialization properties.
    /// </summary>
    public class WindowInitProperties
    {
        public bool Resizable { get; set; } = true;
        public bool Visible { get; set; } = true;
        public bool Decorated { get; set; } = true;
        public bool Focused { get; set; } = true;
        public bool AutoIconify { get; set; } = false;
        public bool AlwaysOnTop { get; set; } = false;
        public bool Maximized { get; set; } = false;
        public bool CentreCursor { get; set; } = false;
        public bool TransparentFramebuffer { get; set; } = false;
        public bool FocusOnShow { get; set; } = true;
        public bool ScaleToMonitor { get; set; } = false;

        public int RedBits { get; set; } = 8;
        public int GreenBits { get; set; } = 8;
        public int BlueBits { get; set; } = 8;
        public int AlphaBits { get; set; } = 8;
        public int DepthBits { get; set; } = 24;
        public int StencilBits { get; set; } = 4;
        public bool Stereoscopic { get; set; } = false;
        public int Samples { get; set; } = 0;
        public bool SRGBSupported { get; set; } = false;
        public bool DoubleBuffered { get; set; } = true;

        public int RefreshRate { get; set; } = -1;

        public Monitor Monitor { get; set; } = Monitor.None;
        public Window SharedWindow { get; set; } = Window.None;

        public static WindowInitProperties Default { get; } = new WindowInitProperties();
    }
}
