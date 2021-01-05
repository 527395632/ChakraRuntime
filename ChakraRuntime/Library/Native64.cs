using System;
using System.Runtime.InteropServices;

namespace ChakraRuntime.Parts
{
    /// <summary>
    ///     Native interfaces.
    /// </summary>
    static class Native64
    {
        const string DllName = @"x64\ChakraCore.dll";

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateRuntime(JsRuntimeFlags _attributes, JsThreadServiceHandle _threadService, out JsRuntime _runtime);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCollectGarbage(JsRuntime _handle);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDisposeRuntime(JsRuntime _handle);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetRuntimeMemoryUsage(JsRuntime _runtime, out UIntPtr _memoryUsage);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetRuntimeMemoryLimit(JsRuntime _runtime, out UIntPtr _memoryLimit);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetRuntimeMemoryLimit(JsRuntime _runtime, UIntPtr _memoryLimit);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetRuntimeMemoryAllocationHandle(JsRuntime _runtime, IntPtr _handleState, JsMemoryAllocationHandle _allocationHandle);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetRuntimeBeforeCollectHandle(JsRuntime _runtime, IntPtr _handleState, JsBeforeCollectHandle _beforeCollectHandle);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsAddRef(JsContext _reference, out uint _count);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsAddRef(JsValue _reference, out uint _count);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsRelease(JsContext _reference, out uint _count);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsRelease(JsValue _reference, out uint _count);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateContext(JsRuntime _runtime, out JsContext _newContext);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetCurrentContext(out JsContext _currentContext);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetCurrentContext(JsContext _context);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetRuntime(JsContext _context, out JsRuntime _runtime);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsIdle(out uint _nextIdleTick);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        internal static extern JsStatusFlags JsParseScript(string _script, JsSourceContext _sourceContext, string _sourceUrl, out JsValue _result);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        internal static extern JsStatusFlags JsRunScript(string _script, JsSourceContext _sourceContext, string _sourceUrl, out JsValue _result);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        internal static extern JsStatusFlags JsSerializeScript(string _script, byte[] _buffer, ref ulong _bufferSize);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        internal static extern JsStatusFlags JsParseSerializedScript(string _script, byte[] _buffer, JsSourceContext _sourceContext, string _sourceUrl, out JsValue _result);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        internal static extern JsStatusFlags JsRunSerializedScript(string _script, byte[] _buffer, JsSourceContext _sourceContext, string _sourceUrl, out JsValue _result);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        internal static extern JsStatusFlags JsGetPropertyIdFromName(string _name, out JsPropertyId _propertyId);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        internal static extern JsStatusFlags JsGetPropertyNameFromId(JsPropertyId _propertyId, out string _name);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetUndefinedValue(out JsValue _undefinedValue);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetNullValue(out JsValue _nullValue);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetTrueValue(out JsValue _trueValue);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetFalseValue(out JsValue _falseValue);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsBoolToBoolean(bool _value, out JsValue _booleanValue);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsBooleanToBool(JsValue _booleanValue, out bool _boolValue);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsConvertValueToBoolean(JsValue _value, out JsValue _booleanValue);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetValueType(JsValue _value, out JsValueFlags _type);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDoubleToNumber(double _doubleValue, out JsValue _value);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsIntToNumber(int _intValue, out JsValue _value);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsNumberToDouble(JsValue _value, out double _doubleValue);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsConvertValueToNumber(JsValue _value, out JsValue _numberValue);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetStringLength(JsValue _sringValue, out int _length);

        [DllImport(DllName, CharSet = CharSet.Unicode)]
        internal static extern JsStatusFlags JsPointerToString(string _value, UIntPtr _stringLength, out JsValue _stringValue);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsStringToPointer(JsValue _value, out IntPtr _stringValue, out UIntPtr _stringLength);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsConvertValueToString(JsValue _value, out JsValue _stringValue);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetGlobalObject(out JsValue _globalObject);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateObject(out JsValue _obj);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateExternalObject(IntPtr _data, JsObjectFinalizeHandle _finalizeHandle, out JsValue _obj);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsConvertValueToObject(JsValue _value, out JsValue _obj);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetPrototype(JsValue _obj, out JsValue _prototypeObject);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetPrototype(JsValue _obj, JsValue _prototypeObject);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetExtensionAllowed(JsValue _obj, out bool _value);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsPreventExtension(JsValue _obj);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetProperty(JsValue _obj, JsPropertyId _propertyId, out JsValue _value);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetOwnPropertyDescriptor(JsValue _obj, JsPropertyId _propertyId, out JsValue _propertyDescriptor);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetOwnPropertyNames(JsValue _obj, out JsValue _propertyNames);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetProperty(JsValue _obj, JsPropertyId _propertyId, JsValue _value, bool _useStrictRules);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsHasProperty(JsValue _obj, JsPropertyId _propertyId, out bool _hasProperty);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDeleteProperty(JsValue _obj, JsPropertyId _propertyId, bool _useStrictRules, out JsValue _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDefineProperty(JsValue _obj, JsPropertyId _propertyId, JsValue _propertyDescriptor, out bool _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsHasIndexedProperty(JsValue _obj, JsValue _index, out bool _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetIndexedProperty(JsValue _obj, JsValue _index, out JsValue _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetIndexedProperty(JsValue _obj, JsValue _index, JsValue _value);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDeleteIndexedProperty(JsValue _obj, JsValue _index);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsEquals(JsValue _obj1, JsValue _obj2, out bool _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsStrictEquals(JsValue _obj1, JsValue _obj2, out bool _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsHasExternalData(JsValue _obj, out bool _value);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetExternalData(JsValue _obj, out IntPtr _externalData);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetExternalData(JsValue _obj, IntPtr _externalData);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateArray(uint _length, out JsValue _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCallFunction(JsValue _function, JsValue[] _arguments, ushort _argumentCount, out JsValue _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsConstructObject(JsValue _function, JsValue[] _arguments, ushort _argumentCount, out JsValue _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateFunction(JsNativeFunction _nativeFunction, IntPtr _externalData, out JsValue _function);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateError(JsValue _message, out JsValue _error);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateRangeError(JsValue _message, out JsValue _error);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateReferenceError(JsValue _message, out JsValue _error);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateSyntaxError(JsValue _message, out JsValue _error);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateTypeError(JsValue _message, out JsValue _error);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateURIError(JsValue _message, out JsValue _error);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsHasException(out bool _hasException);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetAndClearException(out JsValue _exception);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetException(JsValue _exception);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDisableRuntimeExecution(JsRuntime _runtime);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsEnableRuntimeExecution(JsRuntime _runtime);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsIsRuntimeExecutionDisabled(JsRuntime _runtime, out bool _isDisabled);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetObjectBeforeCollectHandle(JsValue _reference, IntPtr _handleState, JsObjectBeforeCollectHandle _beforeCollectHandle);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateNamedFunction(JsValue _name, JsNativeFunction _nativeFunction, IntPtr _handleState, out JsValue _function);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetPromiseContinuationHandle(JsPromiseContinuationHandle _promiseContinuationHandle, IntPtr _handleState);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateArrayBuffer(uint _byteLength, out JsValue _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateTypedArray(JsTypedArrayFlags _arrayType, JsValue _arrayBuffer, uint _byteOffset, uint _elementLength, out JsValue _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateDataView(JsValue _arrayBuffer, uint _byteOffset, uint _byteOffsetLength, out JsValue _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetArrayBufferStorage(JsValue _arrayBuffer, out IntPtr _buffer, out uint _bufferLength);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetTypedArrayStorage(JsValue _typedArray, out IntPtr _buffer, out uint _bufferLength, out JsTypedArrayFlags _arrayType, out int _elementSize);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetDataViewStorage(JsValue _dataView, out IntPtr _buffer, out uint _bufferLength);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetPropertyIdType(JsPropertyId _propertyId, out JsPropertyIdFlags _propertyIdType);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateSymbol(JsValue _description, out JsValue _symbol);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetSymbolFromPropertyId(JsPropertyId _propertyId, out JsValue _symbol);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetPropertyIdFromSymbol(JsValue _symbol, out JsPropertyId _propertyId);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetOwnPropertySymbols(JsValue _obj, out JsValue _propertySymbols);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsNumberToInt(JsValue _value, out int _intValue);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetIndexedPropertiesToExternalData(JsValue _obj, IntPtr _data, JsTypedArrayFlags _arrayType, uint _elementLength);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetIndexedPropertiesExternalData(JsValue _obj, IntPtr _data, out JsTypedArrayFlags _arrayType, out uint _elementLength);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsHasIndexedPropertiesExternalData(JsValue _obj, out bool _value);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsInstanceOf(JsValue _obj, JsValue _constructor, out bool _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsCreateExternalArrayBuffer(IntPtr _data, uint _byteLength, JsObjectFinalizeHandle _finalizeHandle, IntPtr _handleState, out JsValue _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetTypedArrayInfo(JsValue _typedArray, out JsTypedArrayFlags _arrayType, out JsValue _arrayBuffer, out uint _byteOffset, out uint _byteLength);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetContextOfObject(JsValue _obj, out JsContext _context);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsGetContextData(JsContext _context, out IntPtr _data);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetContextData(JsContext _context, IntPtr _data);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsParseSerializedScriptWithHandle(JsSerializedScriptLoadSourceHandle _scriptLoadHandle, JsSerializedScriptUnloadHandle _scriptUnloadHandle, byte[] _buffer, JsSourceContext _sourceContext, string _sourceUrl, out JsValue _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsRunSerializedScriptWithHandle(JsSerializedScriptLoadSourceHandle _scriptLoadHandle, JsSerializedScriptUnloadHandle _scriptUnloadHandle, byte[] _buffer, JsSourceContext _sourceContext, string _sourceUrl, out JsValue _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsInitializeModuleRecord(JsModule _referencingModule, JsValue _normalizedSpecifier, out JsModule _moduleRecord);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetModuleHostInfo(JsModule _requestModule, JsModuleFlags _moduleHostInfo, JsFetchImportedModuleHandle _handle);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetModuleHostInfo(JsModule _requestModule, JsModuleFlags _moduleHostInfo, JsNotifyModuleReadyHandle _handle);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetModuleHostInfo(JsModule _requestModule, JsModuleFlags _moduleHostInfo, JsFetchImportedModuleFromScriptHandle _handle);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsSetModuleHostInfo(JsModule module, JsModuleFlags kind, JsValue value);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsParseModuleSource(JsModule _requestModule, JsSourceContext _sourceContext, byte[] _script, uint _scriptLength, JsParseModuleFlags _sourceFlag, out JsValue _exception);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsModuleEvaluation(JsModule _requestModule, out JsValue _result);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagStartDebugging(JsRuntime _runtime, JsDiagDebugEventHandle _debugEventHandle, IntPtr _handleState);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagStopDebugging(JsRuntime _runtime, out IntPtr _handleState);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagSetBreakpoint(uint _scriptId, uint _lineNumber, uint _column, out JsValue _breakpoint);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagRequestAsyncBreak(JsRuntime _runtime);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagGetBreakpoints(out JsValue _breakpoints);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagRemoveBreakpoint(uint _breakpointId);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagGetScripts(out JsValue _scripts);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagEvaluate(JsValue _expression, uint _stackFrameIndex, JsParseScriptFlags _parseAttributes, bool _forceSetValueProp, out JsValue _eval);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagGetObjectFromHandle(uint objectHandle, out JsValue handleObject);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagGetSource(uint scriptId, out JsValue source);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagGetFunctionPosition(JsValue function, out JsValue functionPosition);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagGetStackTrace(out JsValue stackTrace);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagGetStackProperties(uint stackFrameIndex, out JsValue properties);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagGetProperties(uint objectHandle, uint fromCount, uint totalCount, out JsValue propertiesObject);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagSetBreakOnException(JsRuntime runtimeHandle, JsDiagBreakOnExceptionFlags exceptionAttributes);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagGetBreakOnException(JsRuntime runtimeHandle, out JsDiagBreakOnExceptionFlags exceptionAttributes);

        [DllImport(DllName)]
        internal static extern JsStatusFlags JsDiagSetStepType(JsDiagStepFlags stepType);
    }
}