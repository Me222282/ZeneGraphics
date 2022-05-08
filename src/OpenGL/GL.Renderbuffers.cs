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
    public unsafe partial class GL
    {
		[OpenGLSupport(3.0)]
		public static void BindRenderbuffer(uint target, uint renderbuffer)
		{
			if (target == GLEnum.Renderbuffer)
			{
				context.boundRenderbuffer = renderbuffer;
			}

			Functions.BindRenderbuffer(target, renderbuffer);
		}

		[OpenGLSupport(4.5)]
		public static void CreateRenderbuffers(int n, uint* renderbuffers)
		{
			Functions.CreateRenderbuffers(n, renderbuffers);
		}

		[OpenGLSupport(3.0)]
		public static void DeleteRenderbuffers(int n, uint* renderbuffers)
		{
			Functions.DeleteRenderbuffers(n, renderbuffers);
		}
		[OpenGLSupport(3.0)]
		public static void DeleteRenderbuffer(uint renderbuffer)
		{
			DeleteRenderbuffers(1, &renderbuffer);
		}

		[OpenGLSupport(3.0)]
		public static void GenRenderbuffers(int n, uint* renderbuffers)
		{
			Functions.GenRenderbuffers(n, renderbuffers);
		}
		[OpenGLSupport(3.0)]
		public static uint GenRenderbuffer()
		{
			uint renderbuffer;
			GenRenderbuffers(1, &renderbuffer);
			return renderbuffer;
		}

		[OpenGLSupport(4.5)]
		public static void GetNamedRenderbufferParameteriv(uint renderbuffer, uint pname, int* @params)
		{
			Functions.GetNamedRenderbufferParameteriv(renderbuffer, pname, @params);
		}
		[OpenGLSupport(3.0)]
		public static void GetRenderbufferParameteriv(uint target, uint pname, int* @params)
		{
			Functions.GetRenderbufferParameteriv(target, pname, @params);
		}

		[OpenGLSupport(3.0)]
		public static void RenderbufferStorage(IRenderbuffer target, uint internalformat, int width, int height)
		{
			Functions.RenderbufferStorage(GLEnum.Renderbuffer, internalformat, width, height);

			target.Properties._width = width;
			target.Properties._height = height;
			target.Properties._samples = 0;
			target.Properties.InternalFormatChanged();
		}
		[OpenGLSupport(3.0)]
		public static void RenderbufferStorageMultisample(IRenderbuffer target, int samples, uint internalformat, int width, int height)
		{
			Functions.RenderbufferStorageMultisample(GLEnum.Renderbuffer, samples, internalformat, width, height);

			target.Properties._width = width;
			target.Properties._height = height;
			target.Properties._samples = samples;
			target.Properties.InternalFormatChanged();
		}

		[OpenGLSupport(4.5)]
		public static void NamedRenderbufferStorage(IRenderbuffer renderbuffer, uint internalformat, int width, int height)
		{
			Functions.NamedRenderbufferStorage(renderbuffer.Id, internalformat, width, height);

			renderbuffer.Properties._width = width;
			renderbuffer.Properties._height = height;
			renderbuffer.Properties._samples = 0;
			renderbuffer.Properties.InternalFormatChanged();
		}
		[OpenGLSupport(4.5)]
		public static void NamedRenderbufferStorageMultisample(IRenderbuffer renderbuffer, int samples, uint internalformat, int width, int height)
		{
			Functions.NamedRenderbufferStorageMultisample(renderbuffer.Id, samples, internalformat, width, height);

			renderbuffer.Properties._width = width;
			renderbuffer.Properties._height = height;
			renderbuffer.Properties._samples = samples;
			renderbuffer.Properties.InternalFormatChanged();
		}
	}
}
