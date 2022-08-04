using System;

namespace Zene.Graphics
{
    public unsafe sealed class BufferProperties : IProperties
    {
        public BufferProperties(IBuffer source)
        {
            Source = source;
        }

        public IBuffer Source { get; }
        IGLObject IProperties.Source => Source;

        internal int _size = 0;
        public int Size => _size;

        internal bool _mapped = false;
        public bool Mapped => _mapped;
        internal AccessType _access = AccessType.ReadWrte;
        public AccessType MappedAccess => _access;
        internal MappedAccessFlags _accessFlags = 0;
        public MappedAccessFlags MappedAccessFlags => _accessFlags;
        internal int _mapLength = 0;
        public int MapLength => _mapLength;
        internal int _mapOffset = 0;
        public int MapOffset => _mapOffset;

        public bool Sync()
        {
            throw new NotImplementedException();
        }
    }
}
