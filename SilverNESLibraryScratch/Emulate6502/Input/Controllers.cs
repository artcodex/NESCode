using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.Input
{
    [Flags]
    public enum NESButtons
    {
        A = 1,
        B = 2,
        Select = 4,
        Start = 8,
        Up = 16,
        Down = 32,
        Left = 64,
        Right = 128
    }

    public enum NESControllers
    {
        Joypad0 = 0,
        Joypad1 = 1
    }

    //we only handle the main nes joypads currently
    //really simplifying things here, plus in the browser
    //this may make most sense to start
    public class Controllers : Memory.AbstractMemoryMapper
    {
        public uint CONTROLLER_PORT_0 = 0x4016;
        public uint CONTROLLER_PORT_1 = 0x4017;

        private bool _currentStrobe = false;

        //controller states
        private byte _currentStatePad0;
        private byte _currentStatePad1;

        //we will use other variables to hold
        //modified states so that, the new
        //info is only copied over on 
        //joypad strobe
        private byte _newStatePad0;
        private byte _newStatePad1;

        //to return the signature at reads 17-20 we
        //will hold the read counts which are reset on strobe
        private uint _readCount0;
        private uint _readCount1;


        private Emulator.NesEmulator _parent;

        public Controllers(Emulator.NesEmulator parent)
        {
            _parent = parent;
            InitializeControllers();
        }

        private void InitializeControllers()
        {
            _parent.MainMemory.AttachMemoryMapping(this, CONTROLLER_PORT_0, CONTROLLER_PORT_1);
        }

        public void Reset()
        {
            _currentStatePad0 = 0;
            _currentStatePad1 = 0;
            _newStatePad0 = 0;
            _newStatePad1 = 0;
            _readCount1 = 0;
            _readCount0 = 0;
            _currentStrobe = false;
        }

        public override string ToString()
        {
            return "NES Input Ports";
        }

        public override byte Read(uint address)
        {
            byte temp = 0;

            if (address == CONTROLLER_PORT_0)
            {
                temp = _currentStatePad0;
                _currentStatePad0 >>= 1;

                switch (_readCount0++)
                {
                    case 17:
                        temp = 0x00;
                        break;
                    case 18:
                        temp = 0x00;
                        break;
                    case 19:
                        temp = 0x00;
                        break;
                    case 20:
                        temp = 0x01;
                        break;
                }
            }
            else if (address == CONTROLLER_PORT_1)
            {
                //return signature at reads 17-20
                switch (_readCount1++)
                {
                    case 17:
                        temp = 0x00;
                        break;
                    case 18:
                        temp = 0x00;
                        break;
                    case 19:
                        temp = 0x01;
                        break;
                    case 20:
                        temp = 0x00;
                        break;
                }

                temp = _currentStatePad1;
                _currentStatePad1 >>= 1;
            }

            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(address, this, CpuObjects.MemoryOperation.Read, temp);
            }

            return (byte)(temp & 0x01);
        }

        public override void Write(uint address, byte value)
        {
            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(address, this, CpuObjects.MemoryOperation.Write, value);
            }

            //we only handle writes to joypad 0
            if (address == CONTROLLER_PORT_0)
            {
                //are we currently on strobe ?
                //if so re-read the values into the pad states
                if (_currentStrobe)
                {
                    _currentStatePad0 = _newStatePad0;
                    _currentStatePad1 = _newStatePad1;
                    _readCount0 = 0;
                    _readCount1 = 0;

                    _currentStrobe = false;
                }
                else
                {
                    //if 1 is written to the port we are entering strobing
                    if ((value & 0x01) == 0x01)
                    {
                        _currentStrobe = true;
                    }
                }
            }
        }

        public void PressControllerKey(NESControllers controller, NESButtons button)
        {
            if (controller == NESControllers.Joypad0)
            {
                _newStatePad0 |= (byte)(0x01 << (((int)button) - 1));
            }
            else if (controller == NESControllers.Joypad1)
            {
                _newStatePad1 |= (byte)(0x01 << (((int)button) - 1));
            }
        }

        public void ReleaseControllerKey(NESControllers controller, NESButtons button)
        {
            if (controller == NESControllers.Joypad0)
            {
                _newStatePad0 -= (byte)(0x01 << (((int)button) - 1));
            }
            else if (controller == NESControllers.Joypad1)
            {
                _newStatePad1 -= (byte)(0x01 << (((int)button) - 1));
            }
        }

        public void ResetControllerState(NESControllers controller, byte state)
        {
            if (controller == NESControllers.Joypad0)
            {
                _newStatePad0 = state;
            }
            else if (controller == NESControllers.Joypad1)
            {
                _newStatePad1 = state;
            }
        }

        public override void ReadBlock(uint startAddress, uint endAddress, byte[] values)
        {
            throw new NotImplementedException();
        }

        public override void WriteBlock(uint startAddress, uint endAddress, byte[] values)
        {
            throw new NotImplementedException();
        }
    }
}
