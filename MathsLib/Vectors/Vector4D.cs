using System;

namespace MathsLib
{
    public sealed partial record Vector4D
    {
        public double x { get; }
        public double y { get; }
        public double z { get; }
        public double w { get; }
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="X">x component</param>
        /// <param name="Y">y component</param>
        /// <param name="Z">z component</param>
        /// <param name="W">w component</param>
        public Vector4D(double X, double Y, double Z, double W)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }
        
        /// <summary>
        /// Constructor from 3D Vector with optional w component.
        /// Used when w != 0.
        /// </summary>
        /// <param name="a">3D Vector</param>
        /// <param name="w">w component, defaults to 1</param>
        public Vector4D(Vector3D a, double w = 1) : this(
            a.x,
            a.y,
            a.z,
            w) { }

        /// <summary>
        /// Returns components in the form of a tuple
        /// </summary>
        public (double x, double y, double z, double w) Tuple => (x, y, z, w);

        /// <summary>
        /// Implicit conversion from 3D vector. w = 0.
        /// </summary>
        /// <param name="v">3D Vector</param>
        /// <returns></returns>
        public static implicit operator Vector4D(Vector3D v) => new(
            v.x,
            v.y,
            v.z,
            0);

        /// <summary>
        /// Custom ToString Method, truncates to a specified number of digits
        /// </summary>
        /// <param name="digits">Number of digits to include</param>
        /// <returns></returns>
        public string ToString(int digits) => $"{x.ToString(digits)}, {y.ToString(digits)}, {z.ToString(digits)}, {w.ToString(digits)}";
        
        /// <summary>
        /// Override of default ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{x}, {y}, {z}, {w}";

        public void Deconstruct(out double x, out double y, out double z, out double w)
        {
            x = this.x;
            y = this.y;
            z = this.z;
            w = this.w;
        }
    }



    public sealed partial record Vector4D
    {
        /// <summary>
        /// Vector Magnitude
        /// </summary>
        public double Magnitude => Math.Sqrt(x * x + y * y + z * z + w * w);

        /// <summary>
        /// Normalised form of the vector
        /// </summary>
        public Vector4D Normalised => Normalise(this);
       
        /// <summary>
        /// Normalised (Unit) Vector
        /// </summary>
        /// <param name="a">Vector to normalise</param>
        /// <returns></returns>
        public static Vector4D Normalise(Vector4D a) => a / a.Magnitude;

        /// <summary>
        /// Vector Equality Check
        /// </summary>
        /// <param name="a">LHS</param>
        /// <param name="b">RHS</param>
        /// <returns></returns>
        public static bool Equals(Vector4D a, Vector4D b) =>
            Math.Abs(a.x - b.x) < Maths.Tolerance && Math.Abs(a.y - b.y) < Maths.Tolerance &&
            Math.Abs(a.z - b.z) < Maths.Tolerance &&
            Math.Abs(a.w - b.w) < Maths.Tolerance;
        
        /// <summary>
        /// Dot product of two Vectors
        /// </summary>
        /// <param name="a">LHS</param>
        /// <param name="b">RHS</param>
        /// <returns></returns>
        public static double Dot(Vector4D a, Vector4D b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;

        /// <summary>
        /// Angle between two Vectors
        /// </summary>
        /// <param name="a">LHS</param>
        /// <param name="b">RHS</param>
        /// <returns>Angle in radians</returns>
        public static double Angle(Vector4D a, Vector4D b) => Math.Acos(Dot(a, b) / (a.Magnitude * b.Magnitude));

    }


    public sealed partial record Vector4D
    {
        public static Vector4D operator *(Vector4D a, Vector4D b) => new(
            a.x * b.x,
            a.y * b.y,
            a.z * b.z,
            a.w * b.w);

        public static Vector4D operator *(Vector4D a, double s) => new(
            a.x * s,
            a.y * s,
            a.z * s,
            a.w * s);

        public static Vector4D operator *(double s, Vector4D a) => a * s;

        public static Vector4D operator /(Vector4D a, Vector4D b) => new(
            a.x / b.x,
            a.y / b.y,
            a.z / b.z,
            a.w / b.w);

        public static Vector4D operator /(Vector4D a, double s) => new(
            a.x / s,
            a.y / s,
            a.z / s,
            a.w / s);

        public static Vector4D operator +(Vector4D a, Vector4D b) => new(
            a.x + b.x,
            a.y + b.y,
            a.z + b.z,
            a.w + b.w);

        public static Vector4D operator -(Vector4D a, Vector4D b) => new(
            a.x - b.x,
            a.y - b.y,
            a.z - b.z,
            a.w - b.w);
    }


    public sealed partial record Vector4D
    {
        public static Vector4D Zero => new(0, 0, 0, 0);
        public static Vector4D One => new(1, 1, 1, 1);
        public static Vector4D X => new(1, 0, 0, 0);
        public static Vector4D Y => new(0, 1, 0, 0);
        public static Vector4D Z => new(0, 0, 1, 0);
        public static Vector4D W => new(0, 0, 0, 1);

    }
}