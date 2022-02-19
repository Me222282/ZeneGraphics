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
using static Zene.Graphics.Base.GL;

namespace Zene.Graphics.Base
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
