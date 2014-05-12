using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.PPU
{
    public class NameTables : Memory.AbstractMemoryMapper
    {
        public const uint NAME_TABLE_COUNT = 4;

        //These represent the actual NES nametables (max 4 addressable)
        //as well as the pointers used for mirroring.
        //NOTE: Can these nametables ever change during execution, if not we can simply
        //re-init the vram pointers at startup to minimize memory usage
        private NameTable[] _nesNameTables;
        public  NameTable[] VRAM;



        //pointer to the parent PPU unit
        private PPU _parentPPU;

        public NameTables(PPU ppu)
        {
            _parentPPU = ppu;
            InitializeNameTables();
        }

        public void Reset()
        {
            foreach (var nametable in _nesNameTables)
            {
                nametable.Reset();
            }
        }

        private void InitializeNameTables()
        {
            _nesNameTables = new NameTable[NAME_TABLE_COUNT];
            VRAM = new NameTable[NAME_TABLE_COUNT];

            for (int i = 0; i < NAME_TABLE_COUNT; i++)
            {
                _nesNameTables[i] = new NameTable();
            }

            //set the initial mirroring, all nametables point to 0
            SetMirroring(0, 0, 0, 0);

            //lock the address range into the mapper
            _parentPPU.PPUMemory.AttachMemoryMapping(this, 0x2000, 0x3F00);
        }

        public byte GetTileIndexAt(byte nameTable, byte row, byte col)
        {
            if (nameTable < NAME_TABLE_COUNT)
            {
                return VRAM[nameTable].GetTileIndexAt(row, col);
            }
            else
            {
                throw new IndexOutOfRangeException("Name table referenced is out of range");
            }
        }

        public byte GetTileColorMSB(byte nameTable, byte row, byte col)
        {
            if (nameTable < NAME_TABLE_COUNT)
            {
                return VRAM[nameTable].GetTileColorMSB(row, col);
            }
            else
            {
                throw new IndexOutOfRangeException("Name table referenced is out of range");
            }
        }

        public void SetMirroring(uint nID_0, uint nID_1, uint nID_2, uint nID_3)
        {
            VRAM[0] = _nesNameTables[nID_0];
            VRAM[1] = _nesNameTables[nID_1];
            VRAM[2] = _nesNameTables[nID_2];
            VRAM[3] = _nesNameTables[nID_3];
        }

        public override string ToString()
        {
            return "PPU NameTables Parent";
        }

        #region MemoryMapper Members

        public override byte Read(uint address)
        {
            uint nameTable = 0;

            //account for mirroring of the name tables
            address &= 0x2FFF;
            nameTable = (address & 0x0C00) >> 10;

            //push to range 0-0x400
            address &= 0x3FF;

            //read from nametable prescribed
            byte retVal = VRAM[nameTable].NameTableBytes[address];

            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(address, this, CpuObjects.MemoryOperation.Read, retVal);
            }

            return retVal;
        }

        public override void Write(uint address, byte value)
        {
            uint nameTable = 0;

            //account for mirroring of the name tables
            address &= 0x2FFF;
            nameTable = (address & 0x0C00) >> 10;

            //push to range 0-0x400
            address &= 0x3FF;

            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(address, this, CpuObjects.MemoryOperation.Write, value);
            }

            //read from nametable prescribed
            VRAM[nameTable].NameTableBytes[address] = value;
        }

        public override void ReadBlock(uint startAddress, uint endAddress, byte[] values)
        {
            throw new NotImplementedException("Not allowed for NameTables");
        }

        public override void WriteBlock(uint startAddress, uint endAddress, byte[] values)
        {
            throw new NotImplementedException("Not allowed for NameTables");
        }

        #endregion
    }
}
