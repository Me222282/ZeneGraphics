﻿using System;
using System.Collections.Generic;

namespace Zene.Graphics
{
    public abstract class Font
    {
        public Font(double spaceWidth, double lineHeight)
        {
            SpaceWidth = spaceWidth;
            LineHeight = lineHeight;
        }

        public abstract void BindTexture(uint slot);

        public double SpaceWidth { get; }
        public double LineHeight { get; }

        public abstract CharFontData GetCharacterData(char character);

        public abstract string Name { get; }

        public virtual double CharSpace => 0d;
        public virtual double LineSpace => 0d;

        public double GetLineWidth(ReadOnlySpan<char> text, double charSpace, int tabSize) => GetLineWidth(text, charSpace, tabSize, 0, out _);
        public double GetLineWidth(ReadOnlySpan<char> text, double charSpace, int tabSize, int startIndex, out int newLineIndex)
        {
            newLineIndex = 0;

            // No text
            if (text.Length == 0){ return 0; }

            // Width of line - starts with all the line spaces between each charater
            double result = 0;

            for (int i = startIndex; i < text.Length; i++)
            {
                // No character - it is null
                if (text[i] == '\0') { continue; }

                if (text[i] == ' ')
                {
                    result += SpaceWidth + charSpace;
                    continue;
                }
                if (text[i] == '\t')
                {
                    result += (SpaceWidth * tabSize) + charSpace;
                    continue;
                }
                // End of line
                if (text[i] == '\n')
                {
                    newLineIndex = i;
                    break;
                }

                CharFontData charData = GetCharacterData(text[i]);

                // Add charater width
                result += charData.Size.X + charSpace;
            }

            // If there was no new line character found - newLineIndex doesn't exist
            if (newLineIndex == 0 && result != 0)
            {
                newLineIndex = text.Length;
            }
            // Remove extra line space at end
            return result - charSpace;
        }

        public List<double> GetLineWidths(ReadOnlySpan<char> text, double charSpace, int tabSize)
        {
            // No text
            if (text.Length == 0) { return new List<double>(); }

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
                    result[currentLine] += SpaceWidth + charSpace;
                    continue;
                }
                if (text[i] == '\t')
                {
                    result[currentLine] += (SpaceWidth * tabSize) + charSpace;
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
                result[currentLine] += charData.Size.X + charSpace;
            }

            return result;
        }
    }
}
