using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public class DrawManager : IDrawingContext
    {
        public DrawManager()
        {
            Framebuffer = GL.context.baseFrameBuffer;
        }
        public DrawManager(IFramebuffer framebuffer)
        {
            Framebuffer = framebuffer;
        }
        public DrawManager(IFramebuffer framebuffer, IShaderProgram shader)
        {
            Framebuffer = framebuffer;
            Shader = shader;
        }

        public IFramebuffer Framebuffer { get; set; }
        public IShaderProgram Shader { get; set; }

        //public GLBox FrameBounds { get; set; }
        //IBox IDrawingContext.FrameBounds => FrameBounds;
        IBox IDrawingContext.FrameBounds => new GLBox(Vector2I.Zero, Framebuffer.Properties.Size);
    }
}
