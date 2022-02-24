using System;

namespace Zene.Graphics
{
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Method | 
        AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class OpenGLSupportAttribute : Attribute
    {
        public OpenGLSupportAttribute(double version)
        {
            Version = version;
        }

        public double Version { get; }
    }
}
