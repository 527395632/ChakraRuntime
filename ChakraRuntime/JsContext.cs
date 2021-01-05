using System;
using System.Threading;

namespace ChakraRuntime
{
    public struct JsContext : IDisposable
    {
        private readonly IntPtr p_reference;

        private JsContext(IntPtr reference)
        {
            p_reference = reference;
        }

        public bool IsValid => p_reference != IntPtr.Zero;
        public JsRuntime Runtime
        {
            get
            {
                JsRuntime handle;
                Native.ThrowIfError(Native.JsGetRuntime(this, out handle));
                return handle;
            }
        }

        public JsValue LoadMoule(string moduleName, string moduleFileName, JsModuleLoadHandle loader)
        {
            Global.SetProperty("__APP__", JsValue.CreateObject(), true);
            var wait = new EventWaitHandle(false, EventResetMode.ManualReset);
            JsModule.Root.LoadModule("app.boot", new JsSourceContext(), namespance =>
            {
                return namespance.Equals("app.boot") ? 
                new JsModuleLoader(namespance, $"import {{ { moduleName } }} from '{ moduleFileName }'; __APP__ = new { moduleName }();") :
                loader(namespance);
            }, (module, exception) => wait.Set()).Eval();
            wait.WaitOne();
            var value = Global.GetProperty("__APP__");
            Global.DeleteProperty("__APP__", true);
            return value;
        }
        public JsValue ParseScript(string _script, JsSourceContext _sourceContext, string _sourceName)
        {
            JsValue result;
            Native.ThrowIfError(Native.JsParseScript(_script, _sourceContext, _sourceName, out result));
            return result;
        }
        public JsValue ParseScript(string _script, byte[] _buffer, JsSourceContext _sourceContext, string _sourceName)
        {
            Native.ThrowIfError(Native.JsParseSerializedScript(_script, _buffer, _sourceContext, _sourceName, out var result));
            return result;
        }
        public JsValue ParseScript(string _script) => ParseScript(_script, JsSourceContext.None, string.Empty);
        public JsValue ParseScript(string _script, byte[] _buffer) => ParseScript(_script, _buffer, JsSourceContext.None, string.Empty);
        public JsValue RunScript(string _script, JsSourceContext _sourceContext, string _sourceName)
        {
            Native.ThrowIfError(Native.JsRunScript(_script, _sourceContext, _sourceName, out var result));
            return result;
        }
        public JsValue RunScript(string _script, byte[] _buffer, JsSourceContext _sourceContext, string _sourceName)
        {
            Native.ThrowIfError(Native.JsRunSerializedScript(_script, _buffer, _sourceContext, _sourceName, out var result));
            return result;
        }
        public JsValue RunScript(string _script) => RunScript(_script, JsSourceContext.None, string.Empty);
        public JsValue RunScript(string _script, byte[] _buffer) => RunScript(_script, _buffer, JsSourceContext.None, string.Empty);
        public ulong SerializeScript(string _script, byte[] _buffer)
        {
            var bufferSize = (ulong)_buffer.Length;
            Native.ThrowIfError(Native.JsSerializeScript(_script, _buffer, ref bufferSize));
            return bufferSize;
        }
        public JsValue GetAndClearException()
        {
            JsValue reference;
            Native.ThrowIfError(Native.JsGetAndClearException(out reference));
            return reference;
        }
        public void SetException(JsValue _exception) => Native.ThrowIfError(Native.JsSetException(_exception));
        public JsValue Global
        {
            get
            {
                Native.ThrowIfError(Native.JsGetGlobalObject(out var value));
                return value;
            }
        }
        public bool HasException
        {
            get
            {
                bool hasException;
                Native.ThrowIfError(Native.JsHasException(out hasException));
                return hasException;
            }
        }
        public uint Idle()
        {
            uint ticks;
            Native.ThrowIfError(Native.JsIdle(out ticks));
            return ticks;
        }
        public uint AddRef()
        {
            Native.ThrowIfError(Native.JsContextAddRef(this, out var count));
            return count;
        }
        public uint Release()
        {
            Native.ThrowIfError(Native.JsContextRelease(this, out var count));
            return count;
        }
        public void Dispose()
        {
            if (Current.IsValid)
                Current = Invalid;
        }

        public static ObjectProxyHandle ProxyHandle { get; set; } = new ObjectProxyHandle(() => new ObjectProxy());
        public static JsContext Invalid => new JsContext(IntPtr.Zero);
        public static JsContext Current
        {
            get
            {
                Native.ThrowIfError(Native.JsGetCurrentContext(out var reference));
                return reference;
            }
            internal set
            {
                Native.ThrowIfError(Native.JsSetCurrentContext(value));
            }
        }
    }
}