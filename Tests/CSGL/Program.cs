using System;
using Zene.Windowing;

namespace CSGL
{
    public unsafe class Program
    {
        static void Main()
        {
            Core.Init();

            //ShadowLWindow window = new ShadowLWindow(800, 500, "Work");

            //CubeMapTest window = new CubeMapTest(800, 500, "Work");

            Window3D window = new Window3D(800, 500, "Work");

            //WindowTest window = new WindowTest(800, 500, "Test");

            //Window2D window = new Window2D(800, 500, "Work");

            try
            {
                window.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }

            Core.Terminate();
        }
    }
}
