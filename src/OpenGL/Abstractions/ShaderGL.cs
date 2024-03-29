﻿using System;
using System.Text;

namespace Zene.Graphics.Base
{
    /// <summary>
    /// The most basic implimentation of an OpenGL shader object.
    /// </summary>
    public unsafe class ShaderGL : IShader
    {
        /// <summary>
        /// Creates an OpenGL shader object based on a given <see cref="ShaderType"/>.
        /// </summary>
        /// <param name="target">The type of texture to create.</param>
        public ShaderGL(ShaderType shaderType)
        {
            Id = GL.CreateShader((uint)shaderType);

            Type = shaderType;
        }

        public uint Id { get; }
        public ShaderType Type { get; }

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
                GL.DeleteShader(Id);
            }
        }

        public bool CompileStatus
        {
            get
            {
                int output = 0;

                GL.GetShaderiv(Id, GLEnum.CompileStatus, &output);

                return output == GLEnum.True;
            }
        }

        public void Compile()
        {
            GL.CompileShader(Id);

            // Compile error
            if (!CompileStatus)
            {
                StringBuilder message = GetShaderInfoLog();

                throw new ShaderException(this, message.ToString());
            }
        }

        /// <summary>
        /// Compiles the shader object.
        /// </summary>
        protected void CompileShader()
        {
            GL.CompileShader(Id);
        }

        /// <summary>
        /// Returns the number of characters in the information log for this shader object, including the null termination character.
        /// </summary>
        /// <returns></returns>
        protected int GetInfoLogLength()
        {
            int output = 0;

            GL.GetShaderiv(Id, GLEnum.InfoLogLength, &output);

            return output;
        }
        /// <summary>
        /// Teturns the length of the concatenation of the source strings that make up the shader source for this shaderobject, including the null termination character.
        /// </summary>
        /// <returns></returns>
        protected int GetShaderSourceLength()
        {
            int output = 0;

            GL.GetShaderiv(Id, GLEnum.ShaderSourceLength, &output);

            return output;
        }

        /// <summary>
        /// Returns the information log for this shader object.
        /// </summary>
        /// <param name="output">The <see cref="StringBuilder"/> to write the log into.</param>
        protected StringBuilder GetShaderInfoLog()
        {
            int length = GetInfoLogLength();
            StringBuilder output = new StringBuilder(length);
            GL.GetShaderInfoLog(Id, length, null, output);

            return output;
        }
        /// <summary>
        /// Returns the source code string from this shader object.
        /// </summary>
        /// <param name="output">The <see cref="StringBuilder"/> to write the source code into.</param>
        protected StringBuilder GetShaderSource()
        {
            int length = GetShaderSourceLength();
            StringBuilder output = new StringBuilder(length);
            GL.GetShaderSource(Id, length, null, output);

            return output;
        }

        /// <summary>
        /// Replaces the source code in a shader object.
        /// </summary>
        /// <param name="source">The source code to be loaded into the shader.</param>
        protected void ShaderSource(string source)
        {
            GL.ShaderSource(Id, source);
        }
        /// <summary>
        /// Replaces the source code in a shader object.
        /// </summary>
        /// <param name="source">The source code to be loaded into the shader.</param>
        protected void ShaderSource(string[] source)
        {
            int[] lengths = new int[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                lengths[i] = source[i].Length;
            }

            fixed (int* lengPtr = &lengths[0])
            {
                GL.ShaderSource(Id, source.Length, source, lengPtr);
            }
        }
    }
}
