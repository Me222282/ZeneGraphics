using Zene.Graphics.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Zene.Structs;

namespace Zene.Graphics.Shaders
{
    public enum MatrixType
    {
        Matrix2x2,
        Matrix2x3,
        Matrix2x4,
        Matrix3x2,
        Matrix3x3,
        Matrix3x4,
        Matrix4x2,
        Matrix4x3,
        Matrix4x4
    }

    public unsafe class CustomShader : IShaderProgram
    {
        public CustomShader(string vertexPath, string fragmentPath)
        {
            Program = GL.CreateProgram();
            string vSource = File.ReadAllText(vertexPath);
            uint vs = CompileShader(GLEnum.VertexShader, vSource);
            string fSource = File.ReadAllText(fragmentPath);
            uint fs = CompileShader(GLEnum.FragmentShader, fSource);

            GL.AttachShader(Program, vs);
            GL.AttachShader(Program, fs);
            GL.LinkProgram(Program);
            GL.ValidateProgram(Program);

            GL.DetachShader(Program, vs);
            GL.DetachShader(Program, fs);
            GL.DeleteShader(vs);
            GL.DeleteShader(fs);

            //List<int> layouts = FindLayouts(vSource);
            //layouts.AddRange(FindLayouts(fSource));

            //LayoutLocations = layouts.ToArray();

            uniformNames = FindUniforms(vSource);
            uniformNames.AddRange(FindUniforms(fSource));

            List<int> uniforms = new List<int>();

            foreach (string uniform in uniformNames) {
                uniforms.Add(GL.GetUniformLocation(Program, uniform)); }

            uniformLocations = uniforms.ToArray();
        }

        public uint Program { get; }
        uint IIdentifiable.Id => Program;

        protected bool _Bound;
        public bool Bound
        {
            get
            {
                return _Bound;
            }
            set
            {
                if (value && (!_Bound))
                {
                    Bind();
                }
                else if ((!value) && _Bound)
                {
                    Unbind();
                }
            }
        }

        //public int[] LayoutLocations { get; }

        private readonly int[] uniformLocations;

        private readonly List<string> uniformNames;

        private static List<string> FindUniforms(string source)
        {
            List<string> uniforms = new List<string>();

            string checkSource = source.Trim();

            while (checkSource.Contains("uniform"))
            {
                int i = checkSource.IndexOf("uniform");

                checkSource = checkSource.Remove(0, i + 8);

                checkSource = checkSource.Trim();

                int i2 = checkSource.IndexOf(' ');

                checkSource = checkSource.Remove(0, i2 + 1);

                int i3 = checkSource.IndexOf(';');

                string uniform = checkSource.Remove(i3);

                uniform = uniform.Trim();

                uniforms.Add(uniform);
            }

            return uniforms;
        }

        /*
        private List<int> FindLayouts(string source)
        {
            List<int> layouts = new List<int>();

            string checkSource = source.Trim();

            while (checkSource.Contains("layout"))
            {
                int i = checkSource.IndexOf("layout");

                checkSource = checkSource.Remove(0, i + 6);

                checkSource = checkSource.Trim();

                if (!checkSource.StartsWith('(')) { continue; }

                string inTest = checkSource.Remove(checkSource.IndexOf('\n'));

                if (!inTest.Contains("in ")) { continue; }

                int i2 = checkSource.IndexOf("location");

                checkSource = checkSource.Remove(0, i2 + 8);

                checkSource = checkSource.Trim();

                int i3 = checkSource.IndexOf(' ');

                checkSource = checkSource.Remove(0, i3 + 1);

                checkSource = checkSource.Trim();

                string layout = checkSource.Remove(checkSource.IndexOf(')'));

                layout = layout.Trim();

                layouts.Add(int.Parse(layout));
            }

            return layouts;
        }
        */

        public void SetUniform<T>(string name, T value)
        {
            Type t = typeof(T);

            if (t == typeof(int))
            {
                GL.Uniform1i(LocationOf(name), Convert.ToInt32(value));
            }
            else if (t == typeof(uint))
            {
                GL.Uniform1ui(LocationOf(name), Convert.ToUInt32(value));
            }
            else if (t == typeof(float))
            {
                GL.Uniform1f(LocationOf(name), Convert.ToSingle(value));
            }
            else if (t == typeof(double))
            {
                GL.Uniform1d(LocationOf(name), Convert.ToDouble(value));
            }
            else
            {
                throw new Exception("Uniforms must be of type int, uint, float or double.");
            }
        }

        public void SetUniform<T>(string name, T value1, T value2)
        {
            Type t = typeof(T);

            if (t == typeof(int))
            {
                GL.Uniform2i(LocationOf(name), Convert.ToInt32(value1), Convert.ToInt32(value2));
            }
            else if (t == typeof(uint))
            {
                GL.Uniform2ui(LocationOf(name), Convert.ToUInt32(value1), Convert.ToUInt32(value2));
            }
            else if (t == typeof(float))
            {
                GL.Uniform2f(LocationOf(name), Convert.ToSingle(value1), Convert.ToSingle(value2));
            }
            else if (t == typeof(double))
            {
                GL.Uniform2d(LocationOf(name), Convert.ToDouble(value1), Convert.ToDouble(value2));
            }
            else
            {
                throw new Exception("Uniforms must be of type int, uint, float or double.");
            }
        }

        public void SetUniform<T>(string name, T value1, T value2, T value3)
        {
            Type t = typeof(T);

            if (t == typeof(int))
            {
                GL.Uniform3i(LocationOf(name), Convert.ToInt32(value1), Convert.ToInt32(value2), Convert.ToInt32(value3));
            }
            else if (t == typeof(uint))
            {
                GL.Uniform3ui(LocationOf(name), Convert.ToUInt32(value1), Convert.ToUInt32(value2), Convert.ToUInt32(value3));
            }
            else if (t == typeof(float))
            {
                GL.Uniform3f(LocationOf(name), Convert.ToSingle(value1), Convert.ToSingle(value2), Convert.ToSingle(value3));
            }
            else if (t == typeof(double))
            {
                GL.Uniform3d(LocationOf(name), Convert.ToDouble(value1), Convert.ToDouble(value2), Convert.ToDouble(value3));
            }
            else
            {
                throw new Exception("Uniforms must be of type int, uint, float or double.");
            }
        }

        public void SetUniform<T>(string name, T value1, T value2, T value3, T value4)
        {
            Type t = typeof(T);

            if (t == typeof(int))
            {
                GL.Uniform4i(LocationOf(name), Convert.ToInt32(value1), Convert.ToInt32(value2), Convert.ToInt32(value3), Convert.ToInt32(value4));
            }
            else if (t == typeof(uint))
            {
                GL.Uniform4ui(LocationOf(name), Convert.ToUInt32(value1), Convert.ToUInt32(value2), Convert.ToUInt32(value3), Convert.ToUInt32(value4));
            }
            else if (t == typeof(float))
            {
                GL.Uniform4f(LocationOf(name), Convert.ToSingle(value1), Convert.ToSingle(value2), Convert.ToSingle(value3), Convert.ToSingle(value4));
            }
            else if (t == typeof(double))
            {
                GL.Uniform4d(LocationOf(name), Convert.ToDouble(value1), Convert.ToDouble(value2), Convert.ToDouble(value3), Convert.ToDouble(value4));
            }
            else
            {
                throw new Exception("Uniforms must be of type int, uint, float or double.");
            }
        }

        public void SetUniform<T>(string name, params T[] values) where T : unmanaged
        {
            Type t = typeof(T);

            if (t == typeof(int))
            {
                fixed (void* value = &values[0])
                {
                    GL.Uniform1iv(LocationOf(name), values.Length, (int*)value);
                }
            }
            else if (t == typeof(uint))
            {
                fixed (void* value = &values[0])
                {
                    GL.Uniform1uiv(LocationOf(name), values.Length, (uint*)value);
                }
            }
            else if (t == typeof(float))
            {
                fixed (void* value = &values[0])
                {
                    GL.Uniform1fv(LocationOf(name), values.Length, (float*)value);
                }
            }
            else if (t == typeof(double))
            {
                fixed (void* value = &values[0])
                {
                    GL.Uniform1dv(LocationOf(name), values.Length, (double*)value);
                }
            }
            else
            {
                throw new Exception("Uniforms must be of type int, uint, float or double.");
            }
        }

        /*
        public void SetUniformMatrix(string name, MatrixType type, IMatrix matrix)
        {
            if (type == MatrixType.Matrix2x2)
            {
                GL.ProgramUniformMatrix2fv(Program, LocationOf(name), false, matrix.GetGLData());
            }
            else if (type == MatrixType.Matrix2x3)
            {
                GL.ProgramUniformMatrix2x3fv(Program, LocationOf(name), false, matrix.GetGLData());
            }
            else if (type == MatrixType.Matrix2x4)
            {
                GL.ProgramUniformMatrix2x4fv(Program, LocationOf(name), false, matrix.GetGLData());
            }
            else if (type == MatrixType.Matrix3x2)
            {
                GL.ProgramUniformMatrix3x2fv(Program, LocationOf(name), false, matrix.GetGLData());
            }
            else if (type == MatrixType.Matrix3x3)
            {
                GL.ProgramUniformMatrix3fv(Program, LocationOf(name), false, matrix.GetGLData());
            }
            else if (type == MatrixType.Matrix3x4)
            {
                GL.ProgramUniformMatrix3x4fv(Program, LocationOf(name), false, matrix.GetGLData());
            }
            else if (type == MatrixType.Matrix4x2)
            {
                GL.ProgramUniformMatrix4x2fv(Program, LocationOf(name), false, matrix.GetGLData());
            }
            else if (type == MatrixType.Matrix4x3)
            {
                GL.ProgramUniformMatrix4x3fv(Program, LocationOf(name), false, matrix.GetGLData());
            }
            else if (type == MatrixType.Matrix4x4)
            {
                GL.ProgramUniformMatrix4fv(Program, LocationOf(name), false, matrix.GetGLData());
            }
        }*/

        private int LocationOf(string name)
        {
            int i = uniformNames.IndexOf(name);

            if (i == -1) { throw new Exception("Uniform name does not exist."); }

            return uniformLocations[i];
        }

        public void Dispose()
        {
            GL.DeleteProgram(Program);
        }

        public void Bind()
        {
            GL.UseProgram(Program);

            _Bound = true;
        }

        public void Unbind()
        {
            GL.UseProgram(0);

            _Bound = false;
        }

        public static uint CompileShader(uint type, string source)
        {
            uint id = GL.CreateShader(type);
            GL.ShaderSource(id, source);
            GL.CompileShader(id);
            
            GL.GetShaderiv(id, GLEnum.CompileStatus, out int result);
            
            if (result == GLEnum.False)
            {
                GL.GetShaderiv(id, GLEnum.InfoLogLength, out int length);
                StringBuilder message = new StringBuilder(length);
                GL.GetShaderInfoLog(id, length, null, message);
                GL.DeleteShader(id);

                Console.WriteLine(message.ToString());
            }

            return id;
        }

        /*
        public static uint CreateShader(string vertexPath, string fragmentPath)
        {
            uint program = GL.CreateProgram();
            string vSource = File.ReadAllText(vertexPath);
            uint vs = CompileShader(GLEnum.VertexShader, vSource);
            string fSource = File.ReadAllText(fragmentPath);
            uint fs = CompileShader(GLEnum.FragmentShader, fSource);
            
            GL.AttachShader(program, vs);
            GL.AttachShader(program, fs);
            GL.LinkProgram(program);
            GL.ValidateProgram(program);

            GL.DetachShader(program, vs);
            GL.DetachShader(program, fs);
            GL.DeleteShader(vs);
            GL.DeleteShader(fs);

            return program;
        }*/

        public static uint CreateShader(string vSource, string fSource)
        {
            uint program = GL.CreateProgram();
            uint vs = CompileShader(GLEnum.VertexShader, vSource);
            uint fs = CompileShader(GLEnum.FragmentShader, fSource);

            GL.AttachShader(program, vs);
            GL.AttachShader(program, fs);
            GL.LinkProgram(program);
            GL.ValidateProgram(program);

            GL.DetachShader(program, vs);
            GL.DetachShader(program, fs);
            GL.DeleteShader(vs);
            GL.DeleteShader(fs);

            return program;
        }
    }
}
