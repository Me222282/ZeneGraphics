namespace Zene.Graphics.OpenGL.Abstract3
{
    [OpenGLSupport(3.0)]
    public static unsafe class RenderBufferOpenGL
    {
        /// <summary>
        /// Sets the OpenGL context to referance <paramref name="renderbuffer"/>.
        /// </summary>
        public static void SetGLContext(this IRenderbuffer renderbuffer)
        {
            if (!renderbuffer.Bound())
            {
                renderbuffer.Bind();
            }
        }

        /// <summary>
        /// Establish data storage, format and dimensions of a renderbuffer object's image.
        /// </summary>
        /// <param name="width">Specifies the width of the renderbuffer, in pixels.</param>
        /// <param name="height">Specifies the height of the renderbuffer, in pixels.</param>
        [OpenGLSupport(3.0)]
        public static void RenderbufferStorage(this IRenderbuffer renderbuffer, int width, int height)
        {
            renderbuffer.SetGLContext();
            GL.RenderbufferStorage(GLEnum.Renderbuffer, (uint)renderbuffer.InternalFormat, width, height);
        }
        /// <summary>
        /// Establish data storage, format, dimensions and sample count of a renderbuffer object's image.
        /// </summary>
        /// <param name="samples">Specifies the number of samples to be used for the renderbuffer object's storage.</param>
        /// <param name="width">Specifies the width of the renderbuffer, in pixels.</param>
        /// <param name="height">Specifies the height of the renderbuffer, in pixels.</param>
        [OpenGLSupport(3.0)]
        public static void RenderbufferStorageMultisample(this IRenderbuffer renderbuffer, int samples, int width, int height)
        {
            renderbuffer.SetGLContext();
            GL.RenderbufferStorageMultisample(GLEnum.Renderbuffer, samples, (uint)renderbuffer.InternalFormat, width, height);
        }

        //
        // Get Parameters
        //

        /// <summary>
        /// Returns the width in pixels of the image of this specified renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public static int GetWidth(this IRenderbuffer renderbuffer)
        {
            renderbuffer.SetGLContext();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferWidth, &output);

            return output;
        }
        /// <summary>
        /// Returns the height in pixels of the image of this specified renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public static int GetHeight(this IRenderbuffer renderbuffer)
        {
            renderbuffer.SetGLContext();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferHeight, &output);

            return output;
        }
        /// <summary>
        /// Returns the number of samples of the image of this specified renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public static int GetSamples(this IRenderbuffer renderbuffer)
        {
            renderbuffer.SetGLContext();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferSamples, &output);

            return output;
        }
        /// <summary>
        /// Returns the actual resolution in bits for the red component of the image of the renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public static int GetRedSize(this IRenderbuffer renderbuffer)
        {
            renderbuffer.SetGLContext();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferRedSize, &output);

            return output;
        }
        /// <summary>
        /// Returns the actual resolution in bits for the green component of the image of the renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public static int GetGreenSize(this IRenderbuffer renderbuffer)
        {
            renderbuffer.SetGLContext();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferGreenSize, &output);

            return output;
        }
        /// <summary>
        /// Returns the actual resolution in bits for the blue component of the image of the renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public static int GetBlueSize(this IRenderbuffer renderbuffer)
        {
            renderbuffer.SetGLContext();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferBlueSize, &output);

            return output;
        }
        /// <summary>
        /// Returns the actual resolution in bits for the alpha component of the image of the renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public static int GetAlphaSize(this IRenderbuffer renderbuffer)
        {
            renderbuffer.SetGLContext();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferAlphaSize, &output);

            return output;
        }
        /// <summary>
        /// Returns the actual resolution in bits for the depth component of the image of the renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public static int GetDepthSize(this IRenderbuffer renderbuffer)
        {
            renderbuffer.SetGLContext();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferDepthSize, &output);

            return output;
        }
        /// <summary>
        /// Returns the actual resolution in bits for the stencil component of the image of the renderbuffer object.
        /// </summary>
        [OpenGLSupport(3.0)]
        public static int GetStencilSize(this IRenderbuffer renderbuffer)
        {
            renderbuffer.SetGLContext();
            int output;

            GL.GetRenderbufferParameteriv(GLEnum.Renderbuffer, GLEnum.RenderbufferStencilSize, &output);

            return output;
        }
    }
}
