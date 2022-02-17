using System.Text;

namespace Zene.Graphics.OpenGL.Abstract3
{
    public static unsafe class ShaderOpenGL
    {
        /// <summary>
        /// Compiles the shader object.
        /// </summary>
        public static void CompileShader(this IShader shader)
        {
            GL.CompileShader(shader.Id);
        }

        /// <summary>
        /// Determines whether the last compile operation on this shader object was successful.
        /// </summary>
        /// <returns></returns>
        public static bool GetCompileStatus(this IShader shader)
        {
            int output = 0;

            GL.GetShaderiv(shader.Id, GLEnum.CompileStatus, &output);

            return output == GLEnum.True;
        }
        /// <summary>
        /// Returns the number of characters in the information log for this shader object, including the null termination character.
        /// </summary>
        /// <returns></returns>
        public static int GetInfoLogLength(this IShader shader)
        {
            int output = 0;

            GL.GetShaderiv(shader.Id, GLEnum.InfoLogLength, &output);

            return output;
        }
        /// <summary>
        /// Teturns the length of the concatenation of the source strings that make up the shader source for this shaderobject, including the null termination character.
        /// </summary>
        /// <returns></returns>
        public static int GetShaderSourceLength(this IShader shader)
        {
            int output = 0;

            GL.GetShaderiv(shader.Id, GLEnum.ShaderSourceLength, &output);

            return output;
        }

        /// <summary>
        /// Returns the information log for this shader object.
        /// </summary>
        /// <param name="output">The <see cref="StringBuilder"/> to write the log into.</param>
        public static void GetShaderInfoLog(this IShader shader, StringBuilder output)
        {
            GL.GetShaderInfoLog(shader.Id, output.Capacity, null, output);
        }
        /// <summary>
        /// Returns the source code string from this shader object.
        /// </summary>
        /// <param name="output">The <see cref="StringBuilder"/> to write the source code into.</param>
        public static void GetShaderSource(this IShader shader, StringBuilder output)
        {
            GL.GetShaderSource(shader.Id, output.Capacity, null, output);
        }

        /// <summary>
        /// Replaces the source code in a shader object.
        /// </summary>
        /// <param name="source">The source code to be loaded into the shader.</param>
        public static void ShaderSource(this IShader shader, string source)
        {
            GL.ShaderSource(shader.Id, source);
        }
        /// <summary>
        /// Replaces the source code in a shader object.
        /// </summary>
        /// <param name="source">The source code to be loaded into the shader.</param>
        public static void ShaderSource(this IShader shader, string[] source)
        {
            int[] lengths = new int[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                lengths[i] = source[i].Length;
            }

            fixed (int* lengPtr = &lengths[0])
            {
                GL.ShaderSource(shader.Id, source.Length, source, lengPtr);
            }
        }
    }
}
