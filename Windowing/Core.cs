using System;
using Zene.Windowing.Base;

namespace Zene.Windowing
{
    public static class Core
    {
        public static void Init()
        {
            if (GLFW.Init() == GLFW.False)
            {
                throw new InvalidOperationException("Failed to initialize GLFW.");
            }
        }

        public static void Terminate() => GLFW.Terminate();
    }
}
