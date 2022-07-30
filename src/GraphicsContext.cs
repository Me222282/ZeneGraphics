using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public class GraphicsContext
    {
        public unsafe GraphicsContext(bool stereo, bool doubleBuffered, int width, int height, double version)
        {
            baseFrameBuffer = new FramebufferGL(0, stereo, doubleBuffered, width, height);

            this.version = version;
            viewport = new RectangleI(0, 0, width, height);

            // Setup texture binding referance
            int size = 0;
            GL.GetIntegerv(GLEnum.MaxTextureImageUnits, &size);
            boundTextures = new GL.TextureBinding[size];
        }

        internal uint boundShaderProgram = 0;
        internal GL.BufferBinding boundBuffers = new GL.BufferBinding();

        internal GL.FrameBufferBinding boundFrameBuffers = new GL.FrameBufferBinding();

        internal uint boundRenderbuffer = 0;

        internal uint activeTextureUnit = 0;
        internal GL.TextureBinding[] boundTextures;

        internal RectangleI viewport;

        internal double version;

        internal FramebufferGL baseFrameBuffer;
        internal ColourF frameClearColour = ColourF.Zero;
        internal double frameClearDepth = 1d;
        internal int frameClearStencil = 0;

        public static GraphicsContext None { get; } = new GraphicsContext(false, false, 0, 0, 0d);
    }
}
