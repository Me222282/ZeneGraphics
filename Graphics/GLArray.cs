using System;
using System.Collections;
using System.Collections.Generic;
using Zene.Structs;

namespace Zene.Graphics
{
    /// <summary>
    /// A 1, 2 or 3 dimensional array of type <typeparamref name="T"/> stored in the format expected by OpenGL textures.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe class GLArray<T> : IEnumerable<T> where T : unmanaged
    {
        /// <summary>
        /// Creates an array from raw values.
        /// </summary>
        /// <param name="width">The width of the array.</param>
        /// <param name="height">The height of the array.</param>
        /// <param name="depth">The depth of the array.</param>
        /// <param name="values">The raw data to be stored in the array.</param>
        public GLArray(int width, int height, int depth, params T[] values)
        {
            if (values.Length != width * height * depth)
            {
                throw new Exception($"The data in {nameof(values)} doesn't match the given size.");
            }

            if (width < 1 || height < 1 || depth < 1)
            {
                throw new Exception($"{nameof(width)}, {nameof(height)} and {nameof(depth)} must be greater than 0.");
            }

            Width = width;
            Height = height;
            Depth = depth;
            _zSize = Width * Height;

            _data = values;
        }
        /// <summary>
        /// Creates an empty 1 dimensional array.
        /// </summary>
        /// <param name="length">The width of the aray.</param>
        public GLArray(int length)
        {
            _data = new T[length];

            if (length < 1)
            {
                throw new Exception($"{nameof(length)} must be greater than 0.");
            }

            Width = length;
            Height = 1;
            Depth = 1;
            _zSize = Width;
        }
        /// <summary>
        /// Creates an empty 2 dimensional array.
        /// </summary>
        /// <param name="width">The width of the array.</param>
        /// <param name="height">The height of the array.</param>
        public GLArray(int width, int height)
        {
            _data = new T[width * height];

            if (width < 1 || height < 1)
            {
                throw new Exception($"{nameof(width)} and {nameof(height)} must be greater than 0.");
            }

            Width = width;
            Height = height;
            Depth = 1;
            _zSize = Width * Height;
        }
        /// <summary>
        /// Creates an empty 3 dimensional array.
        /// </summary>
        /// <param name="width">The width of the array.</param>
        /// <param name="height">The height of the array.</param>
        /// <param name="depth">The depth of the array.</param>
        public GLArray(int width, int height, int depth)
        {
            _data = new T[width * height * depth];

            if (width < 1 || height < 1 || depth < 1)
            {
                throw new Exception($"{nameof(width)}, {nameof(height)} and {nameof(depth)} must be greater than 0.");
            }

            Width = width;
            Height = height;
            Depth = depth;
            _zSize = Width * Height;
        }

        protected GLArray(bool empty)
        {
            if (empty)
            {
                _data = Array.Empty<T>();
                Width = 0;
                Height = 0;
                Depth = 0;
                _zSize = 0;
            }
        }

        /// <summary>
        /// The width of the array.
        /// </summary>
        public virtual int Width { get; } = 1;
        /// <summary>
        /// The height of the array.
        /// </summary>
        public virtual int Height { get; } = 1;
        /// <summary>
        /// The depth of the array.
        /// </summary>
        public virtual int Depth { get; } = 1;
        protected readonly int _zSize;

        private T[] _data;
        /// <summary>
        /// The raw data of the array.
        /// </summary>
        public T[] Data => _data;
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

        protected void SetData(T[] values)
        {
            if (values.Length != Width * Height * Depth)
            {
                throw new Exception($"The data in {nameof(values)} doesn't match the given size.");
            }

            _data = values;
        }

        public virtual T this[int index]
        {
            get
            {
                return Data[index];
            }
            set
            {
                Data[index] = value;
            }
        }
        public virtual T this[int x, int y]
        {
            get
            {
                return Data[x + ((Height - y - 1) * Width)];
            }
            set
            {
                Data[x + ((Height - y - 1) * Width)] = value;
            }
        }
        public virtual T this[int x, int y, int z]
        {
            get
            {
                return Data[x + ((Height - y - 1) * Width) + (z * _zSize)];
            }
            set
            {
                Data[x + ((Height - y - 1) * Width) + (z * _zSize)] = value;
            }
        }

        /// <summary>
        /// Gets a 1 dimensional section of the array.
        /// </summary>
        /// <param name="offset">The offset into the array.</param>
        /// <param name="size">The size of the sub section.</param>
        /// <returns></returns>
        public GLArray<T> SubSection(int offset, int size)
        {
            GLArray<T> output = new GLArray<T>(size);

            try
            {
                for (int x = 0; x < size; x++)
                {
                    output[x] = Data[x + offset];
                }
            }
            catch { throw; }

            return output;
        }
        /// <summary>
        /// Gets a 2 dimensional section of the array.
        /// </summary>
        /// <param name="x">The x offset into the array.</param>
        /// <param name="y">The y offset into the array.</param>
        /// <param name="width">The width of the sub section.</param>
        /// <param name="height">The height of the sub section.</param>
        /// <returns></returns>
        public GLArray<T> SubSection(int x, int y, int width, int height)
        {
            GLArray<T> output = new GLArray<T>(width, height);

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
        /// <summary>
        /// Gets a 2 dimensional section of the array.
        /// </summary>
        /// <param name="box">The bounding box to source from.</param>
        /// <returns></returns>
        public GLArray<T> SubSection(IBox box)
        {
            int width = (int)box.Width;
            int height = (int)box.Height;

            GLArray<T> output = new GLArray<T>(width, height);

            int x = (int)box.Left;
            int y = (int)box.Bottom;

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
        public GLArray<T> SubSection(int x, int y, int z, int width, int height, int depth)
        {
            GLArray<T> output = new GLArray<T>(width, height, depth);

            try
            {
                for (int sx = 0; sx < width; sx++)
                {
                    for (int sy = 0; sy < height; sy++)
                    {
                        for (int sz = 0; sz < depth; sz++)
                        {
                            output[sx, sy, sz] = this[sx + x, sy + y, sz + z];
                        }
                    }
                }
            }
            catch { throw; }

            return output;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)Data).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Data.GetEnumerator();
        /*
        private int _current = 0;
        /// <summary>
        /// This is just for initialisation. <see cref="GLArray{T}"/> is not a dynamic array.
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {
            if (_current >= Data.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            Data[_current] = value;
            _current++;
        }
        */
        public static implicit operator T[](GLArray<T> glArray)
        {
            return glArray.Data;
        }
        public static explicit operator GLArray<T>(T[,] array)
        {
            int width = array.GetLength(0);
            int height = array.GetLength(1);

            T[] data = new T[width * height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    data[x + (y * width)] = array[x, height - y - 1];
                }
            }

            return new GLArray<T>(width, height, 1, data);
        }
        public static explicit operator GLArray<T>(T[,,] array)
        {
            int width = array.GetLength(0);
            int height = array.GetLength(1);
            int depth = array.GetLength(2);

            T[] data = new T[width * height * depth];

            int size2 = width * height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        data[x + (y * width) + (z * size2)] = array[x, height - y - 1, z];
                    }
                }
            }

            return new GLArray<T>(width, height, depth, data);
        }
        public static explicit operator GLArray<T>(T[][,] array)
        {
            int width = array[0].GetLength(0);
            int height = array[0].GetLength(1);
            int depth = array.Length;

            T[] data = new T[width * height * depth];

            int size2 = width * height;

            try
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int z = 0; z < depth; z++)
                        {
                            data[x + (y * width) + (z * size2)] = array[z][x, height - y - 1];
                        }
                    }
                }
            }
            catch { throw; }

            return new GLArray<T>(width, height, depth, data);
        }
        public static explicit operator GLArray<T>(T[][] array)
        {
            int width = array[0].Length;
            int height = array.Length;

            T[] data = new T[width * height];

            try
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        data[x + (y * width)] = array[height - y - 1][x];
                    }
                }
            }
            catch { throw; }

            return new GLArray<T>(width, height, 1, data);
        }

        public static implicit operator T*(GLArray<T> glArray)
        {
            if (glArray.Data.Length == 0)
            {
                return (T*)IntPtr.Zero;
            }

            fixed (T* ptr = &glArray.Data[0])
            {
                return ptr;
            }
        }
        public static implicit operator IntPtr(GLArray<T> glArray)
        {
            if (glArray.Data.Length == 0)
            {
                return IntPtr.Zero;
            }

            fixed (T* ptr = &glArray.Data[0])
            {
                return (IntPtr)ptr;
            }
        }

        public static GLArray<T> Empty { get; } = new GLArray<T>(true);
    }
}
