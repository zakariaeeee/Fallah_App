using Fallah_App.Context;
using Fallah_App.Filters;
using Fallah_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fallah_App.Controllers.Client
{
    public class CompteController : FilterNotifController
    {
        MyContext db;
        public CompteController(MyContext db):base(db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult changerpassword()
        {
            if (HttpContext.Session.GetInt32("id") == null) 
            {
                RedirectToAction("login","Authentification");
            }
            return View();
        }
        [HttpPost]
        public IActionResult changerpassword(string NvPassword,User user)
        {
                    string Password = InscriptionController.HashPasswordWithSalt(user.Password);
                    int id = (int)HttpContext.Session.GetInt32("id");
                    User u = (User)db.users.Where(us => us.Id == id).FirstOrDefault();
                    string Password1 = InscriptionController.HashPasswordWithSalt(user.Password);
                    u = (User)db.users.Where(us => us.Password == Password1).FirstOrDefault();
                    if (u == null)
                    {
                        ViewData["message1"] = "le mot de passe actuel et incorect";
                        return View();
                    }
                    if (NvPassword == user.PasswordConfirmation)
                    {
                        string Password_ = InscriptionController.HashPasswordWithSalt(NvPassword);
                        u.Password = Password_;
                        db.users.Update(u);
                        db.SaveChanges();
                        return RedirectToAction("login", "Authentification");
                    }
                    else
                    {
                        ViewData["message"] = "le mot de pass ou confirmation de mot de pass et incorect";
                    }
            return View();
        }
        public IActionResult MotDePasseOublier()
        {
            return View();
        }
        public IActionResult ListAgriculteur()
        {
            
            return View(db.users.OfType<Agriculteur>().ToList());
        }
        public IActionResult Information(int id)
        {
            return View(db.users.OfType<Agriculteur>().Where(a=>a.Id==id).FirstOrDefault());
        }
    }
}
