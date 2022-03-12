using System;
using System.IO;
using Zene.Graphics;
using Zene.Graphics.Base;
using Zene.Graphics.Shaders;
using Zene.Structs;

namespace FXAA
{
    /// <summary>
    /// http://blog.simonrodriguez.fr/articles/2016/07/implementing_fxaa.html
    /// </summary>
    public class FXAAShader2 : IShaderProgram
    {
        public FXAAShader2()
        {
            Id = CustomShader.CreateShader(
                File.ReadAllText("resources/fxaaV2.glsl"),
                File.ReadAllText("resources/fxaaF2.glsl")
            );
            
            GL.BindAttribLocation(Id, 2, "vTex");
            
            _uniformTexture = GL.GetUniformLocation(Id, "u_colorTexture");
            
            _uniformInvSize = GL.GetUniformLocation(Id, "inverseScreenSize");
        }
        
        public uint Id { get; }
        
        private int _uniformTexture;
        private int _uniformInvSize;
        
        private int _texture = 0;
        public int TextureSlot
        {
            get => _texture;
            set
            {
                _texture = value;
                
                GL.ProgramUniform1i(Id, _uniformTexture, value);
            }
        }
        
        private Vector2 _size = Vector2.Zero;
        public Vector2 Size
        {
            get => _size;
            set
            {
                _size = value;
                
                GL.ProgramUniform2f(Id, _uniformInvSize, (float)(1.0 / value.X), (float)(1.0 / value.Y));
            }
        }
        
        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }
            
            GL.DeleteProgram(Id);
            
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        public void Bind() => GL.UseProgram(Id);
        public void Unbind() => GL.UseProgram(0);
    }
}