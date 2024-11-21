using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Login
{
    public class LoginFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Login.IsActive)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"Controller", "Home"}, {"Action", "login"}
                });
            }
        }
    }
}