using System;
using Zene.Graphics;
using Zene.Graphics.Base;
using Zene.Sprites;
using Zene.Structs;
using Zene.Physics;
//using System.Threading.Tasks;

namespace PhysicsTest
{
    public record PlanetProperty(Vector2 Location, double Diameter, double Density, Colour3 Colour);

    public class PlanetGroup : ISprite, IDisplay
    {
        /// <summary>
        /// Initialising and assigning _texture.
        /// </summary>
        static PlanetGroup()
        {
            byte[] byteData = Bitmap.ExtractData("Resources/planet.png", out int w, out int h);
            GLArray<Vector2<byte>> texData = new GLArray<Vector2<byte>>(w, h);
            for (int i = 0; i < texData.Size; i++)
            {
                texData[i] = new Vector2<byte>(byteData[i * 4], byteData[(i * 4) + 3]);
            }
            _texture = new Texture2D(TextureFormat.Rg8, TextureData.Byte);
            _texture.SetData(w, h, BaseFormat.Rg, texData);
            _texture.WrapStyle = WrapStyle.EdgeClamp;
            _texture.MinFilter = TextureSampling.Blend;
            _texture.MagFilter = TextureSampling.Nearest;
        }
        public PlanetGroup(PlanetProperty[] planetData, IBox bounds, TextureShader shader)
        {
            PlanetCount = planetData.Length;
            _shader = shader;
            Bounds = bounds;

            //
            // Drawing setup
            //

            _drawable = new DrawObject<Vector2, byte>(
                    new Vector2[]
                    {
                        new Vector2(0.5, 0.5), new Vector2(1, 1),
                        new Vector2(0.5, -0.5), new Vector2(1, 0),
                        new Vector2(-0.5, -0.5), new Vector2(0, 0),
                        new Vector2(-0.5, 0.5), new Vector2(0, 1)
                    },
                    new byte[]
                    {
                        0, 1, 2,
                        2, 3, 0
                    },
                    2, 0, AttributeSize.D2, BufferUsage.DrawFrequent);
            _drawable.AddAttribute(1, 1, AttributeSize.D2); // Texture Coordinates

            Vector4[] instData = new Vector4[PlanetCount * 5];
            Gravities = new GravityForce[PlanetCount];
            Collisions = new PlanetCollision[PlanetCount];
            _planets = new PlanetStruct[PlanetCount];

            // Create data to assgin instance buffer
            for (int i = 0; i < PlanetCount; i++)
            {
                //
                // Graphical Data
                //
                Matrix4 matrix = 
                    Matrix4.CreateScale(planetData[i].Diameter, planetData[i].Diameter, 1) *
                    Matrix4.CreateTranslation((Vector3)planetData[i].Location);

                int instIndex = i * 5;

                // Matrix
                instData[instIndex] = matrix.Row0;
                instData[instIndex + 1] = matrix.Row1;
                instData[instIndex + 2] = matrix.Row2;
                instData[instIndex + 3] = matrix.Row3;

                // Colour
                instData[instIndex + 4] = (Vector4)((Vector3)planetData[i].Colour);

                //
                // Physics Data
                //
                PlanetStruct planet = new PlanetStruct(planetData[i]);
                _planets[i] = planet;

                Gravities[i] = planet.Gravity;
                Collisions[i] = planet.Collision;
            }

            _instanceData = new ArrayBuffer<Vector4>(instData, 5, BufferUsage.DrawFrequent);

            // Set matrix instance reference
            _drawable.Vao.Bind();
            _drawable.Vao.AddBuffer(_instanceData, 2, 0, DataType.Double, AttributeSize.D4);
            _drawable.Vao.AddBuffer(_instanceData, 3, 1, DataType.Double, AttributeSize.D4);
            _drawable.Vao.AddBuffer(_instanceData, 4, 2, DataType.Double, AttributeSize.D4);
            _drawable.Vao.AddBuffer(_instanceData, 5, 3, DataType.Double, AttributeSize.D4);
            // Set colour reference
            _drawable.Vao.AddBuffer(_instanceData, 6, 4, DataType.Double, AttributeSize.D3);

            // Set up instancing
            GL.VertexAttribDivisor(2, 1);
            GL.VertexAttribDivisor(3, 1);
            GL.VertexAttribDivisor(4, 1);
            GL.VertexAttribDivisor(5, 1);
            GL.VertexAttribDivisor(6, 1);

            _drawable.Vao.Unbind();
        }

        /*
        private class PlanetStruct : IPlanet
        {
            public PlanetStruct(PlanetProperty properties)
            {
                _box = new Box(properties.Location, new Vector2(properties.Diameter));
                Gravity = new GravityForce(properties.Location, CalculateMass(properties.Diameter * 0.5, properties.Density));
                Collision = new PlanetCollision(this);
                Colour = properties.Colour;
                Velocity = Velocity.Zero;
            }

            private Box _box;
            public Box Box
            {
                get
                {
                    return _box;
                }
            }
            IBox IPlanet.Box => Box;
            public double Radius
            {
                get
                {
                    return Box.Width * 0.5;
                }
            }
            public double Diameter
            {
                get
                {
                    return Box.Width;
                }
            }
            public double Mass
            {
                get
                {
                    return Gravity.Mass;
                }
            }

            public Colour3 Colour { get; set; }

            public GravityForce Gravity { get; }
            public PlanetCollision Collision { get; }

            public Velocity Velocity { get; set; }

            public Matrix4 CreateMatrix()
            {
                return Matrix4.CreateScale(Diameter, Diameter, 1) *
                    Matrix4.CreateTranslation((Vector3)Box.Center);
            }
            public Vector4 CreateColour()
            {
                return (Vector4)((Vector3)Colour);
            }

            public Velocity GetCollision(Vector2 location, double radius, double frameTime)
            {
                // Distance from this to location
                double dist = location.Distance(_box.Center);

                // Cirles are far enough apart to not overlap
                if (dist >= (radius + Radius)) { return Velocity.Zero; }

                // Direction from this to location
                Vector2 dir = (location - _box.Location).Normalized();
                // The distance need to move to not be overlapping
                double innerDist = (radius + Radius) - dist;

                return new Velocity(dir, innerDist / frameTime);
            }

            public void ApplyPhysics(double frameTime, PlanetStruct[] planets)
            {
                // Gravity physics
                foreach (PlanetStruct planet in planets)
                {
                    if (planet == this) { continue; }

                    Velocity += planet.Gravity.GetForce(new PhyisicsProperties()
                    {
                        Box = _box,
                        NextPosition = _box.Location + (Velocity.Movement * frameTime),
                        Mass = Gravity.Mass,
                        Velocity = Velocity
                    }, frameTime);
                }

                // Collisions
                foreach (PlanetStruct planet in planets)
                {
                    if (planet == this) { continue; }

                    Velocity += planet.GetCollision(
                        _box.Location + (Velocity.Movement * frameTime),
                        Radius, frameTime);
                }

                _box.Location += Velocity.Movement * frameTime;
                Gravity.CenterOfMass = _box.Location;
            }
        }*/
        private class PlanetStruct : IPlanet
        {
            public PlanetStruct(PlanetProperty properties)
            {
                Box = new Box(properties.Location, new Vector2(properties.Diameter));
                Gravity = new GravityForce(properties.Location, CalculateMass(properties.Diameter * 0.5, properties.Density));
                Collision = new PlanetCollision(this);
            }

            public IBox Box { get; }
            public double Radius
            {
                get
                {
                    return Box.Width * 0.5;
                }
            }

            public GravityForce Gravity { get; }
            public PlanetCollision Collision { get; }
        }

        private static readonly Texture2D _texture;
        private readonly DrawObject<Vector2, byte> _drawable;
        private readonly ArrayBuffer<Vector4> _instanceData;
        private readonly TextureShader _shader;

        public int PlanetCount { get; }

        public GravityForce[] Gravities { get; }
        public PlanetCollision[] Collisions { get; }
        private readonly PlanetStruct[] _planets;

        public IBox Bounds { get; }
        IDisplay ISprite.Display => this;

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            _drawable.Dispose();
            _instanceData.Dispose();

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        void IDisplay.Draw(ISprite sprite)
        {
            if (sprite != this)
            {
                throw new Exception($"{nameof(PlanetGroup)} display can only draw itself.");
            }

            Draw(Box.Infinity);
        }
        public void Draw(IBox view)
        {
            if (!view.Overlaps(Bounds)) { return; }
            /*
            Vector4[] instData = new Vector4[PlanetCount * 5];
            // Set instance data for drawing
            for (int i = 0; i < PlanetCount; i++)
            {
                Matrix4 m = _planets[i].CreateMatrix();

                int instIndex = i * 5;

                // Matrix
                instData[instIndex] = m.Row0;
                instData[instIndex + 1] = m.Row1;
                instData[instIndex + 2] = m.Row2;
                instData[instIndex + 3] = m.Row3;

                // Colour
                instData[instIndex + 4] = _planets[i].CreateColour();
            }
            _instanceData.SetData(instData);
            */

            _shader.Bind();

            _shader.TextureSlot = 0;
            _texture.Bind(0);

            _drawable.DrawMultiple(PlanetCount);

            _shader.Unbind();
        }
        /*
        public void ApplyPhysics(double frameTime)
        {
            Parallel.ForEach(_planets, planet =>
            {
                planet.ApplyPhysics(frameTime, _planets);
            });
        }
        */
        private static double CalculateMass(double radius, double density)
        {
            // Find volume of planet in m^3
            double volume = (4 * Math.PI * Math.Pow(radius, 3)) / 3;

            return density * volume;
        }

        /// <summary>
        /// Smallest planet density in kg/m^3
        /// </summary>
        public const double MinDensity = 2300;
        /// <summary>
        /// Largest planet density in kg/m^3
        /// </summary>
        public const double MaxDensity = 10_000;
    }
}
