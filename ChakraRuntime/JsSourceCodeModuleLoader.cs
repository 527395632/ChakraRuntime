using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChakraRuntime
{
    public class JsSourceCodeModuleLoader : IJsModuleLoader
    {
        public JsSourceCodeModuleLoader(JsValue name, string sourceCode)
        {
            this.Name = name;
            SourceCode = sourceCode;
        }

        public JsValue Name { get; }

        protected internal string SourceCode { get; }
    }
}
