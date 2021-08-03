using System;
using System.Collections.Generic;
using System.Text;
using WebFramework.Common;

namespace Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message, ApiResualtStatusCode statusCode)
            : base(message, ApiResualtStatusCode.NotFound)
        {

        }
    }
}
