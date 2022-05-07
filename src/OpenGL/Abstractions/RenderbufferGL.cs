using System;

namespace Zene.Graphics.Base
{
    /// <summary>
    /// The most basic implimentation of an OpenGL renderbuffer.
    /// </summary>
    [OpenGLSupport(3.0)]
    public unsafe sealed class RenderbufferGL : IRenderbuffer
    {
        /// <summary>
        /// Creates an OpenGL renderbuffer object.
        /// </summary>
        public RenderbufferGL()
        {
            InternalFormat = 0;

            Id = GL.GenRenderbuffer();

            Properties = new TexRenProperties(this);
        }
        internal RenderbufferGL(uint id, TextureFormat format = 0)
        {
            Id = id;
            InternalFormat = format;

            Properties = new TexRenProperties(this);
        }

        public uint Id { get; }

        public TextureFormat InternalFormat { get; private set; }

        public TexRenProperties Properties { get; }

        private bool _disposed = false;
        [OpenGLSupport(3.0)]
        public void Dispose()
        {
            if (_disposed) { return; }

            GL.DeleteRenderbuffer(Id);

            _disposed = true;
            GC.SuppressFinalize(this);
        }
        [OpenGLSupport(3.0)]
        public void Bind()
        {
            if (this.Bound()) { return; }

            GL.BindRenderbuffer(GLEnum.Renderbuffer, Id);
        }
        [OpenGLSupport(3.0)]
        public void Unbind()
        {
            if (!this.Bound()) { return; }

            GL.BindRenderbuffer(GLEnum.Renderbuffer, 0);
        }

        /// <summary>
        /// Establish data storage, format and dimensions of a renderbuffer object's image.
        /// </summary>
        /// <param name="intFormat">Specifies the internal format to use for the renderbuffer object's image.</param>
        /// <param name="width">Specifies the width of the renderbuffer, in pixels.</param>
        /// <param name="height">Specifies the height of the renderbuffer, in pixels.</param>
        [OpenGLSupport(3.0)]
        public void RenderbufferStorage(TextureFormat intFormat, int width, int height)
        {
            Bind();
            GL.RenderbufferStorage(this, (uint)intFormat, width, height);

            InternalFormat = intFormat;
        }

        /// <summary>
        /// Establish data storage, format, dimensions and sample count of a renderbuffer object's image.
        /// </summary>
        /// <param name="intFormat">Specifies the internal format to use for the renderbuffer object's image.</param>
        /// <param name="samples">Specifies the number of samples to be used for the renderbuffer object's storage.</param>
        /// <param name="width">Specifies the width of the renderbuffer, in pixels.</param>
        /// <param name="height">Specifies the height of the renderbuffer, in pixels.</param>
        [OpenGLSupport(3.0)]
        public void RenderbufferStorageMultisample(TextureFormat intFormat, int samples, int width, int height)
        {
            Bind();
            GL.RenderbufferStorageMultisample(this, samples, (uint)intFormat, width, height);

            InternalFormat = intFormat;
        }

        //
        // Get Parameters
        //

        /// <summary>
        /// Returns the width in pixels of the image of this specified renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int GetWidth()
        {
            Bind();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferWidth, &output);

            return output;
        }

        /// <summary>
        /// Returns the height in pixels of the image of this specified renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int GetHeight()
        {
            Bind();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferHeight, &output);

            return output;
        }

        /// <summary>
        /// Returns the number of samples of the image of this specified renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int GetSamples()
        {
            Bind();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferSamples, &output);

            return output;
        }

        /// <summary>
        /// Returns the actual resolution in bits for the red component of the image of the renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int GetRedSize()
        {
            Bind();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferRedSize, &output);

            return output;
        }

        /// <summary>
        /// Returns the actual resolution in bits for the green component of the image of the renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int GetGreenSize()
        {
            Bind();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferGreenSize, &output);

            return output;
        }

        /// <summary>
        /// Returns the actual resolution in bits for the blue component of the image of the renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int GetBlueSize()
        {
            Bind();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferBlueSize, &output);

            return output;
        }

        /// <summary>
        /// Returns the actual resolution in bits for the alpha component of the image of the renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int GetAlphaSize()
        {
            Bind();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferAlphaSize, &output);

            return output;
        }

        /// <summary>
        /// Returns the actual resolution in bits for the depth component of the image of the renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int GetDepthSize()
        {
            Bind();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferDepthSize, &output);

            return output;
        }

        /// <summary>
        /// Returns the actual resolution in bits for the stencil component of the image of the renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public int GetStencilSize()
        {
            Bind();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferStencilSize, &output);

            return output;
        }
    }
}
