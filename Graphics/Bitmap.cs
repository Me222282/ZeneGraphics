using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using StbImageSharp;
using Zene.Structs;

namespace Zene.Graphics
{
    public enum ImageEncoding
    {
        Unknown,
        Png,
        Jpeg,
        Bmp,
        Hdr,
        Tga
    }

    public delegate void ForeachBitmapHandler(Colour value, int x, int y);
    public delegate void ForeachColourHandler(Colour value, int i);

    public unsafe class Bitmap : GLArray<Colour>
    {
        public Bitmap(int width, int height)
            : base(width, height)
        {
            Width = width;
            Height = height;
        }
        public Bitmap(int width, int height, Colour[] data)
            : base(width, height, 1, data)
        {
            Width = width;
            Height = height;
        }
        public Bitmap(int width, int height, Colour value)
            : base(width, height, 1)
        {
            Array.Fill(Data, value);

            Width = width;
            Height = height;
        }
        public Bitmap(string path)
            : this(new FileStream(path, FileMode.Open), true)
        {
            
        }
        public Bitmap(Stream stream, bool close = false)
            : base(false)
        {
            ImageResult imageData = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

            if (close) { stream.Close(); }

            Width = imageData.Width;
            Height = imageData.Height;

            SetData(new Colour[imageData.Height * imageData.Width]);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    byte r = imageData.Data[(x * 4) + (y * 4 * Width)];
                    byte g = imageData.Data[(x * 4) + (y * 4 * Width) + 1];
                    byte b = imageData.Data[(x * 4) + (y * 4 * Width) + 2];
                    byte a = imageData.Data[(x * 4) + (y * 4 * Width) + 3];

                    Data[x + (y * Width)] = new Colour(r, g, b, a);
                }
            }
        }

        public override int Width { get; }
        public override int Height { get; }

        public Bitmap SubBitmap(int x, int y, int width, int height)
        {
            Bitmap output = new Bitmap(width, height);

            try
            {
                for (int sx = 0; sx < width; sx++)
                {
                    for (int sy = 0; sy < height; sy++)
                    {
                        output[sx, sy] = this[sx + x, sy + y];
                    }
                }
            }
            catch { throw; }

            return output;
        }
        public Bitmap SubBitmap(IBox box)
        {
            int width = (int)box.Width;
            int height = (int)box.Height;

            Bitmap output = new Bitmap(width, height);

            int x = (int)box.Left;
            int y = Height - (int)box.Top - 1;

            try
            {
                for (int sx = 0; sx < width; sx++)
                {
                    for (int sy = 0; sy < height; sy++)
                    {
                        output[sx, sy] = this[sx + x, sy + y];
                    }
                }
            }
            catch { throw; }

            return output;
        }

        public void FlipHorizontally()
        {
            Colour[] data = new Colour[Height * Width];

            int wm1 = Width - 1;

            // Create flip
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    data[x + (y * Width)] = Data[(wm1 - x) + (y * Width)];
                }
            }

            SetData(data);
        }
        public void FlipVertically()
        {
            Colour[] data = new Colour[Width * Height];

            // Create flip
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    data[x + (y * Width)] = this[x, y];
                }
            }

            SetData(data);
        }

        public Bitmap GetHorizontalFlip()
        {
            Bitmap data = new Bitmap(Width, Height);

            int wm1 = Width - 1;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    data[x, y] = this[wm1 - x, y];
                }
            }

            return data;
        }
        public Bitmap GetVerticalFlip()
        {
            Bitmap data = new Bitmap(Width, Height);

            int hm1 = Height - 1;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    data[x, y] = this[x, hm1 - y];
                }
            }

            return data;
        }
        
        public Span<byte> AsBytes()
        {
            return new Span<byte>(this, Bytes);
        }

        public void Export(string path, ImageEncoding format, bool alpha = true) => Export(new FileStream(path, FileMode.Open), format, alpha, true);
        public void Export(Stream stream, ImageEncoding format, bool alpha = true, bool close = false)
        {
            if (format == ImageEncoding.Unknown)
            {
                throw new Exception("Cannot export to unkown encoding.");
            }

            ImageWriter writer = new ImageWriter();

            // Does the data contain an alpha channel
            ColorComponents storeType = alpha ? ColorComponents.RedGreenBlueAlpha : ColorComponents.RedGreenBlue;

            // Write data in given format
            switch (format)
            {
                case ImageEncoding.Png:
                    writer.WritePng(GetVerticalFlip(), Width, Height, storeType, stream);
                    break;

                case ImageEncoding.Jpeg:
                    writer.WriteJpg(GetVerticalFlip(), Width, Height, storeType, stream, 100);
                    break;

                case ImageEncoding.Bmp:
                    writer.WriteBmp(GetVerticalFlip(), Width, Height, storeType, stream);
                    break;

                case ImageEncoding.Tga:
                    writer.WriteTga(GetVerticalFlip(), Width, Height, storeType, stream);
                    break;

                case ImageEncoding.Hdr:
                    writer.WriteHdr(GetVerticalFlip(), Bytes, Width, Height, storeType, stream);
                    break;
            }

            if (close) { stream.Close(); }
        }

        public void Foreach(ForeachBitmapHandler handler)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                handler(Data[i], i % Width, i / Width);
            }
        }
        public void Foreach(ForeachColourHandler handler)
        {
            for (int i = 0; i < Data.Length; i++)
            {
                handler(Data[i], i);
            }
        }

        public Vector2I GetLocation(int index)
        {
            if (index >= Data.Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            return new Vector2I(index % Width, index / Width);
        }

        public static Bitmap FromArray(int width, int height, byte[] data)
        {
            fixed (byte* ptr = &data[0])
            {
                return FromPointer(width, height, ptr);
            }
        }
        public static Bitmap FromArray(int width, int height, Span<byte> data)
        {
            fixed (byte* ptr = &data[0])
            {
                return FromPointer(width, height, ptr);
            }
        }
        public static Bitmap FromPointer(int width, int height, void* data)
        {
            Colour[] cData = new Colour[width * height];

            byte* bytePtr = (byte*)data;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    byte r = bytePtr[(x * 4) + (y * 4 * width)];
                    byte g = bytePtr[(x * 4) + (y * 4 * width) + 1];
                    byte b = bytePtr[(x * 4) + (y * 4 * width) + 2];
                    byte a = bytePtr[(x * 4) + (y * 4 * width) + 3];

                    cData[x + (y * width)] = new Colour(r, g, b, a);
                }
            }

            return new Bitmap(width, height, cData);
        }
        public static Bitmap FromArray(int width, int height, float[] data)
        {
            Colour[] cData = new Colour[height * width];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float r = data[(x * 4) + (y * 4 * width)];
                    float g = data[(x * 4) + (y * 4 * width) + 1];
                    float b = data[(x * 4) + (y * 4 * width) + 2];
                    float a = data[(x * 4) + (y * 4 * width) + 3];

                    cData[x + (y * width)] = (Colour)new ColourF(r, g, b, a);
                }
            }

            return new Bitmap(width, height, cData);
        }

        public static byte[] ExtractData(string path, out int width, out int height) => ExtractData(new FileStream(path, FileMode.Open), out width, out height, true);
        public static byte[] ExtractData(Stream stream, out int width, out int height, bool close = false)
        {
            ImageResult imageData = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

            if (close) { stream.Close(); }

            width = imageData.Width;
            height = imageData.Height;

            return imageData.Data;
        }

        private static readonly ConcurrentDictionary<Texture2D, KeyValuePair<ImageResult, bool>> _loadingTextures =
            new ConcurrentDictionary<Texture2D, KeyValuePair<ImageResult, bool>>();
        private static readonly List<Texture2D> _textures = new List<Texture2D>();
        public static void LoadTexture(Texture2D texture, Stream stream, bool mipmap)
        {
            if (_loadingTextures.ContainsKey(texture))
            {
                throw new Exception($"{nameof(texture)} is already being worked on.");
            }

            KeyValuePair<ImageResult, bool> defaultPair = new KeyValuePair<ImageResult, bool>(null, mipmap);
            bool added = _loadingTextures.TryAdd(texture, defaultPair);
            _textures.Add(texture);

            if (!added)
            {
                throw new Exception("Couldn't add texture temperarerly to collection.");
            }

            byte[] data = stream.ReadAllBytes();

            Task.Run(() =>
            {
                // Load image
                //ImageResult imageData = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                ImageResult imageData = ImageResult.FromMemory(data, ColorComponents.RedGreenBlueAlpha);

                KeyValuePair<ImageResult, bool> value = new KeyValuePair<ImageResult, bool>(imageData, mipmap);
                
                while (!_loadingTextures.TryUpdate(texture, value, defaultPair))
                {
                    // Content in condition statment
                }
            });
        }

        public static bool CheckTextures()
        {
            foreach (KeyValuePair<Texture2D, KeyValuePair<ImageResult, bool>> texture in _loadingTextures)
            {
                if (texture.Value.Key == null) { continue; }

                texture.Key.SetData(texture.Value.Key.Width, texture.Value.Key.Height, BaseFormat.Rgba,
                    new GLArray<byte>(texture.Value.Key.Width * 4, texture.Value.Key.Height, 1, texture.Value.Key.Data));

                if (texture.Value.Value) { texture.Key.CreateMipMap(); }
            }

            foreach (Texture2D key in _textures)
            {
                if (_loadingTextures.TryGetValue(key, out KeyValuePair<ImageResult, bool> value))
                {
                    if (value.Key == null) { continue; }

                    while (!_loadingTextures.TryRemove(key, out _))
                    {
                        // Content in condition statment
                    }
                }
            }

            return _loadingTextures.IsEmpty;
        }

        public static ImageEncoding GetImageEncoding(string path)
        {
            Stream s = new FileStream(path, FileMode.Open);
            ImageEncoding ie = GetImageEncoding(s);

            s.Close();
            return ie;
        }
        public static ImageEncoding GetImageEncoding(Stream file)
        {
            if (StbImage.stbi__png_test(new StbImage.stbi__context(file)) != 0)
            {
                return ImageEncoding.Png;
            }
            if (StbImage.stbi__bmp_test(new StbImage.stbi__context(file)) != 0)
            {
                return ImageEncoding.Bmp;
            }
            if (StbImage.stbi__jpeg_test(new StbImage.stbi__context(file)) != 0)
            {
                return ImageEncoding.Jpeg;
            }
            if (StbImage.stbi__hdr_test(new StbImage.stbi__context(file)) != 0)
            {
                return ImageEncoding.Hdr;
            }
            if (StbImage.stbi__tga_test(new StbImage.stbi__context(file)) != 0)
            {
                return ImageEncoding.Tga;
            }

            return ImageEncoding.Unknown;
        }

        /// <summary>
        /// Determines whether textures should be fliped on load. Since OpenGL expects textures to be upside down, this should stay be True.
        /// </summary>
        public static bool AutoFlipTextures
        {
            get => StbImage.stbi__vertically_flip_on_load == 1;
            set => StbImage.stbi__vertically_flip_on_load = value ? 1 : 0;
        }
    }
}
