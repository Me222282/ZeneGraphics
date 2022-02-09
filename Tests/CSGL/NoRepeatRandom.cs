using System;
using System.Collections.Generic;

namespace CSGL
{
    public class NoRepeatRandom
    {
        private Random r = new Random();

        private List<int> ints;

        private int _min;
        private int _max;

        public void Set(int min, int max)
        {
            _min = min;
            _max = max;

            ints = new List<int>();

            for (int i = min; i < max + 1; i++) { ints.Add(i); }
        }

        public int Next()
        {
            if (ints.Count == 0) { Set(_min, _max); }

            int i = r.Next(0, ints.Count);

            int n = ints[i];

            ints.RemoveAt(i);

            return n;
        }
    }
}
