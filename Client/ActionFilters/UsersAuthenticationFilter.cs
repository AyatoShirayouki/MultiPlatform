using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Client.ActionFilters
{
	public class UsersAuthenticationFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (string.IsNullOrEmpty(context.HttpContext.Session.GetString("UserId")))
			{
				context.Result = new RedirectResult("/Authentication/Login");
			}
		}
	}
}
