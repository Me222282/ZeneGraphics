using Zene.Structs;

namespace Zene.Graphics
{
    public sealed unsafe class DepthMapShader : BaseShaderProgram, IMatrixShader
    {
        public DepthMapShader()
        {
            Create(ShaderPresets.DepthMapVertex, ShaderPresets.DepthMapFragment,
                  "matrix", "depthOffset");

            SetUniformF(Uniforms[0], Matrix4.Identity);
        }

        public Matrix4 Matrix1 { get; set; } = Matrix4.Identity;
        public Matrix4 Matrix2 { get; set; } = Matrix4.Identity;
        public Matrix4 Matrix3 { get; set; } = Matrix4.Identity;

        public override void PrepareDraw()
        {
            SetUniformF(Uniforms[0], Matrix1 * Matrix2 * Matrix3);
        }

        private double _depthOffset = 0d;
        public double DepthOffset
        {
            get => _depthOffset;
            set
            {
                _depthOffset = value;

                SetUniformF(Uniforms[1], value);
            }
        }
    }
}
