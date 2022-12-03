/*
using System;
using System.Collections.Generic;
using System.Text;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public class EditTextRenderer : BaseShaderProgram, IDisposable
    {
        private Matrix4 _m1 = Matrix4.Identity;
        public Matrix4 Model
        {
            get => _m1;
            set
            {
                _m1 = value;
                SetMatrices();
            }
        }
        private Matrix4 _m2 = Matrix4.Identity;
        public Matrix4 View
        {
            get => _m2;
            set
            {
                _m2 = value;
                SetMatrices();
            }
        }
        private Matrix4 _m3 = Matrix4.Identity;
        public Matrix4 Projection
        {
            get => _m3;
            set
            {
                _m3 = value;
                SetMatrices();
            }
        }
        private void SetMatrices()
        {
            Matrix4 matrix = _m1 * _m2 * _m3;
            SetUniformF(Uniforms[0], ref matrix);
        }

        private Colour _colour;
        public Colour Colour
        {
            get => _colour;
            set
            {
                _colour = value;

                SetUniformF(Uniforms[1], (Vector4)value);
            }
        }
        private Colour _colourSelectFor;
        public Colour SelectedColour
        {
            get => _colourSelectFor;
            set
            {
                _colourSelectFor = value;

                SetUniformF(Uniforms[2], (Vector4)value);
            }
        }
        private Colour _colourSelectBack;
        public Colour SelectedColourBack
        {
            get => _colourSelectBack;
            set
            {
                _colourSelectBack = value;

                SetUniformF(Uniforms[3], (Vector4)value);
            }
        }

        public EditTextRenderer(int capacity)
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
            _instanceData = new ArrayBuffer<Vector2>(5, BufferUsage.DrawRepeated);
            _instanceData.InitData(capacity * 5);

            // Add instance reference
            _drawable.Vao.AddBuffer(_instanceData, 2, 0, DataType.Double, AttributeSize.D2);
            _drawable.Vao.AddBuffer(_instanceData, 3, 1, DataType.Double, AttributeSize.D2);
            _drawable.Vao.AddBuffer(_instanceData, 4, 2, DataType.Double, AttributeSize.D2);
            _drawable.Vao.AddBuffer(_instanceData, 5, 3, DataType.Double, AttributeSize.D2);
            _drawable.Vao.AddBuffer(_instanceData, 6, 4, DataType.Double, AttributeSize.D2);
            // Set indexes as instance referances
            _drawable.Vao.Bind();
            GL.VertexAttribDivisor(2, 1);
            GL.VertexAttribDivisor(3, 1);
            GL.VertexAttribDivisor(4, 1);
            GL.VertexAttribDivisor(5, 1);
            GL.VertexAttribDivisor(6, 1);
            _drawable.Vao.Unbind();

            //
            // Shader setup
            //

            Create(ShaderPresets.TextEditVert, ShaderPresets.TextEditFrag,
                "matrix", "uColour", "uSelectColour", "uSelectTextColour", "uTextureSlot");

            // Set matrices in shader to default
            SetMatrices();
            // Set colours to default
            Colour = new Colour(255, 255, 255);
            SelectedColourBack = new Colour(255, 255, 255);
            SelectedColour = Colour.Zero;
            //GL.ProgramUniform4f(ShaderId, _uniformColour, 1f, 1f, 1f, 1f);
            //GL.ProgramUniform4f(ShaderId, _uniformColourSelectBack, 1f, 1f, 1f, 1f);
            //GL.ProgramUniform4f(ShaderId, _uniformColourSelectFor, 0f, 0f, 0f, 0f);
        }
        private readonly DrawObject<Vector2, byte> _drawable;
        private readonly ArrayBuffer<Vector2> _instanceData;

        private int _capacity;
        public int Capacity
        {
            get => _capacity;
            set
            {
                _capacity = value;

                _instanceData.InitData(_capacity * 5);
            }
        }

        public bool AutoIncreaseCapacity { get; set; } = false;

        public int TabSize { get; set; } = 4;

        public void DrawLeftBound(ReadOnlySpan<char> text, int selectStart, int selectEnd, Font font, double charSpace, double lineSpace)
        {
            if (font == null)
            {
                throw new ArgumentNullException(nameof(font));
            }
            // No text is to be drawn
            if (text == null || text == "") { return; }

            // Remove all whitespace and null values that are not selected
            string compText = TrimSpace(text, selectStart, selectEnd);

            if (compText.Length > _capacity && !AutoIncreaseCapacity)
            {
                throw new Exception($"{nameof(text)} has too many drawable characters. Must be less than, or equal to, {nameof(Capacity)} (Unselected white space doesn't count).");
            }
            else if (compText.Length > _capacity)
            {
                Capacity = compText.Length;
            }

            // The current character offset
            Vector2 offsetCurrent = Vector2.Zero;
            // The instance data containing offsets for each character
            Vector2[] data = new Vector2[compText.Length * 5];

            // Special for loop with two counters
            int i = 0;
            int count = 0;
            while (count < compText.Length)
            {
                // No character
                if (text[i] == '\0' || text[i] == '\a' || text[i] == '\u0027')
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
                    // All selected charaters are drawn
                    if (i >= selectStart && i <= selectEnd)
                    {
                        data[count * 5] = offsetCurrent;
                        data[(count * 5) + 1] = Vector2.Zero;
                        data[(count * 5) + 2] = Vector2.Zero;
                        data[(count * 5) + 3] = new Vector2(font.SpaceWidth);
                        data[(count * 5) + 4] = Vector2.One;
                        count++;
                    }
                    continue;
                }
                if (text[i] == '\t')
                {
                    offsetCurrent.X += (font.SpaceWidth * TabSize) + charSpace;
                    i++;
                    // All selected charaters are drawn
                    if (i >= selectStart && i <= selectEnd)
                    {
                        data[count * 5] = offsetCurrent;
                        data[(count * 5) + 1] = Vector2.Zero;
                        data[(count * 5) + 2] = Vector2.Zero;
                        data[(count * 5) + 3] = new Vector2(font.SpaceWidth * TabSize);
                        data[(count * 5) + 4] = Vector2.One;
                        count++;
                    }
                    continue;
                }
                // Character should be skipped - offsetCurrent adjusted for new line
                if (text[i] == '\n')
                {
                    offsetCurrent.Y -= font.LineHeight + lineSpace;
                    offsetCurrent.X = 0;
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
                        offsetCurrent.X = 0;
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
                data[count * 5] = offsetCurrent + charData.ExtraOffset;
                data[(count * 5) + 1] = charData.TextureCoordOffset;
                data[(count * 5) + 2] = charData.TextureRefSize;
                data[(count * 5) + 3] = charData.Size;
                // Character inside select range
                if (i >= selectStart && i <= selectEnd)
                {
                    data[(count * 5) + 4] = Vector2.One;
                }

                // Adjust offset for next character
                offsetCurrent.X += charData.Size.X + charSpace;
                // Continue counters
                count++;
                i++;
            }
            // Pass instance data to gpu
            _instanceData.EditData(0, data);

            //
            // Draw object
            //

            // Bind shader
            GL.UseProgram(this);

            // Set texture slot
            SetUniformI(Uniforms[4], 0);

            font.BindTexture(0);

            _drawable.DrawMultiple(compText.Length);
        }
        public void DrawLeftBound(ReadOnlySpan<char> text, int selectStart, int selectEnd, Font font) => DrawLeftBound(text, selectStart, selectEnd, font, 0, 0);

        public void DrawCentred(ReadOnlySpan<char> text, int selectStart, int selectEnd, Font font, double charSpace, double lineSpace)
        {
            if (font == null)
            {
                throw new ArgumentNullException(nameof(font));
            }
            // No text is to be drawn
            if (text == null || text == "") { return; }

            // Remove all whitespace and null values that are not selected
            string compText = TrimSpace(text, selectStart, selectEnd);

            if (compText.Length > _capacity && !AutoIncreaseCapacity)
            {
                throw new Exception($"{nameof(text)} has too many drawable characters. Must be less than, or equal to, {nameof(Capacity)} (Unselected white space doesn't count).");
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
            Vector2[] data = new Vector2[compText.Length * 5];

            // Special for loop with two counters
            int i = 0;
            int count = 0;
            // The current line in the text that is being calculated
            int lineCurrent = 0;
            while (count < compText.Length)
            {
                // No character
                if (text[i] == '\0' || text[i] == '\a' || text[i] == '\u0027')
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
                    // All selected charaters are drawn
                    if (i >= selectStart && i <= selectEnd)
                    {
                        data[count * 5] = offsetCurrent;
                        data[(count * 5) + 1] = Vector2.Zero;
                        data[(count * 5) + 2] = Vector2.Zero;
                        data[(count * 5) + 3] = new Vector2(font.SpaceWidth);
                        data[(count * 5) + 4] = Vector2.One;
                        count++;
                    }
                    continue;
                }
                if (text[i] == '\t')
                {
                    offsetCurrent.X += (font.SpaceWidth * TabSize) + charSpace;
                    i++;
                    // All selected charaters are drawn
                    if (i >= selectStart && i <= selectEnd)
                    {
                        data[count * 5] = offsetCurrent;
                        data[(count * 5) + 1] = Vector2.Zero;
                        data[(count * 5) + 2] = Vector2.Zero;
                        data[(count * 5) + 3] = new Vector2(font.SpaceWidth);
                        data[(count * 5) + 4] = Vector2.One;
                        count++;
                    }
                    continue;
                }
                // Character should be skipped - offsetCurrent adjusted for new line
                if (text[i] == '\n')
                {
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
                data[count * 5] = offsetCurrent + charData.ExtraOffset;
                data[(count * 5) + 1] = charData.TextureCoordOffset;
                data[(count * 5) + 2] = charData.TextureRefSize;
                data[(count * 5) + 3] = charData.Size;
                // Character inside select range
                if (i >= selectStart && i <= selectEnd)
                {
                    data[(count * 5) + 4] = Vector2.One;
                }

                // Adjust offset for next character
                offsetCurrent.X += charData.Size.X + charSpace;
                // Continue counters
                count++;
                i++;
            }
            // Pass instance data to gpu
            _instanceData.EditData(0, data);

            //
            // Draw object
            //

            // Bind shader
            GL.UseProgram(this);

            // Set texture slot
            SetUniformI(Uniforms[4], 0);

            font.BindTexture(0);

            _drawable.DrawMultiple(compText.Length);
        }
        public void DrawCentred(ReadOnlySpan<char> text, int selectStart, int selectEnd, Font font) => DrawCentred(text, selectStart, selectEnd, font, 0, 0);

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            if (dispose)
            {
                _drawable.Dispose();
                _instanceData.Dispose();
            }
        }

        private static string TrimSpace(ReadOnlySpan<char> text, int selectStart, int selectEnd)
        {
            char[] data = new char[text.Length];

            int dataIndex = 0;
            for (int i = 0; i < text.Length; i++)
            {
                // Charater is never drawn
                if (text[i] == '\0' || text[i] == '\n' || text[i] == '\r' || text[i] == '\a' || text[i] == '\u0027') { continue; }

                // Character inside select range
                if (i >= selectStart && i <= selectEnd)
                {
                    if (text[i] == '\t')
                    {
                        data[dataIndex] = '\t';
                        dataIndex++;
                        continue;
                    }
                    if (text[i] == ' ')
                    {
                        data[dataIndex] = ' ';
                        dataIndex++;
                        continue;
                    }
                }
                // Unselected white space is removed
                else
                {
                    if (text[i] == '\t' || text[i] == ' ') { continue; }
                }

                data[dataIndex] = text[i];
                dataIndex++;
            }

            return new string(data, 0, dataIndex);
        }
    }
}
*/