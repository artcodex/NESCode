using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Emulate6502.Cartridge;
using Emulate6502.Emulator;
using Emulate6502.CpuObjects;
using System.Threading;

namespace TestPPUFrameDraw
{
    public partial class Form1 : Form
    {
        private Emulate6502.Emulator.NesEmulator _nesEmulate;
        private MemoryStream _currentFrame;
        private AutoResetEvent ev;
        private MemoryStream last = null;

        private double _count;
        private long _periods;

        public Form1()
        {
            InitializeComponent();
            _nesEmulate = new NesEmulator();
            ev = new AutoResetEvent(false);
            Emulate6502.Performance.PerfMonitor.Current.StartMonitor();
            _nesEmulate.CPU.CaptureOpCodes = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(ofd.FileName, FileMode.Open))
                {
                    _nesEmulate.LoadCartridgeFromStream(s);
                }
            }
        }

        private void NextFrame()
        {
            int i = 0;
            DateTime n1 = DateTime.Now;
            DateTime n2 = DateTime.Now;
            TimeSpan s;

            while (true)
            {
               // for (int i = 0; i < 50; i++)
                //{
                    MemoryStream[] n = null;
                    Emulate6502.Performance.PerfMonitor.Current.Mark(Emulate6502.Performance.PerfItems.EmulateFrame);
                    n = _nesEmulate.NextFrame();
                    Emulate6502.Performance.PerfMonitor.Current.Measure(Emulate6502.Performance.PerfItems.EmulateFrame);
                    _count += 1;
                    
                    s = DateTime.Now.Subtract(n1);
                        
                    
                    if (s.TotalSeconds >= 1.0)
                    {
                        _periods++;
                        n1 = DateTime.Now;
                        label1.Invoke(new ThreadStart(FrameCrank));
                    }

                    s = DateTime.Now.Subtract(n2);

                    if (s.TotalMilliseconds >= (1000.0 / 10.0))
                    {
                        n2 = DateTime.Now;
                        panel1.Invoke(new ThreadStart(Invalid));
                    }

                    if (n != null)
                    {
                        last = n[0];
                    }
                    i++;
                //}
                //ev.WaitOne();
                    
                    /*if (i >= 10)
                    {
                        panel1.Invoke(new ThreadStart(Invalid));
                        i = 0;
                    }*/
                    //Thread.Sleep(100);
            }
        }

        private void FrameCrank()
        {
            label1.Text = Math.Round(_count / _periods).ToString();
        }

        private void Invalid()
        {
            panel1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_nesEmulate != null)
            {
                panel1.Paint += new PaintEventHandler(panel1_Paint);
                //Debugger.Current.Attach();
                //Debugger.Current.AddBreakpoint(0xC330);
                Thread n = new Thread(NextFrame);
                n.Start();
            }
        }

        void panel1_Paint(object sender, PaintEventArgs e)
        {
            Bitmap simple = new Bitmap(256, 240);

            if (last != null)
            {
                byte[] bytes = last.ToArray();
                for (int x = 0; x < 256; x++)
                {
                    for (int y = 0; y < 240; y++)
                    {
                        uint s = (uint)(((y * 256) + x) * 4);
                        Color c = Color.FromArgb(bytes[s], bytes[s + 1], bytes[s + 2]);
                        //Color c = Color.FromArgb(255, 255, 255);
                        simple.SetPixel(x, y, c);
                    }
                }

                e.Graphics.DrawImage(simple, 0, 0);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Debugger.Current.Step();
            panel1.Invalidate();
            ev.Set();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            HandleKeyDown(e.KeyCode);
        }

        private void HandleKeyDown(Keys code)
        {
            switch (code)
            {
                case Keys.S:
                    _nesEmulate.Controllers.PressControllerKey(Emulate6502.Input.NESControllers.Joypad0, Emulate6502.Input.NESButtons.Start);
                    break;
                case Keys.Left:
                    _nesEmulate.Controllers.PressControllerKey(Emulate6502.Input.NESControllers.Joypad0, Emulate6502.Input.NESButtons.Left);
                    break;
                case Keys.Right:
                    _nesEmulate.Controllers.PressControllerKey(Emulate6502.Input.NESControllers.Joypad0, Emulate6502.Input.NESButtons.Right);
                    break;
            }
        }


        private void HandleKeyUp(Keys code)
        {
            switch (code)
            {
                case Keys.S:
                    _nesEmulate.Controllers.ReleaseControllerKey(Emulate6502.Input.NESControllers.Joypad0, Emulate6502.Input.NESButtons.Start);
                    break;
                case Keys.Left:
                    _nesEmulate.Controllers.ReleaseControllerKey(Emulate6502.Input.NESControllers.Joypad0, Emulate6502.Input.NESButtons.Left);
                    break;
                case Keys.Right:
                    _nesEmulate.Controllers.ReleaseControllerKey(Emulate6502.Input.NESControllers.Joypad0, Emulate6502.Input.NESButtons.Right);
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            HandleKeyUp(e.KeyCode);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Log (*.txt)|*.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(File.Open(sfd.FileName, FileMode.Create)))
                {
                    int i = 0;
                    string[] names = Enum.GetNames(typeof(Emulate6502.Performance.PerfItems));
                    foreach (var pitem in Enum.GetValues(typeof(Emulate6502.Performance.PerfItems)))
                    {
                        double current = Math.Round(Emulate6502.Performance.PerfMonitor.Current[(Emulate6502.Performance.PerfItems)pitem], 5);
                        sw.WriteLine(string.Format("{0} = {1}", names[i++], current.ToString()));
                    }
                }

                //System.Diagnostics.Process.Start("notepad.exe", sfd.FileName);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Log (*.txt)|*.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(File.Open(sfd.FileName, FileMode.Create)))
                {
                    foreach (var key in _nesEmulate.CPU.ExecutedOpCodes.Keys)
                    {
                        sw.WriteLine(string.Format("{0}: {1}", key.ToString(), _nesEmulate.CPU.ExecutedOpCodes[key]));
                    }
                }

                System.Diagnostics.Process.Start("notepad.exe", sfd.FileName);
            }
        }
    }
}
