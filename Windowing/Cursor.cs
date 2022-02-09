using System;
using System.Runtime.InteropServices;
using Zene.Graphics;
using Zene.Structs;
using Zene.Windowing.Base;

namespace Zene.Windowing
{
    /// <summary>
    /// An ojbect that manages a GLFW cursor.
    /// </summary>
    public unsafe class Cursor : IDisposable
    {
        /// <summary>
        /// Creates a cursor from a bitmap.
        /// </summary>
        /// <param name="image">The image data to represent the cursor.</param>
        /// <param name="hotX">The pixel location from the top-left of the image where the pointer X location will be sourced.</param>
        /// <param name="hotY">The pixel location from the top-left of the image where the pointer Y location will be sourced.</param>
        public Cursor(Bitmap image, int hotX, int hotY)
        {
            _disposed = false;
            // Creates a new bitmap and flips so it starts at top-left not bottom-left
            Bitmap imageCopy = new Bitmap(image.Data);
            imageCopy.FlipVertically();

            GLFW.Image imageData = new GLFW.Image
            {
                Width = image.Width,
                Height = image.Height
            };

            fixed (Colour* pixelPtr = &imageCopy.Data[0, 0])
            {
                imageData.Pixels = (IntPtr)pixelPtr;
            }

            // Converts imageData to a pointer
            IntPtr imagePtr = IntPtr.Zero;
            Marshal.StructureToPtr(imageData, imagePtr, true);

            _cursor = GLFW.CreateCursor(imagePtr, hotX, hotY);
        }
        internal Cursor(IntPtr cursor)
        {
            _disposed = false;
            _cursor = cursor;
        }

        private readonly IntPtr _cursor;
        /// <summary>
        /// Gets the pointer to the GLFW object.
        /// </summary>
        public IntPtr Handle => _cursor;

        private bool _disposed;
        public void Dispose()
        {
            if (_disposed) { return; }

            GLFW.DestroyCursor(_cursor);

            _disposed = true;

            GC.SuppressFinalize(this);
        }

        public override string ToString()
        {
            return $"{base.ToString()}: {_cursor}";
        }

        static Cursor()
        {
            Arrow = new Cursor(GLFW.CreateStandardCursor(GLFW.ArrowCursor));
            IBeam = new Cursor(GLFW.CreateStandardCursor(GLFW.IbeamCursor));
            CrossHair = new Cursor(GLFW.CreateStandardCursor(GLFW.CrosshairCursor));
            Hand = new Cursor(GLFW.CreateStandardCursor(GLFW.HandCursor));
            ResizeHorizontal = new Cursor(GLFW.CreateStandardCursor(GLFW.HresizeCursor));
            ResizeVertical = new Cursor(GLFW.CreateStandardCursor(GLFW.VresizeCursor));
            ResizeTopLeft = new Cursor(GLFW.CreateStandardCursor(GLFW.NWSEresizeCursor));
            ResizeTopRight = new Cursor(GLFW.CreateStandardCursor(GLFW.NESWresizeCursor));
            ResizeAll = new Cursor(GLFW.CreateStandardCursor(GLFW.ResizeAllCursor));
            NotAllowed = new Cursor(GLFW.CreateStandardCursor(GLFW.NotAllowedCursor));
        }

        /// <summary>
        /// Gets the system arrow cursor.
        /// </summary>
        public static Cursor Arrow { get; }
        /// <summary>
        /// Gets the system ibeam cursor.
        /// </summary>
        public static Cursor IBeam { get; }
        /// <summary>
        /// Gets the system cross hair cursor.
        /// </summary>
        public static Cursor CrossHair { get; }
        /// <summary>
        /// Gets the system hand cursor.
        /// </summary>
        public static Cursor Hand { get; }
        /// <summary>
        /// Gets the system horizontal resize cursor.
        /// </summary>
        public static Cursor ResizeHorizontal { get; }
        /// <summary>
        /// Gets the system vertical resize cursor.
        /// </summary>
        public static Cursor ResizeVertical { get; }
        /// <summary>
        /// Gets the system top-left or bottom-right resize cursor.
        /// </summary>
        public static Cursor ResizeTopLeft { get; }
        /// <summary>
        /// Gets the system top-right or bottom-left resize cursor.
        /// </summary>
        public static Cursor ResizeTopRight { get; }
        /// <summary>
        /// Gets the system top-right or bottom-left resize cursor.
        /// </summary>
        public static Cursor ResizeBottomLeft => ResizeTopRight;
        /// <summary>
        /// Gets the system top-left or bottom-right resize cursor.
        /// </summary>
        public static Cursor ResizeBottomRight => ResizeTopLeft;
        /// <summary>
        /// Gets the system resize all cursor.
        /// </summary>
        public static Cursor ResizeAll { get; }
        /// <summary>
        /// Gets the system not allowed cursor.
        /// </summary>
        public static Cursor NotAllowed { get; }
        /// <summary>
        /// Gets the normal system cursor.
        /// </summary>
        public static Cursor Default { get; } = new Cursor(IntPtr.Zero);
    }
}
