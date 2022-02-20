using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Builder;
using WebApi.Core.Entity.BaseJsonResponse;

namespace WebApi.Core.GlobalErrorHandling
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.ContentType = "application/json";

                        BaseJsonResponseError baseJsonResponseError;

                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var message = contextFeature.Error.GetBaseException().Message;

                        baseJsonResponseError = new BaseJsonResponseError("Internal Server Error", message, context.Response.StatusCode.ToString());

                        BaseJsonResponse baseJsonResponse = new BaseJsonResponse();

                        baseJsonResponse.Header.Errors.Add(baseJsonResponseError);

                        var jsonString = JsonConvert.SerializeObject(baseJsonResponse);

                        await context.Response.WriteAsync(jsonString);
                    }
                });
            });
        }
    }
}
