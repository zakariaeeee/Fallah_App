using System.ComponentModel.DataAnnotations.Schema;

namespace Fallah_App.Models
{
    public class Resultat
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date_De_Saisie { get; set; }
        public int Statut_Favorable { get; set; }

        public ConseilPlante? ConseilPlante { get; set; }
        [ForeignKey(nameof(ConseilPlante))]
        public int? Id_ConseilPlante { get; set; }
        public AgriculteurForme agriculteurForme { get; set; }

        [ForeignKey(nameof(agriculteurForme))]
        public int Id_agriculteurForme { get; set; }

        public ConseilTerre? ConseilTerre { get; set; }

        [ForeignKey(nameof(ConseilTerre))]
        public int? Id_ConseilTerre { get; set; }
    }
}
