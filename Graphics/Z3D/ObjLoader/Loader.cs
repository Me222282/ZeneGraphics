using System.Collections.Generic;
using System.IO;
using ObjLoader.Data;
using ObjLoader.TypeParsers;

namespace ObjLoader
{
    internal class Loader
    {
        private readonly DataStore _dataStore;
        private readonly List<ITypeParser> _typeParsers = new List<ITypeParser>();

        private readonly List<string> _unrecognizedLines = new List<string>();

        public Loader()
        {
            _dataStore = new DataStore();
            SetupTypeParsers(
                new VertexParser(_dataStore),
                new FaceParser(_dataStore),
                new NormalParser(_dataStore),
                new TextureParser(_dataStore),
                new GroupParser(_dataStore));
        }

        private void SetupTypeParsers(params ITypeParser[] parsers)
        {
            foreach (var parser in parsers)
            {
                _typeParsers.Add(parser);
            }
        }

        private void ParseLine(string keyword, string data)
        {
            foreach (var typeParser in _typeParsers)
            {
                if (typeParser.CanParse(keyword))
                {
                    typeParser.Parse(data);
                    return;
                }
            }

            _unrecognizedLines.Add(keyword + ' ' + data);
        }

        public LoadResult Load(Stream lineStream)
        {
            StartLoad(lineStream);

            return CreateResult();
        }

        private LoadResult CreateResult()
        {
            LoadResult result = new LoadResult
            {
                Vertices = _dataStore.Vertices,
                Textures = _dataStore.Textures,
                Normals = _dataStore.Normals,
                Groups = _dataStore.Groups
            };
            return result;
        }

        private StreamReader _lineStreamReader;

        private void StartLoad(Stream lineStream)
        {
            _lineStreamReader = new StreamReader(lineStream);

            while (!_lineStreamReader.EndOfStream)
            {
                ParseLine();
            }
        }

        private void ParseLine()
        {
            string currentLine = _lineStreamReader.ReadLine();

            if (string.IsNullOrWhiteSpace(currentLine) || currentLine[0] == '#')
            {
                return;
            }

            string[] fields = currentLine.Trim().Split(null, 2);
            string keyword = fields[0].Trim();
            string data = fields[1].Trim();

            ParseLine(keyword, data);
        }
    }
}