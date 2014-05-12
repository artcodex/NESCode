using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestDissasembler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(ofd.FileName, FileMode.Open))
                {
                    Emulate6502.Cartridge.Cartridge cart = Emulate6502.Cartridge.Cartridge.Load(s);
                    List<StringBuilder> romBanks = Emulate6502.CpuObjects.Dissasembler.Dissasemble(cart);

                    foreach (var bank in romBanks)
                    {
                        textBox1.Text += bank.ToString();
                    }
                }
            }
        }
    }
}
