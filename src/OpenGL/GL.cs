// Copyright (c) 2017-2019 Zachary Snow
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Text;

namespace Zene.Graphics.Base
{
	public static unsafe partial class GL
	{
		public struct BufferBinding
		{
			public uint Array;
			public uint AtomicCounter;
			public uint CopyRead;
			public uint CopyWrite;
			public uint DispatchIndirect;
			public uint DrawIndirect;
			public uint ElementArray;
			public uint PixelPack;
			public uint PixelUnpack;
			public uint Query;
			public uint ShaderStorage;
			public uint Texture;
			public uint TransformFeedback;
			public uint Uniform;
		}

		[OpenGLSupport(4.1)]
		public static void ActiveShaderProgram(uint pipeline, uint program)
		{
			Functions.ActiveShaderProgram(pipeline, program);
		}

		[OpenGLSupport(2.0)]
		public static void AttachShader(uint program, uint shader)
		{
			Functions.AttachShader(program, shader);
		}

		[OpenGLSupport(3.0)]
		public static void BeginConditionalRender(uint id, uint mode)
		{
			Functions.BeginConditionalRender(id, mode);
		}

		[OpenGLSupport(1.5)]
		public static void BeginQuery(uint target, uint id)
		{
			Functions.BeginQuery(target, id);
		}

		[OpenGLSupport(4.0)]
		public static void BeginQueryIndexed(uint target, uint index, uint id)
		{
			Functions.BeginQueryIndexed(target, index, id);
		}

		[OpenGLSupport(3.0)]
		public static void BeginTransformFeedback(uint primitiveMode)
		{
			Functions.BeginTransformFeedback(primitiveMode);
		}

		[OpenGLSupport(2.0)]
		public static void BindAttribLocation(uint program, uint index, string name)
		{
			Functions.BindAttribLocation(program, index, name);
		}

		[OpenGLSupport(1.5)]
		public static void BindBuffer(uint target, uint buffer)
		{
			Functions.BindBuffer(target, buffer);

			switch (target)
			{
				case GLEnum.ArrayBuffer:
					context.boundBuffers.Array = buffer;
					return;
				case GLEnum.AtomicCounterBuffer:
					context.boundBuffers.AtomicCounter = buffer;
					return;
				case GLEnum.CopyReadBuffer:
					context.boundBuffers.CopyRead = buffer;
					return;
				case GLEnum.CopyWriteBuffer:
					context.boundBuffers.CopyWrite = buffer;
					return;
				case GLEnum.DispatchIndirectBuffer:
					context.boundBuffers.DispatchIndirect = buffer;
					return;
				case GLEnum.DrawIndirectBuffer:
					context.boundBuffers.DrawIndirect = buffer;
					return;
				case GLEnum.ElementArrayBuffer:
					context.boundBuffers.ElementArray = buffer;
					return;
				case GLEnum.PixelPackBuffer:
					context.boundBuffers.PixelPack = buffer;
					return;
				case GLEnum.PixelUnpackBuffer:
					context.boundBuffers.PixelUnpack = buffer;
					return;
				case GLEnum.QueryBuffer:
					context.boundBuffers.Query = buffer;
					return;
				case GLEnum.ShaderStorageBuffer:
					context.boundBuffers.ShaderStorage = buffer;
					return;
				case GLEnum.TextureBuffer:
					context.boundBuffers.Texture = buffer;
					return;
				case GLEnum.TransformFeedbackBuffer:
					context.boundBuffers.TransformFeedback = buffer;
					return;
				case GLEnum.UniformBuffer:
					context.boundBuffers.Uniform = buffer;
					return;
			}
		}

		[OpenGLSupport(3.0)]
		public static void BindBufferBase(uint target, uint index, uint buffer)
		{
			Functions.BindBufferBase(target, index, buffer);
		}

		[OpenGLSupport(3.0)]
		public static void BindBufferRange(uint target, uint index, uint buffer, int offset, int size)
		{
			Functions.BindBufferRange(target, index, buffer, offset, size);
		}

		[OpenGLSupport(4.4)]
		public static void BindBuffersBase(uint target, uint first, int count, uint* buffers)
		{
			Functions.BindBuffersBase(target, first, count, buffers);
		}

		[OpenGLSupport(4.4)]
		public static void BindBuffersRange(uint target, uint first, int count, uint* buffers, int* offsets, int* sizes)
		{
			Functions.BindBuffersRange(target, first, count, buffers, offsets, sizes);
		}

		[OpenGLSupport(3.0)]
		public static void BindFragDataLocation(uint program, uint colour, string name)
		{
			Functions.BindFragDataLocation(program, colour, name);
		}

		[OpenGLSupport(3.3)]
		public static void BindFragDataLocationIndexed(uint program, uint colourNumber, uint index, string name)
		{
			Functions.BindFragDataLocationIndexed(program, colourNumber, index, name);
		}

		[OpenGLSupport(4.1)]
		public static void BindProgramPipeline(uint pipeline)
		{
			Functions.BindProgramPipeline(pipeline);
		}

		[OpenGLSupport(3.3)]
		public static void BindSampler(uint unit, uint sampler)
		{
			Functions.BindSampler(unit, sampler);
		}

		[OpenGLSupport(4.4)]
		public static void BindSamplers(uint first, int count, uint* samplers)
		{
			Functions.BindSamplers(first, count, samplers);
		}

		[OpenGLSupport(4.0)]
		public static void BindTransformFeedback(uint target, uint id)
		{
			Functions.BindTransformFeedback(target, id);
		}

		[OpenGLSupport(3.0)]
		public static void BindVertexArray(uint array)
		{
			Functions.BindVertexArray(array);
		}

		[OpenGLSupport(4.3)]
		public static void BindVertexBuffer(uint bindingindex, uint buffer, int offset, int stride)
		{
			Functions.BindVertexBuffer(bindingindex, buffer, offset, stride);
		}

		[OpenGLSupport(4.4)]
		public static void BindVertexBuffers(uint first, int count, uint* buffers, int* offsets, int* strides)
		{
			Functions.BindVertexBuffers(first, count, buffers, offsets, strides);
		}

		[OpenGLSupport(1.4)]
		public static void BlendColour(float red, float green, float blue, float alpha)
		{
			Functions.BlendColour(red, green, blue, alpha);
		}

		[OpenGLSupport(1.4)]
		public static void BlendEquation(uint mode)
		{
			Functions.BlendEquation(mode);
		}

		[OpenGLSupport(4.0)]
		public static void BlendEquationi(uint buf, uint mode)
		{
			Functions.BlendEquationi(buf, mode);
		}

		[OpenGLSupport(2.0)]
		public static void BlendEquationSeparate(uint modeRGB, uint modeAlpha)
		{
			Functions.BlendEquationSeparate(modeRGB, modeAlpha);
		}

		[OpenGLSupport(4.0)]
		public static void BlendEquationSeparatei(uint buf, uint modeRGB, uint modeAlpha)
		{
			Functions.BlendEquationSeparatei(buf, modeRGB, modeAlpha);
		}

		[OpenGLSupport(1.0)]
		public static void BlendFunc(uint sfactor, uint dfactor)
		{
			Functions.BlendFunc(sfactor, dfactor);
		}

		[OpenGLSupport(4.0)]
		public static void BlendFunci(uint buf, uint src, uint dst)
		{
			Functions.BlendFunci(buf, src, dst);
		}

		[OpenGLSupport(1.4)]
		public static void BlendFuncSeparate(uint sfactorRGB, uint dfactorRGB, uint sfactorAlpha, uint dfactorAlpha)
		{
			Functions.BlendFuncSeparate(sfactorRGB, dfactorRGB, sfactorAlpha, dfactorAlpha);
		}

		[OpenGLSupport(4.0)]
		public static void BlendFuncSeparatei(uint buf, uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha)
		{
			Functions.BlendFuncSeparatei(buf, srcRGB, dstRGB, srcAlpha, dstAlpha);
		}

		[OpenGLSupport(1.5)]
		public static void BufferData(uint target, int size, void* data, uint usage)
		{
			Functions.BufferData(target, size, data, usage);
		}

		[OpenGLSupport(4.4)]
		public static void BufferStorage(uint target, int size, void* data, uint flags)
		{
			Functions.BufferStorage(target, size, data, flags);
		}

		[OpenGLSupport(1.5)]
		public static void BufferSubData(uint target, int offset, int size, void* data)
		{
			Functions.BufferSubData(target, offset, size, data);
		}

		[OpenGLSupport(3.0)]
		public static void ClampColour(uint target, uint clamp)
		{
			Functions.ClampColour(target, clamp);
		}

		[OpenGLSupport(4.3)]
		public static void ClearBufferData(uint target, uint internalformat, uint format, uint type, void* data)
		{
			Functions.ClearBufferData(target, internalformat, format, type, data);
		}

		[OpenGLSupport(3.0)]
		public static void ClearBufferfi(uint buffer, int drawbuffer, float depth, int stencil)
		{
			Functions.ClearBufferfi(buffer, drawbuffer, depth, stencil);
		}

		[OpenGLSupport(3.0)]
		public static void ClearBufferfv(uint buffer, int drawbuffer, float* value)
		{
			Functions.ClearBufferfv(buffer, drawbuffer, value);
		}

		[OpenGLSupport(3.0)]
		public static void ClearBufferiv(uint buffer, int drawbuffer, int* value)
		{
			Functions.ClearBufferiv(buffer, drawbuffer, value);
		}

		[OpenGLSupport(4.3)]
		public static void ClearBufferSubData(uint target, uint internalformat, int offset, int size, uint format, uint type, void* data)
		{
			Functions.ClearBufferSubData(target, internalformat, offset, size, format, type, data);
		}

		[OpenGLSupport(3.0)]
		public static void ClearBufferuiv(uint buffer, int drawbuffer, uint* value)
		{
			Functions.ClearBufferuiv(buffer, drawbuffer, value);
		}

		[OpenGLSupport(4.5)]
		public static void ClearNamedBufferData(uint buffer, uint internalformat, uint format, uint type, void* data)
		{
			Functions.ClearNamedBufferData(buffer, internalformat, format, type, data);
		}

		[OpenGLSupport(4.5)]
		public static void ClearNamedBufferSubData(uint buffer, uint internalformat, int offset, int size, uint format, uint type, void* data)
		{
			Functions.ClearNamedBufferSubData(buffer, internalformat, offset, size, format, type, data);
		}

		[OpenGLSupport(3.2)]
		public static uint ClientWaitSync(IntPtr sync, uint flags, ulong timeout)
		{
			return Functions.ClientWaitSync(sync, flags, timeout);
		}

		[OpenGLSupport(4.5)]
		public static void ClipControl(uint origin, uint depth)
		{
			Functions.ClipControl(origin, depth);
		}

		[OpenGLSupport(1.0)]
		public static void ColourMask(bool red, bool green, bool blue, bool alpha)
		{
			Functions.ColourMask(red, green, blue, alpha);
		}

		[OpenGLSupport(3.0)]
		public static void ColourMaski(uint index, bool r, bool g, bool b, bool a)
		{
			Functions.ColourMaski(index, r, g, b, a);
		}

		[OpenGLSupport(2.0)]
		public static void CompileShader(uint shader)
		{
			Functions.CompileShader(shader);
		}

		[OpenGLSupport(3.1)]
		public static void CopyBufferSubData(uint readTarget, uint writeTarget, int readOffset, int writeOffset, int size)
		{
			Functions.CopyBufferSubData(readTarget, writeTarget, readOffset, writeOffset, size);
		}

		[OpenGLSupport(4.5)]
		public static void CopyNamedBufferSubData(uint readBuffer, uint writeBuffer, int readOffset, int writeOffset, int size)
		{
			Functions.CopyNamedBufferSubData(readBuffer, writeBuffer, readOffset, writeOffset, size);
		}

		[OpenGLSupport(4.5)]
		public static void CreateBuffers(int n, uint* buffers)
		{
			Functions.CreateBuffers(n, buffers);
		}

		[OpenGLSupport(2.0)]
		public static uint CreateProgram()
		{
			return Functions.CreateProgram();
		}

		[OpenGLSupport(4.5)]
		public static void CreateProgramPipelines(int n, uint* pipelines)
		{
			Functions.CreateProgramPipelines(n, pipelines);
		}

		[OpenGLSupport(4.5)]
		public static void CreateQueries(uint target, int n, uint* ids)
		{
			Functions.CreateQueries(target, n, ids);
		}

		[OpenGLSupport(4.5)]
		public static void CreateSamplers(int n, uint* samplers)
		{
			Functions.CreateSamplers(n, samplers);
		}

		[OpenGLSupport(2.0)]
		public static uint CreateShader(uint type)
		{
			return Functions.CreateShader(type);
		}

		[OpenGLSupport(4.1)]
		public static uint CreateShaderProgramv(uint type, int count, string[] strings)
		{
			return Functions.CreateShaderProgramv(type, count, strings);
		}

		[OpenGLSupport(4.5)]
		public static void CreateTransformFeedbacks(int n, uint* ids)
		{
			Functions.CreateTransformFeedbacks(n, ids);
		}

		[OpenGLSupport(4.5)]
		public static void CreateVertexArrays(int n, uint* arrays)
		{
			Functions.CreateVertexArrays(n, arrays);
		}

		[OpenGLSupport(1.0)]
		public static void CullFace(uint mode)
		{
			Functions.CullFace(mode);
		}

		[OpenGLSupport(4.3)]
		public static void DebugMessageCallback(DebugProc callback, void* userParam)
		{
			Functions.DebugMessageCallback(callback, userParam);
		}

		[OpenGLSupport(4.3)]
		public static void DebugMessageControl(uint source, uint type, uint severity, int count, uint* ids, bool enabled)
		{
			Functions.DebugMessageControl(source, type, severity, count, ids, enabled);
		}

		[OpenGLSupport(4.3)]
		public static void DebugMessageInsert(uint source, uint type, uint id, uint severity, int length, string buf)
		{
			Functions.DebugMessageInsert(source, type, id, severity, length, buf);
		}

		[OpenGLSupport(1.5)]
		public static void DeleteBuffers(int n, uint* buffers)
		{
			Functions.DeleteBuffers(n, buffers);
		}

		[OpenGLSupport(2.0)]
		public static void DeleteProgram(uint program)
		{
			Functions.DeleteProgram(program);
		}

		[OpenGLSupport(4.1)]
		public static void DeleteProgramPipelines(int n, uint* pipelines)
		{
			Functions.DeleteProgramPipelines(n, pipelines);
		}

		[OpenGLSupport(1.5)]
		public static void DeleteQueries(int n, uint* ids)
		{
			Functions.DeleteQueries(n, ids);
		}

		[OpenGLSupport(3.3)]
		public static void DeleteSamplers(int count, uint* samplers)
		{
			Functions.DeleteSamplers(count, samplers);
		}

		[OpenGLSupport(2.0)]
		public static void DeleteShader(uint shader)
		{
			Functions.DeleteShader(shader);
		}

		[OpenGLSupport(3.2)]
		public static void DeleteSync(IntPtr sync)
		{
			Functions.DeleteSync(sync);
		}

		[OpenGLSupport(4.0)]
		public static void DeleteTransformFeedbacks(int n, uint* ids)
		{
			Functions.DeleteTransformFeedbacks(n, ids);
		}

		[OpenGLSupport(3.0)]
		public static void DeleteVertexArrays(int n, uint* arrays)
		{
			Functions.DeleteVertexArrays(n, arrays);
		}

		[OpenGLSupport(1.0)]
		public static void DepthFunc(uint func)
		{
			Functions.DepthFunc(func);
		}

		[OpenGLSupport(1.0)]
		public static void DepthMask(bool flag)
		{
			Functions.DepthMask(flag);
		}

		[OpenGLSupport(1.0)]
		public static void DepthRange(double n, double f)
		{
			Functions.DepthRange(n, f);
		}

		[OpenGLSupport(4.1)]
		public static void DepthRangeArrayv(uint first, int count, double* v)
		{
			Functions.DepthRangeArrayv(first, count, v);
		}

		[OpenGLSupport(4.1)]
		public static void DepthRangef(float n, float f)
		{
			Functions.DepthRangef(n, f);
		}

		[OpenGLSupport(4.1)]
		public static void DepthRangeIndexed(uint index, double n, double f)
		{
			Functions.DepthRangeIndexed(index, n, f);
		}

		[OpenGLSupport(2.0)]
		public static void DetachShader(uint program, uint shader)
		{
			Functions.DetachShader(program, shader);
		}

		[OpenGLSupport(1.0)]
		public static void Disable(uint cap)
		{
			Functions.Disable(cap);
		}

		[OpenGLSupport(3.0)]
		public static void Disablei(uint target, uint index)
		{
			Functions.Disablei(target, index);
		}

		[OpenGLSupport(4.5)]
		public static void DisableVertexArrayAttrib(uint vaobj, uint index)
		{
			Functions.DisableVertexArrayAttrib(vaobj, index);
		}

		[OpenGLSupport(2.0)]
		public static void DisableVertexAttribArray(uint index)
		{
			Functions.DisableVertexAttribArray(index);
		}

		[OpenGLSupport(4.3)]
		public static void DispatchCompute(uint num_groups_x, uint num_groups_y, uint num_groups_z)
		{
			Functions.DispatchCompute(num_groups_x, num_groups_y, num_groups_z);
		}

		[OpenGLSupport(4.3)]
		public static void DispatchComputeIndirect(int indirect)
		{
			Functions.DispatchComputeIndirect(indirect);
		}

		[OpenGLSupport(1.1)]
		public static void DrawArrays(uint mode, int first, int count)
		{
			Functions.DrawArrays(mode, first, count);
		}

		[OpenGLSupport(4.0)]
		public static void DrawArraysIndirect(uint mode, void* indirect)
		{
			Functions.DrawArraysIndirect(mode, indirect);
		}

		[OpenGLSupport(3.1)]
		public static void DrawArraysInstanced(uint mode, int first, int count, int instancecount)
		{
			Functions.DrawArraysInstanced(mode, first, count, instancecount);
		}

		[OpenGLSupport(4.2)]
		public static void DrawArraysInstancedBaseInstance(uint mode, int first, int count, int instancecount, uint baseinstance)
		{
			Functions.DrawArraysInstancedBaseInstance(mode, first, count, instancecount, baseinstance);
		}

		[OpenGLSupport(1.1)]
		public static void DrawElements(uint mode, int count, uint type, void* indices)
		{
			Functions.DrawElements(mode, count, type, indices);
		}

		[OpenGLSupport(3.2)]
		public static void DrawElementsBaseVertex(uint mode, int count, uint type, void* indices, int basevertex)
		{
			Functions.DrawElementsBaseVertex(mode, count, type, indices, basevertex);
		}

		[OpenGLSupport(4.0)]
		public static void DrawElementsIndirect(uint mode, uint type, void* indirect)
		{
			Functions.DrawElementsIndirect(mode, type, indirect);
		}

		[OpenGLSupport(3.1)]
		public static void DrawElementsInstanced(uint mode, int count, uint type, void* indices, int instancecount)
		{
			Functions.DrawElementsInstanced(mode, count, type, indices, instancecount);
		}

		[OpenGLSupport(4.2)]
		public static void DrawElementsInstancedBaseInstance(uint mode, int count, uint type, void* indices, int instancecount, uint baseinstance)
		{
			Functions.DrawElementsInstancedBaseInstance(mode, count, type, indices, instancecount, baseinstance);
		}

		[OpenGLSupport(3.2)]
		public static void DrawElementsInstancedBaseVertex(uint mode, int count, uint type, void* indices, int instancecount, int basevertex)
		{
			Functions.DrawElementsInstancedBaseVertex(mode, count, type, indices, instancecount, basevertex);
		}

		[OpenGLSupport(4.2)]
		public static void DrawElementsInstancedBaseVertexBaseInstance(uint mode, int count, uint type, void* indices, int instancecount, int basevertex, uint baseinstance)
		{
			Functions.DrawElementsInstancedBaseVertexBaseInstance(mode, count, type, indices, instancecount, basevertex, baseinstance);
		}

		[OpenGLSupport(1.2)]
		public static void DrawRangeElements(uint mode, uint start, uint end, int count, uint type, void* indices)
		{
			Functions.DrawRangeElements(mode, start, end, count, type, indices);
		}

		[OpenGLSupport(3.2)]
		public static void DrawRangeElementsBaseVertex(uint mode, uint start, uint end, int count, uint type, void* indices, int basevertex)
		{
			Functions.DrawRangeElementsBaseVertex(mode, start, end, count, type, indices, basevertex);
		}

		[OpenGLSupport(4.0)]
		public static void DrawTransformFeedback(uint mode, uint id)
		{
			Functions.DrawTransformFeedback(mode, id);
		}

		[OpenGLSupport(4.2)]
		public static void DrawTransformFeedbackInstanced(uint mode, uint id, int instancecount)
		{
			Functions.DrawTransformFeedbackInstanced(mode, id, instancecount);
		}

		[OpenGLSupport(4.0)]
		public static void DrawTransformFeedbackStream(uint mode, uint id, uint stream)
		{
			Functions.DrawTransformFeedbackStream(mode, id, stream);
		}

		[OpenGLSupport(4.2)]
		public static void DrawTransformFeedbackStreamInstanced(uint mode, uint id, uint stream, int instancecount)
		{
			Functions.DrawTransformFeedbackStreamInstanced(mode, id, stream, instancecount);
		}

		[OpenGLSupport(1.0)]
		public static void Enable(uint cap)
		{
			Functions.Enable(cap);
		}

		[OpenGLSupport(3.0)]
		public static void Enablei(uint target, uint index)
		{
			Functions.Enablei(target, index);
		}

		[OpenGLSupport(4.5)]
		public static void EnableVertexArrayAttrib(uint vaobj, uint index)
		{
			Functions.EnableVertexArrayAttrib(vaobj, index);
		}

		[OpenGLSupport(2.0)]
		public static void EnableVertexAttribArray(uint index)
		{
			Functions.EnableVertexAttribArray(index);
		}

		[OpenGLSupport(3.0)]
		public static void EndConditionalRender()
		{
			Functions.EndConditionalRender();
		}

		[OpenGLSupport(1.5)]
		public static void EndQuery(uint target)
		{
			Functions.EndQuery(target);
		}

		[OpenGLSupport(4.0)]
		public static void EndQueryIndexed(uint target, uint index)
		{
			Functions.EndQueryIndexed(target, index);
		}

		[OpenGLSupport(3.0)]
		public static void EndTransformFeedback()
		{
			Functions.EndTransformFeedback();
		}

		[OpenGLSupport(3.2)]
		public static IntPtr FenceSync(uint condition, uint flags)
		{
			return Functions.FenceSync(condition, flags);
		}

		[OpenGLSupport(1.0)]
		public static void Finish()
		{
			Functions.Finish();
		}

		[OpenGLSupport(1.0)]
		public static void Flush()
		{
			Functions.Flush();
		}

		[OpenGLSupport(3.0)]
		public static void FlushMappedBufferRange(uint target, int offset, int length)
		{
			Functions.FlushMappedBufferRange(target, offset, length);
		}

		[OpenGLSupport(4.5)]
		public static void FlushMappedNamedBufferRange(uint buffer, int offset, int length)
		{
			Functions.FlushMappedNamedBufferRange(buffer, offset, length);
		}

		[OpenGLSupport(1.0)]
		public static void FrontFace(uint mode)
		{
			Functions.FrontFace(mode);
		}

		[OpenGLSupport(1.5)]
		public static void GenBuffers(int n, uint* buffers)
		{
			Functions.GenBuffers(n, buffers);
		}

		[OpenGLSupport(4.1)]
		public static void GenProgramPipelines(int n, uint* pipelines)
		{
			Functions.GenProgramPipelines(n, pipelines);
		}

		[OpenGLSupport(1.5)]
		public static void GenQueries(int n, uint* ids)
		{
			Functions.GenQueries(n, ids);
		}

		[OpenGLSupport(3.3)]
		public static void GenSamplers(int count, uint* samplers)
		{
			Functions.GenSamplers(count, samplers);
		}

		[OpenGLSupport(4.0)]
		public static void GenTransformFeedbacks(int n, uint* ids)
		{
			Functions.GenTransformFeedbacks(n, ids);
		}

		[OpenGLSupport(3.0)]
		public static void GenVertexArrays(int n, uint* arrays)
		{
			Functions.GenVertexArrays(n, arrays);
		}

		[OpenGLSupport(4.2)]
		public static void GetActiveAtomicCounterBufferiv(uint program, uint bufferIndex, uint pname, int* @params)
		{
			Functions.GetActiveAtomicCounterBufferiv(program, bufferIndex, pname, @params);
		}

		[OpenGLSupport(2.0)]
		public static void GetActiveAttrib(uint program, uint index, int bufSize, int* length, int* size, uint* type, StringBuilder name)
		{
			Functions.GetActiveAttrib(program, index, bufSize, length, size, type, name);
		}

		[OpenGLSupport(4.0)]
		public static void GetActiveSubroutineName(uint program, uint shadertype, uint index, int bufsize, int* length, StringBuilder name)
		{
			Functions.GetActiveSubroutineName(program, shadertype, index, bufsize, length, name);
		}

		[OpenGLSupport(4.0)]
		public static void GetActiveSubroutineUniformiv(uint program, uint shadertype, uint index, uint pname, int* values)
		{
			Functions.GetActiveSubroutineUniformiv(program, shadertype, index, pname, values);
		}

		[OpenGLSupport(4.0)]
		public static void GetActiveSubroutineUniformName(uint program, uint shadertype, uint index, int bufsize, int* length, StringBuilder name)
		{
			Functions.GetActiveSubroutineUniformName(program, shadertype, index, bufsize, length, name);
		}

		[OpenGLSupport(2.0)]
		public static void GetActiveUniform(uint program, uint index, int bufSize, int* length, int* size, uint* type, StringBuilder name)
		{
			Functions.GetActiveUniform(program, index, bufSize, length, size, type, name);
		}

		[OpenGLSupport(3.1)]
		public static void GetActiveUniformBlockiv(uint program, uint uniformBlockIndex, uint pname, int* @params)
		{
			Functions.GetActiveUniformBlockiv(program, uniformBlockIndex, pname, @params);
		}

		[OpenGLSupport(3.1)]
		public static void GetActiveUniformBlockName(uint program, uint uniformBlockIndex, int bufSize, int* length, StringBuilder uniformBlockName)
		{
			Functions.GetActiveUniformBlockName(program, uniformBlockIndex, bufSize, length, uniformBlockName);
		}

		[OpenGLSupport(3.1)]
		public static void GetActiveUniformName(uint program, uint uniformIndex, int bufSize, int* length, StringBuilder uniformName)
		{
			Functions.GetActiveUniformName(program, uniformIndex, bufSize, length, uniformName);
		}

		[OpenGLSupport(3.1)]
		public static void GetActiveUniformsiv(uint program, int uniformCount, uint* uniformIndices, uint pname, int* @params)
		{
			Functions.GetActiveUniformsiv(program, uniformCount, uniformIndices, pname, @params);
		}

		[OpenGLSupport(2.0)]
		public static void GetAttachedShaders(uint program, int maxCount, int* count, uint* shaders)
		{
			Functions.GetAttachedShaders(program, maxCount, count, shaders);
		}

		[OpenGLSupport(2.0)]
		public static int GetAttribLocation(uint program, string name)
		{
			return Functions.GetAttribLocation(program, name);
		}

		[OpenGLSupport(3.0)]
		public static void GetBooleani_v(uint target, uint index, bool* data)
		{
			Functions.GetBooleani_v(target, index, data);
		}

		[OpenGLSupport(1.0)]
		public static void GetBooleanv(uint pname, bool* data)
		{
			Functions.GetBooleanv(pname, data);
		}

		[OpenGLSupport(3.2)]
		public static void GetBufferParameteri64v(uint target, uint pname, long* @params)
		{
			Functions.GetBufferParameteri64v(target, pname, @params);
		}

		[OpenGLSupport(1.5)]
		public static void GetBufferParameteriv(uint target, uint pname, int* @params)
		{
			Functions.GetBufferParameteriv(target, pname, @params);
		}

		[OpenGLSupport(1.5)]
		public static void GetBufferPointerv(uint target, uint pname, void** @params)
		{
			Functions.GetBufferPointerv(target, pname, @params);
		}

		[OpenGLSupport(1.5)]
		public static void GetBufferSubData(uint target, int offset, int size, void* data)
		{
			Functions.GetBufferSubData(target, offset, size, data);
		}

		[OpenGLSupport(4.3)]
		public static uint GetDebugMessageLog(uint count, int bufSize, uint* sources, uint* types, uint* ids, uint* severities, int* lengths, StringBuilder messageLog)
		{
			return Functions.GetDebugMessageLog(count, bufSize, sources, types, ids, severities, lengths, messageLog);
		}

		[OpenGLSupport(4.1)]
		public static void GetDoublei_v(uint target, uint index, double* data)
		{
			Functions.GetDoublei_v(target, index, data);
		}

		[OpenGLSupport(1.0)]
		public static void GetDoublev(uint pname, double* data)
		{
			Functions.GetDoublev(pname, data);
		}

		[OpenGLSupport(1.0)]
		public static uint GetError()
		{
			return Functions.GetError();
		}

		[OpenGLSupport(4.1)]
		public static void GetFloati_v(uint target, uint index, float* data)
		{
			Functions.GetFloati_v(target, index, data);
		}

		[OpenGLSupport(1.0)]
		public static void GetFloatv(uint pname, float* data)
		{
			Functions.GetFloatv(pname, data);
		}

		[OpenGLSupport(3.3)]
		public static int GetFragDataIndex(uint program, string name)
		{
			return Functions.GetFragDataIndex(program, name);
		}

		[OpenGLSupport(3.0)]
		public static int GetFragDataLocation(uint program, string name)
		{
			return Functions.GetFragDataLocation(program, name);
		}

		[OpenGLSupport(4.5)]
		public static uint GetGraphicsResetStatus()
		{
			return Functions.GetGraphicsResetStatus();
		}

		[OpenGLSupport(3.2)]
		public static void GetInteger64i_v(uint target, uint index, long* data)
		{
			Functions.GetInteger64i_v(target, index, data);
		}

		[OpenGLSupport(3.2)]
		public static void GetInteger64v(uint pname, long* data)
		{
			Functions.GetInteger64v(pname, data);
		}

		[OpenGLSupport(3.0)]
		public static void GetIntegeri_v(uint target, uint index, int* data)
		{
			Functions.GetIntegeri_v(target, index, data);
		}

		[OpenGLSupport(1.0)]
		public static void GetIntegerv(uint pname, int* data)
		{
			Functions.GetIntegerv(pname, data);
		}

		[OpenGLSupport(3.2)]
		public static void GetMultisamplefv(uint pname, uint index, float* val)
		{
			Functions.GetMultisamplefv(pname, index, val);
		}

		[OpenGLSupport(4.5)]
		public static void GetNamedBufferParameteri64v(uint buffer, uint pname, long* @params)
		{
			Functions.GetNamedBufferParameteri64v(buffer, pname, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetNamedBufferParameteriv(uint buffer, uint pname, int* @params)
		{
			Functions.GetNamedBufferParameteriv(buffer, pname, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetNamedBufferPointerv(uint buffer, uint pname, void** @params)
		{
			Functions.GetNamedBufferPointerv(buffer, pname, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetNamedBufferSubData(uint buffer, int offset, int size, void* data)
		{
			Functions.GetNamedBufferSubData(buffer, offset, size, data);
		}

		[OpenGLSupport(4.5)]
		public static void GetnUniformdv(uint program, int location, int bufSize, double* @params)
		{
			Functions.GetnUniformdv(program, location, bufSize, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetnUniformfv(uint program, int location, int bufSize, float* @params)
		{
			Functions.GetnUniformfv(program, location, bufSize, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetnUniformiv(uint program, int location, int bufSize, int* @params)
		{
			Functions.GetnUniformiv(program, location, bufSize, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetnUniformuiv(uint program, int location, int bufSize, uint* @params)
		{
			Functions.GetnUniformuiv(program, location, bufSize, @params);
		}

		[OpenGLSupport(4.3)]
		public static void GetObjectLabel(uint identifier, uint name, int bufSize, int* length, StringBuilder label)
		{
			Functions.GetObjectLabel(identifier, name, bufSize, length, label);
		}

		[OpenGLSupport(4.3)]
		public static void GetObjectPtrLabel(void* ptr, int bufSize, int* length, StringBuilder label)
		{
			Functions.GetObjectPtrLabel(ptr, bufSize, length, label);
		}

		[OpenGLSupport(1.1)]
		public static void GetPointerv(uint pname, void** @params)
		{
			Functions.GetPointerv(pname, @params);
		}

		[OpenGLSupport(4.1)]
		public static void GetProgramBinary(uint program, int bufSize, int* length, uint* binaryFormat, void* binary)
		{
			Functions.GetProgramBinary(program, bufSize, length, binaryFormat, binary);
		}

		[OpenGLSupport(2.0)]
		public static void GetProgramInfoLog(uint program, int bufSize, int* length, StringBuilder infoLog)
		{
			Functions.GetProgramInfoLog(program, bufSize, length, infoLog);
		}

		[OpenGLSupport(4.3)]
		public static void GetProgramInterfaceiv(uint program, uint programInterface, uint pname, int* @params)
		{
			Functions.GetProgramInterfaceiv(program, programInterface, pname, @params);
		}

		[OpenGLSupport(2.0)]
		public static void GetProgramiv(uint program, uint pname, int* @params)
		{
			Functions.GetProgramiv(program, pname, @params);
		}

		[OpenGLSupport(4.1)]
		public static void GetProgramPipelineInfoLog(uint pipeline, int bufSize, int* length, StringBuilder infoLog)
		{
			Functions.GetProgramPipelineInfoLog(pipeline, bufSize, length, infoLog);
		}

		[OpenGLSupport(4.1)]
		public static void GetProgramPipelineiv(uint pipeline, uint pname, int* @params)
		{
			Functions.GetProgramPipelineiv(pipeline, pname, @params);
		}

		[OpenGLSupport(4.3)]
		public static uint GetProgramResourceIndex(uint program, uint programInterface, string name)
		{
			return Functions.GetProgramResourceIndex(program, programInterface, name);
		}

		[OpenGLSupport(4.3)]
		public static void GetProgramResourceiv(uint program, uint programInterface, uint index, int propCount, uint* props, int bufSize, int* length, int* @params)
		{
			Functions.GetProgramResourceiv(program, programInterface, index, propCount, props, bufSize, length, @params);
		}

		[OpenGLSupport(4.3)]
		public static int GetProgramResourceLocation(uint program, uint programInterface, string name)
		{
			return Functions.GetProgramResourceLocation(program, programInterface, name);
		}

		[OpenGLSupport(4.3)]
		public static int GetProgramResourceLocationIndex(uint program, uint programInterface, string name)
		{
			return Functions.GetProgramResourceLocationIndex(program, programInterface, name);
		}

		[OpenGLSupport(4.3)]
		public static void GetProgramResourceName(uint program, uint programInterface, uint index, int bufSize, int* length, StringBuilder name)
		{
			Functions.GetProgramResourceName(program, programInterface, index, bufSize, length, name);
		}

		[OpenGLSupport(4.0)]
		public static void GetProgramStageiv(uint program, uint shadertype, uint pname, int* values)
		{
			Functions.GetProgramStageiv(program, shadertype, pname, values);
		}

		[OpenGLSupport(4.5)]
		public static void GetQueryBufferObjecti64v(uint id, uint buffer, uint pname, int offset)
		{
			Functions.GetQueryBufferObjecti64v(id, buffer, pname, offset);
		}

		[OpenGLSupport(4.5)]
		public static void GetQueryBufferObjectiv(uint id, uint buffer, uint pname, int offset)
		{
			Functions.GetQueryBufferObjectiv(id, buffer, pname, offset);
		}

		[OpenGLSupport(4.5)]
		public static void GetQueryBufferObjectui64v(uint id, uint buffer, uint pname, int offset)
		{
			Functions.GetQueryBufferObjectui64v(id, buffer, pname, offset);
		}

		[OpenGLSupport(4.5)]
		public static void GetQueryBufferObjectuiv(uint id, uint buffer, uint pname, int offset)
		{
			Functions.GetQueryBufferObjectuiv(id, buffer, pname, offset);
		}

		[OpenGLSupport(4.0)]
		public static void GetQueryIndexediv(uint target, uint index, uint pname, int* @params)
		{
			Functions.GetQueryIndexediv(target, index, pname, @params);
		}

		[OpenGLSupport(1.5)]
		public static void GetQueryiv(uint target, uint pname, int* @params)
		{
			Functions.GetQueryiv(target, pname, @params);
		}

		[OpenGLSupport(3.3)]
		public static void GetQueryObjecti64v(uint id, uint pname, long* @params)
		{
			Functions.GetQueryObjecti64v(id, pname, @params);
		}

		[OpenGLSupport(1.5)]
		public static void GetQueryObjectiv(uint id, uint pname, int* @params)
		{
			Functions.GetQueryObjectiv(id, pname, @params);
		}

		[OpenGLSupport(3.3)]
		public static void GetQueryObjectui64v(uint id, uint pname, ulong* @params)
		{
			Functions.GetQueryObjectui64v(id, pname, @params);
		}
		
		[OpenGLSupport(1.5)]
		public static void GetQueryObjectuiv(uint id, uint pname, uint* @params)
		{
			Functions.GetQueryObjectuiv(id, pname, @params);
		}

		[OpenGLSupport(3.3)]
		public static void GetSamplerParameterfv(uint sampler, uint pname, float* @params)
		{
			Functions.GetSamplerParameterfv(sampler, pname, @params);
		}

		[OpenGLSupport(3.3)]
		public static void GetSamplerParameterIiv(uint sampler, uint pname, int* @params)
		{
			Functions.GetSamplerParameterIiv(sampler, pname, @params);
		}

		[OpenGLSupport(3.3)]
		public static void GetSamplerParameterIuiv(uint sampler, uint pname, uint* @params)
		{
			Functions.GetSamplerParameterIuiv(sampler, pname, @params);
		}

		[OpenGLSupport(3.3)]
		public static void GetSamplerParameteriv(uint sampler, uint pname, int* @params)
		{
			Functions.GetSamplerParameteriv(sampler, pname, @params);
		}

		[OpenGLSupport(2.0)]
		public static void GetShaderInfoLog(uint shader, int bufSize, int* length, StringBuilder infoLog)
		{
			Functions.GetShaderInfoLog(shader, bufSize, length, infoLog);
		}

		[OpenGLSupport(2.0)]
		public static void GetShaderiv(uint shader, uint pname, int* @params)
		{
			Functions.GetShaderiv(shader, pname, @params);
		}

		[OpenGLSupport(4.1)]
		public static void GetShaderPrecisionFormat(uint shadertype, uint precisiontype, int* range, int* precision)
		{
			Functions.GetShaderPrecisionFormat(shadertype, precisiontype, range, precision);
		}

		[OpenGLSupport(2.0)]
		public static void GetShaderSource(uint shader, int bufSize, int* length, StringBuilder source)
		{
			Functions.GetShaderSource(shader, bufSize, length, source);
		}

		[OpenGLSupport(1.0)]
		public static IntPtr GetString(uint name)
		{
			return Functions.GetString(name);
		}

		[OpenGLSupport(3.0)]
		public static IntPtr GetStringi(uint name, uint index)
		{
			return Functions.GetStringi(name, index);
		}

		[OpenGLSupport(4.0)]
		public static uint GetSubroutineIndex(uint program, uint shadertype, string name)
		{
			return Functions.GetSubroutineIndex(program, shadertype, name);
		}

		[OpenGLSupport(4.0)]
		public static int GetSubroutineUniformLocation(uint program, uint shadertype, string name)
		{
			return Functions.GetSubroutineUniformLocation(program, shadertype, name);
		}

		[OpenGLSupport(3.2)]
		public static void GetSynciv(IntPtr sync, uint pname, int bufSize, int* length, int* values)
		{
			Functions.GetSynciv(sync, pname, bufSize, length, values);
		}

		[OpenGLSupport(4.5)]
		public static void GetTransformFeedbacki_v(uint xfb, uint pname, uint index, int* param)
		{
			Functions.GetTransformFeedbacki_v(xfb, pname, index, param);
		}

		[OpenGLSupport(4.5)]
		public static void GetTransformFeedbacki64_v(uint xfb, uint pname, uint index, long* param)
		{
			Functions.GetTransformFeedbacki64_v(xfb, pname, index, param);
		}

		[OpenGLSupport(4.5)]
		public static void GetTransformFeedbackiv(uint xfb, uint pname, int* param)
		{
			Functions.GetTransformFeedbackiv(xfb, pname, param);
		}

		[OpenGLSupport(3.0)]
		public static void GetTransformFeedbackVarying(uint program, uint index, int bufSize, int* length, int* size, uint* type, StringBuilder name)
		{
			Functions.GetTransformFeedbackVarying(program, index, bufSize, length, size, type, name);
		}

		[OpenGLSupport(3.1)]
		public static uint GetUniformBlockIndex(uint program, string uniformBlockName)
		{
			return Functions.GetUniformBlockIndex(program, uniformBlockName);
		}

		[OpenGLSupport(4.0)]
		public static void GetUniformdv(uint program, int location, double* @params)
		{
			Functions.GetUniformdv(program, location, @params);
		}

		[OpenGLSupport(2.0)]
		public static void GetUniformfv(uint program, int location, float* @params)
		{
			Functions.GetUniformfv(program, location, @params);
		}

		[OpenGLSupport(3.1)]
		public static void GetUniformIndices(uint program, int uniformCount, string[] uniformNames, uint* uniformIndices)
		{
			Functions.GetUniformIndices(program, uniformCount, uniformNames, uniformIndices);
		}

		[OpenGLSupport(2.0)]
		public static void GetUniformiv(uint program, int location, int* @params)
		{
			Functions.GetUniformiv(program, location, @params);
		}

		[OpenGLSupport(2.0)]
		public static int GetUniformLocation(uint program, string name)
		{
			return Functions.GetUniformLocation(program, name);
		}

		[OpenGLSupport(4.0)]
		public static void GetUniformSubroutineuiv(uint shadertype, int location, uint* @params)
		{
			Functions.GetUniformSubroutineuiv(shadertype, location, @params);
		}

		[OpenGLSupport(3.0)]
		public static void GetUniformuiv(uint program, int location, uint* @params)
		{
			Functions.GetUniformuiv(program, location, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetVertexArrayIndexed64iv(uint vaobj, uint index, uint pname, long* param)
		{
			Functions.GetVertexArrayIndexed64iv(vaobj, index, pname, param);
		}

		[OpenGLSupport(4.5)]
		public static void GetVertexArrayIndexediv(uint vaobj, uint index, uint pname, int* param)
		{
			Functions.GetVertexArrayIndexediv(vaobj, index, pname, param);
		}

		[OpenGLSupport(4.5)]
		public static void GetVertexArrayiv(uint vaobj, uint pname, int* param)
		{
			Functions.GetVertexArrayiv(vaobj, pname, param);
		}

		[OpenGLSupport(2.0)]
		public static void GetVertexAttribdv(uint index, uint pname, double* @params)
		{
			Functions.GetVertexAttribdv(index, pname, @params);
		}

		[OpenGLSupport(2.0)]
		public static void GetVertexAttribfv(uint index, uint pname, float* @params)
		{
			Functions.GetVertexAttribfv(index, pname, @params);
		}

		[OpenGLSupport(3.0)]
		public static void GetVertexAttribIiv(uint index, uint pname, int* @params)
		{
			Functions.GetVertexAttribIiv(index, pname, @params);
		}

		[OpenGLSupport(3.0)]
		public static void GetVertexAttribIuiv(uint index, uint pname, uint* @params)
		{
			Functions.GetVertexAttribIuiv(index, pname, @params);
		}

		[OpenGLSupport(2.0)]
		public static void GetVertexAttribiv(uint index, uint pname, int* @params)
		{
			Functions.GetVertexAttribiv(index, pname, @params);
		}

		[OpenGLSupport(4.1)]
		public static void GetVertexAttribLdv(uint index, uint pname, double* @params)
		{
			Functions.GetVertexAttribLdv(index, pname, @params);
		}

		[OpenGLSupport(2.0)]
		public static void GetVertexAttribPointerv(uint index, uint pname, void** pointer)
		{
			Functions.GetVertexAttribPointerv(index, pname, pointer);
		}

		[OpenGLSupport(1.0)]
		public static void Hint(uint target, uint mode)
		{
			Functions.Hint(target, mode);
		}

		[OpenGLSupport(4.3)]
		public static void InvalidateBufferData(uint buffer)
		{
			Functions.InvalidateBufferData(buffer);
		}

		[OpenGLSupport(4.3)]
		public static void InvalidateBufferSubData(uint buffer, int offset, int length)
		{
			Functions.InvalidateBufferSubData(buffer, offset, length);
		}

		[OpenGLSupport(1.0)]
		public static bool IsEnabled(uint cap)
		{
			return Functions.IsEnabled(cap);
		}

		[OpenGLSupport(3.0)]
		public static bool IsEnabledi(uint target, uint index)
		{
			return Functions.IsEnabledi(target, index);
		}

		[OpenGLSupport(1.0)]
		public static void LineWidth(float width)
		{
			Functions.LineWidth(width);
		}

		[OpenGLSupport(2.0)]
		public static void LinkProgram(uint program)
		{
			Functions.LinkProgram(program);
		}

		[OpenGLSupport(1.0)]
		public static void LogicOp(uint opcode)
		{
			Functions.LogicOp(opcode);
		}

		[OpenGLSupport(1.5)]
		public static IntPtr MapBuffer(uint target, uint access)
		{
			return Functions.MapBuffer(target, access);
		}

		[OpenGLSupport(3.0)]
		public static IntPtr MapBufferRange(uint target, int offset, int length, uint access)
		{
			return Functions.MapBufferRange(target, offset, length, access);
		}

		[OpenGLSupport(4.5)]
		public static IntPtr MapNamedBuffer(uint buffer, uint access)
		{
			return Functions.MapNamedBuffer(buffer, access);
		}

		[OpenGLSupport(4.5)]
		public static IntPtr MapNamedBufferRange(uint buffer, int offset, int length, uint access)
		{
			return Functions.MapNamedBufferRange(buffer, offset, length, access);
		}

		[OpenGLSupport(4.2)]
		public static void MemoryBarrier(uint barriers)
		{
			Functions.MemoryBarrier(barriers);
		}

		[OpenGLSupport(4.5)]
		public static void MemoryBarrierByRegion(uint barriers)
		{
			Functions.MemoryBarrierByRegion(barriers);
		}

		[OpenGLSupport(4.0)]
		public static void MinSampleShading(float value)
		{
			Functions.MinSampleShading(value);
		}

		[OpenGLSupport(1.4)]
		public static void MultiDrawArrays(uint mode, int* first, int* count, int drawcount)
		{
			Functions.MultiDrawArrays(mode, first, count, drawcount);
		}

		[OpenGLSupport(4.3)]
		public static void MultiDrawArraysIndirect(uint mode, void* indirect, int drawcount, int stride)
		{
			Functions.MultiDrawArraysIndirect(mode, indirect, drawcount, stride);
		}

		//[OpenGLSupport(4.6)]
		//public static void MultiDrawArraysIndirectCount(uint mode, void* indirect, int drawcount, int maxdrawcount, int stride)
		//{
		//	Functions.MultiDrawArraysIndirectCount(mode, indirect, drawcount, maxdrawcount, stride);
		//}

		[OpenGLSupport(1.4)]
		public static void MultiDrawElements(uint mode, int* count, uint type, void** indices, int drawcount)
		{
			Functions.MultiDrawElements(mode, count, type, indices, drawcount);
		}

		[OpenGLSupport(3.2)]
		public static void MultiDrawElementsBaseVertex(uint mode, int* count, uint type, void** indices, int drawcount, int* basevertex)
		{
			Functions.MultiDrawElementsBaseVertex(mode, count, type, indices, drawcount, basevertex);
		}

		[OpenGLSupport(4.3)]
		public static void MultiDrawElementsIndirect(uint mode, uint type, void* indirect, int drawcount, int stride)
		{
			Functions.MultiDrawElementsIndirect(mode, type, indirect, drawcount, stride);
		}

		//[OpenGLSupport(4.6)]
		//public static void MultiDrawElementsIndirectCount(uint mode, uint type, void* indirect, int drawcount, int maxdrawcount, int stride)
		//{
		//	Functions.MultiDrawElementsIndirectCount(mode, type, indirect, drawcount, maxdrawcount, stride);
		//}

		[OpenGLSupport(4.5)]
		public static void NamedBufferData(uint buffer, int size, void* data, uint usage)
		{
			Functions.NamedBufferData(buffer, size, data, usage);
		}

		[OpenGLSupport(4.5)]
		public static void NamedBufferStorage(uint buffer, int size, void* data, uint flags)
		{
			Functions.NamedBufferStorage(buffer, size, data, flags);
		}

		[OpenGLSupport(4.5)]
		public static void NamedBufferSubData(uint buffer, int offset, int size, void* data)
		{
			Functions.NamedBufferSubData(buffer, offset, size, data);
		}

		[OpenGLSupport(4.3)]
		public static void ObjectLabel(uint identifier, uint name, int length, string label)
		{
			Functions.ObjectLabel(identifier, name, length, label);
		}
		[OpenGLSupport(4.3)]
		public static void ObjectPtrLabel(void* ptr, int length, string label)
		{
			Functions.ObjectPtrLabel(ptr, length, label);
		}

		[OpenGLSupport(4.0)]
		public static void PatchParameterfv(uint pname, float* values)
		{
			Functions.PatchParameterfv(pname, values);
		}

		[OpenGLSupport(4.0)]
		public static void PatchParameteri(uint pname, int value)
		{
			Functions.PatchParameteri(pname, value);
		}

		[OpenGLSupport(4.0)]
		public static void PauseTransformFeedback()
		{
			Functions.PauseTransformFeedback();
		}

		[OpenGLSupport(1.0)]
		public static void PixelStoref(uint pname, float param)
		{
			Functions.PixelStoref(pname, param);
		}

		[OpenGLSupport(1.0)]
		public static void PixelStorei(uint pname, int param)
		{
			Functions.PixelStorei(pname, param);
		}

		[OpenGLSupport(1.4)]
		public static void PointParameterf(uint pname, float param)
		{
			Functions.PointParameterf(pname, param);
		}

		[OpenGLSupport(1.4)]
		public static void PointParameterfv(uint pname, float* @params)
		{
			Functions.PointParameterfv(pname, @params);
		}

		[OpenGLSupport(1.4)]
		public static void PointParameteri(uint pname, int param)
		{
			Functions.PointParameteri(pname, param);
		}

		[OpenGLSupport(1.4)]
		public static void PointParameteriv(uint pname, int* @params)
		{
			Functions.PointParameteriv(pname, @params);
		}

		[OpenGLSupport(1.0)]
		public static void PointSize(float size)
		{
			Functions.PointSize(size);
		}

		[OpenGLSupport(1.0)]
		public static void PolygonMode(uint face, uint mode)
		{
			Functions.PolygonMode(face, mode);
		}

		[OpenGLSupport(1.1)]
		public static void PolygonOffset(float factor, float units)
		{
			Functions.PolygonOffset(factor, units);
		}

		//[OpenGLSupport(4.6)]
		//public static void PolygonOffsetClamp(float factor, float units, float clamp)
		//{
		//	Functions.PolygonOffsetClamp(factor, units, clamp);
		//}

		[OpenGLSupport(4.3)]
		public static void PopDebugGroup()
		{
			Functions.PopDebugGroup();
		}

		[OpenGLSupport(3.1)]
		public static void PrimitiveRestartIndex(uint index)
		{
			Functions.PrimitiveRestartIndex(index);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramBinary(uint program, uint binaryFormat, void* binary, int length)
		{
			Functions.ProgramBinary(program, binaryFormat, binary, length);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramParameteri(uint program, uint pname, int value)
		{
			Functions.ProgramParameteri(program, pname, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform1d(uint program, int location, double v0)
		{
			Functions.ProgramUniform1d(program, location, v0);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform1dv(uint program, int location, int count, double* value)
		{
			Functions.ProgramUniform1dv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform1f(uint program, int location, float v0)
		{
			Functions.ProgramUniform1f(program, location, v0);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform1fv(uint program, int location, int count, float* value)
		{
			Functions.ProgramUniform1fv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform1i(uint program, int location, int v0)
		{
			Functions.ProgramUniform1i(program, location, v0);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform1iv(uint program, int location, int count, int* value)
		{
			Functions.ProgramUniform1iv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform1ui(uint program, int location, uint v0)
		{
			Functions.ProgramUniform1ui(program, location, v0);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform1uiv(uint program, int location, int count, uint* value)
		{
			Functions.ProgramUniform1uiv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform2d(uint program, int location, double v0, double v1)
		{
			Functions.ProgramUniform2d(program, location, v0, v1);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform2dv(uint program, int location, int count, double* value)
		{
			Functions.ProgramUniform2dv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform2f(uint program, int location, float v0, float v1)
		{
			Functions.ProgramUniform2f(program, location, v0, v1);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform2fv(uint program, int location, int count, float* value)
		{
			Functions.ProgramUniform2fv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform2i(uint program, int location, int v0, int v1)
		{
			Functions.ProgramUniform2i(program, location, v0, v1);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform2iv(uint program, int location, int count, int* value)
		{
			Functions.ProgramUniform2iv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform2ui(uint program, int location, uint v0, uint v1)
		{
			Functions.ProgramUniform2ui(program, location, v0, v1);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform2uiv(uint program, int location, int count, uint* value)
		{
			Functions.ProgramUniform2uiv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform3d(uint program, int location, double v0, double v1, double v2)
		{
			Functions.ProgramUniform3d(program, location, v0, v1, v2);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform3dv(uint program, int location, int count, double* value)
		{
			Functions.ProgramUniform3dv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform3f(uint program, int location, float v0, float v1, float v2)
		{
			Functions.ProgramUniform3f(program, location, v0, v1, v2);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform3fv(uint program, int location, int count, float* value)
		{
			Functions.ProgramUniform3fv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform3i(uint program, int location, int v0, int v1, int v2)
		{
			Functions.ProgramUniform3i(program, location, v0, v1, v2);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform3iv(uint program, int location, int count, int* value)
		{
			Functions.ProgramUniform3iv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform3ui(uint program, int location, uint v0, uint v1, uint v2)
		{
			Functions.ProgramUniform3ui(program, location, v0, v1, v2);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform3uiv(uint program, int location, int count, uint* value)
		{
			Functions.ProgramUniform3uiv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform4d(uint program, int location, double v0, double v1, double v2, double v3)
		{
			Functions.ProgramUniform4d(program, location, v0, v1, v2, v3);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform4dv(uint program, int location, int count, double* value)
		{
			Functions.ProgramUniform4dv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform4f(uint program, int location, float v0, float v1, float v2, float v3)
		{
			Functions.ProgramUniform4f(program, location, v0, v1, v2, v3);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform4fv(uint program, int location, int count, float* value)
		{
			Functions.ProgramUniform4fv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform4i(uint program, int location, int v0, int v1, int v2, int v3)
		{
			Functions.ProgramUniform4i(program, location, v0, v1, v2, v3);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform4iv(uint program, int location, int count, int* value)
		{
			Functions.ProgramUniform4iv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform4ui(uint program, int location, uint v0, uint v1, uint v2, uint v3)
		{
			Functions.ProgramUniform4ui(program, location, v0, v1, v2, v3);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniform4uiv(uint program, int location, int count, uint* value)
		{
			Functions.ProgramUniform4uiv(program, location, count, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix2dv(uint program, int location, int count, bool transpose, double* value)
		{
			Functions.ProgramUniformMatrix2dv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix2fv(uint program, int location, int count, bool transpose, float* value)
		{
			Functions.ProgramUniformMatrix2fv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix2x3dv(uint program, int location, int count, bool transpose, double* value)
		{
			Functions.ProgramUniformMatrix2x3dv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix2x3fv(uint program, int location, int count, bool transpose, float* value)
		{
			Functions.ProgramUniformMatrix2x3fv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix2x4dv(uint program, int location, int count, bool transpose, double* value)
		{
			Functions.ProgramUniformMatrix2x4dv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix2x4fv(uint program, int location, int count, bool transpose, float* value)
		{
			Functions.ProgramUniformMatrix2x4fv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix3dv(uint program, int location, int count, bool transpose, double* value)
		{
			Functions.ProgramUniformMatrix3dv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix3fv(uint program, int location, int count, bool transpose, float* value)
		{
			Functions.ProgramUniformMatrix3fv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix3x2dv(uint program, int location, int count, bool transpose, double* value)
		{
			Functions.ProgramUniformMatrix3x2dv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix3x2fv(uint program, int location, int count, bool transpose, float* value)
		{
			Functions.ProgramUniformMatrix3x2fv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix3x4dv(uint program, int location, int count, bool transpose, double* value)
		{
			Functions.ProgramUniformMatrix3x4dv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix3x4fv(uint program, int location, int count, bool transpose, float* value)
		{
			Functions.ProgramUniformMatrix3x4fv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix4dv(uint program, int location, int count, bool transpose, double* value)
		{
			Functions.ProgramUniformMatrix4dv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix4fv(uint program, int location, int count, bool transpose, float* value)
		{
			Functions.ProgramUniformMatrix4fv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix4x2dv(uint program, int location, int count, bool transpose, double* value)
		{
			Functions.ProgramUniformMatrix4x2dv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix4x2fv(uint program, int location, int count, bool transpose, float* value)
		{
			Functions.ProgramUniformMatrix4x2fv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix4x3dv(uint program, int location, int count, bool transpose, double* value)
		{
			Functions.ProgramUniformMatrix4x3dv(program, location, count, transpose, value);
		}

		[OpenGLSupport(4.1)]
		public static void ProgramUniformMatrix4x3fv(uint program, int location, int count, bool transpose, float* value)
		{
			Functions.ProgramUniformMatrix4x3fv(program, location, count, transpose, value);
		}

		[OpenGLSupport(3.2)]
		public static void ProvokingVertex(uint mode)
		{
			Functions.ProvokingVertex(mode);
		}

		[OpenGLSupport(4.3)]
		public static void PushDebugGroup(uint source, uint id, int length, string message)
		{
			Functions.PushDebugGroup(source, id, length, message);
		}

		[OpenGLSupport(3.3)]
		public static void QueryCounter(uint id, uint target)
		{
			Functions.QueryCounter(id, target);
		}

		[OpenGLSupport(4.5)]
		public static void ReadnPixels(int x, int y, int width, int height, uint format, uint type, int bufSize, void* data)
		{
			Functions.ReadnPixels(x, y, width, height, format, type, bufSize, data);
		}

		[OpenGLSupport(1.0)]
		public static void ReadPixels(int x, int y, int width, int height, uint format, uint type, void* pixels)
		{
			Functions.ReadPixels(x, y, width, height, format, type, pixels);
		}

		[OpenGLSupport(4.1)]
		public static void ReleaseShaderCompiler()
		{
			Functions.ReleaseShaderCompiler();
		}

		[OpenGLSupport(4.0)]
		public static void ResumeTransformFeedback()
		{
			Functions.ResumeTransformFeedback();
		}

		[OpenGLSupport(1.3)]
		public static void SampleCoverage(float value, bool invert)
		{
			Functions.SampleCoverage(value, invert);
		}

		[OpenGLSupport(3.2)]
		public static void SampleMaski(uint maskNumber, uint mask)
		{
			Functions.SampleMaski(maskNumber, mask);
		}

		[OpenGLSupport(3.3)]
		public static void SamplerParameterf(uint sampler, uint pname, float param)
		{
			Functions.SamplerParameterf(sampler, pname, param);
		}

		[OpenGLSupport(3.3)]
		public static void SamplerParameterfv(uint sampler, uint pname, float* param)
		{
			Functions.SamplerParameterfv(sampler, pname, param);
		}

		[OpenGLSupport(3.3)]
		public static void SamplerParameteri(uint sampler, uint pname, int param)
		{
			Functions.SamplerParameteri(sampler, pname, param);
		}

		[OpenGLSupport(3.3)]
		public static void SamplerParameterIiv(uint sampler, uint pname, int* param)
		{
			Functions.SamplerParameterIiv(sampler, pname, param);
		}

		[OpenGLSupport(3.3)]
		public static void SamplerParameterIuiv(uint sampler, uint pname, uint* param)
		{
			Functions.SamplerParameterIuiv(sampler, pname, param);
		}

		[OpenGLSupport(3.3)]
		public static void SamplerParameteriv(uint sampler, uint pname, int* param)
		{
			Functions.SamplerParameteriv(sampler, pname, param);
		}

		[OpenGLSupport(1.0)]
		public static void Scissor(int x, int y, int width, int height)
		{
			Functions.Scissor(x, y, width, height);
		}

		[OpenGLSupport(4.1)]
		public static void ScissorArrayv(uint first, int count, int* v)
		{
			Functions.ScissorArrayv(first, count, v);
		}

		[OpenGLSupport(4.1)]
		public static void ScissorIndexed(uint index, int left, int bottom, int width, int height)
		{
			Functions.ScissorIndexed(index, left, bottom, width, height);
		}

		[OpenGLSupport(4.1)]
		public static void ScissorIndexedv(uint index, int* v)
		{
			Functions.ScissorIndexedv(index, v);
		}

		[OpenGLSupport(4.1)]
		public static void ShaderBinary(int count, uint* shaders, uint binaryformat, void* binary, int length)
		{
			Functions.ShaderBinary(count, shaders, binaryformat, binary, length);
		}

		[OpenGLSupport(2.0)]
		public static void ShaderSource(uint shader, int count, string[] @string, int* length)
		{
			Functions.ShaderSource(shader, count, @string, length);
		}

		[OpenGLSupport(4.3)]
		public static void ShaderStorageBlockBinding(uint program, uint storageBlockIndex, uint storageBlockBinding)
		{
			Functions.ShaderStorageBlockBinding(program, storageBlockIndex, storageBlockBinding);
		}

		//[OpenGLSupport(4.6)]
		//public static void SpecializeShader(uint shader, string pEntryPoint, uint numSpecializationConstants, uint* pConstantIndex, uint* pConstantValue)
		//{
		//	Functions.SpecializeShader(shader, pEntryPoint, numSpecializationConstants, pConstantIndex, pConstantValue);
		//}

		[OpenGLSupport(1.0)]
		public static void StencilFunc(uint func, int @ref, uint mask)
		{
			Functions.StencilFunc(func, @ref, mask);
		}

		[OpenGLSupport(2.0)]
		public static void StencilFuncSeparate(uint face, uint func, int @ref, uint mask)
		{
			Functions.StencilFuncSeparate(face, func, @ref, mask);
		}

		[OpenGLSupport(1.0)]
		public static void StencilMask(uint mask)
		{
			Functions.StencilMask(mask);
		}

		[OpenGLSupport(2.0)]
		public static void StencilMaskSeparate(uint face, uint mask)
		{
			Functions.StencilMaskSeparate(face, mask);
		}

		[OpenGLSupport(1.0)]
		public static void StencilOp(uint fail, uint zfail, uint zpass)
		{
			Functions.StencilOp(fail, zfail, zpass);
		}

		[OpenGLSupport(2.0)]
		public static void StencilOpSeparate(uint face, uint sfail, uint dpfail, uint dppass)
		{
			Functions.StencilOpSeparate(face, sfail, dpfail, dppass);
		}

		[OpenGLSupport(4.5)]
		public static void TextureBarrier()
		{
			Functions.TextureBarrier();
		}

		[OpenGLSupport(4.5)]
		public static void TransformFeedbackBufferBase(uint xfb, uint index, uint buffer)
		{
			Functions.TransformFeedbackBufferBase(xfb, index, buffer);
		}

		[OpenGLSupport(4.5)]
		public static void TransformFeedbackBufferRange(uint xfb, uint index, uint buffer, int offset, int size)
		{
			Functions.TransformFeedbackBufferRange(xfb, index, buffer, offset, size);
		}

		[OpenGLSupport(3.0)]
		public static void TransformFeedbackVaryings(uint program, int count, string[] varyings, uint bufferMode)
		{
			Functions.TransformFeedbackVaryings(program, count, varyings, bufferMode);
		}

		[OpenGLSupport(4.0)]
		public static void Uniform1d(int location, double x)
		{
			Functions.Uniform1d(location, x);
		}

		[OpenGLSupport(4.0)]
		public static void Uniform1dv(int location, int count, double* value)
		{
			Functions.Uniform1dv(location, count, value);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform1f(int location, float v0)
		{
			Functions.Uniform1f(location, v0);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform1fv(int location, int count, float* value)
		{
			Functions.Uniform1fv(location, count, value);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform1i(int location, int v0)
		{
			Functions.Uniform1i(location, v0);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform1iv(int location, int count, int* value)
		{
			Functions.Uniform1iv(location, count, value);
		}

		[OpenGLSupport(3.0)]
		public static void Uniform1ui(int location, uint v0)
		{
			Functions.Uniform1ui(location, v0);
		}

		[OpenGLSupport(3.0)]
		public static void Uniform1uiv(int location, int count, uint* value)
		{
			Functions.Uniform1uiv(location, count, value);
		}

		[OpenGLSupport(4.0)]
		public static void Uniform2d(int location, double x, double y)
		{
			Functions.Uniform2d(location, x, y);
		}

		[OpenGLSupport(4.0)]
		public static void Uniform2dv(int location, int count, double* value)
		{
			Functions.Uniform2dv(location, count, value);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform2f(int location, float v0, float v1)
		{
			Functions.Uniform2f(location, v0, v1);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform2fv(int location, int count, float* value)
		{
			Functions.Uniform2fv(location, count, value);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform2i(int location, int v0, int v1)
		{
			Functions.Uniform2i(location, v0, v1);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform2iv(int location, int count, int* value)
		{
			Functions.Uniform2iv(location, count, value);
		}

		[OpenGLSupport(3.0)]
		public static void Uniform2ui(int location, uint v0, uint v1)
		{
			Functions.Uniform2ui(location, v0, v1);
		}

		[OpenGLSupport(3.0)]
		public static void Uniform2uiv(int location, int count, uint* value)
		{
			Functions.Uniform2uiv(location, count, value);
		}

		[OpenGLSupport(4.0)]
		public static void Uniform3d(int location, double x, double y, double z)
		{
			Functions.Uniform3d(location, x, y, z);
		}

		[OpenGLSupport(4.0)]
		public static void Uniform3dv(int location, int count, double* value)
		{
			Functions.Uniform3dv(location, count, value);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform3f(int location, float v0, float v1, float v2)
		{
			Functions.Uniform3f(location, v0, v1, v2);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform3fv(int location, int count, float* value)
		{
			Functions.Uniform3fv(location, count, value);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform3i(int location, int v0, int v1, int v2)
		{
			Functions.Uniform3i(location, v0, v1, v2);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform3iv(int location, int count, int* value)
		{
			Functions.Uniform3iv(location, count, value);
		}

		[OpenGLSupport(3.0)]
		public static void Uniform3ui(int location, uint v0, uint v1, uint v2)
		{
			Functions.Uniform3ui(location, v0, v1, v2);
		}

		[OpenGLSupport(3.0)]
		public static void Uniform3uiv(int location, int count, uint* value)
		{
			Functions.Uniform3uiv(location, count, value);
		}

		[OpenGLSupport(4.0)]
		public static void Uniform4d(int location, double x, double y, double z, double w)
		{
			Functions.Uniform4d(location, x, y, z, w);
		}

		[OpenGLSupport(4.0)]
		public static void Uniform4dv(int location, int count, double* value)
		{
			Functions.Uniform4dv(location, count, value);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform4f(int location, float v0, float v1, float v2, float v3)
		{
			Functions.Uniform4f(location, v0, v1, v2, v3);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform4fv(int location, int count, float* value)
		{
			Functions.Uniform4fv(location, count, value);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform4i(int location, int v0, int v1, int v2, int v3)
		{
			Functions.Uniform4i(location, v0, v1, v2, v3);
		}

		[OpenGLSupport(2.0)]
		public static void Uniform4iv(int location, int count, int* value)
		{
			Functions.Uniform4iv(location, count, value);
		}

		[OpenGLSupport(3.0)]
		public static void Uniform4ui(int location, uint v0, uint v1, uint v2, uint v3)
		{
			Functions.Uniform4ui(location, v0, v1, v2, v3);
		}

		[OpenGLSupport(3.0)]
		public static void Uniform4uiv(int location, int count, uint* value)
		{
			Functions.Uniform4uiv(location, count, value);
		}

		[OpenGLSupport(3.1)]
		public static void UniformBlockBinding(uint program, uint uniformBlockIndex, uint uniformBlockBinding)
		{
			Functions.UniformBlockBinding(program, uniformBlockIndex, uniformBlockBinding);
		}

		[OpenGLSupport(4.0)]
		public static void UniformMatrix2dv(int location, int count, bool transpose, double* value)
		{
			Functions.UniformMatrix2dv(location, count, transpose, value);
		}

		[OpenGLSupport(2.0)]
		public static void UniformMatrix2fv(int location, int count, bool transpose, float* value)
		{
			Functions.UniformMatrix2fv(location, count, transpose, value);
		}

		[OpenGLSupport(4.0)]
		public static void UniformMatrix2x3dv(int location, int count, bool transpose, double* value)
		{
			Functions.UniformMatrix2x3dv(location, count, transpose, value);
		}

		[OpenGLSupport(2.1)]
		public static void UniformMatrix2x3fv(int location, int count, bool transpose, float* value)
		{
			Functions.UniformMatrix2x3fv(location, count, transpose, value);
		}

		[OpenGLSupport(4.0)]
		public static void UniformMatrix2x4dv(int location, int count, bool transpose, double* value)
		{
			Functions.UniformMatrix2x4dv(location, count, transpose, value);
		}

		[OpenGLSupport(2.1)]
		public static void UniformMatrix2x4fv(int location, int count, bool transpose, float* value)
		{
			Functions.UniformMatrix2x4fv(location, count, transpose, value);
		}

		[OpenGLSupport(4.0)]
		public static void UniformMatrix3dv(int location, int count, bool transpose, double* value)
		{
			Functions.UniformMatrix3dv(location, count, transpose, value);
		}

		[OpenGLSupport(2.0)]
		public static void UniformMatrix3fv(int location, int count, bool transpose, float* value)
		{
			Functions.UniformMatrix3fv(location, count, transpose, value);
		}

		[OpenGLSupport(4.0)]
		public static void UniformMatrix3x2dv(int location, int count, bool transpose, double* value)
		{
			Functions.UniformMatrix3x2dv(location, count, transpose, value);
		}

		[OpenGLSupport(2.1)]
		public static void UniformMatrix3x2fv(int location, int count, bool transpose, float* value)
		{
			Functions.UniformMatrix3x2fv(location, count, transpose, value);
		}

		[OpenGLSupport(4.0)]
		public static void UniformMatrix3x4dv(int location, int count, bool transpose, double* value)
		{
			Functions.UniformMatrix3x4dv(location, count, transpose, value);
		}

		[OpenGLSupport(2.1)]
		public static void UniformMatrix3x4fv(int location, int count, bool transpose, float* value)
		{
			Functions.UniformMatrix3x4fv(location, count, transpose, value);
		}

		[OpenGLSupport(4.0)]
		public static void UniformMatrix4dv(int location, int count, bool transpose, double* value)
		{
			Functions.UniformMatrix4dv(location, count, transpose, value);
		}

		[OpenGLSupport(2.0)]
		public static void UniformMatrix4fv(int location, int count, bool transpose, float* value)
		{
			Functions.UniformMatrix4fv(location, count, transpose, value);
		}

		[OpenGLSupport(4.0)]
		public static void UniformMatrix4x2dv(int location, int count, bool transpose, double* value)
		{
			Functions.UniformMatrix4x2dv(location, count, transpose, value);
		}

		[OpenGLSupport(2.1)]
		public static void UniformMatrix4x2fv(int location, int count, bool transpose, float* value)
		{
			Functions.UniformMatrix4x2fv(location, count, transpose, value);
		}

		[OpenGLSupport(4.0)]
		public static void UniformMatrix4x3dv(int location, int count, bool transpose, double* value)
		{
			Functions.UniformMatrix4x3dv(location, count, transpose, value);
		}

		[OpenGLSupport(2.1)]
		public static void UniformMatrix4x3fv(int location, int count, bool transpose, float* value)
		{
			Functions.UniformMatrix4x3fv(location, count, transpose, value);
		}

		[OpenGLSupport(4.0)]
		public static void UniformSubroutinesuiv(uint shadertype, int count, uint* indices)
		{
			Functions.UniformSubroutinesuiv(shadertype, count, indices);
		}

		[OpenGLSupport(1.5)]
		public static bool UnmapBuffer(uint target)
		{
			return Functions.UnmapBuffer(target);
		}

		[OpenGLSupport(4.5)]
		public static bool UnmapNamedBuffer(uint buffer)
		{
			return Functions.UnmapNamedBuffer(buffer);
		}

		[OpenGLSupport(2.0)]
		public static void UseProgram(uint program)
		{
			context.boundShaderProgram = program;

			Functions.UseProgram(program);
		}

		[OpenGLSupport(4.1)]
		public static void UseProgramStages(uint pipeline, uint stages, uint program)
		{
			Functions.UseProgramStages(pipeline, stages, program);
		}

		[OpenGLSupport(2.0)]
		public static void ValidateProgram(uint program)
		{
			Functions.ValidateProgram(program);
		}

		[OpenGLSupport(4.1)]
		public static void ValidateProgramPipeline(uint pipeline)
		{
			Functions.ValidateProgramPipeline(pipeline);
		}

		[OpenGLSupport(4.5)]
		public static void VertexArrayAttribBinding(uint vaobj, uint attribindex, uint bindingindex)
		{
			Functions.VertexArrayAttribBinding(vaobj, attribindex, bindingindex);
		}

		[OpenGLSupport(4.5)]
		public static void VertexArrayAttribFormat(uint vaobj, uint attribindex, int size, uint type, bool normalized, uint relativeoffset)
		{
			Functions.VertexArrayAttribFormat(vaobj, attribindex, size, type, normalized, relativeoffset);
		}

		[OpenGLSupport(4.5)]
		public static void VertexArrayAttribIFormat(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset)
		{
			Functions.VertexArrayAttribIFormat(vaobj, attribindex, size, type, relativeoffset);
		}

		[OpenGLSupport(4.5)]
		public static void VertexArrayAttribLFormat(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset)
		{
			Functions.VertexArrayAttribLFormat(vaobj, attribindex, size, type, relativeoffset);
		}

		[OpenGLSupport(4.5)]
		public static void VertexArrayBindingDivisor(uint vaobj, uint bindingindex, uint divisor)
		{
			Functions.VertexArrayBindingDivisor(vaobj, bindingindex, divisor);
		}

		[OpenGLSupport(4.5)]
		public static void VertexArrayElementBuffer(uint vaobj, uint buffer)
		{
			Functions.VertexArrayElementBuffer(vaobj, buffer);
		}

		[OpenGLSupport(4.5)]
		public static void VertexArrayVertexBuffer(uint vaobj, uint bindingindex, uint buffer, int offset, int stride)
		{
			Functions.VertexArrayVertexBuffer(vaobj, bindingindex, buffer, offset, stride);
		}

		[OpenGLSupport(4.5)]
		public static void VertexArrayVertexBuffers(uint vaobj, uint first, int count, uint* buffers, int* offsets, int* strides)
		{
			Functions.VertexArrayVertexBuffers(vaobj, first, count, buffers, offsets, strides);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib1d(uint index, double x)
		{
			Functions.VertexAttrib1d(index, x);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib1dv(uint index, double* v)
		{
			Functions.VertexAttrib1dv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib1f(uint index, float x)
		{
			Functions.VertexAttrib1f(index, x);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib1fv(uint index, float* v)
		{
			Functions.VertexAttrib1fv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib1s(uint index, short x)
		{
			Functions.VertexAttrib1s(index, x);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib1sv(uint index, short* v)
		{
			Functions.VertexAttrib1sv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib2d(uint index, double x, double y)
		{
			Functions.VertexAttrib2d(index, x, y);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib2dv(uint index, double* v)
		{
			Functions.VertexAttrib2dv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib2f(uint index, float x, float y)
		{
			Functions.VertexAttrib2f(index, x, y);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib2fv(uint index, float* v)
		{
			Functions.VertexAttrib2fv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib2s(uint index, short x, short y)
		{
			Functions.VertexAttrib2s(index, x, y);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib2sv(uint index, short* v)
		{
			Functions.VertexAttrib2sv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib3d(uint index, double x, double y, double z)
		{
			Functions.VertexAttrib3d(index, x, y, z);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib3dv(uint index, double* v)
		{
			Functions.VertexAttrib3dv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib3f(uint index, float x, float y, float z)
		{
			Functions.VertexAttrib3f(index, x, y, z);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib3fv(uint index, float* v)
		{
			Functions.VertexAttrib3fv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib3s(uint index, short x, short y, short z)
		{
			Functions.VertexAttrib3s(index, x, y, z);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib3sv(uint index, short* v)
		{
			Functions.VertexAttrib3sv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4bv(uint index, sbyte* v)
		{
			Functions.VertexAttrib4bv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4d(uint index, double x, double y, double z, double w)
		{
			Functions.VertexAttrib4d(index, x, y, z, w);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4dv(uint index, double* v)
		{
			Functions.VertexAttrib4dv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4f(uint index, float x, float y, float z, float w)
		{
			Functions.VertexAttrib4f(index, x, y, z, w);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4fv(uint index, float* v)
		{
			Functions.VertexAttrib4fv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4iv(uint index, int* v)
		{
			Functions.VertexAttrib4iv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4Nbv(uint index, sbyte* v)
		{
			Functions.VertexAttrib4Nbv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4Niv(uint index, int* v)
		{
			Functions.VertexAttrib4Niv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4Nsv(uint index, short* v)
		{
			Functions.VertexAttrib4Nsv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4Nub(uint index, byte x, byte y, byte z, byte w)
		{
			Functions.VertexAttrib4Nub(index, x, y, z, w);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4Nubv(uint index, byte* v)
		{
			Functions.VertexAttrib4Nubv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4Nuiv(uint index, uint* v)
		{
			Functions.VertexAttrib4Nuiv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4Nusv(uint index, ushort* v)
		{
			Functions.VertexAttrib4Nusv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4s(uint index, short x, short y, short z, short w)
		{
			Functions.VertexAttrib4s(index, x, y, z, w);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4sv(uint index, short* v)
		{
			Functions.VertexAttrib4sv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4ubv(uint index, byte* v)
		{
			Functions.VertexAttrib4ubv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4uiv(uint index, uint* v)
		{
			Functions.VertexAttrib4uiv(index, v);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttrib4usv(uint index, ushort* v)
		{
			Functions.VertexAttrib4usv(index, v);
		}

		[OpenGLSupport(4.3)]
		public static void VertexAttribBinding(uint attribindex, uint bindingindex)
		{
			Functions.VertexAttribBinding(attribindex, bindingindex);
		}

		[OpenGLSupport(3.3)]
		public static void VertexAttribDivisor(uint index, uint divisor)
		{
			Functions.VertexAttribDivisor(index, divisor);
		}

		[OpenGLSupport(4.3)]
		public static void VertexAttribFormat(uint attribindex, int size, uint type, bool normalized, uint relativeoffset)
		{
			Functions.VertexAttribFormat(attribindex, size, type, normalized, relativeoffset);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI1i(uint index, int x)
		{
			Functions.VertexAttribI1i(index, x);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI1iv(uint index, int* v)
		{
			Functions.VertexAttribI1iv(index, v);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI1ui(uint index, uint x)
		{
			Functions.VertexAttribI1ui(index, x);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI1uiv(uint index, uint* v)
		{
			Functions.VertexAttribI1uiv(index, v);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI2i(uint index, int x, int y)
		{
			Functions.VertexAttribI2i(index, x, y);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI2iv(uint index, int* v)
		{
			Functions.VertexAttribI2iv(index, v);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI2ui(uint index, uint x, uint y)
		{
			Functions.VertexAttribI2ui(index, x, y);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI2uiv(uint index, uint* v)
		{
			Functions.VertexAttribI2uiv(index, v);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI3i(uint index, int x, int y, int z)
		{
			Functions.VertexAttribI3i(index, x, y, z);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI3iv(uint index, int* v)
		{
			Functions.VertexAttribI3iv(index, v);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI3ui(uint index, uint x, uint y, uint z)
		{
			Functions.VertexAttribI3ui(index, x, y, z);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI3uiv(uint index, uint* v)
		{
			Functions.VertexAttribI3uiv(index, v);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI4bv(uint index, sbyte* v)
		{
			Functions.VertexAttribI4bv(index, v);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI4i(uint index, int x, int y, int z, int w)
		{
			Functions.VertexAttribI4i(index, x, y, z, w);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI4iv(uint index, int* v)
		{
			Functions.VertexAttribI4iv(index, v);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI4sv(uint index, short* v)
		{
			Functions.VertexAttribI4sv(index, v);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI4ubv(uint index, byte* v)
		{
			Functions.VertexAttribI4ubv(index, v);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI4ui(uint index, uint x, uint y, uint z, uint w)
		{
			Functions.VertexAttribI4ui(index, x, y, z, w);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI4uiv(uint index, uint* v)
		{
			Functions.VertexAttribI4uiv(index, v);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribI4usv(uint index, ushort* v)
		{
			Functions.VertexAttribI4usv(index, v);
		}

		[OpenGLSupport(4.3)]
		public static void VertexAttribIFormat(uint attribindex, int size, uint type, uint relativeoffset)
		{
			Functions.VertexAttribIFormat(attribindex, size, type, relativeoffset);
		}

		[OpenGLSupport(3.0)]
		public static void VertexAttribIPointer(uint index, int size, uint type, int stride, void* pointer)
		{
			Functions.VertexAttribIPointer(index, size, type, stride, pointer);
		}

		[OpenGLSupport(4.1)]
		public static void VertexAttribL1d(uint index, double x)
		{
			Functions.VertexAttribL1d(index, x);
		}

		[OpenGLSupport(4.1)]
		public static void VertexAttribL1dv(uint index, double* v)
		{
			Functions.VertexAttribL1dv(index, v);
		}

		[OpenGLSupport(4.1)]
		public static void VertexAttribL2d(uint index, double x, double y)
		{
			Functions.VertexAttribL2d(index, x, y);
		}

		[OpenGLSupport(4.1)]
		public static void VertexAttribL2dv(uint index, double* v)
		{
			Functions.VertexAttribL2dv(index, v);
		}

		[OpenGLSupport(4.1)]
		public static void VertexAttribL3d(uint index, double x, double y, double z)
		{
			Functions.VertexAttribL3d(index, x, y, z);
		}

		[OpenGLSupport(4.1)]
		public static void VertexAttribL3dv(uint index, double* v)
		{
			Functions.VertexAttribL3dv(index, v);
		}

		[OpenGLSupport(4.1)]
		public static void VertexAttribL4d(uint index, double x, double y, double z, double w)
		{
			Functions.VertexAttribL4d(index, x, y, z, w);
		}

		[OpenGLSupport(4.1)]
		public static void VertexAttribL4dv(uint index, double* v)
		{
			Functions.VertexAttribL4dv(index, v);
		}

		[OpenGLSupport(4.3)]
		public static void VertexAttribLFormat(uint attribindex, int size, uint type, uint relativeoffset)
		{
			Functions.VertexAttribLFormat(attribindex, size, type, relativeoffset);
		}

		[OpenGLSupport(4.1)]
		public static void VertexAttribLPointer(uint index, int size, uint type, int stride, void* pointer)
		{
			Functions.VertexAttribLPointer(index, size, type, stride, pointer);
		}

		[OpenGLSupport(3.3)]
		public static void VertexAttribP1ui(uint index, uint type, bool normalized, uint value)
		{
			Functions.VertexAttribP1ui(index, type, normalized, value);
		}

		[OpenGLSupport(3.3)]
		public static void VertexAttribP1uiv(uint index, uint type, bool normalized, uint* value)
		{
			Functions.VertexAttribP1uiv(index, type, normalized, value);
		}

		[OpenGLSupport(3.3)]
		public static void VertexAttribP2ui(uint index, uint type, bool normalized, uint value)
		{
			Functions.VertexAttribP2ui(index, type, normalized, value);
		}

		[OpenGLSupport(3.3)]
		public static void VertexAttribP2uiv(uint index, uint type, bool normalized, uint* value)
		{
			Functions.VertexAttribP2uiv(index, type, normalized, value);
		}

		[OpenGLSupport(3.3)]
		public static void VertexAttribP3ui(uint index, uint type, bool normalized, uint value)
		{
			Functions.VertexAttribP3ui(index, type, normalized, value);
		}

		[OpenGLSupport(3.3)]
		public static void VertexAttribP3uiv(uint index, uint type, bool normalized, uint* value)
		{
			Functions.VertexAttribP3uiv(index, type, normalized, value);
		}

		[OpenGLSupport(3.3)]
		public static void VertexAttribP4ui(uint index, uint type, bool normalized, uint value)
		{
			Functions.VertexAttribP4ui(index, type, normalized, value);
		}

		[OpenGLSupport(3.3)]
		public static void VertexAttribP4uiv(uint index, uint type, bool normalized, uint* value)
		{
			Functions.VertexAttribP4uiv(index, type, normalized, value);
		}

		[OpenGLSupport(2.0)]
		public static void VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, void* pointer)
		{
			Functions.VertexAttribPointer(index, size, type, normalized, stride, pointer);
		}

		[OpenGLSupport(4.3)]
		public static void VertexBindingDivisor(uint bindingindex, uint divisor)
		{
			Functions.VertexBindingDivisor(bindingindex, divisor);
		}

		[OpenGLSupport(3.2)]
		public static void WaitSync(IntPtr sync, uint flags, ulong timeout)
		{
			Functions.WaitSync(sync, flags, timeout);
		}
	}
}