using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.Memory
{
    //Vert basic memory prototype based on a byte
    //array. We do basically no type or bounds checking
    //in this prototype

    public class Memory : AbstractMemoryMapper
    {
        public const uint MAP_SPACE = 0x10001;
        private MemoryMapper[] _addressMappings;
        private EmptyMapper _empty;

        public Memory()
        {
            InitiliazeMemory();
        }

        private void InitiliazeMemory()
        {
            _addressMappings = new MemoryMapper[MAP_SPACE];
            _empty = new EmptyMapper();

            for (uint s = 0; s < MAP_SPACE; s++)
            {
                _addressMappings[s] = _empty;
            }
        }

        #region Public Mapper Functions
        //Must fix the dual range mapping logic, to restrict mappers
        //not to overlap ranges for a single memory helper
        public void AttachMemoryMapping(MemoryMapper mapper, uint start, uint end)
        {
            for (uint s = start; s <= end; s++)
            {
                _addressMappings[s] = mapper;
            }
        }

        public void RemoveMemoryMapping(MemoryMapper mapper)
        {
            for (uint s = 0; s < MAP_SPACE; s++)
            {
                if (_addressMappings[s] == mapper)
                {
                    _addressMappings[s] = _empty;
                }
            }
        }

        //method for some other customer to remove a memory space mapping
        public void RemoveMemoryMapping(uint start, uint end)
        {
            for (uint s = start; s <= end; s++)
            {
                _addressMappings[s] = _empty;
            }
        }

        public void RemoveMemoryMapping(MemoryMapper mapper, uint start, uint end)
        {
            for (uint s = start; s <= end; s++)
            {
                if (_addressMappings[s] == mapper)
                {
                    _addressMappings[s] = _empty;
                }
            }
        }
        #endregion

        public override byte Read(uint address)
        {
            return _addressMappings[address].Read(address);
        }

        public override void Write(uint address, byte value)
        {
            _addressMappings[address].Write(address, value);
        }

        public override void ReadBlock(uint startAddress, uint endAddress, byte[] values)
        {
            _addressMappings[startAddress].ReadBlock(startAddress, endAddress, values);
        }

        public override void WriteBlock(uint startAddress, uint endAddress, byte[] values)
        {
            _addressMappings[startAddress].WriteBlock(startAddress, endAddress, values);
        }
    }
}
