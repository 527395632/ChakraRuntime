using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace ChakraRuntime
{
    [AttributeUsage(AttributeTargets.Class |
        AttributeTargets.Field |
        AttributeTargets.Property |
        AttributeTargets.Method |
        AttributeTargets.Constructor |
        AttributeTargets.Delegate,
        AllowMultiple = false, 
        Inherited = true)]
    public class AsNameAttribute : Attribute
    {
        public AsNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    internal class JsObject : RealProxy
    {
        private JsValue _stubValue = JsValue.Invalid;
        private Type _classToProxy = null;

        internal JsObject(JsValue stubData, Type classToProxy)
            : base(classToProxy)
        {
            if (!stubData.IsValid)
                throw new ArgumentException(stubData);
            _stubValue = stubData;
            _classToProxy = classToProxy;
        }

        public override IMessage Invoke(IMessage msg)
        {
            if (msg is IConstructionCallMessage)
            {
                var ctorReturnMessage = this.InitializeServerObject((IConstructionCallMessage)msg);
                SetStubData(this, ctorReturnMessage.ReturnValue);
                return ctorReturnMessage;
            }
            else if (msg is IMethodCallMessage)
            {
                var message = (IMethodCallMessage)msg;
                IMethodReturnMessage retMessage = null;
                try
                {
                    if (message.MethodName.StartsWith("get_"))
                    {
                        var name = message.MethodName.Substring(4, message.MethodName.Length - 4);
                        var asName = _classToProxy.GetProperty(name).GetCustomAttribute<AsNameAttribute>(true)?.Name;
                        if (!_stubValue.IsValid)
                            throw new Exception("无效的JsValue!");
                        if (!_stubValue.HasProperty(string.IsNullOrWhiteSpace(asName) ? name : asName))
                            throw new Exception("该属性在JsValue中不存在!");
                        var obj = _stubValue.GetProperty(string.IsNullOrWhiteSpace(asName) ? name : asName).ProxyObject(((MethodInfo)message.MethodBase).ReturnType);
                        return new ReturnMessage(obj, new object[0], 0, message.LogicalCallContext, message);
                    }
                    else if (message.MethodName.StartsWith("set_"))
                    {
                        var name = message.MethodName.Substring(4, message.MethodName.Length - 4);
                        var asName = _classToProxy.GetProperty(name).GetCustomAttribute<AsNameAttribute>(true)?.Name;
                        if (!_stubValue.IsValid)
                            throw new Exception("无效的JsValue!");
                        if (!_stubValue.HasProperty(string.IsNullOrWhiteSpace(asName) ? name : asName))
                            throw new Exception("该属性在JsValue中不存在!");
                        _stubValue.SetProperty(string.IsNullOrWhiteSpace(asName) ? name : asName, JsValue.FromObject(message.Args[0]), true);
                        return new ReturnMessage(null, new object[0], 0, message.LogicalCallContext, message);
                    }
                    else if (message.MethodName.Equals("FieldGetter"))
                    {
                        var name = message.Args[1].ToString();
                        var asName = _classToProxy.GetField(name).GetCustomAttribute<AsNameAttribute>(true)?.Name;
                        if (!_stubValue.IsValid)
                            throw new Exception("无效的JsValue!");
                        if (!_stubValue.HasProperty(string.IsNullOrWhiteSpace(asName) ? name : asName))
                            throw new Exception("该属性在JsValue中不存在!");
                        var obj = _stubValue.GetProperty(string.IsNullOrWhiteSpace(asName) ? name : asName).ProxyObject(((MethodInfo)message.MethodBase).ReturnType);
                        return new ReturnMessage(null, new object[] { message.Args[0], message.Args[1], obj }, 3, message.LogicalCallContext, message);
                    }
                    else if (message.MethodName.Equals("FieldSetter"))
                    {
                        var name = message.Args[1].ToString();
                        var asName = _classToProxy.GetField(name).GetCustomAttribute<AsNameAttribute>(true)?.Name;
                        if (!_stubValue.IsValid)
                            throw new Exception("无效的JsValue!");
                        if (!_stubValue.HasProperty(string.IsNullOrWhiteSpace(asName) ? name : asName))
                            throw new Exception("该属性在JsValue中不存在!");
                        _stubValue.SetProperty(string.IsNullOrWhiteSpace(asName) ? name : asName, JsValue.FromObject(message.Args[2]), true);
                        return new ReturnMessage(null, new object[] { message.Args[0], message.Args[1], null }, 3, message.LogicalCallContext, message);
                    }
                    else
                    {
                        var name = message.MethodName;
                        var asName = _classToProxy.GetMethod(name).GetCustomAttribute<AsNameAttribute>(true)?.Name;
                        if (!_stubValue.IsValid)
                            throw new Exception("无效的JsValue!");
                        if (!_stubValue.HasProperty(string.IsNullOrWhiteSpace(asName) ? name : asName))
                            throw new Exception("该属性在JsValue中不存在!");
                        var method = _stubValue.GetProperty(string.IsNullOrWhiteSpace(asName) ? name : asName);
                        var args = new List<JsValue>();
                        args.Add(method);
                        foreach (var item in message.Args)
                            args.Add(JsValue.FromObject(item));
                        var obj = method.Call(args.ToArray()).ProxyObject(((MethodInfo)message.MethodBase).ReturnType);
                        return new ReturnMessage(obj, new object[0], 0, message.LogicalCallContext, message);
                    }
                }
                catch (Exception e)
                {
                    retMessage = new ReturnMessage(e, message);
                }
                return retMessage;
            }
            return msg;
        }
    }
}
