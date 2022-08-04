using Zene.Graphics.Base;

namespace Zene.Graphics
{
    public enum AttributeSize
    {
        D1 = 1,
        D2 = 2,
        D3 = 3,
        D4 = 4,
        BGRA = (int)GLEnum.Bgra
    }

    public enum DataType : uint
    {
        Byte = 0x1400,
        UByte = 0x1401,
        Short = 0x1402,
        UShort = 0x1403,
        Int = 0x1404,
        UInt = 0x1405,
        HalfFloat = 0x140b,
        Float = 0x1406,
        Double = 0x140a,
        Fixed = 0x140c,
        IntRev = 0x8d9f,
        UIntRev = 0x8368,
        UIntFRev = 0x8c3b
    }

    public interface IVertexArray : IGLObject
    {
        public new VertexArrayProperties Properties { get; }
        IProperties IGLObject.Properties => Properties;
    }
}
