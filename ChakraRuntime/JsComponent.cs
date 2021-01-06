using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChakraRuntime
{
    public abstract class JsComponent : JsProxyHandle, IDisposable
    {
        protected internal virtual string Name => this.GetType().GetCustomAttributes(false).Where(q => q is AsNameAttribute && !string.IsNullOrWhiteSpace(((AsNameAttribute)q).Name)).Select(q => (AsNameAttribute)q).FirstOrDefault().Name;

        public abstract void Dispose();
    }
}
