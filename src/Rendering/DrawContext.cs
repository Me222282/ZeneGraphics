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
        public DrawContext(IFramebuffer framebuffer, IDrawingShader shader)
        {
            Framebuffer = framebuffer;
            Shader = shader;
        }

        public IFramebuffer Framebuffer { get; set; }
        public IDrawingShader Shader { get; set; }

        //public GLBox FrameBounds { get; set; }
        //IBox IDrawingContext.FrameBounds => FrameBounds;
        GLBox IDrawingContext.FrameBounds => new GLBox(Vector2I.Zero, Framebuffer.Properties.Size);

        public IMatrix Projection { get; set; } = Matrix.Identity;
        public IMatrix View { get; set; } = Matrix.Identity;
        public IMatrix Model { get; set; } = Matrix.Identity;

        public DepthState DepthState { get; set; }
        public RenderState RenderState { get; set; }
    }
}
