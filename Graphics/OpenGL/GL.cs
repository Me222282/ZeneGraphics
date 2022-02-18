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
using System.Runtime.InteropServices;
using System.Text;

namespace Zene.Graphics.OpenGL
{
	public static unsafe partial class GL
	{
		public struct TextureBinding
		{
			public uint Texture1D;
			public uint Texture1DArray;
			public uint Texture2D;
			public uint Texture2DArray;
			public uint Texture2DArrayMS;
			public uint Texture2DMS;
			public uint Texture3D;
			public uint CubeMap;
			public uint CubeMapArray;
			public uint Buffer;
			public uint Rectangle;
		}

		public struct BufferBinding
		{
			public uint Array;
			public uint AtomicCounter;
			public uint CopyRead;
			public uint CopyWrite;
			public uint DispatchIndirect;
			public uint Indirect;
			public uint ElementArray;
			public uint PixelPack;
			public uint PixelUnpack;
			public uint Query;
			public uint ShaderStorage;
			public uint Texture;
			public uint TransformFeedback;
			public uint Uniform;
		}

		public struct FrameBufferBinding
		{
			public uint Read;
			public uint Draw;
		}

		public static double Version { get; private set; } = 0.0;

		public static uint ActiveTextureUnit { get; private set; } = 0;

		public static TextureBinding[] BoundTextures { get; private set; }
		private static FrameBufferBinding _boundFrameBuffers = new FrameBufferBinding();
		public static FrameBufferBinding BoundFrameBuffers => _boundFrameBuffers;
		public static uint BoundRenderbuffer { get; private set; } = 0;
		public static uint BoundShaderProgram { get; private set; } = 0;
		private static BufferBinding _boundBuffers = new BufferBinding();
		public static BufferBinding BoundBuffers => _boundBuffers;

		public delegate void DebugProc(uint source, uint type, uint id, uint severity, int length, string message, IntPtr userParam);

		public static class Delegates
		{
			public delegate void ActiveShaderProgram(uint pipeline, uint program);

			public delegate void ActiveTexture(uint texture);

			public delegate void AttachShader(uint program, uint shader);

			public delegate void BeginConditionalRender(uint id, uint mode);

			public delegate void BeginQuery(uint target, uint id);

			public delegate void BeginQueryIndexed(uint target, uint index, uint id);

			public delegate void BeginTransformFeedback(uint primitiveMode);

			public delegate void BindAttribLocation(uint program, uint index, string name);

			public delegate void BindBuffer(uint target, uint buffer);

			public delegate void BindBufferBase(uint target, uint index, uint buffer);

			public delegate void BindBufferRange(uint target, uint index, uint buffer, int offset, int size);

			public delegate void BindBuffersBase(uint target, uint first, int count, uint* buffers);

			public delegate void BindBuffersRange(uint target, uint first, int count, uint* buffers, int* offsets, int* sizes);

			public delegate void BindFragDataLocation(uint program, uint colour, string name);

			public delegate void BindFragDataLocationIndexed(uint program, uint colourNumber, uint index, string name);

			public delegate void BindFramebuffer(uint target, uint framebuffer);

			public delegate void BindImageTexture(uint unit, uint texture, int level, bool layered, int layer, uint access, uint format);

			public delegate void BindProgramPipeline(uint pipeline);

			public delegate void BindRenderbuffer(uint target, uint renderbuffer);

			public delegate void BindSampler(uint unit, uint sampler);

			public delegate void BindSamplers(uint first, int count, uint* samplers);

			public delegate void BindTexture(uint target, uint texture);

			public delegate void BindTextureUnit(uint unit, uint texture);

			public delegate void BindTransformFeedback(uint target, uint id);

			public delegate void BindVertexArray(uint array);

			public delegate void BindVertexBuffer(uint bindingindex, uint buffer, int offset, int stride);

			public delegate void BindVertexBuffers(uint first, int count, uint* buffers, int* offsets, int* strides);

			public delegate void BlendColour(float red, float green, float blue, float alpha);

			public delegate void BlendEquation(uint mode);

			public delegate void BlendEquationi(uint buf, uint mode);

			public delegate void BlendEquationSeparate(uint modeRGB, uint modeAlpha);

			public delegate void BlendEquationSeparatei(uint buf, uint modeRGB, uint modeAlpha);

			public delegate void BlendFunc(uint sfactor, uint dfactor);

			public delegate void BlendFunci(uint buf, uint src, uint dst);

			public delegate void BlendFuncSeparate(uint sfactorRGB, uint dfactorRGB, uint sfactorAlpha, uint dfactorAlpha);

			public delegate void BlendFuncSeparatei(uint buf, uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha);

			public delegate void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, uint filter);

			public delegate void BlitNamedFramebuffer(uint readFramebuffer, uint drawFramebuffer, int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, uint filter);

			public delegate void BufferData(uint target, int size, void* data, uint usage);

			public delegate void BufferStorage(uint target, int size, void* data, uint flags);

			public delegate void BufferSubData(uint target, int offset, int size, void* data);

			public delegate uint CheckFramebufferStatus(uint target);

			public delegate uint CheckNamedFramebufferStatus(uint framebuffer, uint target);

			public delegate void ClampColour(uint target, uint clamp);

			public delegate void Clear(uint mask);

			public delegate void ClearBufferData(uint target, uint internalformat, uint format, uint type, void* data);

			public delegate void ClearBufferfi(uint buffer, int drawbuffer, float depth, int stencil);

			public delegate void ClearBufferfv(uint buffer, int drawbuffer, float* value);

			public delegate void ClearBufferiv(uint buffer, int drawbuffer, int* value);

			public delegate void ClearBufferSubData(uint target, uint internalformat, int offset, int size, uint format, uint type, void* data);

			public delegate void ClearBufferuiv(uint buffer, int drawbuffer, uint* value);

			public delegate void ClearColour(float red, float green, float blue, float alpha);

			public delegate void ClearDepth(double depth);

			public delegate void ClearDepthf(float d);

			public delegate void ClearNamedBufferData(uint buffer, uint internalformat, uint format, uint type, void* data);

			public delegate void ClearNamedBufferSubData(uint buffer, uint internalformat, int offset, int size, uint format, uint type, void* data);

			public delegate void ClearNamedFramebufferfi(uint framebuffer, uint buffer, int drawbuffer, float depth, int stencil);

			public delegate void ClearNamedFramebufferfv(uint framebuffer, uint buffer, int drawbuffer, float* value);

			public delegate void ClearNamedFramebufferiv(uint framebuffer, uint buffer, int drawbuffer, int* value);

			public delegate void ClearNamedFramebufferuiv(uint framebuffer, uint buffer, int drawbuffer, uint* value);

			public delegate void ClearStencil(int s);

			public delegate void ClearTexImage(uint texture, int level, uint format, uint type, void* data);

			public delegate void ClearTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, void* data);

			public delegate uint ClientWaitSync(IntPtr sync, uint flags, ulong timeout);

			public delegate void ClipControl(uint origin, uint depth);

			public delegate void ColourMask(bool red, bool green, bool blue, bool alpha);

			public delegate void ColourMaski(uint index, bool r, bool g, bool b, bool a);

			public delegate void CompileShader(uint shader);

			public delegate void CompressedTexImage1D(uint target, int level, uint internalformat, int width, int border, int imageSize, void* data);

			public delegate void CompressedTexImage2D(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, void* data);

			public delegate void CompressedTexImage3D(uint target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, void* data);

			public delegate void CompressedTexSubImage1D(uint target, int level, int xoffset, int width, uint format, int imageSize, void* data);

			public delegate void CompressedTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, void* data);

			public delegate void CompressedTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, void* data);

			public delegate void CompressedTextureSubImage1D(uint texture, int level, int xoffset, int width, uint format, int imageSize, void* data);

			public delegate void CompressedTextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, void* data);

			public delegate void CompressedTextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, void* data);

			public delegate void CopyBufferSubData(uint readTarget, uint writeTarget, int readOffset, int writeOffset, int size);

			public delegate void CopyImageSubData(uint srcName, uint srcTarget, int srcLevel, int srcX, int srcY, int srcZ, uint dstName, uint dstTarget, int dstLevel, int dstX, int dstY, int dstZ, int srcWidth, int srcHeight, int srcDepth);

			public delegate void CopyNamedBufferSubData(uint readBuffer, uint writeBuffer, int readOffset, int writeOffset, int size);

			public delegate void CopyTexImage1D(uint target, int level, uint internalformat, int x, int y, int width, int border);

			public delegate void CopyTexImage2D(uint target, int level, uint internalformat, int x, int y, int width, int height, int border);

			public delegate void CopyTexSubImage1D(uint target, int level, int xoffset, int x, int y, int width);

			public delegate void CopyTexSubImage2D(uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height);

			public delegate void CopyTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height);

			public delegate void CopyTextureSubImage1D(uint texture, int level, int xoffset, int x, int y, int width);

			public delegate void CopyTextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int x, int y, int width, int height);

			public delegate void CopyTextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height);

			public delegate void CreateBuffers(int n, uint* buffers);

			public delegate void CreateFramebuffers(int n, uint* framebuffers);

			public delegate uint CreateProgram();

			public delegate void CreateProgramPipelines(int n, uint* pipelines);

			public delegate void CreateQueries(uint target, int n, uint* ids);

			public delegate void CreateRenderbuffers(int n, uint* renderbuffers);

			public delegate void CreateSamplers(int n, uint* samplers);

			public delegate uint CreateShader(uint type);

			public delegate uint CreateShaderProgramv(uint type, int count, string[] strings);

			public delegate void CreateTextures(uint target, int n, uint* textures);

			public delegate void CreateTransformFeedbacks(int n, uint* ids);

			public delegate void CreateVertexArrays(int n, uint* arrays);

			public delegate void CullFace(uint mode);

			public delegate void DebugMessageCallback(DebugProc callback, void* userParam);

			public delegate void DebugMessageControl(uint source, uint type, uint severity, int count, uint* ids, bool enabled);

			public delegate void DebugMessageInsert(uint source, uint type, uint id, uint severity, int length, string buf);

			public delegate void DeleteBuffers(int n, uint* buffers);

			public delegate void DeleteFramebuffers(int n, uint* framebuffers);

			public delegate void DeleteProgram(uint program);

			public delegate void DeleteProgramPipelines(int n, uint* pipelines);

			public delegate void DeleteQueries(int n, uint* ids);

			public delegate void DeleteRenderbuffers(int n, uint* renderbuffers);

			public delegate void DeleteSamplers(int count, uint* samplers);

			public delegate void DeleteShader(uint shader);

			public delegate void DeleteSync(IntPtr sync);

			public delegate void DeleteTextures(int n, uint* textures);

			public delegate void DeleteTransformFeedbacks(int n, uint* ids);

			public delegate void DeleteVertexArrays(int n, uint* arrays);

			public delegate void DepthFunc(uint func);

			public delegate void DepthMask(bool flag);

			public delegate void DepthRange(double n, double f);

			public delegate void DepthRangeArrayv(uint first, int count, double* v);

			public delegate void DepthRangef(float n, float f);

			public delegate void DepthRangeIndexed(uint index, double n, double f);

			public delegate void DetachShader(uint program, uint shader);

			public delegate void Disable(uint cap);

			public delegate void Disablei(uint target, uint index);

			public delegate void DisableVertexArrayAttrib(uint vaobj, uint index);

			public delegate void DisableVertexAttribArray(uint index);

			public delegate void DispatchCompute(uint num_groups_x, uint num_groups_y, uint num_groups_z);

			public delegate void DispatchComputeIndirect(int indirect);

			public delegate void DrawArrays(uint mode, int first, int count);

			public delegate void DrawArraysIndirect(uint mode, void* indirect);

			public delegate void DrawArraysInstanced(uint mode, int first, int count, int instancecount);

			public delegate void DrawArraysInstancedBaseInstance(uint mode, int first, int count, int instancecount, uint baseinstance);

			public delegate void DrawBuffer(uint buf);

			public delegate void DrawBuffers(int n, uint* bufs);

			public delegate void DrawElements(uint mode, int count, uint type, void* indices);

			public delegate void DrawElementsBaseVertex(uint mode, int count, uint type, void* indices, int basevertex);

			public delegate void DrawElementsIndirect(uint mode, uint type, void* indirect);

			public delegate void DrawElementsInstanced(uint mode, int count, uint type, void* indices, int instancecount);

			public delegate void DrawElementsInstancedBaseInstance(uint mode, int count, uint type, void* indices, int instancecount, uint baseinstance);

			public delegate void DrawElementsInstancedBaseVertex(uint mode, int count, uint type, void* indices, int instancecount, int basevertex);

			public delegate void DrawElementsInstancedBaseVertexBaseInstance(uint mode, int count, uint type, void* indices, int instancecount, int basevertex, uint baseinstance);

			public delegate void DrawRangeElements(uint mode, uint start, uint end, int count, uint type, void* indices);

			public delegate void DrawRangeElementsBaseVertex(uint mode, uint start, uint end, int count, uint type, void* indices, int basevertex);

			public delegate void DrawTransformFeedback(uint mode, uint id);

			public delegate void DrawTransformFeedbackInstanced(uint mode, uint id, int instancecount);

			public delegate void DrawTransformFeedbackStream(uint mode, uint id, uint stream);

			public delegate void DrawTransformFeedbackStreamInstanced(uint mode, uint id, uint stream, int instancecount);

			public delegate void Enable(uint cap);

			public delegate void Enablei(uint target, uint index);

			public delegate void EnableVertexArrayAttrib(uint vaobj, uint index);

			public delegate void EnableVertexAttribArray(uint index);

			public delegate void EndConditionalRender();

			public delegate void EndQuery(uint target);

			public delegate void EndQueryIndexed(uint target, uint index);

			public delegate void EndTransformFeedback();

			public delegate IntPtr FenceSync(uint condition, uint flags);

			public delegate void Finish();

			public delegate void Flush();

			public delegate void FlushMappedBufferRange(uint target, int offset, int length);

			public delegate void FlushMappedNamedBufferRange(uint buffer, int offset, int length);

			public delegate void FramebufferParameteri(uint target, uint pname, int param);

			public delegate void FramebufferRenderbuffer(uint target, uint attachment, uint renderbuffertarget, uint renderbuffer);

			public delegate void FramebufferTexture(uint target, uint attachment, uint texture, int level);

			public delegate void FramebufferTexture1D(uint target, uint attachment, uint textarget, uint texture, int level);

			public delegate void FramebufferTexture2D(uint target, uint attachment, uint textarget, uint texture, int level);

			public delegate void FramebufferTexture3D(uint target, uint attachment, uint textarget, uint texture, int level, int zoffset);

			public delegate void FramebufferTextureLayer(uint target, uint attachment, uint texture, int level, int layer);

			public delegate void FrontFace(uint mode);

			public delegate void GenBuffers(int n, uint* buffers);

			public delegate void GenerateMipmap(uint target);

			public delegate void GenerateTextureMipmap(uint texture);

			public delegate void GenFramebuffers(int n, uint* framebuffers);

			public delegate void GenProgramPipelines(int n, uint* pipelines);

			public delegate void GenQueries(int n, uint* ids);

			public delegate void GenRenderbuffers(int n, uint* renderbuffers);

			public delegate void GenSamplers(int count, uint* samplers);

			public delegate void GenTextures(int n, uint* textures);

			public delegate void GenTransformFeedbacks(int n, uint* ids);

			public delegate void GenVertexArrays(int n, uint* arrays);

			public delegate void GetActiveAtomicCounterBufferiv(uint program, uint bufferIndex, uint pname, int* @params);

			public delegate void GetActiveAttrib(uint program, uint index, int bufSize, int* length, int* size, uint* type, StringBuilder name);

			public delegate void GetActiveSubroutineName(uint program, uint shadertype, uint index, int bufsize, int* length, StringBuilder name);

			public delegate void GetActiveSubroutineUniformiv(uint program, uint shadertype, uint index, uint pname, int* values);

			public delegate void GetActiveSubroutineUniformName(uint program, uint shadertype, uint index, int bufsize, int* length, StringBuilder name);

			public delegate void GetActiveUniform(uint program, uint index, int bufSize, int* length, int* size, uint* type, StringBuilder name);

			public delegate void GetActiveUniformBlockiv(uint program, uint uniformBlockIndex, uint pname, int* @params);

			public delegate void GetActiveUniformBlockName(uint program, uint uniformBlockIndex, int bufSize, int* length, StringBuilder uniformBlockName);

			public delegate void GetActiveUniformName(uint program, uint uniformIndex, int bufSize, int* length, StringBuilder uniformName);

			public delegate void GetActiveUniformsiv(uint program, int uniformCount, uint* uniformIndices, uint pname, int* @params);

			public delegate void GetAttachedShaders(uint program, int maxCount, int* count, uint* shaders);

			public delegate int GetAttribLocation(uint program, string name);

			public delegate void GetBooleani_v(uint target, uint index, bool* data);

			public delegate void GetBooleanv(uint pname, bool* data);

			public delegate void GetBufferParameteri64v(uint target, uint pname, long* @params);

			public delegate void GetBufferParameteriv(uint target, uint pname, int* @params);

			public delegate void GetBufferPointerv(uint target, uint pname, void** @params);

			public delegate void GetBufferSubData(uint target, int offset, int size, void* data);

			public delegate void GetCompressedTexImage(uint target, int level, void* img);

			public delegate void GetCompressedTextureImage(uint texture, int level, int bufSize, void* pixels);

			public delegate void GetCompressedTextureSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int bufSize, void* pixels);

			public delegate uint GetDebugMessageLog(uint count, int bufSize, uint* sources, uint* types, uint* ids, uint* severities, int* lengths, StringBuilder messageLog);

			public delegate void GetDoublei_v(uint target, uint index, double* data);

			public delegate void GetDoublev(uint pname, double* data);

			public delegate uint GetError();

			public delegate void GetFloati_v(uint target, uint index, float* data);

			public delegate void GetFloatv(uint pname, float* data);

			public delegate int GetFragDataIndex(uint program, string name);

			public delegate int GetFragDataLocation(uint program, string name);

			public delegate void GetFramebufferAttachmentParameteriv(uint target, uint attachment, uint pname, int* @params);

			public delegate void GetFramebufferParameteriv(uint target, uint pname, int* @params);

			public delegate uint GetGraphicsResetStatus();

			public delegate void GetInteger64i_v(uint target, uint index, long* data);

			public delegate void GetInteger64v(uint pname, long* data);

			public delegate void GetIntegeri_v(uint target, uint index, int* data);

			public delegate void GetIntegerv(uint pname, int* data);

			public delegate void GetInternalformati64v(uint target, uint internalformat, uint pname, int bufSize, long* @params);

			public delegate void GetInternalformativ(uint target, uint internalformat, uint pname, int bufSize, int* @params);

			public delegate void GetMultisamplefv(uint pname, uint index, float* val);

			public delegate void GetNamedBufferParameteri64v(uint buffer, uint pname, long* @params);

			public delegate void GetNamedBufferParameteriv(uint buffer, uint pname, int* @params);

			public delegate void GetNamedBufferPointerv(uint buffer, uint pname, void** @params);

			public delegate void GetNamedBufferSubData(uint buffer, int offset, int size, void* data);

			public delegate void GetNamedFramebufferAttachmentParameteriv(uint framebuffer, uint attachment, uint pname, int* @params);

			public delegate void GetNamedFramebufferParameteriv(uint framebuffer, uint pname, int* param);

			public delegate void GetNamedRenderbufferParameteriv(uint renderbuffer, uint pname, int* @params);

			public delegate void GetnCompressedTexImage(uint target, int lod, int bufSize, void* pixels);

			public delegate void GetnTexImage(uint target, int level, uint format, uint type, int bufSize, void* pixels);

			public delegate void GetnUniformdv(uint program, int location, int bufSize, double* @params);

			public delegate void GetnUniformfv(uint program, int location, int bufSize, float* @params);

			public delegate void GetnUniformiv(uint program, int location, int bufSize, int* @params);

			public delegate void GetnUniformuiv(uint program, int location, int bufSize, uint* @params);

			public delegate void GetObjectLabel(uint identifier, uint name, int bufSize, int* length, StringBuilder label);

			public delegate void GetObjectPtrLabel(void* ptr, int bufSize, int* length, StringBuilder label);

			public delegate void GetPointerv(uint pname, void** @params);

			public delegate void GetProgramBinary(uint program, int bufSize, int* length, uint* binaryFormat, void* binary);

			public delegate void GetProgramInfoLog(uint program, int bufSize, int* length, StringBuilder infoLog);

			public delegate void GetProgramInterfaceiv(uint program, uint programInterface, uint pname, int* @params);

			public delegate void GetProgramiv(uint program, uint pname, int* @params);

			public delegate void GetProgramPipelineInfoLog(uint pipeline, int bufSize, int* length, StringBuilder infoLog);

			public delegate void GetProgramPipelineiv(uint pipeline, uint pname, int* @params);

			public delegate uint GetProgramResourceIndex(uint program, uint programInterface, string name);

			public delegate void GetProgramResourceiv(uint program, uint programInterface, uint index, int propCount, uint* props, int bufSize, int* length, int* @params);

			public delegate int GetProgramResourceLocation(uint program, uint programInterface, string name);

			public delegate int GetProgramResourceLocationIndex(uint program, uint programInterface, string name);

			public delegate void GetProgramResourceName(uint program, uint programInterface, uint index, int bufSize, int* length, StringBuilder name);

			public delegate void GetProgramStageiv(uint program, uint shadertype, uint pname, int* values);

			public delegate void GetQueryBufferObjecti64v(uint id, uint buffer, uint pname, int offset);

			public delegate void GetQueryBufferObjectiv(uint id, uint buffer, uint pname, int offset);

			public delegate void GetQueryBufferObjectui64v(uint id, uint buffer, uint pname, int offset);

			public delegate void GetQueryBufferObjectuiv(uint id, uint buffer, uint pname, int offset);

			public delegate void GetQueryIndexediv(uint target, uint index, uint pname, int* @params);

			public delegate void GetQueryiv(uint target, uint pname, int* @params);

			public delegate void GetQueryObjecti64v(uint id, uint pname, long* @params);

			public delegate void GetQueryObjectiv(uint id, uint pname, int* @params);

			public delegate void GetQueryObjectui64v(uint id, uint pname, ulong* @params);

			public delegate void GetQueryObjectuiv(uint id, uint pname, uint* @params);

			public delegate void GetRenderbufferParameteriv(uint target, uint pname, int* @params);

			public delegate void GetSamplerParameterfv(uint sampler, uint pname, float* @params);

			public delegate void GetSamplerParameterIiv(uint sampler, uint pname, int* @params);

			public delegate void GetSamplerParameterIuiv(uint sampler, uint pname, uint* @params);

			public delegate void GetSamplerParameteriv(uint sampler, uint pname, int* @params);

			public delegate void GetShaderInfoLog(uint shader, int bufSize, int* length, StringBuilder infoLog);

			public delegate void GetShaderiv(uint shader, uint pname, int* @params);

			public delegate void GetShaderPrecisionFormat(uint shadertype, uint precisiontype, int* range, int* precision);

			public delegate void GetShaderSource(uint shader, int bufSize, int* length, StringBuilder source);

			public delegate IntPtr GetString(uint name);

			public delegate IntPtr GetStringi(uint name, uint index);

			public delegate uint GetSubroutineIndex(uint program, uint shadertype, string name);

			public delegate int GetSubroutineUniformLocation(uint program, uint shadertype, string name);

			public delegate void GetSynciv(IntPtr sync, uint pname, int bufSize, int* length, int* values);

			public delegate void GetTexImage(uint target, int level, uint format, uint type, void* pixels);

			public delegate void GetTexLevelParameterfv(uint target, int level, uint pname, float* @params);

			public delegate void GetTexLevelParameteriv(uint target, int level, uint pname, int* @params);

			public delegate void GetTexParameterfv(uint target, uint pname, float* @params);

			public delegate void GetTexParameterIiv(uint target, uint pname, int* @params);

			public delegate void GetTexParameterIuiv(uint target, uint pname, uint* @params);

			public delegate void GetTexParameteriv(uint target, uint pname, int* @params);

			public delegate void GetTextureImage(uint texture, int level, uint format, uint type, int bufSize, void* pixels);

			public delegate void GetTextureLevelParameterfv(uint texture, int level, uint pname, float* @params);

			public delegate void GetTextureLevelParameteriv(uint texture, int level, uint pname, int* @params);

			public delegate void GetTextureParameterfv(uint texture, uint pname, float* @params);

			public delegate void GetTextureParameterIiv(uint texture, uint pname, int* @params);

			public delegate void GetTextureParameterIuiv(uint texture, uint pname, uint* @params);

			public delegate void GetTextureParameteriv(uint texture, uint pname, int* @params);

			public delegate void GetTextureSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, int bufSize, void* pixels);

			public delegate void GetTransformFeedbacki_v(uint xfb, uint pname, uint index, int* param);

			public delegate void GetTransformFeedbacki64_v(uint xfb, uint pname, uint index, long* param);

			public delegate void GetTransformFeedbackiv(uint xfb, uint pname, int* param);

			public delegate void GetTransformFeedbackVarying(uint program, uint index, int bufSize, int* length, int* size, uint* type, StringBuilder name);

			public delegate uint GetUniformBlockIndex(uint program, string uniformBlockName);

			public delegate void GetUniformdv(uint program, int location, double* @params);

			public delegate void GetUniformfv(uint program, int location, float* @params);

			public delegate void GetUniformIndices(uint program, int uniformCount, string[] uniformNames, uint* uniformIndices);

			public delegate void GetUniformiv(uint program, int location, int* @params);

			public delegate int GetUniformLocation(uint program, string name);

			public delegate void GetUniformSubroutineuiv(uint shadertype, int location, uint* @params);

			public delegate void GetUniformuiv(uint program, int location, uint* @params);

			public delegate void GetVertexArrayIndexed64iv(uint vaobj, uint index, uint pname, long* param);

			public delegate void GetVertexArrayIndexediv(uint vaobj, uint index, uint pname, int* param);

			public delegate void GetVertexArrayiv(uint vaobj, uint pname, int* param);

			public delegate void GetVertexAttribdv(uint index, uint pname, double* @params);

			public delegate void GetVertexAttribfv(uint index, uint pname, float* @params);

			public delegate void GetVertexAttribIiv(uint index, uint pname, int* @params);

			public delegate void GetVertexAttribIuiv(uint index, uint pname, uint* @params);

			public delegate void GetVertexAttribiv(uint index, uint pname, int* @params);

			public delegate void GetVertexAttribLdv(uint index, uint pname, double* @params);

			public delegate void GetVertexAttribPointerv(uint index, uint pname, void** pointer);

			public delegate void Hint(uint target, uint mode);

			public delegate void InvalidateBufferData(uint buffer);

			public delegate void InvalidateBufferSubData(uint buffer, int offset, int length);

			public delegate void InvalidateFramebuffer(uint target, int numAttachments, uint* attachments);

			public delegate void InvalidateNamedFramebufferData(uint framebuffer, int numAttachments, uint* attachments);

			public delegate void InvalidateNamedFramebufferSubData(uint framebuffer, int numAttachments, uint* attachments, int x, int y, int width, int height);

			public delegate void InvalidateSubFramebuffer(uint target, int numAttachments, uint* attachments, int x, int y, int width, int height);

			public delegate void InvalidateTexImage(uint texture, int level);

			public delegate void InvalidateTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth);

			public delegate bool IsBuffer(uint buffer);

			public delegate bool IsEnabled(uint cap);

			public delegate bool IsEnabledi(uint target, uint index);

			public delegate bool IsFramebuffer(uint framebuffer);

			public delegate bool IsProgram(uint program);

			public delegate bool IsProgramPipeline(uint pipeline);

			public delegate bool IsQuery(uint id);

			public delegate bool IsRenderbuffer(uint renderbuffer);

			public delegate bool IsSampler(uint sampler);

			public delegate bool IsShader(uint shader);

			public delegate bool IsSync(IntPtr sync);

			public delegate bool IsTexture(uint texture);

			public delegate bool IsTransformFeedback(uint id);

			public delegate bool IsVertexArray(uint array);

			public delegate void LineWidth(float width);

			public delegate void LinkProgram(uint program);

			public delegate void LogicOp(uint opcode);

			public delegate IntPtr MapBuffer(uint target, uint access);

			public delegate IntPtr MapBufferRange(uint target, int offset, int length, uint access);

			public delegate IntPtr MapNamedBuffer(uint buffer, uint access);

			public delegate IntPtr MapNamedBufferRange(uint buffer, int offset, int length, uint access);

			public delegate void MemoryBarrier(uint barriers);

			public delegate void MemoryBarrierByRegion(uint barriers);

			public delegate void MinSampleShading(float value);

			public delegate void MultiDrawArrays(uint mode, int* first, int* count, int drawcount);

			public delegate void MultiDrawArraysIndirect(uint mode, void* indirect, int drawcount, int stride);

			//public delegate void MultiDrawArraysIndirectCount(uint mode, void* indirect, int drawcount, int maxdrawcount, int stride);

			public delegate void MultiDrawElements(uint mode, int* count, uint type, void** indices, int drawcount);

			public delegate void MultiDrawElementsBaseVertex(uint mode, int* count, uint type, void** indices, int drawcount, int* basevertex);

			public delegate void MultiDrawElementsIndirect(uint mode, uint type, void* indirect, int drawcount, int stride);

			//public delegate void MultiDrawElementsIndirectCount(uint mode, uint type, void* indirect, int drawcount, int maxdrawcount, int stride);

			public delegate void NamedBufferData(uint buffer, int size, void* data, uint usage);

			public delegate void NamedBufferStorage(uint buffer, int size, void* data, uint flags);

			public delegate void NamedBufferSubData(uint buffer, int offset, int size, void* data);

			public delegate void NamedFramebufferDrawBuffer(uint framebuffer, uint buf);

			public delegate void NamedFramebufferDrawBuffers(uint framebuffer, int n, uint* bufs);

			public delegate void NamedFramebufferParameteri(uint framebuffer, uint pname, int param);

			public delegate void NamedFramebufferReadBuffer(uint framebuffer, uint src);

			public delegate void NamedFramebufferRenderbuffer(uint framebuffer, uint attachment, uint renderbuffertarget, uint renderbuffer);

			public delegate void NamedFramebufferTexture(uint framebuffer, uint attachment, uint texture, int level);

			public delegate void NamedFramebufferTextureLayer(uint framebuffer, uint attachment, uint texture, int level, int layer);

			public delegate void NamedRenderbufferStorage(uint renderbuffer, uint internalformat, int width, int height);

			public delegate void NamedRenderbufferStorageMultisample(uint renderbuffer, int samples, uint internalformat, int width, int height);

			public delegate void ObjectLabel(uint identifier, uint name, int length, string label);

			public delegate void ObjectPtrLabel(void* ptr, int length, string label);

			public delegate void PatchParameterfv(uint pname, float* values);

			public delegate void PatchParameteri(uint pname, int value);

			public delegate void PauseTransformFeedback();

			public delegate void PixelStoref(uint pname, float param);

			public delegate void PixelStorei(uint pname, int param);

			public delegate void PointParameterf(uint pname, float param);

			public delegate void PointParameterfv(uint pname, float* @params);

			public delegate void PointParameteri(uint pname, int param);

			public delegate void PointParameteriv(uint pname, int* @params);

			public delegate void PointSize(float size);

			public delegate void PolygonMode(uint face, uint mode);

			public delegate void PolygonOffset(float factor, float units);

			//public delegate void PolygonOffsetClamp(float factor, float units, float clamp);

			public delegate void PopDebugGroup();

			public delegate void PrimitiveRestartIndex(uint index);

			public delegate void ProgramBinary(uint program, uint binaryFormat, void* binary, int length);

			public delegate void ProgramParameteri(uint program, uint pname, int value);

			public delegate void ProgramUniform1d(uint program, int location, double v0);

			public delegate void ProgramUniform1dv(uint program, int location, int count, double* value);

			public delegate void ProgramUniform1f(uint program, int location, float v0);

			public delegate void ProgramUniform1fv(uint program, int location, int count, float* value);

			public delegate void ProgramUniform1i(uint program, int location, int v0);

			public delegate void ProgramUniform1iv(uint program, int location, int count, int* value);

			public delegate void ProgramUniform1ui(uint program, int location, uint v0);

			public delegate void ProgramUniform1uiv(uint program, int location, int count, uint* value);

			public delegate void ProgramUniform2d(uint program, int location, double v0, double v1);

			public delegate void ProgramUniform2dv(uint program, int location, int count, double* value);

			public delegate void ProgramUniform2f(uint program, int location, float v0, float v1);

			public delegate void ProgramUniform2fv(uint program, int location, int count, float* value);

			public delegate void ProgramUniform2i(uint program, int location, int v0, int v1);

			public delegate void ProgramUniform2iv(uint program, int location, int count, int* value);

			public delegate void ProgramUniform2ui(uint program, int location, uint v0, uint v1);

			public delegate void ProgramUniform2uiv(uint program, int location, int count, uint* value);

			public delegate void ProgramUniform3d(uint program, int location, double v0, double v1, double v2);

			public delegate void ProgramUniform3dv(uint program, int location, int count, double* value);

			public delegate void ProgramUniform3f(uint program, int location, float v0, float v1, float v2);

			public delegate void ProgramUniform3fv(uint program, int location, int count, float* value);

			public delegate void ProgramUniform3i(uint program, int location, int v0, int v1, int v2);

			public delegate void ProgramUniform3iv(uint program, int location, int count, int* value);

			public delegate void ProgramUniform3ui(uint program, int location, uint v0, uint v1, uint v2);

			public delegate void ProgramUniform3uiv(uint program, int location, int count, uint* value);

			public delegate void ProgramUniform4d(uint program, int location, double v0, double v1, double v2, double v3);

			public delegate void ProgramUniform4dv(uint program, int location, int count, double* value);

			public delegate void ProgramUniform4f(uint program, int location, float v0, float v1, float v2, float v3);

			public delegate void ProgramUniform4fv(uint program, int location, int count, float* value);

			public delegate void ProgramUniform4i(uint program, int location, int v0, int v1, int v2, int v3);

			public delegate void ProgramUniform4iv(uint program, int location, int count, int* value);

			public delegate void ProgramUniform4ui(uint program, int location, uint v0, uint v1, uint v2, uint v3);

			public delegate void ProgramUniform4uiv(uint program, int location, int count, uint* value);

			public delegate void ProgramUniformMatrix2dv(uint program, int location, int count, bool transpose, double* value);

			public delegate void ProgramUniformMatrix2fv(uint program, int location, int count, bool transpose, float* value);

			public delegate void ProgramUniformMatrix2x3dv(uint program, int location, int count, bool transpose, double* value);

			public delegate void ProgramUniformMatrix2x3fv(uint program, int location, int count, bool transpose, float* value);

			public delegate void ProgramUniformMatrix2x4dv(uint program, int location, int count, bool transpose, double* value);

			public delegate void ProgramUniformMatrix2x4fv(uint program, int location, int count, bool transpose, float* value);

			public delegate void ProgramUniformMatrix3dv(uint program, int location, int count, bool transpose, double* value);

			public delegate void ProgramUniformMatrix3fv(uint program, int location, int count, bool transpose, float* value);

			public delegate void ProgramUniformMatrix3x2dv(uint program, int location, int count, bool transpose, double* value);

			public delegate void ProgramUniformMatrix3x2fv(uint program, int location, int count, bool transpose, float* value);

			public delegate void ProgramUniformMatrix3x4dv(uint program, int location, int count, bool transpose, double* value);

			public delegate void ProgramUniformMatrix3x4fv(uint program, int location, int count, bool transpose, float* value);

			public delegate void ProgramUniformMatrix4dv(uint program, int location, int count, bool transpose, double* value);

			public delegate void ProgramUniformMatrix4fv(uint program, int location, int count, bool transpose, float* value);

			public delegate void ProgramUniformMatrix4x2dv(uint program, int location, int count, bool transpose, double* value);

			public delegate void ProgramUniformMatrix4x2fv(uint program, int location, int count, bool transpose, float* value);

			public delegate void ProgramUniformMatrix4x3dv(uint program, int location, int count, bool transpose, double* value);

			public delegate void ProgramUniformMatrix4x3fv(uint program, int location, int count, bool transpose, float* value);

			public delegate void ProvokingVertex(uint mode);

			public delegate void PushDebugGroup(uint source, uint id, int length, string message);

			public delegate void QueryCounter(uint id, uint target);

			public delegate void ReadBuffer(uint src);

			public delegate void ReadnPixels(int x, int y, int width, int height, uint format, uint type, int bufSize, void* data);

			public delegate void ReadPixels(int x, int y, int width, int height, uint format, uint type, void* pixels);

			public delegate void ReleaseShaderCompiler();

			public delegate void RenderbufferStorage(uint target, uint internalformat, int width, int height);

			public delegate void RenderbufferStorageMultisample(uint target, int samples, uint internalformat, int width, int height);

			public delegate void ResumeTransformFeedback();

			public delegate void SampleCoverage(float value, bool invert);

			public delegate void SampleMaski(uint maskNumber, uint mask);

			public delegate void SamplerParameterf(uint sampler, uint pname, float param);

			public delegate void SamplerParameterfv(uint sampler, uint pname, float* param);

			public delegate void SamplerParameteri(uint sampler, uint pname, int param);

			public delegate void SamplerParameterIiv(uint sampler, uint pname, int* param);

			public delegate void SamplerParameterIuiv(uint sampler, uint pname, uint* param);

			public delegate void SamplerParameteriv(uint sampler, uint pname, int* param);

			public delegate void Scissor(int x, int y, int width, int height);

			public delegate void ScissorArrayv(uint first, int count, int* v);

			public delegate void ScissorIndexed(uint index, int left, int bottom, int width, int height);

			public delegate void ScissorIndexedv(uint index, int* v);

			public delegate void ShaderBinary(int count, uint* shaders, uint binaryformat, void* binary, int length);

			public delegate void ShaderSource(uint shader, int count, string[] @string, int* length);

			public delegate void ShaderStorageBlockBinding(uint program, uint storageBlockIndex, uint storageBlockBinding);

			//public delegate void SpecializeShader(uint shader, string pEntryPoint, uint numSpecializationConstants, uint* pConstantIndex, uint* pConstantValue);

			public delegate void StencilFunc(uint func, int @ref, uint mask);

			public delegate void StencilFuncSeparate(uint face, uint func, int @ref, uint mask);

			public delegate void StencilMask(uint mask);

			public delegate void StencilMaskSeparate(uint face, uint mask);

			public delegate void StencilOp(uint fail, uint zfail, uint zpass);

			public delegate void StencilOpSeparate(uint face, uint sfail, uint dpfail, uint dppass);

			public delegate void TexBuffer(uint target, uint internalformat, uint buffer);

			public delegate void TexBufferRange(uint target, uint internalformat, uint buffer, int offset, int size);

			public delegate void TexImage1D(uint target, int level, int internalformat, int width, int border, uint format, uint type, void* pixels);

			public delegate void TexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, void* pixels);

			public delegate void TexImage2DMultisample(uint target, int samples, uint internalformat, int width, int height, bool fixedsamplelocations);

			public delegate void TexImage3D(uint target, int level, int internalformat, int width, int height, int depth, int border, uint format, uint type, void* pixels);

			public delegate void TexImage3DMultisample(uint target, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations);

			public delegate void TexParameterf(uint target, uint pname, float param);

			public delegate void TexParameterfv(uint target, uint pname, float* @params);

			public delegate void TexParameteri(uint target, uint pname, int param);

			public delegate void TexParameterIiv(uint target, uint pname, int* @params);

			public delegate void TexParameterIuiv(uint target, uint pname, uint* @params);

			public delegate void TexParameteriv(uint target, uint pname, int* @params);

			public delegate void TexStorage1D(uint target, int levels, uint internalformat, int width);

			public delegate void TexStorage2D(uint target, int levels, uint internalformat, int width, int height);

			public delegate void TexStorage2DMultisample(uint target, int samples, uint internalformat, int width, int height, bool fixedsamplelocations);

			public delegate void TexStorage3D(uint target, int levels, uint internalformat, int width, int height, int depth);

			public delegate void TexStorage3DMultisample(uint target, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations);

			public delegate void TexSubImage1D(uint target, int level, int xoffset, int width, uint format, uint type, void* pixels);

			public delegate void TexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, void* pixels);

			public delegate void TexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, void* pixels);

			public delegate void TextureBarrier();

			public delegate void TextureBuffer(uint texture, uint internalformat, uint buffer);

			public delegate void TextureBufferRange(uint texture, uint internalformat, uint buffer, int offset, int size);

			public delegate void TextureParameterf(uint texture, uint pname, float param);

			public delegate void TextureParameterfv(uint texture, uint pname, float* param);

			public delegate void TextureParameteri(uint texture, uint pname, int param);

			public delegate void TextureParameterIiv(uint texture, uint pname, int* @params);

			public delegate void TextureParameterIuiv(uint texture, uint pname, uint* @params);

			public delegate void TextureParameteriv(uint texture, uint pname, int* param);

			public delegate void TextureStorage1D(uint texture, int levels, uint internalformat, int width);

			public delegate void TextureStorage2D(uint texture, int levels, uint internalformat, int width, int height);

			public delegate void TextureStorage2DMultisample(uint texture, int samples, uint internalformat, int width, int height, bool fixedsamplelocations);

			public delegate void TextureStorage3D(uint texture, int levels, uint internalformat, int width, int height, int depth);

			public delegate void TextureStorage3DMultisample(uint texture, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations);

			public delegate void TextureSubImage1D(uint texture, int level, int xoffset, int width, uint format, uint type, void* pixels);

			public delegate void TextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, void* pixels);

			public delegate void TextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, void* pixels);

			public delegate void TextureView(uint texture, uint target, uint origtexture, uint internalformat, uint minlevel, uint numlevels, uint minlayer, uint numlayers);

			public delegate void TransformFeedbackBufferBase(uint xfb, uint index, uint buffer);

			public delegate void TransformFeedbackBufferRange(uint xfb, uint index, uint buffer, int offset, int size);

			public delegate void TransformFeedbackVaryings(uint program, int count, string[] varyings, uint bufferMode);

			public delegate void Uniform1d(int location, double x);

			public delegate void Uniform1dv(int location, int count, double* value);

			public delegate void Uniform1f(int location, float v0);

			public delegate void Uniform1fv(int location, int count, float* value);

			public delegate void Uniform1i(int location, int v0);

			public delegate void Uniform1iv(int location, int count, int* value);

			public delegate void Uniform1ui(int location, uint v0);

			public delegate void Uniform1uiv(int location, int count, uint* value);

			public delegate void Uniform2d(int location, double x, double y);

			public delegate void Uniform2dv(int location, int count, double* value);

			public delegate void Uniform2f(int location, float v0, float v1);

			public delegate void Uniform2fv(int location, int count, float* value);

			public delegate void Uniform2i(int location, int v0, int v1);

			public delegate void Uniform2iv(int location, int count, int* value);

			public delegate void Uniform2ui(int location, uint v0, uint v1);

			public delegate void Uniform2uiv(int location, int count, uint* value);

			public delegate void Uniform3d(int location, double x, double y, double z);

			public delegate void Uniform3dv(int location, int count, double* value);

			public delegate void Uniform3f(int location, float v0, float v1, float v2);

			public delegate void Uniform3fv(int location, int count, float* value);

			public delegate void Uniform3i(int location, int v0, int v1, int v2);

			public delegate void Uniform3iv(int location, int count, int* value);

			public delegate void Uniform3ui(int location, uint v0, uint v1, uint v2);

			public delegate void Uniform3uiv(int location, int count, uint* value);

			public delegate void Uniform4d(int location, double x, double y, double z, double w);

			public delegate void Uniform4dv(int location, int count, double* value);

			public delegate void Uniform4f(int location, float v0, float v1, float v2, float v3);

			public delegate void Uniform4fv(int location, int count, float* value);

			public delegate void Uniform4i(int location, int v0, int v1, int v2, int v3);

			public delegate void Uniform4iv(int location, int count, int* value);

			public delegate void Uniform4ui(int location, uint v0, uint v1, uint v2, uint v3);

			public delegate void Uniform4uiv(int location, int count, uint* value);

			public delegate void UniformBlockBinding(uint program, uint uniformBlockIndex, uint uniformBlockBinding);

			public delegate void UniformMatrix2dv(int location, int count, bool transpose, double* value);

			public delegate void UniformMatrix2fv(int location, int count, bool transpose, float* value);

			public delegate void UniformMatrix2x3dv(int location, int count, bool transpose, double* value);

			public delegate void UniformMatrix2x3fv(int location, int count, bool transpose, float* value);

			public delegate void UniformMatrix2x4dv(int location, int count, bool transpose, double* value);

			public delegate void UniformMatrix2x4fv(int location, int count, bool transpose, float* value);

			public delegate void UniformMatrix3dv(int location, int count, bool transpose, double* value);

			public delegate void UniformMatrix3fv(int location, int count, bool transpose, float* value);

			public delegate void UniformMatrix3x2dv(int location, int count, bool transpose, double* value);

			public delegate void UniformMatrix3x2fv(int location, int count, bool transpose, float* value);

			public delegate void UniformMatrix3x4dv(int location, int count, bool transpose, double* value);

			public delegate void UniformMatrix3x4fv(int location, int count, bool transpose, float* value);

			public delegate void UniformMatrix4dv(int location, int count, bool transpose, double* value);

			public delegate void UniformMatrix4fv(int location, int count, bool transpose, float* value);

			public delegate void UniformMatrix4x2dv(int location, int count, bool transpose, double* value);

			public delegate void UniformMatrix4x2fv(int location, int count, bool transpose, float* value);

			public delegate void UniformMatrix4x3dv(int location, int count, bool transpose, double* value);

			public delegate void UniformMatrix4x3fv(int location, int count, bool transpose, float* value);

			public delegate void UniformSubroutinesuiv(uint shadertype, int count, uint* indices);

			public delegate bool UnmapBuffer(uint target);

			public delegate bool UnmapNamedBuffer(uint buffer);

			public delegate void UseProgram(uint program);

			public delegate void UseProgramStages(uint pipeline, uint stages, uint program);

			public delegate void ValidateProgram(uint program);

			public delegate void ValidateProgramPipeline(uint pipeline);

			public delegate void VertexArrayAttribBinding(uint vaobj, uint attribindex, uint bindingindex);

			public delegate void VertexArrayAttribFormat(uint vaobj, uint attribindex, int size, uint type, bool normalized, uint relativeoffset);

			public delegate void VertexArrayAttribIFormat(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset);

			public delegate void VertexArrayAttribLFormat(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset);

			public delegate void VertexArrayBindingDivisor(uint vaobj, uint bindingindex, uint divisor);

			public delegate void VertexArrayElementBuffer(uint vaobj, uint buffer);

			public delegate void VertexArrayVertexBuffer(uint vaobj, uint bindingindex, uint buffer, int offset, int stride);

			public delegate void VertexArrayVertexBuffers(uint vaobj, uint first, int count, uint* buffers, int* offsets, int* strides);

			public delegate void VertexAttrib1d(uint index, double x);

			public delegate void VertexAttrib1dv(uint index, double* v);

			public delegate void VertexAttrib1f(uint index, float x);

			public delegate void VertexAttrib1fv(uint index, float* v);

			public delegate void VertexAttrib1s(uint index, short x);

			public delegate void VertexAttrib1sv(uint index, short* v);

			public delegate void VertexAttrib2d(uint index, double x, double y);

			public delegate void VertexAttrib2dv(uint index, double* v);

			public delegate void VertexAttrib2f(uint index, float x, float y);

			public delegate void VertexAttrib2fv(uint index, float* v);

			public delegate void VertexAttrib2s(uint index, short x, short y);

			public delegate void VertexAttrib2sv(uint index, short* v);

			public delegate void VertexAttrib3d(uint index, double x, double y, double z);

			public delegate void VertexAttrib3dv(uint index, double* v);

			public delegate void VertexAttrib3f(uint index, float x, float y, float z);

			public delegate void VertexAttrib3fv(uint index, float* v);

			public delegate void VertexAttrib3s(uint index, short x, short y, short z);

			public delegate void VertexAttrib3sv(uint index, short* v);

			public delegate void VertexAttrib4bv(uint index, sbyte* v);

			public delegate void VertexAttrib4d(uint index, double x, double y, double z, double w);

			public delegate void VertexAttrib4dv(uint index, double* v);

			public delegate void VertexAttrib4f(uint index, float x, float y, float z, float w);

			public delegate void VertexAttrib4fv(uint index, float* v);

			public delegate void VertexAttrib4iv(uint index, int* v);

			public delegate void VertexAttrib4Nbv(uint index, sbyte* v);

			public delegate void VertexAttrib4Niv(uint index, int* v);

			public delegate void VertexAttrib4Nsv(uint index, short* v);

			public delegate void VertexAttrib4Nub(uint index, byte x, byte y, byte z, byte w);

			public delegate void VertexAttrib4Nubv(uint index, byte* v);

			public delegate void VertexAttrib4Nuiv(uint index, uint* v);

			public delegate void VertexAttrib4Nusv(uint index, ushort* v);

			public delegate void VertexAttrib4s(uint index, short x, short y, short z, short w);

			public delegate void VertexAttrib4sv(uint index, short* v);

			public delegate void VertexAttrib4ubv(uint index, byte* v);

			public delegate void VertexAttrib4uiv(uint index, uint* v);

			public delegate void VertexAttrib4usv(uint index, ushort* v);

			public delegate void VertexAttribBinding(uint attribindex, uint bindingindex);

			public delegate void VertexAttribDivisor(uint index, uint divisor);

			public delegate void VertexAttribFormat(uint attribindex, int size, uint type, bool normalized, uint relativeoffset);

			public delegate void VertexAttribI1i(uint index, int x);

			public delegate void VertexAttribI1iv(uint index, int* v);

			public delegate void VertexAttribI1ui(uint index, uint x);

			public delegate void VertexAttribI1uiv(uint index, uint* v);

			public delegate void VertexAttribI2i(uint index, int x, int y);

			public delegate void VertexAttribI2iv(uint index, int* v);

			public delegate void VertexAttribI2ui(uint index, uint x, uint y);

			public delegate void VertexAttribI2uiv(uint index, uint* v);

			public delegate void VertexAttribI3i(uint index, int x, int y, int z);

			public delegate void VertexAttribI3iv(uint index, int* v);

			public delegate void VertexAttribI3ui(uint index, uint x, uint y, uint z);

			public delegate void VertexAttribI3uiv(uint index, uint* v);

			public delegate void VertexAttribI4bv(uint index, sbyte* v);

			public delegate void VertexAttribI4i(uint index, int x, int y, int z, int w);

			public delegate void VertexAttribI4iv(uint index, int* v);

			public delegate void VertexAttribI4sv(uint index, short* v);

			public delegate void VertexAttribI4ubv(uint index, byte* v);

			public delegate void VertexAttribI4ui(uint index, uint x, uint y, uint z, uint w);

			public delegate void VertexAttribI4uiv(uint index, uint* v);

			public delegate void VertexAttribI4usv(uint index, ushort* v);

			public delegate void VertexAttribIFormat(uint attribindex, int size, uint type, uint relativeoffset);

			public delegate void VertexAttribIPointer(uint index, int size, uint type, int stride, void* pointer);

			public delegate void VertexAttribL1d(uint index, double x);

			public delegate void VertexAttribL1dv(uint index, double* v);

			public delegate void VertexAttribL2d(uint index, double x, double y);

			public delegate void VertexAttribL2dv(uint index, double* v);

			public delegate void VertexAttribL3d(uint index, double x, double y, double z);

			public delegate void VertexAttribL3dv(uint index, double* v);

			public delegate void VertexAttribL4d(uint index, double x, double y, double z, double w);

			public delegate void VertexAttribL4dv(uint index, double* v);

			public delegate void VertexAttribLFormat(uint attribindex, int size, uint type, uint relativeoffset);

			public delegate void VertexAttribLPointer(uint index, int size, uint type, int stride, void* pointer);

			public delegate void VertexAttribP1ui(uint index, uint type, bool normalized, uint value);

			public delegate void VertexAttribP1uiv(uint index, uint type, bool normalized, uint* value);

			public delegate void VertexAttribP2ui(uint index, uint type, bool normalized, uint value);

			public delegate void VertexAttribP2uiv(uint index, uint type, bool normalized, uint* value);

			public delegate void VertexAttribP3ui(uint index, uint type, bool normalized, uint value);

			public delegate void VertexAttribP3uiv(uint index, uint type, bool normalized, uint* value);

			public delegate void VertexAttribP4ui(uint index, uint type, bool normalized, uint value);

			public delegate void VertexAttribP4uiv(uint index, uint type, bool normalized, uint* value);

			public delegate void VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, void* pointer);

			public delegate void VertexBindingDivisor(uint bindingindex, uint divisor);

			public delegate void Viewport(int x, int y, int width, int height);

			public delegate void ViewportArrayv(uint first, int count, float* v);

			public delegate void ViewportIndexedf(uint index, float x, float y, float w, float h);

			public delegate void ViewportIndexedfv(uint index, float* v);

			public delegate void WaitSync(IntPtr sync, uint flags, ulong timeout);

		}

		public static class Functions
		{
			public static Delegates.ActiveShaderProgram ActiveShaderProgram { get; set; }

			public static Delegates.ActiveTexture ActiveTexture { get; set; }

			public static Delegates.AttachShader AttachShader { get; set; }

			public static Delegates.BeginConditionalRender BeginConditionalRender { get; set; }

			public static Delegates.BeginQuery BeginQuery { get; set; }

			public static Delegates.BeginQueryIndexed BeginQueryIndexed { get; set; }

			public static Delegates.BeginTransformFeedback BeginTransformFeedback { get; set; }

			public static Delegates.BindAttribLocation BindAttribLocation { get; set; }

			public static Delegates.BindBuffer BindBuffer { get; set; }

			public static Delegates.BindBufferBase BindBufferBase { get; set; }

			public static Delegates.BindBufferRange BindBufferRange { get; set; }

			public static Delegates.BindBuffersBase BindBuffersBase { get; set; }

			public static Delegates.BindBuffersRange BindBuffersRange { get; set; }

			public static Delegates.BindFragDataLocation BindFragDataLocation { get; set; }

			public static Delegates.BindFragDataLocationIndexed BindFragDataLocationIndexed { get; set; }

			public static Delegates.BindFramebuffer BindFramebuffer { get; set; }

			public static Delegates.BindImageTexture BindImageTexture { get; set; }

			public static Delegates.BindProgramPipeline BindProgramPipeline { get; set; }

			public static Delegates.BindRenderbuffer BindRenderbuffer { get; set; }

			public static Delegates.BindSampler BindSampler { get; set; }

			public static Delegates.BindSamplers BindSamplers { get; set; }

			public static Delegates.BindTexture BindTexture { get; set; }

			public static Delegates.BindTextureUnit BindTextureUnit { get; set; }

			public static Delegates.BindTransformFeedback BindTransformFeedback { get; set; }

			public static Delegates.BindVertexArray BindVertexArray { get; set; }

			public static Delegates.BindVertexBuffer BindVertexBuffer { get; set; }

			public static Delegates.BindVertexBuffers BindVertexBuffers { get; set; }

			public static Delegates.BlendColour BlendColour { get; set; }

			public static Delegates.BlendEquation BlendEquation { get; set; }

			public static Delegates.BlendEquationi BlendEquationi { get; set; }

			public static Delegates.BlendEquationSeparate BlendEquationSeparate { get; set; }

			public static Delegates.BlendEquationSeparatei BlendEquationSeparatei { get; set; }

			public static Delegates.BlendFunc BlendFunc { get; set; }

			public static Delegates.BlendFunci BlendFunci { get; set; }

			public static Delegates.BlendFuncSeparate BlendFuncSeparate { get; set; }

			public static Delegates.BlendFuncSeparatei BlendFuncSeparatei { get; set; }

			public static Delegates.BlitFramebuffer BlitFramebuffer { get; set; }

			public static Delegates.BlitNamedFramebuffer BlitNamedFramebuffer { get; set; }

			public static Delegates.BufferData BufferData { get; set; }

			public static Delegates.BufferStorage BufferStorage { get; set; }

			public static Delegates.BufferSubData BufferSubData { get; set; }

			public static Delegates.CheckFramebufferStatus CheckFramebufferStatus { get; set; }

			public static Delegates.CheckNamedFramebufferStatus CheckNamedFramebufferStatus { get; set; }

			public static Delegates.ClampColour ClampColour { get; set; }

			public static Delegates.Clear Clear { get; set; }

			public static Delegates.ClearBufferData ClearBufferData { get; set; }

			public static Delegates.ClearBufferfi ClearBufferfi { get; set; }

			public static Delegates.ClearBufferfv ClearBufferfv { get; set; }

			public static Delegates.ClearBufferiv ClearBufferiv { get; set; }

			public static Delegates.ClearBufferSubData ClearBufferSubData { get; set; }

			public static Delegates.ClearBufferuiv ClearBufferuiv { get; set; }

			public static Delegates.ClearColour ClearColour { get; set; }

			public static Delegates.ClearDepth ClearDepth { get; set; }

			public static Delegates.ClearDepthf ClearDepthf { get; set; }

			public static Delegates.ClearNamedBufferData ClearNamedBufferData { get; set; }

			public static Delegates.ClearNamedBufferSubData ClearNamedBufferSubData { get; set; }

			public static Delegates.ClearNamedFramebufferfi ClearNamedFramebufferfi { get; set; }

			public static Delegates.ClearNamedFramebufferfv ClearNamedFramebufferfv { get; set; }

			public static Delegates.ClearNamedFramebufferiv ClearNamedFramebufferiv { get; set; }

			public static Delegates.ClearNamedFramebufferuiv ClearNamedFramebufferuiv { get; set; }

			public static Delegates.ClearStencil ClearStencil { get; set; }

			public static Delegates.ClearTexImage ClearTexImage { get; set; }

			public static Delegates.ClearTexSubImage ClearTexSubImage { get; set; }

			public static Delegates.ClientWaitSync ClientWaitSync { get; set; }

			public static Delegates.ClipControl ClipControl { get; set; }

			public static Delegates.ColourMask ColourMask { get; set; }

			public static Delegates.ColourMaski ColourMaski { get; set; }

			public static Delegates.CompileShader CompileShader { get; set; }

			public static Delegates.CompressedTexImage1D CompressedTexImage1D { get; set; }

			public static Delegates.CompressedTexImage2D CompressedTexImage2D { get; set; }

			public static Delegates.CompressedTexImage3D CompressedTexImage3D { get; set; }

			public static Delegates.CompressedTexSubImage1D CompressedTexSubImage1D { get; set; }

			public static Delegates.CompressedTexSubImage2D CompressedTexSubImage2D { get; set; }

			public static Delegates.CompressedTexSubImage3D CompressedTexSubImage3D { get; set; }

			public static Delegates.CompressedTextureSubImage1D CompressedTextureSubImage1D { get; set; }

			public static Delegates.CompressedTextureSubImage2D CompressedTextureSubImage2D { get; set; }

			public static Delegates.CompressedTextureSubImage3D CompressedTextureSubImage3D { get; set; }

			public static Delegates.CopyBufferSubData CopyBufferSubData { get; set; }

			public static Delegates.CopyImageSubData CopyImageSubData { get; set; }

			public static Delegates.CopyNamedBufferSubData CopyNamedBufferSubData { get; set; }

			public static Delegates.CopyTexImage1D CopyTexImage1D { get; set; }

			public static Delegates.CopyTexImage2D CopyTexImage2D { get; set; }

			public static Delegates.CopyTexSubImage1D CopyTexSubImage1D { get; set; }

			public static Delegates.CopyTexSubImage2D CopyTexSubImage2D { get; set; }

			public static Delegates.CopyTexSubImage3D CopyTexSubImage3D { get; set; }

			public static Delegates.CopyTextureSubImage1D CopyTextureSubImage1D { get; set; }

			public static Delegates.CopyTextureSubImage2D CopyTextureSubImage2D { get; set; }

			public static Delegates.CopyTextureSubImage3D CopyTextureSubImage3D { get; set; }

			public static Delegates.CreateBuffers CreateBuffers { get; set; }

			public static Delegates.CreateFramebuffers CreateFramebuffers { get; set; }

			public static Delegates.CreateProgram CreateProgram { get; set; }

			public static Delegates.CreateProgramPipelines CreateProgramPipelines { get; set; }

			public static Delegates.CreateQueries CreateQueries { get; set; }

			public static Delegates.CreateRenderbuffers CreateRenderbuffers { get; set; }

			public static Delegates.CreateSamplers CreateSamplers { get; set; }

			public static Delegates.CreateShader CreateShader { get; set; }

			public static Delegates.CreateShaderProgramv CreateShaderProgramv { get; set; }

			public static Delegates.CreateTextures CreateTextures { get; set; }

			public static Delegates.CreateTransformFeedbacks CreateTransformFeedbacks { get; set; }

			public static Delegates.CreateVertexArrays CreateVertexArrays { get; set; }

			public static Delegates.CullFace CullFace { get; set; }

			public static Delegates.DebugMessageCallback DebugMessageCallback { get; set; }

			public static Delegates.DebugMessageControl DebugMessageControl { get; set; }

			public static Delegates.DebugMessageInsert DebugMessageInsert { get; set; }

			public static Delegates.DeleteBuffers DeleteBuffers { get; set; }

			public static Delegates.DeleteFramebuffers DeleteFramebuffers { get; set; }

			public static Delegates.DeleteProgram DeleteProgram { get; set; }

			public static Delegates.DeleteProgramPipelines DeleteProgramPipelines { get; set; }

			public static Delegates.DeleteQueries DeleteQueries { get; set; }

			public static Delegates.DeleteRenderbuffers DeleteRenderbuffers { get; set; }

			public static Delegates.DeleteSamplers DeleteSamplers { get; set; }

			public static Delegates.DeleteShader DeleteShader { get; set; }

			public static Delegates.DeleteSync DeleteSync { get; set; }

			public static Delegates.DeleteTextures DeleteTextures { get; set; }

			public static Delegates.DeleteTransformFeedbacks DeleteTransformFeedbacks { get; set; }

			public static Delegates.DeleteVertexArrays DeleteVertexArrays { get; set; }

			public static Delegates.DepthFunc DepthFunc { get; set; }

			public static Delegates.DepthMask DepthMask { get; set; }

			public static Delegates.DepthRange DepthRange { get; set; }

			public static Delegates.DepthRangeArrayv DepthRangeArrayv { get; set; }

			public static Delegates.DepthRangef DepthRangef { get; set; }

			public static Delegates.DepthRangeIndexed DepthRangeIndexed { get; set; }

			public static Delegates.DetachShader DetachShader { get; set; }

			public static Delegates.Disable Disable { get; set; }

			public static Delegates.Disablei Disablei { get; set; }

			public static Delegates.DisableVertexArrayAttrib DisableVertexArrayAttrib { get; set; }

			public static Delegates.DisableVertexAttribArray DisableVertexAttribArray { get; set; }

			public static Delegates.DispatchCompute DispatchCompute { get; set; }

			public static Delegates.DispatchComputeIndirect DispatchComputeIndirect { get; set; }

			public static Delegates.DrawArrays DrawArrays { get; set; }

			public static Delegates.DrawArraysIndirect DrawArraysIndirect { get; set; }

			public static Delegates.DrawArraysInstanced DrawArraysInstanced { get; set; }

			public static Delegates.DrawArraysInstancedBaseInstance DrawArraysInstancedBaseInstance { get; set; }

			public static Delegates.DrawBuffer DrawBuffer { get; set; }

			public static Delegates.DrawBuffers DrawBuffers { get; set; }

			public static Delegates.DrawElements DrawElements { get; set; }

			public static Delegates.DrawElementsBaseVertex DrawElementsBaseVertex { get; set; }

			public static Delegates.DrawElementsIndirect DrawElementsIndirect { get; set; }

			public static Delegates.DrawElementsInstanced DrawElementsInstanced { get; set; }

			public static Delegates.DrawElementsInstancedBaseInstance DrawElementsInstancedBaseInstance { get; set; }

			public static Delegates.DrawElementsInstancedBaseVertex DrawElementsInstancedBaseVertex { get; set; }

			public static Delegates.DrawElementsInstancedBaseVertexBaseInstance DrawElementsInstancedBaseVertexBaseInstance { get; set; }

			public static Delegates.DrawRangeElements DrawRangeElements { get; set; }

			public static Delegates.DrawRangeElementsBaseVertex DrawRangeElementsBaseVertex { get; set; }

			public static Delegates.DrawTransformFeedback DrawTransformFeedback { get; set; }

			public static Delegates.DrawTransformFeedbackInstanced DrawTransformFeedbackInstanced { get; set; }

			public static Delegates.DrawTransformFeedbackStream DrawTransformFeedbackStream { get; set; }

			public static Delegates.DrawTransformFeedbackStreamInstanced DrawTransformFeedbackStreamInstanced { get; set; }

			public static Delegates.Enable Enable { get; set; }

			public static Delegates.Enablei Enablei { get; set; }

			public static Delegates.EnableVertexArrayAttrib EnableVertexArrayAttrib { get; set; }

			public static Delegates.EnableVertexAttribArray EnableVertexAttribArray { get; set; }

			public static Delegates.EndConditionalRender EndConditionalRender { get; set; }

			public static Delegates.EndQuery EndQuery { get; set; }

			public static Delegates.EndQueryIndexed EndQueryIndexed { get; set; }

			public static Delegates.EndTransformFeedback EndTransformFeedback { get; set; }

			public static Delegates.FenceSync FenceSync { get; set; }

			public static Delegates.Finish Finish { get; set; }

			public static Delegates.Flush Flush { get; set; }

			public static Delegates.FlushMappedBufferRange FlushMappedBufferRange { get; set; }

			public static Delegates.FlushMappedNamedBufferRange FlushMappedNamedBufferRange { get; set; }

			public static Delegates.FramebufferParameteri FramebufferParameteri { get; set; }

			public static Delegates.FramebufferRenderbuffer FramebufferRenderbuffer { get; set; }

			public static Delegates.FramebufferTexture FramebufferTexture { get; set; }

			public static Delegates.FramebufferTexture1D FramebufferTexture1D { get; set; }

			public static Delegates.FramebufferTexture2D FramebufferTexture2D { get; set; }

			public static Delegates.FramebufferTexture3D FramebufferTexture3D { get; set; }

			public static Delegates.FramebufferTextureLayer FramebufferTextureLayer { get; set; }

			public static Delegates.FrontFace FrontFace { get; set; }

			public static Delegates.GenBuffers GenBuffers { get; set; }

			public static Delegates.GenerateMipmap GenerateMipmap { get; set; }

			public static Delegates.GenerateTextureMipmap GenerateTextureMipmap { get; set; }

			public static Delegates.GenFramebuffers GenFramebuffers { get; set; }

			public static Delegates.GenProgramPipelines GenProgramPipelines { get; set; }

			public static Delegates.GenQueries GenQueries { get; set; }

			public static Delegates.GenRenderbuffers GenRenderbuffers { get; set; }

			public static Delegates.GenSamplers GenSamplers { get; set; }

			public static Delegates.GenTextures GenTextures { get; set; }

			public static Delegates.GenTransformFeedbacks GenTransformFeedbacks { get; set; }

			public static Delegates.GenVertexArrays GenVertexArrays { get; set; }

			public static Delegates.GetActiveAtomicCounterBufferiv GetActiveAtomicCounterBufferiv { get; set; }

			public static Delegates.GetActiveAttrib GetActiveAttrib { get; set; }

			public static Delegates.GetActiveSubroutineName GetActiveSubroutineName { get; set; }

			public static Delegates.GetActiveSubroutineUniformiv GetActiveSubroutineUniformiv { get; set; }

			public static Delegates.GetActiveSubroutineUniformName GetActiveSubroutineUniformName { get; set; }

			public static Delegates.GetActiveUniform GetActiveUniform { get; set; }

			public static Delegates.GetActiveUniformBlockiv GetActiveUniformBlockiv { get; set; }

			public static Delegates.GetActiveUniformBlockName GetActiveUniformBlockName { get; set; }

			public static Delegates.GetActiveUniformName GetActiveUniformName { get; set; }

			public static Delegates.GetActiveUniformsiv GetActiveUniformsiv { get; set; }

			public static Delegates.GetAttachedShaders GetAttachedShaders { get; set; }

			public static Delegates.GetAttribLocation GetAttribLocation { get; set; }

			public static Delegates.GetBooleani_v GetBooleani_v { get; set; }

			public static Delegates.GetBooleanv GetBooleanv { get; set; }

			public static Delegates.GetBufferParameteri64v GetBufferParameteri64v { get; set; }

			public static Delegates.GetBufferParameteriv GetBufferParameteriv { get; set; }

			public static Delegates.GetBufferPointerv GetBufferPointerv { get; set; }

			public static Delegates.GetBufferSubData GetBufferSubData { get; set; }

			public static Delegates.GetCompressedTexImage GetCompressedTexImage { get; set; }

			public static Delegates.GetCompressedTextureImage GetCompressedTextureImage { get; set; }

			public static Delegates.GetCompressedTextureSubImage GetCompressedTextureSubImage { get; set; }

			public static Delegates.GetDebugMessageLog GetDebugMessageLog { get; set; }

			public static Delegates.GetDoublei_v GetDoublei_v { get; set; }

			public static Delegates.GetDoublev GetDoublev { get; set; }

			public static Delegates.GetError GetError { get; set; }

			public static Delegates.GetFloati_v GetFloati_v { get; set; }

			public static Delegates.GetFloatv GetFloatv { get; set; }

			public static Delegates.GetFragDataIndex GetFragDataIndex { get; set; }

			public static Delegates.GetFragDataLocation GetFragDataLocation { get; set; }

			public static Delegates.GetFramebufferAttachmentParameteriv GetFramebufferAttachmentParameteriv { get; set; }

			public static Delegates.GetFramebufferParameteriv GetFramebufferParameteriv { get; set; }

			public static Delegates.GetGraphicsResetStatus GetGraphicsResetStatus { get; set; }

			public static Delegates.GetInteger64i_v GetInteger64i_v { get; set; }

			public static Delegates.GetInteger64v GetInteger64v { get; set; }

			public static Delegates.GetIntegeri_v GetIntegeri_v { get; set; }

			public static Delegates.GetIntegerv GetIntegerv { get; set; }

			public static Delegates.GetInternalformati64v GetInternalformati64v { get; set; }

			public static Delegates.GetInternalformativ GetInternalformativ { get; set; }

			public static Delegates.GetMultisamplefv GetMultisamplefv { get; set; }

			public static Delegates.GetNamedBufferParameteri64v GetNamedBufferParameteri64v { get; set; }

			public static Delegates.GetNamedBufferParameteriv GetNamedBufferParameteriv { get; set; }

			public static Delegates.GetNamedBufferPointerv GetNamedBufferPointerv { get; set; }

			public static Delegates.GetNamedBufferSubData GetNamedBufferSubData { get; set; }

			public static Delegates.GetNamedFramebufferAttachmentParameteriv GetNamedFramebufferAttachmentParameteriv { get; set; }

			public static Delegates.GetNamedFramebufferParameteriv GetNamedFramebufferParameteriv { get; set; }

			public static Delegates.GetNamedRenderbufferParameteriv GetNamedRenderbufferParameteriv { get; set; }

			public static Delegates.GetnCompressedTexImage GetnCompressedTexImage { get; set; }

			public static Delegates.GetnTexImage GetnTexImage { get; set; }

			public static Delegates.GetnUniformdv GetnUniformdv { get; set; }

			public static Delegates.GetnUniformfv GetnUniformfv { get; set; }

			public static Delegates.GetnUniformiv GetnUniformiv { get; set; }

			public static Delegates.GetnUniformuiv GetnUniformuiv { get; set; }

			public static Delegates.GetObjectLabel GetObjectLabel { get; set; }

			public static Delegates.GetObjectPtrLabel GetObjectPtrLabel { get; set; }

			public static Delegates.GetPointerv GetPointerv { get; set; }

			public static Delegates.GetProgramBinary GetProgramBinary { get; set; }

			public static Delegates.GetProgramInfoLog GetProgramInfoLog { get; set; }

			public static Delegates.GetProgramInterfaceiv GetProgramInterfaceiv { get; set; }

			public static Delegates.GetProgramiv GetProgramiv { get; set; }

			public static Delegates.GetProgramPipelineInfoLog GetProgramPipelineInfoLog { get; set; }

			public static Delegates.GetProgramPipelineiv GetProgramPipelineiv { get; set; }

			public static Delegates.GetProgramResourceIndex GetProgramResourceIndex { get; set; }

			public static Delegates.GetProgramResourceiv GetProgramResourceiv { get; set; }

			public static Delegates.GetProgramResourceLocation GetProgramResourceLocation { get; set; }

			public static Delegates.GetProgramResourceLocationIndex GetProgramResourceLocationIndex { get; set; }

			public static Delegates.GetProgramResourceName GetProgramResourceName { get; set; }

			public static Delegates.GetProgramStageiv GetProgramStageiv { get; set; }

			public static Delegates.GetQueryBufferObjecti64v GetQueryBufferObjecti64v { get; set; }

			public static Delegates.GetQueryBufferObjectiv GetQueryBufferObjectiv { get; set; }

			public static Delegates.GetQueryBufferObjectui64v GetQueryBufferObjectui64v { get; set; }

			public static Delegates.GetQueryBufferObjectuiv GetQueryBufferObjectuiv { get; set; }

			public static Delegates.GetQueryIndexediv GetQueryIndexediv { get; set; }

			public static Delegates.GetQueryiv GetQueryiv { get; set; }

			public static Delegates.GetQueryObjecti64v GetQueryObjecti64v { get; set; }

			public static Delegates.GetQueryObjectiv GetQueryObjectiv { get; set; }

			public static Delegates.GetQueryObjectui64v GetQueryObjectui64v { get; set; }

			public static Delegates.GetQueryObjectuiv GetQueryObjectuiv { get; set; }

			public static Delegates.GetRenderbufferParameteriv GetRenderbufferParameteriv { get; set; }

			public static Delegates.GetSamplerParameterfv GetSamplerParameterfv { get; set; }

			public static Delegates.GetSamplerParameterIiv GetSamplerParameterIiv { get; set; }

			public static Delegates.GetSamplerParameterIuiv GetSamplerParameterIuiv { get; set; }

			public static Delegates.GetSamplerParameteriv GetSamplerParameteriv { get; set; }

			public static Delegates.GetShaderInfoLog GetShaderInfoLog { get; set; }

			public static Delegates.GetShaderiv GetShaderiv { get; set; }

			public static Delegates.GetShaderPrecisionFormat GetShaderPrecisionFormat { get; set; }

			public static Delegates.GetShaderSource GetShaderSource { get; set; }

			public static Delegates.GetString GetString { get; set; }

			public static Delegates.GetStringi GetStringi { get; set; }

			public static Delegates.GetSubroutineIndex GetSubroutineIndex { get; set; }

			public static Delegates.GetSubroutineUniformLocation GetSubroutineUniformLocation { get; set; }

			public static Delegates.GetSynciv GetSynciv { get; set; }

			public static Delegates.GetTexImage GetTexImage { get; set; }

			public static Delegates.GetTexLevelParameterfv GetTexLevelParameterfv { get; set; }

			public static Delegates.GetTexLevelParameteriv GetTexLevelParameteriv { get; set; }

			public static Delegates.GetTexParameterfv GetTexParameterfv { get; set; }

			public static Delegates.GetTexParameterIiv GetTexParameterIiv { get; set; }

			public static Delegates.GetTexParameterIuiv GetTexParameterIuiv { get; set; }

			public static Delegates.GetTexParameteriv GetTexParameteriv { get; set; }

			public static Delegates.GetTextureImage GetTextureImage { get; set; }

			public static Delegates.GetTextureLevelParameterfv GetTextureLevelParameterfv { get; set; }

			public static Delegates.GetTextureLevelParameteriv GetTextureLevelParameteriv { get; set; }

			public static Delegates.GetTextureParameterfv GetTextureParameterfv { get; set; }

			public static Delegates.GetTextureParameterIiv GetTextureParameterIiv { get; set; }

			public static Delegates.GetTextureParameterIuiv GetTextureParameterIuiv { get; set; }

			public static Delegates.GetTextureParameteriv GetTextureParameteriv { get; set; }

			public static Delegates.GetTextureSubImage GetTextureSubImage { get; set; }

			public static Delegates.GetTransformFeedbacki_v GetTransformFeedbacki_v { get; set; }

			public static Delegates.GetTransformFeedbacki64_v GetTransformFeedbacki64_v { get; set; }

			public static Delegates.GetTransformFeedbackiv GetTransformFeedbackiv { get; set; }

			public static Delegates.GetTransformFeedbackVarying GetTransformFeedbackVarying { get; set; }

			public static Delegates.GetUniformBlockIndex GetUniformBlockIndex { get; set; }

			public static Delegates.GetUniformdv GetUniformdv { get; set; }

			public static Delegates.GetUniformfv GetUniformfv { get; set; }

			public static Delegates.GetUniformIndices GetUniformIndices { get; set; }

			public static Delegates.GetUniformiv GetUniformiv { get; set; }

			public static Delegates.GetUniformLocation GetUniformLocation { get; set; }

			public static Delegates.GetUniformSubroutineuiv GetUniformSubroutineuiv { get; set; }

			public static Delegates.GetUniformuiv GetUniformuiv { get; set; }

			public static Delegates.GetVertexArrayIndexed64iv GetVertexArrayIndexed64iv { get; set; }

			public static Delegates.GetVertexArrayIndexediv GetVertexArrayIndexediv { get; set; }

			public static Delegates.GetVertexArrayiv GetVertexArrayiv { get; set; }

			public static Delegates.GetVertexAttribdv GetVertexAttribdv { get; set; }

			public static Delegates.GetVertexAttribfv GetVertexAttribfv { get; set; }

			public static Delegates.GetVertexAttribIiv GetVertexAttribIiv { get; set; }

			public static Delegates.GetVertexAttribIuiv GetVertexAttribIuiv { get; set; }

			public static Delegates.GetVertexAttribiv GetVertexAttribiv { get; set; }

			public static Delegates.GetVertexAttribLdv GetVertexAttribLdv { get; set; }

			public static Delegates.GetVertexAttribPointerv GetVertexAttribPointerv { get; set; }

			public static Delegates.Hint Hint { get; set; }

			public static Delegates.InvalidateBufferData InvalidateBufferData { get; set; }

			public static Delegates.InvalidateBufferSubData InvalidateBufferSubData { get; set; }

			public static Delegates.InvalidateFramebuffer InvalidateFramebuffer { get; set; }

			public static Delegates.InvalidateNamedFramebufferData InvalidateNamedFramebufferData { get; set; }

			public static Delegates.InvalidateNamedFramebufferSubData InvalidateNamedFramebufferSubData { get; set; }

			public static Delegates.InvalidateSubFramebuffer InvalidateSubFramebuffer { get; set; }

			public static Delegates.InvalidateTexImage InvalidateTexImage { get; set; }

			public static Delegates.InvalidateTexSubImage InvalidateTexSubImage { get; set; }

			public static Delegates.IsBuffer IsBuffer { get; set; }

			public static Delegates.IsEnabled IsEnabled { get; set; }

			public static Delegates.IsEnabledi IsEnabledi { get; set; }

			public static Delegates.IsFramebuffer IsFramebuffer { get; set; }

			public static Delegates.IsProgram IsProgram { get; set; }

			public static Delegates.IsProgramPipeline IsProgramPipeline { get; set; }

			public static Delegates.IsQuery IsQuery { get; set; }

			public static Delegates.IsRenderbuffer IsRenderbuffer { get; set; }

			public static Delegates.IsSampler IsSampler { get; set; }

			public static Delegates.IsShader IsShader { get; set; }

			public static Delegates.IsSync IsSync { get; set; }

			public static Delegates.IsTexture IsTexture { get; set; }

			public static Delegates.IsTransformFeedback IsTransformFeedback { get; set; }

			public static Delegates.IsVertexArray IsVertexArray { get; set; }

			public static Delegates.LineWidth LineWidth { get; set; }

			public static Delegates.LinkProgram LinkProgram { get; set; }

			public static Delegates.LogicOp LogicOp { get; set; }

			public static Delegates.MapBuffer MapBuffer { get; set; }

			public static Delegates.MapBufferRange MapBufferRange { get; set; }

			public static Delegates.MapNamedBuffer MapNamedBuffer { get; set; }

			public static Delegates.MapNamedBufferRange MapNamedBufferRange { get; set; }

			public static Delegates.MemoryBarrier MemoryBarrier { get; set; }

			public static Delegates.MemoryBarrierByRegion MemoryBarrierByRegion { get; set; }

			public static Delegates.MinSampleShading MinSampleShading { get; set; }

			public static Delegates.MultiDrawArrays MultiDrawArrays { get; set; }

			public static Delegates.MultiDrawArraysIndirect MultiDrawArraysIndirect { get; set; }

			//public static Delegates.MultiDrawArraysIndirectCount MultiDrawArraysIndirectCount { get; set; }

			public static Delegates.MultiDrawElements MultiDrawElements { get; set; }

			public static Delegates.MultiDrawElementsBaseVertex MultiDrawElementsBaseVertex { get; set; }

			public static Delegates.MultiDrawElementsIndirect MultiDrawElementsIndirect { get; set; }

			//public static Delegates.MultiDrawElementsIndirectCount MultiDrawElementsIndirectCount { get; set; }

			public static Delegates.NamedBufferData NamedBufferData { get; set; }

			public static Delegates.NamedBufferStorage NamedBufferStorage { get; set; }

			public static Delegates.NamedBufferSubData NamedBufferSubData { get; set; }

			public static Delegates.NamedFramebufferDrawBuffer NamedFramebufferDrawBuffer { get; set; }

			public static Delegates.NamedFramebufferDrawBuffers NamedFramebufferDrawBuffers { get; set; }

			public static Delegates.NamedFramebufferParameteri NamedFramebufferParameteri { get; set; }

			public static Delegates.NamedFramebufferReadBuffer NamedFramebufferReadBuffer { get; set; }

			public static Delegates.NamedFramebufferRenderbuffer NamedFramebufferRenderbuffer { get; set; }

			public static Delegates.NamedFramebufferTexture NamedFramebufferTexture { get; set; }

			public static Delegates.NamedFramebufferTextureLayer NamedFramebufferTextureLayer { get; set; }

			public static Delegates.NamedRenderbufferStorage NamedRenderbufferStorage { get; set; }

			public static Delegates.NamedRenderbufferStorageMultisample NamedRenderbufferStorageMultisample { get; set; }

			public static Delegates.ObjectLabel ObjectLabel { get; set; }

			public static Delegates.ObjectPtrLabel ObjectPtrLabel { get; set; }

			public static Delegates.PatchParameterfv PatchParameterfv { get; set; }

			public static Delegates.PatchParameteri PatchParameteri { get; set; }

			public static Delegates.PauseTransformFeedback PauseTransformFeedback { get; set; }

			public static Delegates.PixelStoref PixelStoref { get; set; }

			public static Delegates.PixelStorei PixelStorei { get; set; }

			public static Delegates.PointParameterf PointParameterf { get; set; }

			public static Delegates.PointParameterfv PointParameterfv { get; set; }

			public static Delegates.PointParameteri PointParameteri { get; set; }

			public static Delegates.PointParameteriv PointParameteriv { get; set; }

			public static Delegates.PointSize PointSize { get; set; }

			public static Delegates.PolygonMode PolygonMode { get; set; }

			public static Delegates.PolygonOffset PolygonOffset { get; set; }

			//public static Delegates.PolygonOffsetClamp PolygonOffsetClamp { get; set; }

			public static Delegates.PopDebugGroup PopDebugGroup { get; set; }

			public static Delegates.PrimitiveRestartIndex PrimitiveRestartIndex { get; set; }

			public static Delegates.ProgramBinary ProgramBinary { get; set; }

			public static Delegates.ProgramParameteri ProgramParameteri { get; set; }

			public static Delegates.ProgramUniform1d ProgramUniform1d { get; set; }

			public static Delegates.ProgramUniform1dv ProgramUniform1dv { get; set; }

			public static Delegates.ProgramUniform1f ProgramUniform1f { get; set; }

			public static Delegates.ProgramUniform1fv ProgramUniform1fv { get; set; }

			public static Delegates.ProgramUniform1i ProgramUniform1i { get; set; }

			public static Delegates.ProgramUniform1iv ProgramUniform1iv { get; set; }

			public static Delegates.ProgramUniform1ui ProgramUniform1ui { get; set; }

			public static Delegates.ProgramUniform1uiv ProgramUniform1uiv { get; set; }

			public static Delegates.ProgramUniform2d ProgramUniform2d { get; set; }

			public static Delegates.ProgramUniform2dv ProgramUniform2dv { get; set; }

			public static Delegates.ProgramUniform2f ProgramUniform2f { get; set; }

			public static Delegates.ProgramUniform2fv ProgramUniform2fv { get; set; }

			public static Delegates.ProgramUniform2i ProgramUniform2i { get; set; }

			public static Delegates.ProgramUniform2iv ProgramUniform2iv { get; set; }

			public static Delegates.ProgramUniform2ui ProgramUniform2ui { get; set; }

			public static Delegates.ProgramUniform2uiv ProgramUniform2uiv { get; set; }

			public static Delegates.ProgramUniform3d ProgramUniform3d { get; set; }

			public static Delegates.ProgramUniform3dv ProgramUniform3dv { get; set; }

			public static Delegates.ProgramUniform3f ProgramUniform3f { get; set; }

			public static Delegates.ProgramUniform3fv ProgramUniform3fv { get; set; }

			public static Delegates.ProgramUniform3i ProgramUniform3i { get; set; }

			public static Delegates.ProgramUniform3iv ProgramUniform3iv { get; set; }

			public static Delegates.ProgramUniform3ui ProgramUniform3ui { get; set; }

			public static Delegates.ProgramUniform3uiv ProgramUniform3uiv { get; set; }

			public static Delegates.ProgramUniform4d ProgramUniform4d { get; set; }

			public static Delegates.ProgramUniform4dv ProgramUniform4dv { get; set; }

			public static Delegates.ProgramUniform4f ProgramUniform4f { get; set; }

			public static Delegates.ProgramUniform4fv ProgramUniform4fv { get; set; }

			public static Delegates.ProgramUniform4i ProgramUniform4i { get; set; }

			public static Delegates.ProgramUniform4iv ProgramUniform4iv { get; set; }

			public static Delegates.ProgramUniform4ui ProgramUniform4ui { get; set; }

			public static Delegates.ProgramUniform4uiv ProgramUniform4uiv { get; set; }

			public static Delegates.ProgramUniformMatrix2dv ProgramUniformMatrix2dv { get; set; }

			public static Delegates.ProgramUniformMatrix2fv ProgramUniformMatrix2fv { get; set; }

			public static Delegates.ProgramUniformMatrix2x3dv ProgramUniformMatrix2x3dv { get; set; }

			public static Delegates.ProgramUniformMatrix2x3fv ProgramUniformMatrix2x3fv { get; set; }

			public static Delegates.ProgramUniformMatrix2x4dv ProgramUniformMatrix2x4dv { get; set; }

			public static Delegates.ProgramUniformMatrix2x4fv ProgramUniformMatrix2x4fv { get; set; }

			public static Delegates.ProgramUniformMatrix3dv ProgramUniformMatrix3dv { get; set; }

			public static Delegates.ProgramUniformMatrix3fv ProgramUniformMatrix3fv { get; set; }

			public static Delegates.ProgramUniformMatrix3x2dv ProgramUniformMatrix3x2dv { get; set; }

			public static Delegates.ProgramUniformMatrix3x2fv ProgramUniformMatrix3x2fv { get; set; }

			public static Delegates.ProgramUniformMatrix3x4dv ProgramUniformMatrix3x4dv { get; set; }

			public static Delegates.ProgramUniformMatrix3x4fv ProgramUniformMatrix3x4fv { get; set; }

			public static Delegates.ProgramUniformMatrix4dv ProgramUniformMatrix4dv { get; set; }

			public static Delegates.ProgramUniformMatrix4fv ProgramUniformMatrix4fv { get; set; }

			public static Delegates.ProgramUniformMatrix4x2dv ProgramUniformMatrix4x2dv { get; set; }

			public static Delegates.ProgramUniformMatrix4x2fv ProgramUniformMatrix4x2fv { get; set; }

			public static Delegates.ProgramUniformMatrix4x3dv ProgramUniformMatrix4x3dv { get; set; }

			public static Delegates.ProgramUniformMatrix4x3fv ProgramUniformMatrix4x3fv { get; set; }

			public static Delegates.ProvokingVertex ProvokingVertex { get; set; }

			public static Delegates.PushDebugGroup PushDebugGroup { get; set; }

			public static Delegates.QueryCounter QueryCounter { get; set; }

			public static Delegates.ReadBuffer ReadBuffer { get; set; }

			public static Delegates.ReadnPixels ReadnPixels { get; set; }

			public static Delegates.ReadPixels ReadPixels { get; set; }

			public static Delegates.ReleaseShaderCompiler ReleaseShaderCompiler { get; set; }

			public static Delegates.RenderbufferStorage RenderbufferStorage { get; set; }

			public static Delegates.RenderbufferStorageMultisample RenderbufferStorageMultisample { get; set; }

			public static Delegates.ResumeTransformFeedback ResumeTransformFeedback { get; set; }

			public static Delegates.SampleCoverage SampleCoverage { get; set; }

			public static Delegates.SampleMaski SampleMaski { get; set; }

			public static Delegates.SamplerParameterf SamplerParameterf { get; set; }

			public static Delegates.SamplerParameterfv SamplerParameterfv { get; set; }

			public static Delegates.SamplerParameteri SamplerParameteri { get; set; }

			public static Delegates.SamplerParameterIiv SamplerParameterIiv { get; set; }

			public static Delegates.SamplerParameterIuiv SamplerParameterIuiv { get; set; }

			public static Delegates.SamplerParameteriv SamplerParameteriv { get; set; }

			public static Delegates.Scissor Scissor { get; set; }

			public static Delegates.ScissorArrayv ScissorArrayv { get; set; }

			public static Delegates.ScissorIndexed ScissorIndexed { get; set; }

			public static Delegates.ScissorIndexedv ScissorIndexedv { get; set; }

			public static Delegates.ShaderBinary ShaderBinary { get; set; }

			public static Delegates.ShaderSource ShaderSource { get; set; }

			public static Delegates.ShaderStorageBlockBinding ShaderStorageBlockBinding { get; set; }

			//public static Delegates.SpecializeShader SpecializeShader { get; set; }

			public static Delegates.StencilFunc StencilFunc { get; set; }

			public static Delegates.StencilFuncSeparate StencilFuncSeparate { get; set; }

			public static Delegates.StencilMask StencilMask { get; set; }

			public static Delegates.StencilMaskSeparate StencilMaskSeparate { get; set; }

			public static Delegates.StencilOp StencilOp { get; set; }

			public static Delegates.StencilOpSeparate StencilOpSeparate { get; set; }

			public static Delegates.TexBuffer TexBuffer { get; set; }

			public static Delegates.TexBufferRange TexBufferRange { get; set; }

			public static Delegates.TexImage1D TexImage1D { get; set; }

			public static Delegates.TexImage2D TexImage2D { get; set; }

			public static Delegates.TexImage2DMultisample TexImage2DMultisample { get; set; }

			public static Delegates.TexImage3D TexImage3D { get; set; }

			public static Delegates.TexImage3DMultisample TexImage3DMultisample { get; set; }

			public static Delegates.TexParameterf TexParameterf { get; set; }

			public static Delegates.TexParameterfv TexParameterfv { get; set; }

			public static Delegates.TexParameteri TexParameteri { get; set; }

			public static Delegates.TexParameterIiv TexParameterIiv { get; set; }

			public static Delegates.TexParameterIuiv TexParameterIuiv { get; set; }

			public static Delegates.TexParameteriv TexParameteriv { get; set; }

			public static Delegates.TexStorage1D TexStorage1D { get; set; }

			public static Delegates.TexStorage2D TexStorage2D { get; set; }

			public static Delegates.TexStorage2DMultisample TexStorage2DMultisample { get; set; }

			public static Delegates.TexStorage3D TexStorage3D { get; set; }

			public static Delegates.TexStorage3DMultisample TexStorage3DMultisample { get; set; }

			public static Delegates.TexSubImage1D TexSubImage1D { get; set; }

			public static Delegates.TexSubImage2D TexSubImage2D { get; set; }

			public static Delegates.TexSubImage3D TexSubImage3D { get; set; }

			public static Delegates.TextureBarrier TextureBarrier { get; set; }

			public static Delegates.TextureBuffer TextureBuffer { get; set; }

			public static Delegates.TextureBufferRange TextureBufferRange { get; set; }

			public static Delegates.TextureParameterf TextureParameterf { get; set; }

			public static Delegates.TextureParameterfv TextureParameterfv { get; set; }

			public static Delegates.TextureParameteri TextureParameteri { get; set; }

			public static Delegates.TextureParameterIiv TextureParameterIiv { get; set; }

			public static Delegates.TextureParameterIuiv TextureParameterIuiv { get; set; }

			public static Delegates.TextureParameteriv TextureParameteriv { get; set; }

			public static Delegates.TextureStorage1D TextureStorage1D { get; set; }

			public static Delegates.TextureStorage2D TextureStorage2D { get; set; }

			public static Delegates.TextureStorage2DMultisample TextureStorage2DMultisample { get; set; }

			public static Delegates.TextureStorage3D TextureStorage3D { get; set; }

			public static Delegates.TextureStorage3DMultisample TextureStorage3DMultisample { get; set; }

			public static Delegates.TextureSubImage1D TextureSubImage1D { get; set; }

			public static Delegates.TextureSubImage2D TextureSubImage2D { get; set; }

			public static Delegates.TextureSubImage3D TextureSubImage3D { get; set; }

			public static Delegates.TextureView TextureView { get; set; }

			public static Delegates.TransformFeedbackBufferBase TransformFeedbackBufferBase { get; set; }

			public static Delegates.TransformFeedbackBufferRange TransformFeedbackBufferRange { get; set; }

			public static Delegates.TransformFeedbackVaryings TransformFeedbackVaryings { get; set; }

			public static Delegates.Uniform1d Uniform1d { get; set; }

			public static Delegates.Uniform1dv Uniform1dv { get; set; }

			public static Delegates.Uniform1f Uniform1f { get; set; }

			public static Delegates.Uniform1fv Uniform1fv { get; set; }

			public static Delegates.Uniform1i Uniform1i { get; set; }

			public static Delegates.Uniform1iv Uniform1iv { get; set; }

			public static Delegates.Uniform1ui Uniform1ui { get; set; }

			public static Delegates.Uniform1uiv Uniform1uiv { get; set; }

			public static Delegates.Uniform2d Uniform2d { get; set; }

			public static Delegates.Uniform2dv Uniform2dv { get; set; }

			public static Delegates.Uniform2f Uniform2f { get; set; }

			public static Delegates.Uniform2fv Uniform2fv { get; set; }

			public static Delegates.Uniform2i Uniform2i { get; set; }

			public static Delegates.Uniform2iv Uniform2iv { get; set; }

			public static Delegates.Uniform2ui Uniform2ui { get; set; }

			public static Delegates.Uniform2uiv Uniform2uiv { get; set; }

			public static Delegates.Uniform3d Uniform3d { get; set; }

			public static Delegates.Uniform3dv Uniform3dv { get; set; }

			public static Delegates.Uniform3f Uniform3f { get; set; }

			public static Delegates.Uniform3fv Uniform3fv { get; set; }

			public static Delegates.Uniform3i Uniform3i { get; set; }

			public static Delegates.Uniform3iv Uniform3iv { get; set; }

			public static Delegates.Uniform3ui Uniform3ui { get; set; }

			public static Delegates.Uniform3uiv Uniform3uiv { get; set; }

			public static Delegates.Uniform4d Uniform4d { get; set; }

			public static Delegates.Uniform4dv Uniform4dv { get; set; }

			public static Delegates.Uniform4f Uniform4f { get; set; }

			public static Delegates.Uniform4fv Uniform4fv { get; set; }

			public static Delegates.Uniform4i Uniform4i { get; set; }

			public static Delegates.Uniform4iv Uniform4iv { get; set; }

			public static Delegates.Uniform4ui Uniform4ui { get; set; }

			public static Delegates.Uniform4uiv Uniform4uiv { get; set; }

			public static Delegates.UniformBlockBinding UniformBlockBinding { get; set; }

			public static Delegates.UniformMatrix2dv UniformMatrix2dv { get; set; }

			public static Delegates.UniformMatrix2fv UniformMatrix2fv { get; set; }

			public static Delegates.UniformMatrix2x3dv UniformMatrix2x3dv { get; set; }

			public static Delegates.UniformMatrix2x3fv UniformMatrix2x3fv { get; set; }

			public static Delegates.UniformMatrix2x4dv UniformMatrix2x4dv { get; set; }

			public static Delegates.UniformMatrix2x4fv UniformMatrix2x4fv { get; set; }

			public static Delegates.UniformMatrix3dv UniformMatrix3dv { get; set; }

			public static Delegates.UniformMatrix3fv UniformMatrix3fv { get; set; }

			public static Delegates.UniformMatrix3x2dv UniformMatrix3x2dv { get; set; }

			public static Delegates.UniformMatrix3x2fv UniformMatrix3x2fv { get; set; }

			public static Delegates.UniformMatrix3x4dv UniformMatrix3x4dv { get; set; }

			public static Delegates.UniformMatrix3x4fv UniformMatrix3x4fv { get; set; }

			public static Delegates.UniformMatrix4dv UniformMatrix4dv { get; set; }

			public static Delegates.UniformMatrix4fv UniformMatrix4fv { get; set; }

			public static Delegates.UniformMatrix4x2dv UniformMatrix4x2dv { get; set; }

			public static Delegates.UniformMatrix4x2fv UniformMatrix4x2fv { get; set; }

			public static Delegates.UniformMatrix4x3dv UniformMatrix4x3dv { get; set; }

			public static Delegates.UniformMatrix4x3fv UniformMatrix4x3fv { get; set; }

			public static Delegates.UniformSubroutinesuiv UniformSubroutinesuiv { get; set; }

			public static Delegates.UnmapBuffer UnmapBuffer { get; set; }

			public static Delegates.UnmapNamedBuffer UnmapNamedBuffer { get; set; }

			public static Delegates.UseProgram UseProgram { get; set; }

			public static Delegates.UseProgramStages UseProgramStages { get; set; }

			public static Delegates.ValidateProgram ValidateProgram { get; set; }

			public static Delegates.ValidateProgramPipeline ValidateProgramPipeline { get; set; }

			public static Delegates.VertexArrayAttribBinding VertexArrayAttribBinding { get; set; }

			public static Delegates.VertexArrayAttribFormat VertexArrayAttribFormat { get; set; }

			public static Delegates.VertexArrayAttribIFormat VertexArrayAttribIFormat { get; set; }

			public static Delegates.VertexArrayAttribLFormat VertexArrayAttribLFormat { get; set; }

			public static Delegates.VertexArrayBindingDivisor VertexArrayBindingDivisor { get; set; }

			public static Delegates.VertexArrayElementBuffer VertexArrayElementBuffer { get; set; }

			public static Delegates.VertexArrayVertexBuffer VertexArrayVertexBuffer { get; set; }

			public static Delegates.VertexArrayVertexBuffers VertexArrayVertexBuffers { get; set; }

			public static Delegates.VertexAttrib1d VertexAttrib1d { get; set; }

			public static Delegates.VertexAttrib1dv VertexAttrib1dv { get; set; }

			public static Delegates.VertexAttrib1f VertexAttrib1f { get; set; }

			public static Delegates.VertexAttrib1fv VertexAttrib1fv { get; set; }

			public static Delegates.VertexAttrib1s VertexAttrib1s { get; set; }

			public static Delegates.VertexAttrib1sv VertexAttrib1sv { get; set; }

			public static Delegates.VertexAttrib2d VertexAttrib2d { get; set; }

			public static Delegates.VertexAttrib2dv VertexAttrib2dv { get; set; }

			public static Delegates.VertexAttrib2f VertexAttrib2f { get; set; }

			public static Delegates.VertexAttrib2fv VertexAttrib2fv { get; set; }

			public static Delegates.VertexAttrib2s VertexAttrib2s { get; set; }

			public static Delegates.VertexAttrib2sv VertexAttrib2sv { get; set; }

			public static Delegates.VertexAttrib3d VertexAttrib3d { get; set; }

			public static Delegates.VertexAttrib3dv VertexAttrib3dv { get; set; }

			public static Delegates.VertexAttrib3f VertexAttrib3f { get; set; }

			public static Delegates.VertexAttrib3fv VertexAttrib3fv { get; set; }

			public static Delegates.VertexAttrib3s VertexAttrib3s { get; set; }

			public static Delegates.VertexAttrib3sv VertexAttrib3sv { get; set; }

			public static Delegates.VertexAttrib4bv VertexAttrib4bv { get; set; }

			public static Delegates.VertexAttrib4d VertexAttrib4d { get; set; }

			public static Delegates.VertexAttrib4dv VertexAttrib4dv { get; set; }

			public static Delegates.VertexAttrib4f VertexAttrib4f { get; set; }

			public static Delegates.VertexAttrib4fv VertexAttrib4fv { get; set; }

			public static Delegates.VertexAttrib4iv VertexAttrib4iv { get; set; }

			public static Delegates.VertexAttrib4Nbv VertexAttrib4Nbv { get; set; }

			public static Delegates.VertexAttrib4Niv VertexAttrib4Niv { get; set; }

			public static Delegates.VertexAttrib4Nsv VertexAttrib4Nsv { get; set; }

			public static Delegates.VertexAttrib4Nub VertexAttrib4Nub { get; set; }

			public static Delegates.VertexAttrib4Nubv VertexAttrib4Nubv { get; set; }

			public static Delegates.VertexAttrib4Nuiv VertexAttrib4Nuiv { get; set; }

			public static Delegates.VertexAttrib4Nusv VertexAttrib4Nusv { get; set; }

			public static Delegates.VertexAttrib4s VertexAttrib4s { get; set; }

			public static Delegates.VertexAttrib4sv VertexAttrib4sv { get; set; }

			public static Delegates.VertexAttrib4ubv VertexAttrib4ubv { get; set; }

			public static Delegates.VertexAttrib4uiv VertexAttrib4uiv { get; set; }

			public static Delegates.VertexAttrib4usv VertexAttrib4usv { get; set; }

			public static Delegates.VertexAttribBinding VertexAttribBinding { get; set; }

			public static Delegates.VertexAttribDivisor VertexAttribDivisor { get; set; }

			public static Delegates.VertexAttribFormat VertexAttribFormat { get; set; }

			public static Delegates.VertexAttribI1i VertexAttribI1i { get; set; }

			public static Delegates.VertexAttribI1iv VertexAttribI1iv { get; set; }

			public static Delegates.VertexAttribI1ui VertexAttribI1ui { get; set; }

			public static Delegates.VertexAttribI1uiv VertexAttribI1uiv { get; set; }

			public static Delegates.VertexAttribI2i VertexAttribI2i { get; set; }

			public static Delegates.VertexAttribI2iv VertexAttribI2iv { get; set; }

			public static Delegates.VertexAttribI2ui VertexAttribI2ui { get; set; }

			public static Delegates.VertexAttribI2uiv VertexAttribI2uiv { get; set; }

			public static Delegates.VertexAttribI3i VertexAttribI3i { get; set; }

			public static Delegates.VertexAttribI3iv VertexAttribI3iv { get; set; }

			public static Delegates.VertexAttribI3ui VertexAttribI3ui { get; set; }

			public static Delegates.VertexAttribI3uiv VertexAttribI3uiv { get; set; }

			public static Delegates.VertexAttribI4bv VertexAttribI4bv { get; set; }

			public static Delegates.VertexAttribI4i VertexAttribI4i { get; set; }

			public static Delegates.VertexAttribI4iv VertexAttribI4iv { get; set; }

			public static Delegates.VertexAttribI4sv VertexAttribI4sv { get; set; }

			public static Delegates.VertexAttribI4ubv VertexAttribI4ubv { get; set; }

			public static Delegates.VertexAttribI4ui VertexAttribI4ui { get; set; }

			public static Delegates.VertexAttribI4uiv VertexAttribI4uiv { get; set; }

			public static Delegates.VertexAttribI4usv VertexAttribI4usv { get; set; }

			public static Delegates.VertexAttribIFormat VertexAttribIFormat { get; set; }

			public static Delegates.VertexAttribIPointer VertexAttribIPointer { get; set; }

			public static Delegates.VertexAttribL1d VertexAttribL1d { get; set; }

			public static Delegates.VertexAttribL1dv VertexAttribL1dv { get; set; }

			public static Delegates.VertexAttribL2d VertexAttribL2d { get; set; }

			public static Delegates.VertexAttribL2dv VertexAttribL2dv { get; set; }

			public static Delegates.VertexAttribL3d VertexAttribL3d { get; set; }

			public static Delegates.VertexAttribL3dv VertexAttribL3dv { get; set; }

			public static Delegates.VertexAttribL4d VertexAttribL4d { get; set; }

			public static Delegates.VertexAttribL4dv VertexAttribL4dv { get; set; }

			public static Delegates.VertexAttribLFormat VertexAttribLFormat { get; set; }

			public static Delegates.VertexAttribLPointer VertexAttribLPointer { get; set; }

			public static Delegates.VertexAttribP1ui VertexAttribP1ui { get; set; }

			public static Delegates.VertexAttribP1uiv VertexAttribP1uiv { get; set; }

			public static Delegates.VertexAttribP2ui VertexAttribP2ui { get; set; }

			public static Delegates.VertexAttribP2uiv VertexAttribP2uiv { get; set; }

			public static Delegates.VertexAttribP3ui VertexAttribP3ui { get; set; }

			public static Delegates.VertexAttribP3uiv VertexAttribP3uiv { get; set; }

			public static Delegates.VertexAttribP4ui VertexAttribP4ui { get; set; }

			public static Delegates.VertexAttribP4uiv VertexAttribP4uiv { get; set; }

			public static Delegates.VertexAttribPointer VertexAttribPointer { get; set; }

			public static Delegates.VertexBindingDivisor VertexBindingDivisor { get; set; }

			public static Delegates.Viewport Viewport { get; set; }

			public static Delegates.ViewportArrayv ViewportArrayv { get; set; }

			public static Delegates.ViewportIndexedf ViewportIndexedf { get; set; }

			public static Delegates.ViewportIndexedfv ViewportIndexedfv { get; set; }

			public static Delegates.WaitSync WaitSync { get; set; }

		}

#if !GLDOTNET_EXCLUDE_GLINIT
		public static void Init(Func<string, IntPtr> getProcAddress, double version)
		{
			// Setup version acessor
			if (Version < version) { Version = version; }


			if (getProcAddress == null) throw new ArgumentNullException(nameof(getProcAddress));

			T getProc<T>(string name) => Marshal.GetDelegateForFunctionPointer<T>(getProcAddress("gl" + name));

			if (version >= 1.0)
			{
				Functions.BlendFunc = getProc<Delegates.BlendFunc>("BlendFunc");
				Functions.Clear = getProc<Delegates.Clear>("Clear");
				Functions.ClearColour = getProc<Delegates.ClearColour>("ClearColour");
				Functions.ClearDepth = getProc<Delegates.ClearDepth>("ClearDepth");
				Functions.ClearStencil = getProc<Delegates.ClearStencil>("ClearStencil");
				Functions.ColourMask = getProc<Delegates.ColourMask>("ColourMask");
				Functions.CullFace = getProc<Delegates.CullFace>("CullFace");
				Functions.DepthFunc = getProc<Delegates.DepthFunc>("DepthFunc");
				Functions.DepthMask = getProc<Delegates.DepthMask>("DepthMask");
				Functions.DepthRange = getProc<Delegates.DepthRange>("DepthRange");
				Functions.Disable = getProc<Delegates.Disable>("Disable");
				Functions.DrawBuffer = getProc<Delegates.DrawBuffer>("DrawBuffer");
				Functions.Enable = getProc<Delegates.Enable>("Enable");
				Functions.Finish = getProc<Delegates.Finish>("Finish");
				Functions.Flush = getProc<Delegates.Flush>("Flush");
				Functions.FrontFace = getProc<Delegates.FrontFace>("FrontFace");
				Functions.GetBooleanv = getProc<Delegates.GetBooleanv>("GetBooleanv");
				Functions.GetDoublev = getProc<Delegates.GetDoublev>("GetDoublev");
				Functions.GetError = getProc<Delegates.GetError>("GetError");
				Functions.GetFloatv = getProc<Delegates.GetFloatv>("GetFloatv");
				Functions.GetIntegerv = getProc<Delegates.GetIntegerv>("GetIntegerv");
				Functions.GetString = getProc<Delegates.GetString>("GetString");
				Functions.GetTexImage = getProc<Delegates.GetTexImage>("GetTexImage");
				Functions.GetTexLevelParameterfv = getProc<Delegates.GetTexLevelParameterfv>("GetTexLevelParameterfv");
				Functions.GetTexLevelParameteriv = getProc<Delegates.GetTexLevelParameteriv>("GetTexLevelParameteriv");
				Functions.GetTexParameterfv = getProc<Delegates.GetTexParameterfv>("GetTexParameterfv");
				Functions.GetTexParameteriv = getProc<Delegates.GetTexParameteriv>("GetTexParameteriv");
				Functions.Hint = getProc<Delegates.Hint>("Hint");
				Functions.IsEnabled = getProc<Delegates.IsEnabled>("IsEnabled");
				Functions.LineWidth = getProc<Delegates.LineWidth>("LineWidth");
				Functions.LogicOp = getProc<Delegates.LogicOp>("LogicOp");
				Functions.PixelStoref = getProc<Delegates.PixelStoref>("PixelStoref");
				Functions.PixelStorei = getProc<Delegates.PixelStorei>("PixelStorei");
				Functions.PointSize = getProc<Delegates.PointSize>("PointSize");
				Functions.PolygonMode = getProc<Delegates.PolygonMode>("PolygonMode");
				Functions.ReadBuffer = getProc<Delegates.ReadBuffer>("ReadBuffer");
				Functions.ReadPixels = getProc<Delegates.ReadPixels>("ReadPixels");
				Functions.Scissor = getProc<Delegates.Scissor>("Scissor");
				Functions.StencilFunc = getProc<Delegates.StencilFunc>("StencilFunc");
				Functions.StencilMask = getProc<Delegates.StencilMask>("StencilMask");
				Functions.StencilOp = getProc<Delegates.StencilOp>("StencilOp");
				Functions.TexImage1D = getProc<Delegates.TexImage1D>("TexImage1D");
				Functions.TexImage2D = getProc<Delegates.TexImage2D>("TexImage2D");
				Functions.TexParameterf = getProc<Delegates.TexParameterf>("TexParameterf");
				Functions.TexParameterfv = getProc<Delegates.TexParameterfv>("TexParameterfv");
				Functions.TexParameteri = getProc<Delegates.TexParameteri>("TexParameteri");
				Functions.TexParameteriv = getProc<Delegates.TexParameteriv>("TexParameteriv");
				Functions.Viewport = getProc<Delegates.Viewport>("Viewport");
			}

			if (version >= 1.1)
			{
				Functions.BindTexture = getProc<Delegates.BindTexture>("BindTexture");
				Functions.CopyTexImage1D = getProc<Delegates.CopyTexImage1D>("CopyTexImage1D");
				Functions.CopyTexImage2D = getProc<Delegates.CopyTexImage2D>("CopyTexImage2D");
				Functions.CopyTexSubImage1D = getProc<Delegates.CopyTexSubImage1D>("CopyTexSubImage1D");
				Functions.CopyTexSubImage2D = getProc<Delegates.CopyTexSubImage2D>("CopyTexSubImage2D");
				Functions.DeleteTextures = getProc<Delegates.DeleteTextures>("DeleteTextures");
				Functions.DrawArrays = getProc<Delegates.DrawArrays>("DrawArrays");
				Functions.DrawElements = getProc<Delegates.DrawElements>("DrawElements");
				Functions.GenTextures = getProc<Delegates.GenTextures>("GenTextures");
				Functions.GetPointerv = getProc<Delegates.GetPointerv>("GetPointerv");
				Functions.IsTexture = getProc<Delegates.IsTexture>("IsTexture");
				Functions.PolygonOffset = getProc<Delegates.PolygonOffset>("PolygonOffset");
				Functions.TexSubImage1D = getProc<Delegates.TexSubImage1D>("TexSubImage1D");
				Functions.TexSubImage2D = getProc<Delegates.TexSubImage2D>("TexSubImage2D");
			}

			if (version >= 1.2)
			{
				Functions.CopyTexSubImage3D = getProc<Delegates.CopyTexSubImage3D>("CopyTexSubImage3D");
				Functions.DrawRangeElements = getProc<Delegates.DrawRangeElements>("DrawRangeElements");
				Functions.TexImage3D = getProc<Delegates.TexImage3D>("TexImage3D");
				Functions.TexSubImage3D = getProc<Delegates.TexSubImage3D>("TexSubImage3D");
			}

			if (version >= 1.3)
			{
				Functions.ActiveTexture = getProc<Delegates.ActiveTexture>("ActiveTexture");
				Functions.CompressedTexImage1D = getProc<Delegates.CompressedTexImage1D>("CompressedTexImage1D");
				Functions.CompressedTexImage2D = getProc<Delegates.CompressedTexImage2D>("CompressedTexImage2D");
				Functions.CompressedTexImage3D = getProc<Delegates.CompressedTexImage3D>("CompressedTexImage3D");
				Functions.CompressedTexSubImage1D = getProc<Delegates.CompressedTexSubImage1D>("CompressedTexSubImage1D");
				Functions.CompressedTexSubImage2D = getProc<Delegates.CompressedTexSubImage2D>("CompressedTexSubImage2D");
				Functions.CompressedTexSubImage3D = getProc<Delegates.CompressedTexSubImage3D>("CompressedTexSubImage3D");
				Functions.GetCompressedTexImage = getProc<Delegates.GetCompressedTexImage>("GetCompressedTexImage");
				Functions.SampleCoverage = getProc<Delegates.SampleCoverage>("SampleCoverage");
			}

			if (version >= 1.4)
			{
				Functions.BlendColour = getProc<Delegates.BlendColour>("BlendColour");
				Functions.BlendEquation = getProc<Delegates.BlendEquation>("BlendEquation");
				Functions.BlendFuncSeparate = getProc<Delegates.BlendFuncSeparate>("BlendFuncSeparate");
				Functions.MultiDrawArrays = getProc<Delegates.MultiDrawArrays>("MultiDrawArrays");
				Functions.MultiDrawElements = getProc<Delegates.MultiDrawElements>("MultiDrawElements");
				Functions.PointParameterf = getProc<Delegates.PointParameterf>("PointParameterf");
				Functions.PointParameterfv = getProc<Delegates.PointParameterfv>("PointParameterfv");
				Functions.PointParameteri = getProc<Delegates.PointParameteri>("PointParameteri");
				Functions.PointParameteriv = getProc<Delegates.PointParameteriv>("PointParameteriv");
			}

			if (version >= 1.5)
			{
				Functions.BeginQuery = getProc<Delegates.BeginQuery>("BeginQuery");
				Functions.BindBuffer = getProc<Delegates.BindBuffer>("BindBuffer");
				Functions.BufferData = getProc<Delegates.BufferData>("BufferData");
				Functions.BufferSubData = getProc<Delegates.BufferSubData>("BufferSubData");
				Functions.DeleteBuffers = getProc<Delegates.DeleteBuffers>("DeleteBuffers");
				Functions.DeleteQueries = getProc<Delegates.DeleteQueries>("DeleteQueries");
				Functions.EndQuery = getProc<Delegates.EndQuery>("EndQuery");
				Functions.GenBuffers = getProc<Delegates.GenBuffers>("GenBuffers");
				Functions.GenQueries = getProc<Delegates.GenQueries>("GenQueries");
				Functions.GetBufferParameteriv = getProc<Delegates.GetBufferParameteriv>("GetBufferParameteriv");
				Functions.GetBufferPointerv = getProc<Delegates.GetBufferPointerv>("GetBufferPointerv");
				Functions.GetBufferSubData = getProc<Delegates.GetBufferSubData>("GetBufferSubData");
				Functions.GetQueryiv = getProc<Delegates.GetQueryiv>("GetQueryiv");
				Functions.GetQueryObjectiv = getProc<Delegates.GetQueryObjectiv>("GetQueryObjectiv");
				Functions.GetQueryObjectuiv = getProc<Delegates.GetQueryObjectuiv>("GetQueryObjectuiv");
				Functions.IsBuffer = getProc<Delegates.IsBuffer>("IsBuffer");
				Functions.IsQuery = getProc<Delegates.IsQuery>("IsQuery");
				Functions.MapBuffer = getProc<Delegates.MapBuffer>("MapBuffer");
				Functions.UnmapBuffer = getProc<Delegates.UnmapBuffer>("UnmapBuffer");
			}

			if (version >= 2.0)
			{
				Functions.AttachShader = getProc<Delegates.AttachShader>("AttachShader");
				Functions.BindAttribLocation = getProc<Delegates.BindAttribLocation>("BindAttribLocation");
				Functions.BlendEquationSeparate = getProc<Delegates.BlendEquationSeparate>("BlendEquationSeparate");
				Functions.CompileShader = getProc<Delegates.CompileShader>("CompileShader");
				Functions.CreateProgram = getProc<Delegates.CreateProgram>("CreateProgram");
				Functions.CreateShader = getProc<Delegates.CreateShader>("CreateShader");
				Functions.DeleteProgram = getProc<Delegates.DeleteProgram>("DeleteProgram");
				Functions.DeleteShader = getProc<Delegates.DeleteShader>("DeleteShader");
				Functions.DetachShader = getProc<Delegates.DetachShader>("DetachShader");
				Functions.DisableVertexAttribArray = getProc<Delegates.DisableVertexAttribArray>("DisableVertexAttribArray");
				Functions.DrawBuffers = getProc<Delegates.DrawBuffers>("DrawBuffers");
				Functions.EnableVertexAttribArray = getProc<Delegates.EnableVertexAttribArray>("EnableVertexAttribArray");
				Functions.GetActiveAttrib = getProc<Delegates.GetActiveAttrib>("GetActiveAttrib");
				Functions.GetActiveUniform = getProc<Delegates.GetActiveUniform>("GetActiveUniform");
				Functions.GetAttachedShaders = getProc<Delegates.GetAttachedShaders>("GetAttachedShaders");
				Functions.GetAttribLocation = getProc<Delegates.GetAttribLocation>("GetAttribLocation");
				Functions.GetProgramInfoLog = getProc<Delegates.GetProgramInfoLog>("GetProgramInfoLog");
				Functions.GetProgramiv = getProc<Delegates.GetProgramiv>("GetProgramiv");
				Functions.GetShaderInfoLog = getProc<Delegates.GetShaderInfoLog>("GetShaderInfoLog");
				Functions.GetShaderiv = getProc<Delegates.GetShaderiv>("GetShaderiv");
				Functions.GetShaderSource = getProc<Delegates.GetShaderSource>("GetShaderSource");
				Functions.GetUniformfv = getProc<Delegates.GetUniformfv>("GetUniformfv");
				Functions.GetUniformiv = getProc<Delegates.GetUniformiv>("GetUniformiv");
				Functions.GetUniformLocation = getProc<Delegates.GetUniformLocation>("GetUniformLocation");
				Functions.GetVertexAttribdv = getProc<Delegates.GetVertexAttribdv>("GetVertexAttribdv");
				Functions.GetVertexAttribfv = getProc<Delegates.GetVertexAttribfv>("GetVertexAttribfv");
				Functions.GetVertexAttribiv = getProc<Delegates.GetVertexAttribiv>("GetVertexAttribiv");
				Functions.GetVertexAttribPointerv = getProc<Delegates.GetVertexAttribPointerv>("GetVertexAttribPointerv");
				Functions.IsProgram = getProc<Delegates.IsProgram>("IsProgram");
				Functions.IsShader = getProc<Delegates.IsShader>("IsShader");
				Functions.LinkProgram = getProc<Delegates.LinkProgram>("LinkProgram");
				Functions.ShaderSource = getProc<Delegates.ShaderSource>("ShaderSource");
				Functions.StencilFuncSeparate = getProc<Delegates.StencilFuncSeparate>("StencilFuncSeparate");
				Functions.StencilMaskSeparate = getProc<Delegates.StencilMaskSeparate>("StencilMaskSeparate");
				Functions.StencilOpSeparate = getProc<Delegates.StencilOpSeparate>("StencilOpSeparate");
				Functions.Uniform1f = getProc<Delegates.Uniform1f>("Uniform1f");
				Functions.Uniform1fv = getProc<Delegates.Uniform1fv>("Uniform1fv");
				Functions.Uniform1i = getProc<Delegates.Uniform1i>("Uniform1i");
				Functions.Uniform1iv = getProc<Delegates.Uniform1iv>("Uniform1iv");
				Functions.Uniform2f = getProc<Delegates.Uniform2f>("Uniform2f");
				Functions.Uniform2fv = getProc<Delegates.Uniform2fv>("Uniform2fv");
				Functions.Uniform2i = getProc<Delegates.Uniform2i>("Uniform2i");
				Functions.Uniform2iv = getProc<Delegates.Uniform2iv>("Uniform2iv");
				Functions.Uniform3f = getProc<Delegates.Uniform3f>("Uniform3f");
				Functions.Uniform3fv = getProc<Delegates.Uniform3fv>("Uniform3fv");
				Functions.Uniform3i = getProc<Delegates.Uniform3i>("Uniform3i");
				Functions.Uniform3iv = getProc<Delegates.Uniform3iv>("Uniform3iv");
				Functions.Uniform4f = getProc<Delegates.Uniform4f>("Uniform4f");
				Functions.Uniform4fv = getProc<Delegates.Uniform4fv>("Uniform4fv");
				Functions.Uniform4i = getProc<Delegates.Uniform4i>("Uniform4i");
				Functions.Uniform4iv = getProc<Delegates.Uniform4iv>("Uniform4iv");
				Functions.UniformMatrix2fv = getProc<Delegates.UniformMatrix2fv>("UniformMatrix2fv");
				Functions.UniformMatrix3fv = getProc<Delegates.UniformMatrix3fv>("UniformMatrix3fv");
				Functions.UniformMatrix4fv = getProc<Delegates.UniformMatrix4fv>("UniformMatrix4fv");
				Functions.UseProgram = getProc<Delegates.UseProgram>("UseProgram");
				Functions.ValidateProgram = getProc<Delegates.ValidateProgram>("ValidateProgram");
				Functions.VertexAttrib1d = getProc<Delegates.VertexAttrib1d>("VertexAttrib1d");
				Functions.VertexAttrib1dv = getProc<Delegates.VertexAttrib1dv>("VertexAttrib1dv");
				Functions.VertexAttrib1f = getProc<Delegates.VertexAttrib1f>("VertexAttrib1f");
				Functions.VertexAttrib1fv = getProc<Delegates.VertexAttrib1fv>("VertexAttrib1fv");
				Functions.VertexAttrib1s = getProc<Delegates.VertexAttrib1s>("VertexAttrib1s");
				Functions.VertexAttrib1sv = getProc<Delegates.VertexAttrib1sv>("VertexAttrib1sv");
				Functions.VertexAttrib2d = getProc<Delegates.VertexAttrib2d>("VertexAttrib2d");
				Functions.VertexAttrib2dv = getProc<Delegates.VertexAttrib2dv>("VertexAttrib2dv");
				Functions.VertexAttrib2f = getProc<Delegates.VertexAttrib2f>("VertexAttrib2f");
				Functions.VertexAttrib2fv = getProc<Delegates.VertexAttrib2fv>("VertexAttrib2fv");
				Functions.VertexAttrib2s = getProc<Delegates.VertexAttrib2s>("VertexAttrib2s");
				Functions.VertexAttrib2sv = getProc<Delegates.VertexAttrib2sv>("VertexAttrib2sv");
				Functions.VertexAttrib3d = getProc<Delegates.VertexAttrib3d>("VertexAttrib3d");
				Functions.VertexAttrib3dv = getProc<Delegates.VertexAttrib3dv>("VertexAttrib3dv");
				Functions.VertexAttrib3f = getProc<Delegates.VertexAttrib3f>("VertexAttrib3f");
				Functions.VertexAttrib3fv = getProc<Delegates.VertexAttrib3fv>("VertexAttrib3fv");
				Functions.VertexAttrib3s = getProc<Delegates.VertexAttrib3s>("VertexAttrib3s");
				Functions.VertexAttrib3sv = getProc<Delegates.VertexAttrib3sv>("VertexAttrib3sv");
				Functions.VertexAttrib4bv = getProc<Delegates.VertexAttrib4bv>("VertexAttrib4bv");
				Functions.VertexAttrib4d = getProc<Delegates.VertexAttrib4d>("VertexAttrib4d");
				Functions.VertexAttrib4dv = getProc<Delegates.VertexAttrib4dv>("VertexAttrib4dv");
				Functions.VertexAttrib4f = getProc<Delegates.VertexAttrib4f>("VertexAttrib4f");
				Functions.VertexAttrib4fv = getProc<Delegates.VertexAttrib4fv>("VertexAttrib4fv");
				Functions.VertexAttrib4iv = getProc<Delegates.VertexAttrib4iv>("VertexAttrib4iv");
				Functions.VertexAttrib4Nbv = getProc<Delegates.VertexAttrib4Nbv>("VertexAttrib4Nbv");
				Functions.VertexAttrib4Niv = getProc<Delegates.VertexAttrib4Niv>("VertexAttrib4Niv");
				Functions.VertexAttrib4Nsv = getProc<Delegates.VertexAttrib4Nsv>("VertexAttrib4Nsv");
				Functions.VertexAttrib4Nub = getProc<Delegates.VertexAttrib4Nub>("VertexAttrib4Nub");
				Functions.VertexAttrib4Nubv = getProc<Delegates.VertexAttrib4Nubv>("VertexAttrib4Nubv");
				Functions.VertexAttrib4Nuiv = getProc<Delegates.VertexAttrib4Nuiv>("VertexAttrib4Nuiv");
				Functions.VertexAttrib4Nusv = getProc<Delegates.VertexAttrib4Nusv>("VertexAttrib4Nusv");
				Functions.VertexAttrib4s = getProc<Delegates.VertexAttrib4s>("VertexAttrib4s");
				Functions.VertexAttrib4sv = getProc<Delegates.VertexAttrib4sv>("VertexAttrib4sv");
				Functions.VertexAttrib4ubv = getProc<Delegates.VertexAttrib4ubv>("VertexAttrib4ubv");
				Functions.VertexAttrib4uiv = getProc<Delegates.VertexAttrib4uiv>("VertexAttrib4uiv");
				Functions.VertexAttrib4usv = getProc<Delegates.VertexAttrib4usv>("VertexAttrib4usv");
				Functions.VertexAttribPointer = getProc<Delegates.VertexAttribPointer>("VertexAttribPointer");
			}

			if (version >= 2.1)
			{
				Functions.UniformMatrix2x3fv = getProc<Delegates.UniformMatrix2x3fv>("UniformMatrix2x3fv");
				Functions.UniformMatrix2x4fv = getProc<Delegates.UniformMatrix2x4fv>("UniformMatrix2x4fv");
				Functions.UniformMatrix3x2fv = getProc<Delegates.UniformMatrix3x2fv>("UniformMatrix3x2fv");
				Functions.UniformMatrix3x4fv = getProc<Delegates.UniformMatrix3x4fv>("UniformMatrix3x4fv");
				Functions.UniformMatrix4x2fv = getProc<Delegates.UniformMatrix4x2fv>("UniformMatrix4x2fv");
				Functions.UniformMatrix4x3fv = getProc<Delegates.UniformMatrix4x3fv>("UniformMatrix4x3fv");
			}

			if (version >= 3.0)
			{
				Functions.BeginConditionalRender = getProc<Delegates.BeginConditionalRender>("BeginConditionalRender");
				Functions.BeginTransformFeedback = getProc<Delegates.BeginTransformFeedback>("BeginTransformFeedback");
				Functions.BindBufferBase = getProc<Delegates.BindBufferBase>("BindBufferBase");
				Functions.BindBufferRange = getProc<Delegates.BindBufferRange>("BindBufferRange");
				Functions.BindFragDataLocation = getProc<Delegates.BindFragDataLocation>("BindFragDataLocation");
				Functions.BindFramebuffer = getProc<Delegates.BindFramebuffer>("BindFramebuffer");
				Functions.BindRenderbuffer = getProc<Delegates.BindRenderbuffer>("BindRenderbuffer");
				Functions.BindVertexArray = getProc<Delegates.BindVertexArray>("BindVertexArray");
				Functions.BlitFramebuffer = getProc<Delegates.BlitFramebuffer>("BlitFramebuffer");
				Functions.CheckFramebufferStatus = getProc<Delegates.CheckFramebufferStatus>("CheckFramebufferStatus");
				Functions.ClampColour = getProc<Delegates.ClampColour>("ClampColour");
				Functions.ClearBufferfi = getProc<Delegates.ClearBufferfi>("ClearBufferfi");
				Functions.ClearBufferfv = getProc<Delegates.ClearBufferfv>("ClearBufferfv");
				Functions.ClearBufferiv = getProc<Delegates.ClearBufferiv>("ClearBufferiv");
				Functions.ClearBufferuiv = getProc<Delegates.ClearBufferuiv>("ClearBufferuiv");
				Functions.ColourMaski = getProc<Delegates.ColourMaski>("ColourMaski");
				Functions.DeleteFramebuffers = getProc<Delegates.DeleteFramebuffers>("DeleteFramebuffers");
				Functions.DeleteRenderbuffers = getProc<Delegates.DeleteRenderbuffers>("DeleteRenderbuffers");
				Functions.DeleteVertexArrays = getProc<Delegates.DeleteVertexArrays>("DeleteVertexArrays");
				Functions.Disablei = getProc<Delegates.Disablei>("Disablei");
				Functions.Enablei = getProc<Delegates.Enablei>("Enablei");
				Functions.EndConditionalRender = getProc<Delegates.EndConditionalRender>("EndConditionalRender");
				Functions.EndTransformFeedback = getProc<Delegates.EndTransformFeedback>("EndTransformFeedback");
				Functions.FlushMappedBufferRange = getProc<Delegates.FlushMappedBufferRange>("FlushMappedBufferRange");
				Functions.FramebufferRenderbuffer = getProc<Delegates.FramebufferRenderbuffer>("FramebufferRenderbuffer");
				Functions.FramebufferTexture1D = getProc<Delegates.FramebufferTexture1D>("FramebufferTexture1D");
				Functions.FramebufferTexture2D = getProc<Delegates.FramebufferTexture2D>("FramebufferTexture2D");
				Functions.FramebufferTexture3D = getProc<Delegates.FramebufferTexture3D>("FramebufferTexture3D");
				Functions.FramebufferTextureLayer = getProc<Delegates.FramebufferTextureLayer>("FramebufferTextureLayer");
				Functions.GenerateMipmap = getProc<Delegates.GenerateMipmap>("GenerateMipmap");
				Functions.GenFramebuffers = getProc<Delegates.GenFramebuffers>("GenFramebuffers");
				Functions.GenRenderbuffers = getProc<Delegates.GenRenderbuffers>("GenRenderbuffers");
				Functions.GenVertexArrays = getProc<Delegates.GenVertexArrays>("GenVertexArrays");
				Functions.GetBooleani_v = getProc<Delegates.GetBooleani_v>("GetBooleani_v");
				Functions.GetFragDataLocation = getProc<Delegates.GetFragDataLocation>("GetFragDataLocation");
				Functions.GetFramebufferAttachmentParameteriv = getProc<Delegates.GetFramebufferAttachmentParameteriv>("GetFramebufferAttachmentParameteriv");
				Functions.GetIntegeri_v = getProc<Delegates.GetIntegeri_v>("GetIntegeri_v");
				Functions.GetRenderbufferParameteriv = getProc<Delegates.GetRenderbufferParameteriv>("GetRenderbufferParameteriv");
				Functions.GetStringi = getProc<Delegates.GetStringi>("GetStringi");
				Functions.GetTexParameterIiv = getProc<Delegates.GetTexParameterIiv>("GetTexParameterIiv");
				Functions.GetTexParameterIuiv = getProc<Delegates.GetTexParameterIuiv>("GetTexParameterIuiv");
				Functions.GetTransformFeedbackVarying = getProc<Delegates.GetTransformFeedbackVarying>("GetTransformFeedbackVarying");
				Functions.GetUniformuiv = getProc<Delegates.GetUniformuiv>("GetUniformuiv");
				Functions.GetVertexAttribIiv = getProc<Delegates.GetVertexAttribIiv>("GetVertexAttribIiv");
				Functions.GetVertexAttribIuiv = getProc<Delegates.GetVertexAttribIuiv>("GetVertexAttribIuiv");
				Functions.IsEnabledi = getProc<Delegates.IsEnabledi>("IsEnabledi");
				Functions.IsFramebuffer = getProc<Delegates.IsFramebuffer>("IsFramebuffer");
				Functions.IsRenderbuffer = getProc<Delegates.IsRenderbuffer>("IsRenderbuffer");
				Functions.IsVertexArray = getProc<Delegates.IsVertexArray>("IsVertexArray");
				Functions.MapBufferRange = getProc<Delegates.MapBufferRange>("MapBufferRange");
				Functions.RenderbufferStorage = getProc<Delegates.RenderbufferStorage>("RenderbufferStorage");
				Functions.RenderbufferStorageMultisample = getProc<Delegates.RenderbufferStorageMultisample>("RenderbufferStorageMultisample");
				Functions.TexParameterIiv = getProc<Delegates.TexParameterIiv>("TexParameterIiv");
				Functions.TexParameterIuiv = getProc<Delegates.TexParameterIuiv>("TexParameterIuiv");
				Functions.TransformFeedbackVaryings = getProc<Delegates.TransformFeedbackVaryings>("TransformFeedbackVaryings");
				Functions.Uniform1ui = getProc<Delegates.Uniform1ui>("Uniform1ui");
				Functions.Uniform1uiv = getProc<Delegates.Uniform1uiv>("Uniform1uiv");
				Functions.Uniform2ui = getProc<Delegates.Uniform2ui>("Uniform2ui");
				Functions.Uniform2uiv = getProc<Delegates.Uniform2uiv>("Uniform2uiv");
				Functions.Uniform3ui = getProc<Delegates.Uniform3ui>("Uniform3ui");
				Functions.Uniform3uiv = getProc<Delegates.Uniform3uiv>("Uniform3uiv");
				Functions.Uniform4ui = getProc<Delegates.Uniform4ui>("Uniform4ui");
				Functions.Uniform4uiv = getProc<Delegates.Uniform4uiv>("Uniform4uiv");
				Functions.VertexAttribI1i = getProc<Delegates.VertexAttribI1i>("VertexAttribI1i");
				Functions.VertexAttribI1iv = getProc<Delegates.VertexAttribI1iv>("VertexAttribI1iv");
				Functions.VertexAttribI1ui = getProc<Delegates.VertexAttribI1ui>("VertexAttribI1ui");
				Functions.VertexAttribI1uiv = getProc<Delegates.VertexAttribI1uiv>("VertexAttribI1uiv");
				Functions.VertexAttribI2i = getProc<Delegates.VertexAttribI2i>("VertexAttribI2i");
				Functions.VertexAttribI2iv = getProc<Delegates.VertexAttribI2iv>("VertexAttribI2iv");
				Functions.VertexAttribI2ui = getProc<Delegates.VertexAttribI2ui>("VertexAttribI2ui");
				Functions.VertexAttribI2uiv = getProc<Delegates.VertexAttribI2uiv>("VertexAttribI2uiv");
				Functions.VertexAttribI3i = getProc<Delegates.VertexAttribI3i>("VertexAttribI3i");
				Functions.VertexAttribI3iv = getProc<Delegates.VertexAttribI3iv>("VertexAttribI3iv");
				Functions.VertexAttribI3ui = getProc<Delegates.VertexAttribI3ui>("VertexAttribI3ui");
				Functions.VertexAttribI3uiv = getProc<Delegates.VertexAttribI3uiv>("VertexAttribI3uiv");
				Functions.VertexAttribI4bv = getProc<Delegates.VertexAttribI4bv>("VertexAttribI4bv");
				Functions.VertexAttribI4i = getProc<Delegates.VertexAttribI4i>("VertexAttribI4i");
				Functions.VertexAttribI4iv = getProc<Delegates.VertexAttribI4iv>("VertexAttribI4iv");
				Functions.VertexAttribI4sv = getProc<Delegates.VertexAttribI4sv>("VertexAttribI4sv");
				Functions.VertexAttribI4ubv = getProc<Delegates.VertexAttribI4ubv>("VertexAttribI4ubv");
				Functions.VertexAttribI4ui = getProc<Delegates.VertexAttribI4ui>("VertexAttribI4ui");
				Functions.VertexAttribI4uiv = getProc<Delegates.VertexAttribI4uiv>("VertexAttribI4uiv");
				Functions.VertexAttribI4usv = getProc<Delegates.VertexAttribI4usv>("VertexAttribI4usv");
				Functions.VertexAttribIPointer = getProc<Delegates.VertexAttribIPointer>("VertexAttribIPointer");
			}

			if (version >= 3.1)
			{
				Functions.CopyBufferSubData = getProc<Delegates.CopyBufferSubData>("CopyBufferSubData");
				Functions.DrawArraysInstanced = getProc<Delegates.DrawArraysInstanced>("DrawArraysInstanced");
				Functions.DrawElementsInstanced = getProc<Delegates.DrawElementsInstanced>("DrawElementsInstanced");
				Functions.GetActiveUniformBlockiv = getProc<Delegates.GetActiveUniformBlockiv>("GetActiveUniformBlockiv");
				Functions.GetActiveUniformBlockName = getProc<Delegates.GetActiveUniformBlockName>("GetActiveUniformBlockName");
				Functions.GetActiveUniformName = getProc<Delegates.GetActiveUniformName>("GetActiveUniformName");
				Functions.GetActiveUniformsiv = getProc<Delegates.GetActiveUniformsiv>("GetActiveUniformsiv");
				Functions.GetUniformBlockIndex = getProc<Delegates.GetUniformBlockIndex>("GetUniformBlockIndex");
				Functions.GetUniformIndices = getProc<Delegates.GetUniformIndices>("GetUniformIndices");
				Functions.PrimitiveRestartIndex = getProc<Delegates.PrimitiveRestartIndex>("PrimitiveRestartIndex");
				Functions.TexBuffer = getProc<Delegates.TexBuffer>("TexBuffer");
				Functions.UniformBlockBinding = getProc<Delegates.UniformBlockBinding>("UniformBlockBinding");
			}

			if (version >= 3.2)
			{
				Functions.ClientWaitSync = getProc<Delegates.ClientWaitSync>("ClientWaitSync");
				Functions.DeleteSync = getProc<Delegates.DeleteSync>("DeleteSync");
				Functions.DrawElementsBaseVertex = getProc<Delegates.DrawElementsBaseVertex>("DrawElementsBaseVertex");
				Functions.DrawElementsInstancedBaseVertex = getProc<Delegates.DrawElementsInstancedBaseVertex>("DrawElementsInstancedBaseVertex");
				Functions.DrawRangeElementsBaseVertex = getProc<Delegates.DrawRangeElementsBaseVertex>("DrawRangeElementsBaseVertex");
				Functions.FenceSync = getProc<Delegates.FenceSync>("FenceSync");
				Functions.FramebufferTexture = getProc<Delegates.FramebufferTexture>("FramebufferTexture");
				Functions.GetBufferParameteri64v = getProc<Delegates.GetBufferParameteri64v>("GetBufferParameteri64v");
				Functions.GetInteger64i_v = getProc<Delegates.GetInteger64i_v>("GetInteger64i_v");
				Functions.GetInteger64v = getProc<Delegates.GetInteger64v>("GetInteger64v");
				Functions.GetMultisamplefv = getProc<Delegates.GetMultisamplefv>("GetMultisamplefv");
				Functions.GetSynciv = getProc<Delegates.GetSynciv>("GetSynciv");
				Functions.IsSync = getProc<Delegates.IsSync>("IsSync");
				Functions.MultiDrawElementsBaseVertex = getProc<Delegates.MultiDrawElementsBaseVertex>("MultiDrawElementsBaseVertex");
				Functions.ProvokingVertex = getProc<Delegates.ProvokingVertex>("ProvokingVertex");
				Functions.SampleMaski = getProc<Delegates.SampleMaski>("SampleMaski");
				Functions.TexImage2DMultisample = getProc<Delegates.TexImage2DMultisample>("TexImage2DMultisample");
				Functions.TexImage3DMultisample = getProc<Delegates.TexImage3DMultisample>("TexImage3DMultisample");
				Functions.WaitSync = getProc<Delegates.WaitSync>("WaitSync");
			}

			if (version >= 3.3)
			{
				Functions.BindFragDataLocationIndexed = getProc<Delegates.BindFragDataLocationIndexed>("BindFragDataLocationIndexed");
				Functions.BindSampler = getProc<Delegates.BindSampler>("BindSampler");
				Functions.DeleteSamplers = getProc<Delegates.DeleteSamplers>("DeleteSamplers");
				Functions.GenSamplers = getProc<Delegates.GenSamplers>("GenSamplers");
				Functions.GetFragDataIndex = getProc<Delegates.GetFragDataIndex>("GetFragDataIndex");
				Functions.GetQueryObjecti64v = getProc<Delegates.GetQueryObjecti64v>("GetQueryObjecti64v");
				Functions.GetQueryObjectui64v = getProc<Delegates.GetQueryObjectui64v>("GetQueryObjectui64v");
				Functions.GetSamplerParameterfv = getProc<Delegates.GetSamplerParameterfv>("GetSamplerParameterfv");
				Functions.GetSamplerParameterIiv = getProc<Delegates.GetSamplerParameterIiv>("GetSamplerParameterIiv");
				Functions.GetSamplerParameterIuiv = getProc<Delegates.GetSamplerParameterIuiv>("GetSamplerParameterIuiv");
				Functions.GetSamplerParameteriv = getProc<Delegates.GetSamplerParameteriv>("GetSamplerParameteriv");
				Functions.IsSampler = getProc<Delegates.IsSampler>("IsSampler");
				Functions.QueryCounter = getProc<Delegates.QueryCounter>("QueryCounter");
				Functions.SamplerParameterf = getProc<Delegates.SamplerParameterf>("SamplerParameterf");
				Functions.SamplerParameterfv = getProc<Delegates.SamplerParameterfv>("SamplerParameterfv");
				Functions.SamplerParameteri = getProc<Delegates.SamplerParameteri>("SamplerParameteri");
				Functions.SamplerParameterIiv = getProc<Delegates.SamplerParameterIiv>("SamplerParameterIiv");
				Functions.SamplerParameterIuiv = getProc<Delegates.SamplerParameterIuiv>("SamplerParameterIuiv");
				Functions.SamplerParameteriv = getProc<Delegates.SamplerParameteriv>("SamplerParameteriv");
				Functions.VertexAttribDivisor = getProc<Delegates.VertexAttribDivisor>("VertexAttribDivisor");
				Functions.VertexAttribP1ui = getProc<Delegates.VertexAttribP1ui>("VertexAttribP1ui");
				Functions.VertexAttribP1uiv = getProc<Delegates.VertexAttribP1uiv>("VertexAttribP1uiv");
				Functions.VertexAttribP2ui = getProc<Delegates.VertexAttribP2ui>("VertexAttribP2ui");
				Functions.VertexAttribP2uiv = getProc<Delegates.VertexAttribP2uiv>("VertexAttribP2uiv");
				Functions.VertexAttribP3ui = getProc<Delegates.VertexAttribP3ui>("VertexAttribP3ui");
				Functions.VertexAttribP3uiv = getProc<Delegates.VertexAttribP3uiv>("VertexAttribP3uiv");
				Functions.VertexAttribP4ui = getProc<Delegates.VertexAttribP4ui>("VertexAttribP4ui");
				Functions.VertexAttribP4uiv = getProc<Delegates.VertexAttribP4uiv>("VertexAttribP4uiv");
			}

			if (version >= 4.0)
			{
				Functions.BeginQueryIndexed = getProc<Delegates.BeginQueryIndexed>("BeginQueryIndexed");
				Functions.BindTransformFeedback = getProc<Delegates.BindTransformFeedback>("BindTransformFeedback");
				Functions.BlendEquationi = getProc<Delegates.BlendEquationi>("BlendEquationi");
				Functions.BlendEquationSeparatei = getProc<Delegates.BlendEquationSeparatei>("BlendEquationSeparatei");
				Functions.BlendFunci = getProc<Delegates.BlendFunci>("BlendFunci");
				Functions.BlendFuncSeparatei = getProc<Delegates.BlendFuncSeparatei>("BlendFuncSeparatei");
				Functions.DeleteTransformFeedbacks = getProc<Delegates.DeleteTransformFeedbacks>("DeleteTransformFeedbacks");
				Functions.DrawArraysIndirect = getProc<Delegates.DrawArraysIndirect>("DrawArraysIndirect");
				Functions.DrawElementsIndirect = getProc<Delegates.DrawElementsIndirect>("DrawElementsIndirect");
				Functions.DrawTransformFeedback = getProc<Delegates.DrawTransformFeedback>("DrawTransformFeedback");
				Functions.DrawTransformFeedbackStream = getProc<Delegates.DrawTransformFeedbackStream>("DrawTransformFeedbackStream");
				Functions.EndQueryIndexed = getProc<Delegates.EndQueryIndexed>("EndQueryIndexed");
				Functions.GenTransformFeedbacks = getProc<Delegates.GenTransformFeedbacks>("GenTransformFeedbacks");
				Functions.GetActiveSubroutineName = getProc<Delegates.GetActiveSubroutineName>("GetActiveSubroutineName");
				Functions.GetActiveSubroutineUniformiv = getProc<Delegates.GetActiveSubroutineUniformiv>("GetActiveSubroutineUniformiv");
				Functions.GetActiveSubroutineUniformName = getProc<Delegates.GetActiveSubroutineUniformName>("GetActiveSubroutineUniformName");
				Functions.GetProgramStageiv = getProc<Delegates.GetProgramStageiv>("GetProgramStageiv");
				Functions.GetQueryIndexediv = getProc<Delegates.GetQueryIndexediv>("GetQueryIndexediv");
				Functions.GetSubroutineIndex = getProc<Delegates.GetSubroutineIndex>("GetSubroutineIndex");
				Functions.GetSubroutineUniformLocation = getProc<Delegates.GetSubroutineUniformLocation>("GetSubroutineUniformLocation");
				Functions.GetUniformdv = getProc<Delegates.GetUniformdv>("GetUniformdv");
				Functions.GetUniformSubroutineuiv = getProc<Delegates.GetUniformSubroutineuiv>("GetUniformSubroutineuiv");
				Functions.IsTransformFeedback = getProc<Delegates.IsTransformFeedback>("IsTransformFeedback");
				Functions.MinSampleShading = getProc<Delegates.MinSampleShading>("MinSampleShading");
				Functions.PatchParameterfv = getProc<Delegates.PatchParameterfv>("PatchParameterfv");
				Functions.PatchParameteri = getProc<Delegates.PatchParameteri>("PatchParameteri");
				Functions.PauseTransformFeedback = getProc<Delegates.PauseTransformFeedback>("PauseTransformFeedback");
				Functions.ResumeTransformFeedback = getProc<Delegates.ResumeTransformFeedback>("ResumeTransformFeedback");
				Functions.Uniform1d = getProc<Delegates.Uniform1d>("Uniform1d");
				Functions.Uniform1dv = getProc<Delegates.Uniform1dv>("Uniform1dv");
				Functions.Uniform2d = getProc<Delegates.Uniform2d>("Uniform2d");
				Functions.Uniform2dv = getProc<Delegates.Uniform2dv>("Uniform2dv");
				Functions.Uniform3d = getProc<Delegates.Uniform3d>("Uniform3d");
				Functions.Uniform3dv = getProc<Delegates.Uniform3dv>("Uniform3dv");
				Functions.Uniform4d = getProc<Delegates.Uniform4d>("Uniform4d");
				Functions.Uniform4dv = getProc<Delegates.Uniform4dv>("Uniform4dv");
				Functions.UniformMatrix2dv = getProc<Delegates.UniformMatrix2dv>("UniformMatrix2dv");
				Functions.UniformMatrix2x3dv = getProc<Delegates.UniformMatrix2x3dv>("UniformMatrix2x3dv");
				Functions.UniformMatrix2x4dv = getProc<Delegates.UniformMatrix2x4dv>("UniformMatrix2x4dv");
				Functions.UniformMatrix3dv = getProc<Delegates.UniformMatrix3dv>("UniformMatrix3dv");
				Functions.UniformMatrix3x2dv = getProc<Delegates.UniformMatrix3x2dv>("UniformMatrix3x2dv");
				Functions.UniformMatrix3x4dv = getProc<Delegates.UniformMatrix3x4dv>("UniformMatrix3x4dv");
				Functions.UniformMatrix4dv = getProc<Delegates.UniformMatrix4dv>("UniformMatrix4dv");
				Functions.UniformMatrix4x2dv = getProc<Delegates.UniformMatrix4x2dv>("UniformMatrix4x2dv");
				Functions.UniformMatrix4x3dv = getProc<Delegates.UniformMatrix4x3dv>("UniformMatrix4x3dv");
				Functions.UniformSubroutinesuiv = getProc<Delegates.UniformSubroutinesuiv>("UniformSubroutinesuiv");
			}

			if (version >= 4.1)
			{
				Functions.ActiveShaderProgram = getProc<Delegates.ActiveShaderProgram>("ActiveShaderProgram");
				Functions.BindProgramPipeline = getProc<Delegates.BindProgramPipeline>("BindProgramPipeline");
				Functions.ClearDepthf = getProc<Delegates.ClearDepthf>("ClearDepthf");
				Functions.CreateShaderProgramv = getProc<Delegates.CreateShaderProgramv>("CreateShaderProgramv");
				Functions.DeleteProgramPipelines = getProc<Delegates.DeleteProgramPipelines>("DeleteProgramPipelines");
				Functions.DepthRangeArrayv = getProc<Delegates.DepthRangeArrayv>("DepthRangeArrayv");
				Functions.DepthRangef = getProc<Delegates.DepthRangef>("DepthRangef");
				Functions.DepthRangeIndexed = getProc<Delegates.DepthRangeIndexed>("DepthRangeIndexed");
				Functions.GenProgramPipelines = getProc<Delegates.GenProgramPipelines>("GenProgramPipelines");
				Functions.GetDoublei_v = getProc<Delegates.GetDoublei_v>("GetDoublei_v");
				Functions.GetFloati_v = getProc<Delegates.GetFloati_v>("GetFloati_v");
				Functions.GetProgramBinary = getProc<Delegates.GetProgramBinary>("GetProgramBinary");
				Functions.GetProgramPipelineInfoLog = getProc<Delegates.GetProgramPipelineInfoLog>("GetProgramPipelineInfoLog");
				Functions.GetProgramPipelineiv = getProc<Delegates.GetProgramPipelineiv>("GetProgramPipelineiv");
				Functions.GetShaderPrecisionFormat = getProc<Delegates.GetShaderPrecisionFormat>("GetShaderPrecisionFormat");
				Functions.GetVertexAttribLdv = getProc<Delegates.GetVertexAttribLdv>("GetVertexAttribLdv");
				Functions.IsProgramPipeline = getProc<Delegates.IsProgramPipeline>("IsProgramPipeline");
				Functions.ProgramBinary = getProc<Delegates.ProgramBinary>("ProgramBinary");
				Functions.ProgramParameteri = getProc<Delegates.ProgramParameteri>("ProgramParameteri");
				Functions.ProgramUniform1d = getProc<Delegates.ProgramUniform1d>("ProgramUniform1d");
				Functions.ProgramUniform1dv = getProc<Delegates.ProgramUniform1dv>("ProgramUniform1dv");
				Functions.ProgramUniform1f = getProc<Delegates.ProgramUniform1f>("ProgramUniform1f");
				Functions.ProgramUniform1fv = getProc<Delegates.ProgramUniform1fv>("ProgramUniform1fv");
				Functions.ProgramUniform1i = getProc<Delegates.ProgramUniform1i>("ProgramUniform1i");
				Functions.ProgramUniform1iv = getProc<Delegates.ProgramUniform1iv>("ProgramUniform1iv");
				Functions.ProgramUniform1ui = getProc<Delegates.ProgramUniform1ui>("ProgramUniform1ui");
				Functions.ProgramUniform1uiv = getProc<Delegates.ProgramUniform1uiv>("ProgramUniform1uiv");
				Functions.ProgramUniform2d = getProc<Delegates.ProgramUniform2d>("ProgramUniform2d");
				Functions.ProgramUniform2dv = getProc<Delegates.ProgramUniform2dv>("ProgramUniform2dv");
				Functions.ProgramUniform2f = getProc<Delegates.ProgramUniform2f>("ProgramUniform2f");
				Functions.ProgramUniform2fv = getProc<Delegates.ProgramUniform2fv>("ProgramUniform2fv");
				Functions.ProgramUniform2i = getProc<Delegates.ProgramUniform2i>("ProgramUniform2i");
				Functions.ProgramUniform2iv = getProc<Delegates.ProgramUniform2iv>("ProgramUniform2iv");
				Functions.ProgramUniform2ui = getProc<Delegates.ProgramUniform2ui>("ProgramUniform2ui");
				Functions.ProgramUniform2uiv = getProc<Delegates.ProgramUniform2uiv>("ProgramUniform2uiv");
				Functions.ProgramUniform3d = getProc<Delegates.ProgramUniform3d>("ProgramUniform3d");
				Functions.ProgramUniform3dv = getProc<Delegates.ProgramUniform3dv>("ProgramUniform3dv");
				Functions.ProgramUniform3f = getProc<Delegates.ProgramUniform3f>("ProgramUniform3f");
				Functions.ProgramUniform3fv = getProc<Delegates.ProgramUniform3fv>("ProgramUniform3fv");
				Functions.ProgramUniform3i = getProc<Delegates.ProgramUniform3i>("ProgramUniform3i");
				Functions.ProgramUniform3iv = getProc<Delegates.ProgramUniform3iv>("ProgramUniform3iv");
				Functions.ProgramUniform3ui = getProc<Delegates.ProgramUniform3ui>("ProgramUniform3ui");
				Functions.ProgramUniform3uiv = getProc<Delegates.ProgramUniform3uiv>("ProgramUniform3uiv");
				Functions.ProgramUniform4d = getProc<Delegates.ProgramUniform4d>("ProgramUniform4d");
				Functions.ProgramUniform4dv = getProc<Delegates.ProgramUniform4dv>("ProgramUniform4dv");
				Functions.ProgramUniform4f = getProc<Delegates.ProgramUniform4f>("ProgramUniform4f");
				Functions.ProgramUniform4fv = getProc<Delegates.ProgramUniform4fv>("ProgramUniform4fv");
				Functions.ProgramUniform4i = getProc<Delegates.ProgramUniform4i>("ProgramUniform4i");
				Functions.ProgramUniform4iv = getProc<Delegates.ProgramUniform4iv>("ProgramUniform4iv");
				Functions.ProgramUniform4ui = getProc<Delegates.ProgramUniform4ui>("ProgramUniform4ui");
				Functions.ProgramUniform4uiv = getProc<Delegates.ProgramUniform4uiv>("ProgramUniform4uiv");
				Functions.ProgramUniformMatrix2dv = getProc<Delegates.ProgramUniformMatrix2dv>("ProgramUniformMatrix2dv");
				Functions.ProgramUniformMatrix2fv = getProc<Delegates.ProgramUniformMatrix2fv>("ProgramUniformMatrix2fv");
				Functions.ProgramUniformMatrix2x3dv = getProc<Delegates.ProgramUniformMatrix2x3dv>("ProgramUniformMatrix2x3dv");
				Functions.ProgramUniformMatrix2x3fv = getProc<Delegates.ProgramUniformMatrix2x3fv>("ProgramUniformMatrix2x3fv");
				Functions.ProgramUniformMatrix2x4dv = getProc<Delegates.ProgramUniformMatrix2x4dv>("ProgramUniformMatrix2x4dv");
				Functions.ProgramUniformMatrix2x4fv = getProc<Delegates.ProgramUniformMatrix2x4fv>("ProgramUniformMatrix2x4fv");
				Functions.ProgramUniformMatrix3dv = getProc<Delegates.ProgramUniformMatrix3dv>("ProgramUniformMatrix3dv");
				Functions.ProgramUniformMatrix3fv = getProc<Delegates.ProgramUniformMatrix3fv>("ProgramUniformMatrix3fv");
				Functions.ProgramUniformMatrix3x2dv = getProc<Delegates.ProgramUniformMatrix3x2dv>("ProgramUniformMatrix3x2dv");
				Functions.ProgramUniformMatrix3x2fv = getProc<Delegates.ProgramUniformMatrix3x2fv>("ProgramUniformMatrix3x2fv");
				Functions.ProgramUniformMatrix3x4dv = getProc<Delegates.ProgramUniformMatrix3x4dv>("ProgramUniformMatrix3x4dv");
				Functions.ProgramUniformMatrix3x4fv = getProc<Delegates.ProgramUniformMatrix3x4fv>("ProgramUniformMatrix3x4fv");
				Functions.ProgramUniformMatrix4dv = getProc<Delegates.ProgramUniformMatrix4dv>("ProgramUniformMatrix4dv");
				Functions.ProgramUniformMatrix4fv = getProc<Delegates.ProgramUniformMatrix4fv>("ProgramUniformMatrix4fv");
				Functions.ProgramUniformMatrix4x2dv = getProc<Delegates.ProgramUniformMatrix4x2dv>("ProgramUniformMatrix4x2dv");
				Functions.ProgramUniformMatrix4x2fv = getProc<Delegates.ProgramUniformMatrix4x2fv>("ProgramUniformMatrix4x2fv");
				Functions.ProgramUniformMatrix4x3dv = getProc<Delegates.ProgramUniformMatrix4x3dv>("ProgramUniformMatrix4x3dv");
				Functions.ProgramUniformMatrix4x3fv = getProc<Delegates.ProgramUniformMatrix4x3fv>("ProgramUniformMatrix4x3fv");
				Functions.ReleaseShaderCompiler = getProc<Delegates.ReleaseShaderCompiler>("ReleaseShaderCompiler");
				Functions.ScissorArrayv = getProc<Delegates.ScissorArrayv>("ScissorArrayv");
				Functions.ScissorIndexed = getProc<Delegates.ScissorIndexed>("ScissorIndexed");
				Functions.ScissorIndexedv = getProc<Delegates.ScissorIndexedv>("ScissorIndexedv");
				Functions.ShaderBinary = getProc<Delegates.ShaderBinary>("ShaderBinary");
				Functions.UseProgramStages = getProc<Delegates.UseProgramStages>("UseProgramStages");
				Functions.ValidateProgramPipeline = getProc<Delegates.ValidateProgramPipeline>("ValidateProgramPipeline");
				Functions.VertexAttribL1d = getProc<Delegates.VertexAttribL1d>("VertexAttribL1d");
				Functions.VertexAttribL1dv = getProc<Delegates.VertexAttribL1dv>("VertexAttribL1dv");
				Functions.VertexAttribL2d = getProc<Delegates.VertexAttribL2d>("VertexAttribL2d");
				Functions.VertexAttribL2dv = getProc<Delegates.VertexAttribL2dv>("VertexAttribL2dv");
				Functions.VertexAttribL3d = getProc<Delegates.VertexAttribL3d>("VertexAttribL3d");
				Functions.VertexAttribL3dv = getProc<Delegates.VertexAttribL3dv>("VertexAttribL3dv");
				Functions.VertexAttribL4d = getProc<Delegates.VertexAttribL4d>("VertexAttribL4d");
				Functions.VertexAttribL4dv = getProc<Delegates.VertexAttribL4dv>("VertexAttribL4dv");
				Functions.VertexAttribLPointer = getProc<Delegates.VertexAttribLPointer>("VertexAttribLPointer");
				Functions.ViewportArrayv = getProc<Delegates.ViewportArrayv>("ViewportArrayv");
				Functions.ViewportIndexedf = getProc<Delegates.ViewportIndexedf>("ViewportIndexedf");
				Functions.ViewportIndexedfv = getProc<Delegates.ViewportIndexedfv>("ViewportIndexedfv");
			}

			if (version >= 4.2)
			{
				Functions.BindImageTexture = getProc<Delegates.BindImageTexture>("BindImageTexture");
				Functions.DrawArraysInstancedBaseInstance = getProc<Delegates.DrawArraysInstancedBaseInstance>("DrawArraysInstancedBaseInstance");
				Functions.DrawElementsInstancedBaseInstance = getProc<Delegates.DrawElementsInstancedBaseInstance>("DrawElementsInstancedBaseInstance");
				Functions.DrawElementsInstancedBaseVertexBaseInstance = getProc<Delegates.DrawElementsInstancedBaseVertexBaseInstance>("DrawElementsInstancedBaseVertexBaseInstance");
				Functions.DrawTransformFeedbackInstanced = getProc<Delegates.DrawTransformFeedbackInstanced>("DrawTransformFeedbackInstanced");
				Functions.DrawTransformFeedbackStreamInstanced = getProc<Delegates.DrawTransformFeedbackStreamInstanced>("DrawTransformFeedbackStreamInstanced");
				Functions.GetActiveAtomicCounterBufferiv = getProc<Delegates.GetActiveAtomicCounterBufferiv>("GetActiveAtomicCounterBufferiv");
				Functions.GetInternalformativ = getProc<Delegates.GetInternalformativ>("GetInternalformativ");
				Functions.MemoryBarrier = getProc<Delegates.MemoryBarrier>("MemoryBarrier");
				Functions.TexStorage1D = getProc<Delegates.TexStorage1D>("TexStorage1D");
				Functions.TexStorage2D = getProc<Delegates.TexStorage2D>("TexStorage2D");
				Functions.TexStorage3D = getProc<Delegates.TexStorage3D>("TexStorage3D");
			}

			if (version >= 4.3)
			{
				Functions.BindVertexBuffer = getProc<Delegates.BindVertexBuffer>("BindVertexBuffer");
				Functions.ClearBufferData = getProc<Delegates.ClearBufferData>("ClearBufferData");
				Functions.ClearBufferSubData = getProc<Delegates.ClearBufferSubData>("ClearBufferSubData");
				Functions.CopyImageSubData = getProc<Delegates.CopyImageSubData>("CopyImageSubData");
				Functions.DebugMessageCallback = getProc<Delegates.DebugMessageCallback>("DebugMessageCallback");
				Functions.DebugMessageControl = getProc<Delegates.DebugMessageControl>("DebugMessageControl");
				Functions.DebugMessageInsert = getProc<Delegates.DebugMessageInsert>("DebugMessageInsert");
				Functions.DispatchCompute = getProc<Delegates.DispatchCompute>("DispatchCompute");
				Functions.DispatchComputeIndirect = getProc<Delegates.DispatchComputeIndirect>("DispatchComputeIndirect");
				Functions.FramebufferParameteri = getProc<Delegates.FramebufferParameteri>("FramebufferParameteri");
				Functions.GetDebugMessageLog = getProc<Delegates.GetDebugMessageLog>("GetDebugMessageLog");
				Functions.GetFramebufferParameteriv = getProc<Delegates.GetFramebufferParameteriv>("GetFramebufferParameteriv");
				Functions.GetInternalformati64v = getProc<Delegates.GetInternalformati64v>("GetInternalformati64v");
				Functions.GetObjectLabel = getProc<Delegates.GetObjectLabel>("GetObjectLabel");
				Functions.GetObjectPtrLabel = getProc<Delegates.GetObjectPtrLabel>("GetObjectPtrLabel");
				Functions.GetProgramInterfaceiv = getProc<Delegates.GetProgramInterfaceiv>("GetProgramInterfaceiv");
				Functions.GetProgramResourceIndex = getProc<Delegates.GetProgramResourceIndex>("GetProgramResourceIndex");
				Functions.GetProgramResourceiv = getProc<Delegates.GetProgramResourceiv>("GetProgramResourceiv");
				Functions.GetProgramResourceLocation = getProc<Delegates.GetProgramResourceLocation>("GetProgramResourceLocation");
				Functions.GetProgramResourceLocationIndex = getProc<Delegates.GetProgramResourceLocationIndex>("GetProgramResourceLocationIndex");
				Functions.GetProgramResourceName = getProc<Delegates.GetProgramResourceName>("GetProgramResourceName");
				Functions.InvalidateBufferData = getProc<Delegates.InvalidateBufferData>("InvalidateBufferData");
				Functions.InvalidateBufferSubData = getProc<Delegates.InvalidateBufferSubData>("InvalidateBufferSubData");
				Functions.InvalidateFramebuffer = getProc<Delegates.InvalidateFramebuffer>("InvalidateFramebuffer");
				Functions.InvalidateSubFramebuffer = getProc<Delegates.InvalidateSubFramebuffer>("InvalidateSubFramebuffer");
				Functions.InvalidateTexImage = getProc<Delegates.InvalidateTexImage>("InvalidateTexImage");
				Functions.InvalidateTexSubImage = getProc<Delegates.InvalidateTexSubImage>("InvalidateTexSubImage");
				Functions.MultiDrawArraysIndirect = getProc<Delegates.MultiDrawArraysIndirect>("MultiDrawArraysIndirect");
				Functions.MultiDrawElementsIndirect = getProc<Delegates.MultiDrawElementsIndirect>("MultiDrawElementsIndirect");
				Functions.ObjectLabel = getProc<Delegates.ObjectLabel>("ObjectLabel");
				Functions.ObjectPtrLabel = getProc<Delegates.ObjectPtrLabel>("ObjectPtrLabel");
				Functions.PopDebugGroup = getProc<Delegates.PopDebugGroup>("PopDebugGroup");
				Functions.PushDebugGroup = getProc<Delegates.PushDebugGroup>("PushDebugGroup");
				Functions.ShaderStorageBlockBinding = getProc<Delegates.ShaderStorageBlockBinding>("ShaderStorageBlockBinding");
				Functions.TexBufferRange = getProc<Delegates.TexBufferRange>("TexBufferRange");
				Functions.TexStorage2DMultisample = getProc<Delegates.TexStorage2DMultisample>("TexStorage2DMultisample");
				Functions.TexStorage3DMultisample = getProc<Delegates.TexStorage3DMultisample>("TexStorage3DMultisample");
				Functions.TextureView = getProc<Delegates.TextureView>("TextureView");
				Functions.VertexAttribBinding = getProc<Delegates.VertexAttribBinding>("VertexAttribBinding");
				Functions.VertexAttribFormat = getProc<Delegates.VertexAttribFormat>("VertexAttribFormat");
				Functions.VertexAttribIFormat = getProc<Delegates.VertexAttribIFormat>("VertexAttribIFormat");
				Functions.VertexAttribLFormat = getProc<Delegates.VertexAttribLFormat>("VertexAttribLFormat");
				Functions.VertexBindingDivisor = getProc<Delegates.VertexBindingDivisor>("VertexBindingDivisor");
			}

			if (version >= 4.4)
			{
				Functions.BindBuffersBase = getProc<Delegates.BindBuffersBase>("BindBuffersBase");
				Functions.BindBuffersRange = getProc<Delegates.BindBuffersRange>("BindBuffersRange");
				Functions.BindSamplers = getProc<Delegates.BindSamplers>("BindSamplers");
				Functions.BindVertexBuffers = getProc<Delegates.BindVertexBuffers>("BindVertexBuffers");
				Functions.BufferStorage = getProc<Delegates.BufferStorage>("BufferStorage");
				Functions.ClearTexImage = getProc<Delegates.ClearTexImage>("ClearTexImage");
				Functions.ClearTexSubImage = getProc<Delegates.ClearTexSubImage>("ClearTexSubImage");
			}

			if (version >= 4.5)
			{
				Functions.BindTextureUnit = getProc<Delegates.BindTextureUnit>("BindTextureUnit");
				Functions.BlitNamedFramebuffer = getProc<Delegates.BlitNamedFramebuffer>("BlitNamedFramebuffer");
				Functions.CheckNamedFramebufferStatus = getProc<Delegates.CheckNamedFramebufferStatus>("CheckNamedFramebufferStatus");
				Functions.ClearNamedBufferData = getProc<Delegates.ClearNamedBufferData>("ClearNamedBufferData");
				Functions.ClearNamedBufferSubData = getProc<Delegates.ClearNamedBufferSubData>("ClearNamedBufferSubData");
				Functions.ClearNamedFramebufferfi = getProc<Delegates.ClearNamedFramebufferfi>("ClearNamedFramebufferfi");
				Functions.ClearNamedFramebufferfv = getProc<Delegates.ClearNamedFramebufferfv>("ClearNamedFramebufferfv");
				Functions.ClearNamedFramebufferiv = getProc<Delegates.ClearNamedFramebufferiv>("ClearNamedFramebufferiv");
				Functions.ClearNamedFramebufferuiv = getProc<Delegates.ClearNamedFramebufferuiv>("ClearNamedFramebufferuiv");
				Functions.ClipControl = getProc<Delegates.ClipControl>("ClipControl");
				Functions.CompressedTextureSubImage1D = getProc<Delegates.CompressedTextureSubImage1D>("CompressedTextureSubImage1D");
				Functions.CompressedTextureSubImage2D = getProc<Delegates.CompressedTextureSubImage2D>("CompressedTextureSubImage2D");
				Functions.CompressedTextureSubImage3D = getProc<Delegates.CompressedTextureSubImage3D>("CompressedTextureSubImage3D");
				Functions.CopyNamedBufferSubData = getProc<Delegates.CopyNamedBufferSubData>("CopyNamedBufferSubData");
				Functions.CopyTextureSubImage1D = getProc<Delegates.CopyTextureSubImage1D>("CopyTextureSubImage1D");
				Functions.CopyTextureSubImage2D = getProc<Delegates.CopyTextureSubImage2D>("CopyTextureSubImage2D");
				Functions.CopyTextureSubImage3D = getProc<Delegates.CopyTextureSubImage3D>("CopyTextureSubImage3D");
				Functions.CreateBuffers = getProc<Delegates.CreateBuffers>("CreateBuffers");
				Functions.CreateFramebuffers = getProc<Delegates.CreateFramebuffers>("CreateFramebuffers");
				Functions.CreateProgramPipelines = getProc<Delegates.CreateProgramPipelines>("CreateProgramPipelines");
				Functions.CreateQueries = getProc<Delegates.CreateQueries>("CreateQueries");
				Functions.CreateRenderbuffers = getProc<Delegates.CreateRenderbuffers>("CreateRenderbuffers");
				Functions.CreateSamplers = getProc<Delegates.CreateSamplers>("CreateSamplers");
				Functions.CreateTextures = getProc<Delegates.CreateTextures>("CreateTextures");
				Functions.CreateTransformFeedbacks = getProc<Delegates.CreateTransformFeedbacks>("CreateTransformFeedbacks");
				Functions.CreateVertexArrays = getProc<Delegates.CreateVertexArrays>("CreateVertexArrays");
				Functions.DisableVertexArrayAttrib = getProc<Delegates.DisableVertexArrayAttrib>("DisableVertexArrayAttrib");
				Functions.EnableVertexArrayAttrib = getProc<Delegates.EnableVertexArrayAttrib>("EnableVertexArrayAttrib");
				Functions.FlushMappedNamedBufferRange = getProc<Delegates.FlushMappedNamedBufferRange>("FlushMappedNamedBufferRange");
				Functions.GenerateTextureMipmap = getProc<Delegates.GenerateTextureMipmap>("GenerateTextureMipmap");
				Functions.GetCompressedTextureImage = getProc<Delegates.GetCompressedTextureImage>("GetCompressedTextureImage");
				Functions.GetCompressedTextureSubImage = getProc<Delegates.GetCompressedTextureSubImage>("GetCompressedTextureSubImage");
				Functions.GetGraphicsResetStatus = getProc<Delegates.GetGraphicsResetStatus>("GetGraphicsResetStatus");
				Functions.GetNamedBufferParameteri64v = getProc<Delegates.GetNamedBufferParameteri64v>("GetNamedBufferParameteri64v");
				Functions.GetNamedBufferParameteriv = getProc<Delegates.GetNamedBufferParameteriv>("GetNamedBufferParameteriv");
				Functions.GetNamedBufferPointerv = getProc<Delegates.GetNamedBufferPointerv>("GetNamedBufferPointerv");
				Functions.GetNamedBufferSubData = getProc<Delegates.GetNamedBufferSubData>("GetNamedBufferSubData");
				Functions.GetNamedFramebufferAttachmentParameteriv = getProc<Delegates.GetNamedFramebufferAttachmentParameteriv>("GetNamedFramebufferAttachmentParameteriv");
				Functions.GetNamedFramebufferParameteriv = getProc<Delegates.GetNamedFramebufferParameteriv>("GetNamedFramebufferParameteriv");
				Functions.GetNamedRenderbufferParameteriv = getProc<Delegates.GetNamedRenderbufferParameteriv>("GetNamedRenderbufferParameteriv");
				Functions.GetnCompressedTexImage = getProc<Delegates.GetnCompressedTexImage>("GetnCompressedTexImage");
				Functions.GetnTexImage = getProc<Delegates.GetnTexImage>("GetnTexImage");
				Functions.GetnUniformdv = getProc<Delegates.GetnUniformdv>("GetnUniformdv");
				Functions.GetnUniformfv = getProc<Delegates.GetnUniformfv>("GetnUniformfv");
				Functions.GetnUniformiv = getProc<Delegates.GetnUniformiv>("GetnUniformiv");
				Functions.GetnUniformuiv = getProc<Delegates.GetnUniformuiv>("GetnUniformuiv");
				Functions.GetQueryBufferObjecti64v = getProc<Delegates.GetQueryBufferObjecti64v>("GetQueryBufferObjecti64v");
				Functions.GetQueryBufferObjectiv = getProc<Delegates.GetQueryBufferObjectiv>("GetQueryBufferObjectiv");
				Functions.GetQueryBufferObjectui64v = getProc<Delegates.GetQueryBufferObjectui64v>("GetQueryBufferObjectui64v");
				Functions.GetQueryBufferObjectuiv = getProc<Delegates.GetQueryBufferObjectuiv>("GetQueryBufferObjectuiv");
				Functions.GetTextureImage = getProc<Delegates.GetTextureImage>("GetTextureImage");
				Functions.GetTextureLevelParameterfv = getProc<Delegates.GetTextureLevelParameterfv>("GetTextureLevelParameterfv");
				Functions.GetTextureLevelParameteriv = getProc<Delegates.GetTextureLevelParameteriv>("GetTextureLevelParameteriv");
				Functions.GetTextureParameterfv = getProc<Delegates.GetTextureParameterfv>("GetTextureParameterfv");
				Functions.GetTextureParameterIiv = getProc<Delegates.GetTextureParameterIiv>("GetTextureParameterIiv");
				Functions.GetTextureParameterIuiv = getProc<Delegates.GetTextureParameterIuiv>("GetTextureParameterIuiv");
				Functions.GetTextureParameteriv = getProc<Delegates.GetTextureParameteriv>("GetTextureParameteriv");
				Functions.GetTextureSubImage = getProc<Delegates.GetTextureSubImage>("GetTextureSubImage");
				Functions.GetTransformFeedbacki_v = getProc<Delegates.GetTransformFeedbacki_v>("GetTransformFeedbacki_v");
				Functions.GetTransformFeedbacki64_v = getProc<Delegates.GetTransformFeedbacki64_v>("GetTransformFeedbacki64_v");
				Functions.GetTransformFeedbackiv = getProc<Delegates.GetTransformFeedbackiv>("GetTransformFeedbackiv");
				Functions.GetVertexArrayIndexed64iv = getProc<Delegates.GetVertexArrayIndexed64iv>("GetVertexArrayIndexed64iv");
				Functions.GetVertexArrayIndexediv = getProc<Delegates.GetVertexArrayIndexediv>("GetVertexArrayIndexediv");
				Functions.GetVertexArrayiv = getProc<Delegates.GetVertexArrayiv>("GetVertexArrayiv");
				Functions.InvalidateNamedFramebufferData = getProc<Delegates.InvalidateNamedFramebufferData>("InvalidateNamedFramebufferData");
				Functions.InvalidateNamedFramebufferSubData = getProc<Delegates.InvalidateNamedFramebufferSubData>("InvalidateNamedFramebufferSubData");
				Functions.MapNamedBuffer = getProc<Delegates.MapNamedBuffer>("MapNamedBuffer");
				Functions.MapNamedBufferRange = getProc<Delegates.MapNamedBufferRange>("MapNamedBufferRange");
				Functions.MemoryBarrierByRegion = getProc<Delegates.MemoryBarrierByRegion>("MemoryBarrierByRegion");
				Functions.NamedBufferData = getProc<Delegates.NamedBufferData>("NamedBufferData");
				Functions.NamedBufferStorage = getProc<Delegates.NamedBufferStorage>("NamedBufferStorage");
				Functions.NamedBufferSubData = getProc<Delegates.NamedBufferSubData>("NamedBufferSubData");
				Functions.NamedFramebufferDrawBuffer = getProc<Delegates.NamedFramebufferDrawBuffer>("NamedFramebufferDrawBuffer");
				Functions.NamedFramebufferDrawBuffers = getProc<Delegates.NamedFramebufferDrawBuffers>("NamedFramebufferDrawBuffers");
				Functions.NamedFramebufferParameteri = getProc<Delegates.NamedFramebufferParameteri>("NamedFramebufferParameteri");
				Functions.NamedFramebufferReadBuffer = getProc<Delegates.NamedFramebufferReadBuffer>("NamedFramebufferReadBuffer");
				Functions.NamedFramebufferRenderbuffer = getProc<Delegates.NamedFramebufferRenderbuffer>("NamedFramebufferRenderbuffer");
				Functions.NamedFramebufferTexture = getProc<Delegates.NamedFramebufferTexture>("NamedFramebufferTexture");
				Functions.NamedFramebufferTextureLayer = getProc<Delegates.NamedFramebufferTextureLayer>("NamedFramebufferTextureLayer");
				Functions.NamedRenderbufferStorage = getProc<Delegates.NamedRenderbufferStorage>("NamedRenderbufferStorage");
				Functions.NamedRenderbufferStorageMultisample = getProc<Delegates.NamedRenderbufferStorageMultisample>("NamedRenderbufferStorageMultisample");
				Functions.ReadnPixels = getProc<Delegates.ReadnPixels>("ReadnPixels");
				Functions.TextureBarrier = getProc<Delegates.TextureBarrier>("TextureBarrier");
				Functions.TextureBuffer = getProc<Delegates.TextureBuffer>("TextureBuffer");
				Functions.TextureBufferRange = getProc<Delegates.TextureBufferRange>("TextureBufferRange");
				Functions.TextureParameterf = getProc<Delegates.TextureParameterf>("TextureParameterf");
				Functions.TextureParameterfv = getProc<Delegates.TextureParameterfv>("TextureParameterfv");
				Functions.TextureParameteri = getProc<Delegates.TextureParameteri>("TextureParameteri");
				Functions.TextureParameterIiv = getProc<Delegates.TextureParameterIiv>("TextureParameterIiv");
				Functions.TextureParameterIuiv = getProc<Delegates.TextureParameterIuiv>("TextureParameterIuiv");
				Functions.TextureParameteriv = getProc<Delegates.TextureParameteriv>("TextureParameteriv");
				Functions.TextureStorage1D = getProc<Delegates.TextureStorage1D>("TextureStorage1D");
				Functions.TextureStorage2D = getProc<Delegates.TextureStorage2D>("TextureStorage2D");
				Functions.TextureStorage2DMultisample = getProc<Delegates.TextureStorage2DMultisample>("TextureStorage2DMultisample");
				Functions.TextureStorage3D = getProc<Delegates.TextureStorage3D>("TextureStorage3D");
				Functions.TextureStorage3DMultisample = getProc<Delegates.TextureStorage3DMultisample>("TextureStorage3DMultisample");
				Functions.TextureSubImage1D = getProc<Delegates.TextureSubImage1D>("TextureSubImage1D");
				Functions.TextureSubImage2D = getProc<Delegates.TextureSubImage2D>("TextureSubImage2D");
				Functions.TextureSubImage3D = getProc<Delegates.TextureSubImage3D>("TextureSubImage3D");
				Functions.TransformFeedbackBufferBase = getProc<Delegates.TransformFeedbackBufferBase>("TransformFeedbackBufferBase");
				Functions.TransformFeedbackBufferRange = getProc<Delegates.TransformFeedbackBufferRange>("TransformFeedbackBufferRange");
				Functions.UnmapNamedBuffer = getProc<Delegates.UnmapNamedBuffer>("UnmapNamedBuffer");
				Functions.VertexArrayAttribBinding = getProc<Delegates.VertexArrayAttribBinding>("VertexArrayAttribBinding");
				Functions.VertexArrayAttribFormat = getProc<Delegates.VertexArrayAttribFormat>("VertexArrayAttribFormat");
				Functions.VertexArrayAttribIFormat = getProc<Delegates.VertexArrayAttribIFormat>("VertexArrayAttribIFormat");
				Functions.VertexArrayAttribLFormat = getProc<Delegates.VertexArrayAttribLFormat>("VertexArrayAttribLFormat");
				Functions.VertexArrayBindingDivisor = getProc<Delegates.VertexArrayBindingDivisor>("VertexArrayBindingDivisor");
				Functions.VertexArrayElementBuffer = getProc<Delegates.VertexArrayElementBuffer>("VertexArrayElementBuffer");
				Functions.VertexArrayVertexBuffer = getProc<Delegates.VertexArrayVertexBuffer>("VertexArrayVertexBuffer");
				Functions.VertexArrayVertexBuffers = getProc<Delegates.VertexArrayVertexBuffers>("VertexArrayVertexBuffers");
			}
			/*
			if (versionMajor > 4 || (versionMajor == 4 && versionMinor >= 6))
			{
				Functions.MultiDrawArraysIndirectCount = getProc<Delegates.MultiDrawArraysIndirectCount>("MultiDrawArraysIndirectCount");
				Functions.MultiDrawElementsIndirectCount = getProc<Delegates.MultiDrawElementsIndirectCount>("MultiDrawElementsIndirectCount");
				Functions.PolygonOffsetClamp = getProc<Delegates.PolygonOffsetClamp>("PolygonOffsetClamp");
				Functions.SpecializeShader = getProc<Delegates.SpecializeShader>("SpecializeShader");
			}*/

			if (version > 4.5)
            {
				Console.WriteLine($"OpenGL version {version} is not properly supported. The Zene Graphics Library was made for OpenGL version 4.5 and below.");
            }

			// Setup texture binding referance
			int size = 0;
			GetIntegerv(GLEnum.MaxTextureImageUnits, &size);
			BoundTextures = new TextureBinding[size];
			/*
			for (int i = 0; i < size; i++)
            {
				BoundTextures[i] = new TextureBinding();
            }*/
		}
#endif

		[OpenGLSupport(4.1)]
		public static void ActiveShaderProgram(uint pipeline, uint program)
		{
			Functions.ActiveShaderProgram(pipeline, program);
		}

		[OpenGLSupport(1.3)]
		public static void ActiveTexture(uint texture)
		{
			ActiveTextureUnit = texture - GLEnum.Texture0;

			Functions.ActiveTexture(texture);
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
					_boundBuffers.Array = buffer;
					return;
				case GLEnum.AtomicCounterBuffer:
					_boundBuffers.AtomicCounter = buffer;
					return;
				case GLEnum.CopyReadBuffer:
					_boundBuffers.CopyRead = buffer;
					return;
				case GLEnum.CopyWriteBuffer:
					_boundBuffers.CopyWrite = buffer;
					return;
				case GLEnum.DispatchIndirectBuffer:
					_boundBuffers.DispatchIndirect = buffer;
					return;
				case GLEnum.DrawIndirectBuffer:
					_boundBuffers.Indirect = buffer;
					return;
				case GLEnum.ElementArrayBuffer:
					_boundBuffers.ElementArray = buffer;
					return;
				case GLEnum.PixelPackBuffer:
					_boundBuffers.PixelPack = buffer;
					return;
				case GLEnum.PixelUnpackBuffer:
					_boundBuffers.PixelUnpack = buffer;
					return;
				case GLEnum.QueryBuffer:
					_boundBuffers.Query = buffer;
					return;
				case GLEnum.ShaderStorageBuffer:
					_boundBuffers.ShaderStorage = buffer;
					return;
				case GLEnum.TextureBuffer:
					_boundBuffers.Texture = buffer;
					return;
				case GLEnum.TransformFeedbackBuffer:
					_boundBuffers.TransformFeedback = buffer;
					return;
				case GLEnum.UniformBuffer:
					_boundBuffers.Uniform = buffer;
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

		[OpenGLSupport(3.0)]
		public static void BindFramebuffer(uint target, uint framebuffer)
		{
			Functions.BindFramebuffer(target, framebuffer);

			switch (target)
            {
				case GLEnum.Framebuffer:
					_boundFrameBuffers.Draw = framebuffer;
					_boundFrameBuffers.Read = framebuffer;
					return;

				case GLEnum.ReadFramebuffer:
					_boundFrameBuffers.Read = framebuffer;
					return;

				case GLEnum.DrawFramebuffer:
					_boundFrameBuffers.Draw = framebuffer;
					return;
            }
		}

		[OpenGLSupport(4.2)]
		public static void BindImageTexture(uint unit, ITexture texture, int level, bool layered, int layer, uint access, uint format)
		{
			if (texture != null)
			{
				Functions.BindImageTexture(unit, texture.Id, level, layered, layer, access, format);

				switch (texture.Target)
				{
					case TextureTarget.Texture1D:
						BoundTextures[unit].Texture1D = texture.Id;
						return;
					case TextureTarget.Texture1DArray:
						BoundTextures[unit].Texture1DArray = texture.Id;
						return;
					case TextureTarget.Texture2D:
						BoundTextures[unit].Texture2D = texture.Id;
						return;
					case TextureTarget.Texture2DArray:
						BoundTextures[unit].Texture2DArray = texture.Id;
						return;
					case TextureTarget.Multisample2D:
						BoundTextures[unit].Texture2DMS = texture.Id;
						return;
					case TextureTarget.MultisampleArray2D:
						BoundTextures[unit].Texture2DArrayMS = texture.Id;
						return;
					case TextureTarget.Texture3D:
						BoundTextures[unit].Texture3D = texture.Id;
						return;
					case TextureTarget.CubeMap:
						BoundTextures[unit].CubeMap = texture.Id;
						return;
					case TextureTarget.CubeMapArray:
						BoundTextures[unit].CubeMapArray = texture.Id;
						return;
					case TextureTarget.Buffer:
						BoundTextures[unit].Buffer = texture.Id;
						return;
					case TextureTarget.Rectangle:
						BoundTextures[unit].Rectangle = texture.Id;
						return;
				}
				return;
			}

			// Reset all texture binding referances in unit
			BoundTextures[unit] = new TextureBinding();

			Functions.BindImageTexture(unit, 0, level, layered, layer, access, format);
		}

		[OpenGLSupport(4.4)]
		public static void BindImageTextures(uint first, int count, ITexture[] textures)
		{
			for (uint i = 0; i < count; i++)
            {
				if (textures == null || textures[i] == null)
                {
					BindImageTexture(first + i, null, 0, false, 0, GLEnum.ReadOnly, GLEnum.R8);
				}
				BindImageTexture(first + i, textures[i], 0, true, 0, GLEnum.ReadWrite, (uint)textures[i].InternalFormat);
            }
		}

		[OpenGLSupport(4.1)]
		public static void BindProgramPipeline(uint pipeline)
		{
			Functions.BindProgramPipeline(pipeline);
		}

		[OpenGLSupport(3.0)]
		public static void BindRenderbuffer(uint target, uint renderbuffer)
		{
			if (target == GLEnum.Renderbuffer)
            {
				BoundRenderbuffer = renderbuffer;
			}

			Functions.BindRenderbuffer(target, renderbuffer);
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

		[OpenGLSupport(1.1)]
		public static void BindTexture(uint target, uint texture)
		{
			Functions.BindTexture(target, texture);

			switch (target)
            {
				case GLEnum.Texture1d:
					BoundTextures[ActiveTextureUnit].Texture1D = texture;
					return;
				case GLEnum.Texture1dArray:
					BoundTextures[ActiveTextureUnit].Texture1DArray = texture;
					return;
				case GLEnum.Texture2d:
					BoundTextures[ActiveTextureUnit].Texture2D = texture;
					return;
				case GLEnum.Texture2dArray:
					BoundTextures[ActiveTextureUnit].Texture2DArray = texture;
					return;
				case GLEnum.Texture2dMultisample:
					BoundTextures[ActiveTextureUnit].Texture2DMS = texture;
					return;
				case GLEnum.Texture2dMultisampleArray:
					BoundTextures[ActiveTextureUnit].Texture2DArrayMS = texture;
					return;
				case GLEnum.Texture3d:
					BoundTextures[ActiveTextureUnit].Texture3D = texture;
					return;
				case GLEnum.TextureCubeMap:
					BoundTextures[ActiveTextureUnit].CubeMap = texture;
					return;
				case GLEnum.TextureCubeMapArray:
					BoundTextures[ActiveTextureUnit].CubeMapArray = texture;
					return;
				case GLEnum.TextureBuffer:
					BoundTextures[ActiveTextureUnit].Buffer = texture;
					return;
				case GLEnum.TextureRectangle:
					BoundTextures[ActiveTextureUnit].Rectangle = texture;
					return;
			}
		}

		/// <summary>
		/// Note: <paramref name="textures"/> cannot be null nor can any element of <paramref name="textures"/> be null.
		/// </summary>
		/// <param name="first"></param>
		/// <param name="count"></param>
		/// <param name="textures"></param>
		[OpenGLSupport(1.3)]
		public static void BindTextures(uint first, int count, ITexture[] textures)
		{
			if (textures == null) { return; }

			for (uint i = 0; i < count; i++)
            {
				if (textures[i] == null) { continue; }

				ActiveTexture(GLEnum.Texture0 + first + i);
				BindTexture((uint)textures[i].Target, textures[i].Id);
            }
		}

		[OpenGLSupport(4.5)]
		public static void BindTextureUnit(uint unit, ITexture texture)
		{
			if (texture != null)
            {
				Functions.BindTextureUnit(unit, texture.Id);

				switch (texture.Target)
				{
					case TextureTarget.Texture1D:
						BoundTextures[unit].Texture1D = texture.Id;
						return;
					case TextureTarget.Texture1DArray:
						BoundTextures[unit].Texture1DArray = texture.Id;
						return;
					case TextureTarget.Texture2D:
						BoundTextures[unit].Texture2D = texture.Id;
						return;
					case TextureTarget.Texture2DArray:
						BoundTextures[unit].Texture2DArray = texture.Id;
						return;
					case TextureTarget.Multisample2D:
						BoundTextures[unit].Texture2DMS = texture.Id;
						return;
					case TextureTarget.MultisampleArray2D:
						BoundTextures[unit].Texture2DArrayMS = texture.Id;
						return;
					case TextureTarget.Texture3D:
						BoundTextures[unit].Texture3D = texture.Id;
						return;
					case TextureTarget.CubeMap:
						BoundTextures[unit].CubeMap = texture.Id;
						return;
					case TextureTarget.CubeMapArray:
						BoundTextures[unit].CubeMapArray = texture.Id;
						return;
					case TextureTarget.Buffer:
						BoundTextures[unit].Buffer = texture.Id;
						return;
					case TextureTarget.Rectangle:
						BoundTextures[unit].Rectangle = texture.Id;
						return;
				}
				return;
			}
			// Reset all texture binding referances in unit
			BoundTextures[unit] = new TextureBinding();

			Functions.BindTextureUnit(unit, 0);
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

		[OpenGLSupport(3.0)]
		public static void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, uint filter)
		{
			Functions.BlitFramebuffer(srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, mask, filter);
		}

		[OpenGLSupport(4.5)]
		public static void BlitNamedFramebuffer(uint readFramebuffer, uint drawFramebuffer, int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, uint filter)
		{
			Functions.BlitNamedFramebuffer(readFramebuffer, drawFramebuffer, srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, mask, filter);
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
		public static uint CheckFramebufferStatus(uint target)
		{
			return Functions.CheckFramebufferStatus(target);
		}

		[OpenGLSupport(4.5)]
		public static uint CheckNamedFramebufferStatus(uint framebuffer, uint target)
		{
			return Functions.CheckNamedFramebufferStatus(framebuffer, target);
		}

		[OpenGLSupport(3.0)]
		public static void ClampColour(uint target, uint clamp)
		{
			Functions.ClampColour(target, clamp);
		}

		[OpenGLSupport(1.0)]
		public static void Clear(uint mask)
		{
			Functions.Clear(mask);
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

		[OpenGLSupport(1.0)]
		public static void ClearColour(float red, float green, float blue, float alpha)
		{
			Functions.ClearColour(red, green, blue, alpha);
		}

		[OpenGLSupport(1.0)]
		public static void ClearDepth(double depth)
		{
			Functions.ClearDepth(depth);
		}

		[OpenGLSupport(4.1)]
		public static void ClearDepthf(float d)
		{
			Functions.ClearDepthf(d);
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

		[OpenGLSupport(4.5)]
		public static void ClearNamedFramebufferfi(uint framebuffer, uint buffer, int drawbuffer, float depth, int stencil)
		{
			Functions.ClearNamedFramebufferfi(framebuffer, buffer, drawbuffer, depth, stencil);
		}

		[OpenGLSupport(4.5)]
		public static void ClearNamedFramebufferfv(uint framebuffer, uint buffer, int drawbuffer, float* value)
		{
			Functions.ClearNamedFramebufferfv(framebuffer, buffer, drawbuffer, value);
		}

		[OpenGLSupport(4.5)]
		public static void ClearNamedFramebufferiv(uint framebuffer, uint buffer, int drawbuffer, int* value)
		{
			Functions.ClearNamedFramebufferiv(framebuffer, buffer, drawbuffer, value);
		}

		[OpenGLSupport(4.5)]
		public static void ClearNamedFramebufferuiv(uint framebuffer, uint buffer, int drawbuffer, uint* value)
		{
			Functions.ClearNamedFramebufferuiv(framebuffer, buffer, drawbuffer, value);
		}

		[OpenGLSupport(1.0)]
		public static void ClearStencil(int s)
		{
			Functions.ClearStencil(s);
		}

		[OpenGLSupport(4.4)]
		public static void ClearTexImage(uint texture, int level, uint format, uint type, void* data)
		{
			Functions.ClearTexImage(texture, level, format, type, data);
		}

		[OpenGLSupport(4.4)]
		public static void ClearTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, void* data)
		{
			Functions.ClearTexSubImage(texture, level, xoffset, yoffset, zoffset, width, height, depth, format, type, data);
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

		[OpenGLSupport(1.3)]
		public static void CompressedTexImage1D(uint target, int level, uint internalformat, int width, int border, int imageSize, void* data)
		{
			Functions.CompressedTexImage1D(target, level, internalformat, width, border, imageSize, data);
		}

		[OpenGLSupport(1.3)]
		public static void CompressedTexImage2D(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, void* data)
		{
			Functions.CompressedTexImage2D(target, level, internalformat, width, height, border, imageSize, data);
		}

		[OpenGLSupport(1.3)]
		public static void CompressedTexImage3D(uint target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, void* data)
		{
			Functions.CompressedTexImage3D(target, level, internalformat, width, height, depth, border, imageSize, data);
		}

		[OpenGLSupport(1.3)]
		public static void CompressedTexSubImage1D(uint target, int level, int xoffset, int width, uint format, int imageSize, void* data)
		{
			Functions.CompressedTexSubImage1D(target, level, xoffset, width, format, imageSize, data);
		}

		[OpenGLSupport(1.3)]
		public static void CompressedTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, void* data)
		{
			Functions.CompressedTexSubImage2D(target, level, xoffset, yoffset, width, height, format, imageSize, data);
		}

		[OpenGLSupport(1.3)]
		public static void CompressedTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, void* data)
		{
			Functions.CompressedTexSubImage3D(target, level, xoffset, yoffset, zoffset, width, height, depth, format, imageSize, data);
		}

		[OpenGLSupport(4.5)]
		public static void CompressedTextureSubImage1D(uint texture, int level, int xoffset, int width, uint format, int imageSize, void* data)
		{
			Functions.CompressedTextureSubImage1D(texture, level, xoffset, width, format, imageSize, data);
		}

		[OpenGLSupport(4.5)]
		public static void CompressedTextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, void* data)
		{
			Functions.CompressedTextureSubImage2D(texture, level, xoffset, yoffset, width, height, format, imageSize, data);
		}

		[OpenGLSupport(4.5)]
		public static void CompressedTextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, void* data)
		{
			Functions.CompressedTextureSubImage3D(texture, level, xoffset, yoffset, zoffset, width, height, depth, format, imageSize, data);
		}

		[OpenGLSupport(3.1)]
		public static void CopyBufferSubData(uint readTarget, uint writeTarget, int readOffset, int writeOffset, int size)
		{
			Functions.CopyBufferSubData(readTarget, writeTarget, readOffset, writeOffset, size);
		}

		[OpenGLSupport(4.3)]
		public static void CopyImageSubData(uint srcName, uint srcTarget, int srcLevel, int srcX, int srcY, int srcZ, uint dstName, uint dstTarget, int dstLevel, int dstX, int dstY, int dstZ, int srcWidth, int srcHeight, int srcDepth)
		{
			Functions.CopyImageSubData(srcName, srcTarget, srcLevel, srcX, srcY, srcZ, dstName, dstTarget, dstLevel, dstX, dstY, dstZ, srcWidth, srcHeight, srcDepth);
		}

		[OpenGLSupport(4.5)]
		public static void CopyNamedBufferSubData(uint readBuffer, uint writeBuffer, int readOffset, int writeOffset, int size)
		{
			Functions.CopyNamedBufferSubData(readBuffer, writeBuffer, readOffset, writeOffset, size);
		}

		[OpenGLSupport(1.1)]
		public static void CopyTexImage1D(uint target, int level, uint internalformat, int x, int y, int width, int border)
		{
			Functions.CopyTexImage1D(target, level, internalformat, x, y, width, border);
		}

		[OpenGLSupport(1.1)]
		public static void CopyTexImage2D(uint target, int level, uint internalformat, int x, int y, int width, int height, int border)
		{
			Functions.CopyTexImage2D(target, level, internalformat, x, y, width, height, border);
		}

		[OpenGLSupport(1.1)]
		public static void CopyTexSubImage1D(uint target, int level, int xoffset, int x, int y, int width)
		{
			Functions.CopyTexSubImage1D(target, level, xoffset, x, y, width);
		}

		[OpenGLSupport(1.1)]
		public static void CopyTexSubImage2D(uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height)
		{
			Functions.CopyTexSubImage2D(target, level, xoffset, yoffset, x, y, width, height);
		}

		[OpenGLSupport(1.2)]
		public static void CopyTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height)
		{
			Functions.CopyTexSubImage3D(target, level, xoffset, yoffset, zoffset, x, y, width, height);
		}

		[OpenGLSupport(4.5)]
		public static void CopyTextureSubImage1D(uint texture, int level, int xoffset, int x, int y, int width)
		{
			Functions.CopyTextureSubImage1D(texture, level, xoffset, x, y, width);
		}

		[OpenGLSupport(4.5)]
		public static void CopyTextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int x, int y, int width, int height)
		{
			Functions.CopyTextureSubImage2D(texture, level, xoffset, yoffset, x, y, width, height);
		}

		[OpenGLSupport(4.5)]
		public static void CopyTextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height)
		{
			Functions.CopyTextureSubImage3D(texture, level, xoffset, yoffset, zoffset, x, y, width, height);
		}

		[OpenGLSupport(4.5)]
		public static void CreateBuffers(int n, uint* buffers)
		{
			Functions.CreateBuffers(n, buffers);
		}

		[OpenGLSupport(4.5)]
		public static void CreateFramebuffers(int n, uint* framebuffers)
		{
			Functions.CreateFramebuffers(n, framebuffers);
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
		public static void CreateRenderbuffers(int n, uint* renderbuffers)
		{
			Functions.CreateRenderbuffers(n, renderbuffers);
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
		public static void CreateTextures(uint target, int n, uint* textures)
		{
			Functions.CreateTextures(target, n, textures);
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

		[OpenGLSupport(3.0)]
		public static void DeleteFramebuffers(int n, uint* framebuffers)
		{
			Functions.DeleteFramebuffers(n, framebuffers);
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

		[OpenGLSupport(3.0)]
		public static void DeleteRenderbuffers(int n, uint* renderbuffers)
		{
			Functions.DeleteRenderbuffers(n, renderbuffers);
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

		[OpenGLSupport(1.1)]
		public static void DeleteTextures(int n, uint* textures)
		{
			Functions.DeleteTextures(n, textures);
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

		[OpenGLSupport(1.0)]
		public static void DrawBuffer(uint buf)
		{
			Functions.DrawBuffer(buf);
		}

		[OpenGLSupport(2.0)]
		public static void DrawBuffers(int n, uint* bufs)
		{
			Functions.DrawBuffers(n, bufs);
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

		[OpenGLSupport(4.3)]
		public static void FramebufferParameteri(uint target, uint pname, int param)
		{
			Functions.FramebufferParameteri(target, pname, param);
		}

		[OpenGLSupport(3.0)]
		public static void FramebufferRenderbuffer(uint target, uint attachment, uint renderbuffertarget, uint renderbuffer)
		{
			Functions.FramebufferRenderbuffer(target, attachment, renderbuffertarget, renderbuffer);
		}

		[OpenGLSupport(3.2)]
		public static void FramebufferTexture(uint target, uint attachment, uint texture, int level)
		{
			Functions.FramebufferTexture(target, attachment, texture, level);
		}

		[OpenGLSupport(3.0)]
		public static void FramebufferTexture1D(uint target, uint attachment, uint textarget, uint texture, int level)
		{
			Functions.FramebufferTexture1D(target, attachment, textarget, texture, level);
		}

		[OpenGLSupport(3.0)]
		public static void FramebufferTexture2D(uint target, uint attachment, uint textarget, uint texture, int level)
		{
			Functions.FramebufferTexture2D(target, attachment, textarget, texture, level);
		}

		[OpenGLSupport(3.0)]
		public static void FramebufferTexture3D(uint target, uint attachment, uint textarget, uint texture, int level, int zoffset)
		{
			Functions.FramebufferTexture3D(target, attachment, textarget, texture, level, zoffset);
		}

		[OpenGLSupport(3.0)]
		public static void FramebufferTextureLayer(uint target, uint attachment, uint texture, int level, int layer)
		{
			Functions.FramebufferTextureLayer(target, attachment, texture, level, layer);
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

		[OpenGLSupport(3.0)]
		public static void GenerateMipmap(uint target)
		{
			Functions.GenerateMipmap(target);
		}

		[OpenGLSupport(4.5)]
		public static void GenerateTextureMipmap(uint texture)
		{
			Functions.GenerateTextureMipmap(texture);
		}

		[OpenGLSupport(3.0)]
		public static void GenFramebuffers(int n, uint* framebuffers)
		{
			Functions.GenFramebuffers(n, framebuffers);
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

		[OpenGLSupport(3.0)]
		public static void GenRenderbuffers(int n, uint* renderbuffers)
		{
			Functions.GenRenderbuffers(n, renderbuffers);
		}

		[OpenGLSupport(3.3)]
		public static void GenSamplers(int count, uint* samplers)
		{
			Functions.GenSamplers(count, samplers);
		}

		[OpenGLSupport(1.1)]
		public static void GenTextures(int n, uint* textures)
		{
			Functions.GenTextures(n, textures);
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

		[OpenGLSupport(1.3)]
		public static void GetCompressedTexImage(uint target, int level, void* img)
		{
			Functions.GetCompressedTexImage(target, level, img);
		}

		[OpenGLSupport(4.5)]
		public static void GetCompressedTextureImage(uint texture, int level, int bufSize, void* pixels)
		{
			Functions.GetCompressedTextureImage(texture, level, bufSize, pixels);
		}

		[OpenGLSupport(4.5)]
		public static void GetCompressedTextureSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int bufSize, void* pixels)
		{
			Functions.GetCompressedTextureSubImage(texture, level, xoffset, yoffset, zoffset, width, height, depth, bufSize, pixels);
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

		[OpenGLSupport(3.0)]
		public static void GetFramebufferAttachmentParameteriv(uint target, uint attachment, uint pname, int* @params)
		{
			Functions.GetFramebufferAttachmentParameteriv(target, attachment, pname, @params);
		}

		[OpenGLSupport(4.3)]
		public static void GetFramebufferParameteriv(uint target, uint pname, int* @params)
		{
			Functions.GetFramebufferParameteriv(target, pname, @params);
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

		[OpenGLSupport(4.3)]
		public static void GetInternalformati64v(uint target, uint internalformat, uint pname, int bufSize, long* @params)
		{
			Functions.GetInternalformati64v(target, internalformat, pname, bufSize, @params);
		}

		[OpenGLSupport(4.2)]
		public static void GetInternalformativ(uint target, uint internalformat, uint pname, int bufSize, int* @params)
		{
			Functions.GetInternalformativ(target, internalformat, pname, bufSize, @params);
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
		public static void GetNamedFramebufferAttachmentParameteriv(uint framebuffer, uint attachment, uint pname, int* @params)
		{
			Functions.GetNamedFramebufferAttachmentParameteriv(framebuffer, attachment, pname, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetNamedFramebufferParameteriv(uint framebuffer, uint pname, int* param)
		{
			Functions.GetNamedFramebufferParameteriv(framebuffer, pname, param);
		}

		[OpenGLSupport(4.5)]
		public static void GetNamedRenderbufferParameteriv(uint renderbuffer, uint pname, int* @params)
		{
			Functions.GetNamedRenderbufferParameteriv(renderbuffer, pname, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetnCompressedTexImage(uint target, int lod, int bufSize, void* pixels)
		{
			Functions.GetnCompressedTexImage(target, lod, bufSize, pixels);
		}

		[OpenGLSupport(4.5)]
		public static void GetnTexImage(uint target, int level, uint format, uint type, int bufSize, void* pixels)
		{
			Functions.GetnTexImage(target, level, format, type, bufSize, pixels);
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

		[OpenGLSupport(3.0)]
		public static void GetRenderbufferParameteriv(uint target, uint pname, int* @params)
		{
			Functions.GetRenderbufferParameteriv(target, pname, @params);
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

		[OpenGLSupport(1.0)]
		public static void GetTexImage(uint target, int level, uint format, uint type, void* pixels)
		{
			Functions.GetTexImage(target, level, format, type, pixels);
		}
		
		[OpenGLSupport(4.5)]
		public static void GetTextureImage(uint texture, int level, uint format, uint type, int bufSize, void* pixels)
		{
			Functions.GetTextureImage(texture, level, format, type, bufSize, pixels);
		}

		[OpenGLSupport(4.5)]
		public static void GetTextureLevelParameterfv(uint texture, int level, uint pname, float* @params)
		{
			Functions.GetTextureLevelParameterfv(texture, level, pname, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetTextureLevelParameteriv(uint texture, int level, uint pname, int* @params)
		{
			Functions.GetTextureLevelParameteriv(texture, level, pname, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetTextureParameterfv(uint texture, uint pname, float* @params)
		{
			Functions.GetTextureParameterfv(texture, pname, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetTextureParameterIiv(uint texture, uint pname, int* @params)
		{
			Functions.GetTextureParameterIiv(texture, pname, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetTextureParameterIuiv(uint texture, uint pname, uint* @params)
		{
			Functions.GetTextureParameterIuiv(texture, pname, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetTextureParameteriv(uint texture, uint pname, int* @params)
		{
			Functions.GetTextureParameteriv(texture, pname, @params);
		}

		[OpenGLSupport(4.5)]
		public static void GetTextureSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, int bufSize, void* pixels)
		{
			Functions.GetTextureSubImage(texture, level, xoffset, yoffset, zoffset, width, height, depth, format, type, bufSize, pixels);
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

		[OpenGLSupport(4.3)]
		public static void InvalidateFramebuffer(uint target, int numAttachments, uint* attachments)
		{
			Functions.InvalidateFramebuffer(target, numAttachments, attachments);
		}

		[OpenGLSupport(4.5)]
		public static void InvalidateNamedFramebufferData(uint framebuffer, int numAttachments, uint* attachments)
		{
			Functions.InvalidateNamedFramebufferData(framebuffer, numAttachments, attachments);
		}

		[OpenGLSupport(4.5)]
		public static void InvalidateNamedFramebufferSubData(uint framebuffer, int numAttachments, uint* attachments, int x, int y, int width, int height)
		{
			Functions.InvalidateNamedFramebufferSubData(framebuffer, numAttachments, attachments, x, y, width, height);
		}

		[OpenGLSupport(4.3)]
		public static void InvalidateSubFramebuffer(uint target, int numAttachments, uint* attachments, int x, int y, int width, int height)
		{
			Functions.InvalidateSubFramebuffer(target, numAttachments, attachments, x, y, width, height);
		}

		[OpenGLSupport(4.3)]
		public static void InvalidateTexImage(uint texture, int level)
		{
			Functions.InvalidateTexImage(texture, level);
		}

		[OpenGLSupport(4.3)]
		public static void InvalidateTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth)
		{
			Functions.InvalidateTexSubImage(texture, level, xoffset, yoffset, zoffset, width, height, depth);
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

		[OpenGLSupport(4.5)]
		public static void NamedFramebufferDrawBuffer(uint framebuffer, uint buf)
		{
			Functions.NamedFramebufferDrawBuffer(framebuffer, buf);
		}

		[OpenGLSupport(4.5)]
		public static void NamedFramebufferDrawBuffers(uint framebuffer, int n, uint* bufs)
		{
			Functions.NamedFramebufferDrawBuffers(framebuffer, n, bufs);
		}

		[OpenGLSupport(4.5)]
		public static void NamedFramebufferParameteri(uint framebuffer, uint pname, int param)
		{
			Functions.NamedFramebufferParameteri(framebuffer, pname, param);
		}

		[OpenGLSupport(4.5)]
		public static void NamedFramebufferReadBuffer(uint framebuffer, uint src)
		{
			Functions.NamedFramebufferReadBuffer(framebuffer, src);
		}

		[OpenGLSupport(4.5)]
		public static void NamedFramebufferRenderbuffer(uint framebuffer, uint attachment, uint renderbuffertarget, uint renderbuffer)
		{
			Functions.NamedFramebufferRenderbuffer(framebuffer, attachment, renderbuffertarget, renderbuffer);
		}

		[OpenGLSupport(4.5)]
		public static void NamedFramebufferTexture(uint framebuffer, uint attachment, uint texture, int level)
		{
			Functions.NamedFramebufferTexture(framebuffer, attachment, texture, level);
		}

		[OpenGLSupport(4.5)]
		public static void NamedFramebufferTextureLayer(uint framebuffer, uint attachment, uint texture, int level, int layer)
		{
			Functions.NamedFramebufferTextureLayer(framebuffer, attachment, texture, level, layer);
		}

		[OpenGLSupport(4.5)]
		public static void NamedRenderbufferStorage(uint renderbuffer, uint internalformat, int width, int height)
		{
			Functions.NamedRenderbufferStorage(renderbuffer, internalformat, width, height);
		}

		[OpenGLSupport(4.5)]
		public static void NamedRenderbufferStorageMultisample(uint renderbuffer, int samples, uint internalformat, int width, int height)
		{
			Functions.NamedRenderbufferStorageMultisample(renderbuffer, samples, internalformat, width, height);
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

		[OpenGLSupport(1.0)]
		public static void ReadBuffer(uint src)
		{
			Functions.ReadBuffer(src);
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

		[OpenGLSupport(3.0)]
		public static void RenderbufferStorage(uint target, uint internalformat, int width, int height)
		{
			Functions.RenderbufferStorage(target, internalformat, width, height);
		}

		[OpenGLSupport(3.0)]
		public static void RenderbufferStorageMultisample(uint target, int samples, uint internalformat, int width, int height)
		{
			Functions.RenderbufferStorageMultisample(target, samples, internalformat, width, height);
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

		[OpenGLSupport(3.1)]
		public static void TexBuffer(uint target, uint internalformat, uint buffer)
		{
			Functions.TexBuffer(target, internalformat, buffer);
		}

		[OpenGLSupport(4.3)]
		public static void TexBufferRange(uint target, uint internalformat, uint buffer, int offset, int size)
		{
			Functions.TexBufferRange(target, internalformat, buffer, offset, size);
		}

		[OpenGLSupport(1.0)]
		public static void TexImage1D(uint target, int level, int internalformat, int width, int border, uint format, uint type, void* pixels)
		{
			Functions.TexImage1D(target, level, internalformat, width, border, format, type, pixels);
		}

		[OpenGLSupport(1.0)]
		public static void TexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, void* pixels)
		{
			Functions.TexImage2D(target, level, internalformat, width, height, border, format, type, pixels);
		}

		[OpenGLSupport(3.2)]
		public static void TexImage2DMultisample(uint target, int samples, uint internalformat, int width, int height, bool fixedsamplelocations)
		{
			Functions.TexImage2DMultisample(target, samples, internalformat, width, height, fixedsamplelocations);
		}

		[OpenGLSupport(1.2)]
		public static void TexImage3D(uint target, int level, int internalformat, int width, int height, int depth, int border, uint format, uint type, void* pixels)
		{
			Functions.TexImage3D(target, level, internalformat, width, height, depth, border, format, type, pixels);
		}

		[OpenGLSupport(3.2)]
		public static void TexImage3DMultisample(uint target, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations)
		{
			Functions.TexImage3DMultisample(target, samples, internalformat, width, height, depth, fixedsamplelocations);
		}

		[OpenGLSupport(4.2)]
		public static void TexStorage1D(uint target, int levels, uint internalformat, int width)
		{
			Functions.TexStorage1D(target, levels, internalformat, width);
		}

		[OpenGLSupport(4.2)]
		public static void TexStorage2D(uint target, int levels, uint internalformat, int width, int height)
		{
			Functions.TexStorage2D(target, levels, internalformat, width, height);
		}

		[OpenGLSupport(4.3)]
		public static void TexStorage2DMultisample(uint target, int samples, uint internalformat, int width, int height, bool fixedsamplelocations)
		{
			Functions.TexStorage2DMultisample(target, samples, internalformat, width, height, fixedsamplelocations);
		}

		[OpenGLSupport(4.2)]
		public static void TexStorage3D(uint target, int levels, uint internalformat, int width, int height, int depth)
		{
			Functions.TexStorage3D(target, levels, internalformat, width, height, depth);
		}

		[OpenGLSupport(4.3)]
		public static void TexStorage3DMultisample(uint target, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations)
		{
			Functions.TexStorage3DMultisample(target, samples, internalformat, width, height, depth, fixedsamplelocations);
		}

		[OpenGLSupport(1.1)]
		public static void TexSubImage1D(uint target, int level, int xoffset, int width, uint format, uint type, void* pixels)
		{
			Functions.TexSubImage1D(target, level, xoffset, width, format, type, pixels);
		}

		[OpenGLSupport(1.1)]
		public static void TexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, void* pixels)
		{
			Functions.TexSubImage2D(target, level, xoffset, yoffset, width, height, format, type, pixels);
		}

		[OpenGLSupport(1.2)]
		public static void TexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, void* pixels)
		{
			Functions.TexSubImage3D(target, level, xoffset, yoffset, zoffset, width, height, depth, format, type, pixels);
		}

		[OpenGLSupport(4.5)]
		public static void TextureBarrier()
		{
			Functions.TextureBarrier();
		}

		[OpenGLSupport(4.5)]
		public static void TextureBuffer(uint texture, uint internalformat, uint buffer)
		{
			Functions.TextureBuffer(texture, internalformat, buffer);
		}

		[OpenGLSupport(4.5)]
		public static void TextureBufferRange(uint texture, uint internalformat, uint buffer, int offset, int size)
		{
			Functions.TextureBufferRange(texture, internalformat, buffer, offset, size);
		}

		[OpenGLSupport(4.5)]
		public static void TextureParameterf(uint texture, uint pname, float param)
		{
			Functions.TextureParameterf(texture, pname, param);
		}

		[OpenGLSupport(4.5)]
		public static void TextureParameterfv(uint texture, uint pname, float* param)
		{
			Functions.TextureParameterfv(texture, pname, param);
		}

		[OpenGLSupport(4.5)]
		public static void TextureParameteri(uint texture, uint pname, int param)
		{
			Functions.TextureParameteri(texture, pname, param);
		}

		[OpenGLSupport(4.5)]
		public static void TextureParameterIiv(uint texture, uint pname, int* @params)
		{
			Functions.TextureParameterIiv(texture, pname, @params);
		}

		[OpenGLSupport(4.5)]
		public static void TextureParameterIuiv(uint texture, uint pname, uint* @params)
		{
			Functions.TextureParameterIuiv(texture, pname, @params);
		}

		[OpenGLSupport(4.5)]
		public static void TextureParameteriv(uint texture, uint pname, int* param)
		{
			Functions.TextureParameteriv(texture, pname, param);
		}

		[OpenGLSupport(4.5)]
		public static void TextureStorage1D(uint texture, int levels, uint internalformat, int width)
		{
			Functions.TextureStorage1D(texture, levels, internalformat, width);
		}

		[OpenGLSupport(4.5)]
		public static void TextureStorage2D(uint texture, int levels, uint internalformat, int width, int height)
		{
			Functions.TextureStorage2D(texture, levels, internalformat, width, height);
		}

		[OpenGLSupport(4.5)]
		public static void TextureStorage2DMultisample(uint texture, int samples, uint internalformat, int width, int height, bool fixedsamplelocations)
		{
			Functions.TextureStorage2DMultisample(texture, samples, internalformat, width, height, fixedsamplelocations);
		}

		[OpenGLSupport(4.5)]
		public static void TextureStorage3D(uint texture, int levels, uint internalformat, int width, int height, int depth)
		{
			Functions.TextureStorage3D(texture, levels, internalformat, width, height, depth);
		}

		[OpenGLSupport(4.5)]
		public static void TextureStorage3DMultisample(uint texture, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations)
		{
			Functions.TextureStorage3DMultisample(texture, samples, internalformat, width, height, depth, fixedsamplelocations);
		}

		[OpenGLSupport(4.5)]
		public static void TextureSubImage1D(uint texture, int level, int xoffset, int width, uint format, uint type, void* pixels)
		{
			Functions.TextureSubImage1D(texture, level, xoffset, width, format, type, pixels);
		}

		[OpenGLSupport(4.5)]
		public static void TextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, void* pixels)
		{
			Functions.TextureSubImage2D(texture, level, xoffset, yoffset, width, height, format, type, pixels);
		}

		[OpenGLSupport(4.5)]
		public static void TextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, void* pixels)
		{
			Functions.TextureSubImage3D(texture, level, xoffset, yoffset, zoffset, width, height, depth, format, type, pixels);
		}

		[OpenGLSupport(4.3)]
		public static void TextureView(uint texture, uint target, uint origtexture, uint internalformat, uint minlevel, uint numlevels, uint minlayer, uint numlayers)
		{
			Functions.TextureView(texture, target, origtexture, internalformat, minlevel, numlevels, minlayer, numlayers);
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
			BoundShaderProgram = program;

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

		[OpenGLSupport(1.0)]
		public static void Viewport(int x, int y, int width, int height)
		{
			Functions.Viewport(x, y, width, height);
		}

		[OpenGLSupport(4.1)]
		public static void ViewportArrayv(uint first, int count, float* v)
		{
			Functions.ViewportArrayv(first, count, v);
		}

		[OpenGLSupport(4.1)]
		public static void ViewportIndexedf(uint index, float x, float y, float w, float h)
		{
			Functions.ViewportIndexedf(index, x, y, w, h);
		}

		[OpenGLSupport(4.1)]
		public static void ViewportIndexedfv(uint index, float* v)
		{
			Functions.ViewportIndexedfv(index, v);
		}

		[OpenGLSupport(3.2)]
		public static void WaitSync(IntPtr sync, uint flags, ulong timeout)
		{
			Functions.WaitSync(sync, flags, timeout);
		}

	}
}
