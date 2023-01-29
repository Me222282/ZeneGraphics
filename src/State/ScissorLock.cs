using Zene.Structs;

namespace Zene.Graphics
{
    public class ScissorLock : Scissor
    {
        public ScissorLock(bool enabled = false)
            : base(enabled)
        {
        }

        public ScissorLock(IBox bounds)
            : base(bounds)
        {
        }

        public ScissorLock(Vector2I location, Vector2I size)
            : base(location, size)
        {
        }

        public ScissorLock(int x, int y, int width, int height)
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
