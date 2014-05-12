using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Emulate6502.Cartridge;
using Emulate6502.CpuObjects;

namespace Emulate6502.CartridgeTests
{
    public class CartridgeTests
    {
        public void Init()
        {

        }

        public void TestLoadCartridge(Stream io)
        {
            /*Cartridge.Cartridge cart = Cartridge.Cartridge.Load(io);

            if (cart.Signature != "NES")
            {
                throw new Exception("Invalid cart signature");
            }

            if (cart.ByteSignature != 0x1a)
            {
                throw new Exception("Invalid cart byte signature");
            }*/

            /*Cpu cpu = new Cpu(null);
            cpu.MainMemory.WriteBlock(0xFFFF - Cartridge.Cartridge.PROG_ROM_SIZE, 0xFFFF, cart.ProgramRomBanks[0]);
            cpu.Run((ushort)(0xFFFF - Cartridge.Cartridge.PROG_ROM_SIZE + 100));*/
        }
    }
}
