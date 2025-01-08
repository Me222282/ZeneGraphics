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
        public static void WriteFramebuffer(this IDrawingContext dc, IFramebuffer framebuffer, GLBox source, BufferBit mask, TextureSampling filter)
            => framebuffer.BlitBuffer(dc.Framebuffer, source, dc.FrameBounds, mask, filter);

        public static void Copy(this IDrawingContext dc, IDrawingContext source)
            => WriteFramebuffer(dc, source.Framebuffer, source.FrameBounds, BufferBit.All, TextureSampling.Nearest);
        public static void Copy(this IDrawingContext dc, IDrawingContext source, BufferBit mask)
            => WriteFramebuffer(dc, source.Framebuffer, source.FrameBounds, mask, TextureSampling.Nearest);
        public static void Copy(this IDrawingContext dc, IDrawingContext source, GLBox bounds, BufferBit mask)
            => WriteFramebuffer(dc, source.Framebuffer, bounds, mask, TextureSampling.Nearest);
        public static void Copy(this IDrawingContext dc, IDrawingContext source, GLBox bounds)
            => WriteFramebuffer(dc, source.Framebuffer, bounds, BufferBit.All, TextureSampling.Nearest);

        public static void DrawBox(this IDrawingContext dc, Box bounds, ColourF colour)
        {
            dc.Shader = Shapes.BasicShader;
            Shapes.BasicShader.Colour = colour;
            Shapes.BasicShader.ColourSource = ColourSource.UniformColour;

            IMatrix model = dc.Model;
            Matrix4 m;
            if (model is Matrix4 mod) { m = mod; }
            else { m = new Matrix4(model); }
            if (dc.RenderState.postMatrixMods)
            {
                dc.Model = m * Matrix4.CreateBox(bounds);
            }
            else
            {
                dc.Model = Matrix4.CreateBox(bounds) * m;
            }
            
            dc.Draw(Shapes.Square);
            dc.Model = model;
        }
        public static void DrawBox(this IDrawingContext dc, Box bounds, ITexture texture)
        {
            dc.Shader = Shapes.BasicShader;
            texture.Bind(0);
            Shapes.BasicShader.Texture = texture;
            Shapes.BasicShader.ColourSource = ColourSource.Texture;

            IMatrix model = dc.Model;
            Matrix4 m;
            if (model is Matrix4 mod) { m = mod; }
            else { m = new Matrix4(model); }
            if (dc.RenderState.postMatrixMods)
            {
                dc.Model = m * Matrix4.CreateBox(bounds);
            }
            else
            {
                dc.Model = Matrix4.CreateBox(bounds) * m;
            }
            
            dc.Draw(Shapes.Square);
            Shapes.BasicShader.Texture = null;
            dc.Model = model;
        }
        public static void DrawRoundedBox(this IDrawingContext dc, Box bounds, ColourF colour, floatv cornerRadius)
        {
            dc.Shader = Shapes.BorderShader;
            Shapes.BorderShader.Colour = colour;
            Shapes.BorderShader.ColourSource = ColourSource.UniformColour;

            Shapes.BorderShader.BorderWidth = 0;
            Shapes.BorderShader.Radius = cornerRadius;
            Shapes.BorderShader.Size = bounds.Size;

            IMatrix model = dc.Model;
            Matrix4 m;
            if (model is Matrix4 mod) { m = mod; }
            else { m = new Matrix4(model); }
            if (dc.RenderState.postMatrixMods)
            {
                dc.Model = m * Matrix4.CreateBox(bounds);
            }
            else
            {
                dc.Model = Matrix4.CreateBox(bounds) * m;
            }
            
            dc.Draw(Shapes.Square);
            dc.Model = model;
        }
        public static void DrawRoundedBox(this IDrawingContext dc, Box bounds, ITexture texture, floatv cornerRadius)
        {
            dc.Shader = Shapes.BorderShader;
            Shapes.BorderShader.Texture = texture;
            Shapes.BorderShader.ColourSource = ColourSource.Texture;

            Shapes.BorderShader.BorderWidth = 0;
            Shapes.BorderShader.Radius = cornerRadius;
            Shapes.BorderShader.Size = bounds.Size;

            IMatrix model = dc.Model;
            Matrix4 m;
            if (model is Matrix4 mod) { m = mod; }
            else { m = new Matrix4(model); }
            if (dc.RenderState.postMatrixMods)
            {
                dc.Model = m * Matrix4.CreateBox(bounds);
            }
            else
            {
                dc.Model = Matrix4.CreateBox(bounds) * m;
            }
            
            dc.Draw(Shapes.Square);
            dc.Model = model;
        }
        public static void DrawBorderBox(this IDrawingContext dc, Box bounds, ColourF colour, floatv borderWidth, ColourF borderColour, floatv cornerRadius = 0)
        {
            dc.Shader = Shapes.BorderShader;
            Shapes.BorderShader.Colour = colour;
            Shapes.BorderShader.ColourSource = ColourSource.UniformColour;

            Shapes.BorderShader.BorderWidth = borderWidth;
            Shapes.BorderShader.BorderColour = borderColour;
            Shapes.BorderShader.Radius = cornerRadius;
            Shapes.BorderShader.Size = bounds.Size;

            IMatrix model = dc.Model;
            Matrix4 m;
            if (model is Matrix4 mod) { m = mod; }
            else { m = new Matrix4(model); }
            if (dc.RenderState.postMatrixMods)
            {
                dc.Model = m * Matrix4.CreateBox(bounds);
            }
            else
            {
                dc.Model = Matrix4.CreateBox(bounds) * m;
            }
            
            dc.Draw(Shapes.Square);
            dc.Model = model;
        }
        public static void DrawBorderBox(this IDrawingContext dc, Box bounds, ITexture texture, floatv borderWidth, ColourF borderColour, floatv cornerRadius = 0)
        {
            dc.Shader = Shapes.BorderShader;
            Shapes.BorderShader.Texture = texture;
            Shapes.BorderShader.ColourSource = ColourSource.Texture;

            Shapes.BorderShader.BorderWidth = borderWidth;
            Shapes.BorderShader.BorderColour = borderColour;
            Shapes.BorderShader.Radius = cornerRadius;
            Shapes.BorderShader.Size = bounds.Size;

            IMatrix model = dc.Model;
            Matrix4 m;
            if (model is Matrix4 mod) { m = mod; }
            else { m = new Matrix4(model); }
            if (dc.RenderState.postMatrixMods)
            {
                dc.Model = m * Matrix4.CreateBox(bounds);
            }
            else
            {
                dc.Model = Matrix4.CreateBox(bounds) * m;
            }
            
            dc.Draw(Shapes.Square);
            dc.Model = model;
        }
        public static void DrawEllipse(this IDrawingContext dc, Box bounds, ColourF colour)
        {
            dc.Shader = Shapes.CircleShader;
            Shapes.CircleShader.Colour = colour;
            Shapes.CircleShader.ColourSource = ColourSource.UniformColour;
            Shapes.CircleShader.Size = 1;
            Shapes.CircleShader.LineWidth = 0;
            Shapes.CircleShader.Offset = 0.5f;

            IMatrix model = dc.Model;
            Matrix4 m;
            if (model is Matrix4 mod) { m = mod; }
            else { m = new Matrix4(model); }
            if (dc.RenderState.postMatrixMods)
            {
                dc.Model = m * Matrix4.CreateBox(bounds);
            }
            else
            {
                dc.Model = Matrix4.CreateBox(bounds) * m;
            }
            
            dc.Draw(Shapes.Square);
            dc.Model = model;
        }
        public static void DrawEllipse(this IDrawingContext dc, Box bounds, ITexture texture)
        {
            dc.Shader = Shapes.BasicShader;
            Shapes.CircleShader.Texture = texture;
            Shapes.CircleShader.ColourSource = ColourSource.Texture;
            Shapes.CircleShader.Size = 1;
            Shapes.CircleShader.LineWidth = 0;
            Shapes.CircleShader.Offset = 0.5f;

            IMatrix model = dc.Model;
            Matrix4 m;
            if (model is Matrix4 mod) { m = mod; }
            else { m = new Matrix4(model); }
            if (dc.RenderState.postMatrixMods)
            {
                dc.Model = m * Matrix4.CreateBox(bounds);
            }
            else
            {
                dc.Model = Matrix4.CreateBox(bounds) * m;
            }
            
            dc.Draw(Shapes.Square);
            dc.Model = model;
        }
        public static void DrawCircle(this IDrawingContext dc, Vector2 location, floatv radius, ColourF colour)
            => DrawEllipse(dc, new Box(location, radius), colour);
        public static void DrawCircle(this IDrawingContext dc, Vector2 location, floatv radius, ITexture texture)
        => DrawEllipse(dc, new Box(location, radius), texture);
        private static void DrawRingP(this IDrawingContext dc, Box bounds, floatv lineWidth, ColourF borderColour)
        {
            dc.Shader = Shapes.CircleShader;
            // Shapes.CircleShader.Colour = colour;
            Shapes.CircleShader.BorderColour = borderColour;
            // Shapes.CircleShader.ColourSource = ColourSource.UniformColour;
            Shapes.CircleShader.Size = Math.Min(bounds.Width, bounds.Height);
            Shapes.CircleShader.LineWidth = lineWidth;
            Shapes.CircleShader.Offset = 0.5f;

            IMatrix model = dc.Model;
            Matrix4 m;
            if (model is Matrix4 mod) { m = mod; }
            else { m = new Matrix4(model); }
            if (dc.RenderState.postMatrixMods)
            {
                dc.Model = m * Matrix4.CreateBox(bounds);
            }
            else
            {
                dc.Model = Matrix4.CreateBox(bounds) * m;
            }
            
            dc.Draw(Shapes.Square);
            dc.Model = model;
        }
        public static void DrawRing(this IDrawingContext dc, Box bounds, floatv lineWidth, ColourF colour)
        {
            Shapes.CircleShader.ColourSource = ColourSource.Discard;
            DrawRingP(dc, bounds, lineWidth, colour);
        }
        public static void DrawBorderEllipse(this IDrawingContext dc, Box bounds, floatv lineWidth, ColourF colour, ColourF borderColour)
        {
            Shapes.CircleShader.ColourSource = ColourSource.UniformColour;
            Shapes.CircleShader.Colour = colour;
            DrawRingP(dc, bounds, lineWidth, borderColour);
        }
        public static void DrawBorderEllipse(this IDrawingContext dc, Box bounds, floatv lineWidth, ITexture texture, ColourF borderColour)
        {
            Shapes.CircleShader.ColourSource = ColourSource.Texture;
            Shapes.CircleShader.Texture = texture;
            DrawRingP(dc, bounds, lineWidth, borderColour);
        }
        private static void DrawArcP(this IDrawingContext dc, Vector2 a, Vector2 b, floatv curve, floatv lineWidth, ColourF colour)
        {
            if (curve == 0d)
            {
                // draw line
            }
            if (curve < 0d)
            {
                curve = -curve;
                Vector2 tmp = a;
                a = b;
                b = tmp;
            }
            
            dc.Shader = Shapes.CircleShader;
            Shapes.CircleShader.BorderColour = colour;
            
            Vector2 mid = (a + b) / 2d;
            Vector2 t = (b - a);
            Vector2 dir = t.Rotated90();
            Vector2 cp = mid + (dir * curve);
            floatv r = curve / (2 - (t.SquaredLength / (2 * cp.SquaredDistance(a))));

            // slowest part!!
            floatv length = dir.Length;
            
            Vector2 size = (length, length * curve);
            if (curve > 0.5)
            {
                size.X = r * 2;
            }

            floatv hsx = size.X * 0.5f;
            // Box bounds = new Box(-hsx, hsx, size.Y, 0d);
            
            // Shapes.CircleShader.Size = r * 2d;
            Shapes.CircleShader.Offset = ((hsx, size.Y - r) / size);
            Shapes.CircleShader.SetSR(size, r);
            Shapes.CircleShader.LineWidth = lineWidth;

            floatv cos = t.X / length;
            floatv sin = t.Y / length;

            // Matrix4 rotat = new Matrix4(
            //     new Vector4(cos, sin, 0, 0),
            //     new Vector4(-sin, cos, 0, 0),
            //     new Vector4(0, 0, 1, 0),
            //     new Vector4(0, 0, 0, 1));

            // Matrix4 mod = Matrix4.CreateBox(bounds) * rotat * Matrix4.CreateTranslation(mid);
            floatv w = size.X;
            floatv h = size.Y;
            floatv y = size.Y * 0.5f;
            Matrix4 mod = new Matrix4(
                new Vector4(cos * w, sin * w, 0, 0),
                new Vector4(-sin * h, cos * h, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(mid.X - y * sin, y * cos + mid.Y, 0, 1));

            IMatrix model = dc.Model;
            Matrix4 m;
            if (model is Matrix4 mm) { m = mm; }
            else { m = new Matrix4(model); }
            if (dc.RenderState.postMatrixMods)
            {
                dc.Model = m * mod;
            }
            else
            {
                dc.Model = mod * m;
            }
            
            dc.Draw(Shapes.Square);
            dc.Model = model;
        }
        public static void DrawArc(this IDrawingContext dc, Vector2 a, Vector2 b, floatv curve, floatv lineWidth, ColourF colour)
        {
            Shapes.CircleShader.ColourSource = ColourSource.Discard;
            DrawArcP(dc, a, b, curve, lineWidth, colour);
        }
        public static void DrawFilledArc(this IDrawingContext dc, Vector2 a, Vector2 b, floatv curve, floatv lineWidth, ColourF borderColour, Colour colour)
        {
            Shapes.CircleShader.ColourSource = ColourSource.UniformColour;
            Shapes.CircleShader.Colour = colour;
            DrawArcP(dc, a, b, curve, lineWidth, borderColour);
        }
        public static void DrawFilledArc(this IDrawingContext dc, Vector2 a, Vector2 b, floatv curve, Colour colour)
        {
            Shapes.CircleShader.ColourSource = ColourSource.UniformColour;
            Shapes.CircleShader.Colour = colour;
            DrawArcP(dc, a, b, curve, 0, ColourF.Zero);
        }
        public static void DrawFilledArc(this IDrawingContext dc, Vector2 a, Vector2 b, floatv curve, ITexture texture)
        {
            Shapes.CircleShader.ColourSource = ColourSource.Texture;
            Shapes.CircleShader.Texture = texture;
            DrawArcP(dc, a, b, curve, 0, ColourF.Zero);
        }

        public static void DrawTriangle(this IDrawingContext dc, Vector2 a, Vector2 b, Vector2 c, ColourF colour)
        {
            dc.Shader = Shapes.BasicShader;
            Shapes.BasicShader.Colour = colour;
            Shapes.BasicShader.ColourSource = ColourSource.UniformColour;
            
            IMatrix model = dc.Model;
            Matrix4 m;
            if (model is Matrix4 mod) { m = mod; }
            else { m = new Matrix4(model); }
            if (dc.RenderState.postMatrixMods)
            {
                dc.Model = m * CreateTriangle(a, b, c);
            }
            else
            {
                dc.Model = CreateTriangle(a, b, c) * m;
            }
            
            dc.Draw(Shapes.Triangle);
            dc.Model = model;
        }
        public static void DrawTriangle(this IDrawingContext dc, Triangle2 tri, ColourF colour)
        {
            dc.Shader = Shapes.BasicShader;
            Shapes.BasicShader.Colour = colour;
            Shapes.BasicShader.ColourSource = ColourSource.UniformColour;

            IMatrix model = dc.Model;
            Matrix4 m;
            if (model is Matrix4 mod) { m = mod; }
            else { m = new Matrix4(model); }
            if (dc.RenderState.postMatrixMods)
            {
                dc.Model = m * CreateTriangle(tri.A, tri.B, tri.C);
            }
            else
            {
                dc.Model = CreateTriangle(tri.A, tri.B, tri.C) * m;
            }
            
            dc.Draw(Shapes.Triangle);
            dc.Model = model;
        }
        private static Matrix4 CreateTriangle(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            floatv e = p1.X;
            floatv f = p1.Y;
            floatv a = p2.X - e;
            floatv b = p2.Y - f;
            floatv c = p3.X - e;
            floatv d = p3.Y - f;

            return new Matrix4(
                new Vector4(a, b, 0, 0),
                new Vector4(c, d, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(e, f, 0, 1));
        }
    }
}
