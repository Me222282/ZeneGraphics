using System;
using System.Collections.Generic;
using System.Linq;
using Zene.Structs;

namespace Zene.Graphics
{
    public class DrawingTexture<T> : GLObject, IDrawable where T : unmanaged
    {
        public DrawingTexture(IEnumerable<T> points, uint texCoordIndex, Bitmap bitmap, WrapStyle wrapStyle, TextureSampling resizeQuality, BufferUsage usage, bool useMipMap)
        {
            T[] aPoints = points.ToArray();

            int size = aPoints.Length % 4;
            int dataSize = size;

            Type t = typeof(T);

            if (t == typeof(Vector2)) { size = 2; }
            else if (t == typeof(Vector3)) { size = 3; }
            else if (t == typeof(Vector4)) { size = 4; }

            if (size <= 1)
            {
                throw new Exception("Cannot have a 1 dimensional vertex position.");
            }

            if (aPoints.Length / 4 > 4)
            {
                throw new Exception("Vertexes can only have a maximum of 4 dimensions.");
            }

            if (aPoints.Length % 4 != 0)
            {
                throw new Exception("Textures can only have 4 vertices.");
            }

            _DrawObject = new DrawObject<T, byte>(AddTexCoords(aPoints, size), IndexArray, (uint)dataSize + 2, 0, (AttributeSize)size, usage);

            _Texture = Texture2D.Create(bitmap, wrapStyle, resizeQuality, useMipMap);
            _texData = bitmap;
            _wrapStyle = wrapStyle;
            _quality = resizeQuality;
            _mipmap = useMipMap;

            TextureCoordIndex = texCoordIndex;

            _AttributeSize = size;
        }

        private readonly DrawObject<T, byte> _DrawObject;

        private Texture2D _Texture;
        private Bitmap _texData;
        private readonly WrapStyle _wrapStyle;
        private readonly TextureSampling _quality;
        private readonly bool _mipmap;

        private readonly int _AttributeSize;

        private uint _TexCoordI;
        public uint TextureCoordIndex
        {
            get
            {
                return _TexCoordI;
            }
            set
            {
                _TexCoordI = value;

                AddTexCoordsAttribute(value, _AttributeSize);
            }
        }

        public void SetData(Bitmap data)
        {
            _Texture.SetData(data.Width, data.Height, BaseFormat.Rgba, (GLArray<Colour>)data);
            _texData = data;
        }

        public void SetData(IEnumerable<T> data)
        {
            T[] aData = data.ToArray();

            if (aData.Length / 4 > 4)
            {
                throw new Exception("Vertexes can only have a maximum of 4 dimensions.");
            }

            if (aData.Length % 4 != 0)
            {
                throw new Exception("Textures can only have 4 vertices.");
            }

            _DrawObject.SetData(AddTexCoords(aData, _AttributeSize));
        }

        public void SetData(Bitmap bitmap, IEnumerable<T> points)
        {
            SetData(bitmap);
            SetData(points);
        }

        private void AddTexCoordsAttribute(uint texCoordIndex, int size)
        {
            Type t = typeof(T);

            if (t == typeof(Vector2)
                || t == typeof(Vector3)
                || t == typeof(Vector4))
            {
                _DrawObject.AddAttribute(texCoordIndex, 1, AttributeSize.D2);
            }
            else
            {
                _DrawObject.AddAttribute(texCoordIndex, size, AttributeSize.D2);
            }
        }

        public override void CreateData()
        {
            _DrawObject.CreateData();
            _Texture = Texture2D.Create(_texData, _wrapStyle, _quality, _mipmap);

            AddTexCoordsAttribute(_TexCoordI, _AttributeSize);

            _dataCreated = true;
        }

        public override void DeleteData()
        {
            if (!DataCreated) { return; }

            _DrawObject.DeleteData();
            _Texture.Dispose();

            _dataCreated = false;

            _bound = false;
        }

        public override void Bind()
        {
            _DrawObject.Bind();
            _Texture.Bind();

            _bound = true;
        }

        public override void Unbind()
        {
            _DrawObject.Bind();
            _Texture.Bind();

            _bound = false;
        }

        public virtual void Draw()
        {
            if (!_dataCreated)
            {
                CreateData();
            }

            bool bound = _bound;

            if (!bound) { Bind(); }

            _DrawObject.Draw();

            if (!bound) { Unbind(); }
        }

        private static readonly byte[] IndexArray = new byte[] { 0, 1, 2, 2, 3, 0 };

        private static List<T2> AddTexCoords<T2>(T2[] aPoints, int size)
        {
            List<T2> data = new List<T2>();

            if (aPoints[0] is Vector2)
            {
                // Top Right
                data.Add(aPoints[0]);
                data.Add((T2)(object)new Vector2(1.0, 1.0));
                // Bottom Right
                data.Add(aPoints[1]);
                data.Add((T2)(object)new Vector2(1.0, 0.0));
                // Bottom Left
                data.Add(aPoints[2]);
                data.Add((T2)(object)new Vector2(0.0, 0.0));
                // Top Left
                data.Add(aPoints[3]);
                data.Add((T2)(object)new Vector2(0.0, 1.0));
            }
            else if (aPoints[0] is Vector3)
            {
                // Top Right
                data.Add(aPoints[0]);
                data.Add((T2)(object)new Vector3(1.0, 1.0, 0));
                // Bottom Right
                data.Add(aPoints[1]);
                data.Add((T2)(object)new Vector3(1.0, 0.0, 0));
                // Bottom Left
                data.Add(aPoints[2]);
                data.Add((T2)(object)new Vector3(0.0, 0.0, 0));
                // Top Left
                data.Add(aPoints[3]);
                data.Add((T2)(object)new Vector3(0.0, 1.0, 0));
            }
            else if (aPoints[0] is Vector4)
            {
                // Top Right
                data.Add(aPoints[0]);
                data.Add((T2)(object)new Vector4(1.0, 1.0, 0, 0));
                // Bottom Right
                data.Add(aPoints[1]);
                data.Add((T2)(object)new Vector4(1.0, 0.0, 0, 0));
                // Bottom Left
                data.Add(aPoints[2]);
                data.Add((T2)(object)new Vector4(0.0, 0.0, 0, 0));
                // Top Left
                data.Add(aPoints[3]);
                data.Add((T2)(object)new Vector4(0.0, 1.0, 0, 0));
            }
            else
            {
                if (size == 2)
                {
                    // Top Right
                    data.Add(aPoints[0]);
                    data.Add(aPoints[1]);
                    data.Add((T2)(object)1);
                    data.Add((T2)(object)1);
                    // Bottom Right
                    data.Add(aPoints[2]);
                    data.Add(aPoints[3]);
                    data.Add((T2)(object)1);
                    data.Add((T2)(object)0);
                    // Bottom Left
                    data.Add(aPoints[4]);
                    data.Add(aPoints[5]);
                    data.Add((T2)(object)0);
                    data.Add((T2)(object)0);
                    // Top Left
                    data.Add(aPoints[6]);
                    data.Add(aPoints[7]);
                    data.Add((T2)(object)0);
                    data.Add((T2)(object)1);
                }
                else if (size == 3)
                {
                    // Top Right
                    data.Add(aPoints[0]);
                    data.Add(aPoints[1]);
                    data.Add(aPoints[2]);
                    data.Add((T2)(object)1);
                    data.Add((T2)(object)1);
                    // Bottom Right
                    data.Add(aPoints[3]);
                    data.Add(aPoints[4]);
                    data.Add(aPoints[5]);
                    data.Add((T2)(object)1);
                    data.Add((T2)(object)0);
                    // Bottom Left
                    data.Add(aPoints[6]);
                    data.Add(aPoints[7]);
                    data.Add(aPoints[8]);
                    data.Add((T2)(object)0);
                    data.Add((T2)(object)0);
                    // Top Left
                    data.Add(aPoints[9]);
                    data.Add(aPoints[10]);
                    data.Add(aPoints[11]);
                    data.Add((T2)(object)0);
                    data.Add((T2)(object)1);
                }
                else
                {
                    // Top Right
                    data.Add(aPoints[0]);
                    data.Add(aPoints[1]);
                    data.Add(aPoints[2]);
                    data.Add(aPoints[3]);
                    data.Add((T2)(object)1);
                    data.Add((T2)(object)1);
                    // Bottom Right
                    data.Add(aPoints[4]);
                    data.Add(aPoints[5]);
                    data.Add(aPoints[6]);
                    data.Add(aPoints[7]);
                    data.Add((T2)(object)1);
                    data.Add((T2)(object)0);
                    // Bottom Left
                    data.Add(aPoints[8]);
                    data.Add(aPoints[9]);
                    data.Add(aPoints[10]);
                    data.Add(aPoints[11]);
                    data.Add((T2)(object)0);
                    data.Add((T2)(object)0);
                    // Top Left
                    data.Add(aPoints[12]);
                    data.Add(aPoints[13]);
                    data.Add(aPoints[14]);
                    data.Add(aPoints[15]);
                    data.Add((T2)(object)0);
                    data.Add((T2)(object)1);
                }
            }

            return data;
        }
    }
}
