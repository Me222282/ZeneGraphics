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
    }
}
