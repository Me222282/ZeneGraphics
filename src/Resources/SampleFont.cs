using System.IO;

namespace Zene.Graphics
{
    public sealed class SampleFont : Font, IIdentifiable
    {
        public SampleFont()
            : base(11, 21, 6)
        {
            // Load font image
            byte[] byteData = Bitmap.ExtractData(new MemoryStream(Resources.SampleFont), out int w, out int h, close:true);

            // Create and setup texture
            SourceTexture = new Texture2D(TextureFormat.R8, TextureData.Byte)
            {
                WrapStyle = WrapStyle.EdgeClamp,
                MinFilter = TextureSampling.Blend,
                MagFilter = TextureSampling.Blend
            };
            // Assign data
            SourceTexture.SetData(w, h, BaseFormat.Rgba, new GLArray<byte>(w, h, 4, byteData));

            SourceTexture.CreateMipMap();
        }

        public override Texture2D SourceTexture { get; }
        uint IIdentifiable.Id => SourceTexture.Id;

        private static CharFontData GetChar(int ox, int oy, int sx, int sy)
        {
            return new CharFontData((ox, oy), (sx, sy));
        }
        private static CharFontData GetChar(int ox, int oy, int sx, int sy, int offset)
        {
            return new CharFontData((ox, oy), (sx, sy), (0, offset));
        }

        private static readonly CharFontData[] _characterData = new CharFontData[]
        {
            // !
            GetChar(6, 487, 5, 19),
            // "
            GetChar(20, 498, 8, 8, 12),
            // #
            GetChar(38, 487, 16, 19),
            // $
            GetChar(63, 485, 12, 21, -1),
            // %
            GetChar(84, 487, 20, 19),
            // &
            GetChar(112, 487, 18, 19),
            // '
            GetChar(140, 498, 4, 8, 12),
            // (
            GetChar(153, 483, 7, 23, -2),
            // )
            GetChar(170, 483, 7, 23, -2),
            // *
            GetChar(186, 494, 13, 12, 8),
            // +
            GetChar(208, 493, 13, 13, 3),
            // ,
            GetChar(231, 499, 5, 8),
            // -
            GetChar(246, 503, 8, 3, 8),
            // .
            GetChar(263, 502, 5, 5),
            // /
            GetChar(276, 487, 10, 19),
            // 0
            GetChar(295, 487, 14, 19),
            // 1
            GetChar(317, 487, 8, 19),
            // 2
            GetChar(334, 487, 13, 19),
            // 3
            GetChar(356, 487, 13, 19),
            // 4
            GetChar(378, 487, 15, 19),
            // 5
            GetChar(403, 487, 12, 19),
            // 6
            GetChar(424, 487, 14, 19),
            // 7
            GetChar(446, 487, 14, 19),
            // 8
            GetChar(468, 487, 13, 19),
            // 9
            GetChar(490, 487, 13, 19),
            // :
            GetChar(246, 478, 5, 15),
            // ;
            GetChar(260, 474, 6, 18),
            // <
            GetChar(186, 471, 13, 13, 3),
            // =
            GetChar(208, 475, 13, 9, 5),
            // >
            GetChar(19, 465, 13, 13, 3),
            // ?
            GetChar(275, 459, 11, 19),
            // @
            GetChar(317, 456, 21, 22, -2),
            // A
            GetChar(347, 458, 17, 19),
            // B
            GetChar(378, 459, 14, 19),
            // C
            GetChar(401, 458, 15, 19),
            // D
            GetChar(425, 458, 16, 19),
            // E
            GetChar(41, 459, 12, 19),
            // F
            GetChar(294, 458, 12, 19),
            // G
            GetChar(450, 458, 17, 19),
            // H
            GetChar(475, 458, 15, 19),
            // I
            GetChar(230, 470, 4, 19),
            // J
            GetChar(84, 454, 8, 23, -4),
            // K
            GetChar(101, 458, 14, 19),
            // L
            GetChar(124, 458, 12, 19),
            // M
            GetChar(144, 455, 19, 19),
            // N
            GetChar(243, 445, 16, 19),
            // O
            GetChar(172, 442, 18, 19),
            // P
            GetChar(61, 456, 13, 19),
            // Q
            GetChar(199, 438, 18, 23, -4),
            // R
            GetChar(5, 436, 14, 19),
            // S
            GetChar(373, 431, 13, 19),
            // T
            GetChar(28, 431, 15, 19),
            // U
            GetChar(267, 430, 15, 19),
            // V
            GetChar(291, 430, 16, 19),
            // W
            GetChar(475, 430, 24, 19),
            // X
            GetChar(425, 430, 15, 19),
            // Y
            GetChar(347, 430, 15, 19),
            // Z
            GetChar(102, 430, 14, 19),
            // [
            GetChar(227, 438, 7, 23, -2),
            // \
            GetChar(125, 430, 9, 19),
            // ]
            GetChar(450, 425, 7, 23, -2),
            // ^
            GetChar(396, 435, 13, 13, 5),
            // _
            GetChar(317, 444, 13, 4),
            // `
            GetChar(53, 442, 6, 5, 13),
            // a
            GetChar(143, 431, 12, 15),
            // b
            GetChar(68, 424, 13, 20),
            // c
            GetChar(243, 421, 11, 15),
            // d
            GetChar(316, 415, 14, 20),
            // e
            GetChar(164, 417, 13, 15),
            // f
            GetChar(187, 408, 10, 20),
            // g
            GetChar(206, 407, 14, 21, -6),
            // h
            GetChar(5, 407, 13, 20),
            // i
            GetChar(53, 413, 4, 19),
            // j
            GetChar(396, 401, 7, 25, -6),
            // k
            GetChar(26, 402, 12, 20),
            // l
            GetChar(229, 409, 4, 20),
            // m
            GetChar(412, 406, 20, 15),
            // n
            GetChar(371, 406, 13, 15),
            // o
            GetChar(338, 406, 14, 15),
            // p
            GetChar(465, 400, 13, 21, -6),
            // q
            GetChar(90, 400, 14, 21, -6),
            // r
            GetChar(487, 406, 9, 15),
            // s
            GetChar(291, 406, 11, 15),
            // t
            GetChar(113, 404, 9, 18),
            // u
            GetChar(131, 407, 13, 15),
            // v
            GetChar(263, 406, 14, 15),
            // w
            GetChar(152, 393, 20, 15),
            // x
            GetChar(442, 402, 13, 15),
            // y
            GetChar(66, 394, 14, 21, -6),
            // z
            GetChar(243, 397, 11, 15),
            // {
            GetChar(312, 382, 9, 23, -2),
            // |
            GetChar(48, 378, 3, 26, -4),
            // }
            GetChar(182, 376, 9, 23, -2),
            // ~
            GetChar(200, 394, 13, 5, 7)
        };

        public override CharFontData GetCharacterData(char character)
        {
            int char33 = character - 33;

            if (char33 >= 0 &&
                char33 < _characterData.Length)
            {
                return _characterData[character - 33];
            }

            return CharFontData.Unsupported;
        }
        public override CharFontData GetCharacterData(char character, char pre, char post)
        {
            CharFontData cfd = GetCharacterData(character);

            if (character != '_') { return cfd; }

            if (pre == '_')
            {
                cfd.TexturePosision = (cfd.TexturePosision.X + 1, cfd.TexturePosision.Y);
                cfd.Size = (cfd.Size.X - 1, cfd.Size.Y);
            }
            if (post == '_')
            {
                cfd.Size = (cfd.Size.X - 1, cfd.Size.Y);
            }

            return cfd;
        }

        public static SampleFont GetInstance()
        {
            IIdentifiable i = State.CurrentContext.GetTrack(typeof(SampleFont));

            if (i != null)
            {
                return i as SampleFont;
            }

            SampleFont font = new SampleFont();
            State.CurrentContext.TrackObject(font);

            return font;
        }
    }
}