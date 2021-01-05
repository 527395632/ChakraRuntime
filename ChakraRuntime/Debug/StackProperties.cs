using System;
using System.Xml.Serialization;

namespace ChakraRuntime.Debug
{
    public struct StackProperties
    {
        public Variable ThisObject;
        public Variable Arguments;
        public Variable[] Locals;
        public Variable Globals;
        public Scope[] Scopes;
    }
}
