using System;
using System.Collections.Generic;
using System.Text;
using WebFramework.Common;

namespace Common.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string message, ApiResualtStatusCode statusCode)
            :base(message , ApiResualtStatusCode.BadRequest)
        {

        }
    }
}
