using Zene.Structs;

namespace Zene.Sprites
{
    public interface ISprite3D : ISprite
    {
        public new IBox3 Bounds { get; }
    }
}
