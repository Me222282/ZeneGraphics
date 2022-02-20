using System;
using Zene.Graphics;
using Zene.Structs;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Zene.Windowing;
using System.Diagnostics;
using Zene.Graphics.Shaders;
using Zene.Windowing.Base;

namespace GUI
{
    public class Program
    {
        static void Main()
        {
            Core.Init();

            //Window w = new Window(10, 10, "tlikhser5otu984957745");

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
            Core.Init();

            Window w = new Window(10, 10, "tlikhser5otu984957745");
            GLFW.SwapInterval(0);

            Texture2D texture = Texture2D.Create(_textureData, WrapStyle.Repeated, TextureSampling.Blend, false);
            DrawObject<double, byte> drawable = new DrawObject<double, byte>(new double[]
            {
                0.5, 0.5, 1, 1,
                -0.5, 0.5, 0, 1,
                -0.5, -0.5, 0, 0,
                0.5, -0.5, 1, 0
            }, new byte[] { 0, 1, 2, 2, 3, 0 }, 4, 0, AttributeSize.D2, BufferUsage.DrawFrequent);
            drawable.AddAttribute(2, 2, AttributeSize.D2);
            BasicShader shader = new BasicShader();

            shader.Bind();
            shader.SetColourSource(ColourSource.Texture);
            shader.SetTextureSlot(0);
            texture.Bind(0);

            drawable.Draw();

            GLFW.SwapBuffers(w.Handle);

            shader.Dispose();
            drawable.Dispose();
            texture.Dispose();
            w.Dispose();

            Core.Terminate();
        }
    }
}