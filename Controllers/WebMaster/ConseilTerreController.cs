using Fallah_App.Context;
using Fallah_App.Filters;
using Fallah_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Fallah_App.Controllers.WebMaster
{
    public class ConseilTerreController : FilterNotifController
    {
        MyContext db;

        public ConseilTerreController(MyContext db):base(db)
        {
            this.db = db;
        }

        public IActionResult Ajouter()
        {
            if(TempData["erorImage"] != null)
            {
                ViewBag.eror = true;
            }
          
            ViewBag.Category=db.categoryTerres.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Ajouter(Models.ConseilTerre conseil, int[] categories)
        {
            if(conseil.Text_Arabe == null && conseil.Text_Francais==null && conseil.Audio == null)
            {
                ViewBag.erornull = true;
                return View(conseil);
            }
            List<CategoryTerre> categoryTerres = new List<CategoryTerre>();
           for(int i=0;i<categories.Count();i++)
            {
                Models.CategoryTerre categoryTerre = db.categoryTerres.Where(c=>c.Id==categories[i]).Include(c=>c.terres).Include(c=>c.ConseilTerres).FirstOrDefault();
                categoryTerres.Add(categoryTerre);
            }
           conseil.CategoryTerres= categoryTerres;
            if (conseil.File != null)
            {


                String[] ext = { ".mp3", ".wav", ".aac", ".flac", ".ogg", ".m4a", ".wma", ".aiff" };
                String file_ext = Path.GetExtension(conseil.File.FileName).ToLower();
                if (!ext.Contains(file_ext))
                {
                    TempData["erorImage"] = true;
                    return RedirectToAction("ajouter");
                }
                if (ext.Contains(file_ext))
                {
                    String newName = Guid.NewGuid() + conseil.File.FileName;
                    String path_file = Path.Combine("wwwroot/Audio", newName);
                    conseil.Audio = newName;
                    using (FileStream stream = System.IO.File.Create(path_file))
                    {
                        conseil.File.CopyTo(stream);
                    }
                    
                }

            }
            conseil.Id_WebMaster = (int)HttpContext.Session.GetInt32("id");

            db.conseilTerres.Add(conseil);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            ViewBag.conseilTerres = db.conseilTerres.Include(c=>c.webMaster).Include(c=>c.CategoryTerres).ToList();
            return View();
        }

        public IActionResult ListConseil()
        {
            int id = (int)HttpContext.Session.GetInt32("id");
            ViewBag.conseilTerres = db.conseilTerres
                .Include(t => t.CategoryTerres)
                    .ThenInclude(e => e.terres)
                        .ThenInclude(a => a.Agriculteur)
                .Where(a => a.CategoryTerres.Any(t => t.terres.Any(e => e.Agriculteur.Id == id)))
                .ToList(); ;
            return View();
        }

        public IActionResult Supprimer(int id)
        {
            ConseilTerre cs = db.conseilTerres.Include(c=>c.CategoryTerres).Where(cc => cc.Id == id).FirstOrDefault();
            foreach(CategoryTerre ct in cs.CategoryTerres.ToList())
            {
                cs.CategoryTerres.Remove(ct);
            }
            db.conseilTerres.Remove(cs);
            db.SaveChanges();

            return RedirectToAction("List");
        }

        public IActionResult Modifier(int id)
        {

            if (TempData["erorImageM"] != null)
            {
                ViewBag.eror = true;
            }
            ViewBag.Category = db.categoryTerres.ToList();
            return View(db.conseilTerres.Include(c => c.webMaster).Include(c => c.CategoryTerres).Where(cc => cc.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public IActionResult Modifier(Models.ConseilTerre conseil, int[] categories)
        {
            if (conseil.Text_Arabe == null && conseil.Text_Francais == null && conseil.Audio == null)
            {
                ViewBag.erornull = true;
                return View(conseil);
            }
            List<CategoryTerre> categoryTerres = new List<CategoryTerre>();

            if (categories.Count() != 0)
            {
                ConseilTerre cs = db.conseilTerres.Include(c => c.CategoryTerres).Where(cc => cc.Id == conseil.Id).FirstOrDefault();
                foreach (CategoryTerre ct in cs.CategoryTerres.ToList())
                {
                    cs.CategoryTerres.Remove(ct);
                }
                db.SaveChanges();
                db.Entry(cs).State = EntityState.Detached;
                for (int i = 0; i < categories.Count(); i++)
                {
                    Models.CategoryTerre categoryTerre = db.categoryTerres.Where(c => c.Id == categories[i]).FirstOrDefault();
                    categoryTerres.Add(categoryTerre);
                }
                conseil.CategoryTerres = categoryTerres;
            }
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
                    conseil.Audio = newName;
                    using (FileStream stream = System.IO.File.Create(path_file))
                    {
                        conseil.File.CopyTo(stream);
                    }

                }

            }
            else
            {
                Models.ConseilTerre cf = db.conseilTerres.Where(cc => cc.Id == conseil.Id).FirstOrDefault();
                conseil.Audio = cf.Audio;
                db.Entry(cf).State = EntityState.Detached;

            }
            conseil.Id_WebMaster = (int)HttpContext.Session.GetInt32("id");
           
            
            db.conseilTerres.Update(conseil);
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
