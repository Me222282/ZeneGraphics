using System;
using Zene.Graphics;
using Zene.Graphics.Base;
using Zene.Graphics.Shaders;
using Zene.Windowing;
using Zene.Windowing.Base;
using Zene.Structs;

namespace CSGL
{
    public class WindowTest : Window
    {
        public WindowTest(int width, int height, string title)
            : base(width, height, title)
        {
            SetUp();
        }

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            if (dispose)
            {
                _shader.Dispose();

                _drawObject.Dispose();
                _texture.Dispose();
            }
        }

        public void Run()
        {
            GLFW.SwapInterval(1);

            while (GLFW.WindowShouldClose(Handle) == 0)
            {
                Draw();

                GLFW.SwapBuffers(Handle);

                GLFW.PollEvents();
            }

            Dispose();
        }

        private readonly float[] vertData = new float[]
        {
            /*Vertex*/ 0f, 5f, 5f,      /*Tex Coord*/ 0f, 0f,
            /*Vertex*/ 0f, 5f, -5f,     /*Tex Coord*/ 1f, 0f,
            /*Vertex*/ 0f, -5f, -5f,    /*Tex Coord*/ 1f, 1f,
            /*Vertex*/ 0f, -5f, 5f,     /*Tex Coord*/ 0f, 1f
        };

        private readonly byte[] indices = new byte[]
        {
            0, 1, 2,
            2, 3, 0
        };

        private DrawObject<float, byte> _drawObject;

        private Texture2D _texture;

        private BasicShader _shader;

        private Room _room;

        protected virtual void SetUp()
        {
            _shader = new BasicShader();

            _texture = Texture2D.Create("Resources/CharacterLeftN.png", WrapStyle.EdgeClamp, TextureSampling.Nearest, false);

            _drawObject = new DrawObject<float, byte>(vertData, indices, 5, 0, AttributeSize.D3, BufferUsage.DrawFrequent);
            _drawObject.AddAttribute((int)BasicShader.Location.TextureCoords, 3, AttributeSize.D2); // Texture Coordinates

            GL.Enable(GLEnum.Blend);
            GL.BlendFunc(GLEnum.SrcAlpha, GLEnum.OneMinusSrcAlpha);
            GL.Enable(GLEnum.DepthTest);

            _room = new Room();
            BaseFramebuffer.ClearColour = new ColourF(1f, 1f, 1f);
        }

        private Matrix3 rotationMatrix;

        private readonly double moveSpeed = 0.5;

        private Vector3 CameraPos = Vector3.Zero;

        private double pDir = 0;

        protected virtual void Draw()
        {
            MouseMovement();

            rotationMatrix = Matrix3.CreateRotationY(rotateY);

            Vector3 cameraMove = new Vector3(0, 0, 0);

            if (_left) 
            {
                pDir = 0;
                cameraMove.X += moveSpeed;
            }
            if (_right) 
            {
                pDir = 0.5;
                cameraMove.X -= moveSpeed;
            }
            if (_forward)   { cameraMove.Z += moveSpeed; }
            if (_backward)  { cameraMove.Z -= moveSpeed; }
            if (_up)        { cameraMove.Y += moveSpeed; }
            if (_down)      { cameraMove.Y -= moveSpeed; }

            if (_lShift)    { cameraMove *= 2; }
            if (_lAltGoFast) { cameraMove *= 4; }

            CameraPos += cameraMove * rotationMatrix;

            _shader.Bind();

            _shader.Matrix2 = Matrix4.CreateTranslation(CameraPos) * Matrix4.CreateRotationY(rotateY) *
                Matrix4.CreateRotationX(Radian.Percent(-0.125 + 0.5));

            BaseFramebuffer.Clear(BufferBit.Colour | BufferBit.Depth);

            _shader.Matrix1 = Matrix4.Identity;
            _shader.SetColourSource(ColourSource.Texture);
            _shader.SetTextureSlot(Room.TexTexSlot);
            _room.Draw();

            _shader.Matrix1 = Matrix4.CreateScale(0.25) * Matrix4.CreateRotationY(Radian.Percent(pDir)) * Matrix4.CreateRotationZ(-0.125) *
                Matrix4.CreateTranslation(-CameraPos + new Vector3(-5, 5, 0));
            _shader.SetColourSource(ColourSource.Texture);
            _shader.SetTextureSlot(0);

            _texture.Bind(0);
            _drawObject.Draw();
            _texture.Unbind();
        }

        private bool _left;
        private bool _right;
        private bool _up;
        private bool _down;
        private bool _forward;
        private bool _backward;
        private bool _lShift;
        private bool _lAltGoFast;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Keys.LeftShift)
            {
                _lShift = true;
            }
            else if (e.Key == Keys.LeftAlt)
            {
                _lAltGoFast = true;
            }
            else if (e.Key == Keys.LeftControl)
            {
                _down = true;
            }
            else if (e.Key == Keys.S)
            {
                _backward = true;
            }
            else if (e.Key == Keys.W)
            {
                _forward = true;
            }
            else if (e.Key == Keys.A)
            {
                _left = true;
            }
            else if (e.Key == Keys.D)
            {
                _right = true;
            }
            else if (e.Key == Keys.Space)
            {
                _up = true;
            }
            else if (e.Key == Keys.Escape)
            {
                Close();
            }
            else if (e.Key == Keys.Tab)
            {
                FullScreen = !FullScreen;
                GLFW.SwapInterval(1);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.Key == Keys.LeftShift)
            {
                _lShift = false;
            }
            else if (e.Key == Keys.LeftAlt)
            {
                _lAltGoFast = false;
            }
            else if (e.Key == Keys.LeftControl)
            {
                _down = false;
            }
            else if (e.Key == Keys.S)
            {
                _backward = false;
            }
            else if (e.Key == Keys.W)
            {
                _forward = false;
            }
            else if (e.Key == Keys.A)
            {
                _left = false;
            }
            else if (e.Key == Keys.D)
            {
                _right = false;
            }
            else if (e.Key == Keys.Space)
            {
                _up = false;
            }
        }

        protected override void OnSizeChange(SizeChangeEventArgs e)
        {
            base.OnSizeChange(e);

            _width = (int)e.Width;
            _height = (int)e.Height;

            _shader.Matrix3 = Matrix4.CreatePerspectiveFieldOfView(Radian.Degrees(65), (double)e.Width / e.Height, 0.1, 1000);
        }

        protected override void OnSizePixelChange(SizeChangeEventArgs e)
        {
            base.OnSizePixelChange(e);

            BaseFramebuffer.ViewSize = new Vector2I((int)e.Width, (int)e.Height);
        }

        private Vector2 mouseLocation;

        private Radian rotateX = 0;
        private Radian rotateY = 0;
        private Radian rotateZ = 0;

        private int _width;
        private int _height;

        private void MouseMovement()
        {
            GLFW.GetCursorPos(Handle, out double mX, out double mY);

            if (new Vector2(mX, mY) == mouseLocation) { return; }

            double distanceX = mX - mouseLocation.X;
            double distanceY = mouseLocation.Y - mY;

            mouseLocation = new Vector2(mX, mY);

            rotateY += Radian.Degrees(distanceX * 0.1);
            rotateX += Radian.Degrees(distanceY * 0.1);

            GLFW.GetWindowPos(Handle, out int x, out int y);

            Vector2 newMPos = new Vector2(x + (_width / 2), y + (_height / 2));

            mouseLocation = newMPos;

            GLFW.SetCursorPos(Handle, newMPos.X, newMPos.Y);

            GLFW.GetCursorPos(Handle, out double cX, out double cY);

            mouseLocation = new Vector2(cX, cY);
        }
    }
}
