using System;
using Zene.Graphics;
using Zene.Structs;

namespace TexelPhysics
{
    public class World
    {
        public World(int width, int height)
        {
            Width = width;
            Height = height;

            _worldMapA = new CellMap(Width, Height);
            _worldMapB = new CellMap(Width, Height);
        }

        public int Width { get; }
        public int Height { get; }

        private readonly CellMap _worldMapA;
        private readonly CellMap _worldMapB;

        public MovementDirection[] Randoms { get; } = new MovementDirection[]
        {
            MovementDirection.RandomSide,
            //MovementDirection.RandomUpSide,
            MovementDirection.RandomDownSide,
            //MovementDirection.RandomTopBottom,
            //MovementDirection.RandomLeftTopBottom,
            //MovementDirection.RandomRightTopBottom,
            //MovementDirection.RandomStright,
            //MovementDirection.RandomDiagonal,
            //MovementDirection.Random
        };

        private static readonly Random _r = new Random();

        public bool InBounds(int x, int y)
        {
            return x < Width && y < Height && x > 0 && y > 0;
        }
        public bool MoveCell(int x, int y, MovementDirection movementDirection)
        {
            // Out of bounds value
            if (!InBounds(x, y)) { return false; }

            Cell mover = _worldMapA[x, y];

            // Not a valid movement direction
            if (!(movementDirection | mover.MovementDirections)) { return false; }

            int newX = x;
            int newY = y;

            // Calculate new position
            if (movementDirection == MovementDirection.Down) { newY += 1; }
            else if (movementDirection == MovementDirection.RandomSide)
            {
                bool lr = _r.Next(0, 2) == 0;
                if (lr) { newX -= 1; }
                else { newX += 1; }
            }
            else if (movementDirection == MovementDirection.RandomDownSide)
            {
                bool lr = _r.Next(0, 2) == 0;
                if (lr) { newX -= 1; }
                else { newX += 1; }

                newY += 1;
            }

            // Out of bounds position
            if (!InBounds(newX, newY)) { return false; }

            Cell newPosValue = _worldMapB[newX, newY];

            // New position is valid
            if (newPosValue.CellType == CellType.Empty || (newPosValue.CellType == CellType.Water && mover.CellType != CellType.Water))
            {
                _worldMapB[newX, newY] = mover;
                _worldMapB[x, y] = newPosValue;
                return true;
            }

            return false;
        }
        public bool SetCell(int x, int y, Cell cell)
        {
            if (InBounds(x, y))
            {
                _worldMapA[x, y] = cell;

                return true;
            }

            return false;
        }

        public void UpdateCells()
        {
            // Clear map B ready for writing
            _worldMapB.CopyFrom(_worldMapA);

            for (int x = 0; x < _worldMapA.Width; x++)
            {
                for (int y = 0; y < _worldMapA.Height; y++)
                {
                    int currentX = Width - x - 1;
                    int currentY = Height - y - 1;

                    Cell cell = _worldMapA[currentX, currentY];

                    // Cell doesn't contain data
                    if (cell.CellType == CellType.Empty) { continue; }

                    // Try move cell
                    for (int i = 0; i < cell.MovementDirections.Length; i++)
                    {
                        bool move = MoveCell(currentX, currentY, cell.MovementDirections[i]);

                        if (move) { break; }
                    }
                }
            }

            // Swap cell maps
            _worldMapA.Swap(_worldMapB);
        }

        public Bitmap GetTextureData() => _worldMapA.ToBitmap();

        public readonly static Colour SandColour = new Colour(235, 200, 175);
        public readonly static Colour WaterColour = new Colour(105, 170, 255);
        public readonly static Colour StaticColour = new Colour(150, 150, 150);
    }
}
