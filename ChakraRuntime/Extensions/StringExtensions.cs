using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChakraRuntime.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamel(this string source)
        {
            var sb = new StringBuilder();
            var args = source.Split(' ');
            sb.Append(args[0].ToLower());

            for (int i = 1; i < args.Length; i++)
            {
                sb.Append(args[i].Substring(0, 1).ToUpper());
                sb.Append(args[i].Substring(1).ToLower());
            }
            return sb.ToString();
        }
    }
}
