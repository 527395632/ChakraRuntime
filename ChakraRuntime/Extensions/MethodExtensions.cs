using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ChakraRuntime.Extensions
{
    public static class MethodExtensions
    {
        public static bool Find(this IEnumerable<MethodBase> source, JsValue[] arguments, out MethodBase method, out object[] args)
        {
            MethodBase outMethod = null;
            object[] outArgs = null;
            var findRet = source.Any(q =>
             {
                 var param = q.GetParameters();
                 if (param.Length == arguments.Length - 1)
                 {
                     var paramObjs = new object[param.Length];
                     for (int i = 0; i < paramObjs.Length; i++)
                     {
                         try
                         {
                             paramObjs[i] = arguments[i + 1].ProxyObject(param[i].ParameterType);
                         }
                         catch
                         {
                             return false;
                         }
                     }
                     outMethod = q;
                     outArgs = paramObjs;
                     return true;
                 }
                 return false;
             });
            method = outMethod;
            args = outArgs;
            return findRet;
        }
    }
}