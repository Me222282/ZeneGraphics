using Zene.Structs;

namespace Zene.Graphics
{
    public interface IBasicShader : IMatrixShader, IShaderProgram
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
        /// The slot of the texture used for the fragments colour. Only applies <see cref="ColourSource"/> is set to <see cref="ColourSource.Texture"/>.
        /// </summary>
        public int TextureSlot { get; set; }
    }
}
