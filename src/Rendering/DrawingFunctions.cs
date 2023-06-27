using System;
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

        public static void Draw(this IDrawingContext dc, Drawable renderable)
        {
            if (renderable == null)
            {
                throw new ArgumentNullException(nameof(renderable));
            }

            if (renderable.Instances > 1)
            {
                Draw(dc, renderable.VertexArray, renderable.Info, renderable.Instances);
                return;
            }

            Draw(dc, renderable.VertexArray, renderable.Info);
        }
        public static void Draw(this IDrawingContext dc, IDrawObject obj)
            => Draw(dc, obj.GetRenderable(dc));

        public static void Draw(this IDrawingContext dc, IDrawObject obj, int instances)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            Drawable renderable = obj.GetRenderable(dc);

            Draw(dc, renderable.VertexArray, renderable.Info, instances);
        }

        public static void Render(this IDrawingContext dc, IRenderable renderable) => renderable?.OnRender(dc);
        public static void Render<T>(this IDrawingContext dc, IRenderable<T> renderable, T param) => renderable?.OnRender(dc, param);

        public static void WriteFramebuffer(this IDrawingContext dc, IFramebuffer framebuffer, BufferBit mask, TextureSampling filter)
            => WriteFramebuffer(dc, framebuffer, new GLBox(Vector2I.Zero, framebuffer.Properties.Size), mask, filter);
        public static void WriteFramebuffer(this IDrawingContext dc, IFramebuffer framebuffer, IBox source, BufferBit mask, TextureSampling filter)
            => framebuffer.BlitBuffer(dc.Framebuffer, source, dc.FrameBounds, mask, filter);

        public static void Copy(this IDrawingContext dc, IDrawingContext source)
            => WriteFramebuffer(dc, source.Framebuffer, source.FrameBounds, BufferBit.All, TextureSampling.Nearest);
        public static void Copy(this IDrawingContext dc, IDrawingContext source, BufferBit mask)
            => WriteFramebuffer(dc, source.Framebuffer, source.FrameBounds, mask, TextureSampling.Nearest);
        public static void Copy(this IDrawingContext dc, IDrawingContext source, IBox bounds, BufferBit mask)
            => WriteFramebuffer(dc, source.Framebuffer, bounds, mask, TextureSampling.Nearest);
        public static void Copy(this IDrawingContext dc, IDrawingContext source, IBox bounds)
            => WriteFramebuffer(dc, source.Framebuffer, bounds, BufferBit.All, TextureSampling.Nearest);

        public static void DrawBox(this IDrawingContext dc, IBox bounds, ColourF colour)
        {
            if (dc is not DrawManager dm)
            {
                throw new ArgumentException($"Specific draw functions can only be drawn with ${typeof(DrawManager)}", nameof(dc));
            }

            dm.Shader = Shapes.BasicShader;
            dm.Shader.Colour = colour;
            dm.Shader.ColourSource = ColourSource.UniformColour;

            dm.Model = Matrix4.CreateBox(bounds);
            dm.Draw(Shapes.Square);
        }
        public static void DrawBox(this IDrawingContext dc, IBox bounds, ITexture texture)
        {
            if (dc is not DrawManager dm)
            {
                throw new ArgumentException($"Specific draw functions can only be drawn with ${typeof(DrawManager)}", nameof(dc));
            }

            dm.Shader = Shapes.BasicShader;
            texture.Bind(0);
            dm.Shader.TextureSlot = 0;
            dm.Shader.ColourSource = ColourSource.Texture;

            dm.Model = Matrix4.CreateBox(bounds);
            dm.Draw(Shapes.Square);
        }
        public static void DrawRoundedBox(this IDrawingContext dc, IBox bounds, ColourF colour, double cornerRadius)
        {
            if (dc is not DrawManager dm)
            {
                throw new ArgumentException($"Specific draw functions can only be drawn with ${typeof(DrawManager)}", nameof(dc));
            }

            dm.Shader = Shapes.BorderShader;
            dm.Shader.Colour = colour;
            dm.Shader.ColourSource = ColourSource.UniformColour;

            Shapes.BorderShader.BorderWidth = 0;
            Shapes.BorderShader.Radius = cornerRadius;
            Shapes.BorderShader.Size = bounds.Size;

            dm.Model = Matrix4.CreateBox(bounds);
            dm.Draw(Shapes.Square);
        }
        public static void DrawRoundedBox(this IDrawingContext dc, IBox bounds, ITexture texture, double cornerRadius)
        {
            if (dc is not DrawManager dm)
            {
                throw new ArgumentException($"Specific draw functions can only be drawn with ${typeof(DrawManager)}", nameof(dc));
            }

            dm.Shader = Shapes.BorderShader;
            texture.Bind(0);
            dm.Shader.TextureSlot = 0;
            dm.Shader.ColourSource = ColourSource.Texture;

            Shapes.BorderShader.BorderWidth = 0;
            Shapes.BorderShader.Radius = cornerRadius;
            Shapes.BorderShader.Size = bounds.Size;

            dm.Model = Matrix4.CreateBox(bounds);
            dm.Draw(Shapes.Square);
        }
        public static void DrawBorderBox(this IDrawingContext dc, IBox bounds, ColourF colour, double borderWidth, ColourF borderColour, double cornerRadius = 0)
        {
            if (dc is not DrawManager dm)
            {
                throw new ArgumentException($"Specific draw functions can only be drawn with ${typeof(DrawManager)}", nameof(dc));
            }

            dm.Shader = Shapes.BorderShader;
            dm.Shader.Colour = colour;
            dm.Shader.ColourSource = ColourSource.UniformColour;

            Shapes.BorderShader.BorderWidth = borderWidth;
            Shapes.BorderShader.BorderColour = borderColour;
            Shapes.BorderShader.Radius = cornerRadius;
            Shapes.BorderShader.Size = bounds.Size;

            dm.Model = Matrix4.CreateBox(bounds);
            dm.Draw(Shapes.Square);
        }
        public static void DrawBorderBox(this IDrawingContext dc, IBox bounds, ITexture texture, double borderWidth, ColourF borderColour, double cornerRadius = 0)
        {
            if (dc is not DrawManager dm)
            {
                throw new ArgumentException($"Specific draw functions can only be drawn with ${typeof(DrawManager)}", nameof(dc));
            }

            dm.Shader = Shapes.BorderShader;
            texture.Bind(0);
            dm.Shader.TextureSlot = 0;
            dm.Shader.ColourSource = ColourSource.Texture;

            Shapes.BorderShader.BorderWidth = borderWidth;
            Shapes.BorderShader.BorderColour = borderColour;
            Shapes.BorderShader.Radius = cornerRadius;
            Shapes.BorderShader.Size = bounds.Size;

            dm.Model = Matrix4.CreateBox(bounds);
            dm.Draw(Shapes.Square);
        }
        public static void DrawEllipse(this IDrawingContext dc, IBox bounds, ColourF colour)
        {
            if (dc is not DrawManager dm)
            {
                throw new ArgumentException($"Specific draw functions can only be drawn with ${typeof(DrawManager)}", nameof(dc));
            }

            dm.Shader = Shapes.CircleShader;
            dm.Shader.Colour = colour;
            dm.Shader.ColourSource = ColourSource.UniformColour;
            Shapes.CircleShader.LineWidth = 0.5;
            Shapes.CircleShader.Size = 1d;

            dm.Model = Matrix4.CreateBox(bounds);
            dm.Draw(Shapes.Square);
        }
        public static void DrawEllipse(this IDrawingContext dc, IBox bounds, ITexture texture)
        {
            if (dc is not DrawManager dm)
            {
                throw new ArgumentException($"Specific draw functions can only be drawn with ${typeof(DrawManager)}", nameof(dc));
            }

            dm.Shader = Shapes.BasicShader;
            texture.Bind(0);
            dm.Shader.TextureSlot = 0;
            dm.Shader.ColourSource = ColourSource.Texture;
            Shapes.CircleShader.LineWidth = 0.5;
            Shapes.CircleShader.Size = 1d;

            dm.Model = Matrix4.CreateBox(bounds);
            dm.Draw(Shapes.Square);
        }
        public static void DrawRing(this IDrawingContext dc, IBox bounds, double lineWidth, ColourF colour)
        {
            if (dc is not DrawManager dm)
            {
                throw new ArgumentException($"Specific draw functions can only be drawn with ${typeof(DrawManager)}", nameof(dc));
            }

            dm.Shader = Shapes.CircleShader;
            dm.Shader.Colour = colour;
            dm.Shader.ColourSource = ColourSource.UniformColour;
            Shapes.CircleShader.LineWidth = lineWidth;
            Shapes.CircleShader.Size = Math.Min(bounds.Width, bounds.Height);
            Shapes.CircleShader.InnerColour = ColourF.Zero;

            dm.Model = Matrix4.CreateBox(bounds);
            dm.Draw(Shapes.Square);
        }
    }
}
