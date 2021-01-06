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
                        var asNameAttribute = _classToProxy.GetProperty(name).GetCustomAttributes(true).FirstOrDefault();
                        if (asNameAttribute != null)
                            name = ((AsNameAttribute)asNameAttribute).Name;
                        if (!_stubValue.IsValid)
                            throw new Exception("无效的JsValue!");
                        if (!_stubValue.HasProperty(name))
                            throw new Exception("该属性在JsValue中不存在!");
                        var obj = _stubValue.GetProperty(name).ProxyObject(((MethodInfo)message.MethodBase).ReturnType);
                        return new ReturnMessage(obj, new object[0], 0, message.LogicalCallContext, message);
                    }
                    else if (message.MethodName.StartsWith("set_"))
                    {
                        var name = message.MethodName.Substring(4, message.MethodName.Length - 4);
                        var asNameAttribute = _classToProxy.GetProperty(name).GetCustomAttributes(true).FirstOrDefault();
                        if (asNameAttribute != null)
                            name = ((AsNameAttribute)asNameAttribute).Name;
                        if (!_stubValue.IsValid)
                            throw new Exception("无效的JsValue!");
                        if (!_stubValue.HasProperty(name))
                            throw new Exception("该属性在JsValue中不存在!");
                        _stubValue.SetProperty(name, JsValue.FromObject(message.Args[0]), true);
                        return new ReturnMessage(null, new object[0], 0, message.LogicalCallContext, message);
                    }
                    else if (message.MethodName.Equals("FieldGetter"))
                    {
                        var name = message.Args[1].ToString();
                        var asNameAttribute = _classToProxy.GetProperty(name).GetCustomAttributes(true).FirstOrDefault();
                        if (asNameAttribute != null)
                            name = ((AsNameAttribute)asNameAttribute).Name;
                        if (!_stubValue.IsValid)
                            throw new Exception("无效的JsValue!");
                        if (!_stubValue.HasProperty(name))
                            throw new Exception("该属性在JsValue中不存在!");
                        var obj = _stubValue.GetProperty(name).ProxyObject(((MethodInfo)message.MethodBase).ReturnType);
                        return new ReturnMessage(null, new object[] { message.Args[0], message.Args[1], obj }, 3, message.LogicalCallContext, message);
                    }
                    else if (message.MethodName.Equals("FieldSetter"))
                    {
                        var name = message.Args[1].ToString();
                        var asNameAttribute = _classToProxy.GetProperty(name).GetCustomAttributes(true).FirstOrDefault();
                        if (asNameAttribute != null)
                            name = ((AsNameAttribute)asNameAttribute).Name;
                        if (!_stubValue.IsValid)
                            throw new Exception("无效的JsValue!");
                        if (!_stubValue.HasProperty(name))
                            throw new Exception("该属性在JsValue中不存在!");
                        _stubValue.SetProperty(name, JsValue.FromObject(message.Args[2]), true);
                        return new ReturnMessage(null, new object[] { message.Args[0], message.Args[1], null }, 3, message.LogicalCallContext, message);
                    }
                    else
                    {
                        var name = message.MethodName;
                        var asNameAttribute = _classToProxy.GetProperty(name).GetCustomAttributes(true).FirstOrDefault();
                        if (asNameAttribute != null)
                            name = ((AsNameAttribute)asNameAttribute).Name;
                        if (!_stubValue.IsValid)
                            throw new Exception("无效的JsValue!");
                        if (!_stubValue.HasProperty(name))
                            throw new Exception("该属性在JsValue中不存在!");
                        var method = _stubValue.GetProperty(name);
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
