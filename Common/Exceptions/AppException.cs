using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WebFramework.Common;

namespace Common.Exceptions
{
   public class AppException : Exception
    {

        public AppException()
           : this(ApiResualtStatusCode.ServerError)
        {
        }

        public AppException(ApiResualtStatusCode statusCode)
            : this(statusCode, null)
        {
        }

        public AppException(string message)
            : this(ApiResualtStatusCode.ServerError, message)
        {
        }

        public AppException(ApiResualtStatusCode statusCode, string message)
            : this(statusCode, message, HttpStatusCode.InternalServerError)
        {
        }

        public AppException(string message, object additionalData)
            : this(ApiResualtStatusCode.ServerError, message, additionalData)
        {
        }

        public AppException(ApiResualtStatusCode statusCode, object additionalData)
            : this(statusCode, null, additionalData)
        {
        }

        public AppException(ApiResualtStatusCode statusCode, string message, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, additionalData)
        {
        }

        public AppException(ApiResualtStatusCode statusCode, string message, HttpStatusCode httpStatusCode)
            : this(statusCode, message, httpStatusCode, null)
        {
        }

        public AppException(ApiResualtStatusCode statusCode, string message, HttpStatusCode httpStatusCode, object additionalData)
            : this(statusCode, message, httpStatusCode, null, additionalData)
        {
        }

        public AppException(string message, Exception exception)
            : this(ApiResualtStatusCode.ServerError, message, exception)
        {
        }

        public AppException(string message, Exception exception, object additionalData)
            : this(ApiResualtStatusCode.ServerError, message, exception, additionalData)
        {
        }

        public AppException(ApiResualtStatusCode statusCode, string message, Exception exception)
            : this(statusCode, message, HttpStatusCode.InternalServerError, exception)
        {
        }

        public AppException(ApiResualtStatusCode statusCode, string message, Exception exception, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, exception, additionalData)
        {
        }

        public AppException(ApiResualtStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception)
            : this(statusCode, message, httpStatusCode, exception, null)
        {
        }

        public AppException(ApiResualtStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception, object additionalData)
            : base(message, exception)
        {
            StatusCode = statusCode;
            HttpStatusCode = httpStatusCode;
            AdditionalData = additionalData;
        }

        public ApiResualtStatusCode StatusCode { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public object AdditionalData { get; set; }


    }
}
