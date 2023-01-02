using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zene.Graphics.Base;

namespace Zene.Graphics
{
    public static class DrawingFunctions
    {
        /*
        public static void DrawVertexArray(this IDrawingContext dc, IVertexArray va, int points)
        {
            if (va.Properties._elementBuffer == null)
            {
                dc.DrawArrays(va, DrawMode.Triangles, 0, points);
                return;
            }

            dc.DrawElements(va, DrawMode.Triangles, points, va.Properties._elementBuffer.Properties, 0);
            return;
        }*/

        public static void DrawObject<T, I>(this IDrawingContext dc, DrawObject<T, I> obj)
            where T : unmanaged
            where I : unmanaged
        {
            dc.DrawElements(obj, DrawMode.Triangles, obj.Ibo.Size, obj.IndexType, 0);
        }
        public static void DrawObject<T, I>(this IDrawingContext dc, DrawObject<T, I> obj, int instances)
            where T : unmanaged
            where I : unmanaged
        {
            dc.DrawElementsInstanced(obj, DrawMode.Triangles, obj.Ibo.Size, obj.IndexType, 0, instances);
        }
    }
}
