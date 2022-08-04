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
    public interface IShaderProgram : IIdentifiable, IBindable, IDisposable
    {

    }
}
