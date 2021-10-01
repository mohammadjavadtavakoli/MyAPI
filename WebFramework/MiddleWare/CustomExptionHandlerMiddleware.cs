using Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Api;
using WebFramework.Common;

namespace WebFramework.MiddleWare
{
    public static class CustomExptionHandlerMiddlewareExtention
    {
        public static void CustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExptionHandlerMiddleware>();
        }
    }
    public class CustomExptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        private readonly ILogger<CustomExptionHandlerMiddleware> logger;

        private readonly IHostingEnvironment env;

        public CustomExptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExptionHandlerMiddleware> logger, IHostingEnvironment hostingEnvironment)
        {
            this.next = next;
            this.logger = logger;
            this.env = hostingEnvironment;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string message = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            ApiResualtStatusCode apiResualtStatusCode = ApiResualtStatusCode.ServerError;

            try
            {
                await next(httpContext);
            }
            catch (AppException ex)
            {
                logger.LogError(ex, ex.Message);
                httpStatusCode = ex.HttpStatusCode;
                apiResualtStatusCode = ex.StatusCode;
                if (env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = ex.Message,
                        ["StackTrace"] = ex.StackTrace,
                    };
                    if (ex.InnerException != null)
                    {
                        dic.Add("InnerException.Exception", ex.InnerException.Message);
                        dic.Add("InnerException.StackTrace", ex.StackTrace);
                    }
                    if (ex.AdditionalData != null)
                    {
                        dic.Add("AdditionalData", JsonConvert.SerializeObject(ex.AdditionalData));
                    }

                    message = JsonConvert.SerializeObject(dic);
                }
                else
                {
                    message = ex.Message;
                }

                await WriteToResponceAsync();

                logger.LogError(ex, ex.Message);

            }

            catch (Exception Exception)
            {
                logger.LogError(Exception, Exception.Message);
                if (env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = Exception.Message,
                        ["StackTrace"] = Exception.StackTrace,
                    };
                    if (Exception.InnerException != null)
                    {
                        dic.Add("InnerException.Exception", Exception.InnerException.Message);
                        dic.Add("InnerException.StackTrace", Exception.StackTrace);
                    }

                    message = JsonConvert.SerializeObject(dic);
                }
                await WriteToResponceAsync();
                
            }

            async Task WriteToResponceAsync()
            {
                var apiresult = new ApiResult(false, apiResualtStatusCode, message);
                var json = JsonConvert.SerializeObject(apiresult);
                httpContext.Response.StatusCode = (int)httpStatusCode;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
