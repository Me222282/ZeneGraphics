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

        private Matrix4 _m1 = Matrix4.Identity;
        public Matrix4 Matrix1
        {
            get => _m1;
            set
            {
                _m1 = value;
                SetMatrices();
            }
        }
        private Matrix4 _m2 = Matrix4.Identity;
        public Matrix4 Matrix2
        {
            get => _m2;
            set
            {
                _m2 = value;
                SetMatrices();
            }
        }
        private Matrix4 _m3 = Matrix4.Identity;
        public Matrix4 Matrix3
        {
            get => _m3;
            set
            {
                _m3 = value;
                SetMatrices();
            }
        }

        public void SetMatrices(Matrix4 a, Matrix4 b, Matrix4 c)
        {
            _m1 = a;
            _m2 = b;
            _m3 = c;
            SetMatrices();
        }

        private void SetMatrices()
        {
            SetUniformF(Uniforms[0], _m1 * _m2 * _m3);
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
