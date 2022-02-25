using Zene.Graphics;
using Zene.Structs;

namespace Zene.GUI
{
    public interface IFormShader : IShader
    {
        public Colour Colour { set; }
        public int TextureSlot { set; }

        public Matrix4 Matrix1 { set; }
        public Matrix4 Matrix2 { set; }
        public Matrix4 Matrix3 { set; }
    }
}
