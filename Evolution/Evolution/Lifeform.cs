using System;
using Zene.Structs;

namespace Zene.Evolution
{
    public class Lifeform
    {
        private Lifeform(Gene[] genes, NeuralNetwork neuralNetwork, Vector2I pos, World world)
        {
            Genes = genes;
            NeuralNetwork = neuralNetwork;

            if (genes.Length < 1)
            {
                Colour = new Colour();
            }
            else
            {
                Colour = GenColour(genes);
            }
            _location = pos;
            PreLocation = pos;

            Properties = new LifeProperties(0);

            CurrentWorld = world;
        }

        public Gene[] Genes { get; }
        public NeuralNetwork NeuralNetwork { get; }
        public int Age { get; set; } = 0;

        public Colour Colour { get; }
        public LifeProperties Properties;
        public World CurrentWorld { get; }

        private Vector2I _location;
        public Vector2I Location
        {
            get
            {
                return _location;
            }
            set
            {
                // The movement is posible - no going outside bounds or into other lifeforms
                // Also updates world location tracking
                if (CurrentWorld.MoveLifeform(this, value))
                {
                    PreLocation = _location;
                    _location = value;
                }
            }
        }
        public Vector2I PreLocation { get; private set; }

        public bool Shift(Vector2I value)
        {
            // The movement is posible - no going outside bounds or into other lifeforms
            // Also updates world location tracking
            if (CurrentWorld.MoveLifeform(this, _location + value))
            {
                PreLocation = _location;
                _location += value;

                return true;
            }

            return false;
        }

        public Lifeform CreateChild(Vector2I location, World world)
        {
            Gene[] genes = new Gene[Genes.Length];

            for (int i = 0; i < Genes.Length; i++)
            {
                genes[i] = Genes[i].CreateChild();
            }

            return new Lifeform(
                genes,
                NeuralNetwork.Generate(genes),
                location,
                world);
        }

        /// <summary>
        /// Calculate the actions for this <see cref="Lifeform"/> for this frame.
        /// </summary>
        public void Update()
        {
            Age++;

            Properties.ClearNeuronValues();

            NeuralNetwork.Compute(this);
        }

        public override bool Equals(object obj)
        {
            return obj is Lifeform l &&
                l.Genes == Genes &&
                l.NeuralNetwork == NeuralNetwork &&
                l.Age == Age &&
                l.Properties == Properties;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Genes, NeuralNetwork, Age, Properties);
        }

        public static Random Random { get; set; }
        public static object RandSync { get; } = new object();

        public static Lifeform Generate(int seed, int nGene, Vector2I location, World world)
        {
            Gene[] genes = new Gene[nGene];

            Random r = new Random(seed);

            for (int i = 0; i < nGene; i++)
            {
                genes[i] = Gene.Generate(r.Next());
            }

            return new Lifeform(
                genes, 
                NeuralNetwork.Generate(genes),
                location,
                world);
        }

        public static Lifeform Generate(int nGene, Vector2I location, World world)
        {
            Gene[] genes = new Gene[nGene];

            for (int i = 0; i < nGene; i++)
            {
                genes[i] = Gene.Generate();
            }

            return new Lifeform(
                genes,
                NeuralNetwork.Generate(genes),
                location,
                world);
        }

        public static Lifeform Empty { get; } = Dud(Vector2I.Zero, null);
        /// <summary>
        /// Creates a lifeless lifeform.
        /// </summary>
        public static Lifeform Dud(Vector2I location, World world)
        {
            return new Lifeform(Array.Empty<Gene>(), NeuralNetwork.Empty(), location, world);
        }

        public static bool operator ==(Lifeform a, Lifeform b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Lifeform a, Lifeform b)
        {
            return !a.Equals(b);
        }

        public static bool OneInChance(double chance)
        {
            int max = (int)Math.Round(1 / chance);

            // Fix dealing with vary small chances - numbers bigger
            if (max == int.MinValue) { max += 1; }

            // Random doesn't support negatives
            if (max < 0) { max *= -1; }

            bool compare;

            lock (RandSync)
            {
                compare = Random.Next(0, max) == Random.Next(0, max);
            }

            return compare;
        }
        private static Colour GenColour(Gene[] genes)
        {
            int r = 0;
            int g = 0;
            int b = 0;

            foreach (Gene gene in genes)
            {
                r += gene.Source % 256;
                g += gene.Destination % 256;
                b += gene.Strength % 256;
            }

            return new Colour(
                (byte)((r % 16) * 16),
                (byte)((g % 16) * 16),
                (byte)((b % 16) * 16));
        }
    }
}
