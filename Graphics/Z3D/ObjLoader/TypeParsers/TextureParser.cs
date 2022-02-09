using ObjLoader.Common;
using ObjLoader.Data;
using Zene.Structs;

namespace ObjLoader.TypeParsers
{
    internal class TextureParser : TypeParserBase
    {
        private readonly DataStore _textureDataStore;

        public TextureParser(DataStore textureDataStore)
        {
            _textureDataStore = textureDataStore;
        }

        protected override string Keyword
        {
            get { return "vt"; }
        }

        public override void Parse(string line)
        {
            string[] parts = line.Split(' ');

            double x = parts[0].ParseInvariantFloat();
            double y = parts[1].ParseInvariantFloat();

            Vector2 texture = new Vector2(x, y);
            _textureDataStore.AddTexture(texture);
        }
    }
}