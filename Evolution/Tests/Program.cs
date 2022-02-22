using System;
using System.IO;
using System.Collections.Generic;
using Zene.Evolution;
using Zene.Structs;
using Zene.Windowing;
using System.Text;

namespace EvolutionTest
{
    class Program
    {
        private static double _mutation = 0.001;

        private static void Main(string[] args)
        {
            Core.Init();

            //SimulateLive(1, 1000, 80, 128, 300, true, 0);
            
            SimulateLive(1, 1000, 4, 128, 300, true, 0);

            //Simulate(300, new int[] { 100, 200, 300 }, 1, 1000, 4, 128, 300);
            //RunGeneration(new string[] { "output100.gen", "output200.gen", "output300.gen" });
            //RunGeneration(args);
            /*
            if (!File.Exists("settings.txt"))
            {
                Console.WriteLine("No settings file.");
                Console.ReadLine();
                return;
            }*/

            //SimulateLive(GetSettings("settings.txt"));
            //Simulate(GetSettings("settings.txt"));

            Core.Terminate();
        }

        private static void Simulate(int generations, int[] exportGenerations, int seed, int lifeforms, int brainSize, int worldSize, int genLength)
        {
            SetupEnvironment(seed);

            List<int> exportGens = new List<int>(exportGenerations);

            World world = new World(lifeforms, brainSize, worldSize, worldSize);

            FramePart[,] frames = null;
            generations++;

            int counter = 0;
            while (world.Generation < generations)
            {
                bool exportGen = exportGens.Contains(world.Generation);

                if (counter >= genLength)
                {
                    if (exportGen)
                    {
                        ExportFrames($"output{world.Generation}.gen", frames, counter, world.Lifeforms.Length, worldSize);
                        ExportLifeforms($"lifeforms{world.Generation}.txt", world.Lifeforms);
                    }

                    counter = 0;
                    world = world.NextGeneration(lifeforms, CheckLifeform);

                    exportGen = exportGens.Contains(world.Generation);

                    if (exportGen)
                    {
                        frames = new FramePart[genLength, world.Lifeforms.Length];
                    }
                }

                world.Update();

                if (exportGen)
                {
                    for (int i = 0; i < world.Lifeforms.Length; i++)
                    {
                        frames[counter, i] = new FramePart(world.Lifeforms[i]);
                    }
                }

                counter++;
            }
        }
        private static void Simulate(Settings s)
        {
            Simulate(s.Gens, s.ExportGens, s.Seed, s.LifeForms, s.BrainSize, s.WorldSize, s.GenLength);
        }
        private static void SimulateLive(int seed, int lifeforms, int brainSize, int worldSize, int genLength, bool vsync, int delay)
        {
            SetupEnvironment(seed);

            WindowL window = new WindowL(128 * 6, 128 * 6, "Work", 
                lifeforms, brainSize, worldSize, genLength);

            window.Run(vsync, delay);
        }
        private static void SimulateLive(Settings s)
        {
            SimulateLive(s.Seed, s.LifeForms, s.BrainSize, s.WorldSize, s.GenLength, s.VSync, s.Delay);
        }
        private static void RunGeneration(string[] paths)
        {
            byte[][] data = new byte[paths.Length][];

            StringBuilder text = new StringBuilder();

            for (int i = 0; i < paths.Length; i++)
            {
                text.Append(paths[i]);

                if (i + 1 != paths.Length)
                {
                    text.Append(" - ");
                }

                data[i] = File.ReadAllBytes(paths[i]);
            }

            WindowW window = new WindowW(128 * 6, 128 * 6, text.ToString(), data);

            window.Run();
        }

        //public static ICheckLifeform CheckLifeformFunc;

        public struct Settings
        {
            public Settings(int g, int s, int l, int b, int w, int gl, int d, bool v, int[] e)
            {
                Gens = g;
                Seed = s;
                LifeForms = l;
                BrainSize = b;
                WorldSize = w;
                GenLength = gl;
                Delay = d;
                VSync = v;
                ExportGens = e;
            }

            public int Gens { get; }
            public int Seed { get; }
            public int LifeForms { get; }
            public int BrainSize { get; }
            public int WorldSize { get; }
            public int GenLength { get; }
            public int Delay { get; }
            public bool VSync { get; }
            public int[] ExportGens { get; }
        }
        public static Settings GetSettings(string path)
        {
            string[] lines = File.ReadAllLines(path);

            _mutation = double.Parse(lines[0]);
            int gens = int.Parse(lines[1]);
            int seed = int.Parse(lines[2]);
            int lfs = int.Parse(lines[3]);
            int brain = int.Parse(lines[4]);
            int world = int.Parse(lines[5]);
            int length = int.Parse(lines[6]);
            int delay = int.Parse(lines[7]);
            bool vsync = bool.Parse(lines[8]);

            int[] exports = new int[lines.Length - 10];

            for (int i = 10; i < lines.Length; i++)
            {
                exports[i - 10] = int.Parse(lines[i]);
            }
            /*
            Assembly asm = Assembly.LoadFrom(lines[9]);
            Type type = asm.GetType("CheckLifeform");
            CheckLifeformFunc = Activator.CreateInstanceFrom(lines[9], "CheckLifeform") as ICheckLifeform;
            */
            return new Settings(gens, seed, lfs, brain, world, length, delay, vsync ,exports);
        }

        public static bool CheckLifeform(Lifeform lifeform)
        {
            // Get to left
            //return lifeform.Location.X > (lifeform.CurrentWorld.Width / 2);
            
            // Get to centre X
            //return lifeform.Location.X > (lifeform.CurrentWorld.Width / 4) &&
            //    lifeform.Location.X < (lifeform.CurrentWorld.Width - (lifeform.CurrentWorld.Width / 4));
            
            // Get to corners
            //return (lifeform.Location.X < (lifeform.CurrentWorld.Width / 4) && (lifeform.Location.Y < (lifeform.CurrentWorld.Height / 4))) ||
            //    (lifeform.Location.X > (lifeform.CurrentWorld.Width - (lifeform.CurrentWorld.Width / 4)) && (lifeform.Location.Y > (lifeform.CurrentWorld.Height - (lifeform.CurrentWorld.Height / 4)))) ||
            //    (lifeform.Location.X > (lifeform.CurrentWorld.Width - (lifeform.CurrentWorld.Width / 4)) && (lifeform.Location.Y < (lifeform.CurrentWorld.Height / 4))) ||
            //    (lifeform.Location.X < (lifeform.CurrentWorld.Width / 4) && (lifeform.Location.Y > (lifeform.CurrentWorld.Height - (lifeform.CurrentWorld.Height / 4))));
            
            // Get to checkered patern location
            //return ((lifeform.Location.X + lifeform.Location.Y) % 2) == 0;
            
            // Get to the centre
            return (lifeform.Location.X > (lifeform.CurrentWorld.Width / 4)) &&
                (lifeform.Location.X < (lifeform.CurrentWorld.Width - (lifeform.CurrentWorld.Width / 4))) &&
                (lifeform.Location.Y > (lifeform.CurrentWorld.Height / 4)) &&
                (lifeform.Location.Y < (lifeform.CurrentWorld.Height - (lifeform.CurrentWorld.Height / 4)));
        }

        public struct FramePart
        {
            public FramePart(Lifeform l)
            {
                Colour = l.Colour;
                Position = l.Location;
            }
            public FramePart(byte r, byte g, byte b, int x, int y)
            {
                Colour = new Colour(r, g, b);
                Position = new Vector2I(x, y);
            }

            public Colour Colour { get; }
            public Vector2I Position { get; }
        }

        public static unsafe void ExportFrames(string path, FramePart[,] frames, int frameCount, int lifeCount, int worldSize)
        {
            byte[] data = new byte[(sizeof(FramePart) * frameCount * lifeCount) + (sizeof(int) * 3)];

            byte[] dataAdd;
            int writeOffset = 0;

            dataAdd = BitConverter.GetBytes(worldSize);
            for (int i = 0; i < dataAdd.Length; i++)
            {
                data[writeOffset] = dataAdd[i];
                writeOffset++;
            }

            dataAdd = BitConverter.GetBytes(frameCount);
            for (int i = 0; i < dataAdd.Length; i++)
            {
                data[writeOffset] = dataAdd[i];
                writeOffset++;
            }

            dataAdd = BitConverter.GetBytes(lifeCount);
            for (int i = 0; i < dataAdd.Length; i++)
            {
                data[writeOffset] = dataAdd[i];
                writeOffset++;
            }

            for (int f = 0; f < frameCount; f++)
            {
                for (int l = 0; l < lifeCount; l++)
                {
                    data[writeOffset] = frames[f, l].Colour.R;
                    writeOffset++;

                    data[writeOffset] = frames[f, l].Colour.G;
                    writeOffset++;

                    data[writeOffset] = frames[f, l].Colour.B;
                    writeOffset++;

                    dataAdd = BitConverter.GetBytes(frames[f, l].Position.X);
                    for (int i = 0; i < dataAdd.Length; i++)
                    {
                        data[writeOffset] = dataAdd[i];
                        writeOffset++;
                    }

                    dataAdd = BitConverter.GetBytes(frames[f, l].Position.Y);
                    for (int i = 0; i < dataAdd.Length; i++)
                    {
                        data[writeOffset] = dataAdd[i];
                        writeOffset++;
                    }
                }
            }

            File.WriteAllBytes(path, data);
        }
        public static unsafe FramePart[,] ImportFrames(byte[] data, out int frameCount, out int lifeCount, out int worldSize)
        {
            int readOffset = 0;

            worldSize = BitConverter.ToInt32(data, readOffset);
            readOffset += 4;
            frameCount = BitConverter.ToInt32(data, readOffset);
            readOffset += 4;
            lifeCount = BitConverter.ToInt32(data, readOffset);
            readOffset += 4;

            FramePart[,] frames = new FramePart[frameCount, lifeCount];

            for (int f = 0; f < frameCount; f++)
            {
                for (int l = 0; l < lifeCount; l++)
                {
                    byte r = data[readOffset];
                    readOffset++;
                    byte g = data[readOffset];
                    readOffset++;
                    byte b = data[readOffset];
                    readOffset++;

                    int x = BitConverter.ToInt32(data, readOffset);
                    readOffset += 4;
                    int y = BitConverter.ToInt32(data, readOffset);
                    readOffset += 4;

                    frames[f, l] = new FramePart(r, g, b, x, y);
                }
            }

            return frames;
        }

        public static void ExportLifeforms(string path, Lifeform[] lifeforms)
        {
            List<string> lines = new List<string>();

            for (int i = 0; i < lifeforms.Length; i++)
            {
                StringBuilder str = new StringBuilder($"Lifeform {i}\n");

                foreach (Neuron n in lifeforms[i].NeuralNetwork.Neurons)
                {
                    switch (n.Source)
                    {
                        case InnerCell:
                            InnerCell inn = (InnerCell)n.Source;
                            str.Append($"IN{inn.NeuronAllocant}");
                            break;

                        case PUCell:
                            str.Append($"PU_");
                            break;

                        case PDCell:
                            str.Append($"PD_");
                            break;

                        case PRCell:
                            str.Append($"PR_");
                            break;

                        case PLCell:
                            str.Append($"PL_");
                            break;

                        case XPCell:
                            str.Append($"XP_");
                            break;

                        case YPCell:
                            str.Append($"YP_");
                            break;
                    }

                    str.Append(" - ");

                    switch (n.Destination)
                    {
                        case InnerCell:
                            InnerCell inn = (InnerCell)n.Destination;
                            str.Append($"IN{inn.NeuronAllocant}");
                            break;

                        case XMCell:
                            str.Append($"XM_");
                            break;

                        case YMCell:
                            str.Append($"YM_");
                            break;
                    }

                    str.AppendLine($" - {n.Scale}");
                }

                lines.Add(str.ToString());
                lines.Add("");
            }

            File.WriteAllLines(path, lines);
        }

        public static void SetupEnvironment(int seed)
        {
            int innerCellCount = 4;

            // Inner Cells
            for (int i = 0; i < innerCellCount; i++)
            {
                InnerCell.Add();
            }

            // Position Cells
            NeuralNetwork.PosibleGetCells.Add(new XPCell());
            NeuralNetwork.PosibleGetCells.Add(new YPCell());
            // Specific
            NeuralNetwork.PosibleGetCells.Add(new PUCell());
            NeuralNetwork.PosibleGetCells.Add(new PDCell());
            NeuralNetwork.PosibleGetCells.Add(new PLCell());
            NeuralNetwork.PosibleGetCells.Add(new PRCell());

            XMCell.Add();
            YMCell.Add();

            LifeProperties.NeuronValueNumber = NeuralNetwork.PosibleSetCells.Count;

            Gene.MutationChance = _mutation;
            Lifeform.Random = new Random(seed);
        }
    }
}
