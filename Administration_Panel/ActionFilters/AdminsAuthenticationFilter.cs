using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Administration_Panel.ActionFilters
{
    public class AdminsAuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrEmpty(context.HttpContext.Session.GetString("AdminId")))
            {
                context.Result = new RedirectResult("/Authentication/Login");
            }
        }
    }
}