using System;


namespace MathsLib
{
    /*
     * Constructors
     */
    public sealed partial record Matrix4D
    {
        /// <summary>
        /// X (Forward) Vector component
        /// </summary>
        public Vector4D F { get; }
        
        /// <summary>
        /// Y (Up) Vector component
        /// </summary>
        public Vector4D U { get; }
        
        /// <summary>
        /// Z (Right) Vector component
        /// </summary>
        public Vector4D R { get; }
        
        /// <summary>
        /// W Vector component
        /// </summary>
        public Vector4D W { get; }

        public override string ToString()
        {
            Matrix4D t = Transpose;
            return $"[ {t.F}\n  {t.U}\n  {t.R}\n  {t.W} ]";
        }

        /// <summary>
        /// implicit conversion from 3 by 3 Matrix.
        /// W Vector component is (0, 0, 0, 1).
        /// </summary>
        /// <param name="m">3 by 3 matrix to convert</param>
        /// <returns></returns>
        public static implicit operator Matrix4D(Matrix3D m) => new(m.F, m.U, m.R, Vector4D.W);

        /// <summary>
        /// Constructor from 3 3D Vectors.
        /// W Vector component is (0, 0, 0, 1)
        /// </summary>
        /// <param name="f"></param>
        /// <param name="u"></param>
        /// <param name="r"></param>
        public Matrix4D(Vector3D f, Vector3D u, Vector3D r) : this(f, u, r, Vector4D.W) { }

        /// <summary>
        /// Constructor from 3 by 3 Matrix with a specified W Vector component
        /// </summary>
        /// <param name="m">3 by 3 Matrix</param>
        /// <param name="w">W Vector component</param>
        public Matrix4D(Matrix3D m, Vector4D w) : this(m.F, m.U, m.R, w) { }

        /// <summary>
        /// Default Constructor from 4 4D Vectors.
        /// </summary>
        /// <param name="F">X (Forward) Vector component</param>
        /// <param name="U">Y (Up) Vector component</param>
        /// <param name="R">Z (Right) Vector component</param>
        /// <param name="W">W Vector component</param>
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
        
        
        /// <summary>
        /// Multiplication operator between a 4D Matrix and 4D Vector
        /// </summary>
        /// <param name="m">4D matrix (lhs)</param>
        /// <param name="v">4D vector (rhs)</param>
        /// <returns></returns>
        public static Vector4D operator *(Matrix4D m, Vector4D v) => new(
             m.F.x * v.x + m.U.x * v.y + m.R.x * v.z + m.W.x * v.w,
             m.F.y * v.x + m.U.y * v.y + m.R.y * v.z + m.W.y * v.w,
             m.F.z * v.x + m.U.z * v.y + m.R.z * v.z + m.W.z * v.w, 
             m.F.w * v.x + m.U.w * v.y + m.R.w * v.z + m.W.w * v.w
        );
        
        /// <summary>
        /// Multiplication operator between a 4D Matrix and 3D Vector.
        /// Note: W component of each component vector in the Matrix is ignored
        /// </summary>
        /// <param name="m">4D matrix (lhs)</param>
        /// <param name="v">3D vector (rhs)</param>
        /// <returns></returns>
        public static Vector3D operator *(Matrix4D m, Vector3D v) => new(
            m.F.x * v.x + m.U.x * v.y + m.R.x * v.z + m.W.x,
            m.F.y * v.x + m.U.y * v.y + m.R.y * v.z + m.W.y,
            m.F.z * v.x + m.U.z * v.y + m.R.z * v.z + m.W.z
        );
        /* old erroneous function:
         public static Vector4D operator *(Matrix4D m, Vector3D v) => new(
            m.F.x * v.x + m.U.x * v.y + m.R.x * v.z,
            m.F.y * v.x + m.U.y * v.y + m.R.y * v.z,
            m.F.z * v.x + m.U.z * v.y + m.R.z * v.z,
            1
        ); */
        
        /// <summary>
        /// Multiplication operator between two 4 by 4 Matrices.
        /// Identical to four Matrix4D * Vector4D operations - one for each column of the second matrix
        /// </summary>
        /// <param name="a">4D Matrix (lhs)</param>
        /// <param name="b">4D Matrix (rhs)</param>
        /// <returns></returns>
        public static Matrix4D operator *(Matrix4D a, Matrix4D b) => new(a * b.F, a * b.U, a * b.R, a * b.W);
        
        
        //public static Vector4D operator *(Matrix4D m, Vector4D v) => m.Transpose.MultiplyTransposed(v);
        //public static Vector4D operator *(Matrix4D m, Vector3D v) => m.Transpose.MultiplyTransposed(new Vector4D(v));

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

        /// <summary>
        /// 4 by 4 Identity Matrix
        /// </summary>
        public static Matrix4D Identity => new(
            Vector4D.X,
            Vector4D.Y,
            Vector4D.Z,
            Vector4D.W);

        /// <summary>
        /// Transpose of the Matrix
        /// </summary>
        public Matrix4D Transpose => new(
            new Vector4D(F.x, U.x, R.x, W.x),
            new Vector4D(F.y, U.y, R.y, W.y),
            new Vector4D(F.z, U.z, R.z, W.z),
            new Vector4D(F.w, U.w, R.w, W.w)
        );
        
        /// <summary>
        /// Calculates a scale matrix based on 3 values
        /// </summary>
        /// <param name="scaleX">X scale factor</param>
        /// <param name="scaleY">Y scale factor</param>
        /// <param name="scaleZ">Z scale factor</param>
        /// <returns></returns>
        public static Matrix4D Scale(float scaleX, float scaleY, float scaleZ) => new(
            new Vector3D(scaleX, 0, 0),
            new Vector3D(0, scaleY, 0),
            new Vector3D(0, 0, scaleZ));

        /// <summary>
        /// Calculates a scale matrix based on a Vector3D holding individual scale factors
        /// </summary>
        /// <param name="scaleVector">3D scale vector</param>
        /// <returns></returns>
        public static Matrix4D Scale(Vector3D scaleVector) => new(
            new Vector3D(scaleVector.x, 0, 0),
            new Vector3D(0, scaleVector.y, 0),
            new Vector3D(0, 0, scaleVector.z)
        );

        /// <summary>
        /// Calculates a scale matrix based on a common scale factor for x, y, and z
        /// </summary>
        /// <param name="scale">scale factor</param>
        /// <returns></returns>
        public static Matrix4D Scale(float scale) => new(
            new Vector3D(scale, 0, 0),
            new Vector3D(0, scale, 0),
            new Vector3D(0, 0, scale)
        );
        
        /// <summary>
        /// Calculates a translation matrix based on a 3D vector representing world position
        /// </summary>
        /// <param name="worldPos">3D world position vector</param>
        /// <returns></returns>
        public static Matrix4D Translation(Vector3D worldPos) => new(Matrix3D.Identity, new Vector4D(worldPos));
        
        /// <summary>
        /// Calculates a Transformation Matrix based on provided translation, rotation, and scale matrices
        /// order of operations: Scale -> Rotate -> Translate
        /// </summary>
        /// <param name="translation">translation matrix</param>
        /// <param name="rotation">rotation matrix</param>
        /// <param name="scale">scale matrix</param>
        /// <returns></returns>
        public static Matrix4D TRS(Matrix4D translation, Matrix4D rotation, Matrix4D scale) => translation * (rotation * scale);

        /// <summary>
        /// Constructs a Transformation Matrix based on rotation matrix and translation and scale vectors, using a formula I derived
        /// </summary>
        /// <param name="translation">3D translation matrix</param>
        /// <param name="rotation">4D rotation matrix</param>
        /// <param name="scale">3D scale vector</param>
        /// <returns></returns>
        public static Matrix4D TRS(Vector3D translation, Matrix4D rotation, Vector3D scale)
        {
            return new Matrix4D(rotation.F * scale.x, rotation.U * scale.y, rotation.R * scale.z, new Vector4D(translation, 1));
        }
        
        
        /// <summary>
        /// Assuming the original matrix is a translation matrix, calculates the inverse translation
        /// </summary>
        public Matrix4D InverseTranslation => new(Matrix3D.Identity, new Vector4D(-W.x, -W.y, -W.z, 1));

        /// <summary>
        /// Assuming the original matrix is a rotation matrix, calculates the inverse rotation
        /// </summary>
        public Matrix4D InverseRotation => Transpose;

        /// <summary>
        /// Assuming the original matrix is a scale matrix, calculates the inverse scale
        /// </summary>
        public Matrix4D InverseScale => new Matrix3D(
            new Vector3D(1 / F.x, 0, 0),
            new Vector3D(0, 1 / U.y, 0),
            new Vector3D(0, 0, 1 / R.z));

        /// <summary>
        /// Calculates an Inverse Transformation Matrix based on provided inverse translation, inverse rotation, and inverse scale matrices.
        /// Primarily used for Inverse Transformations
        /// order of operations: Translate -> Rotate -> Scale
        /// </summary>
        /// <param name="scale">inverse scale matrix</param>
        /// <param name="rotation">inverse rotation matrix</param>
        /// <param name="translation">inverse translation matrix</param>
        /// <returns></returns>
        public static Matrix4D SRT(Matrix4D scale, Matrix4D rotation, Matrix4D translation) => scale * (rotation * translation);

        /// <summary>
        /// Constructs an Inverse Transformation Matrix based on rotation matrix and translation and scale vectors, using a formula I derived.
        /// DOES NOT TAKE INVERSE PARAMETERS. 
        /// </summary>
        /// <param name="scale">3D scale vector</param>
        /// <param name="rotation">4D rotation matrix</param>
        /// <param name="translation">3D translation vector</param>
        /// <returns></returns>
        public static Matrix4D SRT(Vector3D scale, Matrix4D rotation, Vector3D translation)
        {
            Matrix4D M = new(
                 new Vector4D(rotation.F, -Vector3D.Dot(rotation.F, translation)) / scale.x, 
                new Vector4D(rotation.U, -Vector3D.Dot(rotation.U, translation)) / scale.y, 
                new Vector4D(rotation.R, -Vector3D.Dot(rotation.R, translation)) / scale.z,
                Vector4D.W
                 );
            return M.Transpose;
        }

        /// <summary>
        /// Assuming matrix is a rotation matrix, calculates the rotation as Euler Angles
        /// </summary>
        /// <returns>3D vector representing Euler Angles</returns>
        public Vector3D EulerAngles()
        {
            /*
             * Maths for calculation from the OpenGL FAQ on Matrices and Quaternions, Question 37 (Various., n.d.)
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
        /// <summary>
        /// Constructs a Rotation matrix from Euler Angles
        /// </summary>
        /// <param name="angles">3D vector representing Euler Angles</param>
        /// <returns></returns>
        public static Matrix4D Rotation(Vector3D angles)
        {
            /*
             * Maths for calculation from the OpenGL FAQ on Matrices and Quaternions, Question 36 (Various., n.d.)
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

        /// <summary>
        /// Constructs a Rotation matrix from a Quaternion
        /// </summary>
        /// <param name="Q">Quaternion</param>
        /// <returns></returns>
        public static Matrix4D Rotation(Quaternion Q)
        {
            /*
             * Maths for calculation from the OpenGL FAQ on Matrices and Quaternions, Question 54 (Various., n.d.)
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
        
        /// <summary>
        /// Constructs a Rotation matrix from an angle and axis
        /// </summary>
        /// <param name="theta">angle</param>
        /// <param name="axis">axis of rotation</param>
        /// <returns></returns>
        public static Matrix4D Rotation(float theta, Vector3D axis)
        {
            /*
             * Maths for calculation from the OpenGL FAQ on Matrices and Quaternions, Question 38 (Various., n.d.)
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