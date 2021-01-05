using ChakraRuntime.Components;
using System;
using System.Text;

namespace ChakraRuntime
{
    public struct JsModule : IEquatable<JsModule>
    {
        private readonly IntPtr p_ptr;

        internal JsModule(IntPtr _ptr)
        {
            p_ptr = _ptr;
        }

        public JsValue Parse(string _src) => Parse(_src, JsSourceContext.None);
        public JsValue Parse(string _src, JsSourceContext _ctx)
        {
            var data = Encoding.UTF8.GetBytes(_src);
            Native.ThrowIfError(Native.JsParseModuleSource(this, _ctx, data, (uint)data.Length, JsParseModuleFlags.UTF8, out var exception));
            return exception;
        }
        public JsValue Eval()
        {
            Native.ThrowIfError(Native.JsModuleEvaluation(this, out var res));
            return res;
        }

        public void SetHostInfo(JsFetchImportedModuleHandle _fetch) => Native.ThrowIfError(Native.JsSetModuleHostInfo(this, _fetch));
        public void SetHostInfo(JsNotifyModuleReadyHandle _ready) => Native.ThrowIfError(Native.JsSetModuleHostInfo(this, _ready));
        public void SetHostInfo(JsFetchImportedModuleFromScriptHandle _fetch) => Native.ThrowIfError(Native.JsSetModuleHostInfo(this, _fetch));
        public JsModule LoadMoule(JsValue name, JsSourceContext context, ComponentLoaderHandle loader, SourceHandle reader, ModuleReadyHandle ready)
        {
            var module = Create(this, name);
            module.SetHostInfo((JsModule _module, JsValue _specifier, out JsModule _record) =>
            {
                var namespance = (string)_specifier;
                if (namespance.StartsWith("@"))
                {
                    foreach (var item in loader.Invoke(namespance))
                        JsContext.Current.Global.SetProperty(item.Name, item.ProxyObject(item.GetComponent()), true);
                    _record = _module;
                    return JsStatusFlags.OK;
                }
                _record = _module.LoadMoule(_specifier, context, loader, reader, ready);
                return JsStatusFlags.OK;
            });
            module.SetHostInfo((JsSourceContext ctx, JsValue _specifier, out JsModule _record) =>
            {
                _record = Invalid.LoadMoule(_specifier, context, loader, reader, ready);
                return JsStatusFlags.OK;
            });
            module.SetHostInfo((JsModule _referencingModule, JsValue _exception) =>
            {
                ready.Invoke(_referencingModule, _exception);
                return JsStatusFlags.OK;
            });
            module.Parse(reader.Invoke(name), context++);
            return module;
        }

        public bool Equals(JsModule _other) => p_ptr.Equals(_other.p_ptr);
        public override bool Equals(object _obj)
        {
            if (ReferenceEquals(null, _obj)) return false;
            return _obj is JsModule && Equals((JsModule)_obj);
        }
        public override int GetHashCode() => p_ptr.GetHashCode();

        public static JsModule Root => new JsModule(IntPtr.Zero);
        public static JsModule Invalid => new JsModule(new IntPtr(-1));
        public static bool operator ==(JsModule _left, JsModule _right) => _left.Equals(_right);
        public static bool operator !=(JsModule _left, JsModule _right) => !(_left == _right);
        public static JsModule Create(JsModule _reference, string name) => Create(_reference, name);
        public static JsModule Create(JsModule _reference, JsValue name)
        {
            if (string.IsNullOrWhiteSpace(name.ToString()))
                name = "<NULL>";
            Native.ThrowIfError(Native.JsInitializeModuleRecord(_reference, name, out var module));
            Native.ThrowIfError(Native.JsSetModuleHostInfo(module, name));
            return module;
        }
    }
}
