using System;
using Zene.Graphics.Base;

namespace Zene.Graphics
{
    /// <summary>
    /// An struct that contains data about framebuffer attachments.
    /// </summary>
    public struct FramebufferAttachment
    {
        internal FramebufferAttachment(IRenderTexture attachment, CubeMapFace face, int level)
        {
            Attachment = attachment;
            Face = face;
            Layer = face.ToLayer();
            Level = level;
        }
        internal FramebufferAttachment(IRenderTexture attachment, CubeMapFace face, int layer, int level)
        {
            Attachment = attachment;
            Face = face;
            Layer = layer;
            Level = level;
        }
        internal FramebufferAttachment(IRenderTexture attachment, int layer, int level)
        {
            Attachment = attachment;
            Face = 0;
            Layer = layer;
            Level = level;
        }

        /// <summary>
        /// The object at the attachment
        /// </summary>
        public IRenderTexture Attachment { get; }
        /// <summary>
        /// Detremines wether <see cref="Attachment"/> is a layered object.
        /// </summary>
        public bool Layered
        {
            get
            {
                if (Attachment.IsRenderbuffer)
                {
                    return ((ITexture)Attachment).Properties.Layered;
                }

                return false;
            }
        }

        /// <summary>
        /// The <see cref="CubeMapFace"/> from <see cref="Attachment"/> that is attached to the framebuffer.
        /// </summary>
        public CubeMapFace Face { get; }
        /// <summary>
        /// The layer from <see cref="Attachment"/> that is attached to the framebuffer.
        /// </summary>
        public int Layer { get; }
        /// <summary>
        /// The mipmap level from <see cref="Attachment"/> that is attached to the framebuffer.
        /// </summary>
        public int Level { get; }

        /// <summary>
        /// Determines wether this <see cref="FramebufferAttachment"/> represents a null attachment.
        /// </summary>
        public bool IsNone()
        {
            return Attachment == null;
        }

        /// <summary>
        /// A <see cref="FramebufferAttachment"/> that represents no attachment.
        /// </summary>
        public static FramebufferAttachment None { get; } = new FramebufferAttachment(null, 0, 0, 0);
    }

    public unsafe sealed class FramebufferProperties
    {
        public FramebufferProperties(IFramebuffer source)
        {
            Handle = source;
            _attachments = new AttachList();
        }
        public IFramebuffer Handle { get; }

        internal FramebufferProperties(IFramebuffer source, int w, int h, int s)
        {
            Handle = source;
            _fromAttach = false;
            _width = w;
            _height = h;
            _samples = s;
        }

        private readonly bool _fromAttach = true;
        private int _width = 0;
        private int _height = 0;
        private readonly int _samples = 0;

        internal void Size(int width, int height)
        {
            _width = width;
            _height = height;
        }

        /// <summary>
        /// The width of the attachments attached to <see cref="Handle"/>.
        /// </summary>
        public int Width
        {
            get
            {
                if (_fromAttach)
                {
                    return _attachments.AttahcObject.Properties._width;
                }

                return _width;
            }
        }
        /// <summary>
        /// The width of the attachments attached to <see cref="Handle"/>.
        /// </summary>
        public int Height
        {
            get
            {
                if (_fromAttach)
                {
                    return _attachments.AttahcObject.Properties._height;
                }

                return _height;
            }
        }
        /// <summary>
        /// The coverage mask size for this framebuffer.
        /// </summary>
        public int Samples
        {
            get
            {
                if (_fromAttach)
                {
                    return _attachments.AttahcObject.Properties._samples;
                }

                return _samples;
            }
        }

        /// <summary>
        /// Gets the <see cref="FramebufferAttachment"/> associated with <paramref name="attachment"/>.
        /// </summary>
        /// <param name="attachment">The framebufffer attachment to retrieve.</param>
        public FramebufferAttachment this[FrameAttachment attachment]
        {
            get
            {
                if (attachment == 0)
                {
                    throw new ArgumentException($"{attachment} is an invalid framebuffer attachment.", nameof(attachment));
                }

                return _attachments.Get((uint)attachment);
            }
        }

        /*
        internal int _samples;
        /// <summary>
        /// The coverage mask size for this framebuffer.
        /// </summary>
        public int Samples
        {
            get => _samples;
            init => _samples = value;
        }*/

        internal AttachList _attachments;

        /// <summary>
        /// Determines whether double buffering is supported by <see cref="Handle"/>.
        /// </summary>
        public bool DoubleBuffered { get; internal init; } = false;
        internal int _sampleBuffers;
        /// <summary>
        /// The number of sample buffers a part of <see cref="Handle"/>.
        /// </summary>
        public int SampleBuffers
        {
            get => _sampleBuffers;
            internal init => _sampleBuffers = value;
        }
        /// <summary>
        /// Determines whether stereo buffers are support by <see cref="Handle"/>.
        /// </summary>
        public bool Stereo { get; internal init; } = false;

        internal class AttachList
        {
            public AttachList()
            {
                attachments = new FramebufferAttachment[State.MaxColourAttach + 2];
            }

            private const uint _offset = GLEnum.ColourAttachment0 - 2;

            private readonly FramebufferAttachment[] attachments;
            private int getter = 0;

            public void Add(FramebufferAttachment value, uint attachment)
            {
                switch (attachment)
                {
                    case GLEnum.DepthAttachment:
                    case GLEnum.DepthStencilAttachment:
                        attachments[0] = value;
                        getter = 0;
                        return;

                    case GLEnum.StencilAttachment:
                        attachments[1] = value;
                        getter = 1;
                        return;

                    default:
                        getter = (int)(attachment - _offset);
                        attachments[getter] = value;
                        return;
                }
            }
            public void Remove(uint attachment)
            {
                switch (attachment)
                {
                    case GLEnum.DepthAttachment:
                    case GLEnum.DepthStencilAttachment:
                        attachments[0] = FramebufferAttachment.None;
                        break;

                    case GLEnum.StencilAttachment:
                        attachments[1] = FramebufferAttachment.None;
                        break;

                    default:
                        attachments[attachment - _offset] = FramebufferAttachment.None;
                        break;
                }

                // Getter is still referencing a valid attachment
                if (!attachments[getter].IsNone()) { return; }

                // Find first attachment
                for (int i = 0; i < attachments.Length; i++)
                {
                    if (!attachments[i].IsNone())
                    {
                        getter = i;
                        return;
                    }
                }

                getter = 0;
            }
            public FramebufferAttachment Get(uint attachment)
            {
                return attachment switch
                {
                    GLEnum.DepthAttachment or GLEnum.DepthStencilAttachment => attachments[0],
                    GLEnum.StencilAttachment => attachments[1],
                    _ => attachments[attachment - _offset],
                };
            }
            public IRenderTexture AttahcObject => attachments[getter].Attachment;
        }
    }
}
