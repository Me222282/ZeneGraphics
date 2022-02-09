using System;
using System.Collections.Generic;
using System.IO;
using ObjLoader;
using ObjLoader.Data;
using Zene.Structs;

namespace Zene.Graphics.Z3D
{
    public class Object3D : IDrawable, IDisposable
    {
        private DrawObject<Vector3, uint> _object;

        //private Material _mat;

        public void Draw()
        {
            _object.Draw();
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) { return; }

            _object.Dispose();
            _object = null;

            _disposed = true;

            GC.SuppressFinalize(this);
        }

        //
        // Object3D Creators
        //

        /// <summary>
        /// Creates a 3D object from a .obj file.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="texCoordI">The shader location of for texture coords</param>
        /// <param name="normalI">The shader location of for normals</param>
        /// <returns></returns>
        public static Object3D FromObj(string path, uint texCoordI, uint normalI)
        {
            ExtractObj(path, out List<Vector3> data, out List<uint> indexData);

            Object3D o = new Object3D
            {
                _object = new DrawObject<Vector3, uint>(data, indexData, 3, 0, AttributeSize.D3, BufferUsage.DrawFrequent)
            };
            o._object.AddAttribute(texCoordI, 1, AttributeSize.D2); // Texture coords
            o._object.AddAttribute(normalI, 2, AttributeSize.D3); // Normals

            return o;
        }

        /// <summary>
        /// Creates a 3D object from a .obj file without texture coords.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="normalI">The shader location of for normals</param>
        /// <returns></returns>
        public static Object3D FromObj(string path, uint normalI)
        {
            ExtractObjVt(path, out List<Vector3> data, out List<uint> indexData);

            Object3D o = new Object3D
            {
                _object = new DrawObject<Vector3, uint>(data, indexData, 2, 0, AttributeSize.D3, BufferUsage.DrawFrequent)
            };
            o._object.AddAttribute(normalI, 1, AttributeSize.D3); // Normals

            return o;
        }

        /// <summary>
        /// Creates a 3D object from a .obj file with auto asigned tangents.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="texCoordI">The shader location of for texture coords</param>
        /// <param name="normalI">The shader location of for normals</param>
        /// <param name="tangentI">The shader location of for tangents</param>
        /// <returns></returns>
        public static Object3D FromObj(string path, uint texCoordI, uint normTexCoordI, uint normalI, uint tangentI)
        {
            ExtractObjVta(path, out List<Vector3> data, out List<uint> indexData);

            Object3D o = new Object3D
            {
                _object = new DrawObject<Vector3, uint>(data, indexData, 4, 0, AttributeSize.D3, BufferUsage.DrawFrequent)
            };
            o._object.AddAttribute(texCoordI, 1, AttributeSize.D2); // Texture coords
            o._object.AddAttribute(normTexCoordI, 1, AttributeSize.D2); // Normal map texture coords
            o._object.AddAttribute(normalI, 2, AttributeSize.D3); // Normals
            o._object.AddAttribute(tangentI, 3, AttributeSize.D3); // Tangents

            return o;
        }

        //
        // .obj extracters
        //

        // Double
        /// <summary>
        /// Extracts the vertices and indices from a .obj file.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="vertices">The outputed vertices</param>
        /// <param name="indices">THe outputed indices</param>
        public static void ExtractObj(string path, out List<double> vertices, out List<uint> indices)
        {
            Loader loader = new Loader();

            FileStream stream = new FileStream(path, FileMode.Open);

            LoadResult result = loader.Load(stream);

            vertices = new List<double>();

            indices = new List<uint>();

            foreach (Group g in result.Groups)
            {
                foreach (Face f in g.Faces)
                {
                    for (int i = 0; i < f.Count; i++)
                    {
                        Vector3I index = f[i] - 1;

                        indices.Add((uint)(vertices.Count / 8));

                        vertices.AddRange(new double[]
                        {
                            result.Vertices[index.X].X,
                            result.Vertices[index.X].Y,
                            result.Vertices[index.X].Z,

                            result.Textures[index.Y].X,
                            result.Textures[index.Y].Y,

                            result.Normals[index.Z].X,
                            result.Normals[index.Z].Y,
                            result.Normals[index.Z].Z
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Extracts the vertices and indices from a .obj file without texture coords.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="vertices">The outputed vertices</param>
        /// <param name="indices">THe outputed indices</param>
        public static void ExtractObjVt(string path, out List<double> vertices, out List<uint> indices)
        {
            Loader loader = new Loader();

            FileStream stream = new FileStream(path, FileMode.Open);

            LoadResult result = loader.Load(stream);

            vertices = new List<double>();

            indices = new List<uint>();

            foreach (Group g in result.Groups)
            {
                foreach (Face f in g.Faces)
                {
                    for (int i = 0; i < f.Count; i++)
                    {
                        int vi = f[i].X - 1;
                        int ni = f[i].Z - 1;

                        indices.Add((uint)(vertices.Count / 6));

                        vertices.AddRange(new double[]
                        {
                            result.Vertices[vi].X,
                            result.Vertices[vi].Y,
                            result.Vertices[vi].Z,

                            result.Normals[ni].X,
                            result.Normals[ni].Y,
                            result.Normals[ni].Z
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Extracts the vertices and indices from a .obj file with auto asigned normals.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="vertices">The outputed vertices</param>
        /// <param name="indices">THe outputed indices</param>
        public static void ExtractObjVn(string path, out List<double> vertices, out List<uint> indices)
        {
            Loader loader = new Loader();

            FileStream stream = new FileStream(path, FileMode.Open);

            LoadResult result = loader.Load(stream);

            vertices = new List<double>();

            indices = new List<uint>();

            foreach (Group g in result.Groups)
            {
                foreach (Face f in g.Faces)
                {
                    for (int i = 0; i < f.Count; i++)
                    {
                        int vi = f[i].X - 1;

                        indices.Add((uint)(vertices.Count / 3));

                        vertices.AddRange(new double[]
                        {
                            result.Vertices[vi].X,
                            result.Vertices[vi].Y,
                            result.Vertices[vi].Z
                        });
                    }
                }
            }

            AddNormals(vertices.ToArray(), 3, indices.ToArray(), out vertices, out indices);
        }

        /// <summary>
        /// Extracts the vertices and indices from a .obj file with auto asigned normals and tangents.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="vertices">The outputed vertices</param>
        /// <param name="indices">THe outputed indices</param>
        public static void ExtractObjVnVta(string path, out List<double> vertices, out List<uint> indices)
        {
            Loader loader = new Loader();

            FileStream stream = new FileStream(path, FileMode.Open);

            LoadResult result = loader.Load(stream);

            vertices = new List<double>();

            indices = new List<uint>();

            foreach (Group g in result.Groups)
            {
                foreach (Face f in g.Faces)
                {
                    for (int i = 0; i < f.Count; i++)
                    {
                        int vi = f[i].X - 1;
                        int ti = f[i].Y - 1;

                        indices.Add((uint)(vertices.Count / 5));

                        vertices.AddRange(new double[]
                        {
                            result.Vertices[vi].X,
                            result.Vertices[vi].Y,
                            result.Vertices[vi].Z,

                            result.Textures[ti].X,
                            result.Textures[ti].Y
                        });
                    }
                }
            }

            AddNormalTangents(vertices.ToArray(), 5, 3, indices.ToArray(), out vertices, out indices);
        }

        /// <summary>
        /// Extracts the vertices and indices from a .obj file with auto asigned tangents.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="vertices">The outputed vertices</param>
        /// <param name="indices">THe outputed indices</param>
        public static void ExtractObjVta(string path, out List<double> vertices, out List<uint> indices)
        {
            ExtractObj(path, out vertices, out indices);

            AddTangents(vertices.ToArray(), 8, 3, indices.ToArray(), out vertices, out indices);
        }

        // Point3
        /// <summary>
        /// Extracts the vertices and indices from a .obj file.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="vertices">The outputed vertices</param>
        /// <param name="indices">THe outputed indices</param>
        public static void ExtractObj(string path, out List<Vector3> vertices, out List<uint> indices)
        {
            Loader loader = new Loader();

            FileStream stream = new FileStream(path, FileMode.Open);

            LoadResult result = loader.Load(stream);

            vertices = new List<Vector3>();

            indices = new List<uint>();

            foreach (Group g in result.Groups)
            {
                foreach (Face f in g.Faces)
                {
                    for (int i = 0; i < f.Count; i++)
                    {
                        Vector3I index = f[i] - 1;

                        indices.Add((uint)(vertices.Count / 3));

                        vertices.AddRange(new Vector3[]
                        {
                            result.Vertices[index.X],
                            new Vector3(result.Textures[index.Y], 0),
                            result.Normals[index.Z],
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Extracts the vertices and indices from a .obj file without texture coords.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="vertices">The outputed vertices</param>
        /// <param name="indices">THe outputed indices</param>
        public static void ExtractObjVt(string path, out List<Vector3> vertices, out List<uint> indices)
        {
            Loader loader = new Loader();

            FileStream stream = new FileStream(path, FileMode.Open);

            LoadResult result = loader.Load(stream);

            vertices = new List<Vector3>();

            indices = new List<uint>();

            foreach (Group g in result.Groups)
            {
                foreach (Face f in g.Faces)
                {
                    for (int i = 0; i < f.Count; i++)
                    {
                        int vi = f[i].X - 1;
                        int ni = f[i].Z - 1;

                        indices.Add((uint)(vertices.Count / 2));

                        vertices.AddRange(new Vector3[]
                        {
                            result.Vertices[vi],
                            result.Normals[ni]
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Extracts the vertices and indices from a .obj file with auto asigned normals.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="vertices">The outputed vertices</param>
        /// <param name="indices">THe outputed indices</param>
        public static void ExtractObjVn(string path, out List<Vector3> vertices, out List<uint> indices)
        {
            Loader loader = new Loader();

            FileStream stream = new FileStream(path, FileMode.Open);

            LoadResult result = loader.Load(stream);

            vertices = new List<Vector3>();

            indices = new List<uint>();

            foreach (Group g in result.Groups)
            {
                foreach (Face f in g.Faces)
                {
                    for (int i = 0; i < f.Count; i++)
                    {
                        int vi = f[i].X - 1;

                        indices.Add((uint)(vertices.Count));

                        vertices.Add(result.Vertices[vi]);
                    }
                }
            }

            AddNormals(vertices.ToArray(), 1, indices.ToArray(), out vertices, out indices);
        }

        /// <summary>
        /// Extracts the vertices and indices from a .obj file with auto asigned normals and tangents.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="vertices">The outputed vertices</param>
        /// <param name="indices">THe outputed indices</param>
        public static void ExtractObjVnVta(string path, out List<Vector3> vertices, out List<uint> indices)
        {
            Loader loader = new Loader();

            FileStream stream = new FileStream(path, FileMode.Open);

            LoadResult result = loader.Load(stream);

            vertices = new List<Vector3>();

            indices = new List<uint>();

            foreach (Group g in result.Groups)
            {
                foreach (Face f in g.Faces)
                {
                    for (int i = 0; i < f.Count; i++)
                    {
                        int vi = f[i].X - 1;
                        int ti = f[i].Y - 1;

                        indices.Add((uint)(vertices.Count / 2));

                        vertices.AddRange(new Vector3[]
                        {
                            result.Vertices[vi],
                            new Vector3(result.Textures[ti], 0)
                        });
                    }
                }
            }

            AddNormalTangents(vertices.ToArray(), 2, 1, indices.ToArray(), out vertices, out indices);
        }

        /// <summary>
        /// Extracts the vertices and indices from a .obj file with auto asigned tangents.
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="vertices">The outputed vertices</param>
        /// <param name="indices">THe outputed indices</param>
        public static void ExtractObjVta(string path, out List<Vector3> vertices, out List<uint> indices)
        {
            ExtractObj(path, out vertices, out indices);

            AddTangents(vertices.ToArray(), 3, 1, indices.ToArray(), out vertices, out indices);
        }

        //
        // Auto extensions
        //

        // Double
        /// <summary>
        /// Add noramls to a list of 3 dimensional vertex data.
        /// </summary>
        /// <param name="vertices">The original vertex data</param>
        /// <param name="vertexSize">The number of values per vertex, including the positions</param>
        /// <param name="indexData">The original index data</param>
        /// <param name="newVerts">The list of new vertices with noramls at the end of each vertex</param>
        /// <param name="newIndices">The list of new indices</param>
        public static void AddNormals(double[] vertices, byte vertexSize, uint[] indexData, out List<double> newVerts, out List<uint> newIndices)
        {
            // The new list of data
            newVerts = new List<double>();
            newIndices = new List<uint>();

            for (int i = 0; i < indexData.Length; i += 3)
            {
                // Vertex 1 on plane
                Vector3 a = new Vector3(
                    vertices[indexData[i] * vertexSize],
                    vertices[(indexData[i] * vertexSize) + 1],
                    vertices[(indexData[i] * vertexSize) + 2]);
                // Vertex 2 on plane
                Vector3 b = new Vector3(
                    vertices[indexData[i + 1] * vertexSize],
                    vertices[(indexData[i + 1] * vertexSize) + 1],
                    vertices[(indexData[i + 1] * vertexSize) + 2]);
                // Vertex 3 on plane
                Vector3 c = new Vector3(
                    vertices[indexData[i + 2] * vertexSize],
                    vertices[(indexData[i + 2] * vertexSize) + 1],
                    vertices[(indexData[i + 2] * vertexSize) + 2]);

                // Calculate normal based on points on the plane
                Vector3 n = Vector3.PlaneNormal(a, b, c);

                //
                // Point 1
                //
                // Add positions back to first point on plane
                newVerts.AddRange(new double[] { a.X, a.Y, a.Z });
                // Add any other data that was part of the origianl
                for (int ia = 3; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i] * vertexSize) + ia]);
                }
                // Add normal attribute
                newVerts.AddRange(new double[] { n.X, n.Y, n.Z });
                // Add index of point to indices
                newIndices.Add((uint)i);

                //
                // Point 2
                //
                // Add positions back to second point on plane
                newVerts.AddRange(new double[] { b.X, b.Y, b.Z });
                // Add any other data that was part of the origianl
                for (int ia = 3; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i + 1] * vertexSize) + ia]);
                }
                // Add normal attribute
                newVerts.AddRange(new double[] { n.X, n.Y, n.Z });
                // Add index of point to indices
                newIndices.Add((uint)i + 1);

                //
                // Point 3
                //
                // Add positions back to third point on plane
                newVerts.AddRange(new double[] { c.X, c.Y, c.Z });
                // Add any other data that was part of the origianl
                for (int ia = 3; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i + 2] * vertexSize) + ia]);
                }
                // Add normal attribute
                newVerts.AddRange(new double[] { n.X, n.Y, n.Z });
                // Add index of point to indices
                newIndices.Add((uint)i + 2);
            }
        }

        /// <summary>
        /// Add noramls and tangents to a list of 3 dimensional vertex data.
        /// </summary>
        /// <param name="vertices">The original vertex data</param>
        /// <param name="vertexSize">The number of values per vertex, including the positions</param>
        /// <param name="normalMapCoordI">The location per vertex where the normal map coodinates start</param>
        /// <param name="indexData">The original index data</param>
        /// <param name="newVerts">The list of new vertices with noramls then tangents at the end of each vertex</param>
        /// <param name="newIndices">The list of new indices</param>
        public static void AddNormalTangents(double[] vertices, byte vertexSize, uint normalMapCoordI, uint[] indexData, out List<double> newVerts, out List<uint> newIndices)
        {
            // The new list of data
            newVerts = new List<double>();
            newIndices = new List<uint>();

            for (int i = 0; i < indexData.Length; i += 3)
            {
                //
                // Positions
                //
                // Vertex 1 on plane
                Vector3 a = new Vector3(
                    vertices[indexData[i] * vertexSize],
                    vertices[(indexData[i] * vertexSize) + 1],
                    vertices[(indexData[i] * vertexSize) + 2]);
                // Vertex 2 on plane
                Vector3 b = new Vector3(
                    vertices[indexData[i + 1] * vertexSize],
                    vertices[(indexData[i + 1] * vertexSize) + 1],
                    vertices[(indexData[i + 1] * vertexSize) + 2]);
                // Vertex 3 on plane
                Vector3 c = new Vector3(
                    vertices[indexData[i + 2] * vertexSize],
                    vertices[(indexData[i + 2] * vertexSize) + 1],
                    vertices[(indexData[i + 2] * vertexSize) + 2]);

                //
                // Normal Map Coords
                //
                // Vertex 1 on plane
                Vector3 uvA = new Vector3(
                    vertices[(indexData[i] * vertexSize) + normalMapCoordI],
                    vertices[(indexData[i] * vertexSize) + normalMapCoordI + 1],
                    vertices[(indexData[i] * vertexSize) + normalMapCoordI + 2]);
                // Vertex 2 on plane
                Vector3 uvB = new Vector3(
                    vertices[(indexData[i + 1] * vertexSize) + normalMapCoordI],
                    vertices[(indexData[i + 1] * vertexSize) + normalMapCoordI + 1],
                    vertices[(indexData[i + 1] * vertexSize) + normalMapCoordI + 2]);
                // Vertex 3 on plane
                Vector3 uvC = new Vector3(
                    vertices[(indexData[i + 2] * vertexSize) + normalMapCoordI],
                    vertices[(indexData[i + 2] * vertexSize) + normalMapCoordI + 1],
                    vertices[(indexData[i + 2] * vertexSize) + normalMapCoordI + 2]);

                // Calculate normal based on points on the plane
                Vector3 n = Vector3.PlaneNormal(a, b, c);

                //
                // Tangent calculation
                //
                Vector3 edge1 = b - a;
                Vector3 edge2 = c - a;
                Vector2 delta1 = (Vector2)uvB - (Vector2)uvA;
                Vector2 delta2 = (Vector2)uvC - (Vector2)uvA;

                double f = 1.0 / ((delta1.X * delta2.Y) - (delta2.X * delta1.Y));

                // Calculate tangent based on the points on the plane
                // and the locations of the noraml texture coords
                Vector3 t = new Vector3(
                    f * ((delta2.Y * edge1.X) - (delta1.Y * edge2.X)),
                    f * ((delta2.Y * edge1.Y) - (delta1.Y * edge2.Y)),
                    f * ((delta2.Y * edge1.Z) - (delta1.Y * edge2.Z)));

                //
                // Point 1
                //
                // Add positions back to first point on plane
                newVerts.AddRange(new double[] { a.X, a.Y, a.Z });
                // Add any other data that was part of the origianl
                for (int ia = 3; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i] * vertexSize) + ia]);
                }
                // Add normal attribute
                newVerts.AddRange(new double[] { n.X, n.Y, n.Z });
                // Add tangent attribute
                newVerts.AddRange(new double[] { t.X, t.Y, t.Z });
                // Add index of point to indices
                newIndices.Add((uint)i);

                //
                // Point 2
                //
                // Add positions back to second point on plane
                newVerts.AddRange(new double[] { b.X, b.Y, b.Z });
                // Add any other data that was part of the origianl
                for (int ia = 3; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i + 1] * vertexSize) + ia]);
                }
                // Add normal attribute
                newVerts.AddRange(new double[] { n.X, n.Y, n.Z });
                // Add tangent attribute
                newVerts.AddRange(new double[] { t.X, t.Y, t.Z });
                // Add index of point to indices
                newIndices.Add((uint)i + 1);

                //
                // Point 3
                //
                // Add positions back to third point on plane
                newVerts.AddRange(new double[] { c.X, c.Y, c.Z });
                // Add any other data that was part of the origianl
                for (int ia = 3; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i + 2] * vertexSize) + ia]);
                }
                // Add normal attribute
                newVerts.AddRange(new double[] { n.X, n.Y, n.Z });
                // Add tangent attribute
                newVerts.AddRange(new double[] { t.X, t.Y, t.Z });
                // Add index of point to indices
                newIndices.Add((uint)i + 2);
            }
        }

        /// <summary>
        /// Add noramls and tangents to a list of 3 dimensional vertex data.
        /// </summary>
        /// <param name="vertices">The original vertex data</param>
        /// <param name="vertexSize">The number of values per vertex, including the positions</param>
        /// <param name="normalMapCoordI">The location per vertex where the normal map coodinates start</param>
        /// <param name="indexData">The original index data</param>
        /// <param name="newVerts">The list of new vertices with tangents at the end of each vertex</param>
        /// <param name="newIndices">The list of new indices</param>
        public static void AddTangents(double[] vertices, byte vertexSize, uint normalMapCoordI, uint[] indexData, out List<double> newVerts, out List<uint> newIndices)
        {
            // The new list of data
            newVerts = new List<double>();
            newIndices = new List<uint>();

            for (int i = 0; i < indexData.Length; i += 3)
            {
                //
                // Positions
                //
                // Vertex 1 on plane
                Vector3 a = new Vector3(
                    vertices[indexData[i] * vertexSize],
                    vertices[(indexData[i] * vertexSize) + 1],
                    vertices[(indexData[i] * vertexSize) + 2]);
                // Vertex 2 on plane
                Vector3 b = new Vector3(
                    vertices[indexData[i + 1] * vertexSize],
                    vertices[(indexData[i + 1] * vertexSize) + 1],
                    vertices[(indexData[i + 1] * vertexSize) + 2]);
                // Vertex 3 on plane
                Vector3 c = new Vector3(
                    vertices[indexData[i + 2] * vertexSize],
                    vertices[(indexData[i + 2] * vertexSize) + 1],
                    vertices[(indexData[i + 2] * vertexSize) + 2]);

                //
                // Normal Map Coords
                //
                // Vertex 1 on plane
                Vector3 uvA = new Vector3(
                    vertices[(indexData[i] * vertexSize) + normalMapCoordI],
                    vertices[(indexData[i] * vertexSize) + normalMapCoordI + 1],
                    vertices[(indexData[i] * vertexSize) + normalMapCoordI + 2]);
                // Vertex 2 on plane
                Vector3 uvB = new Vector3(
                    vertices[(indexData[i + 1] * vertexSize) + normalMapCoordI],
                    vertices[(indexData[i + 1] * vertexSize) + normalMapCoordI + 1],
                    vertices[(indexData[i + 1] * vertexSize) + normalMapCoordI + 2]);
                // Vertex 3 on plane
                Vector3 uvC = new Vector3(
                    vertices[(indexData[i + 2] * vertexSize) + normalMapCoordI],
                    vertices[(indexData[i + 2] * vertexSize) + normalMapCoordI + 1],
                    vertices[(indexData[i + 2] * vertexSize) + normalMapCoordI + 2]);

                //
                // Tangent calculation
                //
                Vector3 edge1 = b - a;
                Vector3 edge2 = c - a;
                Vector2 delta1 = (Vector2)uvB - (Vector2)uvA;
                Vector2 delta2 = (Vector2)uvC - (Vector2)uvA;

                double f = 1.0 / ((delta1.X * delta2.Y) - (delta2.X * delta1.Y));

                // Calculate tangent based on the points on the plane
                // and the locations of the noraml texture coords
                Vector3 t = new Vector3(
                    f * ((delta2.Y * edge1.X) - (delta1.Y * edge2.X)),
                    f * ((delta2.Y * edge1.Y) - (delta1.Y * edge2.Y)),
                    f * ((delta2.Y * edge1.Z) - (delta1.Y * edge2.Z)));

                //
                // Point 1
                //
                // Add positions back to first point on plane
                newVerts.AddRange(new double[] { a.X, a.Y, a.Z });
                // Add any other data that was part of the origianl
                for (int ia = 3; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i] * vertexSize) + ia]);
                }
                // Add tangent attribute
                newVerts.AddRange(new double[] { t.X, t.Y, t.Z });
                // Add index of point to indices
                newIndices.Add((uint)i);

                //
                // Point 2
                //
                // Add positions back to second point on plane
                newVerts.AddRange(new double[] { b.X, b.Y, b.Z });
                // Add any other data that was part of the origianl
                for (int ia = 3; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i + 1] * vertexSize) + ia]);
                }
                // Add tangent attribute
                newVerts.AddRange(new double[] { t.X, t.Y, t.Z });
                // Add index of point to indices
                newIndices.Add((uint)i + 1);

                //
                // Point 3
                //
                // Add positions back to third point on plane
                newVerts.AddRange(new double[] { c.X, c.Y, c.Z });
                // Add any other data that was part of the origianl
                for (int ia = 3; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i + 2] * vertexSize) + ia]);
                }
                // Add tangent attribute
                newVerts.AddRange(new double[] { t.X, t.Y, t.Z });
                // Add index of point to indices
                newIndices.Add((uint)i + 2);
            }
        }

        // Point3
        /// <summary>
        /// Add noramls to a list of 3 dimensional vertex data stored as <see cref="Point3"/>.
        /// </summary>
        /// <param name="vertices">The original vertex data</param>
        /// <param name="vertexSize">The number of values per vertex, including the positions</param>
        /// <param name="indexData">The original index data</param>
        /// <param name="newVerts">The list of new vertices with noramls at the end of each vertex</param>
        /// <param name="newIndices">The list of new indices</param>
        public static void AddNormals(Vector3[] vertices, byte vertexSize, uint[] indexData, out List<Vector3> newVerts, out List<uint> newIndices)
        {
            // The new list of data
            newVerts = new List<Vector3>();
            newIndices = new List<uint>();

            for (int i = 0; i < indexData.Length; i += 3)
            {
                // Point 1 on plane
                Vector3 a = vertices[indexData[i] * vertexSize];
                // Point 2 on plane
                Vector3 b = vertices[indexData[i + 1] * vertexSize];
                // Point 3 on plane
                Vector3 c = vertices[indexData[i + 2] * vertexSize];

                // Calculate normal based on points on the plane
                Vector3 n = Vector3.PlaneNormal(a, b, c);

                //
                // Point 1
                //
                // Add positions back to first point on plane
                newVerts.Add(a);
                // Add any other data that was part of the origianl
                for (int ia = 1; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i] * vertexSize) + ia]);
                }
                // Add normal attribute
                newVerts.Add(n);
                // Add index of point to indices
                newIndices.Add((uint)i);

                //
                // Point 2
                //
                // Add positions back to second point on plane
                newVerts.Add(b);
                // Add any other data that was part of the origianl
                for (int ia = 1; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i + 1] * vertexSize) + ia]);
                }
                // Add normal attribute
                newVerts.Add(n);
                // Add index of point to indices
                newIndices.Add((uint)i + 1);

                //
                // Point 3
                //
                // Add positions back to third point on plane
                newVerts.Add(c);
                // Add any other data that was part of the origianl
                for (int ia = 1; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i + 2] * vertexSize) + ia]);
                }
                // Add normal attribute
                newVerts.Add(n);
                // Add index of point to indices
                newIndices.Add((uint)i + 2);
            }
        }

        /// <summary>
        /// Add noramls and tangents to a list of 3 dimensional vertex data stored as <see cref="Vector3"/>.
        /// </summary>
        /// <param name="vertices">The original vertex data</param>
        /// <param name="vertexSize">The number of values per vertex, including the positions</param>
        /// <param name="nomalMapCoordI">The location per vertex where the normal map coodinates start</param>
        /// <param name="indexData">The original index data</param>
        /// <param name="newVerts">The list of new vertices with noramls then tangents at the end of each vertex</param>
        /// <param name="newIndices">The list of new indices</param>
        public static void AddNormalTangents(Vector3[] vertices, byte vertexSize, uint nomalMapCoordI, uint[] indexData, out List<Vector3> newVerts, out List<uint> newIndices)
        {
            // The new list of data
            newVerts = new List<Vector3>();
            newIndices = new List<uint>();

            for (int i = 0; i < indexData.Length; i += 3)
            {
                //
                // Positions
                //
                // Vertex 1 on plane
                Vector3 a = vertices[indexData[i] * vertexSize];
                // Vertex 2 on plane
                Vector3 b = vertices[indexData[i + 1] * vertexSize];
                // Vertex 3 on plane
                Vector3 c = vertices[indexData[i + 2] * vertexSize];

                //
                // Normal Map Coords
                //
                // Vertex 1 on plane
                Vector3 uvA = vertices[(indexData[i] * vertexSize) + nomalMapCoordI];
                // Vertex 2 on plane
                Vector3 uvB = vertices[(indexData[i + 1] * vertexSize) + nomalMapCoordI];
                // Vertex 3 on plane
                Vector3 uvC = vertices[(indexData[i + 2] * vertexSize) + nomalMapCoordI];

                // Calculate normal based on points on the plane
                Vector3 n = Vector3.PlaneNormal(a, b, c);

                //
                // Tangent calculation
                //
                Vector3 edge1 = b - a;
                Vector3 edge2 = c - a;
                Vector2 delta1 = (Vector2)uvB - (Vector2)uvA;
                Vector2 delta2 = (Vector2)uvC - (Vector2)uvA;

                double f = 1.0 / ((delta1.X * delta2.Y) - (delta2.X * delta1.Y));

                // Calculate tangent based on the points on the plane
                // and the locations of the noraml texture coords
                Vector3 t = new Vector3(
                    f * ((delta2.Y * edge1.X) - (delta1.Y * edge2.X)),
                    f * ((delta2.Y * edge1.Y) - (delta1.Y * edge2.Y)),
                    f * ((delta2.Y * edge1.Z) - (delta1.Y * edge2.Z)));

                //
                // Point 1
                //
                // Add positions back to first point on plane
                newVerts.Add(a);
                // Add any other data that was part of the origianl
                for (int ia = 1; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i] * vertexSize) + ia]);
                }
                // Add normal attribute
                newVerts.Add(n);
                // Add tangent attribute
                newVerts.Add(t);
                // Add index of point to indices
                newIndices.Add((uint)i);

                //
                // Point 2
                //
                // Add positions back to second point on plane
                newVerts.Add(b);
                // Add any other data that was part of the origianl
                for (int ia = 1; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i + 1] * vertexSize) + ia]);
                }
                // Add normal attribute
                newVerts.Add(n);
                // Add tangent attribute
                newVerts.Add(t);
                // Add index of point to indices
                newIndices.Add((uint)i + 1);

                //
                // Point 3
                //
                // Add positions back to third point on plane
                newVerts.Add(c);
                // Add any other data that was part of the origianl
                for (int ia = 1; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i + 2] * vertexSize) + ia]);
                }
                // Add normal attribute
                newVerts.Add(n);
                // Add tangent attribute
                newVerts.Add(t);
                // Add index of point to indices
                newIndices.Add((uint)i + 2);
            }
        }

        /// <summary>
        /// Add noramls and tangents to a list of 3 dimensional vertex data stored as <see cref="Vector3"/>.
        /// </summary>
        /// <param name="vertices">The original vertex data</param>
        /// <param name="vertexSize">The number of values per vertex, including the positions</param>
        /// <param name="nomalMapCoordI">The location per vertex where the normal map coodinates start</param>
        /// <param name="indexData">The original index data</param>
        /// <param name="newVerts">The list of new vertices with tangents at the end of each vertex</param>
        /// <param name="newIndices">The list of new indices</param>
        public static void AddTangents(Vector3[] vertices, byte vertexSize, uint nomalMapCoordI, uint[] indexData, out List<Vector3> newVerts, out List<uint> newIndices)
        {
            // The new list of data
            newVerts = new List<Vector3>();
            newIndices = new List<uint>();

            for (int i = 0; i < indexData.Length; i += 3)
            {
                //
                // Positions
                //
                // Vertex 1 on plane
                Vector3 a = vertices[indexData[i] * vertexSize];
                // Vertex 2 on plane
                Vector3 b = vertices[indexData[i + 1] * vertexSize];
                // Vertex 3 on plane
                Vector3 c = vertices[indexData[i + 2] * vertexSize];

                //
                // Normal Map Coords
                //
                // Vertex 1 on plane
                Vector3 uvA = vertices[(indexData[i] * vertexSize) + nomalMapCoordI];
                // Vertex 2 on plane
                Vector3 uvB = vertices[(indexData[i + 1] * vertexSize) + nomalMapCoordI];
                // Vertex 3 on plane
                Vector3 uvC = vertices[(indexData[i + 2] * vertexSize) + nomalMapCoordI];

                //
                // Tangent calculation
                //
                Vector3 edge1 = b - a;
                Vector3 edge2 = c - a;
                Vector2 delta1 = (Vector2)uvB - (Vector2)uvA;
                Vector2 delta2 = (Vector2)uvC - (Vector2)uvA;

                double f = 1.0 / ((delta1.X * delta2.Y) - (delta2.X * delta1.Y));

                // Calculate tangent based on the points on the plane
                // and the locations of the noraml texture coords
                Vector3 t = new Vector3(
                    f * ((delta2.Y * edge1.X) - (delta1.Y * edge2.X)),
                    f * ((delta2.Y * edge1.Y) - (delta1.Y * edge2.Y)),
                    f * ((delta2.Y * edge1.Z) - (delta1.Y * edge2.Z)));

                //
                // Point 1
                //
                // Add positions back to first point on plane
                newVerts.Add(a);
                // Add any other data that was part of the origianl
                for (int ia = 1; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i] * vertexSize) + ia]);
                }
                // Add tangent attribute
                newVerts.Add(t);
                // Add index of point to indices
                newIndices.Add((uint)i);

                //
                // Point 2
                //
                // Add positions back to second point on plane
                newVerts.Add(b);
                // Add any other data that was part of the origianl
                for (int ia = 1; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i + 1] * vertexSize) + ia]);
                }
                // Add tangent attribute
                newVerts.Add(t);
                // Add index of point to indices
                newIndices.Add((uint)i + 1);

                //
                // Point 3
                //
                // Add positions back to third point on plane
                newVerts.Add(c);
                // Add any other data that was part of the origianl
                for (int ia = 1; ia < vertexSize; ia++)
                {
                    newVerts.Add(vertices[(indexData[i + 2] * vertexSize) + ia]);
                }
                // Add tangent attribute
                newVerts.Add(t);
                // Add index of point to indices
                newIndices.Add((uint)i + 2);
            }
        }
    }
}
