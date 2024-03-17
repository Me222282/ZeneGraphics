using System;
using Zene.Graphics.Base;
using Zene.Structs;

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

    public enum ShaderLocation : uint
    {
        Vertex = 0,
        Colour = 1,
        TextureCoords = 2,
        Normal = 3,
        Tangent = 4,
        NormalTexture = 5
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

        /// <summary>
        /// Determines whether the last compile operation on this shader object was successful.
        /// </summary>
        public bool CompileStatus { get; }
    }
}
