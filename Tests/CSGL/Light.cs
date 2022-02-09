using System;
using Zene.Graphics;
using Zene.Structs;

namespace CSGL
{
    public class Light : IDrawable, IDisposable
    {
        public Light(Vector3 position, double size, BufferUsage usage)
        {
            _disposed = false;

            _vertData = new Vector3[]
            {
                /*Vertex*/ new Vector3((-size) + position.X, size + position.Y, size + position.Z),
                /*Vertex*/ new Vector3(size + position.X, size + position.Y, size + position.Z),
                /*Vertex*/ new Vector3(size + position.X, (-size) + position.Y, size + position.Z),
                /*Vertex*/ new Vector3((-size) + position.X, (-size) + position.Y, size + position.Z),

                /*Vertex*/ new Vector3((-size) + position.X, size + position.Y, (-size) + position.Z),
                /*Vertex*/ new Vector3(size + position.X, size + position.Y, (-size) + position.Z),
                /*Vertex*/ new Vector3(size + position.X, (-size) + position.Y, (-size) + position.Z),
                /*Vertex*/ new Vector3((-size) + position.X, (-size) + position.Y, (-size) + position.Z)
            };

            _drawable = new DrawObject<Vector3, byte>(_vertData, _indexData, 1, 0, AttributeSize.D3, usage);
        }

        private readonly Vector3[] _vertData;

        private readonly byte[] _indexData = new byte[]
        {
            // Front
            0, 1, 2,
            2, 3, 0,

            // Back
            5, 4, 7,
            7, 6, 5,

            // Left
            4, 0, 3,
            3, 7, 4,

            // Right
            1, 5, 6,
            6, 2, 1,

            // Top
            4, 5, 1,
            1, 0, 4,

            // Bottom
            2, 6, 7,
            7, 3, 2
        };

        private readonly DrawObject<Vector3, byte> _drawable;

        public void Draw()
        {
            _drawable.Draw();
        }

        private bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                _drawable.Dispose();
            }
        }
    }
}
