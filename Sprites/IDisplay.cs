using System;

namespace Zene.Sprites
{
    public interface IDisplay : IDisposable
    {
        public void Draw(ISprite sprite);
    }
}
