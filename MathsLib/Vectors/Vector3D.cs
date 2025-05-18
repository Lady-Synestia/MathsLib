using System;

namespace MathsLib
{
    public sealed partial record Vector3D(double x, double y, double z)
    {
        public double x { get; } = x;
        public double y { get; } = y;
        public double z { get; } = z;
        

        /// <summary>
        /// implicit conversion from 4D Vector
        /// </summary>
        /// <param name="v">4D Vector</param>
        /// <returns></returns>
        public static implicit operator Vector3D(Vector4D v) => new(v.x, v.y, v.z);

        // x: pitch, y: yaw, z: roll
        public static Vector3D FromAngles(Vector3D angles) => new(
            Math.Cos(angles.y) * Math.Cos(angles.x),
            Math.Sin(angles.x),
            Math.Cos(angles.x) * Math.Sin(angles.y));

        public static Vector3D FromAngles2D(double roll) => new(
            Math.Cos(roll),
            Math.Sin(roll),
            0);

        /// <summary>
        /// Custom ToString Method, truncating values to a specified number of digits
        /// </summary>
        /// <param name="digits">Number of digits to include</param>
        /// <returns></returns>
        public string ToString(int digits) => $"{x.ToString(digits)}, {y.ToString(digits)}, {z.ToString(digits)}";
        
        /// <summary>
        /// Default ToString method override
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{x}, {y}, {z}";
        
        // return values as tuple
        public (double x, double y, double z) Tuple => (x, y, z);
    }


    public sealed partial record Vector3D
    {
        // vector operations

        /// <summary>
        /// Vector Magnitude
        /// </summary>
        public double Magnitude => Math.Sqrt(x * x + y * y + z * z);
        
        /// <summary>
        /// Vector Magnitude Squared
        /// </summary>
        public double SqrMagnitude => x * x + y * y + z * z;
        
        /// <summary>
        /// Normalised form of the vector
        /// </summary>
        public Vector3D Normalised => Normalise(this);
        
        /// <summary>
        /// Calculates normalised (unit) form of a vector
        /// </summary>
        /// <param name="a">Vector to normalise</param>
        /// <returns>normalised vector</returns>
        public static Vector3D Normalise(Vector3D a) => a / a.Magnitude;

        /// <summary>
        /// Custom vector equality comparison. accounts for doubleing point error margins
        /// </summary>
        /// <param name="a">LHS</param>
        /// <param name="b">RHS</param>
        /// <returns></returns>
        public static bool Equals(Vector3D a, Vector3D b) => Math.Abs(a.x - b.x) < Maths.Tolerance &&
                                                             Math.Abs(a.y - b.y) < Maths.Tolerance &&
                                                             Math.Abs(a.z - b.z) < Maths.Tolerance;

        /// <summary>
        /// Dot product of two vectors
        /// </summary>
        /// <param name="a">LHS</param>
        /// <param name="b">RHS</param>
        /// <returns></returns>
        public static double Dot(Vector3D a, Vector3D b) => (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
        
        /// <summary>
        /// Dot product of two vectors. LHS is vector performed upon.
        /// </summary>
        /// <param name="b">RHS</param>
        /// <returns></returns>
        public double Dot(Vector3D b) => Dot(this, b);

        /// <summary>
        /// Distance between two vector positions
        /// </summary>
        /// <param name="a">LHS</param>
        /// <param name="b">RHS</param>
        /// <returns></returns>
        public static double Distance(Vector3D a, Vector3D b) => Math.Sqrt(DistanceSquared(a, b));
        
        /// <summary>
        /// Distance squared between two vector positions
        /// </summary>
        /// <param name="a">LHS</param>
        /// <param name="b">RHS</param>
        /// <returns></returns>
        public static double DistanceSquared(Vector3D a, Vector3D b)
        {
            double dx = a.x - b.x;
            double dy = a.y - b.y;
            double dz = a.z - b.z;

            return dx * dx + dy * dy + dz * dz;
        }

        /// <summary>
        /// Angle between two vectors
        /// </summary>
        /// <param name="a">LHS</param>
        /// <param name="b">RHS</param>
        /// <returns>Angle in radians</returns>
        public static double Angle(Vector3D a, Vector3D b) => Math.Acos(Dot(a, b) / (a.Magnitude * b.Magnitude));

        /// <summary>
        /// Cross product of two vectors
        /// </summary>
        /// <param name="a">LHS</param>
        /// <param name="b">RHS</param>
        /// <returns></returns>
        public static Vector3D Cross(Vector3D a, Vector3D b) => new(
            a.y * b.z - a.z * b.y,
            a.z * b.x - a.x * b.z,
            a.x * b.y - a.y * b.x);

        /// <summary>
        /// clamps the magnitude of a Vector to a specified value
        /// </summary>
        /// <param name="a">Vector to clamp</param>
        /// <param name="maxlength">value to clamp to</param>
        /// <returns>Clamped vector</returns>
        public static Vector3D ClampMagnitude(Vector3D a, double maxlength) => a.Magnitude > maxlength ? Normalise(a) * maxlength : a;

        /// <summary>
        /// calculates the midpoint of two vectors - same as Lerp(a, b, 0.5)
        /// </summary>
        /// <param name="a">Vector From</param>
        /// <param name="b">Vector To</param>
        /// <returns></returns>
        public static Vector3D Midpoint(Vector3D a, Vector3D b) => a + (b - a) * 0.5f;

        /// <summary>
        /// Calculates a point a specified distance along the line between a and b
        /// </summary>
        /// <param name="a">Vector from</param>
        /// <param name="b">Vector to</param>
        /// <param name="t">fractional amount of total distance</param>
        /// <returns></returns>
        public static Vector3D Lerp(Vector3D a, Vector3D b, double t) => a + (b - a) * t;

        /// <summary>
        /// Calculates the vector from a to b
        /// </summary>
        /// <param name="a">Vector From</param>
        /// <param name="b">Vector To</param>
        /// <returns></returns>
        public static Vector3D Between(Vector3D a, Vector3D b) => b - a;
        
        /// <summary>
        /// Calculates vector From another vector To this vector
        /// </summary>
        /// <param name="other">Vector from</param>
        /// <returns></returns>
        public Vector3D From(Vector3D other) => Between(other, this);
        
        /// <summary>
        /// Calculates vector To another vector From this vector
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector3D To(Vector3D other) => Between(this, other);

        /// <summary>
        /// Returns a vector with the largest values for each component of each of the vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Maximised vector</returns>
        public static Vector3D Max(Vector3D a, Vector3D b) => new(
            Math.Max(a.x, b.x),
            Math.Max(a.y, b.y),
            Math.Max(a.z, b.z));

        /// <summary>
        /// Returns a vector with the smalled values for each component of each of the vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Minimised vector</returns>
        public static Vector3D Min(Vector3D a, Vector3D b) => new(
            Math.Min(a.x, b.x),
            Math.Min(a.y, b.y),
            Math.Min(a.z, b.z));

        /// <summary>
        /// Projects a vector onto another vector
        /// </summary>
        /// <param name="a">Vector to project</param>
        /// <param name="b">Vector projected to</param>
        /// <returns>Projected vector</returns>
        public static Vector3D Project(Vector3D a, Vector3D b) => b * Dot(a, b);
    }

    public sealed partial record Vector3D
    {
        // component-wise operations

        // vector * vector is the Scalar Product of the vectors
        public static Vector3D operator *(Vector3D a, Vector3D b) => new(
            a.x * b.x,
            a.y * b.y,
            a.z * b.z);

        public static Vector3D operator *(Vector3D v, double s) => new(
            v.x * s,
            v.y * s,
            v.z * s);

        public static Vector3D operator *(double s, Vector3D v) => v * s;

        // Scalar Division of vectors
        public static Vector3D operator /(Vector3D a, Vector3D b) => new(
            a.x / b.x,
            a.y / b.y,
            a.z / b.z);

        public static Vector3D operator /(Vector3D a, double s) => new(
            a.x / s,
            a.y / s,
            a.z / s);

        public static Vector3D operator +(Vector3D a, Vector3D b) => new(
            a.x + b.x,
            a.y + b.y,
            a.z + b.z);

        public static Vector3D operator -(Vector3D a, Vector3D b) => new(
            a.x - b.x,
            a.y - b.y,
            a.z - b.z);
    }

    public sealed partial record Vector3D
    {
        // default vector values
        public static Vector3D Zero => new(0, 0, 0);

        public static Vector3D One => new(1, 1, 1);

        public static Vector3D X => new(1, 0, 0);

        public static Vector3D Y => new(0, 1, 0);

        public static Vector3D Z => new(0, 0, 1);
    }
}