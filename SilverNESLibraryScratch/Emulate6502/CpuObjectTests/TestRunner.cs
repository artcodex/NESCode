using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Emulate6502.CpuObjectTests
{
    public static class TestRunner
    {
        public static void ExecuteTests()
        {
            MemoryTests memtests = new MemoryTests();

            memtests.Init();
            memtests.Test1();

            StackTests stacktests = new StackTests();

            stacktests.Init();
            stacktests.Test1();

            CartridgeTests.CartridgeTests carttests = new Emulate6502.CartridgeTests.CartridgeTests();

            carttests.Init();

            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (var stream = File.Open(ofd.FileName, FileMode.Open))
                {
                    carttests.TestLoadCartridge(stream);
                }
            }
        }
    }
}
