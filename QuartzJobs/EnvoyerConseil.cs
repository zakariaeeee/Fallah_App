using Fallah_App.Context;
using Fallah_App.Models;
using Fallah_App.Service;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Fallah_App.QuartzJobs
{
    public class EnvoyerConseil : IJob
    {
        MyContext db;

        public EnvoyerConseil(MyContext db)
        {
            this.db = db;
        }

        public  Task Execute(IJobExecutionContext context)
        {
           Notification notification = new Notification();
                notification.TextFrancais = "Conseil d'Aujourd'hui";
                notification.TextArabe = "نصيحة اليوم";
                notification.type = "conseil";
            db.notifications.Add(notification);
            db.SaveChanges();
            List<Agriculteur> agriculteurs = db.users.OfType<Agriculteur>().ToList();



            foreach (Agriculteur u in agriculteurs)
            {
                AgriculteurNotification agriculteurNotification = new AgriculteurNotification();
                agriculteurNotification.Notification = notification;
                agriculteurNotification.Agriculteur = u;
                agriculteurNotification.IsSeen = false;
                db.agriculteurNotifications.Add(agriculteurNotification);
            }
            db.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
