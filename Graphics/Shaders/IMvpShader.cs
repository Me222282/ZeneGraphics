using Zene.Structs;

namespace Zene.Graphics.Shaders
{
    public interface IMvpShader : IShaderProgram
    {
        public void SetModelMatrix(Matrix4 matrix);

        public void SetViewMatrix(Matrix4 matrix);

        public void SetProjectionMatrix(Matrix4 matrix);
    }
}
