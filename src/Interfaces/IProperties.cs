namespace Zene.Graphics
{
    public interface IProperties
    {
        public IGLObject Source { get; }

        public bool Sync();
    }
}
