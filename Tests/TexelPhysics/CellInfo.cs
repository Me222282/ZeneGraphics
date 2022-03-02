using System;

namespace TexelPhysics
{
    public readonly struct CellInfo
    {
        private static object _id = (uint)1;

        public CellInfo(double spaceS, double r, double s, double m, bool g)
        {
            lock (_id)
            {
                Id = (uint)_id;
                _id = ((uint)_id) + 1;
            }
            SpacingStrength = spaceS;
            Resistance = r;
            Strength = s;
            GravityAffected = g;
            Mass = m;
        }

        public uint Id { get; }
        public double SpacingStrength { get; }
        public double Resistance { get; }
        public double Strength { get; }
        public bool GravityAffected { get; }
        public double Mass { get; }

        public override bool Equals(object obj)
        {
            return obj is CellInfo i &&
                i.Id == Id;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(CellInfo l, CellInfo r)
        {
            return l.Equals(r);
        }
        public static bool operator !=(CellInfo l, CellInfo r)
        {
            return !l.Equals(r);
        }
    }
}
