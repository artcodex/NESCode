using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.PPU
{
    public enum NESPalette
    {
        Background = 0,
        Sprite
    }

    public class NESPixel
    {
        public byte R {get; set;}
        public byte G {get; set;}
        public byte B {get; set;}
        public bool WrittenBySprite { get; set; }
        public bool WrittenByBG { get; set; }

        public NESPixel(byte red, byte green, byte blue)
        {
            Init(red, green, blue);
        }

        private void Init(byte red, byte green, byte blue)
        {
            R = red;
            G = green;
            B = blue;

            WrittenByBG = false;
            WrittenBySprite = false;
        }

        public void Clear()
        {
            Init(0, 0, 0);
        }

        public void Clear(byte red, byte green, byte blue)
        {
            Init(red, green, blue);
        }

        public void Clear(NESPixel pixel)
        {
            Init(pixel.R, pixel.G, pixel.B);
        }

        public void Set(NESPixel pixel)
        {
            R = pixel.R;
            G = pixel.G;
            B = pixel.B;
        }
    }

    public class Palette : Memory.AbstractMemoryMapper
    {
        public const uint NES_PALETTE_SIZE = 0x10;
        public const uint NES_PALETTE_COLOR_COUNT = 64;

        private const uint PALETTE_MAP_START = 0x3F00;
        private const uint PALETTE_MAP_END = 0x4000;

        private byte[] _bg_pal;
        private byte[] _sp_pal;
        private PPU _parentPPU;

        //the full NES RGB color palette
        private static NESPixel[] _NESColorPalette = new NESPixel[]
        {
            new NESPixel(0x75, 0x75, 0x75), new NESPixel(0x27, 0x1B, 0x8F), new NESPixel(0x00, 0x00, 0xAB),
            new NESPixel(0x47, 0x00, 0x9F), new NESPixel(0x8F, 0x00, 0x77), new NESPixel(0xAB, 0x00, 0x13),
            new NESPixel(0xA7, 0x00, 0x00), new NESPixel(0x7F, 0x0B, 0x00), new NESPixel(0x43, 0x2F, 0x00),
            new NESPixel(0x00, 0x47, 0x00), new NESPixel(0x00, 0x51, 0x00), new NESPixel(0x00, 0x3F, 0x17),
            new NESPixel(0x1B, 0x3F, 0x5F), new NESPixel(0x00, 0x00, 0x00), new NESPixel(0x00, 0x00, 0x00),
            new NESPixel(0x00, 0x00, 0x00), new NESPixel(0xBC, 0xBC, 0xBC), new NESPixel(0x00, 0x73, 0xEF),
            new NESPixel(0x23, 0x3B, 0xEF), new NESPixel(0x83, 0x00, 0xF3), new NESPixel(0xBF, 0x00, 0xBF),
            new NESPixel(0xE7, 0x00, 0x5B), new NESPixel(0xDB, 0x2B, 0x00), new NESPixel(0xCB, 0x4F, 0x0F),
            new NESPixel(0x8B, 0x73, 0x00), new NESPixel(0x00, 0x97, 0x00), new NESPixel(0x00, 0xAB, 0x00),
            new NESPixel(0x00, 0x93, 0x3B), new NESPixel(0x00, 0x83, 0x8B), new NESPixel(0x00, 0x00, 0x00),
            new NESPixel(0x00, 0x00, 0x00), new NESPixel(0x00, 0x00, 0x00), new NESPixel(0xFF, 0xFF, 0xFF),
            new NESPixel(0x3F, 0xBF, 0xFF), new NESPixel(0x5F, 0x97, 0xFF), new NESPixel(0xA7, 0x8B, 0xFD),
            new NESPixel(0xF7, 0x7B, 0xFF), new NESPixel(0xFF, 0x77, 0xB7), new NESPixel(0xFF, 0x77, 0x63),
            new NESPixel(0xFF, 0x9B, 0x3B), new NESPixel(0xF3, 0xBF, 0x3F), new NESPixel(0x83, 0xD3, 0x13),
            new NESPixel(0x4F, 0xDF, 0x4B), new NESPixel(0x58, 0xF8, 0x98), new NESPixel(0x00, 0xEB, 0xDB),
            new NESPixel(0x00, 0x00, 0x00), new NESPixel(0x00, 0x00, 0x00), new NESPixel(0x00, 0x00, 0x00),
            new NESPixel(0xFF, 0xFF, 0xFF), new NESPixel(0xAB, 0xE7, 0xFF), new NESPixel(0xC7, 0xD7, 0xFF),
            new NESPixel(0xD7, 0xCB, 0xFF), new NESPixel(0xFF, 0xC7, 0xFF), new NESPixel(0xFF, 0xC7, 0xDB),
            new NESPixel(0xFF, 0xBF, 0xB3), new NESPixel(0xFF, 0xDB, 0xAB), new NESPixel(0xFF, 0xE7, 0xA3),
            new NESPixel(0xE3, 0xFF, 0xA3), new NESPixel(0xAB, 0xF3, 0xBF), new NESPixel(0xB3, 0xFF, 0xCF),
            new NESPixel(0x9F, 0xFF, 0xF3), new NESPixel(0x00, 0x00, 0x00), new NESPixel(0x00, 0x00, 0x00),
            new NESPixel(0x00, 0x00, 0x00)
        };

        public Palette(PPU ppu)
        {
            _parentPPU = ppu;
            InitializePalette();
        }

        public void Reset()
        {
            _bg_pal = new byte[NES_PALETTE_SIZE];
            _sp_pal = new byte[NES_PALETTE_SIZE];
        }

        private void InitializePalette()
        {
            _bg_pal = new byte[NES_PALETTE_SIZE];
            _sp_pal = new byte[NES_PALETTE_SIZE];

            _parentPPU.PPUMemory.AttachMemoryMapping(this, PALETTE_MAP_START, PALETTE_MAP_END);
        }

        public override string ToString()
        {
            return "PPU Palette";
        }

        #region Palette Helpers
        public byte GetPaletteEntry(NESPalette palette, byte paletteAddr)
        {
            byte[] paletteEntries = palette == NESPalette.Background ? _bg_pal : _sp_pal;

            //we only care about the least significant 4 bits (i.e 16 entries)
            paletteAddr &= 0x0F;

            //cater for mirroring
            //are we a multiple of 4, if not we get actual value
            //else we mirror entry 0
            //For monocrhome the palette rgb values for 00, 10, 20, 30 all represent 
            //grayscales, and so the actual requested value is clamped to one of these
            if ((paletteAddr & 0x03) > 0)
            {
                return (byte)(_parentPPU.IsColorDisplay ? paletteEntries[paletteAddr] : paletteEntries[paletteAddr] & 0xF0);
            }
            else
            {
                //return the bg color taking monochrome settings into account
                return (byte)(_parentPPU.IsColorDisplay ? paletteEntries[0] : paletteEntries[0] & 0xF0);
            }
        }

        public NESPixel BackgroundColor
        {
            get
            {
                return _NESColorPalette[_bg_pal[0]];
            }
        }

        public NESPixel GetPaletteColor(byte paletteEntry)
        {
            if (paletteEntry < NES_PALETTE_COLOR_COUNT)
            {
                return _NESColorPalette[paletteEntry];
            }
            else
            {
                throw new IndexOutOfRangeException("Palette entry out of range");
            }
        }
#endregion

        #region MemoryMapper Members

        public override byte Read(uint address)
        {
            //account for mirroring
            address &= 0x1F;
            byte retVal = 0;

            if ((address & 0x10) == 0x00)
            {
                retVal = _bg_pal[address & 0x0F];
            }
            else
            {
                retVal = _sp_pal[address & 0x0F];
            }

            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(address, this, CpuObjects.MemoryOperation.Read, retVal);
            }

            return retVal;
        }

        public override void Write(uint address, byte value)
        {
            //account for mirroring
            address &= 0x1F;

            //make sure we only use 6 bits in data
            value &= 0x3F;

            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(address, this, CpuObjects.MemoryOperation.Write, value);
            }

            //is it the 0 pal entry
            if ((address & 0x0F) == 0x00)
            {
                _bg_pal[0] = value;
                _sp_pal[0] = value;
            }
            else if ((address & 0x10) == 0x00)
            {
                _bg_pal[address & 0x0F] = value;
            }
            else
            {
                _sp_pal[address & 0x0F] = value;
            }
        }

        public override void ReadBlock(uint startAddress, uint endAddress, byte[] values)
        {
            throw new NotImplementedException("Can't block read from palette memory");
        }

        public override void WriteBlock(uint startAddress, uint endAddress, byte[] values)
        {
            throw new NotImplementedException("Can't block write from palette memory");
        }

        #endregion
    }
}
