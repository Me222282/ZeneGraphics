using Zene.Graphics.Base;

namespace Zene.Graphics
{
    public class DepthState
    {
        protected internal bool Locked { get; set; } = false;

        public bool Current => GL.context.depth == this;

        internal bool testing = false;
        public bool Testing
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
        public bool Mask
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
        public bool Clamp
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
        public DepthFunction Function
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

        internal double near = 0d;
        public double Near
        {
            get => near;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    near = value;
                    return;
                }

                GL.DepthRange(value, far);
            }
        }

        internal double far = 1d;
        public double Far
        {
            get => far;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    far = value;
                    return;
                }

                GL.DepthRange(near, value);
            }
        }
    }
}
