using System;
using Zene.Graphics;
using Zene.Structs;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Zene.Windowing;

namespace GUI
{
    public class Program
    {
        static void Main()
        {
            Core.Init();

            Window w = new Window(10, 10, "tlikhser5otu984957745");

            BenchmarkRunner.Run<Bench>();
            Console.ReadLine();

            Core.Terminate();
        }
    }

    [MemoryDiagnoser]
    public class Bench
    {
        private static readonly GLArray<Colour> _textureData = new GLArray<Colour>(5, 5, 1, new Colour[]
            {
                Colour.Zero, Colour.Zero, Colour.Zero, Colour.Zero, Colour.Zero,
                Colour.Zero, Colour.Zero, Colour.Zero, Colour.Zero, Colour.Zero,
                Colour.Zero, Colour.Zero, Colour.Zero, Colour.Zero, Colour.Zero,
                Colour.Zero, Colour.Zero, Colour.Zero, Colour.Zero, Colour.Zero,
                Colour.Zero, Colour.Zero, Colour.Zero, Colour.Zero, Colour.Zero
            });

        [Benchmark]
        public void Textures()
        {
            Texture2D t = Texture2D.Create(_textureData, WrapStyle.Repeated, TextureSampling.Nearest, false);

            t.Dispose();
        }
    }
}