using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.ManagementWebApp.Filters
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UserCheck : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Response.HasStarted)
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.HttpContext.Session.SetString("Controller", context.RouteData.Values["Controller"].ToString());
                    context.HttpContext.Session.SetString("Action", context.RouteData.Values["Action"].ToString());

                    context.Result = new RedirectToActionResult("Index", "Login", null);
                }
            }
        }
    }
}
