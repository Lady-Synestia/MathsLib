using System;

namespace MathsLib
{
    public sealed partial record Vector3D(float x, float y, float z)
    {
        public float x { get; } = x;
        public float y { get; } = y;
        public float z { get; } = z;

        // copy constructor
        //public Vector3D(Vector3D a) : this (a.x, a.y, a.z) { }

        // implicit conversion from 4D Vector
        public static implicit operator Vector3D(Vector4D v) => new(v.x, v.y, v.z);

        // x: pitch, y: yaw, z: roll
        public static Vector3D FromAngles(Vector3D angles) => new(
            MathF.Cos(angles.y) * MathF.Cos(angles.x),
            MathF.Sin(angles.x),
            MathF.Cos(angles.x) * MathF.Sin(angles.y));

        public static Vector3D FromAngles2D(float roll) => new(
            MathF.Cos(roll),
            MathF.Sin(roll),
            0);


        public string ToString(int digits) => $"{x.ToString(digits)}, {y.ToString(digits)}, {z.ToString(digits)}";
        public override string ToString() => $"{x}, {y}, {z}";
        
        // return values as tuple
        public (float x, float y, float z) Tuple => (x, y, z);
    }


    public sealed partial record Vector3D
    {
        // vector operations

        // using pythagoras
        public float Magnitude => MathF.Sqrt(x * x + y * y + z * z);
        public float SqrMagnitude => x * x + y * y + z * z;
        public Vector3D Normalised => Normalise(this);
        
        public static Vector3D Normalise(Vector3D a) => a / a.Magnitude;

        // comparing two Vectors
        public static bool Equals(Vector3D a, Vector3D b) => MathF.Abs(a.x - b.x) < Maths.Tolerance &&
                                                             MathF.Abs(a.y - b.y) < Maths.Tolerance &&
                                                             MathF.Abs(a.z - b.z) < Maths.Tolerance;
        //public static bool operator ==(Vector3D a, Vector3D b) => Equals(a, b);
        //public static bool operator !=(Vector3D a, Vector3D b) => !Equals(a, b);

        // Dot product of two vectors
        public static float Dot(Vector3D a, Vector3D b) => (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
        
        public float Dot(Vector3D b) => Dot(this, b); // concise syntax

        // Distance between two vector positions
        public static float Distance(Vector3D a, Vector3D b) => MathF.Sqrt(DistanceSquared(a, b));
        
        public static float DistanceSquared(Vector3D a, Vector3D b)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            float dz = a.z - b.z;

            return dx * dx + dy * dy + dz * dz;
        }

        // Angle between two vectors
        public static float Angle(Vector3D a, Vector3D b) => MathF.Acos(Dot(a, b) / (a.Magnitude * b.Magnitude));

        // Cross product of two vectors
        public static Vector3D Cross(Vector3D a, Vector3D b) => new(
            a.y * b.z - a.z * b.y,
            a.z * b.x - a.x * b.z,
            a.x * b.y - a.y * b.x);

        // clamps the magnitude of a Vector, returns new vector
        public static Vector3D ClampMagnitude(Vector3D a, float maxlength) =>
            a.Magnitude > maxlength ? Normalise(a) * maxlength : a;

        // calculates the midpoint of two vectors - same as Lerp(a, b, 0.5)
        public static Vector3D Midpoint(Vector3D a, Vector3D b) => a + (b - a) * 0.5f;

        // calculates point on the line between a and b
        public static Vector3D Lerp(Vector3D a, Vector3D b, float t) => a + (b - a) * t;

        // calculates the vector from a to b
        public static Vector3D Between(Vector3D a, Vector3D b) => b - a;
        public Vector3D From(Vector3D other) => Between(other, this); // concise syntax for second operand
        public Vector3D To(Vector3D other) => Between(this, other); // concise syntax for first operand

        // returns a vector with the largest values of each of the vectors
        public static Vector3D Max(Vector3D a, Vector3D b) => new(
            MathF.Max(a.x, b.x),
            MathF.Max(a.y, b.y),
            MathF.Max(a.z, b.z));

        // returns a vector with the smallest values of each of the vectors
        public static Vector3D Min(Vector3D a, Vector3D b) => new(
            MathF.Min(a.x, b.x),
            MathF.Min(a.y, b.y),
            MathF.Min(a.z, b.z));

        // projects a vector onto another vector
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

        public static Vector3D operator *(Vector3D v, float s) => new(
            v.x * s,
            v.y * s,
            v.z * s);

        public static Vector3D operator *(float s, Vector3D v) => v * s;

        public static Vector3D operator /(Vector3D a, Vector3D b) => new(
            a.x / b.x,
            a.y / b.y,
            a.z / b.z);

        public static Vector3D operator /(Vector3D a, float s) => new(
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