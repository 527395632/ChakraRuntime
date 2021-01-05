using System;
using System.Runtime.InteropServices;

namespace ChakraRuntime
{
    public delegate void JsBackgroundWorkItemHandle(IntPtr handleData);
    public delegate void JsBeforeCollectHandle(IntPtr handleState);
    public delegate void JsDiagDebugEventHandle(JsDiagDebugFlags _debugEvent, JsValue _eventData, IntPtr _handleState);
    public delegate JsStatusFlags JsFetchImportedModuleFromScriptHandle(JsSourceContext referencingSourceContext, JsValue specifier, out JsModule dependentModuleRecord);
    public delegate JsStatusFlags JsFetchImportedModuleHandle(JsModule referencingModule, JsValue specifier, out JsModule dependentModuleRecord);
    public delegate void JsImportedModuleHandle(JsModule _referencingModule, JsValue _specifier, out JsModule _dependentModuleRecord);
    public delegate bool JsMemoryAllocationHandle(IntPtr _handleState, JsMemoryEventFlags _allocationEvent, UIntPtr _allocationSize);
    public delegate JsStatusFlags JsNotifyModuleReadyHandle(JsModule _referencingModule, /*ref */JsValue _exception);
    public delegate void JsObjectBeforeCollectHandle(JsValue _reference, IntPtr _handleState);
    public delegate void JsObjectFinalizeHandle(IntPtr _data);
    public delegate void JsPromiseContinuationHandle(JsValue _task, IntPtr _handleState);
    public delegate bool JsSerializedScriptLoadSourceHandle(JsSourceContext _sourceContext, out string _scriptBuffer);
    public delegate void JsSerializedScriptUnloadHandle(JsSourceContext _sourceContext);
    public delegate bool JsThreadServiceHandle(JsBackgroundWorkItemHandle _handleFunction, IntPtr _handleData);
    public delegate JsValue JsNativeFunction(JsValue _callee, [MarshalAs(UnmanagedType.U1)] bool _isConstructCall, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] JsValue[] _arguments, ushort _argumentCount, IntPtr _handleData);
    public delegate void ModuleReadyHandle(JsModule _referencingModule, JsValue _exception);
    public delegate ProxyContext ObjectProxyHandle();
    public delegate IJsModuleLoader JsModuleLoadHandle(JsValue specifier);
}