using System;
using System.Collections.Generic;
using Zene.Structs;

namespace Zene.Physics
{
    public static class Collisions
    {
        /// <summary>
        /// The tolerance at which to assume axis aligned movement.
        /// </summary>
        private const double _gridDirTolerance = 0.000001;

        #region Any IBox

        /// <summary>
        /// Determines whether <paramref name="box"/> has collided with any <see cref="Box"/> from <paramref name="compare"/>.
        /// </summary>
        /// <param name="box">The box to test.</param>
        /// <param name="dir">The direction of the boxes movement.</param>
        /// <param name="vel">The velocity the box is moving at.</param>
        /// <param name="compare">The boxes to compare to.</param>
        public static bool Gird<T>(this T box, Vector2 dir, double vel, IEnumerable<IBox> compare) where T : IBox, new()
        {
            return Grid(box, dir, dir * vel, compare);
        }
        /// <summary>
        /// Determines whether <paramref name="box"/> has collided with any <see cref="Box"/> from <paramref name="compare"/>.
        /// </summary>
        /// <param name="box">The box to test.</param>
        /// <param name="offset">The movement offset of the box.</param>
        /// <param name="compare">The boxes to compare to.</param>
        public static bool Grid<T>(this T box, Vector2 offset, IEnumerable<IBox> compare) where T : IBox, new()
        {
            return Grid(box, offset.Normalized(), offset, compare);
        }
        /// <summary>
        /// Determines which <see cref="Box"/> from <paramref name="compare"/> <paramref name="box"/> has collided with.
        /// </summary>
        /// <param name="box">The box to test.</param>
        /// <param name="dir">The direction of the boxes movement.</param>
        /// <param name="vel">The velocity the box is moving at.</param>
        /// <param name="compare">The boxes to compare to.</param>
        /// <param name="collision">The box collided with.</param>
        public static bool Grid<T>(this T box, Vector2 dir, double vel, IEnumerable<IBox> compare, out IBox collision) where T : IBox, new()
        {
            return Grid(box, dir, dir * vel, compare, out collision);
        }
        /// <summary>
        /// Determines which <see cref="Box"/> from <paramref name="compare"/> <paramref name="box"/> has collided with.
        /// </summary>
        /// <param name="box">The box to test.</param>
        /// <param name="offset">The movement offset of the box.</param>
        /// <param name="compare">The boxes to compare to.</param>
        /// <param name="collision">The box collided with.</param>
        public static bool Grid<T>(this T box, Vector2 offset, IEnumerable<IBox> compare, out IBox collision) where T : IBox, new()
        {
            return Grid(box, offset.Normalized(), offset, compare, out collision);
        }

        private static bool Grid<T>(T box, Vector2 dir, Vector2 offset, IEnumerable<IBox> compare) where T : IBox, new()
        {
            // The new box location
            IBox newBox = box.Shifted(offset);

            // The bounding box of the movement
            IBox moveBound = box.Combine(newBox);

            // All boxes that have the potential to be a collision
            List<IBox> potential = new List<IBox>();

            // Remove all boxes that don't have the potential to be a collision
            foreach (IBox b in compare)
            {
                if (b.Overlaps(moveBound))
                {
                    potential.Add(b);
                }
            }

            // No potential, no collision
            if (potential.Count == 0) { return false; }

            // Is direction on an angle
            if ((dir.X > _gridDirTolerance || dir.X < -_gridDirTolerance) &&
                (dir.Y > _gridDirTolerance || dir.Y > -_gridDirTolerance))
            {
                // Collisions on an angle
                return GridAngleCol(box, dir, potential, out _);
            }

            return true;
        }
        private static bool Grid<T>(T box, Vector2 dir, Vector2 offset, IEnumerable<IBox> compare, out IBox collision) where T : IBox, new()
        {
            // The new box location
            IBox newBox = box.Shifted(offset);

            // The bounding box of the movement
            IBox moveBound = box.Combine(newBox);

            // All boxes that have the potential to be a collision
            List<IBox> potential = new List<IBox>();

            // Remove all boxes that don't have the potential to be a collision
            foreach (IBox b in compare)
            {
                if (b.Overlaps(moveBound))
                {
                    potential.Add(b);
                }
            }

            // No potential, no collision
            if (potential.Count == 0)
            {
                collision = new T();

                return false;
            }

            int DistanceSort(IBox x, IBox y)
            {
                double xDis = Math.Min(
                    Math.Min((box.Center - new Vector2(x.Left, x.Top)).SquaredLength,       // Top Left
                        (box.Center - new Vector2(x.Right, x.Top)).SquaredLength),          // Top Right
                    Math.Min((box.Center - new Vector2(x.Left, x.Bottom)).SquaredLength,    // Bottom Left
                        (box.Center - new Vector2(x.Right, x.Bottom)).SquaredLength));      // Bottom Right

                double yDis = Math.Min(
                    Math.Min((box.Center - new Vector2(y.Left, y.Top)).SquaredLength,       // Top Left
                        (box.Center - new Vector2(y.Right, y.Top)).SquaredLength),          // Top Right
                    Math.Min((box.Center - new Vector2(y.Left, y.Bottom)).SquaredLength,    // Bottom Left
                        (box.Center - new Vector2(y.Right, y.Bottom)).SquaredLength));      // Bottom Right

                if (xDis < yDis)
                {
                    return 1;
                }
                else if (xDis > yDis)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }

            potential.Sort(DistanceSort);

            // Is direction on an angle
            if ((dir.X > _gridDirTolerance || dir.X < -_gridDirTolerance) &&
                (dir.Y > _gridDirTolerance || dir.Y > -_gridDirTolerance))
            {
                // Collisions on an angle
                return GridAngleCol(box, dir, potential, out collision);
            }

            collision = potential[0];

            return true;
        }

        private static bool GridAngleCol<T>(T origin, Vector2 dir, IEnumerable<IBox> compare, out IBox collision) where T : IBox, new()
        {
            Vector2 tolerance = new Vector2(origin.Width / 2, origin.Height / 2);
            // The line of movement
            Line2 line = new Line2(dir, origin.Center);

            foreach (IBox box in compare)
            {
                if (box.Intersects(line/*, tolerance*/))
                {
                    collision = box;

                    return true;
                }
            }

            collision = new T();

            return false;
        }

        #endregion

        #region Specific IBox

        /// <summary>
        /// Determines whether <paramref name="box"/> has collided with any <see cref="Box"/> from <paramref name="compare"/>.
        /// </summary>
        /// <param name="box">The box to test.</param>
        /// <param name="dir">The direction of the boxes movement.</param>
        /// <param name="vel">The velocity the box is moving at.</param>
        /// <param name="compare">The boxes to compare to.</param>
        public static bool Grid<T>(this T box, Vector2 dir, double vel, IEnumerable<T> compare) where T : IBox, new()
        {
            return Grid(box, dir, dir * vel, compare);
        }
        /// <summary>
        /// Determines whether <paramref name="box"/> has collided with any <see cref="Box"/> from <paramref name="compare"/>.
        /// </summary>
        /// <param name="box">The box to test.</param>
        /// <param name="offset">The movement offset of the box.</param>
        /// <param name="compare">The boxes to compare to.</param>
        public static bool Grid<T>(this T box, Vector2 offset, IEnumerable<T> compare) where T : IBox, new()
        {
            return Grid(box, offset.Normalized(), offset, compare);
        }
        /// <summary>
        /// Determines which <see cref="Box"/> from <paramref name="compare"/> <paramref name="box"/> has collided with.
        /// </summary>
        /// <param name="box">The box to test.</param>
        /// <param name="dir">The direction of the boxes movement.</param>
        /// <param name="vel">The velocity the box is moving at.</param>
        /// <param name="compare">The boxes to compare to.</param>
        /// <param name="collision">The box collided with.</param>
        public static bool Grid<T>(this T box, Vector2 dir, double vel, IEnumerable<T> compare, out T collision) where T : IBox, new()
        {
            return Grid(box, dir, dir * vel, compare, out collision);
        }
        /// <summary>
        /// Determines which <see cref="Box"/> from <paramref name="compare"/> <paramref name="box"/> has collided with.
        /// </summary>
        /// <param name="box">The box to test.</param>
        /// <param name="offset">The movement offset of the box.</param>
        /// <param name="compare">The boxes to compare to.</param>
        /// <param name="collision">The box collided with.</param>
        public static bool Grid<T>(this T box, Vector2 offset, IEnumerable<T> compare, out T collision) where T : IBox, new()
        {
            return Grid(box, offset.Normalized(), offset, compare, out collision);
        }

        private static bool Grid<T>(T box, Vector2 dir, Vector2 offset, IEnumerable<T> compare) where T : IBox, new()
        {
            // The new box location
            T newBox = box.Shifted(offset);

            // The bounding box of the movement
            T moveBound = box.Combine(newBox);

            // All boxes that have the potential to be a collision
            List<T> potential = new List<T>();

            // Remove all boxes that don't have the potential to be a collision
            foreach (T b in compare)
            {
                if (b.Overlaps(moveBound))
                {
                    potential.Add(b);
                }
            }

            // No potential, no collision
            if (potential.Count == 0) { return false; }

            // Is direction on an angle
            if ((dir.X > _gridDirTolerance || dir.X < -_gridDirTolerance) &&
                (dir.Y > _gridDirTolerance || dir.Y > -_gridDirTolerance))
            {
                // Collisions on an angle
                return GridAngleCol(box, dir, potential, out _);
            }

            return true;
        }
        private static bool Grid<T>(T box, Vector2 dir, Vector2 offset, IEnumerable<T> compare, out T collision) where T : IBox, new()
        {
            // The new box location
            T newBox = box.Shifted(offset);

            // The bounding box of the movement
            T moveBound = box.Combine(newBox);

            // All boxes that have the potential to be a collision
            List<T> potential = new List<T>();

            // Remove all boxes that don't have the potential to be a collision
            foreach (T b in compare)
            {
                if (b.Overlaps(moveBound))
                {
                    potential.Add(b);
                }
            }

            // No potential, no collision
            if (potential.Count == 0)
            {
                collision = new T();

                return false;
            }

            int DistanceSort(T x, T y)
            {
                double xDis = Math.Min(
                    Math.Min((box.Center - new Vector2(x.Left, x.Top)).SquaredLength,       // Top Left
                        (box.Center - new Vector2(x.Right, x.Top)).SquaredLength),          // Top Right
                    Math.Min((box.Center - new Vector2(x.Left, x.Bottom)).SquaredLength,    // Bottom Left
                        (box.Center - new Vector2(x.Right, x.Bottom)).SquaredLength));      // Bottom Right

                double yDis = Math.Min(
                    Math.Min((box.Center - new Vector2(y.Left, y.Top)).SquaredLength,       // Top Left
                        (box.Center - new Vector2(y.Right, y.Top)).SquaredLength),          // Top Right
                    Math.Min((box.Center - new Vector2(y.Left, y.Bottom)).SquaredLength,    // Bottom Left
                        (box.Center - new Vector2(y.Right, y.Bottom)).SquaredLength));      // Bottom Right

                if (xDis < yDis)
                {
                    return 1;
                }
                else if (xDis > yDis)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }

            potential.Sort(DistanceSort);

            // Is direction on an angle
            if ((dir.X > _gridDirTolerance || dir.X < -_gridDirTolerance) &&
                (dir.Y > _gridDirTolerance || dir.Y > -_gridDirTolerance))
            {
                // Collisions on an angle
                return GridAngleCol(box, dir, potential, out collision);
            }

            collision = potential[0];

            return true;
        }

        private static bool GridAngleCol<T>(T origin, Vector2 dir, IEnumerable<T> compare, out T collision) where T : IBox, new()
        {
            Vector2 tolerance = new Vector2(origin.Width / 2, origin.Height / 2);
            // The line of movement
            Line2 line = new Line2(dir, origin.Center);

            foreach (T box in compare)
            {
                if (box.Intersects(line, tolerance))
                {
                    collision = box;

                    return true;
                }
            }

            collision = new T();

            return false;
        }

        #endregion
    }
}
