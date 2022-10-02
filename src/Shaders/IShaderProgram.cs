using System;

namespace Zene.Graphics
{
    public enum ColourIndex : uint
    {
        First = 0,
        Second = 1
    }

    /// <summary>
    /// Objects that encapsulate an OpenGL shader program.
    /// </summary>
    public interface IShaderProgram : IGLObject
    {
        public new ShaderProgramProperties Properties { get; }
        IProperties IGLObject.Properties => Properties;
    }

    public interface IUniformStruct
    {
        public enum UniformType
        {
            Bool,
            Int,
            Uint,
            Float,
            Double,

            BVec2,
            BVec3,
            BVec4,

            IVec2,
            IVec3,
            IVec4,

            UiVec2,
            UiVec3,
            UiVec4,

            FVec2,
            FVec3,
            FVec4,

            DVec2,
            DVec3,
            DVec4
        }

        public struct Member
        {
            public Member(UniformType type, bool fad)
            {
                Type = type;
                DoubleAsFloat = fad;
            }

            public UniformType Type { get; }
            public bool DoubleAsFloat { get; }

            public static implicit operator Member(UniformType type) => new Member(type, false);
        }

        public Member[] Members();
    }
}
