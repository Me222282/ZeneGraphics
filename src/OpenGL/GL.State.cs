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

using Zene.Structs;

namespace Zene.Graphics.Base
{
    public static unsafe partial class GL
    {
		[OpenGLSupport(1.0)]
		internal static void Enable(uint cap)
		{
			Functions.Enable(cap);
		}

		[OpenGLSupport(1.0)]
		internal static void Disable(uint cap)
		{
			Functions.Disable(cap);
		}

		internal static void SetDepthState(DepthState ds)
        {
			if (ds == null || context.depth == ds) { return; }

			if (context.boundFrameBuffers.Draw.LockedState &&
				context.boundFrameBuffers.Draw.DepthState != null)
            {
				return;
			}

			DepthState old = context.depth;
			context.depth = ds;

			if (old.testing != ds.testing)
            {
				if (ds.testing)
                {
					Functions.Enable(GLEnum.DepthTest);
                }
				else
                {
					Functions.Disable(GLEnum.DepthTest);
				}
            }

			if (old.clamp != ds.clamp)
			{
				if (ds.testing)
				{
					Functions.Disable(GLEnum.DepthClamp);
				}
				else
				{
					Functions.Enable(GLEnum.DepthClamp);
				}
			}

			if (old.mask != ds.mask)
			{
				Functions.DepthMask(ds.mask);
			}

			if (old.func != ds.func)
			{
				Functions.DepthFunc((uint)ds.func);
			}

			if (old.near != ds.near ||
				old.far != ds.far)
			{
				Functions.DepthRange(ds.near, ds.far);
			}
		}

		[OpenGLSupport(1.0)]
		internal static void DepthRange(double n, double f)
		{
			if (context.depth.near == n &&
				context.depth.far == f)
			{
				return;
			}

			context.depth.near = n;
			context.depth.far = f;

			Functions.DepthRange(n, f);
		}

		[OpenGLSupport(1.0)]
		internal static void DepthFunc(DepthFunction func)
		{
			if (context.depth.func == func) { return; }

			context.depth.func = func;

			Functions.DepthFunc((uint)func);
		}

		[OpenGLSupport(1.0)]
		internal static void DepthMask(bool flag)
		{
			if (context.depth.mask == flag) { return; }

			context.depth.mask = flag;

			Functions.DepthMask(flag);
		}

		internal static void SetViewState(Viewport vs)
        {
			if (vs == null || context.viewport == vs) { return; }

			if (context.boundFrameBuffers.Draw.LockedState &&
				context.boundFrameBuffers.Draw.Viewport != null)
			{
				return;
			}

			Viewport old = context.viewport;
			context.viewport = vs;

			if (old.view != vs.view)
            {
				Functions.Viewport(vs.view.X, vs.view.Y, vs.view.Width, vs.view.Height);
            }
        }

		[OpenGLSupport(1.0)]
		internal static void Viewport(RectangleI view)
		{
			if (context.viewport.view == view) { return; }

			context.viewport.view = view;

			Functions.Viewport(view.X, view.Y, view.Width, view.Height);
		}
	}
}
