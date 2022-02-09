using System;
using ObjLoader.Common;
using ObjLoader.Data;
using Zene.Structs;

namespace ObjLoader.TypeParsers
{
    internal class VertexParser : TypeParserBase
    {
        private readonly DataStore _vertexDataStore;

        public VertexParser(DataStore vertexDataStore)
        {
            _vertexDataStore = vertexDataStore;
        }

        protected override string Keyword
        {
            get { return "v"; }
        }

        public override void Parse(string line)
        {
            string[] parts = line.Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries);

            double x = parts[0].ParseInvariantFloat();
            double y = parts[1].ParseInvariantFloat();
            double z = parts[2].ParseInvariantFloat();

            Vector3 vertex = new Vector3(x, y, z);
            _vertexDataStore.AddVertex(vertex);
        }
    }
}