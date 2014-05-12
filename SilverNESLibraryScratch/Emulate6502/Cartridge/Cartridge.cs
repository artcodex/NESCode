using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Emulate6502.Helpers;

namespace Emulate6502.Cartridge
{
    public enum NesMapper
    {
        NROM = 0,
        MMC1 = 1,
        UNROM = 2,
        CNROM = 3,
        MMC3 = 4
    }

    public enum MirrorType
    {
        Horizontal = 0,
        Vertical,
        FourScreen
    }

    public class Cartridge
    {
        public const uint TRAINER_SIZE = 512;
        public const uint PROG_ROM_SIZE = 16 * 1024;
        public const uint VID_ROM_SIZE = 8 * 1024;

        //parent emulator
        private Emulator.NesEmulator _parent;

        private BitStream _input = null;

        private Cartridge(Stream input, Emulator.NesEmulator parent)
        {
            _parent = parent;
            _input = new BitStream(input);
            Initialise();
        }

        public static Cartridge Load(Stream input, Emulator.NesEmulator parent)
        {
            Cartridge cart = new Cartridge(input, parent);
            return cart;
        }

        public static Cartridge Load(Stream input)
        {
            return Load(input, null);
        }


        public string Signature { get; set; }
        public byte ByteSignature { get; set; }
        public byte ProgramRomBankCount { get; set; }
        public byte VideoRomBankCount { get; set; }
        public byte RamBankCount { get; set; }
        public byte RomControlByte1 { get; set; }
        public byte RomControlByte2 { get; set; }
        public NesMapper Mapper { get; set; }
        public MirrorType Mirroring { get; set; }
        public bool BatteryBackedRamPresent { get; set; }
        public bool TrainerPresent { get; set; }
        public List<byte[]> ProgramRomBanks { get; set; }
        public List<byte[]> VideoRomBanks { get; set; }
        public byte[] Trainer { get; set; }
        public Mappers.Mapper MapperObject { get; set; }

        public void LoadMapper()
        {
            //if we load the cartridge without a parent and set it's parent later
            //this is a supported scenario, just in case we only want to open
            //the file without the full emulator overhead
            if (_parent != null)
            {
                switch (Mapper)
                {
                    case NesMapper.NROM:
                        MapperObject = new Mappers.NRomMapper(_parent);
                        break;
                }
            }
        }

        public void SetEmulator(Emulator.NesEmulator parent)
        {
            _parent = parent;
        }

        private void Initialise()
        {
            byte[] temp = null;
             
            if (_input != null)
            {
                temp = new byte[3];
                _input.ReadBytes(temp, 0, 3);

                Signature = Encoding.Default.GetString(temp);

                if (!Signature.Equals("NES"))
                {
                    throw new InvalidDataException("Invalid iNES file");
                }

                ByteSignature  = (byte)_input.Read(8);

                if (ByteSignature != 0x1A)
                {
                    throw new InvalidDataException("Invalid iNES file");
                }

                //read info from header
                ProgramRomBankCount = (byte)_input.Read(8);
                VideoRomBankCount = (byte)_input.Read(8);
                RomControlByte1 = (byte)_input.Read(8);
                RomControlByte2 = (byte)_input.Read(8);
                RamBankCount = (byte)_input.Read(8);

                temp = new byte[7];
                _input.ReadBytes(temp, 0, 7);

                //test that all 7 reserved bytes are 0.
                //not always the case
                /*for (int k = 0; k < temp.Length; k++)
                {
                    if (temp[k] != 0)
                    {
                        throw new InvalidDataException("Invalid iNES file");
                    }
                }*/

                //test that bits 0-3 of control byte 2 are 0
                if ((RomControlByte2 & 0x0f) != 0)
                {
                    throw new InvalidDataException("Invalid iNES file");
                }

                //set info from control bytes
                Mapper = (NesMapper)(((RomControlByte1 & 0xf0) >> 4) | (RomControlByte2 & 0xf0));
                Mirroring = (MirrorType)(RomControlByte1 & 0x01);
                BatteryBackedRamPresent = ((RomControlByte1 & 0x02) == 0x02);
                TrainerPresent = ((RomControlByte1 & 0x04) == 0x04);
                Mirroring = ((RomControlByte1 & 0x08) == 0x08) ? MirrorType.FourScreen : Mirroring;

                if (TrainerPresent) 
                {
                    Trainer = new byte[TRAINER_SIZE];
                    _input.ReadBytes(Trainer, 0, (int)TRAINER_SIZE);
                }

                //read program rom banks
                ProgramRomBanks = new List<byte[]>();

                for (int i = 0; i < ProgramRomBankCount; i++)
                {
                    temp = new byte[PROG_ROM_SIZE];
                    _input.ReadBytes(temp, 0, (int)PROG_ROM_SIZE);
                    ProgramRomBanks.Add(temp);
                }

                //read video rom banks
                VideoRomBanks = new List<byte[]>();

                for (int i = 0; i < VideoRomBankCount; i++)
                {
                    temp = new byte[VID_ROM_SIZE];
                    _input.ReadBytes(temp, 0, (int)VID_ROM_SIZE);
                    VideoRomBanks.Add(temp);
                }

                //set up the nes mapper
                //this should be done last so the mapper will have access to the 
                //cartridge info
                //NOTE: This is now done by emulator directly after loading
                //the cartridge.
                //CreateMapper();
            }
        }
    }
}
