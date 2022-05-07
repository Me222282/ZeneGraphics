namespace ObjLoader.TypeParsers
{
    internal interface ITypeParser
    {
        bool CanParse(string keyword);
        void Parse(string line);
    }
}