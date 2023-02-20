using System;
using System.Collections.Generic;
using System.Text;
using Zene.Graphics.Base;
using Zene.Structs;

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

        internal unsafe void SyncUniforms()
        {
            int uniformCount = GL.GetProgrami(Source.Id, GLEnum.ActiveUniforms);
            _uniforms = new UniformVariable[uniformCount];

            StringBuilder name = new StringBuilder(GL.GetProgrami(Source.Id, GLEnum.ActiveUniformMaxLength));

            int size = 0;
            for (int i = 0; i < uniformCount; i += size)
            {
                uint type = 0;

                name.Clear();
                GL.GetActiveUniform(Source.Id, (uint)i, name.Capacity, null, &size, &type, name);

                _uniforms[i] = new UniformVariable(name.ToString(), i, size, type);
            }
        }

        internal UniformVariable[] _uniforms = Array.Empty<UniformVariable>();
        internal readonly List<IShader> _attachedShaders = new List<IShader>();

        public ReadOnlySpan<IShader> AttachedShaders => _attachedShaders.ToArray();
        public int UniformCount => _uniforms.Length;

        public UniformVariable this[int index] => _uniforms[index];
        public UniformVariable this[Index index] => _uniforms[index];
    }

    public enum UniformType : uint
    {
        Bool = GLEnum.Bool,
        Int = GLEnum.Int,
        Uint = GLEnum.UInt,
        Float = GLEnum.Float,
        Double = GLEnum.Double,

        BVec2 = GLEnum.BoolVec2,
        BVec3 = GLEnum.BoolVec3,
        BVec4 = GLEnum.BoolVec4,

        IVec2 = GLEnum.IntVec2,
        IVec3 = GLEnum.IntVec3,
        IVec4 = GLEnum.IntVec4,

        UiVec2 = GLEnum.UIntVec2,
        UiVec3 = GLEnum.UIntVec3,
        UiVec4 = GLEnum.UIntVec4,

        FVec2 = GLEnum.FloatVec2,
        FVec3 = GLEnum.FloatVec3,
        FVec4 = GLEnum.FloatVec4,

        DVec2 = GLEnum.DoubleVec2,
        DVec3 = GLEnum.DoubleVec3,
        DVec4 = GLEnum.DoubleVec4,

        FMat2 = GLEnum.FloatMat2,
        FMat3 = GLEnum.FloatMat3,
        FMat4 = GLEnum.FloatMat4,
        FMat2x3 = GLEnum.FloatMat2x3,
        FMat2x4 = GLEnum.FloatMat2x4,
        FMat3x2 = GLEnum.FloatMat3x2,
        FMat3x4 = GLEnum.FloatMat3x4,
        FMat4x2 = GLEnum.FloatMat4x2,
        FMat4x3 = GLEnum.FloatMat4x3,

        DMat2 = GLEnum.DoubleMat2,
        DMat3 = GLEnum.DoubleMat3,
        DMat4 = GLEnum.DoubleMat4,
        DMat2x3 = GLEnum.DoubleMat2x3,
        DMat2x4 = GLEnum.DoubleMat2x4,
        DMat3x2 = GLEnum.DoubleMat3x2,
        DMat3x4 = GLEnum.DoubleMat3x4,
        DMat4x2 = GLEnum.DoubleMat4x2,
        DMat4x3 = GLEnum.DoubleMat4x3,

        Sampler1D = GLEnum.Sampler1d,
        Sampler2D = GLEnum.Sampler2d,
        Sampler3D = GLEnum.Sampler3d,
        SamplerCube = GLEnum.SamplerCube,
        Sampler1DShadow = GLEnum.Sampler1dShadow,
        Sampler2DShadow = GLEnum.Sampler2dShadow,
        Sampler1DArray = GLEnum.Sampler1dArray,
        Sampler2DArray = GLEnum.Sampler2dArray,
        Sampler1DArrayShadow = GLEnum.Sampler1dArrayShadow,
        Sampler2DArrayShadow = GLEnum.Sampler2dArrayShadow,
        Sampler2DMultisample = GLEnum.Sampler2dMultisample,
        Sampler2DMultisampleArray = GLEnum.Sampler2dMultisampleArray,
        SamplerCubeShadow = GLEnum.SamplerCubeShadow,
        SamplerBuffer = GLEnum.SamplerBuffer,
        Sampler2DRect = GLEnum.Sampler2dRect,
        Sampler2DRectShadow = GLEnum.Sampler2dRectShadow,

        IntSampler1D = GLEnum.IntSampler1d,
        IntSampler2D = GLEnum.IntSampler2d,
        IntSampler3D = GLEnum.IntSampler3d,
        IntSamplerCube = GLEnum.IntSamplerCube,
        IntSampler1DArray = GLEnum.IntSampler1dArray,
        IntSampler2DArray = GLEnum.IntSampler2dArray,
        IntSampler2DMultisample = GLEnum.IntSampler2dMultisample,
        IntSampler2DMultisampleArray = GLEnum.IntSampler2dMultisampleArray,
        IntSamplerBuffer = GLEnum.IntSamplerBuffer,
        IntSampler2DRect = GLEnum.IntSampler2dRect,

        UintSampler1D = GLEnum.UIntSampler1d,
        UintSampler2D = GLEnum.UIntSampler2d,
        UintSampler3D = GLEnum.UIntSampler3d,
        UintSamplerCube = GLEnum.UIntSamplerCube,
        UintSampler1DArray = GLEnum.UIntSampler1dArray,
        UintSampler2DArray = GLEnum.UIntSampler2dArray,
        UintSampler2DMultisample = GLEnum.UIntSampler2dMultisample,
        UintSampler2DMultisampleArray = GLEnum.UIntSampler2dMultisampleArray,
        UintSamplerBuffer = GLEnum.UIntSamplerBuffer,
        UintSampler2DRect = GLEnum.UIntSampler2dRect,
    }

    internal delegate void SetUniform(int location, int size, int index, object value);

    public unsafe struct UniformVariable
    {
        public UniformVariable(string name, int location, int size, uint type)
        {
            Name = name;
            Location = location;
            Size = size;
            Type = (UniformType)type;

            su = Type switch
            {
                UniformType.Bool => UniformInt,
                UniformType.BVec2 => UniformInt2,
                UniformType.BVec3 => UniformInt3,
                UniformType.BVec4 => UniformInt4,

                UniformType.Int => UniformInt,
                UniformType.IVec2 => UniformInt2,
                UniformType.IVec3 => UniformInt3,
                UniformType.IVec4 => UniformInt4,

                UniformType.Uint => UniformUInt,
                UniformType.UiVec2 => UniformUInt2,
                UniformType.UiVec3 => UniformUInt3,
                UniformType.UiVec4 => UniformUInt4,

                UniformType.Float => UniformFloat,
                UniformType.FVec2 => UniformFloat2,
                UniformType.FVec3 => UniformFloat3,
                UniformType.FVec4 => UniformFloat4,

                UniformType.Double => UniformDouble,
                UniformType.DVec2 => UniformDouble2,
                UniformType.DVec3 => UniformDouble3,
                UniformType.DVec4 => UniformDouble4,

                UniformType.FMat2 => UniformFMat2,
                UniformType.FMat2x3 => UniformFMat2,
                UniformType.FMat2x4 => UniformFMat2x4,
                UniformType.FMat3 => UniformFMat3,
                UniformType.FMat3x2 => UniformFMat3x2,
                UniformType.FMat3x4 => UniformFMat3x4,
                UniformType.FMat4 => UniformFMat4,
                UniformType.FMat4x2 => UniformFMat4x2,
                UniformType.FMat4x3 => UniformFMat4x3,

                UniformType.DMat2 => UniformDMat2,
                UniformType.DMat2x3 => UniformDMat2,
                UniformType.DMat2x4 => UniformDMat2x4,
                UniformType.DMat3 => UniformDMat3,
                UniformType.DMat3x2 => UniformDMat3x2,
                UniformType.DMat3x4 => UniformDMat3x4,
                UniformType.DMat4 => UniformDMat4,
                UniformType.DMat4x2 => UniformDMat4x2,
                UniformType.DMat4x3 => UniformDMat4x3,

                UniformType.Sampler1D | UniformType.Sampler1DArray | UniformType.Sampler1DArrayShadow |
                UniformType.Sampler1DShadow | UniformType.Sampler2DArray |
                UniformType.Sampler2DArrayShadow | UniformType.Sampler2DMultisample | UniformType.Sampler2DMultisampleArray |
                UniformType.Sampler2DRect | UniformType.Sampler2DRectShadow | UniformType.Sampler2DShadow |
                UniformType.Sampler3D | UniformType.SamplerBuffer | UniformType.SamplerCube | UniformType.SamplerCubeShadow |
                UniformType.IntSampler1D | UniformType.IntSampler1DArray | UniformType.IntSampler2D |
                UniformType.IntSampler2DArray | UniformType.IntSampler2DMultisample | UniformType.IntSampler2DMultisampleArray |
                UniformType.IntSampler2DRect | UniformType.IntSampler3D | UniformType.IntSamplerBuffer |
                UniformType.IntSamplerCube => UniformInt,
                UniformType.Sampler2D => UniformInt,

                UniformType.UintSampler1D | UniformType.UintSampler1DArray | UniformType.UintSampler2D |
                UniformType.UintSampler2DArray | UniformType.UintSampler2DMultisample | UniformType.UintSampler2DMultisampleArray |
                UniformType.UintSampler2DRect | UniformType.UintSampler3D | UniformType.UintSamplerBuffer |
                UniformType.UintSamplerCube => UniformUInt,

                _ => throw new NotSupportedException("Type not supported")
            };
        }

        public string Name { get; }
        public int Location { get; }
        public int Size { get; }
        public UniformType Type { get; }
        internal SetUniform su;

        private static void UniformInt(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform1iv(location, size, (int*)(IntPtr)value);
                return;
            }

            GL.Uniform1i(location + index, (int)value);
        }
        private static void UniformInt2(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform2iv(location, size, (int*)(IntPtr)value);
                return;
            }

            (int x, int y) = ((int, int))value;

            GL.Uniform2i(location + index, x, y);
        }
        private static void UniformInt3(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform3iv(location, size, (int*)(IntPtr)value);
                return;
            }

            (int x, int y, int z) = ((int, int, int))value;

            GL.Uniform3i(location + index, x, y, z);
        }
        private static void UniformInt4(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform4iv(location, size, (int*)(IntPtr)value);
                return;
            }

            (int x, int y, int z, int w) = ((int, int, int, int))value;

            GL.Uniform4i(location + index, x, y, z, w);
        }

        private static void UniformUInt(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform1uiv(location, size, (uint*)(IntPtr)value);
                return;
            }

            GL.Uniform1ui(location + index, (uint)value);
        }
        private static void UniformUInt2(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform2uiv(location, size, (uint*)(IntPtr)value);
                return;
            }

            (uint x, uint y) = ((uint, uint))value;

            GL.Uniform2ui(location + index, x, y);
        }
        private static void UniformUInt3(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform3uiv(location, size, (uint*)(IntPtr)value);
                return;
            }

            (uint x, uint y, uint z) = ((uint, uint, uint))value;

            GL.Uniform3ui(location + index, x, y, z);
        }
        private static void UniformUInt4(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform4uiv(location, size, (uint*)(IntPtr)value);
                return;
            }

            (uint x, uint y, uint z, uint w) = ((uint, uint, uint, uint))value;

            GL.Uniform4ui(location + index, x, y, z, w);
        }

        private static void UniformFloat(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform1fv(location, size, (float*)(IntPtr)value);
                return;
            }

            GL.Uniform1f(location + index, (float)value);
        }
        private static void UniformFloat2(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform2fv(location, size, (float*)(IntPtr)value);
                return;
            }

            (float x, float y) = ((float, float))value;

            GL.Uniform2f(location + index, x, y);
        }
        private static void UniformFloat3(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform3fv(location, size, (float*)(IntPtr)value);
                return;
            }

            (float x, float y, float z) = ((float, float, float))value;

            GL.Uniform3f(location + index, x, y, z);
        }
        private static void UniformFloat4(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform4fv(location, size, (float*)(IntPtr)value);
                return;
            }

            (float x, float y, float z, float w) = ((float, float, float, float))value;

            GL.Uniform4f(location + index, x, y, z, w);
        }

        private static void UniformDouble(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform1dv(location, size, (double*)(IntPtr)value);
                return;
            }

            GL.Uniform1d(location + index, (double)value);
        }
        private static void UniformDouble2(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform2dv(location, size, (double*)(IntPtr)value);
                return;
            }

            (double x, double y) = ((double, double))value;

            GL.Uniform2d(location + index, x, y);
        }
        private static void UniformDouble3(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform3dv(location, size, (double*)(IntPtr)value);
                return;
            }

            (double x, double y, double z) = ((double, double, double))value;

            GL.Uniform3d(location + index, x, y, z);
        }
        private static void UniformDouble4(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                GL.Uniform4dv(location, size, (double*)(IntPtr)value);
                return;
            }

            (double x, double y, double z, double w) = ((double, double, double, double))value;

            GL.Uniform4d(location + index, x, y, z, w);
        }

        private static void UniformFMat2(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                float* matData = stackalloc float[4 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 4;
                    matData[mI] = (float)msV[0, 0];
                    matData[mI + 1] = (float)msV[1, 0];

                    matData[mI + 2] = (float)msV[0, 1];
                    matData[mI + 3] = (float)msV[1, 1];
                }

                GL.UniformMatrix2fv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;

            float* data = stackalloc float[4];
            MatrixSpan ms = matrix.MatrixData();

            data[0] = (float)ms[0, 0];
            data[1] = (float)ms[1, 0];

            data[2] = (float)ms[0, 1];
            data[3] = (float)ms[1, 1];

            GL.UniformMatrix2fv(location, size, false, data);
        }
        private static void UniformFMat2x3(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                float* matData = stackalloc float[6 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 6;
                    matData[mI] = (float)msV[0, 0];
                    matData[mI + 1] = (float)msV[1, 0];
                    matData[mI + 2] = (float)msV[2, 0];

                    matData[mI + 3] = (float)msV[0, 1];
                    matData[mI + 4] = (float)msV[1, 1];
                    matData[mI + 5] = (float)msV[2, 1];
                }

                GL.UniformMatrix2x3fv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;

            float* data = stackalloc float[6];
            MatrixSpan ms = matrix.MatrixData();

            data[0] = (float)ms[0, 0];
            data[1] = (float)ms[1, 0];
            data[2] = (float)ms[2, 0];

            data[3] = (float)ms[0, 1];
            data[4] = (float)ms[1, 1];
            data[5] = (float)ms[2, 1];

            GL.UniformMatrix2x3fv(location, size, false, data);
        }
        private static void UniformFMat2x4(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                float* matData = stackalloc float[8 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 8;
                    matData[mI] = (float)msV[0, 0];
                    matData[mI + 1] = (float)msV[1, 0];
                    matData[mI + 2] = (float)msV[2, 0];
                    matData[mI + 3] = (float)msV[3, 0];

                    matData[mI + 4] = (float)msV[0, 1];
                    matData[mI + 5] = (float)msV[1, 1];
                    matData[mI + 6] = (float)msV[2, 1];
                    matData[mI + 7] = (float)msV[3, 1];
                }

                GL.UniformMatrix2x4fv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;

            float* data = stackalloc float[8];
            MatrixSpan ms = matrix.MatrixData();

            data[0] = (float)ms[0, 0];
            data[1] = (float)ms[1, 0];
            data[2] = (float)ms[2, 0];
            data[3] = (float)ms[3, 0];

            data[4] = (float)ms[0, 1];
            data[5] = (float)ms[1, 1];
            data[6] = (float)ms[2, 1];
            data[7] = (float)ms[3, 1];

            GL.UniformMatrix2x4fv(location, size, false, data);
        }
        private static void UniformFMat3(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                float* matData = stackalloc float[9 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 9;
                    matData[mI] = (float)msV[0, 0];
                    matData[mI + 1] = (float)msV[1, 0];
                    matData[mI + 2] = (float)msV[2, 0];

                    matData[mI + 3] = (float)msV[0, 1];
                    matData[mI + 4] = (float)msV[1, 1];
                    matData[mI + 5] = (float)msV[2, 1];

                    matData[mI + 6] = (float)msV[0, 2];
                    matData[mI + 7] = (float)msV[1, 2];
                    matData[mI + 8] = (float)msV[2, 2];
                }

                GL.UniformMatrix3fv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;

            float* data = stackalloc float[9];
            MatrixSpan ms = matrix.MatrixData();

            data[0] = (float)ms[0, 0];
            data[1] = (float)ms[1, 0];
            data[2] = (float)ms[2, 0];

            data[3] = (float)ms[0, 1];
            data[4] = (float)ms[1, 1];
            data[5] = (float)ms[2, 1];

            data[6] = (float)ms[0, 2];
            data[7] = (float)ms[1, 2];
            data[8] = (float)ms[2, 2];

            GL.UniformMatrix3fv(location, size, false, data);
        }
        private static void UniformFMat3x2(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                float* matData = stackalloc float[6 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 6;
                    matData[mI] = (float)msV[0, 0];
                    matData[mI + 1] = (float)msV[1, 0];

                    matData[mI + 2] = (float)msV[0, 1];
                    matData[mI + 3] = (float)msV[1, 1];

                    matData[mI + 4] = (float)msV[0, 2];
                    matData[mI + 5] = (float)msV[1, 2];
                }

                GL.UniformMatrix3x2fv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;

            float* data = stackalloc float[6];
            MatrixSpan ms = matrix.MatrixData();

            data[0] = (float)ms[0, 0];
            data[1] = (float)ms[1, 0];

            data[2] = (float)ms[0, 1];
            data[3] = (float)ms[1, 1];

            data[4] = (float)ms[0, 2];
            data[5] = (float)ms[1, 2];

            GL.UniformMatrix3x2fv(location, size, false, data);
        }
        private static void UniformFMat3x4(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                float* matData = stackalloc float[12 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 12;
                    matData[mI] = (float)msV[0, 0];
                    matData[mI + 1] = (float)msV[1, 0];
                    matData[mI + 2] = (float)msV[2, 0];
                    matData[mI + 3] = (float)msV[3, 0];

                    matData[mI + 4] = (float)msV[0, 1];
                    matData[mI + 5] = (float)msV[1, 1];
                    matData[mI + 6] = (float)msV[2, 1];
                    matData[mI + 7] = (float)msV[3, 1];

                    matData[mI + 8] = (float)msV[0, 2];
                    matData[mI + 9] = (float)msV[1, 2];
                    matData[mI + 10] = (float)msV[2, 2];
                    matData[mI + 11] = (float)msV[3, 2];
                }

                GL.UniformMatrix3x4fv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;

            float* data = stackalloc float[12];
            MatrixSpan ms = matrix.MatrixData();

            data[0] = (float)ms[0, 0];
            data[1] = (float)ms[1, 0];
            data[2] = (float)ms[2, 0];
            data[3] = (float)ms[3, 0];

            data[4] = (float)ms[0, 1];
            data[5] = (float)ms[1, 1];
            data[6] = (float)ms[2, 1];
            data[7] = (float)ms[3, 1];

            data[8] = (float)ms[0, 2];
            data[9] = (float)ms[1, 2];
            data[10] = (float)ms[2, 2];
            data[11] = (float)ms[3, 2];

            GL.UniformMatrix3x4fv(location, size, false, data);
        }
        private static void UniformFMat4(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                float* matData = stackalloc float[16 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 16;
                    matData[mI] = (float)msV[0, 0];
                    matData[mI + 1] = (float)msV[1, 0];
                    matData[mI + 2] = (float)msV[2, 0];
                    matData[mI + 3] = (float)msV[3, 0];

                    matData[mI + 4] = (float)msV[0, 1];
                    matData[mI + 5] = (float)msV[1, 1];
                    matData[mI + 6] = (float)msV[2, 1];
                    matData[mI + 7] = (float)msV[3, 1];

                    matData[mI + 8] = (float)msV[0, 2];
                    matData[mI + 9] = (float)msV[1, 2];
                    matData[mI + 10] = (float)msV[2, 2];
                    matData[mI + 11] = (float)msV[3, 2];

                    matData[mI + 12] = (float)msV[0, 3];
                    matData[mI + 13] = (float)msV[1, 3];
                    matData[mI + 14] = (float)msV[2, 3];
                    matData[mI + 15] = (float)msV[3, 3];
                }

                GL.UniformMatrix4fv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;

            float* data = stackalloc float[16];
            MatrixSpan ms = matrix.MatrixData();

            data[0] = (float)ms[0, 0];
            data[1] = (float)ms[1, 0];
            data[2] = (float)ms[2, 0];
            data[3] = (float)ms[3, 0];

            data[4] = (float)ms[0, 1];
            data[5] = (float)ms[1, 1];
            data[6] = (float)ms[2, 1];
            data[7] = (float)ms[3, 1];

            data[8] = (float)ms[0, 2];
            data[9] = (float)ms[1, 2];
            data[10] = (float)ms[2, 2];
            data[11] = (float)ms[3, 2];

            data[12] = (float)ms[0, 3];
            data[13] = (float)ms[1, 3];
            data[14] = (float)ms[2, 3];
            data[15] = (float)ms[3, 3];

            GL.UniformMatrix4fv(location, size, false, data);
        }
        private static void UniformFMat4x2(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                float* matData = stackalloc float[8 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 8;
                    matData[mI] = (float)msV[0, 0];
                    matData[mI + 1] = (float)msV[1, 0];

                    matData[mI + 2] = (float)msV[0, 1];
                    matData[mI + 3] = (float)msV[1, 1];

                    matData[mI + 4] = (float)msV[0, 2];
                    matData[mI + 5] = (float)msV[1, 2];

                    matData[mI + 6] = (float)msV[0, 3];
                    matData[mI + 7] = (float)msV[1, 3];
                }

                GL.UniformMatrix4x2fv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;

            float* data = stackalloc float[8];
            MatrixSpan ms = matrix.MatrixData();

            data[0] = (float)ms[0, 0];
            data[1] = (float)ms[1, 0];

            data[2] = (float)ms[0, 1];
            data[3] = (float)ms[1, 1];

            data[4] = (float)ms[0, 2];
            data[5] = (float)ms[1, 2];

            data[6] = (float)ms[0, 3];
            data[7] = (float)ms[1, 3];

            GL.UniformMatrix4x2fv(location, size, false, data);
        }
        private static void UniformFMat4x3(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                float* matData = stackalloc float[12 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 12;
                    matData[mI] = (float)msV[0, 0];
                    matData[mI + 1] = (float)msV[1, 0];
                    matData[mI + 2] = (float)msV[2, 0];

                    matData[mI + 3] = (float)msV[0, 1];
                    matData[mI + 4] = (float)msV[1, 1];
                    matData[mI + 5] = (float)msV[2, 1];

                    matData[mI + 6] = (float)msV[0, 2];
                    matData[mI + 7] = (float)msV[1, 2];
                    matData[mI + 8] = (float)msV[2, 2];

                    matData[mI + 9] = (float)msV[0, 3];
                    matData[mI + 10] = (float)msV[1, 3];
                    matData[mI + 11] = (float)msV[2, 3];
                }

                GL.UniformMatrix4x3fv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;

            float* data = stackalloc float[12];
            MatrixSpan ms = matrix.MatrixData();

            data[0] = (float)ms[0, 0];
            data[1] = (float)ms[1, 0];
            data[2] = (float)ms[2, 0];

            data[3] = (float)ms[0, 1];
            data[4] = (float)ms[1, 1];
            data[5] = (float)ms[2, 1];

            data[6] = (float)ms[0, 2];
            data[7] = (float)ms[1, 2];
            data[8] = (float)ms[2, 2];

            data[9] = (float)ms[0, 3];
            data[10] = (float)ms[1, 3];
            data[11] = (float)ms[2, 3];

            GL.UniformMatrix4x3fv(location, size, false, data);
        }

        private static void UniformDMat2(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                double* matData = stackalloc double[4 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 4;
                    matData[mI] = msV[0, 0];
                    matData[mI + 1] = msV[1, 0];

                    matData[mI + 2] = msV[0, 1];
                    matData[mI + 3] = msV[1, 1];
                }

                GL.UniformMatrix2dv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;
            MatrixSpan ms = matrix.MatrixData();

            GL.UniformMatrix2dv(location, size, false, ms.Pointer);
        }
        private static void UniformDMat2x3(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                double* matData = stackalloc double[6 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 6;
                    matData[mI] = msV[0, 0];
                    matData[mI + 1] = msV[1, 0];
                    matData[mI + 2] = msV[2, 0];

                    matData[mI + 3] = msV[0, 1];
                    matData[mI + 4] = msV[1, 1];
                    matData[mI + 5] = msV[2, 1];
                }

                GL.UniformMatrix2x3dv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;
            MatrixSpan ms = matrix.MatrixData();

            GL.UniformMatrix2x3dv(location, size, false, ms.Pointer);
        }
        private static void UniformDMat2x4(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                double* matData = stackalloc double[8 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 8;
                    matData[mI] = msV[0, 0];
                    matData[mI + 1] = msV[1, 0];
                    matData[mI + 2] = msV[2, 0];
                    matData[mI + 3] = msV[3, 0];

                    matData[mI + 4] = msV[0, 1];
                    matData[mI + 5] = msV[1, 1];
                    matData[mI + 6] = msV[2, 1];
                    matData[mI + 7] = msV[3, 1];
                }

                GL.UniformMatrix2x4dv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;
            MatrixSpan ms = matrix.MatrixData();

            GL.UniformMatrix2x4dv(location, size, false, ms.Pointer);
        }
        private static void UniformDMat3(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                double* matData = stackalloc double[9 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 9;
                    matData[mI] = msV[0, 0];
                    matData[mI + 1] = msV[1, 0];
                    matData[mI + 2] = msV[2, 0];

                    matData[mI + 3] = msV[0, 1];
                    matData[mI + 4] = msV[1, 1];
                    matData[mI + 5] = msV[2, 1];

                    matData[mI + 6] = msV[0, 2];
                    matData[mI + 7] = msV[1, 2];
                    matData[mI + 8] = msV[2, 2];
                }

                GL.UniformMatrix3dv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;
            MatrixSpan ms = matrix.MatrixData();

            GL.UniformMatrix3dv(location, size, false, ms.Pointer);
        }
        private static void UniformDMat3x2(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                double* matData = stackalloc double[6 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 6;
                    matData[mI] = msV[0, 0];
                    matData[mI + 1] = msV[1, 0];

                    matData[mI + 2] = msV[0, 1];
                    matData[mI + 3] = msV[1, 1];

                    matData[mI + 4] = msV[0, 2];
                    matData[mI + 5] = msV[1, 2];
                }

                GL.UniformMatrix3x2dv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;
            MatrixSpan ms = matrix.MatrixData();

            GL.UniformMatrix3x2dv(location, size, false, ms.Pointer);
        }
        private static void UniformDMat3x4(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                double* matData = stackalloc double[12 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 12;
                    matData[mI] = msV[0, 0];
                    matData[mI + 1] = msV[1, 0];
                    matData[mI + 2] = msV[2, 0];
                    matData[mI + 3] = msV[3, 0];

                    matData[mI + 4] = msV[0, 1];
                    matData[mI + 5] = msV[1, 1];
                    matData[mI + 6] = msV[2, 1];
                    matData[mI + 7] = msV[3, 1];

                    matData[mI + 8] = msV[0, 2];
                    matData[mI + 9] = msV[1, 2];
                    matData[mI + 10] = msV[2, 2];
                    matData[mI + 11] = msV[3, 2];
                }

                GL.UniformMatrix3x4dv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;
            MatrixSpan ms = matrix.MatrixData();

            GL.UniformMatrix3x4dv(location, size, false, ms.Pointer);
        }
        private static void UniformDMat4(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                double* matData = stackalloc double[16 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 16;
                    matData[mI] = msV[0, 0];
                    matData[mI + 1] = msV[1, 0];
                    matData[mI + 2] = msV[2, 0];
                    matData[mI + 3] = msV[3, 0];

                    matData[mI + 4] = msV[0, 1];
                    matData[mI + 5] = msV[1, 1];
                    matData[mI + 6] = msV[2, 1];
                    matData[mI + 7] = msV[3, 1];

                    matData[mI + 8] = msV[0, 2];
                    matData[mI + 9] = msV[1, 2];
                    matData[mI + 10] = msV[2, 2];
                    matData[mI + 11] = msV[3, 2];

                    matData[mI + 12] = msV[0, 3];
                    matData[mI + 13] = msV[1, 3];
                    matData[mI + 14] = msV[2, 3];
                    matData[mI + 15] = msV[3, 3];
                }

                GL.UniformMatrix4dv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;
            MatrixSpan ms = matrix.MatrixData();

            GL.UniformMatrix4dv(location, size, false, ms.Pointer);
        }
        private static void UniformDMat4x2(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                double* matData = stackalloc double[8 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 8;
                    matData[mI] = msV[0, 0];
                    matData[mI + 1] = msV[1, 0];

                    matData[mI + 2] = msV[0, 1];
                    matData[mI + 3] = msV[1, 1];

                    matData[mI + 4] = msV[0, 2];
                    matData[mI + 5] = msV[1, 2];

                    matData[mI + 6] = msV[0, 3];
                    matData[mI + 7] = msV[1, 3];
                }

                GL.UniformMatrix4x2dv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;
            MatrixSpan ms = matrix.MatrixData();

            GL.UniformMatrix4x2dv(location, size, false, ms.Pointer);
        }
        private static void UniformDMat4x3(int location, int size, int index, object value)
        {
            if (size > 1 && index < 1)
            {
                IMatrix[] matrices = (IMatrix[])value;

                double* matData = stackalloc double[12 * size];

                for (int i = 0; i < size; i++)
                {
                    MatrixSpan msV = matrices[i].MatrixData();

                    int mI = i * 12;
                    matData[mI] = msV[0, 0];
                    matData[mI + 1] = msV[1, 0];
                    matData[mI + 2] = msV[2, 0];

                    matData[mI + 3] = msV[0, 1];
                    matData[mI + 4] = msV[1, 1];
                    matData[mI + 5] = msV[2, 1];

                    matData[mI + 6] = msV[0, 2];
                    matData[mI + 7] = msV[1, 2];
                    matData[mI + 8] = msV[2, 2];

                    matData[mI + 9] = msV[0, 3];
                    matData[mI + 10] = msV[1, 3];
                    matData[mI + 11] = msV[2, 3];
                }

                GL.UniformMatrix4x3dv(location, size, false, matData);
                return;
            }

            IMatrix matrix = (IMatrix)value;
            MatrixSpan ms = matrix.MatrixData();

            GL.UniformMatrix4x3dv(location, size, false, ms.Pointer);
        }
    }
}
