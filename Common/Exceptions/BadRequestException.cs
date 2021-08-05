using System;
using System.Collections.Generic;
using System.Text;
using WebFramework.Common;

namespace Common.Exceptions
{
    public class BadRequestException : AppException
    {

        public BadRequestException()
    : base(ApiResualtStatusCode.BadRequest)
        {
        }

        public BadRequestException(string message)
            : base(ApiResualtStatusCode.BadRequest, message)
        {
        }

        public BadRequestException(object additionalData)
            : base(ApiResualtStatusCode.BadRequest, additionalData)
        {
        }

        public BadRequestException(string message, object additionalData)
            : base(ApiResualtStatusCode.BadRequest, message, additionalData)
        {
        }

        public BadRequestException(string message, Exception exception)
            : base(ApiResualtStatusCode.BadRequest, message, exception)
        {
        }

        public BadRequestException(string message, Exception exception, object additionalData)
            : base(ApiResualtStatusCode.BadRequest, message, exception, additionalData)
        {
        }
    }
}
