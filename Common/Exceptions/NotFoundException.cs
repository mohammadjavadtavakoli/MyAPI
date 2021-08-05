using System;
using System.Collections.Generic;
using System.Text;
using WebFramework.Common;

namespace Common.Exceptions
{
    public class NotFoundException : AppException
    {
       
        public NotFoundException()
           : base(ApiResualtStatusCode.NotFound)
        {
        }

        public NotFoundException(string message)
            : base(ApiResualtStatusCode.NotFound, message)
        {
        }

        public NotFoundException(object additionalData)
            : base(ApiResualtStatusCode.NotFound, additionalData)
        {
        }

        public NotFoundException(string message, object additionalData)
            : base(ApiResualtStatusCode.NotFound, message, additionalData)
        {
        }

        public NotFoundException(string message, Exception exception)
            : base(ApiResualtStatusCode.NotFound, message, exception)
        {
        }

        public NotFoundException(string message, Exception exception, object additionalData)
            : base(ApiResualtStatusCode.NotFound, message, exception, additionalData)
        {
        }
    }
}
