using System;

namespace MathsLib
{
    /*
     * Constructors
     */
    public sealed partial record Quaternion
    {
        /// <summary>
        /// w (real) component
        /// </summary>
        public float w { get; }
        
        /// <summary>
        /// coefficient of i in vector component
        /// </summary>
        public float x { get; }
        
        /// <summary>
        /// coefficient of j in vector component
        /// </summary>
        public float y { get; }
        
        /// <summary>
        /// coefficient of k in vector component
        /// </summary>
        public float z { get; }

        /// <summary>
        /// Constructor from real and vector parts.
        /// Use only if you know values are correct.
        /// </summary>
        /// <param name="w">Real part</param>
        /// <param name="tuple">Vector part</param>
        public Quaternion(float w, (float x, float y, float z) tuple)
        {
            this.w = w;
            x = tuple.x;
            y = tuple.y;
            z = tuple.z;
        }
        
        /*public Quaternion(Vector3D v, float w=0)
        {
            this.w = w;
            x = v.x;
            y = v.y;
            z = v.z;
        }*/

        /// <summary>
        /// Constructor from angle and axis parts
        /// </summary>
        /// <param name="angle">angle in degrees</param>
        /// <param name="axis">Does not need to be normalised</param>
        public Quaternion(float angle, Vector3D axis)
        {
            axis = axis.Normalised;
            angle *= Maths.Radians;
            float halfAngle = angle * 0.5f;
            w = MathF.Cos(halfAngle);
            x = axis.x * MathF.Sin(halfAngle);
            y = axis.y * MathF.Sin(halfAngle);
            z = axis.z * MathF.Sin(halfAngle);
        }

        /// <summary>
        /// Constructor from euler angles
        /// </summary>
        /// <param name="angles">3D Vector representation of Euler Angles</param>
        public Quaternion(Vector3D angles)
        {
            float magnitude = angles.Magnitude;
            angles *= Maths.Radians;
            Vector3D normalised = angles.Normalised;

            w = MathF.Cos(magnitude / 2);
            x = MathF.Sin(magnitude / 2) * normalised.x;
            y = MathF.Sin(magnitude / 2) * normalised.y;
            z = MathF.Sin(magnitude / 2) * normalised.z;
        }

        /// <summary>
        /// Constructor from rotation matrix
        /// </summary>
        /// <param name="mat">4D rotation matrix</param>
        public Quaternion(Matrix4D mat)
        {
            /*
             * Maths for calculation from the OpenGL FAQ on Matrices and Quaternions, Question 55 (Various., n.d.)
             */

            float trace = 1 + mat.F.x + mat.U.y + mat.R.z;
            if (trace > Maths.Tolerance)
            {
                float s = MathF.Sqrt(trace) * 2;
                w = 0.25f * s;
                x = (mat.R.y - mat.U.z) / s;
                y = (mat.F.z - mat.R.x) / s;
                z = (mat.U.x - mat.F.y) / s;
            }
            else if (mat.F.x - mat.U.y > Maths.Tolerance && mat.F.x - mat.R.z > Maths.Tolerance)
            {
                float s = MathF.Sqrt(1.0f + mat.F.x - mat.U.y - mat.R.z) * 2;
                w = (mat.R.y - mat.U.z) / s;
                x = 0.25f * s;
                y = (mat.F.z + mat.R.x) / s;
                z = (mat.U.x + mat.U.y) / s;
            }
            else if (mat.U.y - mat.R.z > Maths.Tolerance)
            {
                float s = MathF.Sqrt(1.0f + mat.U.y - mat.F.x - mat.R.z) * 2;
                w = (mat.U.z - mat.R.x) / s;
                x = (mat.U.x + mat.F.y) / s;
                y = 0.25f * s;
                z = (mat.R.y + mat.U.z) / s;
            }
            else
            {
                float s = MathF.Sqrt(1.0f + mat.R.z - mat.F.x - mat.U.y) * 2;
                w = (mat.U.x - mat.F.y) / s;
                x = (mat.F.z + mat.R.x) / s;
                y = (mat.R.y + mat.U.z) / s;
                z = 0.25f * s;
            }
        }

    }

/*
 * Operators
 */
    public sealed partial record Quaternion
    {
        /// <summary>
        /// Quaternion multiplication
        /// </summary>
        /// <param name="a">lhs Quaternion</param>
        /// <param name="b">rhs Quaternion</param>
        /// <returns></returns>
        public static Quaternion operator *(Quaternion a, Quaternion b) => new(b.w * a.w - Vector3D.Dot(b.v, a.v),
            (b.w * a.v + a.w * b.v + Vector3D.Cross(a.v, b.v)).Tuple);
    }

/*
 * Methods
 */
    public sealed partial record Quaternion
    {
        /// <summary>
        /// Quaternion representing no rotation
        /// </summary>
        public static Quaternion Zero => new(1, (0, 0, 0));
        
        /// <summary>
        /// Vector part of Quaternion. Used in quaternion multiplication
        /// </summary>
        public Vector3D v => new (x, y, z);
    
        /// <summary>
        /// Quaternion Inverse, flips signs of Vector part
        /// </summary>
        public Quaternion Inverse => new (w, (-x, -y, -z));

        /// <summary>
        /// Quaternion Magnitude
        /// </summary>
        public float Magnitude => MathF.Sqrt(w*w + x*x + y*y + z*z);
    
        /// <summary>
        /// Normalised Quaternion. Quaternions should always be normalised
        /// </summary>
        public Quaternion Normalised => new (w/Magnitude, (x/Magnitude, y/Magnitude, z/Magnitude));
    
        /// <summary>
        /// Quaternion Dot Product.
        /// The same as normal 4D Dot Product
        /// </summary>
        /// <param name="a">lhs</param>
        /// <param name="b">rhs</param>
        /// <returns></returns>
        public static float Dot(Quaternion a, Quaternion b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        
        
        /// <summary>
        /// Calculates angle-axis representation from Quaternion
        /// </summary>
        /// <returns>angle (in degrees) and axis</returns>
        public (float angle, Vector3D axis) AxisAngle()
        {
            float halfAngle = MathF.Acos(w);
            float sinH = MathF.Sin(halfAngle);
            Vector3D axis = new (
                x / sinH, 
                y / sinH, 
                z / sinH);
            return (halfAngle / Maths.Radians * 2, axis);
        }

        /// <summary>
        /// Custom ToString method specifying a number of digits to display
        /// </summary>
        /// <param name="digits">number of digits</param>
        /// <returns></returns>
        public string ToString(int digits)
        {
           return $"{w.ToString(digits)} {format(x)}i + {format(y)}j + {format(z)}k"; 
           
           string format(float value) => Maths.sign(value) + MathF.Abs(value).ToString(digits);
        } 
        
        public override string ToString()
        {
           return $"{w.ToString()} {format(x)}i + {format(y)}j + {format(z)}k"; 
           
           string format(float value) => Maths.sign(value) + MathF.Abs(value).ToString();
        } 
        //public override string ToString() => $"{w} + {x}i + {y}j + {z}k";
        
        /// <summary>
        /// Rotates a 3D Vector by a Quaternion using q * p * q`
        /// </summary>
        /// <param name="q">Quaternion to rotate by</param>
        /// <param name="p">vector to rotate</param>
        /// <returns></returns>
        public static Vector3D Rotate(Quaternion q, Vector3D p) => (q * new Quaternion(p) * q.Inverse).v;
        
        
        /// <summary>
        /// Spherically interpolates one quaternion to another
        /// </summary>
        /// <param name="a">Starting Quaternion</param>
        /// <param name="b">Ending Quaternion</param>
        /// <param name="t">fraction of rotation</param>
        /// <returns></returns>
        public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
        {
            Quaternion d = b * a.Inverse;
            (float angle, Vector3D axis) = d.AxisAngle();
            return new Quaternion(t * angle, axis);
        }
    }
}







