namespace Zene.Structs
{
    public static class VectExts
    {
        //
        // Vector2
        //

        public static OpenTK.Mathematics.Vector2 ToVecTKf(this Vector2 obj)
        {
            return new OpenTK.Mathematics.Vector2((float)obj.X, (float)obj.Y);
        }
        public static OpenTK.Mathematics.Vector2d ToVecTKd(this Vector2 obj)
        {
            return new OpenTK.Mathematics.Vector2d(obj.X, obj.Y);
        }
        public static OpenTK.Mathematics.Vector2h ToVecTKh(this Vector2 obj)
        {
            return new OpenTK.Mathematics.Vector2h((OpenTK.Mathematics.Half)obj.X, (OpenTK.Mathematics.Half)obj.Y);
        }
        public static OpenTK.Mathematics.Vector2i ToVecTKi(this Vector2 obj)
        {
            return new OpenTK.Mathematics.Vector2i((int)obj.X, (int)obj.Y);
        }

        public static Vector2 ToVecZ(this OpenTK.Mathematics.Vector2 obj)
        {
            return new Vector2(obj.X, obj.Y);
        }
        public static Vector2 ToVecZ(this OpenTK.Mathematics.Vector2d obj)
        {
            return new Vector2(obj.X, obj.Y);
        }
        public static Vector2 ToVecZ(this OpenTK.Mathematics.Vector2h obj)
        {
            return new Vector2(obj.X, obj.Y);
        }
        public static Vector2 ToVecZ(this OpenTK.Mathematics.Vector2i obj)
        {
            return new Vector2(obj.X, obj.Y);
        }

        //
        // Vector2I
        //

        public static OpenTK.Mathematics.Vector2 ToVecTKf(this Vector2I obj)
        {
            return new OpenTK.Mathematics.Vector2(obj.X, obj.Y);
        }
        public static OpenTK.Mathematics.Vector2d ToVecTKd(this Vector2I obj)
        {
            return new OpenTK.Mathematics.Vector2d(obj.X, obj.Y);
        }
        public static OpenTK.Mathematics.Vector2h ToVecTKh(this Vector2I obj)
        {
            return new OpenTK.Mathematics.Vector2h((OpenTK.Mathematics.Half)obj.X, (OpenTK.Mathematics.Half)obj.Y);
        }
        public static OpenTK.Mathematics.Vector2i ToVecTKi(this Vector2I obj)
        {
            return new OpenTK.Mathematics.Vector2i(obj.X, obj.Y);
        }

        public static Vector2I ToVecZi(this OpenTK.Mathematics.Vector2 obj)
        {
            return new Vector2I(obj.X, obj.Y);
        }
        public static Vector2I ToVecZi(this OpenTK.Mathematics.Vector2d obj)
        {
            return new Vector2I(obj.X, obj.Y);
        }
        public static Vector2I ToVecZi(this OpenTK.Mathematics.Vector2h obj)
        {
            return new Vector2I(obj.X, obj.Y);
        }
        public static Vector2I ToVecZi(this OpenTK.Mathematics.Vector2i obj)
        {
            return new Vector2I(obj.X, obj.Y);
        }

        //
        // Vector2T
        //

        public static OpenTK.Mathematics.Vector2 ToVecTKf<T>(this Vector2<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Vector2((float)(object)obj.X, (float)(object)obj.Y);
        }
        public static OpenTK.Mathematics.Vector2d ToVecTKd<T>(this Vector2<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Vector2d((double)(object)obj.X, (double)(object)obj.Y);
        }
        public static OpenTK.Mathematics.Vector2h ToVecTKh<T>(this Vector2<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Vector2h((OpenTK.Mathematics.Half)(object)obj.X, (OpenTK.Mathematics.Half)(object)obj.Y);
        }
        public static OpenTK.Mathematics.Vector2i ToVecTKi<T>(this Vector2<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Vector2i((int)(object)obj.X, (int)(object)obj.Y);
        }

        public static Vector2<T> ToVecZ<T>(this OpenTK.Mathematics.Vector2 obj) where T :unmanaged
        {
            return new Vector2<T>((T)(object)obj.X, (T)(object)obj.Y);
        }
        public static Vector2<T> ToVecZ<T>(this OpenTK.Mathematics.Vector2d obj) where T : unmanaged
        {
            return new Vector2<T>((T)(object)obj.X, (T)(object)obj.Y);
        }
        public static Vector2<T> ToVecZ<T>(this OpenTK.Mathematics.Vector2h obj) where T : unmanaged
        {
            return new Vector2<T>((T)(object)obj.X, (T)(object)obj.Y);
        }
        public static Vector2<T> ToVecZ<T>(this OpenTK.Mathematics.Vector2i obj) where T : unmanaged
        {
            return new Vector2<T>((T)(object)obj.X, (T)(object)obj.Y);
        }

        //
        // Vector3
        //

        public static OpenTK.Mathematics.Vector3 ToVecTKf(this Vector3 obj)
        {
            return new OpenTK.Mathematics.Vector3((float)obj.X, (float)obj.Y, (float)obj.Z);
        }
        public static OpenTK.Mathematics.Vector3d ToVecTKd(this Vector3 obj)
        {
            return new OpenTK.Mathematics.Vector3d(obj.X, obj.Y, obj.Z);
        }
        public static OpenTK.Mathematics.Vector3h ToVecTKh(this Vector3 obj)
        {
            return new OpenTK.Mathematics.Vector3h((OpenTK.Mathematics.Half)obj.X, (OpenTK.Mathematics.Half)obj.Y, (OpenTK.Mathematics.Half)obj.Z);
        }
        public static OpenTK.Mathematics.Vector3i ToVecTKi(this Vector3 obj)
        {
            return new OpenTK.Mathematics.Vector3i((int)obj.X, (int)obj.Y, (int)obj.Z);
        }

        public static Vector3 ToVecZ(this OpenTK.Mathematics.Vector3 obj)
        {
            return new Vector3(obj.X, obj.Y, obj.Z);
        }
        public static Vector3 ToVecZ(this OpenTK.Mathematics.Vector3d obj)
        {
            return new Vector3(obj.X, obj.Y, obj.Z);
        }
        public static Vector3 ToVecZ(this OpenTK.Mathematics.Vector3h obj)
        {
            return new Vector3(obj.X, obj.Y, obj.Z);
        }
        public static Vector3 ToVecZ(this OpenTK.Mathematics.Vector3i obj)
        {
            return new Vector3(obj.X, obj.Y, obj.Z);
        }

        //
        // Vector3I
        //

        public static OpenTK.Mathematics.Vector3 ToVecTKf(this Vector3I obj)
        {
            return new OpenTK.Mathematics.Vector3(obj.X, obj.Y, obj.Z);
        }
        public static OpenTK.Mathematics.Vector3d ToVecTKd(this Vector3I obj)
        {
            return new OpenTK.Mathematics.Vector3d(obj.X, obj.Y, obj.Z);
        }
        public static OpenTK.Mathematics.Vector3h ToVecTKh(this Vector3I obj)
        {
            return new OpenTK.Mathematics.Vector3h((OpenTK.Mathematics.Half)obj.X, (OpenTK.Mathematics.Half)obj.Y, (OpenTK.Mathematics.Half)obj.Z);
        }
        public static OpenTK.Mathematics.Vector3i ToVecTKi(this Vector3I obj)
        {
            return new OpenTK.Mathematics.Vector3i(obj.X, obj.Y, obj.Z);
        }

        public static Vector3I ToVecZi(this OpenTK.Mathematics.Vector3 obj)
        {
            return new Vector3I(obj.X, obj.Y, obj.Z);
        }
        public static Vector3I ToVecZi(this OpenTK.Mathematics.Vector3d obj)
        {
            return new Vector3I(obj.X, obj.Y, obj.Z);
        }
        public static Vector3I ToVecZi(this OpenTK.Mathematics.Vector3h obj)
        {
            return new Vector3I(obj.X, obj.Y, obj.Z);
        }
        public static Vector3I ToVecZi(this OpenTK.Mathematics.Vector3i obj)
        {
            return new Vector3I(obj.X, obj.Y, obj.Z);
        }

        //
        // Vector3T
        //

        public static OpenTK.Mathematics.Vector3 ToVecTKf<T>(this Vector3<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Vector3((float)(object)obj.X, (float)(object)obj.Y, (float)(object)obj.Z);
        }
        public static OpenTK.Mathematics.Vector3d ToVecTKd<T>(this Vector3<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Vector3d((double)(object)obj.X, (double)(object)obj.Y, (double)(object)obj.Z);
        }
        public static OpenTK.Mathematics.Vector3h ToVecTKh<T>(this Vector3<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Vector3h((OpenTK.Mathematics.Half)(object)obj.X, (OpenTK.Mathematics.Half)(object)obj.Y, (OpenTK.Mathematics.Half)(object)obj.Z);
        }
        public static OpenTK.Mathematics.Vector3i ToVecTKi<T>(this Vector3<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Vector3i((int)(object)obj.X, (int)(object)obj.Y, (int)(object)obj.Z);
        }

        public static Vector3<T> ToVecZ<T>(this OpenTK.Mathematics.Vector3 obj) where T : unmanaged
        {
            return new Vector3<T>((T)(object)obj.X, (T)(object)obj.Y, (T)(object)obj.Z);
        }
        public static Vector3<T> ToVecZ<T>(this OpenTK.Mathematics.Vector3d obj) where T : unmanaged
        {
            return new Vector3<T>((T)(object)obj.X, (T)(object)obj.Y, (T)(object)obj.Z);
        }
        public static Vector3<T> ToVecZ<T>(this OpenTK.Mathematics.Vector3h obj) where T : unmanaged
        {
            return new Vector3<T>((T)(object)obj.X, (T)(object)obj.Y, (T)(object)obj.Z);
        }
        public static Vector3<T> ToVecZ<T>(this OpenTK.Mathematics.Vector3i obj) where T : unmanaged
        {
            return new Vector3<T>((T)(object)obj.X, (T)(object)obj.Y, (T)(object)obj.Z);
        }

        //
        // Vector4
        //

        public static OpenTK.Mathematics.Vector4 ToVecTKf(this Vector4 obj)
        {
            return new OpenTK.Mathematics.Vector4((float)obj.X, (float)obj.Y, (float)obj.Z, (float)obj.W);
        }
        public static OpenTK.Mathematics.Vector4d ToVecTKd(this Vector4 obj)
        {
            return new OpenTK.Mathematics.Vector4d(obj.X, obj.Y, obj.Z, obj.W);
        }
        public static OpenTK.Mathematics.Vector4h ToVecTKh(this Vector4 obj)
        {
            return new OpenTK.Mathematics.Vector4h((OpenTK.Mathematics.Half)obj.X, (OpenTK.Mathematics.Half)obj.Y, (OpenTK.Mathematics.Half)obj.Z, (OpenTK.Mathematics.Half)obj.W);
        }
        public static OpenTK.Mathematics.Vector4i ToVecTKi(this Vector4 obj)
        {
            return new OpenTK.Mathematics.Vector4i((int)obj.X, (int)obj.Y, (int)obj.Z, (int)obj.W);
        }

        public static Vector4 ToVecZ(this OpenTK.Mathematics.Vector4 obj)
        {
            return new Vector4(obj.X, obj.Y, obj.Z, obj.W);
        }
        public static Vector4 ToVecZ(this OpenTK.Mathematics.Vector4d obj)
        {
            return new Vector4(obj.X, obj.Y, obj.Z, obj.W);
        }
        public static Vector4 ToVecZ(this OpenTK.Mathematics.Vector4h obj)
        {
            return new Vector4(obj.X, obj.Y, obj.Z, obj.W);
        }
        public static Vector4 ToVecZ(this OpenTK.Mathematics.Vector4i obj)
        {
            return new Vector4(obj.X, obj.Y, obj.Z, obj.W);
        }

        //
        // Vector4I
        //

        public static OpenTK.Mathematics.Vector4 ToVecTKf(this Vector4I obj)
        {
            return new OpenTK.Mathematics.Vector4(obj.X, obj.Y, obj.Z, obj.W);
        }
        public static OpenTK.Mathematics.Vector4d ToVecTKd(this Vector4I obj)
        {
            return new OpenTK.Mathematics.Vector4d(obj.X, obj.Y, obj.Z, obj.W);
        }
        public static OpenTK.Mathematics.Vector4h ToVecTKh(this Vector4I obj)
        {
            return new OpenTK.Mathematics.Vector4h((OpenTK.Mathematics.Half)obj.X, (OpenTK.Mathematics.Half)obj.Y, (OpenTK.Mathematics.Half)obj.Z, (OpenTK.Mathematics.Half)obj.W);
        }
        public static OpenTK.Mathematics.Vector4i ToVecTKi(this Vector4I obj)
        {
            return new OpenTK.Mathematics.Vector4i(obj.X, obj.Y, obj.Z, obj.W);
        }

        public static Vector4I ToVecZi(this OpenTK.Mathematics.Vector4 obj)
        {
            return new Vector4I(obj.X, obj.Y, obj.Z, obj.W);
        }
        public static Vector4I ToVecZi(this OpenTK.Mathematics.Vector4d obj)
        {
            return new Vector4I(obj.X, obj.Y, obj.Z, obj.W);
        }
        public static Vector4I ToVecZi(this OpenTK.Mathematics.Vector4h obj)
        {
            return new Vector4I(obj.X, obj.Y, obj.Z, obj.W);
        }
        public static Vector4I ToVecZi(this OpenTK.Mathematics.Vector4i obj)
        {
            return new Vector4I(obj.X, obj.Y, obj.Z, obj.W);
        }

        //
        // Vector4T
        //

        public static OpenTK.Mathematics.Vector4 ToVecTKf<T>(this Vector4<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Vector4((float)(object)obj.X, (float)(object)obj.Y, (float)(object)obj.Z, (float)(object)obj.W);
        }
        public static OpenTK.Mathematics.Vector4d ToVecTKd<T>(this Vector4<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Vector4d((double)(object)obj.X, (double)(object)obj.Y, (double)(object)obj.Z, (double)(object)obj.W);
        }
        public static OpenTK.Mathematics.Vector4h ToVecTKh<T>(this Vector4<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Vector4h((OpenTK.Mathematics.Half)(object)obj.X, (OpenTK.Mathematics.Half)(object)obj.Y, 
                (OpenTK.Mathematics.Half)(object)obj.Z, (OpenTK.Mathematics.Half)(object)obj.W);
        }
        public static OpenTK.Mathematics.Vector4i ToVecTKi<T>(this Vector4<T> obj) where T : unmanaged
        {
            return new OpenTK.Mathematics.Vector4i((int)(object)obj.X, (int)(object)obj.Y, (int)(object)obj.Z, (int)(object)obj.W);
        }

        public static Vector4<T> ToVecZ<T>(this OpenTK.Mathematics.Vector4 obj) where T : unmanaged
        {
            return new Vector4<T>((T)(object)obj.X, (T)(object)obj.Y, (T)(object)obj.Z, (T)(object)obj.W);
        }
        public static Vector4<T> ToVecZ<T>(this OpenTK.Mathematics.Vector4d obj) where T : unmanaged
        {
            return new Vector4<T>((T)(object)obj.X, (T)(object)obj.Y, (T)(object)obj.Z, (T)(object)obj.W);
        }
        public static Vector4<T> ToVecZ<T>(this OpenTK.Mathematics.Vector4h obj) where T : unmanaged
        {
            return new Vector4<T>((T)(object)obj.X, (T)(object)obj.Y, (T)(object)obj.Z, (T)(object)obj.W);
        }
        public static Vector4<T> ToVecZ<T>(this OpenTK.Mathematics.Vector4i obj) where T : unmanaged
        {
            return new Vector4<T>((T)(object)obj.X, (T)(object)obj.Y, (T)(object)obj.Z, (T)(object)obj.W);
        }
    }
}
