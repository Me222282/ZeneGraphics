namespace Zene.Structs
{
    public interface IBox3 : IBox
    {
        /// <summary>
        /// The z position of the front side of the box.
        /// </summary>
        public double Front { get; set; }
        /// <summary>
        /// The z position of the back side of the box.
        /// </summary>
        public double Back { get; set; }

        /// <summary>
        /// The center location of the box.
        /// </summary>
        public Vector3 Center { get; }
        Vector2 IBox.Centre => (Vector2)Center;

        /// <summary>
        /// The z location of the box.
        /// </summary>
        public double Z { get; }
        /// <summary>
        /// The depth of the box (<see cref="Front"/> - <see cref="Back"/>).
        /// </summary>
        public double Depth { get; }
    }
}
