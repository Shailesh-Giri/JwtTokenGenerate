using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PracticeJWTApi.Filter
{
    public class PJWTAuthenticationAttribute : ActionFilterAttribute
    {
        private string[] _roles;

        public PJWTAuthenticationAttribute(params string[] roles)
        {
            _roles = roles; 
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            bool isAuthenticate = context.HttpContext.User.Identity.IsAuthenticated;

            if (!isAuthenticate)
            {
                context.Result = new RedirectToActionResult("SignIn", "Auth", new { message = "User Details not found" });
                return;
            }

            foreach (var role in _roles)
            {
                if (!context.HttpContext.User.IsInRole(role))
                {
                    context.Result = new RedirectToActionResult("Index", "Home", new { message = "Role Details not found" });
                    return;
                }
            }
        }

    }
}
