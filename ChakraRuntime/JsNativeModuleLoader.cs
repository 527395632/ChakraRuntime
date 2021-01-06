using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChakraRuntime
{
    public abstract class JsNativeModuleLoader : IJsModuleLoader
    {
        public JsNativeModuleLoader(JsValue name)
        {
            this.Name = name;
        }

        public JsValue Name { get; }

        public abstract JsComponent[] GetComponents();
    }
}
