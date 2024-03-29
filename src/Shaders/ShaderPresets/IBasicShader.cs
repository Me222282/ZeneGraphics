using Zene.Structs;

namespace Zene.Graphics
{
    public interface IBasicShader : IDrawingShader
    {
        /// <summary>
        /// The source of the fragment's colour.
        /// </summary>
        public ColourSource ColourSource { get; set; }
        /// <summary>
        /// The colour of the fragment. Only applies <see cref="ColourSource"/> is set to <see cref="ColourSource.UniformColour"/>.
        /// </summary>
        public ColourF Colour { get; set; }
        /// <summary>
        /// The texture used for the fragments colour. Only applies if <see cref="ColourSource"/> is set to <see cref="ColourSource.Texture"/>.
        /// </summary>
        public ITexture Texture { get; set; }
    }
}
