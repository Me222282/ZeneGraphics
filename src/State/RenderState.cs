using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public enum MatrixMods : byte
    {
        /// <summary>
        /// Draw functions ignore the inital state of <see cref="IDrawingContext.Model" />.
        /// </summary>
        None,
        /// <summary>
        /// Draw functions apply model multiplications before <see cref="IDrawingContext.Model" />.
        /// </summary>
        Pre,
        /// <summary>
        /// Draw functions apply model multiplications after <see cref="IDrawingContext.Model" />.
        /// </summary>
        Post
    }
    
    public class RenderState
    {
        protected internal bool Locked { get; set; } = false;

        public bool Current => GL.context.renderState == this;

        public static RenderState Default { get; } = new RenderState();
        public static RenderState BlendReady => new RenderState()
        {
            Blending = true,
            SourceScaleBlending = BlendFunction.SourceAlpha,
            DestinationScaleBlending = BlendFunction.OneMinusSourceAlpha
        };

        internal PolygonMode polygonMode = PolygonMode.Fill;
        /// <summary>
        /// Select a polygon rasterization mode.
        /// </summary>
        public virtual PolygonMode PolygonMode
        {
            get => polygonMode;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    polygonMode = value;
                    return;
                }

                GL.PolygonMode(GLEnum.FrontAndBack, value);
            }
        }

        internal BlendFunction ssb = BlendFunction.One;
        /// <summary>
        /// The blend function applied to the source colour.
        /// </summary>
        public virtual BlendFunction SourceScaleBlending
        {
            get => ssb;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    ssb = value;
                    return;
                }

                GL.BlendFunc(value, dsb);
            }
        }

        internal BlendFunction dsb = BlendFunction.Zero;
        /// <summary>
        /// The blend function applied to the destination colour.
        /// </summary>
        public virtual BlendFunction DestinationScaleBlending
        {
            get => dsb;
            set
            {
                if (Locked) { return; }

                if (!Current)
                {
                    dsb = value;
                    return;
                }

                GL.BlendFunc(ssb, value);
            }
        }

        internal bool blending = false;
        /// <summary>
        /// Determines whether to blend the computed fragment colour values with the values in the colour buffers.
        /// </summary>
        public virtual bool Blending
        {
            get => blending;
            set
            {
                if (Locked) { return; }

                blending = value;
                if (!Current) { return; }

                if (value)
                {
                    GL.Enable(GLEnum.Blend);
                    return;
                }

                GL.Disable(GLEnum.Blend);
            }
        }

        internal bool faceCulling = false;
        /// <summary>
        /// Determines whether to cull polygons based on their winding in window coordinates.
        /// </summary>
        public virtual bool FaceCulling
        {
            get => faceCulling;
            set
            {
                if (Locked) { return; }

                faceCulling = value;
                if (!Current) { return; }

                if (value)
                {
                    GL.Enable(GLEnum.CullFace);
                    return;
                }

                GL.Disable(GLEnum.CullFace);
            }
        }

        private MatrixMods matrixMods = MatrixMods.Post;
        /// <summary>
        /// Determines how changes to matrices made by draw functions should manage their inital values.
        /// </summary>
        /// <remarks>
        /// The default value is <see cref="MatrixMods.Post" />.
        /// </remarks>
        public MatrixMods MatrixMods
        {
            get => matrixMods;
            set
            {
                if (Locked) { return; }

                matrixMods = value;
                modelMatrix = matrixMods switch
                {
                    MatrixMods.Pre => PreMatrixModsFunc,
                    MatrixMods.Post => PostMatrixModsFunc,
                    _ => NoMatrixModsFunc,
                };
            }
        }
        
        internal Func<IMatrix, Matrix4, IMatrix> modelMatrix = PostMatrixModsFunc;
        private static IMatrix PostMatrixModsFunc(IMatrix init, Matrix4 n)
        {
            Matrix4 m;
            if (init is Matrix4 mod) { m = mod; }
            else { m = new Matrix4(init); }
            
            return m * n;
        }
        private static IMatrix PreMatrixModsFunc(IMatrix init, Matrix4 n)
        {
            Matrix4 m;
            if (init is Matrix4 mod) { m = mod; }
            else { m = new Matrix4(init); }
            
            return n * m;
        }
        private static IMatrix NoMatrixModsFunc(IMatrix init, Matrix4 n) => n;
    }
}
