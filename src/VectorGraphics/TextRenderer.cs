using System;
using System.Collections.Generic;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public class TextRenderer : BaseShaderProgram, IDisposable
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

        private const int _blockSize = 4;

        public TextRenderer(int capacity)
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
            _drawable.AddAttribute(1, 1, AttributeSize.D2); // Texture Coordinates*

            // Setup instance offsets ready for drawing
            _instanceData = new ArrayBuffer<Vector2>(_blockSize, BufferUsage.DrawRepeated);
            _instanceData.InitData(capacity * _blockSize);

            // Add instance reference
            _drawable.Vao.AddInstanceBuffer(_instanceData, 2, 0, DataType.Double, AttributeSize.D2, 1);
            _drawable.Vao.AddInstanceBuffer(_instanceData, 3, 1, DataType.Double, AttributeSize.D2, 1);
            _drawable.Vao.AddInstanceBuffer(_instanceData, 4, 2, DataType.Double, AttributeSize.D2, 1);
            _drawable.Vao.AddInstanceBuffer(_instanceData, 5, 3, DataType.Double, AttributeSize.D2, 1);
            // Colour
            //_drawable.Vao.AddInstanceBuffer(_instanceData, 6, 4, DataType.Double, AttributeSize.D4, 1);

            //
            // Shader setup
            //

            Create(ShaderPresets.TextVert, ShaderPresets.TextFrag,
                "matrix", "uColour", "uTextureSlot");

            // Set matrices in shader to default
            SetMatrices();
            // Set colour to default
            Colour = new Colour(255, 255, 255);
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

                _instanceData.InitData(_capacity * _blockSize);
                //_instanceData.SetData(new Vector2[_capacity * _blockSize]);
                /*
                _drawable.Vao.Bind();
                // Add instance reference
                _drawable.Vao.AddBuffer(_instanceData, 2, 0, DataType.Double, AttributeSize.D2);
                _drawable.Vao.AddBuffer(_instanceData, 3, 1, DataType.Double, AttributeSize.D2);
                _drawable.Vao.AddBuffer(_instanceData, 4, 2, DataType.Double, AttributeSize.D2);
                _drawable.Vao.AddBuffer(_instanceData, 5, 3, DataType.Double, AttributeSize.D2);

                _drawable.Vao.Unbind();*/
            }
        }

        public bool AutoIncreaseCapacity { get; set; } = false;

        public int TabSize { get; set; } = 4;

        public void DrawLeftBound(ReadOnlySpan<char> text, Font font, double charSpace, double lineSpace)
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

            // The current character offset
            Vector2 offsetCurrent = Vector2.Zero;
            // The instance data containing offsets for each character
            Vector2[] data = new Vector2[compText.Length * _blockSize];

            // Special for loop with two counters
            int i = 0;
            int count = 0;
            while (count < compText.Length)
            {
                // No character - it is null
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
            _instanceData.EditData(0, data);

            //
            // Draw object
            //

            // Bind shader
            GL.UseProgram(this);

            // Set texture slot
            SetUniformI(Uniforms[2], 0);
            //GL.ProgramUniform1i(ShaderId, _uniformColourSource, 1);

            font.BindTexture(0);

            _drawable.DrawMultiple(compText.Length);
        }
        public void DrawLeftBound(ReadOnlySpan<char> text, Font font) => DrawLeftBound(text, font, 0d, 0d);

        /*
        public void DrawLeftBound(ReadOnlySpan<char> text, ReadOnlySpan<Colour> colours, Font font, double charSpace, double lineSpace)
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
            
            if (compText.Length != colours.Length)
            {
                throw new ArgumentException($"There must be a colour in {nameof(colours)} for every drawable charater in {nameof(text)}.", nameof(colours));
            }
            
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

            // The current character offset
            Vector2 offsetCurrent = Vector2.Zero;
            // The instance data containing offsets for each character
            Vector2[] data = new Vector2[compText.Length * _blockSize];

            // Special for loop with two counters
            int i = 0;
            int count = 0;
            while (count < compText.Length)
            {
                // No character - it is null
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
                data[count * _blockSize] = offsetCurrent + charData.ExtraOffset;
                data[(count * _blockSize) + 1] = charData.TextureCoordOffset;
                data[(count * _blockSize) + 2] = charData.TextureRefSize;
                data[(count * _blockSize) + 3] = charData.Size;
                // Colour
                ColourF cf = colours[count];
                data[(count * _blockSize) + 4] = new Vector2(cf.R, cf.G);
                data[(count * _blockSize) + 5] = new Vector2(cf.B, cf.A);

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
            GL.ProgramUniform1i(ShaderId, _uniformColourSource, 2);

            font.BindTexture(0);

            _drawable.DrawMultiple(compText.Length);
        }
        public void DrawCentred(ReadOnlySpan<char> text, ReadOnlySpan<Colour> colours, Font font, double charSpace, double lineSpace)
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
            
            if (compText.Length != colours.Length)
            {
                throw new ArgumentException($"There must be a colour in {nameof(colours)} for every drawable charater in {nameof(text)}.", nameof(colours));
            }
            
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
                // Colour
                ColourF cf = colours[count];
                data[(count * _blockSize) + 4] = new Vector2(cf.R, cf.G);
                data[(count * _blockSize) + 5] = new Vector2(cf.B, cf.A);

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
            GL.ProgramUniform1i(ShaderId, _uniformColourSource, 2);

            font.BindTexture(0);

            _drawable.DrawMultiple(compText.Length);
        }
        */

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
            _instanceData.EditData(0, data);

            //
            // Draw object
            //

            // Bind shader
            GL.UseProgram(this);

            // Set texture slot
            SetUniformI(Uniforms[2], 0);
            //GL.ProgramUniform1i(ShaderId, _uniformColourSource, 1);

            font.BindTexture(0);

            _drawable.DrawMultiple(compText.Length);
        }
        public void DrawCentred(ReadOnlySpan<char> text, Font font) => DrawCentred(text, font, 0d, 0d);

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            if (dispose)
            {
                _drawable.Dispose();
                _instanceData.Dispose();
            }
        }
    }
}