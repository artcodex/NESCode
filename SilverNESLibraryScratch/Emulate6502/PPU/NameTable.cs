using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.PPU
{
    //represents the name table as well as the attribute table attached to it.
    public class NameTable : Memory.AbstractMemoryMapper
    {
        public const uint NAME_TABLE_SIZE = 0x400;
        public const uint NAME_TABLE_ROWS = 30;
        public const uint NAME_TABLE_COLS = 32;
        public const uint ATTRIB_TILES_PER_BYTE = 16;

        public byte[] NameTableBytes;

        public NameTable()
        {
            InitializeNameTable();
        }

        private void InitializeNameTable()
        {
            NameTableBytes = new byte[NAME_TABLE_SIZE];
        }

        public byte GetTileIndexAt(byte row, byte col)
        {
            uint tableIndex = (row * NAME_TABLE_COLS) + col;

            if (tableIndex < (NAME_TABLE_ROWS * NAME_TABLE_COLS))
            {
                return NameTableBytes[tableIndex];
            }
            else
            {
                throw new IndexOutOfRangeException("Invalid nametable row/col value");
            }
        }

        public override string ToString()
        {
            return "PPU NameTable";
        }

        public void Reset()
        {
            NameTableBytes = new byte[NAME_TABLE_SIZE];
        }

        public byte GetTileColorMSB(byte row, byte col)
        {
            uint attribStartIndex = NAME_TABLE_COLS * NAME_TABLE_ROWS;
            uint scalarTile = (row * NAME_TABLE_COLS) + col;
            byte msbLocation = 0;
            byte tileColorMSB = 0;

            if (scalarTile < (NAME_TABLE_ROWS * NAME_TABLE_COLS))
            {
                byte attribColorByte = NameTableBytes[(scalarTile / ATTRIB_TILES_PER_BYTE) + attribStartIndex];

                //now that we have the correct by holding the msb bits we need
                //to narrow down the logical operations/bit shifts to get the 
                //correct 2-bit msb for the color. we look at the tiles location
                //within the group of 16, and then place it in one of the 
                //groups of 4.
                msbLocation = (byte)(((scalarTile % ATTRIB_TILES_PER_BYTE) / 4) * 2);
                tileColorMSB = (byte)((attribColorByte & (0x03 << msbLocation)) >> msbLocation);

                return tileColorMSB;
            }
            else
            {
                throw new IndexOutOfRangeException("Tile row/col out of range");
            }
        }

        #region MemoryMapper

        //Need to look to refactor this code copy in some or other way.

        public override byte Read(uint address)
        {
            byte retVal = NameTableBytes[address];

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

            NameTableBytes[address] = value;
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
                    values[start - j] = NameTableBytes[j];
                }
            }
            else
            {
                for (uint j = start; j < end; j++)
                {
                    values[j - start] = NameTableBytes[j];
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
                    NameTableBytes[j] = values[start - j];
                }
            }
            else
            {
                for (uint j = start; j < end; j++)
                {
                    NameTableBytes[j] = values[j - start];
                }
            }
        }
        #endregion
    }
}
