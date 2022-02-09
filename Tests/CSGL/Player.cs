using System;
using Zene.Graphics;
using Zene.Graphics.Z3D;
using Zene.Graphics.Shaders;

namespace CSGL
{
    public class Player : IDrawable, IDisposable
    {
        public Player(double width, double height, double depth)
        {
            
            double hW = width / 2;
            double hH = height / 2;
            double hD = depth / 2;
            /*
            Window3D.AddNormals(new Point3[8]
                {
                    new Point3(-hW, -hH, -hD),
                    new Point3(+hW, -hH, -hD),
                    new Point3(+hW, +hH, -hD),
                    new Point3(-hW, +hH, -hD),
                    new Point3(-hW, +hH, +hD),
                    new Point3(+hW, +hH, +hD),
                    new Point3(+hW, -hH, +hD),
                    new Point3(-hW, -hH, +hD)
                }, 1, new byte[24]
                {
                    0, 1, 2,
                    2, 3, 0,

                    6, 7, 4,
                    4, 5, 6,

                    1, 6, 5,
                    5, 2, 1,

                    7, 0, 3,
                    3, 4, 7
                }, out List<Point3> verts, out List<uint> indices);

            _object = new DrawObject<Point3, uint>(verts, indices, 2, 0, AttributeSize.D3, Usage.Frequent);

            _object.AddAttribute((uint)LightingShader.Location.Normal, 1, AttributeSize.D3); // Normals*/

            _object = Object3D.FromObj("Resources/Bean.obj", (uint)LightingShader.Location.TextureCoords, (uint)LightingShader.Location.Normal);
            _objectSmall = Object3D.FromObj("Resources/SmallBean.obj", (uint)LightingShader.Location.TextureCoords, (uint)LightingShader.Location.Normal);

            Small = false;

            ColBox = new CObject(-hW, hW, -hH, hH, hD, -hD);
        }

        private readonly Object3D _object;
        private readonly Object3D _objectSmall;

        public CObject ColBox { get; set; }

        public bool Small { get; set; }

        public void Draw()
        {
            if (Small)
            {
                _objectSmall.Draw();
                return;
            }

            _object.Draw();
        }

        public void Dispose()
        {
            _object.Dispose();
        }
    }
}
