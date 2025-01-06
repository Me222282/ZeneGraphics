using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// Manages the "default" framebuffer, i.e. has a Id of 0.
    /// </summary>
    public sealed class BaseFramebuffer : IFramebuffer
    {
        /// <summary>
        /// The name of this object.
        /// </summary>
        uint IIdentifiable.Id => 0;
        /// <summary>
        /// The framebuffer target this framebuffer was last bound to.
        /// </summary>
        FrameTarget IFramebuffer.Binding => Binding;

        FramebufferProperties IFramebuffer.Properties => GL.context.baseFrameBuffer.Properties;

        public void Size(int width, int height) => GL.context.baseFrameBuffer.Properties.SetSize(width, height);

        /// <summary>
        /// Bind the framebuffer.
        /// </summary>
        [OpenGLSupport(3.0)]
        void IFramebuffer.Bind(FrameTarget target) => GL.context.baseFrameBuffer.Bind(target);
        /// <summary>
        /// Bind the framebuffer for a specific task.
        /// </summary>
        /// <param name="target">The task to bind the framebuffer for.</param>
        [OpenGLSupport(3.0)]
        void IBindable.Bind() => GL.context.baseFrameBuffer.Bind();
        void IBindable.Unbind()
        {
            if (!this.Bound()) { return; }

            GL.BindFramebuffer((uint)Binding, null);
        }


        bool IFramebuffer.LockedState => GL.context.baseFrameBuffer.LockedState;

        [OpenGLSupport(1.0)]
        Viewport IFramebuffer.Viewport => GL.context.baseFrameBuffer.Viewport;

        [OpenGLSupport(1.0)]
        Scissor IFramebuffer.Scissor => GL.context.baseFrameBuffer.Scissor;

        FrameDrawTarget IFramebuffer.ReadBuffer
        {
            get => FrameDrawTarget.Colour0;
            set => throw new NotSupportedException();
        }
        private static readonly FrameDrawTarget[] _drawBuffers = new FrameDrawTarget[] { FrameDrawTarget.Colour0 };
        FrameDrawTarget[] IFramebuffer.DrawBuffers
        {
            get => _drawBuffers;
            set => throw new NotSupportedException();
        }

        /// <summary>
        /// Clears the specified attachments to a generic value.
        /// </summary>
        /// <param name="buffer">The buffers to clear</param>
        [OpenGLSupport(1.0)]
        void IFramebuffer.Clear(BufferBit buffer) => Clear(buffer);

        void IDisposable.Dispose()
        {
            // Cannot dispose anything

            GC.SuppressFinalize(this);
        }
        bool IFramebuffer.Validate() => true;

        // Static methods - main part of class

        /// <summary>
        /// The name of this object.
        /// </summary>
        public static uint Id => 0;
        /// <summary>
        /// The framebuffer target this framebuffer was last bound to.
        /// </summary>
        public static FrameTarget Binding => GL.context.baseFrameBuffer.Binding;

        public static FramebufferProperties Properties => GL.context.baseFrameBuffer.Properties;

        /// <summary>
        /// Bind the framebuffer.
        /// </summary>
        [OpenGLSupport(3.0)]
        public static void Bind(FrameTarget target) => GL.context.baseFrameBuffer.Bind(target);
        /// <summary>
        /// Bind the framebuffer for a specific task.
        /// </summary>
        /// <param name="target">The task to bind the framebuffer for.</param>
        [OpenGLSupport(3.0)]
        public static void Bind() => GL.context.baseFrameBuffer.Bind();

        /// <summary>
        /// Gets or sets the render size and location for the framebuffer.
        /// </summary>
        [OpenGLSupport(1.0)]
        public static RectangleI View
        {
            get => GL.context.baseFrameBuffer.View;
            set => GL.context.baseFrameBuffer.View = value;
        }
        /// <summary>
        /// Sets the render size for the framebuffer.
        /// </summary>
        [OpenGLSupport(1.0)]
        public static Vector2I ViewSize
        {
            get => GL.context.baseFrameBuffer.ViewSize;
            set => GL.context.baseFrameBuffer.ViewSize = value;
        }
        /// <summary>
        /// Sets the render location for the framebuffer.
        /// </summary>
        public static Vector2I ViewLocation
        {
            get => GL.context.baseFrameBuffer.ViewLocation;
            set => GL.context.baseFrameBuffer.ViewLocation = value;
        }

        /// <summary>
        /// The clear colour that is used when <see cref="Clear(BufferBit)"/> is called.
        /// </summary>
        public static ColourF ClearColour
        {
            get => GL.context.frameClearColour;
            set => GL.context.frameClearColour = value;
        }
        /// <summary>
        /// The depth value that is used when <see cref="Clear(BufferBit)"/> is called.
        /// </summary>
        public static floatv ClearDepth
        {
            get => GL.context.frameClearDepth;
            set => GL.context.frameClearDepth = value;
        }
        /// <summary>
        /// The stencil value that is used when <see cref="Clear(BufferBit)"/> is called.
        /// </summary>
        public static int ClearStencil
        {
            get => GL.context.frameClearStencil;
            set => GL.context.frameClearStencil = value;
        }

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
                GL.ClearStencil(ClearStencil);
            }

            GL.Clear((uint)buffer);
        }

        /// <summary>
        /// Copies the contents of a framebuffer to this framebuffer.
        /// </summary>
        /// <param name="source">The source framebuffer.</param>
        /// <param name="mask">The attachments to copy.</param>
        /// <param name="filter">The quality of the copy.</param>
        [OpenGLSupport(3.0)]
        public void Write(IFramebuffer source, BufferBit mask, TextureSampling filter)
        {
            Base.Extensions.FramebufferOpenGL.BlitBuffer(this, source,
                new GLBox(0, 0, source.Properties.Width, source.Properties.Height),
                new GLBox(0, 0, Properties.Width, Properties.Height), mask, filter);
        }
    }
}
