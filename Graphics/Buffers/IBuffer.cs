using System;
using Zene.Graphics.OpenGL;

namespace Zene.Graphics
{
    /// <summary>
    /// The frequency of buffer data being get and set.
    /// </summary>
    public enum BufferUsage : uint
    {
        /// <summary>
        /// Data is set once and infrequently referanced to draw.
        /// </summary>
        DrawInfrequent = GLEnum.StreamDraw,
        /// <summary>
        /// Data is set once and frequently referanced to draw.
        /// </summary>
        DrawFrequent = GLEnum.StaticDraw,
        /// <summary>
        /// Data is set frequently and frequently referanced to draw.
        /// </summary>
        DrawRepeated = GLEnum.DynamicDraw,
        /// <summary>
        /// Data is set once and infrequently referanced to retreive by the application.
        /// </summary>
        ReadInfrequent = GLEnum.StreamRead,
        /// <summary>
        /// Data is set once and frequently referanced to retreive by the application.
        /// </summary>
        ReadFrequent = GLEnum.StaticRead,
        /// <summary>
        /// Data is set frequently and frequently referanced to retreive by the application.
        /// </summary>
        ReadRepeated = GLEnum.DynamicRead,
        /// <summary>
        /// Data is set once and infrequently referanced by other OpenGL objects.
        /// </summary>
        CopyInfrequent = GLEnum.StreamCopy,
        /// <summary>
        /// Data is set once and frequently referanced to other OpenGL objects.
        /// </summary>
        CopyFrequent = GLEnum.StaticCopy,
        /// <summary>
        /// Data is set frequently and frequently referanced to other OpenGL objects.
        /// </summary>
        CopyRepeated = GLEnum.DynamicCopy
    }

    /// <summary>
    /// The type of buffer being targeted.
    /// </summary>
    public enum BufferTarget : uint
    {
        Array = GLEnum.ArrayBuffer,
        AtomicCounter = GLEnum.AtomicCounterBuffer,
        CopyRead = GLEnum.CopyReadBuffer,
        CopyWrite = GLEnum.CopyWriteBuffer,
        DispatchIndirect = GLEnum.DispatchIndirectBuffer,
        DrawIndirect = GLEnum.DrawIndirectBuffer,
        ElementArray = GLEnum.ElementArrayBuffer,
        PixelPack = GLEnum.PixelPackBuffer,
        PixelUnPack = GLEnum.PixelUnpackBuffer,
        Query = GLEnum.QueryBuffer,
        ShaderStorage = GLEnum.ShaderStorageBuffer,
        Texture = GLEnum.TextureBuffer,
        TransformFeedback = GLEnum.TransformFeedbackBuffer,
        Uniform = GLEnum.UniformBuffer
    }

    /// <summary>
    /// Object that encapsulate an OpenGL buffer object.
    /// </summary>
    public interface IBuffer : IIdentifiable, IDisposable, IBindable
    {
        /// <summary>
        /// How frequent data is get and set.
        /// </summary>
        public BufferUsage UsageType { get; }

        /// <summary>
        /// The type of this buffer.
        /// </summary>
        public BufferTarget Target { get; }
    }
}
