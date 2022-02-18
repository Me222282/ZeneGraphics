namespace Zene.Structs
{
    public interface IBox
    {
        /// <summary>
        /// The x position of the left side of the box.
        /// </summary>
        public double Left { get; set; }
        /// <summary>
        /// The x position of the right side of the box.
        /// </summary>
        public double Right { get; set; }
        /// <summary>
        /// The y position of the Bottom size of the box.
        /// </summary>
        public double Bottom { get; set; }
        /// <summary>
        /// The y position of the top side of the box.
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// The center location of the box.
        /// </summary>
        public Vector2 Centre { get; }

        /// <summary>
        /// The x location of the box.
        /// </summary>
        public double X { get; }
        /// <summary>
        /// The y location of the box.
        /// </summary>
        public double Y { get; }
        /// <summary>
        /// The width of the box (<see cref="Right"/> - <see cref="Left"/>).
        /// </summary>
        public double Width { get; }
        /// <summary>
        /// The height of the box (<see cref="Top"/> - <see cref="Bottom"/>).
        /// </summary>
        public double Height { get; }
    }
}
