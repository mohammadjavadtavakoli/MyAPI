using System;
using System.Collections.Generic;
using System.Text;
using WebFramework.Common;

namespace Common.Exceptions
{
   public class AppException : Exception
    {


        public AppException(string message, ApiResualtStatusCode statusCode):base(message)
        {
            this.StatusCode = statusCode  ;
        }

        public ApiResualtStatusCode StatusCode { get; set; }

    }
}
