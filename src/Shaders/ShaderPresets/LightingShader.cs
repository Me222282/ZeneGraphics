using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics.Shaders
{
    public class LightingShader : IMvpShader
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

            Program = CustomShader.CreateShader(ShaderPresets.LightingVertex, fSource);

            LightNumber = lightNumber;

            FindUniforms();

            SetModelMatrix(Matrix4.Identity);
            SetViewMatrix(Matrix4.Identity);
            SetProjectionMatrix(Matrix4.Identity);
        }

        public uint Program { get; private set; }
        uint IIdentifiable.Id => Program;

        private bool _Bound;
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

        public int LightNumber { get; }

        private int _uniformColourType;

        public void SetColourSource(ColourSource type)
        {
            GL.ProgramUniform1i(Program, _uniformColourType, (int)type);
        }

        private int _uniformColour;

        public void SetDrawColour(float r, float g, float b, float a)
        {
            GL.ProgramUniform4f(Program, _uniformColour,
                r,
                g,
                b,
                a);
        }

        public void SetDrawColour(float r, float g, float b)
        {
            GL.ProgramUniform4f(Program, _uniformColour,
                r,
                g,
                b,
                1f);
        }

        public void SetDrawColour(Colour colour)
        {
            ColourF c = (ColourF)colour;

            SetDrawColour(c.R, c.G, c.B, c.A);
        }

        public void SetDrawColour(ColourF colour)
        {
            SetDrawColour(colour.R, colour.G, colour.B, colour.A);
        }

        private int _uniformAmbientLight;

        public void SetAmbientLight(Colour colour)
        {
            ColourF c = (ColourF)colour;

            GL.ProgramUniform3f(Program, _uniformAmbientLight, c.R, c.G, c.B);
        }

        private int _uLight;

        public void SetLight(int index, Light light)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            int uIndex = (index * 5) + _uLight;

            ColourF lc = light.LightColour;

            GL.ProgramUniform3f(Program, uIndex, lc.R, lc.G, lc.B);

            ColourF ac = light.AmbientLight;

            GL.ProgramUniform3f(Program, uIndex + 1, ac.R, ac.G, ac.B);

            float w = 1;
            if (light.Direction) { w = 0; }

            GL.ProgramUniform4f(Program, uIndex + 2, (float)light.LightVector.X, 
                (float)light.LightVector.Y, (float)light.LightVector.Z, w);

            if (!light.Direction)
            {
                GL.ProgramUniform1f(Program, uIndex + 3, (float)light.Linear);
                GL.ProgramUniform1f(Program, uIndex + 4, (float)light.Quadratic);
            }
        }

        public void SetLightColour(int index, ColourF lightColour)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            ColourF c = lightColour;

            GL.ProgramUniform3f(Program, (index * 5) + _uLight, c.R, c.G, c.B);
        }

        public void SetLightAmbient(int index, ColourF ambientColour)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            ColourF c = ambientColour;

            GL.ProgramUniform3f(Program, (index * 5) + _uLight + 1, c.R, c.G, c.B);
        }

        public void SetLightPosition(int index, Vector3 position, bool direction = false)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            float w = 1;
            if (direction) { w = 0; }

            GL.ProgramUniform4f(Program, (index * 5) + _uLight + 2, (float)position.X,
                (float)position.Y, (float)position.Z, w);
        }

        public void SetLightDistance(int index, double linear, double quadratic)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            int uIndex = (index * 5) + _uLight;

            GL.ProgramUniform1f(Program, uIndex + 3, (float)linear);
            GL.ProgramUniform1f(Program, uIndex + 4, (float)quadratic);
        }

        private readonly int _uSpotLight;

        public void SetSpotLight(int index, SpotLight light)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            int uIndex = (index * 7) + _uSpotLight;

            ColourF lc = light.LightColour;

            GL.ProgramUniform3f(Program, uIndex, lc.R, lc.G, lc.B);

            GL.ProgramUniform3f(Program, uIndex + 1, (float)light.LightVector.X,
                (float)light.LightVector.Y, (float)light.LightVector.Z);

            GL.ProgramUniform3f(Program, uIndex + 2, (float)light.Direction.X,
                (float)light.Direction.Y, (float)light.Direction.Z);

            GL.ProgramUniform1f(Program, uIndex + 3, (float)light.CosAngle);
            GL.ProgramUniform1f(Program, uIndex + 4, (float)light.CosOuterAngle);
            GL.ProgramUniform1f(Program, uIndex + 5, (float)light.Linear);
            GL.ProgramUniform1f(Program, uIndex + 6, (float)light.Quadratic);
        }

        public void SetSpotLightColour(int index, ColourF lightColour)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            ColourF c = lightColour;

            GL.ProgramUniform3f(Program, (index * 7) + _uSpotLight, c.R, c.G, c.B);
        }

        public unsafe ColourF GetSpotLightColour(int index)
        {
            ColourF c;

            GL.GetnUniformfv(Program, (index * 7) + _uSpotLight, 3 * sizeof(float), (float*)&c);

            return c;
        }

        public void SetSpotLightPosition(int index, Vector3 position)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            GL.ProgramUniform3f(Program, (index * 7) + _uSpotLight + 1, (float)position.X,
                (float)position.Y, (float)position.Z);
        }

        public void SetSpotLightDirection(int index, Vector3 direction)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            GL.ProgramUniform3f(Program, (index * 7) + _uSpotLight + 2, (float)direction.X,
                (float)direction.Y, (float)direction.Z);
        }

        public void SetSpotLightAngle(int index, Radian innerAngle, Radian outerAngle)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            int uIndex = (index * 7) + _uSpotLight;

            GL.ProgramUniform1f(Program, uIndex + 3, (float)Math.Cos(innerAngle));
            GL.ProgramUniform1f(Program, uIndex + 4, (float)Math.Cos(outerAngle));
        }

        public void SetSpotLightDistance(int index, double linear, double quadratic)
        {
            if (index >= LightNumber) { throw new IndexOutOfRangeException(); }

            int uIndex = (index * 7) + _uSpotLight;

            GL.ProgramUniform1f(Program, uIndex + 5, (float)linear);
            GL.ProgramUniform1f(Program, uIndex + 6, (float)quadratic);
        }

        private int _uniformCameraPos;

        public void SetCameraPosition(Vector3 position)
        {
            GL.ProgramUniform3f(Program, _uniformCameraPos, (float)position.X, (float)position.Y, (float)position.Z);
        }

        private int _uniformDrawLight;

        public void DrawLighting(bool value)
        {
            int set = 0;

            if (value) { set = 1; }

            GL.ProgramUniform1i(Program, _uniformDrawLight, set);
        }

        private int _uniformIngorBL;

        public void IngorBlackLight(bool value)
        {
            int set = 0;

            if (value) { set = 1; }

            GL.ProgramUniform1i(Program, _uniformIngorBL, set);
        }

        private UniformMaterial _uMaterial = new UniformMaterial();

        public void SetMaterial(Material material)
        {
            GL.ProgramUniform1i(Program, _uMaterial.Shine, (int)material.Shine);
            GL.ProgramUniform1i(Program, _uMaterial.DiffuseLightSource, material.DiffuseLightSource);

            ColourF dl = material.DiffuseLight;
            GL.ProgramUniform3f(Program, _uMaterial.DiffuseLight, dl.R, dl.G, dl.B);
            GL.ProgramUniform1i(Program, _uMaterial.DiffTextureSlot, material.DiffTextureSlot);
            GL.ProgramUniform1i(Program, _uMaterial.SpecularLightSource, material.SpecularLightSource);

            ColourF sl = material.SpecularLight;
            GL.ProgramUniform3f(Program, _uMaterial.SpecularLight, sl.R, sl.G, sl.B);
            GL.ProgramUniform1i(Program, _uMaterial.SpecTextureSlot, material.SpecTextureSlot);
        }

        private int _uniformTexture;

        public void SetTextureSlot(int slot)
        {
            GL.ProgramUniform1i(Program, _uniformTexture, slot);
        }

        private int _uniformNormalSlot;

        public void SetNormalMapSlot(int slot)
        {
            GL.ProgramUniform1i(Program, _uniformNormalSlot, slot);
        }

        private int _uniformShadowMapSlot;

        public void SetShadowMapSlot(int slot)
        {
            GL.ProgramUniform1i(Program, _uniformShadowMapSlot, slot);
        }

        private int _uniformDoNormMap;

        public void UseNormalMapping(bool value)
        {
            int b = 0;
            if (value) { b = 1; }

            GL.ProgramUniform1i(Program, _uniformDoNormMap, b);
        }

        private int _uniformMatrixM;
        private int _uniformMatrixV;
        private int _uniformMatrixP;

        public void SetModelMatrix(Matrix4 matrix)
        {
            GL.ProgramUniformMatrix4fv(Program, _uniformMatrixM, false, matrix.GetGLData());
        }

        public void SetViewMatrix(Matrix4 matrix)
        {
            GL.ProgramUniformMatrix4fv(Program, _uniformMatrixV, false, matrix.GetGLData());
        }

        public void SetProjectionMatrix(Matrix4 matrix)
        {
            GL.ProgramUniformMatrix4fv(Program, _uniformMatrixP, false, matrix.GetGLData());
        }

        private int _uniformMatrixLS;

        public void SetLightSpaceMatrix(Matrix4 matrix)
        {
            GL.ProgramUniformMatrix4fv(Program, _uniformMatrixLS, false, matrix.GetGLData());
        }

        private void FindUniforms()
        {
            _uniformColourType = GL.GetUniformLocation(Program, "colourType");

            _uniformColour = GL.GetUniformLocation(Program, "uColour");

            _uLight = 12;
            _uniformAmbientLight = GL.GetUniformLocation(Program, "ambientLight");

            _uniformCameraPos = GL.GetUniformLocation(Program, "cameraPos");

            _uniformDrawLight = GL.GetUniformLocation(Program, "drawLight");
            _uniformIngorBL = GL.GetUniformLocation(Program, "ingorBlackLight");

            _uniformTexture = GL.GetUniformLocation(Program, "uTextureSlot");
            _uniformNormalSlot = GL.GetUniformLocation(Program, "uNormalMap");
            _uniformDoNormMap = GL.GetUniformLocation(Program, "normalMapping");

            _uniformMatrixM = GL.GetUniformLocation(Program, "model");
            _uniformMatrixV = GL.GetUniformLocation(Program, "view");
            _uniformMatrixP = GL.GetUniformLocation(Program, "projection");

            _uniformMatrixLS = GL.GetUniformLocation(Program, "lightSpaceMatrix");

            _uMaterial.DiffuseLightSource = GL.GetUniformLocation(Program, "uMaterial.DiffuseLightSource");
            _uMaterial.DiffuseLight = GL.GetUniformLocation(Program, "uMaterial.DiffuseLight");
            _uMaterial.DiffTextureSlot = GL.GetUniformLocation(Program, "uMaterial.DiffTextureSlot");
            _uMaterial.SpecularLightSource = GL.GetUniformLocation(Program, "uMaterial.SpecularLightSource");
            _uMaterial.SpecularLight = GL.GetUniformLocation(Program, "uMaterial.SpecularLight");
            _uMaterial.SpecTextureSlot = GL.GetUniformLocation(Program, "uMaterial.SpecTextureSlot");
            _uMaterial.Shine = GL.GetUniformLocation(Program, "uMaterial.Shine");

            _uniformShadowMapSlot = GL.GetUniformLocation(Program, "uShadowMapSlot");
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if (!_disposed)
            {
                GL.DeleteProgram(Program);
                _disposed = true;

                GC.SuppressFinalize(this);
            }
        }

        public void Bind()
        {
            GL.UseProgram(this);

            _Bound = true;
        }

        public void Unbind()
        {
            GL.UseProgram(null);

            _Bound = false;
        }

        private struct UniformMaterial
        {
            public int DiffuseLightSource;
            public int DiffuseLight;
            public int DiffTextureSlot;
            public int SpecularLightSource;
            public int SpecularLight;
            public int SpecTextureSlot;
            public int Shine;
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

    public struct Material
    {
        public enum Source
        {
            Attribute = 2,
            None = 4,
            Default = 0
        }

        public Material(ColourF diffuseLight, ColourF specularLight, Shine shine)
        {
            DiffuseLightSource = 1;
            DiffuseLight = diffuseLight;
            DiffTextureSlot = 0;

            SpecularLightSource = 1;
            SpecularLight = specularLight;
            SpecTextureSlot = 0;

            Shine = shine;
        }

        public Material(ColourF diffuseLight, int specTextureSlot, Shine shine)
        {
            DiffuseLightSource = 1;
            DiffuseLight = diffuseLight;
            DiffTextureSlot = 0;

            SpecularLightSource = 3;
            SpecularLight = ColourF.Zero;
            SpecTextureSlot = specTextureSlot;

            Shine = shine;
        }

        public Material(int diffTextureSlot, ColourF specularLight, Shine shine)
        {
            DiffuseLightSource = 3;
            DiffuseLight = ColourF.Zero;
            DiffTextureSlot = diffTextureSlot;

            SpecularLightSource = 1;
            SpecularLight = specularLight;
            SpecTextureSlot = 0;

            Shine = shine;
        }

        public Material(int diffTextureSlot, int specTextureSlot, Shine shine)
        {
            DiffuseLightSource = 3;
            DiffuseLight = ColourF.Zero;
            DiffTextureSlot = diffTextureSlot;

            SpecularLightSource = 3;
            SpecularLight = ColourF.Zero;
            SpecTextureSlot = specTextureSlot;

            Shine = shine;
        }

        public Material(int diffTextureSlot, Source specSource, Shine shine)
        {
            DiffuseLightSource = 3;
            DiffuseLight = ColourF.Zero;
            DiffTextureSlot = diffTextureSlot;

            SpecularLightSource = (int)specSource;
            SpecularLight = ColourF.Zero;
            SpecTextureSlot = 0;

            Shine = shine;
        }

        public Material(ColourF diffuseLight, Source specSource, Shine shine)
        {
            DiffuseLightSource = 1;
            DiffuseLight = diffuseLight;
            DiffTextureSlot = 0;

            SpecularLightSource = (int)specSource;
            SpecularLight = ColourF.Zero;
            SpecTextureSlot = 0;

            Shine = shine;
        }

        public Material(Source diffSource, ColourF specularLight, Shine shine)
        {
            DiffuseLightSource = (int)diffSource;
            DiffuseLight = ColourF.Zero;
            DiffTextureSlot = 0;

            SpecularLightSource = 1;
            SpecularLight = specularLight;
            SpecTextureSlot = 0;

            Shine = shine;
        }

        public Material(Source diffSource, int specTextureSlot, Shine shine)
        {
            DiffuseLightSource = (int)diffSource;
            DiffuseLight = ColourF.Zero;
            DiffTextureSlot = 0;

            SpecularLightSource = 3;
            SpecularLight = ColourF.Zero;
            SpecTextureSlot = specTextureSlot;

            Shine = shine;
        }

        public Material(Source diffSource, Source specSource, Shine shine)
        {
            DiffuseLightSource = (int)diffSource;
            DiffuseLight = ColourF.Zero;
            DiffTextureSlot = 0;

            SpecularLightSource = (int)specSource;
            SpecularLight = ColourF.Zero;
            SpecTextureSlot = 0;

            Shine = shine;
        }

        public int DiffuseLightSource { get; set; }
        public ColourF DiffuseLight { get; set; }
        public int DiffTextureSlot { get; set; }

        public int SpecularLightSource { get; set; }
        public ColourF SpecularLight { get; set; }
        public int SpecTextureSlot { get; set; }

        public Shine Shine { get; set; }
    }

    public struct Light
    {
        public Light(ColourF colour, ColourF ambientColour, double linear, double quadratic, Vector3 point, bool direction = false)
        {
            LightColour = colour;
            AmbientLight = ambientColour;
            LightVector = point;
            Direction = direction;
            Linear = linear;
            Quadratic = quadratic;
        }

        public ColourF LightColour { get; set; }
        public ColourF AmbientLight { get; set; }
        public Vector3 LightVector { get; set; }
        public bool Direction { get; set; }

        public double Linear { get; set; }
        public double Quadratic { get; set; }
    }

    public struct SpotLight
    {
        public SpotLight(ColourF colour, Radian angle, Radian outerAngle, double linear, double quadratic, Vector3 point, Vector3 direction)
        {
            LightColour = colour;
            LightVector = point;
            Direction = direction;

            CosAngle = Math.Cos(angle);
            CosOuterAngle = Math.Cos(outerAngle);
            Linear = linear;
            Quadratic = quadratic;
        }

        public ColourF LightColour { get; set; }
        public Vector3 LightVector { get; set; }
        public Vector3 Direction { get; set; }

        public double CosAngle { get; set; }
        public double CosOuterAngle { get; set; }
        public double Linear { get; set; }
        public double Quadratic { get; set; }
    }
}
