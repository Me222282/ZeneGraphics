using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zene.Graphics.Base;

namespace Zene.Graphics
{
    /// <summary>
    /// An object that manages errors.
    /// </summary>
    public static class Debugger
    {
        public enum Type
        {
            /// <summary>
            /// Errors are not managed.
            /// </summary>
            None = 0,
            /// <summary>
            /// Errors are added to alist which can be emptied to the console with <see cref="FlushErrors"/>.
            /// </summary>
            AddToList = 1,
            /// <summary>
            /// Errors are outputed to the console strait away.
            /// </summary>
            CallConsole = 3,
            /// <summary>
            /// Errors are thrown as exceptions.
            /// </summary>
            ThrowException = 4
        }

        private static readonly List<string> _errors = new List<string>();

        /// <summary>
        /// The type of which pushed errors should be managed.
        /// </summary>
        public static Type Manager { get; set; } = Type.CallConsole;

        private const string _unknownError = "An unknown error has occurred.";

        /// <summary>
        /// Manages exception <paramref name="e"/> based on <see cref="Manager"/>.
        /// </summary>
        /// <param name="e">The exception to manage.</param>
        public static Task PushError(Exception e)
        {
            if (Manager == Type.None) { return Task.CompletedTask; }

            if (Manager == Type.ThrowException)
            {
                throw e;
            }

            return Task.Run(() =>
            {
                switch (Manager)
                {
                    case Type.CallConsole:
                        Console.WriteLine(e.Message);
                        return;

                    case Type.AddToList:
                        _errors.Add(e.Message);
                        return;
                }
            });
        }
        /// <summary>
        /// Manages error <paramref name="message"/> based on <see cref="Manager"/>.
        /// </summary>
        /// <param name="message">The error message to manage.</param>
        public static Task PushError(string message)
        {
            if (Manager == Type.None) { return Task.CompletedTask; }

            if (Manager == Type.ThrowException)
            {
                throw new Exception(message);
            }
            
            return Task.Run(() =>
            {
                switch (Manager)
                {
                    case Type.CallConsole:
                        Console.WriteLine(message);
                        return;

                    case Type.AddToList:
                        _errors.Add(message);
                        return;
                }
            });
        }
        /// <summary>
        /// Manages an unknown based on <see cref="Manager"/>.
        /// </summary>
        public static Task PushError()
        {
            if (Manager == Type.None) { return Task.CompletedTask; }

            if (Manager == Type.ThrowException)
            {
                throw new Exception(_unknownError);
            }

            return Task.Run(() =>
            {
                switch (Manager)
                {
                    case Type.CallConsole:
                        Console.WriteLine(_unknownError);
                        return;

                    case Type.AddToList:
                        _errors.Add(_unknownError);
                        return;
                }
            });
        }

        private static readonly Random _random = new Random();

        /// <summary>
        /// Manages the last unmanged OpenGL error base on <see cref="Manager"/>.
        /// </summary>
        public static void PushGLError()
        {
            if (Manager == Type.None) { return; }

            uint error = GL.GetError();

            if (Manager == Type.ThrowException)
            {
                throw new Exception($"{GetGLError(error)}");
            }

            string message = $"{GetGLError(error)} thrown at {Environment.StackTrace}.";

            if (GL.Version >= 4.3)
            {
                GL.DebugMessageInsert(GLEnum.DebugSourceApplication, GLEnum.DebugTypeError, (uint)_random.Next(), GLEnum.DebugSeverityNotification, message.Length, message);
            }
            else
            {
                switch (Manager)
                {
                    case Type.CallConsole:
                        Console.WriteLine(message);
                        return;

                    case Type.AddToList:
                        _errors.Add(message);
                        return;
                }
            }
        }

        /// <summary>
        /// Outputs all recorded errors to console.
        /// </summary>
        /// <remarks>
        /// For this method to perform anything, errors must have been recorded when <see cref="Manager"/> is <see cref="Type.AddToList"/>.
        /// </remarks>
        public static Task FlushErrors()
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < _errors.Count; i++)
                {
                    Console.WriteLine(_errors[i]);
                }

                _errors.Clear();
            });
        }

        /// <summary>
        /// Determines the string equivalent of OpenGL error <paramref name="error"/>.
        /// </summary>
        /// <param name="error">The error to retrieve the string of.</param>
        public static string GetGLError(uint error)
        {
            return error switch
            {
                GLEnum.NoError => "GL_NO_ERROR",
                GLEnum.InvalidEnum => "GL_INVALID_ENUM",
                GLEnum.InvalidOperation => "GL_INVALID_OPERATION",
                GLEnum.StackOverflow => "GL_STACK_OVERFLOW",
                GLEnum.StackUnderflow => "GL_STACK_UNDERFLOW",
                GLEnum.OutOfMemory => "GL_OUT_OF_MEMORY",
                GLEnum.InvalidFramebufferOperation => "GL_INVALID_FRAMEBUFFER_OPERATION",
                GLEnum.ContextLost => "GL_CONTEXT_LOST",
                GLEnum.InvalidValue => "GL_INVALID_VALUE",
                GLEnum.InvalidIndex => "GL_INVALID_INDEX",
                _ => "GL_UNKNOWN"
            };
        }
    }
}
