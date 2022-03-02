using System;
using Zene.Structs;

namespace TexelPhysics
{
    public struct Cell
    {
        private static object _id = (double)1;

        public Cell(CellInfo type, Colour colour)
        {
            lock (_id)
            {
                Id = (ulong)((double)_id);
                _id = ((double)_id) + 1;
            }
            Info = type;
            Colour = colour;
            Velocity = Vector2.Zero;
        }

        public ulong Id { get; }
        public CellInfo Info { get; }
        public Colour Colour { get; }

        public Vector2 Velocity { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is CellInfo i &&
                Info == i) ||
                (obj is Cell c &&
                    c.Id == Id);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(Cell c, CellInfo type)
        {
            return c.Info == type;
        }
        public static bool operator !=(Cell c, CellInfo type)
        {
            return c.Info != type;
        }
        public static bool operator ==(Cell l, Cell r)
        {
            return l.Id == r.Id;
        }
        public static bool operator !=(Cell l, Cell r)
        {
            return l.Id != r.Id;
        }

        public static CellInfo Empty { get; } = new CellInfo();
        public static CellInfo Sand { get; } = new CellInfo(5, 2, 1, 3, true);
        public static CellInfo Water { get; } = new CellInfo(2, 0.5, 5, 8, true);
        public static CellInfo Stone { get; } = new CellInfo(50, 100, 50, 6, false);

        public static Cell CreateSand() => new Cell(Sand, World.SandColour);
        public static Cell CreateStone() => new Cell(Stone, World.StoneColour);
        public static Cell CreateWater() => new Cell(Water, World.WaterColour);

        public enum Type
        {
            Empty = 0,
            Sand,
            Water,
            Stone
        }
        public static Cell CreateType(Type type)
        {
            return type switch
            {
                Type.Sand => CreateSand(),
                Type.Stone => CreateStone(),
                Type.Water => CreateWater(),
                _ => new Cell(),
            };
        }
    }
}
