using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public class Scissor
    {
        protected internal bool Locked { get; set; } = false;

        public bool Current => GL.context.scissor == this;

        public Scissor(bool enabled = false)
        {
            view = new RectangleI();
            this.enabled = enabled;
        }
        public Scissor(int x, int y, int width, int height)
        {
            view = new RectangleI(x, y, width, height);
            enabled = true;
        }
        public Scissor(IBox bounds)
        {
            view = new RectangleI(bounds);
            enabled = true;
        }

        internal bool enabled = false;
        public bool Enabled
        {
            get => enabled;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    enabled = value;
                    return;
                }

                if (value)
                {
                    GL.Enable(GLEnum.ScissorTest);
                    return;
                }

                GL.Disable(GLEnum.ScissorTest);
            }
        }

        internal RectangleI view;
        public RectangleI View
        {
            get => view;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    view = value;
                    return;
                }

                GL.Scissor(value);
            }
        }

        public Vector2I Size
        {
            get => view.Size;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    view.Size = value;
                    return;
                }

                GL.Scissor(new RectangleI(view.Location, value));
            }
        }
        public int Width
        {
            get => view.Width;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    view.Width = value;
                    return;
                }

                GL.Scissor(new RectangleI(view.Location, (value, view.Height)));
            }
        }
        public int Height
        {
            get => view.Height;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    view.Height = value;
                    return;
                }

                GL.Scissor(new RectangleI(view.Location, (view.Width, value)));
            }
        }

        public Vector2I Location
        {
            get => view.Location;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    view.Location = value;
                    return;
                }

                GL.Scissor(new RectangleI(value, view.Size));
            }
        }
        public int X
        {
            get => view.X;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    view.X = value;
                    return;
                }

                GL.Scissor(new RectangleI((value, view.Y), view.Size));
            }
        }
        public int Y
        {
            get => view.Y;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    view.Y = value;
                    return;
                }

                GL.Scissor(new RectangleI((view.X, value), view.Size));
            }
        }
    }
}
