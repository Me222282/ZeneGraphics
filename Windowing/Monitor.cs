using System;
using System.Runtime.InteropServices;
using Zene.Structs;
using Zene.Windowing.Base;

namespace Zene.Windowing
{
    /// <summary>
    /// An object that manages a GLFW monitor.
    /// </summary>
    public unsafe struct Monitor
    {
        private Monitor(IntPtr handle)
        {
            _monitor = handle;
        }

        private readonly IntPtr _monitor;
        /// <summary>
        /// Gets the pointer to the GLFW object.
        /// </summary>
        public IntPtr Handle => _monitor;

        /// <summary>
        /// Gets the video mode currently being used by the monitor
        /// </summary>
        public VideoMode VideoMode
        {
            get
            {
                return GLFW.GetVideoMode(_monitor);
            }
        }
        /// <summary>
        /// Gets an array of supported video modes for this monitor.
        /// </summary>
        public VideoMode[] SupportedVideoModes
        {
            get
            {
                IntPtr ptr = GLFW.GetVideoModes(_monitor, out int length);

                int byteSize = sizeof(VideoMode);

                VideoMode[] array = new VideoMode[length];

                for (int i = 0; i < length; i++)
                {
                    array[i] = Marshal.PtrToStructure<VideoMode>(new IntPtr(ptr.ToInt64() + (i * byteSize)));
                }

                return array;
            }
        }
        /// <summary>
        /// Gets the an estemation of the physical size of the monitor in millimetres.
        /// </summary>
        public Vector2I PhysicalSize
        {
            get
            {
                GLFW.GetMonitorPhysicalSize(_monitor, out int w, out int h);

                return new Vector2I(w, h);
            }
        }
        /// <summary>
        /// Gets the ratio between the current DPI and the platform's default DPI.
        /// </summary>
        /// <remarks>
        /// This is used to correctly scale UI elements to fit with the users platform settings.
        /// </remarks>
        public Vector2 ContentScale
        {
            get
            {
                GLFW.GetMonitorContentScale(_monitor, out IntPtr x, out IntPtr y);

                float[] values = new float[2];

                // Convert the x and y values to floats
                Marshal.Copy(x, values, 0, 1);
                Marshal.Copy(y, values, 1, 1);

                return new Vector2(values[0], values[1]);
            }
        }
        /// <summary>
        /// Gets the position of the monitor on the virtual desktop, in screen coordinates.
        /// </summary>
        public Vector2I Location
        {
            get
            {
                GLFW.GetMonitorPos(_monitor, out int x, out int y);

                return new Vector2I(x, y);
            }
        }
        /// <summary>
        /// Gets the area of a monitor not occupied by global task bars or menu bars in screen coordinates.
        /// </summary>
        public RectangleI WorkArea
        {
            get
            {
                GLFW.GetMonitorWorkarea(_monitor, out int x, out int y, out int w, out int h);

                return new RectangleI(x, y, w, h);
            }
        }
        /// <summary>
        /// Gets the monitor represented as a string.
        /// </summary>
        public string Name
        {
            get
            {
                IntPtr ptr = GLFW.GetMonitorName(_monitor);

                if (ptr == IntPtr.Zero) { return string.Empty; }

                return Marshal.PtrToStringUTF8(ptr);
            }
        }
        /// <summary>
        /// Gets or sets a custom pointer for this monitor.
        /// </summary>
        public IntPtr UserPointer
        {
            get => GLFW.GetMonitorUserPointer(_monitor);
            set
            {
                GLFW.SetMonitorUserPointer(_monitor, value);
            }
        }
        /// <summary>
        /// Gets or sets the gamma ramp for a monitor.
        /// </summary>
        /// <remarks>
        /// This can be used for colour correction or brighness. Brightness is best set with <see cref="Gamma"/>.
        /// </remarks>
        public GammaRamp GammaRamp
        {
            get => GLFW.GetGammaRamp(_monitor);
            set
            {
                IntPtr ptr = IntPtr.Zero;
                // Get pointer to struct
                Marshal.StructureToPtr(value, ptr, true);

                GLFW.SetGammaRamp(_monitor, ptr);
            }
        }
        /// <summary>
        /// Sets the strength of a preset gamma ramp which changes the brightness of the monitor.
        /// </summary>
        public double Gamma
        {
            set
            {
                GLFW.SetGamma(_monitor, (float)value);
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}: {Name}";
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_monitor);
        }

        public override bool Equals(object obj)
        {
            return obj is Monitor m &&
                _monitor == m._monitor;
        }

        /// <summary>
        /// Determines whether two <see cref="Monitor"/> instances are pointing to the same monitor.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator ==(Monitor l, Monitor r)
        {
            return l.Equals(r);
        }
        /// <summary>
        /// Determines whether two <see cref="Monitor"/> instances are pointing to different monitors.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator !=(Monitor l, Monitor r)
        {
            return !l.Equals(r);
        }

        /// <summary>
        /// Gets the primary monitor for the current context.
        /// </summary>
        public static Monitor Primary
        {
            get
            {
                return new Monitor(GLFW.GetPrimaryMonitor());
            }
        }
        /// <summary>
        /// Retrieves an array of all connected monitors.
        /// </summary>
        public static Monitor[] Connected
        {
            get
            {
                IntPtr arrayPtr = GLFW.GetMonitors(out int count);

                if (arrayPtr == IntPtr.Zero)
                {
                    return null;
                }

                Monitor[] result = new Monitor[count];

                for (int i = 0; i < count; i++)
                {
                    result[i] = new Monitor(
                        Marshal.ReadIntPtr(arrayPtr, i * IntPtr.Size));
                }

                return result;
            }
        }

        /// <summary>
        /// A null value monitor.
        /// </summary>
        public static Monitor None { get; } = new Monitor(IntPtr.Zero);
        /// <summary>
        /// Creates a monitor from an already created monitor.
        /// </summary>
        /// <param name="handle">The GLFW pointer to a monitor object.</param>
        /// <returns></returns>
        public static Monitor FromHandle(IntPtr handle)
        {
            return new Monitor(handle);
        }
    }
}
