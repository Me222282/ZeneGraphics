using System;
using Zene.Graphics.Base;
using Zene.Graphics.Shaders;
using Zene.Structs;

namespace Zene.Graphics.Z3D
{
    public class ShadowMapper : IFramebuffer, IShaderProgram
    {
        public ShadowMapper(int width, int height)
        {
            _shader = new DepthMapShader();

            // Framebuffer initialization
            _framebuffer = new TextureRenderer(width, height);
            _framebuffer.SetDepthAttachment(TextureFormat.DepthComponent32f, true);
            _framebuffer.DrawBuffer = FrameDrawTarget.None;
            _framebuffer.ReadBuffer = FrameDrawTarget.None;
            _framebuffer.Unbind();

            // Texture properties
            _texture = _framebuffer.GetTexture(FrameAttachment.Depth);
            _texture.MinFilter = TextureSampling.Blend;
            _texture.MagFilter = TextureSampling.Blend;
            _texture.WrapStyle = WrapStyle.BorderClamp;
            _texture.BorderColour = new ColourF(1f, 1f, 1f, 1f);
            _texture.Unbind();
        }

        public uint Id => _framebuffer.Id;
        public FrameTarget Binding => _framebuffer.Binding;
        public uint Program => _shader.Program;

        FramebufferProperties IFramebuffer.Properties => ((IFramebuffer)_framebuffer).Properties;

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

        public FrameDrawTarget ReadBuffer { get => FrameDrawTarget.None; set => throw new NotSupportedException(); }
        private static readonly FrameDrawTarget[] _drawbuffers = new FrameDrawTarget[] { FrameDrawTarget.None };
        public FrameDrawTarget[] DrawBuffers { get => _drawbuffers; set => throw new NotSupportedException(); }

        /// <summary>
        /// Bind the framebuffer for a specific task.
        /// </summary>
        /// <param name="target">The task to bind the framebuffer for.</param>
        public void Bind(FrameTarget target)
        {
            _shader.Bind();

            _framebuffer.Bind(target);
        }
        void IFramebuffer.Bind(FrameTarget target) => GL.BindFramebuffer((uint)target, Id);
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
            _shader.Unbind();

            _framebuffer.Unbind();
        }
        void IBindable.Unbind() => GL.BindFramebuffer(GLEnum.Framebuffer, 0);
        public bool Validate()
        {
            return _framebuffer.Validate();
        }

        /// <summary>
        /// Clears the data inside the framebuffer.
        /// </summary>
        public void Clear() => _framebuffer.Clear(BufferBit.Depth);
        void IFramebuffer.Clear(BufferBit buffer) => _framebuffer.Clear(buffer);

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
