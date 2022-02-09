using System;

namespace Zene.Evolution
{
    public struct Gene
    {
        private Gene(short src, short des, int str)
        {
            Source = src;
            Destination = des;
            Strength = str;
        }

        public short Source { get; }
        public short Destination { get; }
        public int Strength { get; }

        public Gene CreateChild()
        {
            short source = Source;
            short destination = Destination;
            int strength = Strength;

            if (Lifeform.OneInChance(MutationChance))
            {
                //Console.WriteLine("Mutation");

                int mutant = Lifeform.Random.Next(0, 3);
                short change = (short)Lifeform.Random.Next(0, 2);

                // Make sure that the change is either -1 or +1
                if (change == 0) { change--; }

                switch (mutant)
                {
                    case 0:
                        source += change;
                        break;

                    case 1:
                        destination += change;
                        break;

                    case 2:
                        strength += change;
                        break;
                }
            }

            return new Gene(
                source,
                destination,
                strength);
        }

        public static double MutationChance { get; set; }

        public static Gene Generate(int seed)
        {
            Random r = new Random(seed);

            return new Gene(
                (short)r.Next(short.MaxValue),
                (short)r.Next(short.MaxValue),
                r.Next());
        }

        public static Gene Generate()
        {
            short a;
            short b;
            int c;

            lock (Lifeform.RandSync)
            {
                a = (short)Lifeform.Random.Next(short.MaxValue);
                b = (short)Lifeform.Random.Next(short.MaxValue);
                c = Lifeform.Random.Next();
            }

            return new Gene(a, b, c);
        }
    }
}
