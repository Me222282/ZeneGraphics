using Zene.Structs;

namespace Zene.Graphics
{
    public interface IMatrixShader : IShaderProgram
    {
        /// <summary>
        /// The model matrix.
        /// </summary>
        public IMatrix Matrix1 { get; set; }
        /// <summary>
        /// The view matrix.
        /// </summary>
        public IMatrix Matrix2 { get; set; }
        /// <summary>
        /// The projection matrix.
        /// </summary>
        public IMatrix Matrix3 { get; set; }
        /*
        /// <summary>
        /// Set all matrices at once.
        /// </summary>
        /// <param name="a">Matrix 1.</param>
        /// <param name="b">Matrix 2.</param>
        /// <param name="c">Matrix 3.</param>
        public void SetMatrices(Matrix4 a, Matrix4 b, Matrix4 c);*/
    }
}
