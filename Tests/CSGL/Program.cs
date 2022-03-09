using System;
using System.Text;
using Zene.Graphics;
using Zene.Graphics.Base;
using Zene.Structs;
using Zene.Windowing;

namespace CSGL
{
    public unsafe class Program
    {
        static void Main()
        {
            Core.Init();
            
            //TestTextureParam();


            //ShadowLWindow window = new ShadowLWindow(800, 500, "Work");

            //CubeMapTest window = new CubeMapTest(800, 500, "Work");

            Window3D window = new Window3D(800, 500, "Work");

            //WindowTest window = new WindowTest(800, 500, "Test");

            //Window2D window = new Window2D(800, 500, "Work");

            window.Run();
            /*
            try
            {
                window.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }*/

            Core.Terminate();
        }

        private static void TestTextureParam()
        {
            Window w = new Window(800, 500, "Test", new WindowInitProperties()
            {
                Resizable = false,
                TransparentFramebuffer = true
            });

            GLArray<ColourI> a = new GLArray<ColourI>(1, 1);

            TextureFormat[] allFormats = new TextureFormat[]
            {
                TextureFormat.CompressedRed,
                TextureFormat.CompressedRedRgtc1,
                TextureFormat.CompressedRg,
                TextureFormat.CompressedRgb,
                TextureFormat.CompressedRgba,
                TextureFormat.CompressedRgbaBptcUnorm,
                TextureFormat.CompressedRgbBptcSignedFloat,
                TextureFormat.CompressedRgRgtc2,
                TextureFormat.CompressedSignedRedRgtc1,
                TextureFormat.CompressedSignedRgRgtc2,
                TextureFormat.CompressedSrgb,
                TextureFormat.CompressedSrgba,
                TextureFormat.CompressedSrgbaBptcUnorm,
                TextureFormat.Depth24Stencil8,
                TextureFormat.Depth32fStencil8,
                TextureFormat.DepthComponent,
                TextureFormat.DepthComponent16,
                TextureFormat.DepthComponent24,
                TextureFormat.DepthComponent32,
                TextureFormat.DepthComponent32f,
                TextureFormat.DepthStencil,
                TextureFormat.R11fG11fB10f,
                TextureFormat.R16,
                TextureFormat.R16f,
                TextureFormat.R16i,
                TextureFormat.R16Snorm,
                TextureFormat.R16ui,
                TextureFormat.R32f,
                TextureFormat.R32i,
                TextureFormat.R32ui,
                TextureFormat.R3G3B2,
                TextureFormat.R8,
                TextureFormat.R8i,
                TextureFormat.R8Snorm,
                TextureFormat.R8ui,
                TextureFormat.Rg16,
                TextureFormat.Rg16f,
                TextureFormat.Rg16i,
                TextureFormat.Rg16Snorm,
                TextureFormat.Rg16ui,
                TextureFormat.Rg32f,
                TextureFormat.Rg32i,
                TextureFormat.Rg32ui,
                TextureFormat.Rg8,
                TextureFormat.Rg8i,
                TextureFormat.Rg8Snorm,
                TextureFormat.Rg8ui,
                TextureFormat.Rgb,
                TextureFormat.Rgb10,
                TextureFormat.Rgb10A2,
                TextureFormat.Rgb10A2ui,
                TextureFormat.Rgb12,
                TextureFormat.Rgb16,
                TextureFormat.Rgb16f,
                TextureFormat.Rgb16i,
                TextureFormat.Rgb16Snorm,
                TextureFormat.Rgb16ui,
                TextureFormat.Rgb32f,
                TextureFormat.Rgb32i,
                TextureFormat.Rgb32ui,
                TextureFormat.Rgb4,
                TextureFormat.Rgb5,
                TextureFormat.Rgb5A1,
                TextureFormat.Rgb8,
                TextureFormat.Rgb8i,
                TextureFormat.Rgb8Snorm,
                TextureFormat.Rgb8ui,
                TextureFormat.Rgb9E5,
                TextureFormat.Rgba,
                TextureFormat.Rgba12,
                TextureFormat.Rgba16,
                TextureFormat.Rgba16f,
                TextureFormat.Rgba16i,
                TextureFormat.Rgba16Snorm,
                TextureFormat.Rgba16ui,
                TextureFormat.Rgba2,
                TextureFormat.Rgba32f,
                TextureFormat.Rgba32i,
                TextureFormat.Rgba32ui,
                TextureFormat.Rgba4,
                TextureFormat.Rgba8,
                TextureFormat.Rgba8i,
                TextureFormat.Rgba8Snorm,
                TextureFormat.Rgba8ui,
                TextureFormat.Srgb,
                TextureFormat.Srgb8,
                TextureFormat.Srgba8,
                TextureFormat.Srgba
            };

            State.OutputDebug = false;

            StringBuilder str = new StringBuilder();

            Console.WriteLine("Start");

            for (int i = 0; i < allFormats.Length; i++)
            {
                Texture2D texture = new Texture2D(allFormats[i], TextureData.Byte);
                if (allFormats[i].IsDepth())
                {
                    texture.SetData(a.Width, a.Height, BaseFormat.DepthComponent, a);
                }
                else
                {
                    texture.SetData(a.Width, a.Height, BaseFormat.R, a);
                }

                if (GL.GetError() != GLEnum.NoError)
                {
                    texture.SetData(a.Width, a.Height, BaseFormat.RedInteger, a);
                }

                str.AppendLine(allFormats[i].ToString());
                str.AppendLine($"Red:     {texture.RedSize}");
                str.AppendLine($"         {texture.RedChannel}");
                str.AppendLine($"Green:   {texture.GreenSize}");
                str.AppendLine($"         {texture.GreenChannel}");
                str.AppendLine($"Blue:    {texture.BlueSize}");
                str.AppendLine($"         {texture.BlueChannel}");
                str.AppendLine($"Alpha:   {texture.AlphaSize}");
                str.AppendLine($"         {texture.AlphaChannel}");
                str.AppendLine($"Depth:   {texture.DepthSize}");
                str.AppendLine($"         {texture.DepthChannel}");
                str.AppendLine();

                texture.Dispose();
            }

            System.IO.File.WriteAllText("output.txt", str.ToString());

            Console.WriteLine("finished");

            while (Zene.Windowing.Base.GLFW.WindowShouldClose(w.Handle) == 0)
            {
                Zene.Windowing.Base.GLFW.PollEvents();
                Zene.Windowing.Base.GLFW.SwapBuffers(w.Handle);
            }

            w.Dispose();
        }
    }
}
