﻿using System;
using Zene.Graphics.Base;
using Zene.Graphics.Base.Extensions;
using Zene.Structs;

namespace Zene.Graphics
{
    public static class DrawingFunctions
    {
        public static void Draw(this IDrawingContext dc, IVertexArray va, RenderInfo info)
        {
            if (va.Properties._elementBuffer == null && info.Indexed)
            {
                throw new VertexArrayException(va, "Attempted to draw object indexed without an element array buffer.");
            }

            if (!info.Indexed)
            {
                dc.DrawArrays(va, info.DrawMode, info.VertexOffset, info.VertexCount);
                return;
            }

            dc.DrawElements(va, info.DrawMode, info.VertexCount, info.IndexType, info.VertexOffset);
            return;
        }
        public static void Draw(this IDrawingContext dc, IVertexArray va, RenderInfo info, int instances)
        {
            if (va.Properties._elementBuffer == null && info.Indexed)
            {
                throw new VertexArrayException(va, "Attempted to draw object indexed without an element array buffer.");
            }

            if (!info.Indexed)
            {
                dc.DrawArraysInstanced(va, info.DrawMode, info.VertexOffset, info.VertexCount, instances);
                return;
            }

            dc.DrawElementsInstanced(va, info.DrawMode, info.VertexCount, info.IndexType, info.VertexOffset, instances);
            return;
        }

        public static void Draw(this IDrawingContext dc, Renderable renderable)
        {
            if (renderable.FromFramebuffer)
            {
                renderable.framebuffer.BlitBuffer(
                    dc.Framebuffer,
                    renderable.framebufferBounds,
                    new GLBox(Vector2I.Zero, dc.Framebuffer.Properties.Size),
                    renderable.bufferBit,
                    renderable.sampling);
                return;
            }

            if (renderable.Instances > 1)
            {
                Draw(dc, renderable.vertexArray, renderable.Info, renderable.Instances);
                return;
            }

            Draw(dc, renderable.vertexArray, renderable.Info);
        }
        public static void Draw(this IDrawingContext dc, IDrawObject obj)
            => Draw(dc, obj.GetRenderable(dc));

        public static void Draw(this IDrawingContext dc, IDrawObject obj, int instances)
        {
            Renderable renderable = obj.GetRenderable(dc);

            if (renderable.FromFramebuffer)
            {
                throw new DrawingException(dc, "Cannot draw framebuffer with instancing.");
            }

            Draw(dc, renderable.vertexArray, renderable.Info, instances);
        }
    }
}