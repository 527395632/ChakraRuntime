using ChakraRuntime;
using ChakraRuntime.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var runtime = JsRuntime.Create())
            using (var context = runtime.CreateContext())
            {
                testDBG(runtime);

                test1(context);
                test2(context);
                test3(context);
                test4(context);

                Console.ReadLine();
            }
        }

        static void testDBG(JsRuntime runtime)
        {
            runtime.StartDebug((_event, _data, _state) =>
            {
                var jFileName = _data.GetProperty("fileName");

                if (jFileName.ValueType == JsValueFlags.String)
                {
                    var fileName = jFileName.ToString();

                    if (fileName.Equals("sample.js", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (_event == JsDiagDebugFlags.SourceCompile)
                        {
                            var scriptId = (int)_data.GetProperty("scriptId");

                            runtime.SetBreakpoint((uint)scriptId, 4, 0, out var breakpoint4);

                            runtime.SetBreakpoint((uint)scriptId, 7, 0, out var breakpoint7);
                        }
                    }
                }

                if (_event == JsDiagDebugFlags.BreakPoint || _event == JsDiagDebugFlags.StepComplete)
                {
                    // 获取this、Locals、Scopes、argument等信息
                    var prop = runtime.DiagGetStackProperties(0);
                    // 获取全局数据
                    var ret1 = runtime.DiagGetProperties(prop.Globals.Handle, 0, 99);
                    // 获取堆栈信息
                    var stackTrace = runtime.DiagGetStackTrace();

                    Console.WriteLine($"当前执行的文件为: { stackTrace[0].SourceCode.FileName }, 在第 { stackTrace[0].Line }行, 代码为: { stackTrace[0].SourceText }, 变量信息如下: { string.Join(";", prop.Locals.Select(q => $"{q.Name}={q.Value}").ToArray()) }");

                    // 单步执行
                    runtime.DiagSetStepType(JsDiagStepFlags.StepIn);
                }
            });
        }

        static void test1(JsContext context)
        {
            // 普通的Js代码运行
            var ret = context.RunScript("1+1");
            Console.WriteLine((int)ret);
        }

        static void test2(JsContext context)
        {
            // Js中的Date和C#的DateTime转换
            var time = (DateTime)context.RunScript("new Date()");
            Console.WriteLine(time);
        }

        static void test3(JsContext context)
        {
            // 往当前上下文的全局变量中设置一个"echo"方法
            context.Global.SetProperty("echo", JsValue.CreateFunction((_callee, _isConstructCall, _arguments, _argumentCount, _callbackData) =>
            {
                Console.WriteLine(_arguments[1].ToJsonString());
                return _arguments[1];
            }), true);
            context.RunScript("echo('{ age: 18 }')");
        }

        static void test4(JsContext context)
        {
            var app = context.LoadMoule("app", "sample.js", namespance =>
            {
                return new ComponentLoader[] { }; //componentDic.Where(q => q.Key.StartsWith(namespance, StringComparison.CurrentCultureIgnoreCase)).Select(q => q.Value).ToArray();
            }, fileName =>
            {
                return File.ReadAllText($@"{ AppDomain.CurrentDomain.BaseDirectory }\js\{ (fileName.EndsWith("js") ? fileName : $"{ fileName }.js") }");
            });
            var retValue = app.GetProperty("main").Call(app);
            Console.WriteLine(retValue.ToString());
        }
    }
}
