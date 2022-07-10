using System;
using Zene.Structs;

namespace Zene.Graphics.Base
{
    public unsafe class ShaderProgramGL : IShaderProgram
    {
        public ShaderProgramGL()
        {
            Id = GL.CreateProgram();
        }

        public uint Id { get; }

        public void Bind()
        {
            if (this.Bound()) { return; }

            GL.UseProgram(Id);
        }
        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            GL.DeleteProgram(Id);

            _disposed = true;
            GC.SuppressFinalize(this);
        }
        public void Unbind()
        {
            if (!this.Bound()) { return; }

            GL.UseProgram(0);
        }

        /// <summary>
        /// Validates this program.
        /// </summary>
        public void Validate()
        {
            GL.ValidateProgram(Id);
        }

        /// <summary>
        /// Attaches a shader object to this program.
        /// </summary>
        /// <param name="shader">Specifies the shader object that is to be attached.</param>
        public void AttachShader(IShader shader)
        {
            GL.AttachShader(Id, shader.Id);
        }
        /// <summary>
        /// Detaches a shader object from this program to which it was attached.
        /// </summary>
        /// <param name="shader">Specifies the shader object to be detached.</param>
        public void DetachShader(IShader shader)
        {
            GL.DetachShader(Id, shader.Id);
        }

        /// <summary>
        /// Associates a generic vertex attribute index with a named attribute variable.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be bound.</param>
        /// <param name="name">Specifies a string containing the name of the vertex shader attribute variable to which <paramref name="index"/> is to be bound.</param>
        public void BindAttribLocation(uint index, string name)
        {
            GL.BindAttribLocation(Id, index, name);
        }
        /// <summary>
        /// Bind a user-defined varying out variable to a fragment shader colour number.
        /// </summary>
        /// <param name="colourAttach">The colour number to bind the user-defined varying out variable to.</param>
        /// <param name="name">The name of the user-defined varying out variable whose binding to modify.</param>
        public void BindFragDataLocation(uint colourAttach, string name)
        {
            GL.BindFragDataLocation(Id, colourAttach, name);
        }
        /// <summary>
        /// Bind a user-defined varying out variable to a fragment shader colour number and index.
        /// </summary>
        /// <param name="colourAttach">The colour number to bind the user-defined varying out variable to.</param>
        /// <param name="index">The index of the color input to bind the user-defined varying out variable to.</param>
        /// <param name="name">The name of the user-defined varying out variable whose binding to modify.</param>
        public void BindFragDataLocationIndexed(uint colourAttach, ColourIndex index, string name)
        {
            GL.BindFragDataLocationIndexed(Id, colourAttach, (uint)index, name);
        }

        /// <summary>
        /// Links this program.
        /// </summary>
        public void LinkProgram()
        {
            GL.LinkProgram(Id);
        }
        /// <summary>
        /// Loads this program with the binary representation of <paramref name="program"/>'s compiled and linked executable source.
        /// </summary>
        /// <param name="program">The program to get binaries from.</param>
        public void ProgramBinary(IShaderProgram program)
        {
            int length = 0;
            GL.GetProgramiv(program.Id, GLEnum.ProgramBinaryLength, &length);

            byte* data = stackalloc byte[length];
            uint format = 0;

            GL.GetProgramBinary(program.Id, length, null, &format, data);

            GL.ProgramBinary(Id, format, data, length);
        }

        /// <summary>
        /// Change an active shader storage block binding.
        /// </summary>
        /// <param name="storageBlockIndex">The index storage block within the program.</param>
        /// <param name="storageBlockBinding">The index storage block binding to associate with the specified storage block.</param>
        public void ShaderStorageBlockBinding(uint storageBlockIndex, uint storageBlockBinding)
        {
            GL.ShaderStorageBlockBinding(Id, storageBlockIndex, storageBlockBinding);
        }
        /// <summary>
        /// Assign a binding point to an active uniform block.
        /// </summary>
        /// <param name="uniformBlockIndex">The index of the active uniform block within program whose binding to assign.</param>
        /// <param name="uniformBlockBinding">Specifies the binding point to which to bind the uniform block with index <paramref name="uniformBlockIndex"/> within this program.</param>
        public void UniformBlockBinding(uint uniformBlockIndex, uint uniformBlockBinding)
        {
            GL.UniformBlockBinding(Id, uniformBlockIndex, uniformBlockBinding);
        }

        /// <summary>
        /// Load active subroutine uniforms.
        /// </summary>
        /// <param name="type">Specifies the shader stage from which to query for subroutine uniform index.</param>
        /// <param name="indices">Specifies an array holding the indices to load into the shader subroutine variables.</param>
        public void UniformSubroutines(ShaderType type, uint[] indices)
        {
            Bind();

            fixed (uint* ptr = &indices[0])
            {
                GL.UniformSubroutinesuiv((uint)type, indices.Length, ptr);
            }
        }

        //
        // Uniforms
        //

        /// <summary>
        /// Specify the value of a float uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformF(int location, double value)
        {
            Bind();

            GL.Uniform1f(location, (float)value);
        }
        /// <summary>
        /// Specify the value of a double uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformD(int location, double value)
        {
            Bind();

            GL.Uniform1d(location, value);
        }
        /// <summary>
        /// Specify the value of a float uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformF(int location, float value)
        {
            Bind();

            GL.Uniform1f(location, value);
        }
        /// <summary>
        /// Specify the value of an integer uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformI(int location, int value)
        {
            Bind();

            GL.Uniform1i(location, value);
        }
        /// <summary>
        /// Specify the value of an unsigned integer uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformUI(int location, uint value)
        {
            Bind();

            GL.Uniform1ui(location, value);
        }

        /// <summary>
        /// Specify the values of a float array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformF(int location, double[] values)
        {
            Bind();

            float* data = stackalloc float[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                data[i] = (float)values[i];
            }

            GL.Uniform1fv(location, values.Length, data);
        }
        /// <summary>
        /// Specify the values of a double array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformD(int location, double[] values)
        {
            Bind();

            fixed (double* ptr = &values[0])
            {
                GL.Uniform1dv(location, values.Length, ptr);
            }
        }
        /// <summary>
        /// Specify the values of a float array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformF(int location, float[] values)
        {
            Bind();

            fixed (float* ptr = &values[0])
            {
                GL.Uniform1fv(location, values.Length, ptr);
            }
        }
        /// <summary>
        /// Specify the values of an integer array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformI(int location, int[] values)
        {
            Bind();

            fixed (int* ptr = &values[0])
            {
                GL.Uniform1iv(location, values.Length, ptr);
            }
        }
        /// <summary>
        /// Specify the values of an unsigned integer array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformUI(int location, uint[] values)
        {
            Bind();

            fixed (uint* ptr = &values[0])
            {
                GL.Uniform1uiv(location, values.Length, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a float vector2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformF(int location, Vector2 value)
        {
            Bind();

            GL.Uniform2f(location, (float)value.X, (float)value.Y);
        }
        /// <summary>
        /// Specify the value of a double vector2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformD(int location, Vector2 value)
        {
            Bind();

            GL.Uniform2d(location, value.X, value.Y);
        }
        /// <summary>
        /// Specify the value of a float vector2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformF(int location, Vector2<float> value)
        {
            Bind();

            GL.Uniform2f(location, value.X, value.Y);
        }
        /// <summary>
        /// Specify the value of an integer vector2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformI(int location, Vector2I value)
        {
            Bind();

            GL.Uniform2i(location, value.X, value.Y);
        }
        /// <summary>
        /// Specify the value of an unsigned integer vector2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformUI(int location, Vector2<uint> value)
        {
            Bind();

            GL.Uniform2ui(location, value.X, value.Y);
        }

        /// <summary>
        /// Specify the values of a float vector2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformF(int location, Vector2[] values)
        {
            Bind();

            int size = values.Length * 2;
            float* data = stackalloc float[size];

            for (int i = 0; i < size; i += 2)
            {
                data[i] = (float)values[i].X;
                data[i + 1] = (float)values[i].Y;
            }

            GL.Uniform2fv(location, values.Length, data);
        }
        /// <summary>
        /// Specify the values of a double vector2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformD(int location, Vector2[] values)
        {
            Bind();

            fixed (Vector2* ptr = &values[0])
            {
                GL.Uniform2dv(location, values.Length, (double*)ptr);
            }
        }
        /// <summary>
        /// Specify the values of a float vector2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformF(int location, Vector2<float>[] values)
        {
            Bind();

            fixed (Vector2<float>* ptr = &values[0])
            {
                GL.Uniform2fv(location, values.Length, (float*)ptr);
            }
        }
        /// <summary>
        /// Specify the values of an integer vector2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformI(int location, Vector2I[] values)
        {
            Bind();

            fixed (Vector2I* ptr = &values[0])
            {
                GL.Uniform2iv(location, values.Length, (int*)ptr);
            }
        }
        /// <summary>
        /// Specify the values of an unsigned integer vector2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformUI(int location, Vector2<uint>[] values)
        {
            Bind();

            fixed (Vector2<uint>* ptr = &values[0])
            {
                GL.Uniform2uiv(location, values.Length, (uint*)ptr);
            }
        }

        /// <summary>
        /// Specify the value of a float vector3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformF(int location, Vector3 value)
        {
            Bind();

            GL.Uniform3f(location, (float)value.X, (float)value.Y, (float)value.Z);
        }
        /// <summary>
        /// Specify the value of a double vector3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformD(int location, Vector3 value)
        {
            Bind();

            GL.Uniform3d(location, value.X, value.Y, value.Z);
        }
        /// <summary>
        /// Specify the value of a float vector3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformF(int location, Vector3<float> value)
        {
            Bind();

            GL.Uniform3f(location, value.X, value.Y, value.Z);
        }
        /// <summary>
        /// Specify the value of an integer vector3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformI(int location, Vector3I value)
        {
            Bind();

            GL.Uniform3i(location, value.X, value.Y, value.Z);
        }
        /// <summary>
        /// Specify the value of an unsigned integer vector3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformUI(int location, Vector3<uint> value)
        {
            Bind();

            GL.Uniform3ui(location, value.X, value.Y, value.Z);
        }

        /// <summary>
        /// Specify the values of a float vector3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformF(int location, Vector3[] values)
        {
            Bind();

            int size = values.Length * 3;
            float* data = stackalloc float[size];

            for (int i = 0; i < size; i += 3)
            {
                data[i] = (float)values[i].X;
                data[i + 1] = (float)values[i].Y;
                data[i + 2] = (float)values[i].Z;
            }

            GL.Uniform3fv(location, values.Length, data);
        }
        /// <summary>
        /// Specify the values of a double vector3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformD(int location, Vector3[] values)
        {
            Bind();

            fixed (Vector3* ptr = &values[0])
            {
                GL.Uniform3dv(location, values.Length, (double*)ptr);
            }
        }
        /// <summary>
        /// Specify the values of a float vector3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformF(int location, Vector3<float>[] values)
        {
            Bind();

            fixed (Vector3<float>* ptr = &values[0])
            {
                GL.Uniform3fv(location, values.Length, (float*)ptr);
            }
        }
        /// <summary>
        /// Specify the values of an integer vector3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformI(int location, Vector3I[] values)
        {
            Bind();

            fixed (Vector3I* ptr = &values[0])
            {
                GL.Uniform3iv(location, values.Length, (int*)ptr);
            }
        }
        /// <summary>
        /// Specify the values of an unsigned integer vecotr3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformUI(int location, Vector3<uint>[] values)
        {
            Bind();

            fixed (Vector3<uint>* ptr = &values[0])
            {
                GL.Uniform3uiv(location, values.Length, (uint*)ptr);
            }
        }

        /// <summary>
        /// Specify the value of a float vector4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformF(int location, Vector4 value)
        {
            Bind();

            GL.Uniform4f(location, (float)value.X, (float)value.Y, (float)value.Z, (float)value.W);
        }
        /// <summary>
        /// Specify the value of a double vector4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformD(int location, Vector4 value)
        {
            Bind();

            GL.Uniform4d(location, value.X, value.Y, value.Z, value.W);
        }
        /// <summary>
        /// Specify the value of a float vector4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformF(int location, Vector4<float> value)
        {
            Bind();

            GL.Uniform4f(location, value.X, value.Y, value.Z, value.W);
        }
        /// <summary>
        /// Specify the value of an integer vector4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformI(int location, Vector4I value)
        {
            Bind();

            GL.Uniform4i(location, value.X, value.Y, value.Z, value.W);
        }
        /// <summary>
        /// Specify the value of an unsigned integer vector4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniformUI(int location, Vector4<uint> value)
        {
            Bind();

            GL.Uniform4ui(location, value.X, value.Y, value.Z, value.W);
        }

        /// <summary>
        /// Specify the values of a float vector4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformF(int location, Vector4[] values)
        {
            Bind();

            int size = values.Length * 4;
            float* data = stackalloc float[size];

            for (int i = 0; i < size; i += 4)
            {
                data[i] = (float)values[i].X;
                data[i + 1] = (float)values[i].Y;
                data[i + 2] = (float)values[i].Z;
                data[i + 3] = (float)values[i].W;
            }

            GL.Uniform4fv(location, values.Length, data);
        }
        /// <summary>
        /// Specify the values of a double vector4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformD(int location, Vector4[] values)
        {
            Bind();

            fixed (Vector4* ptr = &values[0])
            {
                GL.Uniform4dv(location, values.Length, (double*)ptr);
            }
        }
        /// <summary>
        /// Specify the values of a float vector4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformF(int location, Vector4<float>[] values)
        {
            Bind();

            fixed (Vector4<float>* ptr = &values[0])
            {
                GL.Uniform4fv(location, values.Length, (float*)ptr);
            }
        }
        /// <summary>
        /// Specify the values of an integer vector4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformI(int location, Vector4I[] values)
        {
            Bind();

            fixed (Vector4I* ptr = &values[0])
            {
                GL.Uniform4iv(location, values.Length, (int*)ptr);
            }
        }
        /// <summary>
        /// Specify the value of an unsigned integer vector4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniformUI(int location, Vector4<uint>[] values)
        {
            Bind();

            fixed (Vector4<uint>* ptr = &values[0])
            {
                GL.Uniform4uiv(location, values.Length, (uint*)ptr);
            }
        }

        //
        // Matrix uniforms
        //

        /// <summary>
        /// Specify the value of a float matrix2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, ref Matrix2 matrix)
        {
            Bind();

            float* data = stackalloc float[2 * 2]
            {
                (float)matrix[0, 0], (float)matrix[0, 1],
                (float)matrix[1, 0], (float)matrix[1, 1]
            };

            GL.UniformMatrix2fv(location, 1, false, data);
        }
        /// <summary>
        /// Specify the value of a float matrix2x3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, ref Matrix2x3 matrix)
        {
            Bind();

            float* data = stackalloc float[2 * 3]
            {
                (float)matrix[0, 0], (float)matrix[0, 1],
                (float)matrix[1, 0], (float)matrix[1, 1],
                (float)matrix[2, 0], (float)matrix[2, 1]
            };

            GL.UniformMatrix2x3fv(location, 1, false, data);
        }
        /// <summary>
        /// Specify the value of a float matrix2x4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, ref Matrix2x4 matrix)
        {
            Bind();

            float* data = stackalloc float[2 * 4]
            {
                (float)matrix[0, 0], (float)matrix[0, 1],
                (float)matrix[1, 0], (float)matrix[1, 1],
                (float)matrix[2, 0], (float)matrix[2, 1],
                (float)matrix[3, 0], (float)matrix[3, 1]
            };

            GL.UniformMatrix2x4fv(location, 1, false, data);
        }

        /// <summary>
        /// Specify the value of a float matrix3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, ref Matrix3 matrix)
        {
            Bind();

            float* data = stackalloc float[3 * 3]
            {
                (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2],
                (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2],
                (float)matrix[2, 0], (float)matrix[2, 1], (float)matrix[2, 2]
            };

            GL.UniformMatrix3fv(location, 1, false, data);
        }
        /// <summary>
        /// Specify the value of a float matrix3x2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, ref Matrix3x2 matrix)
        {
            Bind();

            float* data = stackalloc float[3 * 2]
            {
                (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2],
                (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2]
            };

            GL.UniformMatrix3x2fv(location, 1, false, data);
        }
        /// <summary>
        /// Specify the value of a float matrix3x4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, ref Matrix3x4 matrix)
        {
            Bind();

            float* data = stackalloc float[3 * 4]
            {
                (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2],
                (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2],
                (float)matrix[2, 0], (float)matrix[2, 1], (float)matrix[2, 2],
                (float)matrix[3, 0], (float)matrix[3, 1], (float)matrix[3, 2]
            };

            GL.UniformMatrix3x4fv(location, 1, false, data);
        }

        /// <summary>
        /// Specify the value of a float matrix4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, ref Matrix4 matrix)
        {
            Bind();

            float* data = stackalloc float[4 * 4]
            {
                (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2], (float)matrix[0, 3],
                (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2], (float)matrix[1, 3],
                (float)matrix[2, 0], (float)matrix[2, 1], (float)matrix[2, 2], (float)matrix[2, 3],
                (float)matrix[3, 0], (float)matrix[3, 1], (float)matrix[3, 2], (float)matrix[3, 3]
            };

            GL.UniformMatrix4fv(location, 1, false, data);
        }
        /// <summary>
        /// Specify the value of a float matrix4x3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, ref Matrix4x3 matrix)
        {
            Bind();

            float* data = stackalloc float[4 * 3]
            {
                (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2], (float)matrix[0, 3],
                (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2], (float)matrix[1, 3],
                (float)matrix[2, 0], (float)matrix[2, 1], (float)matrix[2, 2], (float)matrix[2, 3]
            };

            GL.UniformMatrix4x3fv(location, 1, false, data);
        }
        /// <summary>
        /// Specify the value of a float matrix4x2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, ref Matrix4x2 matrix)
        {
            Bind();

            float* data = stackalloc float[4 * 2]
            {
                (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2], (float)matrix[0, 3],
                (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2], (float)matrix[1, 3]
            };

            GL.UniformMatrix4x2fv(location, 1, false, data);
        }

        /// <summary>
        /// Specify the value of a float matrix2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix2[] matrices)
        {
            Bind();

            float* data = stackalloc float[2 * 2 * matrices.Length];
            
            for (int i = 0; i < matrices.Length; i++)
            {
                data[i * 4] = (float)matrices[i][0, 0];
                data[(i * 4) + 1] = (float)matrices[i][0, 1];

                data[(i * 4) + 2] = (float)matrices[i][1, 0];
                data[(i * 4) + 3] = (float)matrices[i][1, 1];
            }

            GL.UniformMatrix2fv(location, matrices.Length, false, data);
        }
        /// <summary>
        /// Specify the value of a float matrix2x3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix2x3[] matrices)
        {
            Bind();

            float* data = stackalloc float[2 * 3 * matrices.Length];

            for (int i = 0; i < matrices.Length; i++)
            {
                data[i * 6] = (float)matrices[i][0, 0];
                data[(i * 6) + 1] = (float)matrices[i][0, 1];

                data[(i * 6) + 2] = (float)matrices[i][1, 0];
                data[(i * 6) + 3] = (float)matrices[i][1, 1];

                data[(i * 6) + 4] = (float)matrices[i][2, 0];
                data[(i * 6) + 5] = (float)matrices[i][2, 1];
            }

            GL.UniformMatrix2x3fv(location, matrices.Length, false, data);
        }
        /// <summary>
        /// Specify the value of a float matrix2x4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix2x4[] matrices)
        {
            Bind();

            float* data = stackalloc float[2 * 4 * matrices.Length];

            for (int i = 0; i < matrices.Length; i++)
            {
                data[i * 8] = (float)matrices[i][0, 0];
                data[(i * 8) + 1] = (float)matrices[i][0, 1];

                data[(i * 8) + 2] = (float)matrices[i][1, 0];
                data[(i * 8) + 3] = (float)matrices[i][1, 1];

                data[(i * 8) + 4] = (float)matrices[i][2, 0];
                data[(i * 8) + 5] = (float)matrices[i][2, 1];

                data[(i * 8) + 6] = (float)matrices[i][3, 0];
                data[(i * 8) + 7] = (float)matrices[i][3, 1];
            }

            GL.UniformMatrix2x4fv(location, matrices.Length, false, data);
        }

        /// <summary>
        /// Specify the value of a float matrix3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix3[] matrices)
        {
            Bind();

            float* data = stackalloc float[3 * 3 * matrices.Length];

            for (int i = 0; i < matrices.Length; i++)
            {
                data[i * 9] = (float)matrices[i][0, 0];
                data[(i * 9) + 1] = (float)matrices[i][0, 1];
                data[(i * 9) + 2] = (float)matrices[i][0, 2];

                data[(i * 9) + 3] = (float)matrices[i][1, 0];
                data[(i * 9) + 4] = (float)matrices[i][1, 1];
                data[(i * 9) + 5] = (float)matrices[i][1, 2];

                data[(i * 9) + 6] = (float)matrices[i][2, 0];
                data[(i * 9) + 7] = (float)matrices[i][2, 1];
                data[(i * 9) + 8] = (float)matrices[i][2, 2];
            }

            GL.UniformMatrix3fv(location, matrices.Length, false, data);
        }
        /// <summary>
        /// Specify the value of a float matrix3x2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix3x2[] matrices)
        {
            Bind();

            float* data = stackalloc float[3 * 2 * matrices.Length];

            for (int i = 0; i < matrices.Length; i++)
            {
                data[i * 6] = (float)matrices[i][0, 0];
                data[(i * 6) + 1] = (float)matrices[i][0, 1];
                data[(i * 6) + 2] = (float)matrices[i][0, 2];

                data[(i * 6) + 3] = (float)matrices[i][1, 0];
                data[(i * 6) + 4] = (float)matrices[i][1, 1];
                data[(i * 6) + 5] = (float)matrices[i][1, 2];
            }

            GL.UniformMatrix3x2fv(location, matrices.Length, false, data);
        }
        /// <summary>
        /// Specify the value of a float matrix3x4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix3x4[] matrices)
        {
            Bind();

            float* data = stackalloc float[3 * 4 * matrices.Length];

            for (int i = 0; i < matrices.Length; i++)
            {
                data[i * 12] = (float)matrices[i][0, 0];
                data[(i * 12) + 1] = (float)matrices[i][0, 1];
                data[(i * 12) + 2] = (float)matrices[i][0, 2];

                data[(i * 12) + 3] = (float)matrices[i][1, 0];
                data[(i * 12) + 4] = (float)matrices[i][1, 1];
                data[(i * 12) + 5] = (float)matrices[i][1, 2];

                data[(i * 12) + 6] = (float)matrices[i][2, 0];
                data[(i * 12) + 7] = (float)matrices[i][2, 1];
                data[(i * 12) + 8] = (float)matrices[i][2, 2];

                data[(i * 12) + 9] = (float)matrices[i][3, 0];
                data[(i * 12) + 10] = (float)matrices[i][3, 1];
                data[(i * 12) + 11] = (float)matrices[i][3, 2];
            }

            GL.UniformMatrix3x4fv(location, matrices.Length, false, data);
        }

        /// <summary>
        /// Specify the value of a float matrix4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix4[] matrices)
        {
            Bind();

            float* data = stackalloc float[4 * 4 * matrices.Length];

            for (int i = 0; i < matrices.Length; i++)
            {
                data[i * 16] = (float)matrices[i][0, 0];
                data[(i * 16) + 1] = (float)matrices[i][0, 1];
                data[(i * 16) + 2] = (float)matrices[i][0, 2];
                data[(i * 16) + 3] = (float)matrices[i][0, 3];

                data[(i * 16) + 4] = (float)matrices[i][1, 0];
                data[(i * 16) + 5] = (float)matrices[i][1, 1];
                data[(i * 16) + 6] = (float)matrices[i][1, 2];
                data[(i * 16) + 7] = (float)matrices[i][1, 3];

                data[(i * 16) + 8] = (float)matrices[i][2, 0];
                data[(i * 16) + 9] = (float)matrices[i][2, 1];
                data[(i * 16) + 10] = (float)matrices[i][2, 2];
                data[(i * 16) + 11] = (float)matrices[i][2, 3];

                data[(i * 16) + 12] = (float)matrices[i][3, 0];
                data[(i * 16) + 13] = (float)matrices[i][3, 1];
                data[(i * 16) + 14] = (float)matrices[i][3, 2];
                data[(i * 16) + 15] = (float)matrices[i][3, 3];
            }

            GL.UniformMatrix4fv(location, matrices.Length, false, data);
        }
        /// <summary>
        /// Specify the value of a float matrix4x3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix4x3[] matrices)
        {
            Bind();

            float* data = stackalloc float[4 * 3 * matrices.Length];

            for (int i = 0; i < matrices.Length; i++)
            {
                data[i * 16] = (float)matrices[i][0, 0];
                data[(i * 16) + 1] = (float)matrices[i][0, 1];
                data[(i * 16) + 2] = (float)matrices[i][0, 2];
                data[(i * 16) + 3] = (float)matrices[i][0, 3];

                data[(i * 16) + 4] = (float)matrices[i][1, 0];
                data[(i * 16) + 5] = (float)matrices[i][1, 1];
                data[(i * 16) + 6] = (float)matrices[i][1, 2];
                data[(i * 16) + 7] = (float)matrices[i][1, 3];

                data[(i * 16) + 8] = (float)matrices[i][2, 0];
                data[(i * 16) + 9] = (float)matrices[i][2, 1];
                data[(i * 16) + 10] = (float)matrices[i][2, 2];
                data[(i * 16) + 11] = (float)matrices[i][2, 3];
            }

            GL.UniformMatrix4x3fv(location, matrices.Length, false, data);
        }
        /// <summary>
        /// Specify the value of a float matrix4x2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix4x2[] matrices)
        {
            Bind();

            float* data = stackalloc float[4 * 2 * matrices.Length];

            for (int i = 0; i < matrices.Length; i++)
            {
                data[i * 16] = (float)matrices[i][0, 0];
                data[(i * 16) + 1] = (float)matrices[i][0, 1];
                data[(i * 16) + 2] = (float)matrices[i][0, 2];
                data[(i * 16) + 3] = (float)matrices[i][0, 3];

                data[(i * 16) + 4] = (float)matrices[i][1, 0];
                data[(i * 16) + 5] = (float)matrices[i][1, 1];
                data[(i * 16) + 6] = (float)matrices[i][1, 2];
                data[(i * 16) + 7] = (float)matrices[i][1, 3];
            }

            GL.UniformMatrix4x2fv(location, matrices.Length, false, data);
        }

        /// <summary>
        /// Specify the value of a double matrix2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformD(int location, ref Matrix2 matrix)
        {
            Bind();

            fixed (double* ptr = &matrix.Data[0])
            {
                GL.UniformMatrix2dv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a double matrix2x3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformD(int location, ref Matrix2x3 matrix)
        {
            Bind();

            fixed (double* ptr = &matrix.Data[0])
            {
                GL.UniformMatrix2x3dv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a double matrix2x4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformD(int location, ref Matrix2x4 matrix)
        {
            Bind();

            fixed (double* ptr = &matrix.Data[0])
            {
                GL.UniformMatrix2x4dv(location, 1, false, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a double matrix3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformD(int location, ref Matrix3 matrix)
        {
            Bind();

            fixed (double* ptr = &matrix.Data[0])
            {
                GL.UniformMatrix3dv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a double matrix3x2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformD(int location, ref Matrix3x2 matrix)
        {
            Bind();

            fixed (double* ptr = &matrix.Data[0])
            {
                GL.UniformMatrix3x2dv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a double matrix3x4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformD(int location, ref Matrix3x4 matrix)
        {
            Bind();

            fixed (double* ptr = &matrix.Data[0])
            {
                GL.UniformMatrix3x4dv(location, 1, false, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a double matrix4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformD(int location, ref Matrix4 matrix)
        {
            Bind();

            fixed (double* ptr = &matrix.Data[0])
            {
                GL.UniformMatrix4dv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a double matrix4x3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformD(int location, ref Matrix4x3 matrix)
        {
            Bind();

            fixed (double* ptr = &matrix.Data[0])
            {
                GL.UniformMatrix4x3dv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a double matrix4x2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformD(int location, ref Matrix4x2 matrix)
        {
            Bind();

            fixed (double* ptr = &matrix.Data[0])
            {
                GL.UniformMatrix4x2dv(location, 1, false, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a double matrix2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformD(int location, Matrix2[] matrices)
        {
            Bind();

            fixed (double* ptr = &matrices[0].Data[0])
            {
                GL.UniformMatrix2dv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a double matrix2x3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformD(int location, Matrix2x3[] matrices)
        {
            Bind();

            fixed (double* ptr = &matrices[0].Data[0])
            {
                GL.UniformMatrix2x3dv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a double matrix2x4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformD(int location, Matrix2x4[] matrices)
        {
            Bind();

            fixed (double* ptr = &matrices[0].Data[0])
            {
                GL.UniformMatrix2x4dv(location, matrices.Length, false, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a double matrix3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformD(int location, Matrix3[] matrices)
        {
            Bind();

            fixed (double* ptr = &matrices[0].Data[0])
            {
                GL.UniformMatrix3dv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a double matrix3x2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformD(int location, Matrix3x2[] matrices)
        {
            Bind();

            fixed (double* ptr = &matrices[0].Data[0])
            {
                GL.UniformMatrix3x2dv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a double matrix3x4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformD(int location, Matrix3x4[] matrices)
        {
            Bind();

            fixed (double* ptr = &matrices[0].Data[0])
            {
                GL.UniformMatrix3x4dv(location, matrices.Length, false, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a double matrix4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformD(int location, Matrix4[] matrices)
        {
            Bind();

            fixed(double * ptr = &matrices[0].Data[0])
            {
                GL.UniformMatrix4dv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a double matrix4x3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformD(int location, Matrix4x3[] matrices)
        {
            Bind();

            fixed (double* ptr = &matrices[0].Data[0])
            {
                GL.UniformMatrix4x3dv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a double matrix4x2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformD(int location, Matrix4x2[] matrices)
        {
            Bind();

            fixed (double* ptr = &matrices[0].Data[0])
            {
                GL.UniformMatrix4x2dv(location, matrices.Length, false, ptr);
            }
        }

        // Float input

        /// <summary>
        /// Specify the value of a float matrix2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, Matrix2<float> matrix)
        {
            Bind();

            fixed (float* ptr = matrix.Data)
            {
                GL.UniformMatrix2fv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix2x3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, Matrix2x3<float> matrix)
        {
            Bind();

            fixed (float* ptr = matrix.Data)
            {
                GL.UniformMatrix2x3fv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix2x4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, Matrix2x4<float> matrix)
        {
            Bind();

            fixed (float* ptr = matrix.Data)
            {
                GL.UniformMatrix2x4fv(location, 1, false, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a float matrix3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, Matrix3<float> matrix)
        {
            Bind();

            fixed (float* ptr = matrix.Data)
            {
                GL.UniformMatrix3fv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix3x2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, Matrix3x2<float> matrix)
        {
            Bind();

            fixed (float* ptr = matrix.Data)
            {
                GL.UniformMatrix3x2fv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix3x4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, Matrix3x4<float> matrix)
        {
            Bind();

            fixed (float* ptr = matrix.Data)
            {
                GL.UniformMatrix3x4fv(location, 1, false, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a float matrix4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, Matrix4<float> matrix)
        {
            Bind();

            fixed (float* ptr = matrix.Data)
            {
                GL.UniformMatrix4fv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix4x3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, Matrix4x3<float> matrix)
        {
            Bind();

            fixed (float* ptr = matrix.Data)
            {
                GL.UniformMatrix4x3fv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix4x2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniformF(int location, Matrix4x2<float> matrix)
        {
            Bind();

            fixed (float* ptr = matrix.Data)
            {
                GL.UniformMatrix4x2fv(location, 1, false, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a float matrix2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix2<float>[] matrices)
        {
            Bind();

            fixed (float* ptr = matrices[0].Data)
            {
                GL.UniformMatrix2fv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix2x3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix2x3<float>[] matrices)
        {
            Bind();

            fixed (float* ptr = matrices[0].Data)
            {
                GL.UniformMatrix2x3fv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix2x4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix2x4<float>[] matrices)
        {
            Bind();

            fixed (float* ptr = matrices[0].Data)
            {
                GL.UniformMatrix2x4fv(location, matrices.Length, false, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a float matrix3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix3<float>[] matrices)
        {
            Bind();

            fixed (float* ptr = matrices[0].Data)
            {
                GL.UniformMatrix3fv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix3x2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix3x2<float>[] matrices)
        {
            Bind();

            fixed (float* ptr = matrices[0].Data)
            {
                GL.UniformMatrix3x2fv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix3x4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix3x4<float>[] matrices)
        {
            Bind();

            fixed (float* ptr = matrices[0].Data)
            {
                GL.UniformMatrix3x4fv(location, matrices.Length, false, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a float matrix4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix4<float>[] matrices)
        {
            Bind();

            fixed (float* ptr = matrices[0].Data)
            {
                GL.UniformMatrix4fv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix4x3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix4x3<float>[] matrices)
        {
            Bind();

            fixed (float* ptr = matrices[0].Data)
            {
                GL.UniformMatrix4x3fv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix4x2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniformF(int location, Matrix4x2<float>[] matrices)
        {
            Bind();

            fixed (float* ptr = matrices[0].Data)
            {
                GL.UniformMatrix4x2fv(location, matrices.Length, false, ptr);
            }
        }
    }
}
