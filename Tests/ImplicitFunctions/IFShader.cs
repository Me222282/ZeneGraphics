using System;
using System.IO;
using Zene.Graphics;
using Zene.Graphics.Base;
using Zene.Graphics.Shaders;

namespace ImplicitFunctions
{
    public class IFShader : IShaderProgram
    {
        public IFShader()
        {
            Id = CustomShader.CreateShader(
                File.ReadAllText("resources/ImplicitFuncVert.shader"),
                File.ReadAllText("resources/ImplicitFuncFrag.shader"));
        }

        public uint Id { get; private set; }

        public void Recreate()
        {
            Id = CustomShader.CreateShader(
                File.ReadAllText("resources/ImplicitFuncVert.shader"),
                File.ReadAllText("resources/ImplicitFuncFrag.shader"));
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            GL.DeleteProgram(Id);

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        public void Bind()
        {
            GL.UseProgram(Id);
        }
        public void Unbind()
        {
            GL.UseProgram(0);
        }
    }
}
