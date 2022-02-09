using System;
using Zene.Evolution;

namespace EvolutionTest
{
    public struct XPCell : INeuronCell
    {
        public int GetOrder => 0;
        public int SetOrder => throw new NotSupportedException();

        public double GetValue(Lifeform lifeform)
        {
            return (((double)lifeform.Location.X / lifeform.CurrentWorld.Width) * 2) - 1;
            //return (double)lifeform.Location.X / lifeform.CurrentWorld.Width;
        }

        public void SetValue(Lifeform lifeform, double value) => throw new NotSupportedException();
        public void Activate(Lifeform lifeform) => throw new NotSupportedException();
    }
}
