using Fallah_App.Context;
using Fallah_App.Filters;
using Fallah_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;

namespace Fallah_App.Controllers
{
    public class ConseilPlanteController : FilterNotifController
    {
        MyContext db;
        public ConseilPlanteController(MyContext db):base(db)
        {

            this.db = db;
        }
        public IActionResult Ajouter()
        {
            if (TempData["erorImage"] != null)
            {
                ViewBag.eror = true;
            }

            ViewBag.listeplante = db.plantes.ToList();
            return View();
            
        }
        [HttpPost]
        public IActionResult Ajouter(Models.ConseilPlante csp, int[] plante)
        {   if(csp.File == null && csp.Text_Francais==null && csp.Text_Arabe == null)
            {
                ViewBag.erornull = true;
                return View(csp);
            }
        if(csp.weatherCode == null)
            {
                ViewBag.w = true;
                return View(csp);
            }
            List<Plante> plantes = new List<Plante>();
            for (int i = 0; i < plante.Count(); i++)
            {
                Models.Plante pln = db.plantes.Where(c => c.Id == plante[i]).Include(c=>c.conseilPlantes).FirstOrDefault();
                plantes.Add(pln);
            }
            csp.plantes = plantes;
            if (csp.File != null)
            {
                String[] ext = { ".mp3", ".wav", ".aac", ".flac", ".ogg", ".m4a", ".wma", ".aiff" };
                String file_ext = Path.GetExtension(csp.File.FileName).ToLower();
                if (!ext.Contains(file_ext))
                {
                    TempData["erorImageM"] = true;
                    return RedirectToAction("Modifier");
                }
                if (ext.Contains(file_ext))
                {
                    String newName = Guid.NewGuid() + csp.File.FileName;
                    String path_file = Path.Combine("wwwroot/Audio", newName);
                    csp.audio = newName;
                    using (FileStream stream = System.IO.File.Create(path_file))
                    {
                        csp.File.CopyTo(stream);
                    }

                }

            }
           
            csp.Id_WebMaster = (int)HttpContext.Session.GetInt32("id");
           
            db.conseilPlantes.Add(csp);
            csp.Date_De_Creation = DateTime.Now;
            db.SaveChanges();

            return RedirectToAction("List");
        }
        public IActionResult List()
        {
          
            ViewBag.conseil = db.conseilPlantes.Include(c => c.plantes).Include(c => c.webMaster).ToList()   ;
            return View();
        }

        public IActionResult Supprimer(int id)
        {
            ConseilPlante cp = db.conseilPlantes.Include(c => c.plantes).Where(cc => cc.Id == id).FirstOrDefault();
            foreach(Plante p in cp.plantes.ToList()) {
                cp.plantes.Remove(p);
            }
            db.conseilPlantes.Remove(cp);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Modifier(int id)
        {
            if (TempData["erorImageM"] != null)
            {
                ViewBag.eror = true;
            }
            ViewBag.listeplante = db.plantes.ToList();
            return View(db.conseilPlantes.Include(c => c.webMaster).Include(c => c.plantes).Where(cc => cc.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public IActionResult Modifier(Models.ConseilPlante conseil, int[] plante)
        {
            if (conseil.Text_Arabe == null && conseil.Text_Francais == null && conseil.audio == null)
            {
                ViewBag.erornull = true;
                return View(conseil);
            }
            List<Plante> plantes = new List<Plante>();
            if (plante.Count() != 0)
            {
                ConseilPlante cs = db.conseilPlantes.Include(c => c.plantes).Where(cc => cc.Id == conseil.Id).FirstOrDefault();
                foreach (Plante ct in cs.plantes.ToList())
                {
                    cs.plantes.Remove(ct);
                }
                db.SaveChanges();
                db.Entry(cs).State = EntityState.Detached;
                for (int i = 0; i < plante.Count(); i++)
                {
                    Models.Plante pln= db.plantes.Where(c => c.Id == plante[i]).FirstOrDefault();
                    plantes.Add(pln);
                }
                conseil.plantes = plantes;
            }
            Models.ConseilPlante cf = db.conseilPlantes.Where(cc => cc.Id == conseil.Id).FirstOrDefault();
            db.Entry(cf).State = EntityState.Detached;
            if (conseil.File != null)
            {
                String[] ext = { ".mp3", ".wav", ".aac", ".flac", ".ogg", ".m4a", ".wma", ".aiff" };
                String file_ext = Path.GetExtension(conseil.File.FileName).ToLower();
                if (!ext.Contains(file_ext))
                {
                    TempData["erorImageM"] = true;
                    return RedirectToAction("Modifier");
                }
                if (ext.Contains(file_ext))
                {
                    String newName = Guid.NewGuid() + conseil.File.FileName;
                    String path_file = Path.Combine("wwwroot/Audio", newName);
                    conseil.audio = newName;
                    using (FileStream stream = System.IO.File.Create(path_file))
                    {
                        conseil.File.CopyTo(stream);
                    }

                }

            }
            else
            {
                conseil.audio = cf.audio;

            }
            conseil.Id_WebMaster = (int)HttpContext.Session.GetInt32("id");
            conseil.nbr_Modif = cf.nbr_Modif+1;

            db.conseilPlantes.Update(conseil);
              
            db.SaveChanges();
            return RedirectToAction("List");
        }
      
    }
}
