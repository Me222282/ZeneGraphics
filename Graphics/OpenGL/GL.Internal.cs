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

		[OpenGLSupport(3.0)]
		internal static bool IsFramebuffer(uint framebuffer)
		{
			return Functions.IsFramebuffer(framebuffer);
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
