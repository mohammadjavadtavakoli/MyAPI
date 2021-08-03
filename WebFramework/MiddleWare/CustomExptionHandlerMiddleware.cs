using Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public CustomExptionHandlerMiddleware(RequestDelegate next ,ILogger<CustomExptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch(AppException ex)
            {
                logger.LogError(ex, ex.Message);
                var apiresult = new ApiResult(false,ex.StatusCode);
                var json = JsonConvert.SerializeObject(apiresult);
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(json);
            }
           
            catch (Exception)
            {
                logger.LogError("خطایی رخ داده است");
                var apiresult = new ApiResult(false, ApiResualtStatusCode.ServerError);
                var json = JsonConvert.SerializeObject(apiresult);
               await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
