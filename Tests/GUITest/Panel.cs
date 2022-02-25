using System;
using System.Linq;
using Zene.Graphics;
using Zene.Graphics.Shaders;
using Zene.Structs;
using Zene.Windowing;

namespace GUITest
{
    internal class Panel : IDisposable
    {
        public Panel(Program handle, Box bounds, DrawObject<double, byte> draw, BasicShader shader)
        {
            _frame = new TextureRenderer((int)bounds.Width, (int)bounds.Height);
            _frame.SetColourAttachment(0, TextureFormat.Rgba8);
            _frame.SetDepthAttachment(TextureFormat.DepthComponent16, false);
            _frame.ClearColour = _colour;

            _bounds = bounds;
            _tabBox = bounds;
            _tabBox.Bottom = _tabBox.Top - TabSize;

            _drawable = draw;
            _shader = shader;

            _handle = handle;
        }

        private Box _bounds;
        private Box _tabBox;
        public Box Bounds
        {
            get => _bounds;
            set
            {
                _bounds = value;
                _tabBox = value;
                _tabBox.Bottom = _tabBox.Top - TabSize;

                _frame.Size = (Vector2I)value.Size;
                _frame.ViewSize = (Vector2I)value.Size;
            }
        }

        private double _tabSize = 25;
        public double TabSize
        {
            get
            {
                return _tabSize;
            }
            set
            {
                _tabSize = value;

                _redraw = true;

                // Set tab box with new tabsize
                _tabBox = _bounds;
                _tabBox.Bottom = _tabBox.Top - TabSize;
            }
        }

        public double ResizeRange { get; set; } = 10;

        private readonly Program _handle;
        private readonly DrawObject<double, byte> _drawable;
        private readonly BasicShader _shader;

        private readonly TextureRenderer _frame;

        private Vector2 _mouseOffset;
        private bool _followMouse;

        private bool _canResizeRight;
        private bool _canResizeLeft;
        private bool _canResizeTop;
        private bool _canResizeBottom;

        private bool _isResizeRight;
        private bool _isResizeLeft;
        private bool _isResizeTop;
        private bool _isResizeBottom;

        public Cursor MouseMove(MouseEventArgs e, bool cursorCanSet)
        {
            if (_followMouse)
            {
                Vector2 oldLocation = _bounds.Location;

                _bounds.Location = e.Location + _mouseOffset;
                _tabBox.Location += _bounds.Location - oldLocation;
            }

            if (!cursorCanSet) { return null; }

            double halfRange = ResizeRange * 0.5;

            bool resizeL = false;
            bool resizeR = false;
            bool resizeT = false;
            bool resizeB = false;

            Box newBounds = _bounds;

            // Resize right
            if (_isResizeRight)
            {
                newBounds.Right = e.Location.X;

                if (newBounds.Width < TabSize)
                {
                    newBounds.Right = _bounds.Left + TabSize;
                }

                Bounds = newBounds;
                _redraw = true;

                resizeR = true;
            }
            // Resize left
            if (_isResizeLeft)
            {
                newBounds.Left = e.Location.X;

                if (newBounds.Width < TabSize)
                {
                    newBounds.Left = _bounds.Right - TabSize;
                }

                Bounds = newBounds;
                _redraw = true;

                resizeL = true;
            }
            // Resize top
            if (_isResizeTop)
            {
                newBounds.Top = e.Location.Y;

                if (newBounds.Height < TabSize)
                {
                    newBounds.Top = _bounds.Bottom + TabSize;
                }

                Bounds = newBounds;
                _redraw = true;

                resizeT = true;
            }
            // Resize bottom
            if (_isResizeBottom)
            {
                newBounds.Bottom = e.Location.Y;

                if (newBounds.Height < TabSize)
                {
                    newBounds.Bottom = _bounds.Top - TabSize;
                }

                resizeB = true;
            }

            if (newBounds != _bounds)
            {
                Bounds = newBounds;
                _redraw = true;
            }

            // A resize isn't taking place
            if (!resizeL && !resizeR && !resizeT && !resizeB)
            {
                if (
                // Cursor is is range of right side of panel
                (e.Location.Y > (_bounds.Bottom - halfRange)) && (e.Location.Y < (_bounds.Top + halfRange)) &&
                (e.Location.X > (_bounds.Right - halfRange)) && (e.Location.X < (_bounds.Right + halfRange)))
                {
                    _canResizeRight = true;
                    resizeR = true;
                }
                else
                {
                    _canResizeRight = false;
                }
                if (
                    // Cursor is is range of left side of panel
                    (e.Location.Y > (_bounds.Bottom - halfRange)) && (e.Location.Y < (_bounds.Top + halfRange)) &&
                    (e.Location.X > (_bounds.Left - halfRange)) && (e.Location.X < (_bounds.Left + halfRange)))
                {
                    _canResizeLeft = true;
                    resizeL = true;
                }
                else
                {
                    _canResizeLeft = false;
                }
                if (
                    // Cursor is is range of top side of panel
                    (e.Location.X > (_bounds.Left - halfRange)) && (e.Location.X < (_bounds.Right + halfRange)) &&
                    (e.Location.Y > (_bounds.Top - halfRange)) && (e.Location.Y < (_bounds.Top + halfRange)))
                {
                    _canResizeTop = true;
                    resizeT = true;
                }
                else
                {
                    _canResizeTop = false;
                }
                if (
                    // Cursor is is range of bottom side of panel
                    (e.Location.X > (_bounds.Left - halfRange)) && (e.Location.X < (_bounds.Right + halfRange)) &&
                    (e.Location.Y > (_bounds.Bottom - halfRange)) && (e.Location.Y < (_bounds.Bottom + halfRange)))
                {
                    _canResizeBottom = true;
                    resizeB = true;
                }
                else
                {
                    _canResizeBottom = false;
                }
            }

            // Set cursor icons for resizeing
            if (resizeL)
            {
                if (resizeT)
                {
                    return Cursor.ResizeTopLeft;
                }

                if (resizeB)
                {
                    return Cursor.ResizeBottomLeft;
                }

                return Cursor.ResizeHorizontal;
            }
            if (resizeR)
            {
                if (resizeT)
                {
                    return Cursor.ResizeTopRight;
                }

                if (resizeB)
                {
                    return Cursor.ResizeBottomRight;
                }

                return Cursor.ResizeHorizontal;
            }
            if (resizeT || resizeB)
            {
                return Cursor.ResizeVertical;
            }

            // Not resizeing but cursor
            _canResizeRight = false;
            _canResizeLeft = false;
            _canResizeTop = false;
            _canResizeBottom = false;

            return _bounds.Contains(e.Location) ? Cursor.Default : null;
        }
        public bool MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                bool resize = false;

                if (_canResizeRight)
                {
                    _isResizeRight = true;
                    resize = true;
                }
                if (_canResizeLeft)
                {
                    _isResizeLeft = true;
                    resize = true;
                }
                if (_canResizeTop)
                {
                    _isResizeTop = true;
                    resize = true;
                }
                if (_canResizeBottom)
                {
                    _isResizeBottom = true;
                    resize = true;
                }

                if (resize)
                {
                    return true;
                }
            }

            if (e.Button == MouseButton.Left && _bounds.Contains(e.Location))
            {
                if (_tabBox.Contains(e.Location))
                {
                    _followMouse = true;
                    _mouseOffset = _bounds.Location - e.Location;
                }

                return true;
            }

            return false;
        }
        public void MouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                _followMouse = false;

                _isResizeRight = false;
                _isResizeLeft = false;
                _isResizeTop = false;
                _isResizeBottom = false;
            }
        }

        private Colour _tabColour = new Colour(45, 60, 75);
        public Colour TabColour
        {
            get => _tabColour;
            set
            {
                _tabColour = value;

                _redraw = true;
            }
        }
        private Colour _colour = new Colour(50, 50, 55);
        public Colour Colour
        {
            get => _colour;
            set
            {
                _colour = value;

                _frame.ClearColour = value;
                _redraw = true;
            }
        }

        private bool _redraw = true;

        public void Draw(double depth)
        {
            if (_redraw)
            {
                _frame.Bind();
                _frame.Clear(BufferBit.Colour | BufferBit.Depth);

                _shader.Bind();
                _shader.Matrix3 = Matrix4.CreateOrthographic(_bounds.Width, _bounds.Height, 0, -2);

                // Draw tab
                _shader.SetColourSource(ColourSource.UniformColour);
                _shader.SetDrawColour(_tabColour);

                _shader.Matrix1 = Matrix4.CreateScale(_bounds.Width, _tabBox.Height, depth)
                    * Matrix4.CreateTranslation((Vector3)(_tabBox.Centre - _bounds.Centre));
                _drawable.Draw();

                _redraw = false;
            }

            // Program framebuffer - draw to main framebuffer
            _handle.BindFramebuffer();

            _handle.SetProjection();

            _shader.Bind();
            _shader.SetColourSource(ColourSource.Texture);
            _shader.SetTextureSlot(0);

            Box bounds = Bounds;
            _shader.Matrix1 = Matrix4.CreateScale(bounds.Width, bounds.Height, depth) *
                Matrix4.CreateTranslation((Vector3)bounds.Centre);

            // Bind framebuffer colour texture
            _frame.GetTexture(FrameAttachment.Colour0).Bind(0);
            _drawable.Draw();
        }

        private bool _disposed;
        public void Dispose()
        {
            if (_disposed) { return; }

            _disposed = true;

            _frame.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
