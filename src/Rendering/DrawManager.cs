/*using System;
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
        public DrawManager(IFramebuffer framebuffer, IBasicShader shader)
        {
            Framebuffer = framebuffer;
            Shader = shader;
        }

        public IFramebuffer Framebuffer { get; set; }
        private bool _basicShader;
        private IShaderProgram _shader;
        IShaderProgram IDrawingContext.Shader
        {
            get => _shader;
            set
            {
                if (value is IBasicShader ibs)
                {
                    _basicShader = true;
                    _bShader = ibs;
                }
                else
                {
                    _basicShader = false;
                }

                _shader = value;
            }
        }
        private IBasicShader _bShader;
        public IBasicShader Shader
        {
            get => _bShader;
            set
            {
                _basicShader = true;
                _shader = value;
                _bShader = value;
            }
        }

        public IMatrix Projection { get; set; }
        public IMatrix View { get; set; }
        public IMatrix Model { get; set; }

        //public GLBox FrameBounds { get; set; }
        //IBox IDrawingContext.FrameBounds => FrameBounds;
        IBox IDrawingContext.FrameBounds => new GLBox(Vector2I.Zero, Framebuffer.Properties.Size);

        public void PrepareDraw()
        {
            if (!_basicShader || _bShader == null) { return; }

            _bShader.Matrix1 = Model;
            _bShader.Matrix2 = View;
            _bShader.Matrix3 = Projection;
        }
    }
}
*/