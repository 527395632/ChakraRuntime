using ChakraRuntime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ChakraRuntime.Components
{
    public class JsModuleFileAssemblyLoader : JsNativeModuleLoader
    {
        public JsModuleFileAssemblyLoader(JsValue name) : base(name)
        {
        }

        public override JsComponent[] GetComponents()
        {
            var loaders = new List<JsComponent>();
            foreach (var path in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Components"), "*.dll", SearchOption.AllDirectories))
            {
                foreach (var type in Assembly.Load(AssemblyName.GetAssemblyName(path)).GetTypes())
                {
                    if (!type.IsAbstract && !type.IsInterface && !type.IsSpecialName && type.IsSubclassOf(typeof(JsComponent)))
                    {
                        var namespances = type.GetCustomAttributes(true).Where(q => q is AsNameAttribute).Select(q => ((AsNameAttribute)q).Name).ToList();
                        namespances.Reverse();
                        var namespance = string.Join(".", namespances.ToArray());
                        if (namespance.StartsWith(Name, StringComparison.CurrentCultureIgnoreCase))
                            loaders.Add((JsComponent)Activator.CreateInstance(type));
                    }
                }
            }
            return loaders.ToArray();
        }
    }
}
