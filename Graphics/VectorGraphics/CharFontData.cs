using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that contains data about how to reference and render a certain <see cref="char"/>.
    /// </summary>
    public struct CharFontData
    {
        public CharFontData(Vector2 texCoordOffset, Vector2 texSize, Vector2 size, Vector2 extraOffset)
        {
            TextureCoordOffset = texCoordOffset;
            TextureRefSize = texSize;
            Size = size;
            ExtraOffset = extraOffset;

            Supported = true;
        }
        public CharFontData(Vector2 texCoordOffset, Vector2 texSize)
        {
            TextureCoordOffset = texCoordOffset;
            TextureRefSize = texSize;
            Size = Vector2.One;
            ExtraOffset = Vector2.Zero;

            Supported = true;
        }

        public Vector2 TextureCoordOffset { get; }
        public Vector2 TextureRefSize { get; }
        public Vector2 Size { get; }
        public Vector2 ExtraOffset { get; }
        public bool Supported { get; }

        public override string ToString()
        {
            if (!Supported) { return "Unsupported character"; }

            return $"CharFontData\n{{\n    Size: [{Size}]\n    Offset: [{ExtraOffset}]\n    TexSize: [{TextureRefSize}]\n    TexOffset: [{TextureCoordOffset}]\n}}";
        }
        public string ToString(string format)
        {
            if (!Supported) { return "Unsupported character"; }

            return $"CharFontData\n{{\n    Size: [{Size.ToString(format)}]\n    Offset: [{ExtraOffset.ToString(format)}]\n    TexSize: [{TextureRefSize.ToString(format)}]\n    TexOffset: [{TextureCoordOffset.ToString(format)}]\n}}";
        }

        public static CharFontData Unsupported { get; } = new CharFontData();
    }
}
