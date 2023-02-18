using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public class DrawContext : IDrawingContext
    {
        public DrawContext()
        {
            Framebuffer = GL.context.baseFrameBuffer;
        }
        public DrawContext(IFramebuffer framebuffer)
        {
            Framebuffer = framebuffer;
        }
        public DrawContext(IFramebuffer framebuffer, IShaderProgram shader)
        {
            Framebuffer = framebuffer;
            Shader = shader;
        }

        public IFramebuffer Framebuffer { get; set; }
        public IShaderProgram Shader { get; set; }

        //public GLBox FrameBounds { get; set; }
        //IBox IDrawingContext.FrameBounds => FrameBounds;
        IBox IDrawingContext.FrameBounds => new GLBox(Vector2I.Zero, Framebuffer.Properties.Size);

        public void PrepareDraw()
        {
            
        }
    }
}
