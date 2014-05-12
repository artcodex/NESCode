using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emulate6502.CpuObjects;

namespace Emulate6502.CpuObjectTests
{
    //Test framework is really basic at the moment
    //and will stay this way for the forseable future
    internal class MemoryTests
    {
        private Memory.Memory _memoryObject;

        public void Init()
        {
            _memoryObject = new Memory.Memory();
        }

        public void Test1()
        {
            uint add1 = 0x00FF;
            uint add2 = 0xFF00;

            _memoryObject.Write(add1, 34);
            _memoryObject.Write(add2, 18);

            bool check1 = _memoryObject.Read(add1) == 34;
            bool check2 = _memoryObject.Read(add2) == 18;

            if (!(check1 && check2))
            {
                throw new Exception("Memoy object tests failed");
            }
        }
    }
}
