using System.ComponentModel.DataAnnotations.Schema;

namespace Fallah_App.Models
{
    public class WebMaster:User
    {
        public List<Demande> DemandeList { get; set; }
        public List<ConseilTerre> conseilTerres { get; set; }
        public List<ConseilPlante> conseilPlantes { get; set; }
        public List<Notification> notifications { get; set; }


    }
}
