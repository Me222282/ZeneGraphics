using System;

namespace Zene.Graphics
{
    public class DrawingException : Exception
    {
        public DrawingException(IDrawingContext context, string message)
            : base($"Drawing function failed and threw exception: {message}")
        {
            Context = context;
        }

        public IDrawingContext Context { get; }
    }
}
