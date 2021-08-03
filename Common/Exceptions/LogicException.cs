using System;
using System.Collections.Generic;
using System.Text;
using WebFramework.Common;

namespace Common.Exceptions
{
   public class LogicException : AppException
    {
        public LogicException(string message, ApiResualtStatusCode statusCode)
            : base(message, ApiResualtStatusCode.BadRequest)
        {

        }
    }
}
