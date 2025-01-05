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
        private int _mi;
        
        public IMatrix Matrix1 { get; set; }
        public IMatrix Matrix2 { get; set; }
        public IMatrix Matrix3 { get; set; }

        protected void Create(string vertex, string fragment, int mi, params string[] uniformNames)
        {
            _mi = mi;
            
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

        public override void PrepareDraw()
        {
            if (_mi < 0) { return; }
            
            Matrix4 m1;
            Matrix4 m2;
            Matrix4 m3;
            
            if (Matrix1 is Matrix4 m) { m1 = m; }
            else { m1 = new Matrix4(Matrix1); }
            
            if (Matrix2 is Matrix4 v) { m2 = v; }
            else { m2 = new Matrix4(Matrix2); }
            
            if (Matrix3 is Matrix4 p) { m3 = p; }
            else { m3 = new Matrix4(Matrix3); }
            
            SetUniform(Uniforms[_mi], m1 * m2 * m3);
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
