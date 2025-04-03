using System;

namespace MathsLib
{
    /*
     * Constructors
     */
    public sealed partial record Quaternion
    {
        public float w { get; }
        public float x { get; }
        public float y { get; }
        public float z { get; }

        // constructor from real and vector parts
        public Quaternion(float w, (float x, float y, float z) tuple)
        {
            this.w = w;
            x = tuple.x;
            y = tuple.y;
            z = tuple.z;
        }
    
        // constructor for 0 real part
        public Quaternion(Vector3D v, float w=0)
        {
            this.w = w;
            x = v.x;
            y = v.y;
            z = v.z;
        }

        // constructor from angle and axis parts
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

        // constructor from euler angles
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

        // constructor from rotation matrix
        public Quaternion(Matrix4D mat)
        {
            /*
             * Maths for calculation from: https://www.opengl-tutorial.org/assets/faq_quaternions/index.html#Q55
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
        // Multiplies quaternions
        public static Quaternion operator *(Quaternion a, Quaternion b) => new(b.w * a.w - Vector3D.Dot(b.v, a.v),
            (b.w * a.v + a.w * b.v + Vector3D.Cross(a.v, b.v)).Tuple);
    }

/*
 * Methods
 */
    public sealed partial record Quaternion
    {

        public static Quaternion Zero => new(0, (0, 0, 0));
    
        // returns vector part
        public Vector3D v => new (x, y, z);
    
        // inverse of the quaternion
        public Quaternion Inverse => new (w, (-x, -y, -z));

        public float Magnitude => MathF.Sqrt(w*w + x*x + y*y + z*z);
    
        public Quaternion Normalised => new (w/Magnitude, (x/Magnitude, y/Magnitude, z/Magnitude));
    
    
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
    
        public override string ToString() => $"{MathF.Round(w, 4)} + {MathF.Round(x, 4)}i + {MathF.Round(y, 4)}j + {MathF.Round(z, 4)}k";
        //public override string ToString() => $"{w} + {x}i + {y}j + {z}k";
    
        // rotates a vector by a quaternion using q * p * q`
        public static Vector3D Rotate(Quaternion q, Vector3D p) => (q * new Quaternion(p) * q.Inverse).v;

        public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
        {
            Quaternion d = b * a.Inverse;
            float wt = MathF.Cos(t * MathF.Acos(d.w));
            Vector3D vt = (d.v / MathF.Acos(d.w)) * MathF.Sin(t * MathF.Acos(d.w));
            Quaternion dt = new(wt, vt);
            return dt * a;
        }

        public static Quaternion Slerp2(Quaternion a, Quaternion b, float t)
        {
            Quaternion d = b * a.Inverse;
            (float angle, Vector3D axis) = d.AxisAngle();
            Quaternion dt = new(angle, axis);
            return dt * a;
        }
    }
}







