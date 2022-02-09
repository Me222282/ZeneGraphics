using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zene.Graphics;
using Zene.Structs;

namespace Zene.Forms
{
    public class FormPanel : IFormObject
    {
        public List<IFormObject> Children { get; } = new List<IFormObject>();
        public IFormObject Parent => null;

        public IFormShader Shader { get; set; }
        private Rectangle _bounds;
        public Rectangle Bounds
        {
            get
            {
                return _bounds;
            }
            set
            {
                //FrameBuffer.SetSize((int)value.Width, (int)value.Height);

                _bounds = value;
            }
        }
        IBox IFormObject.Bounds => Bounds;

        public IFrameBuffer FrameBuffer { get; }

        public void Draw()
        {
            FrameBuffer.Bind();

            foreach (IFormObject obj in Children)
            {
                obj.Draw();
            }

            
        }
    }
}
