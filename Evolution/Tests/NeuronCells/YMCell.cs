using System;
using Zene.Evolution;
using Zene.Structs;

namespace EvolutionTest
{
    public struct YMCell : INeuronCell
    {
        public YMCell(int neuronAllocant)
        {
            NeuronAllocant = neuronAllocant;
        }

        // The allocated position in the LifeformProperties.NeuronValues array.
        public readonly int NeuronAllocant;

        public int GetOrder => throw new NotSupportedException();
        public int SetOrder => 1;

        public double GetValue(Lifeform lifeform) => throw new NotSupportedException();

        public void SetValue(Lifeform lifeform, double value)
        {
            lifeform.Properties.NeuronValues[NeuronAllocant] += value;
        }

        public void Activate(Lifeform lifeform)
        {
            double value = Math.Tanh(lifeform.Properties.NeuronValues[NeuronAllocant]);

            if (value == 0) { return; }

            if (value > 0)
            {
                if (Lifeform.OneInChance(value))
                {
                    lifeform.Shift(new Vector2I(0, 1));
                    return;
                }
            }

            if (Lifeform.OneInChance(-value))
            {
                lifeform.Shift(new Vector2I(0, -1));
            }
        }

        public static void Add()
        {
            NeuralNetwork.PosibleSetCells.Add(new YMCell(NeuralNetwork.PosibleSetCells.Count));
        }
    }
}
