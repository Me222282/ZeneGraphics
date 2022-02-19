using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// Manages the "default" framebuffer, i.e. has a Id of 0.
    /// </summary>
    public static class BaseFramebuffer
    {
        static BaseFramebuffer()
        {
            _frameBuffer = new FrameBufferGL(0);
        }

        /// <summary>
        /// The name of this object.
        /// </summary>
        public static uint Id => 0;
        /// <summary>
        /// The framebuffer target this framebuffer was last bound to.
        /// </summary>
        public static FrameTarget Binding => _frameBuffer.Binding;

        private static readonly FrameBufferGL _frameBuffer;

        /// <summary>
        /// Bind the framebuffer.
        /// </summary>
        [OpenGLSupport(3.0)]
        public static void Bind(FrameTarget target) => _frameBuffer.Bind(target);
        /// <summary>
        /// Bind the framebuffer for a specific task.
        /// </summary>
        /// <param name="target">The task to bind the framebuffer for.</param>
        [OpenGLSupport(3.0)]
        public static void Bind() => _frameBuffer.Bind();

        /// <summary>
        /// Gets or sets the render size and location for the framebuffer.
        /// </summary>
        [OpenGLSupport(1.0)]
        public static RectangleI View
        {
            get => _frameBuffer.View;
            set => _frameBuffer.View = value;
        }
        /// <summary>
        /// Sets the render size for the framebuffer.
        /// </summary>
        [OpenGLSupport(1.0)]
        public static Vector2I ViewSize
        {
            get => _frameBuffer.ViewSize;
            set => _frameBuffer.ViewSize = value;
        }
        /// <summary>
        /// Sets the render location for the framebuffer.
        /// </summary>
        public static Vector2I ViewLocation
        {
            get => _frameBuffer.ViewLocation;
            set => _frameBuffer.ViewLocation = value;
        }

        /// <summary>
        /// The clear colour that is used when <see cref="Clear(BufferBit)"/> is called.
        /// </summary>
        public static ColourF ClearColour { get; set; } = ColourF.Zero;
        /// <summary>
        /// The depth value that is used when <see cref="Clear(BufferBit)"/> is called.
        /// </summary>
        public static double ClearDepth { get; set; } = 0.0;
        /// <summary>
        /// The stencil value that is used when <see cref="Clear(BufferBit)"/> is called.
        /// </summary>
        public static int CLearStencil { get; set; } = 0;

        /// <summary>
        /// Clears the specified attachments to a generic value.
        /// </summary>
        /// <param name="buffer">The buffers to clear</param>
        [OpenGLSupport(1.0)]
        public static void Clear(BufferBit buffer)
        {
            Bind();

            if ((buffer & BufferBit.Colour) == BufferBit.Colour)
            {
                GL.ClearColour(ClearColour.R, ClearColour.G, ClearColour.B, ClearColour.A);
            }
            if ((buffer & BufferBit.Depth) == BufferBit.Depth)
            {
                GL.ClearDepth(ClearDepth);
            }
            if ((buffer & BufferBit.Stencil) == BufferBit.Stencil)
            {
                GL.ClearStencil(CLearStencil);
            }

            GL.Clear((uint)buffer);
        }
    }
}
