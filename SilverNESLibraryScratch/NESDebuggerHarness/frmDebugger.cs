using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using Diag = System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Emulate6502;
using Emulate6502.CpuObjects;
using Emulate6502.Cartridge;
using Emulate6502.Memory;
using Emulate6502.Emulator;
using System.Text.RegularExpressions;

namespace NESDebuggerHarness
{
    public partial class frmDebugger : Form
    {
        private const uint StartAddress = 0xFFFC;
        private const uint MaxStatements = 30;
        private Cartridge _currentCart = null;
        private NesEmulator _emulator = null;
        private List<uint> _breakPoints = null;
        private Thread _execute = null;
        private AutoResetEvent _uiWait;
        private AutoResetEvent _frame;
        private uint _lastReload;
        private bool _isRunState = false;

        private uint _count = 0;
        private bool _toggled = false;
        private uint _statements = 0;

        public delegate void AddToListFunc(Line current, bool isSelected);
        public delegate void ClearListFunc();
        public delegate void ThreadSafeFunc();
        public delegate void ThreadSafeStringFunc(string txt);


        public frmDebugger()
        {
            InitializeComponent();
        }

        private void EnableBreakpointMenus()
        {
            mnuRemoveBreakPoint.Enabled = true;
            mnuAddBreakPoint.Enabled = true;
            btnMemory.Enabled = true;
            tblCPUInfo.Enabled = true;
            _isRunState = false;
            _uiWait.Set();
        }

        private void DisableBreakpointMenus()
        {
            mnuRemoveBreakPoint.Enabled = false;
            mnuAddBreakPoint.Enabled = false;
            btnMemory.Enabled = false;
            tblCPUInfo.Enabled = false;
            _isRunState = true;
            _uiWait.Set();
        }


        private void frmDebugger_Load(object sender, EventArgs e)
        {
            _emulator = new NesEmulator();
            _uiWait = new AutoResetEvent(false);
            _frame = new AutoResetEvent(false);

            //initialise the memory dropdown
            cmbMemory.SelectedIndex = 0;

            //setup the global breakpoint hit function
            Debugger.BreakpointHit += (a, t) =>
            {
                HandleBreak(t);
            };
        }

        private void WriteNotification(string notice)
        {
            lblNotification.Text = notice;
        }

        private void HandleBreak(DebuggerBreakType type)
        {
            string notice = "A {0} event has occured";
            notice = string.Format(notice, type == DebuggerBreakType.Breakpoint ? "Breakpoint" : "AddressTrap");

            if (lblNotification.InvokeRequired)
            {
                lblNotification.Invoke(new ThreadSafeStringFunc(WriteNotification), notice);
            }
            else
            {
                WriteNotification(notice);
            }

            if (lstAssembly.InvokeRequired)
            {
                lstAssembly.BeginInvoke(new ThreadSafeFunc(EnableBreakpointMenus));
            }
            else
            {
                EnableBreakpointMenus();
            }

            _statements++;

            /*if (_count >= MaxStatements / 2)
            {
                _toggled = true;
            }*/

            /*if (!_toggled)
            {
                _lastReload = _emulator.CPU.ProgramCounter;
                ReloadStatements(_lastReload);
                _toggled = true;
                //_count++;
            }
            else
            {
                ReloadStatements(_lastReload);
                _lastReload = _emulator.CPU.ProgramCounter;
            }*/

            ReloadStatements(_emulator.CPU.ProgramCounter);

            //update the registers on the correct thread
            //we do this before execute to get the initial update
            if (lstAssembly.InvokeRequired)
            {
                lstAssembly.BeginInvoke(new ThreadSafeFunc(UpdateRegisters));
            }
            else
            {
                UpdateRegisters();
            }
        }

        private void btnLoadCart_Click(object sender, EventArgs e)
        {
            LoadCart();
        }

        private void AddToList(Line current, bool isSelected)
        {
            lstAssembly.Items.Add(current);

            if (isSelected)
            {
                lstAssembly.SelectedItem = current;
            }

            _uiWait.Set();
        }

        private void ClearList()
        {
            lstAssembly.Items.Clear();
            _uiWait.Set();
        }

        private void ReloadStatements(uint address)
        {
            if (!chkUpdateUI.Checked && _isRunState)
            {
                return;
            }

            if (lstAssembly.InvokeRequired)
            {
                lstAssembly.BeginInvoke(new ClearListFunc(ClearList));
                _uiWait.WaitOne();
            }
            else
            {
                ClearList();
            }

            uint running = _emulator.CPU.ProgramCounter;
            
            for (int i = 0; i < MaxStatements; i++)
            {
                string statement = string.Empty;
                Line current = new Line { Address = address, HasBreakPoint = BreakListContains(address), IsRunning = (address == running)};

                try
                {
                    Dissasembler.DissasembleNextStatement(_emulator.MainMemory, ref address, out statement);
                }
                catch (Exception ex)
                {
                    //basically break on the statement and handle the exception
                    Debugger.Current.Pause();

                    if (txtErrorConsole.InvokeRequired)
                    {
                        txtErrorConsole.BeginInvoke(new ThreadSafeStringFunc(SetErrorConsole), ex.ToString());
                    }
                    else
                    {
                        SetErrorConsole(ex.ToString());
                    }
                }

                current.Statement = statement;

                if (lstAssembly.InvokeRequired)
                {
                    lstAssembly.BeginInvoke(new AddToListFunc(AddToList), current, current.IsRunning);
                    _uiWait.WaitOne();
                }
                else
                {
                    AddToList(current, current.IsRunning);
                }
            }


        }

        private void Execute()
        {
            
            uint _count = 0;
            bool _toggled = false;
            _lastReload = _emulator.CPU.ProgramCounter;
            _statements = 0;
            uint _currentFrame = 0;

            while (true)
            {
                try
                {

                    //cannot execute assembler while manipulating breakpoints
                    //good idea not to read breakpoint list either during this
                    _emulator.NextFrame();
                }
                catch (Exception ex)
                {
                    //basically break on the statement and handle the exception
                    Debugger.Current.Pause();

                    if (txtErrorConsole.InvokeRequired)
                    {
                        txtErrorConsole.BeginInvoke(new ThreadSafeStringFunc(SetErrorConsole), ex.ToString());
                    }
                    else
                    {
                        SetErrorConsole(ex.ToString());
                    }
                }

                //Debugger.Current.Detach();

                if (!_isRunState)
                {
                    Debugger.Current.Pause();
                    HandleBreak(DebuggerBreakType.Breakpoint);
                    _frame.WaitOne();
                }

                if (_statements >= 100)
                {
                    _statements = 0;
                    Thread.Sleep(2);
                }

                if ((_currentFrame % 1000) == 0)
                {
                    this.Invoke(new ThreadSafeStringFunc(SetFrameNo), _currentFrame.ToString("000000"));
                }

                _currentFrame++;
            }
        }

        private void SetFrameNo(string frame)
        {
            this.Text = "NES Debugger - Frame " + frame;
        }

        private void SetErrorConsole(string text)
        {
            //most well take advantage of good thread use and run the re-enable from here
            EnableBreakpointMenus();
            txtErrorConsole.Text = text;

            //if this isn't checked we need to make sure the statement/register view is up to date
            if (!chkUpdateUI.Checked)
            {
                ReloadStatements(_emulator.CPU.ProgramCounter);
                UpdateRegisters();
            }
        }

        private void UpdateRegisters()
        {
            //don't update the UI during pure run state
            //if user doesn't want this
            if (!chkUpdateUI.Checked && _isRunState)
            {
                return;
            }

            txtPC.Text = "0x" + _emulator.CPU.ProgramCounter.ToString("X4");
            txtPS.Text = "0x" + _emulator.CPU.ProcessorStatus.ToString("X2");
            txtSP.Text = "0x" + _emulator.CPU.StackPointer.ToString("X2");
            txtX.Text = "0x" + _emulator.CPU.X.ToString("X2");
            txtY.Text = "0x" + _emulator.CPU.Y.ToString("X2");
            txtACC.Text = "0x" + _emulator.CPU.Accumulator.ToString("X2");
        }

        private void EditRegisters()
        {
            //validate all values against regex

            if (!Regex.IsMatch(txtPC.Text, "0x[0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F]") ||
                !Regex.IsMatch(txtPS.Text, "0x[0-9a-fA-F][0-9a-fA-F]") ||
                !Regex.IsMatch(txtSP.Text, "0x[0-9a-fA-F][0-9a-fA-F]") ||
                !Regex.IsMatch(txtX.Text, "0x[0-9a-fA-F][0-9a-fA-F]") ||
                !Regex.IsMatch(txtY.Text, "0x[0-9a-fA-F][0-9a-fA-F]") ||
                !Regex.IsMatch(txtACC.Text, "0x[0-9a-fA-F][0-9a-fA-F]"))
            {
                MessageBox.Show("Invalid value entered for CPU register");
                return;
            }

            int pcChange = (int)ushort.Parse(txtPC.Text.Substring(2), System.Globalization.NumberStyles.HexNumber) - (int)_emulator.CPU.ProgramCounter;
            bool needsReload = pcChange != 0;

            _emulator.CPU.ProgramCounter = ushort.Parse(txtPC.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            _emulator.CPU.ProcessorStatus = byte.Parse(txtPS.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            _emulator.CPU.StackPointer = byte.Parse(txtPS.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            _emulator.CPU.X = byte.Parse(txtX.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            _emulator.CPU.Y = byte.Parse(txtY.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);
            _emulator.CPU.Accumulator = byte.Parse(txtACC.Text.Substring(2), System.Globalization.NumberStyles.HexNumber);

            if (needsReload)
            {
                //this is not accurate but we have little capability of accuracy at this point
                //we've moved forward too much.
                //ReloadStatements((uint)((int)_lastReload + pcChange));
                ReloadStatements(_emulator.CPU.ProgramCounter);
            }
        }

        private void LoadCart()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (_execute != null)
                {
                    _execute.Abort();
                    Debugger.Current.Detach();
                }

                _execute = new Thread(Execute);

                //attach the debugger
                //reset breakpoints
                _breakPoints = new List<uint>();
                Debugger.Current.Attach();

                using (Stream file = File.Open(ofd.FileName, FileMode.Open))
                {
                    lblLoadedCart.Text = ofd.FileName;
                    _emulator.LoadCartridgeFromStream(file);
                    Debugger.Current.Emulator = _emulator;
                    //just adding a simple trap for 2007 in code (avoid extra overhead for now)
                    //will need to consider other options to this
                    //Debugger.Current.AddAddressTrap(0x2007);
                    //Debugger.Current.AddAddressTrap(0xC001);
                    //Debugger.Current.AddBreakpoint(0xC053);
                    //Debugger.Current.AddBreakpoint(0xC034);
                    _execute.Start();
                    tblDebuggerCmds.Enabled = true;
                    btnMemory.Enabled = true;
                }
            }
        }

        private void mnuAddBreakPoint_Click(object sender, EventArgs e)
        {
            AddBreakpoint();
        }

        private void AddToBreakList(uint address)
        {
            lock (_breakPoints)
            {
                if (!_breakPoints.Contains(address))
                {
                    _breakPoints.Add(address);
                }
            }
        }

        private bool BreakListContains(uint address)
        {
            lock (_breakPoints)
            {
                return _breakPoints.Contains(address);
            }
        }

        private void RemoveFromBreakList(uint address)
        {
            lock (_breakPoints)
            {
                _breakPoints.Remove(address);
            }
        }

        private void AddBreakpoint()
        {
            if (lstAssembly.SelectedItem != null)
            {
                Line current = (Line)lstAssembly.SelectedItem;

                //this is for the form, will do the same for the 
                //emulator

                AddToBreakList(current.Address);

                Debugger.Current.AddBreakpoint(current.Address);

                //also modify the current line for the visual update
                current.HasBreakPoint = true;
                ReloadStatements(_emulator.CPU.ProgramCounter);
            }
        }

        private void RemoveBreakpoint()
        {
            if (lstAssembly.SelectedItem != null)
            {
                Line current = (Line)lstAssembly.SelectedItem;

                //this is for the form, will do the same for the 
                //emulator
                RemoveFromBreakList(current.Address);

                Debugger.Current.RemoveBreakpoint(current.Address);

                current.HasBreakPoint = false;
                ReloadStatements(_emulator.CPU.ProgramCounter);
            }
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            Step();
        }

        private void Step()
        {
            if (Debugger.Current.IsAttached)
            {
                _frame.Set();
                Debugger.Current.Step();
            }
        }

        private void Run()
        {
            if (Debugger.Current.IsAttached)
            {
                _frame.Set();
                DisableBreakpointMenus();
                Debugger.Current.Run();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void Pause()
        {
            if (Debugger.Current.IsAttached)
            {
                EnableBreakpointMenus();
                Debugger.Current.Pause();
                ReloadStatements(_emulator.CPU.ProgramCounter);
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void Reset()
        {
            Pause();
            txtErrorConsole.Text = string.Empty;
            _execute.Abort();
            _emulator.Reset();

            //recreate the thread less hassles
            _execute = new Thread(Execute);
            _execute.Start();
        }

        private void mnuRemoveBreakPoint_Click(object sender, EventArgs e)
        {
            RemoveBreakpoint();
        }

        private void frmDebugger_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_execute != null)
            {
                _execute.Abort();
            }
        }

        private void frmDebugger_KeyPress(object sender, KeyPressEventArgs e)
        {
            HandleKey(e.KeyChar);
        }

        private void HandleKey(char key)
        {
            switch (key)
            {
                case 's':
                    Step();
                    break;
                case 'p':
                    Pause();
                    break;
                case 'r':
                    Run();
                    break;
                case 'n':
                    NextFrame();
                    break;
                case 'x':
                    Reset();
                    break;
            }
        }

        private void lstAssembly_KeyPress(object sender, KeyPressEventArgs e)
        {
            HandleKey(e.KeyChar);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnMemory_Click(object sender, EventArgs e)
        {
            ShowMemory();
        }

        private void ShowMemory()
        {
            if (cmbMemory.SelectedIndex >= 0)
            {
                frmMemory mem = null;

                if (cmbMemory.SelectedItem.ToString() == "Main")
                {
                    mem = new frmMemory(_emulator.MainMemory);
                }
                else if (cmbMemory.SelectedItem.ToString() == "PPU")
                {
                    mem = new frmMemory(_emulator.PPU.PPUMemory);
                }

                mem.Text = "NES Memory: " + cmbMemory.SelectedItem.ToString();
                mem.Show();
            }
        }

        private void btnJumpToLine_Click(object sender, EventArgs e)
        {

        }

        private void txtState_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtState_Validated(object sender, EventArgs e)
        {

        }

        private void txtState_Leave(object sender, EventArgs e)
        {
            EditRegisters();
        }

        private void btnNextFrame_Click(object sender, EventArgs e)
        {
            NextFrame();
        }

        private void NextFrame()
        {
            if (Debugger.Current.IsAttached)
            {
                EnableBreakpointMenus();
                Debugger.Current.Run();
                _frame.Set();
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            HandleRecordLogs();
        }

        private void HandleRecordLogs()
        {
            if (Debugger.Current.IsRecording)
            {
                Debugger.Current.StopRecorder();
                btnRecord.BackColor = SystemColors.Control;
                btnRecord.Text = "&Start Recording";
                
            }
            else
            {
                Debugger.Current.StartRecorder();
                btnRecord.BackColor = Color.Red;
                btnRecord.Text = "&Stop Recording";
            }
        }

        private void btnSerialize_Click(object sender, EventArgs e)
        {
            SerializeLogs();
        }

        private void SerializeLogs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Log Text File (*.txt)|*txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string cpuFile = Path.Combine(Path.GetDirectoryName(sfd.FileName), Path.GetFileNameWithoutExtension(sfd.FileName) + "_cpu.txt");
                string memFile = Path.Combine(Path.GetDirectoryName(sfd.FileName), Path.GetFileNameWithoutExtension(sfd.FileName) + "_memory.txt");

                using (StreamWriter log = new StreamWriter(File.Open(cpuFile, FileMode.Create)))
                {
                    log.Write(Debugger.Current.ExecutedInstructions.ToString());
                }

                using (StreamWriter log = new StreamWriter(File.Open(memFile, FileMode.Create)))
                {
                    log.Write(Debugger.Current.MemoryLog.ToString());
                }

                //Diag.Process.Start("notepad", cpuFile);
                //Diag.Process.Start("notepad", memFile);
            }
        }
    }

    public class Line : INotifyPropertyChanged
    {
        private uint _address;
        private string _statement;
        private bool _hasBreakpoint;
        private bool _isRunning;

        public uint Address 
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                NotifyChanged("Address");
                NotifyChanged("LineView");
            }
        }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
                NotifyChanged("IsRunning");
                NotifyChanged("LineView");
            }
        }

        public string Statement 
        {
            get
            {
                return _statement;
            }
            set
            {
                _statement = value;
                NotifyChanged("Statement");
                NotifyChanged("LineView");
            }
        }

        public bool HasBreakPoint 
        {
            get
            {
                return _hasBreakpoint;
            }
            set
            {
                _hasBreakpoint = value;
                NotifyChanged("HasBreakPoint");
                NotifyChanged("LineView");
            }
        }

        public string LineView
        {
            get
            {
                return this.ToString();
            }
        }

        public override string ToString()
        {
            return  Statement + (HasBreakPoint ? "    (Breakpoint)" : "") + (IsRunning ? "    [Running]": "");
        }

        #region INotifyPropertyChanged Members

        private void NotifyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
