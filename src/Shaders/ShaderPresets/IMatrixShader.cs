using Zene.Structs;

namespace Zene.Graphics
{
    public interface IMatrixShader : IShaderProgram
    {
        /// <summary>
        /// The model matrix.
        /// </summary>
        public Matrix4 Matrix1 { get; set; }
        /// <summary>
        /// The view matrix.
        /// </summary>
        public Matrix4 Matrix2 { get; set; }
        /// <summary>
        /// The projection matrix.
        /// </summary>
        public Matrix4 Matrix3 { get; set; }
    }
}
