namespace Zene.Graphics
{
    public class DepthStateLock : DepthState
    {
        public new bool Locked
        {
            get => base.Locked;
            set => base.Locked = value;
        }
    }
}
