using Zene.Graphics;
using Zene.Windowing;
using Zene.Windowing.Base;
using Zene.Structs;
using System;

namespace CSGL
{
    public class CubeMapTest : Window
    {
        public CubeMapTest(int width, int height, string title)
            : base(width, height, title)
        {
            _shader = new SkyBoxShader();

            _drawObject = new DrawObject<double, byte>(
                new double[]
                {
                    -1.0, 1.0, 1.0,     // 0
                    1.0, 1.0, 1.0,      // 1
                    1.0, -1.0, 1.0,     // 2
                    -1.0, -1.0, 1.0,    // 3

                    -1.0, 1.0, -1.0,    // 4
                    1.0, 1.0, -1.0,     // 5
                    1.0, -1.0, -1.0,    // 6
                    -1.0, -1.0, -1.0    // 7
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

            Bitmap.AutoFlipTextures = false;

            /*
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
            *//*
            Bitmap skyBox = new Bitmap("Resources/CubeMapHD.png");
            Console.WriteLine("Texture Decoded.");
            Console.WriteLine(test[0, 0]);

            _cubeMap = CubeMap.Create(new Bitmap[]
            {
                skyBox.SubBitmap(faceSize * 1, 0, faceSize, faceSize),      // Right
                skyBox.SubBitmap(faceSize * 3, 0, faceSize, faceSize),      // Left
                skyBox.SubBitmap(faceSize * 4, 0, faceSize, faceSize),      // Top
                skyBox.SubBitmap(faceSize * 5, 0, faceSize, faceSize),      // Bottom
                skyBox.SubBitmap(faceSize * 0, 0, faceSize, faceSize),      // Front
                skyBox.SubBitmap(faceSize * 2, 0, faceSize, faceSize)       // Back
            }, WrapStyle.EdgeClamp, TextureSampling.Blend, false);
            Console.WriteLine("Texture Loaded.");*/
            /*
            int faceSize = 2048;
            _cubeMap = CubeMap.LoadSync("Resources/CubeMapHD.png", new RectangleI[]
            {
                new RectangleI(faceSize * 1, faceSize, faceSize, faceSize),    // Right
                new RectangleI(faceSize * 3, faceSize, faceSize, faceSize),    // Left
                new RectangleI(faceSize * 4, faceSize, faceSize, faceSize),    // Top
                new RectangleI(faceSize * 5, faceSize, faceSize, faceSize),    // Bottom
                new RectangleI(faceSize * 0, faceSize, faceSize, faceSize),    // Front
                new RectangleI(faceSize * 2, faceSize, faceSize, faceSize),    // Back
            }, WrapStyle.EdgeClamp, TextureSampling.Blend, false);*/

            
            _cubeMap = CubeMap.LoadSync(new string[]
            {
                "Resources/cubeMaps/Storforsen4/posx.jpg",  // Right
                "Resources/cubeMaps/Storforsen4/negx.jpg",  // Left
                "Resources/cubeMaps/Storforsen4/posy.jpg",  // Top
                "Resources/cubeMaps/Storforsen4/negy.jpg",  // Bottom
                "Resources/cubeMaps/Storforsen4/posz.jpg",  // Front
                "Resources/cubeMaps/Storforsen4/negz.jpg"   // Back
            }, 2048, WrapStyle.EdgeClamp, TextureSampling.Blend, true);

            State.SeamlessCubeMaps = true;
            State.DepthTesting = true;

            // Hide mouse
            GLFW.SetInputMode(Handle, GLFW.Cursor, GLFW.CursorHidden);

            OnSizeChange(new SizeChangeEventArgs(width, height));
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
        private bool _textureLoaded = false;
        private Vector3 _offset = Vector3.Zero;
        private void Draw()
        {
            if (!_textureLoaded)
            {
                _textureLoaded = CubeMap.CheckSyncLoading();
            }

            Framebuffer.Clear(BufferBit.Colour | BufferBit.Depth);

            MouseMovement();

            Vector3 cameraMove = new Vector3(0, 0, 0);

            if (_w)
            {
                cameraMove.Z -= 0.025;
            }
            if (_s)
            {
                cameraMove.Z += 0.025;
            }
            if (_a)
            {
                cameraMove.X += 0.025;
            }
            if (_d)
            {
                cameraMove.X -= 0.025;
            }
            if (_space)
            {
                cameraMove.Y -= 0.025;
            }
            if (_ctrl)
            {
                cameraMove.Y += 0.025;
            }

            Matrix3 rotationMatrix = Matrix3.CreateRotationY(rotateY) * Matrix3.CreateRotationX(rotateX);

            _offset += cameraMove * rotationMatrix;

            _shader.Bind();
            _shader.View = Matrix4.CreateTranslation(_offset) * Matrix4.CreateRotationY(rotateY) * Matrix4.CreateRotationX(rotateX);
            _shader.TextureSlot = 0;

            _cubeMap.Bind(0);

            _drawObject.Draw();

            _cubeMap.Unbind();
        }

        private double _zoom = 60;
        private const double _near = 0.00001;
        private const double _far = 10;
        protected override void OnScroll(ScrollEventArgs e)
        {
            base.OnScroll(e);

            _zoom -= e.DeltaY;

            if (_zoom >= 179)
            {
                _zoom = 179;
            }

            else if (_zoom <= 1)
            {
                _zoom = 1;
            }

            _shader.Projection = Matrix4.CreatePerspectiveFieldOfView(Radian.Degrees(_zoom), (double)Width / Height, _near, _far);
        }

        protected override void OnSizeChange(SizeChangeEventArgs e)
        {
            base.OnSizeChange(e);

            _width = (int)e.Width;
            _height = (int)e.Height;

            _shader.Projection = Matrix4.CreatePerspectiveFieldOfView(Radian.Degrees(_zoom), (double)e.Width / e.Height, _near, _far);
        }
        protected override void OnSizePixelChange(SizeChangeEventArgs e)
        {
            base.OnSizePixelChange(e);

            // Invalide size
            if ((int)e.Width <= 0 || (int)e.Height <= 0) { return; }

            Framebuffer.ViewSize = new Vector2I((int)e.Width, (int)e.Height);
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

            if (e.Key == Keys.Enter)
            {
                _offset = Vector3.Zero;
                return;
            }

            if (e.Key == Keys.W)
            {
                _w = true;
                return;
            }
            if (e.Key == Keys.S)
            {
                _s = true;
                return;
            }
            if (e.Key == Keys.A)
            {
                _a = true;
                return;
            }
            if (e.Key == Keys.D)
            {
                _d = true;
                return;
            }
            if (e.Key == Keys.Space)
            {
                _space = true;
                return;
            }
            if (e.Key == Keys.LeftControl)
            {
                _ctrl = true;
                return;
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.Key == Keys.W)
            {
                _w = false;
                return;
            }
            if (e.Key == Keys.S)
            {
                _s = false;
                return;
            }
            if (e.Key == Keys.A)
            {
                _a = false;
                return;
            }
            if (e.Key == Keys.D)
            {
                _d = false;
                return;
            }
            if (e.Key == Keys.Space)
            {
                _space = false;
                return;
            }
            if (e.Key == Keys.LeftControl)
            {
                _ctrl = false;
                return;
            }
        }
        private bool _w;
        private bool _s;
        private bool _a;
        private bool _d;
        private bool _space;
        private bool _ctrl;

        private Vector2 mouseLocation;
        private Radian rotateX = 0;
        private Radian rotateY = 0;
        private int _width;
        private int _height;

        private bool _mouseShow = false;
        private void MouseMovement()
        {
            if (_mouseShow) { return; }

            GLFW.GetCursorPos(Handle, out double mX, out double mY);

            if (new Vector2(mX, mY) == mouseLocation) { return; }

            double distanceX = mX - mouseLocation.X;
            double distanceY = mouseLocation.Y - mY;

            mouseLocation = new Vector2(mX, mY);

            rotateY -= Radian.Degrees(distanceX * 0.1);
            rotateX += Radian.Degrees(distanceY * 0.1);

            Vector2 newMPos = new Vector2(_width / 2, _height / 2);

            mouseLocation = newMPos;

            GLFW.SetCursorPos(Handle, newMPos.X, newMPos.Y);
        }
    }
}
