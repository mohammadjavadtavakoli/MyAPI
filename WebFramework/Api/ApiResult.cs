using System;
using System.Collections.Generic;
using System.Text;

namespace WebFramework.Api
{
  public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public ApiResualtStatusCode StatusCode { get; set; }
        public string  Message { get; set; }
     
    }

    public class ApiResult<TData>:ApiResult
    {
        public TData Data { get; set; }
    }

}
