using System;
using Zene.Graphics.OpenGL;
using Zene.Structs;

namespace Zene.Graphics
{
    public enum FrameDrawTarget : uint
    {
        None = GLEnum.None,
        FrontLeft = GLEnum.FrontLeft,
        FrontRight = GLEnum.FrontRight,
        BackLeft = GLEnum.BackLeft,
        BackRight = GLEnum.BackRight,
        Front = GLEnum.Front,
        Back = GLEnum.Back,
        Left = GLEnum.Left,
        Right = GLEnum.Right,
        Colour0 = GLEnum.ColourAttachment0,
        Colour1 = GLEnum.ColourAttachment1,
        Colour2 = GLEnum.ColourAttachment2,
        Colour3 = GLEnum.ColourAttachment3,
        Colour4 = GLEnum.ColourAttachment4,
        Colour5 = GLEnum.ColourAttachment5,
        Colour6 = GLEnum.ColourAttachment6,
        Colour7 = GLEnum.ColourAttachment7,
        Colour8 = GLEnum.ColourAttachment8,
        Colour9 = GLEnum.ColourAttachment9,
        Colour10 = GLEnum.ColourAttachment10,
        Colour11 = GLEnum.ColourAttachment11,
        Colour12 = GLEnum.ColourAttachment12,
        Colour13 = GLEnum.ColourAttachment13,
        Colour14 = GLEnum.ColourAttachment14,
        Colour15 = GLEnum.ColourAttachment15,
        Colour16 = GLEnum.ColourAttachment16,
        Colour17 = GLEnum.ColourAttachment17,
        Colour18 = GLEnum.ColourAttachment18,
        Colour19 = GLEnum.ColourAttachment19,
        Colour20 = GLEnum.ColourAttachment20,
        Colour21 = GLEnum.ColourAttachment21,
        Colour22 = GLEnum.ColourAttachment22,
        Colour23 = GLEnum.ColourAttachment23,
        Colour24 = GLEnum.ColourAttachment24,
        Colour25 = GLEnum.ColourAttachment25,
        Colour26 = GLEnum.ColourAttachment26,
        Colour27 = GLEnum.ColourAttachment27,
        Colour28 = GLEnum.ColourAttachment28,
        Colour29 = GLEnum.ColourAttachment29,
        Colour30 = GLEnum.ColourAttachment30,
        Colour31 = GLEnum.ColourAttachment31,
    }
    public enum FrameAttachment : uint
    {
        Depth = GLEnum.DepthAttachment,
        DepthStencil = GLEnum.DepthStencilAttachment,
        Stencil = GLEnum.StencilAttachment,
        Colour0 = GLEnum.ColourAttachment0,
        Colour1 = GLEnum.ColourAttachment1,
        Colour2 = GLEnum.ColourAttachment2,
        Colour3 = GLEnum.ColourAttachment3,
        Colour4 = GLEnum.ColourAttachment4,
        Colour5 = GLEnum.ColourAttachment5,
        Colour6 = GLEnum.ColourAttachment6,
        Colour7 = GLEnum.ColourAttachment7,
        Colour8 = GLEnum.ColourAttachment8,
        Colour9 = GLEnum.ColourAttachment9,
        Colour10 = GLEnum.ColourAttachment10,
        Colour11 = GLEnum.ColourAttachment11,
        Colour12 = GLEnum.ColourAttachment12,
        Colour13 = GLEnum.ColourAttachment13,
        Colour14 = GLEnum.ColourAttachment14,
        Colour15 = GLEnum.ColourAttachment15,
        Colour16 = GLEnum.ColourAttachment16,
        Colour17 = GLEnum.ColourAttachment17,
        Colour18 = GLEnum.ColourAttachment18,
        Colour19 = GLEnum.ColourAttachment19,
        Colour20 = GLEnum.ColourAttachment20,
        Colour21 = GLEnum.ColourAttachment21,
        Colour22 = GLEnum.ColourAttachment22,
        Colour23 = GLEnum.ColourAttachment23,
        Colour24 = GLEnum.ColourAttachment24,
        Colour25 = GLEnum.ColourAttachment25,
        Colour26 = GLEnum.ColourAttachment26,
        Colour27 = GLEnum.ColourAttachment27,
        Colour28 = GLEnum.ColourAttachment28,
        Colour29 = GLEnum.ColourAttachment29,
        Colour30 = GLEnum.ColourAttachment30,
        Colour31 = GLEnum.ColourAttachment31,
    }

    public enum FrameTarget : uint
    {
        FrameBuffer = GLEnum.Framebuffer,
        Read = GLEnum.ReadFramebuffer,
        Draw = GLEnum.DrawFramebuffer
    }

    public enum ColourEncode : uint
    {
        Linear = GLEnum.Linear,
        SRGB = GLEnum.Srgb
    }
    public enum AttachType : uint
    {
        None = 0,
        Texture = GLEnum.Texture,
        Renderbuffer = GLEnum.Renderbuffer
    }

    /// <summary>
    /// The completion status of an <see cref="IFrameBuffer"/>.
    /// </summary>
    public enum FrameBufferStatus : uint
    {
        /// <summary>
        /// The specified framebuffer is complete.
        /// </summary>
        Complete = GLEnum.FramebufferComplete,
        /// <summary>
        /// The specified framebuffer is the default read or draw framebuffer, but the default framebuffer does not exist.
        /// </summary>
        Undefined = GLEnum.FramebufferUndefined,
        /// <summary>
        /// The framebuffer attachment points are framebuffer incomplete.
        /// </summary>
        IncompleteAttachment = GLEnum.FramebufferIncompleteAttachment,
        /// <summary>
        /// The framebuffer does not have at least one image attached to it.
        /// </summary>
        MissingAttachment = GLEnum.FramebufferIncompleteMissingAttachment,
        /// <summary>
        /// The framebuffer isn't draw complete.
        /// </summary>
        IncompleteDraw = GLEnum.FramebufferIncompleteDrawBuffer,
        /// <summary>
        /// The framebuffer isn't read complete.
        /// </summary>
        IncompleteRead = GLEnum.FramebufferIncompleteReadBuffer,
        /// <summary>
        /// The combination of internal formats of the attached images violates an implementation-dependent set of restrictions.
        /// </summary>
        Unsupported = GLEnum.FramebufferUnsupported,
        /// <summary>
        /// The samples of all framebuffer attachments is not the same or they do not all have fixed sample locations.
        /// </summary>
        IncompleteSamples = GLEnum.FramebufferIncompleteMultisample,
        /// <summary>
        /// Any framebuffer attachment is layered, and any populated attachment is not layered, or if all populated colour attachments are not from textures of the same target.
        /// </summary>
        IncompleteLayers = GLEnum.FramebufferIncompleteLayerTargets
    }

    /// <summary>
    /// The buffer attachment types for <see cref="TextureRendererOld"/>.
    /// </summary>
    public enum BufferBit : uint
    {
        /// <summary>
        /// Colour attachment
        /// </summary>
        Colour = GLEnum.ColourBufferBit,
        /// <summary>
        /// Depth attachment
        /// </summary>
        Depth = GLEnum.DepthBufferBit,
        /// <summary>
        /// Stencil attachment
        /// </summary>
        Stencil = GLEnum.StencilBufferBit
    }

    /// <summary>
    /// Objects that encapsulate an OpnelGL framebuffer.
    /// </summary>
    [OpenGLSupport(3.0)]
    public unsafe interface IFrameBuffer : IBindable, IIdentifiable, IDisposable
    {
        /// <summary>
        /// The framebuffer target this framebuffer was last bound to.
        /// </summary>
        public FrameTarget Binding { get; }

        /// <summary>
        /// Checks whether the framebuffer is complete.
        /// </summary>
        /// <returns>True if this framebuffer is complete, otherwise False.</returns>
        [OpenGLSupport(3.0)]
        public bool Validate();

        /// <summary>
        /// Bind the framebuffer for a specific task.
        /// </summary>
        /// <param name="target">The task to bind the framebuffer for.</param>
        [OpenGLSupport(3.0)]
        public void Bind(FrameTarget target);

        /// <summary>
        /// Sets the render size and location for the current framebuffer.
        /// </summary>
        /// <param name="x">The x location in pixels</param>
        /// <param name="y">The y location in pixels</param>
        /// <param name="width">The width in pixels</param>
        /// <param name="height">The height in pixels</param>
        [OpenGLSupport(1.0)]
        public static void View(int x, int y, int width, int height)
        {
            GL.Viewport(x, y, width, height);
        }

        /// <summary>
        /// Sets the render size and location for the current framebuffer.
        /// </summary>
        /// <param name="width">The width in pixels</param>
        /// <param name="height">The height in pixels</param>
        [OpenGLSupport(1.0)]
        public static void View(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
        }

        /// <summary>
        /// Clears the specified attachments to a generic value.
        /// </summary>
        /// <param name="buffer">The buffers to clear</param>
        [OpenGLSupport(1.0)]
        public static void Clear(BufferBit buffer)
        {
            GL.Clear((uint)buffer);
        }

        /// <summary>
        /// Clears the colour attachments to a specifid value.
        /// </summary>
        /// <param name="value">The value to clear the colour</param>
        [OpenGLSupport(1.0)]
        public static void ClearColour(Colour value)
        {
            ColourF c = (ColourF)value;

            GL.ClearColor(c.R, c.G, c.B, c.A);
            GL.Clear(GLEnum.ColourBufferBit);
        }
        /// <summary>
        /// Clears the colour attachments to a specifid value.
        /// </summary>
        /// <param name="value">The value to clear the colour</param>
        [OpenGLSupport(1.0)]
        public static void ClearColour(ColourF value)
        {
            GL.ClearColor(value.R, value.G, value.B, value.A);
            GL.Clear(GLEnum.ColourBufferBit);
        }

        /// <summary>
        /// Clears the depth attachment to a specifid value.
        /// </summary>
        /// <param name="value">The value to clear the depth</param>
        [OpenGLSupport(1.0)]
        public static void ClearDepth(double value)
        {
            GL.ClearDepth(value);
            GL.Clear(GLEnum.DepthBufferBit);
        }

        /// <summary>
        /// Clears the stencil attachment to a specifid value.
        /// </summary>
        /// <param name="value">The value to clear the stencil</param>
        [OpenGLSupport(1.0)]
        public static void ClearStencil(int value)
        {
            GL.ClearStencil(value);
            GL.Clear(GLEnum.StencilBufferBit);
        }

        /// <summary>
        /// Gets the maximum colour attachments for the hardware being used.
        /// </summary>
        /// <returns>The maximum number of colour attachments.</returns>
        [OpenGLSupport(3.0)]
        public static int MaxColourAttach()
        {
            int value = 0; // Output value
            // Get the maximum colour attachments
            GL.GetIntegerv(GLEnum.MaxColourAttachments, ref value);

            return value;
        }

        /// <summary>
        /// Read a block of pixels from the bound frame buffer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">Specify the window x coordinate of the first pixel that is read from the frame buffer. This location is the lower left corner of a rectangular block of pixels.</param>
        /// <param name="y">Specify the window y coordinate of the first pixel that is read from the frame buffer. This location is the lower left corner of a rectangular block of pixels.</param>
        /// <param name="width">Specify the width of the pixel rectangle.</param>
        /// <param name="height">Specify the height of the pixel rectangle.</param>
        /// <param name="format">Specifies the format of the returned data.</param>
        /// <param name="type">Specifies the data type of the returned data.</param>
        /// <returns></returns>
        [OpenGLSupport(1.0)]
        public static GLArray<T> ReadPixels<T>(int x, int y, int width, int height, BaseFormat format, TextureData type) where T : unmanaged
        {
            GLArray<T> data = new GLArray<T>(
                (width * format.GetSize() * type.GetSize()) / sizeof(T),
                height);

            GL.ReadPixels(x, y, width, height, (uint)format, (uint)type, data);

            return data;
        }

        /// <summary>
        /// Select a colour buffer source for pixels.
        /// </summary>
        /// <param name="mode">Specifies a colour buffer.</param>
        [OpenGLSupport(1.0)]
        public static void ReadBuffer(FrameDrawTarget mode)
        {
            GL.ReadBuffer((uint)mode);
        }

        /// <summary>
        /// Specify which colour buffer is to be drawn into.
        /// </summary>
        /// <param name="buffer"></param>
        [OpenGLSupport(1.0)]
        public static void DrawBuffer(FrameDrawTarget buffer)
        {
            GL.DrawBuffer((uint)buffer);
        }
    }
}
