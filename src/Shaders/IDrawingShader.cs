using Zene.Structs;

namespace Zene.Graphics
{
    public interface IDrawingShader : IShaderProgram
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
    }
}
