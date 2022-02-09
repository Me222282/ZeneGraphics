using System;
using Zene.Graphics;

namespace CSGL
{
    public struct BitmapD
    {
        public BitmapD(int width, int height)
        {
            Width = width;
            Height = height;

            _data = new double[width, height];
        }

        public BitmapD(int width, int height, double[,] data)
        {
            Width = width;
            Height = height;

            if (data.Length != (width * height))
            {
                throw new Exception("Given colour data doesn't fit given width and height.");
            }

            _data = data;
        }

        public BitmapD(Bitmap b, double offset, double scale)
        {
            Width = b.Width;
            Height = b.Height;

            _data = new double[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _data[x, y] = (b[x, y].R * scale) + offset;
                }
            }
        }

        public unsafe BitmapD(string path)
        {
            byte[] data = Bitmap.ExtractData(path, out int width, out int height);

            _data = new double[width, height];

            Width = width;
            Height = height;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    byte r = data[(x * 4) + (y * 4 * width)];
                    byte g = data[(x * 4) + (y * 4 * width) + 1];
                    byte b = data[(x * 4) + (y * 4 * width) + 2];
                    byte a = data[(x * 4) + (y * 4 * width) + 3];

                    _data[x, height - y - 1] = BitConverter.ToSingle(new byte[] { r, g, b, a }, 0);
                }
            }
        }

        public int Width { get; }

        public int Height { get; }

        private readonly double[,] _data;

        public void SetValue(int x, int y, double value)
        {
            x = x < Width ? x : Width - 1;
            x = x < 0 ? 0 : x;

            y = y < Height ? y : Height - 1;
            y = y < 0 ? 0 : y;

            _data[x, y] = value;
        }

        public double GetValue(int x, int y)
        {
            x = x < Width ? x : Width - 1;
            x = x < 0 ? 0 : x;

            y = y < Height ? y : Height - 1;
            y = y < 0 ? 0 : y;

            return _data[x, y];
        }
    }
}
