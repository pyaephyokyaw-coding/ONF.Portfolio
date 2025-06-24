using Microsoft.AspNetCore.Mvc;
using ONF.Portfolio.Web.Models;
using System.Diagnostics;

namespace ONF.Portfolio.Web.Controllers
{
    public class BaseController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        protected IActionResult HandleError(string message)
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
