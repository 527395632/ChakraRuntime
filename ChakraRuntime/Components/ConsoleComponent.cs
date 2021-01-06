using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChakraRuntime.Components
{
    [AsName("Console")]
    public class ConsoleComponent : SdkComponent
    {
        public void Log(string text)
        {
            Console.WriteLine(text);
        }

        public override void Dispose()
        {
        }
    }
}