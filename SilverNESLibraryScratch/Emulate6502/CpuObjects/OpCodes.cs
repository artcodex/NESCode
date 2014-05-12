using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.CpuObjects
{
    public enum OpCodes
    {
        //ADC
        ADC_I = 0x69,
        ADC_Z = 0x65,
        ADC_ZX = 0x75,
        ADC_A = 0x6D,
        ADC_AX = 0x7D,
        ADC_AY = 0x79,
        ADC_IX = 0x61,
        ADC_IY = 0x71,

        //AND
        AND_I = 0x29,
        AND_Z = 0x25,
        AND_ZX = 0x35,
        AND_A = 0x2D,
        AND_AX = 0x3D,
        AND_AY = 0x39,
        AND_IX = 0x21,
        AND_IY = 0x31,

        //ASL
        ASL_A = 0x0A,
        ASL_Z = 0x06,
        ASL_ZX = 0x16,
        ASL_AB = 0x0E,
        ASL_AX = 0x1E,

        //Branch
        BCC = 0x90,
        BCS = 0xB0,
        BEQ = 0xF0,
        BIT_Z = 0x24,
        BIT_A = 0x2C,
        BMI = 0x30,
        BNE = 0xD0,
        BPL = 0x10,
        BRK = 0x00,
        BVC = 0x50,
        BVS = 0x70,

        //Flags
        CLC = 0x18,
        CLD = 0xD8,
        CLI = 0x58,
        CLV = 0xB8,
        
        //CMP
        CMP_I = 0xC9,
        CMP_Z = 0xC5,
        CMP_ZX = 0xD5,
        CMP_A = 0xCD,
        CMP_AX = 0xDD,
        CMP_AY = 0xD9,
        CMP_IX = 0xC1,
        CMP_IY = 0xD1,

        //CPX
        CPX_I = 0xE0,
        CPX_Z = 0xE4,
        CPX_A = 0xEC,

        //CPY
        CPY_I = 0xC0,
        CPY_Z = 0xC4,
        CPY_A = 0xCC,

        //DEC
        DEC_Z = 0xC6,
        DEC_ZX = 0xD6,
        DEC_A = 0xCE,
        DEC_AX = 0xDE,

        //DEX
        DEX = 0xCA,

        //DEY
        DEY = 0x88,

        //EOR
        EOR_I = 0x49,
        EOR_Z = 0x45,
        EOR_ZX = 0x55,
        EOR_A = 0x4D,
        EOR_AX = 0x5D,
        EOR_AY = 0x59,
        EOR_IX = 0x41,
        EOR_IY = 0x51,

        //INC
        INC_Z = 0xE6,
        INC_ZX = 0xF6,
        INC_A = 0xEE,
        INC_AX = 0xFE,

        //INX
        INX = 0xE8,

        //INY
        INY = 0xC8,

        //JMP
        JMP_A = 0x4C,
        JMP_I = 0x6C,

        //JSR
        JSR = 0x20,

        //LDA
        LDA_I = 0xA9,
        LDA_Z = 0xA5,
        LDA_ZX = 0xB5,
        LDA_A = 0xAD,
        LDA_AX = 0xBD,
        LDA_AY = 0xB9,
        LDA_IX = 0xA1,
        LDA_IY = 0xB1,

        //LDX
        LDX_I = 0xA2,
        LDX_Z = 0xA6,
        LDX_ZY = 0xB6,
        LDX_A = 0xAE,
        LDX_AY = 0xBE,

        //LDY
        LDY_I = 0xA0,
        LDY_Z = 0xA4,
        LDY_ZX = 0xB4,
        LDY_A = 0xAC,
        LDY_AX = 0xBC,

        //LSR
        LSR_AC = 0x4A,
        LSR_Z = 0x46,
        LSR_ZX = 0x56,
        LSR_A = 0x4E,
        LSR_AX = 0x5E,

        //NOP
        NOP = 0xEA,

        //ORA
        ORA_I = 0x09,
        ORA_Z = 0x05,
        ORA_ZX = 0x15,
        ORA_A = 0x0D,
        ORA_AX = 0x1D,
        ORA_AY = 0x19,
        ORA_IX = 0x01,
        ORA_IY = 0x11,

        //PHA
        PHA = 0x48,

        //PHP
        PHP = 0x08,

        //PLA
        PLA = 0x68,

        //PLP
        PLP = 0x28,

        //ROL
        ROL_AC = 0x2A,
        ROL_Z = 0x26,
        ROL_ZX = 0x36,
        ROL_A = 0x2E,
        ROL_AX = 0x3E,

        //ROR
        ROR_AC = 0x6A,
        ROR_Z = 0x66,
        ROR_ZX = 0x76,
        ROR_A = 0x6E,
        ROR_AX = 0x7E,

        //RTI
        RTI = 0x40,

        //RTS
        RTS = 0x60,

        //SBC
        SBC_I = 0xE9,
        SBC_Z = 0xE5,
        SBC_ZX = 0xF5,
        SBC_A = 0xED,
        SBC_AX = 0xFD,
        SBC_AY = 0xF9,
        SBC_IX = 0xE1,
        SBC_IY = 0xF1,

        //SEC
        SEC = 0x38,

        //SED
        SED = 0xF8,

        //SEI
        SEI = 0x78,

        //STA
        STA_Z = 0x85,
        STA_ZX = 0x95,
        STA_A = 0x8D,
        STA_AX = 0x9D,
        STA_AY = 0x99,
        STA_IX = 0x81,
        STA_IY = 0x91,

        //STX
        STX_Z = 0x86,
        STX_ZY = 0x96,
        STX_A = 0x8E,

        //STY
        STY_Z = 0x84,
        STY_ZX = 0x94,
        STY_A = 0x8C,

        //TAX
        TAX = 0xAA,

        //TAY
        TAY = 0xA8,

        //TSX
        TSX = 0xBA,

        //TXA 
        TXA = 0x8A,

        //TXS
        TXS = 0x9A,

        //TYA
        TYA = 0x98
    }

    public class CycleMapper
    {
        public delegate int CycleModFunction(OpCodes code, uint address);
        private Dictionary<OpCodes, byte> _cycleMap;

        /*public int AdjustCycles(OpCodes opcode, ushort pc, ushort address)
        {

        }*/

        private void InitializeCycleMapper()
        {
            _cycleMap = new Dictionary<OpCodes, byte>();

            //generate the cyle map
            //ADC
            _cycleMap.Add(OpCodes.ADC_I, 2);
            _cycleMap.Add(OpCodes.ADC_Z, 3);
            _cycleMap.Add(OpCodes.ADC_ZX, 4);
            _cycleMap.Add(OpCodes.ADC_A, 4);
            _cycleMap.Add(OpCodes.ADC_AX, 4);
            _cycleMap.Add(OpCodes.ADC_AY, 4);
            _cycleMap.Add(OpCodes.ADC_IX, 6);
            _cycleMap.Add(OpCodes.ADC_IY, 5);

            //AND
            _cycleMap.Add(OpCodes.AND_I, 2);
            _cycleMap.Add(OpCodes.AND_Z, 3);
            _cycleMap.Add(OpCodes.AND_ZX, 4);
            _cycleMap.Add(OpCodes.AND_A, 4);
            _cycleMap.Add(OpCodes.AND_AX, 4);
            _cycleMap.Add(OpCodes.AND_AY, 4);
            _cycleMap.Add(OpCodes.AND_IX, 6);
            _cycleMap.Add(OpCodes.AND_IY, 5);

            //ASL
            _cycleMap.Add(OpCodes.ASL_A, 2);
            _cycleMap.Add(OpCodes.ASL_Z, 5);
            _cycleMap.Add(OpCodes.ASL_ZX, 6);
            _cycleMap.Add(OpCodes.ASL_AB, 6);
            _cycleMap.Add(OpCodes.ASL_AX, 7);

            //Branch
            _cycleMap.Add(OpCodes.BCC, 2);
            _cycleMap.Add(OpCodes.BCS, 2);
            _cycleMap.Add(OpCodes.BEQ, 2);
            _cycleMap.Add(OpCodes.BIT_Z, 3);
            _cycleMap.Add(OpCodes.BIT_A, 4);
            _cycleMap.Add(OpCodes.BMI, 2);
            _cycleMap.Add(OpCodes.BNE, 2);
            _cycleMap.Add(OpCodes.BPL, 2);
            _cycleMap.Add(OpCodes.BRK, 7);
            _cycleMap.Add(OpCodes.BVC, 2);
            _cycleMap.Add(OpCodes.BVS, 2);

            //Flags
            _cycleMap.Add(OpCodes.CLC, 2);
            _cycleMap.Add(OpCodes.CLD, 2);
            _cycleMap.Add(OpCodes.CLI, 2);
            _cycleMap.Add(OpCodes.CLV, 2);

            //CMP
            _cycleMap.Add(OpCodes.CMP_I, 2);
            _cycleMap.Add(OpCodes.CMP_Z, 3);
            _cycleMap.Add(OpCodes.CMP_ZX, 4);
            _cycleMap.Add(OpCodes.CMP_A, 4);
            _cycleMap.Add(OpCodes.CMP_AX, 4);
            _cycleMap.Add(OpCodes.CMP_AY, 4);
            _cycleMap.Add(OpCodes.CMP_IX, 6);
            _cycleMap.Add(OpCodes.CMP_IY, 5);

            //CPX
            _cycleMap.Add(OpCodes.CPX_I, 2);
            _cycleMap.Add(OpCodes.CPX_Z, 3);
            _cycleMap.Add(OpCodes.CPX_A, 4);

            //CPY
            _cycleMap.Add(OpCodes.CPY_I, 2);
            _cycleMap.Add(OpCodes.CPY_Z, 3);
            _cycleMap.Add(OpCodes.CPY_A, 4);

            //DEC
            _cycleMap.Add(OpCodes.DEC_Z, 5);
            _cycleMap.Add(OpCodes.DEC_ZX, 6);
            _cycleMap.Add(OpCodes.DEC_A, 6);
            _cycleMap.Add(OpCodes.DEC_AX, 7);

            //DEX
            _cycleMap.Add(OpCodes.DEX, 2);

            //DEY
            _cycleMap.Add(OpCodes.DEY, 2);

            //EOR
            _cycleMap.Add(OpCodes.EOR_I, 2);
            _cycleMap.Add(OpCodes.EOR_Z, 3);
            _cycleMap.Add(OpCodes.EOR_ZX, 4);
            _cycleMap.Add(OpCodes.EOR_A, 4);
            _cycleMap.Add(OpCodes.EOR_AX, 4);
            _cycleMap.Add(OpCodes.EOR_AY, 4);
            _cycleMap.Add(OpCodes.EOR_IX, 6);
            _cycleMap.Add(OpCodes.EOR_IY, 5);

            //INC
            _cycleMap.Add(OpCodes.INC_Z, 5);
            _cycleMap.Add(OpCodes.INC_ZX, 6);
            _cycleMap.Add(OpCodes.INC_A, 6);
            _cycleMap.Add(OpCodes.INC_AX, 7);

            //INX
            _cycleMap.Add(OpCodes.INX, 2);

            //INY
            _cycleMap.Add(OpCodes.INY, 2);

            //JMP
            _cycleMap.Add(OpCodes.JMP_A, 3);
            _cycleMap.Add(OpCodes.JMP_I, 5);

            //JSR
            _cycleMap.Add(OpCodes.JSR, 6);

            //LDA
            _cycleMap.Add(OpCodes.LDA_I, 2);
            _cycleMap.Add(OpCodes.LDA_Z, 3);
            _cycleMap.Add(OpCodes.LDA_ZX, 4);
            _cycleMap.Add(OpCodes.LDA_A, 4);
            _cycleMap.Add(OpCodes.LDA_AX, 4);
            _cycleMap.Add(OpCodes.LDA_AY, 4);
            _cycleMap.Add(OpCodes.LDA_IX, 6);
            _cycleMap.Add(OpCodes.LDA_IY, 5);

            //LDX
            _cycleMap.Add(OpCodes.LDX_I, 2);
            _cycleMap.Add(OpCodes.LDX_Z, 3);
            _cycleMap.Add(OpCodes.LDX_ZY, 4);
            _cycleMap.Add(OpCodes.LDX_A, 4);
            _cycleMap.Add(OpCodes.LDX_AY, 4);

            //LDY
            _cycleMap.Add(OpCodes.LDY_I, 2);
            _cycleMap.Add(OpCodes.LDY_Z, 3);
            _cycleMap.Add(OpCodes.LDY_ZX, 4);
            _cycleMap.Add(OpCodes.LDY_A, 4);
            _cycleMap.Add(OpCodes.LDY_AX, 4);

            //LSR
            _cycleMap.Add(OpCodes.LSR_AC, 2);
            _cycleMap.Add(OpCodes.LSR_Z, 5);
            _cycleMap.Add(OpCodes.LSR_ZX, 6);
            _cycleMap.Add(OpCodes.LSR_A, 6);
            _cycleMap.Add(OpCodes.LSR_AX, 7);

            //NOP
            _cycleMap.Add(OpCodes.NOP, 2);

            //ORA
            _cycleMap.Add(OpCodes.ORA_I, 2);
            _cycleMap.Add(OpCodes.ORA_Z, 3);
            _cycleMap.Add(OpCodes.ORA_ZX, 4);
            _cycleMap.Add(OpCodes.ORA_A, 4);
            _cycleMap.Add(OpCodes.ORA_AX, 4);
            _cycleMap.Add(OpCodes.ORA_AY, 4);
            _cycleMap.Add(OpCodes.ORA_IX, 6);
            _cycleMap.Add(OpCodes.ORA_IY, 5);

            //PHA
            _cycleMap.Add(OpCodes.PHA, 3);

            //PHP
            _cycleMap.Add(OpCodes.PHP, 3);

            //PLA
            _cycleMap.Add(OpCodes.PLA, 4);

            //PLP
            _cycleMap.Add(OpCodes.PLP, 4);

            //ROL
            _cycleMap.Add(OpCodes.ROL_AC, 2);
            _cycleMap.Add(OpCodes.ROL_Z, 5);
            _cycleMap.Add(OpCodes.ROL_ZX, 6);
            _cycleMap.Add(OpCodes.ROL_A, 6);
            _cycleMap.Add(OpCodes.ROL_AX, 7);

            //ROR
            _cycleMap.Add(OpCodes.ROR_AC, 2);
            _cycleMap.Add(OpCodes.ROR_Z, 5);
            _cycleMap.Add(OpCodes.ROR_ZX, 6);
            _cycleMap.Add(OpCodes.ROR_A, 6);
            _cycleMap.Add(OpCodes.ROR_AX, 7);

            //RTI
            _cycleMap.Add(OpCodes.RTI, 6);

            //RTS
            _cycleMap.Add(OpCodes.RTS, 6);

            //SBC
            _cycleMap.Add(OpCodes.SBC_I, 2);
            _cycleMap.Add(OpCodes.SBC_Z, 3);
            _cycleMap.Add(OpCodes.SBC_ZX, 4);
            _cycleMap.Add(OpCodes.SBC_A, 4);
            _cycleMap.Add(OpCodes.SBC_AX, 4);
            _cycleMap.Add(OpCodes.SBC_AY, 4);
            _cycleMap.Add(OpCodes.SBC_IX, 6);
            _cycleMap.Add(OpCodes.SBC_IY, 5);

            //SEC
            _cycleMap.Add(OpCodes.SEC, 2);

            //SED
            _cycleMap.Add(OpCodes.SED, 2);

            //SEI
            _cycleMap.Add(OpCodes.SEI, 2);

            //STA
            _cycleMap.Add(OpCodes.STA_Z, 3);
            _cycleMap.Add(OpCodes.STA_ZX, 4);
            _cycleMap.Add(OpCodes.STA_A, 4);
            _cycleMap.Add(OpCodes.STA_AX, 5);
            _cycleMap.Add(OpCodes.STA_AY, 5);
            _cycleMap.Add(OpCodes.STA_IX, 6);
            _cycleMap.Add(OpCodes.STA_IY, 6);

            //STX
            _cycleMap.Add(OpCodes.STX_Z, 3);
            _cycleMap.Add(OpCodes.STX_ZY, 4);
            _cycleMap.Add(OpCodes.STX_A, 4);

            //STY
            _cycleMap.Add(OpCodes.STY_Z, 3);
            _cycleMap.Add(OpCodes.STY_ZX, 4);
            _cycleMap.Add(OpCodes.STY_A, 4);

            //TAX
            _cycleMap.Add(OpCodes.TAX, 2);

            //TAY
            _cycleMap.Add(OpCodes.TAY, 2);

            //TSX
            _cycleMap.Add(OpCodes.TSX, 2);

            //TXA 
            _cycleMap.Add(OpCodes.TXA, 2);

            //TXS
            _cycleMap.Add(OpCodes.TXS, 2);

            //TYA
            _cycleMap.Add(OpCodes.TYA, 2);
        }

        public CycleMapper()
        {
            InitializeCycleMapper();
        }

        public byte this[OpCodes code]
        {
            get
            {
                if (_cycleMap.ContainsKey(code))
                {
                    return _cycleMap[code];
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
