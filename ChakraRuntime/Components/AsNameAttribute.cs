using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChakraRuntime.Components
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public class AsNameAttribute : Attribute
    {
        public AsNameAttribute(string name) => this.Name = name;

        public string Name { get; }
    }
}
