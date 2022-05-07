using System.Collections.Generic;
using ObjLoader.Data;
using Zene.Structs;

namespace ObjLoader
{
    internal class LoadResult  
    {
        public IList<Vector3> Vertices { get; set; }
        public IList<Vector2> Textures { get; set; }
        public IList<Vector3> Normals { get; set; }
        public IList<Group> Groups { get; set; }
        //public IList<Material> Materials { get; set; }
    }
}