using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Zene.Graphics.Base
{
    public static unsafe partial class GL
    {
        [OpenGLSupport(1.0)]
        public static float GetTexLevelParameterfv(uint target, int level, uint pname)
        {
            float value = 0;
            GetTexLevelParameterfv(target, level, pname, &value);
            return value;
        }
        [OpenGLSupport(1.0)]
        public static int GetTexLevelParameteriv(uint target, int level, uint pname)
        {
            int value = 0;
            GetTexLevelParameteriv(target, level, pname, &value);
            return value;
        }

        [OpenGLSupport(1.5)]
        public static void BufferData(uint target, int size, IntPtr data, uint usage)
        {
            BufferData(target, size, data.ToPointer(), usage);
        }

        [OpenGLSupport(1.5)]
        public static void BufferData<T>(uint target, int size, T[] data, uint usage)
            where T: unmanaged
        {
            fixed (void* dataPtr = data)
            {
                BufferData(target, size, dataPtr, usage);
            }
        }

        [OpenGLSupport(1.5)]
        public static void BufferSubData<T>(uint target, int offset, int size, T[] data)
            where T : unmanaged
        {
            fixed (void* dataPtr = data)
            {
                BufferSubData(target, offset, size, dataPtr);
            }
        }

        [OpenGLSupport(1.5)]
        public static void DeleteBuffer(uint buffer)
        {
            DeleteBuffers(1, &buffer);
        }

        [OpenGLSupport(3.0)]
        public static void DeleteRenderbuffer(uint renderbuffer)
        {
            DeleteRenderbuffers(1, &renderbuffer);
        }

        [OpenGLSupport(3.0)]
        public static void DeleteVertexArray(uint vertexArray)
        {
            DeleteVertexArrays(1, &vertexArray);
        }

        [OpenGLSupport(1.1)]
        public static void DrawElements(uint mode, int count, uint type, IntPtr indices)
        {
            DrawElements(mode, count, type, indices.ToPointer());
        }

        [OpenGLSupport(1.5)]
        public static uint GenBuffer()
        {
            uint buffer;
            GenBuffers(1, &buffer);
            return buffer;
        }

        [OpenGLSupport(3.0)]
        public static uint GenRenderbuffer()
        {
            uint renderbuffer;
            GenRenderbuffers(1, &renderbuffer);
            return renderbuffer;
        }

        [OpenGLSupport(3.0)]
        public static uint GenVertexArray()
        {
            uint vertexArray;
            GenVertexArrays(1, &vertexArray);
            return vertexArray;
        }

        [OpenGLSupport(1.0)]
        public static void GetIntegerv(uint pname, ref int data)
        {
            fixed (int* dataPtr = &data)
            {
                GetIntegerv(pname, dataPtr);
            }
        }

        [OpenGLSupport(2.0)]
        public static int GetProgrami(uint program, uint pname)
        {
            int param = 0;
            GetProgramiv(program, pname, &param);
            return param;
        }

        [OpenGLSupport(2.0)]
        public static void GetProgramInfoLog(uint program, int bufSize, out int length, StringBuilder infoLog)
        {
            length = default;
            fixed (int* lengthPtr = &length)
            {
                GetProgramInfoLog(program, bufSize, lengthPtr, infoLog);
            }
        }

        [OpenGLSupport(2.0)]
        public static void GetProgramiv(uint program, uint pname, out int @params)
        {
            @params = default;
            fixed (int* paramsPtr = &@params)
            {
                GetProgramiv(program, pname, paramsPtr);
            }
        }

        [OpenGLSupport(2.0)]
        public static int GetShaderi(uint shader, uint pname)
        {
            int param = 0;
            GetShaderiv(shader, pname, &param);
            return param;
        }

        [OpenGLSupport(2.0)]
        public static void GetShaderInfoLog(uint shader, int bufSize, out int length, StringBuilder infoLog)
        {
            length = default;
            fixed (int* lengthPtr = &length)
            {
                GetShaderInfoLog(shader, bufSize, lengthPtr, infoLog);
            }
        }

        [OpenGLSupport(2.0)]
        public static void GetShaderiv(uint shader, uint pname, out int @params)
        {
            @params = default;
            fixed (int* paramsPtr = &@params)
            {
                GetShaderiv(shader, pname, paramsPtr);
            }
        }

        [OpenGLSupport(2.0)]
        public static void GetShaderSource(uint shader, int bufSize, out int length, StringBuilder source)
        {
            length = default;
            fixed (int* lengthPtr = &@length)
            {
                GetShaderSource(shader, bufSize, lengthPtr, source);
            }
        }

        [OpenGLSupport(2.0)]
        public static void ShaderSource(uint shader, string @string)
        {
            var stringArray = new string[] { @string };
            int length = @string.Length;
            ShaderSource(shader, 1, stringArray, &length);
        }

        [OpenGLSupport(1.0)]
        public static void TexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, IntPtr pixels)
        {
            TexImage2D(target, level, internalformat, width, height, border, format, type, pixels.ToPointer());
        }

        [OpenGLSupport(2.0)]
        public static void UniformMatrix4fv(int location, int count, bool transpose, ref float value)
        {
            fixed (float* valuePtr = &value)
            {
                UniformMatrix4fv(location, count, transpose, valuePtr);
            }
        }

        [OpenGLSupport(2.0)]
        public static void VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)
        {
            VertexAttribPointer(index, size, type, normalized, stride, pointer.ToPointer());
        }

#if !GLDOTNET_EXCLUDE_SYSTEM_MEMORY
        [OpenGLSupport(1.5)]
        public static void BufferData<T>(uint target, ReadOnlySpan<T> data, uint usage)
            where T: unmanaged
        {
            fixed (void* dataPtr = &MemoryMarshal.GetReference(data))
            {
                BufferData(target, Marshal.SizeOf<T>() * data.Length, dataPtr, usage);
            }
        }

        [OpenGLSupport(1.5)]
        public static unsafe void BufferSubData<T>(uint target, int offset, ReadOnlySpan<T> data)
            where T: unmanaged
        {
            fixed (void* dataPtr = &MemoryMarshal.GetReference(data))
            {
                BufferSubData(target, offset, Marshal.SizeOf<T>() * data.Length, dataPtr);
            }
        }

        [OpenGLSupport(1.0)]
        public static unsafe void TexImage1D<T>(uint target, int level, int internalformat, int width, int border, uint format, uint type, ReadOnlySpan<T> pixels)
            where T: unmanaged
        {
            fixed (void* dataPtr = &MemoryMarshal.GetReference(pixels))
            {
                TexImage1D(target, level, internalformat, width, border, format, type, dataPtr);
            }
        }

        [OpenGLSupport(1.0)]
        public static unsafe void TexImage2D<T>(uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, ReadOnlySpan<T> pixels)
            where T: unmanaged
        {
            fixed (void* dataPtr = &MemoryMarshal.GetReference(pixels))
            {
                TexImage2D(target, level, internalformat, width, height, border, format, type, dataPtr);
            }
        }

        [OpenGLSupport(1.2)]
        public static unsafe void TexImage3D<T>(uint target, int level, int internalformat, int width, int height, int depth, int border, uint format, uint type, ReadOnlySpan<T> pixels)
            where T: unmanaged
        {
            fixed (void* dataPtr = &MemoryMarshal.GetReference(pixels))
            {
                TexImage3D(target, level, internalformat, width, height, depth, border, format, type, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix2dv(uint program, int location, bool transpose, ReadOnlySpan<double> matrix)
        {
            fixed (double* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix2dv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix2x3dv(uint program, int location, bool transpose, ReadOnlySpan<double> matrix)
        {
            fixed (double* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix2x3dv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix2x4dv(uint program, int location, bool transpose, ReadOnlySpan<double> matrix)
        {
            fixed (double* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix2x4dv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix3dv(uint program, int location, bool transpose, ReadOnlySpan<double> matrix)
        {
            fixed (double* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix3dv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix3x2dv(uint program, int location, bool transpose, ReadOnlySpan<double> matrix)
        {
            fixed (double* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix3x2dv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix3x4dv(uint program, int location, bool transpose, ReadOnlySpan<double> matrix)
        {
            fixed (double* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix3x4dv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix4dv(uint program, int location, bool transpose, ReadOnlySpan<double> matrix)
        {
            fixed (double* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix4dv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix4x2dv(uint program, int location, bool transpose, ReadOnlySpan<double> matrix)
        {
            fixed (double* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix4x2dv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix4x3dv(uint program, int location, bool transpose, ReadOnlySpan<double> matrix)
        {
            fixed (double* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix4x3dv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix2fv(uint program, int location, bool transpose, ReadOnlySpan<float> matrix)
        {
            fixed (float* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix2fv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix2x3fv(uint program, int location, bool transpose, ReadOnlySpan<float> matrix)
        {
            fixed (float* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix2x3fv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix2x4fv(uint program, int location, bool transpose, ReadOnlySpan<float> matrix)
        {
            fixed (float* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix2x4fv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix3fv(uint program, int location, bool transpose, ReadOnlySpan<float> matrix)
        {
            fixed (float* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix4fv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix3x2fv(uint program, int location, bool transpose, ReadOnlySpan<float> matrix)
        {
            fixed (float* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix3x2fv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix3x4fv(uint program, int location, bool transpose, ReadOnlySpan<float> matrix)
        {
            fixed (float* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix3x4fv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix4fv(uint program, int location, bool transpose, ReadOnlySpan<float> matrix)
        {
            fixed (float* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix4fv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix4x2fv(uint program, int location, bool transpose, ReadOnlySpan<float> matrix)
        {
            fixed (float* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix4x2fv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.1)]
        public static unsafe void ProgramUniformMatrix4x3fv(uint program, int location, bool transpose, ReadOnlySpan<float> matrix)
        {
            fixed (float* dataPtr = &MemoryMarshal.GetReference(matrix))
            {
                ProgramUniformMatrix4x3fv(program, location, 1, transpose, dataPtr);
            }
        }

        [OpenGLSupport(4.3)]
        public static void DebugMessageCallback(DebugProc callback, IntPtr userParam)
        {
            DebugMessageCallback(callback, userParam.ToPointer());
        }
#endif
    }
}
