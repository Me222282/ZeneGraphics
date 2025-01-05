using Zene.Structs;

namespace Zene.Graphics
{
    public enum ColourSource
    {
        None = 0,
        UniformColour = 1,
        AttributeColour = 2,
        Texture = 3,
        // Not always supported
        Discard = 4
    }

    public sealed class BasicShader : BaseShaderProgram, IBasicShader
    {
        public BasicShader()
        {
            Create(ShaderPresets.BasicVertex, ShaderPresets.BasicFragment, 3,
                "colourType", "uColour", "uTextureSlot", "matrix");

            SetUniform(Uniforms[3], Matrix4.Identity);
            SetUniform(Uniforms[2], 0);
        }

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

        public ITexture Texture { get; set; }
        
        public override void PrepareDraw()
        {
            base.PrepareDraw();
            Texture?.Bind(0);
        }

        protected override void Dispose(bool dispose)
        {
            base.Dispose(dispose);

            State.CurrentContext.RemoveTrack(this);
        }
        /// <summary>
        /// Gets the instance of the <see cref="BasicShader"/> for this <see cref="GraphicsContext"/>.
        /// </summary>
        /// <returns></returns>
        public static BasicShader GetInstance() => GetInstance<BasicShader>();
    }
}
