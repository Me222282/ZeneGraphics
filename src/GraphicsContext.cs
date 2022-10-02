﻿using Zene.Graphics.Base;
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

            // Setup indexed buffer
            boundBuffers = new GL.BufferBinding();

            if (version >= 3.0)
            {
                int tfSize = 0;
                GL.GetIntegerv(GLEnum.MaxTransformFeedbackBuffers, &tfSize);
                boundBuffers.TransformFeedback = new IBuffer[tfSize];
            }
            else
            {
                boundBuffers.TransformFeedback = new IBuffer[1];
            }
            if (version >= 3.1)
            {
                int uSize = 0;
                GL.GetIntegerv(GLEnum.MaxUniformBufferBindings, &uSize);
                boundBuffers.Uniform = new IBuffer[uSize];
            }
            else
            {
                boundBuffers.Uniform = new IBuffer[1];
            }
            if (version >= 4.2)
            {
                int acSize = 0;
                GL.GetIntegerv(GLEnum.MaxAtomicCounterBufferBindings, &acSize);
                boundBuffers.AtomicCounter = new IBuffer[acSize];
            }
            else
            {
                boundBuffers.AtomicCounter = new IBuffer[1];
            }
            if (version >= 4.3)
            {
                int ssSize = 0;
                GL.GetIntegerv(GLEnum.MaxShaderStorageBufferBindings, &ssSize);
                boundBuffers.ShaderStorage = new IBuffer[ssSize];
            }
            else
            {
                boundBuffers.ShaderStorage = new IBuffer[1];
            }
        }

        public ActionManager Actions { get; } = new ActionManager();

        internal IShaderProgram boundShaderProgram;
        internal GL.BufferBinding boundBuffers;

        internal IRenderbuffer boundRenderbuffer;

        internal IVertexArray boundVertexArray;
        internal VertexArrayGL baseVertexArray = new VertexArrayGL(0);

        internal uint activeTextureUnit = 0;
        internal GL.TextureBinding[] boundTextures;

        internal RectangleI viewport;

        internal double version;

        internal GL.FrameBufferBinding boundFrameBuffers = new GL.FrameBufferBinding();
        internal FramebufferGL baseFrameBuffer;
        internal ColourF frameClearColour = ColourF.Zero;
        internal double frameClearDepth = 1d;
        internal int frameClearStencil = 0;

        public static GraphicsContext None { get; } = new GraphicsContext(false, false, 0, 0, 0d);
    }
}
