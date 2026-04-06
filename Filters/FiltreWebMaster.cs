using Microsoft.AspNetCore.Mvc.Filters;

namespace Fallah_App.les_filtres
{
    public class FiltreWebMaster: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Session.GetInt32("id") != null)
            {
                if (context.HttpContext.Session.GetString("role") != "WebMaster")
                {
                    context.HttpContext.Response.Redirect("/Authentification/login");
                }
            }
            else {
                context.HttpContext.Response.Redirect("/Authentification/login");
            }
        }
    }
}
