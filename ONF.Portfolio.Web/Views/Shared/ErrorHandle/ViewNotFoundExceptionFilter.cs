using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.TeamFoundation.Work.WebApi.Exceptions;

namespace ONF.Portfolio.Web.Views.Shared.ErrorHandle
{
    public class ViewNotFoundExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ViewNotFoundExceptionFilter> _logger;

        public ViewNotFoundExceptionFilter(ILogger<ViewNotFoundExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ViewNotFoundException)
            {
                context.Result = new RedirectToActionResult("Error404", "Base", null);
                context.ExceptionHandled = true;
            }
        }
    }
}
