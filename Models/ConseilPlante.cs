using Fallah_App.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fallah_App.Models
{
    public class ConseilPlante
    {
        public int Id { get; set; }
        public string ?Text_Arabe { get; set; }
        public string ?Text_Francais { get; set; }
        public double ?Quantite_Eau { get; set; }
        public DateTime Date_De_Creation { get; set; }
        public DateTime? Date_De_Modification{ get; set; }
        public int nbr_Modif {get; set; }
        public string? Resultat_Attendue {get; set; }
        public double? Temperature_Max { get; set; }
        public double? Temperature_Min { get; set; }
        public double? Humidite_Max { get; set; }
        public double? Humidite_Min { get; set; }
        public double? Vent_Max { get; set; }
        public double? Vent_Min { get; set; }
        [NotMapped]
        public EnumWeatherCode weatherCode  { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string? audio { get; set; }

        public int Weather_Code { get; set; }
        public List<Resultat>? resultat { get; set; }
        public WebMaster webMaster { get; set; }

        [ForeignKey(nameof(webMaster))]
        public int Id_WebMaster { get; set; }
        public List<Plante> plantes {get; set; }

    }
}
