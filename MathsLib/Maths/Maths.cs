using System;
using System.Data;
using System.Numerics;
using System.Globalization;

namespace MathsLib
{
    public static partial class Maths
    {
        /// <summary>
        /// Tolerance value for equivalence and rounding.
        /// </summary>
        public const double Tolerance = 0.0001f;

        /// <summary>
        /// Multiply by to convert to radians, divide by to convert to degrees
        /// </summary>
        public const double Radians = MathF.PI / 180;

        /// <summary>
        /// Clamps a value to be within tolerance
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Clamp(double value)
        {
            if (Math.Abs(value - Math.Round(value)) < Tolerance)
            {
                return Math.Round(value);
            }
            return value;
        }

        /// <summary>
        /// Gets the sign of a value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static char sign(double value) => value < 0 ? '-' : '+';
        
        /// <summary>
        /// Custom equivalence comparison
        /// </summary>
        /// <param name="x">LHS</param>
        /// <param name="y">RHS</param>
        /// <returns></returns>
        public static bool Equal(double x, double y) => Math.Abs(x - y) < Tolerance;

        /// <summary>
        /// Custom ToString method to truncate a double to a specified number of digits
        /// </summary>
        /// <param name="value">Value to truncate</param>
        /// <param name="digits">digits to truncate to</param>
        /// <returns></returns>
        public static string ToString(this double value, int digits)
        {
            return string.Format($"{{0,{digits+7}:0.{new string('0', digits)}e+00}}", value);
        }
    }
}