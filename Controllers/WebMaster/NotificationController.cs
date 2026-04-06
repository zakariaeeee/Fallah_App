using Fallah_App.Context;
using Fallah_App.Controllers.Client;
using Fallah_App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Fallah_App.Filters;
namespace Fallah_App.Controllers.WebMaster
{

 
    public class NotificationController : FilterNotifController
    {
        IMemoryCache memoryCache;
        MyContext db;
        public NotificationController(MyContext db, IMemoryCache memoryCache):base(db)
        {

            this.db = db;
            this.memoryCache = memoryCache;
        }
        
        public IActionResult List()
        {
            if(TempData["err"] != null)
            {
                ViewBag.eror = true;
            }
            return View(db.notifications.ToList());
        }
        public IActionResult seen()
        {
            int idLog = (int)HttpContext.Session.GetInt32("id");
            ViewBag.listNotifSeen = db.agriculteurNotifications.Include(u => u.Notification).Include(a => a.webMaster).Where(a => a.Agriculteur.Id == idLog && a.IsSeen==true).OrderBy(a => a.Notification.type == "risque").ThenBy(a => a.Notification.type == "moyen").ToList();
            return View();
        }
        public IActionResult unseen()
        {
            int idLog = (int)HttpContext.Session.GetInt32("id");
            ViewBag.listNotifUnseen = db.agriculteurNotifications.Include(u => u.Notification).Include(a => a.webMaster).Where(a => a.Agriculteur.Id == idLog && a.IsSeen == false).OrderBy(a => a.Notification.type == "risque").ThenBy(a => a.Notification.type == "moyen").ToList();
            return View();
        }
        public IActionResult ListNotification()
        {
            if (HttpContext.Session.GetInt32("id") != null)
            {
                int idLog = (int)HttpContext.Session.GetInt32("id");
                ViewBag.listNotif = db.agriculteurNotifications.Include(u => u.Notification).Include(a => a.webMaster).Where(a => a.Agriculteur.Id == idLog).ToList();
                /* List<AgriculteurNotification> listNotifAgr = db.agriculteurNotifications.Include(u => u.Notification).Include(a => a.webMaster).Where(a => a.Agriculteur.Id == idLog && a.IsSeen==false).ToList();
                 foreach(AgriculteurNotification ag in listNotifAgr)
                 {
                     ag.IsSeen= true;
                     db.agriculteurNotifications.Update(ag);
                     db.SaveChanges();
                 }
                */
                return View();
            }
            else RedirectToAction("login", "authentification");
            return View();
           
        }
        public IActionResult Selected(int id)
        {
            int idAgriculteur =  (int)HttpContext.Session.GetInt32("id");
            string previousUrl = HttpContext.Request.Headers["Referer"].ToString();

            var not = db.agriculteurNotifications.Include(u => u.Notification).Where(a => a.Agriculteur.Id == idAgriculteur && a.Notification.Id==id && a.IsSeen==false).FirstOrDefault();
            if(not!=null)
            {
                not.IsSeen = true;
                db.agriculteurNotifications.Update(not);
                db.SaveChanges();
                return Redirect(previousUrl);
            }
            return Ok();
        }
        public IActionResult Ajouter()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ajouter(Notification notification)
        {
            notification.Id_WebMaster = (int)HttpContext.Session.GetInt32("id");
            if (notification.TextFrancais == null)
            {
                ViewBag.nullType = "ce champ est obligatoire";
            }
            if (notification.TextFrancais==null)
            {
                ViewBag.nullTextFr = "ce champ est obligatoire";
            }
            if (notification.TextArabe == null)
            {
                ViewBag.nullTextAr = "ce champ est obligatoire";
            }
            
            if (notification.TextArabe!=null || notification.TextFrancais!=null)
                {
                  
                    db.notifications.Add(notification);
                    db.SaveChanges();
                    return RedirectToAction("List");
                }
            
               
           
           
           return View(notification);

        }
        public IActionResult Supprimer(int id)
        {
            Notification notification = db.notifications.Include(a=>a.AgriculteurNotifications).Where(a=>a.Id==id).FirstOrDefault();
            if (notification.AgriculteurNotifications.Count()!=0)
            {
                TempData["err"] = true;
                return RedirectToAction("List");


            }
            db.notifications.Remove(notification);
            db.SaveChanges();
            return RedirectToAction("List");
            
        }
        public IActionResult Modifier(int id)
        {
            Notification notification = db.notifications.Find(id);
            return View(notification);
        }
        [HttpPost]
        public IActionResult Modifier(Notification notification)
        {
            notification.Id_WebMaster = (int)HttpContext.Session.GetInt32("id");
            if (notification.TextFrancais == null)
            {
                ViewBag.nullTextFr = "ce champ est obligatoire";
            }
            if (notification.TextArabe == null)
            {
                ViewBag.nullTextAr = "ce champ est obligatoire";
            }

            if (notification.TextArabe != null || notification.TextFrancais != null)
            {
                db.notifications.Update(notification);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(notification);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Broadcast(Notification notif)
        {
            Notification notification = db.notifications.Find(notif.Id);
            List < Agriculteur > agriculteurs= db.users.OfType<Agriculteur>().ToList();
     


            foreach (Agriculteur u in agriculteurs)
            {
                AgriculteurNotification agriculteurNotification = new AgriculteurNotification();
                agriculteurNotification.Notification = notification;
                agriculteurNotification.Agriculteur = u;
                agriculteurNotification.IsSeen= false;
                agriculteurNotification.webmasterid = (int)HttpContext.Session.GetInt32("id");
                db.agriculteurNotifications.Add(agriculteurNotification);
            }                      
            db.SaveChanges();         
            return RedirectToAction("List");
        }
        public IActionResult Specefic()
        {
            ViewBag.listDesPlantes= db.plantes.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Specefic(Notification notif,Plante p)
        {
            Notification notification = db.notifications.Find(notif.Id);
            Plante plante =db.plantes.Find(p.Id);
            List<Agriculteur> agriculteurs = db.users.OfType<Agriculteur>().Include(a => a.Terres).ThenInclude(q => q.plantes).Where(a => a.Terres.Any(t => t.plantes.Any(p => p.Nom == plante.Nom))).ToList();
            if(agriculteurs!=null)
            {  
            foreach (Agriculteur u in agriculteurs)
            {
                AgriculteurNotification agriculteurNotification = new AgriculteurNotification();
                agriculteurNotification.Notification = notification;
                agriculteurNotification.Agriculteur = u;
                agriculteurNotification.IsSeen = false;
                agriculteurNotification.webmasterid = (int)HttpContext.Session.GetInt32("id");
                db.agriculteurNotifications.Add(agriculteurNotification);
            }
            }
            db.SaveChanges();
            return RedirectToAction("List");
        }

    }
}
