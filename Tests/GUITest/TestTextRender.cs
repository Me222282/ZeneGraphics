using System;
using System.Collections.Generic;
using System.IO;
using Zene.Graphics;
using Zene.Graphics.Base;
using Zene.Graphics.Shaders;
using Zene.Structs;

namespace GUITest
{
    public class TestTextRender : IShaderProgram
    {
        public uint ShaderId { get; private set; }
        uint IIdentifiable.Id => ShaderId;

        private int _uniformMatrix;
        private Matrix4 _m1 = Matrix4.Identity;
        public Matrix4 Model
        {
            get
            {
                return _m1;
            }
            set
            {
                _m1 = value;
                SetMatrices();
            }
        }
        private Matrix4 _m2 = Matrix4.Identity;
        public Matrix4 View
        {
            get
            {
                return _m2;
            }
            set
            {
                _m2 = value;
                SetMatrices();
            }
        }
        private Matrix4 _m3 = Matrix4.Identity;
        public Matrix4 Projection
        {
            get
            {
                return _m3;
            }
            set
            {
                _m3 = value;
                SetMatrices();
            }
        }
        private void SetMatrices()
        {
            GL.ProgramUniformMatrix4fv(ShaderId, _uniformMatrix, false, (_m1 * _m2 * _m3).GetGLData());
        }

        private int _uniformColour;
        private Colour _colour;
        public Colour Colour
        {
            get
            {
                return _colour;
            }
            set
            {
                _colour = value;
                ColourF cf = value;
                GL.ProgramUniform4f(ShaderId, _uniformColour, cf.R, cf.G, cf.B, cf.A);
            }
        }

        private int _uniformTexSlot;

        void IBindable.Bind()
        {
            GL.UseProgram(ShaderId);
        }
        void IBindable.Unbind()
        {
            GL.UseProgram(0);
        }

        private const int _blockSize = 4;

        public TestTextRender(int capacity)
        {
            _capacity = capacity;

            // Create drawable object
            _drawable = new DrawObject<Vector2, byte>(new Vector2[]
            {
                new Vector2(0, 0), new Vector2(0, 1),
                new Vector2(0, -1), new Vector2(0, 0),
                new Vector2(1, -1), new Vector2(1, 0),
                new Vector2(1, 0), new Vector2(1, 1)
            }, new byte[] { 0, 1, 2, 2, 3, 0 }, 2, 0, AttributeSize.D2, BufferUsage.DrawFrequent);
            _drawable.AddAttribute(1, 1, AttributeSize.D2); // Texture Coordinates

            // Setup instance offsets ready for drawing
            _instanceData = new ArrayBuffer<Vector2>(new Vector2[capacity * _blockSize], _blockSize, BufferUsage.DrawRepeated);

            _drawable.Vao.Bind();
            // Add instance reference
            _drawable.Vao.AddBuffer(_instanceData, 2, 0, DataType.Double, AttributeSize.D2);
            _drawable.Vao.AddBuffer(_instanceData, 3, 1, DataType.Double, AttributeSize.D2);
            _drawable.Vao.AddBuffer(_instanceData, 4, 2, DataType.Double, AttributeSize.D2);
            _drawable.Vao.AddBuffer(_instanceData, 5, 3, DataType.Double, AttributeSize.D2);
            // Colour
            //_drawable.Vao.AddBuffer(_instanceData, 6, 4, DataType.Double, AttributeSize.D4);
            // Set indexes as instance referances
            GL.VertexAttribDivisor(2, 1);
            GL.VertexAttribDivisor(3, 1);
            GL.VertexAttribDivisor(4, 1);
            GL.VertexAttribDivisor(5, 1);
            //GL.VertexAttribDivisor(6, 1);

            _drawable.Vao.Unbind();

            //
            // Shader
            //

            Reload();
        }
        private readonly DrawObject<Vector2, byte> _drawable;
        private ArrayBuffer<Vector2> _instanceData;

        private int _capacity;
        public int Capacity
        {
            get
            {
                return _capacity;
            }
            set
            {
                _capacity = value;

                _instanceData = new ArrayBuffer<Vector2>(new Vector2[_capacity * _blockSize], _blockSize, BufferUsage.DrawRepeated);

                _drawable.Vao.Bind();
                // Add instance reference
                _drawable.Vao.AddBuffer(_instanceData, 2, 0, DataType.Double, AttributeSize.D2);
                _drawable.Vao.AddBuffer(_instanceData, 3, 1, DataType.Double, AttributeSize.D2);
                _drawable.Vao.AddBuffer(_instanceData, 4, 2, DataType.Double, AttributeSize.D2);
                _drawable.Vao.AddBuffer(_instanceData, 5, 3, DataType.Double, AttributeSize.D2);

                _drawable.Vao.Unbind();
            }
        }

        public bool AutoIncreaseCapacity { get; set; } = false;
        public int TabSize { get; set; } = 4;

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            _drawable.Dispose();
            _instanceData.Dispose();
            GL.DeleteProgram(ShaderId);

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        public void DrawCentred(ReadOnlySpan<char> text, Font font, double charSpace, double lineSpace)
        {
            if (font == null)
            {
                throw new ArgumentNullException(nameof(font));
            }
            // No text is to be drawn
            if (text == null || text == "") { return; }

            // Remove all whitespace and null values
            string compText = new string(text).Replace(" ", "");
            compText = compText.Replace("\n", "");
            compText = compText.Replace("\r", "");
            compText = compText.Replace("\t", "");
            compText = compText.Replace("\0", "");
            compText = compText.Replace("\a", "");

            // No visable characters are drawn
            if (compText == "") { return; }

            if (compText.Length > _capacity && !AutoIncreaseCapacity)
            {
                throw new Exception($"{nameof(text)} has too many drawable characters. Must be less than, or equal to, {nameof(Capacity)} (White space doesn't count).");
            }
            else if (compText.Length > _capacity)
            {
                Capacity = compText.Length;
            }

            // The widths of each line in text
            List<double> lineWidths = font.GetLineWidths(text, charSpace, TabSize);

            // The current character offset
            Vector2 offsetCurrent = new Vector2(
                lineWidths[0] * -0.5,
                // The offset for Y
                (
                    (font.LineHeight * lineWidths.Count) +
                    ((lineWidths.Count - 1) * lineSpace)
                ) * 0.5);
            // The instance data containing offsets for each character
            Vector2[] data = new Vector2[compText.Length * _blockSize];

            // Special for loop with two counters
            int i = 0;
            int count = 0;
            // The current line in the text that is being calculated
            int lineCurrent = 0;
            while (count < compText.Length)
            {
                // No character
                if (text[i] == '\0' || text[i] == '\a')
                {
                    i++;
                    // Index in compressed text shouldn't be changed - it has no null characters
                    continue;
                }
                // Character should be skipped to add space
                if (text[i] == ' ')
                {
                    offsetCurrent.X += font.SpaceWidth + charSpace;
                    i++;
                    // Index in compressed text shouldn't be changed - it has no white space
                    continue;
                }
                if (text[i] == '\t')
                {
                    offsetCurrent.X += (font.SpaceWidth * TabSize) + charSpace;
                    i++;
                    // Index in compressed text shouldn't be changed - it has no white space
                    continue;
                }
                // Character should be skipped - offsetCurrent adjusted for new line
                if (text[i] == '\n')
                {
                    // Sometimes there is both
                    if (text.Length > (i + 1) && text[i + 1] == '\r')
                    {
                        i++;
                        continue;
                    }

                    offsetCurrent.Y -= font.LineHeight + lineSpace;
                    lineCurrent++;
                    offsetCurrent.X = lineWidths[lineCurrent] * -0.5;
                    i++;
                    // Index in compressed text shouldn't be changed - it has no white space
                    continue;
                }
                // New lines for some operating systems
                if (text[i] == '\r')
                {
                    // Sometimes there is both
                    if (text.Length > (i + 1) && text[i + 1] != '\n')
                    {
                        offsetCurrent.Y -= font.LineHeight + lineSpace;
                        lineCurrent++;
                        offsetCurrent.X = lineWidths[lineCurrent] * -0.5;
                    }

                    i++;
                    // Index in compressed text shouldn't be changed - it has no white space
                    continue;
                }
                CharFontData charData = font.GetCharacterData(text[i]);

                if (!charData.Supported)
                {
                    throw new UnsupportedCharacterException(text[i], font);
                }

                // Set drawing offset data
                data[count * _blockSize] = offsetCurrent + charData.ExtraOffset;
                data[(count * _blockSize) + 1] = charData.TextureCoordOffset;
                data[(count * _blockSize) + 2] = charData.TextureRefSize;
                data[(count * _blockSize) + 3] = charData.Size;

                // Adjust offset for next character
                offsetCurrent.X += charData.Size.X + charData.Buffer + charSpace;
                // Continue counters
                count++;
                i++;
            }
            // Pass instance data to gpu
            _instanceData.SetData(data);

            //
            // Draw object
            //

            // Bind shader
            GL.UseProgram(ShaderId);

            // Set texture slot
            GL.ProgramUniform1i(ShaderId, _uniformTexSlot, 0);
            //GL.ProgramUniform1i(ShaderId, _uniformColourSource, 1);

            font.BindTexture(0);

            _drawable.DrawMultiple(compText.Length);
        }

        public void Reload()
        {
            ShaderId = CustomShader.CreateShader(ShaderPresets.TextVert, File.ReadAllText("resources/textfrag.shader"));

            // Fetch uniform locations
            _uniformMatrix = GL.GetUniformLocation(ShaderId, "matrix");
            _uniformColour = GL.GetUniformLocation(ShaderId, "uColour");
            _uniformTexSlot = GL.GetUniformLocation(ShaderId, "uTextureSlot");

            // Set matrices in shader to default
            SetMatrices();
            // Set colour to default
            GL.ProgramUniform4f(ShaderId, _uniformColour, 1f, 1f, 1f, 1f);
        }
    }
}
