using System;
using Zene.Structs;

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

        public void PrepareDraw();
    }

    public interface IUniformStruct
    {
        public struct Member
        {
            public Member(UniformType type, bool fad)
            {
                Type = type;
                CastFloat = fad;
            }

            public UniformType Type { get; }
            public bool CastFloat { get; }

            public static implicit operator Member(UniformType type) => new Member(type, false);
        }

        public Member[] Members();
    }
}
