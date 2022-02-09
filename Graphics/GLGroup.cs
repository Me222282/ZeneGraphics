using System;

namespace Zene.Graphics
{
    /// <summary>
    /// A 1, 2 or 3 dimensional array of blocks of type <typeparamref name="T"/> stored in the format expected by OpenGL textures.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe class GLGroup<T> where T : unmanaged
    {
        /// <summary>
        /// Creates a reformated array of <paramref name="array"/> with the blocks sized <paramref name="blockSize"/> .
        /// </summary>
        /// <param name="blockSize"></param>
        /// <param name="array"></param>
        public GLGroup(int blockSize, GLArray<T> array)
        {
            if ((array.Width % blockSize) != 0)
            {
                throw new Exception($"{nameof(blockSize)} does not evenly fit inside the width of {nameof(array)}.");
            }

            Data = array.Data;

            Width = array.Width / blockSize;
            Height = array.Height;
            Depth = array.Depth;

            BlockSize = blockSize;
            _realWidth = array.Width;
            _zSize = _realWidth * Height;
        }

        /// <summary>
        /// The width of the array.
        /// </summary>
        /// <remarks>
        /// Note: This is the number of blocks, not literal values.
        /// </remarks>
        public int Width { get; }
        /// <summary>
        /// The height of the array.
        /// </summary>
        public int Height { get; }
        /// <summary>
        /// The depth of the array.
        /// </summary>
        public int Depth { get; }
        /// <summary>
        /// The number of values in each block.
        /// </summary>
        public int BlockSize { get; }

        private readonly int _realWidth;
        private readonly int _zSize;

        /// <summary>
        /// The raw data of the array.
        /// </summary>
        public T[] Data { get; }
        /// <summary>
        /// The length of the raw data in the array.
        /// </summary>
        public int Size
        {
            get
            {
                return Data.Length;
            }
        }
        /// <summary>
        /// The size in bytes of the array.
        /// </summary>
        public int Bytes
        {
            get
            {
                return Data.Length * sizeof(T);
            }
        }

        public T[] this[int index]
        {
            get
            {
                T[] output = new T[BlockSize];

                int start = index * BlockSize;

                for (int i = 0; i < BlockSize; i++)
                {
                    output[i] = Data[start + i];
                }

                return output;
            }
            set
            {
                if (value.Length < BlockSize)
                {
                    throw new Exception($"Cannot set smaller than {nameof(BlockSize)}.");
                }

                int start = index * BlockSize;

                for (int i = 0; i < BlockSize; i++)
                {
                    Data[start + i] = value[i];
                }
            }
        }
        public T[] this[int x, int y]
        {
            get
            {
                T[] output = new T[BlockSize];

                int start = (x * BlockSize) + ((Height - y - 1) * _realWidth);

                for (int i = 0; i < BlockSize; i++)
                {
                    output[i] = Data[start + i];
                }

                return output;
            }
            set
            {
                if (value.Length < BlockSize)
                {
                    throw new Exception($"Cannot set smaller than {nameof(BlockSize)}.");
                }

                int start = (x * BlockSize) + ((Height - y - 1) * _realWidth);

                for (int i = 0; i < BlockSize; i++)
                {
                    Data[start + i] = value[i];
                }
            }
        }
        public T[] this[int x, int y, int z]
        {
            get
            {
                T[] output = new T[BlockSize];

                int start = (x * BlockSize) + ((Height - y - 1) * _realWidth) + (z * _zSize);

                for (int i = 0; i < BlockSize; i++)
                {
                    output[i] = Data[start + i];
                }

                return output;
            }
            set
            {
                if (value.Length < BlockSize)
                {
                    throw new Exception($"Cannot set smaller than {nameof(BlockSize)}.");
                }

                int start = (x * BlockSize) + ((Height - y - 1) * _realWidth) + (z * _zSize);

                for (int i = 0; i < BlockSize; i++)
                {
                    Data[start + i] = value[i];
                }
            }
        }

        /// <summary>
        /// Gets a 1 dimensional section of the array.
        /// </summary>
        /// <param name="offset">The offset into the array.</param>
        /// <param name="size">The size of the sub section.</param>
        /// <returns></returns>
        public GLGroup<T> SubSection(int offset, int size)
        {
            size *= BlockSize;
            offset *= BlockSize;

            GLArray<T> output = new GLArray<T>(size);

            try
            {
                for (int x = 0; x < size; x++)
                {
                    output.Data[x] = Data[x + offset];
                }
            }
            catch { throw; }

            return new GLGroup<T>(BlockSize, output);
        }
        /// <summary>
        /// Gets a 2 dimensional section of the array.
        /// </summary>
        /// <param name="x">The x offset into the array.</param>
        /// <param name="y">The y offset into the array.</param>
        /// <param name="width">The width of the sub section.</param>
        /// <param name="height">The height of the sub section.</param>
        /// <returns></returns>
        public GLGroup<T> SubSection(int x, int y, int width, int height)
        {
            width *= BlockSize;
            x *= BlockSize;

            GLArray<T> output = new GLArray<T>(width, height);

            try
            {
                for (int sx = 0; sx < width; sx++)
                {
                    for (int sy = 0; sy < height; sy++)
                    {
                        output.Data[
                            (sx * BlockSize) +                  // X
                            ((Height - sy - 1) * _realWidth)] = // Y
                            Data[
                                ((sx + x) * BlockSize) +                 // X
                                ((Height - (sy + y) - 1) * _realWidth)]; // Y
                    }
                }
            }
            catch { throw; }

            return new GLGroup<T>(BlockSize, output);
        }
        /// <summary>
        /// Gets a 3 dimensional section of the array.
        /// </summary>
        /// <param name="x">The x offset into the array.</param>
        /// <param name="y">The y offset into the array.</param>
        /// <param name="z">The z offset into the array.</param>
        /// <param name="width">The width of the sub section.</param>
        /// <param name="height">The height of the sub section.</param>
        /// <param name="depth">The depth of the sub section.</param>
        /// <returns></returns>
        public GLGroup<T> SubSection(int x, int y, int z, int width, int height, int depth)
        {
            width *= BlockSize;
            x *= BlockSize;

            GLArray<T> output = new GLArray<T>(width, height, depth);

            try
            {
                for (int sx = 0; sx < width; sx++)
                {
                    for (int sy = 0; sy < height; sy++)
                    {
                        for (int sz = 0; sz < depth; sz++)
                        {
                            output.Data[
                                (sx * BlockSize) +                  // X
                                ((Height - sy - 1) * _realWidth) +  // Y
                                (sz * _zSize)] =                    // Z
                                Data[
                                    ((sx + x) * BlockSize) +                    // X
                                    ((Height - (sy + y)  - 1) * _realWidth) +   // Y
                                    ((sz + z) * _zSize)];                       // Z
                        }
                    }
                }
            }
            catch { throw; }

            return new GLGroup<T>(BlockSize, output);
        }

        public static implicit operator T[](GLGroup<T> glGroup)
        {
            return glGroup.Data;
        }
        public static implicit operator GLArray<T>(GLGroup<T> glGroup)
        {
            return new GLArray<T>(glGroup._realWidth, glGroup.Height, glGroup.Depth, glGroup.Data);
        }

        public static implicit operator T*(GLGroup<T> glGroup)
        {
            fixed (T* ptr = &glGroup.Data[0])
            {
                return ptr;
            }
        }
    }
}
