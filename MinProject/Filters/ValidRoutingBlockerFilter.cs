using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MinProject.Filters
{
    public class ValidRoutingBlockerFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var currentLoginUser = context.HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(currentLoginUser))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Account", action = "Login" }));
            }

            base.OnActionExecuting(context);
        }


    }
}

