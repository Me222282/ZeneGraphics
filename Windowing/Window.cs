using System;
using System.Runtime.InteropServices;
using Zene.Graphics;
using Zene.Graphics.Base;
using Zene.Structs;
using Zene.Windowing.Base;

namespace Zene.Windowing
{
    public unsafe class Window : IDisposable
    {
        private Window(IntPtr window)
        {
            _window = window;
        }

        public Window(int width, int height, string title, WindowInitProperties properties = null)
            : this(width, height, title, 4.5, properties)
        {

        }
        public Window(int width, int height, string title, double version, WindowInitProperties properties = null)
        {
            //GLFW.WindowHint(GLFW.GLFW_DOUBLEBUFFER, GLFW.GLFW_FALSE);
            GLFW.WindowHint(GLFW.ClientApi, GLFW.OpenglApi);
            if (version < 3.2)
            {
                GLFW.WindowHint(GLFW.OpenglProfile, GLFW.OpenglAnyProfile);
            }
            else
            {
                GLFW.WindowHint(GLFW.OpenglProfile, GLFW.OpenglCoreProfile);
            }
            GLFW.WindowHint(GLFW.ContextVersionMajor, (int)Math.Floor(version));
            GLFW.WindowHint(GLFW.ContextVersionMinor, (int)Math.Floor(
                (version - (int)Math.Floor(version)) * 10));

            // Make sure there is no null refernace exception
            if (properties == null)
            {
                properties = WindowInitProperties.Default;
            }

            Core.SetInitProperties(properties);
            RefreshRate = properties.RefreshRate;
            _baseFramebuffer = new BaseFramebuffer(
                properties.Stereoscopic,
                properties.DoubleBuffered,
                properties.Samples,
                width, height);
            _baseFramebuffer.Size(width, height);

            _window = GLFW.CreateWindow(width, height, title, properties.Monitor.Handle, properties.SharedWindow.Handle);

            if (_window == IntPtr.Zero)
            {
                _disposed = true;
                GLFW.Terminate();
                throw new InvalidOperationException("Failed to create window.");
            }

            GLFW.MakeContextCurrent(_window);

            // Full screen required properties
            _normalWidth = Width;
            _normalHeight = Height;
            _normalLocation = Location;

            SetCallBacks();

            GL.Init(GLFW.GetProcAddress, version);

            // Setup debug callback - error output/display
            // If supported in current opengl version
            if (((Action<GL.DebugProc, IntPtr>)GL.DebugMessageCallback).GLSupported())
            {
                OnDebugCallBack = (source, type, _, _, _, message, _) => GLError(type, message);

                GL.Enable(GLEnum.DebugOutput);
                GL.DebugMessageCallback(OnDebugCallBack, null);
            }
        }

        private readonly GL.DebugProc OnDebugCallBack;
        protected virtual void GLError(uint type, string message)
        {
            if (type == GLEnum.DebugTypeError && message != null)
            {
                Console.WriteLine($"GL Error: {message}");
            }
        }

        private readonly IntPtr _window;
        
        public IntPtr Handle
        {
            get
            {
                return _window;
            }
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);

                _disposed = true;

                GC.SuppressFinalize(this);
            }
        }

        protected virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                GLFW.DestroyWindow(_window);
                //GLFW.Terminate();
            }
        }

        public void Close()
        {
            GLFW.SetWindowShouldClose(_window, 1);
        }

        private readonly BaseFramebuffer _baseFramebuffer;
        public virtual IFramebuffer Framebuffer => _baseFramebuffer;

        private int _normalWidth;
        private int _normalHeight;
        private Vector2I _normalLocation;

        private int _refreshRate;
        public int RefreshRate
        {
            get => _refreshRate;
            set
            {
                _refreshRate = value;

                // Resets window's monitor if none - thus changing refresh rate
                if (!_fullScreen)
                {
                    FullScreen = _fullScreen;
                }
            }
        }

        private bool _fullScreen = false;
        public bool FullScreen
        {
            get => _fullScreen;
            set
            {
                _fullScreen = value;

                if (value)
                {
                    _normalWidth = Width;
                    _normalHeight = Height;
                    _normalLocation = Location;

                    Monitor monitor = Monitor.Primary;
                    VideoMode videoMode = monitor.VideoMode;

                    GLFW.SetWindowMonitor(_window, monitor.Handle, 0, 0, videoMode.Width, videoMode.Height, videoMode.RefreshRate);
                }
                else
                {
                    GLFW.SetWindowMonitor(_window, IntPtr.Zero, _normalLocation.X, _normalLocation.Y, _normalWidth, _normalHeight, RefreshRate);
                }
            }
        }

        public int Width
        {
            get
            {
                GLFW.GetWindowSize(_window, out int w, out _);

                return w;
            }
            set
            {
                GLFW.SetWindowSize(_window, value, Height);
            }
        }
        public int Height
        {
            get
            {
                GLFW.GetWindowSize(_window, out _, out int h);

                return h;
            }
            set
            {
                GLFW.SetWindowSize(_window, Width, value);
            }
        }
        public Vector2I Size
        {
            get
            {
                GLFW.GetWindowSize(_window, out int w, out int h);

                return new Vector2I(w, h);
            }
            set
            {
                GLFW.SetWindowSize(_window, value.X, value.Y);
            }
        }

        public Vector2I Location
        {
            get
            {
                GLFW.GetWindowPos(_window, out int x, out int y);

                return new Vector2I(x, y);
            }
            set
            {
                GLFW.SetWindowPos(_window, value.X, value.Y);
            }
        }
        public Vector2 MouseLocation
        {
            get
            {
                GLFW.GetCursorPos(_window, out double x, out double y);

                return new Vector2(x, y);
            }
            set
            {
                GLFW.SetCursorPos(_window, value.X, value.Y);
            }
        }

        private Cursor _cursor;
        public Cursor CursorStyle
        {
            get => _cursor;
            set
            {
                _cursor = value;

                if (value == null)
                {
                    GLFW.SetCursor(_window, IntPtr.Zero);
                    return;
                }

                GLFW.SetCursor(_window, value.Handle);
            }
        }

        private GLFW.FileDropHandler _onFileDropCallBack;
        private GLFW.CharHandler _onTextInput;
        private GLFW.KeyChangeHandler _onKeyCallBack;
        private GLFW.ScrollHandler _onScrollCallBack;
        private GLFW.MouseEnterHandler _onCursorEnterCallBack;
        private GLFW.MousePosHandler _onMouseMoveCalBack;
        private GLFW.MouseButtonHandler _onMouseButtonCallBack;
        private GLFW.WindowSizeHandler _onSizeCallBack;
        private GLFW.WindowMaximizeHandler _onMaximizedCallBack;
        private GLFW.WindowFocusHandler _onFocusCallBack;
        private GLFW.WindowRefreshHandler _onRefreshCallBack;
        private GLFW.WindowCloseHandler _onCloseCallBack;
        private GLFW.WindowPosHandler _onWindowMoveCallBack;
        private GLFW.FramebufferSizeHandler _onFrameBufferCallBack;

        public event FileDropEventHandler FileDrop;
        public event TextInputEventHandler TextInput;
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;
        public event ScrolEventHandler Scroll;
        public event EventHandler MouseEnter;
        public event EventHandler MouseLeave;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseUp;
        public event SizeChangeEventHandler SizeChange;
        public event EventHandler Maximized;
        public event FocusedEventHandler Focus;
        public event EventHandler Refresh;
        public event EventHandler Closing;
        public event PositionEventHandler WindowMove;
        public event SizeChangeEventHandler SizePixelChange;

        private void SetCallBacks()
        {
            _onFileDropCallBack = (_, count, paths) => DropCallBack(count, paths);
            _onTextInput = (_, unicode) => OnTextInput(new TextInputEventArgs((char)unicode));
            _onKeyCallBack = (_, key, dumy, action, mods) => KeyCallBack(key, action, mods);
            _onScrollCallBack = (_, x, y) => OnScroll(new ScrollEventArgs(x, y));
            _onCursorEnterCallBack = (_, enter) => MouseOver(enter == 1);
            _onMouseMoveCalBack = (_, x, y) => OnMouseMove(new MouseEventArgs(x, y));
            _onMouseButtonCallBack = (_, button, action, mod) => MouseButon(button, action, mod);
            _onSizeCallBack = (_, width, height) => OnSizeChange(new SizeChangeEventArgs(width, height));
            _onMaximizedCallBack = (_, i) => OnMaximized(new EventArgs());
            _onFocusCallBack = (_, focus) => OnFocus(new FocusedEventArgs(focus == 1));
            _onRefreshCallBack = (_) => OnRefresh(new EventArgs());
            _onCloseCallBack = (_) => OnClosing(new EventArgs());
            _onWindowMoveCallBack = (_, x, y) => OnWindowMove(new PositionEventArgs(x, y));
            _onFrameBufferCallBack = (_, width, height) => OnSizePixelChange(new SizeChangeEventArgs(width, height));

            GLFW.SetDropCallback(_window, _onFileDropCallBack);
            GLFW.SetCharCallback(_window, _onTextInput);
            GLFW.SetKeyCallback(_window, _onKeyCallBack);
            GLFW.SetScrollCallback(_window, _onScrollCallBack);
            GLFW.SetCursorEnterCallback(_window, _onCursorEnterCallBack);
            GLFW.SetCursorPosCallback(_window, _onMouseMoveCalBack);
            GLFW.SetMouseButtonCallback(_window, _onMouseButtonCallBack);
            GLFW.SetWindowSizeCallback(_window, _onSizeCallBack);
            GLFW.SetWindowMaximizeCallback(_window, _onMaximizedCallBack);
            GLFW.SetWindowFocusCallback(_window, _onFocusCallBack);
            GLFW.SetWindowCloseCallback(_window, _onCloseCallBack);
            GLFW.SetWindowPosCallback(_window, _onWindowMoveCallBack);
            GLFW.SetFramebufferSizeCallback(_window, _onFrameBufferCallBack);
        }

        private void DropCallBack(int count, IntPtr paths)
        {
            string[] arrayOfPaths = new string[count];

            byte** data = (byte**)paths.ToPointer();

            for (int i = 0; i < count; i++)
            {
                arrayOfPaths[i] = Marshal.PtrToStringUTF8(new IntPtr(data[i]));
            }

            OnFileDrop(new FileDropEventArgs(arrayOfPaths));
        }
        private void KeyCallBack(int key, int action, int mods)
        {
            if (action == GLFW.Press || action == GLFW.Repeat)
            {
                OnKeyDown(new KeyEventArgs((Keys)key, (Mods)mods, (KeyAction)action));
            }
            else
            {
                OnKeyUp(new KeyEventArgs((Keys)key, (Mods)mods, (KeyAction)action));
            }
        }
        private void MouseOver(bool entered)
        {
            if (entered)
            {
                OnMouseEnter(new EventArgs());
            }
            else
            {
                OnMouseLeave(new EventArgs());
            }
        }
        private void MouseButon(int button, int action, int mods)
        {
            if (action == GLFW.Press)
            {
                OnMouseDown(new MouseEventArgs(MouseLocation, (MouseButton)button, (Mods)mods));
            }
            else
            {
                OnMouseUp(new MouseEventArgs(MouseLocation, (MouseButton)button, (Mods)mods));
            }
        }

        protected virtual void OnFileDrop(FileDropEventArgs e)
        {
            FileDrop?.Invoke(this, e);
        }
        protected virtual void OnTextInput(TextInputEventArgs e)
        {
            TextInput?.Invoke(this, e);
        }
        protected virtual void OnKeyDown(KeyEventArgs e)
        {
            KeyDown?.Invoke(this, e);
        }
        protected virtual void OnKeyUp(KeyEventArgs e)
        {
            KeyUp?.Invoke(this, e);
        }
        protected virtual void OnScroll(ScrollEventArgs e)
        {
            Scroll?.Invoke(this, e);
        }
        protected virtual void OnMouseEnter(EventArgs e)
        {
            MouseEnter?.Invoke(this, e);
        }
        protected virtual void OnMouseLeave(EventArgs e)
        {
            MouseLeave?.Invoke(this, e);
        }
        protected virtual void OnMouseMove(MouseEventArgs e)
        {
            MouseMove?.Invoke(this, e);
        }
        protected virtual void OnMouseDown(MouseEventArgs e)
        {
            MouseDown?.Invoke(this, e);
        }
        protected virtual void OnMouseUp(MouseEventArgs e)
        {
            MouseUp?.Invoke(this, e);
        }
        protected virtual void OnSizeChange(SizeChangeEventArgs e)
        {
            SizeChange?.Invoke(this, e);
        }
        protected virtual void OnMaximized(EventArgs e)
        {
            Maximized?.Invoke(this, e);
        }
        protected virtual void OnFocus(FocusedEventArgs e)
        {
            Focus?.Invoke(this, e);
        }
        protected virtual void OnRefresh(EventArgs e)
        {
            Refresh?.Invoke(this, e);
        }
        protected virtual void OnClosing(EventArgs e)
        {
            Closing?.Invoke(this, e);
        }
        protected virtual void OnWindowMove(PositionEventArgs e)
        {
            WindowMove?.Invoke(this, e);
        }
        protected virtual void OnSizePixelChange(SizeChangeEventArgs e)
        {
            SizePixelChange?.Invoke(this, e);

            _baseFramebuffer.Size((int)e.Width, (int)e.Height);
        }

        /// <summary>
        /// A null value window.
        /// </summary>
        public static Window None { get; } = new Window(IntPtr.Zero);
        /// <summary>
        /// Creates a window from an already created window.
        /// </summary>
        /// <param name="handle">The GLFW pointer to a window object.</param>
        /// <returns></returns>
        public static Window FromHandle(IntPtr handle)
        {
            return new Window(handle);
        }
    }
}
