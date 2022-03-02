using System;
using System.Collections.Generic;
using Zene.Structs;

namespace TexelPhysics
{
    public struct CellHolder
    {
        public static uint MaxOverlapCells { get; set; } = 8;

        public CellHolder(int x, int y)
        {
            X = x;
            Y = y;
            _cells = new List<Cell>((int)MaxOverlapCells);
        }

        public int X { get; }
        public int Y { get; }

        private readonly List<Cell> _cells;

        public Cell this[int index]
        {
            get => _cells[index];
            set
            {
                if (value != _cells[index]) { return; }

                _cells[index] = value;
            }
        }
        
        /// <summary>
        /// The minimum force required to push a cell into this space.
        /// </summary>
        public double Resistance
        {
            get
            {
                double total = 0;

                for (int i = 0; i < _cells.Count; i++)
                {
                    total += _cells[i].Info.SpacingStrength;
                }

                return total;
            }
        }
        public Colour CombinedColour
        {
            get
            {
                ColourF total = ColourF.Zero;

                for (int i = 0; i < _cells.Count; i++)
                {
                    //total += _cells[i].Colour;
                    total.R *= _cells[i].Colour.R;
                    total.G *= _cells[i].Colour.G;
                    total.B *= _cells[i].Colour.B;
                }

                return (Colour)total;
            }
        }

        public bool Empty => _cells[0] == Cell.Empty;

        public bool CanPush(double force)
        {
            return (_cells.Count < MaxOverlapCells) && (force >= Resistance);
        }

        public bool Push(Cell cell, double force)
        {
            // Cannot add any more cells
            if (_cells.Count >= MaxOverlapCells) { return false; }
            // Force wasn't strong enough to push into this cell space
            if (force < Resistance) { return false; }

            _cells.Add(cell);
            return true;
        }
        public void Pull(int index)
        {
            if (index >= MaxOverlapCells) { throw new IndexOutOfRangeException(); }

            _cells.RemoveAt(index);
        }

        public void Override(Cell cell)
        {
            _cells.Clear();
            _cells.Capacity = (int)MaxOverlapCells;
            _cells.Add(cell);
        }
    }
}
