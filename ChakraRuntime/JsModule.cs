﻿using ChakraRuntime.Extensions;
using System;
using System.Linq;
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

        public JsModule LoadModule(JsValue name, JsSourceContext context, JsModuleLoadHandle handle, ModuleReadyHandle ready)
        {
            JsModule module = this;
            var loader = handle.Invoke(name);
            if (loader is JsSourceCodeModuleLoader)
            {
                module = Create(this, name);
                module.SetHostInfo((JsModule _module, JsValue _specifier, out JsModule _record) =>
                {
                    _record = _module.LoadModule(_specifier, context, handle, ready);
                    return JsStatusFlags.OK;
                });
                module.SetHostInfo((JsSourceContext ctx, JsValue _specifier, out JsModule _record) =>
                {
                    _record = Invalid.LoadModule(_specifier, context, handle, ready);
                    return JsStatusFlags.OK;
                });
                module.SetHostInfo((JsModule _referencingModule, JsValue _exception) =>
                {
                    ready.Invoke(_referencingModule, _exception);
                    return JsStatusFlags.OK;
                });
                module.Parse(((JsSourceCodeModuleLoader)loader).SourceCode, context++);
                return module;
            }
            else
            {
                foreach (var item in ((JsNativeModuleLoader)loader).GetComponents())
                {
                    JsContext.Current.Global.SetProperty(item.Name.ToCamel(), JsValue.FromObject(item, item), true);
                }
                return this;
            }
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
