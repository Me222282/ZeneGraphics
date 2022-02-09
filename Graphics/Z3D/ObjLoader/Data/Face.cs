using System.Collections.Generic;
using Zene.Structs;

namespace ObjLoader.Data
{
    internal class Face
    {
        private readonly List<Vector3I> _vertices = new List<Vector3I>();

        public void AddVertex(Vector3I vertex)
        {
            _vertices.Add(vertex);
        }

        public Vector3I this[int i]
        {
            get { return _vertices[i]; }
        }

        public int Count
        {
            get { return _vertices.Count; }
        }
    }
}