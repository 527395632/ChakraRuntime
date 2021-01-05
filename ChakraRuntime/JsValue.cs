using System;
using System.Collections.Generic;
using System.Linq;
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
        public JsValue NewObject(params JsValue[] _arguments)
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
        public T ToObject<T>() => (T)ToObject(this, typeof(T));
        public object ToObject(Type type) => ToObject(this, type);
        public string ToJsonString() => ToJsonString(this);
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
            return (T)ToObject(value, typeof(T));
        }
        public static string ToJsonString(JsValue value)
        {
            var json = JsContext.Current.Global.GetProperty("JSON");
            return json.GetProperty("stringify").Call(json, value);
        }
        public static object ToObject<T>(JsValue value)
        {
            return ToObject(value, typeof(T));
        }
        public static object ToObject(JsValue value, Type type)
        {
            if (type.IsEnum && value.ValueType == JsValueFlags.String)
            {
                return Enum.Parse(type, value);
            }
            else
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
                    case TypeCode.Object: return new JavaScriptSerializer().Deserialize(ToJsonString(value), type);
                    default:
                        throw new NotSupportedException("暂不支持该类型!");
                }
            }
        }
        public static JsValue FromObject(object obj)
        {
            if (obj == null)
                return Null;
            switch (Type.GetTypeCode(obj.GetType()))
            {
                case TypeCode.Boolean: return (bool)obj;
                case TypeCode.Char: return (char)obj;
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal: return (double)obj;
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64: return (int)obj;
                case TypeCode.String: return (string)obj;
                case TypeCode.DBNull:
                case TypeCode.Empty: return Null;
                case TypeCode.DateTime: return (DateTime)obj;
                case TypeCode.Object:
                    var json = JsContext.Current.Global.GetProperty("JSON");
                    return json.GetProperty("parse").Call(json, new JavaScriptSerializer().Serialize(obj));
                default:
                    return Undefined;
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
            return value;
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
            return date.NewObject(date, (value - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalMilliseconds);
        }
        public static implicit operator DateTime(JsValue value)
        {
            var milliseconds = (double)value.ConvertToNumber();

            return TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)).AddMilliseconds(milliseconds);
        }
    }
}