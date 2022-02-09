using System;
using System.Text.RegularExpressions;

namespace Zene.Graphics
{
    public class UnsupportedCharacterException : Exception
    {
        public UnsupportedCharacterException(string message)
            : base(message)
        {

        }

        public UnsupportedCharacterException(char charater)
            : base($"\'{Regex.Unescape(charater.ToString())}\' is not supported.")
        {

        }

        public UnsupportedCharacterException(char charater, Font font)
            : base($"\'{Regex.Unescape(charater.ToString())}\' is not supported by font {font.Name}.")
        {

        }
    }
}
