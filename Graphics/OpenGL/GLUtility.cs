using System;
using System.Text;
using static Zene.Graphics.OpenGL.GL;

namespace Zene.Graphics.OpenGL
{
    public static class GLUtility
    {
        [OpenGLSupport(1.0)]
        public static void CheckErrors(string functionName)
        {
            uint error = GetError();

            if (error != GLEnum.NoError)
                throw new InvalidOperationException($"{functionName} resulted in error: {GetErrorString(error)}");
        }

        [OpenGLSupport(2.0)]
        public static uint CreateAndCompileShader(uint type, string source)
        {
            var shader = CreateShader(type);
            ShaderSource(shader, source);
            CompileShader(shader);

            CheckErrors(nameof(CompileShader));

            GetShaderiv(shader, GLEnum.CompileStatus, out var result);
            if (result == GLEnum.False)
            {
                string infoLog = GetShaderInfoLog(shader);
                throw new InvalidOperationException($"Failed to compile shader: {infoLog}");
            }

            return shader;
        }

        [OpenGLSupport(2.0)]
        public static uint CreateAndLinkProgram(params uint[] shaders)
        {
            uint program = CreateProgram();

            foreach (var shader in shaders)
                AttachShader(program, shader);

            LinkProgram(program);
            CheckErrors(nameof(LinkProgram));

            GetProgramiv(program, GLEnum.LinkStatus, out var result);
            if (result == GLEnum.False)
            {
                string infoLog = GetProgramInfoLog(program);
                throw new InvalidOperationException($"Failed to link program: {infoLog}");
            }

            return program;
        }

        public static string GetErrorString(uint error)
        {
            switch (error)
            {
                case GLEnum.NoError:
                    return nameof(GLEnum.NoError);
                case GLEnum.InvalidEnum:
                    return nameof(GLEnum.InvalidEnum);
                case GLEnum.InvalidValue:
                    return nameof(GLEnum.InvalidValue);
                case GLEnum.InvalidFramebufferOperation:
                    return nameof(GLEnum.InvalidFramebufferOperation);
                case GLEnum.InvalidOperation:
                    return nameof(GLEnum.InvalidOperation);
                case GLEnum.OutOfMemory:
                    return nameof(GLEnum.OutOfMemory);
                case GLEnum.StackOverflow:
                    return nameof(GLEnum.StackOverflow);
                case GLEnum.StackUnderflow:
                    return nameof(GLEnum.StackUnderflow);
                default:
                    break;
            }

            return "UNKNOWN";
        }

        [OpenGLSupport(2.0)]
        public static string GetProgramInfoLog(uint program)
        {
            GetProgramiv(program, GLEnum.InfoLogLength, out var infoLogLegth);

            StringBuilder infoLog = new StringBuilder(infoLogLegth);
            GL.GetProgramInfoLog(program, infoLog.Capacity, out _, infoLog);
            return infoLog.ToString();
        }

        [OpenGLSupport(2.0)]
        public static string GetShaderInfoLog(uint shader)
        {
            GetShaderiv(shader, GLEnum.InfoLogLength, out var infoLogLength);

            StringBuilder infoLog = new StringBuilder(infoLogLength);
            GL.GetShaderInfoLog(shader, infoLog.Capacity, out _, infoLog);
            return infoLog.ToString();
        }
    }
}
