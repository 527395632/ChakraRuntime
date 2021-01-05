using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChakraRuntime
{
    public class ObjectProxy
    {
        protected internal virtual JsValue OnConstructInvoke(Type _reference, ConstructorInfo[] constructors, JsValue _callee, [MarshalAs(UnmanagedType.U1)] bool _isConstructCall, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] JsValue[] _arguments, ushort _argumentCount)
        {
            if (!_isConstructCall)
                throw new JsScriptException(JsStatusFlags.ScriptException, "此对象需要初始化后再使用!");

            return constructors.Select(q =>
            {
                var arguments = q.GetParameters();
                if (arguments.Length == _argumentCount - 1)
                {
                    var args = new object[arguments.Length];
                    for (int i = 0; i < args.Length; i++)
                    {
                        try
                        {
                            args[i] = _arguments[i + 1].ChangeObject(arguments[i].ParameterType);
                        }
                        catch (Exception)
                        {
                            args = null;
                            break;
                        }
                    }
                    if (args != null)
                        return JsValue.FromObject(q.Invoke(args));
                }
                return JsValue.Invalid;
            }).FirstOrDefault(q => q.IsValid);
        }
        protected internal virtual JsValue OnInvoke(object _reference, MethodBase[] methods, JsValue _callee, [MarshalAs(UnmanagedType.U1)] bool _isConstructCall, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] JsValue[] _arguments, ushort _argumentCount)
        {
            if (_isConstructCall)
                throw new JsScriptException(JsStatusFlags.ScriptException, "方法或属性不能使用实例化关键字 'new'");

            return methods.Select(q =>
            {
                var arguments = q.GetParameters();
                if (arguments.Length == _argumentCount - 1)
                {
                    var args = new object[arguments.Length];
                    for (int i = 0; i < args.Length; i++)
                    {
                        try
                        {
                            args[i] = _arguments[i + 1].ChangeObject(arguments[i].ParameterType);
                        }
                        catch (Exception)
                        {
                            args = null;
                            break;
                        }
                    }
                    if (args != null)
                        return JsValue.FromObject(q.Invoke(_reference, args));
                }
                return JsValue.Invalid;
            }).FirstOrDefault(q => q.IsValid);
        }
        protected internal virtual JsValue OnGet(object _reference, PropertyInfo property, JsValue _callee, [MarshalAs(UnmanagedType.U1)] bool _isConstructCall, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] JsValue[] _arguments, ushort _argumentCount)
        {
            if (_isConstructCall)
                throw new JsScriptException(JsStatusFlags.ScriptException, "方法或属性不能使用实例化关键字 'new'");
            return JsValue.FromObject(property.GetValue(_reference));
        }
        protected internal virtual JsValue OnSet(object _reference, PropertyInfo property, JsValue _callee, [MarshalAs(UnmanagedType.U1)] bool _isConstructCall, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] JsValue[] _arguments, ushort _argumentCount)
        {
            if (_isConstructCall)
                throw new JsScriptException(JsStatusFlags.ScriptException, "方法或属性不能使用实例化关键字 'new'");
            property.SetValue(_reference, _arguments[1].ChangeObject(property.PropertyType));
            return JsValue.Undefined;
        }
    }
}
