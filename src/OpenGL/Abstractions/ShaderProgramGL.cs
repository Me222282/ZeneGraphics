using System;
using Zene.Structs;

namespace Zene.Graphics.Base
{
    public unsafe partial class ShaderProgramGL : IShaderProgram
    {
        public ShaderProgramGL()
        {
            Id = GL.CreateProgram();

            Properties = new ShaderProgramProperties(this);
        }

        public uint Id { get; }

        public ShaderProgramProperties Properties { get; }

        public void Bind()
        {
            if (this.Bound()) { return; }

            GL.UseProgram(this);
        }
        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            Dispose(true);

            _disposed = true;
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                GL.DeleteProgram(Id);
            }
        }
        public void Unbind()
        {
            if (!this.Bound()) { return; }

            GL.UseProgram(null);
        }

        /// <summary>
        /// Validates this program.
        /// </summary>
        public void Validate()
        {
            GL.ValidateProgram(Id);
        }

        public virtual void PrepareDraw() { }

        /// <summary>
        /// Attaches a shader object to this program.
        /// </summary>
        /// <param name="shader">Specifies the shader object that is to be attached.</param>
        protected void AttachShader(IShader shader)
        {
            GL.AttachShader(this, shader);
        }
        /// <summary>
        /// Detaches a shader object from this program to which it was attached.
        /// </summary>
        /// <param name="shader">Specifies the shader object to be detached.</param>
        protected void DetachShader(IShader shader)
        {
            GL.DetachShader(this, shader);
        }

        protected void DetachAllShaders()
        {
            IShader[] shaders = Properties._attachedShaders.ToArray();

            for (int i = 0; i < shaders.Length; i++)
            {
                DetachShader(shaders[i]);
            }
        }

        /// <summary>
        /// Associates a generic vertex attribute index with a named attribute variable.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be bound.</param>
        /// <param name="name">Specifies a string containing the name of the vertex shader attribute variable to which <paramref name="index"/> is to be bound.</param>
        protected void BindAttribLocation(uint index, string name)
        {
            GL.BindAttribLocation(Id, index, name);
        }
        /// <summary>
        /// Bind a user-defined varying out variable to a fragment shader colour number.
        /// </summary>
        /// <param name="colourAttach">The colour number to bind the user-defined varying out variable to.</param>
        /// <param name="name">The name of the user-defined varying out variable whose binding to modify.</param>
        protected void BindFragDataLocation(uint colourAttach, string name)
        {
            GL.BindFragDataLocation(Id, colourAttach, name);
        }
        /// <summary>
        /// Bind a user-defined varying out variable to a fragment shader colour number and index.
        /// </summary>
        /// <param name="colourAttach">The colour number to bind the user-defined varying out variable to.</param>
        /// <param name="index">The index of the color input to bind the user-defined varying out variable to.</param>
        /// <param name="name">The name of the user-defined varying out variable whose binding to modify.</param>
        protected void BindFragDataLocationIndexed(uint colourAttach, ColourIndex index, string name)
        {
            GL.BindFragDataLocationIndexed(Id, colourAttach, (uint)index, name);
        }

        /// <summary>
        /// Links this program.
        /// </summary>
        protected void LinkProgram()
        {
            GL.LinkProgram(Id);

            Properties.SyncUniforms();
        }
        /// <summary>
        /// Loads this program with the binary representation of <paramref name="program"/>'s compiled and linked executable source.
        /// </summary>
        /// <param name="program">The program to get binaries from.</param>
        protected void ProgramBinary(IShaderProgram program)
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
        protected void ShaderStorageBlockBinding(uint storageBlockIndex, uint storageBlockBinding)
        {
            GL.ShaderStorageBlockBinding(Id, storageBlockIndex, storageBlockBinding);
        }
        /// <summary>
        /// Assign a binding point to an active uniform block.
        /// </summary>
        /// <param name="uniformBlockIndex">The index of the active uniform block within program whose binding to assign.</param>
        /// <param name="uniformBlockBinding">Specifies the binding point to which to bind the uniform block with index <paramref name="uniformBlockIndex"/> within this program.</param>
        protected void UniformBlockBinding(uint uniformBlockIndex, uint uniformBlockBinding)
        {
            GL.UniformBlockBinding(Id, uniformBlockIndex, uniformBlockBinding);
        }

        /// <summary>
        /// Load active subroutine uniforms.
        /// </summary>
        /// <param name="type">Specifies the shader stage from which to query for subroutine uniform index.</param>
        /// <param name="indices">Specifies an array holding the indices to load into the shader subroutine variables.</param>
        protected void UniformSubroutines(ShaderType type, uint[] indices)
        {
            Bind();

            fixed (uint* ptr = &indices[0])
            {
                GL.UniformSubroutinesuiv((uint)type, indices.Length, ptr);
            }
        }

        /// <summary>
        /// Returns the location of a uniform variable.
        /// </summary>
        /// <param name="name">A string containing the name of the uniform variable whose location is to be queried.</param>
        /// <returns></returns>
        protected int GetUniformLocation(string name)
        {
            return Properties.FindUniform(name).Location;
            //return GL.GetUniformLocation(Id, name);
        }

        /// <summary>
        /// Specify the value of a uniform variable with a double.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, double value)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];

            if (uv.Type == UniformType.Float)
            {
                uv.su(location, 1, 0, (float)value);
                return;
            }
            if (uv.Type == UniformType.Double)
            {
                uv.su(location, 1, 0, value);
                return;
            }
            if (uv.Type == UniformType.Int)
            {
                uv.su(location, 1, 0, (int)value);
                return;
            }
            if (uv.Type == UniformType.Uint)
            {
                uv.su(location, 1, 0, (uint)value);
                return;
            }

            throw new Exception("Invalid data type.");
        }
        /// <summary>
        /// Specify the value of a uniform variable with a float.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, float value)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];

            if (uv.Type == UniformType.Float)
            {
                uv.su(location, 1, 0, value);
                return;
            }
            if (uv.Type == UniformType.Double)
            {
                uv.su(location, 1, 0, (double)value);
                return;
            }
            if (uv.Type == UniformType.Int)
            {
                uv.su(location, 1, 0, (int)value);
                return;
            }
            if (uv.Type == UniformType.Uint)
            {
                uv.su(location, 1, 0, (uint)value);
                return;
            }

            throw new Exception("Invalid data type.");
        }
        /// <summary>
        /// Specify the value of a uniform variable with an integer.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, int value)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];

            if (uv.Type == UniformType.Int || 
                (uv.Type >= UniformType.Sampler1D &&
                uv.Type <= UniformType.Sampler2DRectShadow) ||
                (uv.Type >= UniformType.Sampler1DArray &&
                uv.Type <= UniformType.SamplerCubeShadow) ||
                (uv.Type >= UniformType.IntSampler1D &&
                uv.Type <= UniformType.IntSamplerBuffer) ||
                uv.Type == UniformType.Sampler2DMultisample ||
                uv.Type == UniformType.Sampler2DMultisampleArray ||
                uv.Type == UniformType.IntSampler2DMultisample ||
                uv.Type == UniformType.IntSampler2DMultisampleArray)
            {
                uv.su(location, 1, 0, value);
                return;
            }
            if (uv.Type == UniformType.Float)
            {
                uv.su(location, 1, 0, (float)value);
                return;
            }
            if (uv.Type == UniformType.Double)
            {
                uv.su(location, 1, 0, (double)value);
                return;
            }
            if (uv.Type == UniformType.Uint)
            {
                uv.su(location, 1, 0, (uint)value);
                return;
            }

            throw new Exception("Invalid data type.");
        }
        /// <summary>
        /// Specify the value of a uniform variable with an unsigned integer.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, uint value)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];

            if (uv.Type == UniformType.Uint ||
                (uv.Type >= UniformType.UintSampler1D &&
                uv.Type <= UniformType.UintSamplerBuffer) ||
                uv.Type == UniformType.UintSampler2DMultisample ||
                uv.Type == UniformType.UintSampler2DMultisampleArray)
            {
                uv.su(location, 1, 0, value);
                return;
            }
            if (uv.Type == UniformType.Int)
            {
                uv.su(location, 1, 0, (int)value);
                return;
            }
            if (uv.Type == UniformType.Float)
            {
                uv.su(location, 1, 0, (float)value);
                return;
            }
            if (uv.Type == UniformType.Double)
            {
                uv.su(location, 1, 0, (double)value);
                return;
            }

            throw new Exception("Invalid data type.");
        }
        /// <summary>
        /// Specify the value of a uniform variable with a boolean.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, bool value)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];

            if (uv.Type == UniformType.Bool)
            {
                uv.su(location, 1, 0, value ? 1 : 0);
                return;
            }

            throw new Exception("Invalid data type.");
        }

        /// <summary>
        /// Specify the value of a uniform variable with a double array.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, double[] values)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];

            if (uv.Type == UniformType.Float)
            {
                float* data = stackalloc float[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    data[i] = (float)values[i];
                }

                uv.su(location, values.Length, 0, new IntPtr(data));
                return;
            }
            if (uv.Type == UniformType.Double)
            {
                fixed (double* ptr = &values[0])
                {
                    uv.su(location, values.Length, 0, new IntPtr(ptr));
                }
                return;
            }
            if (uv.Type == UniformType.Int)
            {
                int* data = stackalloc int[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    data[i] = (int)values[i];
                }

                uv.su(location, values.Length, 0, new IntPtr(data));
                return;
            }
            if (uv.Type == UniformType.Uint)
            {
                uint* data = stackalloc uint[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    data[i] = (uint)values[i];
                }

                uv.su(location, values.Length, 0, new IntPtr(data));
                return;
            }

            throw new Exception("Invalid data type.");
        }
        /// <summary>
        /// Specify the value of a uniform variable with a float array.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, float[] values)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];

            if (uv.Type == UniformType.Float)
            {
                fixed (float* ptr = &values[0])
                {
                    uv.su(location, values.Length, 0, new IntPtr(ptr));
                }
                return;
            }
            if (uv.Type == UniformType.Double)
            {
                double* data = stackalloc double[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    data[i] = values[i];
                }

                uv.su(location, values.Length, 0, new IntPtr(data));
                return;
            }
            if (uv.Type == UniformType.Int)
            {
                int* data = stackalloc int[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    data[i] = (int)values[i];
                }

                uv.su(location, values.Length, 0, new IntPtr(data));
                return;
            }
            if (uv.Type == UniformType.Uint)
            {
                uint* data = stackalloc uint[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    data[i] = (uint)values[i];
                }

                uv.su(location, values.Length, 0, new IntPtr(data));
                return;
            }

            throw new Exception("Invalid data type.");
        }
        /// <summary>
        /// Specify the value of a uniform variable with an integer array.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, int[] values)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];

            if (uv.Type == UniformType.Int ||
                (uv.Type >= UniformType.Sampler1D &&
                uv.Type <= UniformType.Sampler2DRectShadow) ||
                (uv.Type >= UniformType.Sampler1DArray &&
                uv.Type <= UniformType.SamplerCubeShadow) ||
                (uv.Type >= UniformType.IntSampler1D &&
                uv.Type <= UniformType.IntSamplerBuffer) ||
                uv.Type == UniformType.Sampler2DMultisample ||
                uv.Type == UniformType.Sampler2DMultisampleArray ||
                uv.Type == UniformType.IntSampler2DMultisample ||
                uv.Type == UniformType.IntSampler2DMultisampleArray)
            {
                fixed (int* ptr = &values[0])
                {
                    uv.su(location, values.Length, 0, new IntPtr(ptr));
                }
                return;
            }
            if (uv.Type == UniformType.Float)
            {
                float* data = stackalloc float[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    data[i] = values[i];
                }

                uv.su(location, values.Length, 0, new IntPtr(data));
                return;
            }
            if (uv.Type == UniformType.Double)
            {
                double* data = stackalloc double[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    data[i] = values[i];
                }

                uv.su(location, values.Length, 0, new IntPtr(data));
                return;
            }
            if (uv.Type == UniformType.Uint)
            {
                uint* data = stackalloc uint[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    data[i] = (uint)values[i];
                }

                uv.su(location, values.Length, 0, new IntPtr(data));
                return;
            }

            throw new Exception("Invalid data type.");
        }
        /// <summary>
        /// Specify the value of a uniform variable with an unsigned integer array.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, uint[] values)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];

            if (uv.Type == UniformType.Uint ||
                (uv.Type >= UniformType.UintSampler1D &&
                uv.Type <= UniformType.UintSamplerBuffer) ||
                uv.Type == UniformType.UintSampler2DMultisample ||
                uv.Type == UniformType.UintSampler2DMultisampleArray)
            {
                fixed (uint* ptr = &values[0])
                {
                    uv.su(location, values.Length, 0, new IntPtr(ptr));
                }
                return;
            }
            if (uv.Type == UniformType.Int)
            {
                int* data = stackalloc int[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    data[i] = (int)values[i];
                }

                uv.su(location, values.Length, 0, new IntPtr(data));
                return;
            }
            if (uv.Type == UniformType.Float)
            {
                float* data = stackalloc float[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    data[i] = values[i];
                }

                uv.su(location, values.Length, 0, new IntPtr(data));
                return;
            }
            if (uv.Type == UniformType.Double)
            {
                double* data = stackalloc double[values.Length];

                for (int i = 0; i < values.Length; i++)
                {
                    data[i] = values[i];
                }

                uv.su(location, values.Length, 0, new IntPtr(data));
                return;
            }

            throw new Exception("Invalid data type.");
        }

        /// <summary>
        /// Specify the value of a uniform variable with a vector2 double.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, Vector2 value)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];

            if (uv.Type == UniformType.FVec2)
            {
                uv.su(location, 1, 0, ((float)value.X, (float)value.Y));
                return;
            }
            if (uv.Type == UniformType.DVec2)
            {
                uv.su(location, 1, 0, (value.X, value.Y));
                return;
            }
            if (uv.Type == UniformType.IVec2)
            {
                uv.su(location, 1, 0, ((int)value.X, (int)value.Y));
                return;
            }
            if (uv.Type == UniformType.UiVec2)
            {
                uv.su(location, 1, 0, ((uint)value.X, (uint)value.Y));
                return;
            }

            throw new Exception("Invalid data type.");
        }
        /// <summary>
        /// Specify the value of a uniform variable with a vector2 integer.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, Vector2I value)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];

            if (uv.Type == UniformType.IVec2)
            {
                uv.su(location, 1, 0, (value.X, value.Y));
                return;
            }
            if (uv.Type == UniformType.FVec2)
            {
                uv.su(location, 1, 0, ((float)value.X, (float)value.Y));
                return;
            }
            if (uv.Type == UniformType.DVec2)
            {
                uv.su(location, 1, 0, new ValueTuple<double, double>(value.X, value.Y));
                return;
            }
            if (uv.Type == UniformType.UiVec2)
            {
                uv.su(location, 1, 0, ((uint)value.X, (uint)value.Y));
                return;
            }

            throw new Exception("Invalid data type.");
        }
        /// <summary>
        /// Specify the value of a uniform variable with a vector3 double.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, Vector3 value)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];

            if (uv.Type == UniformType.FVec3)
            {
                uv.su(location, 1, 0, ((float)value.X, (float)value.Y, (float)value.Z));
                return;
            }
            if (uv.Type == UniformType.DVec3)
            {
                uv.su(location, 1, 0, (value.X, value.Y, value.Z));
                return;
            }
            if (uv.Type == UniformType.IVec3)
            {
                uv.su(location, 1, 0, ((int)value.X, (int)value.Y, (int)value.Z));
                return;
            }
            if (uv.Type == UniformType.UiVec3)
            {
                uv.su(location, 1, 0, ((uint)value.X, (uint)value.Y, (uint)value.Z));
                return;
            }

            throw new Exception("Invalid data type.");
        }
        /// <summary>
        /// Specify the value of a uniform variable with a vector3 integer.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, Vector3I value)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];

            if (uv.Type == UniformType.IVec3)
            {
                uv.su(location, 1, 0, (value.X, value.Y, value.Z));
                return;
            }
            if (uv.Type == UniformType.FVec3)
            {
                uv.su(location, 1, 0, ((float)value.X, (float)value.Y, (float)value.Z));
                return;
            }
            if (uv.Type == UniformType.DVec3)
            {
                uv.su(location, 1, 0, new ValueTuple<double, double, double>(value.X, value.Y, value.Z));
                return;
            }
            if (uv.Type == UniformType.UiVec3)
            {
                uv.su(location, 1, 0, ((uint)value.X, (uint)value.Y, (uint)value.Z));
                return;
            }

            throw new Exception("Invalid data type.");
        }
        /// <summary>
        /// Specify the value of a uniform variable with a vector4 double.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, Vector4 value)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];

            if (uv.Type == UniformType.FVec4)
            {
                uv.su(location, 1, 0, ((float)value.X, (float)value.Y, (float)value.Z, (float)value.W));
                return;
            }
            if (uv.Type == UniformType.DVec4)
            {
                uv.su(location, 1, 0, (value.X, value.Y, value.Z, value.W));
                return;
            }
            if (uv.Type == UniformType.IVec4)
            {
                uv.su(location, 1, 0, ((int)value.X, (int)value.Y, (int)value.Z, (int)value.W));
                return;
            }
            if (uv.Type == UniformType.UiVec4)
            {
                uv.su(location, 1, 0, ((uint)value.X, (uint)value.Y, (uint)value.Z, (uint)value.W));
                return;
            }

            throw new Exception("Invalid data type.");
        }
        /// <summary>
        /// Specify the value of a uniform variable with a vector4 integer.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, Vector4I value)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];

            if (uv.Type == UniformType.IVec4)
            {
                uv.su(location, 1, 0, (value.X, value.Y, value.Z, value.W));
                return;
            }
            if (uv.Type == UniformType.FVec4)
            {
                uv.su(location, 1, 0, ((float)value.X, (float)value.Y, (float)value.Z, (float)value.W));
                return;
            }
            if (uv.Type == UniformType.DVec4)
            {
                uv.su(location, 1, 0, new ValueTuple<double, double, double, double>(value.X, value.Y, value.Z, value.W));
                return;
            }
            if (uv.Type == UniformType.UiVec4)
            {
                uv.su(location, 1, 0, ((uint)value.X, (uint)value.Y, (uint)value.Z, (uint)value.W));
                return;
            }

            throw new Exception("Invalid data type.");
        }

        /// <summary>
        /// Specify the value of a uniform variable with a matrix.
        /// </summary>
        /// <param name="location">Specifies the location of the uniform variable to be modified.</param>
        /// <param name="value">The value to set the uniform to.</param>
        protected void SetUniform(int location, IMatrix value)
        {
            Bind();

            UniformVariable uv = Properties._uniforms[location];
            uv.su(location, 1, 0, value);
        }

        protected void SetUniform<T>(int location, T value) where T : unmanaged, IUniformStruct
        {
            Bind();

            IUniformStruct.Member[] memebers = value.Members();

            int offset = 0;
            byte* ptr = (byte*)&value;

            for (int i = 0; i < memebers.Length; i++)
            {
                switch (memebers[i].Type)
                {
                    case UniformType.Bool:
                        GL.Uniform1i(location + i, ptr[offset]);
                        offset++;
                        break;

                    case UniformType.Int:
                        GL.Uniform1i(location + i, *(int*)(ptr + offset));
                        offset += 4;
                        break;

                    case UniformType.Uint:
                        GL.Uniform1ui(location + i, *(uint*)(ptr + offset));
                        offset += 4;
                        break;

                    case UniformType.Float:
                        GL.Uniform1f(location + i, *(float*)(ptr + offset));
                        offset += 4;
                        break;

                    case UniformType.Double:
                        if (memebers[i].DoubleAsFloat)
                        {
                            GL.Uniform1f(location + i, (float)*(double*)(ptr + offset));
                            offset += 8;
                            break;
                        }

                        GL.Uniform1d(location + i, *(double*)(ptr + offset));
                        offset += 8;
                        break;

                    case UniformType.BVec2:
                        GL.Uniform2i(location + i, ptr[offset], ptr[offset + 1]);
                        offset += 2;
                        break;

                    case UniformType.IVec2:
                        int* currenti2 = (int*)(ptr + offset);

                        GL.Uniform2i(location + i, *currenti2, *(currenti2 + 1));
                        offset += 8;
                        break;

                    case UniformType.UiVec2:
                        uint* currentu2 = (uint*)(ptr + offset);

                        GL.Uniform2ui(location + i, *currentu2, *(currentu2 + 1));
                        offset += 8;
                        break;

                    case UniformType.FVec2:
                        float* currentf2 = (float*)(ptr + offset);

                        GL.Uniform2f(location + i, *currentf2, *(currentf2 + 1));
                        offset += 8;
                        break;

                    case UniformType.DVec2:
                        double* currentd2 = (double*)(ptr + offset);
                        offset += 16;

                        if (memebers[i].DoubleAsFloat)
                        {
                            GL.Uniform2f(location + i, (float)*currentd2, (float)*(currentd2 + 1));
                            break;
                        }

                        GL.Uniform2d(location + i, *currentd2, *(currentd2 + 1));
                        break;

                    case UniformType.BVec3:
                        GL.Uniform3i(location + i, ptr[offset], ptr[offset + 1], ptr[offset + 2]);
                        offset += 3;
                        break;

                    case UniformType.IVec3:
                        int* currenti3 = (int*)(ptr + offset);

                        GL.Uniform3i(location + i, *currenti3, *(currenti3 + 1), *(currenti3 + 2));
                        offset += 12;
                        break;

                    case UniformType.UiVec3:
                        uint* currentu3 = (uint*)(ptr + offset);

                        GL.Uniform3ui(location + i, *currentu3, *(currentu3 + 1), *(currentu3 + 2));
                        offset += 12;
                        break;

                    case UniformType.FVec3:
                        float* currentf3 = (float*)(ptr + offset);

                        GL.Uniform3f(location + i, *currentf3, *(currentf3 + 1), *(currentf3 + 2));
                        offset += 12;
                        break;

                    case UniformType.DVec3:
                        double* currentd3 = (double*)(ptr + offset);
                        offset += 24;

                        if (memebers[i].DoubleAsFloat)
                        {
                            GL.Uniform3f(location + i, (float)*currentd3, (float)*(currentd3 + 1), (float)*(currentd3 + 2));
                            break;
                        }

                        GL.Uniform3d(location + i, *currentd3, *(currentd3 + 1), *(currentd3 + 2));
                        break;

                    case UniformType.BVec4:
                        GL.Uniform4i(location + i, ptr[offset], ptr[offset + 1], ptr[offset + 2], ptr[offset + 3]);
                        offset += 4;
                        break;

                    case UniformType.IVec4:
                        int* currenti4 = (int*)(ptr + offset);

                        GL.Uniform4i(location + i, *currenti4, *(currenti4 + 1), *(currenti4 + 2), *(currenti4 + 3));
                        offset += 16;
                        break;

                    case UniformType.UiVec4:
                        uint* currentu4 = (uint*)(ptr + offset);

                        GL.Uniform4ui(location + i, *currentu4, *(currentu4 + 1), *(currentu4 + 2), *(currentu4 + 3));
                        offset += 16;
                        break;

                    case UniformType.FVec4:
                        float* currentf4 = (float*)(ptr + offset);

                        GL.Uniform4f(location + i, *currentf4, *(currentf4 + 1), *(currentf4 + 2), *(currentf4 + 3));
                        offset += 16;
                        break;

                    case UniformType.DVec4:
                        double* currentd4 = (double*)(ptr + offset);
                        offset += 32;

                        if (memebers[i].DoubleAsFloat)
                        {
                            GL.Uniform4f(location + i, (float)*currentd4, (float)*(currentd4 + 1), (float)*(currentd4 + 2), (float)*(currentd4 + 3));
                            break;
                        }

                        GL.Uniform4d(location + i, *currentd4, *(currentd4 + 1), *(currentd4 + 2), *(currentd4 + 3));
                        break;
                }
            }
        }
        protected void SetUniform<T>(int location, int index, T value) where T : unmanaged, IUniformStruct
        {
            Bind();

            IUniformStruct.Member[] memebers = value.Members();

            location += memebers.Length * index;

            int offset = 0;
            byte* ptr = (byte*)&value;

            for (int i = 0; i < memebers.Length; i++)
            {
                switch (memebers[i].Type)
                {
                    case UniformType.Bool:
                        GL.Uniform1i(location + i, ptr[offset]);
                        offset++;
                        break;

                    case UniformType.Int:
                        GL.Uniform1i(location + i, *(int*)(ptr + offset));
                        offset += 4;
                        break;

                    case UniformType.Uint:
                        GL.Uniform1ui(location + i, *(uint*)(ptr + offset));
                        offset += 4;
                        break;

                    case UniformType.Float:
                        GL.Uniform1f(location + i, *(float*)(ptr + offset));
                        offset += 4;
                        break;

                    case UniformType.Double:
                        offset += 8;

                        if (memebers[i].DoubleAsFloat)
                        {
                            GL.Uniform1f(location + i, (float)*(double*)(ptr + offset));
                            break;
                        }

                        GL.Uniform1d(location + i, *(double*)(ptr + offset));
                        break;

                    case UniformType.BVec2:
                        GL.Uniform2i(location + i, ptr[offset], ptr[offset + 1]);
                        offset += 2;
                        break;

                    case UniformType.IVec2:
                        int* currenti2 = (int*)(ptr + offset);

                        GL.Uniform2i(location + i, *currenti2, *(currenti2 + 1));
                        offset += 8;
                        break;

                    case UniformType.UiVec2:
                        uint* currentu2 = (uint*)(ptr + offset);

                        GL.Uniform2ui(location + i, *currentu2, *(currentu2 + 1));
                        offset += 8;
                        break;

                    case UniformType.FVec2:
                        float* currentf2 = (float*)(ptr + offset);

                        GL.Uniform2f(location + i, *currentf2, *(currentf2 + 1));
                        offset += 8;
                        break;

                    case UniformType.DVec2:
                        double* currentd2 = (double*)(ptr + offset);
                        offset += 16;

                        if (memebers[i].DoubleAsFloat)
                        {
                            GL.Uniform2f(location + i, (float)*currentd2, (float)*(currentd2 + 1));
                            break;
                        }

                        GL.Uniform2d(location + i, *currentd2, *(currentd2 + 1));
                        break;

                    case UniformType.BVec3:
                        GL.Uniform3i(location + i, ptr[offset], ptr[offset + 1], ptr[offset + 2]);
                        offset += 3;
                        break;

                    case UniformType.IVec3:
                        int* currenti3 = (int*)(ptr + offset);

                        GL.Uniform3i(location + i, *currenti3, *(currenti3 + 1), *(currenti3 + 2));
                        offset += 12;
                        break;

                    case UniformType.UiVec3:
                        uint* currentu3 = (uint*)(ptr + offset);

                        GL.Uniform3ui(location + i, *currentu3, *(currentu3 + 1), *(currentu3 + 2));
                        offset += 12;
                        break;

                    case UniformType.FVec3:
                        float* currentf3 = (float*)(ptr + offset);

                        GL.Uniform3f(location + i, *currentf3, *(currentf3 + 1), *(currentf3 + 2));
                        offset += 12;
                        break;

                    case UniformType.DVec3:
                        double* currentd3 = (double*)(ptr + offset);
                        offset += 24;

                        if (memebers[i].DoubleAsFloat)
                        {
                            GL.Uniform3f(location + i, (float)*currentd3, (float)*(currentd3 + 1), (float)*(currentd3 + 2));
                            break;
                        }

                        GL.Uniform3d(location + i, *currentd3, *(currentd3 + 1), *(currentd3 + 2));
                        break;

                    case UniformType.BVec4:
                        GL.Uniform4i(location + i, ptr[offset], ptr[offset + 1], ptr[offset + 2], ptr[offset + 3]);
                        offset += 4;
                        break;

                    case UniformType.IVec4:
                        int* currenti4 = (int*)(ptr + offset);

                        GL.Uniform4i(location + i, *currenti4, *(currenti4 + 1), *(currenti4 + 2), *(currenti4 + 3));
                        offset += 16;
                        break;

                    case UniformType.UiVec4:
                        uint* currentu4 = (uint*)(ptr + offset);

                        GL.Uniform4ui(location + i, *currentu4, *(currentu4 + 1), *(currentu4 + 2), *(currentu4 + 3));
                        offset += 16;
                        break;

                    case UniformType.FVec4:
                        float* currentf4 = (float*)(ptr + offset);

                        GL.Uniform4f(location + i, *currentf4, *(currentf4 + 1), *(currentf4 + 2), *(currentf4 + 3));
                        offset += 16;
                        break;

                    case UniformType.DVec4:
                        double* currentd4 = (double*)(ptr + offset);
                        offset += 32;
                        
                        if (memebers[i].DoubleAsFloat)
                        {
                            GL.Uniform4f(location + i, (float)*currentd4, (float)*(currentd4 + 1), (float)*(currentd4 + 2), (float)*(currentd4 + 3));
                            break;
                        }

                        GL.Uniform4d(location + i, *currentd4, *(currentd4 + 1), *(currentd4 + 2), *(currentd4 + 3));
                        break;
                }
            }
        }


        protected float GetUniformF(int location)
        {
            float value = 0;

            GL.GetUniformfv(Id, location, &value);

            return value;
        }

        protected int GetUniformI(int location)
        {
            int value = 0;

            GL.GetUniformiv(Id, location, &value);

            return value;
        }

        protected Vector2 GetUniformF2(int location)
        {
            Vector2<float> value = new Vector2<float>();

            GL.GetUniformfv(Id, location, (float*)&value);

            return new Vector2(value.X, value.Y);
        }

        protected Vector3 GetUniformF3(int location)
        {
            Vector3<float> value = new Vector3<float>();

            GL.GetUniformfv(Id, location, (float*)&value);

            return new Vector3(value.X, value.Y, value.Z);
        }

        protected Vector4 GetUniformF4(int location)
        {
            Vector4<float> value = new Vector4<float>();

            GL.GetUniformfv(Id, location, (float*)&value);

            return new Vector4(value.X, value.Y, value.Z, value.W);
        }
    }
}
