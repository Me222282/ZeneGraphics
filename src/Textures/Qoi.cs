using System;
using System.IO;
using Zene.Structs;

namespace Zene.Graphics
{
    internal class Qoi
    {
        private const byte _opRGB = 0b_11111110;
        private const byte _opRGBA = 0b_11111111;
        private const byte _opIndex = 0b_00000000;
        private const byte _opDiff = 0b_01000000;
        private const byte _opLuma = 0b_10000000;
        private const byte _opRun = 0b_11000000;

        private const byte _mask = 0b_11000000;
        private const byte _get2Bits = 0b_00000011;
        private const byte _get4Bits = 0b_00001111;
        /*
        public static Stream Encode(GLArray<Colour> data, bool inculdeAlpha)
        {
            if (data.Depth > 1)
            {
                throw new ArgumentException("Encoder doesn't support 3D image data.");
            }

            MemoryStream ms = new MemoryStream();

            Colour[] array = new Colour[64];

            // Magic bytes defining that file is of type qoi
            ms.Write(_magic, 0, 4);
            ms.Position = 4;

            for (int i = 0; i < data.Size; i++)
            {

            }
        }
        */
        public static GLArray<Colour> Decode(Stream stream)
        {
            // Read header
            byte[] header = new byte[14];
            stream.Read(header, 0, 14);

            if (!CheckHeader(header))
            {
                throw new ArgumentException("Data doesn't contain a valid QOI file.");
            }

            int width = BitConverter.ToInt32(header, 4);
            int height = BitConverter.ToInt32(header, 8);

            if (width == 0 || height == 0)
            {
                throw new ArgumentException("Data doesn't contain a valid QOI file. The size is 0.");
            }

            GLArray<Colour> data = new GLArray<Colour>(width, height);

            Colour[] array = new Colour[64];

            int i = 0;
            while (i < data.Length)
            {
                PassValue(data, array, ref i, stream);
            }

            if (!CheckEnding(stream))
            {
                throw new ArgumentException("Data doesn't contain a valid QOI file. The size is too small for data");
            }

            return data;
        }

        private static void PassValue(GLArray<Colour> data, Colour[] array, ref int index, Stream stream)
        {
            byte b = GetNextByte(stream);

            Colour c;

            if (b == _opRGB)
            {
                c = new Colour(
                    GetNextByte(stream),
                    GetNextByte(stream),
                    GetNextByte(stream),
                    data[index - 1].A);
            }
            else if (b == _opRGBA)
            {
                c = new Colour(
                    GetNextByte(stream),
                    GetNextByte(stream),
                    GetNextByte(stream),
                    GetNextByte(stream));
            }
            else if ((b & _mask) == _opIndex)
            {
                c = array[b];
            }
            else if ((b & _mask) == _opDiff)
            {
                Colour pre = data[index - 1];

                c = new Colour(
                    Plus(pre.R, ((b >> 4) & _get2Bits) - 2),
                    Plus(pre.G, ((b >> 2) & _get2Bits) - 2),
                    Plus(pre.B, (b & _get2Bits) - 2),
                    pre.A);
            }
            else if ((b & _mask) == _opLuma)
            {
                Colour pre = data[index - 1];

                int g = (b ^ _opLuma) - 32;

                c = new Colour(
                    Plus(pre.R, (((b >> 4) & _get4Bits) - 8) + g),
                    Plus(pre.G, g),
                    Plus(pre.B, ((b & _get4Bits) - 8) + g),
                    pre.A);
            }
            // _opRun
            else
            {
                Colour pre = data[index - 1];
                int length = (b ^ _opRun) + 1;

                for (int i = 0; i < length; i++)
                {
                    data[index + i] = pre;
                }

                index += length;
                return;
            }

            data[index] = c;
            array[GetIndex(c)] = c;
            index++;
        }

        private static byte GetNextByte(Stream stream)
        {
            int val = stream.ReadByte();

            if (val < 0)
            {
                throw new ArgumentException("Data doesn't contain a valid QOI file. Not enough data to match size.");
            }

            return (byte)val;
        }

        private static bool CheckHeader(byte[] data)
        {
            return data[0] == 'q' &&
                data[1] == 'o' &&
                data[2] == 'i' &&
                data[3] == 'f';
        }
        private static bool CheckEnding(Stream stream)
        {
            // 7 0x00
            return stream.ReadByte() == 0 &&
                stream.ReadByte() == 0 &&
                stream.ReadByte() == 0 &&
                stream.ReadByte() == 0 &&
                stream.ReadByte() == 0 &&
                stream.ReadByte() == 0 &&
                stream.ReadByte() == 0 &&
                // 1 0x01
                stream.ReadByte() == 1;
        }

        /// <summary>
        /// Stupid microsoft cannot implement basic operation
        /// </summary>
        private static byte Plus(byte l, int r)
        {
            return (byte)((l + r) % 256);
        }

        private static int GetIndex(Colour value)
        {
            return ((value.R * 3) + (value.G * 5) + (value.B * 7) + (value.A * 11)) % 64;
        }
        private static int GetIndex(Colour3 value)
        {
            return ((value.R * 3) + (value.G * 5) + (value.B * 7)) % 64;
        }
    }
}
