using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Emulate6502.Memory;
using Emulate6502.CpuObjects;

namespace NESDebuggerHarness
{
    public partial class frmMemory : Form
    {
        private MemoryMapper _memMap;
        private uint _currentAddress = 0;
        private object _currentBox = null;

        public frmMemory(MemoryMapper mapper)
        {
            _memMap = mapper;
            InitializeComponent();
            InitializeMemGrid();
        }

        private void InitializeMemGrid()
        {
            for (int k = 1; k < 17; k++)
            {
                Label lblColHead = new Label();
                lblColHead.Text = "0x" + (k-1).ToString("X1");
                lblColHead.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                tblMemory.Controls.Add(lblColHead, k, 0);
            }

            for (int k = 1; k < 9; k++)
            {
                Label lblRowHead = new Label();
                lblRowHead.Tag = ((k - 1) * 0x10);
                lblRowHead.Text = "0x" + ((k-1) * 0x10).ToString("X4");
                lblRowHead.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                tblMemory.Controls.Add(lblRowHead, 0, k);
            }

            for (int i = 1; i < 17; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    TextBox memCell = new TextBox();
                    memCell.ContextMenuStrip = ctxAddressTraps;
                    memCell.Tag = ((j - 1) * 16) + (i-1);
                    memCell.Leave += new EventHandler(memCell_Leave);
                    memCell.Enter += new EventHandler(memCell_Enter);
                    tblMemory.Controls.Add(memCell, i, j);
                }
            }

            txtAddress.Text = _currentAddress.ToString("0000");
            SetView();
        }

        void memCell_Enter(object sender, EventArgs e)
        {
            _currentBox = sender;
        }

        void memCell_Leave(object sender, EventArgs e)
        {
            /*if (sender is TextBox)
            {
                TextBox memory = (TextBox)sender;

                if (memory.Tag != null)
                {
                    if (!Regex.IsMatch(memory.Text, "0x[0-9A-Fa-f][0-9A-Fa-f]"))
                    {
                        lblError.Text = "Error: Invalid byte value for memory: " + memory.Text;
                        SetView();
                    }
                    else
                    {
                        lblError.Text = string.Empty;
                        uint offset = Convert.ToUInt32(memory.Tag);
                        
                        //disabling write till I figure out a better plan
                        
                        //disable traps during write/view
                        Debugger.Current.DisableMemoryTraps();
                        _memMap.Write(_currentAddress + offset, byte.Parse(memory.Text.Substring(2), System.Globalization.NumberStyles.HexNumber));
                        SetView();
                        Debugger.Current.EnableMemoryTraps();
                    }
                }
            }*/
        }

        private void SetView()
        {
            Debugger.Current.DisableMemoryTraps();
            foreach (var cell in tblMemory.Controls)
            {
                if (cell is TextBox)
                {
                    TextBox memCell = (TextBox)cell;
                    uint offset = Convert.ToUInt32(memCell.Tag);

                    if (Debugger.Current.AddressTraps.Contains(_currentAddress + offset))
                    {
                        memCell.BackColor = Color.Red;
                    }
                    else
                    {
                        memCell.BackColor = Color.White;
                    }

                    try
                    {
                        memCell.Text = "0x" + _memMap.Read(_currentAddress + offset).ToString("X2");
                    }
                    catch
                    {
                        memCell.Text = "#Err";
                    }
                }
                else if (cell is Label)
                {
                    Label lblHead = (Label)cell;

                    if (lblHead.Tag != null)
                    {
                        int baseValue = Convert.ToInt32(lblHead.Tag);
                        lblHead.Text = "0x" + (_currentAddress + baseValue).ToString("X4");
                    }
                }
            }
            Debugger.Current.EnableMemoryTraps();
        }

        private int GetDecimal(string hex)
        {
            int pos = hex.Length-1;
            int result = 0;

            foreach (var c in hex)
            {
                if (c >= '0' && c <= '9')
                    result += ((int)c - 48) * (int)Math.Pow(16, pos);
                else
                {
                    result += ((c - 'A') + 10) * (int)Math.Pow(16, pos);
                }
                pos--;
            }

            return result;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(txtAddress.Text, "[0-9a-fA-F]+"))
            {
                MessageBox.Show("Invalid memory address");
                return;
            }
            
            _currentAddress = (uint)GetDecimal(txtAddress.Text);
            SetView();
        }

        private void txtAddress_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void trapAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrapAddress(_currentBox);
        }

        private void TrapAddress(object sender)
        {
            if (sender != null && sender is TextBox)
            {
                uint address = _currentAddress;
                TextBox memCell = (TextBox)sender;
                address += Convert.ToUInt32(memCell.Tag);
                memCell.BackColor = Color.Red;

                Debugger.Current.AddAddressTrap(address);
            }
        }

        private void RemoveAddressTrap(object sender)
        {
            if (sender != null && sender is TextBox)
            {
                uint address = _currentAddress;
                TextBox memCell = (TextBox)sender;
                address += Convert.ToUInt32(memCell.Tag);
                memCell.BackColor = Color.White;

                Debugger.Current.RemoveAddressTrap(address);
            }
        }

        private void removeTrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAddressTrap(_currentBox);
        }
    }
}
