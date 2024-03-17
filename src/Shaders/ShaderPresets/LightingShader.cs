using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public sealed class LightingShader : BaseShaderProgram, IBasicShader
    {
        public enum Location : uint
        {
            Positions = ShaderLocation.Vertex,
            ColourAttribute = ShaderLocation.Colour,
            TextureCoords = ShaderLocation.TextureCoords,
            Normal = ShaderLocation.Normal,
            Tangents = ShaderLocation.Tangent,
            NormalMapTextureCoords = ShaderLocation.NormalTexture,
            AmbientLightColur = 6,
            AmbientLightTextureCoords = 7,
            SpecularLightColur = 8,
            SpecularLightTextureCoords = 9
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
                // uMaterial
                "uMaterial.DiffuseLightSource", "uMaterial.DiffuseLight",
                "uMaterial.DiffTextureSlot", "uMaterial.SpecularLightSource",
                "uMaterial.SpecularLight", "uMaterial.SpecTextureSlot",
                "uMaterial.Shine");

            LightNumber = lightNumber;

            _m2m3 = Matrix.Identity * Matrix.Identity;

            _uLight = Properties.IndexFromLocation(12);
            _uSpotLight = Properties.IndexFromLocation(_uSpotLight);

            int[] lightOffsets = Properties.FindStructOffsets(_uLight, "LightColour", "AmbientLight", "LightVector", "Linear", "Quadratic");
            _lightColourOffset = lightOffsets[0];
            _lightAmbientOffset = lightOffsets[1];
            _lightPositionOffset = lightOffsets[2];
            _lightLinearOffset = lightOffsets[3];
            _lightQuadraticOffset = lightOffsets[4];

            lightOffsets = Properties.FindStructOffsets(_uSpotLight, "Colour", "Position", "Direction", "CosInner", "CosOuter", "Linear", "Quadratic");
            _spotLightColourOffset = lightOffsets[0];
            _spotLightPositionOffset = lightOffsets[1];
            _spotLightDirectionOffset = lightOffsets[2];
            _spotLightInnerOffset = lightOffsets[3];
            _spotLightOuterOffset = lightOffsets[4];
            _spotLightLinearOffset = lightOffsets[5];
            _spotLightQuadraticOffset = lightOffsets[6];

            SetUniform(Uniforms[6], 0);
            SetUniform(Uniforms[7], 1);
            SetUniform(Uniforms[12], 2);
        }

        private readonly int _lightColourOffset;
        private readonly int _lightAmbientOffset;
        private readonly int _lightPositionOffset;
        private readonly int _lightLinearOffset;
        private readonly int _lightQuadraticOffset;
        private readonly int _spotLightColourOffset;
        private readonly int _spotLightPositionOffset;
        private readonly int _spotLightDirectionOffset;
        private readonly int _spotLightInnerOffset;
        private readonly int _spotLightOuterOffset;
        private readonly int _spotLightLinearOffset;
        private readonly int _spotLightQuadraticOffset;

        public int LightNumber { get; }

        private ColourSource _source = 0;
        public ColourSource ColourSource
        {
            get => _source;
            set
            {
                _source = value;

                SetUniform(Uniforms[0], (int)value);
            }
        }

        private ColourF _colour = ColourF.Zero;
        public ColourF Colour
        {
            get => _colour;
            set
            {
                _colour = value;

                SetUniform(Uniforms[1], (Vector4)value);
            }
        }

        private ColourF _ambientLight = ColourF.Zero;
        public ColourF AmbientLight
        {
            get => _ambientLight;
            set
            {
                _ambientLight = value;

                SetUniform(Uniforms[2], (Vector3)(ColourF3)value);
            }
        }

        private readonly int _uLight;
        public void SetLight(int index, Light light)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }
            
            SetUniform(_uLight, index, light);
            /*
            int uIndex = (index * 5) + _uLight;

            SetUniformF(uIndex, (Vector3)light.LightColour);

            SetUniformF(uIndex + 1, (Vector3)light.AmbientLight);

            SetUniformF(uIndex + 2, light.LightVector);

            if (light.LightVector.W > 0)
            {
                SetUniformF(uIndex + 3, light.Linear);
                SetUniformF(uIndex + 4, light.Quadratic);
            }*/
        }

        public void SetLightColour(int index, ColourF lightColour)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            SetUniform(_uLight + (index * 5) + _lightColourOffset, (Vector3)(ColourF3)lightColour);
        }

        public void SetLightAmbient(int index, ColourF ambientColour)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            SetUniform(_uLight + (index * 5) + _lightAmbientOffset, (Vector3)(ColourF3)ambientColour);
        }

        public void SetLightPosition(int index, Vector4 position)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            SetUniform(_uLight + (index * 5) + _lightPositionOffset, position);
        }

        public void SetLightDistance(int index, double linear, double quadratic)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            int uIndex = (index * 5) + _uLight;

            SetUniform(uIndex + _lightLinearOffset, linear);
            SetUniform(uIndex + _lightQuadraticOffset, quadratic);
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

            SetUniform(_uSpotLight + (index * 7) + _spotLightColourOffset, (Vector3)(ColourF3)lightColour);
        }

        public unsafe ColourF GetSpotLightColour(int index)
        {
            ColourF c;

            int location = Properties[(index * 7) + _uSpotLight + _spotLightColourOffset].Location;
            GL.GetnUniformfv(Id, location, 3 * sizeof(float), (float*)&c);

            return c;
        }

        public void SetSpotLightPosition(int index, Vector3 position)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            SetUniform(_uSpotLight + (index * 7) + _spotLightPositionOffset, position);
        }

        public void SetSpotLightDirection(int index, Vector3 direction)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            SetUniform(_uSpotLight + (index * 7) + _spotLightDirectionOffset, direction);
        }

        public void SetSpotLightAngle(int index, Radian innerAngle, Radian outerAngle)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            int uIndex = (index * 7) + _uSpotLight;

            SetUniform(uIndex + _spotLightInnerOffset, Math.Cos(innerAngle));
            SetUniform(uIndex + _spotLightOuterOffset, Math.Cos(outerAngle));
        }

        public void SetSpotLightDistance(int index, double linear, double quadratic)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            int uIndex = (index * 7) + _uSpotLight;

            SetUniform(uIndex + _spotLightLinearOffset, linear);
            SetUniform(uIndex + _spotLightQuadraticOffset, quadratic);
        }

        private Vector3 _camPos = Vector3.Zero;
        public Vector3 CameraPosition
        {
            get => _camPos;
            set
            {
                _camPos = value;

                SetUniform(Uniforms[3], value);
            }
        }

        private bool _drawlight = false;
        public bool DrawLighting
        {
            get => _drawlight;
            set
            {
                _drawlight = value;

                SetUniform(Uniforms[4], value);
            }
        }

        private bool _ingorBlackLight = false;
        public bool IngorBlackLight
        {
            get => _ingorBlackLight;
            set
            {
                _ingorBlackLight = value;

                SetUniform(Uniforms[5], value);
            }
        }

        public void SetMaterial(Material material)
        {
            //SetUniform(Uniforms[13], material);

            SetUniform(Uniforms[19], (int)material.Shine);
            SetUniform(Uniforms[13], material.DiffuseLightSource);

            SetUniform(Uniforms[14], (Vector3)material.DiffuseLight);
            SetUniform(Uniforms[15], material.DiffTextureSlot);
            SetUniform(Uniforms[16], material.SpecularLightSource);

            SetUniform(Uniforms[17], (Vector3)material.SpecularLight);
            SetUniform(Uniforms[18], material.SpecTextureSlot);
        }

        public ITexture Texture { get; set; }
        public ITexture NormalMap { get; set; }
        public ITexture ShadowMap { get; set; }

        private bool _normalMapping = false;
        public bool NormalMapping
        {
            get => _normalMapping;
            set
            {
                _normalMapping = value;

                SetUniform(Uniforms[8], value);
            }
        }

        public override IMatrix Matrix1 { get; set; } = Matrix.Identity;
        public override IMatrix Matrix2
        {
            get => _m2m3.Left;
            set => _m2m3.Left = value;
        }
        public override IMatrix Matrix3
        {
            get => _m2m3.Right;
            set => _m2m3.Right = value;
        }

        private readonly MultiplyMatrix _m2m3;

        public override void PrepareDraw()
        {
            SetUniform(Uniforms[9], Matrix1);
            SetUniform(Uniforms[10], _m2m3);

            Texture?.Bind(0);
            NormalMap?.Bind(1);
            ShadowMap?.Bind(2);

            if (LightSpaceMatrix is null) { return; }
            SetUniform(Uniforms[11], LightSpaceMatrix);
        }

        public IMatrix LightSpaceMatrix { get; set; }
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
            UniformType.Int,
            UniformType.FVec3,
            UniformType.Int,

            UniformType.Int,
            UniformType.FVec3,
            UniformType.Int,

            UniformType.Int
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
            UniformType.FVec3,
            UniformType.FVec3,
            new IUniformStruct.Member(UniformType.DVec4, true),

            new IUniformStruct.Member(UniformType.Double, true),
            new IUniformStruct.Member(UniformType.Double, true)
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
            UniformType.FVec3,
            new IUniformStruct.Member(UniformType.DVec3, true),
            new IUniformStruct.Member(UniformType.DVec3, true),

            new IUniformStruct.Member(UniformType.Double, true),
            new IUniformStruct.Member(UniformType.Double, true),
            new IUniformStruct.Member(UniformType.Double, true),
            new IUniformStruct.Member(UniformType.Double, true)
        };
        public IUniformStruct.Member[] Members() => _members;
    }
}
