using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ChakraRuntime.Components
{
    public abstract class ComponentLoader
    {
        public string Name => ((AsNameAttribute)this.GetType().GetCustomAttributes(false)[0]).Name;
        protected internal abstract object GetComponent();

        protected virtual JsValue OnNewCall(Type _reference, ConstructorInfo[] constructors, JsValue _callee, [MarshalAs(UnmanagedType.U1)] bool _isConstructCall, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] JsValue[] _arguments, ushort _argumentCount)
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
                            args[i] = _arguments[i + 1].ToObject(arguments[i].ParameterType);
                        }
                        catch (Exception)
                        {
                            args = null;
                            break;
                        }
                    }
                    if (args != null)
                        return ProxyObject(q.Invoke(args));
                }
                return JsValue.Invalid;
            }).FirstOrDefault(q => q.IsValid);
        }
        protected virtual JsValue OnCall(object _reference, MethodBase[] methods, JsValue _callee, [MarshalAs(UnmanagedType.U1)] bool _isConstructCall, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] JsValue[] _arguments, ushort _argumentCount)
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
                            args[i] = _arguments[i + 1].ToObject(arguments[i].ParameterType);
                        }
                        catch (Exception)
                        {
                            args = null;
                            break;
                        }
                    }
                    if (args != null)
                        return ProxyObject(q.Invoke(_reference, args));
                }
                return JsValue.Invalid;
            }).FirstOrDefault(q => q.IsValid);
        }
        protected virtual JsValue OnGetCall(object _reference, PropertyInfo property, JsValue _callee, [MarshalAs(UnmanagedType.U1)] bool _isConstructCall, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] JsValue[] _arguments, ushort _argumentCount)
        {
            if (_isConstructCall)
                throw new JsScriptException(JsStatusFlags.ScriptException, "方法或属性不能使用实例化关键字 'new'");
            return ProxyObject(property.GetValue(_reference));
        }
        protected virtual JsValue OnSetCall(object _reference, PropertyInfo property, JsValue _callee, [MarshalAs(UnmanagedType.U1)] bool _isConstructCall, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] JsValue[] _arguments, ushort _argumentCount)
        {
            if (_isConstructCall)
                throw new JsScriptException(JsStatusFlags.ScriptException, "方法或属性不能使用实例化关键字 'new'");
            property.SetValue(_reference, _arguments[1].ToObject(property.PropertyType));
            return JsValue.Undefined;
        }
        protected virtual internal JsValue ProxyObject(object value)
        {
            if (value is Type)
                return JsValue.CreateFunction((_callee, _isConstructCall, _arguments, _argumentCount, _handleData) => OnNewCall((Type)value, ((Type)value).GetConstructors(), _callee, _isConstructCall, _arguments, _argumentCount));

            var type = value.GetType();
            if (type.IsEnum) return value.ToString();

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean: return (bool)value;
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal: return (double)value;
                case TypeCode.Char:
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64: return (int)value;
                case TypeCode.String: return (string)value;
                case TypeCode.DBNull:
                case TypeCode.Empty: return JsValue.Null;
                case TypeCode.DateTime:
                case TypeCode.Object:
                    var proxy = JsValue.CreateObject();
                    var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(q => !q.IsSpecialName);
                    foreach (var method in methods.Select(q => q.Name).Distinct().ToArray())
                        proxy.SetProperty(method, JsValue.CreateFunction((_callee, _isConstructCall, _arguments, _argumentCount, _handleData) => OnCall(value, methods.Where(q => q.Name.Equals(method)).ToArray(), _callee, _isConstructCall, _arguments, _argumentCount)), true);
                    var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    foreach (var property in properties)
                    {
                        var descriptor = JsValue.CreateObject();
                        descriptor.SetProperty("configurable", false, true);
                        if (property.CanRead)
                            descriptor.SetProperty("get", JsValue.CreateFunction((_callee, _isConstructCall, _arguments, _argumentCount, _handleData) => OnGetCall(value, property, _callee, _isConstructCall, _arguments, _argumentCount)), true);
                        if (property.CanWrite)
                            descriptor.SetProperty("set", JsValue.CreateFunction((_callee, _isConstructCall, _arguments, _argumentCount, _handleData) => OnSetCall(value, property, _callee, _isConstructCall, _arguments, _argumentCount)), true);

                        proxy.DefineProperty(property.Name, descriptor);
                    }
                    return proxy;
                default:
                    throw new NotSupportedException("暂不支持该类型!");
            }
        }
    }
}