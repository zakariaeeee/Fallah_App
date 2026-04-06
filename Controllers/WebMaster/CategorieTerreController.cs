using Fallah_App.Models;
using Microsoft.AspNetCore.Mvc;
using Fallah_App.Context;
using System.Numerics;
using Microsoft.Extensions.Caching.Memory;
using Fallah_App.Filters;

namespace Fallah_App.Controllers.WebMaster
{
    public class CategorieTerreController : FilterNotifController
    {

        MyContext db;
        public CategorieTerreController(MyContext db):base(db)
        {
            
            this.db = db;
        }
        public IActionResult List()
        {
            return View(db.categoryTerres.ToList());
        }
        public IActionResult Ajouter()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ajouter(CategoryTerre c)
        {
               CategoryTerre categoryterre = db.categoryTerres.Where(ct=> ct.Attribut_De_Categorisation == c.Attribut_De_Categorisation).FirstOrDefault();
                if (categoryterre!= null)
                {
                    ViewBag.ERR = "cette categorie existe deja";
                    return View(c);
                }
                db.categoryTerres.Add(c);
                db.SaveChanges();
                return RedirectToAction("List");
           
            
        }
        public IActionResult Supprimer(int id)
        {
            if (id == 0 || !(id is int))
            {
                return RedirectToAction("Index", "ERROR404");
            }
            CategoryTerre ct = db.categoryTerres.Find(id);

            if (ct == null)
            {
                return RedirectToAction("Index", "ERROR404");


            }
            db.categoryTerres.Remove(ct);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Modifier(int id)
        {
            if (id == 0 || !(id is int))
            {
                return RedirectToAction("Index", "ERROR404");
            }
            CategoryTerre ct = db.categoryTerres.Find(id);

            if (ct == null)
            {
                return RedirectToAction("Index", "ERROR404");


            }
            return View(ct);
        }
        [HttpPost]
        public IActionResult Modifier(CategoryTerre ct)
        {
            db.categoryTerres.Update(ct);
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
