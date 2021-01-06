using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChakraRuntime
{
    [AttributeUsage(AttributeTargets.Class |
        AttributeTargets.Field |
        AttributeTargets.Property |
        AttributeTargets.Method |
        AttributeTargets.Constructor |
        AttributeTargets.Delegate,
        AllowMultiple = true,
        Inherited = true)]
    public class AsNameAttribute : Attribute
    {
        public AsNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

}
