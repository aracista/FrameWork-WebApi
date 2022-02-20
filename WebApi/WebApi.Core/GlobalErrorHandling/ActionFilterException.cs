using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using WebApi.Core.Entity.BaseJsonResponse;

namespace WebApi.Core.GlobalErrorHandling
{
    public class ActionFilterException : IActionFilter, IExceptionFilter
    {
        Stopwatch sw = Stopwatch.StartNew();
        public void OnActionExecuting(ActionExecutingContext context)
        {
            CheckModelState(context);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnException(ExceptionContext context)
        {
            //DoLog(context, "FAILED");
            HandleException(context);

        }

        private void CheckModelState(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.HttpContext.Response.ContentType = "application/json";

                BaseJsonResponseError baseJsonResponseError;

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                BaseJsonResponse baseJsonResponse = new BaseJsonResponse();

                foreach (var state in context.ModelState)
                {
                    if (state.Value.ValidationState == ModelValidationState.Invalid)
                    {
                        baseJsonResponseError = new BaseJsonResponseError("Invalid parameter", state.Value.Errors[0].ErrorMessage, "400");
                        baseJsonResponse.Header.Errors.Add(baseJsonResponseError);
                    }
                }

                context.Result = new JsonResult(baseJsonResponse);
            }
        }

        private void HandleException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception.Data.Count > 0)
                {
                    context.HttpContext.Response.ContentType = "application/json";

                    BaseJsonResponseError baseJsonResponseError;

                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    string cause = context.Exception.Data["Cause"] != null ? context.Exception.Data["Cause"].ToString() : context.Exception.Data["Message"].ToString(); // kalau kosong akan diisi sama dengan message karena ada bberapa modul memakai ini
                    string message = context.Exception.Data["Message"] != null ? context.Exception.Data["Message"].ToString() : "";
                    string statusCode = context.Exception.Data["StatusCode"] != null ? context.Exception.Data["StatusCode"].ToString() : "";

                    baseJsonResponseError = new BaseJsonResponseError(message, cause, statusCode);

                    BaseJsonResponse baseJsonResponse = new BaseJsonResponse();

                    baseJsonResponse.Header.Errors.Add(baseJsonResponseError);

                    context.Result = new JsonResult(baseJsonResponse);

                    context.ExceptionHandled = true;
                }
            }
        }
    }
}
