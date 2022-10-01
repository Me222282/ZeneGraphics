using System;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that manages a GLFW window.
    /// </summary>
    public interface IWindow : IDisposable
    {
        /// <summary>
        /// A pointer to the GLFW window object.
        /// </summary>
        public IntPtr Handle { get; }

        /// <summary>
        /// The OpenGL context data surrounding this GLFW window.
        /// </summary>
        public GraphicsContext GraphicsContext { get; }

        /// <summary>
        /// The width of the window's framebuffer.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// The height of the window's framebuffer.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The title of the window.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Marks the window as ready to close.
        /// </summary>
        public void Close();
    }
}
