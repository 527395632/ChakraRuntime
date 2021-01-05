using System;
using System.Xml.Serialization;

namespace ChakraRuntime.Debug
{
    [Serializable]
    public struct Variable
    {
        public string Name;
        public string Type;
        public string ClassName;
        public string Display;
        public PropertyAttributesFlags PropertyAttributes;
        public uint Handle;
        public string Value;
    }
}
