using System.ComponentModel.DataAnnotations;

namespace Fallah_App.Models
{
    public class CategoryTerre
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "ce champ est obligatoire")]
        public string Attribut_De_Categorisation { get; set; }
        [Required(ErrorMessage = "ce champ est obligatoire")]
        public double Valeur_Max { get; set; }
        [Required(ErrorMessage = "ce champ est obligatoire")]
        public double Valeur_Min { get; set; }

        public List<ConseilTerre> ConseilTerres {get; set; }
        public List<Terre> terres {get; set; }

    }
}
