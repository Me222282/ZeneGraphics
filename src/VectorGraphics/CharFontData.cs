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
            Buffer = 0;

            Supported = true;
        }
        public CharFontData(Vector2 texCoordOffset, Vector2 texSize, Vector2 size, Vector2 extraOffset, double buffer)
        {
            TextureCoordOffset = texCoordOffset;
            TextureRefSize = texSize;
            Size = size;
            ExtraOffset = extraOffset;
            Buffer = buffer;

            Supported = true;
        }
        public CharFontData(Vector2 texCoordOffset, Vector2 texSize)
        {
            TextureCoordOffset = texCoordOffset;
            TextureRefSize = texSize;
            Size = Vector2.One;
            ExtraOffset = Vector2.Zero;
            Buffer = 0;

            Supported = true;
        }

        public Vector2 TextureCoordOffset { get; set; }
        public Vector2 TextureRefSize { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 ExtraOffset { get; set; }
        public double Buffer { get; set; }
        public bool Supported { get; }

        public override string ToString()
        {
            if (!Supported) { return "Unsupported character"; }

            return $"CharFontData\n{{\n    Size: [{Size}]\n    Offset: [{ExtraOffset}]\n    TexSize: [{TextureRefSize}]\n    TexOffset: [{TextureCoordOffset}]\n     Buffer: [{Buffer}]\n}}";
        }
        public string ToString(string format)
        {
            if (!Supported) { return "Unsupported character"; }

            return $"CharFontData\n{{\n    Size: [{Size.ToString(format)}]\n    Offset: [{ExtraOffset.ToString(format)}]\n    TexSize: [{TextureRefSize.ToString(format)}]\n    TexOffset: [{TextureCoordOffset.ToString(format)}]\n     Buffer: [{Buffer.ToString(format)}]\n}}";
        }

        public static CharFontData Unsupported { get; } = new CharFontData();
    }
}
