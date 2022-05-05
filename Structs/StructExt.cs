using System;

namespace Zene.Structs
{
    /// <summary>
    /// The class containing extensions included in the library.
    /// </summary>
    public static class StructExt
    {
        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to <paramref name="min"/>, and less than <paramref name="max"/>.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min">The inclusive lower bound of the random number returned.</param>
        /// <param name="max">The exclusive upper bound of the random number returned. <paramref name="max"/> must be greater than or equal to <paramref name="min"/>.</param>
        public static double NextDouble(this Random random, double min, double max)
        {
            if (max < min)
            {
                throw new Exception($"{nameof(max)} must be greater than or equal to {nameof(min)}.");
            }

            return (random.NextDouble() * (max - min)) + min;
        }

        /// <summary>
        /// Returns a colour with random values for the RGB components.
        /// </summary>
        /// <remarks>
        /// Alpha has a value of 255.
        /// </remarks>
        /// <param name="random"></param>
        public static Colour NextColour(this Random random)
        {
            return new Colour(
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256));
        }
        /// <summary>
        /// Returns a colour with random values for the RGBA components.
        /// </summary>
        /// <param name="random"></param>
        public static Colour NextColourA(this Random random)
        {
            return new Colour(
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256));
        }

        /// <summary>
        /// Returns a colour with random values for the RGB components.
        /// </summary>
        /// <param name="random"></param>
        public static Colour3 NextColour3(this Random random)
        {
            return new Colour3(
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256));
        }
        /// <summary>
        /// Returns a colour in SDR stored as floats with random values for the RGB components.
        /// </summary>
        /// <param name="random"></param>
        public static ColourF3 NextColourF3(this Random random)
        {
            return new ColourF3(
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                (float)random.NextDouble());
        }
        /// <summary>
        /// Returns a colour in SDR stored as integers with random values for the RGB components.
        /// </summary>
        /// <param name="random"></param>
        public static ColourI3 NextColourI3(this Random random)
        {
            return new ColourI3(
                random.Next(0, 256),
                random.Next(0, 256),
                random.Next(0, 256));
        }

        /// <summary>
        /// Returns a colour in SDR stored as floats with random values for the RGB components.
        /// </summary>
        /// <remarks>
        /// Alpha has a value of 1.0f.
        /// </remarks>
        /// <param name="random"></param>
        public static ColourF NextColourF(this Random random)
        {
            return new ColourF(
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                (float)random.NextDouble());
        }
        /// <summary>
        /// Returns a colour in SDR stored as floats with random values for the RGBA components.
        /// </summary>
        /// <param name="random"></param>
        public static ColourF NextColourFA(this Random random)
        {
            return new ColourF(
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                (float)random.NextDouble());
        }

        /// <summary>
        /// Returns a colour in SDR stored as integers with random values for the RGB components.
        /// </summary>
        /// <remarks>
        /// Alpha has a value of 255.
        /// </remarks>
        /// <param name="random"></param>
        public static ColourI NextColourI(this Random random)
        {
            return new ColourI(
                random.Next(0, 256),
                random.Next(0, 256),
                random.Next(0, 256));
        }
        /// <summary>
        /// Returns a colour in SDR stored as integers with random values for the RGBA components.
        /// </summary>
        /// <param name="random"></param>
        public static ColourI NextColourIA(this Random random)
        {
            return new ColourI(
                random.Next(0, 256),
                random.Next(0, 256),
                random.Next(0, 256),
                random.Next(0, 256));
        }

        public static Vector2 NextVector2(this Random random)
        {
            return new Vector2(
                random.NextDouble(),
                random.NextDouble());
        }
        public static Vector2 NextVector2(this Random random, double scale)
        {
            return new Vector2(
                random.NextDouble() * scale,
                random.NextDouble() * scale);
        }
        public static Vector2 NextVector2(this Random random, double min, double max)
        {
            return new Vector2(
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min);
        }
        public static Vector2 NextVector2(this Random random, double minX, double maxX, double minY, double maxY)
        {
            return new Vector2(
                (random.NextDouble() * (maxX - minX)) + minX,
                (random.NextDouble() * (maxY - minY)) + minY);
        }

        public static Vector2I NextVector2I(this Random random)
        {
            return new Vector2I(
                random.Next(),
                random.Next());
        }
        public static Vector2I NextVector2I(this Random random, int min, int max)
        {
            return new Vector2I(
                random.Next(min, max),
                random.Next(min, max));
        }
        public static Vector2I NextVector2I(this Random random, int minX, int maxX, int minY, int maxY)
        {
            return new Vector2I(
                random.Next(minX, maxX),
                random.Next(minY, maxY));
        }

        public static Vector3 NextVector3(this Random random)
        {
            return new Vector3(
                random.NextDouble(),
                random.NextDouble(),
                random.NextDouble());
        }
        public static Vector3 NextVector3(this Random random, double scale)
        {
            return new Vector3(
                random.NextDouble() * scale,
                random.NextDouble() * scale,
                random.NextDouble() * scale);
        }
        public static Vector3 NextVector3(this Random random, double min, double max)
        {
            return new Vector3(
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min);
        }
        public static Vector3 NextVector3(this Random random, double minX, double maxX, double minY, double maxY, double minZ, double maxZ)
        {
            return new Vector3(
                (random.NextDouble() * (maxX - minX)) + minX,
                (random.NextDouble() * (maxY - minY)) + minY,
                (random.NextDouble() * (maxZ - minZ)) + minZ);
        }

        public static Vector3I NextVector3I(this Random random)
        {
            return new Vector3I(
                random.Next(),
                random.Next(),
                random.Next());
        }
        public static Vector3I NextVector3I(this Random random, int min, int max)
        {
            return new Vector3I(
                random.Next(min, max),
                random.Next(min, max),
                random.Next(min, max));
        }
        public static Vector3I NextVector3I(this Random random, int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
        {
            return new Vector3I(
                random.Next(minX, maxX),
                random.Next(minY, maxY),
                random.Next(minZ, maxZ));
        }

        public static Vector4 NextVector4(this Random random)
        {
            return new Vector4(
                random.NextDouble(),
                random.NextDouble(),
                random.NextDouble(),
                random.NextDouble());
        }
        public static Vector4 NextVector4(this Random random, double scale)
        {
            return new Vector4(
                random.NextDouble() * scale,
                random.NextDouble() * scale,
                random.NextDouble() * scale,
                random.NextDouble() * scale);
        }
        public static Vector4 NextVector4(this Random random, double min, double max)
        {
            return new Vector4(
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min,
                (random.NextDouble() * (max - min)) + min);
        }
        public static Vector4 NextVector4(this Random random, double minX, double maxX, double minY, double maxY, double minZ, double maxZ, double minW, double maxW)
        {
            return new Vector4(
                (random.NextDouble() * (maxX - minX)) + minX,
                (random.NextDouble() * (maxY - minY)) + minY,
                (random.NextDouble() * (maxZ - minZ)) + minZ,
                (random.NextDouble() * (maxW - minW)) + minW);
        }

        public static Vector4I NextVector4I(this Random random)
        {
            return new Vector4I(
                random.Next(),
                random.Next(),
                random.Next(),
                random.Next());
        }
        public static Vector4I NextVector4I(this Random random, int min, int max)
        {
            return new Vector4I(
                random.Next(min, max),
                random.Next(min, max),
                random.Next(min, max),
                random.Next(min, max));
        }
        public static Vector4I NextVector4I(this Random random, int minX, int maxX, int minY, int maxY, int minZ, int maxZ, int minW, int maxW)
        {
            return new Vector4I(
                random.Next(minX, maxX),
                random.Next(minY, maxY),
                random.Next(minZ, maxZ),
                random.Next(minW, maxW));
        }

        public static double Lerp(this double a, double b, double blend) => (blend * (b - a)) + a;
    }
}
