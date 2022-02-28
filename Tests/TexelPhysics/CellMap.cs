using Zene.Graphics;

namespace TexelPhysics
{
    public class CellMap
    {
        public CellMap(int width, int height)
        {
            Width = width;
            Height = height;

            _cells = new Cell[height * width];
        }

        public int Width { get; }
        public int Height { get; }

        private Cell[] _cells;

        public Cell this[int x, int y]
        {
            get => _cells[x + (y * Width)];
            set => _cells[x + (y * Width)] = value;
        }

        public Bitmap ToBitmap()
        {
            Bitmap b = new Bitmap(Width, Height);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    b[x, y] = this[x, y].Colour;
                }
            }

            return b;
        }
        public void CopyFrom(CellMap map)
        {
            map._cells.CopyTo(_cells, 0);
        }
        public void Swap(CellMap map)
        {
            Cell[] old = _cells;
            _cells = map._cells;
            map._cells = old;
        }
    }
}
