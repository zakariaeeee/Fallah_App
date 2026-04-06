using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fallah_App.les_filtres
{
    public class FiltreAgriculteur: ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (context.HttpContext.Session.GetInt32("id") != null)
            {
                if (context.HttpContext.Session.GetString("role") == "WebMaster")
                {
                    context.Result = new RedirectResult("/Authentification/login");
                    return;
                }
            }
            else
            {
                context.Result = new RedirectResult("/Authentification/login");
                return;
            }
        }
    }
}
