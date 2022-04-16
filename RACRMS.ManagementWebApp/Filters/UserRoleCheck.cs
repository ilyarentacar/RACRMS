using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RACRMS.ManagementWebApp.Filters
{
    public class UserRoleCheck : ActionFilterAttribute
    {
        private string[] userRole { get; }

        public UserRoleCheck(params string[] userRole)
        {
            this.userRole = userRole;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Claim claim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            if (claim == null || !userRole.Any(x => x == claim.Value))
            {
                context.Result = new RedirectToActionResult("Error", "", null);
            }
        }
    }
}
