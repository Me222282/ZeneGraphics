using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public class Viewport
    {
        protected internal bool Locked { get; set; } = false;

        public bool Current => GL.context.viewport == this;

        public Viewport(int x, int y, int width, int height)
        {
            view = new GLBox(x, y, width, height);
        }
        public Viewport(IBox bounds)
        {
            view = new GLBox(bounds);
        }

        internal GLBox view;
        public virtual GLBox View
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

                GL.Viewport(value);
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

                GL.Viewport(new GLBox(view.Location, value));
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

                GL.Viewport(new GLBox(view.Location, (value, view.Height)));
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

                GL.Viewport(new GLBox(view.Location, (view.Width, value)));
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

                GL.Viewport(new GLBox(value, view.Size));
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

                GL.Viewport(new GLBox((value, view.Y), view.Size));
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

                GL.Viewport(new GLBox((view.X, value), view.Size));
            }
        }
    }
}
