using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emulate6502.Memory;

namespace Emulate6502.CpuObjectTests
{
    internal class StackTests
    {
        private Stack _stack;
        private Memory.Memory _backingStore;

        public void Init()
        {
            _backingStore = new Memory.Memory();
            _stack = new Stack(0x00FF, 0x01F0, true, _backingStore);
        }

        public void Test1()
        {
            _stack.Push(25);
            _stack.Push(13);
            _stack.Push(10);

            bool c1 = _stack.Pop() == 10;
            bool c2 = _stack.Pop() == 13;
            bool c3 = _stack.Pop() == 25;
            bool c5 = _backingStore.Read(0x01F0) == 25;

            if (!(c1 && c2 && c3 && c5))
            {
                throw new Exception("Error pushing/popping from stack or reading stack memory");
            }
        }
    }
}
