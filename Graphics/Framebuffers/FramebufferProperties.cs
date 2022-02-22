using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zene.Graphics.Framebuffers
{
    public unsafe sealed class FramebufferProperties
    {
        public FramebufferProperties(IFramebuffer source)
        {
            Handle = source;
        }
        public IFramebuffer Handle { get; }

        internal int _width;
        /// <summary>
        /// THe width of the attachments attached to <see cref="Handle"/>.
        /// </summary>
        public int Width => _width;
        internal int _height;
        /// <summary>
        /// THe width of the attachments attached to <see cref="Handle"/>.
        /// </summary>
        public int Height => _height;

        internal BaseFormat _colourReadFormat;
        /// <summary>
        /// The prefered pixel format for <see cref="Handle"/>.
        /// </summary>
        public BaseFormat ColourReadFormat => _colourReadFormat;
        internal TextureData _colourReadType;
        /// <summary>
        /// The prefered pixel data type for <see cref="Handle"/>.
        /// </summary>
        public TextureData ColourReadType => _colourReadType;
        internal bool _doubleBuffered;
        /// <summary>
        /// Determines whether double buffering is supported by <see cref="Handle"/>.
        /// </summary>
        public bool DoubleBuffered => _doubleBuffered;
        internal int _samples;
        /// <summary>
        /// The coverage mask size for this framebuffer.
        /// </summary>
        public int Samples => _samples;
        internal int _sampleBuffers;
        /// <summary>
        /// The number of sample buffers a part of <see cref="Handle"/>.
        /// </summary>
        public int SampleBuffers => _sampleBuffers;
        internal bool _stereo;
        /// <summary>
        /// Determines whether stereo buffers are support by <see cref="Handle"/>.
        /// </summary>
        public bool Stereo => _stereo;
    }
}
