namespace Zene.Structs
{
    public interface IBox3 : IBox
    {
        /// <summary>
        /// The z position of the front side of the box. Negative.
        /// </summary>
        public double Front { get; set; }
        /// <summary>
        /// The z position of the back side of the box. Positive.
        /// </summary>
        public double Back { get; set; }

        /// <summary>
        /// The center location of the box.
        /// </summary>
        public new Vector3 Centre { get; }
        Vector2 IBox.Centre => (Vector2)Centre;

        /// <summary>
        /// The z location of the box.
        /// </summary>
        public double Z { get; }
        /// <summary>
        /// The depth of the box (<see cref="Back"/> - <see cref="Front"/>).
        /// </summary>
        public double Depth { get; }
    }
}
