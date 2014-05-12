using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Emulate6502.Memory
{
    internal class RamMemory : AbstractMemoryMapper
    {
        public const uint MEMORY_SIZE = 0x0800;
        private byte[] _memory;

        public RamMemory(Memory mapper)
        {
            _memory = new byte[MEMORY_SIZE];

            //attach the ram memory to the main mapper.
            mapper.AttachMemoryMapping(this, 0x0000, 0x1FFF);
        }

        #region MemoryMapper Members

        private uint TranslateAddress(uint address)
        {
            //cater for mirrors (0x0800 is mirror of 0x0000);
            while (address >= 0x800)
            {
                address &= 0x7FF;
            }

#if TRACE_ENABLED
            if (address > 0x100 && address < 0x200)
            {
                Trace.TraceWarning("Directly writing stack memory");
            }
#endif

            return address;
        }

        public override byte Read(uint address)
        {
            byte retVal = _memory[TranslateAddress(address)];

            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(address, this, CpuObjects.MemoryOperation.Read, retVal);
            }

            return retVal;
        }

        public override void Write(uint address, byte value)
        {
            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(address, this, CpuObjects.MemoryOperation.Write, value);
            }

            _memory[TranslateAddress(address)] = value;
        }

        public override string ToString()
        {
            return "RAM Memory";
        }

        public override void ReadBlock(uint startAddress, uint endAddress, byte[] values)
        {
            uint start = startAddress, end = endAddress;

            if (values.Length < Math.Abs(endAddress - startAddress))
            {
                throw new InvalidOperationException("Array to write is not big enough");
            }

            if (start > end)
            {
                for (uint j = start; j >= end; j++)
                {
                    values[start - j] = _memory[TranslateAddress(j)];
                }
            }
            else
            {
                for (uint j = start; j < end; j++)
                {
                    values[j - start] = _memory[TranslateAddress(j)];
                }
            }
        }

        public override void WriteBlock(uint startAddress, uint endAddress, byte[] values)
        {
            uint start = startAddress, end = endAddress;

            if (values.Length < Math.Abs(endAddress - startAddress))
            {
                throw new InvalidOperationException("Array to write is not big enough");
            }

            if (start > end)
            {
                for (uint j = start; j >= end; j++)
                {
                    _memory[TranslateAddress(j)] = values[start - j];
                }
            }
            else
            {
                for (uint j = start; j < end; j++)
                {
                    _memory[TranslateAddress(j)] = values[j - start];
                }
            }
        }
        #endregion
    }
}
