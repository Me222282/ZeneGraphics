using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public abstract class BaseShaderProgram : ShaderProgramGL, IDrawingShader
    {
        private class Shader : ShaderGL
        {
            public Shader(ShaderType type, string source)
                : base(type)
            {
                ShaderSource(source);
            }
        }

        protected int[] Uniforms { get; private set; }

        public virtual IMatrix Matrix1 { get { return Matrix.Identity; } set { } }
        public virtual IMatrix Matrix2 { get { return Matrix.Identity; } set { } }
        public virtual IMatrix Matrix3 { get { return Matrix.Identity; } set { } }

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
                Uniforms[i] = GetUniformIndex(uniformNames[i]);
            }
        }

        protected static T GetInstance<T>()
            where T : BaseShaderProgram, new()
        {
            IIdentifiable i = GL.context.GetTrack(typeof(T));

            if (i != null)
            {
                return i as T;
            }

            T shader = new T();
            GL.context.TrackObject(shader);

            return shader;
        }
    }
}
