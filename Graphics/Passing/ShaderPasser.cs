using System;
using System.Text;
using Zene.Graphics.OpenGL;
using Zene.Graphics.OpenGL.Abstract3;

namespace Zene.Graphics.Passing
{
    /// <summary>
    /// An object for coping and externally managing shader objects.
    /// </summary>
    public class ShaderPasser : IShader
    {
        public ShaderPasser(IShader shader)
        {
            Id = shader.Id;
            Type = shader.Type;
        }
        public ShaderPasser(uint id, ShaderType type)
        {
            Id = id;
            Type = type;
        }

        public uint Id { get; }
        public ShaderType Type { get; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public void Compile()
        {
            this.CompileShader();

            // Compile error
            if (!this.GetCompileStatus())
            {
                int length = this.GetInfoLogLength();
                StringBuilder message = new StringBuilder(length);
                this.GetShaderInfoLog(message);

                throw new ShaderException(this, message.ToString());
            }
        }

        /// <summary>
        /// Returns a new <see cref="ShaderGL"/> equivalent for the data this shader contains.
        /// </summary>
        /// <returns></returns>
        public ShaderGL Pass()
        {
            return new ShaderGL(Id, Type);
        }

        /// <summary>
        /// Returns a new <see cref="ShaderGL"/> equivalent for the data provided.
        /// </summary>
        /// <returns></returns>
        public static ShaderGL Pass(uint id, ShaderType type)
        {
            return new ShaderGL(id, type);
        }
    }
}
