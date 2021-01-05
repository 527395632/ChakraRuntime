using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;

namespace ChakraRuntime
{
    public struct JsValue
    {
        private readonly IntPtr p_reference;
        private JsValue(IntPtr _reference)
        {
            p_reference = _reference;
        }

        public bool IsValid => p_reference != IntPtr.Zero;
        public JsValueFlags ValueType
        {
            get
            {
                Native.ThrowIfError(Native.JsGetValueType(this, out var type));
                return type;
            }
        }
        public int StringLength
        {
            get
            {
                Native.ThrowIfError(Native.JsGetStringLength(this, out var length));
                return length;
            }
        }
        public JsValue Prototype
        {
            get
            {
                Native.ThrowIfError(Native.JsGetPrototype(this, out var prototypeReference));
                return prototypeReference;
            }
            set => Native.ThrowIfError(Native.JsSetPrototype(this, value));
        }
        public bool IsExtensionAllowed
        {
            get
            {
                Native.ThrowIfError(Native.JsGetExtensionAllowed(this, out var allowed));
                return allowed;
            }
        }
        public bool HasExternalData
        {
            get
            {
                Native.ThrowIfError(Native.JsHasExternalData(this, out var hasExternalData));
                return hasExternalData;
            }
        }
        public IntPtr ExternalData
        {
            get
            {
                Native.ThrowIfError(Native.JsGetExternalData(this, out var data));
                return data;
            }
            set => Native.ThrowIfError(Native.JsSetExternalData(this, value));
        }

        public JsValue this[string key] { get => GetIndexedProperty(key); set => SetIndexedProperty(key, this); }
        public JsValue this[int key] { get => GetIndexedProperty(key); set => SetIndexedProperty(key, this); }

        public uint AddRef()
        {
            Native.ThrowIfError(Native.JsAddRef(this, out var count));
            return count;
        }
        public uint Release()
        {
            Native.ThrowIfError(Native.JsRelease(this, out var count));
            return count;
        }
        public JsValue ConstructCall(params JsValue[] _arguments)
        {
            if (_arguments.Length > ushort.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(_arguments));

            Native.ThrowIfError(Native.JsConstructObject(this, _arguments, (ushort)_arguments.Length, out var returnReference));
            return returnReference;
        }
        public JsValue ConvertToBoolean()
        {
            Native.ThrowIfError(Native.JsConvertValueToBoolean(this, out var booleanReference));
            return booleanReference;
        }
        public JsValue ConvertToNumber()
        {
            Native.ThrowIfError(Native.JsConvertValueToNumber(this, out var numberReference));
            return numberReference;
        }
        public JsValue ConvertToString()
        {
            Native.ThrowIfError(Native.JsConvertValueToString(this, out var stringReference));
            return stringReference;
        }
        public JsValue ConvertToObject()
        {
            Native.ThrowIfError(Native.JsConvertValueToObject(this, out var objectReference));
            return objectReference;
        }
        public void PreventExtension()
        {
            Native.ThrowIfError(Native.JsPreventExtension(this));
        }
        public JsValue GetOwnPropertyDescriptor(JsPropertyId _propertyId)
        {
            Native.ThrowIfError(Native.JsGetOwnPropertyDescriptor(this, _propertyId, out var descriptorReference));
            return descriptorReference;
        }
        public JsValue GetOwnPropertyNames()
        {
            Native.ThrowIfError(Native.JsGetOwnPropertyNames(this, out var propertyNamesReference));
            return propertyNamesReference;
        }
        public bool HasProperty(JsPropertyId _propertyId)
        {
            Native.ThrowIfError(Native.JsHasProperty(this, _propertyId, out var hasProperty));
            return hasProperty;
        }
        public JsValue GetProperty(JsPropertyId _id)
        {
            Native.ThrowIfError(Native.JsGetProperty(this, _id, out var propertyReference));
            return propertyReference;
        }
        public void SetProperty(JsPropertyId _id, JsValue _value, bool _useStrictRules) =>
           Native.ThrowIfError(Native.JsSetProperty(this, _id, _value, _useStrictRules));
        public JsValue DeleteProperty(JsPropertyId _propertyId, bool _useStrictRules)
        {
            Native.ThrowIfError(Native.JsDeleteProperty(this, _propertyId, _useStrictRules, out var returnReference));
            return returnReference;
        }
        public bool DefineProperty(JsPropertyId _propertyId, JsValue _propertyDescriptor)
        {
            Native.ThrowIfError(Native.JsDefineProperty(this, _propertyId, _propertyDescriptor, out var result));
            return result;
        }
        public bool HasIndexedProperty(JsValue _index)
        {
            Native.ThrowIfError(Native.JsHasIndexedProperty(this, _index, out var hasProperty));
            return hasProperty;
        }
        public JsValue GetIndexedProperty(JsValue _index)
        {
            Native.ThrowIfError(Native.JsGetIndexedProperty(this, _index, out var propertyReference));
            return propertyReference;
        }
        public void SetIndexedProperty(JsValue _index, JsValue _value)
        {
            Native.ThrowIfError(Native.JsSetIndexedProperty(this, _index, _value));
        }
        public void DeleteIndexedProperty(JsValue _index)
        {
            Native.ThrowIfError(Native.JsDeleteIndexedProperty(this, _index));
        }
        public bool Equals(JsValue _other)
        {
            Native.ThrowIfError(Native.JsEquals(this, _other, out var equals));
            return equals;
        }
        public bool StrictEquals(JsValue _other)
        {
            Native.ThrowIfError(Native.JsStrictEquals(this, _other, out var equals));
            return equals;
        }
        public JsValue Call(params JsValue[] _arguments)
        {
            if (this.ValueType != JsValueFlags.Function)
                throw new JsException(JsStatusFlags.Fatal, "只有方法才能调用Invoke!");
            if (_arguments.Length > ushort.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(_arguments));

            Native.ThrowIfError(Native.JsCallFunction(this, _arguments, (ushort)_arguments.Length, out var returnReference));
            return returnReference;
        }
        public IEnumerable<string> GetPropertyNames()
        {
            var names = GetOwnPropertyNames();
            int len = names.GetProperty("length");
            for (var i = 0; i < len; i++)
                yield return names.GetIndexedProperty(i).ToString();
        }
        public IEnumerable<JsValue> GetArrayValues()
        {
            if (this.ValueType != JsValueFlags.Array)
                throw new InvalidOperationException("Can't enumerate non array value");
            int len = GetProperty("length");
            for (var i = 0; i < len; i++)
                yield return GetIndexedProperty(i);
        }
        public IEnumerable<KeyValuePair<string, JsValue>> GetProperties()
        {
            var _this = this;
            return from nameVal in _this.GetOwnPropertyNames().GetArrayValues()
                   let name = nameVal.ToString()
                   let val = _this.GetProperty(name)
                   select new KeyValuePair<string, JsValue>(name, val);
        }
        public string ToJString() => ToJString(this);
        public T ToJObject<T>()
        {
            return ToJObject<T>(this);
        }      
        public object ToJObject(Type type)
        {
            return ToJObject(this, type);
        }
        public T ProxyObject<T>()
        {
            return ProxyObject<T>(this);
        }
        public object ProxyObject(Type type)
        {
            return ProxyObject(this, type);
        }
        public new string ToString() => (string)this;

        public static JsValue Invalid => new JsValue(IntPtr.Zero);
        public static JsValue Undefined
        {
            get
            {
                Native.ThrowIfError(Native.JsGetUndefinedValue(out var value));
                return value;
            }
        }
        public static JsValue Null
        {
            get
            {
                Native.ThrowIfError(Native.JsGetNullValue(out var value));
                return value;
            }
        }
        public static JsValue CreateObject()
        {
            Native.ThrowIfError(Native.JsCreateObject(out var reference));
            return reference;
        }
        public static JsValue CreateExternalObject(IntPtr _data, JsObjectFinalizeHandle _finalizer)
        {
            Native.ThrowIfError(Native.JsCreateExternalObject(_data, _finalizer, out var reference));
            return reference;
        }
        public static JsValue CreateFunction(JsNativeFunction _function)
        {
            Native.ThrowIfError(Native.JsCreateFunction(_function, IntPtr.Zero, out var reference));
            return reference;
        }
        public static JsValue CreateFunction(JsNativeFunction _function, IntPtr _handleData)
        {
            Native.ThrowIfError(Native.JsCreateFunction(_function, _handleData, out var reference));
            return reference;
        }
        public static JsValue CreateArray(uint _length)
        {
            Native.ThrowIfError(Native.JsCreateArray(_length, out var reference));
            return reference;
        }
        public static JsValue CreateError(JsValue _message)
        {
            Native.ThrowIfError(Native.JsCreateError(_message, out var reference));
            return reference;
        }
        public static JsValue CreateRangeError(JsValue _message)
        {
            Native.ThrowIfError(Native.JsCreateRangeError(_message, out var reference));
            return reference;
        }
        public static JsValue CreateReferenceError(JsValue _message)
        {
            Native.ThrowIfError(Native.JsCreateReferenceError(_message, out var reference));
            return reference;
        }
        public static JsValue CreateSyntaxError(JsValue _message)
        {
            Native.ThrowIfError(Native.JsCreateSyntaxError(_message, out var reference));
            return reference;
        }
        public static JsValue CreateTypeError(JsValue _message)
        {
            Native.ThrowIfError(Native.JsCreateTypeError(_message, out var reference));
            return reference;
        }
        public static JsValue CreateUriError(JsValue _message)
        {
            Native.ThrowIfError(Native.JsCreateUriError(_message, out var reference));
            return reference;
        }
        public static T ToJsonObject<T>(JsValue value)
        {
            return (T)ProxyObject(value, typeof(T));
        }
        public static string ToJString(JsValue value)
        {
            var json = JsContext.Current.Global.GetProperty("JSON");
            return json.GetProperty("stringify").Call(json, value);
        }
        public static T ToJObject<T>(JsValue value)
        {
            return (T)ToJObject(value, typeof(T));
        }
        public static object ToJObject(JsValue value, Type type)
        {
            return new JavaScriptSerializer().Deserialize(value.ToJString(), type);
        }
        public static T ProxyObject<T>(JsValue value)
        {
            return (T)ProxyObject(value, typeof(T));
        }
        public static object ProxyObject(JsValue value, Type type)
        {
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
                case TypeCode.Empty: return null;
                case TypeCode.DateTime: return (DateTime)value;
                case TypeCode.Object: return new JsObject(value, type).GetTransparentProxy();
                default: throw new NotSupportedException("暂不支持该类型!");
            }
        }
        public static JsValue FromObject(object value) => FromObject(JsContext.ProxyHandle.Invoke(), value);
        public static JsValue FromObject(ProxyContext proxy, object value)
        {
            if (value is Type)
                return CreateFunction((_callee, _isConstructCall, _arguments, _argumentCount, _handleData) => JsContext.ProxyHandle.Invoke().OnConstructInvoke((Type)value, ((Type)value).GetConstructors(), _callee, _isConstructCall, _arguments, _argumentCount));
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
                case TypeCode.Empty: return Null;
                case TypeCode.DateTime: return (DateTime)value;
                case TypeCode.Object:
                    var obj = CreateObject();
                    var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(q => !q.IsSpecialName);
                    foreach (var method in methods.Select(q => q.Name).Distinct().ToArray())
                        obj.SetProperty(method, CreateFunction((_callee, _isConstructCall, _arguments, _argumentCount, _handleData) => proxy.OnInvoke(value, methods.Where(q => q.Name.Equals(method)).ToArray(), _callee, _isConstructCall, _arguments, _argumentCount)), true);
                    var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    foreach (var property in properties)
                    {
                        var descriptor = CreateObject();
                        descriptor.SetProperty("configurable", false, true);
                        if (property.CanRead)
                            descriptor.SetProperty("get", CreateFunction((_callee, _isConstructCall, _arguments, _argumentCount, _handleData) => proxy.OnGet(value, property, _callee, _isConstructCall, _arguments, _argumentCount)), true);
                        if (property.CanWrite)
                            descriptor.SetProperty("set", CreateFunction((_callee, _isConstructCall, _arguments, _argumentCount, _handleData) => proxy.OnSet(value, property, _callee, _isConstructCall, _arguments, _argumentCount)), true);

                        obj.DefineProperty(property.Name, descriptor);
                    }
                    return obj;
                default:
                    throw new NotSupportedException("暂不支持该类型!");
            }
        }

        public static implicit operator bool(JsValue value)
        {
            Native.ThrowIfError(Native.JsBooleanToBool(value, out var reference));
            return reference;
        }
        public static implicit operator JsValue(bool value)
        {
            Native.ThrowIfError(Native.JsBoolToBoolean(value, out var reference));
            return reference;
        }
        public static implicit operator double(JsValue value)
        {
            Native.ThrowIfError(Native.JsNumberToDouble(value, out var reference));
            return reference;
        }
        public static implicit operator JsValue(double value)
        {
            Native.ThrowIfError(Native.JsDoubleToNumber(value, out var reference));
            return reference;
        }
        public static implicit operator int(JsValue value)
        {
            Native.ThrowIfError(Native.JsNumberToInt(value, out var reference));
            return reference;
        }
        public static implicit operator JsValue(int value)
        {
            Native.ThrowIfError(Native.JsIntToNumber(value, out var reference));
            return reference;
        }
        public static implicit operator JsValue(string value)
        {
            Native.ThrowIfError(Native.JsPointerToString(value, new UIntPtr((uint)value.Length), out var reference));
            return reference;
        }
        public static implicit operator string(JsValue value)
        {
            if (value.ValueType == JsValueFlags.Undefined || value.ValueType == JsValueFlags.Undefined)
                return string.Empty;

            Native.ThrowIfError(Native.JsStringToPointer(value, out var buffer, out var length));
            return Marshal.PtrToStringUni(buffer, (int)length);
        }
        public static implicit operator JsValue(DateTime value)
        {
            var date = JsContext.Current.Global.GetProperty("Date");
            return date.ConstructCall(date, (value - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds);
        }
        public static implicit operator DateTime(JsValue value)
        {
            var milliseconds = (double)value.ConvertToNumber();

            return TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)).AddMilliseconds(milliseconds);
        }
    }

    public class ProxyContext
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