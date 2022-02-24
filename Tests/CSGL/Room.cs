using System;
using System.Collections.Generic;
using Zene.Graphics;
using Zene.Graphics.Z3D;
using Zene.Graphics.Shaders;
using Zene.Structs;

namespace CSGL
{
    public class Room : IDrawable, IDisposable
    {
        public unsafe Room(LightingShader shader)
        {
            _disposed = false;

            Object3D.AddNormalTangents(wallVerts, 2, 1, wallIndices, out List<Vector3> nwv, out List<uint> nwi);

            wallSide = new DrawObject<Vector3, uint>(nwv, nwi, 4, 0, AttributeSize.D3, BufferUsage.DrawFrequent);
            wallSide.AddAttribute((uint)LightingShader.Location.TextureCoords, 1, AttributeSize.D3); // Texture Coords
            wallSide.AddAttribute((uint)LightingShader.Location.AmbientLightTextureCoords, 1, AttributeSize.D3); // Diffuse Light Textrue
            wallSide.AddAttribute((uint)LightingShader.Location.NormalMapTextureCoords, 1, AttributeSize.D3); // Normal Map Coords
            wallSide.AddAttribute((uint)LightingShader.Location.Normal, 2, AttributeSize.D3); // Normals
            wallSide.AddAttribute((uint)LightingShader.Location.Tangents, 3, AttributeSize.D3); // Tangents

            wallTexture = Texture2D.Create("Resources/Room/wallTex.png", WrapStyle.Repeated, TextureSampling.NearestMipMapBlend, true);
            wallNormal = Texture2D.Create("Resources/Room/wallNor.png", WrapStyle.Repeated, TextureSampling.BlendMipMapBlend, true);


            Object3D.AddNormalTangents(wallTopVerts, 2, 1, wallTopIndices, out List<Vector3> nwtv, out List<uint> nwti);

            wallTop = new DrawObject<Vector3, uint>(nwtv, nwti, 4, 0, AttributeSize.D3, BufferUsage.DrawFrequent);
            wallTop.AddAttribute((uint)LightingShader.Location.TextureCoords, 1, AttributeSize.D3); // Texture Coords
            wallTop.AddAttribute((uint)LightingShader.Location.AmbientLightTextureCoords, 1, AttributeSize.D3); // Diffuse Light Textrue
            wallTop.AddAttribute((uint)LightingShader.Location.NormalMapTextureCoords, 1, AttributeSize.D3); // Normal Map Coords
            wallTop.AddAttribute((uint)LightingShader.Location.Normal, 2, AttributeSize.D3); // Normals
            wallTop.AddAttribute((uint)LightingShader.Location.Tangents, 3, AttributeSize.D3); // Tangents

            wallTopTexture = Texture2D.Create("Resources/Room/wallTopTex.png", WrapStyle.Repeated, TextureSampling.NearestMipMapBlend, true);
            wallTopNormal = Texture2D.Create("Resources/Room/wallTopNor.png", WrapStyle.Repeated, TextureSampling.BlendMipMapBlend, true);


            Object3D.AddNormalTangents(floorVerts, 2, 1, floorIndices, out List<Vector3> nfv, out List<uint> nfi);

            floor = new DrawObject<Vector3, uint>(nfv, nfi, 4, 0, AttributeSize.D3, BufferUsage.DrawFrequent);
            floor.AddAttribute((uint)LightingShader.Location.TextureCoords, 1, AttributeSize.D3); // Texture Coords
            floor.AddAttribute((uint)LightingShader.Location.AmbientLightTextureCoords, 1, AttributeSize.D3); // Diffuse Light Textrue
            floor.AddAttribute((uint)LightingShader.Location.NormalMapTextureCoords, 1, AttributeSize.D3); // Normal Map Coords
            floor.AddAttribute((uint)LightingShader.Location.Normal, 2, AttributeSize.D3); // Normals
            floor.AddAttribute((uint)LightingShader.Location.Tangents, 3, AttributeSize.D3); // Tangents

            floorTexture = Texture2D.Create("Resources/Room/floorTex.png", WrapStyle.Repeated, TextureSampling.NearestMipMapBlend, true);
            floorNormal = Texture2D.Create("Resources/Room/floorNor.png", WrapStyle.Repeated, TextureSampling.BlendMipMapBlend, true);

            RoomMat = new Material(new Colour(106, 80, 93), Material.Source.None, Shine.None);

            shader.SetLight(2, new Zene.Graphics.Shaders.Light(new Colour(120, 100, 120), Colour.Zero, 0.007, 0.00014, new Vector3(8008, -3, -8)));
            shader.SetLight(3, new Zene.Graphics.Shaders.Light(new Colour(120, 100, 120), Colour.Zero, 0.007, 0.00014, new Vector3(8008, -3, -28)));
        }

        public Room()
        {
            _disposed = false;

            Object3D.AddNormalTangents(wallVerts, 2, 1, wallIndices, out List<Vector3> nwv, out List<uint> nwi);

            wallSide = new DrawObject<Vector3, uint>(nwv, nwi, 4, 0, AttributeSize.D3, BufferUsage.DrawFrequent);
            wallSide.AddAttribute((uint)BasicShader.Location.TextureCoords, 1, AttributeSize.D3); // Texture Coords

            wallTexture = Texture2D.Create("Resources/Room/wallTex.png", WrapStyle.Repeated, TextureSampling.NearestMipMapBlend, true);
            wallNormal = Texture2D.Create("Resources/Room/wallNor.png", WrapStyle.Repeated, TextureSampling.BlendMipMapBlend, true);


            Object3D.AddNormalTangents(wallTopVerts, 2, 1, wallTopIndices, out List<Vector3> nwtv, out List<uint> nwti);

            wallTop = new DrawObject<Vector3, uint>(nwtv, nwti, 4, 0, AttributeSize.D3, BufferUsage.DrawFrequent);
            wallTop.AddAttribute((uint)BasicShader.Location.TextureCoords, 1, AttributeSize.D3); // Texture Coords

            wallTopTexture = Texture2D.Create("Resources/Room/wallTopTex.png", WrapStyle.Repeated, TextureSampling.NearestMipMapBlend, true);
            wallTopNormal = Texture2D.Create("Resources/Room/wallTopNor.png", WrapStyle.Repeated, TextureSampling.BlendMipMapBlend, true);


            Object3D.AddNormalTangents(floorVerts, 2, 1, floorIndices, out List<Vector3> nfv, out List<uint> nfi);

            floor = new DrawObject<Vector3, uint>(nfv, nfi, 4, 0, AttributeSize.D3, BufferUsage.DrawFrequent);
            floor.AddAttribute((uint)BasicShader.Location.TextureCoords, 1, AttributeSize.D3); // Texture Coords


            floorTexture = Texture2D.Create("Resources/Room/floorTex.png", WrapStyle.Repeated, TextureSampling.NearestMipMapBlend, true);
            floorNormal = Texture2D.Create("Resources/Room/floorNor.png", WrapStyle.Repeated, TextureSampling.BlendMipMapBlend, true);

            RoomMat = new Material(new Colour(106, 80, 93), Material.Source.None, Shine.None);
        }

        private readonly DrawObject<Vector3, uint> wallSide;
        private readonly Texture2D wallTexture;
        private readonly Texture2D wallNormal;

        private readonly DrawObject<Vector3, uint> wallTop;
        private readonly Texture2D wallTopTexture;
        private readonly Texture2D wallTopNormal;

        private readonly DrawObject<Vector3, uint> floor;
        private readonly Texture2D floorTexture;
        private readonly Texture2D floorNormal;

        public readonly Material RoomMat;

        public const int TexTexSlot = 0;
        public const int NorTexSlot = 1;

        public void Draw()
        {
            wallTexture.Bind(TexTexSlot);
            wallNormal.Bind(NorTexSlot);

            wallSide.Draw();

            wallTexture.Unbind();
            wallNormal.Unbind();


            wallTopTexture.Bind(TexTexSlot);
            wallTopNormal.Bind(NorTexSlot);

            wallTop.Draw();

            wallTopTexture.Unbind();
            wallTopNormal.Unbind();


            floorTexture.Bind(TexTexSlot);
            floorNormal.Bind(NorTexSlot);

            floor.Draw();

            floorTexture.Unbind();
            floorNormal.Unbind();
        }

        private static readonly Vector3[] wallVerts = new Vector3[]
        {
            new Vector3(16, 0, 0), new Vector3(0, 1, 0),      // 0
            new Vector3(0, 0, 0), new Vector3(4, 1, 0),       // 1
            new Vector3(0, 4, 0), new Vector3(4, 0, 0),       // 2
            new Vector3(16, 4, 0), new Vector3(0, 0, 0),      // 3

            new Vector3(0, 0, -16), new Vector3(8, 1, 0),     // 4
            new Vector3(8, 0, -16), new Vector3(10, 1, 0),    // 5
            new Vector3(8, 4, -16), new Vector3(10, 0, 0),    // 6
            new Vector3(0, 4, -16), new Vector3(8, 0, 0),     // 7

            new Vector3(12, 0, -16), new Vector3(-5, 1, 0),   // 8
            new Vector3(16, 0, -16), new Vector3(-4, 1, 0),   // 9
            new Vector3(16, 4, -16), new Vector3(-4, 0, 0),   // 10
            new Vector3(12, 4, -16), new Vector3(-5, 0, 0),   // 11

            new Vector3(20, 0, 4), new Vector3(6, 1, 0),      // 12
            new Vector3(-4, 0, 4), new Vector3(0, 1, 0),      // 13
            new Vector3(-4, 4, 4), new Vector3(0, 0, 0),      // 14
            new Vector3(20, 4, 4), new Vector3(6, 0, 0),      // 15

            new Vector3(-4, 0, -40), new Vector3(23, 1, 0),   // 16
            new Vector3(20, 0, -40), new Vector3(17, 1, 0),   // 17
            new Vector3(20, 4, -40), new Vector3(17, 0, 0),   // 18
            new Vector3(-4, 4, -40), new Vector3(23, 0, 0),   // 19

            new Vector3(-4, 0, 4), new Vector3(34, 1, 0),     // 20
            new Vector3(-4, 4, 4), new Vector3(34, 0, 0),     // 21

            new Vector3(8, 0, -20), new Vector3(11, 1, 0),    // 22
            new Vector3(8, 4, -20), new Vector3(11, 0, 0),    // 23
            new Vector3(12, 0, -20), new Vector3(-6, 1, 0),   // 24
            new Vector3(12, 4, -20), new Vector3(-6, 0, 0),   // 25

            new Vector3(0, 0, -20), new Vector3(13, 1, 0),    // 26
            new Vector3(0, 4, -20), new Vector3(13, 0, 0),    // 27
            new Vector3(0, 4, -36), new Vector3(17, 0, 0),    // 28
            new Vector3(0, 0, -36), new Vector3(17, 1, 0),    // 29

            new Vector3(16, 4, -36), new Vector3(21, 0, 0),    // 30
            new Vector3(16, 0, -36), new Vector3(21, 1, 0),    // 31
            new Vector3(16, 4, -20), new Vector3(25, 0, 0),    // 32
            new Vector3(16, 0, -20), new Vector3(25, 1, 0),    // 33

            new Vector3(12, 0, -20), new Vector3(26, 1, 0),    // 34
            new Vector3(12, 4, -20), new Vector3(26, 0, 0),    // 35
        };

        private static readonly uint[] wallIndices = new uint[]
        {
            // Inner Front
            0, 1, 2,
            2, 3, 0,

            // Inner Back Left
            4, 5, 6,
            6, 7, 4,

            // Inner Back Right
            8, 9, 10,
            10, 11, 8,

            // Inner Back Back Left
            5, 22, 23,
            23, 6, 5,

            // Inner Back Back Right
            24, 8, 11,
            11, 25, 24,

            // Inner Back Back Front Left
            22, 26, 27,
            27, 23, 22,

            // Inner Back Back Front Right
            33, 34, 35,
            35, 32, 33,

            // Inner Back Back Back Left
            26, 29, 28,
            28, 27, 26,

            // Inner Back Back Back
            29, 31, 30,
            30, 28, 29,

            // Inner Back Back Back Right
            31, 33, 32,
            32, 30, 31,

            // Inner Left
            1, 4, 7,
            7, 2, 1,

            // Inner Right
            9, 0, 3,
            3, 10, 9,

            // Outer Front
            13, 12, 15,
            15, 14, 13,

            // Outer Back
            17, 16, 19,
            19, 18, 17,

            // Outer Left
            16, 20, 21,
            21, 19, 16,

            // Outer Right
            12, 17, 18,
            18, 15, 12
        };

        private static readonly double levInTex = (double)1 / 11;
        private static readonly double sixInTex = (double)1 / 6;

        private static readonly Vector3[] wallTopVerts = new Vector3[]
        {
            new Vector3(-4, 0, 4), new Vector3(0, 1, 0),                          // 0
            new Vector3(-4, 0, -40), new Vector3(1, 1, 0),                        // 1
            new Vector3(20, 0, -40), new Vector3(1, 0, 0),                        // 2
            new Vector3(20, 0, 4), new Vector3(0, 0, 0),                          // 3

            new Vector3(0, 0, 0), new Vector3(levInTex, sixInTex * 5, 0),         // 4
            new Vector3(0, 0, -16), new Vector3(levInTex * 5, sixInTex * 5, 0),   // 5
            new Vector3(8, 0, -16), new Vector3(levInTex * 5, sixInTex * 3, 0),   // 6
            new Vector3(8, 0, -20), new Vector3(levInTex * 6, sixInTex * 3, 0),   // 7

            new Vector3(0, 0, -20), new Vector3(levInTex * 6, sixInTex * 5, 0),   // 8
            new Vector3(0, 0, -36), new Vector3(levInTex * 10, sixInTex * 5, 0),  // 9
            new Vector3(16, 0, -36), new Vector3(levInTex * 10, sixInTex, 0),     // 10
            new Vector3(16, 0, -20), new Vector3(levInTex * 6, sixInTex, 0),      // 11

            new Vector3(12, 0, -20), new Vector3(levInTex * 6, sixInTex * 2, 0),  // 12
            new Vector3(12, 0, -16), new Vector3(levInTex * 5, sixInTex * 2, 0),  // 13
            new Vector3(16, 0, -16), new Vector3(levInTex * 5, sixInTex, 0),      // 14
            new Vector3(16, 0, 0), new Vector3(levInTex, sixInTex, 0),            // 15
        };

        private static readonly uint[] wallTopIndices = new uint[]
        {
            0, 1, 9,
            9, 4, 0,
            
            0, 4, 15,
            15, 3, 0,

            15, 10, 2,
            2, 3, 15,

            9, 1, 2,
            2, 10, 9,

            5, 8, 7,
            7, 6, 5,

            13, 12, 11,
            11, 14, 13,
        };

        private static readonly Vector3[] floorVerts = new Vector3[]
        {
            new Vector3(0, 4, 0), new Vector3(0, 1, 0),       // 0
            new Vector3(16, 4, 0), new Vector3(0, 0, 0),      // 1
            new Vector3(16, 4, -36), new Vector3(1, 0, 0),    // 2
            new Vector3(0, 4, -36), new Vector3(1, 1, 0),     // 3
        };

        private static readonly uint[] floorIndices = new uint[]
        {
            0, 3, 2,
            2, 1, 0
        };

        private bool _disposed;
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

                wallSide.Dispose();
                wallTexture.Dispose();
                wallNormal.Dispose();

                wallTop.Dispose();
                wallTopTexture.Dispose();
                wallTopNormal.Dispose();

                floor.Dispose();
                floorTexture.Dispose();
                floorNormal.Dispose();

                GC.SuppressFinalize(this);
            }
        }
    }
}
