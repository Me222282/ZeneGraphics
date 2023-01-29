using Zene.Structs;

namespace Zene.Graphics
{
    public class ViewportLock : Viewport
    {
        public ViewportLock(IBox bounds)
            : base(bounds)
        {
        }

        public ViewportLock(int x, int y, int width, int height)
            : base(x, y, width, height)
        {
        }

        public new bool Locked
        {
            get => base.Locked;
            set => base.Locked = value;
        }
    }
}
