using System;
using Zene.Graphics;
using Zene.Structs;

namespace TexelPhysics
{
    public class World
    {
        public World(int width, int height)
        {
            _worldMapA = new CellMap(width, height);
            _worldMapB = new CellMap(width, height);
        }

        public int Width => _worldMapA.Width;
        public int Height => _worldMapA.Height;

        private readonly CellMap _worldMapA;
        private readonly CellMap _worldMapB;

        private static readonly Random _r = new Random();

        public bool InBounds(int x, int y)
        {
            return x < Width && y < Height && x > 0 && y > 0;
        }

        public static Vector2 Gravity { get; set; } = new Vector2(1, 0);

        private static Vector2I Movment(Cell cell, Vector2I location, CellMap map)
        {
            throw new Exception();
        }
        private static Vector2I Position(Vector2I start, Vector2 end, CellMap map, double force)
        {
            Line2 l = new Line2(new Segment2(start, end));

            Vector2I current = start;
            bool stop = false;
            while (!stop)
            {
                
            }

            return current;
        }

        public void OverrideCell(int x, int y, Cell cell) => _worldMapA.OverrideCell(x, y, cell);

        private void MoveCell(Vector2I location, Cell cell)
        {

        }

        public void UpdateCells()
        {
            // Clear map B ready for writing
            _worldMapB.CopyFrom(_worldMapA);

            _worldMapA.Foreach(cellHolder =>
            {
                if (cellHolder.Empty) { return; }

                Cell c0 = cellHolder[0];

                if (c0.Info.GravityAffected)
                {
                    c0.Velocity += Gravity;
                    cellHolder[0] = c0;
                }


            });

            // Swap cell maps
            _worldMapA.Swap(_worldMapB);
        }

        public Bitmap GetTextureData() => _worldMapA.ToBitmap();

        public readonly static Colour SandColour = new Colour(235, 200, 175);
        public readonly static Colour WaterColour = new Colour(105, 170, 255);
        public readonly static Colour StoneColour = new Colour(150, 150, 150);
    }
}
