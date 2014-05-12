using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.CpuObjects
{
    public static class Dissasembler
    {
        private static int _currentInstructionPtr = 0;
        private static Dictionary<OpCodes, int> _executedCodes = new Dictionary<OpCodes,int>();

        public static List<StringBuilder> Dissasemble(Cartridge.Cartridge nesCartridge)
        {
            List<StringBuilder> sbDissasembledBanks = new List<StringBuilder>();
            Memory.GeneralMemoryMapper _prgMemory = new Emulate6502.Memory.GeneralMemoryMapper();

            foreach (var programBank in nesCartridge.ProgramRomBanks)
            {
                StringBuilder dissasembledBank = new StringBuilder();
                string nextStatement = string.Empty;
                uint addressIndex = 0;

                //fix the memory mapper to current prg bank array
                _prgMemory.SetArray(programBank);

                while (DissasembleNextStatement(_prgMemory, ref addressIndex, out nextStatement))
                {

                    dissasembledBank.Append(nextStatement + Environment.NewLine);

                }

                sbDissasembledBanks.Add(dissasembledBank);
            }

            return sbDissasembledBanks;
        }

        private static string FormatStatement(string statement)
        {
            return string.Format("{0}:\t {1}", _currentInstructionPtr.ToString("X8"),  statement); 
        }

        private static ushort Read16bitAddressAtIndex(Memory.MemoryMapper memory, ref uint index)
        {
            ushort temp = 0;

            temp = memory[index++];
            temp += (ushort)(memory[index++] << 8);

            return temp;
        }

        private static int HandleNegative(byte value)
        {
            if ((value & 0x80) == 0x80)
            {
                value ^= 0xFF;
                value++;

                return -((int)value);
            }
            else
            {
                return (int)value;
            }
        }

        private static void AddDictEntry(OpCodes code)
        {
            if (_executedCodes.ContainsKey(code))
            {
                _executedCodes[code]++;
            }
            else
            {
                _executedCodes.Add(code, 1);
            }
        }

        public static bool DissasembleNextStatement(Memory.MemoryMapper memory, ref uint index, out string nextStatement)
        {
            
            nextStatement = string.Empty;

            if (memory.CanAccess(index + 1))
            {
                _currentInstructionPtr = (int)index;
                OpCodes opCode = (OpCodes)memory[index++];

                AddDictEntry(opCode);

                switch (opCode)
                {
                    //ADC
                    case OpCodes.ADC_A:
                        nextStatement = FormatStatement("ADC $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4"));
                        break;
                    case OpCodes.ADC_Z:
                        nextStatement = FormatStatement("ADC $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.ADC_ZX:
                        nextStatement = FormatStatement("ADC $0x" + memory[index++].ToString("X2") + ",X");
                        break;
                    case OpCodes.ADC_I:
                        nextStatement = FormatStatement("ADC #$0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.ADC_AX:
                        nextStatement = FormatStatement("ADC $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",X"); 
                        break;
                    case OpCodes.ADC_AY:
                        nextStatement = FormatStatement("ADC $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",Y"); 
                        break;
                    case OpCodes.ADC_IX:
                        nextStatement = FormatStatement("ADC ($0x" + memory[index++].ToString("X2") + ",X)");
                        break;
                    case OpCodes.ADC_IY:
                        nextStatement = FormatStatement("ADC ($0x" + memory[index++].ToString("X2") + "),Y");
                        break;

                    //AND
                    case OpCodes.AND_A:
                        nextStatement = FormatStatement("AND $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.AND_AX:
                        nextStatement = FormatStatement("AND $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",X");
                        break;
                    case OpCodes.AND_AY:
                        nextStatement = FormatStatement("AND $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",Y"); 
                        break;
                    case OpCodes.AND_I:
                        nextStatement = FormatStatement("AND #$0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.AND_IX:
                        nextStatement = FormatStatement("AND ($0x" + memory[index++].ToString("X2") + ",X)");
                        break;
                    case OpCodes.AND_IY:
                        nextStatement = FormatStatement("AND ($0x" + memory[index++].ToString("X2") + "),Y");
                        break;
                    case OpCodes.AND_Z:
                        nextStatement = FormatStatement("AND $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.AND_ZX:
                        nextStatement = FormatStatement("AND $0x" + memory[index++].ToString("X2") + ",X");
                        break;

                    //ASL
                    case OpCodes.ASL_AB:
                        nextStatement = FormatStatement("ASL $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.ASL_A:
                        nextStatement = FormatStatement("ASL");
                        break;
                    case OpCodes.ASL_AX:
                        nextStatement = FormatStatement("ASL $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",X");
                        break;
                    case OpCodes.ASL_Z:
                        nextStatement = FormatStatement("ASL $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.ASL_ZX:
                        nextStatement = FormatStatement("ASL $0x" + memory[index++].ToString("X2") + ",X");
                        break;

                    //Branching
                    case OpCodes.BCC:
                        nextStatement = FormatStatement("BCC #$0x" + memory[index++].ToString("X2") + " ($0x" + (index + HandleNegative(memory[index-1])).ToString("X8") + ")"); 
                        break;
                    case OpCodes.BCS:
                        nextStatement = FormatStatement("BCS #$0x" + memory[index++].ToString("X2") + " ($0x" + (index + HandleNegative(memory[index - 1])).ToString("X8") + ")"); 
                        break;
                    case OpCodes.BEQ:
                        nextStatement = FormatStatement("BEQ #$0x" + memory[index++].ToString("X2") + " ($0x" + (index + HandleNegative(memory[index - 1])).ToString("X8") + ")"); 
                        break;
                    case OpCodes.BMI:
                        nextStatement = FormatStatement("BMI #$0x" + memory[index++].ToString("X2") + " ($0x" + (index + HandleNegative(memory[index - 1])).ToString("X8") + ")"); 
                        break;
                    case OpCodes.BNE:
                        nextStatement = FormatStatement("BNE #$0x" + memory[index++].ToString("X2") + " ($0x" + (index + HandleNegative(memory[index - 1])).ToString("X8") + ")"); 
                        break;
                    case OpCodes.BPL:
                        nextStatement = FormatStatement("BPL #$0x" + memory[index++].ToString("X2") + " ($0x" + (index + HandleNegative(memory[index - 1])).ToString("X8") + ")"); 
                        break;
                    case OpCodes.BVC:
                        nextStatement = FormatStatement("BVC #$0x" + memory[index++].ToString("X2") + " ($0x" + (index + HandleNegative(memory[index - 1])).ToString("X8") + ")"); 
                        break;
                    case OpCodes.BVS:
                        nextStatement = FormatStatement("BVS #$0x" + memory[index++].ToString("X2") + " ($0x" + (index + HandleNegative(memory[index - 1])).ToString("X8") + ")"); 
                        break;

                    //BRK
                    case OpCodes.BRK: //(Advanced instruction will look into)
                        nextStatement = FormatStatement("BRK");
                        break;
                    //BIT
                    case OpCodes.BIT_A:
                        nextStatement = FormatStatement("BIT $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.BIT_Z:
                        nextStatement = FormatStatement("BIT $0x" + memory[index++].ToString("X2")); 
                        break;

                    //Status Clear Operations
                    case OpCodes.CLC:
                        nextStatement = FormatStatement("CLC");
                        break;
                    case OpCodes.CLD:
                        nextStatement = FormatStatement("CLD");
                        break;
                    case OpCodes.CLI:
                        nextStatement = FormatStatement("CLI");
                        break;
                    case OpCodes.CLV:
                        nextStatement = FormatStatement("CLV");
                        break;

                    //Compare Operations
                    case OpCodes.CMP_A:
                        nextStatement = FormatStatement("CMP $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.CMP_AX:
                        nextStatement = FormatStatement("CMP $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",X");
                        break;
                    case OpCodes.CMP_AY:
                        nextStatement = FormatStatement("CMP $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",Y"); 
                        break;
                    case OpCodes.CMP_I:
                        nextStatement = FormatStatement("CMP #$0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.CMP_IX:
                        nextStatement = FormatStatement("CMP ($0x" + memory[index++].ToString("X2") + ",X)");
                        break;
                    case OpCodes.CMP_IY:
                        nextStatement = FormatStatement("CMP ($0x" + memory[index++].ToString("X2") + "),Y");
                        break;
                    case OpCodes.CMP_Z:
                        nextStatement = FormatStatement("CMP $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.CMP_ZX:
                        nextStatement = FormatStatement("CMP $0x" + memory[index++].ToString("X2") + ",X");
                        break;
                    case OpCodes.CPX_A:
                        nextStatement = FormatStatement("CPX $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.CPX_I:
                        nextStatement = FormatStatement("CPX #$0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.CPX_Z:
                        nextStatement = FormatStatement("CPX $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.CPY_A:
                        nextStatement = FormatStatement("CPY $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.CPY_I:
                        nextStatement = FormatStatement("CPY #$0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.CPY_Z:
                        nextStatement = FormatStatement("CPY $0x" + memory[index++].ToString("X2")); 
                        break;

                    //Decrement Operations
                    case OpCodes.DEC_A:
                        nextStatement = FormatStatement("DEC $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.DEC_AX:
                        nextStatement = FormatStatement("ADC $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",X");
                        break;
                    case OpCodes.DEC_Z:
                        nextStatement = FormatStatement("DEC $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.DEC_ZX:
                        nextStatement = FormatStatement("DEC $0x" + memory[index++].ToString("X2") + ",X");
                        break;
                    case OpCodes.DEX:
                        nextStatement = FormatStatement("DEX");
                        break;
                    case OpCodes.DEY:
                        nextStatement = FormatStatement("DEY");
                        break;

                    //XOR
                    case OpCodes.EOR_A:
                        nextStatement = FormatStatement("EOR $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.EOR_AX:
                        nextStatement = FormatStatement("EOR $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",X");
                        break;
                    case OpCodes.EOR_AY:
                        nextStatement = FormatStatement("EOR $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",Y"); 
                        break;
                    case OpCodes.EOR_I:
                        nextStatement = FormatStatement("EOR #$0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.EOR_IX:
                        nextStatement = FormatStatement("EOR ($0x" + memory[index++].ToString("X2") + ",X)");
                        break;
                    case OpCodes.EOR_IY:
                        nextStatement = FormatStatement("EOR ($0x" + memory[index++].ToString("X2") + "),Y");
                        break;
                    case OpCodes.EOR_Z:
                        nextStatement = FormatStatement("EOR $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.EOR_ZX:
                        nextStatement = FormatStatement("EOR $0x" + memory[index++].ToString("X2") + ",X");
                        break;

                    //Increment
                    case OpCodes.INC_A:
                        nextStatement = FormatStatement("INC $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.INC_AX:
                        nextStatement = FormatStatement("INC $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",X");
                        break;
                    case OpCodes.INC_Z:
                        nextStatement = FormatStatement("INC $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.INC_ZX:
                        nextStatement = FormatStatement("INC $0x" + memory[index++].ToString("X2") + ",X");
                        break;
                    case OpCodes.INX:
                        nextStatement = FormatStatement("INX");
                        break;
                    case OpCodes.INY:
                        nextStatement = FormatStatement("INY");
                        break;

                    //Jump Operations
                    case OpCodes.JMP_A:
                        nextStatement = FormatStatement("JMP $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.JMP_I:
                        nextStatement = FormatStatement("LDA ($0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ")"); 
                        break;
                    case OpCodes.JSR:
                        nextStatement = FormatStatement("JSR $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4"));
                        break;

                    //Load Operations
                    case OpCodes.LDA_A:
                        nextStatement = FormatStatement("LDA $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.LDA_AX:
                        nextStatement = FormatStatement("LDA $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",X");
                        break;
                    case OpCodes.LDA_AY:
                        nextStatement = FormatStatement("LDA $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",Y"); 
                        break;
                    case OpCodes.LDA_I:
                        nextStatement = FormatStatement("LDA #$0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.LDA_IX:
                        nextStatement = FormatStatement("LDA ($0x" + memory[index++].ToString("X2") + ",X)");
                        break;
                    case OpCodes.LDA_IY:
                        nextStatement = FormatStatement("LDA ($0x" + memory[index++].ToString("X2") + "),Y");
                        break;
                    case OpCodes.LDA_Z:
                        nextStatement = FormatStatement("LDA $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.LDA_ZX:
                        nextStatement = FormatStatement("LDA $0x" + memory[index++].ToString("X2") + ",X");
                        break;
                    case OpCodes.LDX_A:
                        nextStatement = FormatStatement("LDX $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.LDX_AY:
                        nextStatement = FormatStatement("LDX $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",Y"); 
                        break;
                    case OpCodes.LDX_I:
                        nextStatement = FormatStatement("LDX #$0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.LDX_Z:
                        nextStatement = FormatStatement("LDX $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.LDX_ZY:
                        nextStatement = FormatStatement("ADC $0x" + memory[index++].ToString("X2") + ",Y");
                        break;
                    case OpCodes.LDY_A:
                        nextStatement = FormatStatement("LDY $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.LDY_AX:
                        nextStatement = FormatStatement("LDY $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",X");
                        break;
                    case OpCodes.LDY_I:
                        nextStatement = FormatStatement("LDY #$0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.LDY_Z:
                        nextStatement = FormatStatement("LDY $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.LDY_ZX:
                        nextStatement = FormatStatement("LDY $0x" + memory[index++].ToString("X2") + ",X");
                        break;

                    //LSR
                    case OpCodes.LSR_A:
                        nextStatement = FormatStatement("LSR $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.LSR_AC:
                        nextStatement = FormatStatement("LSR");
                        break;
                    case OpCodes.LSR_AX:
                        nextStatement = FormatStatement("LSR $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",X");
                        break;
                    case OpCodes.LSR_Z:
                        nextStatement = FormatStatement("LSR $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.LSR_ZX:
                        nextStatement = FormatStatement("LSR $0x" + memory[index++].ToString("X2") + ",X");
                        break;

                    //NOP
                    case OpCodes.NOP:
                        nextStatement = FormatStatement("NOP");
                        break;

                    //OR
                    case OpCodes.ORA_A:
                        nextStatement = FormatStatement("ORA $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.ORA_AX:
                        nextStatement = FormatStatement("ORA $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",X");
                        break;
                    case OpCodes.ORA_AY:
                        nextStatement = FormatStatement("ORA $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",Y"); 
                        break;
                    case OpCodes.ORA_I:
                        nextStatement = FormatStatement("ORA #$0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.ORA_IX:
                        nextStatement = FormatStatement("ORA ($0x" + memory[index++].ToString("X2") + ",X)");
                        break;
                    case OpCodes.ORA_IY:
                        nextStatement = FormatStatement("ORA ($0x" + memory[index++].ToString("X2") + "),Y");
                        break;
                    case OpCodes.ORA_Z:
                        nextStatement = FormatStatement("ORA $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.ORA_ZX:
                        nextStatement = FormatStatement("ORA $0x" + memory[index++].ToString("X2") + ",X");
                        break;

                    //Stack Operations
                    case OpCodes.PHA:
                        nextStatement = FormatStatement("PHA");
                        break;
                    case OpCodes.PHP:
                        nextStatement = FormatStatement("PHP");
                        break;
                    case OpCodes.PLA:
                        nextStatement = FormatStatement("PLA");
                        break;
                    case OpCodes.PLP:
                        nextStatement = FormatStatement("PLP");
                        break;

                    //ROL
                    case OpCodes.ROL_A:
                        nextStatement = FormatStatement("ROL $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.ROL_AC:
                        nextStatement = FormatStatement("ROL");
                        break;
                    case OpCodes.ROL_AX:
                        nextStatement = FormatStatement("ROL $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",X");
                        break;
                    case OpCodes.ROL_Z:
                        nextStatement = FormatStatement("ROL $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.ROL_ZX:
                        nextStatement = FormatStatement("ROL $0x" + memory[index++].ToString("X2") + ",X");
                        break;

                    //ROR
                    case OpCodes.ROR_A:
                        nextStatement = FormatStatement("ROR $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.ROR_AC:
                        nextStatement = FormatStatement("ROR");
                        break;
                    case OpCodes.ROR_AX:
                        nextStatement = FormatStatement("ROR $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",X");
                        break;
                    case OpCodes.ROR_Z:
                        nextStatement = FormatStatement("ROR $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.ROR_ZX:
                        nextStatement = FormatStatement("ROR $0x" + memory[index++].ToString("X2") + ",X");
                        break;

                    //Return from interrupt/subroutine
                    case OpCodes.RTI:
                        nextStatement = FormatStatement("RTI");
                        break;
                    case OpCodes.RTS:
                        nextStatement = FormatStatement("RTS");
                        break;

                    //SBC
                    case OpCodes.SBC_A:
                        nextStatement = FormatStatement("SBC $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.SBC_AX:
                        nextStatement = FormatStatement("SBC $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",X");
                        break;
                    case OpCodes.SBC_AY:
                        nextStatement = FormatStatement("SBC $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",Y"); 
                        break;
                    case OpCodes.SBC_I:
                        nextStatement = FormatStatement("SBC #$0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.SBC_IX:
                        nextStatement = FormatStatement("SBC ($0x" + memory[index++].ToString("X2") + ",X)");
                        break;
                    case OpCodes.SBC_IY:
                        nextStatement = FormatStatement("SBC ($0x" + memory[index++].ToString("X2") + "),Y");
                        break;
                    case OpCodes.SBC_Z:
                        nextStatement = FormatStatement("SBC $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.SBC_ZX:
                        nextStatement = FormatStatement("SBC $0x" + memory[index++].ToString("X2") + ",X");
                        break;

                    //Flag set operations
                    case OpCodes.SEC:
                        nextStatement = FormatStatement("SEC");
                        break;
                    case OpCodes.SED:
                        nextStatement = FormatStatement("SED");
                        break;
                    case OpCodes.SEI:
                        nextStatement = FormatStatement("SEI");
                        break;

                    //Store operations
                    //Accumulator
                    case OpCodes.STA_A:
                        nextStatement = FormatStatement("STA $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.STA_AX:
                        nextStatement = FormatStatement("STA $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",X");
                        break;
                    case OpCodes.STA_AY:
                        nextStatement = FormatStatement("STA $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4") + ",Y"); 
                        break;
                    case OpCodes.STA_IX:
                        nextStatement = FormatStatement("STA ($0x" + memory[index++].ToString("X2") + ",X)");
                        break;
                    case OpCodes.STA_IY:
                        nextStatement = FormatStatement("STA ($0x" + memory[index++].ToString("X2") + "),Y");
                        break;
                    case OpCodes.STA_Z:
                        nextStatement = FormatStatement("STA $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.STA_ZX:
                        nextStatement = FormatStatement("STA $0x" + memory[index++].ToString("X2") + ",X");
                        break;

                    //X
                    case OpCodes.STX_A:
                        nextStatement = FormatStatement("STX $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.STX_Z:
                        nextStatement = FormatStatement("STX $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.STX_ZY:
                        nextStatement = FormatStatement("STX $0x" + memory[index++].ToString("X2") + ",Y");
                        break;

                    //Y
                    case OpCodes.STY_A:
                        nextStatement = FormatStatement("STY $0x" + Read16bitAddressAtIndex(memory, ref index).ToString("X4")); 
                        break;
                    case OpCodes.STY_Z:
                        nextStatement = FormatStatement("STY $0x" + memory[index++].ToString("X2")); 
                        break;
                    case OpCodes.STY_ZX:
                        nextStatement = FormatStatement("STY $0x" + memory[index++].ToString("X2") + ",X");
                        break;

                    //Transfer operations
                    case OpCodes.TAX:
                        nextStatement = FormatStatement("TAX");
                        break;
                    case OpCodes.TAY:
                        nextStatement = FormatStatement("TAY");
                        break;
                    case OpCodes.TSX:
                        nextStatement = FormatStatement("TSX");
                        break;
                    case OpCodes.TXA:
                        nextStatement = FormatStatement("TXA");
                        break;
                    case OpCodes.TXS:
                        nextStatement = FormatStatement("TXS");
                        break;
                    case OpCodes.TYA:
                        nextStatement = FormatStatement("TYA");
                        break;

                    default:
                        //throw new Exception("Bad opcode");
                        nextStatement = FormatStatement(".db $0x" + ((int)opCode).ToString("X2") + " ;Invalid opcode");
                        break;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
