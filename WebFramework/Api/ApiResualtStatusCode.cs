using System;
using System.Collections.Generic;
using System.Text;

namespace WebFramework.Api
{
    public enum ApiResualtStatusCode
    {
        Success = 0,
        ServerError = 1,
        BadRequest = 2,
        NotFound = 3,
        ListEmpty = 4
    }
}
