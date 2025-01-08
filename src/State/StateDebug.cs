using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
#if DEBUG

    public unsafe class StateDebug
    {
        public static RectangleI Viewport
        {
            get
            {
                int* ptr = stackalloc int[4];

                GL.GetIntegerv(GLEnum.Viewport, ptr);

                return new RectangleI(ptr[0], ptr[1] + ptr[3], ptr[2], ptr[3]);
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
        public static RectangleI ScissorBox
        {
            get
            {
                int* ptr = stackalloc int[4];

                GL.GetIntegerv(GLEnum.ScissorBox, ptr);

                return new RectangleI(ptr[0], ptr[1] + ptr[3], ptr[2], ptr[3]);
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
        public static Vector2 DepthRange
        {
            get
            {
                double* ptr = stackalloc double[2];

                GL.GetDoublev(GLEnum.DepthRange, ptr);

                return new Vector2(ptr[0], ptr[1]);
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
