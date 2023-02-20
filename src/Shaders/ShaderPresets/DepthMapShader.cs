using Zene.Structs;

namespace Zene.Graphics
{
    public sealed unsafe class DepthMapShader : BaseShaderProgram, IMatrixShader
    {
        public DepthMapShader()
        {
            Create(ShaderPresets.DepthMapVertex, ShaderPresets.DepthMapFragment,
                  "matrix", "depthOffset");

            _m2m3 = Matrix.Identity * Matrix.Identity;
            _m1Mm2m3 = Matrix.Identity * _m2m3;

            SetUniform(Uniforms[0], Matrix.Identity);
        }

        public IMatrix Matrix1
        {
            get => _m1Mm2m3.Left;
            set => _m1Mm2m3.Left = value;
        }
        public IMatrix Matrix2
        {
            get => _m2m3.Left;
            set => _m2m3.Left = value;
        }
        public IMatrix Matrix3
        {
            get => _m2m3.Right;
            set => _m2m3.Right = value;
        }

        private readonly MultiplyMatrix _m1Mm2m3;
        private readonly MultiplyMatrix _m2m3;
        public override void PrepareDraw() => SetUniform(Uniforms[0], _m1Mm2m3);

        private double _depthOffset = 0d;
        public double DepthOffset
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
