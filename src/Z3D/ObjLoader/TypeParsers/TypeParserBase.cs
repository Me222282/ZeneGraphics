using ObjLoader.Common;

namespace ObjLoader.TypeParsers
{
    internal abstract class TypeParserBase : ITypeParser
    {
        protected abstract string Keyword { get; }

        public bool CanParse(string keyword)
        {
            return keyword.EqualsOrdinalIgnoreCase(Keyword);
        }

        public abstract void Parse(string line);
    }
}