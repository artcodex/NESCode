using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.Memory
{
    //Again a very simple prototype although
    //we do play with a downward and upward growing stack
    //don't provide constructor overrides
    //currently we will assume a little endian machine
    //with 8-bit stack size (which is exactly the 6502 model)
    //Fix this to not rely on negative internal stack pointer
    public class Stack
    {
        private uint _stackSize;
        private Memory _memorySpace;
        private uint _baseAddress;
        private bool _growsDownward;
        private int _stackPointer;

        public uint BaseAddress 
        {
            get
            {
                return _baseAddress;
            }
        }

        public byte StackPointer
        {
            get
            {
                return (byte)_stackPointer;
            }
            set
            {
                _stackPointer = value;
            }
        }

        public uint StackAbsolutePointer
        {
            get
            {
                return (uint)(((int)_baseAddress) + _stackPointer);
            }
        }

        public bool GrowsDownward
        {
            get
            {
                return _growsDownward;
            }
        }

        public Stack(uint stackSize,
                     uint baseAddress,
                     bool growsDownward,
                     Memory backingMemSpace) 
        {
            _stackSize = stackSize;
            //_baseAddress = baseAddress;
            _growsDownward = growsDownward;
            _baseAddress = _growsDownward ? baseAddress - _stackSize : baseAddress;
            _memorySpace = backingMemSpace;

            //reset the stack
            Reset();
        }

        public void Reset()
        {
            _stackPointer = _growsDownward ? (int)_stackSize + 1 : -1;
        }

        public void Push(byte value)
        {
            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(0, this, CpuObjects.MemoryOperation.Write, value);
            }

            if (GrowsDownward)
            {
                --_stackPointer;
            }
            else
            {
                ++_stackPointer;
            }

            if (Math.Abs(_stackPointer) > _stackSize)
            {
                _stackPointer = 0;
            }
            else if (_stackPointer < 0)
            {
                _stackPointer = (int)_stackSize;
            }

            _memorySpace.Write(StackAbsolutePointer, value);
        }

        public byte Pop()
        {
            byte retValue = _memorySpace.Read(StackAbsolutePointer);

            //handle memory check for sprite ram
            if (CpuObjects.Debugger.Current.IsAttached)
            {
                CpuObjects.Debugger.Current.CheckMemory(0, this, CpuObjects.MemoryOperation.Read, retValue);
            }

            if (GrowsDownward)
            {
                ++_stackPointer;
            }
            else
            {
                --_stackPointer;
            }

            if ((_growsDownward && _stackPointer > _stackSize) ||
                (_stackPointer < 0 && !_growsDownward))
            {
                _stackPointer = _growsDownward ? 0 : (int)_stackSize;
            }

            return retValue;
        }

        public override string ToString()
        {
            return "NES Stack";
        }
    }
}
