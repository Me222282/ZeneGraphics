using Zene.Structs;

namespace Zene.Sprites
{
    public interface ISprite
    {
        public IBox Bounds { get; }

        public IDisplay Display { get; }
    }
}
