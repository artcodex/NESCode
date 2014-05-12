using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.Mappers
{
    public class NRomMapper : AbstractMapper
    {
        public NRomMapper(Emulator.NesEmulator parent) : base(parent)
        {
        }

        public override string ToString()
        {
            return "NROM NES Mapper";
        }

        public override void Reset()
        {
            if (_parent.CurrentCartridge != null)
            {
                if (_parent.CurrentCartridge.VideoRomBankCount > 0)
                {
                    //copy the vrom bank from cartridge to local store
                    Array.Copy(_parent.CurrentCartridge.VideoRomBanks[0], _vram, PPU.PatternTable.PATTERN_TABLE_SIZE * 2);
                }

                if (_parent.CurrentCartridge.ProgramRomBankCount > 0)
                {
                    int bank0 = 0, bank1 = 0;
                    //if we have 1 PRG bank we copy to high/
                    if (_parent.CurrentCartridge.ProgramRomBankCount == 1)
                    {
                        bank0 = 0;
                        bank1 = 0;
                    }
                    else
                    {
                        bank0 = 0;
                        bank1 = 1;
                    }

                    Array.Copy(_parent.CurrentCartridge.ProgramRomBanks[bank0], _code, Cartridge.Cartridge.PROG_ROM_SIZE);
                    Array.Copy(_parent.CurrentCartridge.ProgramRomBanks[bank1], 0, _code, Cartridge.Cartridge.PROG_ROM_SIZE, Cartridge.Cartridge.PROG_ROM_SIZE);
                }
            }
        }

        public override void ReadBlock(uint startAddress, uint endAddress, byte[] values)
        {
            uint start = startAddress, end = endAddress;
            byte[] memory = startAddress < 0x8000 ? _vram : _code;

            if (values.Length < Math.Abs(endAddress - startAddress))
            {
                throw new InvalidOperationException("Array to write is not big enough");
            }

            if (start > end)
            {
                for (uint j = start; j >= end; j++)
                {
                    values[start - j] = memory[j-start];
                }
            }
            else
            {
                for (uint j = start; j < end; j++)
                {
                    values[j - start] = memory[j-start];
                }
            }
        }

        public override void WriteBlock(uint startAddress, uint endAddress, byte[] values)
        {
            throw new NotImplementedException();
        }
    }
}
