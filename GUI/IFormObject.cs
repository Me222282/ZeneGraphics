using System.Collections.Generic;
using Zene.Structs;

namespace Zene.GUI
{
    public interface IFormObject
    {
        public List<IFormObject> Children { get; }
        public IFormObject Parent { get; }

        public IFormShader Shader { get; set; }
        public IBox Bounds { get; }

        public void Draw();
    }
}
