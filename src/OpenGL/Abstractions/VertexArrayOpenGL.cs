using System;

namespace Zene.Graphics.Base
{
    public static unsafe class VertexArrayOpenGL
    {
        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="first">Specifies the starting index in the enabled arrays.</param>
        /// <param name="size">Specifies the number of vertices to be rendered.</param>
        [OpenGLSupport(1.1)]
        public static void DrawArrays(this IDrawingContext dc, IVertexArray vertexArray, DrawMode mode, int first, int size)
        {
            dc.Bind();
            vertexArray.Bind();
            dc.SetMatrices();

            GL.DrawArrays((uint)mode, first, size);
        }
        /// <summary>
        /// Render primitives from array data, taking parameters from memory.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count"></param>
        /// <param name="primCount"></param>
        /// <param name="first"></param>
        /// <param name="baseInstance"></param>
        [OpenGLSupport(4.0)]
        public static void DrawArraysIndirect(this IDrawingContext dc, IVertexArray vertexArray, DrawMode mode, uint count,
            uint primCount, uint first, uint baseInstance)
        {
            dc.Bind();
            vertexArray.Bind();
            dc.SetMatrices();
            // Make sure there is no buffer bound for parameter reference
            State.NullBind(Target.BufferDrawIndirect);

            if (GL.Version < 4.2)
            {
                baseInstance = 0;
            }

            uint* parameters = stackalloc uint[] { count, primCount, first, baseInstance };

            GL.DrawArraysIndirect((uint)mode, &parameters[0]);
        }
        /// <summary>
        /// Render primitives from array data, taking parameters from memory.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="paramSource">The <see cref="IBuffer"/> that contains the parameter data.</param>
        /// <param name="offset">The offset into <paramref name="paramSource"/> where the parameters should be sourced from.</param>
        [OpenGLSupport(4.0)]
        public static void DrawArraysIndirect(this IDrawingContext dc, IVertexArray vertexArray, DrawMode mode,
            IBuffer paramSource, int offset)
        {
            if (paramSource == null)
            {
                throw new ArgumentNullException(nameof(paramSource));
            }

            dc.Bind();
            vertexArray.Bind();
            dc.SetMatrices();

            // Bind param buffer to param reference
            if (!paramSource.Bound(BufferTarget.DrawIndirect))
            {
                GL.BindBuffer(GLEnum.DrawIndirectBuffer, paramSource);
            }

            GL.DrawArraysIndirect((uint)mode, (void*)new IntPtr(offset));
        }
        /// <summary>
        /// Draw multiple instances of a range of elements.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="first">Specifies the starting index in the enabled arrays.</param>
        /// <param name="size">Specifies the number of vertices to be rendered.</param>
        /// <param name="instances">Specifies the number of instances of the specified range of vertices to be rendered.</param>
        [OpenGLSupport(3.1)]
        public static void DrawArraysInstanced(this IDrawingContext dc, IVertexArray vertexArray, DrawMode mode,
            int first, int size, int instances)
        {
            dc.Bind();
            vertexArray.Bind();
            dc.SetMatrices();

            GL.DrawArraysInstanced((uint)mode, first, size, instances);
        }
        /// <summary>
        /// Draw multiple instances of a range of elements with offset applied to instanced attributes.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="first">Specifies the starting index in the enabled arrays.</param>
        /// <param name="size">Specifies the number of vertices to be rendered.</param>
        /// <param name="instances">Specifies the number of instances of the specified range of vertices to be rendered.</param>
        /// <param name="baseInstance">Specifies the base instance for use in fetching instanced vertex attributes.</param>
        [OpenGLSupport(4.2)]
        public static void DrawArraysInstancedBaseInstance(this IDrawingContext dc, IVertexArray vertexArray, DrawMode mode,
            int first, int size, int instances, uint baseInstance)
        {
            dc.Bind();
            vertexArray.Bind();
            dc.SetMatrices();

            GL.DrawArraysInstancedBaseInstance((uint)mode, first, size, instances, baseInstance);
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="first">Specifies the starting index in the enabled arrays.</param>
        /// <param name="size">Specifies the number of vertices to be rendered.</param>
        [OpenGLSupport(1.1)]
        public static void DrawArrays(this IDrawingContext dc, IBuffer arrayBuffer, DrawMode mode, int first, int size)
        {
            if (arrayBuffer.Target != BufferTarget.Array)
            {
                throw new BufferException(arrayBuffer, $"Buffer must have a {BufferTarget.ElementArray} target type.");
            }

            dc.Bind();
            arrayBuffer.Bind();
            dc.SetMatrices();

            GL.DrawArrays((uint)mode, first, size);
        }
        /// <summary>
        /// Render primitives from array data, taking parameters from memory.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count"></param>
        /// <param name="primCount"></param>
        /// <param name="first"></param>
        /// <param name="baseInstance"></param>
        [OpenGLSupport(4.0)]
        public static void DrawArraysIndirect(this IDrawingContext dc, IBuffer arrayBuffer, DrawMode mode, uint count,
            uint primCount, uint first, uint baseInstance)
        {
            if (arrayBuffer.Target != BufferTarget.Array)
            {
                throw new BufferException(arrayBuffer, $"Buffer must have a {BufferTarget.ElementArray} target type.");
            }

            dc.Bind();
            arrayBuffer.Bind();
            dc.SetMatrices();
            // Make sure there is no buffer bound for parameter reference
            State.NullBind(Target.BufferDrawIndirect);

            if (GL.Version < 4.2)
            {
                baseInstance = 0;
            }

            uint* parameters = stackalloc uint[] { count, primCount, first, baseInstance };

            GL.DrawArraysIndirect((uint)mode, &parameters[0]);
        }
        /// <summary>
        /// Render primitives from array data, taking parameters from memory.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="paramSource">The <see cref="IBuffer"/> that contains the parameter data.</param>
        /// <param name="offset">The offset into <paramref name="paramSource"/> where the parameters should be sourced from.</param>
        [OpenGLSupport(4.0)]
        public static void DrawArraysIndirect(this IDrawingContext dc, IBuffer arrayBuffer, DrawMode mode,
            IBuffer paramSource, int offset)
        {
            if (arrayBuffer.Target != BufferTarget.Array)
            {
                throw new BufferException(arrayBuffer, $"Buffer must have a {BufferTarget.ElementArray} target type.");
            }

            if (paramSource == null)
            {
                throw new ArgumentNullException(nameof(paramSource));
            }

            dc.Bind();
            arrayBuffer.Bind();
            dc.SetMatrices();

            // Bind param buffer to param reference
            if (!paramSource.Bound(BufferTarget.DrawIndirect))
            {
                GL.BindBuffer(GLEnum.DrawIndirectBuffer, paramSource);
            }

            GL.DrawArraysIndirect((uint)mode, (void*)new IntPtr(offset));
        }
        /// <summary>
        /// Draw multiple instances of a range of elements.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="first">Specifies the starting index in the enabled arrays.</param>
        /// <param name="size">Specifies the number of vertices to be rendered.</param>
        /// <param name="instances">Specifies the number of instances of the specified range of vertices to be rendered.</param>
        [OpenGLSupport(3.1)]
        public static void DrawArraysInstanced(this IDrawingContext dc, IBuffer arrayBuffer, DrawMode mode,
            int first, int size, int instances)
        {
            if (arrayBuffer.Target != BufferTarget.Array)
            {
                throw new BufferException(arrayBuffer, $"Buffer must have a {BufferTarget.ElementArray} target type.");
            }

            dc.Bind();
            arrayBuffer.Bind();
            dc.SetMatrices();

            GL.DrawArraysInstanced((uint)mode, first, size, instances);
        }
        /// <summary>
        /// Draw multiple instances of a range of elements with offset applied to instanced attributes.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="first">Specifies the starting index in the enabled arrays.</param>
        /// <param name="size">Specifies the number of vertices to be rendered.</param>
        /// <param name="instances">Specifies the number of instances of the specified range of vertices to be rendered.</param>
        /// <param name="baseInstance">Specifies the base instance for use in fetching instanced vertex attributes.</param>
        [OpenGLSupport(4.2)]
        public static void DrawArraysInstancedBaseInstance(this IDrawingContext dc, IBuffer arrayBuffer, DrawMode mode,
            int first, int size, int instances, uint baseInstance)
        {
            if (arrayBuffer.Target != BufferTarget.Array)
            {
                throw new BufferException(arrayBuffer, $"Buffer must have a {BufferTarget.ElementArray} target type.");
            }

            dc.Bind();
            arrayBuffer.Bind();
            dc.SetMatrices();

            GL.DrawArraysInstancedBaseInstance((uint)mode, first, size, instances, baseInstance);
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        [OpenGLSupport(1.1)]
        public static void DrawElements(this IDrawingContext dc, IVertexArray vertexArray, DrawMode mode, int count,
            IndexType type, int offset)
        {
            dc.Bind();
            vertexArray.Bind();
            dc.SetMatrices();

            GL.DrawElements((uint)mode, count, (uint)type, new IntPtr(offset));
        }
        /// <summary>
        /// Render primitives from array data with a per-element offset.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        /// <param name="baseVertex">Specifies a constant that should be added to each element of the array when being referenced.</param>
        [OpenGLSupport(3.2)]
        public static void DrawElementsBaseVertex(this IDrawingContext dc, IVertexArray vertexArray, DrawMode mode, int count,
            IndexType type, int offset, int baseVertex)
        {
            dc.Bind();
            vertexArray.Bind();
            dc.SetMatrices();

            GL.DrawElementsBaseVertex((uint)mode, count, (uint)type, (void*)new IntPtr(offset), baseVertex);
        }
        /// <summary>
        /// Render indexed primitives from array data, taking parameters from memory.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="type">Specifies the type of data in this element buffer.</param>
        /// <param name="count"></param>
        /// <param name="primCount"></param>
        /// <param name="first"></param>
        /// <param name="baseInstance"></param>
        [OpenGLSupport(4.0)]
        public static void DrawElementsIndirect(this IDrawingContext dc, IVertexArray vertexArray, DrawMode mode, IndexType type,
            uint count, uint primCount, uint first, uint baseInstance)
        {
            dc.Bind();
            vertexArray.Bind();
            dc.SetMatrices();
            // Make sure there is no buffer bound for parameter reference
            State.NullBind(Target.BufferDrawIndirect);

            if (GL.Version < 4.2)
            {
                baseInstance = 0;
            }

            uint* parameters = stackalloc uint[] { count, primCount, first, baseInstance };

            GL.DrawElementsIndirect((uint)mode, (uint)type, &parameters[0]);
        }
        /// <summary>
        /// Render indexed primitives from array data, taking parameters from memory.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="type">Specifies the type of data in this element buffer.</param>
        /// <param name="paramSource">The <see cref="IBuffer"/> that contains the parameter data.</param>
        /// <param name="offset">The offset into <paramref name="paramSource"/> where the parameters should be sourced from.</param>
        [OpenGLSupport(4.0)]
        public static void DrawElementsIndirect(this IDrawingContext dc, IVertexArray vertexArray, DrawMode mode, IndexType type,
            IBuffer paramSource, int offset)
        {
            if (paramSource == null)
            {
                throw new ArgumentNullException(nameof(paramSource));
            }

            dc.Bind();
            vertexArray.Bind();
            dc.SetMatrices();

            // Bind param buffer to param reference
            if (!paramSource.Bound(BufferTarget.DrawIndirect))
            {
                GL.BindBuffer(GLEnum.DrawIndirectBuffer, paramSource);
            }

            GL.DrawElementsIndirect((uint)mode, (uint)type, &offset);
        }
        /// <summary>
        /// Draw multiple instances of a set of elements with offset applied to instanced attributes.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        /// <param name="instances">Specifies the number of instances to render.</param>
        [OpenGLSupport(3.1)]
        public static void DrawElementsInstanced(this IDrawingContext dc, IVertexArray vertexArray, DrawMode mode, int count,
            IndexType type, int offset, int instances)
        {
            dc.Bind();
            vertexArray.Bind();
            dc.SetMatrices();

            GL.DrawElementsInstanced((uint)mode, count, (uint)type, (void*)new IntPtr(offset), instances);
        }
        /// <summary>
        /// Draw multiple instances of a set of elements.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        /// <param name="instances">Specifies the number of instances to render.</param>
        /// <param name="baseInstance">Specifies the base instance for use in fetching instanced vertex attributes.</param>
        [OpenGLSupport(4.2)]
        public static void DrawElementsInstancedBaseInstance(this IDrawingContext dc, IVertexArray vertexArray, DrawMode mode, int count,
            IndexType type, int offset, int instances, uint baseInstance)
        {
            dc.Bind();
            vertexArray.Bind();
            dc.SetMatrices();

            GL.DrawElementsInstancedBaseInstance((uint)mode, count, (uint)type, (void*)new IntPtr(offset), instances, baseInstance);
        }
        /// <summary>
        /// Render multiple instances of a set of primitives from array data with a per-element offset.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        /// <param name="instances">Specifies the number of instances to render.</param>
        /// <param name="baseVertex">Specifies a constant that should be added to each element of the array when being referenced.</param>
        [OpenGLSupport(3.2)]
        public static void DrawElementsInstancedBaseVertex(this IDrawingContext dc, IVertexArray vertexArray, DrawMode mode, int count,
            IndexType type, int offset, int instances, int baseVertex)
        {
            dc.Bind();
            vertexArray.Bind();
            dc.SetMatrices();

            GL.DrawElementsInstancedBaseVertex((uint)mode, count, (uint)type, (void*)new IntPtr(offset), instances, baseVertex);
        }
        /// <summary>
        /// Render multiple instances of a set of primitives from array data with a per-element offset.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        /// <param name="instances">Specifies the number of instances to render.</param>
        /// <param name="baseVertex">Specifies a constant that should be added to each element of the array when being referenced.</param>
        /// <param name="baseInstance">Specifies the base instance for use in fetching instanced vertex attributes.</param>
        [OpenGLSupport(3.2)]
        public static void DrawElementsInstancedBaseVertexBaseInstance(this IDrawingContext dc, IVertexArray vertexArray, DrawMode mode, int count,
            IndexType type, int offset,int instances, int baseVertex, uint baseInstance)
        {
            dc.Bind();
            vertexArray.Bind();
            dc.SetMatrices();

            GL.DrawElementsInstancedBaseVertexBaseInstance((uint)mode, count, (uint)type, (void*)new IntPtr(offset), instances, baseVertex, baseInstance);
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="start">Specifies the minimum array index contained in this element buffer.</param>
        /// <param name="end">Specifies the maximum array index contained in this element buffer.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        public static void DrawRangeElements(this IDrawingContext dc, IVertexArray vertexArray, DrawMode mode, uint start, uint end, int count,
            IndexType type, int offset)
        {
            dc.Bind();
            vertexArray.Bind();
            dc.SetMatrices();

            GL.DrawRangeElements((uint)mode, start, end, count, (uint)type, (void*)new IntPtr(offset));
        }
        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="start">Specifies the minimum array index contained in this element buffer.</param>
        /// <param name="end">Specifies the maximum array index contained in this element buffer.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in this element buffer.</param>
        /// <param name="offset">Specifies an offset of the first index in the array of this element buffer.</param>
        /// <param name="baseVertex">Specifies a constant that should be added to each element of the array when being referenced.</param>
        [OpenGLSupport(3.2)]
        public static void DrawRangeElementsBaseVertex(this IDrawingContext dc, IVertexArray vertexArray, DrawMode mode, uint start, uint end,
            int count, IndexType type, int offset, int baseVertex)
        {
            dc.Bind();
            vertexArray.Bind();
            dc.SetMatrices();

            GL.DrawRangeElementsBaseVertex((uint)mode, start, end, count, (uint)type, (void*)new IntPtr(offset), baseVertex);
        }
    }
}
