using System;
using Zene.Graphics.Base;
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
    /// The completion status of an <see cref="IFramebuffer"/>.
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
    [Flags]
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
    public unsafe interface IFramebuffer : IGLObject
    {
        /// <summary>
        /// The properties for this framebuffer.
        /// </summary>
        public new FramebufferProperties Properties { get; }
        IProperties IGLObject.Properties => Properties;

        /// <summary>
        /// The framebuffer target this framebuffer was last bound to.
        /// </summary>
        public FrameTarget Binding { get; }

        /// <summary>
        /// Determines whether the framebuffer can be used with different <see cref="Viewport"/>,
        /// <see cref="DepthState"/> and <see cref="Scissor"/> than the ones currently set.
        /// </summary>
        public bool LockedState { get; }

        /// <summary>
        /// The render size and location of the framebuffer.
        /// </summary>
        [OpenGLSupport(1.0)]
        public Viewport Viewport { get; }

        /// <summary>
        /// How the depth buffer is managed and drawn to.
        /// </summary>
        [OpenGLSupport(1.0)]
        public DepthState DepthState { get; }

        /// <summary>
        /// The clipping bounds for rendering to the framebuffer.
        /// </summary>
        [OpenGLSupport(1.0)]
        public Scissor Scissor { get; }

        /// <summary>
        /// Gets or sets a colour buffer as a source for pixel reads.
        /// </summary>
        [OpenGLSupport(1.0)]
        public FrameDrawTarget ReadBuffer { get; set; }
        /// <summary>
        /// Gets or sets the colour buffers as a destination for draw calls.
        /// </summary>
        [OpenGLSupport(2.0)]
        public FrameDrawTarget[] DrawBuffers { get; set; }

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
        /// Clears the specified attachments to a generic value.
        /// </summary>
        /// <param name="buffer">The buffers to clear</param>
        [OpenGLSupport(1.0)]
        public void Clear(BufferBit buffer);
    }
}
