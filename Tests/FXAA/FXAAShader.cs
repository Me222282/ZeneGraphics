using System;
using System.IO;
using Zene.Graphics;
using Zene.Graphics.Base;
using Zene.Graphics.Shaders;
using Zene.Structs;

namespace FXAA
{
    /// <summary>
    /// https://github.com/McNopper/OpenGL/tree/master/Example42/shader
    /// </summary>
    public class FXAAShader : IShaderProgram
    {
        public FXAAShader()
        {
            Id = CustomShader.CreateShader(
                File.ReadAllText("resources/fxaaVert.glsl"),
                File.ReadAllText("resources/fxaaFrag.glsl")
            );

            _uniformTexture = GL.GetUniformLocation(Id, "u_colorTexture");
            
            _uniformTexelStep = GL.GetUniformLocation(Id, "u_texelStep");
            _uniformShowEdges = GL.GetUniformLocation(Id, "u_showEdges");
            _uniformFxaaOn = GL.GetUniformLocation(Id, "u_fxaaOn");
            
            _uniformLumaThreshold = GL.GetUniformLocation(Id, "u_lumaThreshold");
            _uniformMulReduce = GL.GetUniformLocation(Id, "u_mulReduce");
            _uniformMinReduce = GL.GetUniformLocation(Id, "u_minReduce");
            _uniformMaxSpan = GL.GetUniformLocation(Id, "u_maxSpan");
        }
        
        public uint Id { get; }
        
        private int _uniformTexture;
        
        private int _uniformTexelStep;
        private int _uniformShowEdges;
        private int _uniformFxaaOn;
        
        private int _uniformLumaThreshold;
        private int _uniformMulReduce;
        private int _uniformMinReduce;
        private int _uniformMaxSpan;
        
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
        
        private Vector2 _texelStep = Vector2.Zero;
        public Vector2 TexelStep
        {
            get => _texelStep;
            set
            {
                _texelStep = value;
                
                GL.ProgramUniform2f(Id, _uniformTexelStep, (float)value.X, (float)value.Y);
            }
        }
        private bool _showEdges = false;
        public bool ShowEdges
        {
            get => _showEdges;
            set
            {
                _showEdges = value;
                
                GL.ProgramUniform1i(Id, _uniformShowEdges, value ? 1 : 0);
            }
        }
        private bool _fxaaOn = false;
        public bool FXAAOn
        {
            get => _fxaaOn;
            set
            {
                _fxaaOn = value;
                
                GL.ProgramUniform1i(Id, _uniformFxaaOn, value ? 1 : 0);
            }
        }
        
        private double _lumaThreshold = 0.0;
        public double LumaThreshold
        {
            get => _lumaThreshold;
            set
            {
                _lumaThreshold = value;
                
                GL.ProgramUniform1f(Id, _uniformLumaThreshold, (float)value);
            }
        }
        private double _mulReduce = 0.0;
        public double MulReduce
        {
            get => _mulReduce;
            set
            {
                _mulReduce = value;
                
                GL.ProgramUniform1f(Id, _uniformMulReduce, (float)(1.0 / value));
            }
        }
        private double _minReduce = 0.0;
        public double MinReduce
        {
            get => _minReduce;
            set
            {
                _minReduce = value;
                
                GL.ProgramUniform1f(Id, _uniformMinReduce, (float)(1.0 / value));
            }
        }
        private double _maxSpan = 0.0;
        public double MaxSpan
        {
            get => _maxSpan;
            set
            {
                _maxSpan = value;
                
                GL.ProgramUniform1f(Id, _uniformMaxSpan, (float)value);
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