using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Emulate6502.CpuObjects
{
    public enum DebuggerBreakType
    {
        Breakpoint = 0,
        AddressTrap = 1
    }

    public enum MemoryOperation
    {
        Read = 0,
        Write = 1,
        DMA = 2
    }

    public class Debugger
    {
        private static Debugger _debuggerInstance = null;

        private AutoResetEvent _debuggerEvent;
        private bool _isAttached;
        private List<uint> _breakPoints;
        private List<uint> _addressTraps;
        private bool _isRunning = false;
        private bool _isTrapsDisabled = false;
        private StringBuilder _instructionCache;
        private StringBuilder _memoryLog;
        private bool _doLogging = false;
        private Emulator.NesEmulator _emulator;

        public delegate void BreakpointFiredHandler(uint address, DebuggerBreakType type);
        public static event BreakpointFiredHandler BreakpointHit;

        //singleton object pattern
        public static Debugger Current
        {
            get
            {
                if (_debuggerInstance == null)
                {
                    _debuggerInstance = new Debugger();
                }

                return _debuggerInstance;
            }
        }

        public void Attach()
        {
            _isAttached = true;
        }

        public Emulator.NesEmulator Emulator
        {
            set
            {
                _emulator = value;
            }
        }

        public StringBuilder ExecutedInstructions
        {
            get
            {
                return _instructionCache;
            }
        }

        public StringBuilder MemoryLog
        {
            get
            {
                return _memoryLog;
            }
        }

        public bool IsRecording
        {
            get
            {
                return _doLogging;
            }
        }

        public void Detach()
        {
            _isAttached = false;
        }

        public bool IsAttached
        {
            get
            {
                return _isAttached;
            }
        }

        public List<uint> Breakpoints
        {
            get
            {
                return _breakPoints;
            }
        }

        public List<uint> AddressTraps
        {
            get
            {
                return _addressTraps;
            }
        }

        public void AddAddressTrap(uint address)
        {
            if (!_addressTraps.Contains(address))
            {
                _addressTraps.Add(address);
            }
        }

        public void RemoveAddressTrap(uint address)
        {
            _addressTraps.Remove(address);
        }

        public void Run()
        {
            _isRunning = true;

            //release any waiting step semaphores
            Step();
        }

        public void Pause()
        {
            _isRunning = false;
        }

        private string FormatAsBinary(byte value)
        {
            string result = string.Empty;
            int i = 8;

            while (i > 0)
            {
                result = (value & 0x01).ToString() + result;

                if (i == 5)
                {
                    result = " " + result;
                }

                value >>= 1;
                i--;
            }

            return result;
        }

        public void CheckMemory(uint address, object memoryObject, MemoryOperation operation, byte value)
        {
            bool isAddressTrap = false;

            //do logging if required
            if (_doLogging)
            {
                string logEntry = string.Format("{0}: Address = 0x{1}, Value = 0x{2} ({3}) [{4}]", memoryObject != null ? memoryObject.ToString() : "Mapper Null", address.ToString("X4"), value.ToString("X2"), FormatAsBinary(value), operation.ToString());
                //temporarily disable memory logging (Need a targeted feature for this)
                //_memoryLog.Append(logEntry + Environment.NewLine);
            }

            lock (_addressTraps)
            {
                isAddressTrap = _addressTraps.Contains(address);
            }

            if (isAddressTrap && !_isTrapsDisabled)
            {
                //raise event if there are listeners
                if (BreakpointHit != null)
                {
                    BreakpointHit(address, DebuggerBreakType.AddressTrap);
                }

                //in either case we switch off from running mode
                //and wait for the user to interact before 
                //moving forward.
                _isRunning = false;
                _debuggerEvent.WaitOne();
            }
        }
        
        //needed for the debugging toolset
        public void DisableMemoryTraps()
        {
            _isTrapsDisabled = true;
        }

        public void EnableMemoryTraps()
        {
            _isTrapsDisabled = false;
        }

        public void StartRecorder()
        {
            _doLogging = true;
        }

        public void StopRecorder()
        {
            _doLogging = false;
        }

        public void Check(uint address)
        {
            bool isBreakAddress = false;
         
            //handle instruction caching
            if (_emulator != null && _doLogging)
            {
                uint index = _emulator.CPU.ProgramCounter;
                string nextStatement = string.Empty;

                //dissasemble the next statement
                Dissasembler.DissasembleNextStatement(_emulator.MainMemory, ref index, out nextStatement);

                //add it to the instruction cache
                _instructionCache.Append(nextStatement + Environment.NewLine);
            }

            //make the debugger thread-safe
            lock (_breakPoints)
            {
                isBreakAddress = _breakPoints.Contains(address);
            }

            //if there is a breakpoint here 
            //and we are in running mode (i.e not step mode)
            if (!_isRunning || isBreakAddress)
            {
                //just signal we hit a breakpoint if thats the case
                //regardless whether we are currently running or not
                //so if it's a breakpoint and we have listeners then 
                //take action. This happens either for actual break
                //point or step code, so that we can let the full
                //emulator run as is and handle break points as 
                //events
                if (BreakpointHit != null)
                {
                    BreakpointHit(address, DebuggerBreakType.Breakpoint);
                }

                //in either case we switch off from running mode
                //and wait for the user to interact before 
                //moving forward.
                _isRunning = false;
                _debuggerEvent.WaitOne();
            }

            //if we don't hit the thread pause event above the code just
            //returns quickly. Whats awesome about this model is that
            //when we don't attach a debugger the cpu doesn't need
            //to step into this function at all, everything is provided
            //in the debugger attached code above.
        }

        public void AddBreakpoint(uint address)
        {
            if (!_breakPoints.Contains(address))
            {
                lock (_breakPoints)
                {
                    _breakPoints.Add(address);
                }
            }
        }

        public void RemoveBreakpoint(uint address)
        {
            lock (_breakPoints)
            {
                _breakPoints.Remove(address);
            }
        }

        public void ClearBreakpoints()
        {
            lock (_breakPoints)
            {
                _breakPoints.Clear();
            }
        }

        private Debugger()
        {
            _debuggerEvent = new AutoResetEvent(false);
            _breakPoints = new List<uint>();
            _addressTraps = new List<uint>();
            _instructionCache = new StringBuilder();
            _memoryLog = new StringBuilder();
        }

        public void Reset()
        {
            _memoryLog = new StringBuilder();
            _instructionCache = new StringBuilder();
        }

        public void ClearAllTraps()
        {
            _breakPoints.Clear();
            _addressTraps.Clear();
        }

        public void Step()
        {
            _debuggerEvent.Set();
        }
    }
}
