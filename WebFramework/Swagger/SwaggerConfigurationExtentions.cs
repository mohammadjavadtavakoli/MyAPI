using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WebFramework.Swagger
{
    public static class SwaggerConfigurationExtentions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                var xmlDocPath = Path.Combine(AppContext.BaseDirectory, "MyAPI.xml");
                option.IncludeXmlComments(xmlDocPath, true);

                option.EnableAnnotations();
                option.DescribeAllEnumsAsStrings();
                option.SwaggerDoc("v1", new Info { Version = "v1", Title = "Doc-V1" });
                option.SwaggerDoc("v2", new Info { Version = "v2", Title = "Doc-V2" });

                #region Versioning
                // Remove version parameter from all Operations
                option.OperationFilter<RemoveVersionParameters>();

                //set version "api/v{version}/[controller]" from current swagger doc verion
                option.DocumentFilter<SetVersionInPaths>();

                //Seperate and categorize end-points by doc version
                option.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes<ApiVersionAttribute>(true)
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });
                #endregion

            });
        }

        public static void UseSwaggerAndUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "Doc-V1");
                option.SwaggerEndpoint("/swagger/v2/swagger.json", "Doc-V2");
            });

        }
    }
}
