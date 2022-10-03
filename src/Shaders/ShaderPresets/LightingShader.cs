using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public class LightingShader : BaseShaderProgram, IMatrixShader
    {
        public enum Location : uint
        {
            Positions = 0,
            ColourAttribute = 1,
            TextureCoords = 2,
            Normal = 3,
            AmbientLightColur = 4,
            AmbientLightTextureCoords = 5,
            SpecularLightColur = 6,
            SpecularLightTextureCoords = 7,
            NormalMapTextureCoords = 8,
            Tangents = 9
        }

        public LightingShader(int lightNumber, int spotLightNumber)
        {
            string fSource = ShaderPresets.LightingFragment;
            fSource = fSource.Replace("##dp##", $"{lightNumber}");

            _uSpotLight = (lightNumber * 5) + 12;
            fSource = fSource.Replace("##sl##", $"{_uSpotLight}");
            fSource = fSource.Replace("##s##", $"{spotLightNumber}");

            Create(ShaderPresets.LightingVertex, fSource,
                "colourType", "uColour", "ambientLight", "cameraPos",
                "drawLight", "ingorBlackLight", "uTextureSlot", "uNormalMap",
                "normalMapping", "modelM", "vpM", "lightSpaceMatrix",
                "uShadowMapSlot",
                // Start of uMaterial
                "uMaterial.DiffuseLightSource");

            LightNumber = lightNumber;

            Matrix1 = Matrix4.Identity;
            Matrix2 = Matrix4.Identity;
            Matrix3 = Matrix4.Identity;
        }

        public int LightNumber { get; }

        private ColourSource _source = 0;
        public ColourSource ColourSource
        {
            get => _source;
            set
            {
                _source = value;

                SetUniformI(Uniforms[0], (int)value);
            }
        }

        private ColourF _colour = ColourF.Zero;
        public ColourF Colour
        {
            get => _colour;
            set
            {
                _colour = value;

                SetUniformF(Uniforms[1], (Vector4)value);
            }
        }

        private ColourF _ambientLight = ColourF.Zero;
        public ColourF AmbientLight
        {
            get => _ambientLight;
            set
            {
                _ambientLight = value;

                SetUniformF(Uniforms[2], (Vector3)(ColourF3)value);
            }
        }

        private readonly int _uLight = 12;
        public void SetLight(int index, Light light)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            //SetUniform(_uLight, index, light);

            int uIndex = (index * 5) + _uLight;

            SetUniformF(uIndex, (Vector3)light.LightColour);

            SetUniformF(uIndex + 1, (Vector3)light.AmbientLight);

            SetUniformF(uIndex + 2, light.LightVector);

            if (light.LightVector.W > 0)
            {
                SetUniformF(uIndex + 3, light.Linear);
                SetUniformF(uIndex + 4, light.Quadratic);
            }
        }

        public void SetLightColour(int index, ColourF lightColour)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            SetUniformF(_uLight + (index * 5), (Vector3)(ColourF3)lightColour);
        }

        public void SetLightAmbient(int index, ColourF ambientColour)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            SetUniformF(_uLight + (index * 5) + 1, (Vector3)(ColourF3)ambientColour);
        }

        public void SetLightPosition(int index, Vector4 position)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            SetUniformF(_uLight + (index * 5) + 2, position);
        }

        public void SetLightDistance(int index, double linear, double quadratic)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            int uIndex = (index * 5) + _uLight;

            SetUniformF(uIndex + 3, linear);
            SetUniformF(uIndex + 4, quadratic);
        }

        private readonly int _uSpotLight;

        public void SetSpotLight(int index, SpotLight light)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            SetUniform(_uSpotLight, index, light);
        }

        public void SetSpotLightColour(int index, ColourF lightColour)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            SetUniformF(_uSpotLight + (index * 7), (Vector3)(ColourF3)lightColour);
        }

        public unsafe ColourF GetSpotLightColour(int index)
        {
            ColourF c;

            GL.GetnUniformfv(Id, (index * 7) + _uSpotLight, 3 * sizeof(float), (float*)&c);

            return c;
        }

        public void SetSpotLightPosition(int index, Vector3 position)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            SetUniformF(_uSpotLight + (index * 7) + 1, position);
        }

        public void SetSpotLightDirection(int index, Vector3 direction)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            SetUniformF(_uSpotLight + (index * 7) + 2, direction);
        }

        public void SetSpotLightAngle(int index, Radian innerAngle, Radian outerAngle)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            int uIndex = (index * 7) + _uSpotLight;

            SetUniformF(uIndex + 3, Math.Cos(innerAngle));
            SetUniformF(uIndex + 4, Math.Cos(outerAngle));
        }

        public void SetSpotLightDistance(int index, double linear, double quadratic)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            int uIndex = (index * 7) + _uSpotLight;

            SetUniformF(uIndex + 5, linear);
            SetUniformF(uIndex + 6, quadratic);
        }

        private Vector3 _camPos = Vector3.Zero;
        public Vector3 CameraPosition
        {
            get => _camPos;
            set
            {
                _camPos = value;

                SetUniformF(Uniforms[3], value);
            }
        }

        private bool _drawlight = false;
        public bool DrawLighting
        {
            get => _drawlight;
            set
            {
                _drawlight = value;

                int set = 0;
                if (value) { set = 1; }

                SetUniformI(Uniforms[4], set);
            }
        }

        private bool _ingorBlackLight = false;
        public bool IngorBlackLight
        {
            get => _ingorBlackLight;
            set
            {
                _ingorBlackLight = value;

                int set = 0;
                if (value) { set = 1; }

                SetUniformI(Uniforms[5], set);
            }
        }

        public void SetMaterial(Material material)
        {
            SetUniform(Uniforms[13], material);
        }

        private int _texSlot = 0;
        public int TextureSlot
        {
            get => _texSlot;
            set
            {
                _texSlot = value;

                SetUniformI(Uniforms[6], value);
            }
        }

        private int _normalMapSlot = 0;
        public int NormalMapSlot
        {
            get => _normalMapSlot;
            set
            {
                _normalMapSlot = value;

                SetUniformI(Uniforms[7], value);
            }
        }

        private int _shadowMapSlot = 0;
        public int ShadowMapSlot
        {
            get => _shadowMapSlot;
            set
            {
                _shadowMapSlot = value;

                SetUniformI(Uniforms[12], value);
            }
        }

        private bool _normalMapping = false;
        public bool NormalMapping
        {
            get => _normalMapping;
            set
            {
                _normalMapping = value;

                int set = 0;
                if (value) { set = 1; }

                SetUniformI(Uniforms[8], set);
            }
        }

        private Matrix4 _m1 = Matrix4.Identity;
        public Matrix4 Matrix1
        {
            get => _m1;
            set
            {
                _m1 = value;

                SetUniformF(Uniforms[9], ref value);
            }
        }
        private Matrix4 _m2 = Matrix4.Identity;
        public Matrix4 Matrix2
        {
            get => _m2;
            set
            {
                _m2 = value;
                SetMatrices();
            }
        }
        private Matrix4 _m3 = Matrix4.Identity;
        public Matrix4 Matrix3
        {
            get => _m3;
            set
            {
                _m3 = value;
                SetMatrices();
            }
        }

        private void SetMatrices()
        {
            Matrix4 matrix = _m2 * _m3;
            SetUniformF(Uniforms[10], ref matrix);
        }

        private Matrix4 _lsm = Matrix4.Identity;
        public Matrix4 LightSpaceMatrix
        {
            get => _lsm;
            set
            {
                _lsm = value;

                SetUniformF(Uniforms[11], ref value);
            }
        }
    }

    public enum Shine
    {
        None = 2,
        XS = 4,
        S = 8,
        M = 16,
        L = 32,
        XL = 64,
        XXL = 128,
        XXXL = 256
    }

    public struct Material : IUniformStruct
    {
        public enum Source
        {
            Attribute = 2,
            None = 4,
            Default = 0
        }

        public Material(ColourF3 diffuseLight, ColourF3 specularLight, Shine shine)
        {
            DiffuseLightSource = 1;
            DiffuseLight = diffuseLight;
            DiffTextureSlot = 0;

            SpecularLightSource = 1;
            SpecularLight = specularLight;
            SpecTextureSlot = 0;

            Shine = shine;
        }

        public Material(ColourF3 diffuseLight, int specTextureSlot, Shine shine)
        {
            DiffuseLightSource = 1;
            DiffuseLight = diffuseLight;
            DiffTextureSlot = 0;

            SpecularLightSource = 3;
            SpecularLight = ColourF3.Zero;
            SpecTextureSlot = specTextureSlot;

            Shine = shine;
        }

        public Material(int diffTextureSlot, ColourF3 specularLight, Shine shine)
        {
            DiffuseLightSource = 3;
            DiffuseLight = ColourF3.Zero;
            DiffTextureSlot = diffTextureSlot;

            SpecularLightSource = 1;
            SpecularLight = specularLight;
            SpecTextureSlot = 0;

            Shine = shine;
        }

        public Material(int diffTextureSlot, int specTextureSlot, Shine shine)
        {
            DiffuseLightSource = 3;
            DiffuseLight = ColourF3.Zero;
            DiffTextureSlot = diffTextureSlot;

            SpecularLightSource = 3;
            SpecularLight = ColourF3.Zero;
            SpecTextureSlot = specTextureSlot;

            Shine = shine;
        }

        public Material(int diffTextureSlot, Source specSource, Shine shine)
        {
            DiffuseLightSource = 3;
            DiffuseLight = ColourF3.Zero;
            DiffTextureSlot = diffTextureSlot;

            SpecularLightSource = (int)specSource;
            SpecularLight = ColourF3.Zero;
            SpecTextureSlot = 0;

            Shine = shine;
        }

        public Material(ColourF3 diffuseLight, Source specSource, Shine shine)
        {
            DiffuseLightSource = 1;
            DiffuseLight = diffuseLight;
            DiffTextureSlot = 0;

            SpecularLightSource = (int)specSource;
            SpecularLight = ColourF3.Zero;
            SpecTextureSlot = 0;

            Shine = shine;
        }

        public Material(Source diffSource, ColourF3 specularLight, Shine shine)
        {
            DiffuseLightSource = (int)diffSource;
            DiffuseLight = ColourF3.Zero;
            DiffTextureSlot = 0;

            SpecularLightSource = 1;
            SpecularLight = specularLight;
            SpecTextureSlot = 0;

            Shine = shine;
        }

        public Material(Source diffSource, int specTextureSlot, Shine shine)
        {
            DiffuseLightSource = (int)diffSource;
            DiffuseLight = ColourF3.Zero;
            DiffTextureSlot = 0;

            SpecularLightSource = 3;
            SpecularLight = ColourF3.Zero;
            SpecTextureSlot = specTextureSlot;

            Shine = shine;
        }

        public Material(Source diffSource, Source specSource, Shine shine)
        {
            DiffuseLightSource = (int)diffSource;
            DiffuseLight = ColourF3.Zero;
            DiffTextureSlot = 0;

            SpecularLightSource = (int)specSource;
            SpecularLight = ColourF3.Zero;
            SpecTextureSlot = 0;

            Shine = shine;
        }

        public int DiffuseLightSource { get; set; }
        public ColourF3 DiffuseLight { get; set; }
        public int DiffTextureSlot { get; set; }

        public int SpecularLightSource { get; set; }
        public ColourF3 SpecularLight { get; set; }
        public int SpecTextureSlot { get; set; }

        public Shine Shine { get; set; }

        private static readonly IUniformStruct.Member[] _members = new IUniformStruct.Member[]
        {
            IUniformStruct.UniformType.Int,
            IUniformStruct.UniformType.FVec3,
            IUniformStruct.UniformType.Int,

            IUniformStruct.UniformType.Int,
            IUniformStruct.UniformType.FVec3,
            IUniformStruct.UniformType.Int,

            IUniformStruct.UniformType.Int
        };
        public IUniformStruct.Member[] Members() => _members;
    }

    public struct Light : IUniformStruct
    {
        public Light(ColourF3 colour, ColourF3 ambientColour, double linear, double quadratic, Vector3 point, bool direction = false)
        {
            LightColour = colour;
            AmbientLight = ambientColour;
            LightVector = (point, direction ? 0 : 1);
            Linear = linear;
            Quadratic = quadratic;
        }

        public ColourF3 LightColour { get; set; }
        public ColourF3 AmbientLight { get; set; }
        public Vector4 LightVector { get; set; }

        public double Linear { get; set; }
        public double Quadratic { get; set; }

        private static readonly IUniformStruct.Member[] _members = new IUniformStruct.Member[]
        {
            IUniformStruct.UniformType.FVec3,
            IUniformStruct.UniformType.FVec3,
            new IUniformStruct.Member(IUniformStruct.UniformType.DVec4, true),

            new IUniformStruct.Member(IUniformStruct.UniformType.Double, true),
            new IUniformStruct.Member(IUniformStruct.UniformType.Double, true)
        };
        public IUniformStruct.Member[] Members() => _members;
    }

    public struct SpotLight : IUniformStruct
    {
        public SpotLight(ColourF3 colour, Radian angle, Radian outerAngle, double linear, double quadratic, Vector3 point, Vector3 direction)
        {
            LightColour = colour;
            LightVector = point;
            Direction = direction;

            CosAngle = Math.Cos(angle);
            CosOuterAngle = Math.Cos(outerAngle);
            Linear = linear;
            Quadratic = quadratic;
        }

        public ColourF3 LightColour { get; set; }
        public Vector3 LightVector { get; set; }
        public Vector3 Direction { get; set; }

        public double CosAngle { get; set; }
        public double CosOuterAngle { get; set; }
        public double Linear { get; set; }
        public double Quadratic { get; set; }

        private static readonly IUniformStruct.Member[] _members = new IUniformStruct.Member[]
        {
            IUniformStruct.UniformType.FVec3,
            new IUniformStruct.Member(IUniformStruct.UniformType.DVec3, true),
            new IUniformStruct.Member(IUniformStruct.UniformType.DVec3, true),

            new IUniformStruct.Member(IUniformStruct.UniformType.Double, true),
            new IUniformStruct.Member(IUniformStruct.UniformType.Double, true),
            new IUniformStruct.Member(IUniformStruct.UniformType.Double, true),
            new IUniformStruct.Member(IUniformStruct.UniformType.Double, true)
        };
        public IUniformStruct.Member[] Members() => _members;
    }
}
