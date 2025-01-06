using System;
using System.Collections.Generic;
using Zene.Structs;

namespace Zene.Graphics
{
    public class TextRenderer : IDisposable
    {
        private class MShader : BaseShaderProgram
        {
            public MShader()
            {
                Create(ShaderPresets.TextVert, ShaderPresets.TextFrag, 0,
                    "matrix", "uColour", "fontSampler");

                // Set matrices in shader to default
                SetUniform(Uniforms[0], Matrix4.Identity);

                SetUniform(Uniforms[2], 0);
            }

            public void SetMatrix(IMatrix m, IMatrix v, IMatrix p)
            {
                Matrix4 m1;
                Matrix4 m2;
                Matrix4 m3;
                
                if (m is Matrix4 mm) { m1 = mm; }
                else { m1 = new Matrix4(m); }
                
                if (v is Matrix4 vv) { m2 = vv; }
                else { m2 = new Matrix4(v); }
                
                if (p is Matrix4 pp) { m3 = pp; }
                else { m3 = new Matrix4(p); }
                
                SetUniform(Uniforms[0], m1 * m2 * m3);
            }

            public ColourF Colour
            {
                set
                {
                    SetUniform(Uniforms[1], (Vector4)value);
                }
            }
        }

        private ColourF _colour;
        public ColourF Colour
        {
            get => _colour;
            set
            {
                _colour = value;
                _mShader.Colour = value;
            }
        }

        private readonly DrawObject<Vector2, byte> _drawable;

        public TextRenderer()
        {
            // Create drawable object
            _drawable = new DrawObject<Vector2, byte>(new Vector2[]
            {
                new Vector2(-0.5f, 0.5f), new Vector2(0, 0), new Vector2(0, 1),
                new Vector2(-0.5f, -0.5f), new Vector2(0, -1), new Vector2(0, 0),
                new Vector2(0.5f, -0.5f), new Vector2(1, -1), new Vector2(1, 0),
                new Vector2(0.5f, 0.5f), new Vector2(1, 0), new Vector2(1, 1)
            }, new byte[] { 0, 1, 2, 2, 3, 0 }, 3, 0, AttributeSize.D2, BufferUsage.DrawFrequent);
            _drawable.AddAttribute(1, 1, AttributeSize.D2); // M2 position
            _drawable.AddAttribute(ShaderLocation.TextureCoords, 2, AttributeSize.D2); // Texture Coordinates

            //
            // M
            //

            _mCapacity = 16;

            // Setup instance offsets ready for drawing
            _instanceData = new ArrayBuffer<Vector2>(_blockSize, BufferUsage.DrawRepeated);
            _instanceData.InitData(_mCapacity * _blockSize);

            // Add instance reference
            _drawable.AddInstanceBuffer(_instanceData, 3, 0, DataType.FloatV, AttributeSize.D2, 1);
            _drawable.AddInstanceBuffer(_instanceData, 4, 1, DataType.FloatV, AttributeSize.D2, 1);
            _drawable.AddInstanceBuffer(_instanceData, 5, 2, DataType.FloatV, AttributeSize.D2, 1);
            _drawable.AddInstanceBuffer(_instanceData, 6, 3, DataType.FloatV, AttributeSize.D2, 1);
            // Set indexes as instance referances

            _mShader = new MShader();

            // Default colour
            Colour = new Colour(255, 255, 255);
        }

        private const int _blockSize = 4;
        private int _mCapacity;
        private readonly MShader _mShader;
        private readonly ArrayBuffer<Vector2> _instanceData;

        public int TabSize { get; set; } = 4;

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            _disposed = true;

            _drawable.Dispose();

            _mShader.Dispose();
            _instanceData.Dispose();

            GC.SuppressFinalize(this);
        }

        private static T Clamp<T>(ReadOnlySpan<T> a, int index)
        {
            if (a.Length <= index ||
                index < 0)
            {
                return default;
            }

            return a[index];
        }

        private void SetCap(int v)
        {
            _mCapacity = v;

            _instanceData.InitData(_mCapacity * _blockSize);
        }

        public void DrawCentred(IDrawingContext dc, ReadOnlySpan<char> text, Font font, int charSpace, int lineSpace)
        {
            if (font == null)
            {
                throw new ArgumentNullException(nameof(font));
            }
            // No text is to be drawn
            if (text == null || text == "") { return; }

            if (text.Length > _mCapacity)
            {
                SetCap(text.Length);
            }

            // Pixel space to normalised space
            Vector2 textureMultiplier = 1 /
                // Texture Size
                (Vector2)(font.SourceTexture.Properties.Width, font.SourceTexture.Properties.Height);
            floatv sizeMultiplier = 1 / font.LineHeight;

            floatv lineSpaceD = lineSpace * sizeMultiplier;
            floatv cs = charSpace * sizeMultiplier;
            floatv sw = font.SpaceWidth * sizeMultiplier;

            // The widths of each line in text
            List<floatv> lineWidths = font.GetLineWidths(text, cs, TabSize, sizeMultiplier);

            // The current character offset
            Vector2 offsetCurrent = new Vector2(
                lineWidths[0] * -0.5,
                // The offset for Y
                (
                    lineWidths.Count +
                    ((lineWidths.Count - 1) * lineSpaceD)
                ) * 0.5);
            // The instance data containing offsets for each character
            Vector2[] data = new Vector2[text.Length * _blockSize];

            // Special for loop with two counters
            int i = 0;
            int count = 0;
            char previous;
            char current = '\0';
            // The current line in the text that is being calculated
            int lineCurrent = 0;
            while (i < text.Length)
            {
                previous = current;
                current = text[i];

                // No character
                if (current == '\0' || current == '\a')
                {
                    i++;
                    // Index in compressed text shouldn't be changed - it has no null characters
                    continue;
                }
                // Character should be skipped to add space
                if (current == ' ')
                {
                    offsetCurrent.X += sw + cs;
                    i++;
                    // Index in compressed text shouldn't be changed - it has no white space
                    continue;
                }
                if (current == '\t')
                {
                    offsetCurrent.X += (sw * TabSize) + cs;
                    i++;
                    // Index in compressed text shouldn't be changed - it has no white space
                    continue;
                }
                // Character should be skipped - offsetCurrent adjusted for new line
                if (current == '\n')
                {
                    // Sometimes there is both
                    if (previous == '\r')
                    {
                        i++;
                        continue;
                    }

                    offsetCurrent.Y -= 1 + lineSpaceD;
                    lineCurrent++;
                    offsetCurrent.X = lineWidths[lineCurrent] * -0.5f;
                    i++;
                    // Index in compressed text shouldn't be changed - it has no white space
                    continue;
                }
                // New lines for some operating systems
                if (current == '\r')
                {
                    // Sometimes there is both
                    if (previous != '\n')
                    {
                        offsetCurrent.Y -= 1 + lineSpaceD;
                        lineCurrent++;
                        offsetCurrent.X = lineWidths[lineCurrent] * -0.5f;
                    }

                    i++;
                    // Index in compressed text shouldn't be changed - it has no white space
                    continue;
                }
                CharFontData charData = font.GetCharacterData(current, previous, Clamp(text, i + 1));

                if (!charData.Supported)
                {
                    throw new UnsupportedCharacterException(current, font);
                }

                Vector2 size = charData.Size * sizeMultiplier;

                // Set drawing offset data
                data[count * _blockSize] = offsetCurrent + (charData.ExtraOffset * sizeMultiplier);
                data[count * _blockSize].Y -= 1 - size.Y;
                data[(count * _blockSize) + 1] = charData.TexturePosision * textureMultiplier;
                data[(count * _blockSize) + 2] = charData.Size * textureMultiplier;
                data[(count * _blockSize) + 3] = size;

                // Adjust offset for next character
                offsetCurrent.X += size.X + (charData.Buffer * sizeMultiplier) + cs;
                // Continue counters
                count++;
                i++;
            }
            // nothing to draw
            if (count == 0) { return; }

            // Pass instance data to gpu
            _instanceData.EditData(0, data.AsSpan().Slice(0, count * _blockSize));

            //
            // Draw object
            //

            // Bind shader
            dc.Shader = _mShader;
            _mShader.SetMatrix(dc.Model, dc.View, dc.Projection);

            // Set texture slot - already 0
            //SetUniformI(Uniforms[2], 0);
            font.SourceTexture.Bind(0);

            dc.Draw(_drawable, count);
        }
        public void DrawLeftBound(IDrawingContext dc, ReadOnlySpan<char> text, Font font, int charSpace, int lineSpace, int caretIndex, bool drawCaret, bool centred = true)
        {
            if (font == null)
            {
                throw new ArgumentNullException(nameof(font));
            }
            // No text is to be drawn
            if (text == null || text == "") { return; }

            if (text.Length > _mCapacity)
            {
                SetCap(text.Length);
            }

            // Pixel space to normalised space
            Vector2 textureMultiplier = 1 /
                // Texture Size
                (Vector2)(font.SourceTexture.Properties.Width, font.SourceTexture.Properties.Height);
            floatv sizeMultiplier = 1 / font.LineHeight;

            floatv lineSpaceD = lineSpace * sizeMultiplier;
            floatv cs = charSpace * sizeMultiplier;
            floatv sw = font.SpaceWidth * sizeMultiplier;

            // Get bounding box of text
            Vector2 frameSize = font.GetFrameSize(text, charSpace, lineSpace, TabSize) * sizeMultiplier;

            int dl = text.Length;
            if (drawCaret) { dl++; }
            else { caretIndex = -1; }
            if (caretIndex > text.Length)
            {
                caretIndex = text.Length;
            }

            Vector2 starting = centred ? frameSize / (-2, 2) : 0;
            // The current character offset
            Vector2 offsetCurrent = starting;
            // The instance data containing offsets for each character
            Vector2[] data = new Vector2[dl * _blockSize];

            // Special for loop with two counters
            int i = 0;
            int count = 0;
            char previous;
            char current = '\0';
            int end = Math.Max(text.Length, caretIndex + 1);
            while (i < end)
            {
                // insert caret
                if (i == caretIndex)
                {
                    CharFontData caret = font.GetCharacterData('|');

                    if (!caret.Supported)
                    {
                        throw new UnsupportedCharacterException('|', font);
                    }

                    Vector2 sizeC = caret.Size * sizeMultiplier;

                    // Set drawing offset data
                    Vector2 cPos = offsetCurrent + (caret.ExtraOffset * sizeMultiplier);
                    cPos.Y -= 1 - sizeC.Y;
                    cPos.X -= (sizeC.X + cs) / 2;
                    data[count * _blockSize] = cPos;
                    data[(count * _blockSize) + 1] = caret.TexturePosision * textureMultiplier;
                    data[(count * _blockSize) + 2] = caret.Size * textureMultiplier;
                    data[(count * _blockSize) + 3] = sizeC;
                    count++;

                    // caret is at end
                    if (caretIndex == text.Length) { break; }
                }

                previous = current;
                current = text[i];

                // No character
                if (current == '\0' || current == '\a')
                {
                    i++;
                    // Index in compressed text shouldn't be changed - it has no null characters
                    continue;
                }
                // Character should be skipped to add space
                if (current == ' ')
                {
                    offsetCurrent.X += sw + cs;
                    i++;
                    // Index in compressed text shouldn't be changed - it has no white space
                    continue;
                }
                if (current == '\t')
                {
                    offsetCurrent.X += (sw * TabSize) + cs;
                    i++;
                    // Index in compressed text shouldn't be changed - it has no white space
                    continue;
                }
                // Character should be skipped - offsetCurrent adjusted for new line
                if (current == '\n')
                {
                    // Sometimes there is both
                    if (previous == '\r')
                    {
                        i++;
                        continue;
                    }

                    offsetCurrent.Y -= 1 + lineSpaceD;
                    offsetCurrent.X = starting.X;
                    i++;
                    // Index in compressed text shouldn't be changed - it has no white space
                    continue;
                }
                // New lines for some operating systems
                if (current == '\r')
                {
                    // Sometimes there is both
                    if (previous != '\n')
                    {
                        offsetCurrent.Y -= 1 + lineSpaceD;
                        offsetCurrent.X = starting.X;
                    }

                    i++;
                    // Index in compressed text shouldn't be changed - it has no white space
                    continue;
                }
                CharFontData charData = font.GetCharacterData(current, previous, Clamp(text, i + 1));

                if (!charData.Supported)
                {
                    throw new UnsupportedCharacterException(current, font);
                }

                Vector2 size = charData.Size * sizeMultiplier;

                // Set drawing offset data
                Vector2 pos = offsetCurrent + (charData.ExtraOffset * sizeMultiplier);
                pos.Y -= 1 - size.Y;
                data[count * _blockSize] = pos;
                data[(count * _blockSize) + 1] = charData.TexturePosision * textureMultiplier;
                data[(count * _blockSize) + 2] = charData.Size * textureMultiplier;
                data[(count * _blockSize) + 3] = size;

                // Adjust offset for next character
                offsetCurrent.X += size.X + (charData.Buffer * sizeMultiplier) + cs;
                // Continue counters
                count++;
                i++;
            }
            // nothing to draw
            if (count == 0) { return; }

            // Pass instance data to gpu
            _instanceData.EditData(0, data.AsSpan().Slice(0, count * _blockSize));

            //
            // Draw object
            //

            // Bind shader
            dc.Shader = _mShader;
            _mShader.SetMatrix(dc.Model, dc.View, dc.Projection);

            // Set texture slot - already 0
            //SetUniformI(Uniforms[2], 0);
            font.SourceTexture.Bind(0);

            dc.Draw(_drawable, count);
        }
        public void DrawLeftBound(IDrawingContext dc, ReadOnlySpan<char> text, Font font, int charSpace, int lineSpace, bool centred = true)
            => DrawLeftBound(dc, text, font, charSpace, lineSpace, -1, false, centred);
    }
}