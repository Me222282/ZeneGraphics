using System.Collections.Generic;
using Zene.Structs;

namespace ObjLoader.Data
{
    internal class DataStore
    {
        private Group _currentGroup;

        private readonly List<Group> _groups = new List<Group>();

        private readonly List<Vector3> _vertices = new List<Vector3>();
        private readonly List<Vector2> _textures = new List<Vector2>();
        private readonly List<Vector3> _normals = new List<Vector3>();

        public IList<Vector3> Vertices
        {
            get { return _vertices; }
        }

        public IList<Vector2> Textures
        {
            get { return _textures; }
        }

        public IList<Vector3> Normals
        {
            get { return _normals; }
        }

        public IList<Group> Groups
        {
            get { return _groups; }
        }

        public void AddFace(Face face)
        {
            PushGroupIfNeeded();

            _currentGroup.AddFace(face);
        }

        public void PushGroup(string groupName)
        {
            _currentGroup = new Group(groupName);
            _groups.Add(_currentGroup);
        }

        private void PushGroupIfNeeded()
        {
            if (_currentGroup == null)
            {
                PushGroup("default");
            }
        }

        public void AddVertex(Vector3 vertex)
        {
            _vertices.Add(vertex);
        }

        public void AddTexture(Vector2 texture)
        {
            _textures.Add(texture);
        }

        public void AddNormal(Vector3 normal)
        {
            _normals.Add(normal);
        }
    }
}