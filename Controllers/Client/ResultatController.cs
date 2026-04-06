using Fallah_App.Context;
using Fallah_App.Filters;
using Fallah_App.Migrations;
using Fallah_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Fallah_App.Controllers.Client
{
    public class ResultatController : FilterNotifController
    {
        MyContext db;
        public ResultatController(MyContext db):base(db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult listConseilPlant()
        {
            int id = (int)HttpContext.Session.GetInt32("id");
            ViewBag.conseilPlantes = db.conseilPlantes
                .Include(t => t.plantes)
                    .ThenInclude(e => e.terres)
                        .ThenInclude(a => a.Agriculteur).Include(c=>c.webMaster)
                .Where(a => a.plantes.Any(t => t.terres.Any(e => e.Agriculteur.Id == id)))
                .ToList();
            return View();
        }
        public IActionResult resultat(int id)
        {
           if(id==null)
            {
                return RedirectToAction("listConseilPlant");
            }
                return View();
            
        }
        [HttpPost]
        //khas filtre...
        public IActionResult resultat(Resultat r)
        {
            Resultat rs = new Resultat();
            rs.Date_De_Saisie= DateTime.Now;
            rs.Id_agriculteurForme = (int)HttpContext.Session.GetInt32("id");
            rs.ConseilPlante =db.conseilPlantes.Where(p=>p.Id== r.Id).FirstOrDefault();
            rs.Description = r.Description;
            rs.Statut_Favorable = r.Statut_Favorable;
            db.Add(rs);
            db.SaveChanges();
            return RedirectToAction("listConseilPlant");
        }
        public IActionResult MesResultat()
        {
            int id = (int)HttpContext.Session.GetInt32("id");
            ViewBag.list = db.resultats.Include(a => a.ConseilPlante).ThenInclude(b => b.plantes).Include(a=>a.agriculteurForme).Where(e => e.Id_agriculteurForme == id).ToList();
            return View();
        }
    }
}
