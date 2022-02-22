using Zene.Graphics.Base;

namespace Zene.Graphics
{
    public unsafe sealed class FramebufferProperties
    {
        public FramebufferProperties(IFramebuffer source)
        {
            Handle = source;
            _attachments = new AttachList();
        }
        public IFramebuffer Handle { get; }

        internal FramebufferProperties(IFramebuffer source, int w, int h)
        {
            Handle = source;
            _fromAttach = false;
            _width = w;
            _height = h;
        }

        private readonly bool _fromAttach = true;
        private int _width = 0;
        private int _height = 0;

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
                    return _attachments.Attachment.Properties._width;
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
                    return _attachments.Attachment.Properties._height;
                }

                return _height;
            }
        }

        internal AttachList _attachments;

        /// <summary>
        /// Determines whether double buffering is supported by <see cref="Handle"/>.
        /// </summary>
        public bool DoubleBuffered { get; internal init; } = false;
        internal int _samples;
        /// <summary>
        /// The coverage mask size for this framebuffer.
        /// </summary>
        public int Samples
        {
            get => _samples;
            init => _samples = value;
        }
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
                attachments = new IRenderTexture[State.MaxColourAttach + 2];
            }

            private const uint _offset = GLEnum.ColourAttachment0 - 2;

            private readonly IRenderTexture[] attachments;
            private int getter = 0;

            public void Add(IRenderTexture value, uint attachment)
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
                        attachments[0] = null;
                        break;

                    case GLEnum.StencilAttachment:
                        attachments[1] = null;
                        break;

                    default:
                        attachments[attachment - _offset] = null;
                        break;
                }

                // Find first attachment
                for (int i = 0; i < attachments.Length; i++)
                {
                    if (attachments[i] != null)
                    {
                        getter = i;
                        return;
                    }
                }

                getter = 0;
            }
            public IRenderTexture Attachment => attachments[getter];
        }
    }
}
