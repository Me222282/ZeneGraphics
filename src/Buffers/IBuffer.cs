using System;
using Zene.Graphics.Base;

namespace Zene.Graphics
{
    [Flags]
    public enum MappedAccessFlags : uint
    {
        Read = GLEnum.MapReadBit,
        Write = GLEnum.MapWriteBit,
        Persistent = GLEnum.MapPersistentBit,
        Coherent = GLEnum.MapCoherentBit,

        InvalidateRange = GLEnum.MapInvalidateRangeBit,
        InvalidateBuffer = GLEnum.MapInvalidateBufferBit,
        FlushExplicit = GLEnum.MapFlushExplicitBit,
        Unsynchronized = GLEnum.MapUnsynchronizedBit,
    }

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
        CopyRepeated = GLEnum.DynamicCopy,

        /// <summary>
        /// The contents of the data store may be updated after creation through sub data calls.
        /// </summary>
        DynamicStorage = GLEnum.DynamicStorageBit,
        /// <summary>
        /// The data store may be mapped by the client for read access.
        /// </summary>
        Read = GLEnum.MapReadBit,
        /// <summary>
        /// The data store may be mapped by the client for write access.
        /// </summary>
        Write = GLEnum.MapWriteBit,
        /// <summary>
        /// The client may request that the server read from or write to the buffer while it is mapped.
        /// </summary>
        Persistant = GLEnum.MapPersistentBit,
        /// <summary>
        /// Shared access to buffers that are simultaneously mapped for client access and are used by the server will be coherent.
        /// </summary>
        Coherent = GLEnum.MapCoherentBit,
        /// <summary>
        /// When all other criteria for the buffer storage allocation are met, this bit may be used by an implementation
        /// to determine whether to use storage that is local to the server or to the client to serve as the backing store for the buffer.
        /// </summary>
        ClientStorage = GLEnum.ClientStorageBit
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

    public enum DrawMode : uint
    {
        Points = GLEnum.Points,
        LineStrip = GLEnum.LineStrip,
        LineLoop = GLEnum.LineLoop,
        Lines = GLEnum.Lines,
        [OpenGLSupport(3.2)]
        LineStripAdjacency = GLEnum.LineStripAdjacency,
        [OpenGLSupport(3.2)]
        LinesAdjacency = GLEnum.LinesAdjacency,
        TriangleStrip = GLEnum.TriangleStrip,
        TriangleFan = GLEnum.TriangleFan,
        Triangles = GLEnum.Triangles,
        [OpenGLSupport(3.2)]
        TriangleStripAdjacency = GLEnum.TriangleStripAdjacency,
        [OpenGLSupport(3.2)]
        TrianglesAdjacency = GLEnum.TrianglesAdjacency,
        Patches = GLEnum.Patches
    }

    public enum IndexType : uint
    {
        Byte = GLEnum.Byte,
        Ushort = GLEnum.UShort,
        Uint = GLEnum.UInt
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

        /// <summary>
        /// The properties of this Buffer.
        /// </summary>
        public BufferProperties Properties { get; }

        /// <summary>
        /// Binds this <see cref="IBuffer"/> to read. Used for copying.
        /// </summary>
        /// <returns>The target the object is now bound to.</returns>
        public BufferTarget BindRead();
        /// <summary>
        /// Binds this <see cref="IBuffer"/> to write. Used for copying.
        /// </summary>
        /// <returns>The target the object is now bound to.</returns>
        public BufferTarget BindWrite();
    }
}
