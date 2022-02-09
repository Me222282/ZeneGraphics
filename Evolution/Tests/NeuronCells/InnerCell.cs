using System;
using Zene.Evolution;

namespace EvolutionTest
{
    public struct InnerCell : INeuronCell
    {
        public InnerCell(int neuronAllocant)
        {
            NeuronAllocant = neuronAllocant;
        }

        // The allocated position in the LifeformProperties.NeuronValues array.
        public readonly int NeuronAllocant;

        public int GetOrder => 10;
        public int SetOrder => 0;

        public double GetValue(Lifeform lifeform) => Math.Tanh(lifeform.Properties.NeuronValues[NeuronAllocant]);

        public void SetValue(Lifeform lifeform, double value)
        {
            lifeform.Properties.NeuronValues[NeuronAllocant] += value;
        }

        public void Activate(Lifeform lifeform) { return; }

        public static void Add()
        {
            NeuralNetwork.PosibleGetCells.Add(new InnerCell(NeuralNetwork.PosibleSetCells.Count));
            NeuralNetwork.PosibleSetCells.Add(new InnerCell(NeuralNetwork.PosibleSetCells.Count));
        }
    }
}
