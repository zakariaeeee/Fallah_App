using Fallah_App.Context;
using Fallah_App.les_filtres;
using Fallah_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Mail;

namespace Fallah_App.Controllers.WebMaster
{
    public class DemandesController : Controller
    {
        MyContext db;
        public DemandesController(MyContext db)
        {
            this.db = db;

        }
        public IActionResult List()
        {
            
            return View(db.demandes.ToList());
            //return View(db.demandes.ToList());
        }
        public IActionResult Information(int id)
        {  
            if(id==null || !(id is int))
            {
                return RedirectToAction("Index", "ERROR404");
            }
            Demande demande = db.demandes.Where(demande => demande.Id == id).FirstOrDefault();
            if (demande == null)
            {
                return RedirectToAction("Index", "ERROR404");
            }
            return View(demande);
        }

        public IActionResult Accepter(int id)
        {
            if (id == null || !(id is int))
            {
                return RedirectToAction("Index", "ERROR404");
            }
            Demande demande = db.demandes.Where(demande => demande.Id == id).FirstOrDefault();
            if (demande == null)
            {
                return RedirectToAction("Index", "ERROR404");
            }
            if (demande.forme==true)
            {
                AgriculteurForme agriculteurForme = new AgriculteurForme();
                agriculteurForme.Nom = demande.Nom;
                agriculteurForme.Telephone = demande.Telephone;
                agriculteurForme.Prenom = demande.Prenom;
                agriculteurForme.Date_De_Naissance = demande.Date_De_Naissance;
                agriculteurForme.Login = demande.Login;
                agriculteurForme.Password = demande.Password;
                agriculteurForme.Email = demande.Email;
                agriculteurForme.Image = demande.Image;
                agriculteurForme.Date_Creation_Compte=DateTime.Now;
                db.users.Add(agriculteurForme);
            }
            else
            {
                Agriculteur agriculteur = new Agriculteur();
                agriculteur.Nom = demande.Nom;
                agriculteur.Prenom = demande.Prenom;
                agriculteur.Telephone = demande.Telephone;
                agriculteur.Date_De_Naissance = demande.Date_De_Naissance;
                agriculteur.Login = demande.Login;
                agriculteur.Password = demande.Password;
                agriculteur.Email = demande.Email;
                agriculteur.Image = demande.Image;
                agriculteur.Date_Creation_Compte = DateTime.Now;

                db.users.Add(agriculteur);
            }

            db.demandes.Remove(demande);
            db.SaveChanges();
            EnvoyerEmailAcceptation(demande.Email);
            return RedirectToAction("List");
        }

        public IActionResult Refuser(int id)
        {
            if (id == null || !(id is int))
            {
                return RedirectToAction("Index", "ERROR404");
            }
            Demande demande = db.demandes.Find(id);
            if (demande == null)
            {
                return RedirectToAction("Index", "ERROR404");
            }
            demande.Statut = true;
            //demande.Id_WebMaster = (int)HttpContext.Session.GetInt32("webMasterID");
            db.demandes.Update(demande);
            db.SaveChanges();

            return RedirectToAction("List");
        }

        public void EnvoyerEmailAcceptation(string mail)
        {

            string from = "falla7app@gmail.com";
            string subject = "Acceptation de votre demande de compte";
            string body = "Je suis ravi(e) de vous informer que votre demande de compte a été acceptée avec succès. " +
                "Nous sommes heureux de vous accueillir en tant que nouveau client chez FallahApp." +
                "Votre nouveau compte est maintenant actif et vous pouvez accéder à toutes les fonctionnalités et services proposés par notre entreprise. " +
                "Nous sommes convaincus que vous apprécierez la qualité de nos produits et services. " +
                "N'hésitez pas à nous contacter si vous avez des questions ou si vous avez besoin d'aide pour utiliser votre compte. Nous sommes là pour vous aider à tout moment." +
                "Nous vous remercions de votre confiance et espérons vous offrir une expérience exceptionnelle chez FallahApp." +
                "Cordialement,";

            MailMessage message = new MailMessage(from,mail, subject, body);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential("falla7app@gmail.com", "ifkbjdfapakuqixv");
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
        }
    }
}
