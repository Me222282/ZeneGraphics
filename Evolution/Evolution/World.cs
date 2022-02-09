using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zene.Structs;

namespace Zene.Evolution
{
    public class World
    {
        public World(int seed, int lifeCount, int geneCount, int width, int height)
        {
            Width = width;
            Height = height;

            Generation = 0;

            _rect = new Rectangle(0, height - 1, width - 1, height - 1);

            Lifeforms = new Lifeform[lifeCount];

            LifeformGrid = new Lifeform[width, height];
            Vector2I[] posHierarchy = NoiseMap(width, height, seed);

            _random = new Random(seed);

            for (int i = 0; i < lifeCount; i++)
            {
                Vector2I pos = posHierarchy[i];

                Lifeform life = Lifeform.Generate(_random.Next(), geneCount, pos, this);

                LifeformGrid[pos.X, pos.Y] = life;
                Lifeforms[i] = life;
            }
        }

        public World(int lifeCount, int geneCount, int width, int height)
        {
            Width = width;
            Height = height;

            Generation = 0;

            _rect = new Rectangle(0, height - 1, width - 1, height - 1);

            Lifeforms = new Lifeform[lifeCount];

            LifeformGrid = new Lifeform[width, height];
            Vector2I[] posHierarchy = NoiseMap(width, height, Lifeform.Random.Next());

            _random = Lifeform.Random;

            for (int i = 0; i < lifeCount; i++)
            {
                Vector2I pos = posHierarchy[i];

                Lifeform life = Lifeform.Generate(geneCount, pos, this);

                LifeformGrid[pos.X, pos.Y] = life;
                Lifeforms[i] = life;
            }
        }

        private World(int width, int height, int generation, Random r)
        {
            Width = width;
            Height = height;

            Generation = generation;

            _random = r;

            _rect = new Rectangle(0, height - 1, width - 1, height - 1);
            LifeformGrid = new Lifeform[width, height];
        }

        public Lifeform[,] LifeformGrid { get; private set; }

        public Lifeform[] Lifeforms { get; private set; }

        public int Width { get; }
        public int Height { get; }
        public int Generation { get; }

        private readonly Rectangle _rect;

        private readonly Random _random;

        public void Update()
        {
            Parallel.ForEach(Lifeforms, life =>
            {
                life.Update();
            });
            /*
            foreach (Lifeform life in Lifeforms)
            {
                life.Update();
            }*/
        }

        public delegate void DrawLifeform(Lifeform lifeform);
        /// <summary>
        /// Updates all lifeforms whilst symoltaniusly drawing them with <paramref name="drawMethod"/>.
        /// </summary>
        /// <param name="drawMethod">The method to draw the lifeforms with.</param>
        public void UpdateDraw(DrawLifeform drawMethod)
        {
            foreach (Lifeform life in Lifeforms)
            {
                life.Update();
                drawMethod.Invoke(life);
            }
        }

        public bool MoveLifeform(Lifeform lifeform, Vector2I pos)
        {
            // Position is out of bounds
            if (!_rect.Contains(pos)) { return false; }
            // Position is already taken
            if (LifeformGrid[pos.X, pos.Y] is not null) { return false; }

            LifeformGrid[lifeform.Location.X, lifeform.Location.Y] = null;
            LifeformGrid[pos.X, pos.Y] = lifeform;

            return true;
        }

        public delegate bool LifeformCondition(Lifeform lifeform);
        public World NextGeneration(int width, int height, int lifeCount, LifeformCondition lifeformCondition)
        {
            World world = new World(width, height, Generation + 1, _random);

            List<Lifeform> survivors = new List<Lifeform>();

            // Determine which lifeforms survived to be able to reproduce
            foreach (Lifeform lifeform in Lifeforms)
            {
                if (!lifeformCondition.Invoke(lifeform)) { continue; }

                survivors.Add(lifeform);
            }

            Vector2I[] posHierarchy = NoiseMap(width, height, _random.Next());
            int childCount = (int)Math.Round((double)lifeCount / survivors.Count);
            Console.WriteLine($"Generation {Generation} - {(((double)survivors.Count / Lifeforms.Length) * 100):F2}% survived - {childCount * survivors.Count} new lifeforms");
            world.Lifeforms = new Lifeform[childCount * survivors.Count];
            // Make sure there are no position overlaps
            int count = 0;

            for (int i = 0; i < survivors.Count; i++)
            {
                for (int c = 0; c < childCount; c++)
                {
                    Vector2I pos = posHierarchy[count];

                    Lifeform life = survivors[i].CreateChild(pos, world);
                    world.LifeformGrid[pos.X, pos.Y] = life;
                    world.Lifeforms[count] = life;

                    count++;
                }
            }

            return world;
        }
        public World NextGeneration(int lifeCount, LifeformCondition lifeformCondition) => NextGeneration(Width, Height, lifeCount, lifeformCondition);
        public World NextGeneration(LifeformCondition lifeformCondition) => NextGeneration(Width, Height, Lifeforms.Length, lifeformCondition);

        public Lifeform GetLifeform(Vector2I location)
        {
            return LifeformGrid[location.X, location.Y];
        }
        public Lifeform GetLifeform(int x, int y)
        {
            return LifeformGrid[x, y];
        }

        private static Vector2I[] NoiseMap(int w, int h, int seed)
        {
            static int Compare(NoiseValue x, NoiseValue y)
            {
                return x.Value.CompareTo(y.Value);
            }

            List<NoiseValue> ptList = new List<NoiseValue>();

            Noise.NoiseGenerator oSimplexNoise = new Noise.NoiseGenerator(seed);
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    double value = oSimplexNoise.Evaluate(x, y);

                    ptList.Add(new NoiseValue()
                    {
                        Location = new Vector2I(x, y),
                        Value = value
                    });
                }
            }

            ptList.Sort(Compare);

            Vector2I[] output = new Vector2I[ptList.Count];

            for (int i = 0; i < ptList.Count; i++)
            {
                output[i] = ptList[i].Location;
            }

            return output;
        }
        private struct NoiseValue
        {
            public Vector2I Location { get; set; }
            public double Value { get; set; }
        }
    }
}
