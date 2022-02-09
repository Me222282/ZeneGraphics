using System;
using Zene.Graphics.OpenGL;
using Zene.Graphics.Shaders;
using Zene.Structs;

namespace Zene.Graphics.Z3D
{
    public class ShadowMapper : IFrameBuffer, IShaderProgram
    {
        public ShadowMapper(int width, int height)
        {
            _shader = new DepthMapShader();

            // Framebuffer initialization
            _framebuffer = new TextureRenderer(width, height);
            _framebuffer.SetDepthAttachment(TextureFormat.DepthComponent32f, true);
            _framebuffer.DrawBuffer(FrameDrawTarget.None);
            _framebuffer.ReadBuffer(FrameDrawTarget.None);
            _framebuffer.UnBind();

            // Texture properties
            _texture = _framebuffer.GetTexture(FrameAttachment.Depth);
            _texture.MinFilter = TextureSampling.Blend;
            _texture.MagFilter = TextureSampling.Blend;
            _texture.WrapStyle = WrapStyle.BorderClamp;
            _texture.BorderColour = new ColourF(1f, 1f, 1f, 1f);
            _texture.UnBind();
        }

        public uint Id => _framebuffer.Id;
        public FrameTarget Binding => _framebuffer.Binding;
        public uint Program => _shader.Program;

        private readonly TextureRenderer _framebuffer;
        private readonly Texture2D _texture;

        private readonly DepthMapShader _shader;

        public RectangleI View
        {
            get
            {
                return _framebuffer.View;
            }
            set
            {
                _framebuffer.View = value;
            }
        }
        public Vector2I ViewLocation
        {
            get
            {
                return _framebuffer.ViewLocation;
            }
            set
            {
                _framebuffer.ViewLocation = value;
            }
        }
        public Vector2I ViewSize
        {
            get
            {
                return _framebuffer.ViewSize;
            }
            set
            {
                _framebuffer.ViewSize = value;
            }
        }

        public Vector2I Size
        {
            get => _framebuffer.Size;
            set
            {
                if (value.X <= 0 || value.Y <= 0)
                {
                    throw new FrameBufferException(this, "Framebuffers must have a width and heihgt greater that 0.");
                }

                _framebuffer.Size = value;
            }
        }

        /// <summary>
        /// Bind the framebuffer for a specific task.
        /// </summary>
        /// <param name="target">The task to bind the framebuffer for.</param>
        public void Bind(FrameTarget target)
        {
            _shader.Bind();

            _framebuffer.Bind(target);
        }
        void IFrameBuffer.Bind(FrameTarget target) => GL.BindFramebuffer((uint)target, Id);
        /// <summary>
        /// Bind the framebuffer.
        /// </summary>
        public void Bind()
        {
            _shader.Bind();

            _framebuffer.Bind();
        }
        void IBindable.Bind() => GL.BindFramebuffer(GLEnum.Framebuffer, Id);
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
        /// Unbind the framebuffer.
        /// </summary>
        public void UnBind()
        {
            _shader.UnBind();

            _framebuffer.UnBind();
        }
        void IBindable.UnBind() => GL.BindFramebuffer(GLEnum.Framebuffer, 0);
        public bool Validate()
        {
            return _framebuffer.Validate();
        }

        /// <summary>
        /// Clears the data inside the framebuffer.
        /// </summary>
        public void Clear()
        {
            // Bind framebuffer
            GL.BindFramebuffer(GLEnum.Framebuffer, Id);

            IFrameBuffer.Clear(BufferBit.Depth);
        }

        /// <summary>
        /// Binds the texture apart of this texture.
        /// </summary>
        /// <param name="slot"></param>
        public void BindTexture(uint slot)
        {
            _framebuffer.GetTexture(FrameAttachment.Depth).Bind(slot);
        }

        /// <summary>
        /// The projection matrix applied on render.
        /// </summary>
        public Matrix4 ProjectionMatrix
        {
            set
            {
                _shader.SetProjectionMatrix(value);
            }
        }
        /// <summary>
        /// The view matrix applied on render.
        /// </summary>
        public Matrix4 ViewMatrix
        {
            set
            {
                _shader.SetViewMatrix(value);
            }
        }
        /// <summary>
        /// The model matrix applied on render.
        /// </summary>
        public Matrix4 ModelMatrix
        {
            set
            {
                _shader.SetModelMatrix(value);
            }
        }
    }
}
