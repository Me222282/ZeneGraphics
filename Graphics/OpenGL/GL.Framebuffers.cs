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

namespace Zene.Graphics.Base
{
    public unsafe partial class GL
    {
		public struct FrameBufferBinding
		{
			public uint Read;
			public uint Draw;
		}

		private static FrameBufferBinding _boundFrameBuffers = new FrameBufferBinding();
		public static FrameBufferBinding BoundFrameBuffers => _boundFrameBuffers;

		[OpenGLSupport(3.0)]
		internal static void GenFramebuffers(int n, uint* framebuffers)
		{
			Functions.GenFramebuffers(n, framebuffers);
		}
		[OpenGLSupport(3.0)]
		public static uint GenFramebuffer()
		{
			uint framebuffer;
			Functions.GenFramebuffers(1, &framebuffer);
			return framebuffer;
		}

		[OpenGLSupport(4.5)]
		public static void CreateFramebuffers(int n, uint* framebuffers)
		{
			Functions.CreateFramebuffers(n, framebuffers);
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

		[OpenGLSupport(3.0)]
		internal static void DeleteFramebuffers(int n, uint* framebuffers)
		{
			Functions.DeleteFramebuffers(n, framebuffers);
		}
		[OpenGLSupport(3.0)]
		public static void DeleteFramebuffer(uint framebuffer)
		{
			Functions.DeleteFramebuffers(1, &framebuffer);
		}

		[OpenGLSupport(3.0)]
		internal static bool IsFramebuffer(uint framebuffer)
		{
			return Functions.IsFramebuffer(framebuffer);
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
		public static void FramebufferRenderbuffer(IFramebuffer target, uint attachment, uint renderbuffertarget, IRenderbuffer renderbuffer)
		{
			if (renderbuffer == null)
			{
				Functions.FramebufferRenderbuffer((uint)target.Binding, attachment, renderbuffertarget, 0);
				target.Properties._attachments.Remove(attachment);
				return;
			}

			Functions.FramebufferRenderbuffer((uint)target.Binding, attachment, renderbuffertarget, renderbuffer.Id);

			target.Properties._attachments.Add(renderbuffer, attachment);
			target.Properties._samples = renderbuffer.Properties.Samples;
		}
		[OpenGLSupport(4.5)]
		public static void NamedFramebufferRenderbuffer(IFramebuffer framebuffer, uint attachment, uint renderbuffertarget, IRenderbuffer renderbuffer)
		{
			if (renderbuffer == null)
			{
				Functions.NamedFramebufferRenderbuffer(framebuffer.Id, attachment, renderbuffertarget, 0);
				framebuffer.Properties._attachments.Remove(attachment);
				return;
			}

			Functions.NamedFramebufferRenderbuffer(framebuffer.Id, attachment, renderbuffertarget, renderbuffer.Id);

			framebuffer.Properties._attachments.Add(renderbuffer, attachment);
			framebuffer.Properties._samples = renderbuffer.Properties.Samples;
		}

		[OpenGLSupport(4.5)]
		public static void NamedFramebufferTexture(IFramebuffer framebuffer, uint attachment, ITexture texture, int level)
		{
			if (texture == null)
            {
				Functions.NamedFramebufferTexture(framebuffer.Id, attachment, 0, level);
				framebuffer.Properties._attachments.Remove(attachment);
				return;
			}

			Functions.NamedFramebufferTexture(framebuffer.Id, attachment, texture.Id, level);

			framebuffer.Properties._attachments.Add(texture, attachment);
			framebuffer.Properties._samples = texture.Properties.Samples;
		}
		[OpenGLSupport(3.2)]
		public static void FramebufferTexture(IFramebuffer target, uint attachment, ITexture texture, int level)
		{
			if (texture == null)
			{
				Functions.FramebufferTexture((uint)target.Binding, attachment, 0, level);
				target.Properties._attachments.Remove(attachment);
				return;
			}

			Functions.FramebufferTexture((uint)target.Binding, attachment, texture.Id, level);

			target.Properties._attachments.Add(texture, attachment);
			target.Properties._samples = texture.Properties.Samples;
		}

		[OpenGLSupport(3.0)]
		public static void FramebufferTexture1D(IFramebuffer target, uint attachment, uint textarget, ITexture texture, int level)
		{
			if (texture == null)
			{
				Functions.FramebufferTexture1D((uint)target.Binding, attachment, textarget, 0, level);
				target.Properties._attachments.Remove(attachment);
				return;
			}

			Functions.FramebufferTexture1D((uint)target.Binding, attachment, textarget, texture.Id, level);

			target.Properties._attachments.Add(texture, attachment);
			target.Properties._samples = texture.Properties.Samples;
		}

		[OpenGLSupport(3.0)]
		public static void FramebufferTexture2D(IFramebuffer target, uint attachment, uint textarget, ITexture texture, int level)
		{
			if (texture == null)
			{
				Functions.FramebufferTexture2D((uint)target.Binding, attachment, textarget, 0, level);
				target.Properties._attachments.Remove(attachment);
				return;
			}

			Functions.FramebufferTexture2D((uint)target.Binding, attachment, textarget, texture.Id, level);

			target.Properties._attachments.Add(texture, attachment);
			target.Properties._samples = texture.Properties.Samples;
		}

		[OpenGLSupport(3.0)]
		public static void FramebufferTexture3D(IFramebuffer target, uint attachment, uint textarget, ITexture texture, int level, int zoffset)
		{
			if (texture == null)
			{
				Functions.FramebufferTexture3D((uint)target.Binding, attachment, textarget, 0, level, zoffset);
				target.Properties._attachments.Remove(attachment);
				return;
			}

			Functions.FramebufferTexture3D((uint)target.Binding, attachment, textarget, texture.Id, level, zoffset);

			target.Properties._attachments.Add(texture, attachment);
			target.Properties._samples = texture.Properties.Samples;
		}

		[OpenGLSupport(3.0)]
		public static void FramebufferTextureLayer(IFramebuffer target, uint attachment, ITexture texture, int level, int layer)
		{
			if (texture == null)
			{
				Functions.FramebufferTextureLayer((uint)target.Binding, attachment, 0, level, layer);
				target.Properties._attachments.Remove(attachment);
				return;
			}

			Functions.FramebufferTextureLayer((uint)target.Binding, attachment, texture.Id, level, layer);

			target.Properties._attachments.Add(texture, attachment);
			target.Properties._samples = texture.Properties.Samples;
		}
		[OpenGLSupport(4.5)]
		public static void NamedFramebufferTextureLayer(IFramebuffer framebuffer, uint attachment, ITexture texture, int level, int layer)
		{
			if (texture == null)
			{
				Functions.NamedFramebufferTextureLayer(framebuffer.Id, attachment, 0, level, layer);
				framebuffer.Properties._attachments.Remove(attachment);
				return;
			}

			Functions.NamedFramebufferTextureLayer(framebuffer.Id, attachment, texture.Id, level, layer);

			framebuffer.Properties._attachments.Add(texture, attachment);
			framebuffer.Properties._samples = texture.Properties.Samples;
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

		[OpenGLSupport(4.3)]
		public static void FramebufferParameteri(uint target, uint pname, int param)
		{
			Functions.FramebufferParameteri(target, pname, param);
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

		[OpenGLSupport(1.0)]
		public static void Clear(uint mask)
		{
			Functions.Clear(mask);
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
		[OpenGLSupport(1.0)]
		public static void ClearStencil(int s)
		{
			Functions.ClearStencil(s);
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
		[OpenGLSupport(1.0)]
		public static void ReadBuffer(uint src)
		{
			Functions.ReadBuffer(src);
		}
	}
}
