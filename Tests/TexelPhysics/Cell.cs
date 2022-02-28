using System;
using Zene.Structs;

namespace TexelPhysics
{
    public struct MovementDirection
    {
        private MovementDirection(int d)
        {
            Direction = d;
        }

        private readonly int Direction;

        public readonly static MovementDirection None = new MovementDirection(0);
        //public readonly static MovementDirection Up = new MovementDirection(1);
        public readonly static MovementDirection Down = new MovementDirection(2);
        public readonly static MovementDirection Left = new MovementDirection(3);
        public readonly static MovementDirection Right = new MovementDirection(4);
        public readonly static MovementDirection DownRight = new MovementDirection(5);
        //public readonly static MovementDirection UpRight = new MovementDirection(6);
        public readonly static MovementDirection DownLeft = new MovementDirection(7);
        //public readonly static MovementDirection UpLeft = new MovementDirection(8);
        //public readonly static MovementDirection Random = new MovementDirection(9);
        //public readonly static MovementDirection RandomStright = new MovementDirection(10);
        //public readonly static MovementDirection RandomDiagonal = new MovementDirection(11);
        public readonly static MovementDirection RandomSide = new MovementDirection(12);
        public readonly static MovementDirection RandomDownSide = new MovementDirection(13);
        //public readonly static MovementDirection RandomUpSide = new MovementDirection(14);
        //public readonly static MovementDirection RandomTopBottom = new MovementDirection(15);
        //public readonly static MovementDirection RandomLeftTopBottom = new MovementDirection(16);
        //public readonly static MovementDirection RandomRightTopBottom = new MovementDirection(17);

        public static bool operator |(MovementDirection a, MovementDirection[] b)
        {
            bool equals = false;

            for (int i = 0; i < b.Length; i++)
            {
                if (a.Direction == b[i].Direction)
                {
                    equals = true;
                    break;
                }

                if ((a.Direction == 3 || a.Direction == 4) && b[i].Direction == 12)
                {
                    equals = true;
                    break;
                }

                if ((a.Direction == 5 || a.Direction == 7) && b[i].Direction == 13)
                {
                    equals = true;
                    break;
                }
            }

            return equals;
        }

        public static bool operator &(MovementDirection a, MovementDirection[] b)
        {
            bool equals = true;

            for (int i = 0; i < b.Length; i++)
            {
                if (a.Direction != b[i].Direction)
                {
                    equals = false;
                    break;
                }
            }

            return equals;
        }

        public static bool operator ==(MovementDirection a, MovementDirection b)
        {
            return a.Direction == b.Direction;
        }
        public static bool operator !=(MovementDirection a, MovementDirection b)
        {
            return a.Direction != b.Direction;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public enum CellType
    {
        Empty,
        Sand,
        Water,
        Static,
        UserDefined,
    }

    public struct Cell
    {
        public Cell(MovementDirection[] md, Colour colour, bool autoMove)
        {
            MovementDirections = md;
            Colour = colour;
            CellType = CellType.UserDefined;
            AutoMove = autoMove;
        }

        private Cell(CellType ct)
        {
            CellType = ct;

            switch (ct)
            {
                case CellType.Sand:
                    MovementDirections = _sandMoves;
                    Colour = World.SandColour;
                    AutoMove = true;
                    return;

                case CellType.Water:
                    MovementDirections = _waterMoves;
                    Colour = World.WaterColour;
                    AutoMove = true;
                    return;

                case CellType.Static:
                    MovementDirections = Array.Empty<MovementDirection>();
                    Colour = World.StaticColour;
                    AutoMove = false;
                    return;

                default:
                    MovementDirections = Array.Empty<MovementDirection>();
                    Colour = Colour.Zero;
                    AutoMove = false;
                    return;
            }
        }

        public MovementDirection[] MovementDirections { get; }

        public CellType CellType { get; }

        public Colour Colour { get; }

        public bool AutoMove { get; }

        private static readonly MovementDirection[] _waterMoves = new MovementDirection[]
        {
            MovementDirection.Down,
            MovementDirection.RandomDownSide,
            MovementDirection.RandomSide
        };
        private static readonly MovementDirection[] _sandMoves = new MovementDirection[]
        {
            MovementDirection.Down,
            MovementDirection.RandomDownSide
        };

        public readonly static Cell Empty = new Cell(CellType.Empty);
        public readonly static Cell Sand = new Cell(CellType.Sand);
        public readonly static Cell Water = new Cell(CellType.Water);
        public readonly static Cell Static = new Cell(CellType.Static);
    }
}
