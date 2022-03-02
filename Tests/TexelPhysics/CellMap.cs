using Zene.Graphics;

namespace TexelPhysics
{
    public class CellMap
    {
        public CellMap(int width, int height)
        {
            Width = width;
            Height = height;

            _cells = new CellHolder[width * height];

            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i] = new CellHolder(i % Width, i / Width);
            }
        }

        public int Width { get; }
        public int Height { get; }

        private CellHolder[] _cells;

        public CellHolder this[int x, int y]
        {
            get => _cells[x + (y * Width)];
        }
        public Cell this[int x, int y, int cell]
        {
            get => _cells[x + (y * Width)][cell];
        }

        public bool Push(int x, int y, Cell cell, double force) => _cells[x + (y * Width)].Push(cell, force);

        public delegate void ForeachHandler(CellHolder cell);
        public void Foreach(ForeachHandler method)
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                method(_cells[i]);
            }
        }

        public Bitmap ToBitmap()
        {
            Bitmap b = new Bitmap(Width, Height);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    b[x, y] = this[x, y].CombinedColour;
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
            CellHolder[] old = _cells;
            _cells = map._cells;
            map._cells = old;
        }

        public void OverrideCell(int x, int y, Cell cell) => _cells[x + (y * Width)].Override(cell);
    }
}
