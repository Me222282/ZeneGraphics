using System;

namespace Zene.Graphics.OpenGL
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
            _disposed = false;

            InternalFormat = 0;

            Id = GL.GenRenderbuffer();
        }
        internal RenderbufferGL(uint id, TextureFormat format = 0)
        {
            Id = id;
            InternalFormat = format;

            _disposed = false;
        }

        public uint Id { get; }

        public TextureFormat InternalFormat { get; private set; }

        private bool _disposed;
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
            GL.BindRenderbuffer(GLEnum.Renderbuffer, Id);
        }
        [OpenGLSupport(3.0)]
        public void UnBind()
        {
            if (!this.Bound()) { return; }

            GL.BindRenderbuffer(GLEnum.Renderbuffer, 0);
        }
        /// <summary>
        /// Sets the OpenGL context to referance this object.
        /// </summary>
        public void SetGLContext()
        {
            if (!this.Bound())
            {
                Bind();
            }
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
            SetGLContext();
            GL.RenderbufferStorage(GLEnum.Renderbuffer, (uint)intFormat, width, height);

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
            SetGLContext();
            GL.RenderbufferStorageMultisample(GLEnum.Renderbuffer, samples, (uint)intFormat, width, height);

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
            SetGLContext();
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
            SetGLContext();
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
            SetGLContext();
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
            SetGLContext();
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
            SetGLContext();
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
            SetGLContext();
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
            SetGLContext();
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
            SetGLContext();
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
            SetGLContext();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferStencilSize, &output);

            return output;
        }
    }
}
