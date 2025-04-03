using System;

namespace MathsLib
{
    public static partial class Maths
    {
        public const float Tolerance = 0.0001f;

        public const float Radians = MathF.PI / 180;

        public static float Clamp(float value)
        {
            if (MathF.Abs(value - MathF.Round(value)) < Tolerance)
            {
                return MathF.Round(value);
            }
            return value;
        }
    
        public static bool Equal(float x, float y) => MathF.Abs(x - y) < Tolerance;
    }
}