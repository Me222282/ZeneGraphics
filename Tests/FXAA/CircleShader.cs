using System;
using System.IO;
using Zene.Graphics;
using Zene.Graphics.Base;
using Zene.Graphics.Shaders;
using Zene.Structs;

namespace FXAA
{
    public class CircleShader : IShaderProgram
    {
        public CircleShader()
        {
            Id = CustomShader.CreateShader(
                File.ReadAllText("resources/circleVert.glsl"),
                File.ReadAllText("resources/circleFrag.glsl")
            );

            _uniformMatrix = GL.GetUniformLocation(Id, "matrix");
            _uniformSize = GL.GetUniformLocation(Id, "size");
            _uniformRadius = GL.GetUniformLocation(Id, "radius");
            _uniformMinRadius = GL.GetUniformLocation(Id, "minRadius");
            _uniformColour = GL.GetUniformLocation(Id, "uColour");

            // Set matrix to "0"
            GL.ProgramUniformMatrix4fv(Id, _uniformMatrix, false, Matrix4.Identity.GetGLData());
            // Set size to "1"
            Size = 1.0;
        }
        
        public uint Id { get; }
        
        private int _uniformMatrix;

        private Matrix4 _m1 = Matrix4.Identity;
        public Matrix4 Matrix1
        {
            get
            {
                return _m1;
            }
            set
            {
                _m1 = value;
                SetMatrices();
            }
        }
        private Matrix4 _m2 = Matrix4.Identity;
        public Matrix4 Matrix2
        {
            get
            {
                return _m2;
            }
            set
            {
                _m2 = value;
                SetMatrices();
            }
        }
        private Matrix4 _m3 = Matrix4.Identity;
        public Matrix4 Matrix3
        {
            get
            {
                return _m3;
            }
            set
            {
                _m3 = value;
                SetMatrices();
            }
        }
        private void SetMatrices()
        {
            GL.ProgramUniformMatrix4fv(Id, _uniformMatrix, false, (_m1 * _m2 * _m3).GetGLData());
        }
        
        private int _uniformSize;
        private int _uniformRadius;
        private int _uniformMinRadius;
        
        private double _size;
        public double Size
        {
            get => _size;
            set
            {
                _size = value;
                
                GL.ProgramUniform1f(Id, _uniformSize, (float)value);
                GL.ProgramUniform1f(Id, _uniformRadius, (float)(value * value * 0.25));
            }
        }
        
        private double _lWidth;
        public double LineWidth
        {
            get => _lWidth;
            set
            {
                _lWidth = value;
                
                double len = (_size * 0.5) - value;
                
                GL.ProgramUniform1f(Id, _uniformMinRadius, (float)(len * len));
            }
        }
        
        private int _uniformColour;
        private ColourF _colour;
        public ColourF Colour
        {
            get => _colour;
            set
            {
                _colour = value;
                
                GL.ProgramUniform4f(Id, _uniformColour, (float)value.R, (float)value.G, (float)value.G, (float)value.A);
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