using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChakraRuntime.Debug
{
    public struct StackTrace
    {
        public int Index;
        public uint ScriptId;
        public int Line;
        public int Column;
        public int SourceLength;
        public string SourceText;
        public uint FunctionHandle;

        public SourceCode SourceCode => JsContext.Current.Runtime.DiagGetSource(this.ScriptId);
    }
}
