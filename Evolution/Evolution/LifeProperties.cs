using System;

namespace Zene.Evolution
{
    public struct LifeProperties
    {
        public static int NeuronValueNumber { get; set; } = 0;

        public LifeProperties(int seed)
        {
            Children = 1;
            NeuronValues = new double[NeuronValueNumber];
        }

        public double Children { get; set; }
        public double[] NeuronValues { get; set; }

        public void ClearNeuronValues()
        {
            NeuronValues = new double[NeuronValueNumber];
        }

        public override bool Equals(object obj)
        {
            return obj is LifeProperties p &&
                   Children == p.Children &&
                   NeuronValues == p.NeuronValues;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Children, NeuronValues);
        }

        public static bool operator ==(LifeProperties a, LifeProperties b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(LifeProperties a, LifeProperties b)
        {
            return !a.Equals(b);
        }
    }
}
