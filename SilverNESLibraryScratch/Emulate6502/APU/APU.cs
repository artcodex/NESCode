using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.APU
{
    //dummy APU implementation
    public class APU : Memory.AbstractMemoryMapper
    {
        public const uint APU_REG_START_RANGE = 0x4000;
        public const uint APU_REG_END_RANGE = 0x4013;
        public const uint DMC_ENABLE_REG = 0x4015;


        private Emulator.NesEmulator _parent;

        public APU(Emulator.NesEmulator parent)
        {
            _parent = parent;
            InitializeAPU();
        }

        private void InitializeAPU()
        {
            _parent.MainMemory.AttachMemoryMapping(this, APU_REG_START_RANGE, APU_REG_END_RANGE);
            _parent.MainMemory.AttachMemoryMapping(this, DMC_ENABLE_REG, DMC_ENABLE_REG);
        }

        public override byte Read(uint address)
        {
            //I do nothing currently
            return 0;
        }

        public override void Write(uint address, byte value)
        {
            //I do nothing currently
        }

        public override void ReadBlock(uint startAddress, uint endAddress, byte[] values)
        {
            throw new NotImplementedException();
        }

        public override void WriteBlock(uint startAddress, uint endAddress, byte[] values)
        {
            throw new NotImplementedException();
        }
    }
}
