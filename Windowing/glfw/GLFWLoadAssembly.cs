using System;
using System.IO;
using System.Runtime.InteropServices;

#pragma warning disable CA2101 // Specify marshaling for P/Invoke string arguments

namespace Zene.Windowing.Base
{
    public static partial class GLFW
    {
        private static class Win32
        {
            [DllImport("kernel32")]
            public static extern IntPtr LoadLibrary(string fileName);

            [DllImport("kernel32")]
            public static extern IntPtr GetProcAddress(IntPtr module, string procName);
        }

        private static class Unix
        {
            public static IntPtr LoadLibrary(string fileName)
            {
                IntPtr retVal = dlopen(fileName, 2);
                var errPtr = dlerror();

                if (errPtr != IntPtr.Zero)
                    throw new InvalidOperationException(Marshal.PtrToStringAnsi(errPtr));

                return retVal;
            }

            [DllImport("libdl")]
            private static extern IntPtr dlopen(string fileName, int flags);

            [DllImport("libdl")]
            public static extern IntPtr dlsym(IntPtr handle, string symbol);

            [DllImport("libdl")]
            private static extern IntPtr dlerror();
        }

        private static Func<string, IntPtr> LoadAssembly()
        {
            var assemblyDirectory = Path.GetDirectoryName(typeof(GLFW).Assembly.Location);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                string assemblyPath = Path.Combine(
                    assemblyDirectory,
                    "glfw",
                    "win64",
                    "glfw3.dll");

                IntPtr assembly = Win32.LoadLibrary(assemblyPath);

                if (assembly == IntPtr.Zero)
                    throw new InvalidOperationException($"Failed to load GLFW dll from path '{assemblyPath}'.");

                return x => Win32.GetProcAddress(assembly, x);
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                string extension = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "so" : "dylib";
                //string assemblyPath = $"libglfw.{extension}";
                string assemblyPath = Path.Combine(
                    assemblyDirectory,
                    "glfw",
                    "os64",
                    $"libglfw.{extension}");

                IntPtr assembly = Unix.LoadLibrary(assemblyPath);

                if (assembly == IntPtr.Zero)
                    throw new InvalidOperationException($"Failed to load GLFW {extension} from path '{assemblyPath}'.");

                return functionName => Unix.dlsym(assembly, functionName);
            }

            throw new NotImplementedException("Unsupported platform.");
        }

        public static IntPtr GetNativeWindow(IntPtr window)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return GetWin32Window(window);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return GetX11Window(window);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return GetCocoaWindow(window);
            }

            throw new NotImplementedException("Unsupported platform.");
        }
    }
}