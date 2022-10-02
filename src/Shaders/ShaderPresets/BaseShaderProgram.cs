using Zene.Graphics.Base;

namespace Zene.Graphics
{
    public class BaseShaderProgram : ShaderProgramGL
    {
        private class Shader : ShaderGL
        {
            public Shader(ShaderType type, string source)
                : base(type)
            {
                ShaderSource(source);
            }
        }

        public int[] Uniforms { get; private set; }

        protected void Create(string vertex, string fragment, params string[] uniformNames)
        {
            Shader vertexShader = new Shader(ShaderType.Vertex, vertex);
            vertexShader.Compile();
            Shader fragmentShader = new Shader(ShaderType.Fragment, fragment);
            fragmentShader.Compile();

            AttachShader(vertexShader);
            AttachShader(fragmentShader);

            LinkProgram();
            Validate();

            DetachShader(vertexShader);
            DetachShader(fragmentShader);

            vertexShader.Dispose();
            fragmentShader.Dispose();

            // Load uniforms
            Uniforms = new int[uniformNames.Length];

            for (int i = 0; i < uniformNames.Length; i++)
            {
                Uniforms[i] = GetUniformLocation(uniformNames[i]);
            }
        }
    }
}
