using System;
using Zene.Structs;

namespace Zene.Graphics.Z3D
{
    public class ShadowMapper : IDrawingContext
    {
        public ShadowMapper(int width, int height)
        {
            _shader = DepthMapShader.GetInstance();

            // Framebuffer initialization
            _framebuffer = new TextureRenderer(width, height);
            _framebuffer.SetDepthAttachment(TextureFormat.DepthComponent32f, true);
            _framebuffer.DrawBuffer = FrameDrawTarget.None;
            _framebuffer.ReadBuffer = FrameDrawTarget.None;

            // Texture properties
            _texture = _framebuffer.GetTexture(FrameAttachment.Depth);
            _texture.MinFilter = TextureSampling.Blend;
            _texture.MagFilter = TextureSampling.Blend;
            _texture.WrapStyle = WrapStyle.BorderClamp;
            _texture.BorderColour = new ColourF(1f, 1f, 1f, 1f);
        }

        private readonly TextureRenderer _framebuffer;
        private readonly Texture2D _texture;

        private readonly DepthMapShader _shader;

        public Vector2I Size
        {
            get => _framebuffer.Size;
            set
            {
                if (value.X <= 0 || value.Y <= 0)
                {
                    throw new FrameBufferException(_framebuffer, "Framebuffers must have a width and heihgt greater that 0.");
                }

                _framebuffer.ViewSize = value;
                _framebuffer.Size = value;
            }
        }
        public ITexture DepthMap => _texture;

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            _framebuffer.Dispose();
            _shader.Dispose();

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clears the data inside the framebuffer.
        /// </summary>
        public void Clear() => _framebuffer.Clear(BufferBit.Depth);

        /// <summary>
        /// The projection matrix applied on render.
        /// </summary>
        public IMatrix Projection { get; set; }
        /// <summary>
        /// The view matrix applied on render.
        /// </summary>
        public IMatrix View { get; set; }
        /// <summary>
        /// The model matrix applied on render.
        /// </summary>
        public IMatrix Model { get; set; }

        public IFramebuffer Framebuffer => _framebuffer;
        public IDrawingShader Shader { get => _shader; set => throw new NotSupportedException(); }

        public IBox FrameBounds => new GLBox(Vector2.Zero, _framebuffer.Size);
    }
}
