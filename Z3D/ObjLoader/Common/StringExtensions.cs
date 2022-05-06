using System;
using System.Globalization;

namespace ObjLoader.Common
{
    internal static class StringExtensions
    {
        internal static double ParseInvariantFloat(this string floatString)
        {
            return double.Parse(floatString, CultureInfo.InvariantCulture.NumberFormat);
        }

        internal static int ParseInvariantInt(this string intString)
        {
            return int.Parse(intString, CultureInfo.InvariantCulture.NumberFormat);
        }

        internal static bool EqualsOrdinalIgnoreCase(this string str, string s)
        {
            return str.Equals(s, StringComparison.OrdinalIgnoreCase);
        }

        internal static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}