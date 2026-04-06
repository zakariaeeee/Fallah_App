using Fallah_App.Context;
using Fallah_App.Controllers.Client;
using Fallah_App.Filters;
using Fallah_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Fallah_App.Controllers.WebMaster
{
    public class WebMasterController : FilterNotifController
    {
        IMemoryCache memoryCache;
        MyContext db;
        public WebMasterController(MyContext db, IMemoryCache memoryCache):base(db)
        {
            this.db = db;
            this.memoryCache = memoryCache;

        }  
        public IActionResult List()
        {
           ViewBag.listWebMaster= db.users.OfType<Models.WebMaster>().ToList();
            return View();
        }

        public IActionResult Ajouter()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ajouter(Models.WebMaster webMaster)
        { 
                //recuperer le login dans la table  user
                Models.User login  = db.users.Where(D => D.Login == webMaster.Login).FirstOrDefault();
                Models.WebMaster Email = db.users.OfType<Models.WebMaster>().Where(D => D.Email == webMaster.Email).FirstOrDefault();
            //comparer le login inserer avec le login deja dans la  base
            bool t = false;
            if(webMaster.Password == null)
            {
                ViewData["samePasswordError"] = "le mot de passe est obligatoire";
            }
            if (webMaster.PasswordConfirmation != webMaster.Password)
            {
                ViewData["samePasswordError"] = "le mot de passe et la confirmation sont different";
                t = true;
            }
            if (login != null)
            {
                ViewData["erorLogin"] = "Ce login est déjà utilise";
                t = true;

            }
            if (Email != null)
            {
                ViewData["errorEmail"] = "Ce  Email est déjà utilise .";
                t = true;

            }

            if (webMaster.file == null)
            {
                ViewData["erorImage"] = "L'image est obligatoire";
                t = true;
            }
            else
            {
                String[] extt = { ".jpg", ".png", ".jpeg" };
                String file_extt = Path.GetExtension(webMaster.file.FileName).ToLower();
                if (!extt.Contains(file_extt))
                {
                    ViewData["erorImage"] = "Le choix de fichier doit être une image.";
                    t = true;
                }
            }
            if (t == true)
            {
                return View(webMaster);
            }


            webMaster.Password= InscriptionController.HashPasswordWithSalt(webMaster.Password);           
                String[] ext = { ".jpg", ".png", ".jpeg" };
                String file_ext = Path.GetExtension(webMaster.file.FileName).ToLower();
                if (ext.Contains(file_ext))
                {
                    String newName = Guid.NewGuid() + webMaster.file.FileName;
                    String path_file = Path.Combine("wwwroot/imageAdmin", newName);
                    webMaster.Image = newName;
                    
                    db.users.Add(webMaster);
                    db.SaveChanges();
                    using (FileStream stream = System.IO.File.Create(path_file))
                    {
                        webMaster.file.CopyTo(stream);
                    }
                   
                }
            return RedirectToAction("List");


        }

        
    }
}
