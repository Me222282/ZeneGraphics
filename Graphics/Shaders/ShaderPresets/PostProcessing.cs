using System;
using Zene.Graphics.OpenGL;
using Zene.Structs;

namespace Zene.Graphics.Shaders
{
    public class PostProcessing : PostShader, IDrawable
    {
        public PostProcessing(int width, int height)
        {
            _multiSFramebuffer = new TextureRendererMS(width, height, 4);
            _multiSFramebuffer.SetColourAttachment(0, TextureFormat.Rgb8);
            _multiSFramebuffer.SetDepthAttachment(TextureFormat.DepthComponent24, false);

            _framebuffer = new TextureRenderer(width, height);
            _framebuffer.SetColourAttachment(0, TextureFormat.Rgb8);

            _drawingObject = new DrawObject<Vector2I, byte>(new Vector2I[]
            {
                new Vector2I(-1, -1), new Vector2I(0, 0),
                new Vector2I(1, -1), new Vector2I(1, 0),
                new Vector2I(1, 1), new Vector2I(1, 1),
                new Vector2I(-1, 1), new Vector2I(0, 1)
            }, new byte[] { 0, 1, 2, 2, 3, 0 }, 2, 0, AttributeSize.D2, BufferUsage.DrawFrequent);
            // Texture coordinate attribute
            _drawingObject.AddAttribute((uint)Location.TextureCoords, 1, AttributeSize.D2);
        }

        private readonly TextureRendererMS _multiSFramebuffer;
        private readonly TextureRenderer _framebuffer;

        private readonly DrawObject<Vector2I, byte> _drawingObject;

        public new Vector2I Size
        {
            get => base.Size;
            set
            {
                if (value.X <= 0 || value.Y <= 0)
                {
                    throw new FrameBufferException(_framebuffer, "Framebuffers must have a width and heihgt greater that 0.");
                }

                base.Size = value;

                _multiSFramebuffer.Size = value;
                _framebuffer.Size = value;

                _multiSFramebuffer.ViewSize = value;
            }
        }

        public new void Bind()
        {
            _multiSFramebuffer.Bind();

            _bound = true;
        }

        public new void UnBind()
        {
            _multiSFramebuffer.Bind();

            _bound = false;
        }

        public new void Dispose()
        {
            base.Dispose();

            _multiSFramebuffer.Dispose();
            _framebuffer.Dispose();

            _drawingObject.Dispose();
        }

        public void Draw()
        {
            _multiSFramebuffer.CopyFrameBuffer(_framebuffer, BufferBit.Colour, TextureSampling.Nearest);

            //_multiSFramebuffer.UnBind();
            State.NullBind(Target.Framebuffer);

            GL.Disable(GLEnum.DepthTest);

            GL.UseProgram(Program);
            _framebuffer.GetTexture(FrameAttachment.Colour0).Bind(0);
            SetTextureSlot(0);

            _drawingObject.Draw();

            GL.BindTexture(GLEnum.Texture2d, 0);
            GL.UseProgram(0);
        }
    }
}
