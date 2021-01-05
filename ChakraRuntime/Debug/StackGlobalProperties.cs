using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChakraRuntime.Debug
{
    public struct StackGlobalProperties
    {
        public int StackProperties;
        public Variable[] Properties;
        public Variable[] DebuggerOnlyProperties;
    }
}
