using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.Mappers
{
    public abstract class AbstractMapper : Mapper
    {
        public const uint PRGRAM_SIZE = 0x8000;
        public const uint PRGRAM_START = 0x8000;

        //8k vram backing store
        protected byte[] _vram;

        //32k code backing store
        protected byte[] _code;

        /*//code page pointers into cartridge
        /* 4 code pointers (8k granularity)
         *  8000h-9FFFh
         *  A000h-BFFFh
         *  C000h-DFFFh
         *  E000h-FFFFh
         * */
        //protected uint[] _codePointers;

        //video ram pointers
        //if vrom is present they point inwards to this
        //else they point toward the static vram above.
        /* 8 vram pointers (1k granularity)
         * 0000h-03FFh
         * 0400h-07FFh
         * 0800h-0BFFh
         * 0C00h-0FFFh
         * 1000h-13FFh
         * 1400h-17FFh
         * 1800h-1BFFh
         * 1C00h-1FFFh
         * */
        //protected uint[] _vramPointers;*/

        protected Emulator.NesEmulator _parent;

        public AbstractMapper(Emulator.NesEmulator parent)
        {
            _parent = parent;
            InitializeMapper();
            Reset();
        }

        protected virtual void InitializeMapper()
        {
            _vram = new byte[PPU.PatternTable.PATTERN_TABLE_SIZE * 2];
            _code = new byte[PRGRAM_SIZE];
            _parent.MainMemory.RemoveMemoryMapping(PRGRAM_START, PRGRAM_START + PRGRAM_SIZE);
            _parent.MainMemory.AttachMemoryMapping(this, PRGRAM_START, PRGRAM_START + PRGRAM_SIZE);
        }

        public override string ToString()
        {
            return "NES Mapper";
        }

        #region MemoryMapper Members



        public byte Read(uint address)
        {
            byte retVal = 0;

            if (address >= PRGRAM_START)
            {
                //we are looking at PRG request
                //we need to check bounds because PRG
                //ram is directly accessed and isn't managed
                //access
                if (address < (PRGRAM_START + PRGRAM_SIZE))
                {
                    //return _code[/*_codePointers[address & 0x6000]*/ + (address & 0xFFFF)];

                    retVal = _code[address - PRGRAM_START];
                }
                else
                {
                    //maybe throw exception in this case
                    retVal = 0;
                }
            }
            else
            {
                //video ram access
                //don't need to check address, this is fully filtered through PPU
                //return _vram[_vramPointers[address & 0x1C00] + (address & 0x03FF)];
                retVal =  _vram[address];
            }


            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(address, this, CpuObjects.MemoryOperation.Read, retVal);
            }

            return retVal;
        }

        public void Write(uint address, byte value)
        {
            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(address, this, CpuObjects.MemoryOperation.Write, value);
            }

            if (address >= PRGRAM_START)
            {
                //only care if address is in range
                if (address < (PRGRAM_START + PRGRAM_SIZE))
                {
                    //delegate to the mapper
                    WriteCodeMemory(address, value);
                }
            }
            else
            {
                //all checking for this should already be done
                _vram[address] = value;
            }
        }

        protected virtual void WriteCodeMemory(uint address, byte value) { }

        public abstract void ReadBlock(uint startAddress, uint endAddress, byte[] values);

        public abstract void WriteBlock(uint startAddress, uint endAddress, byte[] values);

        public abstract void Reset();


        #endregion

        #region Mapper Members


        #endregion

        #region MemoryMapper Members

        public byte this[uint index]
        {
            get
            {
                return Read(index);
            }
        }

        public virtual bool CanAccess(uint address)
        {
            return (address >= 0 && address < (PPU.PatternTable.PATTERN_TABLE_SIZE * 2)) ||
                (address >= PRGRAM_START && address < (PRGRAM_START + PRGRAM_SIZE));
        }

        #endregion
    }
}
