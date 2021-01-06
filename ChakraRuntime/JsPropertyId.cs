using System;
using System.Text;

namespace ChakraRuntime
{
    public struct JsPropertyId : IEquatable<JsPropertyId>
    {
        private readonly IntPtr p_id;

        internal JsPropertyId(IntPtr _id)
        {
            p_id = _id;
        }

        public string Name
        {
            get
            {
                Native.ThrowIfError(Native.JsGetPropertyNameFromId(this, out var name));
                return name;
            }
        }

        public bool Equals(JsPropertyId _other) => p_id == _other.p_id;
        public override bool Equals(object _obj)
        {
            if (ReferenceEquals(null, _obj))
                return false;

            return _obj is JsPropertyId && Equals((JsPropertyId)_obj);
        }
        public override int GetHashCode() => p_id.ToInt32();
        public override string ToString() => Name;

        public static JsPropertyId Invalid => new JsPropertyId(IntPtr.Zero);

        public static implicit operator JsPropertyId(string value)
        {
            Native.ThrowIfError(Native.JsGetPropertyIdFromName(value, out var id));
            return id;
        }
        public static implicit operator string(JsPropertyId value)
        {
            Native.ThrowIfError(Native.JsGetPropertyNameFromId(value, out var id));
            return (string)id;
        }
        public static implicit operator JsPropertyId(JsValue sysbom)
        {
            Native.ThrowIfError(Native.JsGetPropertyIdFromSymbol(sysbom, out var id));
            return id;
        }
        public static implicit operator JsValue(JsPropertyId value)
        {
            Native.ThrowIfError(Native.JsGetSymbolFromPropertyId(value, out var id));
            return id;
        }

        public static bool operator ==(JsPropertyId _left, JsPropertyId _right) => _left.Equals(_right);
        public static bool operator !=(JsPropertyId _left, JsPropertyId _right) => !_left.Equals(_right);
    }
}