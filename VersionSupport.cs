using System;
using System.Reflection;
using Zene.Graphics.Base;

namespace Zene.Graphics
{
    public static class VersionSupport
    {
        /// <summary>
        /// Gets the OpenGL version support of an object. Returns <see cref="double.NaN"/> if <paramref name="obj"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="obj">The object to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport(this PropertyInfo prop)
        {
            if (prop == null) { return double.NaN; }

            Attribute supportInfo = prop.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="type"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="type">The type to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport(this Type type)
        {
            if (type == null) { return double.NaN; }

            Attribute supportInfo = type.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Determines whether <paramref name="prop"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static bool IsSupported(this PropertyInfo prop)
        {
            return prop.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="type"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static bool IsSupported(this Type type)
        {
            return type.GetSupport() <= GL.Version;
        }

        //
        // Delegates
        //

        // Action get

        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport(this Action action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1>(this Action<T1> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2>(this Action<T1, T2> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3>(this Action<T1, T2, T3> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5,T6>(this Action<T1, T2, T3, T4, T5, T6> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7>(this Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }

        // Action check

        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported(this Action action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1>(this Action<T1> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2>(this Action<T1, T2> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3>(this Action<T1, T2, T3> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7>(this Action<T1, T2, T3, T4, T5, T6, T7> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action)
        {
            return action.GetSupport() <= GL.Version;
        }

        // Func get

        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<TResult>(this Func<TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, TResult>(this Func<T1, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, TResult>(this Func<T1, T2, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }
        /// <summary>
        /// Gets the OpenGL version support of a type. Returns <see cref="double.NaN"/> if <paramref name="action"/> doesn't have an OpenGLSupport attribute.
        /// </summary>
        /// <param name="action">The method to source verion info from.</param>
        /// <returns></returns>
        public static double GetSupport<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> action)
        {
            if (action == null) { return double.NaN; }

            Attribute supportInfo = action.Method.GetCustomAttribute(typeof(OpenGLSupportAttribute), true);

            if (supportInfo == null) { return double.NaN; }

            return (supportInfo as OpenGLSupportAttribute).Version;
        }

        // Func check

        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<TResult>(this Func<TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, TResult>(this Func<T1, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, TResult>(this Func<T1, T2, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
        /// <summary>
        /// Determines whether <paramref name="action"/> is supported in the current OpenGL context.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IsSupported<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> action)
        {
            return action.GetSupport() <= GL.Version;
        }
    }
}
