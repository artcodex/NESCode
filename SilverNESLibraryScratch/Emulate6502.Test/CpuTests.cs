using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Emulate6502.CpuObjects;

namespace Emulate6502.Test
{
    [TestFixture]
    public class CpuTests
    {
        [Theory]
        public static void TestSimpleRun()
        {
            Cpu cpu = new Cpu();

            cpu.Run();
        }
    }
}
