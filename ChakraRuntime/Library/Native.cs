using System;
using System.Runtime.InteropServices;
using ChakraRuntime.Parts;

namespace ChakraRuntime
{
    public static class Native
    {
        private static readonly bool Is32 = IntPtr.Size == 4;

        internal static JsStatusFlags JsCreateRuntime(JsRuntimeFlags _attributes, JsThreadServiceHandle _threadService, out JsRuntime _runtime) => Is32 ?
            Native32.JsCreateRuntime(_attributes, _threadService, out _runtime) :
            Native64.JsCreateRuntime(_attributes, _threadService, out _runtime);

        internal static JsStatusFlags JsCollectGarbage(JsRuntime _handle) => Is32 ?
            Native32.JsCollectGarbage(_handle) :
            Native64.JsCollectGarbage(_handle);

        internal static JsStatusFlags JsDisposeRuntime(JsRuntime _handle) => Is32 ?
            Native32.JsDisposeRuntime(_handle) :
            Native64.JsDisposeRuntime(_handle);

        internal static JsStatusFlags JsGetRuntimeMemoryUsage(JsRuntime _runtime, out UIntPtr _memoryUsage) => Is32 ?
            Native32.JsGetRuntimeMemoryUsage(_runtime, out _memoryUsage) :
            Native64.JsGetRuntimeMemoryUsage(_runtime, out _memoryUsage);

        internal static JsStatusFlags JsGetRuntimeMemoryLimit(JsRuntime _runtime, out UIntPtr _memoryLimit) => Is32 ?
            Native32.JsGetRuntimeMemoryLimit(_runtime, out _memoryLimit) :
            Native64.JsGetRuntimeMemoryLimit(_runtime, out _memoryLimit);

        internal static JsStatusFlags JsSetRuntimeMemoryLimit(JsRuntime _runtime, UIntPtr _memoryLimit) => Is32 ?
            Native32.JsSetRuntimeMemoryLimit(_runtime, _memoryLimit) :
            Native64.JsSetRuntimeMemoryLimit(_runtime, _memoryLimit);

        internal static JsStatusFlags JsSetRuntimeMemoryAllocationHandle(JsRuntime _runtime, IntPtr _handleState, JsMemoryAllocationHandle _allocationHandle) => Is32 ?
            Native32.JsSetRuntimeMemoryAllocationHandle(_runtime, _handleState, _allocationHandle) :
            Native64.JsSetRuntimeMemoryAllocationHandle(_runtime, _handleState, _allocationHandle);

        internal static JsStatusFlags JsSetRuntimeBeforeCollectHandle(JsRuntime _runtime, IntPtr _handleState, JsBeforeCollectHandle _beforeCollectHandle) => Is32 ?
            Native32.JsSetRuntimeBeforeCollectHandle(_runtime, _handleState, _beforeCollectHandle) :
            Native64.JsSetRuntimeBeforeCollectHandle(_runtime, _handleState, _beforeCollectHandle);

        internal static JsStatusFlags JsContextAddRef(JsContext _reference, out uint _count) => Is32 ?
            Native32.JsAddRef(_reference, out _count) :
            Native64.JsAddRef(_reference, out _count);

        internal static JsStatusFlags JsAddRef(JsValue _reference, out uint _count) => Is32 ?
            Native32.JsAddRef(_reference, out _count) :
            Native64.JsAddRef(_reference, out _count);

        internal static JsStatusFlags JsContextRelease(JsContext _reference, out uint _count) => Is32 ?
            Native32.JsRelease(_reference, out _count) :
            Native64.JsRelease(_reference, out _count);

        internal static JsStatusFlags JsRelease(JsValue _reference, out uint _count) => Is32 ?
            Native32.JsRelease(_reference, out _count) :
            Native64.JsRelease(_reference, out _count);

        internal static JsStatusFlags JsCreateContext(JsRuntime _runtime, out JsContext _newContext) => Is32 ?
            Native32.JsCreateContext(_runtime, out _newContext) :
            Native64.JsCreateContext(_runtime, out _newContext);

        internal static JsStatusFlags JsGetCurrentContext(out JsContext _currentContext) => Is32 ?
            Native32.JsGetCurrentContext(out _currentContext) :
            Native64.JsGetCurrentContext(out _currentContext);

        internal static JsStatusFlags JsSetCurrentContext(JsContext _context) => Is32 ?
            Native32.JsSetCurrentContext(_context) :
            Native64.JsSetCurrentContext(_context);

        internal static JsStatusFlags JsGetRuntime(JsContext _context, out JsRuntime _runtime) => Is32 ?
            Native32.JsGetRuntime(_context, out _runtime) :
            Native64.JsGetRuntime(_context, out _runtime);

        internal static JsStatusFlags JsIdle(out uint _nextIdleTick) => Is32 ?
            Native32.JsIdle(out _nextIdleTick) :
            Native64.JsIdle(out _nextIdleTick);

        internal static JsStatusFlags JsParseScript(string _script, JsSourceContext _sourceContext, string _sourceUrl, out JsValue _result) => Is32 ?
            Native32.JsParseScript(_script, _sourceContext, _sourceUrl, out _result) :
            Native64.JsParseScript(_script, _sourceContext, _sourceUrl, out _result);

        internal static JsStatusFlags JsRunScript(string _script, JsSourceContext _sourceContext, string _sourceUrl, out JsValue _result) => Is32 ?
            Native32.JsRunScript(_script, _sourceContext, _sourceUrl, out _result) :
            Native64.JsRunScript(_script, _sourceContext, _sourceUrl, out _result);

        internal static JsStatusFlags JsSerializeScript(string _script, byte[] _buffer, ref ulong _bufferSize) => Is32 ?
            Native32.JsSerializeScript(_script, _buffer, ref _bufferSize) :
            Native64.JsSerializeScript(_script, _buffer, ref _bufferSize);

        internal static JsStatusFlags JsParseSerializedScript(string _script, byte[] _buffer, JsSourceContext _sourceContext, string _sourceUrl, out JsValue _result) => Is32 ?
            Native32.JsParseSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result) :
            Native64.JsParseSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);

        internal static JsStatusFlags JsRunSerializedScript(string _script, byte[] _buffer, JsSourceContext _sourceContext, string _sourceUrl, out JsValue _result) => Is32 ?
            Native32.JsRunSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result) :
            Native64.JsRunSerializedScript(_script, _buffer, _sourceContext, _sourceUrl, out _result);

        internal static JsStatusFlags JsGetPropertyIdFromName(string _name, out JsPropertyId _propertyId) => Is32 ?
            Native32.JsGetPropertyIdFromName(_name, out _propertyId) :
            Native64.JsGetPropertyIdFromName(_name, out _propertyId);

        internal static JsStatusFlags JsGetPropertyNameFromId(JsPropertyId _propertyId, out string _name) => Is32 ?
            Native32.JsGetPropertyNameFromId(_propertyId, out _name) :
            Native64.JsGetPropertyNameFromId(_propertyId, out _name);

        internal static JsStatusFlags JsGetUndefinedValue(out JsValue _undefinedValue) => Is32 ?
            Native32.JsGetUndefinedValue(out _undefinedValue) :
            Native64.JsGetUndefinedValue(out _undefinedValue);

        internal static JsStatusFlags JsGetNullValue(out JsValue _nullValue) => Is32 ?
            Native32.JsGetNullValue(out _nullValue) :
            Native64.JsGetNullValue(out _nullValue);

        internal static JsStatusFlags JsGetTrueValue(out JsValue _trueValue) => Is32 ?
            Native32.JsGetTrueValue(out _trueValue) :
            Native64.JsGetTrueValue(out _trueValue);

        internal static JsStatusFlags JsGetFalseValue(out JsValue _falseValue) => Is32 ?
            Native32.JsGetFalseValue(out _falseValue) :
            Native64.JsGetFalseValue(out _falseValue);

        internal static JsStatusFlags JsBoolToBoolean(bool _value, out JsValue _booleanValue) => Is32 ?
            Native32.JsBoolToBoolean(_value, out _booleanValue) :
            Native64.JsBoolToBoolean(_value, out _booleanValue);

        internal static JsStatusFlags JsBooleanToBool(JsValue _booleanValue, out bool _boolValue) => Is32 ?
            Native32.JsBooleanToBool(_booleanValue, out _boolValue) :
            Native64.JsBooleanToBool(_booleanValue, out _boolValue);

        internal static JsStatusFlags JsConvertValueToBoolean(JsValue _value, out JsValue _booleanValue) => Is32 ?
            Native32.JsConvertValueToBoolean(_value, out _booleanValue) :
            Native64.JsConvertValueToBoolean(_value, out _booleanValue);

        internal static JsStatusFlags JsGetValueType(JsValue _value, out JsValueFlags _type) => Is32 ?
            Native32.JsGetValueType(_value, out _type) :
            Native64.JsGetValueType(_value, out _type);

        internal static JsStatusFlags JsDoubleToNumber(double _doubleValue, out JsValue _value) => Is32 ?
            Native32.JsDoubleToNumber(_doubleValue, out _value) :
            Native64.JsDoubleToNumber(_doubleValue, out _value);

        internal static JsStatusFlags JsIntToNumber(int _intValue, out JsValue _value) => Is32 ?
            Native32.JsDoubleToNumber(_intValue, out _value) :
            Native64.JsDoubleToNumber(_intValue, out _value);

        internal static JsStatusFlags JsNumberToDouble(JsValue _value, out double _doubleValue) => Is32 ?
            Native32.JsNumberToDouble(_value, out _doubleValue) :
            Native64.JsNumberToDouble(_value, out _doubleValue);

        internal static JsStatusFlags JsConvertValueToNumber(JsValue _value, out JsValue _numberValue) => Is32 ?
            Native32.JsConvertValueToNumber(_value, out _numberValue) :
            Native64.JsConvertValueToNumber(_value, out _numberValue);

        internal static JsStatusFlags JsGetStringLength(JsValue _sringValue, out int _length) => Is32 ?
            Native32.JsGetStringLength(_sringValue, out _length) :
            Native64.JsGetStringLength(_sringValue, out _length);

        internal static JsStatusFlags JsPointerToString(string _value, UIntPtr _stringLength, out JsValue _stringValue) => Is32 ?
            Native32.JsPointerToString(_value, _stringLength, out _stringValue) :
            Native64.JsPointerToString(_value, _stringLength, out _stringValue);

        internal static JsStatusFlags JsStringToPointer(JsValue _value, out IntPtr _stringValue, out UIntPtr _stringLength) => Is32 ?
            Native32.JsStringToPointer(_value, out _stringValue, out _stringLength) :
            Native64.JsStringToPointer(_value, out _stringValue, out _stringLength);

        internal static JsStatusFlags JsConvertValueToString(JsValue _value, out JsValue _stringValue) => Is32 ?
            Native32.JsConvertValueToString(_value, out _stringValue) :
            Native64.JsConvertValueToString(_value, out _stringValue);

        internal static JsStatusFlags JsGetGlobalObject(out JsValue _globalObject) => Is32 ?
            Native32.JsGetGlobalObject(out _globalObject) :
            Native64.JsGetGlobalObject(out _globalObject);

        internal static JsStatusFlags JsCreateObject(out JsValue _obj) => Is32 ?
            Native32.JsCreateObject(out _obj) :
            Native64.JsCreateObject(out _obj);

        internal static JsStatusFlags JsCreateExternalObject(IntPtr _data, JsObjectFinalizeHandle _finalizeHandle, out JsValue _obj) => Is32 ?
            Native32.JsCreateExternalObject(_data, _finalizeHandle, out _obj) :
            Native64.JsCreateExternalObject(_data, _finalizeHandle, out _obj);

        internal static JsStatusFlags JsConvertValueToObject(JsValue _value, out JsValue _obj) => Is32 ?
            Native32.JsConvertValueToObject(_value, out _obj) :
            Native64.JsConvertValueToObject(_value, out _obj);

        internal static JsStatusFlags JsGetPrototype(JsValue _obj, out JsValue _prototypeObject) => Is32 ?
            Native32.JsGetPrototype(_obj, out _prototypeObject) :
            Native64.JsGetPrototype(_obj, out _prototypeObject);

        internal static JsStatusFlags JsSetPrototype(JsValue _obj, JsValue _prototypeObject) => Is32 ?
            Native32.JsSetPrototype(_obj, _prototypeObject) :
            Native64.JsSetPrototype(_obj, _prototypeObject);

        internal static JsStatusFlags JsGetExtensionAllowed(JsValue _obj, out bool _value) => Is32 ?
            Native32.JsGetExtensionAllowed(_obj, out _value) :
            Native64.JsGetExtensionAllowed(_obj, out _value);

        internal static JsStatusFlags JsPreventExtension(JsValue _obj) => Is32 ?
            Native32.JsPreventExtension(_obj) :
            Native64.JsPreventExtension(_obj);

        internal static JsStatusFlags JsGetProperty(JsValue _obj, JsPropertyId _propertyId, out JsValue _value) => Is32 ?
            Native32.JsGetProperty(_obj, _propertyId, out _value) :
            Native64.JsGetProperty(_obj, _propertyId, out _value);

        internal static JsStatusFlags JsGetOwnPropertyDescriptor(JsValue _obj, JsPropertyId _propertyId, out JsValue _propertyDescriptor) => Is32 ?
            Native32.JsGetOwnPropertyDescriptor(_obj, _propertyId, out _propertyDescriptor) :
            Native64.JsGetOwnPropertyDescriptor(_obj, _propertyId, out _propertyDescriptor);

        internal static JsStatusFlags JsGetOwnPropertyNames(JsValue _obj, out JsValue _propertyNames) => Is32 ?
            Native32.JsGetOwnPropertyNames(_obj, out _propertyNames) :
            Native64.JsGetOwnPropertyNames(_obj, out _propertyNames);

        internal static JsStatusFlags JsSetProperty(JsValue _obj, JsPropertyId _propertyId, JsValue _value, bool _useStrictRules) => Is32 ?
            Native32.JsSetProperty(_obj, _propertyId, _value, _useStrictRules) :
            Native64.JsSetProperty(_obj, _propertyId, _value, _useStrictRules);

        internal static JsStatusFlags JsHasProperty(JsValue _obj, JsPropertyId _propertyId, out bool _hasProperty)
        => Is32 ?
            Native32.JsHasProperty(_obj, _propertyId, out _hasProperty) :
            Native64.JsHasProperty(_obj, _propertyId, out _hasProperty);

        internal static JsStatusFlags JsDeleteProperty(JsValue _obj, JsPropertyId _propertyId, bool _useStrictRules, out JsValue _result) => Is32 ?
            Native32.JsDeleteProperty(_obj, _propertyId, _useStrictRules, out _result) :
            Native64.JsDeleteProperty(_obj, _propertyId, _useStrictRules, out _result);

        internal static JsStatusFlags JsDefineProperty(JsValue _obj, JsPropertyId _propertyId, JsValue _propertyDescriptor, out bool _result) => Is32 ?
            Native32.JsDefineProperty(_obj, _propertyId, _propertyDescriptor, out _result) :
            Native64.JsDefineProperty(_obj, _propertyId, _propertyDescriptor, out _result);

        internal static JsStatusFlags JsHasIndexedProperty(JsValue _obj, JsValue _index, out bool _result) => Is32 ?
            Native32.JsHasIndexedProperty(_obj, _index, out _result) :
            Native64.JsHasIndexedProperty(_obj, _index, out _result);

        internal static JsStatusFlags JsGetIndexedProperty(JsValue _obj, JsValue _index, out JsValue _result) => Is32 ?
            Native32.JsGetIndexedProperty(_obj, _index, out _result) :
            Native64.JsGetIndexedProperty(_obj, _index, out _result);

        internal static JsStatusFlags JsSetIndexedProperty(JsValue _obj, JsValue _index, JsValue _value) => Is32 ?
            Native32.JsSetIndexedProperty(_obj, _index, _value) :
            Native64.JsSetIndexedProperty(_obj, _index, _value);

        internal static JsStatusFlags JsDeleteIndexedProperty(JsValue _obj, JsValue _index) => Is32 ?
            Native32.JsDeleteIndexedProperty(_obj, _index) :
            Native64.JsDeleteIndexedProperty(_obj, _index);

        internal static JsStatusFlags JsEquals(JsValue _obj1, JsValue _obj2, out bool _result) => Is32 ?
            Native32.JsEquals(_obj1, _obj2, out _result) :
            Native64.JsEquals(_obj1, _obj2, out _result);

        internal static JsStatusFlags JsStrictEquals(JsValue _obj1, JsValue _obj2, out bool _result) => Is32 ?
            Native32.JsStrictEquals(_obj1, _obj2, out _result) :
            Native64.JsEquals(_obj1, _obj2, out _result);

        internal static JsStatusFlags JsHasExternalData(JsValue _obj, out bool _value) => Is32 ?
            Native32.JsHasExternalData(_obj, out _value) :
            Native64.JsHasExternalData(_obj, out _value);

        internal static JsStatusFlags JsGetExternalData(JsValue _obj, out IntPtr _externalData) => Is32 ?
            Native32.JsGetExternalData(_obj, out _externalData) :
            Native64.JsGetExternalData(_obj, out _externalData);

        internal static JsStatusFlags JsSetExternalData(JsValue _obj, IntPtr _externalData) => Is32 ?
            Native32.JsSetExternalData(_obj, _externalData) :
            Native64.JsSetExternalData(_obj, _externalData);

        internal static JsStatusFlags JsCreateArray(uint _length, out JsValue _result) => Is32 ?
            Native32.JsCreateArray(_length, out _result) :
            Native64.JsCreateArray(_length, out _result);

        internal static JsStatusFlags JsCallFunction(JsValue _function, JsValue[] _arguments, ushort _argumentCount, out JsValue _result) => Is32 ?
            Native32.JsCallFunction(_function, _arguments, _argumentCount, out _result) :
            Native64.JsCallFunction(_function, _arguments, _argumentCount, out _result);

        internal static JsStatusFlags JsConstructObject(JsValue _function, JsValue[] _arguments, ushort _argumentCount, out JsValue _result) => Is32 ?
            Native32.JsConstructObject(_function, _arguments, _argumentCount, out _result) :
            Native64.JsConstructObject(_function, _arguments, _argumentCount, out _result);

        internal static JsStatusFlags JsCreateFunction(JsNativeFunction _nativeFunction, IntPtr _externalData, out JsValue _function) => Is32 ?
            Native32.JsCreateFunction(_nativeFunction, _externalData, out _function) :
            Native64.JsCreateFunction(_nativeFunction, _externalData, out _function);

        internal static JsStatusFlags JsCreateError(JsValue _message, out JsValue _error) => Is32 ?
            Native32.JsCreateError(_message, out _error) :
            Native64.JsCreateError(_message, out _error);

        internal static JsStatusFlags JsCreateRangeError(JsValue _message, out JsValue _error) => Is32 ?
            Native32.JsCreateRangeError(_message, out _error) :
            Native64.JsCreateRangeError(_message, out _error);

        internal static JsStatusFlags JsCreateReferenceError(JsValue _message, out JsValue _error) => Is32 ?
            Native32.JsCreateReferenceError(_message, out _error) :
            Native64.JsCreateReferenceError(_message, out _error);

        internal static JsStatusFlags JsCreateSyntaxError(JsValue _message, out JsValue _error) => Is32 ?
            Native32.JsCreateSyntaxError(_message, out _error) :
            Native64.JsCreateSyntaxError(_message, out _error);

        internal static JsStatusFlags JsCreateTypeError(JsValue _message, out JsValue _error) => Is32 ?
            Native32.JsCreateTypeError(_message, out _error) :
            Native64.JsCreateTypeError(_message, out _error);

        internal static JsStatusFlags JsCreateUriError(JsValue _message, out JsValue _error) => Is32 ?
            Native32.JsCreateURIError(_message, out _error) :
            Native64.JsCreateURIError(_message, out _error);

        internal static JsStatusFlags JsHasException(out bool _hasException) => Is32 ?
            Native32.JsHasException(out _hasException) :
            Native64.JsHasException(out _hasException);

        internal static JsStatusFlags JsGetAndClearException(out JsValue _exception) => Is32 ?
            Native32.JsGetAndClearException(out _exception) :
            Native64.JsGetAndClearException(out _exception);

        internal static JsStatusFlags JsSetException(JsValue _exception) => Is32 ?
            Native32.JsSetException(_exception) :
            Native64.JsSetException(_exception);

        internal static JsStatusFlags JsDisableRuntimeExecution(JsRuntime _runtime) => Is32 ?
            Native32.JsDisableRuntimeExecution(_runtime) :
            Native64.JsDisableRuntimeExecution(_runtime);

        internal static JsStatusFlags JsEnableRuntimeExecution(JsRuntime _runtime) => Is32 ?
            Native32.JsEnableRuntimeExecution(_runtime) :
            Native64.JsEnableRuntimeExecution(_runtime);

        internal static JsStatusFlags JsIsRuntimeExecutionDisabled(JsRuntime _runtime, out bool _isDisabled) => Is32 ?
            Native32.JsIsRuntimeExecutionDisabled(_runtime, out _isDisabled) :
            Native64.JsIsRuntimeExecutionDisabled(_runtime, out _isDisabled);

        internal static JsStatusFlags JsSetObjectBeforeCollectHandle(JsValue _reference, IntPtr _handleState, JsObjectBeforeCollectHandle _beforeCollectHandle) => Is32 ?
            Native32.JsSetObjectBeforeCollectHandle(_reference, _handleState, _beforeCollectHandle) :
            Native64.JsSetObjectBeforeCollectHandle(_reference, _handleState, _beforeCollectHandle);

        internal static JsStatusFlags JsCreateNamedFunction(JsValue _name, JsNativeFunction _nativeFunction, IntPtr _handleState, out JsValue _function) => Is32 ?
            Native32.JsCreateNamedFunction(_name, _nativeFunction, _handleState, out _function) :
            Native64.JsCreateNamedFunction(_name, _nativeFunction, _handleState, out _function);

        internal static JsStatusFlags JsSetPromiseContinuationHandle(JsPromiseContinuationHandle _promiseContinuationHandle, IntPtr _handleState) => Is32 ?
            Native32.JsSetPromiseContinuationHandle(_promiseContinuationHandle, _handleState) :
            Native64.JsSetPromiseContinuationHandle(_promiseContinuationHandle, _handleState);

        internal static JsStatusFlags JsCreateArrayBuffer(uint _byteLength, out JsValue _result) => Is32 ?
            Native32.JsCreateArrayBuffer(_byteLength, out _result) :
            Native64.JsCreateArrayBuffer(_byteLength, out _result);

        internal static JsStatusFlags JsCreateTypedArray(JsTypedArrayFlags _arrayType, JsValue _arrayBuffer, uint _byteOffset, uint _elementLength, out JsValue _result) => Is32 ?
            Native32.JsCreateTypedArray(_arrayType, _arrayBuffer, _byteOffset, _elementLength, out _result) :
            Native64.JsCreateTypedArray(_arrayType, _arrayBuffer, _byteOffset, _elementLength, out _result);

        internal static JsStatusFlags JsCreateDataView(JsValue _arrayBuffer, uint _byteOffset, uint _byteOffsetLength, out JsValue _result) => Is32 ?
            Native32.JsCreateDataView(_arrayBuffer, _byteOffset, _byteOffsetLength, out _result) :
            Native64.JsCreateDataView(_arrayBuffer, _byteOffset, _byteOffsetLength, out _result);

        internal static JsStatusFlags JsGetArrayBufferStorage(JsValue _arrayBuffer, out IntPtr _buffer, out uint _bufferLength) => Is32 ?
            Native32.JsGetArrayBufferStorage(_arrayBuffer, out _buffer, out _bufferLength) :
            Native64.JsGetArrayBufferStorage(_arrayBuffer, out _buffer, out _bufferLength);

        internal static JsStatusFlags JsGetTypedArrayStorage(JsValue _typedArray, out IntPtr _buffer, out uint _bufferLength, out JsTypedArrayFlags _arrayType, out int _elementSize) => Is32 ?
            Native32.JsGetTypedArrayStorage(_typedArray, out _buffer, out _bufferLength, out _arrayType, out _elementSize) :
            Native64.JsGetTypedArrayStorage(_typedArray, out _buffer, out _bufferLength, out _arrayType, out _elementSize);

        internal static JsStatusFlags JsGetDataViewStorage(JsValue _dataView, out IntPtr _buffer, out uint _bufferLength) => Is32 ?
            Native32.JsGetDataViewStorage(_dataView, out _buffer, out _bufferLength) :
            Native64.JsGetDataViewStorage(_dataView, out _buffer, out _bufferLength);

        internal static JsStatusFlags JsGetPropertyIdType(JsPropertyId _propertyId, out JsPropertyIdFlags _propertyIdType) => Is32 ?
            Native32.JsGetPropertyIdType(_propertyId, out _propertyIdType) :
            Native64.JsGetPropertyIdType(_propertyId, out _propertyIdType);

        internal static JsStatusFlags JsCreateSymbol(JsValue _description, out JsValue _symbol) => Is32 ?
            Native32.JsCreateSymbol(_description, out _symbol) :
            Native64.JsCreateSymbol(_description, out _symbol);

        internal static JsStatusFlags JsGetSymbolFromPropertyId(JsPropertyId _propertyId, out JsValue _symbol) => Is32 ?
            Native32.JsGetSymbolFromPropertyId(_propertyId, out _symbol) :
            Native64.JsGetSymbolFromPropertyId(_propertyId, out _symbol);

        internal static JsStatusFlags JsGetPropertyIdFromSymbol(JsValue _symbol, out JsPropertyId _propertyId) => Is32 ?
            Native32.JsGetPropertyIdFromSymbol(_symbol, out _propertyId) :
            Native64.JsGetPropertyIdFromSymbol(_symbol, out _propertyId);

        internal static JsStatusFlags JsGetOwnPropertySymbols(JsValue _obj, out JsValue _propertySymbols) => Is32 ?
            Native32.JsGetOwnPropertySymbols(_obj, out _propertySymbols) :
            Native64.JsGetOwnPropertySymbols(_obj, out _propertySymbols);

        internal static JsStatusFlags JsNumberToInt(JsValue _value, out int _intValue) => Is32 ?
            Native32.JsNumberToInt(_value, out _intValue) :
            Native64.JsNumberToInt(_value, out _intValue);

        internal static JsStatusFlags JsSetIndexedPropertiesToExternalData(JsValue _obj, IntPtr _data, JsTypedArrayFlags _arrayType, uint _elementLength) => Is32 ?
            Native32.JsSetIndexedPropertiesToExternalData(_obj, _data, _arrayType, _elementLength) :
            Native64.JsSetIndexedPropertiesToExternalData(_obj, _data, _arrayType, _elementLength);

        internal static JsStatusFlags JsGetIndexedPropertiesExternalData(JsValue _obj, IntPtr _data, out JsTypedArrayFlags _arrayType, out uint _elementLength) => Is32 ?
            Native32.JsGetIndexedPropertiesExternalData(_obj, _data, out _arrayType, out _elementLength) :
            Native64.JsGetIndexedPropertiesExternalData(_obj, _data, out _arrayType, out _elementLength);

        internal static JsStatusFlags JsHasIndexedPropertiesExternalData(JsValue _obj, out bool _value) => Is32 ?
            Native32.JsHasIndexedPropertiesExternalData(_obj, out _value) :
            Native64.JsHasIndexedPropertiesExternalData(_obj, out _value);

        internal static JsStatusFlags JsInstanceOf(JsValue _obj, JsValue _constructor, out bool _result) => Is32 ?
            Native32.JsInstanceOf(_obj, _constructor, out _result) :
            Native64.JsInstanceOf(_obj, _constructor, out _result);

        internal static JsStatusFlags JsCreateExternalArrayBuffer(IntPtr _data, uint _byteLength, JsObjectFinalizeHandle _finalizeHandle, IntPtr _handleState, out JsValue _result) => Is32 ?
            Native32.JsCreateExternalArrayBuffer(_data, _byteLength, _finalizeHandle, _handleState, out _result) :
            Native64.JsCreateExternalArrayBuffer(_data, _byteLength, _finalizeHandle, _handleState, out _result);

        internal static JsStatusFlags JsGetTypedArrayInfo(JsValue _typedArray, out JsTypedArrayFlags _arrayType, out JsValue _arrayBuffer, out uint _byteOffset, out uint _byteLength) => Is32 ?
            Native32.JsGetTypedArrayInfo(_typedArray, out _arrayType, out _arrayBuffer, out _byteOffset, out _byteLength) :
            Native64.JsGetTypedArrayInfo(_typedArray, out _arrayType, out _arrayBuffer, out _byteOffset, out _byteLength);

        internal static JsStatusFlags JsGetContextOfObject(JsValue _obj, out JsContext _context) => Is32 ?
            Native32.JsGetContextOfObject(_obj, out _context) :
            Native64.JsGetContextOfObject(_obj, out _context);

        internal static JsStatusFlags JsGetContextData(JsContext _context, out IntPtr _data) => Is32 ?
            Native32.JsGetContextData(_context, out _data) :
            Native64.JsGetContextData(_context, out _data);

        internal static JsStatusFlags JsSetContextData(JsContext _context, IntPtr _data) => Is32 ?
            Native32.JsSetContextData(_context, _data) :
            Native64.JsSetContextData(_context, _data);

        internal static JsStatusFlags JsParseSerializedScriptWithHandle(JsSerializedScriptLoadSourceHandle _scriptLoadHandle, JsSerializedScriptUnloadHandle _scriptUnloadHandle, byte[] _buffer, JsSourceContext _sourceContext, string _sourceUrl, out JsValue _result) => Is32 ?
            Native32.JsParseSerializedScriptWithHandle(_scriptLoadHandle, _scriptUnloadHandle, _buffer, _sourceContext, _sourceUrl, out _result) :
            Native64.JsParseSerializedScriptWithHandle(_scriptLoadHandle, _scriptUnloadHandle, _buffer, _sourceContext, _sourceUrl, out _result);

        internal static JsStatusFlags JsRunSerializedScriptWithHandle(JsSerializedScriptLoadSourceHandle _scriptLoadHandle, JsSerializedScriptUnloadHandle _scriptUnloadHandle, byte[] _buffer, JsSourceContext _sourceContext, string _sourceUrl, out JsValue _result) => Is32 ?
            Native32.JsRunSerializedScriptWithHandle(_scriptLoadHandle, _scriptUnloadHandle, _buffer, _sourceContext, _sourceUrl, out _result) :
            Native64.JsRunSerializedScriptWithHandle(_scriptLoadHandle, _scriptUnloadHandle, _buffer, _sourceContext, _sourceUrl, out _result);

        internal static JsStatusFlags JsInitializeModuleRecord(JsModule _referencingModule, JsValue _normalizedSpecifier, out JsModule _moduleRecord) => Is32 ?
            Native32.JsInitializeModuleRecord(_referencingModule, _normalizedSpecifier, out _moduleRecord) :
            Native64.JsInitializeModuleRecord(_referencingModule, _normalizedSpecifier, out _moduleRecord);

        internal static JsStatusFlags JsSetModuleHostInfo(JsModule _module, JsFetchImportedModuleHandle _handle) => Is32 ?
            Native32.JsSetModuleHostInfo(_module, JsModuleFlags.FetchImportedModuleHandle, _handle) :
            Native64.JsSetModuleHostInfo(_module, JsModuleFlags.FetchImportedModuleHandle, _handle);

        internal static JsStatusFlags JsSetModuleHostInfo(JsModule module, JsValue value) => Is32 ?
            Native32.JsSetModuleHostInfo(module, JsModuleFlags.Url, value) :
            Native64.JsSetModuleHostInfo(module, JsModuleFlags.Url, value);

        internal static JsStatusFlags JsSetModuleHostInfo(JsModule _module, JsNotifyModuleReadyHandle _handle) => Is32 ?
            Native32.JsSetModuleHostInfo(_module, JsModuleFlags.NotifyModuleReadyHandle, _handle) :
            Native64.JsSetModuleHostInfo(_module, JsModuleFlags.NotifyModuleReadyHandle, _handle);

        internal static JsStatusFlags JsSetModuleHostInfo(JsModule _module, JsFetchImportedModuleFromScriptHandle _handle) => Is32 ?
            Native32.JsSetModuleHostInfo(_module, JsModuleFlags.FetchImportedModuleFromScriptHandle, _handle) :
            Native64.JsSetModuleHostInfo(_module, JsModuleFlags.FetchImportedModuleFromScriptHandle, _handle);

        internal static JsStatusFlags JsParseModuleSource(JsModule _requestModule, JsSourceContext _sourceContext, byte[] _script, uint _scriptLength, JsParseModuleFlags _sourceFlag, out JsValue _exception) => Is32 ?
            Native32.JsParseModuleSource(_requestModule, _sourceContext, _script, _scriptLength, _sourceFlag, out _exception) :
            Native64.JsParseModuleSource(_requestModule, _sourceContext, _script, _scriptLength, _sourceFlag, out _exception);

        internal static JsStatusFlags JsModuleEvaluation(JsModule _requestModule, out JsValue _result) => Is32 ?
            Native32.JsModuleEvaluation(_requestModule, out _result) :
            Native64.JsModuleEvaluation(_requestModule, out _result);

        internal static JsStatusFlags JsDiagStartDebugging(JsRuntime _runtime, JsDiagDebugEventHandle _debugEventHandle, IntPtr _handleState) => Is32 ?
            Native32.JsDiagStartDebugging(_runtime, _debugEventHandle, _handleState) :
            Native64.JsDiagStartDebugging(_runtime, _debugEventHandle, _handleState);

        internal static JsStatusFlags JsDiagStopDebugging(JsRuntime _runtime, out IntPtr _handleState) => Is32 ?
            Native32.JsDiagStopDebugging(_runtime, out _handleState) :
            Native64.JsDiagStopDebugging(_runtime, out _handleState);

        internal static JsStatusFlags JsDiagSetBreakpoint(uint _scriptId, uint _lineNumber, uint _column, out JsValue _breakpoint) => Is32 ?
            Native32.JsDiagSetBreakpoint(_scriptId, _lineNumber, _column, out _breakpoint) :
            Native64.JsDiagSetBreakpoint(_scriptId, _lineNumber, _column, out _breakpoint);

        internal static JsStatusFlags JsDiagRequestAsyncBreak(JsRuntime _jsRuntime) => Is32 ?
            Native32.JsDiagRequestAsyncBreak(_jsRuntime) :
            Native64.JsDiagRequestAsyncBreak(_jsRuntime);

        internal static JsStatusFlags JsDiagGetBreakpoints(out JsValue _breakpoints) => Is32 ?
            Native32.JsDiagGetBreakpoints(out _breakpoints) :
            Native64.JsDiagGetBreakpoints(out _breakpoints);

        internal static JsStatusFlags JsDiagRemoveBreakpoint(uint _breakpointId) => Is32 ?
            Native32.JsDiagRemoveBreakpoint(_breakpointId) :
            Native64.JsDiagRemoveBreakpoint(_breakpointId);

        internal static JsStatusFlags JsDiagGetScripts(out JsValue _scripts) => Is32 ?
            Native32.JsDiagGetScripts(out _scripts) :
            Native64.JsDiagGetScripts(out _scripts);

        internal static JsStatusFlags JsDiagEvaluate(JsValue _expression, uint _stackFrameIndex, JsParseScriptFlags _parseAttributes, bool _forceSetValueProp, out JsValue _eval) => Is32 ?
            Native32.JsDiagEvaluate(_expression, _stackFrameIndex, _parseAttributes, _forceSetValueProp, out _eval) :
            Native64.JsDiagEvaluate(_expression, _stackFrameIndex, _parseAttributes, _forceSetValueProp, out _eval);

        public static JsStatusFlags JsDiagGetObjectFromHandle(uint objectHandle, out JsValue handleObject) => Is32 ?
            Native32.JsDiagGetObjectFromHandle(objectHandle, out handleObject) :
            Native64.JsDiagGetObjectFromHandle(objectHandle, out handleObject);

        public static JsStatusFlags JsDiagGetSource(uint scriptId, out JsValue source) => Is32 ?
            Native32.JsDiagGetSource(scriptId, out source) :
            Native64.JsDiagGetSource(scriptId, out source);

        public static JsStatusFlags JsDiagGetFunctionPosition(JsValue function, out JsValue functionPosition) => Is32 ?
            Native32.JsDiagGetFunctionPosition(function, out functionPosition) :
            Native64.JsDiagGetFunctionPosition(function, out functionPosition);

        public static JsStatusFlags JsDiagGetStackTrace(out JsValue stackTrace) => Is32 ?
            Native32.JsDiagGetStackTrace(out stackTrace) :
            Native64.JsDiagGetStackTrace(out stackTrace);

        public static JsStatusFlags JsDiagGetStackProperties(uint stackFrameIndex, out JsValue properties) => Is32 ?
            Native32.JsDiagGetStackProperties(stackFrameIndex, out properties) :
            Native64.JsDiagGetStackProperties(stackFrameIndex, out properties);

        public static JsStatusFlags JsDiagGetProperties(uint objectHandle, uint fromCount, uint totalCount, out JsValue propertiesObject) => Is32 ?
            Native32.JsDiagGetProperties(objectHandle, fromCount, totalCount, out propertiesObject) :
            Native64.JsDiagGetProperties(objectHandle, fromCount, totalCount, out propertiesObject);

        public static JsStatusFlags JsDiagSetBreakOnException(JsRuntime runtimeHandle, JsDiagBreakOnExceptionFlags exceptionAttributes) => Is32 ?
            Native32.JsDiagSetBreakOnException(runtimeHandle, exceptionAttributes) :
            Native64.JsDiagSetBreakOnException(runtimeHandle, exceptionAttributes);

        public static JsStatusFlags JsDiagGetBreakOnException(JsRuntime runtimeHandle, out JsDiagBreakOnExceptionFlags exceptionAttributes) => Is32 ?
            Native32.JsDiagGetBreakOnException(runtimeHandle, out exceptionAttributes) :
            Native64.JsDiagGetBreakOnException(runtimeHandle, out exceptionAttributes);

        public static JsStatusFlags JsDiagSetStepType(JsDiagStepFlags stepType) => Is32 ?
            Native32.JsDiagSetStepType(stepType) :
            Native64.JsDiagSetStepType(stepType);

        internal static void ThrowIfError(JsStatusFlags _error)
        {
            if (_error != JsStatusFlags.OK)
            {
                switch (_error)
                {
                    case JsStatusFlags.InvalidArgument: throw new JsUsageException(_error, "无效参数");
                    case JsStatusFlags.NullArgument: throw new JsUsageException(_error, "空参数 (NULL)");
                    case JsStatusFlags.NoCurrentContext: throw new JsUsageException(_error, "没有当前上下文");
                    case JsStatusFlags.InExceptionState: throw new JsUsageException(_error, "运行时处于异常状态");
                    case JsStatusFlags.NotImplemented: throw new JsUsageException(_error, "方法未实现");
                    case JsStatusFlags.WrongThread: throw new JsUsageException(_error, "运行时在另一个线程上处于活动状态");
                    case JsStatusFlags.RuntimeInUse: throw new JsUsageException(_error, "运行时正在使用中");
                    case JsStatusFlags.BadSerializedScript: throw new JsUsageException(_error, "脚本序列化错误");
                    case JsStatusFlags.InDisabledState: throw new JsUsageException(_error, "运行时已禁用");
                    case JsStatusFlags.CannotDisableExecution: throw new JsUsageException(_error, "无法禁用执行");
                    case JsStatusFlags.AlreadyDebuggingContext: throw new JsUsageException(_error, "上下文已处于调试模式");
                    case JsStatusFlags.HeapEnumInProgress: throw new JsUsageException(_error, "正在进行堆枚举");
                    case JsStatusFlags.ArgumentNotObject: throw new JsUsageException(_error, "参数不是对象");
                    case JsStatusFlags.InProfileHandle: throw new JsUsageException(_error, "在profile句柄中");
                    case JsStatusFlags.InThreadServiceHandle: throw new JsUsageException(_error, "在线程服务聚丙种");
                    case JsStatusFlags.CannotSerializeDebugScript: throw new JsUsageException(_error, "无法序列化调试脚本");
                    case JsStatusFlags.AlreadyProfilingContext: throw new JsUsageException(_error, "已分析此上下文");
                    case JsStatusFlags.IdleNotEnabled: throw new JsUsageException(_error, "句柄未启用");
                    case JsStatusFlags.OutOfMemory: throw new JsEngineException(_error, "内存不足");
                    case JsStatusFlags.ScriptTerminated: throw new JsScriptException(_error, JsValue.Invalid, "脚本已终止");
                    case JsStatusFlags.ScriptEvalDisabled: throw new JsScriptException(_error, JsValue.Invalid, "运行时已禁用 'Eval' 功能");
                    case JsStatusFlags.Fatal: throw new JsFatalException(_error);
                    case JsStatusFlags.ScriptException:
                        {
                            var innerError = JsGetAndClearException(out var errorObject);
                            if (innerError != JsStatusFlags.OK)
                                throw new JsFatalException(innerError);
                            throw new JsScriptException(_error, errorObject, "脚本异常");
                        }
                    case JsStatusFlags.ScriptCompile:
                        {
                            var innerError = JsGetAndClearException(out var errorObject);
                            if (innerError != JsStatusFlags.OK)
                                throw new JsFatalException(innerError);
                            throw new JsScriptException(_error, errorObject, "编译错误");
                        }
                    default: throw new JsFatalException(_error);
                }
            }
        }
    }
}