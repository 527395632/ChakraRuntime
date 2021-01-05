using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChakraRuntime.Debug
{
    public enum PropertyAttributesFlags
    {
        NONE = 0x1,
        HAVE_CHILDRENS = 0x2,
        READ_ONLY_VALUE = 0x4,
        IN_TDZ = 0x8,
    }
}
