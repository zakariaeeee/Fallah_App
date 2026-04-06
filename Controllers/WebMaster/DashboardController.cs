using Fallah_App.Context;
using Fallah_App.Filters;
using Fallah_App.Models;
using Fallah_App.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Fallah_App.Controllers.WebMaster
{
    public class DashboardController : FilterNotifController
    {
        MyContext db;
        public DashboardController(MyContext db):base(db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            /*   count of how many users we have   */
            ViewBag.CountAgriculteures = db.users.ToList().Where(item => item.GetType() == typeof(Agriculteur)).Count();
            ViewBag.CountAgriculteureFormes = db.users.ToList().Where(item => item.GetType() == typeof(AgriculteurForme)).Count();
            ViewBag.CountAdmins = db.users.ToList().Where(item => item.GetType() == typeof(Models.WebMaster)).Count();
            ViewBag.CountDemandes = db.demandes.Count();
            /*     count How Many Users Authentify Each Month */
            int Year = DateTime.Now.Year;
            ViewBag.Year = Year;
            List<int> countHowManyUsersAuthentifyEachMonth = new List<int>();
            ViewBag.MonthsNames = Months.getMonthNames();
            for (int i = 1; i <= 12; i++)
            {
                countHowManyUsersAuthentifyEachMonth.Add(db.users.Count(u => u.Date_Dernier_Auth.Value.Year == Year && u.Date_Dernier_Auth.Value.Month == i));
            }
            ViewBag.countHowManyUsersAuthentifyEachMonth = countHowManyUsersAuthentifyEachMonth;
            /*     count How Many chnages has been made on each plante */
            List<Plante> plantes= db.plantes.ToList();
            ViewBag.Plantes = plantes;
          // List<int> countHowManyChangesOnEachPlante = new List<int>();
            //foreach (Plante plante in plantes) 
            //{
                ViewBag.countHowManyChangesOnEachPlante = db.conseilPlantes.Select(p => p.nbr_Modif).ToList();
            //}
            // ViewBag.countHowManyChangesOnEachPlante = countHowManyChangesOnEachPlante;
            /*     count How Many terre and plantes de we have */
            ViewBag.CountTerres = db.terres.Count();
            ViewBag.CountPlantes = db.plantes.Count();
            return View();
        }
       
    }
}
