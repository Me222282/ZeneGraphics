using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
#if DEBUG

    public unsafe class StateDebug
    {
        public static GLBox Viewport
        {
            get
            {
                int* ptr = stackalloc int[4];

                GL.GetIntegerv(GLEnum.Viewport, ptr);

                return new GLBox(ptr[0], ptr[1], ptr[2], ptr[3]);
            }
        }

        public static bool ScissorTest
        {
            get
            {
                int* ptr = stackalloc int[1];

                GL.GetIntegerv(GLEnum.ScissorTest, ptr);

                return *ptr > 0;
            }
        }
        public static GLBox ScissorBox
        {
            get
            {
                int* ptr = stackalloc int[4];

                GL.GetIntegerv(GLEnum.ScissorBox, ptr);

                return new GLBox(ptr[0], ptr[1], ptr[2], ptr[3]);
            }
        }

        public static DepthFunction DepthFunc
        {
            get
            {
                int* ptr = stackalloc int[1];

                GL.GetIntegerv(GLEnum.DepthFunc, ptr);

                return (DepthFunction)(uint)*ptr;
            }
        }
        public static PolygonMode PolygonMode
        {
            get
            {
                int* ptr = stackalloc int[1];

                GL.GetIntegerv(GLEnum.PolygonMode, ptr);

                return (PolygonMode)(uint)*ptr;
            }
        }
    }

#endif
}
