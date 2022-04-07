using System;
using Zene.Windowing;
using Zene.Windowing.Base;
using Zene.Graphics;
using Zene.Structs;
using System.Text;

namespace CustomConsole
{
    class Program : Window
    {
        static void Main()
        {
            Core.Init();
            
            Program window = new Program(800, 500, VirtualConsole.Directory);

            window.Run();
            
            Core.Terminate();
        }
        
        public Program(int width, int height, string title)
         : base(width, height, title)
        {
            _textRender = new TextRenderer(40)
            {
                AutoIncreaseCapacity = true
            };
            _fontC = new FontC();

            // Opacity
            State.Blending = true;
            Zene.Graphics.Base.GL.BlendFunc(Zene.Graphics.Base.GLEnum.SrcAlpha, Zene.Graphics.Base.GLEnum.OneMinusSrcAlpha);

            OnSizePixelChange(new SizeChangeEventArgs(width, height));
        }

        private readonly TextRenderer _textRender;
        private readonly Font _fontC;

        private readonly StringBuilder _enterText = new StringBuilder(16);

        private double _margin = 5;
        private double _charSize = 15;

        private static char Caret
        {
            get
            {
                char caret = '\x0';

                // Flash caret
                if (((int)Math.Floor(GLFW.GetTime() * 2) % 2) == 0)
                {
                    caret = '_';
                }

                return caret;
            }
        }
        private int _textIndex = 0;

        public void Run()
        {
            // Vsync
            GLFW.SwapInterval(1);

            VirtualConsole.AddFunction("Copy", new StringConverter[] { VirtualConsole.StringParam, VirtualConsole.IntParam }, (objs, info) =>
            {
                bool displayCount = false;

                if (info == "-i")
                {
                    displayCount = true;
                }
                else if (info != null && info.Length != 0)
                {
                    Console.WriteLine(info.Length);
                    VirtualConsole.Log("Invalid extra info");
                    return;
                }

                string text = (string)objs[0];
                int count = (int)objs[1];

                for (int i = 0; i < count; i++)
                {
                    if (displayCount)
                    {
                        VirtualConsole.Log($"{text} - {i}");
                        continue;
                    }

                    VirtualConsole.Log(text);
                }
            });

            while (GLFW.WindowShouldClose(Handle) == GLFW.False)
            {
                Framebuffer.Clear(BufferBit.Colour);

                _textRender.Model = Matrix4.CreateScale(_charSize) *
                    Matrix4.CreateTranslation((Width * -0.5) + _margin, (Height * 0.5) - _margin, 0d);

                for (int i = 0; i < VirtualConsole.Output.Count; i++)
                {
                    _textRender.DrawLeftBound(new string('\n', i) + VirtualConsole.Output[i], _fontC);
                }

                string offset = new string('\n', VirtualConsole.Output.Count);

                _textRender.DrawLeftBound(offset + "Console> " + _enterText.ToString(), _fontC);
                _textRender.DrawLeftBound(offset + new string(' ', 9 + _textIndex) + Caret, _fontC);

                if (Title != VirtualConsole.Directory)
                {
                    Title = VirtualConsole.Directory;
                }

                GLFW.SwapBuffers(Handle);
                GLFW.PollEvents();
            }
        }
        
        protected override void OnSizePixelChange(SizeChangeEventArgs e)
        {
            base.OnSizePixelChange(e);
            
            Framebuffer.ViewSize = e.Size;

            _textRender.Projection = Matrix4.CreateOrthographic(e.Width, e.Height, 0d, 1d);
        }

        private bool _ctrl = false;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e[Keys.LeftControl] || e[Keys.RightControl])
            {
                _ctrl = true;
                return;
            }

            if (e[Keys.Left])
            {
                _textIndex--;

                if (_textIndex < 0)
                {
                    _textIndex = 0;
                }
            }
            if (e[Keys.Right])
            {
                _textIndex++;

                if (_textIndex > _enterText.Length)
                {
                    _textIndex = _enterText.Length;
                }
            }

            if (e[Keys.BackSpace] && _enterText.Length > 0)
            {
                _textIndex--;
                if (_textIndex < 0)
                {
                    _textIndex = 0;
                }
                // Remove character at text index
                _enterText.Remove(_textIndex, 1);
                return;
            }
            if (e[Keys.Enter] || e[Keys.NumPadEnter])
            {
                string command = _enterText.ToString();

                VirtualConsole.Log("Console> " + command);
                VirtualConsole.EnterText(command);

                if (VirtualConsole.Output.Count > 0)
                {
                    VirtualConsole.NewLine();
                }

                _enterText.Clear();
                _textIndex = 0;
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e[Keys.LeftControl] || e[Keys.RightControl])
            {
                _ctrl = false;
                return;
            }
        }

        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);

            // Invalid character
            if (!e[' '] && !_fontC.GetCharacterData(e.Character).Supported) { return; }

            //_enterText.Append(e.Character);
            _enterText.Insert(_textIndex, e.Character);
            _textIndex++;
        }

        protected override void OnScroll(ScrollEventArgs e)
        {
            base.OnScroll(e);

            if (!_ctrl) { return; }

            _charSize += e.DeltaY;

            if (_charSize < 2) { _charSize = 2; }
            else if (_charSize > 100) { _charSize = 100; }
        }
    }
}