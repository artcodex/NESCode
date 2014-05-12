using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.PPU
{
    public enum PatternTableSelection
    {
        Table0 = 0x0000,
        Table1 = 0x1000
    }
    public class PatternTable : Memory.AbstractMemoryMapper
    {
        public const uint PATTERN_TABLE_SIZE = 0x1000;
        public const uint TILE_HEIGHT = 8;
        public const uint TILE_WIDTH = 8;
        public const uint MAX_TILE_ENTRIES = PATTERN_TABLE_SIZE / (2 * TILE_HEIGHT);
        public const int TILE_BYTE_LENGTH = 16;
        
        private PatternTableSelection _startAddress;
        private byte[] _patternTable;
        private PPU _parentPPU;

        public void Reset()
        {
            _patternTable = new byte[PATTERN_TABLE_SIZE];
        }

        public PatternTable(PPU ppu, PatternTableSelection initialSelection)
        {
            _startAddress = initialSelection;
            _parentPPU = ppu;
            InitializePatternTable();
        }

        private void InitializePatternTable()
        {
            _patternTable = new byte[PATTERN_TABLE_SIZE];
            SetPPUMapping();
        }

        public override string ToString()
        {
            return string.Format("PPU Pattern Table [0x{0}]", ((uint)_startAddress).ToString("X4"));
        }

        public PatternTableSelection CurrentStartAddress
        {
            get
            {
                return _startAddress;
            }
        }

        /// <summary>
        /// Returns a single tile from this pattern table
        /// It doesn't know anything further about the possibility of 16x8 sprite tiles
        /// It only considers tiles to be 8x8, 16x8 tiles are just two 8x8 tiles each
        /// a row underneath eachother.
        /// </summary>
        /// <param name="index">The scalar index of the tile</param>
        /// <returns></returns>
        public byte[] GetTileAt(byte index)
        {
            Mappers.Mapper cartMapper = _parentPPU.Emulator.CurrentCartridge.MapperObject;

            if (index < MAX_TILE_ENTRIES)
            {
                //we include the pixel colors per tile each as a single byte
                byte[] pixelColors = new byte[TILE_BYTE_LENGTH * 4];
                int startByte = (index * TILE_BYTE_LENGTH);
                int i = 0;
                int j = 0;
                int k = 0;

                while (i < pixelColors.Length)
                {
                    /*
                     * tile layout is similar to below:
                     * 
                     * 00000000 00010010
                     * 00100000 01010010
                     * 00101010 01010010
                     * .... for 8 rows
                     * 
                     * for pixel 0 we would take bit 1 from byte 0 and add it to bit 1 from byte 8
                     * such that we get color = 000000(1-from byte 8)(1-from byte 0)
                     * */

                    j = (int)(startByte + (i / TILE_HEIGHT));
                    k = (int)(((int)TILE_HEIGHT - 1) - (int)(i % TILE_HEIGHT));
                    pixelColors[i] = (byte)((cartMapper.Read((uint)_startAddress + (uint)j) & (1 << k)) >> k);

                    //we need to handle the case of k=0 in a more special way
                    //i.e we can't shift down by -1 this doesn't make sense, so
                    //we need to shift up by 1.
                    if (k == 0)
                    {
                        pixelColors[i] |= (byte)((cartMapper.Read((uint)_startAddress + (uint)(j + TILE_HEIGHT)) & (1 << k)) << 1);
                    }
                    else
                    {
                        pixelColors[i] |= (byte)((cartMapper.Read((uint)_startAddress + (uint)(j + TILE_HEIGHT)) & (1 << k)) >> (k - 1));
                    }

                    i++;
                }

                return pixelColors;
            }
            else
            {
                throw new IndexOutOfRangeException("Tile index out of range of pattern table");
            }
        }

        private void SetPPUMapping()
        {
            uint start = (uint) _startAddress;

            _parentPPU.PPUMemory.RemoveMemoryMapping(this);
            _parentPPU.PPUMemory.AttachMemoryMapping(this, start, (start + PATTERN_TABLE_SIZE - 1));
        }

        public void SetPatternTable(byte value)
        {

            _startAddress = (value == 0x01) ? PatternTableSelection.Table1 : PatternTableSelection.Table0;
            SetPPUMapping();
        }

        #region MemoryMapper Members

        public override byte Read(uint address)
        {
            Mappers.Mapper cartMapper = _parentPPU.Emulator.CurrentCartridge.MapperObject;

            if (address >= (uint) _startAddress &&
                address < ((uint)_startAddress + PATTERN_TABLE_SIZE))
            {
                return cartMapper.Read(address);
            }

            throw new IndexOutOfRangeException("Address out of range for pattern table");
        }

        public override void Write(uint address, byte value)
        {
            Mappers.Mapper cartMapper = _parentPPU.Emulator.CurrentCartridge.MapperObject;

            if (address >= (uint)_startAddress &&
                address < ((uint)_startAddress + PATTERN_TABLE_SIZE))
            {
                cartMapper.Write(address, value);
            }
            else
            {
                throw new IndexOutOfRangeException("Address out of range for pattern table");
            }
        }

        public override void ReadBlock(uint startAddress, uint endAddress, byte[] values)
        {
            throw new NotImplementedException("Currently not allowed block read from pattern table");
        }

        public override void WriteBlock(uint startAddress, uint endAddress, byte[] values)
        {
            throw new NotImplementedException("Currently not allowed block read from pattern table");
        }

        #endregion
    }
}
