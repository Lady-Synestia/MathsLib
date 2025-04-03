using System;


namespace MathsLib
{
    /*
     * Constructors
     */
    public sealed partial record Matrix4D
    {
        public Vector4D F { get; }
        public Vector4D U { get; }
        public Vector4D R { get; }
        public Vector4D W { get; }

        public override string ToString()
        {
            Matrix4D t = Transpose;
            return $"[ {t.F}\n  {t.U}\n  {t.R}\n  {t.W} ]";
        }

        // constructor from 3x3 matrix
        public static implicit operator Matrix4D(Matrix3D m) => new(m.F, m.U, m.R, Vector4D.W);

        // constructor from 3 3D vectors
        public Matrix4D(Vector3D f, Vector3D u, Vector3D r) : this(f, u, r, Vector4D.W) { }

        public Matrix4D(Matrix3D m, Vector4D w) : this(m.F, m.U, m.R, w) { }

        public Matrix4D(Vector4D F, Vector4D U, Vector4D R, Vector4D W)
        {
            this.F = F;
            this.U = U;
            this.R = R;
            this.W = W;
        }
    }

/*
 * Operators
 */
    public sealed partial record Matrix4D
    {
        // matrix multiplication is equal to taking the dot product between the transpose of the LHS and the RHS
        //public static Vector4D operator *(Matrix4D m, Vector4D v) => m.Transpose.MultiplyTransposed(v);
        public static Vector4D operator *(Matrix4D m, Vector4D v) => new(
             m.F.x * v.x + m.U.x * v.y + m.R.x * v.z + m.W.x * v.w,
             m.F.y * v.x + m.U.y * v.y + m.R.y * v.z + m.W.y * v.w,
             m.F.z * v.x + m.U.z * v.y + m.R.z * v.z + m.W.z * v.w, 
             m.F.w * v.x + m.U.w * v.y + m.R.w * v.z + m.W.w * v.w
        );

        //public static Vector4D operator *(Matrix4D m, Vector3D v) => m.Transpose.MultiplyTransposed(new Vector4D(v));
        /*public static Vector4D operator *(Matrix4D m, Vector3D v) => new(
            m.F.x * v.x + m.U.x * v.y + m.R.x * v.z,
            m.F.y * v.x + m.U.y * v.y + m.R.y * v.z,
            m.F.z * v.x + m.U.z * v.y + m.R.z * v.z,
            1
        ); */
        public static Vector3D operator *(Matrix4D m, Vector3D v) => new(
            m.F.x * v.x + m.U.x * v.y + m.R.x * v.z,
            m.F.y * v.x + m.U.y * v.y + m.R.y * v.z,
            m.F.z * v.x + m.U.z * v.y + m.R.z * v.z
        );

        /*public static Matrix4D operator *(Matrix4D a, Matrix4D b)
        {
            a = a.Transpose;
            return new Matrix4D(
                a.MultiplyTransposed(b.F),
                a.MultiplyTransposed(b.U),
                a.MultiplyTransposed(b.R),
                a.MultiplyTransposed(b.W)
            );
        }*/
        /*public static Matrix4D operator *(Matrix4D a, Matrix4D b) => new(
             new Vector4D(a.F.x * b.F.x + a.U.x * b.F.y + a.R.x * b.F.z + a.W.x * b.F.w, a.F.x * b.F.x + a.U.x * b.F.y + a.R.x * b.F.z + a.W.x * b.F.w, a.F.x * b.F.x + a.U.x * b.F.y + a.R.x * b.F.z + a.W.x * b.F.w, a.F.x * b.F.x + a.U.x * b.F.y + a.R.x * b.F.z + a.W.x * b.F.w),
            new Vector4D(a.F.y * b.U.x + a.U.y * b.U.y + a.R.y * b.U.z + a.W.y * b.U.w, a.F.y * b.U.x + a.U.y * b.U.y + a.R.y * b.U.z + a.W.y * b.U.w, a.F.y * b.U.x + a.U.y * b.U.y + a.R.y * b.U.z + a.W.y * b.U.w, a.F.y * b.U.x + a.U.y * b.U.y + a.R.y * b.U.z + a.W.y * b.U.w),
            new Vector4D(a.F.z * b.R.x + a.U.z * b.R.y + a.R.z * b.R.z + a.W.z * b.R.w, a.F.z * b.R.x + a.U.z * b.R.y + a.R.z * b.R.z + a.W.z * b.R.w, a.F.z * b.R.x + a.U.z * b.R.y + a.R.z * b.R.z + a.W.z * b.R.w, a.F.z * b.R.x + a.U.z * b.R.y + a.R.z * b.R.z + a.W.z * b.R.w),
            new Vector4D(a.F.w * b.W.x + a.U.w * b.W.y + a.R.w * b.W.z + a.W.w * b.W.w, a.F.w * b.W.x + a.U.y * b.W.y + a.R.w * b.W.z + a.W.w * b.W.w, a.F.w * b.W.x + a.U.w * b.W.y + a.R.w * b.W.z + a.W.w * b.W.w, a.F.w * b.W.x + a.U.w * b.W.y + a.R.w * b.W.z + a.W.w * b.W.w)
        );*/
        public static Matrix4D operator *(Matrix4D a, Matrix4D b) => new(a * b.F, a * b.U, a * b.R, a * b.W);
        

        /*private Vector4D MultiplyTransposed(Vector4D v) => new(
            Vector4D.Dot(F, v),
            Vector4D.Dot(U, v),
            Vector4D.Dot(R, v),
            Vector4D.Dot(W, v));*/
    }

/*
 * Methods
 */
    public sealed partial record Matrix4D
    {

        public static Matrix4D Identity => new(
            Vector4D.X,
            Vector4D.Y,
            Vector4D.Z,
            Vector4D.W);

        public Matrix4D Transpose => new(
            new Vector4D(F.x, U.x, R.x, W.x),
            new Vector4D(F.y, U.y, R.y, W.y),
            new Vector4D(F.z, U.z, R.z, W.z),
            new Vector4D(F.w, U.w, R.w, W.w)
        );
        
        public static Matrix4D Scale(float scaleX, float scaleY, float scaleZ) => new(
            new Vector3D(scaleX, 0, 0),
            new Vector3D(0, scaleY, 0),
            new Vector3D(0, 0, scaleZ));

        public static Matrix4D Scale(Vector3D scaleVector) => new(
            new Vector3D(scaleVector.x, 0, 0),
            new Vector3D(0, scaleVector.y, 0),
            new Vector3D(0, 0, scaleVector.z)
        );

        public static Matrix4D Scale(float scale) => new(
            new Vector3D(scale, 0, 0),
            new Vector3D(0, scale, 0),
            new Vector3D(0, 0, scale)
        );
        
        public static Matrix4D Translation(Vector3D worldPos) => new(Matrix3D.Identity, new Vector4D(worldPos));

        // order of operations: Scale -> Rotate -> Translate
        public static Matrix4D TRS(Matrix4D translation, Matrix4D rotation, Matrix4D scale) =>
            translation * (rotation * scale);
        
        public Matrix4D InverseTranslation => new(Matrix3D.Identity, new Vector4D(-W.x, -W.y, -W.z, 1));

        public Matrix4D InverseRotation => Transpose;

        public Matrix4D InverseScale => new Matrix3D(
            new Vector3D(1 / F.x, 0, 0),
            new Vector3D(0, 1 / U.y, 0),
            new Vector3D(0, 0, 1 / R.z));

        // order of operations: Translate -> Rotate -> Scale
        public static Matrix4D SRT(Matrix4D scale, Matrix4D rotation, Matrix4D translation) =>
            scale * (rotation * translation);

        public Vector3D EulerAngles()
        {
            /*
             * Maths for calculation from: https://www.opengl-tutorial.org/assets/faq_quaternions/index.html#Q37
             */

            float angleX;
            float angleZ;
            float angleY = MathF.Asin(F.z);
            float c = MathF.Cos(angleY);
            angleY *= Maths.Radians;

            if (MathF.Abs(angleY) > 0.005f)
            {
                float trX = R.z / c;
                float trY = -U.z / c;
                angleX = MathF.Atan2(trY, trX) * (180 / MathF.PI);

                trX = F.x / c;
                trY = -F.y / c;
                angleZ = MathF.Atan2(trY, trX) * (180 / MathF.PI);
            }
            else
            {
                angleX = 0;
                float trX = U.y;
                float trY = U.x;
                angleZ = MathF.Atan2(trY, trX) * (180 / MathF.PI);
            }

            if (angleX < 0)
            {
                angleX += 360;
            }

            if (angleY < 0)
            {
                angleY += 360;
            }

            if (angleZ < 0)
            {
                angleZ += 360;
            }

            return new Vector3D(angleX, angleY, angleZ);
        }
    }


/*
 * Rotation Matrices
 */
    public sealed partial record Matrix4D
    {
        // rotation matrix from euler angles
        public static Matrix4D Rotation(Vector3D angles)
        {
            /*
             * Maths for calculation from: https://www.opengl-tutorial.org/assets/faq_quaternions/index.html#Q36
             *
             * Simplified version of Matrix4D.RotationInefficient(Vector3D angles)
             */

            angles *= Maths.Radians;

            float cosX = MathF.Cos(angles.x);
            float sinX = MathF.Sin(angles.x);
            float cosY = MathF.Cos(angles.y);
            float sinY = MathF.Sin(angles.y);
            float cosZ = MathF.Cos(angles.z);
            float sinZ = MathF.Sin(angles.z);

            float cosXsinY = cosX * sinY;
            float sinXsinY = sinX * sinY;

            /*
             original code was incorrect as the example matrix was transposed

             return new Matrix4D(
                new Vector4D(C * E, BD * E + A * F, -AD * E + B * F,0),
                new Vector4D(-C * F, -BD * F + A * E, AD * F + B * E, 0),
                new Vector4D(D, -B * C, A * C, 0),
                new Vector4D(0, 0, 0, 1)
            );*/
            return new Matrix4D(
                new Vector4D(cosY * cosZ, -cosY * sinZ, sinY, 0),
                new Vector4D(sinXsinY * cosZ + cosX * sinZ, -sinXsinY * sinZ + cosX * cosZ, -sinX * cosY, 0),
                new Vector4D(-cosXsinY * cosZ + sinX * sinZ, cosXsinY * sinZ + sinX * cosZ, cosX * cosY, 0),
                new Vector4D(0, 0, 0, 1)
            );
        }

        // rotation matrix from quaternion
        public static Matrix4D Rotation(Quaternion Q)
        {
            /*
             * Maths for calculation from: https://www.opengl-tutorial.org/assets/faq_quaternions/index.html#Q54
             */

            //quaternion = quaternion.Normalised;

            float xx = Q.x * Q.x;
            float xy = Q.x * Q.y;
            float xz = Q.x * Q.z;
            float xw = Q.x * Q.w;

            float yy = Q.y * Q.y;
            float yz = Q.y * Q.z;
            float yw = Q.y * Q.w;

            float zz = Q.z * Q.z;
            float zw = Q.z * Q.w;

            return new Matrix4D(
                new Vector4D(1 - 2 * (yy + zz), 2 * (xy - zw), 2 * (xz + yw), 0),
                new Vector4D(2 * (xy + zw), 1 - 2 * (xx + zz), 2 * (yz - xw), 0),
                new Vector4D(2 * (xz - yw), 2 * (yz + xw), 1 - 2 * (xx + yy), 0),
                new Vector4D(0, 0, 0, 1)
            );
        }

        // rotation matrix from axis-angle
        public static Matrix4D Rotation(float theta, Vector3D axis)
        {
            /*
             * Maths for calculation from: https://www.opengl-tutorial.org/assets/faq_quaternions/index.html#Q38
             */

            axis = axis.Normalised;
            theta *= Maths.Radians;

            float cosTheta = MathF.Cos(theta);
            float sinTheta = MathF.Sin(theta);

            return new Matrix4D(
                new Vector4D(cosTheta + axis.x * axis.x * (1 - cosTheta),
                    -axis.z * sinTheta + axis.x * axis.y * (1 - cosTheta),
                    axis.y * sinTheta + axis.x * axis.z * (1 - cosTheta), 0),
                new Vector4D(axis.z * sinTheta + axis.y * axis.x * (1 - cosTheta),
                    cosTheta + axis.y * axis.y * (1 - cosTheta), -axis.x * sinTheta + axis.y * axis.z * (1 - cosTheta),
                    0),
                new Vector4D(-axis.y * sinTheta + axis.z * axis.x * (1 - cosTheta),
                    axis.x * sinTheta + axis.y * axis.z * (1 - cosTheta), cosTheta + axis.z * axis.z * (1 - cosTheta),
                    0),
                new Vector4D(0, 0, 0, 1)
            );
        }
    }
}