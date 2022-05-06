using System;
using System.IO;
using Zene.Structs;

namespace Zene.Graphics
{
    public class Bitmap3
    {
        public Bitmap3(int width, int height, int depth)
        {
            Data = new Colour[depth, height, width];
        }
        public Bitmap3(Colour[,,] colours)
        {
            Data = colours;
        }

        public int Width
        {
            get
            {
                return Data.GetLength(2);
            }
        }
        public int Height
        {
            get
            {
                return Data.GetLength(1);
            }
        }
        public int Depth
        {
            get
            {
                return Data.GetLength(0);
            }
        }

        public Colour[,,] Data { get; set; }

        public Colour[,,] SubSection(int x, int y, int z, int width, int height, int depth)
        {
            Colour[,,] data = new Colour[width, height, depth];

            int ry = Height - y - 1;

            for (int sx = 0; sx < width; sx++)
            {
                for (int sy = 0; sy < height; sy++)
                {
                    for (int sz = 0; sz < depth; sz++)
                    {
                        data[sx, sy, sz] = Data[sz + z, ry - sy, sx + x];
                    }
                }
            }

            return data;
        }
        public Bitmap3 SubBitmap(int x, int y, int z, int width, int height, int depth)
        {
            Bitmap3 b = new Bitmap3(width, height, depth);

            int ry = Height - y - 1;

            for (int sx = 0; sx < width; sx++)
            {
                for (int sy = 0; sy < height; sy++)
                {
                    for (int sz = 0; sz < depth; sz++)
                    {
                        b.Data[sz, sy, sx] = Data[sz + z, ry - sy, sx + x];
                    }
                }
            }

            return b;
        }

        public void FlipHorizontally()
        {
            int width = Width;
            int height = Height;
            int depth = Depth;

            Colour[,,] data = new Colour[width, height, depth];

            // Create flip
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        data[z, y, x] = Data[z, y, width - x - 1];
                    }
                }
            }

            Data = data;
        }
        public void FlipVertically()
        {
            int width = Width;
            int height = Height;
            int depth = Depth;

            Colour[,,] data = new Colour[width, height, depth];

            // Create flip
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int z = 0; z < Depth; z++)
                    {
                        data[z, y, x] = Data[z, Height - y - 1, x];
                    }
                }
            }

            Data = data;
        }
        public void FlipDepth()
        {
            int width = Width;
            int height = Height;
            int depth = Depth;

            Colour[,,] data = new Colour[width, height, depth];

            // Create flip
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        data[z, y, x] = Data[depth - z - 1, y, x];
                    }
                }
            }

            Data = data;
        }

        public Colour[,,] GetHorizontalFlip()
        {
            int width = Width;
            int height = Height;
            int depth = Depth;

            Colour[,,] data = new Colour[width, height, depth];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        data[x, y, z] = Data[z, height - y - 1, width - x - 1];
                    }
                }
            }

            return data;
        }
        public Colour[,,] GetVerticalFlip()
        {
            int width = Width;
            int height = Height;
            int depth = Depth;

            Colour[,,] data = new Colour[width, height, depth];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        data[x, y, z] = Data[z, y, x];
                    }
                }
            }

            return data;
        }
        public Colour[,,] GetDepthFlip()
        {
            int width = Width;
            int height = Height;
            int depth = Depth;

            Colour[,,] data = new Colour[width, height, depth];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        data[x, y, z] = Data[depth - z - 1, height - y - 1, x];
                    }
                }
            }

            return data;
        }

        public Colour this[int x, int y, int z]
        {
            get => Data[z, Height - y - 1, x];
            set => Data[z, Height - y - 1, x] = value;
        }

        public byte[] ToArray()
        {
            int width = Width;
            int height = Height;
            int depth = Depth;

            int size2 = width * height;

            byte[] data = new byte[width * height * depth * 4];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        Colour c = Data[z, y, x];

                        data[(x * 4) + (y * 4 * width) + (z * 4 * size2)] = c.R;
                        data[(x * 4) + (y * 4 * width) + (z * 4 * size2) + 1] = c.G;
                        data[(x * 4) + (y * 4 * width) + (z * 4 * size2) + 2] = c.B;
                        data[(x * 4) + (y * 4 * width) + (z * 4 * size2) + 3] = c.A;
                    }
                }
            }

            return data;
        }
        public float[] ToFloatArray()
        {
            int width = Width;
            int height = Height;
            int depth = Depth;

            int size2 = width * height;

            float[] data = new float[width * height * depth * 4];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        ColourF c = (ColourF)Data[z, y, x];

                        data[(x * 4) + (y * 4 * width) + (z * 4 * size2)] = c.R;
                        data[(x * 4) + (y * 4 * width) + (z * 4 * size2) + 1] = c.G;
                        data[(x * 4) + (y * 4 * width) + (z * 4 * size2) + 2] = c.B;
                        data[(x * 4) + (y * 4 * width) + (z * 4 * size2) + 3] = c.A;
                    }
                }
            }

            return data;
        }

        public static Bitmap3 FromArray(int width, int height, int depth, byte[] data)
        {
            Colour[,,] cData = new Colour[width, height, depth];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        byte r = data[(x * 4) + (y * 4 * width) + (z * 4 * width * height)];
                        byte g = data[(x * 4) + (y * 4 * width) + (z * 4 * width * height) + 1];
                        byte b = data[(x * 4) + (y * 4 * width) + (z * 4 * width * height) + 2];
                        byte a = data[(x * 4) + (y * 4 * width) + (z * 4 * width * height) + 3];

                        cData[z, y, x] = new Colour(r, g, b, a);
                    }
                }
            }

            return new Bitmap3(cData);
        }
        public static Bitmap3 FromArray(int width, int height, int depth, float[] data)
        {
            Colour[,,] cData = new Colour[width, height, depth];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        float r = data[(x * 4) + (y * 4 * width) + (z * 4 * width * height)];
                        float g = data[(x * 4) + (y * 4 * width) + (z * 4 * width * height) + 1];
                        float b = data[(x * 4) + (y * 4 * width) + (z * 4 * width * height) + 2];
                        float a = data[(x * 4) + (y * 4 * width) + (z * 4 * width * height) + 3];

                        cData[z, y, x] = (Colour)new ColourF(r, g, b, a);
                    }
                }
            }

            return new Bitmap3(cData);
        }

        public static implicit operator GLArray<Colour>(Bitmap3 b)
        {
            int width = b.Width;
            int height = b.Height;
            int depth = b.Depth;

            int size2 = width * height;

            Colour[] data = new Colour[width * height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        data[x + (y * width) + (z * size2)] = b.Data[z, y, x];
                    }
                }
            }

            return new GLArray<Colour>(width, height, depth, data);
        }
        public static implicit operator Bitmap3(GLArray<Colour> glArray)
        {
            Bitmap3 b = new Bitmap3(glArray.Width, glArray.Height, glArray.Depth);

            int size2 = glArray.Width * glArray.Height;

            for (int x = 0; x < glArray.Width; x++)
            {
                for (int y = 0; y < glArray.Height; y++)
                {
                    for (int z = 0; z < glArray.Depth; z++)
                    {
                        b.Data[z, y, x] = glArray[x + (y * glArray.Width) + (z * size2)];
                    }
                }
            }

            return b;
        }
    }
}
