using System;

namespace MathsLib
{
    public sealed partial record Vector4D
    {
        public float x { get; }
        public float y { get; }
        public float z { get; }
        public float w { get; }

        /*public Vector4D(Vector4D a) : this (
            a.x,
            a.y,
            a.z,
            a.w) {}*/

        public Vector4D(Vector3D a, float w = 1) : this(
            a.x,
            a.y,
            a.z,
            w) { }

        public Vector4D(float X, float Y, float Z, float W)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        public (float x, float y, float z, float w) Tuple => (x, y, z, w);

        // implicit conversion from 3D vector
        public static implicit operator Vector4D(Vector3D v) => new(
            v.x,
            v.y,
            v.z,
            0);

        public override string ToString() =>
            $"{MathF.Round(x, 4)}, {MathF.Round(y, 4)}, {MathF.Round(z, 4)}, {MathF.Round(w, 4)}";

        public void Deconstruct(out float x, out float y, out float z, out float w)
        {
            x = this.x;
            y = this.y;
            z = this.z;
            w = this.w;
        }
    }



    public sealed partial record Vector4D
    {
        public float Magnitude => MathF.Sqrt(x * x + y * y + z * z + w * w);

        public Vector4D Normalised => Normalise(this);
        public static Vector4D Normalise(Vector4D a) => a / a.Magnitude;

        public static bool Equals(Vector4D a, Vector4D b) =>
            MathF.Abs(a.x - b.x) < Maths.Tolerance && MathF.Abs(a.y - b.y) < Maths.Tolerance &&
            MathF.Abs(a.z - b.z) < Maths.Tolerance &&
            MathF.Abs(a.w - b.w) < Maths.Tolerance;
        
        public static float Dot(Vector4D a, Vector4D b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;

        public static float Angle(Vector4D a, Vector4D b) => MathF.Acos(Dot(a, b) / (a.Magnitude * b.Magnitude));

    }


    public sealed partial record Vector4D
    {
        public static Vector4D operator *(Vector4D a, Vector4D b) => new(
            a.x * b.x,
            a.y * b.y,
            a.z * b.z,
            a.w * b.w);

        public static Vector4D operator *(Vector4D a, float s) => new(
            a.x * s,
            a.y * s,
            a.z * s,
            a.w * s);

        public static Vector4D operator *(float s, Vector4D a) => a * s;

        public static Vector4D operator /(Vector4D a, Vector4D b) => new(
            a.x / b.x,
            a.y / b.y,
            a.z / b.z,
            a.w / b.w);

        public static Vector4D operator /(Vector4D a, float s) => new(
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