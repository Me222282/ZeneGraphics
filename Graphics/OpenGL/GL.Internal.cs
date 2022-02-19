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

namespace Zene.Graphics.Base
{
    public static unsafe partial class GL
    {
		//
		// Is Object
		//

		[OpenGLSupport(1.5)]
		internal static bool IsBuffer(uint buffer)
		{
			return Functions.IsBuffer(buffer);
		}

		[OpenGLSupport(2.0)]
		internal static bool IsProgram(uint program)
		{
			return Functions.IsProgram(program);
		}

		[OpenGLSupport(4.1)]
		internal static bool IsProgramPipeline(uint pipeline)
		{
			return Functions.IsProgramPipeline(pipeline);
		}

		[OpenGLSupport(1.5)]
		internal static bool IsQuery(uint id)
		{
			return Functions.IsQuery(id);
		}

		[OpenGLSupport(3.0)]
		internal static bool IsRenderbuffer(uint renderbuffer)
		{
			return Functions.IsRenderbuffer(renderbuffer);
		}

		[OpenGLSupport(3.3)]
		internal static bool IsSampler(uint sampler)
		{
			return Functions.IsSampler(sampler);
		}

		[OpenGLSupport(2.0)]
		internal static bool IsShader(uint shader)
		{
			return Functions.IsShader(shader);
		}

		[OpenGLSupport(3.2)]
		internal static bool IsSync(IntPtr sync)
		{
			return Functions.IsSync(sync);
		}

		[OpenGLSupport(1.1)]
		internal static bool IsTexture(uint texture)
		{
			return Functions.IsTexture(texture);
		}

		[OpenGLSupport(4.0)]
		internal static bool IsTransformFeedback(uint id)
		{
			return Functions.IsTransformFeedback(id);
		}

		[OpenGLSupport(3.0)]
		internal static bool IsVertexArray(uint array)
		{
			return Functions.IsVertexArray(array);
		}

		//
		// Get texture param
		//

		[OpenGLSupport(1.0)]
		internal static void GetTexLevelParameterfv(uint target, int level, uint pname, float* @params)
		{
			Functions.GetTexLevelParameterfv(target, level, pname, @params);
		}

		[OpenGLSupport(1.0)]
		internal static void GetTexLevelParameteriv(uint target, int level, uint pname, int* @params)
		{
			Functions.GetTexLevelParameteriv(target, level, pname, @params);
		}

		[OpenGLSupport(1.0)]
		internal static void GetTexParameterfv(uint target, uint pname, float* @params)
		{
			Functions.GetTexParameterfv(target, pname, @params);
		}

		[OpenGLSupport(3.0)]
		internal static void GetTexParameterIiv(uint target, uint pname, int* @params)
		{
			Functions.GetTexParameterIiv(target, pname, @params);
		}

		[OpenGLSupport(3.0)]
		internal static void GetTexParameterIuiv(uint target, uint pname, uint* @params)
		{
			Functions.GetTexParameterIuiv(target, pname, @params);
		}

		[OpenGLSupport(1.0)]
		internal static void GetTexParameteriv(uint target, uint pname, int* @params)
		{
			Functions.GetTexParameteriv(target, pname, @params);
		}

		//
		// Set texture param
		//

		[OpenGLSupport(1.0)]
		internal static void TexParameterf(uint target, uint pname, float param)
		{
			Functions.TexParameterf(target, pname, param);
		}

		[OpenGLSupport(1.0)]
		internal static void TexParameterfv(uint target, uint pname, float* @params)
		{
			Functions.TexParameterfv(target, pname, @params);
		}

		[OpenGLSupport(1.0)]
		internal static void TexParameteri(uint target, uint pname, int param)
		{
			Functions.TexParameteri(target, pname, param);
		}

		[OpenGLSupport(3.0)]
		internal static void TexParameterIiv(uint target, uint pname, int* @params)
		{
			Functions.TexParameterIiv(target, pname, @params);
		}

		[OpenGLSupport(3.0)]
		internal static void TexParameterIuiv(uint target, uint pname, uint* @params)
		{
			Functions.TexParameterIuiv(target, pname, @params);
		}

		[OpenGLSupport(1.0)]
		internal static void TexParameteriv(uint target, uint pname, int* @params)
		{
			Functions.TexParameteriv(target, pname, @params);
		}
	}
}
