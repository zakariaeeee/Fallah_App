using Fallah_App.Context;
using Fallah_App.Filters;
using Fallah_App.Models;
using Fallah_App.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Fallah_App.Controllers.Client
{
    public class ConseilAujourdhuiController : FilterNotifController
    {
        MyContext db;

        public ConseilAujourdhuiController(MyContext db):base(db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            int id = (int)HttpContext.Session.GetInt32("id");
            Agriculteur agriculteur = db.users.OfType<Agriculteur>().Include(a => a.Terres).Where(a => a.Id == id).FirstOrDefault();
            List<Models.ConseilPlante> conseils = new List<Models.ConseilPlante>();
            foreach (Terre terre in agriculteur.Terres)
            {
                Meteo meteo = await Meteo.getMeteo(terre.latitude, terre.longitude);
                List<Models.ConseilPlante> cp = db.conseilPlantes.Include(c=>c.webMaster).Include(c => c.plantes).ThenInclude(c => c.terres).ThenInclude(c => c.Agriculteur).Where(c => c.Weather_Code == meteo.daily.weathercode[0] &&  c.plantes.Any(p => p.terres.Any(t => t.Agriculteur.Id == id))).ToList();
                conseils.AddRange(cp);
            }
            ViewBag.conseil = conseils;
            return View();
        }
    }
}
