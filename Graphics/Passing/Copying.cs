namespace Zene.Graphics.Passing
{
    public static class Copying
    {
        /// <summary>
        /// Copies <paramref name="texture"/> to a new <see cref="TexturePasser"/>, spliting the GPU texture between two c# instances.
        /// </summary>
        /// <remarks>
        /// Note: Coping textures can lead to GPU memory errors.
        /// </remarks>
        /// <param name="texture"></param>
        /// <returns></returns>
        public static TexturePasser Copy(this ITexture texture)
        {
            return new TexturePasser(texture);
        }

        /// <summary>
        /// Copies <paramref name="renderbuffer"/> to a new <see cref="RenderbufferPasser"/>, spliting the GPU renderbuffer between two c# instances.
        /// </summary>
        /// <remarks>
        /// Note: Coping renderbuffers can lead to GPU memory errors.
        /// </remarks>
        /// <param name="renderbuffer"></param>
        /// <returns></returns>
        public static RenderbufferPasser Copy(this IRenderbuffer renderbuffer)
        {
            return new RenderbufferPasser(renderbuffer);
        }

        /// <summary>
        /// Copies <paramref name="framebuffer"/> to a new <see cref="FrameBufferPasser"/>, spliting the GPU framebuffer between two c# instances.
        /// </summary>
        /// <remarks>
        /// Note: Coping framebuffers can lead to GPU memory errors.
        /// </remarks>
        /// <param name="framebuffer"></param>
        /// <returns></returns>
        public static FrameBufferPasser Copy(this IFramebuffer framebuffer)
        {
            return new FrameBufferPasser(framebuffer);
        }

        /// <summary>
        /// Copies <paramref name="shader"/> to a new <see cref="ShaderPasser"/>, spliting the GPU shader between two c# instances.
        /// </summary>
        /// <remarks>
        /// Note: Coping shaders can lead to GPU memory errors.
        /// </remarks>
        /// <param name="shader"></param>
        /// <returns></returns>
        public static ShaderPasser Copy(this IShader shader)
        {
            return new ShaderPasser(shader);
        }
    }
}
