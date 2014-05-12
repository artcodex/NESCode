using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.Memory
{
    public class GeneralMemoryMapper : AbstractMemoryMapper
    {
        private byte[] _internalBytes;

        public GeneralMemoryMapper(byte[] memory)
        {
            _internalBytes = memory;
        }

        public GeneralMemoryMapper()
        {
        }

        public void SetArray(byte[] memory)
        {
            //useful for memory usage limiting
            _internalBytes = memory;
        }

        public override byte Read(uint address)
        {
            if (address >= 0 && address < _internalBytes.Length)
            {
                return _internalBytes[address];
            }
            else
            {
                //maybe we want to throw error here ?
                return 0;
            }
        }

        public override void Write(uint address, byte value)
        {
            if (address >= 0 && address < _internalBytes.Length)
            {
                _internalBytes[address] = value;
            }
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
                    values[start - j] = _internalBytes[j];
                }
            }
            else
            {
                for (uint j = start; j < end; j++)
                {
                    values[j - start] = _internalBytes[j];
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
                    _internalBytes[j] = values[start - j];
                }
            }
            else
            {
                for (uint j = start; j < end; j++)
                {
                    _internalBytes[j] = values[j - start];
                }
            }
        }

        public override bool CanAccess(uint address)
        {
            return address >= 0 && address < _internalBytes.Length;
        }
    }
}
