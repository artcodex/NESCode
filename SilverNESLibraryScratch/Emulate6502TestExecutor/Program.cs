using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emulate6502.CpuObjectTests;

namespace Emulate6502TestExecutor
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //TimeSpan t = TimeSpan.FromMilliseconds(1.0 / 1790000);
            //TestRunner.ExecuteTests();
            CPUTests tests = new CPUTests();
            tests.RunTests();
        }
    }
}
