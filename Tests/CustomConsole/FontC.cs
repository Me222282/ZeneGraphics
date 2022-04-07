using System;
using Zene.Graphics;
using Zene.Structs;

namespace CustomConsole
{
    public class FontC : Font
    {
        private const double _pixelHeight = 1.0 / 229;
        private const double _pixelWidth = 1.0 / 649;

        public FontC()
            : base((1d / 42) * 24, 1d)
        {
            // Load font image
            byte[] byteData = Bitmap.ExtractData("resources/fontC.png", out int w, out int h);
            // Convert to one channel GLArray
            GLArray<Vector4<byte>> texData = new GLArray<Vector4<byte>>(w, h);
            for (int i = 0; i < texData.Size; i++)
            {
                texData[i] = new Vector4<byte>(byteData[i * 4], 0, 0, 0);
            }
            // Create and setup texture
            _texture = new Texture2D(TextureFormat.R8, TextureData.Byte)
            {
                WrapStyle = WrapStyle.EdgeClamp,
                MinFilter = TextureSampling.Nearest,
                MagFilter = TextureSampling.Nearest
            };
            // Asign data
            _texture.SetData(w, h, BaseFormat.Rgba, texData);
        }

        public override string Name => "FontC";

        private readonly Texture2D _texture;

        public override void BindTexture(uint slot) => _texture.Bind(slot);

        private static readonly Vector2 _texSize = new Vector2(_pixelWidth * 24, _pixelHeight * 42);
        private static readonly Vector2 _charSize = new Vector2((1d / 42) * 24, 1d);
        private static readonly Vector2 _offset = new Vector2(0d, (1d / 42) * -5);

        private static readonly CharFontData[] _characterData = new CharFontData[]
        {
            // !
            new CharFontData(
                new Vector2(_pixelWidth * 600, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            /*
            new CharFontData(
                Vector2.Zero,
                new Vector2(1),
                new Vector2(20),
                Vector2.Zero),*/
            // "
            new CharFontData(
                new Vector2(_pixelWidth * 250, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // #
            new CharFontData(
                new Vector2(_pixelWidth * 325, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // $
            new CharFontData(
                new Vector2(0, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // %
            new CharFontData(
                new Vector2(_pixelWidth * 225, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // &
            new CharFontData(
                new Vector2(_pixelWidth * 375, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // '
            new CharFontData(
                new Vector2(_pixelWidth * 300, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // (
            new CharFontData(
                new Vector2(_pixelWidth * 425, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // )
            new CharFontData(
                new Vector2(_pixelWidth * 450, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // *
            new CharFontData(
                new Vector2(_pixelWidth * 125, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // +
            new CharFontData(
                new Vector2(_pixelWidth * 75, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // ,
            new CharFontData(
                new Vector2(_pixelWidth * 475, _pixelHeight * 43),
                _texSize,
                _charSize,
                _offset),
            // -
            new CharFontData(
                new Vector2(_pixelWidth * 100, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // .
            new CharFontData(
                new Vector2(_pixelWidth * 500, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // /
            new CharFontData(
                new Vector2(_pixelWidth * 150, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 0
            new CharFontData(
                new Vector2(0, _pixelHeight * 91),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 1
            new CharFontData(
                new Vector2(_pixelWidth * 25, _pixelHeight * 91),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 2
            new CharFontData(
                new Vector2(_pixelWidth * 50, _pixelHeight * 91),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 3
            new CharFontData(
                new Vector2(_pixelWidth * 75, _pixelHeight * 91),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 4
            new CharFontData(
                new Vector2(_pixelWidth * 100, _pixelHeight * 91),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 5
            new CharFontData(
                new Vector2(_pixelWidth * 125, _pixelHeight * 91),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 6
            new CharFontData(
                new Vector2(_pixelWidth * 150, _pixelHeight * 91),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 7
            new CharFontData(
                new Vector2(_pixelWidth * 175, _pixelHeight * 91),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 8
            new CharFontData(
                new Vector2(_pixelWidth * 200, _pixelHeight * 91),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 9
            new CharFontData(
                new Vector2(_pixelWidth * 225, _pixelHeight * 91),
                _texSize,
                _charSize,
                Vector2.Zero),
            // :
            new CharFontData(
                new Vector2(_pixelWidth * 550, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // ;
            new CharFontData(
                new Vector2(_pixelWidth * 525, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // <
            new CharFontData(
                new Vector2(_pixelWidth * 75, 0),
                _texSize,
                _charSize,
                Vector2.Zero),
            // =
            new CharFontData(
                new Vector2(_pixelWidth * 200, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // >
            new CharFontData(
                new Vector2(_pixelWidth * 100, 0),
                _texSize,
                _charSize,
                Vector2.Zero),
            // ?
            new CharFontData(
                new Vector2(_pixelWidth * 575, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // @
            new CharFontData(
                new Vector2(_pixelWidth * 350, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),

            // Alphabet Caps
            new CharFontData(
                new Vector2(0, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 25, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 50, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 75, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 100, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 125, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 150, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 175, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 200, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 225, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 250, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 275, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 300, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 325, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 350, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 375, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 400, _pixelHeight * 182),
                new Vector2(_pixelWidth * 24, _pixelHeight * 47),
                new Vector2((1d / 42) * 24, (1d / 42) * 47),
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 425, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 450, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 475, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 500, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 525, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 550, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 575, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 600, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 625, _pixelHeight * 187),
                _texSize,
                _charSize,
                Vector2.Zero),

            // [
            new CharFontData(
                new Vector2(_pixelWidth * 125, 0),
                _texSize,
                _charSize,
                Vector2.Zero),
            // \
            new CharFontData(
                new Vector2(_pixelWidth * 625, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero),
            // ]
            new CharFontData(
                new Vector2(_pixelWidth * 150, 0),
                _texSize,
                _charSize,
                Vector2.Zero),
            // ^
            new CharFontData(
                new Vector2(_pixelWidth * 175, 0),
                _texSize,
                _charSize,
                Vector2.Zero),
            // _
            new CharFontData(
                new Vector2(_pixelWidth * 400, _pixelHeight * 43),
                _texSize,
                _charSize,
                _offset),

            CharFontData.Unsupported,

            // Alphabet lower case
            new CharFontData(
                new Vector2(_pixelWidth * 0, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 25, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 50, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 75, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 100, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 125, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 150, _pixelHeight * 134),
                _texSize,
                _charSize,
                _offset),
            new CharFontData(
                new Vector2(_pixelWidth * 175, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 200, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 225, _pixelHeight * 134),
                _texSize,
                _charSize,
                _offset),
            new CharFontData(
                new Vector2(_pixelWidth * 250, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 275, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 300, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 325, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 350, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 375, _pixelHeight * 134),
                _texSize,
                _charSize,
                _offset),
            new CharFontData(
                new Vector2(_pixelWidth * 400, _pixelHeight * 134),
                _texSize,
                _charSize,
                _offset),
            new CharFontData(
                new Vector2(_pixelWidth * 425, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 450, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 475, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 500, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 525, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 550, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 575, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 600, _pixelHeight * 134),
                _texSize,
                _charSize,
                _offset),
            new CharFontData(
                new Vector2(_pixelWidth * 625, _pixelHeight * 139),
                _texSize,
                _charSize,
                Vector2.Zero),

            // {
            new CharFontData(
                new Vector2(_pixelWidth * 25, 0),
                _texSize,
                _charSize,
                Vector2.Zero),
            // |
            new CharFontData(
                new Vector2(0),
                _texSize,
                _charSize,
                Vector2.Zero),
            // }
            new CharFontData(
                new Vector2(_pixelWidth * 50, 0),
                _texSize,
                _charSize,
                Vector2.Zero),
            // ~
            new CharFontData(
                new Vector2(_pixelWidth * 275, _pixelHeight * 48),
                _texSize,
                _charSize,
                Vector2.Zero)
        };
        private static readonly CharFontData _poundChar = new CharFontData(
            new Vector2(_pixelWidth * 50, _pixelHeight * 48),
            _texSize,
            _charSize,
            Vector2.Zero);
        private static readonly CharFontData _copywriteChar = new CharFontData(
            new Vector2(_pixelWidth * 200, 0),
            _texSize,
            _charSize,
            Vector2.Zero);
        private static readonly CharFontData _registeredChar = new CharFontData(
            new Vector2(_pixelWidth * 225, 0),
            _texSize,
            _charSize,
            Vector2.Zero);
        private static readonly CharFontData _trademarkChar = new CharFontData(
            new Vector2(_pixelWidth * 250, 0),
            _texSize,
            _charSize,
            Vector2.Zero);

        public override CharFontData GetCharacterData(char character)
        {
            try
            {
                return _characterData[character - 33];
            }
            catch (IndexOutOfRangeException)
            {
                if (character == '£') { return _poundChar; }
                if (character == '©') { return _copywriteChar; }
                if (character == '®') { return _registeredChar; }
                if (character == '™') { return _trademarkChar; }

                return CharFontData.Unsupported;
            }
        }
    }
}
