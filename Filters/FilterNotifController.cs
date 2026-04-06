using Fallah_App.Context;
using Fallah_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Fallah_App.Filters
{
    public class FilterNotifController : Controller
    {
        MyContext data;

        public FilterNotifController(MyContext data)
        {
            this.data = data;

        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {

            HttpContext httpContext = context.HttpContext;
            if (httpContext.Session.GetInt32("id") != null)
            {

                int idLog = (int)httpContext.Session.GetInt32("id");
                context.HttpContext.Items["listNotif"] = data.agriculteurNotifications.Include(u => u.Notification).Include(a => a.webMaster).Where(a => a.Agriculteur.Id == idLog && a.IsSeen == false).Take(5).ToList();
                context.HttpContext.Items["count"] = data.agriculteurNotifications.Where(u => u.Agriculteur.Id == idLog && u.IsSeen == false).Count();

            }
            else { context.Result = new RedirectResult("/Authentification/login"); return; }


        }



        public override void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
