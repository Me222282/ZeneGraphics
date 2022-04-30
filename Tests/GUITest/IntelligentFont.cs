using System;
using Zene.Graphics;
using Zene.Structs;

namespace GUITest
{
    public class IntelligentFont : Font
    {
        public IntelligentFont()
            : base(1d, 1d)
        {
            // Load font image
            byte[] byteData = Bitmap.ExtractData("resources/fontI.png", out int w, out int h);
            // Convert to one channel GLArray
            GLArray<Vector4<byte>> texData = new GLArray<Vector4<byte>>(w, h);
            for (int i = 0; i < texData.Size; i++)
            {
                int offset = i * 4;

                texData[i] = new Vector4<byte>(
                    byteData[offset],
                    byteData[offset + 1],
                    byteData[offset + 2], 0);
            }
            // Create and setup texture
            _texture = new Texture2D(TextureFormat.Rgb8, TextureData.Byte)
            {
                WrapStyle = WrapStyle.EdgeClamp,
                MinFilter = TextureSampling.Blend,
                MagFilter = TextureSampling.Blend
            };
            // Asign data
            _texture.SetData(w, h, BaseFormat.Rgba, texData);
        }

        private readonly Texture2D _texture;

        public override void BindTexture(uint slot) => _texture.Bind(slot);

        private const double _pixelHeight = 1d / 480d;
        private const double _pixelWidth = 1d / 640d;

        private static readonly Vector2 _texSize = new Vector2(_pixelWidth * 40, _pixelHeight * 40);
        private static readonly Vector2 _charSize = Vector2.One;

        private const double _1over40 = 1d / 40d;
        private static Vector2 GetSize(int px, int py)
        {
            return new Vector2(_pixelWidth * px, _pixelHeight * py);
        }
        private static Vector2 GetChar(int pw, int ph)
        {
            return new Vector2(_1over40 * pw, _1over40 * ph);
        }

        private static readonly CharFontData[] _characterData = new CharFontData[]
        {
            // !
            new CharFontData(
                GetSize(58, 448),
                GetSize(3, 22),
                GetChar(3, 22),
                GetChar(0, (40 - 22) / -2)),
            /*
            new CharFontData(
                Vector2.Zero,
                new Vector2(1),
                new Vector2(20),
                Vector2.Zero),*/
            // "
            new CharFontData(
                new Vector2(_pixelWidth * 80, _pixelHeight * 440),
                _texSize,
                _charSize,
                Vector2.Zero),
            // #
            new CharFontData(
                new Vector2(_pixelWidth * 120, _pixelHeight * 440),
                _texSize,
                _charSize,
                Vector2.Zero),
            // $
            new CharFontData(
                new Vector2(_pixelWidth * 160, _pixelHeight * 440),
                _texSize,
                _charSize,
                Vector2.Zero),
            // %
            new CharFontData(
                new Vector2(_pixelWidth * 200, _pixelHeight * 440),
                _texSize,
                _charSize,
                Vector2.Zero),
            // &
            new CharFontData(
                new Vector2(_pixelWidth * 240, _pixelHeight * 440),
                _texSize,
                _charSize,
                Vector2.Zero),
            // '
            new CharFontData(
                new Vector2(_pixelWidth * 280, _pixelHeight * 440),
                _texSize,
                _charSize,
                Vector2.Zero),
            // (
            new CharFontData(
                new Vector2(_pixelWidth * 320, _pixelHeight * 440),
                _texSize,
                _charSize,
                Vector2.Zero),
            // )
            new CharFontData(
                new Vector2(_pixelWidth * 360, _pixelHeight * 440),
                _texSize,
                _charSize,
                Vector2.Zero),
            // *
            new CharFontData(
                new Vector2(_pixelWidth * 400, _pixelHeight * 440),
                _texSize,
                _charSize,
                Vector2.Zero),
            // +
            new CharFontData(
                new Vector2(_pixelWidth * 440, _pixelHeight * 440),
                _texSize,
                _charSize,
                Vector2.Zero),
            // ,
            new CharFontData(
                new Vector2(_pixelWidth * 480, _pixelHeight * 440),
                _texSize,
                _charSize,
                Vector2.Zero),
            // -
            new CharFontData(
                new Vector2(_pixelWidth * 520, _pixelHeight * 440),
                _texSize,
                _charSize,
                Vector2.Zero),
            // .
            new CharFontData(
                new Vector2(_pixelWidth * 560, _pixelHeight * 440),
                _texSize,
                _charSize,
                Vector2.Zero),
            // /
            new CharFontData(
                new Vector2(_pixelWidth * 600, _pixelHeight * 440),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 0
            new CharFontData(
                new Vector2(_pixelWidth * 0, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 1
            new CharFontData(
                new Vector2(_pixelWidth * 40, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 2
            new CharFontData(
                new Vector2(_pixelWidth * 80, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 3
            new CharFontData(
                new Vector2(_pixelWidth * 120, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 4
            new CharFontData(
                new Vector2(_pixelWidth * 160, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 5
            new CharFontData(
                new Vector2(_pixelWidth * 200, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 6
            new CharFontData(
                new Vector2(_pixelWidth * 240, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 7
            new CharFontData(
                new Vector2(_pixelWidth * 280, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 8
            new CharFontData(
                new Vector2(_pixelWidth * 320, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // 9
            new CharFontData(
                new Vector2(_pixelWidth * 360, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // :
            new CharFontData(
                new Vector2(_pixelWidth * 400, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // ;
            new CharFontData(
                new Vector2(_pixelWidth * 440, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // <
            new CharFontData(
                new Vector2(_pixelWidth * 480, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // =
            new CharFontData(
                new Vector2(_pixelWidth * 520, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // >
            new CharFontData(
                new Vector2(_pixelWidth * 560, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // ?
            new CharFontData(
                new Vector2(_pixelWidth * 600, _pixelHeight * 400),
                _texSize,
                _charSize,
                Vector2.Zero),
            // @
            new CharFontData(
                new Vector2(_pixelWidth * 0, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),

            // Alphabet Caps
            new CharFontData(
                new Vector2(_pixelWidth * 40, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 80, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 120, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 160, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 200, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 240, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 280, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 320, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 360, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 400, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 440, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 480, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 520, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 560, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 600, _pixelHeight * 360),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 0, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 40, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 80, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 120, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 160, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 200, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 240, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 280, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 320, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 360, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 400, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),

            // [
            new CharFontData(
                new Vector2(_pixelWidth * 440, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),
            // \
            new CharFontData(
                new Vector2(_pixelWidth * 480, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),
            // ]
            new CharFontData(
                new Vector2(_pixelWidth * 520, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),
            // ^
            new CharFontData(
                new Vector2(_pixelWidth * 560, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),
            // _
            new CharFontData(
                new Vector2(_pixelWidth * 600, _pixelHeight * 320),
                _texSize,
                _charSize,
                Vector2.Zero),

            new CharFontData(
                new Vector2(_pixelWidth * 0, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),

            // Alphabet lower case
            new CharFontData(
                new Vector2(_pixelWidth * 40, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 80, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 120, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 160, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 200, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 240, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 280, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 320, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 360, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 400, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 440, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 480, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 520, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 560, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 600, _pixelHeight * 280),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 0, _pixelHeight * 240),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 40, _pixelHeight * 240),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 80, _pixelHeight * 240),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 120, _pixelHeight * 240),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 160, _pixelHeight * 240),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 200, _pixelHeight * 240),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 240, _pixelHeight * 240),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 280, _pixelHeight * 240),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 320, _pixelHeight * 240),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 360, _pixelHeight * 240),
                _texSize,
                _charSize,
                Vector2.Zero),
            new CharFontData(
                new Vector2(_pixelWidth * 400, _pixelHeight * 240),
                _texSize,
                _charSize,
                Vector2.Zero),

            // {
            new CharFontData(
                new Vector2(_pixelWidth * 440, _pixelHeight * 240),
                _texSize,
                _charSize,
                Vector2.Zero),
            // |
            new CharFontData(
                new Vector2(_pixelWidth * 480, _pixelHeight * 240),
                _texSize,
                _charSize,
                Vector2.Zero),
            // }
            new CharFontData(
                new Vector2(_pixelWidth * 520, _pixelHeight * 240),
                _texSize,
                _charSize,
                Vector2.Zero),
            // ~
            new CharFontData(
                new Vector2(_pixelWidth * 560, _pixelHeight * 240),
                _texSize,
                _charSize,
                Vector2.Zero)
        };

        public override CharFontData GetCharacterData(char character)
        {
            try
            {
                return _characterData[character - 33];
            }
            catch (IndexOutOfRangeException)
            {
                return CharFontData.Unsupported;
            }
        }
    }
}