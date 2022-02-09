using System;

namespace Zene.Structs
{
    public static class StructExt
    {
        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to <paramref name="min"/>, and less than <paramref name="max"/>.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min">The inclusive lower bound of the random number returned.</param>
        /// <param name="max">The exclusive upper bound of the random number returned. <paramref name="max"/> must be greater than or equal to <paramref name="min"/>.</param>
        /// <returns></returns>
        public static double NextDouble(this Random random, double min, double max)
        {
            if (max < min)
            {
                throw new Exception($"{nameof(max)} must be greater than or equal to {nameof(min)}.");
            }

            return (random.NextDouble() * (max - min)) + min;
        }
    }
}
