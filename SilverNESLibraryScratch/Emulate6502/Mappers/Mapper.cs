using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emulate6502.Mappers
{
    public interface Mapper : Memory.MemoryMapper
    { 
        void Reset();
    }
}
