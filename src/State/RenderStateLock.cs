namespace Zene.Graphics
{    
    public class RenderStateLock : RenderState
    {
        public new bool Locked
        {
            get => base.Locked;
            set => base.Locked = value;
        }
    }
}
