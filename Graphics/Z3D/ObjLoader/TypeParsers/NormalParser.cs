using ObjLoader.Common;
using ObjLoader.Data;
using Zene.Structs;

namespace ObjLoader.TypeParsers
{
    internal class NormalParser : TypeParserBase
    {
        private readonly DataStore _normalDataStore;

        public NormalParser(DataStore normalDataStore)
        {
            _normalDataStore = normalDataStore;
        }

        protected override string Keyword
        {
            get { return "vn"; }
        }

        public override void Parse(string line)
        {
            string[] parts = line.Split(' ');

            double x = parts[0].ParseInvariantFloat();
            double y = parts[1].ParseInvariantFloat();
            double z = parts[2].ParseInvariantFloat();

            Vector3 normal = new Vector3(x, y, z);
            _normalDataStore.AddNormal(normal);
        }
    }
}