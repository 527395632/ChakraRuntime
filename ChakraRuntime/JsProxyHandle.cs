using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ChakraRuntime
{
    public class JsProxyHandle
    {
        protected internal virtual JsValue OnConstructInvoke(JsValue callee, JsValue _this, ConstructorInfo constructors, object[] args) => JsValue.FromObject(constructors.Invoke(args));
        protected internal virtual JsValue OnInvoke(JsValue callee, JsValue _this, object reference, MethodBase method, object[] args) => JsValue.FromObject(method.Invoke(reference, args));
        protected internal virtual JsValue OnGet(JsValue callee, JsValue _this, object reference, PropertyInfo property) => JsValue.FromObject(property.GetValue(reference, null));
        protected internal virtual void OnSet(JsValue callee, JsValue _this, object reference, PropertyInfo property, object value) => property.SetValue(reference, value, null);
    }
}