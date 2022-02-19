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
        /// Sets the OpenGL context to referance this object.
        /// </summary>
        public void SetGLContext()
        {
            if (!this.Bound())
            {
                Bind();
            }
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

            byte[] data = new byte[length];
            uint format = 0;

            fixed (byte* ptr = &data[0])
            {
                GL.GetProgramBinary(program.Id, length, null, &format, ptr);

                GL.ProgramBinary(Id, format, ptr, length);
            }
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
            SetGLContext();

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
        public void SetUniform(int location, double value)
        {
            SetGLContext();

            GL.Uniform1f(location, (float)value);
        }
        /// <summary>
        /// Specify the value of a float uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniform(int location, float value)
        {
            SetGLContext();

            GL.Uniform1f(location, value);
        }
        /// <summary>
        /// Specify the value of an integer uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniform(int location, int value)
        {
            SetGLContext();

            GL.Uniform1i(location, value);
        }
        /// <summary>
        /// Specify the value of an unsigned integer uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniform(int location, uint value)
        {
            SetGLContext();

            GL.Uniform1ui(location, value);
        }

        /// <summary>
        /// Specify the values of a float array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniform(int location, double[] values)
        {
            SetGLContext();

            float[] data = new float[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                data[i] = (float)values[i];
            }

            fixed (float* ptr = &data[0])
            {
                GL.Uniform1fv(location, values.Length, ptr);
            }
        }
        /// <summary>
        /// Specify the values of a float array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniform(int location, float[] values)
        {
            SetGLContext();

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
        public void SetUniform(int location, int[] values)
        {
            SetGLContext();

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
        public void SetUniform(int location, uint[] values)
        {
            SetGLContext();

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
        public void SetUniform(int location, Vector2 value)
        {
            SetGLContext();

            GL.Uniform2f(location, (float)value.X, (float)value.Y);
        }
        /// <summary>
        /// Specify the value of a float vector2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniform(int location, Vector2<float> value)
        {
            SetGLContext();

            GL.Uniform2f(location, value.X, value.Y);
        }
        /// <summary>
        /// Specify the value of an integer vector2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniform(int location, Vector2I value)
        {
            SetGLContext();

            GL.Uniform2i(location, value.X, value.Y);
        }
        /// <summary>
        /// Specify the value of an unsigned integer vector2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniform(int location, Vector2<uint> value)
        {
            SetGLContext();

            GL.Uniform2ui(location, value.X, value.Y);
        }

        /// <summary>
        /// Specify the values of a float vector2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniform(int location, Vector2[] values)
        {
            SetGLContext();

            float[] data = new float[values.Length * 2];

            for (int i = 0; i < data.Length; i += 2)
            {
                data[i] = (float)values[i].X;
                data[i + 1] = (float)values[i].Y;
            }

            fixed (float* ptr = &data[0])
            {
                GL.Uniform1fv(location, values.Length, ptr);
            }
        }
        /// <summary>
        /// Specify the values of a float vector2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniform(int location, Vector2<float>[] values)
        {
            SetGLContext();

            fixed (Vector2<float>* ptr = &values[0])
            {
                GL.Uniform1fv(location, values.Length, (float*)ptr);
            }
        }
        /// <summary>
        /// Specify the values of an integer vector2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniform(int location, Vector2I[] values)
        {
            SetGLContext();

            fixed (Vector2I* ptr = &values[0])
            {
                GL.Uniform1iv(location, values.Length, (int*)ptr);
            }
        }
        /// <summary>
        /// Specify the values of an unsigned integer vector2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniform(int location, Vector2<uint>[] values)
        {
            SetGLContext();

            fixed (Vector2<uint>* ptr = &values[0])
            {
                GL.Uniform1uiv(location, values.Length, (uint*)ptr);
            }
        }

        /// <summary>
        /// Specify the value of a float vector3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniform(int location, Vector3 value)
        {
            SetGLContext();

            GL.Uniform3f(location, (float)value.X, (float)value.Y, (float)value.Z);
        }
        /// <summary>
        /// Specify the value of a float vector3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniform(int location, Vector3<float> value)
        {
            SetGLContext();

            GL.Uniform3f(location, value.X, value.Y, value.Z);
        }
        /// <summary>
        /// Specify the value of an integer vector3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniform(int location, Vector3I value)
        {
            SetGLContext();

            GL.Uniform3i(location, value.X, value.Y, value.Z);
        }
        /// <summary>
        /// Specify the value of an unsigned integer vector3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniform(int location, Vector3<uint> value)
        {
            SetGLContext();

            GL.Uniform3ui(location, value.X, value.Y, value.Z);
        }

        /// <summary>
        /// Specify the values of a float vector3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniform(int location, Vector3[] values)
        {
            SetGLContext();

            float[] data = new float[values.Length * 3];

            for (int i = 0; i < data.Length; i += 3)
            {
                data[i] = (float)values[i].X;
                data[i + 1] = (float)values[i].Y;
                data[i + 2] = (float)values[i].Z;
            }

            fixed (float* ptr = &data[0])
            {
                GL.Uniform1fv(location, values.Length, ptr);
            }
        }
        /// <summary>
        /// Specify the values of a float vector3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniform(int location, Vector3<float>[] values)
        {
            SetGLContext();

            fixed (Vector3<float>* ptr = &values[0])
            {
                GL.Uniform1fv(location, values.Length, (float*)ptr);
            }
        }
        /// <summary>
        /// Specify the values of an integer vector3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniform(int location, Vector3I[] values)
        {
            SetGLContext();

            fixed (Vector3I* ptr = &values[0])
            {
                GL.Uniform1iv(location, values.Length, (int*)ptr);
            }
        }
        /// <summary>
        /// Specify the values of an unsigned integer vecotr3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniform(int location, Vector3<uint>[] values)
        {
            SetGLContext();

            fixed (Vector3<uint>* ptr = &values[0])
            {
                GL.Uniform1uiv(location, values.Length, (uint*)ptr);
            }
        }

        /// <summary>
        /// Specify the value of a float vector4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniform(int location, Vector4 value)
        {
            SetGLContext();

            GL.Uniform4f(location, (float)value.X, (float)value.Y, (float)value.Z, (float)value.W);
        }
        /// <summary>
        /// Specify the value of a float vector4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniform(int location, Vector4<float> value)
        {
            SetGLContext();

            GL.Uniform4f(location, value.X, value.Y, value.Z, value.W);
        }
        /// <summary>
        /// Specify the value of an integer vector4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniform(int location, Vector4I value)
        {
            SetGLContext();

            GL.Uniform4i(location, value.X, value.Y, value.Z, value.W);
        }
        /// <summary>
        /// Specify the value of an unsigned integer vector4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        public void SetUniform(int location, Vector4<uint> value)
        {
            SetGLContext();

            GL.Uniform4ui(location, value.X, value.Y, value.Z, value.W);
        }

        /// <summary>
        /// Specify the values of a float vector4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniform(int location, Vector4[] values)
        {
            SetGLContext();

            float[] data = new float[values.Length * 4];

            for (int i = 0; i < data.Length; i += 4)
            {
                data[i] = (float)values[i].X;
                data[i + 1] = (float)values[i].Y;
                data[i + 2] = (float)values[i].Z;
                data[i + 3] = (float)values[i].W;
            }

            fixed (float* ptr = &data[0])
            {
                GL.Uniform1fv(location, values.Length, ptr);
            }
        }
        /// <summary>
        /// Specify the values of a float vector4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniform(int location, Vector4<float>[] values)
        {
            SetGLContext();

            fixed (Vector4<float>* ptr = &values[0])
            {
                GL.Uniform1fv(location, values.Length, (float*)ptr);
            }
        }
        /// <summary>
        /// Specify the values of an integer vector4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniform(int location, Vector4I[] values)
        {
            SetGLContext();

            fixed (Vector4I* ptr = &values[0])
            {
                GL.Uniform1iv(location, values.Length, (int*)ptr);
            }
        }
        /// <summary>
        /// Specify the value of an unsigned integer vector4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="values">The values to set the uniform to.</param>
        public void SetUniform(int location, Vector4<uint>[] values)
        {
            SetGLContext();

            fixed (Vector4<uint>* ptr = &values[0])
            {
                GL.Uniform1uiv(location, values.Length, (uint*)ptr);
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
        public void SetUniform(int location, Matrix2 matrix)
        {
            SetGLContext();

            float[] data = new float[2 * 2]
            {
                (float)matrix[0, 0], (float)matrix[0, 1],
                (float)matrix[1, 0], (float)matrix[1, 1]
            };

            fixed (float* ptr = data)
            {
                GL.UniformMatrix2fv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix2x3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniform(int location, Matrix2x3 matrix)
        {
            SetGLContext();

            float[] data = new float[2 * 3]
            {
                (float)matrix[0, 0], (float)matrix[0, 1],
                (float)matrix[1, 0], (float)matrix[1, 1],
                (float)matrix[2, 0], (float)matrix[2, 1]
            };

            fixed (float* ptr = data)
            {
                GL.UniformMatrix2x3fv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix2x4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniform(int location, Matrix2x4 matrix)
        {
            SetGLContext();

            float[] data = new float[2 * 4]
            {
                (float)matrix[0, 0], (float)matrix[0, 1],
                (float)matrix[1, 0], (float)matrix[1, 1],
                (float)matrix[2, 0], (float)matrix[2, 1],
                (float)matrix[3, 0], (float)matrix[3, 1]
            };

            fixed (float* ptr = data)
            {
                GL.UniformMatrix2x4fv(location, 1, false, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a float matrix3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniform(int location, Matrix3 matrix)
        {
            SetGLContext();

            float[] data = new float[3 * 3]
            {
                (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2],
                (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2],
                (float)matrix[2, 0], (float)matrix[2, 1], (float)matrix[2, 2]
            };

            fixed (float* ptr = data)
            {
                GL.UniformMatrix3fv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix3x2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniform(int location, Matrix3x2 matrix)
        {
            SetGLContext();

            float[] data = new float[3 * 2]
            {
                (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2],
                (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2]
            };

            fixed (float* ptr = data)
            {
                GL.UniformMatrix3x2fv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix3x4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniform(int location, Matrix3x4 matrix)
        {
            SetGLContext();

            float[] data = new float[3 * 4]
            {
                (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2],
                (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2],
                (float)matrix[2, 0], (float)matrix[2, 1], (float)matrix[2, 2],
                (float)matrix[3, 0], (float)matrix[3, 1], (float)matrix[3, 2]
            };

            fixed (float* ptr = data)
            {
                GL.UniformMatrix3x4fv(location, 1, false, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a float matrix4 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniform(int location, Matrix4 matrix)
        {
            SetGLContext();

            float[] data = new float[4 * 4]
            {
                (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2], (float)matrix[0, 3],
                (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2], (float)matrix[1, 3],
                (float)matrix[2, 0], (float)matrix[2, 1], (float)matrix[2, 2], (float)matrix[2, 3],
                (float)matrix[3, 0], (float)matrix[3, 1], (float)matrix[3, 2], (float)matrix[3, 3]
            };

            fixed (float* ptr = data)
            {
                GL.UniformMatrix4fv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix4x3 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniform(int location, Matrix4x3 matrix)
        {
            SetGLContext();

            float[] data = new float[4 * 3]
            {
                (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2], (float)matrix[0, 3],
                (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2], (float)matrix[1, 3],
                (float)matrix[2, 0], (float)matrix[2, 1], (float)matrix[2, 2], (float)matrix[2, 3]
            };

            fixed (float* ptr = data)
            {
                GL.UniformMatrix4x3fv(location, 1, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix4x2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniform(int location, Matrix4x2 matrix)
        {
            SetGLContext();

            float[] data = new float[4 * 2]
            {
                (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2], (float)matrix[0, 3],
                (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2], (float)matrix[1, 3]
            };

            fixed (float* ptr = data)
            {
                GL.UniformMatrix4x2fv(location, 1, false, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a float matrix2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniform(int location, Matrix2[] matrices)
        {
            SetGLContext();

            float[] data = new float[2 * 2 * matrices.Length];
            
            for (int i = 0; i < matrices.Length; i++)
            {
                data[i * 4] = (float)matrices[i][0, 0];
                data[(i * 4) + 1] = (float)matrices[i][0, 1];

                data[(i * 4) + 2] = (float)matrices[i][1, 0];
                data[(i * 4) + 3] = (float)matrices[i][1, 1];
            }

            fixed (float* ptr = data)
            {
                GL.UniformMatrix2fv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix2x3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniform(int location, Matrix2x3[] matrices)
        {
            SetGLContext();

            float[] data = new float[2 * 3 * matrices.Length];

            for (int i = 0; i < matrices.Length; i++)
            {
                data[i * 6] = (float)matrices[i][0, 0];
                data[(i * 6) + 1] = (float)matrices[i][0, 1];

                data[(i * 6) + 2] = (float)matrices[i][1, 0];
                data[(i * 6) + 3] = (float)matrices[i][1, 1];

                data[(i * 6) + 4] = (float)matrices[i][2, 0];
                data[(i * 6) + 5] = (float)matrices[i][2, 1];
            }

            fixed (float* ptr = data)
            {
                GL.UniformMatrix2x3fv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix2x4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniform(int location, Matrix2x4[] matrices)
        {
            SetGLContext();

            float[] data = new float[2 * 4 * matrices.Length];

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

            fixed (float* ptr = data)
            {
                GL.UniformMatrix2x4fv(location, matrices.Length, false, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a float matrix3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniform(int location, Matrix3[] matrices)
        {
            SetGLContext();

            float[] data = new float[3 * 3 * matrices.Length];

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

            fixed (float* ptr = data)
            {
                GL.UniformMatrix3fv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix3x2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniform(int location, Matrix3x2[] matrices)
        {
            SetGLContext();

            float[] data = new float[3 * 2 * matrices.Length];

            for (int i = 0; i < matrices.Length; i++)
            {
                data[i * 6] = (float)matrices[i][0, 0];
                data[(i * 6) + 1] = (float)matrices[i][0, 1];
                data[(i * 6) + 2] = (float)matrices[i][0, 2];

                data[(i * 6) + 3] = (float)matrices[i][1, 0];
                data[(i * 6) + 4] = (float)matrices[i][1, 1];
                data[(i * 6) + 5] = (float)matrices[i][1, 2];
            }

            fixed (float* ptr = data)
            {
                GL.UniformMatrix3x2fv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix3x4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniform(int location, Matrix3x4[] matrices)
        {
            SetGLContext();

            float[] data = new float[3 * 4 * matrices.Length];

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

            fixed (float* ptr = data)
            {
                GL.UniformMatrix3x4fv(location, matrices.Length, false, ptr);
            }
        }

        /// <summary>
        /// Specify the value of a float matrix4 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniform(int location, Matrix4[] matrices)
        {
            SetGLContext();

            float[] data = new float[4 * 4 * matrices.Length];

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

            fixed (float* ptr = data)
            {
                GL.UniformMatrix4fv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix4x3 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniform(int location, Matrix4x3[] matrices)
        {
            SetGLContext();

            float[] data = new float[4 * 3 * matrices.Length];

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

            fixed (float* ptr = data)
            {
                GL.UniformMatrix4x3fv(location, matrices.Length, false, ptr);
            }
        }
        /// <summary>
        /// Specify the value of a float matrix4x2 array uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrices">The array of matrices to set the uniform to.</param>
        public void SetUniform(int location, Matrix4x2[] matrices)
        {
            SetGLContext();

            float[] data = new float[4 * 2 * matrices.Length];

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

            fixed (float* ptr = data)
            {
                GL.UniformMatrix4x2fv(location, matrices.Length, false, ptr);
            }
        }

        // Float input

        /// <summary>
        /// Specify the value of a float matrix2 uniform variable.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="matrix">The matrix to set the uniform to.</param>
        public void SetUniform(int location, Matrix2<float> matrix)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix2x3<float> matrix)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix2x4<float> matrix)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix3<float> matrix)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix3x2<float> matrix)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix3x4<float> matrix)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix4<float> matrix)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix4x3<float> matrix)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix4x2<float> matrix)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix2<float>[] matrices)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix2x3<float>[] matrices)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix2x4<float>[] matrices)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix3<float>[] matrices)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix3x2<float>[] matrices)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix3x4<float>[] matrices)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix4<float>[] matrices)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix4x3<float>[] matrices)
        {
            SetGLContext();

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
        public void SetUniform(int location, Matrix4x2<float>[] matrices)
        {
            SetGLContext();

            fixed (float* ptr = matrices[0].Data)
            {
                GL.UniformMatrix4x2fv(location, matrices.Length, false, ptr);
            }
        }
    }
}
