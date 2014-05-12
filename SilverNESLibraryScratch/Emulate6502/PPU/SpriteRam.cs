using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.PPU
{
    public enum SpriteSizeOption
    {
        Sprite8x8 = 0,
        Sprite8x16 = 1
    }

    public class SpriteInfo
    {
        public byte SpriteY{ get; set; }
        public byte PatternTableIndex { get; set; }
        public byte SpriteX { get; set; }
        public byte ColorPaletteEntryMSB { get; set; }
        public bool BackgroundHasPriority { get; set; }
        public bool FlipHorizontal { get; set; }
        public bool FlipVertical { get; set; }
    }

    public class SpriteRam
    {
        public const uint SPRITE_RAM_SIZE = 256;
        public const uint MAX_SPRITES = 64;
        public const uint MAX_SPRITES_PER_SCANLINE = 8;

        private byte[] _spriteRam;
        private SpriteSizeOption _spriteSize = SpriteSizeOption.Sprite8x8;
        private byte _spRamAddress = 0;
        private SpriteInfo _current = null;

        public SpriteRam(SpriteSizeOption initialSize)
        {
            _spriteSize = initialSize;
            InitializeSpriteRam();
        }

        public void Reset()
        {
            _spriteRam = new byte[SPRITE_RAM_SIZE];
            _spRamAddress = 0;
        }

        private void InitializeSpriteRam()
        {
            _spriteRam = new byte[SPRITE_RAM_SIZE];
            _spRamAddress = 0;
            _current = new SpriteInfo();
        }

        public SpriteInfo GetSpriteInfo(byte spriteNo)
        {
            SpriteInfo info = _current;

            if (spriteNo < MAX_SPRITES)
            {
                uint startIndex = ((uint)spriteNo) * 4; //4 bytes per sprite info record

                info.SpriteY = (byte)(_spriteRam[startIndex++] + 1);
                info.PatternTableIndex = _spriteRam[startIndex++];
                info.ColorPaletteEntryMSB = (byte)(_spriteRam[startIndex] & 0x03);
                info.BackgroundHasPriority = (_spriteRam[startIndex] & 0x20) == 0x20;
                info.FlipHorizontal = (_spriteRam[startIndex] & 0x40) == 0x40;
                info.FlipVertical = (_spriteRam[startIndex++] & 0x80) == 0x80;
                info.SpriteX = _spriteRam[startIndex];

                return info;
            }
            else
            {
                throw new IndexOutOfRangeException("Sprite no. referenced is out of range");
            }
        }

        #region MemoryMapper Members

        public byte SpriteRamAddress
        {
            set
            {
                _spRamAddress = value;
            }
        }

        public SpriteSizeOption SpriteSize
        {
            get
            {
                return _spriteSize;
            }
            set
            {
                _spriteSize = value;
            }
        }

        public byte ReadData()
        {
            byte retVal = _spriteRam[_spRamAddress];

            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(_spRamAddress, this, CpuObjects.MemoryOperation.Read, retVal);
            }

            return retVal;
        }

        public void WriteData(byte data)
        {
            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(_spRamAddress, this, CpuObjects.MemoryOperation.Write, data);
            }

            _spriteRam[_spRamAddress] = data;
            _spRamAddress++;
        }

        public byte[] ReadDMA()
        {
            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(0, this, CpuObjects.MemoryOperation.DMA, 0);
            }

            byte[] newSpriteArray = new byte[SPRITE_RAM_SIZE];
            _spriteRam.CopyTo(newSpriteArray, 0);

            return newSpriteArray;
        }

        public void WriteDMA(byte[] values)
        {
            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(0, this, CpuObjects.MemoryOperation.DMA, 0);
            }

            values.CopyTo(_spriteRam, 0);
        }

        public override string ToString()
        {
            return "PPU Sprite RAM";
        }

        #endregion
    }
}
