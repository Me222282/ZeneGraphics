﻿// Copyright (c) 2017-2019 Zachary Snow
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Runtime.InteropServices;

#pragma warning disable CA1401 // P/Invokes should not be visible
#pragma warning disable CA2101 // Specify marshaling for P/Invoke string arguments

namespace Zene.Graphics.Base
{
    public static class WGL
    {
        private const string OpenGL32Library = "opengl32.dll";
        
        public const uint ContextMajorVersionARB = 0x2091;
        public const uint ContextMinorVersionARB = 0x2092;
        public const uint ContextFlagsARB = 0x2094;

        public const int BITSPIXEL = 12;

        public const uint Doublebuffer = 0x00000001;
        public const uint DrawToWindow = 0x00000004;
        public const uint SupportOpenGL = 0x00000020;
        public const uint TypeRgba = 0;
        public const uint MainPlane = 0;

        [DllImport(OpenGL32Library, EntryPoint = "wglCreateContext", ExactSpelling = true)]
        public static extern IntPtr CreateContext(IntPtr hDc);

        [DllImport(OpenGL32Library, EntryPoint = "wglDeleteContext", ExactSpelling = true)]
        public static extern bool DeleteContext(IntPtr oldContext);

        [DllImport(OpenGL32Library, EntryPoint = "wglGetProcAddress", ExactSpelling = true)]
        private static extern IntPtr GetProcAddressWgl(string lpszProc);

        [DllImport(OpenGL32Library, EntryPoint = "wglMakeCurrent", ExactSpelling = true)]
        public static extern bool MakeCurrent(IntPtr hDc, IntPtr newContext);

        [DllImport(OpenGL32Library, EntryPoint = "wglSwapBuffers", ExactSpelling = true)]
        public static extern bool SwapBuffers(IntPtr hdc);

        [StructLayout(LayoutKind.Sequential)]
        public struct PixelFormatDescriptor
        {
            public ushort nSize;
            public ushort nVersion;
            public uint dwFlags;
            public byte iPixelType;
            public byte cColorBits;
            public byte cRedBits;
            public byte cRedShift;
            public byte cGreenBits;
            public byte cGreenShift;
            public byte cBlueBits;
            public byte cBlueShift;
            public byte cAlphaBits;
            public byte cAlphaShift;
            public byte cAccumBits;
            public byte cAccumRedBits;
            public byte cAccumGreenBits;
            public byte cAccumBlueBits;
            public byte cAccumAlphaBits;
            public byte cDepthBits;
            public byte cStencilBits;
            public byte cAuxBuffers;
            public byte iLayerType;
            public byte bReserved;
            public uint dwLayerMask;
            public uint dwVisibleMask;
            public uint dwDamageMask;
        }

        [DllImport("gdi32.dll")]
        public static extern int ChoosePixelFormat(IntPtr hdc, ref PixelFormatDescriptor ppfd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("kernel32.dll", EntryPoint = "GetModuleHandle")]
        private static extern IntPtr GetModuleHandle(string module);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        private static extern IntPtr GetProcAddressWin32(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern int SetPixelFormat(IntPtr hdc, int iPixelFormat, ref PixelFormatDescriptor ppfd);

        public delegate IntPtr _CreateContextAttribsARB(IntPtr hDC, IntPtr hShareContext, int[] attribList);

        private static IntPtr hWnd;
        private static IntPtr hDC;
        private static IntPtr hRC;
        private static IntPtr hModule;

        public static void Init(IntPtr hWnd, int versionMajor, int versionMinor)
        {
            WGL.hWnd = hWnd;

            hDC = GetDC(hWnd);

            if (hDC == IntPtr.Zero)
                throw new InvalidOperationException("Could not get a device context (hDC).");

            PixelFormatDescriptor pfd = new PixelFormatDescriptor()
            {
                nSize = (ushort)Marshal.SizeOf(typeof(PixelFormatDescriptor)),
                nVersion = 1,
                dwFlags = (SupportOpenGL | DrawToWindow | Doublebuffer),
                iPixelType = (byte)TypeRgba,
                cColorBits = (byte)GetDeviceCaps(hDC, BITSPIXEL),
                cDepthBits = 32,
                iLayerType = (byte)MainPlane
            };

            int pixelformat = ChoosePixelFormat(hDC, ref pfd);

            if (pixelformat == 0)
                throw new InvalidOperationException("Could not find A suitable pixel format.");

            if (SetPixelFormat(hDC, pixelformat, ref pfd) == 0)
                throw new InvalidOperationException("Could not set the pixel format.");

            IntPtr tempContext = CreateContext(hDC);

            if (tempContext == IntPtr.Zero)
                throw new InvalidOperationException("Unable to create temporary render context.");

            if (!MakeCurrent(hDC, tempContext))
                throw new InvalidOperationException("Unable to make temporary render context current.");

            int[] attribs = new int[]
            {
                (int)ContextMajorVersionARB, versionMajor,
                (int)ContextMinorVersionARB, versionMinor,
                (int)ContextFlagsARB, (int)0,
                0
            };

            IntPtr proc = GetProcAddressWgl("wglCreateContextAttribsARB");
            _CreateContextAttribsARB createContextAttribs = (_CreateContextAttribsARB)Marshal.GetDelegateForFunctionPointer(proc, typeof(_CreateContextAttribsARB));
            hRC = createContextAttribs(hDC, IntPtr.Zero, attribs);

            MakeCurrent(IntPtr.Zero, IntPtr.Zero);
            DeleteContext(tempContext);

            if (hRC == IntPtr.Zero)
                throw new InvalidOperationException("Unable to create render context.");

            if (!MakeCurrent(hDC, hRC))
                throw new InvalidOperationException("Unable to make render context current.");

            hModule = LoadLibrary(OpenGL32Library);
        }

        public static void Shutdown()
        {
            MakeCurrent(IntPtr.Zero, IntPtr.Zero);
            DeleteContext(hRC);

            _ = ReleaseDC(hWnd, hDC);
        }

        public static IntPtr GetProcAddress(string name)
        {
            IntPtr procAddress = GetProcAddressWgl(name);

            if (procAddress == IntPtr.Zero)
            {
                procAddress = GetProcAddressWin32(hModule, name);
            }

            return procAddress;
        }

        public static void MakeCurrent()
        {
            MakeCurrent(hDC, hRC);
        }

        public static void SwapBuffers()
        {
            SwapBuffers(hDC);
        }
    }
}
