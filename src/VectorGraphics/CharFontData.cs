using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that contains data about how to reference and render a certain <see cref="char"/>.
    /// </summary>
    public struct CharFontData
    {
        public CharFontData(Vector2I texPos, Vector2I texSize)
        {
            TexturePosision = texPos;
            Size = texSize;
            ExtraOffset = 0;
            Buffer = 0;

            Supported = true;
        }
        public CharFontData(Vector2I texPos, Vector2I texSize, Vector2I offset)
        {
            TexturePosision = texPos;
            Size = texSize;
            ExtraOffset = offset;
            Buffer = 0;

            Supported = true;
        }
        public CharFontData(Vector2I texPos, Vector2I texSize, Vector2I offset, int buffer)
        {
            TexturePosision = texPos;
            Size = texSize;
            ExtraOffset = offset;
            Buffer = buffer;

            Supported = true;
        }

        public Vector2I TexturePosision { get; set; }
        public Vector2I Size { get; set; }
        public Vector2I ExtraOffset { get; set; }
        public int Buffer { get; set; }
        public bool Supported { get; }

        public override string ToString()
        {
            if (!Supported) { return "Unsupported character"; }

            return $"CharFontData\n{{\n    Offset: [{ExtraOffset}]\n    Size: [{Size}]\n    TexPos: [{TexturePosision}]\n     Buffer: [{Buffer}]\n}}";
        }
        public string ToString(string format)
        {
            if (!Supported) { return "Unsupported character"; }

            return $"CharFontData\n{{\n    Offset: [{ExtraOffset.ToString(format)}]\n    Size: [{Size.ToString(format)}]\n    TexPos: [{TexturePosision.ToString(format)}]\n     Buffer: [{Buffer.ToString(format)}]\n}}";
        }

        public static CharFontData Unsupported { get; } = new CharFontData();
    }
}
