using System;
using Zene.Graphics.OpenGL;

namespace Zene.Graphics
{
    public enum ShaderType : uint
    {
        Vertex = GLEnum.VertexShader,
        Fragment = GLEnum.FragmentShader,
        Geometry = GLEnum.GeometryShader,
        Compute = GLEnum.ComputeShader,
        Tessellation = GLEnum.TessControlShader,
        TessEvaluation = GLEnum.TessEvaluationShader
    }

    public enum ShaderPrecision : uint
    {
        LowFloat = GLEnum.LowFloat,
        MediumFloat = GLEnum.MediumFloat,
        HighFloat = GLEnum.HighFloat,
        LowInt = GLEnum.LowInt,
        MediumInt = GLEnum.MediumInt,
        HighInt = GLEnum.HighInt
    }

    public interface IShader : IDisposable, IIdentifiable
    {
        /// <summary>
        /// Compiles the shader object with error handling.
        /// </summary>
        public void Compile();

        /// <summary>
        /// The type of shader this object contains.
        /// </summary>
        public ShaderType Type { get; }
    }
}
