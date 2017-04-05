using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Filters;

namespace Boma.Rs.Api.StartupConfigurations.Filters
{
    public class CustomErrorFilter: ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null) return;

            var customError = new CustomResponseError(actionExecutedContext.Exception);

            actionExecutedContext.Response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new ObjectContent<CustomResponseError>(customError, new JsonMediaTypeFormatter())
            };
        }
    }

    public class CustomResponseError
    {
        public string ErrorDescription { get; set; }
        public Exception ExceptionContent { get; set; }

        public CustomResponseError(string message)
        {
            ErrorDescription = message;
        }

        public CustomResponseError(Exception exception)
        {
            ErrorDescription = exception.Message;
            ExceptionContent = exception;
        }
    }
}