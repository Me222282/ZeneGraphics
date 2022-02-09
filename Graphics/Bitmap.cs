using System;
using System.IO;
using StbImageSharp;
using Zene.Structs;

namespace Zene.Graphics
{
    public enum ImageEncoding
    {
        Png,
        Jpeg,
        Bmp,
        Hdr,
        Tga
    }

    public class Bitmap
    {
        public Bitmap(int width, int height)
        {
            Data = new Colour[height, width];
        }
        public Bitmap(Colour[,] colours)
        {
            Data = colours;
        }
        public Bitmap(string path)
        {
            ImageResult imageData = ImageResult.FromMemory(File.ReadAllBytes(path), ColorComponents.RedGreenBlueAlpha);

            Data = new Colour[imageData.Height, imageData.Width];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    byte r = imageData.Data[(x * 4) + (y * 4 * Width)];
                    byte g = imageData.Data[(x * 4) + (y * 4 * Width) + 1];
                    byte b = imageData.Data[(x * 4) + (y * 4 * Width) + 2];
                    byte a = imageData.Data[(x * 4) + (y * 4 * Width) + 3];

                    Data[y, x] = new Colour(r, g, b, a);
                }
            }
        }
        public Bitmap(Stream stream)
        {
            ImageResult imageData = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

            Data = new Colour[imageData.Width, imageData.Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    byte r = imageData.Data[(x * 4) + (y * 4 * Width)];
                    byte g = imageData.Data[(x * 4) + (y * 4 * Width) + 1];
                    byte b = imageData.Data[(x * 4) + (y * 4 * Width) + 2];
                    byte a = imageData.Data[(x * 4) + (y * 4 * Width) + 3];

                    Data[x, Height - y - 1] = new Colour(r, g, b, a);
                }
            }
        }

        public int Width
        {
            get
            {
                return Data.GetLength(1);
            }
        }
        public int Height
        {
            get
            {
                return Data.GetLength(0);
            }
        }

        public Colour[,] Data { get; set; }
        
        public Colour[,] SubSection(int x, int y, int width, int height)
        {
            Colour[,] data = new Colour[width, height];

            int ry = Height - y - 1;

            for (int sx = 0; sx < width; sx++)
            {
                for (int sy = 0; sy < height; sy++)
                {
                    data[sx, sy] = Data[ry - sy, sx + x];
                }
            }

            return data;
        }
        public Bitmap SubBitmap(int x, int y, int width, int height)
        {
            Bitmap b = new Bitmap(width, height);

            int ry = Height - y - 1;

            for (int sx = 0; sx < width; sx++)
            {
                for (int sy = 0; sy < height; sy++)
                {
                    b.Data[sy, sx] = Data[ry - sy, sx + x];
                }
            }

            return b;
        }

        public void FlipHorizontally()
        {
            int width = Width;
            int height = Height;

            Colour[,] data = new Colour[height, width];

            // Create flip
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    data[y, x] = Data[y, width - x - 1];
                }
            }

            Data = data;
        }
        public void FlipVertically()
        {
            int width = Width;
            int height = Height;

            Colour[,] data = new Colour[width, height];

            // Create flip
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    data[y, x] = Data[height - y - 1, x];
                }
            }

            Data = data;
        }

        public Colour[,] GetHorizontalFlip()
        {
            int width = Width;
            int height = Height;

            Colour[,] data = new Colour[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    data[x, y] = Data[height - y - 1, width - x - 1];
                }
            }

            return data;
        }
        public Colour[,] GetVerticalFlip()
        {
            int width = Width;
            int height = Height;

            Colour[,] data = new Colour[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    data[x, y] = Data[y, x];
                }
            }

            return data;
        }

        public Colour this[int x, int y]
        {
            get => Data[Height - y - 1, x];
            set => Data[Height - y - 1, x] = value;
        }
        
        public byte[] ToBytes()
        {
            int width = Width;
            int height = Height;

            byte[] data = new byte[width * height * 4];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Colour c = Data[y, x];

                    data[(x * 4) + (y * 4 * width)] = c.R;
                    data[(x * 4) + (y * 4 * width) + 1] = c.G;
                    data[(x * 4) + (y * 4 * width) + 2] = c.B;
                    data[(x * 4) + (y * 4 * width) + 3] = c.A;
                }
            }

            return data;
        }
        public float[] ToFloats()
        {
            int width = Width;
            int height = Height;

            float[] data = new float[width * height * 4];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    ColourF c = (ColourF)Data[y, x];

                    data[(x * 4) + (y * 4 * width)] = c.R;
                    data[(x * 4) + (y * 4 * width) + 1] = c.G;
                    data[(x * 4) + (y * 4 * width) + 2] = c.B;
                    data[(x * 4) + (y * 4 * width) + 3] = c.A;
                }
            }

            return data;
        }

        public void Export(string path, ImageEncoding format, bool alpha = true)
        {
            ImageWriter writer = new ImageWriter();

            // The stream to write to
            Stream stream = new FileStream(path, FileMode.Open);

            // Does the data contain an alpha channel
            ColorComponents storeType = alpha ? ColorComponents.RedGreenBlueAlpha : ColorComponents.RedGreenBlue;

            // Get image data
            byte[] data = ToBytes();

            // Write data in given format
            switch (format)
            {
                case ImageEncoding.Png:
                    writer.WritePng(data, Width, Height, storeType, stream);
                    break;

                case ImageEncoding.Jpeg:
                    writer.WriteJpg(data, Width, Height, storeType, stream, 100);
                    break;

                case ImageEncoding.Bmp:
                    writer.WriteBmp(data, Width, Height, storeType, stream);
                    break;

                case ImageEncoding.Tga:
                    writer.WriteTga(data, Width, Height, storeType, stream);
                    break;

                case ImageEncoding.Hdr:
                    writer.WriteHdr(data, Width, Height, storeType, stream);
                    break;
            }

            stream.Close();
        }

        public static Bitmap FromArray(int width, int height, byte[] data)
        {
            Colour[,] cData = new Colour[height, width];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    byte r = data[(x * 4) + (y * 4 * width)];
                    byte g = data[(x * 4) + (y * 4 * width) + 1];
                    byte b = data[(x * 4) + (y * 4 * width) + 2];
                    byte a = data[(x * 4) + (y * 4 * width) + 3];

                    cData[y, x] = new Colour(r, g, b, a);
                }
            }

            return new Bitmap(cData);
        }
        public static Bitmap FromArray(int width, int height, float[] data)
        {
            Colour[,] cData = new Colour[height, width];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float r = data[(x * 4) + (y * 4 * width)];
                    float g = data[(x * 4) + (y * 4 * width) + 1];
                    float b = data[(x * 4) + (y * 4 * width) + 2];
                    float a = data[(x * 4) + (y * 4 * width) + 3];

                    cData[y, x] = (Colour)new ColourF(r, g, b, a);
                }
            }

            return new Bitmap(cData);
        }

        public static byte[] ExtractData(string path, out int width, out int height)
        {
            ImageResult imageData = ImageResult.FromMemory(File.ReadAllBytes(path), ColorComponents.RedGreenBlueAlpha);

            width = imageData.Width;
            height = imageData.Height;

            return imageData.Data;
        }
        public static byte[] ExtractData(Stream stream, out int width, out int height)
        {
            ImageResult imageData = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

            width = imageData.Width;
            height = imageData.Height;

            return imageData.Data;
        }

        public static implicit operator GLArray<Colour>(Bitmap b)
        {
            int width = b.Width;
            int height = b.Height;

            Colour[] data = new Colour[width * height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    data[x + (y * width)] = b.Data[y, x];
                }
            }

            return new GLArray<Colour>(width, height, 1, data);
        }
        public static implicit operator Bitmap(GLArray<Colour> glArray)
        {
            Bitmap b = new Bitmap(glArray.Width, glArray.Height);

            for (int x = 0; x < glArray.Width; x++)
            {
                for (int y = 0; y < glArray.Height; y++)
                {
                    b.Data[y, x] = glArray[x + (y * glArray.Width)];
                }
            }

            return b;
        }
    }
}
