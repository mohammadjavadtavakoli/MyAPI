using System;
using System.Collections.Generic;
using System.Text;
using WebFramework.Common;

namespace Common.Exceptions
{
   public class LogicException : AppException
    {
       public LogicException() 
            : base(ApiResualtStatusCode.LogicError)
        {
        }

        public LogicException(string message) 
            : base(ApiResualtStatusCode.LogicError, message)
        {
        }

        public LogicException(object additionalData) 
            : base(ApiResualtStatusCode.LogicError, additionalData)
        {
        }

        public LogicException(string message, object additionalData) 
            : base(ApiResualtStatusCode.LogicError, message, additionalData)
        {
        }

        public LogicException(string message, Exception exception)
            : base(ApiResualtStatusCode.LogicError, message, exception)
        {
        }

        public LogicException(string message, Exception exception, object additionalData)
            : base(ApiResualtStatusCode.LogicError, message, exception, additionalData)
        {
        }
    }
}
