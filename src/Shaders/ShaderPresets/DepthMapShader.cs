using Zene.Structs;

namespace Zene.Graphics
{
    public sealed unsafe class DepthMapShader : BaseShaderProgram
    {
        public DepthMapShader()
        {
            Create(ShaderPresets.DepthMapVertex, ShaderPresets.DepthMapFragment, 0,
                "matrix", "depthOffset");
            
            SetUniform(Uniforms[0], Matrix4.Identity);
        }
        
        private floatv _depthOffset = 0;
        public floatv DepthOffset
        {
            get => _depthOffset;
            set
            {
                _depthOffset = value;

                SetUniform(Uniforms[1], value);
            }
        }

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            State.CurrentContext.RemoveTrack(this);
        }
        /// <summary>
        /// Gets the instance of the <see cref="DepthMapShader"/> for this <see cref="GraphicsContext"/>.
        /// </summary>
        /// <returns></returns>
        public static DepthMapShader GetInstance() => GetInstance<DepthMapShader>();
    }
}
