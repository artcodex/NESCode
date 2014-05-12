using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Emulate6502.Emulator
{
    public class NesEmulator
    {
        public const uint STACK_SIZE = 256;
        private CpuObjects.Cpu _cpu;
        private PPU.PPU _ppu;
        private Memory.RamMemory _ram;
        private Memory.Memory _mainMemory;
        private Memory.Stack _stack;
        private Cartridge.Cartridge _currentCartridge;
        private APU.APU _apu;
        private Input.Controllers _controllers;
        private MemoryStream _nextVideoFrame;
        private MemoryStream _nextAudioFrame;

        public CpuObjects.Cpu CPU
        {
            get
            {
                return _cpu;
            }
        }

        public PPU.PPU PPU
        {
            get
            {
                return _ppu;
            }
        }

        public Memory.Memory MainMemory
        {
            get
            {
                return _mainMemory;
            }
        }

        public Input.Controllers Controllers
        {
            get
            {
                return _controllers;
            }
        }

        public Memory.Stack Stack
        {
            get
            {
                return _stack;
            }
        }

        public Cartridge.Cartridge CurrentCartridge
        {
            get
            {
                return _currentCartridge;
            }
        }

        public void LoadCartridgeFromStream(Stream input)
        {
            //load the cart and reset the emulator for it
            _currentCartridge = Cartridge.Cartridge.Load(input, this);
            _currentCartridge.LoadMapper();

            Reset();
        }

        public void LoadCartrigde(Cartridge.Cartridge cart)
        {
            _currentCartridge = cart;
            _currentCartridge.LoadMapper();

            Reset();
        }

        //runs a single frame and returns the video/audio samples for it
        //as memory streams
        public MemoryStream[] NextFrame()
        {
            //is anything visible?
            bool isVisible = _ppu.IsVisible;

            //actually perform the frame draw
            Emulate6502.Performance.PerfMonitor.Current.Mark(Emulate6502.Performance.PerfItems.DrawFrame);
            _ppu.DrawFrame();
            Emulate6502.Performance.PerfMonitor.Current.Measure(Emulate6502.Performance.PerfItems.DrawFrame);

            if (isVisible)
            {
                //frame streams for audio and video
                MemoryStream[] frameStreams = new MemoryStream[2];

                //extract the audio and video data
                frameStreams[0] = new MemoryStream(_ppu.LastFrame);

                //for now the audio stream will be null
                frameStreams[1] = null;

                return frameStreams;
            }
            else
            {
                return null;
            }
        }

        public void Reset()
        {
            _cpu.Reset();
            _controllers.Reset();
            _ppu.Reset();

            //reset the debugger as well
            CpuObjects.Debugger.Current.Reset();
        }

        public NesEmulator()
        {
            _mainMemory = new Emulate6502.Memory.Memory();
            _cpu = new Emulate6502.CpuObjects.Cpu(this);
            _ppu = new Emulate6502.PPU.PPU(this);
            _apu = new Emulate6502.APU.APU(this);
            _ram = new Emulate6502.Memory.RamMemory(this.MainMemory);
            _stack = new Memory.Stack(STACK_SIZE, 0x01FF, true, _mainMemory);
            _controllers = new Emulate6502.Input.Controllers(this);
        }
    }
}
