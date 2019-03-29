using Brorep.Application.Exceptions;
using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Brorep.WebUI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = $"urn:brorep:error:{context.Exception.GetType().Name}:{Guid.NewGuid()}",
                Title = context.Exception.Message
            };

            if (context.Exception is ValidationException)
            {
                var vdp = new ValidationProblemDetails(((ValidationException)context.Exception).Failures);

                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                vdp.Status = (int)HttpStatusCode.BadRequest;
                vdp.Detail = "See errors for more information.";
                vdp.Instance = problemDetails.Instance;
                vdp.Title = problemDetails.Title;
                
                context.Result = new JsonResult(vdp);
                return;
            }
            if(context.Exception is AuthenticationException)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                context.Result = new JsonResult(problemDetails);
                return;
            }

            var code = HttpStatusCode.InternalServerError;

            if (context.Exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }

            context.HttpContext.Response.ContentType = "application/problem+json";
            context.HttpContext.Response.StatusCode = (int)code;

            problemDetails.Status = (int)code;

            context.Result = new JsonResult(problemDetails);

            //todo: log error
        }
    }
}
