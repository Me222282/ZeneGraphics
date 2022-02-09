using Zene.Graphics;
using Zene.Windowing;
using Zene.Windowing.Base;
using Zene.Structs;

namespace CSGL
{
    public class CubeMapTest : Window
    {
        public CubeMapTest(int width, int height, string title)
            : base(width, height, title, false)
        {
            _shader = new SkyBoxShader();

            _drawObject = new DrawObject<double, byte>(
                new double[]
                {
                    -1.0, 1.0, 1.0,
                    1.0, 1.0, 1.0,
                    1.0, -1.0, 1.0,
                    -1.0, -1.0, 1.0,
                    -1.0, 1.0, -1.0,
                    1.0, 1.0, -1.0,
                    1.0, -1.0, -1.0,
                    -1.0, -1.0, -1.0
                },
                new byte[]
                {
                    // Front
                    0, 3, 2,
                    2, 1, 0,

                    // Back
                    5, 6, 7,
                    7, 4, 5,

                    // Left
                    4, 7, 3,
                    3, 0, 4,

                    // Right
                    1, 2, 6,
                    6, 5, 1,

                    // Top
                    4, 0, 1,
                    1, 5, 4,

                    // Bottom
                    2, 3, 7,
                    7, 6, 2
                }, 3, 0, AttributeSize.D3, BufferUsage.DrawFrequent);

            Bitmap skyBox = new Bitmap("Resources/StandardCubeMap.jpg");
            int faceSize = skyBox.Width / 4;
            _cubeMap = CubeMap.Create(new Bitmap[]
            {
                skyBox.SubBitmap(faceSize * 2, faceSize, faceSize, faceSize),   // Right
                skyBox.SubBitmap(0, faceSize, faceSize, faceSize),              // Left
                skyBox.SubBitmap(faceSize, 0, faceSize, faceSize),              // Top
                skyBox.SubBitmap(faceSize, faceSize * 2, faceSize, faceSize),   // Bottom
                skyBox.SubBitmap(faceSize, faceSize, faceSize, faceSize),       // Front
                skyBox.SubBitmap(faceSize * 3, faceSize, faceSize, faceSize)    // Back
            }, WrapStyle.EdgeClamp, TextureSampling.Nearest, false);

            State.SeamlessCubeMaps = true;

            // Hide mouse
            GLFW.SetInputMode(Handle, GLFW.Cursor, GLFW.CursorHidden);

            OnSizeChange(new SizeChangeEventArgs(width, height));
            FullScreen = true;
        }

        private readonly DrawObject<double, byte> _drawObject;
        private readonly CubeMap _cubeMap;
        private readonly SkyBoxShader _shader;

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            if (dispose)
            {
                _cubeMap.Dispose();
                _shader.Dispose();
                _drawObject.Dispose();
            }
        }

        public void Run()
        {
            GLFW.SwapInterval(GLFW.True);

            while (GLFW.WindowShouldClose(Handle) == GLFW.False)
            {
                Draw();

                GLFW.SwapBuffers(Handle);
                GLFW.PollEvents();
            }

            Dispose();
        }
        private void Draw()
        {
            IFrameBuffer.Clear(BufferBit.Colour);

            MouseMovement();

            _shader.Bind();
            _shader.ViewMat = Matrix4.CreateRotationY(rotateY) * Matrix4.CreateRotationX(rotateX);
            _shader.TextureSlot = 0;

            _cubeMap.Bind(0);

            _drawObject.Draw();

            _cubeMap.UnBind();
        }

        protected override void OnSizeChange(SizeChangeEventArgs e)
        {
            base.OnSizeChange(e);

            _width = (int)e.Width;
            _height = (int)e.Height;

            Matrix4 proj = Matrix4.CreatePerspectiveFieldOfView(Radian.Degrees(60), e.Width / e.Height, 0.01, 5000);

            _shader.ProjectionMat = proj;
        }
        protected override void OnSizePixelChange(SizeChangeEventArgs e)
        {
            base.OnSizePixelChange(e);

            // Invalide size
            if ((int)e.Width <= 0 || (int)e.Height <= 0) { return; }

            IFrameBuffer.View((int)e.Width, (int)e.Height);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Keys.Escape)
            {
                Close();
                return;
            }

            if (e.Key == Keys.Tab)
            {
                FullScreen = !FullScreen;
                GLFW.SwapInterval(1);
                return;
            }

            if (e.Key == Keys.M)
            {
                if (GLFW.GetInputMode(Handle, GLFW.Cursor) == GLFW.CursorHidden)
                {
                    GLFW.SetInputMode(Handle, GLFW.Cursor, GLFW.CursorNormal);
                    _mouseShow = true;
                    return;
                }

                GLFW.SetInputMode(Handle, GLFW.Cursor, GLFW.CursorHidden);
                _mouseShow = false;
                return;
            }
        }

        private Vector2 mouseLocation;
        private Radian rotateX = Radian.Percent(0.5);
        private Radian rotateY = 0;
        private int _width;
        private int _height;

        private bool _mouseShow = false;
        private void MouseMovement()
        {
            GLFW.GetCursorPos(Handle, out double mX, out double mY);

            if (new Vector2(mX, mY) == mouseLocation) { return; }

            double distanceX = mX - mouseLocation.X;
            double distanceY = mY - mouseLocation.Y;

            mouseLocation = new Vector2(mX, mY);

            rotateY -= Radian.Degrees(distanceX * 0.1);
            rotateX += Radian.Degrees(distanceY * 0.1);

            GLFW.GetWindowPos(Handle, out int x, out int y);

            Vector2 newMPos = new Vector2(x + (_width / 2), y + (_height / 2));

            mouseLocation = newMPos;

            if (!_mouseShow)
            {
                GLFW.SetCursorPos(Handle, newMPos.X, newMPos.Y);
            }

            GLFW.GetCursorPos(Handle, out double cX, out double cY);

            mouseLocation = new Vector2(cX, cY);
        }
    }
}
