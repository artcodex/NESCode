using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.Memory
{
    public abstract class AbstractMemoryMapper : MemoryMapper
    {
        #region MemoryMapper Members

        public byte this[uint index]
        {
            get
            {
                return Read(index);
            }
        }

        public abstract byte Read(uint address);

        public abstract void Write(uint address, byte value);

        public abstract void ReadBlock(uint startAddress, uint endAddress, byte[] values);

        public abstract void WriteBlock(uint startAddress, uint endAddress, byte[] values);

        //std behaviour is to return true
        public virtual bool CanAccess(uint address) 
        { 
            return address >= 0 && address <= ushort.MaxValue; 
        }

        #endregion
    }
}
