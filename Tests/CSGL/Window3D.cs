using System;
using Zene.Graphics.Base;
using Zene.Graphics.Z3D;
using Zene.Graphics;
using Zene.Graphics.Shaders;
using Zene.Windowing;
using Zene.Windowing.Base;
using Zene.Structs;
using System.Collections.Generic;
using System.Diagnostics;

namespace CSGL
{
    public unsafe class Window3D : Window
    {
        public Window3D(int width, int height, string title)
            : base(width, height, title)
        {
            Framebuffer = new PostProcessing(width, height);

            //postShader.Pixelate(true);
            //postShader.UseKernel(true);
            Framebuffer.SetKernel(PostShader.SharpenKernel);
            Framebuffer.SetKernelOffset(200);

            SetUp();

            GLFW.SetInputMode(Handle, GLFW.Cursor, GLFW.CursorHidden);

            OnSizeChange(new SizeChangeEventArgs(width, height));
            BaseFramebuffer.View = new RectangleI(0, 0, width, height);

            State.OutputDebug = false;
        }

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            if (dispose)
            {
                Shader.Dispose();
                Framebuffer.Dispose();

                DrawObject.Dispose();

                Floor.Dispose();
                FloorTexture.Dispose();
                FloorNormalMap.Dispose();

                LightObject.Dispose();

                FarLightObject.Dispose();

                _room.Dispose();

                loadObject.Dispose();
            }
        }

        private readonly List<string> _runTimeLog = new List<string>(new string[] { $"{DateTime.Now}\n" });

        protected override void GLError(uint type, string message)
        {
            base.GLError(type, message);

            if (type == GLEnum.DebugTypeError && message != null)
            {
                _runTimeLog.Add($"GL Output: {message}");
            }
        }

        public void Run()
        {
            _fpsTimer.Start();

            while (GLFW.WindowShouldClose(Handle) == 0)
            {
                State.ClearErrors();

                Framebuffer.Bind();

                GL.PolygonMode(GLEnum.FrontAndBack, _polygonMode);

                Draw();

                GL.PolygonMode(GLEnum.FrontAndBack, GLEnum.Fill);

                Framebuffer.Draw();

                GLFW.SwapBuffers(Handle);

                GLFW.PollEvents();

                _fpsCounter++;

                long time = _fpsTimer.ElapsedMilliseconds;

                if (time >= 10000)
                {
                    double sec = time * 0.001;

                    _runTimeLog.Add($"FPS:{_fpsCounter / sec}");

                    _fpsCounter = 0;
                    _fpsTimer.Restart();
                }
            }

            System.IO.File.WriteAllLines("runtimeLog.txt", _runTimeLog);

            Dispose();
        }

        public override PostProcessing Framebuffer { get; }

        private static readonly Vector3 Red = new Vector3(1, 0, 0);

        private static readonly Vector3 Green = new Vector3(0, 1, 0);

        private static readonly Vector3 Blue = new Vector3(0, 0, 1);

        private static readonly Vector3 Yellow = new Vector3(1, 1, 0);

        private readonly Vector3[] vertData = new Vector3[]
        {
            /*Vertex*/ new Vector3(-5, 5, 5), Red,
            /*Vertex*/ new Vector3(5, 5, 5), Green,
            /*Vertex*/ new Vector3(5, -5, 5), Blue,
            /*Vertex*/ new Vector3(-5, -5, 5), Yellow,

            /*Vertex*/ new Vector3(-5, 5, -5), Blue,
            /*Vertex*/ new Vector3(5, 5, -5), Yellow,
            /*Vertex*/ new Vector3(5, -5, -5), Red,
            /*Vertex*/ new Vector3(-5, -5, -5), Green
        };
        private readonly uint[] indexData = new uint[]
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
        };

        private LightingShader Shader;

        private DrawObject<Vector3, uint> DrawObject;
        private double objectRotation = 0.001;

        private Material ObjectMaterial = new Material(Material.Source.Default, Material.Source.None, Shine.None);

        private Object3D loadObject;
        private Texture2D _loadObjectImage;

        private DrawObject<Vector3, uint> Floor;

        private Texture2D FloorTexture;
        private Texture2D FloorNormalMap;

        private Material FloorMaterial = new Material(Material.Source.Default, Material.Source.Default, Shine.L);

        private Light LightObject;

        private Light FarLightObject;

        private Zene.Graphics.Shaders.Light light;

        private Zene.Graphics.Shaders.Light farLight;

        private double lightAmplitude = 0;

        private Room _room;

        private TextRenderer _textDisplay;
        private Font _font;

        protected virtual void SetUp()
        {
            Shader = new LightingShader(4, 1);

            Object3D.AddNormals(vertData, 2, indexData, out List<Vector3> vertices, out List<uint> indices);

            DrawObject = new DrawObject<Vector3, uint>(vertices, indices, 3, 0, AttributeSize.D3, BufferUsage.DrawFrequent);

            DrawObject.AddAttribute((int)LightingShader.Location.ColourAttribute, 1, AttributeSize.D3); // Colour attribute
            DrawObject.AddAttribute((int)LightingShader.Location.Normal, 2, AttributeSize.D3); // Normals

            Object3D.AddNormalTangents(new Vector3[] { new Vector3(-500, 10, -500), new Vector3(0, 0, 0),
                new Vector3(500, 10, -500), new Vector3(100, 0, 0),
                new Vector3(500, 10, 500), new Vector3(100, 100, 0),
                new Vector3(-500, 10, 500), new Vector3(0, 100, 0)}, 2, 1, new uint[] { 0, 1, 2, 2, 3, 0 }, out List<Vector3> floorVerts, out List<uint> floorInds);

            Floor = new DrawObject<Vector3, uint>(floorVerts, floorInds, 4, 0, AttributeSize.D3, BufferUsage.DrawFrequent);

            FloorTexture = Texture2D.Create("Resources/wood.png", WrapStyle.Repeated, TextureSampling.BlendMipMapBlend, true);

            Shader.SetTextureSlot(0);
            Shader.SetNormalMapSlot(1);
            FloorNormalMap = Texture2D.Create("Resources/woodNor.png", WrapStyle.Repeated, TextureSampling.BlendMipMapBlend, true);

            Floor.AddAttribute((int)LightingShader.Location.TextureCoords, 1, AttributeSize.D3); // Texture Coordinates
            Floor.AddAttribute((int)LightingShader.Location.NormalMapTextureCoords, 1, AttributeSize.D3); // Normal Map Coordinates
            Floor.AddAttribute((int)LightingShader.Location.Normal, 2, AttributeSize.D3); // Normals
            Floor.AddAttribute((int)LightingShader.Location.Tangents, 3, AttributeSize.D3); // Tangents

            State.Blending = true;
            GL.BlendFunc(GLEnum.SrcAlpha, GLEnum.OneMinusSrcAlpha);

            //GL.PolygonMode(GLEnum.FrontAndBack, GLEnum.Line);

            rotationMatrix = Matrix3.CreateRotationX(0);

            light = new Zene.Graphics.Shaders.Light(new Colour(255, 255, 255), Colour.Zero, 0.0014, 0.000007, new Vector3(10, 0, 10));
            Shader.SetLight(0, light);

            farLight = new Zene.Graphics.Shaders.Light(new Colour(200, 200, 200), Colour.Zero, 0.014, 0.0007, new Vector3(500, 0, 500));
            Shader.SetLight(1, farLight);

            Shader.SetAmbientLight(new Colour(12, 12, 15));

            LightObject = new Light(Vector3.Zero, 0.5, BufferUsage.DrawFrequent);

            FarLightObject = new Light(new Vector3(500, 0, 500), 0.5, BufferUsage.DrawFrequent);

            cameraLightCC = cameraLight = new Colour(120, 110, 100);
            Shader.SetSpotLight(0, new SpotLight(cameraLight, Radian.Degrees(22.5), Radian.Degrees(40), 0.0045, 0.00075, Vector3.Zero, new Vector3(0, 0, 1)));

            _room = new Room(Shader);

            Shader.IngorBlackLight(true);

            loadObject = Object3D.FromObj("Resources/Sphere.obj", (uint)LightingShader.Location.TextureCoords, (uint)LightingShader.Location.Normal);

            _textDisplay = new TextRenderer(12)
            {
                AutoIncreaseCapacity = true
            };
            _font = new FontA();

            _loadObjectImage = Texture2D.Create(new GLArray<Colour>(1, 1, 1, new Colour(134, 94, 250)), WrapStyle.EdgeClamp, TextureSampling.Blend, false);
        }

        private readonly Matrix3 lightRotation = Matrix3.CreateRotationY(Radian.Percent(0.001));

        private Vector3 CameraPos = Vector3.Zero;

        private Radian rotateX = Radian.Percent(0.5);
        private Radian rotateY = 0;
        private Radian rotateZ = 0;

        private Matrix3 rotationMatrix;

        private Colour cameraLight;

        private double moveSpeed = 1;

        //private bool _texturesLoaded = false;
        protected virtual void Draw()
        {
            //if (!_texturesLoaded)
            //{
            //    _texturesLoaded = Bitmap.CheckTextures();
            //}

            MouseMovement();

            rotationMatrix = Matrix3.CreateRotationY(rotateY) * Matrix3.CreateRotationX(rotateX);

            Vector3 cameraMove = new Vector3(0, 0, 0);

            if (_left)      { cameraMove.X += moveSpeed; }
            if (_right)     { cameraMove.X -= moveSpeed; }
            if (_forward)   { cameraMove.Z -= moveSpeed; }
            if (_backward)  { cameraMove.Z += moveSpeed; }
            if (_up)        { cameraMove.Y -= moveSpeed; }
            if (_down)      { cameraMove.Y += moveSpeed; }

            if (_lShift)    { cameraMove *= 2; }
            if (_lSlow) { cameraMove *= 0.25; }

            if (_lAltGoFast) { cameraMove *= 4; }

            CameraPos += cameraMove * rotationMatrix;

            Shader.Bind();

            Shader.SetCameraPosition(-CameraPos);

            light.LightVector *= lightRotation;

            Shader.SetLightPosition(0, light.LightVector);

            Matrix4 view = Matrix4.CreateTranslation(CameraPos) * Matrix4.CreateRotationY(rotateY) *
                Matrix4.CreateRotationX(rotateX) * Matrix4.CreateRotationZ(rotateZ);
            Shader.SetViewMatrix(view);
            _textDisplay.View = view;

            Shader.SetSpotLightPosition(0, -CameraPos);
            Shader.SetSpotLightDirection(0, new Vector3(0, 0, 1) *
                Matrix3.CreateRotationX(rotateX - Radian.Percent(0.5)) * Matrix3.CreateRotationY(rotateY) * Matrix3.CreateRotationZ(rotateZ));

            State.DepthTesting = true;

            //IFrameBuffer.ClearColour(new ColourF(0.2f, 0.4f, 0.8f, 1.0f));
            Framebuffer.Clear(BufferBit.Colour | BufferBit.Depth);

            Shader.UseNormalMapping(false);
            Shader.DrawLighting(doLight);
            Shader.SetModelMatrix(Matrix4.CreateRotationX(Radian.Percent(objectRotation)));
            objectRotation += 0.001;
            Shader.SetColourSource(ColourSource.AttributeColour);
            Shader.SetMaterial(ObjectMaterial);

            DrawObject.Draw();

            Shader.DrawLighting(doLight);
            Shader.SetModelMatrix(Matrix4.CreateRotationZ(Radian.Percent(0.5)) * Matrix4.CreateRotationY(Radian.Percent(0.25)) * Matrix4.CreateTranslation(100, 0, 0));
            //Shader.SetColourSource(ColourSource.UniformColour);
            Shader.SetColourSource(ColourSource.Texture);
            Shader.SetDrawColour(new Colour(134, 94, 250));

            _loadObjectImage.Bind(0);
            Shader.SetTextureSlot(0);
            loadObject.Draw();

            Shader.SetColourSource(ColourSource.Texture);
            Shader.SetModelMatrix(Matrix4.Identity);
            Shader.SetMaterial(FloorMaterial);
            Shader.UseNormalMapping(true);

            FloorTexture.Bind(0);
            FloorNormalMap.Bind(1);

            Floor.Draw();

            FloorTexture.Unbind();
            FloorNormalMap.Unbind();

            Shader.UseNormalMapping(false);
            Shader.DrawLighting(false);
            Shader.SetModelMatrix(Matrix4.CreateTranslation(light.LightVector));
            Shader.SetColourSource(ColourSource.UniformColour);
            Shader.SetDrawColour(light.LightColour);
            
            LightObject.Draw();


            double lA = Math.Sin(Radian.Percent(lightAmplitude));
            lightAmplitude += 0.005;

            byte colourValue = (byte)(255 - (int)((lA + 1) * 100));
            farLight.LightColour = new Colour(colourValue, colourValue, colourValue);

            //double lightScale = (3 - ((lA + 1) * 1.5)) + 1;

            double lightSizeL = ((lA + 1) * 4.5) + 1;
            double lightSizeQ = ((lA + 1) * 4.5) + 1;
            farLight.Linear = 0.0014 * lightSizeL;
            farLight.Quadratic = 0.000007 * lightSizeQ;

            Shader.SetLight(1, farLight);

            Shader.SetModelMatrix(Matrix4.Identity);
            Shader.SetColourSource(ColourSource.UniformColour);
            Shader.SetDrawColour(farLight.LightColour);

            FarLightObject.Draw();

            Shader.DrawLighting(doLight);
            Shader.SetMaterial(_room.RoomMat);
            Shader.SetModelMatrix(Matrix4.CreateTranslation(8000, 0, 0));
            Shader.SetColourSource(ColourSource.Texture);
            Shader.SetTextureSlot(Room.TexTexSlot);
            Shader.SetNormalMapSlot(Room.NorTexSlot);
            Shader.UseNormalMapping(true);
            _room.Draw();

            _textDisplay.Model = Matrix4.CreateTranslation(0, 0, -5.1) * Matrix4.CreateRotationX(Radian.Percent(objectRotation));
            _textDisplay.DrawCentred($"{objectRotation:N3}\n", _font, -0.15, 0);
            _textDisplay.DrawCentred($"\n{CameraPos.SquaredLength:N3}", _font, -0.15, 0);
        }

        private readonly Stopwatch _fpsTimer = new Stopwatch();
        private int _fpsCounter = 0;

        private bool _left;
        private bool _right;
        private bool _up;
        private bool _down;
        private bool _forward;
        private bool _backward;
        private bool _lShift;
        private bool _lAltGoFast;
        private bool _lSlow;

        private bool flashLight = true;
        private bool doLight = true;
        private Colour cameraLightCC;

        private bool _postProcess = false;
        private uint _polygonMode = GLEnum.Fill;

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Keys.LeftShift)
            {
                _lShift = true;
            }
            else if (e.Key == Keys.RightShift)
            {
                _lSlow = true;
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
            }
            else if (e.Key == Keys.BackSpace)
            {
                CameraPos = Vector3.Zero;
                rotateX = Radian.Percent(0.5);
                rotateY = 0;
                rotateZ = 0;
                moveSpeed = 1;

                Shader.SetSpotLightColour(0, cameraLight);
                cameraLightCC = cameraLight;
            }
            else if (e.Key == Keys.Enter)
            {
                CameraPos = new Vector3(-8008, -2, 8);
                rotateX = Radian.Percent(0.5);
                rotateY = 0;
                rotateZ = 0;
                moveSpeed = 0.125;

                Shader.SetSpotLightColour(0, new Colour(255, 235, 210));
                cameraLightCC = new Colour(255, 235, 210);
            }
            else if (e.Key == Keys.L)
            {
                flashLight = !flashLight;

                if (flashLight)
                {
                    Shader.SetSpotLightColour(0, cameraLightCC);
                }
                else
                {
                    Shader.SetSpotLightColour(0, Colour.Zero);
                }
            }
            else if (e.Key == Keys.N)
            {
                doLight = !doLight;
            }
            else if (e.Key == Keys.P)
            {
                _postProcess = !_postProcess;

                if (_postProcess)
                {
                    Framebuffer.Pixelate(true);
                    Framebuffer.UseKernel(true);
                }
                else
                {
                    Framebuffer.Pixelate(false);
                    Framebuffer.UseKernel(false);
                }
            }
            else if (e.Key == Keys.M)
            {
                if (GLFW.GetInputMode(Handle, GLFW.Cursor) == GLFW.CursorHidden)
                {
                    GLFW.SetInputMode(Handle, GLFW.Cursor, GLFW.CursorNormal);
                    _mouseShow = true;
                    return;
                }

                GLFW.SetInputMode(Handle, GLFW.Cursor, GLFW.CursorHidden);
                _mouseShow = false;
            }
            else if (e.Key == Keys.J)
            {
                if (_polygonMode == GLEnum.Fill)
                {
                    _polygonMode = GLEnum.Line;
                    return;
                }

                _polygonMode = GLEnum.Fill;
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.Key == Keys.LeftShift)
            {
                _lShift = false;
            }
            else if (e.Key == Keys.RightShift)
            {
                _lSlow = false;
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
        private const double _near = 0.1;
        private const double _far = 3000;
        protected override void OnSizeChange(SizeChangeEventArgs e)
        {
            base.OnSizeChange(e);

            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(Radian.Degrees(_zoom), (double)e.Width / e.Height, _near, _far);

            Shader.SetProjectionMatrix(matrix);
            _textDisplay.Projection = matrix;
        }

        protected override void OnSizePixelChange(SizeChangeEventArgs e)
        {
            base.OnSizePixelChange(e);

            Framebuffer.Size = new Vector2I(Width, Height);
            BaseFramebuffer.ViewSize = new Vector2I(Width, Height);

            double mWidth;
            double mHeight;

            if (e.Width > e.Height)
            {
                double heightPercent = (double)e.Height / e.Width;

                mWidth = 400;

                mHeight = mWidth * heightPercent;
            }
            else
            {
                double widthPercent = (double)e.Width / e.Height;

                mHeight = 56.25 * 4;

                mWidth = mHeight * widthPercent;
            }

            Framebuffer.PixelSize(mWidth, mHeight);
        }

        private double _zoom = 60;
        protected override void OnScroll(ScrollEventArgs e)
        {
            base.OnScroll(e);

            _zoom -= e.DeltaY;

            if (_zoom < 1)
            {
                _zoom = 1;
            }
            else if (_zoom > 179)
            {
                _zoom = 179;
            }

            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(Radian.Degrees(_zoom), (double)Width / Height, _near, _far);

            Shader.SetProjectionMatrix(matrix);
            _textDisplay.Projection = matrix;
        }

        private bool _mouseShow = false;
        private Vector2 mouseLocation;
        private void MouseMovement()
        {
            if (_mouseShow) { return; }

            // Window focused? - shouldn't calculate mouse movement
            if (GLFW.GetWindowAttrib(Handle, GLFW.Focused) == GLFW.False) { return; }

            GLFW.GetCursorPos(Handle, out double mX, out double mY);

            if (new Vector2(mX, mY) == mouseLocation) { return; }

            double distanceX = mX - mouseLocation.X;
            double distanceY = mouseLocation.Y - mY;

            mouseLocation = new Vector2(mX, mY);

            rotateY += Radian.Degrees(distanceX * 0.1);
            rotateX += Radian.Degrees(distanceY * 0.1);

            Vector2 newMPos = new Vector2(Width / 2.0, Height / 2.0);

            mouseLocation = newMPos;

            GLFW.SetCursorPos(Handle, newMPos.X, newMPos.Y);
        }

        protected override void OnFileDrop(FileDropEventArgs e)
        {
            base.OnFileDrop(e);

            if (Bitmap.GetImageEncoding(e.Paths[0]) != ImageEncoding.Unknown)
            {
                _loadObjectImage.Dispose();

                _loadObjectImage = Texture2D.Create(e.Paths[0], WrapStyle.EdgeClamp, TextureSampling.Blend, true);

                return;
            }

            loadObject.Dispose();

            try
            {
                loadObject = Object3D.FromObj(e.Paths[0], (uint)LightingShader.Location.TextureCoords, (uint)LightingShader.Location.Normal);
            }
            catch (Exception)
            {
                loadObject = Object3D.FromObj(e.Paths[0], (uint)LightingShader.Location.Normal);
            }
        }
    }
}
