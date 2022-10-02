using System;
using System.Collections.Generic;

namespace Zene.Graphics
{
    public class ShaderProgramProperties : IProperties
    {
        public ShaderProgramProperties(IShaderProgram source)
        {
            Source = source;
        }

        public IShaderProgram Source { get; }
        IGLObject IProperties.Source => Source;

        public bool Sync()
        {
            throw new NotImplementedException();
        }

        internal readonly List<IShader> _attachedShaders = new List<IShader>();

        public ReadOnlySpan<IShader> AttachedShaders => _attachedShaders.ToArray();
    }
}
