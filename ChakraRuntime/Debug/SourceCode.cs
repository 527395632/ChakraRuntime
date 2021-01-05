using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChakraRuntime.Debug
{
    public struct SourceCode
    {
        public string FileName;
        public int LineCount;
        public int SourceLength;
        public uint ScriptId;
        public string Source;
    }
}
