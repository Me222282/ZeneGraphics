using ObjLoader.Data;

namespace ObjLoader.TypeParsers
{
    internal class GroupParser : TypeParserBase
    {
        private readonly DataStore _groupDataStore;

        public GroupParser(DataStore groupDataStore)
        {
            _groupDataStore = groupDataStore;
        }

        protected override string Keyword
        {
            get { return "g"; }
        }

        public override void Parse(string line)
        {
            _groupDataStore.PushGroup(line);
        }
    }
}