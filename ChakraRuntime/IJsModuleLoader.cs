using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChakraRuntime
{
    public interface IJsModuleLoader
    {
        JsValue Name { get; }
    }
}
