namespace Zene.Structs
{
    public static class MatExts
    {
        //
        // Mat2
        //

        public static OpenTK.Mathematics.Matrix2 ToMatTK(this Matrix2 obj)
        {
            return new OpenTK.Mathematics.Matrix2(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf());
        }
        public static OpenTK.Mathematics.Matrix2x3 ToMatTK(this Matrix2x3 obj)
        {
            return new OpenTK.Mathematics.Matrix2x3(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf());
        }
        public static OpenTK.Mathematics.Matrix2x4 ToMatTK(this Matrix2x4 obj)
        {
            return new OpenTK.Mathematics.Matrix2x4(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf());
        }

        public static Matrix2 ToMatZ(this OpenTK.Mathematics.Matrix2 obj)
        {
            return new Matrix2(obj.Row0.ToVecZ(), obj.Row1.ToVecZ());
        }
        public static Matrix2x3 ToMatZ(this OpenTK.Mathematics.Matrix2x3 obj)
        {
            return new Matrix2x3(obj.Row0.ToVecZ(), obj.Row1.ToVecZ());
        }
        public static Matrix2x4 ToMatZ(this OpenTK.Mathematics.Matrix2x4 obj)
        {
            return new Matrix2x4(obj.Row0.ToVecZ(), obj.Row1.ToVecZ());
        }

        public static OpenTK.Mathematics.Matrix2 ToMatTK<T>(this Matrix2<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Matrix2(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf());
        }
        public static OpenTK.Mathematics.Matrix2x3 ToMatTK<T>(this Matrix2x3<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Matrix2x3(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf());
        }
        public static OpenTK.Mathematics.Matrix2x4 ToMatTK<T>(this Matrix2x4<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Matrix2x4(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf());
        }

        public static Matrix2<T> ToMatZ<T>(this OpenTK.Mathematics.Matrix2 obj) where T : unmanaged
        {
            return new Matrix2<T>(obj.Row0.ToVecZ<T>(), obj.Row1.ToVecZ<T>());
        }
        public static Matrix2x3<T> ToMatZ<T>(this OpenTK.Mathematics.Matrix2x3 obj) where T : unmanaged
        {
            return new Matrix2x3<T>(obj.Row0.ToVecZ<T>(), obj.Row1.ToVecZ<T>());
        }
        public static Matrix2x4<T> ToMatZ<T>(this OpenTK.Mathematics.Matrix2x4 obj) where T : unmanaged
        {
            return new Matrix2x4<T>(obj.Row0.ToVecZ<T>(), obj.Row1.ToVecZ<T>());
        }

        //
        // Mat3
        //

        public static OpenTK.Mathematics.Matrix3 ToMatTK(this Matrix3 obj)
        {
            return new OpenTK.Mathematics.Matrix3(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf(), obj.Row2.ToVecTKf());
        }
        public static OpenTK.Mathematics.Matrix3x2 ToMatTK(this Matrix3x2 obj)
        {
            return new OpenTK.Mathematics.Matrix3x2(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf(), obj.Row2.ToVecTKf());
        }
        public static OpenTK.Mathematics.Matrix3x4 ToMatTK(this Matrix3x4 obj)
        {
            return new OpenTK.Mathematics.Matrix3x4(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf(), obj.Row2.ToVecTKf());
        }

        public static Matrix3 ToMatZ(this OpenTK.Mathematics.Matrix3 obj)
        {
            return new Matrix3(obj.Row0.ToVecZ(), obj.Row1.ToVecZ(), obj.Row2.ToVecZ());
        }
        public static Matrix3x2 ToMatZ(this OpenTK.Mathematics.Matrix3x2 obj)
        {
            return new Matrix3x2(obj.Row0.ToVecZ(), obj.Row1.ToVecZ(), obj.Row2.ToVecZ());
        }
        public static Matrix3x4 ToMatZ(this OpenTK.Mathematics.Matrix3x4 obj)
        {
            return new Matrix3x4(obj.Row0.ToVecZ(), obj.Row1.ToVecZ(), obj.Row2.ToVecZ());
        }

        public static OpenTK.Mathematics.Matrix3 ToMatTK<T>(this Matrix3<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Matrix3(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf(), obj.Row2.ToVecTKf());
        }
        public static OpenTK.Mathematics.Matrix3x2 ToMatTK<T>(this Matrix3x2<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Matrix3x2(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf(), obj.Row2.ToVecTKf());
        }
        public static OpenTK.Mathematics.Matrix3x4 ToMatTK<T>(this Matrix3x4<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Matrix3x4(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf(), obj.Row2.ToVecTKf());
        }

        public static Matrix3<T> ToMatZ<T>(this OpenTK.Mathematics.Matrix3 obj) where T : unmanaged
        {
            return new Matrix3<T>(obj.Row0.ToVecZ<T>(), obj.Row1.ToVecZ<T>(), obj.Row2.ToVecZ<T>());
        }
        public static Matrix3x2<T> ToMatZ<T>(this OpenTK.Mathematics.Matrix3x2 obj) where T : unmanaged
        {
            return new Matrix3x2<T>(obj.Row0.ToVecZ<T>(), obj.Row1.ToVecZ<T>(), obj.Row2.ToVecZ<T>());
        }
        public static Matrix3x4<T> ToMatZ<T>(this OpenTK.Mathematics.Matrix3x4 obj) where T : unmanaged
        {
            return new Matrix3x4<T>(obj.Row0.ToVecZ<T>(), obj.Row1.ToVecZ<T>(), obj.Row2.ToVecZ<T>());
        }

        //
        // Mat4
        //

        public static OpenTK.Mathematics.Matrix4 ToMatTK(this Matrix4 obj)
        {
            return new OpenTK.Mathematics.Matrix4(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf(), obj.Row2.ToVecTKf(), obj.Row3.ToVecTKf());
        }
        public static OpenTK.Mathematics.Matrix4x2 ToMatTK(this Matrix4x2 obj)
        {
            return new OpenTK.Mathematics.Matrix4x2(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf(), obj.Row2.ToVecTKf(), obj.Row3.ToVecTKf());
        }
        public static OpenTK.Mathematics.Matrix4x3 ToMatTK(this Matrix4x3 obj)
        {
            return new OpenTK.Mathematics.Matrix4x3(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf(), obj.Row2.ToVecTKf(), obj.Row3.ToVecTKf());
        }

        public static Matrix4 ToMatZ(this OpenTK.Mathematics.Matrix4 obj)
        {
            return new Matrix4(obj.Row0.ToVecZ(), obj.Row1.ToVecZ(), obj.Row2.ToVecZ(), obj.Row3.ToVecZ());
        }
        public static Matrix4x2 ToMatZ(this OpenTK.Mathematics.Matrix4x2 obj)
        {
            return new Matrix4x2(obj.Row0.ToVecZ(), obj.Row1.ToVecZ(), obj.Row2.ToVecZ(), obj.Row3.ToVecZ());
        }
        public static Matrix4x3 ToMatZ(this OpenTK.Mathematics.Matrix4x3 obj)
        {
            return new Matrix4x3(obj.Row0.ToVecZ(), obj.Row1.ToVecZ(), obj.Row2.ToVecZ(), obj.Row3.ToVecZ());
        }

        public static OpenTK.Mathematics.Matrix4 ToMatTK<T>(this Matrix4<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Matrix4(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf(), obj.Row2.ToVecTKf(), obj.Row3.ToVecTKf());
        }
        public static OpenTK.Mathematics.Matrix4x2 ToMatTK<T>(this Matrix4x2<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Matrix4x2(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf(), obj.Row2.ToVecTKf(), obj.Row3.ToVecTKf());
        }
        public static OpenTK.Mathematics.Matrix4x3 ToMatTK<T>(this Matrix4x3<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Matrix4x3(obj.Row0.ToVecTKf(), obj.Row1.ToVecTKf(), obj.Row2.ToVecTKf(), obj.Row3.ToVecTKf());
        }

        public static Matrix4<T> ToMatZ<T>(this OpenTK.Mathematics.Matrix4 obj) where T : unmanaged
        {
            return new Matrix4<T>(obj.Row0.ToVecZ<T>(), obj.Row1.ToVecZ<T>(), obj.Row2.ToVecZ<T>(), obj.Row3.ToVecZ<T>());
        }
        public static Matrix4x2<T> ToMatZ<T>(this OpenTK.Mathematics.Matrix4x2 obj) where T : unmanaged
        {
            return new Matrix4x2<T>(obj.Row0.ToVecZ<T>(), obj.Row1.ToVecZ<T>(), obj.Row2.ToVecZ<T>(), obj.Row3.ToVecZ<T>());
        }
        public static Matrix4x3<T> ToMatZ<T>(this OpenTK.Mathematics.Matrix4x3 obj) where T : unmanaged
        {
            return new Matrix4x3<T>(obj.Row0.ToVecZ<T>(), obj.Row1.ToVecZ<T>(), obj.Row2.ToVecZ<T>(), obj.Row3.ToVecZ<T>());
        }
    }
}
