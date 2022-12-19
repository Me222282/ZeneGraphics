using System;
using Zene.Graphics.Base;
using Zene.Structs;

namespace Zene.Graphics
{
    public enum Target : uint
    {
        Framebuffer = GLEnum.Framebuffer,
        ReadFramebuffer = GLEnum.ReadFramebuffer,
        DrawFramebuffer = GLEnum.DrawFramebuffer,
        Renderbuffer = GLEnum.Renderbuffer,

        Texture1D = GLEnum.Texture1d,
        Texture1DArray = GLEnum.Texture1dArray,
        Texture2D = GLEnum.Texture2d,
        Texture2DArray = GLEnum.Texture2dArray,
        Texture3D = GLEnum.Texture3d,
        Texture2DMS = GLEnum.Texture2dMultisample,
        Texture2DArrayMS = GLEnum.Texture2dMultisampleArray,
        TextureCubeMap = GLEnum.TextureCubeMap,
        TextureCubeMapArray = GLEnum.TextureCubeMapArray,
        TextureRectangle = GLEnum.TextureRectangle,

        TextureBuffer = GLEnum.TextureBuffer,

        BufferArray = GLEnum.ArrayBuffer,
        BufferAtomicCounter = GLEnum.AtomicCounterBuffer,
        BufferCopyRead = GLEnum.CopyReadBuffer,
        BufferCopyWrite = GLEnum.CopyWriteBuffer,
        BufferDispatchIndirect = GLEnum.DispatchIndirectBuffer,
        BufferDrawIndirect = GLEnum.DrawIndirectBuffer,
        BufferElementArray = GLEnum.ElementArrayBuffer,
        BufferPixelPack = GLEnum.PixelPackBuffer,
        BufferPixelUnPack = GLEnum.PixelUnpackBuffer,
        BufferQuery = GLEnum.QueryBuffer,
        BufferShaderStorage = GLEnum.ShaderStorageBuffer,
        BufferTransformFeedback = GLEnum.TransformFeedbackBuffer,
        BufferUniform = GLEnum.UniformBuffer,

        VertexArray = GLEnum.VertexArray
    }

    public enum DepthFunction : uint
    {
        Never = GLEnum.Never,
        Less = GLEnum.Less,
        Equal = GLEnum.Equal,
        LessEqual = GLEnum.Lequal,
        GreaterEqual = GLEnum.Gequal,
        NotEqual = GLEnum.Notequal,
        Always = GLEnum.Always
    }

    public enum PolygonMode : uint
    {
        Point = GLEnum.Point,
        Line = GLEnum.Line,
        Fill = GLEnum.Fill,
    }

    public enum BlendFunction : uint
    {
        Zero = GLEnum.Zero,
        One = GLEnum.One,
        SourceColour = GLEnum.SrcColour,
        OneMinusSourceColour = GLEnum.OneMinusSrcColour,
        DestinationColour = GLEnum.DstColour,
        OneMinusDestinationColour = GLEnum.OneMinusDstColour,
        SourceAlpha = GLEnum.SrcAlpha,
        OneMinusSourceAlpha = GLEnum.OneMinusSrcAlpha,
        DestinationAlpha = GLEnum.DstAlpha,
        OneMinusDestinationAlpha = GLEnum.OneMinusDstAlpha,
        ConstantColour = GLEnum.ConstantColour,
        OneMinusConstantColour = GLEnum.OneMinusConstantColour,
        ConstantAlpha = GLEnum.ConstantAlpha,
        OneMinusConstantAlpha = GLEnum.OneMinusConstantAlpha,
        SourceAlphaSaturate = GLEnum.SrcAlphaSaturate,
        SourceOneColour = GLEnum.Src1Colour,
        OneMinusSourceOneColour = GLEnum.OneMinusSrc1Colour,
        SourceOneAlpha = GLEnum.Src1Alpha,
        OneMinusSourceOneAlpha = GLEnum.OneMinusSrc1Alpha
    }

    public static unsafe class State
    {
        public static void Init(Func<string, IntPtr> func, double version)
        {
            if (Initialised && version <= GL.Version) { return; }

            GL.Init(func, version);

            SetConstants();

            Initialised = true;
        }

        public static bool Initialised { get; private set; } = false;

        public static GraphicsContext CurrentContext
        {
            get => GL.context;
            set => GL.context = value;
        }

        public static DepthState DepthState
        {
            get => GL.context.depth;
            set => GL.SetDepthState(value);
        }
        public static Viewport Viewport
        {
            get => GL.context.viewport;
            set => GL.SetViewState(value);
        }

        /// <summary>
        /// The render area used by any draws to the bound framebuffer.
        /// </summary>
        public static RectangleI DrawView
        {
            get => GL.context.viewport.view;
            set => GL.context.viewport.View = value;
        }
        /// <summary>
        /// The size of the render area used by any draws to the bound framebuffer.
        /// </summary>
        public static Vector2I DrawViewSize
        {
            get => GL.context.viewport.Size;
            set => GL.context.viewport.Size = value;
        }
        /// <summary>
        /// The location of the render area used by any draws to the bound framebuffer.
        /// </summary>
        public static Vector2I DrawViewLocation
        {
            get => GL.context.viewport.Location;
            set => GL.context.viewport.Location = value;
        }

        /// <summary>
        /// Specify the function used for depth buffer comparisons.
        /// </summary>
        public static DepthFunction DepthFunction
        {
            get => GL.context.depth.func;
            set => GL.context.depth.Function = value;
        }
        /// <summary>
        /// Select a polygon rasterization mode.
        /// </summary>
        public static PolygonMode PolygonMode
        {
            get => GL.context.polygonMode;
            set => GL.PolygonMode(GLEnum.FrontAndBack, value);
        }

        /// <summary>
        /// Get or set blend function applied to the source colour.
        /// </summary>
        public static BlendFunction SourceScaleBlending
        {
            get => GL.context.sfactorBlendFunc;
            set => GL.BlendFunc(value, GL.context.dfactorBlendFunc);
        }
        /// <summary>
        /// Get or set blend function applied to the destination colour.
        /// </summary>
        public static BlendFunction DestinationScaleBlending
        {
            get => GL.context.dfactorBlendFunc;
            set => GL.BlendFunc(GL.context.sfactorBlendFunc, value);
        }

        /// <summary>
        /// Determines whether to blend the computed fragment colour values with the values in the colour buffers.
        /// </summary>
        public static bool Blending
        {
            get
            {
                return GL.IsEnabled(GLEnum.Blend);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.Blend);
                    return;
                }

                GL.Disable(GLEnum.Blend);
            }
        }
        /// <summary>
        /// Determines whether to apply the currently selected logical operation to the computed fragment colour and colour buffer values.
        /// </summary>
        public static bool LogicOPColour
        {
            get
            {
                return GL.IsEnabled(GLEnum.ColourLogicOp);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.ColourLogicOp);
                    return;
                }

                GL.Disable(GLEnum.ColourLogicOp);
            }
        }
        /// <summary>
        /// Determines whether to cull polygons based on their winding in window coordinates.
        /// </summary>
        public static bool FaceCulling
        {
            get
            {
                return GL.IsEnabled(GLEnum.CullFace);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.CullFace);
                    return;
                }

                GL.Disable(GLEnum.CullFace);
            }
        }
        /// <summary>
        /// Determines whether debug messages are produced by a debug context.
        /// </summary>
        public static bool OutputDebug
        {
            get
            {
                return GL.IsEnabled(GLEnum.DebugOutput);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.DebugOutput);
                    return;
                }

                GL.Disable(GLEnum.DebugOutput);
            }
        }
        /// <summary>
        /// Determines whether debug messages are produced synchronously by a debug context.
        /// </summary>
        public static bool OutputDebugSynchronous
        {
            get
            {
                return GL.IsEnabled(GLEnum.DebugOutputSynchronous);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.DebugOutputSynchronous);
                    return;
                }

                GL.Disable(GLEnum.DebugOutputSynchronous);
            }
        }
        /// <summary>
        /// Determines whether there is near or far plane clipping.
        /// </summary>
        public static bool ClampDepth
        {
            get => GL.context.depth.clamp;
            set => GL.context.depth.Clamp = value;
        }
        /// <summary>
        /// Determines whether to do depth comparisons and update the depth buffer.
        /// </summary>
        public static bool DepthTesting
        {
            get => GL.context.depth.testing;
            set => GL.context.depth.Testing = value;
        }
        /// <summary>
        /// Enable or disable writing into the depth buffer.
        /// </summary>
        public static bool DepthMask
        {
            get => GL.context.depth.mask;
            set => GL.context.depth.Mask = value;
        }
        /// <summary>
        /// Determines whether to dither colour components or indices before they are written to the colour buffer.
        /// </summary>
        public static bool Dither
        {
            get
            {
                return GL.IsEnabled(GLEnum.Dither);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.Dither);
                    return;
                }

                GL.Disable(GLEnum.Dither);
            }
        }
        /// <summary>
        /// Determines whether the R, G, and B destination colour values (after conversion from fixed-point to floating-point)
        /// are considered to be encoded for the sRGB colour space and hence are linearized prior to their use in blending.
        /// </summary>
        public static bool FramebufferSRGB
        {
            get
            {
                return GL.IsEnabled(GLEnum.FramebufferSrgb);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.FramebufferSrgb);
                    return;
                }

                GL.Disable(GLEnum.FramebufferSrgb);
            }
        }
        /// <summary>
        /// Determines whether to draw lines with correct filtering. Otherwise, draw aliased lines.
        /// </summary>
        public static bool SmoothLines
        {
            get
            {
                return GL.IsEnabled(GLEnum.LineSmooth);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.LineSmooth);
                    return;
                }

                GL.Disable(GLEnum.LineSmooth);
            }
        }
        /// <summary>
        /// Determines whether to use multiple fragment samples in computing the final colour of a pixel.
        /// </summary>
        public static bool Multisampleing
        {
            get
            {
                return GL.IsEnabled(GLEnum.Multisample);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.Multisample);
                    return;
                }

                GL.Disable(GLEnum.Multisample);
            }
        }
        /// <summary>
        /// Determines whether an offset is added to depth values of a polygon's fragments before the depth comparison is performed.
        /// </summary>
        public static bool PolygonFillOffset
        {
            get
            {
                return GL.IsEnabled(GLEnum.PolygonOffsetFill);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.PolygonOffsetFill);
                    return;
                }

                GL.Disable(GLEnum.PolygonOffsetFill);
            }
        }
        /// <summary>
        /// Determines whether an offset is added to depth values of a polygon's fragments before the depth comparison is performed.
        /// </summary>
        public static bool PolygonLineOffset
        {
            get
            {
                return GL.IsEnabled(GLEnum.PolygonOffsetLine);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.PolygonOffsetLine);
                    return;
                }

                GL.Disable(GLEnum.PolygonOffsetLine);
            }
        }
        /// <summary>
        /// Determines whether an offset is added to depth values of a polygon's fragments before the depth comparison is performed.
        /// </summary>
        public static bool PolygonPointOffset
        {
            get
            {
                return GL.IsEnabled(GLEnum.PolygonOffsetPoint);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.PolygonOffsetPoint);
                    return;
                }

                GL.Disable(GLEnum.PolygonOffsetPoint);
            }
        }
        /// <summary>
        /// Determines whether to draw polygons with proper filtering. Otherwise, draw aliased polygons.
        /// </summary>
        public static bool SmoothPolygons
        {
            get
            {
                return GL.IsEnabled(GLEnum.PolygonSmooth);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.PolygonSmooth);
                    return;
                }

                GL.Disable(GLEnum.PolygonSmooth);
            }
        }
        /// <summary>
        /// Determines whether GL will restart the primitive when the index of the vertex is equal to the primitive restart index
        /// when any one of the draw commands which transfers a set of generic attribute array elements.
        /// </summary>
        public static bool PrimitiveRestart
        {
            get
            {
                return GL.IsEnabled(GLEnum.PrimitiveRestart);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.PrimitiveRestart);
                    return;
                }

                GL.Disable(GLEnum.PrimitiveRestart);
            }
        }
        /// <summary>
        /// Determines whether GL will restart the primitive when the index of the vertex is equal to the primitive restart index
        /// when any one of the draw commands which transfers a set of generic attribute array elements.
        /// </summary>
        public static bool FixedIndexPrimitiveRestart
        {
            get
            {
                return GL.IsEnabled(GLEnum.PrimitiveRestartFixedIndex);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.PrimitiveRestartFixedIndex);
                    return;
                }

                GL.Disable(GLEnum.PrimitiveRestartFixedIndex);
            }
        }
        /// <summary>
        /// Determines whether before rasterization, primitives are discarded after the optional transform feedback stage.
        /// </summary>
        public static bool RasterizerDiscarding
        {
            get
            {
                return GL.IsEnabled(GLEnum.RasterizerDiscard);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.RasterizerDiscard);
                    return;
                }

                GL.Disable(GLEnum.RasterizerDiscard);
            }
        }
        /// <summary>
        /// Determines whether to compute a temporary coverage value where each bit is determined by the alpha value at the corresponding sample location.
        /// </summary>
        public static bool SampleAlphaToCoverage
        {
            get
            {
                return GL.IsEnabled(GLEnum.SampleAlphaToCoverage);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.SampleAlphaToCoverage);
                    return;
                }

                GL.Disable(GLEnum.SampleAlphaToCoverage);
            }
        }
        /// <summary>
        /// Determines whether each sample alpha value is replaced by the maximum representable alpha value.
        /// </summary>
        public static bool SampleAlphaToOne
        {
            get
            {
                return GL.IsEnabled(GLEnum.SampleAlphaToOne);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.SampleAlphaToOne);
                    return;
                }

                GL.Disable(GLEnum.SampleAlphaToOne);
            }
        }
        /// <summary>
        /// Determines whether the fragment's coverage is ANDed with the temporary coverage value.
        /// </summary>
        public static bool SampleCoverage
        {
            get
            {
                return GL.IsEnabled(GLEnum.SampleCoverage);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.SampleCoverage);
                    return;
                }

                GL.Disable(GLEnum.SampleCoverage);
            }
        }
        /// <summary>
        /// Determines whether the sample coverage mask generated for a fragment during rasterization will be ANDed before shading occurs.
        /// </summary>
        public static bool SampleShading
        {
            get
            {
                return GL.IsEnabled(GLEnum.SampleShading);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.SampleShading);
                    return;
                }

                GL.Disable(GLEnum.SampleShading);
            }
        }
        /// <summary>
        /// Determines whether to discard fragments that are outside the scissor rectangle.
        /// </summary>
        public static bool ScissorTesting
        {
            get
            {
                return GL.IsEnabled(GLEnum.ScissorTest);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.ScissorTest);
                    return;
                }

                GL.Disable(GLEnum.ScissorTest);
            }
        }
        /// <summary>
        /// Determines whether to do stencil testing and update the stencil buffer.
        /// </summary>
        public static bool StencilTesting
        {
            get
            {
                return GL.IsEnabled(GLEnum.StencilTest);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.StencilTest);
                    return;
                }

                GL.Disable(GLEnum.StencilTest);
            }
        }
        /// <summary>
        /// Determines whether cubemap textures are sampled such that when linearly sampling from the border between two adjacent faces,
        /// texels from both faces are used to generate the final sample value.
        /// </summary>
        public static bool SeamlessCubeMaps
        {
            get
            {
                return GL.IsEnabled(GLEnum.TextureCubeMapSeamless);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.TextureCubeMapSeamless);
                    return;
                }

                GL.Disable(GLEnum.TextureCubeMapSeamless);
            }
        }
        /// <summary>
        /// Determines whether then the derived point size is taken from the (potentially clipped)
        /// shader builtin gl_PointSize and clamped to the implementation-dependent point size range.
        /// </summary>
        public static bool ProgramPointSize
        {
            get
            {
                return GL.IsEnabled(GLEnum.ProgramPointSize);
            }
            set
            {
                if (value)
                {
                    GL.Enable(GLEnum.ProgramPointSize);
                    return;
                }

                GL.Disable(GLEnum.ProgramPointSize);
            }
        }

        /// <summary>
        /// Force execution of GL commands in finite time.
        /// </summary>
        public static void Flush()
        {
            GL.Flush();
        }
        /// <summary>
        /// Block until all GL execution is complete.
        /// </summary>
        public static void Finish()
        {
            GL.Finish();
        }

        /// <summary>
        /// Gets the Id of the currently bound texture.
        /// </summary>
        /// <param name="target">The texture target to test.</param>
        /// <returns></returns>
        public static ITexture GetBoundTexture(uint unit, TextureTarget target)
        {
            return target switch
            {
                TextureTarget.Texture1D => GL.context.boundTextures[unit].Texture1D,
                TextureTarget.Texture1DArray => GL.context.boundTextures[unit].Texture1DArray,
                TextureTarget.Texture2D => GL.context.boundTextures[unit].Texture2D,
                TextureTarget.Texture2DArray => GL.context.boundTextures[unit].Texture2DArray,
                TextureTarget.Texture3D => GL.context.boundTextures[unit].Texture3D,
                TextureTarget.Rectangle => GL.context.boundTextures[unit].Rectangle,
                TextureTarget.CubeMap => GL.context.boundTextures[unit].CubeMap,
                TextureTarget.CubeMapArray => GL.context.boundTextures[unit].CubeMapArray,
                TextureTarget.Multisample2D => GL.context.boundTextures[unit].Texture2DMS,
                TextureTarget.Multisample2DArray => GL.context.boundTextures[unit].Texture2DArrayMS,
                TextureTarget.Buffer => GL.context.boundTextures[unit].Buffer,
                _ => null
            };
        }
        /// <summary>
        /// Gets the Id of the currently bound framebufer.
        /// </summary>
        /// <param name="target">The framebuffer target to test.</param>
        /// <returns></returns>
        public static IFramebuffer GetBoundFramebuffer(FrameTarget target)
        {
            return target switch
            {
                FrameTarget.FrameBuffer => GL.context.boundFrameBuffers.Draw,
                FrameTarget.Read => GL.context.boundFrameBuffers.Read,
                FrameTarget.Draw => GL.context.boundFrameBuffers.Draw,
                _ => null
            };
        }
        /// <summary>
        /// Gets the Id of the currently bound renderbuffer.
        /// </summary>
        /// <returns></returns>
        public static IRenderbuffer GetBoundRenderbuffer()
        {
            return GL.context.boundRenderbuffer;
        }
        /// <summary>
        /// Gets the Id of the currently bound shader program.
        /// </summary>
        /// <returns></returns>
        public static IShaderProgram GetBoundShaderProgram()
        {
            return GL.context.boundShaderProgram;
        }
        /// <summary>
        /// Gets the Id of the currently bound buffer object.
        /// </summary>
        /// <returns></returns>
        public static IBuffer GetBoundBuffer(BufferTarget target)
        {
            return target switch
            {
                BufferTarget.Array => GL.context.boundBuffers.Array,
                BufferTarget.AtomicCounter => GL.context.boundBuffers.AtomicCounter[0],
                BufferTarget.CopyRead => GL.context.boundBuffers.CopyRead,
                BufferTarget.CopyWrite => GL.context.boundBuffers.CopyWrite,
                BufferTarget.DispatchIndirect => GL.context.boundBuffers.DispatchIndirect,
                BufferTarget.DrawIndirect => GL.context.boundBuffers.DrawIndirect,
                BufferTarget.ElementArray => GL.context.boundBuffers.ElementArray,
                BufferTarget.PixelPack => GL.context.boundBuffers.PixelPack,
                BufferTarget.PixelUnPack => GL.context.boundBuffers.PixelUnpack,
                BufferTarget.Query => GL.context.boundBuffers.Query,
                BufferTarget.ShaderStorage => GL.context.boundBuffers.ShaderStorage[0],
                BufferTarget.Texture => GL.context.boundBuffers.Texture,
                BufferTarget.TransformFeedback => GL.context.boundBuffers.TransformFeedback[0],
                BufferTarget.Uniform => GL.context.boundBuffers.Uniform[0],
                _ => null
            };
        }
        /// <summary>
        /// Gets the Id of the currently bound buffer object.
        /// </summary>
        /// <returns></returns>
        public static IBuffer GetBoundIndexedBuffer(BufferTarget target, int index)
        {
            return target switch
            {
                BufferTarget.AtomicCounter => GL.context.boundBuffers.AtomicCounter[index],
                BufferTarget.ShaderStorage => GL.context.boundBuffers.ShaderStorage[index],
                BufferTarget.TransformFeedback => GL.context.boundBuffers.TransformFeedback[index],
                BufferTarget.Uniform => GL.context.boundBuffers.Uniform[index],
                _ => null
            };
        }
        /// <summary>
        /// Gets the Id of the currently bound vertex array.
        /// </summary>
        /// <returns></returns>
        public static IVertexArray GetBoundVertexArray()
        {
            return GL.context.boundVertexArray;
        }
        /// <summary>
        /// Gets or sets the active texture referance.
        /// </summary>
        /// <returns></returns>
        public static uint ActiveTexture
        {
            get => GL.context.activeTextureUnit;
            set => GL.ActiveTexture(value + GLEnum.Texture0);
        }

        /// <summary>
        /// Sets the binding for <paramref name="target"/> to 0.
        /// </summary>
        /// <param name="target">The target to nullify.</param>
        public static void NullBind(Target target)
        {
            switch (target)
            {
                case Target.Texture1D:
                    if (GL.context.boundTextures[GL.context.activeTextureUnit].Texture1D == null) { return; }
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case Target.Texture1DArray:
                    if (GL.context.boundTextures[GL.context.activeTextureUnit].Texture1DArray == null) { return; }
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case Target.Texture2D:
                    if (GL.context.boundTextures[GL.context.activeTextureUnit].Texture2D == null) { return; }
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case Target.Texture2DArray:
                    if (GL.context.boundTextures[GL.context.activeTextureUnit].Texture2DArray == null) { return; }
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case Target.Texture3D:
                    if (GL.context.boundTextures[GL.context.activeTextureUnit].Texture3D == null) { return; }
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case Target.Texture2DMS:
                    if (GL.context.boundTextures[GL.context.activeTextureUnit].Texture2DMS == null) { return; }
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case Target.Texture2DArrayMS:
                    if (GL.context.boundTextures[GL.context.activeTextureUnit].Texture2DArrayMS == null) { return; }
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case Target.TextureCubeMap:
                    if (GL.context.boundTextures[GL.context.activeTextureUnit].CubeMap == null) { return; }
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case Target.TextureCubeMapArray:
                    if (GL.context.boundTextures[GL.context.activeTextureUnit].CubeMapArray == null) { return; }
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case Target.TextureRectangle:
                    if (GL.context.boundTextures[GL.context.activeTextureUnit].Rectangle == null) { return; }
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case Target.TextureBuffer:
                    if (GL.context.boundTextures[GL.context.activeTextureUnit].Buffer != null)
                    {
                        GL.BindTexture(GLEnum.TextureBuffer, null);
                    }
                    if (GL.context.boundBuffers.Texture != null)
                    {
                        GL.BindBuffer(GLEnum.TextureBuffer, null);
                    }
                    return;

                case Target.DrawFramebuffer:
                case Target.ReadFramebuffer:
                case Target.Framebuffer:
                    BaseFramebuffer.Bind((FrameTarget)target);
                    return;

                case Target.Renderbuffer:
                    if (GL.context.boundRenderbuffer == null) { return; }
                    GL.BindRenderbuffer(GLEnum.Renderbuffer, null);
                    return;


                case Target.VertexArray:
                    if (GL.context.boundVertexArray == GL.context.baseVertexArray) { return; }
                    GL.BindVertexArray(null);
                    return;

                case Target.BufferArray:
                    if (GL.context.boundBuffers.Array == null) { return; }
                    GL.BindBuffer(GLEnum.ArrayBuffer, null);
                    return;

                case Target.BufferAtomicCounter:
                    if (GL.context.boundBuffers.AtomicCounter[0] == null) { return; }
                    GL.BindBuffer(GLEnum.AtomicCounterBuffer, null);
                    return;

                case Target.BufferCopyRead:
                    if (GL.context.boundBuffers.CopyRead == null) { return; }
                    GL.BindBuffer(GLEnum.CopyReadBuffer, null);
                    return;

                case Target.BufferCopyWrite:
                    if (GL.context.boundBuffers.CopyWrite == null) { return; }
                    GL.BindBuffer(GLEnum.CopyWriteBuffer, null);
                    return;

                case Target.BufferDispatchIndirect:
                    if (GL.context.boundBuffers.DispatchIndirect == null) { return; }
                    GL.BindBuffer(GLEnum.DispatchIndirectBuffer, null);
                    return;

                case Target.BufferDrawIndirect:
                    if (GL.context.boundBuffers.DrawIndirect == null) { return; }
                    GL.BindBuffer(GLEnum.DrawIndirectBuffer, null);
                    return;

                case Target.BufferElementArray:
                    if (GL.context.boundBuffers.ElementArray == null) { return; }
                    GL.BindBuffer(GLEnum.ElementArrayBuffer, null);
                    return;

                case Target.BufferPixelPack:
                    if (GL.context.boundBuffers.PixelPack == null) { return; }
                    GL.BindBuffer(GLEnum.PixelPackBuffer, null);
                    return;

                case Target.BufferPixelUnPack:
                    if (GL.context.boundBuffers.PixelUnpack == null) { return; }
                    GL.BindBuffer(GLEnum.PixelUnpackBuffer, null);
                    return;

                case Target.BufferQuery:
                    if (GL.context.boundBuffers.Query == null) { return; }
                    GL.BindBuffer(GLEnum.QueryBuffer, null);
                    return;

                case Target.BufferShaderStorage:
                    if (GL.context.boundBuffers.ShaderStorage[0] == null) { return; }
                    GL.BindBuffer(GLEnum.ShaderStorageBuffer, null);
                    return;

                case Target.BufferTransformFeedback:
                    if (GL.context.boundBuffers.TransformFeedback[0] == null) { return; }
                    GL.BindBuffer(GLEnum.TransformFeedbackBuffer, null);
                    return;

                case Target.BufferUniform:
                    if (GL.context.boundBuffers.Uniform[0] == null) { return; }
                    GL.BindBuffer(GLEnum.UniformBuffer, null);
                    return;
            }
        }
        /// <summary>
        /// Sets the binding for <paramref name="target"/> at <paramref name="index"/> to 0.
        /// </summary>
        /// <param name="target">The target to nullify.</param>
        /// <param name="index">The index of target to nullify.</param>
        public static void NullBind(BufferTarget target, uint index)
        {
            switch (target)
            {
                case BufferTarget.AtomicCounter:
                    if (GL.context.boundBuffers.AtomicCounter[index] == null) { return; }
                    GL.BindBufferBase(GLEnum.AtomicCounterBuffer, index, null);
                    return;

                case BufferTarget.ShaderStorage:
                    if (GL.context.boundBuffers.ShaderStorage[index] == null) { return; }
                    GL.BindBufferBase(GLEnum.ShaderStorageBuffer, index, null);
                    return;

                case BufferTarget.TransformFeedback:
                    if (GL.context.boundBuffers.TransformFeedback[index] == null) { return; }
                    GL.BindBufferBase(GLEnum.TransformFeedbackBuffer, index, null);
                    return;

                case BufferTarget.Uniform:
                    if (GL.context.boundBuffers.Uniform[index] == null) { return; }
                    GL.BindBufferBase(GLEnum.UniformBuffer, index, null);
                    return;
            }
        }
        /// <summary>
        /// Sets the binding for <paramref name="target"/> to 0 at texture slot <paramref name="slot"/>.
        /// </summary>
        /// <param name="target">The texture target to nullify at <paramref name="slot"/>.</param>
        /// <param name="slot">The texture slot to nullify <paramref name="target"/> at.</param>
        public static void NullBind(TextureTarget target, uint slot)
        {
            switch (target)
            {
                case TextureTarget.Texture1D:
                    if (GL.context.boundTextures[slot].Texture1D == null) { return; }
                    GL.ActiveTexture(slot);
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case TextureTarget.Texture1DArray:
                    if (GL.context.boundTextures[slot].Texture1DArray == null) { return; }
                    GL.ActiveTexture(slot);
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case TextureTarget.Texture2D:
                    if (GL.context.boundTextures[slot].Texture2D == null) { return; }
                    GL.ActiveTexture(slot);
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case TextureTarget.Texture2DArray:
                    if (GL.context.boundTextures[slot].Texture2DArray == null) { return; }
                    GL.ActiveTexture(slot);
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case TextureTarget.Texture3D:
                    if (GL.context.boundTextures[slot].Texture3D == null) { return; }
                    GL.ActiveTexture(slot);
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case TextureTarget.Multisample2D:
                    if (GL.context.boundTextures[slot].Texture2DMS == null) { return; }
                    GL.ActiveTexture(slot);
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case TextureTarget.Multisample2DArray:
                    if (GL.context.boundTextures[slot].Texture2DArrayMS == null) { return; }
                    GL.ActiveTexture(slot);
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case TextureTarget.CubeMap:
                    if (GL.context.boundTextures[slot].CubeMap == null) { return; }
                    GL.ActiveTexture(slot);
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case TextureTarget.CubeMapArray:
                    if (GL.context.boundTextures[slot].CubeMapArray == null) { return; }
                    GL.ActiveTexture(slot);
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case TextureTarget.Rectangle:
                    if (GL.context.boundTextures[slot].Rectangle == null) { return; }
                    GL.ActiveTexture(slot);
                    GL.BindTexture(GLEnum.Texture1d, null);
                    return;

                case TextureTarget.Buffer:
                    if (GL.context.boundTextures[slot].Buffer != null)
                    {
                        GL.ActiveTexture(slot);
                        GL.BindTexture(GLEnum.TextureBuffer, null);
                    }
                    if (GL.context.boundBuffers.Texture != null)
                    {
                        GL.BindBuffer(GLEnum.TextureBuffer, null);
                    }
                    return;
            }
        }
        /// <summary>
        /// Retrieve the range and precision for numeric formats supported by the shader compiler.
        /// </summary>
        /// <param name="type">Specifies the type of shader whose precision to query.
        /// Must be <see cref="ShaderType.Vertex"/> or <see cref="ShaderType.Fragment"/>.</param>
        /// <param name="precisionType">Specifies the numeric format whose precision and range to query.</param>
        /// <param name="min">The minimum range.</param>
        /// <param name="max">The maximum rnage.</param>
        /// <param name="precision">Specifies the numeric precision of the implementation.</param>
        public static void GetShaderPrecisionFormat(ShaderType type, ShaderPrecision precisionType, out int min, out int max, out int precision)
        {
            precision = 0;

            int[] range = new int[2];

            fixed (int* rangePtr = &range[0])
            {
                fixed (int* precPtr = &precision)
                {
                    GL.GetShaderPrecisionFormat((uint)type, (uint)precisionType, rangePtr, precPtr);
                }
            }

            min = range[0];
            max = range[1];
        }

        /// <summary>
        /// Gets or sets the minimum rate at which sample shading takes place.
        /// </summary>
        public static double MinSampleShading
        {
            get
            {
                float value = 0;

                GL.GetFloatv(GLEnum.MinSampleShadingValue, &value);

                return value;
            }
            set
            {
                GL.MinSampleShading((float)value);
            }
        }

        /// <summary>
        /// Release the resources consumed by the implementation's shader compiler.
        /// </summary>
        public static void ReleaseShaderCompiler()
        {
            GL.ReleaseShaderCompiler();
        }

        private static void SetConstants()
        {
            if (GL.Version >= 3.0)
            {
                int value = 0;
                GL.GetIntegerv(GLEnum.MaxColourAttachments, ref value);
                MaxColourAttach = value;
            }
            if (GL.Version >= 2.0)
            {
                int value = 0;
                GL.GetIntegerv(GLEnum.MaxDrawBuffers, ref value);
                MaxDrawBuffers = value;

                GL.GetIntegerv(GLEnum.MaxVertexAttribs, ref value);
                MaxVertexAttributes = value;
            }
        }

        /// <summary>
        /// Gets the maximum colour attachments of framebuffers for the hardware being used.
        /// </summary>
        [OpenGLSupport(3.0)]
        public static int MaxColourAttach { get; private set; } = 8;
        /// <summary>
        /// Gets the maximum colour attachments of framebuffers for the hardware being used.
        /// </summary>
        [OpenGLSupport(2.0)]
        public static int MaxDrawBuffers { get; private set; } = 1;

        /// <summary>
        /// Gets the maximum number of vertex attribute indexes.
        /// </summary>
        [OpenGLSupport(2.0)]
        public static int MaxVertexAttributes { get; private set; } = 16;

        /// <summary>
        /// Clears all errors in gl error stack.
        /// </summary>
        public static void ClearErrors()
        {
            while (GL.GetError() != GLEnum.NoError)
            {
                // Nothing happens - content is in check statment
            }
        }
    }
}
