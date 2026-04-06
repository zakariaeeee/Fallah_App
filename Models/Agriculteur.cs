using static System.Net.Mime.MediaTypeNames;

namespace Fallah_App.Models
{
    public class Agriculteur:User
    {
        public string Telephone { get; set; }
        public Boolean IsValid { get; set; }
        public DateTime Date_De_Naissance { get; set; }
        public DateTime Date_Creation_Compte { get; set; }
        public List<AgriculteurNotification> AgriculteurNotifications { get; set; }
        public List<Terre> Terres { get; set; }

    }
}
