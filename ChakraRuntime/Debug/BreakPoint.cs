using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChakraRuntime.Debug
{
    public struct BreakPoint
    {
        public uint BreakpointId;
        public uint ScriptId;
        public uint Line;
        public uint Column;
        public uint SourceLength;
        public string SourceText;
    }
}
