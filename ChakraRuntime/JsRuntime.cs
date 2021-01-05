using System;
using System.Linq;
using ChakraRuntime.Debug;

namespace ChakraRuntime
{
    public struct JsRuntime : IDisposable
    {
        private IntPtr p_handle;
        public bool IsValid => p_handle != IntPtr.Zero;

        public ulong MemoryUsage
        {
            get
            {
                Native.ThrowIfError(Native.JsGetRuntimeMemoryUsage(this, out var memoryUsage));
                return memoryUsage.ToUInt64();
            }
        }

        public ulong MemoryLimit
        {
            get
            {
                Native.ThrowIfError(Native.JsGetRuntimeMemoryLimit(this, out var memoryLimit));
                return memoryLimit.ToUInt64();
            }
            set => Native.ThrowIfError(Native.JsSetRuntimeMemoryLimit(this, new UIntPtr(value)));
        }

        public bool Disabled
        {
            get
            {
                Native.ThrowIfError(Native.JsIsRuntimeExecutionDisabled(this, out var isDisabled));
                return isDisabled;
            }
            set => Native.ThrowIfError(value ? Native.JsDisableRuntimeExecution(this) : Native.JsEnableRuntimeExecution(this));
        }

        public JsContext CreateContext()
        {
            if (JsContext.Current.IsValid)
                throw new SystemException("Context已创建!");
            Native.ThrowIfError(Native.JsCreateContext(this, out var reference));
            JsContext.Current = reference;
            return reference;
        }

        public void SetMemoryAllocationHandle(IntPtr _handleState, JsMemoryAllocationHandle _allocationHandle) => Native.ThrowIfError(Native.JsSetRuntimeMemoryAllocationHandle(this, _handleState, _allocationHandle));
        public void SetBeforeCollectHandle(IntPtr _handleState, JsBeforeCollectHandle _beforeCollectHandle) => Native.ThrowIfError(Native.JsSetRuntimeBeforeCollectHandle(this, _handleState, _beforeCollectHandle));

        public void StartDebug(JsDiagDebugEventHandle _debugEventHandle) => StartDebug(_debugEventHandle, IntPtr.Zero);
        public void StopDebug() => StopDebug(out var state);
        public void StopDebug(out IntPtr _handleState) => Native.ThrowIfError(Native.JsDiagStopDebugging(this, out _handleState));
        public void StartDebug(JsDiagDebugEventHandle _debugEventHandle, IntPtr _handleState) => Native.ThrowIfError(Native.JsDiagStartDebugging(this, _debugEventHandle, _handleState));
        public void RemoveBreakpoint(uint _breakpointId) => Native.ThrowIfError(Native.JsDiagRemoveBreakpoint(_breakpointId));
        public void DiagSetStepType(JsDiagStepFlags stepType) => Native.ThrowIfError(Native.JsDiagSetStepType(stepType));
        public JsValue DiagGetFunctionPosition(JsValue function)
        {
            Native.ThrowIfError(Native.JsDiagGetFunctionPosition(function, out var functionPosition));
            return functionPosition;
        }
        public void DiagSetBreakOnException(JsRuntime runtimeHandle, JsDiagBreakOnExceptionFlags exceptionAttributes)
        {
            Native.ThrowIfError(Native.JsDiagSetBreakOnException(runtimeHandle, exceptionAttributes));
        }
        public BreakPoint SetBreakpoint(uint _scriptId, uint _lineNumber, uint _column, out JsValue _breakpoint)
        {
            Native.ThrowIfError(Native.JsDiagSetBreakpoint(_scriptId, _lineNumber, _column, out _breakpoint));
            return _breakpoint.ToObject<BreakPoint>();
        }
        public SourceCode[] GetScripts()
        {
            Native.ThrowIfError(Native.JsDiagGetScripts(out var scripts));
            return scripts.ToObject<SourceCode[]>();
        }
        public BreakPoint[] GetBreakpoints()
        {
            Native.ThrowIfError(Native.JsDiagGetBreakpoints(out var breakpoints));
            return breakpoints.ToObject<BreakPoint[]>();
        }
        public Variable DiagEvaluate(JsValue _expression, uint _stackFrameIndex, JsParseScriptFlags _parseAttributes, bool _forceSetValueProp)
        {
            Native.ThrowIfError(Native.JsDiagEvaluate(_expression, _stackFrameIndex, _parseAttributes, _forceSetValueProp, out var eval));
            return eval.ToObject<Variable>();
        }
        public Variable DiagGetObjectFromHandle(uint objectHandle)
        {
            Native.ThrowIfError(Native.JsDiagGetObjectFromHandle(objectHandle, out var handleObject));
            return handleObject.ToObject<Variable>();
        }
        public SourceCode DiagGetSource(uint scriptId)
        {
            Native.ThrowIfError(Native.JsDiagGetSource(scriptId, out var source));
            return source.ToObject<SourceCode>();
        }
        public StackTrace[] DiagGetStackTrace()
        {
            Native.ThrowIfError(Native.JsDiagGetStackTrace(out var stackTrace));
            var ret = stackTrace.ToObject<StackTrace[]>();
            
            return ret;
        }
        public StackProperties DiagGetStackProperties(uint stackFrameIndex)
        {
            Native.ThrowIfError(Native.JsDiagGetStackProperties(stackFrameIndex, out var properties));
            var value = properties.ToObject<StackProperties>();
            value.Locals = value.Locals.Where(q => (q.PropertyAttributes & PropertyAttributesFlags.IN_TDZ) != PropertyAttributesFlags.IN_TDZ).ToArray();
            return value;
        }
        public StackGlobalProperties DiagGetProperties(uint objectHandle, uint fromCount, uint totalCount)
        {
            Native.ThrowIfError(Native.JsDiagGetProperties(objectHandle, fromCount, totalCount, out var propertiesObject));
            return propertiesObject.ToObject<StackGlobalProperties>();
        }
        public void RequestAsyncBreak() => Native.ThrowIfError(Native.JsDiagRequestAsyncBreak(this));
        public JsDiagBreakOnExceptionFlags DiagGetBreakOnException()
        {
            Native.ThrowIfError(Native.JsDiagGetBreakOnException(this, out var exceptionAttributes));
            return exceptionAttributes;
        }
        public void CollectGarbage()
        {
            Native.ThrowIfError(Native.JsCollectGarbage(this));
        }
        public void Dispose()
        {
            if (IsValid)
            {
                try
                {
                    StopDebug();
                }
                catch
                { }
                Native.ThrowIfError(Native.JsDisposeRuntime(this));
            }
            p_handle = IntPtr.Zero;
        }

        public static JsRuntime Create(JsRuntimeFlags _attributes, JsRuntimeVersionFlags _version, JsThreadServiceHandle _threadServiceHandle)
        {
            Native.ThrowIfError(Native.JsCreateRuntime(_attributes, _threadServiceHandle, out var handle));
            return handle;
        }
        public static JsRuntime Create(JsRuntimeFlags _attributes, JsRuntimeVersionFlags _version) => Create(_attributes, _version, null);
        public static JsRuntime Create() => Create(JsRuntimeFlags.None, JsRuntimeVersionFlags.V11, null);
    }
}
