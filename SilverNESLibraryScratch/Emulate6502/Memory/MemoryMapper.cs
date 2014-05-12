using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.Memory
{
    public interface MemoryMapper
    {
        byte this[uint index] { get; }
        byte Read(uint address);
        void Write(uint address, byte value);
        void ReadBlock(uint startAddress, uint endAddress, byte[] values);
        void WriteBlock(uint startAddress, uint endAddress, byte[] values);
        bool CanAccess(uint address);
    }
}
