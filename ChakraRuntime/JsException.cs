using System;

namespace ChakraRuntime
{
    public class JsException : Exception
    {
        public JsException(JsStatusFlags _code) :
            this(_code, "运行时发生致命异常")
        {
        }

        public JsException(JsStatusFlags _code, string _message) : base(_message)
        {
            ErrorCode = _code;
        }

        protected JsException(string message, Exception _innerException) : base(message, _innerException)
        {
            if (message != null)
            {
                ErrorCode = (JsStatusFlags)HResult;
            }
        }

        public JsStatusFlags ErrorCode { get; }
    }

    public sealed class JsEngineException : JsException
    {
        public JsEngineException(JsStatusFlags _code) :
            this(_code, "运行时发生致命异常")
        {
        }

        public JsEngineException(JsStatusFlags _code, string _message) :
            base(_code, _message)
        {
        }
    }

    public sealed class JsFatalException : JsException
    {
        public JsFatalException(JsStatusFlags _code) :
            this(_code, "运行时发生致命异常")
        {
        }

        public JsFatalException(JsStatusFlags _code, string _message) :
            base(_code, _message)
        {
        }
    }

    public sealed class JsUsageException : JsException
    {
        public JsUsageException(JsStatusFlags _code) :
            this(_code, "运行时发生致命异常")
        {
        }

        public JsUsageException(JsStatusFlags _code, string _message) :
            base(_code, _message)
        {
        }
    }

    public sealed class JsScriptException : JsException
    {
        private readonly JsValue p_error;

        public JsScriptException(JsStatusFlags _code, JsValue _error) :
            this(_code, _error, "JavaScript Exception")
        {
        }

        public JsScriptException(JsStatusFlags _code, JsValue _error, string message) :
            base(_code, message)
        {
            p_error = _error;
        }

        public string ScriptMessage
        {
            get
            {
                if (!p_error.IsValid)
                    throw new NullReferenceException();
                return p_error.GetProperty("message");
            }
        }

        public string ScriptDescription
        {
            get
            {
                if (!p_error.IsValid)
                    throw new NullReferenceException();
                return p_error.GetProperty("description");
            }
        }

        public int ScriptNumber
        {
            get
            {
                if (!p_error.IsValid)
                    throw new NullReferenceException();
                return p_error.GetProperty("number");
            }
        }

        public string ScriptStack
        {
            get
            {
                if (!p_error.IsValid)
                    throw new NullReferenceException();
                return p_error.GetProperty("stack");
            }
        }
    }
}