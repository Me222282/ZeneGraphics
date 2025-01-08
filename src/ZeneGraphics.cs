#pragma warning disable CS8981
global using floatv =
#if DOUBLE
    System.Double;
#else
    System.Single;
#endif

global using Maths =
#if DOUBLE
    System.Math;
#else
    System.MathF;
#endif
#pragma warning restore CS8981

using System;
using System.IO;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public static class ZeneGraphics
    {
        /// <summary>
        /// Determines whether <paramref name="texture"/> is bound to the current context.
        /// </summary>
        /// <param name="texture">The texture to query.</param>
        /// <returns></returns>
        public static bool Bound(this ITexture texture)
        {
            return texture == State.GetBoundTexture(texture.ReferanceSlot, texture.Target);
        }
        /// <summary>
        /// Determines whether <paramref name="texture"/> is bound to the current context.
        /// </summary>
        /// <param name="texture">The texture to query.</param>
        /// <param name="slot">The slot that the texture needs to be bounds to to return True</param>
        /// <returns></returns>
        public static bool Bound(this ITexture texture, uint slot)
        {
            return texture == State.GetBoundTexture(slot, texture.Target);
        }
        /// <summary>
        /// Determines whether <paramref name="framebuffer"/> is bound to the current context.
        /// </summary>
        /// <param name="framebuffer">The framebuffer to query.</param>
        /// <returns></returns>
        public static bool Bound(this IFramebuffer framebuffer)
        {
            return framebuffer.Binding switch
            {
                FrameTarget.FrameBuffer => framebuffer == GL.context.boundFrameBuffers.Draw &&
                                       framebuffer == GL.context.boundFrameBuffers.Read,
                FrameTarget.Read => framebuffer == GL.context.boundFrameBuffers.Read,
                FrameTarget.Draw => framebuffer == GL.context.boundFrameBuffers.Draw,
                _ => false,
            };
        }
        /// <summary>
        /// Determines whether <paramref name="framebuffer"/> is bound to the current context.
        /// </summary>
        /// <param name="framebuffer">The framebuffer to query.</param>
        /// <param name="target">The target that the framebuffer needs to be bound to to return True.</param>
        /// <returns></returns>
        public static bool Bound(this IFramebuffer framebuffer, FrameTarget target)
        {
            return target switch
            {
                FrameTarget.FrameBuffer => framebuffer == GL.context.boundFrameBuffers.Draw &&
                                       framebuffer == GL.context.boundFrameBuffers.Read,
                FrameTarget.Read => framebuffer == GL.context.boundFrameBuffers.Read,
                FrameTarget.Draw => framebuffer == GL.context.boundFrameBuffers.Draw,
                _ => false,
            };
        }
        /// <summary>
        /// Determines whether <paramref name="renderbuffer"/> is bound to the current context.
        /// </summary>
        /// <param name="renderbuffer">The renderbuffer to query.</param>
        /// <returns></returns>
        public static bool Bound(this IRenderbuffer renderbuffer)
        {
            return renderbuffer == State.GetBoundRenderbuffer();
        }
        /// <summary>
        /// Determines whether <paramref name="shaderProgram"/> is bound to the current context.
        /// </summary>
        /// <param name="shaderProgram">The shader program to query.</param>
        /// <returns></returns>
        public static bool Bound(this IShaderProgram shaderProgram)
        {
            return shaderProgram == State.GetBoundShaderProgram();
        }
        /// <summary>
        /// Determines whether <paramref name="buffer"/> is bound to the current context.
        /// </summary>
        /// <param name="buffer">The buffer to query.</param>
        /// <returns></returns>
        public static bool Bound(this IBuffer buffer)
        {
            return buffer == State.GetBoundBuffer(buffer.Target);
        }
        /// <summary>
        /// Determines whether <paramref name="buffer"/> is bound to <paramref name="target"/> in the current context.
        /// </summary>
        /// <param name="buffer">The buffer to query.</param>
        /// <param name="target">The target that <paramref name="buffer"/> needs to be bound to to return True.</param>
        /// <returns></returns>
        public static bool Bound(this IBuffer buffer, BufferTarget target)
        {
            return buffer == State.GetBoundBuffer(target);
        }
        /// <summary>
        /// Determines whether <paramref name="vertexArray"/> is bound to the current context.
        /// </summary>
        /// <param name="vertexArray">The vertex array to query.</param>
        /// <returns></returns>
        public static bool Bound(this IVertexArray vertexArray)
        {
            return vertexArray == State.GetBoundVertexArray() &&
                (vertexArray == null ||
                vertexArray.Properties._elementBuffer == State.GetBoundBuffer(BufferTarget.ElementArray));
        }

        /// <summary>
        /// Dtermines whether the internal format <paramref name="format"/> represents a compressed format.
        /// </summary>
        /// <param name="format">The format to query.</param>
        /// <returns></returns>
        public static bool IsCompressed(this TextureFormat format)
        {
            return format == TextureFormat.CompressedRed ||
                format == TextureFormat.CompressedRedRgtc1 ||
                format == TextureFormat.CompressedRg ||
                format == TextureFormat.CompressedRgb ||
                format == TextureFormat.CompressedRgba ||
                format == TextureFormat.CompressedRgbaBptcUnorm ||
                format == TextureFormat.CompressedRgbBptcSignedFloat ||
                format == TextureFormat.CompressedRgRgtc2 ||
                format == TextureFormat.CompressedSignedRedRgtc1 ||
                format == TextureFormat.CompressedSignedRgRgtc2 ||
                format == TextureFormat.CompressedSrgb ||
                format == TextureFormat.CompressedSrgba ||
                format == TextureFormat.CompressedSrgbaBptcUnorm;
        }
        /// <summary>
        /// Dtermines whether the internal format <paramref name="format"/> represents a depth component format.
        /// </summary>
        /// <param name="format">The format to query.</param>
        /// <returns></returns>
        public static bool IsDepth(this TextureFormat format)
        {
            return format == TextureFormat.DepthComponent ||
                format == TextureFormat.DepthComponent16 ||
                format == TextureFormat.DepthComponent24 ||
                format == TextureFormat.DepthComponent32 ||
                format == TextureFormat.DepthComponent32f ||
                format == TextureFormat.DepthStencil ||
                format == TextureFormat.Depth24Stencil8 ||
                format == TextureFormat.Depth32fStencil8;
        }
        /// <summary>
        /// Dtermines whether the internal format <paramref name="format"/> represents a format containing a stencil storage.
        /// </summary>
        /// <param name="format">The format to query.</param>
        /// <returns></returns>
        public static bool HasStencil(this TextureFormat format)
        {
            return format == TextureFormat.DepthStencil ||
                format == TextureFormat.Depth24Stencil8 ||
                format == TextureFormat.Depth32fStencil8;
        }

        /// <summary>
        /// Determines whether <paramref name="usage"/> is a usage type used for immutable buffers.
        /// </summary>
        /// <param name="usage">The <see cref="BufferUsage"/> to query.</param>
        /// <returns>True if <paramref name="usage"/> is a usage used for immutable buffers.</returns>
        public static bool IsStorageUsage(this BufferUsage usage)
        {
            return usage switch
            {
                BufferUsage.DynamicStorage or BufferUsage.ClientStorage or BufferUsage.Coherent or
                    BufferUsage.Persistant or BufferUsage.Read or BufferUsage.Write => true,
                _ => false
            };
        }

        /// <summary>
        /// Determines whether <paramref name="access"/> is readable or not.
        /// </summary>
        /// <param name="access">The <see cref="AccessType"/> to query.</param>
        /// <returns></returns>
        public static bool CanRead(this AccessType access)
        {
            return access == AccessType.Read || access == AccessType.ReadWrte;
        }
        /// <summary>
        /// Determines whether <paramref name="access"/> is writable or not.
        /// </summary>
        /// <param name="access">The <see cref="AccessType"/> to query.</param>
        /// <returns></returns>
        public static bool CanWrite(this AccessType access)
        {
            return access == AccessType.Write || access == AccessType.ReadWrte;
        }

        /// <summary>
        /// Determine if an object corresponds to a texture.
        /// </summary>
        /// <param name="obj">Specifies an object that may be a texture.</param>
        public static bool IsTexture(this IIdentifiable obj)
        {
            return GL.IsTexture(obj.Id);
        }
        /// <summary>
        /// Determine if an object corresponds to a renderbuffer.
        /// </summary>
        /// <param name="obj">Specifies an object that may be a renderbuffer.</param>
        public static bool IsRenderbuffer(this IIdentifiable obj)
        {
            return GL.IsRenderbuffer(obj.Id);
        }
        /// <summary>
        /// Determine if an object corresponds to a framebuffer.
        /// </summary>
        /// <param name="obj">Specifies an object that may be a framebuffer.</param>
        public static bool IsFrameBuffer(this IIdentifiable obj)
        {
            return GL.IsFramebuffer(obj.Id);
        }
        /// <summary>
        /// Determine if an object corresponds to a shader object.
        /// </summary>
        /// <param name="obj">Specifies an object that may be a shader.</param>
        public static bool IsShader(this IIdentifiable obj)
        {
            return GL.IsShader(obj.Id);
        }
        /// <summary>
        /// Determine if an object corresponds to a program object.
        /// </summary>
        /// <param name="obj">Specifies an object that may be a program.</param>
        public static bool IsShaderProgram(this IIdentifiable obj)
        {
            return GL.IsProgram(obj.Id);
        }
        /// <summary>
        /// Determine if a name corresponds to a buffer object.
        /// </summary>
        /// <param name="obj">Specifies a value that may be the name of a buffer object.</param>
        public static bool IsBuffer(this IIdentifiable obj)
        {
            return GL.IsBuffer(obj.Id);
        }

        /// <summary>
        /// Determines whether the texture target <paramref name="target"/> represents a 1d texture.
        /// </summary>
        /// <param name="target">Teh texture taret to check.</param>
        /// <returns></returns>
        public static bool Is1D(this TextureTarget target)
        {
            return target == TextureTarget.Texture1D;
        }
        /// <summary>
        /// Determines whether the texture target <paramref name="target"/> represents a 2d texture.
        /// </summary>
        /// <param name="target">Teh texture taret to check.</param>
        /// <returns></returns>
        public static bool Is2D(this TextureTarget target)
        {
            return target == TextureTarget.Rectangle ||
                target == TextureTarget.Texture2D ||
                target == TextureTarget.Texture1DArray ||
                target == TextureTarget.Multisample2D;
        }
        /// <summary>
        /// Determines whether the texture target <paramref name="target"/> represents a 3d texture.
        /// </summary>
        /// <param name="target">Teh texture taret to check.</param>
        /// <returns></returns>
        public static bool Is3D(this TextureTarget target)
        {
            return target == TextureTarget.Multisample2DArray ||
                target == TextureTarget.Texture2DArray ||
                target == TextureTarget.Texture3D;
        }
        /// <summary>
        /// Determines whether the texture target <paramref name="target"/> represents a multisample texture.
        /// </summary>
        /// <param name="target">Teh texture taret to check.</param>
        /// <returns></returns>
        public static bool IsMultisample(this TextureTarget target)
        {
            return target == TextureTarget.Multisample2D ||
                target == TextureTarget.Multisample2DArray;
        }

        /// <summary>
        /// Determines whether <paramref name="texture"/> stores data in a compressed format.
        /// </summary>
        /// <param name="texture">The texture to check.</param>
        /// <returns></returns>
        public static bool IsCompressed(this ITexture texture)
        {
            return texture.InternalFormat.IsCompressed();
        }
        /// <summary>
        /// Returns the layer that <paramref name="face"/> would represent in a cubemap texture.
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        public static int ToLayer(this CubeMapFace face)
        {
            return face switch
            {
                CubeMapFace.Right => 0,
                CubeMapFace.Left => 1,
                CubeMapFace.Top => 2,
                CubeMapFace.Bottom => 3,
                CubeMapFace.Front => 4,
                CubeMapFace.Back => 5,
                _ => (int)face
            };
        }

        /// <summary>
        /// Gets the channel size of the texture format represented by <paramref name="format"/>.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static int GetSize(this BaseFormat format)
        {
            return format switch
            {
                BaseFormat.R => 1,
                BaseFormat.G => 1,
                BaseFormat.B => 1,
                BaseFormat.RedInteger => 1,
                BaseFormat.GreenInteger => 1,
                BaseFormat.BlueInteger => 1,
                BaseFormat.DepthComponent => 1,
                BaseFormat.DepthStencil => 1,
                BaseFormat.StencilIndex => 1,

                BaseFormat.Rg => 2,
                BaseFormat.RgInteger => 2,

                BaseFormat.Bgr => 3,
                BaseFormat.Rgb => 3,
                BaseFormat.BgrInteger => 3,
                BaseFormat.RgbInteger => 3,

                BaseFormat.Bgra => 4,
                BaseFormat.BgraInteger => 4,
                BaseFormat.Rgba => 4,
                BaseFormat.RgbaInteger => 4,

                _ => (int)format
            };
        }
        /// <summary>
        /// Gets the byte size of the data type represented by <paramref name="type"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetSize(this TextureData type)
        {
            return type switch
            {
                TextureData.Byte => 1,
                TextureData.SByte => 1,
                TextureData.UByte332 => 1,
                TextureData.UByte233Reversed => 1,

                TextureData.Short => 2,
                TextureData.UShort => 2,
                TextureData.UShort1555Reversed => 2,
                TextureData.UShort4444 => 2,
                TextureData.UShort4444Reversed => 2,
                TextureData.UShort5551 => 2,
                TextureData.UShort565 => 2,
                TextureData.UShort565Reversed => 2,
                TextureData.HalfFloat => 2,

                TextureData.Int => 4,
                TextureData.UInt => 4,
                TextureData.UInt1010102 => 4,
                TextureData.UInt2101010Reversed => 4,
                TextureData.UInt248 => 4,
                TextureData.UInt5999Rev => 4,
                TextureData.UInt8888 => 4,
                TextureData.UInt8888Reversed => 4,
                TextureData.Float => 4,

                _ => (int)type
            };
        }

        /// <summary>
        /// Read a block of pixels from a framebuffer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="framebuffer">The framebuffer to reead pixels from.</param>
        /// <param name="x">Specify the window x coordinate of the first pixel that is read from the frame buffer. This location is the lower left corner of a rectangular block of pixels.</param>
        /// <param name="y">Specify the window y coordinate of the first pixel that is read from the frame buffer. This location is the lower left corner of a rectangular block of pixels.</param>
        /// <param name="width">Specify the width of the pixel rectangle.</param>
        /// <param name="height">Specify the height of the pixel rectangle.</param>
        /// <param name="format">Specifies the format of the returned data.</param>
        /// <param name="type">Specifies the data type of the returned data.</param>
        /// <returns></returns>
        [OpenGLSupport(1.0)]
        public static unsafe GLArray<T> ReadPixels<T>(this IFramebuffer framebuffer, int x, int y, int width, int height, BaseFormat format, TextureData type) where T : unmanaged
        {
            framebuffer.Bind();

            GLArray<T> data = new GLArray<T>(
                (width * format.GetSize() * type.GetSize()) / sizeof(T),
                height);

            GL.ReadPixels(x, y, width, height, (uint)format, (uint)type, data);

            return data;
        }
        /// <summary>
        /// Read a block of pixels from a framebuffer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="framebuffer">The framebuffer to reead pixels from.</param>
        /// <param name="bounds">The window coordinate based bounds for the block of data to be read from. It is bottom-left based.</param>
        /// <param name="format">Specifies the format of the returned data.</param>
        /// <param name="type">Specifies the data type of the returned data.</param>
        /// <returns></returns>
        [OpenGLSupport(1.0)]
        public static unsafe GLArray<T> ReadPixels<T>(this IFramebuffer framebuffer, RectangleI bounds, BaseFormat format, TextureData type) where T : unmanaged
        {
            framebuffer.Bind();

            RectangleI rect = bounds;

            GLArray<T> data = new GLArray<T>(
                (rect.Width * format.GetSize() * type.GetSize()) / sizeof(T),
                rect.Height);

            GL.ReadPixels(rect.Left, rect.Bottom, rect.Width, rect.Height, (uint)format, (uint)type, data);

            return data;
        }
        /// <summary>
        /// Read all pixels from a framebuffer based on its view size.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="framebuffer">The framebuffer to reead pixels from.</param>
        /// <param name="format">Specifies the format of the returned data.</param>
        /// <param name="type">Specifies the data type of the returned data.</param>
        /// <returns></returns>
        [OpenGLSupport(1.0)]
        public static unsafe GLArray<T> ReadPixels<T>(this IFramebuffer framebuffer, BaseFormat format, TextureData type) where T : unmanaged
        {
            framebuffer.Bind();

            GLArray<T> data = new GLArray<T>(
                (framebuffer.Properties.Width * format.GetSize() * type.GetSize()) / sizeof(T),
                framebuffer.Properties.Height);

            GL.ReadPixels(0, 0, framebuffer.Properties.Width, framebuffer.Properties.Height, (uint)format, (uint)type, data);

            return data;
        }

        public static void Clear(this IFramebuffer framebuffer, Colour colour)
        {
            ColourF f = colour;
            GL.ClearColour(f.R, f.G, f.B, f.A);

            framebuffer.Bind(FrameTarget.Draw);

            GL.Clear((uint)BufferBit.Colour);
        }
        public static void Clear(this IFramebuffer framebuffer, ColourF colour)
        {
            GL.ClearColour(colour.R, colour.G, colour.B, colour.A);

            framebuffer.Bind(FrameTarget.Draw);

            GL.Clear((uint)BufferBit.Colour);
        }
        public static void Clear(this IFramebuffer framebuffer, Colour colour, floatv depth)
        {
            ColourF f = colour;
            GL.ClearColour(f.R, f.G, f.B, f.A);
            GL.ClearDepth(depth);

            framebuffer.Bind(FrameTarget.Draw);

            GL.Clear((uint)(BufferBit.Colour | BufferBit.Depth));
        }
        public static void Clear(this IFramebuffer framebuffer, ColourF colour, floatv depth)
        {
            GL.ClearColour(colour.R, colour.G, colour.B, colour.A);
            GL.ClearDepth(depth);

            framebuffer.Bind(FrameTarget.Draw);

            GL.Clear((uint)(BufferBit.Colour | BufferBit.Depth));
        }
        public static void Clear(this IFramebuffer framebuffer, Colour colour, floatv depth, int stencil)
        {
            ColourF f = colour;
            GL.ClearColour(f.R, f.G, f.B, f.A);
            GL.ClearDepth(depth);
            GL.ClearStencil(stencil);

            framebuffer.Bind(FrameTarget.Draw);

            GL.Clear((uint)(BufferBit.Colour | BufferBit.Depth | BufferBit.Stencil));
        }
        public static void Clear(this IFramebuffer framebuffer, ColourF colour, floatv depth, int stencil)
        {
            GL.ClearColour(colour.R, colour.G, colour.B, colour.A);
            GL.ClearDepth(depth);
            GL.ClearStencil(stencil);

            framebuffer.Bind(FrameTarget.Draw);

            GL.Clear((uint)(BufferBit.Colour | BufferBit.Depth | BufferBit.Stencil));
        }
        public static void Clear(this IFramebuffer framebuffer, Colour colour, int stencil)
        {
            ColourF f = colour;
            GL.ClearColour(f.R, f.G, f.B, f.A);
            GL.ClearStencil(stencil);

            framebuffer.Bind(FrameTarget.Draw);

            GL.Clear((uint)(BufferBit.Colour | BufferBit.Stencil));
        }
        public static void Clear(this IFramebuffer framebuffer, ColourF colour, int stencil)
        {
            GL.ClearColour(colour.R, colour.G, colour.B, colour.A);
            GL.ClearStencil(stencil);

            framebuffer.Bind(FrameTarget.Draw);

            GL.Clear((uint)(BufferBit.Colour | BufferBit.Stencil));
        }
        public static void Clear(this IFramebuffer framebuffer, floatv depth)
        {
            GL.ClearDepth(depth);

            framebuffer.Bind(FrameTarget.Draw);

            GL.Clear((uint)BufferBit.Depth);
        }
        public static void Clear(this IFramebuffer framebuffer, floatv depth, int stencil)
        {
            GL.ClearDepth(depth);
            GL.ClearStencil(stencil);

            framebuffer.Bind(FrameTarget.Draw);

            GL.Clear((uint)(BufferBit.Depth | BufferBit.Stencil));
        }
        public static void Clear(this IFramebuffer framebuffer, int stencil)
        {
            GL.ClearStencil(stencil);

            framebuffer.Bind(FrameTarget.Draw);

            GL.Clear((uint)BufferBit.Stencil);
        }

        public static byte[] ReadAllBytes(this Stream stream)
        {
            if (!stream.CanRead)
            {
                throw new IOException($"Cannot read data from {nameof(stream)}.");
            }

            byte[] data = new byte[stream.Length];

            for (int i = 0; i < data.Length; i++)
            {
                int value = stream.ReadByte();

                if (value < 0) { throw new IOException(); }

                data[i] = (byte)value;
            }

            return data;
        }

        public static bool IsFloatMat(this UniformType ut)
        {
            return (UniformType.FMat2 <= ut && ut <= UniformType.FMat4) ||
                (UniformType.FMat2x3 <= ut && ut <= UniformType.FMat4x3);
        }
        public static bool IsDoubleMat(this UniformType ut)
        {
            return UniformType.DMat2 <= ut && ut <= UniformType.DMat4x3;
        }
    }
}
