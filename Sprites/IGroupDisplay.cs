namespace Zene.Sprites
{
    public interface IGroupDisplay : IDisplay
    {
        public void Draw(ISprite sprite, int count);

        void IDisplay.Draw(ISprite sprite) => Draw(sprite, 1);
    }
}
