using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public class DepthState
    {
        protected internal bool Locked { get; set; } = false;

        public bool Current => GL.context.depth == this;

        internal bool testing = false;
        public virtual bool Testing
        {
            get => testing;
            set
            {
                if (Locked) { return; }

                testing = value;
                if (!Current) { return; }

                if (value)
                {
                    GL.Enable(GLEnum.DepthTest);
                    return;
                }

                GL.Disable(GLEnum.DepthTest);
            }
        }

        internal bool mask = true;
        public virtual bool Mask
        {
            get => mask;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    mask = value;
                    return;
                }

                GL.DepthMask(value);
            }
        }

        internal bool clamp = true;
        public virtual bool Clamp
        {
            get => clamp;
            set
            {
                if (Locked) { return; }

                testing = value;
                if (!Current) { return; }

                if (value)
                {
                    GL.Disable(GLEnum.DepthClamp);
                    return;
                }

                GL.Enable(GLEnum.DepthClamp);
            }
        }

        internal DepthFunction func = DepthFunction.Less;
        public virtual DepthFunction Function
        {
            get => func;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    func = value;
                    return;
                }

                GL.DepthFunc(value);
            }
        }

        internal Vector2 range;
        public virtual Vector2 Range
        {
            get => range;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    range = value;
                    return;
                }

                GL.DepthRange(value.X, value.Y);
            }
        }
        public virtual floatv Near
        {
            get => range.X;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    range.X = value;
                    return;
                }

                GL.DepthRange(value, range.Y);
            }
        }
        public virtual floatv Far
        {
            get => range.Y;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    range.Y = value;
                    return;
                }

                GL.DepthRange(range.X, value);
            }
        }

        public static DepthState Default { get; } = new DepthState();
    }
}
