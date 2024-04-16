using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public class RenderState
    {
        protected internal bool Locked { get; set; } = false;

        public bool Current => GL.context.renderState == this;

        public static RenderState Default { get; } = new RenderState();

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

        internal bool postMatrixMods = true;
        /// <summary>
        /// Determines whether changes to matrices made by draw functions should be applied before or after the already set value.
        /// </summary>
        /// <remarks>
        /// True for multiplications to be applied after, False for before.
        /// </remarks>
        public bool PostMatrixMods
        {
            get => postMatrixMods;
            set
            {
                if (Locked) { return; }

                postMatrixMods = value;
            }
        }
    }
}
