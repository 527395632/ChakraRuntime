using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChakraRuntime
{
    public enum JsDiagBreakOnExceptionFlags
    {
        None = 0x0,
        Uncaught = 0x1,
        FirstChance = 0x2
    }

    public enum JsDiagDebugFlags
    {
        SourceCompile = 0,
        CompileError = 1,
        BreakPoint = 2,
        StepComplete = 3,
        DebuggerStatement = 4,
        AsyncBreak = 5,
        RuntimeException = 6
    }

    public enum JsDiagStepFlags
    {
        StepIn = 0,
        StepOut = 1,
        StepOver = 2,
        StepBack = 3,
        ReverseContinue = 4,
        Continue = 5
    }

    public enum JsMemoryEventFlags
    {
        Allocate = 0,
        Free = 1,
        Failure = 2
    }

   public enum JsModuleFlags
    {
        Exception = 0x01,
        HostDefined = 0x02,
        NotifyModuleReadyHandle = 0x3,
        FetchImportedModuleHandle = 0x4,
        FetchImportedModuleFromScriptHandle = 0x5,
        Url = 0x6
    }

    public enum JsParseModuleFlags
    {
        UTF16LE = 0x00000000,
        UTF8 = 0x00000001
    }

    [Flags]
    public enum JsParseScriptFlags
    {
        None = 0x0,
        LibraryCode = 0x1,
        ArrayBufferIsUtf16 = 0x2,
    }

    public enum JsPropertyIdFlags
    {
        String,
        Symbol
    }

    [Flags]
    public enum JsRuntimeFlags
    {
        None = 0x00000000,
        DisableBackgroundWork = 0x00000001,
        AllowScriptInterrupt = 0x00000002,
        EnableIdleProcessing = 0x00000004,
        DisableNativeCodeGeneration = 0x00000008,
        DisableEval = 0x00000010,
        EnableExperimentalFeatures = 0x00000020,
        DispatchSetExceptionsToDebugger = 0x00000040
    }

    public enum JsRuntimeVersionFlags
    {
        V10 = 0,
        V11 = 1,
        VEdge = -1,
    }

    public enum JsStatusFlags : uint
    {
        OK = 0,
        CategoryUsage = 0x10000,
        InvalidArgument,
        NullArgument,
        NoCurrentContext,
        InExceptionState,
        NotImplemented,
        WrongThread,
        RuntimeInUse,
        BadSerializedScript,
        InDisabledState,
        CannotDisableExecution,
        HeapEnumInProgress,
        ArgumentNotObject,
        InProfileHandle,
        InThreadServiceHandle,
        CannotSerializeDebugScript,
        AlreadyDebuggingContext,
        AlreadyProfilingContext,
        IdleNotEnabled,
        CannotSetProjectionEnqueueHandle,
        CannotStartProjection,
        InObjectBeforeCollectHandle,
        ObjectNotInspectable,
        PropertyNotSymbol,
        PropertyNotString,
        CategoryEngine = 0x20000,
        OutOfMemory,
        CategoryScript = 0x30000,
        ScriptException,
        ScriptCompile,
        ScriptTerminated,
        ScriptEvalDisabled,
        CategoryFatal = 0x40000,
        Fatal,
        WrongRuntime,
    }

    public enum JsTypedArrayFlags
    {
        Int8,
        Uint8,
        Uint8Clamped,
        Int16,
        Uint16,
        Int32,
        Uint32,
        Float32,
        Float64
    }

    public enum JsValueFlags
    {
        Undefined = 0,
        Null = 1,
        Number = 2,
        String = 3,
        Boolean = 4,
        Object = 5,
        Function = 6,
        Error = 7,
        Array = 8,
        Symbol = 9,
        ArrayBuffer = 10,
        TypedArray = 11,
        DataView = 12,
    }
}
