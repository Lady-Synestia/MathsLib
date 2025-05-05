using System;
using System.Data;
using System.Numerics;
using System.Globalization;

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

        public static char sign(float value) => value < 0 ? '-' : '+';
        
        public static bool Equal(float x, float y) => MathF.Abs(x - y) < Tolerance;

        public static string ToString(this float value, int digits)
        {
            return string.Format($"{{0,{digits+7}:0.{new string('0', digits)}e+00}}", value);
        }

        /*public static string ToString(this float value, int sf)
        {
            if (value == 0)
            {
                return "0." + new string('0', sf-1);
            }

            int shifts = 0;
            float shiftBy = value switch
            {
                >= 1 => 0.1f,
                < 0.1f => 10,
                _ => 0
            };
            while (MathF.Abs(value) > 1 || MathF.Abs(value) < 0.1)
            {
                value *= shiftBy;
                shifts++;
            }
            
            float unshift = (shifts != 0 ? (MathF.Pow(1 / shiftBy, shifts)) : 1);
            float exp = shiftBy switch
            {
                10 when unshift < MathF.Pow(0.1f, sf) || Equal(unshift, MathF.Pow(0.1f, sf)) => -shifts,
                0.1f when unshift > MathF.Pow(10, sf) || Equal(unshift, MathF.Pow(10, sf)) => shifts,
                _ => 0
            };

            if (exp != 0)
            {
                value = MathF.Round(value, sf) * 10;
                exp -= 1;
                return exp != 0 ? $"{MathF.Round(value, sf)}e{sign(exp)}{MathF.Abs(exp)}" : $"{value}";
            }

            value = MathF.Round(value, sf) * unshift;
            string s_value = value.ToString(CultureInfo.InvariantCulture);
            
            // padding with 0s
            int negative = 0;
            int fraction = 0;
            int digit = 0;
            for (int chr = 0; chr <= sf; chr++)
            {
                if (digit >= s_value.Length - fraction - negative)
                {
                    if (fraction == 0)
                    {
                        fraction = 1;
                        s_value += '.';
                        continue;
                    }
                    s_value += '0';
                    digit++;
                    continue;
                }
                switch (s_value[digit])
                {
                    case '-':
                        negative = 1;
                        break;
                    case '.':
                        fraction = 1;
                        break;
                    default:
                        digit++;
                        break;
                }
            }

            return s_value;
        }*/
    }
}