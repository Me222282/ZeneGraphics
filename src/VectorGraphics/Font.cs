using System;
using System.Collections.Generic;
using Zene.Structs;

namespace Zene.Graphics
{
    public abstract class Font
    {
        public Font(int spaceWidth, int lineHeight, int baseLineSpace = 0)
        {
            SpaceWidth = spaceWidth;
            LineHeight = lineHeight;
            BaseLineSpace = baseLineSpace;

            Name = GetType().Name;
        }

        public abstract ITexture SourceTexture { get; }

        public int SpaceWidth { get; }
        public int LineHeight { get; }
        public int BaseLineSpace { get; }

        public abstract CharFontData GetCharacterData(char character);

        public virtual string Name { get; }

        public Vector2I GetFrameSize(ReadOnlySpan<char> text, int charSpace, int lineSpace, int tabSize)
        {
            // No text
            if (text.Length == 0) { return 0; }

            int maxWidth = 0;
            int maxHeight = LineHeight;

            int currentWidth = 0;
            int extraHeight = 0;

            for (int i = 0; i < text.Length; i++)
            {
                // No character - it is null
                if (text[i] == '\0') { continue; }

                if (text[i] == ' ')
                {
                    currentWidth += SpaceWidth + charSpace;
                    continue;
                }
                if (text[i] == '\t')
                {
                    currentWidth += (SpaceWidth * tabSize) + charSpace;
                    continue;
                }
                // End of line
                if (text[i] == '\n')
                {
                    // Sometimes there is both
                    if (text.Length > (i + 1) && text[i + 1] == '\r')
                    {
                        continue;
                    }

                    if (maxWidth < currentWidth)
                    {
                        maxWidth = currentWidth;
                    }
                    currentWidth = 0;
                    extraHeight = 0;
                    maxHeight += LineHeight + lineSpace;
                    continue;
                }
                // New lines for some operating systems
                if (text[i] == '\r')
                {
                    // Sometimes there is both
                    if (text.Length > (i + 1) && text[i + 1] != '\n')
                    {
                        if (maxWidth < currentWidth)
                        {
                            maxWidth = currentWidth;
                        }
                        currentWidth = 0;
                        extraHeight = 0;
                        maxHeight += LineHeight + lineSpace;
                    }
                    continue;
                }

                CharFontData charData = GetCharacterData(text[i]);

                // Add charater width
                currentWidth += charData.Size.X + charData.Buffer + charSpace;

                int height = -charData.ExtraOffset.Y - BaseLineSpace;
                if (extraHeight < height)
                {
                    extraHeight = height;
                }
            }

            if (maxWidth < currentWidth)
            {
                maxWidth = currentWidth;
            }

            return (maxWidth, maxHeight + extraHeight + BaseLineSpace);
        }

        internal List<double> GetLineWidths(ReadOnlySpan<char> text, double charSpace, int tabSize, double multiplier)
        {
            // No text
            if (text.Length == 0) { return new List<double>(); }

            double sw = SpaceWidth * multiplier;

            // Width of line - starts with all the line spaces between each charater
            List<double> result = new List<double>()
            {
                -charSpace
            };

            // The current index in the result to reference
            int currentLine = 0;
            for (int i = 0; i < text.Length; i++)
            {
                // No character - it is null
                if (text[i] == '\0') { continue; }

                if (text[i] == ' ')
                {
                    result[currentLine] += sw + charSpace;
                    continue;
                }
                if (text[i] == '\t')
                {
                    result[currentLine] += (sw * tabSize) + charSpace;
                    continue;
                }
                // End of line
                if (text[i] == '\n')
                {
                    currentLine++;
                    result.Add(-charSpace);
                    continue;
                }

                CharFontData charData = GetCharacterData(text[i]);

                // Add charater width
                result[currentLine] += ((charData.Size.X + charData.Buffer) * multiplier) + charSpace;
            }

            return result;
        }
    }
}
